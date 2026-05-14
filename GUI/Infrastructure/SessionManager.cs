using System.Collections.Generic;

namespace GUI.Infrastructure
{
    // Lưu thông tin phiên đăng nhập hiện tại.
    // Singleton — set 1 lần khi login, đọc ở mọi nơi.
    public static class SessionManager
    {
        // Thông tin user đang đăng nhập
        public static int IdDoiTac { get; private set; }
        public static string MaNhanVien { get; private set; }
        public static string HoTen { get; private set; }
        public static string TenVaiTro { get; private set; }
        public static int IdPhienGiaoDich { get; set; }
        public static string CurrentLanguage { get; set; } = "vi-VN";

        // Danh sách mã quyền (VD: "KH_XEM", "KH_SUA", "DH_XOA")
        // Load 1 lần khi login từ bảng NhanSu.PhanQuyen
        private static HashSet<string> _quyenHan = new HashSet<string>();

        /// <summary>
        /// Gọi sau khi xác thực đăng nhập thành công.
        /// Load quyền vào bộ nhớ để kiểm tra nhanh (không query DB mỗi lần).
        /// </summary>
        /// <param name="idDoiTac">Id trong DoiTac.ThongTin</param>
        /// <param name="maNV">Mã nhân viên (VD: NV001)</param>
        /// <param name="hoTen">Họ tên hiển thị</param>
        /// <param name="tenVaiTro">Tên vai trò (VD: Admin, ThuNgan)</param>
        /// <param name="danhSachQuyen">Danh sách MaQuyen từ NhanSu.PhanQuyen</param>
        public static void DangNhap(int idDoiTac, string maNV, string hoTen,
            string tenVaiTro, IEnumerable<string> danhSachQuyen)
        {
            IdDoiTac = idDoiTac;
            MaNhanVien = maNV;
            HoTen = hoTen;
            TenVaiTro = tenVaiTro;

            _quyenHan.Clear();
            foreach (var q in danhSachQuyen)
                _quyenHan.Add(q);
        }

        /// <summary>
        /// Kiểm tra user hiện tại có quyền cụ thể không.
        /// </summary>
        /// <param name="maQuyen">Mã quyền cần kiểm tra (VD: "KH_SUA")</param>
        /// <returns>true nếu có quyền</returns>
        public static bool CoQuyen(string maQuyen)
        {
            if (TenVaiTro == "Admin") return true;
            return _quyenHan.Contains(maQuyen);
        }

        // Xoá session khi đăng xuất
        public static void DangXuat()
        {
            IdDoiTac = 0;
            MaNhanVien = null;
            HoTen = null;
            TenVaiTro = null;
            _quyenHan.Clear();
        }
    }
}
