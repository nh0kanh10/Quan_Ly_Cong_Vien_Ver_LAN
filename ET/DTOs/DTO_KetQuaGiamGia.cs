namespace ET.DTOs
{
    /// Kết quả tính giảm giá — gộp cả 3 nguồn (chiết khấu hạng, KM, điểm).
    /// Được tính bằng BUS_POS.TinhTongGiamGia().
    public class DTO_KetQuaGiamGia
    {
        // Chiết khấu hạng thành viên (từ CauHinh)
        public decimal ChietKhauHang { get; set; }
        public decimal PhanTramCKHang { get; set; }

        // Khuyến mãi (manual quét mã)
        public decimal GiamKhuyenMai { get; set; }
        public System.Collections.Generic.List<DTO_KhuyenMaiPOS> DanhSachKMApDung { get; set; } = new System.Collections.Generic.List<DTO_KhuyenMaiPOS>();

        // Tổng giảm thực tế (MAX hoặc cộng dồn tùy CoChongCheo)
        public decimal TongGiamGia { get; set; }

        // Mô tả hiển thị trên POS
        public string MoTa { get; set; }
    }
}
