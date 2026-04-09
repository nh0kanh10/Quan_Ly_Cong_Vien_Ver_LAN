using System;
using System.Linq;
using System.Collections.Generic;
using DAL;
using ET;
using System.Transactions;

namespace BUS
{
    public class BUS_GiaoDichVi
    {
        private readonly ITheRfidGateway _theRfidGateway;
        private readonly IViDienTuGateway _viGateway;
        private readonly IGiaoDichViGateway _giaoDichGateway;
        private readonly IPhieuThuGateway _phieuThuGateway;
        private readonly IKhachHangGateway _khachHangGateway;

        private static BUS_GiaoDichVi instance;
        public static BUS_GiaoDichVi Instance
        {
            get
            {
                if (instance == null) instance = new BUS_GiaoDichVi();
                return instance;
            }
        }
        public BUS_GiaoDichVi() : this(new DefaultTheRfidGateway(), new DefaultViDienTuGateway(), new DefaultGiaoDichViGateway(), new DefaultPhieuThuGateway(), new DefaultKhachHangGateway()) { }
        public BUS_GiaoDichVi(ITheRfidGateway theRfidGateway, IViDienTuGateway viGateway, IGiaoDichViGateway giaoDichGateway, IPhieuThuGateway phieuThuGateway, IKhachHangGateway khachHangGateway)
        {
            _theRfidGateway = theRfidGateway;
            _viGateway = viGateway;
            _giaoDichGateway = giaoDichGateway;
            _phieuThuGateway = phieuThuGateway;
            _khachHangGateway = khachHangGateway;
        }

        #region Nhận Diện & Trích Xuất Dữ Liệu Thẻ (Cổng Giao Tiếp Định Danh)

        /// <summary>
        /// Xác thực thẻ từ tiếp xúc và truy xuất hồ sơ Ví Điện Tử theo cơ chế trọn gói.
        /// Chặn thẻ mất cắp, kiểm định tính liền mạch của chủ sở hữu.
        /// </summary>
        public OperationResult<ET_ViDienTu> TraCuuViTheoRFID(string maRfid)
        {
            if (string.IsNullOrWhiteSpace(maRfid))
                return OperationResult<ET_ViDienTu>.Failed("Mã RFID không được để trống.");

            var theRfid = _theRfidGateway.LayTheoId(maRfid);
            if (theRfid == null)
                return OperationResult<ET_ViDienTu>.Failed("Không tìm thấy thẻ RFID: " + maRfid);

            if (theRfid.TrangThai != AppConstants.TrangThaiTheRfid.Active)
                return OperationResult<ET_ViDienTu>.Failed("Thẻ RFID đã bị khóa hoặc mất (Trạng thái: " + theRfid.TrangThai + ").");

            var vi = _viGateway.LayTheoId(theRfid.IdVi);
            if (vi == null)
                return OperationResult<ET_ViDienTu>.Failed("Không tìm thấy ví điện tử cho thẻ này.");

            return OperationResult<ET_ViDienTu>.Success(vi);
        }

        #endregion

        #region Xử Lý Dòng Tiền (Khởi Nhận & Tạm Thu)

