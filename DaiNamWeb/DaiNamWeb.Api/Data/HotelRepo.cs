using Dapper;
using DaiNamWeb.Api.Models;

namespace DaiNamWeb.Api.Data;

public class HotelRepo
{
    private readonly DbContext _db;
    public HotelRepo(DbContext db) => _db = db;

    #region Truy vấn loại phòng & phòng trống

    // LoaiPhong PK = IdSanPham, giá lấy từ DanhMuc.SanPham liên kết qua IdSanPham
    public async Task<IEnumerable<RoomTypeInfo>> GetRoomTypes(DateTime checkIn, DateTime checkOut)
    {
        using var conn = _db.CreateConnection();
        return await conn.QueryAsync<RoomTypeInfo>(@"
            SELECT 
                lp.IdSanPham AS IdLoaiPhong,
                lp.TenLoai,
                lp.MoTa,
                lp.SoNguoiToiDa,
                lp.DienTich,
                lp.TienNghi,
                COALESCE(sp.DonGia, 0) AS GiaMoiDem,
                (
                    SELECT COUNT(*) FROM DanhMuc.Phong p2
                    WHERE p2.IdLoaiPhong = lp.IdSanPham AND p2.TrangThai = 'Trong'
                      AND NOT EXISTS (
                          SELECT 1 FROM BanHang.ChiTietDatPhong ct
                          JOIN BanHang.PhieuDatPhong pdp ON ct.IdPhieuDatPhong = pdp.Id
                          WHERE ct.IdPhong = p2.Id
                            AND pdp.NgayNhanPhong < @checkOut
                            AND pdp.NgayTraPhong > @checkIn
                            AND (
                                pdp.TrangThai = 'DangO'
                                OR (pdp.TrangThai = 'DatTruoc' AND pdp.NgayNhanPhong > GETDATE())
                            )
                      )
                ) AS SoPhongTrong
            FROM DanhMuc.LoaiPhong lp
            LEFT JOIN DanhMuc.SanPham sp ON sp.Id = lp.IdSanPham AND sp.DaXoa = 0
            WHERE lp.ConHoatDong = 1
            ORDER BY COALESCE(sp.DonGia, 0)",
            new { checkIn, checkOut });
    }

    #endregion

    #region Đặt phòng (có cọc 30%)

    public async Task<BookingResult> Book(BookingRequest req)
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        using var tx = conn.BeginTransaction();

