using ET.Interfaces;

namespace ET.Models.HeThong
{
    // Bảng HeThong.LuongTrangThai — Quy định chuyển trạng thái hợp lệ.
    // Code C# dùng StateService.ValidateTransition() để check trước khi UPDATE.
    public class LuongTrangThai : IEntity
    {
        public int Id { get; set; }
        public string ThucThe { get; set; }
        public string TuTrangThai { get; set; }
        public string DenTrangThai { get; set; }
        public string MaQuyenCanThiet { get; set; }
        public string MoTa { get; set; }
    }
}
