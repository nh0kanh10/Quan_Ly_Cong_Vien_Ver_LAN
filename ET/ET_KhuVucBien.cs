using System;

namespace ET
{
    public partial class ET_KhuVucBien
    {
        public int Id { get; set; }           
        public decimal? DoSauToiDa { get; set; }
        public bool YeuCauPhao { get; set; }


        public string TenKhuVuc { get; set; }
        public string MoTa { get; set; }
        public string MaCode { get; set; }
    }
}
