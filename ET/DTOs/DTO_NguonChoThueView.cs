using System;

namespace ET.DTOs
{
    public class DTO_NguonChoThueView
    {
        public int IdSanPham { get; set; }
        public int? IdTaiSanChoThue { get; set; }
        
        /// Mã hiển thị. Với Phao bơi là MaSanPham, với Xe/Tủ là MaVachThietBi.
        public string MaHienThi { get; set; }       
        public string TenHienThi { get; set; }     
        public decimal TienThue { get; set; }
        public decimal TienCoc { get; set; }
        
        /// True nếu là tài sản định danh (Xe/Tủ), False nếu là tài sản đếm số lượng (Phao/Khăn).
        public bool IsDinhDanh { get; set; }
        
        /// Trạng thái. Thường chỉ load "SanSang".
        public string TrangThai { get; set; }
    }
}
