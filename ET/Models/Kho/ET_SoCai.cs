using System;

namespace ET.Models.Kho
{
    public class ET_SoCai
    {
        public long Id { get; set; }
        public int IdChungTu { get; set; }
        public int IdSanPham { get; set; }
        public int? IdLoHang { get; set; }
        
        // Bắt buộc phải có Kho Nguồn và Kho Đích
        public int IdKhoXuat { get; set; }
        public int IdKhoNhap { get; set; }
        
        public decimal SoLuong { get; set; }
        public decimal? DonGia { get; set; }
        public DateTime ThoiGian { get; set; }
        public int NguoiTao { get; set; }

        // Trường bổ sung khi JOIN với ChungTu để hiển thị trên UI
        public string GhiChuCT { get; set; }
    }
}
