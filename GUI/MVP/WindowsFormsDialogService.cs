using System;
using System.Windows.Forms;
using BUS.MVP.Core;
using GUI.Helpers; 

namespace GUI.MVP
{
    public class WindowsFormsDialogService : IDialogService
    {
        public bool ShowConfirm(string message, string title)
        {
            return TDCMessageBox.Show(message, title, MessageBoxButtons.YesNo) == DialogResult.Yes;
        }

        public void ShowError(string message, string title = "Lỗi")
        {
            TDCMessageBox.Show(message, title, MessageBoxButtons.OK);
        }

        public void ShowInfo(string message, string title = "Thông báo")
        {
            TDCMessageBox.Show(message, title, MessageBoxButtons.OK);
        }
    }
}
