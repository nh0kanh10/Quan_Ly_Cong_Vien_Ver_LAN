namespace GUI
{
    /// <summary>
    /// Interface chuẩn cho toàn bộ Form trong hệ thống Đại Nam.
    /// Giúp quản lý đồng nhất về bảo mật (RBAC), giao diện (UI/UX) và vòng đời dữ liệu.
    /// </summary>
    public interface IBaseForm
    {
        /// <summary>
        /// Áp dụng phân quyền (Gating VIEW/MANAGE) cho Form.
        /// </summary>
        void ApplyPermissions();

        /// <summary>
        /// Áp dụng giao diện (Theme, Grid Styling, etc.).
        /// </summary>
        void ApplyStyles();

        /// <summary>
        /// Khởi tạo icon cho các nút bấm (FontAwesome).
        /// </summary>
        void InitIcons();

        /// <summary>
        /// Tải hoặc làm mới dữ liệu lên Grid/UI.
        /// </summary>
        void LoadData();
    }
}
