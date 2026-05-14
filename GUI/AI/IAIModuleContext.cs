namespace GUI.AI
{
    // Module (UserControl) implement interface này sẽ có ngữ cảnh AI riêng.
    // Module nào không implement sẽ dùng ngữ cảnh điều hướng mặc định.
    public interface IAIModuleContext
    {
        // MenuKey duy nhất, trùng với key trong _appRouter (VD: "SAN_PHAM")
        string AIContextName { get; }

        // Mô tả ngắn để AI hiểu module này làm gì
        string AIContextDescription { get; }

        // Câu hỏi gợi ý khi người dùng chuyển sang module này
        string[] SuggestedQuestions { get; }

        // Danh sách cột có thể lọc trên GridView chính
        string[] FilterableColumns { get; }
    }
}
