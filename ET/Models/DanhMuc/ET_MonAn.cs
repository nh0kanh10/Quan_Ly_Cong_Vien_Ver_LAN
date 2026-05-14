namespace ET.Models.DanhMuc
{
    public class ET_MonAn
    {
        public int IdSanPham { get; set; }
        public int? IdNhaHang { get; set; }
        public bool CoDiUng { get; set; }
        public string PhanLoai { get; set; }
        public string MoTaNgan { get; set; }
        public bool AnHienMenu { get; set; }
    }
}
