using System;

namespace ET.Models.BanHang
{
    // Mỗi dòng = 1 phiên thuê của 1 sản phẩm (hoặc 1 tài sản vật lý).
    // Gắn với ChiTietDonHang để biết thuộc bill nào.
    // TrangThaiCoc theo dõi tiền cọc: DaCoc tới DaHoan / KhongHoan.
    // TrangThai theo dõi đồ: DangThue tới DaTra / MatDo.
    public class ET_ThueDoChiTiet
    {
        public int Id { get; set; }
        public int IdChiTietDonHang { get; set; }
        public int? IdTaiSanChoThue { get; set; }
        public int SoLuong { get; set; }
        public DateTime ThoiGianThue { get; set; }
        public DateTime? ThoiGianTra { get; set; }
        public string TrangThaiCoc { get; set; }
        public decimal TienCoc { get; set; }
        public decimal PhiPhatSinh { get; set; }
        public int? IdChungTuCoc { get; set; }
        public int? IdChungTuHoan { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public bool DaXoa { get; set; }
      // Phiên thu ngân lúc TRẢ ĐỒ (hoàn cọc).
        public int? IdPhienTra { get; set; }
        public string TenSanPham { get; set; }
        public int IdSanPham { get; set; }
        public string MaDonHang { get; set; }
        public string TenKhachHang { get; set; }
    }
}
