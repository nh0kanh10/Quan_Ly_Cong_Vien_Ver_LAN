using ET.Constants;

namespace ET.Models.BanHang
{
    public class ET_DiemBanHang
    {
        public int    Id            { get; set; }
        public string MaDiemBan    { get; set; }
        public string TenDiemBan   { get; set; }
        public int    IdKhuVuc     { get; set; }
        public bool   ChoPhepBanVe  { get; set; }
        public bool   ChoPhepBanFNB { get; set; }
        public bool   ChoPhepThue   { get; set; }
        public string TrangThai    { get; set; } = AppConstants.TrangThaiHieuLuc.HoatDong;
    }
}
