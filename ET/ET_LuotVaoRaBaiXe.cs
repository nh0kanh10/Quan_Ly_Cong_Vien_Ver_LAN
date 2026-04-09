using System;

namespace ET
{
    public partial class ET_LuotVaoRaBaiXe
    {
        public int Id { get; set; }
        public string BienSo { get; set; }
        public string LoaiXe { get; set; }
        public string MaRfid { get; set; }
        public string AnhBienSo { get; set; }
        public DateTime ThoiGianVao { get; set; }
        public DateTime? ThoiGianRa { get; set; }
        public string TrangThai { get; set; }

        public string TenLoaiXe
        {
            get
            {
                switch (LoaiXe)
                {
                    case AppConstants.LoaiXe.XeDap: return "Xe đạp";
                    case AppConstants.LoaiXe.XeMay: return "Xe máy";
                    case AppConstants.LoaiXe.OTo: return "Ô tô";
                    case AppConstants.LoaiXe.XeDien: return "Xe điện";
                    default: return LoaiXe;
                }
            }
        }

        public string TenTrangThai
        {
            get
            {
                switch (TrangThai)
                {
                    case AppConstants.TrangThaiGuiXe.DangGui: return "Đang gửi";
                    case AppConstants.TrangThaiGuiXe.DaTra: return "Đã trả";
                    case AppConstants.TrangThaiGuiXe.MatVe: return "Mất vé";
                    default: return TrangThai;
                }
            }
        }

        public string ThoiGianGuiHienThi
        {
            get
            {
                if (TrangThai != AppConstants.TrangThaiGuiXe.DangGui) return "";
                var duration = DateTime.Now - ThoiGianVao;
                if (duration.TotalHours >= 1)
                    return string.Format("{0}h {1}p", (int)duration.TotalHours, duration.Minutes);
                return string.Format("{0} phút", (int)duration.TotalMinutes);
            }
        }
    }
}
