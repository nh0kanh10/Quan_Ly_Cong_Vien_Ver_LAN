using System;

namespace ET
{
    public class ET_ChungChiNhanVien
    {
        public int Id { get; set; }
        public int IdNhanVien { get; set; }
        public string LoaiChungChi { get; set; }
        public string SoChungChi { get; set; }
        public string NhaCap { get; set; }
        public DateTime NgayCap { get; set; }
        public DateTime NgayHetHan { get; set; }
        public string TrangThai { get; set; }   // ConHieuLuc | SapHetHan | HetHan — computed DB
        public string HinhAnhFile { get; set; }
    }
}
