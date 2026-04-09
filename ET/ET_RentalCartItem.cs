using System;

namespace ET
{
    public class RentalCartItem
    {
        public int IdSanPham { get; set; }
        public string TenSanPham { get; set; }
        public decimal TienThue { get; set; }
        public decimal TienCoc { get; set; }
        public int SoLuong { get; set; }
        public int IdChiTietDonHang { get; set; } 
        public decimal TongThue => TienThue * SoLuong;
        public decimal TongCoc => TienCoc * SoLuong;
    }
}
