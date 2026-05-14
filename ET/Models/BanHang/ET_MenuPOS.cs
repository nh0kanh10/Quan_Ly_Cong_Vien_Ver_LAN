namespace ET.Models.BanHang
{
    public class ET_MenuPOS
    {
        public int  IdDiemBan     { get; set; }
        public int  IdSanPham     { get; set; }
        public int  ThuTuHienThi  { get; set; }
        public bool ConHoatDong   { get; set; } = true;
    }
}
