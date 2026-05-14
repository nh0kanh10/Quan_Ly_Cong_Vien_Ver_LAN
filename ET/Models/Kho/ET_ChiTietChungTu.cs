using System;

namespace ET.Models.Kho
{
    public class ET_ChiTietChungTu
    {
        public int Id { get; set; }
        public int IdChungTu { get; set; }
        public int IdSanPham { get; set; }

        // Dữ liệu hiển thị phụ (không lưu xuống DB, chỉ để Grid hiển thị)
        public string TenSanPham { get; set; }
        public string TenDonViTinh { get; set; }
        public string MaSanPham { get; set; }

        public int? IdLoHang { get; set; }

        // V3: IdKhoXuat + IdKhoNhap chuyển xuống dòng chi tiết (NOT NULL)
        // Mỗi dòng tự mang cặp kho riêng — 1 phiếu có thể phân phối vào nhiều kho đích
        public int IdKhoXuat { get; set; }
        public int IdKhoNhap { get; set; }

        // Tên kho hiển thị phụ (không lưu DB)
        public string TenKhoXuat { get; set; }
        public string TenKhoNhap { get; set; }

        public decimal SoLuong { get; set; }
        public decimal? DonGia { get; set; }
        public decimal ThanhTien => SoLuong * (DonGia ?? 0);

        // Hệ số quy đổi đơn vị (VD: bán 1 thùng = trừ kho 24 lon)
        public decimal HeSoQuyDoi { get; set; } = 1m;

        // Số lượng thực tế đếm tay khi nhập liệu (dùng ở form trước khi tính SoLuong cuối)
        public decimal DemThucTe { get; set; }

        public string GhiChu { get; set; }

        // Dùng cho Phiếu Kiểm Kê: số lượng hệ thống đang ghi nhận vs thực tế đếm tay
        public decimal? SoLuongHeThong { get; set; }
        public decimal? SoLuongThucTe  { get; set; }
        public string   LyDoChenhLech  { get; set; }
    }
}
