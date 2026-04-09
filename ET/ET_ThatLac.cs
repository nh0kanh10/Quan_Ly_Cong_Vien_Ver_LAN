using System;

namespace ET
{
    public partial class ET_ThatLac
    {
        public int Id { get; set; }
        public string MoTaDoVat { get; set; }
        public string NoiTimThay { get; set; }
        public string TrangThai { get; set; }
        public int? IdKhachHangNhan { get; set; }
        public DateTime ThoiGian { get; set; }
        public string TenKhachNhan { get; set; } 

        public string TenTrangThai
        {
            get
            {
                switch (TrangThai)
                {
                    case "ChoNhan": return "Chờ nhận";
                    case "DaTra": return "Đã trả khách";
                    case "DaThanhLy": return "Đã thanh lý";
                    default: return TrangThai;
                }
            }
        }
        public ET_ThatLac(int id, string moTaDoVat, string noiTimThay, string trangThai, int? idKhachHangNhan)
        {
            Id = id;
            MoTaDoVat = moTaDoVat;
            NoiTimThay = noiTimThay;
            TrangThai = trangThai;
            IdKhachHangNhan = idKhachHangNhan;
        }
        public ET_ThatLac()
        {
        }
    }
}
