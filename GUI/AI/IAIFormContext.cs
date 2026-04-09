namespace GUI.AI
{
    /// <summary>
    /// Interface cho Form muốn đăng ký ngữ cảnh AI riêng (Não Chuyên Sâu).
    /// Form nào KHÔNG implement interface này sẽ dùng Não Điều Hướng mặc định.
    /// </summary>
    public interface IAIFormContext
    {
        /// <summary>Tên context duy nhất (vd: "frmKhoHang")</summary>
        string AIContextName { get; }

        /// <summary>Mô tả ngắn để AI hiểu form này làm gì</summary>
        string AIContextDescription { get; }
    }
}
