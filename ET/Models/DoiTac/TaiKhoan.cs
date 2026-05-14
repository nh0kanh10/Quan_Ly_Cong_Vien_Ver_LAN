using System;
using ET.Interfaces;

namespace ET.Models.DoiTac
{
    // Bảng DoiTac.TaiKhoan — Tài khoản đăng nhập phần mềm.
    // 1 DoiTac có thể có nhiều TaiKhoan (VD: đăng nhập bằng mật khẩu + vân tay).
    public class TaiKhoan : IEntity
    {
        public int Id { get; set; }
        public int IdDoiTac { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhauHash { get; set; }
        public string LoaiTaiKhoan { get; set; }
        public string LoaiDangNhap { get; set; }
        public bool ConHoatDong { get; set; }
        public DateTime? LanDangNhapCuoi { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
