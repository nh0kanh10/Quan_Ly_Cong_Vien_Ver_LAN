using ET.Interfaces;

namespace ET.Models.DanhMuc
{
    public class ET_ComboChiTiet : IEntity
    {
        public int Id { get; set; }
        public int IdCombo { get; set; }
        public int IdSanPham { get; set; }
        public decimal SoLuong { get; set; }
        public decimal TyLePhanBo { get; set; }
    }
}
