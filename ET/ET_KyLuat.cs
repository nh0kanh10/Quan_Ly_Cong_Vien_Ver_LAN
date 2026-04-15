using System;

namespace ET
{
    public class ET_KyLuat
    {
        public int Id { get; set; }
        public int IdNhanVien { get; set; }
        public DateTime NgayApDung { get; set; }
        public string HinhThuc { get; set; }    // CanhCao | TruLuong | DinhChiCoLuong | SaThai
        public decimal SoTienTru { get; set; }
        public int SoNgayDinhChi { get; set; }
        public string MoTa { get; set; }
        public int IdNguoiQuyetDinh { get; set; }
        public string TenNguoiQuyetDinh { get; set; }
        public DateTime? NgayHetHieuLuc { get; set; }
    }
}
