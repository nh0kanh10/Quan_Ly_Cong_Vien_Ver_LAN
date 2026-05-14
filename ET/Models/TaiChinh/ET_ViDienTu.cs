using System;

namespace ET.Models.TaiChinh
{
    public class ET_ViDienTu
    {
        public int Id { get; set; }
        public int IdKhachHang { get; set; }
        public string MaVi { get; set; }
        public bool ConHoatDong { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
