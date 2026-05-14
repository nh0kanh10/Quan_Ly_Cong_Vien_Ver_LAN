using System;
using System.Collections.Generic;

namespace ET.DTOs
{
    public class DTO_ChiTietDonHangHoan
    {
        public int IdChiTietDonHang { get; set; }
        public int IdSanPham { get; set; }
        public string TenSanPham { get; set; }
        public string LoaiSanPham { get; set; }
        public decimal SoLuongMua { get; set; }
        public decimal SoLuongDaHoan { get; set; }  // Tính từ Sum(LichSuHoanHang)
        public decimal DonGiaThucTe { get; set; }
        
        // Dùng trên UI
        public decimal SoLuongMuonHoan { get; set; }
        public string LyDoHoan { get; set; }
        
        // Mở rộng thêm để logic dễ xử lý
        public int? IdKho { get; set; }
        public string GhiChu { get; set; }
    }
}
