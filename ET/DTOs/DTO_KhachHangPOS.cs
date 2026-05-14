namespace ET.DTOs
{
    /// <summary>
    /// Kết quả gộp từ KhachHang + ThongTin, dùng cho panel khách trên POS.
    /// Chỉ chứa các trường cần hiển thị, không phải entity DB.
    /// </summary>
    public class DTO_KhachHangPOS
    {
        public int IdDoiTac { get; set; }
        public string MaKhachHang { get; set; }
        public string HoTen { get; set; }
        public string DienThoai { get; set; }
        public string LoaiKhach { get; set; }
        public string HangThanhVien { get; set; }
        public string TenHang { get; set; }
        public int DiemTichLuy { get; set; }  // Tính từ SUM(LichSuDiem)
        public decimal TongChiTieu { get; set; }  // Tính từ SUM(DonHang.TongThanhToan)

        // Thông tin ví RFID (nếu KH có thẻ RFID gắn ví)
        public int? IdViDienTu { get; set; }
        public decimal SoDuVi { get; set; }
    }
}
