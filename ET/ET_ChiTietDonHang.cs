using System;

namespace ET
{
    public partial class ET_ChiTietDonHang
    {
        public int Id { get; set; }
        public int IdDonHang { get; set; }
        public int? IdSanPham { get; set; }
        public int? IdCombo { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGiaGoc { get; set; }
        public decimal TienGiamGiaDong { get; set; }
        public decimal DonGiaThucTe { get; set; }
    }
}
