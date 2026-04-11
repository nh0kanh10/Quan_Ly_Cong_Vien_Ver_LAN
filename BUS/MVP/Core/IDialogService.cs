using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BUS.MVP.Core
{
    public interface IDialogService
    {
        bool ShowConfirm(string message, string title);
        void ShowError(string message, string title = "Lỗi");
        void ShowInfo(string message, string title = "Thông báo");
    }
}
