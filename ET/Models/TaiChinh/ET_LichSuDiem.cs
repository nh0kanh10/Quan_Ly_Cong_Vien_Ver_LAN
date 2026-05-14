using System;

namespace ET.Models.TaiChinh
{
    public class ET_LichSuDiem
    {
        public int Id { get; set; }
        public int IdKhachHang { get; set; }
        public string LoaiGiaoDich { get; set; }
        public int SoDiem { get; set; }
        public int SoDuSauGD { get; set; }
        public int? IdDonHang { get; set; }
        public string MoTa { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
