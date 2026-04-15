using System;
using System.Collections.Generic;

namespace ET
{
    public partial class ET_NhanVien
    {
        public int Id { get; set; }
        public string MaCode { get; set; }
        public int IdVaiTro { get; set; }
        public int? IdNguoiQuanLy { get; set; }
        public string HoTen { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string ChucVu { get; set; }
        public string BoPhan { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string Cccd { get; set; }
        public string DiaChi { get; set; }
        public string HinhAnh { get; set; }
        public DateTime? NgayVaoLam { get; set; }
        public string TrangThai { get; set; }
        public string TenDangNhap { get; set; }
        public string MatKhau { get; set; }
        public string GhiChu { get; set; }

        // ── HR Module ──────────────────────────────────────────────────────
        /// <summary>Phân biệt Khối Hành Chính và Khối Vận Hành: VanHanh | HanhChinh</summary>
        public string LoaiKhoi { get; set; } = AppConstants.LoaiKhoi.VanHanh;

        /// <summary>FullTime | PartTime | TheoMua | Intern</summary>
        public string LoaiHopDong { get; set; } = AppConstants.LoaiHopDong.FullTime;

        /// <summary>ThuongThuong | NangNhocNguyHiem | DacBietNguyHiem</summary>
        public string NhomCongViec { get; set; } = AppConstants.NhomCongViec.ThuongThuong;

        /// <summary>Lương cơ bản/tháng — dùng cho FullTime</summary>
        public decimal? LuongCoBan { get; set; }

        /// <summary>Lương theo giờ — dùng cho PartTime/Intern</summary>
        public decimal? LuongTheoGio { get; set; }

        // ── Computed helpers (không lưu DB, tính ở BUS layer) ─────────────
        /// <summary>Tên người quản lý — JOIN từ chính bảng NhanVien, chỉ dùng cho display</summary>
        public string TenNguoiQuanLy { get; set; }

        /// <summary>Tên vai trò — JOIN từ VaiTro, chỉ dùng cho display</summary>
        public string TenVaiTro { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }

}
