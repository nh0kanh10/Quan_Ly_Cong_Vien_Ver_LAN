namespace ET.Models.Kho
{
    /// <summary>
    /// Dữ liệu 1 dòng kiểm kê: đối chiếu tồn hệ thống vs đếm tay thực tế.
    /// Tách riêng khỏi ET_ChiTietChungTu vì kiểm kê không di chuyển hàng.
    /// Sau khi phiếu được duyệt, code C# tự sinh ChiTietChungTu điều chỉnh chênh lệch.
    /// </summary>
    public class ET_ChiTietKiemKe
    {
        public int Id { get; set; }
        public int IdChungTu { get; set; }
        public int IdKho { get; set; }
        public int IdSanPham { get; set; }
        public int? IdLoHang { get; set; }

        // Hiển thị phụ (không lưu DB)
        public string TenSanPham { get; set; }
        public string TenDonViTinh { get; set; }
        public string MaSanPham { get; set; }
        public string TenKho { get; set; }

        // Hệ thống nói còn bao nhiêu (Quản lý thấy, Thủ kho KHÔNG thấy khi nhập)
        public decimal SoLuongHeThong { get; set; }

        // Thủ kho đếm tay nhập vào
        public decimal SoLuongThucTe { get; set; }

        // Tự tính: + thừa / - thiếu
        public decimal ChenhLech => SoLuongThucTe - SoLuongHeThong;

        public string LyDoChenhLech { get; set; }
        public string GhiChu { get; set; }
    }
}
