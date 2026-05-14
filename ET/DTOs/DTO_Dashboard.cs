namespace ET.DTOs
{
    /// <summary>
    /// KPI bán hàng: doanh thu, số đơn — phạm vi từ đầu tháng.
    /// </summary>
    public class DTO_DashboardBanHang
    {
        public int SoDonHangHomNay { get; set; }
        public decimal DoanhThuHomNay { get; set; }
        public decimal DoanhThuThang { get; set; }
        public int SoDonChoThanhToan { get; set; }
    }

    /// <summary>
    /// KPI lưu trú: trạng thái phòng hiện tại.
    /// </summary>
    public class DTO_DashboardLuuTru
    {
        public int TongPhong { get; set; }
        public int PhongTrong { get; set; }
        public int PhongDangO { get; set; }
        public int PhongBaoTri { get; set; }
    }

    /// <summary>
    /// KPI kho: tồn kho, cảnh báo hết hàng, phiếu chờ duyệt.
    /// </summary>
    public class DTO_DashboardKho
    {
        public int TongSanPham { get; set; }
        public int SapHetHang { get; set; }
        public int HetHangHoanToan { get; set; }
        public int PhieuChoDuyet { get; set; }
    }

    /// <summary>
    /// KPI khách hàng: tổng + khách mới trong tháng.
    /// </summary>
    public class DTO_DashboardKhachHang
    {
        public int TongKhachHang { get; set; }
        public int KhachMoiThang { get; set; }
    }
}
