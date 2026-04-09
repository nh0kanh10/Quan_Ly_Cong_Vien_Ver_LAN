using Dapper;
using DaiNamWeb.Api.Models;
using System.Data;

namespace DaiNamWeb.Api.Data;

public class TicketRepo
{
    private readonly DbContext _db;
    public TicketRepo(DbContext db) => _db = db;

    #region Gate Config (Khu vực + Trò chơi cho Gate Scanner)

    public async Task<IEnumerable<GateKhuVuc>> GetGateKhuVuc()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<GateKhuVuc>(@"
            SELECT Id, TenKhuVuc FROM KhuVuc
            WHERE TrangThai = N'Hoạt động'
            ORDER BY Id");
    }

    public async Task<IEnumerable<GateThietBi>> GetGateThietBi(int? idKhuVuc)
    {
        using var conn = _db.CreateConnection();
        var sql = @"
            SELECT Id, TenThietBi, IdKhuVuc FROM DanhSachThietBi
            WHERE LoaiThietBi = 'TroChoi' AND TrangThai = 'HoatDong'";
        if (idKhuVuc.HasValue)
            sql += " AND IdKhuVuc = @idKhuVuc";
        sql += " ORDER BY TenThietBi";
        return await conn.QueryAsync<GateThietBi>(sql, new { idKhuVuc });
    }

    #endregion

    #region Get Products & Pricing

    public async Task<IEnumerable<TicketProduct>> GetProducts()
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<TicketProduct>(@"
            SELECT sp.Id, sp.Ten AS TenSanPham, sp.MoTa, sp.DonGia,
                   sp.IdKhuVuc, kv.TenKhuVuc,
                   COALESCE(sv.SoLuotQuyDoi, 1) AS SoLuotQuyDoi
            FROM SanPham sp
            LEFT JOIN KhuVuc kv ON sp.IdKhuVuc = kv.Id
            LEFT JOIN SanPham_Ve sv ON sv.IdSanPham = sp.Id
            WHERE sp.LoaiSanPham = N'Ve'
              AND sp.TrangThai = N'DangBan'
              AND sp.IsDeleted = 0
            ORDER BY sp.DonGia");
    }

    public async Task<TicketPrice> GetPrice(int idSanPham, DateTime date)
    {
        using var conn = _db.CreateConnection();

        var isHoliday = await conn.ExecuteScalarAsync<int>(
            "SELECT COUNT(1) FROM CauHinhNgayLe WHERE Ngay = @date",
            new { date = date.Date });

        var now = DateTime.Now.TimeOfDay;
        var bangGia = await conn.QueryFirstOrDefaultAsync<(decimal GiaNgayThuong, decimal GiaCuoiTuan, decimal GiaNgayLe)>(@"
            SELECT GiaNgayThuong, GiaCuoiTuan, GiaNgayLe
            FROM BangGia
            WHERE IdSanPham = @idSanPham
              AND GioBatDau <= @now AND GioKetThuc >= @now
            ORDER BY Id DESC",
            new { idSanPham, now });

        if (bangGia.GiaNgayThuong == 0)
        {
            var fallback = await conn.ExecuteScalarAsync<decimal>(
                "SELECT DonGia FROM SanPham WHERE Id = @idSanPham", new { idSanPham });
            return new TicketPrice { IdSanPham = idSanPham, GiaApDung = fallback, LoaiGia = "Thuong" };
        }

        if (isHoliday > 0)
            return new TicketPrice { IdSanPham = idSanPham, GiaApDung = bangGia.GiaNgayLe, LoaiGia = "NgayLe" };

        if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday)
            return new TicketPrice { IdSanPham = idSanPham, GiaApDung = bangGia.GiaCuoiTuan, LoaiGia = "CuoiTuan" };

