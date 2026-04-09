using System;

namespace ET
{
    public partial class ET_QuyDoiDonVi
    {
        public int Id { get; set; }
        public int IdSanPham { get; set; }
        public int IdDonViNho { get; set; }
        public int IdDonViLon { get; set; }
        public decimal TyLeQuyDoi { get; set; }
        public decimal? GiaBanRieng { get; set; }
        public bool LaDonViCoBan { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
