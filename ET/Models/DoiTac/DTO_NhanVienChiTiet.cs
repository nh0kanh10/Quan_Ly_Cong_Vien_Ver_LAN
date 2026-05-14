using System;

namespace ET.Models.DoiTac
{
    public class DTO_NhanVienChiTiet
    {
        public int IdDoiTac { get; set; }
        public string HoTen { get; set; }
        public string DienThoai { get; set; }
        public string Email { get; set; }
        public string Cccd { get; set; }
        public string DiaChi { get; set; }
        public string HinhAnh { get; set; }
        public DateTime NgayTao { get; set; }

        public string MaNhanVien { get; set; }
        public int? IdVaiTro { get; set; }
        public int? IdKhuVuc { get; set; }
        public int? IdNguoiQuanLy { get; set; }
        public string ChucVu { get; set; }
        public string GioiTinh { get; set; }
        public DateTime? NgaySinh { get; set; }
        public string LoaiKhoi { get; set; }
        public string LoaiHopDong { get; set; }
        public string NhomCongViec { get; set; }
        public decimal? LuongCoBan { get; set; }
        public decimal? LuongTheoGio { get; set; }
        public string TrangThai { get; set; }

        public string TenVaiTro { get; set; }
        public string TenKhuVuc { get; set; }
        public string TenNguoiQuanLy { get; set; }
        public string PhongBan { get; set; }
        public DateTime? NgayVaoLam { get; set; }
    }
}
