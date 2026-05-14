using System;
using System.Collections.Generic;
using System.Linq;
using ET.DTOs;
using ET.Constants;
using ET.Models.DanhMuc;
using ET.Models.BanHang;
using System.Data.Linq;
using ET.Results;
using DAL;
using DAL.Repositories.BanHang;

namespace BUS.Services.BanHang
{
    public class BUS_LuuTru_Booking
    {
        public static BUS_LuuTru_Booking Instance { get; } = new BUS_LuuTru_Booking();

        private BUS_LuuTru_Booking() { }

        public List<string> GetNextStates(string thucThe, string tuTrangThai)
        {
            if (string.IsNullOrEmpty(tuTrangThai)) return new List<string>();
            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    return db.LuongTrangThais
                        .Where(x => x.ThucThe == thucThe && x.TuTrangThai == tuTrangThai)
                        .Select(x => x.DenTrangThai)
                        .ToList();
                }
            }
            catch { return new List<string>(); }
        }

        #region Xử lý Đặt phòng và Check-in đồng thời

        /// <summary>
        /// Đặt phòng và check-in đồng thời trong 1 transaction duy nhất.
        /// Toàn bộ dữ liệu (KhachHang, PhieuDatPhong, DonHang, ChiTietDatPhong) được ghi 1 lần
        /// nếu thất bại rollback.
        /// </summary>
        public OperationResult<bool> DatPhongVaCheckIn(string tenKhachHang, string sdt, int idPhong, DateTime checkIn, DateTime checkOut, decimal tienCoc, int idNhanVien, bool checkInNgay = true, string maTheRFID = null, int idPhienGiaoDich = 0)
        {
            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    db.Connection.Open();
                    using (var transaction = db.Connection.BeginTransaction())
                    {
                        db.Transaction = transaction;

                        // 1. Tìm hoặc chuẩn bị tạo Khách Hàng mới
                        var khachHang = db.KhachHangs.FirstOrDefault(k => k.ThongTin1.DienThoai == sdt);
                        if (khachHang == null)
                        {
                            var thongTin = new ThongTin
                            {
                                HoTen = tenKhachHang,
                                DienThoai = sdt,
                                LoaiDoiTac = AppConstants.LoaiDoiTac.CaNhan,
                                NgayTao = DateTime.Now
                            };
                            db.ThongTins.InsertOnSubmit(thongTin);

                            khachHang = new KhachHang
                            {
                                ThongTin1 = thongTin,
                                MaKhachHang = $"KH{DateTime.Now:yyMMddHHmmssfff}",
                                LoaiKhach = AppConstants.LoaiKhach.CaNhan
                            };
                            db.KhachHangs.InsertOnSubmit(khachHang);
                        }

                        // 2. Kiểm tra Phòng
                        var phong = db.Phongs.FirstOrDefault(p => p.Id == idPhong);
                        if (phong == null)
                            return OperationResult<bool>.Fail("ERR_PHONG_KHONG_TON_TAI");

                        if (phong.TrangThai != AppConstants.TrangThaiPhong.Trong && phong.TrangThai != AppConstants.TrangThaiPhong.ChoDon)
                            return OperationResult<bool>.Fail("ERR_PHONG_DANG_BAN");

                        // 3. Lấy giá phòng từ Bảng Giá Động (IdLoaiPhong chính là IdSanPham)
                        decimal giaGoc = 0;
                        if (phong.IdLoaiPhong > 0)
                            giaGoc = BUS.Services.DanhMuc.BUS_BangGia.Instance.GetDynamicPrice(phong.IdLoaiPhong, checkIn, out _);

                        // 4. Tạo Phiếu Đặt Phòng (Master)
                        var phieu = new DAL.PhieuDatPhong
                        {
                            MaDatPhong = $"BK{DateTime.Now:yyMMddHHmmssfff}",
                            NgayNhanPhong = checkIn,
                            NgayTraPhong = checkOut,
                            NgayTao = DateTime.Now,
                            TienPhongTraTruoc = tienCoc,
                            TrangThai = checkInNgay ? AppConstants.TrangThaiBooking.DangO : AppConstants.TrangThaiBooking.DatTruoc,
                            // Gán quan hệ object thay vì IdKhachHang trực tiếp
                            // vì KhachHang mới chưa có Id cho đến khi SubmitChanges
                            KhachHang = khachHang
                        };
                        db.PhieuDatPhongs.InsertOnSubmit(phieu);

                        // 4.1 Tạo Master Bill (Đơn Hàng)
                        int soNgay = (checkOut - checkIn).Days;
                        if (soNgay <= 0) soNgay = 1;

                        var bill = new DonHang
                        {
                            MaDonHang = $"DH{DateTime.Now:yyMMddHHmmssfff}",
                            IdNhanVien = idNhanVien,
                            IdPhienThuNgan = idPhienGiaoDich > 0 ? (int?)idPhienGiaoDich : null,
                            NguonBan = AppConstants.NguonBan.TrucTiep,
                            TrangThai = AppConstants.TrangThaiDonHang.DangMo,
                            TongTienHang = giaGoc * soNgay,
                            TongGiamGia = 0,
                            TienThueVAT = 0,
                            TienPhiDichVu = 0,
                            NgayTao = DateTime.Now,
                            KhachHang = khachHang
                        };
                        db.DonHangs.InsertOnSubmit(bill);

                        // 4.2 Tạo Chi Tiết Đơn Hàng (Tiền phòng)
                        var ctDH = new ChiTietDonHang
                        {
                            DonHang = bill,
                            IdSanPham = phong.IdLoaiPhong,
                            SoLuong = soNgay,
                            DonGiaThucTe = giaGoc,
                            GhiChu = $"Tiền phòng {phong.MaPhong}"
                        };
                        db.ChiTietDonHangs.InsertOnSubmit(ctDH);

                        // 5. Tạo Chi Tiết Đặt Phòng
                        var chiTiet = new ChiTietDatPhong
                        {
                            PhieuDatPhong = phieu,
                            ChiTietDonHang = ctDH,
                            IdLoaiPhong = phong.IdLoaiPhong,
                            IdPhong = idPhong,
                            GiaBanDem = giaGoc,
                            TrangThai = checkInNgay ? AppConstants.TrangThaiChiTietDatPhong.DaCheckIn : AppConstants.TrangThaiChiTietDatPhong.ChoDen,
                            NgayCheckIn = checkInNgay ? checkIn : (DateTime?)null,
                            NgayCheckOut = checkOut
                        };
                        db.ChiTietDatPhongs.InsertOnSubmit(chiTiet);

                        // 6. Cập nhật trạng thái Phòng thực tế
                        if (checkInNgay)
                            phong.TrangThai = AppConstants.TrangThaiPhong.DangO;

                        // Lưu toàn bộ 1 lần duy nhất nếu lỗi thì rollback
                        db.SubmitChanges();

                        // Link RFID sau khi KhachHang đã có Id (sau SubmitChanges)
                        if (checkInNgay && !string.IsNullOrEmpty(maTheRFID))
                            DAL.Repositories.TaiChinh.DAL_TheRFID.Instance.GanTheMoi(maTheRFID, khachHang.IdDoiTac, null, 0);

                        transaction.Commit();
                        return OperationResult<bool>.Ok(true);
                    }
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message);
            }
        }

        #endregion

        #region Xử lý Check-in

        public OperationResult<bool> XuLyCheckIn(int idPhieuDatPhong, int idNhanVien, int idPhienThuNgan, string maTheRFID = null)
        {
            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    // Sử dụng Transaction để đảm bảo tính nguyên vẹn dữ liệu
                    db.Connection.Open();
                    using (var transaction = db.Connection.BeginTransaction())
                    {
                        db.Transaction = transaction;

                        // Lấy phiếu đặt phòng
                        var phieu = db.PhieuDatPhongs.FirstOrDefault(p => p.Id == idPhieuDatPhong);
                        if (phieu == null) 
                            return OperationResult<bool>.Fail("ERR_PHIEU_DP_KHONG_TON_TAI");
                        
                        if (phieu.TrangThai != AppConstants.TrangThaiBooking.DatTruoc)
                            return OperationResult<bool>.Fail("ERR_PHIEU_DA_CHECKIN_HUY");

                        // Cập nhật trạng thái phiếu mẹ
                        phieu.TrangThai = AppConstants.TrangThaiBooking.DangO;

                        // Cập nhật các phòng chi tiết
                        var dsChiTiet = db.ChiTietDatPhongs.Where(ct => ct.IdPhieuDatPhong == idPhieuDatPhong).ToList();
                        foreach (var ct in dsChiTiet)
                        {
                            ct.TrangThai = AppConstants.TrangThaiChiTietDatPhong.DaCheckIn;
                            ct.NgayCheckIn = DateTime.Now;

                            // Cập nhật trạng thái phòng thực tế
                            if (ct.IdPhong.HasValue)
                            {
                                var phong = db.Phongs.FirstOrDefault(p => p.Id == ct.IdPhong.Value);
                                if (phong != null)
                                {
                                    // Chốt chặn Nếu phòng đang có người ở thì rollback ngay lập tức
                                    if (phong.TrangThai != AppConstants.TrangThaiPhong.Trong 
                                        && phong.TrangThai != AppConstants.TrangThaiPhong.ChoDon)
                                    {
                                        throw new Exception($"ERR_PHONG_DANG_BAN|{phong.MaPhong}");
                                    }
                                    phong.TrangThai = AppConstants.TrangThaiPhong.DangO;
                                }
                            }
                        }

                        // Link RFID
                        if (!string.IsNullOrEmpty(maTheRFID) && phieu.IdKhachHang > 0)
                        {
                            DAL.Repositories.TaiChinh.DAL_TheRFID.Instance.GanTheMoi(maTheRFID, phieu.IdKhachHang.Value, null, 0);
                        }

                        // Lưu thay đổi vào DB
                        db.SubmitChanges();
                        transaction.Commit();

                        return OperationResult<bool>.Ok(true);
                    }
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message);
            }
        }



        public OperationResult<DTO_ThongTinKhachCheckOut> LayThongTinKhachHangCheckOut(int idChiTietDatPhong)
        {
            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    var ct = db.ChiTietDatPhongs.FirstOrDefault(x => x.Id == idChiTietDatPhong);
                    if (ct == null)
                        return OperationResult<DTO_ThongTinKhachCheckOut>.Fail("ERR_DAT_PHONG_KHONG_TON_TAI");

                    int? idPhieu = ct.IdPhieuDatPhong;
                    if (idPhieu == null || idPhieu <= 0)
                        return OperationResult<DTO_ThongTinKhachCheckOut>.Fail("ERR_PHIEU_DAT_PHONG_KHONG_HOP_LE");

                    var phieu = db.PhieuDatPhongs.FirstOrDefault(p => p.Id == idPhieu);
                    if (phieu == null)
                        return OperationResult<DTO_ThongTinKhachCheckOut>.Fail("ERR_DAT_PHONG_KHONG_TON_TAI");

                    int? idKh = phieu.IdKhachHang;
                    string hangTV = "";
                    int diem = 0;

                    if (idKh.HasValue && idKh > 0)
                    {
                        var kh = db.KhachHangs.FirstOrDefault(k => k.IdDoiTac == idKh.Value);
                        hangTV = kh != null ? (kh.HangThanhVien ?? "") : "";
                        diem = db.LichSuDiems.Where(ld => ld.IdKhachHang == idKh.Value).Sum(ld => (int?)ld.SoDiem) ?? 0;
                    }

                    decimal tienCoc = phieu.TienPhongTraTruoc;

                    // Tính tổng tiền dịch vụ phát sinh (vỡ ly, minibar...) từ DonHang liên kết
                    decimal tienDichVu = 0;
                    if (ct.IdChiTietDonHang > 0)
                    {
                        var ctDon = db.ChiTietDonHangs.FirstOrDefault(x => x.Id == ct.IdChiTietDonHang);
                        if (ctDon != null)
                        {
                            tienDichVu = db.ChiTietDonHangs.Where(x => x.IdDonHang == ctDon.IdDonHang)
                                        .Sum(x => (decimal?)(x.SoLuong * x.DonGiaThucTe)) ?? 0;
                        }
                    }

                    return OperationResult<DTO_ThongTinKhachCheckOut>.Ok(new DTO_ThongTinKhachCheckOut
                    {
                        IdKhachHang = idKh ?? 0,
                        HangThanhVien = hangTV,
                        DiemKhaDung = diem,
                        TienCoc = tienCoc,
                        TienDichVu = tienDichVu
                    });
                }
            }
            catch (Exception ex)
            {
                return OperationResult<DTO_ThongTinKhachCheckOut>.Fail(ex.Message);
            }
        }

        #endregion

        #region Xử lý Check-out (Bao gồm logic cấn trừ công nợ & Lố giờ)

        public OperationResult<bool> XuLyCheckOut_MotPhong(int idChiTietDatPhong, int idNhanVien, int idPhienThuNgan, 
            decimal soTienPhuThu, string ghiChuPhuThu, 
            string phuongThuc, decimal giamGia, int diemQuyDoi, int? idKhuyenMai, decimal tienKhachTra, int? idViDienTu = null)
        {
            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    db.Connection.Open();
                    using (var transaction = db.Connection.BeginTransaction())
                    {
                        db.Transaction = transaction;

                        // 1. Lấy chi tiết phòng đang ở
                        var ct = db.ChiTietDatPhongs.FirstOrDefault(x => x.Id == idChiTietDatPhong);
                        if (ct == null) return OperationResult<bool>.Fail("ERR_PHONG_KHONG_TON_TAI");
                        if (ct.TrangThai != AppConstants.TrangThaiChiTietDatPhong.DaCheckIn)
                            return OperationResult<bool>.Fail("ERR_PHONG_CHUA_CHECKIN");

                        // Guard: Không cho phép vừa áp dụng Khuyến Mãi vừa trừ Điểm tích lũy
                        if (idKhuyenMai.HasValue && idKhuyenMai.Value > 0 && diemQuyDoi > 0)
                            return OperationResult<bool>.Fail("ERR_KHONG_AP_DUNG_DONG_THOI_KM_VA_DIEM");

                        // 2. Lấy giá và Phụ thu lố giờ (Tự động từ Backend)
                        decimal tienPhuThuLoGio = 0;
                        string ghiChuLoGio = "";
                        var ketQuaTinhTien = BUS_LuuTru_TinhToan.Instance.TinhTienPhongVaPhatLoGio(idChiTietDatPhong, out tienPhuThuLoGio, out ghiChuLoGio);
                        if (!ketQuaTinhTien.Success) return OperationResult<bool>.Fail(ketQuaTinhTien.ErrorMessage);

                        // 3. Tìm Master Bill qua FK 
                        var phieu = db.PhieuDatPhongs.FirstOrDefault(p => p.Id == ct.IdPhieuDatPhong);
                        if (phieu == null) return OperationResult<bool>.Fail($"ERR_PHIEU_ME_KHONG_TON_TAI|ID_PHIEU:{ct.IdPhieuDatPhong}");

                        // Validate tiền khách trả đủ trước khi chạm vào DB
                        // soTienConLai = tổng bill - tiền cọc đã nộp lúc đặt phòng
                        decimal tienCocDaNop = phieu.TienPhongTraTruoc;
                        decimal tongBillUocTinh = (ketQuaTinhTien.Data) + tienPhuThuLoGio + soTienPhuThu - giamGia;
                        decimal soTienConLai = tongBillUocTinh - tienCocDaNop;
                        if (soTienConLai < 0) soTienConLai = 0;

                        if (tienKhachTra < soTienConLai)
                            return OperationResult<bool>.Fail(AppConstants.ErrorMessages.ERR_CHECKOUT_TIEN_KHONG_DU);

                        var ctDonHang = db.ChiTietDonHangs.FirstOrDefault(x => x.Id == ct.IdChiTietDonHang);
                        DonHang bill = ctDonHang != null ? db.DonHangs.FirstOrDefault(d => d.Id == ctDonHang.IdDonHang) : null;
                        if (bill == null)
                        {
                            bill = new DonHang
                            {
                                MaDonHang = $"DH{DateTime.Now:yyMMddHHmmssfff}",
                                IdKhachHang = phieu.IdKhachHang,
                                IdNhanVien = idNhanVien,
                                IdPhienThuNgan = idPhienThuNgan > 0 ? (int?)idPhienThuNgan : null,
                                NguonBan = AppConstants.NguonBan.TrucTiep,
                                TrangThai = AppConstants.TrangThaiDonHang.ChoThanhToan,
                                TongTienHang = 0,
                                NgayTao = DateTime.Now
                            };
                            db.DonHangs.InsertOnSubmit(bill);
                            db.SubmitChanges(); 
                        }

                        // 4. Chèn dòng Phụ thu — bắt buộc phải có SP hệ thống SYS_PHU_THU
                        // Không fallback về Id=1 vì sẽ gắn phụ thu vào sản phẩm sai, làm sai báo cáo doanh thu
                        var sysPhuThu = db.SanPhams.FirstOrDefault(s => s.MaSanPham == "SYS_PHU_THU");
                        if (sysPhuThu == null)
                            return OperationResult<bool>.Fail(AppConstants.ErrorMessages.ERR_SYS_PHU_THU_KHONG_TON_TAI);

                        int idSpPhuThu = sysPhuThu.Id;

                        // Luồng tự động (Máy tính - không cho phép người sửa)
                        if (tienPhuThuLoGio > 0)
                        {
                            db.ChiTietDonHangs.InsertOnSubmit(new ChiTietDonHang
                            {
                                IdDonHang = bill.Id,
                                IdSanPham = idSpPhuThu,
                                SoLuong = 1,
                                DonGiaThucTe = tienPhuThuLoGio,
                                GhiChu = ghiChuLoGio
                            });
                        }

                        // Luồng thủ công (Do lễ tân tự nhập từ UI - bể ly, hỏng đồ)
                        if (soTienPhuThu > 0)
                        {
                            db.ChiTietDonHangs.InsertOnSubmit(new ChiTietDonHang
                            {
                                IdDonHang = bill.Id,
                                IdSanPham = idSpPhuThu,
                                SoLuong = 1,
                                DonGiaThucTe = soTienPhuThu,
                                GhiChu = string.IsNullOrEmpty(ghiChuPhuThu) ? "Phụ thu khác" : ghiChuPhuThu
                            });
                        }

                        // 5. Cập nhật trạng thái trả phòng
                        ct.TrangThai = AppConstants.TrangThaiChiTietDatPhong.DaCheckOut;
                        ct.NgayCheckOut = DateTime.Now;

                        if (ct.IdPhong.HasValue)
                        {
                            var phong = db.Phongs.FirstOrDefault(p => p.Id == ct.IdPhong.Value);
                            // Chuyển sang trạng thái chờ dọn dẹp để buồng phòng làm việc
                            if (phong != null) phong.TrangThai = AppConstants.TrangThaiPhong.ChoDon; 
                        }

                        // 6. Check-out cho Master Bill
                        bool conPhongNaoDangO = db.ChiTietDatPhongs.Any(x => x.IdPhieuDatPhong == ct.IdPhieuDatPhong 
                                                                          && x.Id != ct.Id 
                                                                          && x.TrangThai == AppConstants.TrangThaiChiTietDatPhong.DaCheckIn);
                        
                        if (conPhongNaoDangO)
                        {
                            bill.TrangThai = AppConstants.TrangThaiDonHang.MotPhan;
                        }
                        else
                        {
                            phieu.TrangThai = AppConstants.TrangThaiBooking.DaTra;
                            bill.NgayTao = DateTime.Now;
                            bill.TrangThai = AppConstants.TrangThaiDonHang.DaThanhToan;
                        }

                        // 7. Cập nhật Bill Master
                        // Lưu các dòng phụ thu vừa thêm
                        db.SubmitChanges();

                        // Query lại toàn bộ chi tiết (đã bao gồm dòng phụ thu vừa thêm)
                        var allDetails = db.ChiTietDonHangs.Where(x => x.IdDonHang == bill.Id).ToList();

                        // Lưu các dòng phụ thu vừa thêm
                        foreach (var d in allDetails)
                        {
                            if (d.ThanhTien == null || d.ThanhTien == 0)
                                d.ThanhTien = d.SoLuong * d.DonGiaThucTe;
                        }

                        // Tổng tiền hàng = tổng tiền tất cả các dòng chi tiết
                        bill.TongTienHang = allDetails.Sum(x => x.ThanhTien ?? 0);
                        bill.TongGiamGia = giamGia;
                        bill.TongThanhToan = bill.TongTienHang - bill.TongGiamGia + bill.TienThueVAT + bill.TienPhiDichVu;
                        db.SubmitChanges();

                        // 8. Lưu chứng từ thanh toán (ChungTuTC) nếu khách trả thêm tiền
                        if (tienKhachTra > 0)
                        {
                            var chungTuTC = new ChungTuTC
                            {
                                MaChungTu = "PT-" + bill.MaDonHang,
                                LoaiChungTu = AppConstants.LoaiChungTuTC.THU_THANHTOAN,
                                IdDonHang = bill.Id,
                                IdPhienThuNgan = idPhienThuNgan,
                                MaGiaoDichClient = Guid.NewGuid(),
                                SoTien = tienKhachTra,
                                PhuongThuc = phuongThuc,
                                TrangThai = AppConstants.TrangThaiChungTuTC.DaDuyet,
                                IdNguoiTao = idNhanVien,
                                NgayChungTu = DateTime.Now,
                                NgayTao = DateTime.Now
                            };
                            db.ChungTuTCs.InsertOnSubmit(chungTuTC);
                            db.SubmitChanges();

                            var ctPay = new ChiTietThanhToan
                            {
                                IdChungTu = chungTuTC.Id,
                                PhuongThuc = phuongThuc,
                                SoTien = tienKhachTra,
                                GhiChu = "Thanh toán trả phòng"
                            };
                            db.ChiTietThanhToans.InsertOnSubmit(ctPay);
                            db.SubmitChanges();
                            // 8b. Trừ tiền ví RFID nếu có
                            if (phuongThuc == AppConstants.PhuongThucTT.ViRFID && idViDienTu.HasValue)
                            {
                                var socaiVi = new SoCaiVi
                                {
                                    IdVi = idViDienTu.Value,
                                    LoaiPhep = AppConstants.LoaiPhepVi.Tru,
                                    SoTien = tienKhachTra,
                                    IdChungTu = chungTuTC.Id,
                                    MoTa = $"Thanh toán trả phòng {phieu.MaDatPhong}",
                                    NguoiTao = idNhanVien,
                                    NgayTao = DateTime.Now
                                };
                                db.SoCaiVis.InsertOnSubmit(socaiVi);
                            }
                        }

                        // 9. Lưu Khuyến Mãi
                        if (idKhuyenMai.HasValue && giamGia > 0)
                        {
                            var lichSuKM = new KhuyenMai_LichSu
                            {
                                IdKhuyenMai = idKhuyenMai.Value,
                                IdDonHang = bill.Id,
                                IdKhachHang = phieu.IdKhachHang,
                                SoTienGiam = giamGia
                            };
                            db.KhuyenMai_LichSus.InsertOnSubmit(lichSuKM);
                        }

                        // 10. Trừ Điểm và Tích Điểm
                        if (phieu.IdKhachHang.HasValue)
                        {
                            int diemHienTai = db.LichSuDiems
                                .Where(ld => ld.IdKhachHang == phieu.IdKhachHang)
                                .Sum(ld => (int?)ld.SoDiem) ?? 0;

                            if (diemQuyDoi > 0)
                            {
                                var lsTru = new LichSuDiem
                                {
                                    IdKhachHang = phieu.IdKhachHang.Value,
                                    LoaiGiaoDich = AppConstants.LoaiGiaoDichDiem.TruDiem,
                                    SoDiem = -diemQuyDoi,
                                    SoDuSauGD = diemHienTai - diemQuyDoi,
                                    IdDonHang = bill.Id,
                                    MoTa = "Dùng điểm thanh toán trả phòng",
                                    NgayTao = DateTime.Now
                                };
                                db.LichSuDiems.InsertOnSubmit(lsTru);
                                diemHienTai -= diemQuyDoi; 
                            }

                            // Cộng điểm nếu trả tiền thật
                            if (tienKhachTra > 0)
                            {
                                var configDIEM = db.CauHinhs.FirstOrDefault(c => c.Khoa == AppConstants.ConfigKeys.DIEM_QUY_DOI);
                                decimal tyLeQuyDoi = 1000m;
                                if (configDIEM != null && decimal.TryParse(configDIEM.GiaTri, out var val) && val > 0) 
                                    tyLeQuyDoi = val;

                                int diemCong = (int)(tienKhachTra / tyLeQuyDoi);
                                if (diemCong > 0)
                                {
                                    var lsCong = new LichSuDiem
                                    {
                                        IdKhachHang = phieu.IdKhachHang.Value,
                                        LoaiGiaoDich = AppConstants.LoaiGiaoDichDiem.CongDiem,
                                        SoDiem = diemCong,
                                        SoDuSauGD = diemHienTai + diemCong,
                                        IdDonHang = bill.Id,
                                        MoTa = "Tích điểm trả phòng",
                                        NgayTao = DateTime.Now
                                    };
                                    db.LichSuDiems.InsertOnSubmit(lsCong);
                                }
                            }
                        }

                        db.SubmitChanges();
                        transaction.Commit();

                        return OperationResult<bool>.Ok(true);
                    }
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message);
            }
        }

        #endregion

        #region Context Menu Sơ đồ phòng

        public OperationResult<bool> PhuThuDichVu(int idPhong, decimal soTien, string lyDo, int idNhanVien)
        {
            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    db.Connection.Open();
                    using (var transaction = db.Connection.BeginTransaction())
                    {
                        db.Transaction = transaction;

                        // Tìm chi tiết đặt phòng đang ở
                        var ct = db.ChiTietDatPhongs.FirstOrDefault(x => x.IdPhong == idPhong && x.TrangThai == AppConstants.TrangThaiChiTietDatPhong.DaCheckIn);
                        if (ct == null) return OperationResult<bool>.Fail("ERR_PHONG_KHONG_CO_KHACH");

                        var phieu = ct.PhieuDatPhong;
                        // Tìm bill qua FK
                        var ctDonHang = db.ChiTietDonHangs.FirstOrDefault(x => x.Id == ct.IdChiTietDonHang);
                        DonHang bill = ctDonHang != null ? db.DonHangs.FirstOrDefault(d => d.Id == ctDonHang.IdDonHang) : null;
                        
                        if (bill == null)
                        {
                            bill = new DonHang
                            {
                                MaDonHang = "DH" + DateTime.Now.ToString("yyMMddHHmmssfff"),
                                IdKhachHang = phieu.IdKhachHang,
                                IdNhanVien = idNhanVien,
                                NguonBan = AppConstants.NguonBan.TrucTiep,
                                TrangThai = AppConstants.TrangThaiDonHang.DangMo,
                                TongTienHang = 0,
                                NgayTao = DateTime.Now
                            };
                            db.DonHangs.InsertOnSubmit(bill);
                            db.SubmitChanges(); 
                        }

                        // Lấy SP hệ thống SYS_PHU_THU
                        var sysPhuThu = db.SanPhams.FirstOrDefault(s => s.MaSanPham == "SYS_PHU_THU");
                        if (sysPhuThu == null)
                            return OperationResult<bool>.Fail(AppConstants.ErrorMessages.ERR_SYS_PHU_THU_KHONG_TON_TAI);

                        int idSpPhuThu = sysPhuThu.Id;

                        db.ChiTietDonHangs.InsertOnSubmit(new ChiTietDonHang
                        {
                            IdDonHang = bill.Id,
                            IdSanPham = idSpPhuThu,
                            SoLuong = 1,
                            DonGiaThucTe = soTien,
                            ThanhTien = soTien,
                            GhiChu = $"[Phòng {db.Phongs.FirstOrDefault(p=>p.Id == idPhong)?.MaPhong}] {lyDo}"
                        });

                        db.SubmitChanges(); 
                        bill.TongTienHang = db.ChiTietDonHangs.Where(x => x.IdDonHang == bill.Id).Sum(x => x.ThanhTien ?? 0);
                        bill.TongThanhToan = bill.TongTienHang - bill.TongGiamGia + bill.TienThueVAT + bill.TienPhiDichVu;

                        db.SubmitChanges();
                        transaction.Commit();
                        return OperationResult<bool>.Ok(true);
                    }
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message);
            }
        }

        public OperationResult<bool> CapNhatTrangThaiPhong(int idPhong, string trangThaiMoi, int idNhanVien = 1)
        {
            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    var p = db.Phongs.FirstOrDefault(x => x.Id == idPhong);
                    if (p == null) return OperationResult<bool>.Fail("ERR_PHONG_KHONG_TON_TAI");
                    
                    if (p.TrangThai == AppConstants.TrangThaiPhong.DangO)
                        return OperationResult<bool>.Fail("ERR_PHONG_DANG_CO_KHACH_KHONG_DOI_TRANG_THAI");

                    // Lấy luồng trạng thái từ bảng LuongTrangThai
                    var nextStates = GetNextStates("Phong", p.TrangThai);
                    if (!nextStates.Contains(trangThaiMoi))
                        return OperationResult<bool>.Fail("ERR_CHUYEN_TRANG_THAI_KHONG_HOP_LE");

                    // Ghi lịch sử chuyển trạng thái
                    db.LichSuTrangThais.InsertOnSubmit(new LichSuTrangThai
                    {
                        ThucThe = "Phong",
                        IdThucThe = idPhong,
                        TuTrangThai = p.TrangThai,
                        DenTrangThai = trangThaiMoi,
                        IdNguoiThucHien = idNhanVien,
                        ThoiGian = DateTime.Now
                    });

                    p.TrangThai = trangThaiMoi;
                    db.SubmitChanges();
                    return OperationResult<bool>.Ok(true);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message);
            }
        }

        public OperationResult<bool> HuyDatPhong(int idDatPhong, int idNhanVien)
        {
            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    var phieu = db.PhieuDatPhongs.FirstOrDefault(x => x.Id == idDatPhong);
                    if (phieu == null) return OperationResult<bool>.Fail("ERR_PHIEU_DP_KHONG_TON_TAI");
                    
                    if (phieu.TrangThai != AppConstants.TrangThaiBooking.DatTruoc)
                        return OperationResult<bool>.Fail("ERR_PHIEU_DA_CHECKIN_HUY");

                    phieu.TrangThai = AppConstants.TrangThaiBooking.DaHuy;
                    
                    var chiTiets = db.ChiTietDatPhongs.Where(x => x.IdPhieuDatPhong == idDatPhong);
                    foreach(var ct in chiTiets)
                    {
                        ct.TrangThai = AppConstants.TrangThaiChiTietDatPhong.DaHuy;
                        if (ct.IdPhong.HasValue)
                        {
                            var p = db.Phongs.FirstOrDefault(ph => ph.Id == ct.IdPhong);
                            if (p != null && p.TrangThai != AppConstants.TrangThaiPhong.DangO && p.TrangThai != AppConstants.TrangThaiPhong.ChoDon)
                            {
                                p.TrangThai = AppConstants.TrangThaiPhong.Trong;
                            }
                        }
                    }

                    // Hoàn tiền cọc nếu có (chính sách: hủy trước 3 ngày = hoàn 100%, trong 3 ngày = không hoàn)
                    decimal tienCoc = phieu.TienPhongTraTruoc;
                    if (tienCoc > 0)
                    {
                        int ngayConLai = (phieu.NgayNhanPhong.Date - DateTime.Today).Days;
                        decimal tienHoan = ngayConLai >= 3 ? tienCoc : 0m;

                        if (tienHoan > 0)
                        {
                            var chungTu = new ChungTuTC
                            {
                                MaChungTu = $"HC-{phieu.MaDatPhong}",
                                LoaiChungTu = AppConstants.LoaiChungTuTC.HOAN_COC,
                                MaGiaoDichClient = Guid.NewGuid(),
                                SoTien = tienHoan,
                                PhuongThuc = AppConstants.PhuongThucTT.TienMat,
                                TrangThai = AppConstants.TrangThaiChungTuTC.DaDuyet,
                                IdNguoiTao = idNhanVien,
                                NgayChungTu = DateTime.Now,
                                NgayTao = DateTime.Now
                            };
                            db.ChungTuTCs.InsertOnSubmit(chungTu);
                        }
                    }

                    db.SubmitChanges();
                    return OperationResult<bool>.Ok(true);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message);
            }
        }

        public OperationResult<bool> DoiPhong(int idPhongCu, int idPhongMoi, int idNhanVien, string lyDo)
        {
            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    db.Connection.Open();
                    using (var transaction = db.Connection.BeginTransaction())
                    {
                        db.Transaction = transaction;

                        // 1. Kiểm tra phòng cũ
                        var ctCu = db.ChiTietDatPhongs.FirstOrDefault(x => x.IdPhong == idPhongCu && x.TrangThai == AppConstants.TrangThaiChiTietDatPhong.DaCheckIn);
                        if (ctCu == null) return OperationResult<bool>.Fail("ERR_PHONG_CHUA_CHECKIN");

                        var pCu = db.Phongs.FirstOrDefault(x => x.Id == idPhongCu);
                        
                        // 2. Kiểm tra phòng mới
                        var pMoi = db.Phongs.FirstOrDefault(x => x.Id == idPhongMoi);
                        if (pMoi == null) return OperationResult<bool>.Fail("ERR_PHONG_KHONG_TON_TAI");
                        if (pMoi.TrangThai != AppConstants.TrangThaiPhong.Trong && pMoi.TrangThai != AppConstants.TrangThaiPhong.ChoDon)
                            return OperationResult<bool>.Fail("ERR_PHONG_DANG_BAN");

                        // 3. Xử lý chia ChiTietDatPhong & ChiTietDonHang
                        DateTime now = DateTime.Now;
                        int soNgayDaO = (now.Date - ctCu.NgayCheckIn.Value.Date).Days;
                        if (soNgayDaO <= 0) soNgayDaO = 1;

                        int tongNgayDat = (ctCu.NgayCheckOut.Value.Date - ctCu.NgayCheckIn.Value.Date).Days;
                        if (tongNgayDat <= 0) tongNgayDat = 1;

                        int soNgayConLai = tongNgayDat - soNgayDaO;
                        if (soNgayConLai < 0) soNgayConLai = 0;

                        // Lấy đơn hàng
                        var ctDHCold = db.ChiTietDonHangs.FirstOrDefault(x => x.Id == ctCu.IdChiTietDonHang);

                        if (pCu.IdLoaiPhong == pMoi.IdLoaiPhong)
                        {
                            // Đổi cùng loại phòng -> Chỉ cần update ID phòng
                            ctCu.IdPhong = idPhongMoi;
                            if (ctDHCold != null) ctDHCold.GhiChu += $" (Đổi từ phòng {pCu.MaPhong})";
                        }
                        else
                        {
                            // Khác loại phòng -> Split
                            if (ctDHCold != null)
                            {
                                ctDHCold.SoLuong = soNgayDaO;
                                ctDHCold.ThanhTien = ctDHCold.SoLuong * ctDHCold.DonGiaThucTe;

                                if (soNgayConLai > 0)
                                {
                                    // Tạo CT Đơn Hàng mới
                                    decimal giaMoi = BUS.Services.DanhMuc.BUS_BangGia.Instance.GetDynamicPrice(pMoi.IdLoaiPhong, now, out _);
                                    var ctDHNew = new ChiTietDonHang
                                    {
                                        IdDonHang = ctDHCold.IdDonHang,
                                        IdSanPham = pMoi.IdLoaiPhong,
                                        SoLuong = soNgayConLai,
                                        DonGiaThucTe = giaMoi,
                                        ThanhTien = soNgayConLai * giaMoi,
                                        GhiChu = $"Tiền phòng {pMoi.MaPhong} (Đổi từ {pCu.MaPhong})"
                                    };
                                    db.ChiTietDonHangs.InsertOnSubmit(ctDHNew);
                                    db.SubmitChanges(); // Lưu để lấy ID

                                    // Đóng CT cũ, tạo CT mới
                                    ctCu.NgayCheckOut = now;
                                    ctCu.TrangThai = AppConstants.TrangThaiChiTietDatPhong.DaCheckOut; // Đã trả phòng cũ

                                    var ctNew = new ChiTietDatPhong
                                    {
                                        IdPhieuDatPhong = ctCu.IdPhieuDatPhong,
                                        IdChiTietDonHang = ctDHNew.Id,
                                        IdLoaiPhong = pMoi.IdLoaiPhong,
                                        IdPhong = idPhongMoi,
                                        GiaBanDem = giaMoi,
                                        TrangThai = AppConstants.TrangThaiChiTietDatPhong.DaCheckIn,
                                        NgayCheckIn = now,
                                        NgayCheckOut = ctCu.NgayCheckOut.Value.AddDays(soNgayConLai)
                                    };
                                    db.ChiTietDatPhongs.InsertOnSubmit(ctNew);
                                    
                                    var billDoi = db.DonHangs.FirstOrDefault(d => d.Id == ctDHCold.IdDonHang);
                                    if (billDoi != null)
                                    {
                                        decimal giaCu = ctDHCold.DonGiaThucTe;
                                        billDoi.TongTienHang = billDoi.TongTienHang - (soNgayConLai * giaCu) + (soNgayConLai * giaMoi);
                                    }
                                }
                                else
                                {
                                    // Chuyển sang phòng mới nhưng hết ngày (lố giờ) -> Chỉ cần update ID phòng cho dễ
                                    ctCu.IdPhong = idPhongMoi;
                                    ctCu.IdLoaiPhong = pMoi.IdLoaiPhong;
                                }
                            }
                        }

                        // 4. Update trạng thái vật lý
                        pCu.TrangThai = AppConstants.TrangThaiPhong.ChoDon;
                        pMoi.TrangThai = AppConstants.TrangThaiPhong.DangO;

                        // 5. Ghi Log phụ thu / ghi chú đổi phòng nếu có
                        if (!string.IsNullOrEmpty(lyDo))
                        {
                            var billId = ctDHCold?.IdDonHang;
                            if (billId.HasValue)
                            {
                                var sysPhuThu = db.SanPhams.FirstOrDefault(s => s.MaSanPham == "SYS_PHU_THU");
                                int idSpPhuThu = sysPhuThu != null ? sysPhuThu.Id : 1; 

                                db.ChiTietDonHangs.InsertOnSubmit(new ChiTietDonHang
                                {
                                    IdDonHang = billId.Value,
                                    IdSanPham = idSpPhuThu,
                                    SoLuong = 1,
                                    DonGiaThucTe = 0,
                                    GhiChu = $"[Đổi {pCu.MaPhong} -> {pMoi.MaPhong}] {lyDo}"
                                });
                            }
                        }

                        db.SubmitChanges();
                        transaction.Commit();
                        return OperationResult<bool>.Ok(true);
                    }
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message);
            }
        }

        public OperationResult<bool> GiaHanPhong(int idPhong, DateTime ngayCheckOutMoi, int idNhanVien)
        {
            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    db.Connection.Open();
                    using (var transaction = db.Connection.BeginTransaction())
                    {
                        db.Transaction = transaction;

                        // 1. Kiểm tra phòng
                        var ct = db.ChiTietDatPhongs.FirstOrDefault(x => x.IdPhong == idPhong && x.TrangThai == AppConstants.TrangThaiChiTietDatPhong.DaCheckIn);
                        if (ct == null) return OperationResult<bool>.Fail("ERR_PHONG_CHUA_CHECKIN");

                        if (ngayCheckOutMoi.Date <= ct.NgayCheckOut.Value.Date)
                            return OperationResult<bool>.Fail("ERR_NGAY_GIA_HAN_PHAI_LON_HON_NGAY_CU");

                        // 2. Tính số ngày tăng thêm
                        int soNgayThem = (ngayCheckOutMoi.Date - ct.NgayCheckOut.Value.Date).Days;

                        // 3. Cập nhật chi tiết đặt phòng & Phiếu mẹ
                        ct.NgayCheckOut = ct.NgayCheckOut.Value.AddDays(soNgayThem);
                        
                        var phieu = db.PhieuDatPhongs.FirstOrDefault(x => x.Id == ct.IdPhieuDatPhong);
                        if (phieu != null && phieu.NgayTraPhong.Date < ct.NgayCheckOut.Value.Date)
                        {
                            phieu.NgayTraPhong = ct.NgayCheckOut.Value; // Kéo dài phiếu mẹ theo phòng lâu nhất
                        }

                        // 4. Cập nhật chi tiết đơn hàng (tiền phòng) và tổng bill
                        var ctDH = db.ChiTietDonHangs.FirstOrDefault(x => x.Id == ct.IdChiTietDonHang);
                        if (ctDH != null)
                        {
                            ctDH.SoLuong += soNgayThem;
                            ctDH.ThanhTien = ctDH.SoLuong * ctDH.DonGiaThucTe;
                            ctDH.GhiChu += $" (Gia hạn thêm {soNgayThem} ngày)";

                            // Đồng bộ tổng tiền DonHang theo tất cả chi tiết
                            var billGH = db.DonHangs.FirstOrDefault(d => d.Id == ctDH.IdDonHang);
                            if (billGH != null)
                            {
                                billGH.TongTienHang = db.ChiTietDonHangs
                                    .Where(x => x.IdDonHang == billGH.Id)
                                    .Sum(x => x.ThanhTien ?? (x.SoLuong * x.DonGiaThucTe));
                                billGH.TongThanhToan = billGH.TongTienHang - billGH.TongGiamGia + billGH.TienThueVAT + billGH.TienPhiDichVu;
                            }
                        }

                        db.SubmitChanges();
                        transaction.Commit();
                        return OperationResult<bool>.Ok(true);
                    }
                }
            }
            catch (Exception ex)
            {
                return OperationResult<bool>.Fail(ex.Message);
            }
        }

        #endregion

        #region Thông báo đặt phòng (tất cả nguồn: Web, App, POS)

        /// <summary>
        /// Tính khoảng ngày theo bộ lọc rồi gọi DAL_LuuTru.
        /// BUS chỉ chứa business logic không truy cập DB trực tiếp.
        /// <param name="locTheo">TuanNay, HomNay, TatCa</param>
        /// <returns></returns>
        /// </summary>
        public OperationResult<List<DTO_DatPhongOnline>> LayDatPhongChoPhanCong(string locTheo = "TuanNay")
        {
            try
            {
                var today = DateTime.Now.Date;
                DateTime tuNgay, denNgay;

                if (locTheo == "HomNay")
                {
                    tuNgay = today;
                    denNgay = today.AddDays(1).AddSeconds(-1);
                }
                else if (locTheo == "TatCa")
                {
                    tuNgay = new DateTime(2000, 1, 1);
                    denNgay = today.AddYears(10);
                }
                else // TuanNay -> 7 ngày tới (bao gồm cả hôm nay)
                {
                    tuNgay = today;
                    denNgay = today.AddDays(7).AddSeconds(-1);
                }

                var data = DAL.Repositories.BanHang.DAL_LuuTru.Instance
                               .LayDatPhongChoPhanCong(tuNgay, denNgay);

                return OperationResult<List<DTO_DatPhongOnline>>.Ok(data);
            }
            catch (Exception ex)
            {
                return OperationResult<List<DTO_DatPhongOnline>>.Fail(ex.Message);
            }
        }

        #endregion

        #region Xem Bill phòng đang ở

        /// <summary>
        /// Lấy toàn bộ chi tiết bill (tiền phòng, minibar, phụ thu...) của 1 phòng đang ở.
        /// Dùng cho chức năng Xem Bill trên sơ đồ phòng.
        /// </summary>
        public OperationResult<List<DTO_CheckoutItem>> LayChiTietBillPhong(int idChiTietDatPhong)
        {
            try
            {
                using (var db = new DaiNamDBDataContext())
                {
                    var ct = db.ChiTietDatPhongs.FirstOrDefault(x => x.Id == idChiTietDatPhong);
                    if (ct == null) return OperationResult<List<DTO_CheckoutItem>>.Fail("ERR_DAT_PHONG_KHONG_TON_TAI");

                    // Tìm DonHang qua chain: ChiTietDatPhong -> ChiTietDonHang -> DonHang
                    var ctDH = db.ChiTietDonHangs.FirstOrDefault(x => x.Id == ct.IdChiTietDonHang);
                    if (ctDH == null) return OperationResult<List<DTO_CheckoutItem>>.Ok(new List<DTO_CheckoutItem>());

                    var allDetails = db.ChiTietDonHangs
                        .Where(x => x.IdDonHang == ctDH.IdDonHang)
                        .ToList();

                    var result = allDetails.Select(d => new DTO_CheckoutItem
                    {
                        TenMon = d.SanPham != null ? d.SanPham.TenSanPham : (d.GhiChu ?? "—"),
                        SoLuong = d.SoLuong,
                        DonGiaGoc = d.DonGiaThucTe,
                        ThanhTien = d.ThanhTien ?? (d.SoLuong * d.DonGiaThucTe)
                    }).ToList();

                    return OperationResult<List<DTO_CheckoutItem>>.Ok(result);
                }
            }
            catch (Exception ex)
            {
                return OperationResult<List<DTO_CheckoutItem>>.Fail(ex.Message);
            }
        }

        #endregion
    }
}


