using System;

namespace ET.Models.NhanSu
{
    public class DTO_HopDong
    {
        public int Id { get; set; }
        public int IdNhanVien { get; set; }
        public string SoHopDong { get; set; }
        public string LoaiHopDong { get; set; }
        public string ChucDanh { get; set; }
        public decimal LuongCoBan { get; set; }
        public DateTime NgayHieuLuc { get; set; }
        public DateTime? NgayHetHan { get; set; }
        public string LyDoKyMoi { get; set; }
        public string TepHopDong { get; set; }
        public int? IdNguoiKy { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
