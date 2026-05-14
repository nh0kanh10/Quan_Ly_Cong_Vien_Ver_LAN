using System.Collections.Generic;

namespace ET.DTOs
{
    // Gói dữ liệu gửi từ ucGiaoDo xuống BUS khi nhấn Thanh Toán.
    // Chứa giỏ thuê + phương thức thanh toán + thông tin khách.
    public class DTO_RentalCheckoutRequest
    {
        public List<DTO_RentalCartItem> GioThue { get; set; } = new List<DTO_RentalCartItem>();
        public string PhuongThucTT { get; set; }
        public int? IdKhachHang { get; set; }
        public int? IdViDienTu { get; set; }
        public int IdNhanVien { get; set; }
        public int? IdKhuVuc { get; set; }
        public int IdPhienThuNgan { get; set; }  // Bắt buộc: gắn phiên thu ngân vào đơn giao đồ
    }
}
