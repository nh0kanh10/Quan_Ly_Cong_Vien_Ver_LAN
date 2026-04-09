using System;

namespace ET
{
    public partial class ET_BaoTriPhuongTienDua
    {
        public int Id { get; set; }
        public int? IdPhuongTienDua { get; set; }
        public int? IdNguaDua { get; set; }
        public DateTime NgayBaoTri { get; set; }
        public string NoiDung { get; set; }
        public decimal ChiPhi { get; set; }
        public int? IdPhieuChi { get; set; }
    }
}
