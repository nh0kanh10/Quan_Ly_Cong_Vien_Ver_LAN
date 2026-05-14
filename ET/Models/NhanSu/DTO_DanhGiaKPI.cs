using System;

namespace ET.Models.NhanSu
{
    public class DTO_DanhGiaKPI
    {
        public int Id { get; set; }
        public int IdNhanVien { get; set; }
        public string KyDanhGia { get; set; }
        public string LoaiDanhGia { get; set; }
        public decimal? DiemChuyen_Mon { get; set; }
        public decimal? DiemThai_Do { get; set; }
        public decimal? DiemKet_Qua { get; set; }
        public decimal? DiemTong { get; set; }
        public string XepLoai { get; set; }
        public string NhanXetNguoiDG { get; set; }
        public string NhanXetNhanVien { get; set; }
        public int IdNguoiDanhGia { get; set; }
        public string TenNguoiDanhGia { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayDanhGia { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
