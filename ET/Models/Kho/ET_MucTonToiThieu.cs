using System;

namespace ET.Models.Kho
{
    public class ET_MucTonToiThieu
    {
        public int Id { get; set; }
        public int IdSanPham { get; set; }
        public int IdKho { get; set; }
        public decimal MucCanhBao { get; set; }
        public decimal? SoLuongDatHang { get; set; }
        public bool TrangThai { get; set; }
    }
}