        try
        {
            var idDoiTac = await conn.ExecuteScalarAsync<int>(
                "SELECT IdDoiTac FROM DoiTac.TaiKhoan WHERE Id = @Id", new { Id = req.IdTaiKhoan }, tx);

            // UPDLOCK + ROWLOCK: khóa hàng người khác không đọc được phòng này đến khi transaction commit
            // Ngăn 2 request đồng thời book cùng 1 phòng
            var idPhong = await conn.ExecuteScalarAsync<int?>(@"
                SELECT TOP 1 p.Id FROM DanhMuc.Phong p WITH (UPDLOCK, ROWLOCK)
                WHERE p.IdLoaiPhong = @IdLoaiPhong AND p.TrangThai = 'Trong'
                  AND NOT EXISTS (
                      SELECT 1 FROM BanHang.ChiTietDatPhong ct
                      JOIN BanHang.PhieuDatPhong pdp ON ct.IdPhieuDatPhong = pdp.Id
                      WHERE ct.IdPhong = p.Id
                        AND pdp.NgayNhanPhong < @NgayTraPhong
                        AND pdp.NgayTraPhong > @NgayNhanPhong
                        AND (
                            pdp.TrangThai = 'DangO'
                            -- DatTruoc chỉ chẹn slot nếu chưa quá giờ nhận phòng dự kiến (1h grace)
                            OR (pdp.TrangThai = 'DatTruoc' AND DATEADD(HOUR, 1, pdp.NgayNhanPhong) > GETDATE())
                        )
                  )
                ORDER BY p.Id",
                new { req.IdLoaiPhong, req.NgayNhanPhong, req.NgayTraPhong }, tx);

            if (idPhong == null)
                throw new Exception("Không còn phòng trống cho loại phòng và ngày bạn chọn.");

            int soDem = (req.NgayTraPhong - req.NgayNhanPhong).Days;
            if (soDem <= 0) soDem = 1;

            // Lấy giá/đêm từ SanPham liên kết LoaiPhong
            var giaDem = await conn.ExecuteScalarAsync<decimal?>(@"
                SELECT sp.DonGia FROM DanhMuc.SanPham sp
                WHERE sp.Id = @IdLoaiPhong AND sp.DaXoa = 0",
                new { req.IdLoaiPhong }, tx) ?? 500000m;

            decimal tienPhong = giaDem * soDem;
            decimal tienCoc = Math.Round(tienPhong * 0.3m, 0);

            var maDat = $"DP{DateTime.Now:yyMMddHHmmss}";

            // Lấy thông tin khách từ DoiTac.ThongTin
            var khach = await conn.QueryFirstOrDefaultAsync<(string HoTen, string DienThoai)>(
                "SELECT HoTen, DienThoai FROM DoiTac.ThongTin WHERE Id = @idDoiTac",
                new { idDoiTac }, tx);

            // Tạo đơn hàng online cho việc đặt phòng
            var maDH = $"DH{DateTime.Now:yyMMddHHmmss}";
            var idDonHang = await conn.ExecuteScalarAsync<int>(@"
                INSERT INTO BanHang.DonHang (MaDonHang, IdKhachHang, IdNhanVien, NguonBan,
                    TongTienHang, TongGiamGia, TienThueVAT, TienPhiDichVu, TongThanhToan, TrangThai, NgayTao)
                VALUES (@maDH, @idDoiTac, 1, 'Online',
                    @tienPhong, 0, 0, 0, @tienPhong, 'DaThanhToan', GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT);",
                new { maDH, idDoiTac, tienPhong }, tx);

            // Tạo chi tiết đơn hàng — chỉ dùng cột xác nhận tồn tại
            var idCtdh = await conn.ExecuteScalarAsync<int>(@"
                INSERT INTO BanHang.ChiTietDonHang (IdDonHang, IdSanPham, SoLuong, DonGiaThucTe, TienThue)
                VALUES (@idDonHang, @IdLoaiPhong, @soDem, @giaDem, 0);
                SELECT CAST(SCOPE_IDENTITY() AS INT);",
                new { idDonHang, req.IdLoaiPhong, soDem, giaDem }, tx);


            var idPhieu = await conn.ExecuteScalarAsync<int>(@"
                INSERT INTO BanHang.PhieuDatPhong
                    (MaDatPhong, IdKhachHang, TenNguoiDat, SoDienThoai,
                     NgayNhanPhong, NgayTraPhong, TienPhongTraTruoc, TienCocNapVi,
                     TrangThai, GhiChu, NgayTao)
                VALUES (@maDat, @idDoiTac, @hoTen, @dienThoai,
                        @NgayNhanPhong, @NgayTraPhong, @tienCoc, 0,
                        'DatTruoc', @GhiChu, GETDATE());
                SELECT CAST(SCOPE_IDENTITY() AS INT);",
                new { maDat, idDoiTac, hoTen = khach.HoTen, dienThoai = khach.DienThoai,
                      req.NgayNhanPhong, req.NgayTraPhong, tienCoc, req.GhiChu }, tx);

            // Ghi chi tiết đặt phòng — dùng IdChiTietDonHang vừa tạo
            await conn.ExecuteAsync(@"
                INSERT INTO BanHang.ChiTietDatPhong
                    (IdPhieuDatPhong, IdChiTietDonHang, IdLoaiPhong, IdPhong, GiaBanDem, TrangThai,
                     NgayCheckIn, NgayCheckOut)
                VALUES (@idPhieu, @idCtdh, @IdLoaiPhong, @idPhong, @giaDem, 'ChoDen',
                        @NgayNhanPhong, @NgayTraPhong)",
                new { idPhieu, idCtdh, req.IdLoaiPhong, idPhong, giaDem,
                      req.NgayNhanPhong, req.NgayTraPhong }, tx);

            tx.Commit();
            return new BookingResult
            {
                IdPhieuDatPhong = idPhieu,
                MaDatPhong = maDat,
                TienPhong = tienPhong,
                TienCoc = tienCoc
            };
        }
        catch
        {
            tx.Rollback();
            throw;
        }
    }

    #endregion

    #region Đặt phòng của tôi

    public async Task<IEnumerable<MyBookingItem>> GetMyBookings(int idTaiKhoan)
    {
        using var conn = _db.CreateConnection();
        var idDoiTac = await conn.ExecuteScalarAsync<int>(
            "SELECT IdDoiTac FROM DoiTac.TaiKhoan WHERE Id = @idTaiKhoan", new { idTaiKhoan });

        return await conn.QueryAsync<MyBookingItem>(@"
            SELECT pdp.Id AS IdPhieu, pdp.MaDatPhong,
                   lp.TenLoai AS TenLoaiPhong,
                   p.MaPhong,
                   pdp.NgayNhanPhong, pdp.NgayTraPhong,
                   pdp.TienPhongTraTruoc AS TienCoc,
                   ct.GiaBanDem * DATEDIFF(DAY, pdp.NgayNhanPhong, pdp.NgayTraPhong) AS TienPhong,
                   pdp.TrangThai
            FROM BanHang.PhieuDatPhong pdp
            LEFT JOIN BanHang.ChiTietDatPhong ct ON ct.IdPhieuDatPhong = pdp.Id
            LEFT JOIN DanhMuc.Phong p ON ct.IdPhong = p.Id
            LEFT JOIN DanhMuc.LoaiPhong lp ON ct.IdLoaiPhong = lp.IdSanPham
            WHERE pdp.IdKhachHang = @idDoiTac
            ORDER BY pdp.NgayTao DESC",
            new { idDoiTac });
    }

    #endregion

    #region Huỷ đặt phòng

    // Chính sách hoàn cọc:
    // - Hủy trước 3 ngày: hoàn 100% cọc
    // - Hủy trong vòng 3 ngày: không hoàn
    public async Task<CancelBookingResult> CancelBooking(CancelBookingRequest req)
    {
        using var conn = _db.CreateConnection();
        conn.Open();
        using var tx = conn.BeginTransaction();

        try
        {
            // Xác nhận phiếu thuộc đúng tài khoản này
            var idDoiTac = await conn.ExecuteScalarAsync<int>(
                "SELECT IdDoiTac FROM DoiTac.TaiKhoan WHERE Id = @Id", new { Id = req.IdTaiKhoan }, tx);

            var phieu = await conn.QueryFirstOrDefaultAsync<(int Id, DateTime NgayNhanPhong, decimal TienCoc, string TrangThai)>(@"
                SELECT Id, NgayNhanPhong, TienPhongTraTruoc AS TienCoc, TrangThai
                FROM BanHang.PhieuDatPhong
                WHERE Id = @IdPhieu AND IdKhachHang = @idDoiTac",
                new { IdPhieu = req.IdPhieuDatPhong, idDoiTac }, tx);

            if (phieu.Id == 0)
                throw new Exception("Không tìm thấy phiếu đặt phòng.");

            if (phieu.TrangThai != "DatTruoc")
                throw new Exception($"Không thể hủy phiếu ở trạng thái '{phieu.TrangThai}'.");

            // Tính tiền hoàn theo chính sách
            int ngayConLai = (phieu.NgayNhanPhong.Date - DateTime.Today).Days;
            decimal tienHoan = ngayConLai >= 3 ? phieu.TienCoc : 0m;
            string thongBao = ngayConLai >= 3
                ? $"Hủy thành công. Hoàn cọc {tienHoan:N0} VND (hủy trước {ngayConLai} ngày)."
                : $"Hủy thành công. Không hoàn cọc (hủy trong vòng 3 ngày trước ngày đến).";

            // Đánh dấu phiếu là đã hủy
            await conn.ExecuteAsync(@"
                UPDATE BanHang.PhieuDatPhong
                SET TrangThai = 'DaHuy', GhiChu = CONCAT(GhiChu, ' | Hủy bởi khách: ', @LyDo)
                WHERE Id = @IdPhieu",
                new { req.IdPhieuDatPhong, LyDo = req.LyDo ?? "Không rõ" }, tx);

            // Giải phóng phòng vật lý về trạng thái Trong
            await conn.ExecuteAsync(@"
                UPDATE DanhMuc.Phong SET TrangThai = 'Trong'
                WHERE Id = (
                    SELECT TOP 1 IdPhong FROM BanHang.ChiTietDatPhong
                    WHERE IdPhieuDatPhong = @IdPhieu
                )",
                new { IdPhieu = req.IdPhieuDatPhong }, tx);

            tx.Commit();
            return new CancelBookingResult { ThanhCong = true, TienHoanCoc = tienHoan, ThongBao = thongBao };
        }
        catch (Exception ex)
        {
            tx.Rollback();
            return new CancelBookingResult { ThanhCong = false, ThongBao = ex.Message };
        }
    }

    #endregion
}
