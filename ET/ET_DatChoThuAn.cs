using System;

namespace ET
{
    public partial class ET_DatChoThuAn
    {
        public int Id { get; set; }
        public int IdChiTietDonHang { get; set; }  
        public int IdDongVat { get; set; }
        public Guid? IdVeDienTu { get; set; }
        public DateTime ThoiGianDuKien { get; set; }
        public string TrangThai { get; set; }
    }
}
