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
                main.Owner = this; // Đặt Login làm Owner để Form1 có thể gọi Show() lại Login
                this.Hide();
                main.Show();
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
    }
}
