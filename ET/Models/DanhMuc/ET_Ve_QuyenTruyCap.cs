namespace ET.Models.DanhMuc
{
    public class ET_Ve_QuyenTruyCap
    {
        public int Id { get; set; }
        public int IdSanPhamVe { get; set; }
        public int IdKhuVuc { get; set; }
        public int? IdTroChoi { get; set; }
        public int SoLuotChoPhep { get; set; }
        public string GhiChu { get; set; }
    }
}
