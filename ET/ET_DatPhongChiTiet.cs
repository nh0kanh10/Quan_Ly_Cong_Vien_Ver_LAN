using System;

namespace ET
{
    public partial class ET_DatPhongChiTiet
    {
        public int Id { get; set; }
        public int? IdChiTietDonHang { get; set; }  
        public DateTime NgayNhan { get; set; }
        public DateTime NgayTra { get; set; }
        public string TrangThai { get; set; }
    }
}
