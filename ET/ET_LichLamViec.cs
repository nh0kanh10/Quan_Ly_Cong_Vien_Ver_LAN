using System;

namespace ET
{
    public class ET_LichLamViec
    {
        public int Id { get; set; }
        public int IdNhanVien { get; set; }
        public int IdKhuVuc { get; set; }
        public DateTime NgayLam { get; set; }
        public string CaLam { get; set; }
        public TimeSpan GioBatDau { get; set; }
        public TimeSpan GioKetThuc { get; set; }
        public string TrangThai { get; set; }
        public string GhiChu { get; set; }

        public string TenNhanVien { get; set; }
        public string TenKhuVuc { get; set; }

        public static TimeSpan LayGioBatDau(string caLam)
        {
            switch (caLam)
            {
                case "Sang": return new TimeSpan(7, 30, 0);
                case "Chieu": return new TimeSpan(12, 0, 0);
                case "Dem": return new TimeSpan(18, 0, 0);
                default: return new TimeSpan(7, 30, 0);
            }
        }

        public static TimeSpan LayGioKetThuc(string caLam)
        {
            switch (caLam)
            {
                case "Sang": return new TimeSpan(12, 0, 0);
                case "Chieu": return new TimeSpan(17, 30, 0);
                case "Dem": return new TimeSpan(7, 0, 0); 
                default: return new TimeSpan(12, 0, 0);
            }
        }

        public static string LayTenCa(string caLam)
        {
            switch (caLam)
            {
                case "Sang": return "Ca Sáng (7:30 - 12:00)";
                case "Chieu": return "Ca Chiều (12:00 - 17:30)";
                case "Dem": return "Ca Đêm (18:00 - 7:00)";
                default: return caLam;
            }
        }
    }
}
