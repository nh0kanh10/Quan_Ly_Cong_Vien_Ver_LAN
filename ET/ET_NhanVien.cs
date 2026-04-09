using System;

namespace ET
{
    public partial class ET_NhanVien
    {
        public int Id { get; set; }
        public string MaCode { get; set; }
        public int IdVaiTro { get; set; }
        public int? IdKhuVuc { get; set; }
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
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
    }
}
