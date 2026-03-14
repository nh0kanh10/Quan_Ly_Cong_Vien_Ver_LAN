using System;
using System.Windows.Forms;
using BUS;

namespace WindowsFormsApp1
{
    public partial class frmConfigConnect : Form
    {
        public frmConfigConnect()
        {
            InitializeComponent();
        }

        private void btnTest_Click(object sender, EventArgs e)
        {
            string result = BUS_Connection.Instance.TestConnection(txtServer.Text, txtDatabase.Text, txtUser.Text, txtPassword.Text, chkWindowsAuth.Checked);
            if (result == "OK")
            {
                MessageBox.Show("Kết nối thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Kết nối thất bại!\n\nChi tiết lỗi: " + result, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            BUS_Connection.Instance.SaveConnectionString(txtServer.Text, txtDatabase.Text, txtUser.Text, txtPassword.Text, chkWindowsAuth.Checked);
            MessageBox.Show("Đã lưu cấu hình. Vui lòng khởi động lại ứng dụng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            this.Close();
        }

        private void chkWindowsAuth_CheckedChanged(object sender, EventArgs e)
        {
            txtUser.Enabled = txtPassword.Enabled = !chkWindowsAuth.Checked;
        }

        private void frmConfigConnect_Load(object sender, EventArgs e)
        {
            txtServer.Text = ".\\SQLEXPRESS";
            txtDatabase.Text = "QuanLyKhuVuiChoi";
        }
    }
}
