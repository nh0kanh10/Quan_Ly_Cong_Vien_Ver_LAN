using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GUI.Infrastructure;

namespace GUI.SystemForms
{
    public partial class frmConfigConnect : XtraForm
    {
        public frmConfigConnect()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Cấu hình kết nối Cơ sở dữ liệu";
            this.IconOptions.ShowIcon = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string server = txtServer.Text.Trim();
            string database = txtDatabase.Text.Trim();

            if (string.IsNullOrEmpty(server))
            {
                UIHelper.CanhBao("Vui lòng nhập tên Server!");
                return;
            }
            if (string.IsNullOrEmpty(database))
            {
                UIHelper.CanhBao("Vui lòng nhập tên Database!");
                return;
            }

            // Sử dụng BUS_Connection cục bộ
            string result = BUS_Connection.Instance.TestConnection(server, database, txtUser.Text, txtPassword.Text, chkWindowsAuth.Checked);
            if (result == "OK")
            {
                UIHelper.ThongBao("Kết nối thành công!");
            }
            else
            {
                UIHelper.Loi("Kết nối thất bại!\n\nChi tiết lỗi: " + result);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string server = txtServer.Text.Trim();
            string database = txtDatabase.Text.Trim();

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(database))
            {
                UIHelper.CanhBao("Server và Database không được để trống!");
                return;
            }

            if (UIHelper.XacNhan("Bạn có chắc chắn muốn lưu cấu hình này? Hệ thống sẽ yêu cầu khởi động lại."))
            {
                try
                {
                    BUS_Connection.Instance.SaveConnectionString(server, database, txtUser.Text, txtPassword.Text, chkWindowsAuth.Checked);
                    UIHelper.ThongBao("Đã lưu cấu hình. Ứng dụng sẽ đóng lại để áp dụng thay đổi.");
                    Application.Exit();
                }
                catch (Exception ex)
                {
                    UIHelper.Loi("Lỗi khi lưu cấu hình: " + ex.Message);
                }
            }
        }

        private void chkWindowsAuth_CheckedChanged(object sender, EventArgs e)
        {
            txtUser.Enabled = txtPassword.Enabled = !chkWindowsAuth.Checked;
            if (chkWindowsAuth.Checked)
            {
                txtUser.Text = "";
                txtPassword.Text = "";
            }
        }

        private void frmConfigConnect_Load(object sender, EventArgs e)
        {
            txtServer.Text = @".\SQLEXPRESS";
            txtDatabase.Text = "Database_DaiNamv2";
            chkWindowsAuth.Checked = true;
        }
    }
}
