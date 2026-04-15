using System;
using System.Drawing;
using System.Threading.Tasks;
using FontAwesome.Sharp;
using System.Windows.Forms;
using BUS;

namespace GUI
{
    public partial class frmConfigConnect : Form
    {
        public frmConfigConnect()
        {
            InitializeComponent();
            InitIcons();
            ThemeManager.ApplyTheme(this);
        }

        private void InitIcons()
        {
            btnTest.Image = FontAwesome.Sharp.IconChar.Plug.ToBitmap(Color.White, 20);
            btnSave.Image = FontAwesome.Sharp.IconChar.FloppyDisk.ToBitmap(Color.White, 20);
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string server = txtServer.Text.Trim();
            string database = txtDatabase.Text.Trim();

            if (string.IsNullOrEmpty(server))
            {
                TDCMessageBox.Show("Vui lòng nhập tên Server!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrEmpty(database))
            {
                TDCMessageBox.Show("Vui lòng nhập tên Database!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string result = BUS_Connection.Instance.TestConnection(server, database, txtUser.Text, txtPassword.Text, chkWindowsAuth.Checked);
            if (result == "OK")
            {
                TDCMessageBox.Show("Kết nối thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                TDCMessageBox.Show("Kết nối thất bại!\n\nChi tiết lỗi: " + result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            string server = txtServer.Text.Trim();
            string database = txtDatabase.Text.Trim();

            if (string.IsNullOrEmpty(server) || string.IsNullOrEmpty(database))
            {
                TDCMessageBox.Show("Server và Database không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            BUS_Connection.Instance.SaveConnectionString(server, database, txtUser.Text, txtPassword.Text, chkWindowsAuth.Checked);
            TDCMessageBox.Show("Đã lưu cấu hình. Vui lòng khởi động lại ứng dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void chkWindowsAuth_CheckedChanged(object sender, EventArgs e)
        {
            txtUser.ReadOnly = txtPassword.ReadOnly = chkWindowsAuth.Checked;
        }

        private void frmConfigConnect_Load(object sender, EventArgs e)
        {
            txtServer.Text = @".\SQLEXPRESS"; // Giá trị mặc định
            txtDatabase.Text = "DaiNamResort"; 
        }
    }
}
