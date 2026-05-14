namespace ET.Interfaces
{
    // Mọi entity (bảng DB) đều có Id kiểu int.
    // Dùng trong BaseRepository để viết generic CRUD.
    public interface IEntity
    {
        int Id { get; set; }
    }
}
