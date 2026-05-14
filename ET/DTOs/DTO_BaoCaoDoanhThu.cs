using System;

namespace ET.DTOs
{
    public class DTO_BaoCaoDoanhThu
    {
        public DateTime? NgayGiaoDich { get; set; }
        public string MaGiaoDich { get; set; }
        public string TenKhachHang { get; set; }
        public string LoaiGiaoDich { get; set; }
        public string NhomSanPham { get; set; }
        public string TenSanPham { get; set; }
        public decimal? DoanhThu { get; set; }
        public string GhiChuChiTiet { get; set; }
        public string NhanVienThuNgan { get; set; }
        public string PhuongThucThanhToan { get; set; }
    }
}
