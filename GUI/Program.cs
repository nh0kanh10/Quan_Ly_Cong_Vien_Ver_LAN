using System;
using System.Windows.Forms;
using GUI.Infrastructure;
using GUI.Shell;
using GUI.SystemForms;

namespace GUI
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            AppStyle.Init();
            
            if (!BUS_Connection.Instance.CheckCurrentConnection())
            {
                UIHelper.CanhBao("Hệ thống không thể kết nối tới Cơ sở dữ liệu.\nVui lòng kiểm tra lại cấu hình kết nối!");
                using (var configForm = new frmConfigConnect())
                {
                    configForm.ShowDialog();
                }
                if (!BUS_Connection.Instance.CheckCurrentConnection())
                {
                    return;
                }
            }

            try 
            {
                using (var loginForm = new GUI.Modules.Auth.frmLogin())
                {
                    if (loginForm.ShowDialog() == DialogResult.OK)
                    {
                        Application.Run(new frmMain());
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
            }
            catch (Exception ex)
            {
                System.IO.File.WriteAllText("crash.txt", $"{ex.Message}\n{ex.StackTrace}");
                MessageBox.Show(ex.Message + "\n" + ex.StackTrace);
            }
        }
    }
}
