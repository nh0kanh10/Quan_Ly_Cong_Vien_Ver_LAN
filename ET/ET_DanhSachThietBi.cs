using System;

namespace ET
{
    public class ET_DanhSachThietBi
    {
        public int Id { get; set; }
        public string MaCode { get; set; }
        public string TenThietBi { get; set; }
        public string LoaiThietBi { get; set; }  
        public int? IdKhuVuc { get; set; }
        public string MoTa { get; set; }
        public DateTime? NgayMua { get; set; }
        public decimal? GiaTriMua { get; set; }
        public string TrangThai { get; set; }     
        public int? ChuKyBaoTriThang { get; set; }


        public string TenKhuVuc { get; set; }

        public string TenLoaiThietBi
        {
            get
            {
                switch (LoaiThietBi)
                {
                    case "TroChoi": return "Trò chơi";
                    case "TaoSong": return "Máy tạo sóng";
                    case "XeDien": return "Xe điện";
                    case "Kiosk": return "Kiosk";
                    case "BanAn": return "Bàn ăn";
                    case "NguaDua": return "Ngựa đua";
                    case "PhuongTienDua": return "Phương tiện đua";
                    default: return "Khác";
                }
            }
        }

        public string TenTrangThai
        {
            get
            {
                switch (TrangThai)
                {
                    case "HoatDong": return "Hoạt động";
                    case "BaoTri": return "Đang bảo trì";
                    case "TamDong": return "Tạm đóng";
                    case "Hong": return "Hỏng";
                    case "ThanhLy": return "Thanh lý";
                    default: return TrangThai;
                }
            }
        }
    }
}
