using System.Collections.Generic;

namespace ET.DTOs
{
    public class DTO_CheckoutResult
    {
        public bool ThanhCong { get; set; }
        public string Message { get; set; }

        public int? IdDonHang { get; set; }
        public string MaDonHang { get; set; }
        public decimal TienThua { get; set; }
        
        public decimal TongTienHang { get; set; }
        public decimal SoTienGiam { get; set; }
        public decimal TienThanhToanBangDiem { get; set; }
        public decimal TongThanhToan { get; set; }

        public List<DTO_MaVachVe> DanhSachMaVachVe { get; set; } = new List<DTO_MaVachVe>();
        public List<DTO_CheckoutItem> DanhSachMon { get; set; } = new List<DTO_CheckoutItem>();
    }

    public class DTO_CheckoutItem
    {
        public string TenMon { get; set; }
        public decimal SoLuong { get; set; }
        public decimal DonGiaGoc { get; set; }
        public decimal TienGiamGiaDong { get; set; }
        public decimal DonGiaThucTe { get; set; }
        public decimal ThanhTien { get; set; }
    }

    public class DTO_MaVachVe
    {
        public string TenVe { get; set; }
        public string MaVach { get; set; }
    }
}
