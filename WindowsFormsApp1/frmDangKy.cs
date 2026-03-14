using System;
using System.Windows.Forms;
using BUS;
using ET;

namespace WindowsFormsApp1
{
    public partial class frmDangKy : Form
    {
        public frmDangKy()
        {
            InitializeComponent();
        }

        private void btnDangKy_Click(object sender, EventArgs e)
        {
            ET_TaiKhoan et = new ET_TaiKhoan
            {
                TenDangNhap = txtUser.Text.Trim(),
                MatKhau = txtPass.Text.Trim(),
                HoTen = txtHoTen.Text.Trim(),
                VaiTro = "NhanVien",
                TrangThai = true
            };

            string check = BUS_TaiKhoan.Instance.ValidateTaiKhoan(et, true);
            if (!string.IsNullOrEmpty(check))
            {
                MessageBox.Show(check, "Thông báo");
                return;
            }

            if (BUS_TaiKhoan.Instance.ThemTaiKhoan(et))
            {
                MessageBox.Show("Đăng ký thành công! Hãy đăng nhập.");
                this.Close();
            }
            else
            {
                MessageBox.Show("Thất bại!");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
