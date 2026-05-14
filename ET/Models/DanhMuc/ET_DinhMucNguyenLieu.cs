namespace ET.Models.DanhMuc
{
    public class ET_DinhMucNguyenLieu
    {
        public int Id { get; set; }
        public int IdThanhPham { get; set; }
        public int IdNguyenLieu { get; set; }
        public decimal SoLuong { get; set; }      
        public string TenDonVi { get; set; }
    }
}
