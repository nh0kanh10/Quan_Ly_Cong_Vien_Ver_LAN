using System;

namespace ET
{
    public class ET_DonXinNghi
    {
        public int Id { get; set; }
        public int IdNhanVien { get; set; }
        public string LoaiNghi { get; set; } // PhepNam, NghiOm, NghiKhongLuong
        public DateTime NgayBatDau { get; set; }
        public DateTime NgayKetThuc { get; set; }
        public int SoNgay { get; set; } // Computed DB
        public decimal TiLeLuongHuong { get; set; }
        public string NguonChiTra { get; set; } 
        public string LyDo { get; set; }
        public string TrangThai { get; set; } // ChoDuyet, DaDuyet, TuChoi
        public DateTime CreatedAt { get; set; }
    }
}
