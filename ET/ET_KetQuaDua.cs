using System;

namespace ET
{
    public partial class ET_KetQuaDua
    {
        public int Id { get; set; }
        public int IdLichThiDau { get; set; }
        public int IdVdv { get; set; }
        public int? IdPhuongTienDua { get; set; }
        public int? IdNguaDua { get; set; }
        public int ThuTuVeDich { get; set; }
        public TimeSpan? ThanhTichThoiGian { get; set; }
    }
}
