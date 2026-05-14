using System;

namespace ET.Models.NhanSu
{
    public class DTO_LichLamViecChiTiet
    {
        public int Id { get; set; }
        public int IdNhanVien { get; set; }
        public int? IdKhuVuc { get; set; }
        public string TenKhuVuc { get; set; }
        public DateTime NgayLam { get; set; }
        public int IdCaLamMau { get; set; }
        public string TenCa { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }
    }
}
