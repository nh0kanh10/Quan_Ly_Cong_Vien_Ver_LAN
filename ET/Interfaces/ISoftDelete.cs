namespace ET.Interfaces
{
    // Đánh dấu entity hỗ trợ xoá mềm (soft delete).
    // Khi xoá, chỉ set DaXoa = true thay vì DELETE khỏi DB.
    public interface ISoftDelete
    {
        bool DaXoa { get; set; }
    }
}