        /// <summary>
        /// Luồng giao dịch nạp hạch toán vào tài khoản ví cá nhân.
        /// Tích hợp cơ chế Concurrency (OCC) để chống tràn bộ nhớ ghi đè khi khách hàng nạp tiền qua nhiều luồng cùng lúc.
        /// </summary>
        public OperationResult NapTien(string maRfid, decimal soTien, string phuongThuc)
        {
            if (soTien <= 0)
                return OperationResult.Failed("Cảnh báo: Quy định mệnh giá nạp phải lớn hơn 0.");

            if (soTien > 50000000)
                return OperationResult.Failed("Cảnh báo: Giao dịch vượt ngưỡng tối đa (50,000,000 đ) cho một thao tác.");

            var viResult = TraCuuViTheoRFID(maRfid);
            if (!viResult.IsSuccess)
                return OperationResult.Failed(viResult.ErrorMessage);

            var vi = viResult.Data;
            if (vi == null) return OperationResult.Failed("Hệ thống từ chối: Bất thường truy xuất ví điện tử.");

            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    // Bước 1: Tính toán hạch toán khoản dư hiện hữu
                    vi.SoDuKhaDung += soTien;
                    bool updateOk = _viGateway.Sua(vi);
                    if (!updateOk)
                        return OperationResult.Failed("Giao dịch gián đoạn: Từ chối xử lý định mức số dư.");

                    // Bước 2: Lưu vết kiểm toán dòng tiền nội bộ hệ thống thẻ
                    var giaoDich = new ET_GiaoDichVi
                    {
                        MaCode = "GD-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                        IdVi = vi.Id,
                        LoaiGiaoDich = AppConstants.LoaiGiaoDichVi.NapTien,
                        SoTien = soTien,
                        ThoiGian = DateTime.Now,
                        CreatedAt = DateTime.Now
                    };
                    int giaoDichId = _giaoDichGateway.ThemVaLayId(giaoDich);
                    if (giaoDichId <= 0)
                        return OperationResult.Failed("Lỗi hạ tầng: Truy vết giao dịch bất thành.");

                    // Bước 3: Chứng từ sổ cái báo cáo Doanh Thu phòng kế toán
                    var phieuThu = new ET_PhieuThu
                    {
                        MaCode = "PT-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                        IdGiaoDichVi = giaoDichId,
                        SoTien = soTien,
                        PhuongThuc = phuongThuc,
                        ThoiGian = DateTime.Now,
                        CreatedAt = DateTime.Now
                    };
                    bool phieuThuOk = _phieuThuGateway.Them(phieuThu);
                    if (!phieuThuOk)
                        return OperationResult.Failed("Lỗi hạ tầng: Sinh chứng từ điện tử thất bại.");

                    scope.Complete();
                    return OperationResult.Success();
                }
            }
            catch (System.Data.DBConcurrencyException)
            {
                // Bắt bẻ ghi đè OCC từ DAL đẩy lên. Tránh ứng dụng bị crash ném Unhandled Exception!
                return OperationResult.Failed("Xung đột giao dịch: Thẻ này đang được thao tác tại một máy đọc khác. Yêu cầu tải lại.");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Phát sinh sự cố kỹ thuật trong luồng nạp tiền: " + ex.Message);
            }
        }

        /// <summary>
        /// Chuyển đổi trạng thái từ Tiền Mặt thành Khoản Tạm Giữ (Ký Quỹ Đóng Băng).
        /// </summary>
        public OperationResult DongBangTienVi(string maRfid, decimal soTien, int? idDonHangLienQuan = null)
        {
            if (soTien <= 0) return OperationResult.Failed("Cảnh báo: Lượng tiền ký quỹ không hợp lệ.");
            var viResult = TraCuuViTheoRFID(maRfid);
            if (!viResult.IsSuccess) return OperationResult.Failed(viResult.ErrorMessage);
            var vi = viResult.Data;
            if (vi.SoDuKhaDung < soTien) return OperationResult.Failed("Giao dịch từ chối: Nguồn vốn giải ngân trên ví không đủ.");
            
            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    // Bước 1: Trừ tiền mặt khả dụng và Bơm vào Tiền cọc (Đóng băng)
                    vi.SoDuKhaDung -= soTien;
                    vi.SoDuDongBang += soTien; // Fix: Logic kế toán thiếu tích lũy số dư bị đóng băng
                    
                    bool updateOk = _viGateway.Sua(vi);
                    if (!updateOk) return OperationResult.Failed("Từ chối xử lý đóng băng.");
                    
                    var giaoDich = new ET_GiaoDichVi
                    {
                        MaCode = "COC-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                        IdVi = vi.Id,
                        LoaiGiaoDich = AppConstants.LoaiGiaoDichVi.ThuCoc, 
                        SoTien = soTien,
                        IdDonHangLienQuan = idDonHangLienQuan, // FIX: TRUY VẾT DÒNG TIỀN VÀO ĐƠN HÀNG NÀO
                        ThoiGian = DateTime.Now,
                        CreatedAt = DateTime.Now
                    };
                    int giaoDichId = _giaoDichGateway.ThemVaLayId(giaoDich);
                    if (giaoDichId <= 0) return OperationResult.Failed("Lỗi tạo biên bản ủy quyền.");
                    
                    scope.Complete();
                    return OperationResult.Success();
                }
            }
            catch (System.Data.DBConcurrencyException)
            {
                // Fix CAVE-6: OCC Race condition. Tránh bị văng văng (Crash) app!
                return OperationResult.Failed("Xung đột đồng thời: Thu ngân khác đang trừ tiền ví này. Thao tác bị khước từ.");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Sự cố phong tỏa tài sản: " + ex.Message);
            }
        }

        /// <summary>
        /// Giải tỏa (Hoàn trả) tiền cọc về lại sổ dư khả dụng khi khách báo hủy dịch vụ.
        /// </summary>
        public OperationResult GiaiToaTienCoc(int idKhachHang, decimal soTien, int? idDonHangLienQuan = null)
        {
            if (soTien <= 0) return OperationResult.Failed("Cảnh báo: Số tiền hoàn không hợp lệ.");
            var vi = DAL_ViDienTu.Instance.LayTheoKhachHang(idKhachHang);
            if (vi == null) return OperationResult.Failed("Giao dịch từ chối: Không tìm thấy ví điện tử.");

            if (vi.SoDuDongBang < soTien) return OperationResult.Failed("Giao dịch từ chối: Số dư đóng băng không đủ để thực hiện giải tỏa.");

            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    // Chuyển dòng tiền ngược lại
                    vi.SoDuDongBang -= soTien;
                    vi.SoDuKhaDung += soTien;

                    bool updateOk = DAL_ViDienTu.Instance.Sua(vi);
                    if (!updateOk) return OperationResult.Failed("Lỗi: Quá trình cập nhật hoàn cọc bị gián đoạn.");

                    var giaoDich = new ET_GiaoDichVi
                    {
                        MaCode = "HT-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                        IdVi = vi.Id,
                        LoaiGiaoDich = "HoanTienCoc", 
                        SoTien = soTien,
                        IdDonHangLienQuan = idDonHangLienQuan,
                        ThoiGian = DateTime.Now,
                        CreatedAt = DateTime.Now
                    };
                    _giaoDichGateway.ThemVaLayId(giaoDich);

                    scope.Complete();
                    return OperationResult.Success();
                }
            }
            catch (System.Data.DBConcurrencyException)
            {
                return OperationResult.Failed("Xung đột đồng thời: Thu ngân khác đang thao tác. Vui lòng tải lại.");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Sự cố hoàn tiền cọc: " + ex.Message);
            }
        }

        /// <summary>
        /// Khấu trừ vĩnh viễn (Tiêu hao thực tế) tiền cọc khi dịch vụ được xác nhận đã hoàn tất.
        /// </summary>
        public OperationResult KhauTruTienDongBang(int idKhachHang, decimal soTien, int? idDonHangLienQuan = null)
        {
            if (soTien <= 0) return OperationResult.Success(); 
            var vi = DAL_ViDienTu.Instance.LayTheoKhachHang(idKhachHang);
            if (vi == null) return OperationResult.Failed("Không tìm thấy ví điện tử của khách hợp lệ.");

            if (vi.SoDuDongBang < soTien) return OperationResult.Failed("Lỗi định khoản: Kỳ toán hiển thị số dư đóng băng tiền cọc tài khoản bị lệch.");

            try
            {
                using (var scope = new TransactionScope(TransactionScopeOption.Required, TransactionScopeAsyncFlowOption.Enabled))
                {
                    // Chỉ tiêu hao khoản Đóng băng, KHÔNG cộng hoàn tiền
                    vi.SoDuDongBang -= soTien;

                    bool updateOk = DAL_ViDienTu.Instance.Sua(vi);
                    if (!updateOk) return OperationResult.Failed("Lỗi thanh khoản tài chính tiền cọc.");

                    var giaoDich = new ET_GiaoDichVi
                    {
                        MaCode = "TT-" + DateTime.Now.ToString("yyMMddHHmmss") + "-" + Guid.NewGuid().ToString().Substring(0, 4).ToUpper(),
                        IdVi = vi.Id,
                        LoaiGiaoDich = "ThanhToanCoc", 
                        SoTien = soTien,
                        IdDonHangLienQuan = idDonHangLienQuan,
                        ThoiGian = DateTime.Now,
                        CreatedAt = DateTime.Now
                    };
                    _giaoDichGateway.ThemVaLayId(giaoDich);

                    scope.Complete();
                    return OperationResult.Success();
                }
            }
            catch (System.Data.DBConcurrencyException)
            {
                return OperationResult.Failed("Xung đột đồng thời khi thanh toán trừ cọc.");
            }
            catch (Exception ex)
            {
                return OperationResult.Failed("Lỗi khấu trừ: " + ex.Message);
            }
        }

        #endregion

        #region Các Tiện Ích Hành Chính

        /// <summary>
        /// Trích xuất sao kê dòng chảy thanh toán cho chủ thẻ (20 giao dịch mới nhất).
        /// </summary>
        public List<ET_GiaoDichVi> LayLichSuGiaoDich(int idVi)
        {
            return _giaoDichGateway.LoadDS()
                .Where(x => x.IdVi == idVi)
                .OrderByDescending(x => x.ThoiGian)
                .Take(20)
                .ToList();
        }

        /// <summary>
        /// Lấy tên khách hàng theo IdKhachHang từ ví.
        /// </summary>
        public string LayTenKhachHang(int idKhachHang)
        {
            var kh = _khachHangGateway.LayTheoId(idKhachHang);
            return kh != null ? kh.HoTen : "Khách vãng lai";
        }

        public List<ET_ViDienTu> LoadViDienTu()
        {
            return _viGateway.LoadDS();
        }

        public List<ET_TheRFID> LoadTheRfids()
        {
            return _theRfidGateway.LoadDS();
        }

        /// <summary>
        /// Cấu hình khóa, hủy ngang hoặc cấp phép mở khóa trở lại đối với thẻ vật lý.
        /// </summary>
        public OperationResult KhoaMoThe(string maRfid, string trangThai)
        {
            var the = _theRfidGateway.LayTheoId(maRfid);
            if (the == null) return OperationResult.Failed("Không tìm thấy thẻ vật lý để khóa.");

            the.TrangThai = trangThai;
            if (trangThai == AppConstants.TrangThaiTheRfid.Lost || trangThai == AppConstants.TrangThaiTheRfid.Locked)
                the.NgayHuy = DateTime.Now;
            else
                the.NgayHuy = null; // Khởi động kích hoạt lại sẽ xóa án kỉ luật ngày hủy

            bool ok = _theRfidGateway.Sua(the);
            return ok ? OperationResult.Success() : OperationResult.Failed("Can thiệp thẻ thất bại.");
        }
        #endregion
    }
}
