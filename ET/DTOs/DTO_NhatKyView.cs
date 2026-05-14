using System;

namespace ET.DTOs
{
    public class DTO_NhatKyView
    {
        public long Id { get; set; }
        public DateTime ThoiGian { get; set; }
        public string TenNguoiThucHien { get; set; }
        public string ThucThe { get; set; }
        public string HanhDong { get; set; }
        public int IdThucThe { get; set; }
        public string GiaTriCu { get; set; }
        public string GiaTriMoi { get; set; }
        public string GhiChu { get; set; }
    }
}
