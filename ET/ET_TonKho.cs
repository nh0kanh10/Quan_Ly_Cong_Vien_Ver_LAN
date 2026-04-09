using System;

namespace ET
{
    public partial class ET_TonKho
    {
        public int Id { get; set; }
        public int IdKho { get; set; }
        public int IdSanPham { get; set; }
        public int SoLuong { get; set; }
        public byte[] RowVer { get; set; }
    }
}
