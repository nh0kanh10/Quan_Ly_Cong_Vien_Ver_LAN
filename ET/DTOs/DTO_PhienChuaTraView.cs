using System;

namespace ET.DTOs
{
    // Dữ liệu hiển thị trên grid "Phiên chưa trả" (giám sát).
    // Gom từ ThueDoChiTiet + DonHang + SanPham + KhachHang.
    public class DTO_PhienChuaTraView
    {
        public int IdThueDoChiTiet { get; set; }
        public int IdSanPham { get; set; }
        public string TenSanPham { get; set; }
        public int SoLuong { get; set; }
        public DateTime ThoiGianThue { get; set; }

        // Hiển thị trên grid grouped
        public string MaDonHang { get; set; }
        public string TenKhachHang { get; set; }

        // Dùng khi double-click để chuyển sang nhận trả
        public string HeaderNhom => $"{MaDonHang} | {TenKhachHang ?? "Khách vãng lai"} | {ThoiGianThue:dd/MM HH:mm}";
    }
}
