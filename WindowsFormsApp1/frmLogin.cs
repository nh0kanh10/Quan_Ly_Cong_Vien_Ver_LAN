using System;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using ET;

namespace WindowsFormsApp1
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string user = txtUser.Text.Trim();
            string pass = txtPass.Text.Trim();

            ET_TaiKhoan tk = BUS_TaiKhoan.Instance.DangNhap(user, pass);
            if (tk != null)
            {
                MessageBox.Show("Đăng nhập thành công! Chào " + tk.HoTen, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                
                // Lưu thông tin user đăng nhập vào tag của Main hoặc static class nếu cần
                Form1 main = new Form1();
                main.Tag = tk; 
                this.Hide();
                main.ShowDialog();
                this.Close();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void linkDangKy_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmDangKy dk = new frmDangKy();
            this.Hide();
            dk.ShowDialog();
            this.Show();
        }
    }
}
