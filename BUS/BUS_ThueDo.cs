using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace BUS
{
    public class BUS_ThueDo
    {
        private readonly IThueDoChiTietGateway _thueDoGateway;
        private readonly IDonHangGateway _donHangGateway;
        private readonly IViDienTuGateway _viGateway;
        private readonly IGiaoDichViGateway _giaoDichGateway;
        private readonly IPhieuThuGateway _phieuThuGateway;
        private readonly IPhieuChiGateway _phieuChiGateway;
        private readonly IChiTietDonHangGateway _ctdhGateway;

        private static BUS_ThueDo instance;
        public static BUS_ThueDo Instance => instance ?? (instance = new BUS_ThueDo());

        public BUS_ThueDo() : this(new DefaultThueDoChiTietGateway(), new DefaultDonHangGateway(), new DefaultViDienTuGateway(),
                                   new DefaultGiaoDichViGateway(), new DefaultPhieuThuGateway(), new DefaultPhieuChiGateway(), new DefaultChiTietDonHangGateway()) { }

        public BUS_ThueDo(IThueDoChiTietGateway tdGw, IDonHangGateway dhGw, IViDienTuGateway viGw,
                          IGiaoDichViGateway gdGw, IPhieuThuGateway ptGw, IPhieuChiGateway pcGw, IChiTietDonHangGateway ctdhGw)
        {
            _thueDoGateway = tdGw;
            _donHangGateway = dhGw;
            _viGateway = viGw;
            _giaoDichGateway = gdGw;
            _phieuThuGateway = ptGw;
            _phieuChiGateway = pcGw;
            _ctdhGateway = ctdhGw;
        }

        public List<ET_ThueDoChiTiet> LoadDS() => _thueDoGateway.LoadDS();

        public List<ET_DanhSachChuaTraView> GetDanhSachDonChuaTra(DateTime tuNgay, DateTime denNgay)
            => _thueDoGateway.LoadDanhSachChuaTra(tuNgay, denNgay);

        // 
        // GIAO ĐỒ: Tạo đơn + thu cọc (tiền mặt hoặc RFID)
        // PhieuThu là "receipts" — chứa phương thức thanh toán.
        // GiaoDichVi chỉ dùng để log ví (wallet ledger) khi RFID.
        // ThueDoChiTiet.IdPhieuThuCoc -> PhieuThu, không qua GiaoDichVi.
        // 
        public OperationResult RentMultipleItems(ET_DonHang dh, List<ET.RentalCartItem> cart, string phuongThuc, int idNhanVien, int idKhoXuLy = 1)
        {
            if (cart == null || cart.Count == 0) return OperationResult.Failed("Giỏ hàng trống.");

            decimal tongThue = cart.Sum(x => x.TongThue);
            decimal tongCoc = cart.Sum(x => x.TongCoc);
            decimal tongCong = tongThue + tongCoc;

            try
            {
                using (var ts = new System.Transactions.TransactionScope())
                {
                    int idDonHang = _donHangGateway.ThemVaLayId(dh);
                    if (idDonHang <= 0) throw new Exception("Không thể tạo đơn hàng gốc.");

                    // ── 1. THANH TOÁN: tạo PhieuThu (receipt) cho cả thuê + cọc ──
                    int? idGdCocChoVi = null; // GiaoDichVi.Id — chỉ có khi RFID, để trace ví sau này

                    if (phuongThuc == AppConstants.PhuongThucThanhToan.ViRfid && dh.IdKhachHang != null)
                    {
                        var vi = _viGateway.LayTheoKhachHang(dh.IdKhachHang.Value);
                        if (vi == null || vi.SoDuKhaDung < tongCong)
                            throw new Exception("Số dư ví không đủ.");

                        // Trừ ví: tiền thuê bay đi, tiền cọc chuyển sang đóng băng
                        vi.SoDuKhaDung -= tongThue;
                        vi.SoDuKhaDung -= tongCoc;
                        vi.SoDuDongBang += tongCoc;
                        if (!_viGateway.Sua(vi)) throw new Exception("Lỗi cập nhật số dư ví.");

                        // Log wallet ledger (GiaoDichVi — vẫn cần để trace ví)
                        if (tongThue > 0)
                        {
                            var gdThue = new ET_GiaoDichVi
                            {
                                MaCode = "GD-RENT-" + DateTime.Now.Ticks.ToString().Substring(10),
                                IdVi = vi.Id, LoaiGiaoDich = AppConstants.LoaiGiaoDichVi.ThanhToanDichVu,
                                SoTien = tongThue, IdDonHangLienQuan = idDonHang,
                                ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                            };
                            if (_giaoDichGateway.ThemVaLayId(gdThue) <= 0) throw new Exception("Lỗi ghi log thanh toán.");
                        }
                        if (tongCoc > 0)
                        {
                            var gdCoc = new ET_GiaoDichVi
                            {
                                MaCode = "GD-DEP-" + DateTime.Now.Ticks.ToString().Substring(10),
                                IdVi = vi.Id, LoaiGiaoDich = AppConstants.LoaiGiaoDichVi.ThuCoc,
                                SoTien = tongCoc, IdDonHangLienQuan = idDonHang,
                                ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                            };
                            idGdCocChoVi = _giaoDichGateway.ThemVaLayId(gdCoc);
                            if (idGdCocChoVi <= 0) throw new Exception("Lỗi ghi log cọc ví.");
                        }
                    }

                    // PhieuThu là receipt chung — RFID hay tiền mặt đều tạo
                    var pt = new ET_PhieuThu
                    {
                        MaCode = "PT-RENT-" + DateTime.Now.Ticks.ToString().Substring(10),
                        IdDonHang = idDonHang,
                        IdGiaoDichVi = idGdCocChoVi,        // null nếu tiền mặt, có giá trị nếu RFID
                        SoTien = tongCong,
                        PhuongThuc = phuongThuc,
                        ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                    };
                    int idPhieuThuCoc = _phieuThuGateway.ThemVaLayId(pt);
                    if (idPhieuThuCoc <= 0) throw new Exception("Không tạo được Phiếu Thu.");

                    // ── 2. GHI NHẬN CHI TIẾT THUÊ ──
                    foreach (var item in cart)
                    {
                        for (int i = 0; i < item.SoLuong; i++)
                        {
                            decimal tienThue1Mon = item.TongThue / item.SoLuong;
                            decimal tienCoc1Mon = item.TongCoc / item.SoLuong;

                            var ctdh = new ET_ChiTietDonHang
                            {
                                IdDonHang = idDonHang,
                                IdSanPham = item.IdSanPham,
                                SoLuong = 1,
                                DonGiaGoc = tienThue1Mon,
                                TienGiamGiaDong = 0,
                                DonGiaThucTe = tienThue1Mon
                            };
                            int idCtdh = _ctdhGateway.ThemVaLayId(ctdh);
                            if (idCtdh <= 0) throw new Exception("Tạo Line Item (ChiTietDonHang) thất bại.");

                            var td = new ET_ThueDoChiTiet
                            {
                                IdChiTietDonHang = idCtdh,
                                IdSanPham = item.IdSanPham,
                                SoLuong = 1,
                                ThoiGianBatDau = DateTime.Now,
                                SoTienCoc = tienCoc1Mon,
                                TrangThaiCoc = "ChuaHoan",
                                IdPhieuThuCoc = idPhieuThuCoc, // ← receipts link, không qua GiaoDichVi
                                TienThueDaThu = tienThue1Mon
                            };
                            if (!_thueDoGateway.Them(td)) throw new Exception("Lưu chi tiết thuê thất bại.");
                        }
                    }

                    // Tự động trừ tồn kho (kho của khu vực xử lý)
                    foreach(var item in cart)
                    {
                        BUS_KhoHang.Instance.WriteLedgerThueDo(idKhoXuLy, item.IdSanPham, -item.SoLuong, idDonHang, idNhanVien, "XUAT_THUE", "Xuất cho khách thuê");
                    }

                    ts.Complete();
                    return OperationResult.Success();
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Failed(ex.Message);
            }
        }

        // 
        // TRẢ ĐỒ đơn lẻ (ReturnItem)
        // Đọc PhieuThu qua IdPhieuThuCoc để biết phương thức.
        // IdDonHang lấy từ PhieuThu.IdDonHang — luôn hợp lệ dù RFID hay tiền mặt.
        // 
        public OperationResult ReturnItem(int idThueDo, bool coPhat, decimal tienPhat, int idNhanVien, int idKhoXuLy = 1)
        {
            if (coPhat && tienPhat < 0) return OperationResult.Failed("Tiền phạt không được âm.");
            try
            {
                using (var ts = new System.Transactions.TransactionScope())
                {
                    var td = _thueDoGateway.LayTheoId(idThueDo);
                    if (td == null) return OperationResult.Failed("Không tìm thấy dữ liệu thuê đồ.");
                    if (td.TrangThaiCoc != AppConstants.TrangThaiCoc.ChuaHoan) return OperationResult.Failed("Món này đã được hoàn cọc rồi.");

                    td.ThoiGianKetThuc = DateTime.Now;
                    decimal tienHoan = coPhat ? Math.Max(0, td.SoTienCoc - tienPhat) : td.SoTienCoc;
                    decimal tienPhatVuotCoc = coPhat ? Math.Max(0, tienPhat - td.SoTienCoc) : 0;

                    // Resolve IdDonHang: qua PhieuThu — đúng với cả tiền mặt lẫn RFID
                    int idDonHang = 0;
                    bool laViRfid = false;
                    int? idViCanHoan = null; // Chỉ có khi RFID

                    if (td.IdPhieuThuCoc.HasValue)
                    {
                        var phieuThu = _phieuThuGateway.LayTheoId(td.IdPhieuThuCoc.Value);
                        if (phieuThu != null)
                        {
                            idDonHang = phieuThu.IdDonHang ?? 0;
                            laViRfid = phieuThu.PhuongThuc == AppConstants.PhuongThucThanhToan.ViRfid
                                       && phieuThu.IdGiaoDichVi.HasValue;

                            // Nếu RFID: lấy IdVi qua GiaoDichVi mà PhieuThu đã link
                            if (laViRfid)
                            {
                                var gdCoc = _giaoDichGateway.LayTheoId(phieuThu.IdGiaoDichVi.Value);
                                idViCanHoan = gdCoc?.IdVi;
                            }
                        }
                    }

                    // ── Xử lý hoàn cọc RFID ──
                    if (laViRfid && idViCanHoan.HasValue)
                    {
                        var vi = _viGateway.LayTheoId(idViCanHoan.Value);
                        vi.SoDuDongBang -= td.SoTienCoc;
                        vi.SoDuKhaDung += tienHoan;
                        _viGateway.Sua(vi);

                        if (tienHoan > 0)
                        {
                            var gdHoan = new ET_GiaoDichVi
                            {
                                MaCode = "GD-REF-" + DateTime.Now.Ticks.ToString().Substring(10),
                                IdVi = vi.Id, LoaiGiaoDich = AppConstants.LoaiGiaoDichVi.HoanCoc,
                                SoTien = tienHoan, IdDonHangLienQuan = idDonHang,
                                ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                            };
                            int idGdHoan = _giaoDichGateway.ThemVaLayId(gdHoan);
                            // Tạo PhieuChi hoàn ví để kế toán trace
                            var pcHoan = new ET_PhieuChi
                            {
                                MaCode = "PC-REF-" + DateTime.Now.Ticks.ToString().Substring(10),
                                IdDonHang = idDonHang, SoTien = tienHoan,
                                LyDo = coPhat ? "Hoàn cọc RFID (đã trừ phạt)" : "Hoàn full cọc RFID",
                                ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                            };
                            if (_phieuChiGateway.Them(pcHoan))
                                td.IdPhieuChiHoanCoc = 0; // không có DAL LayTheoId PhieuChi — ghi nhận theo IdDonHang
                        }
                    }
                    else // ── Tiền mặt ──
                    {
                        if (tienHoan > 0)
                        {
                            var pc = new ET_PhieuChi
                            {
                                MaCode = "PC-REF-" + DateTime.Now.Ticks.ToString().Substring(10),
                                IdDonHang = idDonHang, // ← luôn hợp lệ, không còn = 0
                                SoTien = tienHoan,
                                LyDo = coPhat ? "Hoàn cọc tiền mặt (đã trừ phạt)" : "Hoàn full cọc tiền mặt",
                                ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                            };
                            _phieuChiGateway.Them(pc);
                        }
                    }

                    // ── Phạt vượt cọc: thu thêm từ khách ──
                    if (tienPhatVuotCoc > 0)
                    {
                        var ptPhat = new ET_PhieuThu
                        {
                            MaCode = "PT-PEN-" + DateTime.Now.Ticks.ToString().Substring(10),
                            IdDonHang = idDonHang, // ← luôn có, không còn = 0
                            SoTien = tienPhatVuotCoc,
                            PhuongThuc = AppConstants.PhuongThucThanhToan.TienMat,
                            ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                        };
                        _phieuThuGateway.Them(ptPhat);
                    }

                    td.TrangThaiCoc = coPhat ? "DaPhat" : "DaHoan";
                    _thueDoGateway.Sua(td);

                    // Mất/hỏng nặng (phạt) có thể mất luôn đồ, nếu trả bình thường thì nhập kho lại. 
                    // Ở đây, ReturnItem nghĩa là thu hồi về. Nếu coPhat => đồ có thể hỏng rồi, nhưng thôi tạm coi trả về, 
                    // sau này kiểm kê kho sẽ tự thanh lý nếu hỏng.
                    BUS_KhoHang.Instance.WriteLedgerThueDo(idKhoXuLy, td.IdSanPham, 1, idDonHang, idNhanVien, "NHAP_THUE", "Nhận lại đồ thuê");

                    ts.Complete();

                    if (tienPhatVuotCoc > 0)
                        return OperationResult.Success(string.Format("Hoàn cọc thành công.\n⚠️ PHẠT VƯỢT CỌC: Cần thu thêm {0:N0}đ TIỀN MẶT.\n(Hệ thống đã tự động tạo Phiếu Thu)", tienPhatVuotCoc));

                    return OperationResult.Success();
                }
            }
            catch (System.Data.DBConcurrencyException)
            {
                return OperationResult.Failed("Ví vừa bị thay đổi bởi giao dịch khác.");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Lỗi hoàn cọc: " + ex.Message);
            }
        }

        // 
        // THU HỒI BATCH (XacNhanThuHoiDoBatch)
        // Gom nhóm theo IdPhieuThuCoc thay vì IdGiaoDichCoc.
        // IdDonHang resolve từ PhieuThu.IdDonHang — luôn đúng.
        // 
        public OperationResult XacNhanThuHoiDoBatch(List<ET.ET_ThueDoChiTiet> dsDangThueHopLe, List<ET.ET_ThuHoiView> dsXuLy, Dictionary<int, decimal> phiDenBuTheoSP, int idNhanVien, int idKhoXuLy = 1)
        {
            try
            {
                using (var ts = new System.Transactions.TransactionScope())
                {
                    // Key = IdPhieuThuCoc (nullable): nhóm các dòng thuê theo receipt chung
                    // null key = không có PhieuThu (edge case cũ — giữ backward compat)
                    var moneyByReceipt = new Dictionary<int?, (decimal sumCocUnfreeze, decimal sumPhatLoGio, decimal sumPhatHuHong, int idDonHang, bool laViRfid, int? idVi)>();

                    foreach (var item in dsXuLy)
                    {
                        var cacDongGoc = dsDangThueHopLe.Where(x => x.IdSanPham == item.IdSanPham).ToList();

                        if (item.TraLanNay + item.BaoMat > cacDongGoc.Count)
                            return OperationResult.Failed($"Lỗi: Số lượng thu hồi [{item.TenSanPham}] vượt số đang giữ!");

                        decimal phiDenBuTrungBinh = item.BaoMat > 0
                            ? (phiDenBuTheoSP.ContainsKey(item.IdSanPham) ? phiDenBuTheoSP[item.IdSanPham] / item.BaoMat : 0)
                            : 0;

                        int index = 0;

                        // A. TRẢ BÌNH THƯỜNG
                        for (int i = 0; i < item.TraLanNay; i++)
                        {
                            var dong = cacDongGoc[index];
                            TimeSpan dur = DateTime.Now - dong.ThoiGianBatDau;
                            decimal tongTienThueThucTe = BUS_BangGia.Instance.TinhTienThueTheoPhut(dong.IdSanPham, dong.ThoiGianBatDau, Math.Max(0, (int)Math.Ceiling(dur.TotalMinutes)));
                            decimal phiLoGio = Math.Max(0, tongTienThueThucTe - dong.TienThueDaThu);

                            EnsureReceiptGroup(moneyByReceipt, dong, _phieuThuGateway, _giaoDichGateway);

                            var cur = moneyByReceipt[dong.IdPhieuThuCoc];
                            moneyByReceipt[dong.IdPhieuThuCoc] = (cur.sumCocUnfreeze + dong.SoTienCoc, cur.sumPhatLoGio + phiLoGio, cur.sumPhatHuHong, cur.idDonHang, cur.laViRfid, cur.idVi);

                            dong.TrangThaiCoc = phiLoGio > 0 ? "DaPhat" : "DaHoan";
                            dong.ThoiGianKetThuc = DateTime.Now;
                            _thueDoGateway.Sua(dong);

                            // Nhập kho lại từng món 
                            BUS_KhoHang.Instance.WriteLedgerThueDo(idKhoXuLy, dong.IdSanPham, 1, cur.idDonHang, idNhanVien, "NHAP_THUE", "Thu hồi đồ lô");

                            index++;
                        }

                        // B. BÁO MẤT / HỎNG
                        for (int i = 0; i < item.BaoMat; i++)
                        {
                            var dong = cacDongGoc[index];
                            TimeSpan dur = DateTime.Now - dong.ThoiGianBatDau;
                            decimal tongTienThueThucTe = BUS_BangGia.Instance.TinhTienThueTheoPhut(dong.IdSanPham, dong.ThoiGianBatDau, Math.Max(0, (int)Math.Ceiling(dur.TotalMinutes)));
                            decimal phiLoGio = Math.Max(0, tongTienThueThucTe - dong.TienThueDaThu);

                            EnsureReceiptGroup(moneyByReceipt, dong, _phieuThuGateway, _giaoDichGateway);

                            var cur = moneyByReceipt[dong.IdPhieuThuCoc];
                            moneyByReceipt[dong.IdPhieuThuCoc] = (cur.sumCocUnfreeze + dong.SoTienCoc, cur.sumPhatLoGio + phiLoGio, cur.sumPhatHuHong + phiDenBuTrungBinh, cur.idDonHang, cur.laViRfid, cur.idVi);

                            dong.TrangThaiCoc = "DaPhat";
                            dong.ThoiGianKetThuc = DateTime.Now;
                            _thueDoGateway.Sua(dong);
                            index++;
                        }
                    }

                    // Xử lý dòng tiền theo từng receipt group
                    decimal tongThuThemTienMat = 0;

                    foreach (var kvp in moneyByReceipt)
                    {
                        var info = kvp.Value;
                        decimal tongPhat = info.sumPhatLoGio + info.sumPhatHuHong;
                        decimal tienHoan = Math.Max(0, info.sumCocUnfreeze - tongPhat);
                        decimal tienPhatVuot = Math.Max(0, tongPhat - info.sumCocUnfreeze);

                        if (info.laViRfid && info.idVi.HasValue)
                        {
                            var vi = _viGateway.LayTheoId(info.idVi.Value);
                            vi.SoDuDongBang -= info.sumCocUnfreeze;
                            vi.SoDuKhaDung += tienHoan;
                            _viGateway.Sua(vi);

                            if (tienHoan > 0)
                            {
                                _giaoDichGateway.ThemVaLayId(new ET_GiaoDichVi
                                {
                                    MaCode = "GD-REF-BT-" + DateTime.Now.Ticks.ToString().Substring(10),
                                    IdVi = vi.Id, LoaiGiaoDich = AppConstants.LoaiGiaoDichVi.HoanCoc,
                                    SoTien = tienHoan, IdDonHangLienQuan = info.idDonHang,
                                    ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                                });
                                _phieuChiGateway.Them(new ET_PhieuChi
                                {
                                    MaCode = "PC-REF-BT-" + DateTime.Now.Ticks.ToString().Substring(10),
                                    IdDonHang = info.idDonHang, SoTien = tienHoan,
                                    LyDo = "Hoàn cọc RFID (batch)", ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                                });
                            }
                        }
                        else
                        {
                            if (tienHoan > 0)
                            {
                                _phieuChiGateway.Them(new ET_PhieuChi
                                {
                                    MaCode = "PC-REF-BT-" + DateTime.Now.Ticks.ToString().Substring(10),
                                    IdDonHang = info.idDonHang, // ← Luôn hợp lệ
                                    SoTien = tienHoan, LyDo = "Hoàn cọc tiền mặt (batch)",
                                    ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                                });
                            }
                        }

                        if (tienPhatVuot > 0)
                        {
                            tongThuThemTienMat += tienPhatVuot;
                            _phieuThuGateway.Them(new ET_PhieuThu
                            {
                                MaCode = "PT-PEN-BT-" + DateTime.Now.Ticks.ToString().Substring(10),
                                IdDonHang = info.idDonHang, // ← Luôn hợp lệ
                                SoTien = tienPhatVuot, PhuongThuc = AppConstants.PhuongThucThanhToan.TienMat,
                                ThoiGian = DateTime.Now, CreatedAt = DateTime.Now, CreatedBy = idNhanVien
                            });
                        }
                    }

                    ts.Complete();

                    return tongThuThemTienMat > 0
                        ? OperationResult.Success($"Đã thu hồi lô đồ thành công.\n⚠️ PHẠT VƯỢT CỌC: Cần thu thêm {tongThuThemTienMat:N0}đ TIỀN MẶT.")
                        : OperationResult.Success("Đã thu hồi đồ và cập nhật hoàn cọc thành công!");
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Lỗi hệ thống khi thu hồi lô đồ: " + ex.Message);
            }
        }

        // ── Helper: đảm bảo IdPhieuThuCoc tồn tại trong dictionary và resolve info ──
        private static void EnsureReceiptGroup(
            Dictionary<int?, (decimal, decimal, decimal, int, bool, int?)> dict,
            ET_ThueDoChiTiet dong,
            IPhieuThuGateway ptGw,
            IGiaoDichViGateway gdGw)
        {
            if (dict.ContainsKey(dong.IdPhieuThuCoc)) return;

            int idDonHang = 0;
            bool laViRfid = false;
            int? idVi = null;

            if (dong.IdPhieuThuCoc.HasValue)
            {
                var pt = ptGw.LayTheoId(dong.IdPhieuThuCoc.Value);
                if (pt != null)
                {
                    idDonHang = pt.IdDonHang ?? 0;
                    laViRfid = pt.PhuongThuc == AppConstants.PhuongThucThanhToan.ViRfid && pt.IdGiaoDichVi.HasValue;
                    if (laViRfid)
                    {
                        var gd = gdGw.LayTheoId(pt.IdGiaoDichVi.Value);
                        idVi = gd?.IdVi;
                    }
                }
            }

            dict[dong.IdPhieuThuCoc] = (0m, 0m, 0m, idDonHang, laViRfid, idVi);
        }
    }
}
