using System;
using ET.Interfaces;

namespace ET.Models.DanhMuc
{
    public class ET_Combo : IEntity, ISoftDelete
    {
        public int Id { get; set; }
        public string MaCombo { get; set; }
        public string TenCombo { get; set; }
        public decimal GiaCombo { get; set; }
        public string MoTa { get; set; }
        public string TrangThai { get; set; }
        public bool DaXoa { get; set; }
        public DateTime NgayTao { get; set; }
    }
}
