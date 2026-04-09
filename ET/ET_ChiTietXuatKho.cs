using System;

namespace ET
{
    public partial class ET_ChiTietXuatKho
    {
        public int Id { get; set; }
        public int IdPhieuXuat { get; set; }
        public int IdSanPham { get; set; }
        public int SoLuong { get; set; }
        public decimal? DonGiaXuat { get; set; }
        public int? IdDonViXuat { get; set; }
        public decimal TyLeQuyDoi { get; set; }
    }
}
