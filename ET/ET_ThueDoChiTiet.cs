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
        public int? IdGiaoDichCoc { get; set; }
        public int? IdGiaoDichHoanCoc { get; set; }
        public int? IdGiaoDichPhat { get; set; }
        public decimal TienThueDaThu { get; set; }
    }
}
