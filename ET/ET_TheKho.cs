using System;

namespace ET
{
    public class ET_TheKho
    {
        public int Id { get; set; }
        public int IdKho { get; set; }
        public int IdSanPham { get; set; }
        public string LoaiGiaoDich { get; set; }
        public int SoLuongThayDoi { get; set; }
        public int? TonCuoi { get; set; }
        public decimal? DonGiaVatTu { get; set; }
        public int? IdThamChieu { get; set; }
        public DateTime ThoiGianGiaoDich { get; set; }
        public int CreatedBy { get; set; }
        public string GhiChu { get; set; }
    }
}
