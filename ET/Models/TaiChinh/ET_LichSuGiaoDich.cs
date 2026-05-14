using System;

namespace ET.Models.TaiChinh
{
    // DTO gộp: dùng để hiển thị lưới lịch sử giao dịch trên frmKhachHang.
    // Gộp từ 2 nguồn: BanHang.DonHang (giao dịch thường) và TaiChinh.SoCaiVi (giao dịch ví).
    public class ET_LichSuGiaoDich
    {
        public string MaGiaoDich { get; set; }
        public string NhomGiaoDich { get; set; }
        public decimal SoTien { get; set; }
        public string LoaiGiaoDich { get; set; }
        public DateTime ThoiGian { get; set; }
    }
}
