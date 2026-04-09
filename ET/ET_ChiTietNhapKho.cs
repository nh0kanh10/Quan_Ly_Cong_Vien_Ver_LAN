using System;

namespace ET
{
    public partial class ET_ChiTietNhapKho
    {
        public int Id { get; set; }
        public int IdPhieuNhap { get; set; }
        public int IdSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal DonGiaNhap { get; set; }
        public int? IdDonViNhap { get; set; }
        public decimal TyLeQuyDoi { get; set; }
    }
}