        return new TicketPrice { IdSanPham = idSanPham, GiaApDung = bangGia.GiaNgayThuong, LoaiGia = "Thuong" };
    }

    #endregion

    #region Purchase

    public async Task<PurchaseResult> Purchase(PurchaseRequest req)
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        using var tx = conn.BeginTransaction();

        try
        {
            var maDH = $"WEB{DateTime.Now:yyMMddHHmmss}";

            // 1. Get real IdKhachHang and LoaiKhach from TaiKhoanWeb -> KhachHang
            var khInfo = await conn.QueryFirstOrDefaultAsync<(int? Id, int DiemTichLuy, string LoaiKhach)>(@"
                SELECT kh.Id, kh.DiemTichLuy, kh.LoaiKhach 
                FROM TaiKhoanWeb tk 
                JOIN KhachHang kh ON tk.IdKhachHang = kh.Id
                WHERE tk.Id = @IdTaiKhoanWeb",
                new { req.IdTaiKhoanWeb }, tx);

            var idKhachHang = khInfo.Id;

            // 2. Use CalculateOrder logic
            var calcResult = await CalculateOrder(new CalculateOrderRequest(req.Items, req.IdTaiKhoanWeb, req.DiemSuDung > 0));

            decimal tongTien = calcResult.TongTienGoc;
            decimal tienGiam = calcResult.TienGiam;
            decimal soTienThucThu = calcResult.ThucThu;

            int diemSuDung = calcResult.DiemSuDung;

            // --- XỬ LÝ SỬ DỤNG ĐIỂM (TIEU DIEM) ---
            if (diemSuDung > 0 && idKhachHang != null)
            {
                int soDuSauGiam = khInfo.DiemTichLuy - diemSuDung;
                await conn.ExecuteAsync("UPDATE KhachHang SET DiemTichLuy = @soDu WHERE Id = @IdKhachHang", 
                                        new { soDu = soDuSauGiam, IdKhachHang = idKhachHang }, tx);
            }

            // 3. Insert DonHang
            var idDonHang = await conn.ExecuteScalarAsync<int>(@"
                INSERT INTO DonHang (MaCode, TongTien, TienGiamGia, TrangThai, IdKhachHang, NguonBan)
                VALUES (@maDH, @tongTien, @tienGiam, @TrangThai, @idKhachHang, @NguonBan);
                SELECT CAST(SCOPE_IDENTITY() AS INT);",
                new { maDH, tongTien, tienGiam, TrangThai = AppConstants.TrangThaiDonHang.DaThanhToan, idKhachHang, NguonBan = AppConstants.NguonBan.Web }, tx);

            var tickets = new List<TicketInfo>();
            var rng = new Random();

            // 4. Insert Details & Tickets
            foreach (var item in req.Items)
            {
                var price = await GetPriceInternal(conn, tx, item.IdSanPham, DateTime.Now);
                decimal thanhTienDong = price * item.SoLuong;
                decimal giamDong = 0;

                // Phan bo discount:
                if (tienGiam > 0 && tongTien > 0)
                {
                    giamDong = Math.Round(thanhTienDong * (tienGiam / tongTien), 0);
                }

                decimal donGiaThucTe = item.SoLuong > 0
                    ? Math.Max(0, price - Math.Round(giamDong / item.SoLuong, 0))
                    : price;
                
                var idCtdh = await conn.ExecuteScalarAsync<int>(@"
                    INSERT INTO ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaGoc, TienGiamGiaDong, DonGiaThucTe)
                    VALUES (@idDonHang, @IdSanPham, @SoLuong, @price, @giamDong, @donGiaThucTe);
                    SELECT CAST(SCOPE_IDENTITY() AS INT);",
                    new { idDonHang, item.IdSanPham, item.SoLuong, price, giamDong, donGiaThucTe }, tx);

                var soLuot = await conn.ExecuteScalarAsync<int?>(@"
                    SELECT SoLuotQuyDoi FROM SanPham_Ve WHERE IdSanPham = @IdSanPham",
                    new { item.IdSanPham }, tx) ?? 1;

                var tenSp = await conn.ExecuteScalarAsync<string>(
                    "SELECT Ten FROM SanPham WHERE Id = @IdSanPham",
                    new { item.IdSanPham }, tx) ?? "";

                for (int i = 0; i < item.SoLuong; i++)
                {
                    var ticketCode = GenerateTicketCode(rng);
                    await conn.ExecuteAsync(@"
                        INSERT INTO VeDienTu (MaCode, IdChiTietDonHang, IdSanPham, SoLuotConLai, TrangThai)
                        VALUES (@ticketCode, @idCtdh, @IdSanPham, @soLuot, N'ChuaSuDung');",
                        new { ticketCode, idCtdh, item.IdSanPham, soLuot }, tx);

                    tickets.Add(new TicketInfo
                    {
                        Id = Guid.Empty,
                        MaCode = ticketCode,
                        TenSanPham = tenSp,
                        SoLuotConLai = soLuot,
                        TrangThai = "ChuaSuDung"
                    });
                }
            }

            // 4. KIỂM TRA & TRỪ TIỀN VÍ NẾU CHỌN THANH TOÁN VÍ
            if (req.PaymentMethod == "ViRFID")
            {
                var pWallet = await conn.QueryFirstOrDefaultAsync<(int Id, decimal SoDu)>("SELECT Id, SoDuKhaDung FROM ViDienTu WHERE IdKhachHang = @idKhachHang", new { idKhachHang }, tx);
                if (pWallet == default || pWallet.SoDu < soTienThucThu)
                {
                    throw new Exception($"KHÔNG ĐỦ SỐ DƯ VÍ. Cần {soTienThucThu:N0}đ nhưng ví chỉ có {(pWallet == default ? 0 : pWallet.SoDu):N0}đ.");
                }

                // Trừ ví bằng Atomic Update
                int affected = await conn.ExecuteAsync(@"
                    UPDATE ViDienTu 
                    SET SoDuKhaDung = SoDuKhaDung - @soTienThucThu 
                    WHERE IdKhachHang = @idKhachHang AND SoDuKhaDung >= @soTienThucThu",
                    new { soTienThucThu, idKhachHang }, tx);

                if (affected == 0) throw new Exception("Giao dịch ví thất bại do tranh chấp số dư.");

                // Ghi Lịch sử Ví
                string viCode = $"V-{DateTime.Now:yyMMddHHmmss}-{Guid.NewGuid().ToString("N").Substring(0, 4)}";
                await conn.ExecuteAsync(@"
                    INSERT INTO GiaoDichVi (IdVi, LoaiGiaoDich, SoTien, IdDonHangLienQuan, ThoiGian, CreatedAt, CreatedBy, MaCode)
                    VALUES (@IdVi, 'ThanhToanDichVu', @SoTienAm, @idDonHang, GETDATE(), GETDATE(), 1, @maCode)",
                    new { IdVi = pWallet.Id, SoTienAm = -soTienThucThu, idDonHang, maCode = viCode }, tx);
            }

            // 5. PhieuThu giả lập
            string rndCode = Guid.NewGuid().ToString("N").Substring(0, 4).ToUpper();
            string dbPhuongThuc = req.PaymentMethod == "ViRFID" ? "ViRFID" : AppConstants.PhuongThucThanhToan.ChuyenKhoan;
            await conn.ExecuteAsync(@"
                INSERT INTO PhieuThu (MaCode, IdDonHang, SoTien, PhuongThuc)
                VALUES (@maPT, @idDonHang, @soTienThucThu, @PhuongThuc)",
                new { maPT = $"PT-W{DateTime.Now:HHmmss}-{rndCode}", idDonHang, soTienThucThu, PhuongThuc = dbPhuongThuc }, tx);

            // --- LƯU LỊCH SỬ DÙNG ĐIỂM LÊN DB (NẾU CÓ) ---
            if (diemSuDung > 0 && idKhachHang != null)
            {
                await conn.ExecuteAsync(@"
                    INSERT INTO LichSuDiem (IdKhachHang, LoaiGiaoDich, SoDiem, SoDuTruoc, SoDuSau, IdDonHang, LyDo, ThoiGian, CreatedBy)
                    VALUES (@IdKh, @LoaiGiaoDich, @SoDiem, @Truoc, @Sau, @IdDh, N'Thanh toán hoá đơn Web', GETDATE(), 1)",
                    new { IdKh = idKhachHang, LoaiGiaoDich = AppConstants.LoaiGiaoDichDiem.SuDung, SoDiem = -diemSuDung, Truoc = khInfo.DiemTichLuy, Sau = khInfo.DiemTichLuy - diemSuDung, IdDh = idDonHang }, tx);
                
                khInfo.DiemTichLuy -= diemSuDung; // Update memory offset
            }

            // --- XỬ LÝ CỘNG ĐIỂM THƯỞNG (Tren soTienThucThu) ---
            if (idKhachHang != null && soTienThucThu >= 100000m)
            {
                decimal heSo = khInfo.LoaiKhach switch {
                    AppConstants.LoaiKhachHang.VVIP => 3.0m, AppConstants.LoaiKhachHang.Vip => 2.0m, AppConstants.LoaiKhachHang.DoanhNghiep => 1.5m, AppConstants.LoaiKhachHang.Doan => 0.5m, AppConstants.LoaiKhachHang.NoiBo => 0m, _ => 1.0m
                };
                
                int diemCong = (int)Math.Floor(soTienThucThu / 100000m * heSo);
                if (diemCong > 0)
                {
                    int soDuTruoc = khInfo.DiemTichLuy;
                    int soDuSau = soDuTruoc + diemCong;

                    await conn.ExecuteAsync("UPDATE KhachHang SET DiemTichLuy = @Sau WHERE Id = @IdKh", 
                        new { Sau = soDuSau, IdKh = idKhachHang }, tx);

                    await conn.ExecuteAsync(@"
                        INSERT INTO LichSuDiem (IdKhachHang, LoaiGiaoDich, SoDiem, SoDuTruoc, SoDuSau, IdDonHang, LyDo, ThoiGian, CreatedBy)
                        VALUES (@IdKh, @LoaiGiaoDich, @SoDiem, @Truoc, @Sau, @IdDh, N'Tích điểm mua vé Web', GETDATE(), 1)",
                        new { IdKh = idKhachHang, LoaiGiaoDich = AppConstants.LoaiGiaoDichDiem.TichLuy, SoDiem = diemCong, Truoc = soDuTruoc, Sau = soDuSau, IdDh = idDonHang }, tx);
                }
            }
            // --- [LOYALTY] TÍNH TỔNG CHI TIÊU & XÉT HẠNG ---
            if (idKhachHang != null && soTienThucThu > 0)
            {
                await conn.ExecuteAsync("sp_CapNhatChiTieuVaHang", 
                    new { IdKhachHang = idKhachHang, ThucThu = soTienThucThu }, 
                    transaction: tx, 
                    commandType: System.Data.CommandType.StoredProcedure);
            }

            tx.Commit();

            return new PurchaseResult
            {
                IdDonHang = idDonHang,
                MaDonHang = maDH,
                TongTien = soTienThucThu,
                Tickets = tickets
            };
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    public async Task<CalculateOrderResult> CalculateOrder(CalculateOrderRequest req)
    {
        using var conn = _db.CreateConnection();
        
        var res = new CalculateOrderResult();

        var khInfo = await conn.QueryFirstOrDefaultAsync<(int? Id, int DiemTichLuy, string LoaiKhach)>(@"
            SELECT kh.Id, kh.DiemTichLuy, kh.LoaiKhach 
            FROM TaiKhoanWeb tk 
            JOIN KhachHang kh ON tk.IdKhachHang = kh.Id
            WHERE tk.Id = @IdTaiKhoanWeb",
            new { req.IdTaiKhoanWeb });

        foreach (var item in req.Items)
        {
            var price = await GetPriceInternal(conn, null, item.IdSanPham, DateTime.Now);
            res.TongTienGoc += price * item.SoLuong;
        }

        if (khInfo.Id == null || res.TongTienGoc <= 0)
        {
            res.ThucThu = res.TongTienGoc;
            return res;
        }

        // 1. Calculate VIP Discount (B2C Only Vé)
        decimal pctVip = khInfo.LoaiKhach switch {
            AppConstants.LoaiKhachHang.VVIP => 0.25m,
            "KIM CƯƠNG" => 0.25m,
            AppConstants.LoaiKhachHang.Vip => 0.10m,
            AppConstants.LoaiKhachHang.HocSinhSinhVien => 0.05m,
            _ => 0m
        };

        // 2. Check Event Promotion
        var kmEvent = await conn.QueryFirstOrDefaultAsync<(decimal GiaTriGiam, string LoaiGiamGia, string TenKhuyenMai)>(@"
            SELECT TOP 1 GiaTriGiam, LoaiGiamGia, TenKhuyenMai 
            FROM KhuyenMai 
            WHERE TrangThai = 1 AND IsDeleted = 0 
              AND NgayBatDau <= GETDATE() AND NgayKetThuc >= GETDATE()
              AND (DonToiThieu IS NULL OR @TongTienGoc >= DonToiThieu)
            ORDER BY GiaTriGiam DESC", new { res.TongTienGoc });

        decimal pctEvent = 0m;
        if (kmEvent != default)
        {
            if (kmEvent.LoaiGiamGia == "PhanTram") pctEvent = kmEvent.GiaTriGiam / 100m;
            else pctEvent = kmEvent.GiaTriGiam / res.TongTienGoc;
        }

        decimal pctMax = Math.Max(pctVip, pctEvent);
        decimal tienGiamChietKhau = Math.Round(res.TongTienGoc * pctMax, 0);

        // 3. Loyalty
        decimal maxGiamDiem = res.TongTienGoc * 0.50m;
        res.DiemKhaDung = Math.Min(khInfo.DiemTichLuy, (int)Math.Floor(maxGiamDiem / 1000m));
        decimal tienGiamDiem = res.DiemKhaDung * 1000m;

        // Choose MAX between TieGiamDiem and TienGiamChietKhau
        if (req.UsePoints && tienGiamDiem > tienGiamChietKhau && res.DiemKhaDung > 0)
        {
            res.TienGiam = tienGiamDiem;
            res.NguonGiam = $"D-Point ({res.DiemKhaDung}đ)";
            res.DiemSuDung = res.DiemKhaDung;
        }
        else
        {
            res.TienGiam = tienGiamChietKhau;
            res.NguonGiam = pctEvent >= pctVip ? (kmEvent.TenKhuyenMai ?? "Sự kiện") : (pctVip > 0 ? $"Hạng {khInfo.LoaiKhach}" : "");
            res.DiemSuDung = 0;
        }

        res.ThucThu = res.TongTienGoc - res.TienGiam;

        // 4. MÔ PHỎNG SỐ ĐIỂM SẼ TÍCH LŨY (Theo số tiền thực thu!)
        if (res.ThucThu >= 100000m)
        {
            decimal heSo = khInfo.LoaiKhach switch {
                AppConstants.LoaiKhachHang.VVIP => 3.0m, AppConstants.LoaiKhachHang.Vip => 2.0m, AppConstants.LoaiKhachHang.DoanhNghiep => 1.5m, AppConstants.LoaiKhachHang.Doan => 0.5m, AppConstants.LoaiKhachHang.NoiBo => 0m, _ => 1.0m
            };
            res.DiemThuong = (int)Math.Floor(res.ThucThu / 100000m * heSo);
        }
        
        return res;
    }

    #endregion

    #region Gate Scan

    public async Task<ScanResult> ScanTicket(ScanRequest req)
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        using var tx = conn.BeginTransaction();

        try
        {
            var ve = await conn.QueryFirstOrDefaultAsync<(Guid Id, string TrangThai, int SoLuotConLai, int IdSanPham)>(@"
                SELECT Id, TrangThai, SoLuotConLai, IdSanPham
                FROM VeDienTu WHERE MaCode = @maQR",
                new { maQR = req.MaQR }, tx);

            if (ve.Id == Guid.Empty)
                return new ScanResult { IsValid = false, Message = "❌ Mã vé không tồn tại!" };

            if (ve.TrangThai == "DaHuy")
                return new ScanResult { IsValid = false, Message = "❌ Vé đã bị hủy!" };

            if (ve.TrangThai == "HetHan" || ve.SoLuotConLai <= 0)
                return new ScanResult { IsValid = false, Message = "❌ Vé đã hết lượt sử dụng!" };

            // Phân tích trò chơi & khu vực của vé + IdThietBi từ SanPham_Ve
            var sp = await conn.QueryFirstOrDefaultAsync<(int? IdKhuVuc, string Ten, int? IdThietBi)>(@"
                SELECT sp.IdKhuVuc, sp.Ten, sv.IdThietBi
                FROM SanPham sp
                LEFT JOIN SanPham_Ve sv ON sv.IdSanPham = sp.Id
                WHERE sp.Id = @IdSanPham",
                new { ve.IdSanPham }, tx);

            // === CHECK 1: KHU VỰC ===
            if (req.IdKhuVuc.HasValue)
            {
                // Nhân viên đứng tại khu vực cụ thể → vé phải đúng khu vực
                if (sp.IdKhuVuc != req.IdKhuVuc.Value)
                {
                    var tenKhuVuc = await conn.ExecuteScalarAsync<string>("SELECT TenKhuVuc FROM KhuVuc WHERE Id = @id", new { id = req.IdKhuVuc.Value }, tx);
                    return new ScanResult { IsValid = false, Message = $"⛔ SAI KHU VỰC: Vé '{sp.Ten}' không dùng cho {tenKhuVuc}!", TenSanPham = sp.Ten };
                }
            }
            else
            {
                // "Cổng chính" (null) → chỉ nhận vé cổng (IdKhuVuc = null), từ chối vé khu vực/trò chơi
                if (sp.IdKhuVuc != null)
                {
                    var tenKhuVucVe = await conn.ExecuteScalarAsync<string>("SELECT TenKhuVuc FROM KhuVuc WHERE Id = @id", new { id = sp.IdKhuVuc }, tx);
                    return new ScanResult { IsValid = false, Message = $"⛔ VÉ KHU VỰC: Vé '{sp.Ten}' thuộc {tenKhuVucVe}, không quẹt được tại Cổng chính!", TenSanPham = sp.Ten };
                }
            }

            // === CHECK 2: TRÒ CHƠI ===
            if (sp.IdThietBi != null)
            {
                // Vé trò chơi → bắt buộc nhân viên phải chọn đúng trò chơi
                if (!req.IdTroChoi.HasValue)
                {
                    var tenTroChoiVe = await conn.ExecuteScalarAsync<string>("SELECT TenThietBi FROM DanhSachThietBi WHERE Id = @id", new { id = sp.IdThietBi }, tx);
                    return new ScanResult { IsValid = false, Message = $"⚠️ VÉ TRÒ CHƠI: Vé '{sp.Ten}' dành cho trò 【{tenTroChoiVe}】— vui lòng hướng dẫn khách đến đúng trò chơi!", TenSanPham = sp.Ten };
                }
                if (sp.IdThietBi != req.IdTroChoi.Value)
                {
                    var tenTroChoi = await conn.ExecuteScalarAsync<string>("SELECT TenThietBi FROM DanhSachThietBi WHERE Id = @id", new { id = req.IdTroChoi.Value }, tx);
                    return new ScanResult { IsValid = false, Message = $"⚠️ SAI TRÒ CHƠI: Vé '{sp.Ten}' không dùng cho trò '{tenTroChoi}'!", TenSanPham = sp.Ten };
                }
            }

            var luotConLai = ve.SoLuotConLai - 1;
            var newStatus = luotConLai <= 0 ? "HetHan" : "DangSuDung";

            await conn.ExecuteAsync(@"
                UPDATE VeDienTu SET SoLuotConLai = @luotConLai, TrangThai = @newStatus, ThoiGianQuet = GETDATE()
                WHERE Id = @Id",
                new { luotConLai, newStatus, ve.Id }, tx);

            tx.Commit();

            return new ScanResult
            {
                IsValid = true,
                Message = $"✅ Vé hợp lệ! Còn {luotConLai} lượt.",
                TenSanPham = sp.Ten,
                SoLuotConLai = luotConLai
            };
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    #endregion

    #region My Tickets

    public async Task<IEnumerable<TicketInfo>> GetMyTickets(int idTaiKhoan)
    {
        using var conn = _db.CreateConnection();

        // Check if TaiKhoanWeb has KhachHang link
        var idKhachHang = await conn.ExecuteScalarAsync<int?>(
            "SELECT IdKhachHang FROM TaiKhoanWeb WHERE Id = @idTaiKhoan",
            new { idTaiKhoan });

        if (idKhachHang != null && idKhachHang > 0)
        {
            // Full path: khách có KhachHang → query qua IdKhachHang
            return await conn.QueryAsync<TicketInfo>(@"
                SELECT v.Id, v.MaCode, sp.Ten AS TenSanPham, v.SoLuotConLai, v.TrangThai
                FROM VeDienTu v
                JOIN SanPham sp ON v.IdSanPham = sp.Id
                JOIN ChiTietDonHang ct ON v.IdChiTietDonHang = ct.Id
                JOIN DonHang dh ON ct.IdDonHang = dh.Id
                WHERE dh.IdKhachHang = @idKhachHang
                ORDER BY v.Id DESC",
                new { idKhachHang });
        }

        // Fallback: legacy account → không có data (chưa nối)
        return Enumerable.Empty<TicketInfo>();
    }

    public async Task<IEnumerable<TransactionHistoryItem>> GetTransactionHistory(int idTaiKhoan)
    {
        using var conn = _db.CreateConnection();
        var idKhachHang = await conn.ExecuteScalarAsync<int?>(
            "SELECT IdKhachHang FROM TaiKhoanWeb WHERE Id = @idTaiKhoan", new { idTaiKhoan });

        if (idKhachHang == null || idKhachHang <= 0) return Enumerable.Empty<TransactionHistoryItem>();

        var sql = @"
            SELECT ThoiGian, 'MuaVe' AS LoaiGiaoDich, TongTien AS SoTien, 
                   MaCode, TrangThai, NguonBan
            FROM DonHang 
            WHERE IdKhachHang = @idKhachHang
            
            UNION ALL

            SELECT gd.ThoiGian, gd.LoaiGiaoDich, gd.SoTien, 
                   gd.MaCode, 'ThanhCong' AS TrangThai, 'Ví_RFID' AS NguonBan
            FROM GiaoDichVi gd
            JOIN ViDienTu v ON gd.IdVi = v.Id
            WHERE v.IdKhachHang = @idKhachHang
            
            ORDER BY ThoiGian DESC
        ";

        return await conn.QueryAsync<TransactionHistoryItem>(sql, new { idKhachHang });
    }

    public async Task<IEnumerable<PointHistoryItem>> GetPointHistory(int idTaiKhoan)
    {
        using var conn = _db.CreateConnection();
        var idKhachHang = await conn.ExecuteScalarAsync<int?>("SELECT IdKhachHang FROM TaiKhoanWeb WHERE Id = @idTaiKhoan", new { idTaiKhoan });
        if (idKhachHang == null || idKhachHang <= 0) return Enumerable.Empty<PointHistoryItem>();

        return await conn.QueryAsync<PointHistoryItem>(@"
            SELECT ThoiGian, LoaiGiaoDich, SoDiem, SoDuTruoc AS TruocGD, SoDuSau AS SauGD, LyDo, 
                   ISNULL((SELECT HoTen FROM NhanVien WHERE Id = CreatedBy), 'Hệ thống') AS NguoiXuLy
            FROM LichSuDiem 
            WHERE IdKhachHang = @idKhachHang
            ORDER BY ThoiGian DESC", new { idKhachHang });
    }

    public async Task<IEnumerable<WalletTransactionItem>> GetWalletHistory(int idTaiKhoan)
    {
        using var conn = _db.CreateConnection();
        var idKhachHang = await conn.ExecuteScalarAsync<int?>("SELECT IdKhachHang FROM TaiKhoanWeb WHERE Id = @idTaiKhoan", new { idTaiKhoan });
        if (idKhachHang == null || idKhachHang <= 0) return Enumerable.Empty<WalletTransactionItem>();

        return await conn.QueryAsync<WalletTransactionItem>(@"
            SELECT gd.ThoiGian, gd.LoaiGiaoDich, gd.SoTien, gd.MaCode AS MaGiaoDich
            FROM GiaoDichVi gd
            JOIN ViDienTu v ON gd.IdVi = v.Id
            WHERE v.IdKhachHang = @idKhachHang
            ORDER BY ThoiGian DESC", new { idKhachHang });
    }

    public async Task<decimal?> GetWalletBalance(int idTaiKhoan)
    {
        using var conn = _db.CreateConnection();
        return await conn.ExecuteScalarAsync<decimal?>(@"
            SELECT v.SoDuKhaDung 
            FROM ViDienTu v
            JOIN TaiKhoanWeb tk ON v.IdKhachHang = tk.IdKhachHang
            WHERE tk.Id = @idTaiKhoan", new { idTaiKhoan });
    }

    #endregion

    #region Helpers

    private async Task<decimal> GetPriceInternal(IDbConnection conn, IDbTransaction? tx, int idSanPham, DateTime date)
    {
        var isHoliday = await conn.ExecuteScalarAsync<int>(
            "SELECT COUNT(1) FROM CauHinhNgayLe WHERE Ngay = @date",
            new { date = date.Date }, tx);

        var now = DateTime.Now.TimeOfDay;
        var bg = await conn.QueryFirstOrDefaultAsync<(decimal GiaNgayThuong, decimal GiaCuoiTuan, decimal GiaNgayLe)>(@"
            SELECT GiaNgayThuong, GiaCuoiTuan, GiaNgayLe FROM BangGia
            WHERE IdSanPham = @idSanPham AND GioBatDau <= @now AND GioKetThuc >= @now
            ORDER BY Id DESC",
            new { idSanPham, now }, tx);

        if (bg.GiaNgayThuong == 0)
            return await conn.ExecuteScalarAsync<decimal>(
                "SELECT DonGia FROM SanPham WHERE Id = @idSanPham", new { idSanPham }, tx);

        if (isHoliday > 0) return bg.GiaNgayLe;
        if (date.DayOfWeek is DayOfWeek.Saturday or DayOfWeek.Sunday) return bg.GiaCuoiTuan;
        return bg.GiaNgayThuong;
    }

    private static string GenerateTicketCode(Random rng)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        return new string(Enumerable.Range(0, 12).Select(_ => chars[rng.Next(chars.Length)]).ToArray());
    }

    #endregion
}
