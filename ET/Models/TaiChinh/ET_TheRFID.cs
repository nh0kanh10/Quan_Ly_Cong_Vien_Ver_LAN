using System;

namespace ET.Models.TaiChinh
{
    public class ET_TheRFID
    {
        public int Id { get; set; }
        public string MaThe { get; set; }
        public int? IdKhachHang { get; set; }
        public int? IdViDienTu { get; set; }
        public string TrangThai { get; set; }
        public DateTime? NgayKichHoat { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public decimal TienCocThe { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
