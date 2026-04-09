using System;

namespace ET
{
    public partial class ET_ViTriNgoi
    {
        public int Id { get; set; }
        public string MaCode { get; set; }
        public string Hang { get; set; }
        public int SoGhe { get; set; }
        public string LoaiGhe { get; set; }
        public int? IdSanPham { get; set; }
        public int? IdKhanDai { get; set; }
        public byte[] RowVer { get; set; }
    }
}
