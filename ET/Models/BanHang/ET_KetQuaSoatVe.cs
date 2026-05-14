namespace ET.Models.BanHang
{
    /// Kết quả trả về sau khi soát 1 vé tại cổng.
    /// MaKetQua: 0=Pass, 1=SaiKhuVuc, 2=HetLuot, 3=KhongTimThay, 4=SaiTroChoi, 5=HetHan, 6=DaHuy
    public class ET_KetQuaSoatVe
    {
        public int MaKetQua { get; set; }
        public ET_VeDienTu VeInfo { get; set; }
        public string ThongBaoKey { get; set; }
    }
}
