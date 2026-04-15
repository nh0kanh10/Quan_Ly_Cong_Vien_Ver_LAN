using System;

namespace ET
{
    public partial class ET_ThueDoChiTiet
    {
        public int Id { get; set; }
        public int IdChiTietDonHang { get; set; }  
        public int IdSanPham { get; set; }
        public int SoLuong { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
        public decimal SoTienCoc { get; set; }
        public string TrangThaiCoc { get; set; }
        public int? IdPhieuThuCoc { get; set; }      // PhieuThu (thu cọc — TienMat hoặc ViRFID)
        public int? IdPhieuChiHoanCoc { get; set; }  // PhieuChi (hoàn cọc khi trả đúng hạn)
        public int? IdPhieuThuPhat { get; set; }     // PhieuThu (khách đóng phạt — hỏng/trả trễ)
        public decimal TienThueDaThu { get; set; }
    }
}
