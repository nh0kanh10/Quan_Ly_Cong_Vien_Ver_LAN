using System;

namespace ET
{
    public partial class ET_ChatLuongNuoc
    {
        public int Id { get; set; }
        public int IdKhuVucBien { get; set; }
        public DateTime Ngay { get; set; }
        public decimal? DoMan { get; set; }
        public decimal? PH { get; set; }
        public decimal? NhietDo { get; set; }
        public int? DoTrong { get; set; }
        public string TrangThaiVeSinh { get; set; }
    }
}
