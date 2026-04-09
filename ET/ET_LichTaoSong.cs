using System;

namespace ET
{
    public partial class ET_LichTaoSong
    {
        public int Id { get; set; }
        public int IdThietBi { get; set; }
        public DateTime ThoiGianBatDau { get; set; }
        public DateTime ThoiGianKetThuc { get; set; }
        public string KieuSong { get; set; }
    }
}
