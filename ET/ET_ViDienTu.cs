using System;

namespace ET
{
    public partial class ET_ViDienTu
    {
        public int Id { get; set; }
        public int IdKhachHang { get; set; }
        public decimal SoDuKhaDung { get; set; }
        public decimal SoDuDongBang { get; set; }
        public byte[] RowVer { get; set; }
    }
}
