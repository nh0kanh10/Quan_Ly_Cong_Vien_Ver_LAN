using System;

namespace ET
{
    public partial class ET_TheRFID
    {
        public string MaRfid { get; set; }
        public int IdVi { get; set; }
        public string TrangThai { get; set; }
        public DateTime NgayKichHoat { get; set; }
        public DateTime? NgayHuy { get; set; }

        public string TenTrangThai
        {
            get
            {
                switch (TrangThai)
                {
                    case "Active": return "Đang hoạt động";
                    case "Pending": return "Chờ kích hoạt";
                    case "Lost": return "Báo mất";
                    case "Revoked": return "Đã bị thu hồi";
                    case "Locked": return "Đã khóa";
                    default: return TrangThai;
                }
            }
        }
    }
}
