using System;

namespace ET.Models.Kho
{
    public class ET_ChungTu
    {
        public int Id { get; set; }
        public string MaChungTu { get; set; }
        public string LoaiChungTu { get; set; }
        public int? IdKhoXuat { get; set; }
        public int? IdKhoNhap { get; set; }
        public int? IdDoiTac { get; set; }
        public int? IdDonHang { get; set; }
        public int? IdBaoTri { get; set; }
        public int? IdChungTuGoc { get; set; }
        public DateTime NgayChungTu { get; set; }
        public string LyDo { get; set; }
        public string TrangThai { get; set; }
        public int IdNguoiTao { get; set; }
        public int? IdNguoiDuyet1 { get; set; }
        public DateTime? NgayDuyet1 { get; set; }
        public int? IdNguoiDuyet2 { get; set; }
        public DateTime? NgayDuyet2 { get; set; }
        public string GhiChu { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
