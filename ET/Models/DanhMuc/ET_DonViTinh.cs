using ET.Interfaces;

namespace ET.Models.DanhMuc
{
    public class ET_DonViTinh : IEntity
    {
        public int Id { get; set; }
        public string MaDonVi { get; set; }
        public string TenDonVi { get; set; }
        public bool ConHoatDong { get; set; }
    }
}
