using System;

namespace ET.Models.BanHang
{
    public class ET_PhienThuNgan
    {
        public int Id { get; set; }
        public int IdNhanVien { get; set; }
        public string IdMayBan { get; set; }
        public int? IdKhoBan { get; set; }

        public DateTime ThoiGianMo { get; set; }
        public DateTime? ThoiGianDong { get; set; }

        public decimal TienDauCa { get; set; }
        public decimal? TienCuoiCa { get; set; }
        public decimal? TongThuTrongCa { get; set; }
        public decimal? TongChiTrongCa { get; set; }
        public decimal? ChenhLech { get; set; }  

        public string GhiChu { get; set; }
        public string TrangThai { get; set; }  // DangMo, DaDong
    }
}
