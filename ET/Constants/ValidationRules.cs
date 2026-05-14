namespace ET.Constants
{
    // Quy tắc validate input — dùng ở BUS (validate) và GUI (mask input).
    // Giá trị lấy từ DB schema (VARCHAR(20) = MaxLength 20).
    public static class ValidationRules
    {
        #region Độ dài tối đa các trường (khớp với DB schema)

        public const int MAX_HO_TEN = 150;
        public const int MAX_DIEN_THOAI = 20;
        public const int MAX_EMAIL = 100;
        public const int MAX_CCCD = 20;
        public const int MAX_DIA_CHI = 300;
        public const int MAX_MA_CODE = 20;          // MaNhanVien, MaKhachHang, MaSanPham...
        public const int MAX_MA_CHUNG_TU = 30;      // MaChungTu, MaDonHang...
        public const int MAX_TEN_SAN_PHAM = 150;
        public const int MAX_GHI_CHU = 500;
        public const int MAX_MO_TA = 200;
        public const int MAX_MA_VACH = 100;          // Mã vạch vé, barcode

        #endregion

        #region Regex kiểm tra định dạng

        // SDT Việt Nam: 0xxx hoặc +84xxx, 9-11 số
        public const string REGEX_DIEN_THOAI = @"^(\+84|0)\d{8,10}$";

        // Email cơ bản
        public const string REGEX_EMAIL = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";

        // CCCD: 12 số
        public const string REGEX_CCCD = @"^\d{12}$";

        // Mã số thuế: 10 hoặc 13 số (có thể có dấu -)
        public const string REGEX_MA_SO_THUE = @"^\d{10}(-\d{3})?$";

        #endregion

        #region Giới hạn số lượng / giá trị

        public const decimal MIN_DON_GIA = 0;
        public const decimal MAX_DON_GIA = 999_999_999;
        public const decimal MIN_SO_LUONG = 0m;
        public const decimal MAX_SO_LUONG = 999_999m;
        public const int MAX_DIEM_DANH_GIA = 5;
        public const int MIN_DIEM_DANH_GIA = 1;

        #endregion
    }
}
