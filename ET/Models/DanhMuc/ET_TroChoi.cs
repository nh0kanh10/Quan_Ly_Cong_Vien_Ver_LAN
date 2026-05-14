namespace ET.Models.DanhMuc
{
    public class ET_TroChoi
    {
        public int Id { get; set; }
        public string TenTroChoi { get; set; }
        public int? IdKhuVuc { get; set; }
        public bool DaXoa { get; set; }
    }
}
