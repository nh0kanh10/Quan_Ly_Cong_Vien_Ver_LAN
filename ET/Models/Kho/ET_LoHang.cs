using System;

namespace ET.Models.Kho
{
    public class ET_LoHang
    {
        public int Id { get; set; }
        public string MaLoHang { get; set; }
        public int IdSanPham { get; set; }
        public int? IdNhaCungCap { get; set; }
        public DateTime? NgaySanXuat { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public decimal SoLuongNhap { get; set; }
        public int? IdChungTuNhap { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayTao { get; set; }
        public string TenSanPham { get; set; }
        public int SoNgayConLai
        {
            get
            {
                if (!NgayHetHan.HasValue) return int.MaxValue;
                return (int)(NgayHetHan.Value.Date - DateTime.Now.Date).TotalDays;
            }
        }
    }
}
