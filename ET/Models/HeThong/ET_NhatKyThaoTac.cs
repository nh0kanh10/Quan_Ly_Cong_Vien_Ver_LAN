using System;

namespace ET.Models.HeThong
{
    public class ET_NhatKyThaoTac
    {
        public long Id { get; set; }
        public string ThucThe { get; set; }
        public int IdThucThe { get; set; }
        public string HanhDong { get; set; }
        public string GiaTriCu { get; set; }
        public string GiaTriMoi { get; set; }
        public int IdNguoiThucHien { get; set; }
        public DateTime ThoiGian { get; set; }
        public string GhiChu { get; set; }
    }
}
