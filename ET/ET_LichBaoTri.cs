using System;

namespace ET
{
    public class ET_LichBaoTri
    {
        public int Id { get; set; }
        public int IdThietBi { get; set; }
        public DateTime NgayBaoTri { get; set; }
        public string LoaiBaoTri { get; set; }
        public string NoiDung { get; set; }
        public decimal ChiPhi { get; set; }
        public int? IdNhanVienThucHien { get; set; }
        public int? IdPhieuChi { get; set; }
        public string TrangThai { get; set; }


        public string TenThietBi { get; set; }
        public string TenNhanVien { get; set; }

        public string TenLoaiBaoTri
        {
            get
            {
                switch (LoaiBaoTri)
                {
                    case "DieuDo": return "Điều độ";
                    case "SuaChua": return "Sửa chữa";
                    case "ThayThe": return "Thay thế";
                    case "ThanhLy": return "Thanh lý";
                    default: return LoaiBaoTri;
                }
            }
        }

        public string TenTrangThai
        {
            get
            {
                switch (TrangThai)
                {
                    case "KeHoach": return "Kế hoạch";
                    case "DangLam": return "Đang làm";
                    case "HoanTat": return "Hoàn tất";
                    default: return TrangThai;
                }
            }
        }
    }
}
