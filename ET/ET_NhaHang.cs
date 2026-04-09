using System;

namespace ET
{
    public partial class ET_NhaHang
    {
        public int Id { get; set; }
        public string TenNhaHang { get; set; }
        public int? IdKhuVuc { get; set; }
        public int? SucChua { get; set; }
        public string MoTa { get; set; }
    }
}
