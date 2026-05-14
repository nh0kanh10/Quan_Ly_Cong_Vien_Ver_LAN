using System.Collections.Generic;

namespace GUI.AI
{
    // Module implement interface này sẽ nhận lệnh thao tác UI từ AI.
    // Lệnh được gọi từ luồng nền nên module phải dùng Invoke() nếu cần thay đổi giao diện.
    public interface IAICommandHandler
    {
        // Thực thi lệnh từ AI: ui_filter_grid, ui_select_row, ui_clear_filter...
        void ExecuteAICommand(string commandName, Dictionary<string, object> args);
    }
}
