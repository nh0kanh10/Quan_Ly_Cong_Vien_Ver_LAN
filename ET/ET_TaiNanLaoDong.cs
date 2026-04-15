using System;

namespace ET
{
    public class ET_TaiNanLaoDong
    {
        public int Id { get; set; }
        public int IdNhanVien { get; set; }
        public DateTime NgayTaiNan { get; set; }
        public string LoaiTaiNan { get; set; }
        public string MucDo { get; set; } // Nhe, TrungBinh, NangNe, TuVong
        public string MoTa { get; set; }
        public int? SoNgayNghi { get; set; }
        public string TrangThai { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
