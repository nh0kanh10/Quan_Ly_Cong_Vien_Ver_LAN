using System;
using System.Windows.Forms;
using BUS;

namespace GUI
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            DevExpress.XtraEditors.WindowsFormsSettings.SetDPIAware();
            DevExpress.XtraEditors.WindowsFormsSettings.DefaultLookAndFeel.SetSkinStyle("WXI");

            // Load saved theme (persist across sessions)
            string savedTheme = Properties.Settings.Default.CurrentTheme;
            ThemeManager.SetTheme(string.IsNullOrEmpty(savedTheme) ? "SlateClassic" : savedTheme);

            if (BUS_Connection.Instance.CheckConnection())
            {
                Application.Run(new frmLogin());
            }
            else
            {
                Application.Run(new frmConfigConnect());
            }
        }
    }
}
