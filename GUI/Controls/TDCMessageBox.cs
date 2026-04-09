using System.Windows.Forms;

namespace GUI
{
    public static class TDCMessageBox
    {
        public static DialogResult Show(string message)
        {
            return Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Show(string message, string title)
        {
            return Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static DialogResult Show(string message, string title, MessageBoxButtons buttons)
        {
            return Show(message, title, buttons, MessageBoxIcon.Information);
        }

        public static DialogResult Show(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            using (var msgForm = new frmCustomMessageBox(message, title, buttons, icon))
            {
                return msgForm.ShowDialog();
            }
        }
    }
}
