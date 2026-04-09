using System;

namespace ET
{
    public partial class ET_GiaiDua
    {
        public int Id { get; set; }
        public string TenGiai { get; set; }
        public DateTime? ThoiGianBatDau { get; set; }
        public DateTime? ThoiGianKetThuc { get; set; }
    }
}
