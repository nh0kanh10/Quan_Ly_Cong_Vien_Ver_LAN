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
        // [DROPPED] LaDonViCoBan — Dư thừa, SanPham.IdDonViCoBan đã làm nhiệm vụ này
        public DateTime CreatedAt { get; set; }
    }
}
