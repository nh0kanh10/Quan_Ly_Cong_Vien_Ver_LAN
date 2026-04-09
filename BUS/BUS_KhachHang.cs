using System;
using DAL;
using ET;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    public class BUS_KhachHang : IBaseBUS<ET_KhachHang>
    {
        private static BUS_KhachHang instance;
        public static BUS_KhachHang Instance
        {
            get
            {
                if (instance == null) instance = new BUS_KhachHang();
                return instance;
            }
        }

        #region Thao tác Dữ liệu Cơ bản (CRUD & Tìm kiếm)
        
        public List<ET_KhachHang> LoadDS()
        {
            return DAL_KhachHang.Instance.LoadDS().Where(x => !x.IsDeleted).ToList();
        }

        public ET_KhachHang GetById(int id)
        {
            return DAL_KhachHang.Instance.LayTheoId(id);
        }

        public ET_KhachHang GetByMaCodeOrSdt(string keyword)
        {
            return DAL_KhachHang.Instance.LayTheoMaCodeHoacSdt(keyword);
        }

        public ResponseResult Them(ET_KhachHang et)
        {
            if (string.IsNullOrEmpty(et.MaCode)) et.MaCode = LayMaCodeTiepTheo();
            et.NgayDangKy = DateTime.Now;
            et.CreatedAt = DateTime.Now;

            bool success = DAL_KhachHang.Instance.Them(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm khách hàng vào CSDL.");
        }

        public ResponseResult Sua(ET_KhachHang et)
        {
            et.UpdatedAt = DateTime.Now;
            bool success = DAL_KhachHang.Instance.Sua(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể cập nhật thông tin khách hàng.");
        }

        public ResponseResult Xoa(int id)
        {
            bool success = DAL_KhachHang.Instance.Xoa(id);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa khách hàng.");
        }

        public List<ET_KhachHang> TimKiem(string tuKhoa, string gioiTinh = "Tất cả")
        {
            var ds = LoadDS();
            if (!string.IsNullOrEmpty(tuKhoa))
            {
                tuKhoa = tuKhoa.ToLower();
                ds = ds.Where(x => (x.HoTen != null && x.HoTen.ToLower().Contains(tuKhoa)) || 
                                  (x.MaCode != null && x.MaCode.ToLower().Contains(tuKhoa)) ||
                                  (x.DienThoai != null && x.DienThoai.Contains(tuKhoa))).ToList();
            }
            if (gioiTinh != "Tất cả")
            {
                ds = ds.Where(x => x.GioiTinh == gioiTinh).ToList();
            }
            return ds;
        }

        public List<ET_KhachHang> TimKiemNangCao(string tuKhoa, string gioiTinh) => TimKiem(tuKhoa, gioiTinh);

        public string LayMaCodeTiepTheo()
        {
            var ds = DAL_KhachHang.Instance.LoadDS();
            int max = 0;
            foreach (var item in ds)
            {
                if (!string.IsNullOrEmpty(item.MaCode) && item.MaCode.StartsWith("KH"))
                {
                    int num;
                    if (int.TryParse(item.MaCode.Substring(2), out num) && num > max) max = num;
                }
            }
            return "KH" + (max + 1).ToString("D5");
        }
        #endregion

        #region Kiểm tra Dữ liệu (Validation)

        public string ValidateKhachHang(ET_KhachHang et, bool isAdd)
        {
            if (string.IsNullOrWhiteSpace(et.HoTen)) return "Họ tên không được để trống.";
            if (et.HoTen.Trim().Length < 2) return "Họ tên phải có ít nhất 2 ký tự.";
            
            if (et.NgaySinh.HasValue)
            {
                if (et.NgaySinh.Value.Date > DateTime.Today)
                    return "Ngày sinh không thể là ngày tương lai.";
                int tuoi = DateTime.Today.Year - et.NgaySinh.Value.Year;
                if (et.NgaySinh.Value.Date > DateTime.Today.AddYears(-tuoi)) tuoi--;
                if (tuoi > 120) return "Tuổi không hợp lệ (> 120).";
            }

            if (string.IsNullOrWhiteSpace(et.DienThoai)) return "Số điện thoại không được để trống.";
            if (!AppConstants.Validation.PhonePattern.IsMatch(et.DienThoai))
                return "Số điện thoại không hợp lệ (phải gồm 10 chữ số).";
            if (!string.IsNullOrWhiteSpace(et.Email) && !AppConstants.Validation.EmailPattern.IsMatch(et.Email))
                return "Định dạng Email không hợp lệ.";
            
            var ds = DAL_KhachHang.Instance.GetAll();
            if (isAdd)
            {
                if (ds.Any(x => x.DienThoai == et.DienThoai)) return "Số điện thoại này đã tồn tại trong hệ thống.";
                if (!string.IsNullOrEmpty(et.MaCode) && ds.Any(x => x.MaCode == et.MaCode)) return "Mã khách hàng này đã tồn tại.";
            }
            else
            {
                if (ds.Any(x => x.DienThoai == et.DienThoai && x.Id != et.Id)) return "Số điện thoại bị trùng với khách hàng khác.";
            }
            return string.Empty;
        }
        #endregion

        #region Giao diện Tổng hợp (Customer 360) - Tích hợp Ví, Thẻ, Lịch sử giao dịch

        public ET_ViDienTu LayViTheoKhachHang(int idKhachHang)
        {
            return DAL_ViDienTu.Instance.LayTheoKhachHang(idKhachHang);
        }

        public ET_TheRFID LayTheRfidTheoVi(int idVi)
        {
            return DAL_TheRFID.Instance.LoadDS().FirstOrDefault(x => x.IdVi == idVi);
        }

        public List<ET_GiaoDichVi> LayLichSuGiaoDich(int idVi)
        {
            return BUS_GiaoDichVi.Instance.LayLichSuGiaoDich(idVi);
        }

        public List<ET_LichSuDiem> LayLichSuDiem(int idKhachHang)
        {
            return DAL_LichSuDiem.Instance.LayTheoKhachHang(idKhachHang);
        }

        public List<ET_SuCo> LaySuCoTheoKhachHang(int idKhachHang)
        {
            return DAL_SuCo.Instance.LayTheoKhachHang(idKhachHang);
        }
        #endregion

        #region Xóa Dữ liệu Thông minh & Cấp phát Thẻ/Ví

        /// <summary>
        /// Quy trình Kiểm tra an toàn trước khi Xóa khách hàng.
        /// Đảm bảo cảnh báo rõ ràng nếu khách hàng vẫn còn số dư tài khoản, tiền cọc, hoặc thẻ đang hoạt động.
        /// </summary>
        public OperationResult<string> KiemTraTruocKhiXoa(int idKhachHang)
        {
            var kh = DAL_KhachHang.Instance.LayTheoId(idKhachHang);
            if (kh == null) return OperationResult<string>.Failed("Không tìm thấy khách hàng.");

            var vi = DAL_ViDienTu.Instance.LayTheoKhachHang(idKhachHang);
            string warning = "";

            if (vi != null)
            {
                // Bước 1: Kiểm tra số dư khả dụng trong ví điện tử
                if (vi.SoDuKhaDung > 0)
                {
                    warning += string.Format("Ví còn số dư: {0:N0} VNĐ\n", vi.SoDuKhaDung);
                    warning += "-> Bắt buộc hoàn tiền cho khách trước khi tiếp tục thao tác xóa.\n\n";
                }

                // Bước 2: Kiểm tra trạng thái hoạt động của TẤT CẢ thẻ RFID liên kết
                var danhSachThe = DAL_TheRFID.Instance.LoadDS().Where(x => x.IdVi == vi.Id && x.TrangThai == AppConstants.TrangThaiTheRfid.Active).ToList();
                if (danhSachThe.Count > 0)
                {
                    warning += string.Format("{0} Thẻ RFID đang HOẠT ĐỘNG\n", danhSachThe.Count);
                    warning += "-> Hệ thống sẽ tự động vô hiệu hóa toàn bộ thẻ khi xóa hồ sơ.\n\n";
                }

                // Bước 3: Kiểm tra các khoản tiền đặt cọc (đóng băng)
                if (vi.SoDuDongBang > 0)
                {
                    warning += string.Format("Tiền đóng băng (cọc): {0:N0} VNĐ\n", vi.SoDuDongBang);
                    warning += "-> Lưu ý: Khách hàng đang có đặt phòng chờ xử lý.\n\n";
                }
            }

            // Bước 4: Kiểm tra lịch sử giao dịch để quyết định hình thức xóa (Vĩnh viễn hay Xóa mềm)
            bool hasTransactions = DAL_LichSuDiem.Instance.LayTheoKhachHang(idKhachHang).Count > 0;
            if (vi != null)
            {
                var giaoDich = BUS_GiaoDichVi.Instance.LayLichSuGiaoDich(vi.Id);
                if (giaoDich.Count > 0) hasTransactions = true;
            }

            if (hasTransactions)
            {
                warning += "Khách hàng có lịch sử giao dịch -> Hệ thống sẽ áp dụng XÓA MỀM (Chỉ ẩn khỏi danh sách để bảo toàn dữ liệu).\n";
            }
            else
            {
                warning += "Khách hàng chưa phát sinh giao dịch -> Hệ thống sẽ xóa vĩnh viễn khỏi cơ sở dữ liệu.\n";
            }

            string msg = string.Format("Bạn có chắc chắn muốn xóa hồ sơ khách hàng này không:\n「{0}」({1})?\n\n{2}",
                kh.HoTen, kh.MaCode, warning);

            return OperationResult<string>.Success(msg);
        }

        /// <summary>
        /// Xóa khách hàng ở mức độ logic (Soft Delete): Tự động khóa các thẻ liên kết và ẩn hồ sơ thay vì xóa vĩnh viễn.
        /// </summary>
        public ResponseResult XoaThongMinh(int idKhachHang)
        {
            var kh = DAL_KhachHang.Instance.LayTheoId(idKhachHang);
            if (kh == null) return ResponseResult.Error("Không tìm thấy khách hàng.");

            var vi = DAL_ViDienTu.Instance.LayTheoKhachHang(idKhachHang);

            // Bước 1: Ngăn chặn thao tác xóa nếu ví điện tử vẫn còn tiền (Khả dụng hoặc Đóng băng)
            if (vi != null)
            {
                if (vi.SoDuKhaDung > 0)
                {
                    return ResponseResult.Error(
                        string.Format("Không thể xóa! Ví khách hàng vẫn còn số dư {0:N0} VNĐ.\nVui lòng làm thủ tục hoàn tiền trước.", vi.SoDuKhaDung));
                }

                if (vi.SoDuDongBang > 0)
                {
                    return ResponseResult.Error(
                        string.Format("Không thể xóa! Khách hàng đang có {0:N0} VNĐ tiền cọc đóng băng.\nVui lòng trả hoặc huỷ các Đơn Đặt Phòng đang chờ xử lý.", vi.SoDuDongBang));
                }
            }

            // Bước 2: Tự động vô hiệu hóa toàn bộ thẻ RFID liên kết đang hoạt động (Vá lỗi lọt thẻ phụ)
            if (vi != null)
            {
                var danhSachThe = DAL_TheRFID.Instance.LoadDS().Where(x => x.IdVi == vi.Id && x.TrangThai == AppConstants.TrangThaiTheRfid.Active).ToList();
                foreach (var the in danhSachThe)
                {
                    BUS_GiaoDichVi.Instance.KhoaMoThe(the.MaRfid, AppConstants.TrangThaiTheRfid.Revoked);
                }
            }

            // Bước 3: Đánh dấu cờ IsDeleted để ẩn hồ sơ khách hàng (Bảo toàn tính toàn vẹn dữ liệu lịch sử)
            kh.IsDeleted = true;
            kh.UpdatedAt = DateTime.Now;
            bool ok = DAL_KhachHang.Instance.Sua(kh);
            return ok ? ResponseResult.Success() : ResponseResult.Error("Lỗi hệ thống: Không thể cập nhật trạng thái xóa khách hàng.");
        }

        /// <summary>
        /// Khởi tạo Ví điện tử mới với số dư mặc định là 0 cho hệ thống Khách hàng.
        /// </summary>
        public OperationResult CapViMoi(int idKhachHang)
        {
            var existing = DAL_ViDienTu.Instance.LayTheoKhachHang(idKhachHang);
            if (existing != null) return OperationResult.Failed("Khách hàng này đã được đăng ký ví điện tử từ trước.");

            var vi = new ET_ViDienTu
            {
                IdKhachHang = idKhachHang,
                SoDuKhaDung = 0,
                SoDuDongBang = 0
            };
            bool ok = DAL_ViDienTu.Instance.Them(vi);
            return ok ? OperationResult.Success() : OperationResult.Failed("Lỗi hệ thống: Không thể khởi tạo ví điện tử mới.");
        }
    }
}

