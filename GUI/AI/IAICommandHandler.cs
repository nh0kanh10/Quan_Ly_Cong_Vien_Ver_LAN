using System.Collections.Generic;

namespace GUI.AI
{
    /// <summary>
    /// Interface cho phép AI thao tác ngược lại giao diện (Bắn Action xuống Form).
    /// </summary>
    public interface IAICommandHandler
    {
        /// <summary>
        /// Thực thi lệnh từ AI (Grid filtering, Selection, Navigation trên form).
        /// LƯU Ý: Hàm này tự động được điều hướng thông qua Form1 Event,
        /// nhưng nó được gọi từ luồng nền, do đó Form phải gọi Invoke nếu can thiệp UI.
        /// </summary>
        void ExecuteAICommand(string commandName, Dictionary<string, object> args);
    }
}
