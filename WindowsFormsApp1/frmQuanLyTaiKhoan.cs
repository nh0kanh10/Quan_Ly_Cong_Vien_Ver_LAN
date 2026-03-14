using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BUS;
using ET;

namespace WindowsFormsApp1
{
    public partial class frmQuanLyTaiKhoan : Form
    {
        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
        }

        private void frmQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            loadDS();
            cboVaiTro.DataSource = new List<string> { "Admin", "NhanVien" };
        }

        private void loadDS()
        {
            dgvTaiKhoan.DataSource = BUS_TaiKhoan.Instance.LoadDS();
        }

        private void dgvTaiKhoan_Click(object sender, EventArgs e)
        {
            if (dgvTaiKhoan.CurrentRow != null)
            {
                int r = dgvTaiKhoan.CurrentCell.RowIndex;
                txtMaTK.Text = dgvTaiKhoan.Rows[r].Cells["MaTaiKhoan"].Value.ToString();
                txtUser.Text = dgvTaiKhoan.Rows[r].Cells["TenDangNhap"].Value.ToString();
                txtPass.Text = dgvTaiKhoan.Rows[r].Cells["MatKhau"].Value.ToString();
                txtHoTen.Text = dgvTaiKhoan.Rows[r].Cells["HoTen"].Value.ToString();
                cboVaiTro.Text = dgvTaiKhoan.Rows[r].Cells["VaiTro"].Value.ToString();
                chkTrangThai.Checked = (bool)dgvTaiKhoan.Rows[r].Cells["TrangThai"].Value;
            }
        }

        private ET_TaiKhoan getET()
        {
            return new ET_TaiKhoan
            {
                MaTaiKhoan = string.IsNullOrEmpty(txtMaTK.Text) ? 0 : int.Parse(txtMaTK.Text),
                TenDangNhap = txtUser.Text,
                MatKhau = txtPass.Text,
                HoTen = txtHoTen.Text,
                VaiTro = cboVaiTro.Text,
                TrangThai = chkTrangThai.Checked
            };
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ET_TaiKhoan et = getET();
            string check = BUS_TaiKhoan.Instance.ValidateTaiKhoan(et, true);
            if (!string.IsNullOrEmpty(check)) { MessageBox.Show(check); return; }

            if (BUS_TaiKhoan.Instance.ThemTaiKhoan(et))
            {
                MessageBox.Show("Thêm thành công!");
                loadDS();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            ET_TaiKhoan et = getET();
            string check = BUS_TaiKhoan.Instance.ValidateTaiKhoan(et, false);
            if (!string.IsNullOrEmpty(check)) { MessageBox.Show(check); return; }

            if (BUS_TaiKhoan.Instance.SuaTaiKhoan(et))
            {
                MessageBox.Show("Sửa thành công!");
                loadDS();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaTK.Text)) return;
            if (MessageBox.Show("Xóa tài khoản này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (BUS_TaiKhoan.Instance.XoaTaiKhoan(int.Parse(txtMaTK.Text)))
                {
                    MessageBox.Show("Xóa thành công!");
                    loadDS();
                }
            }
        }
    }
}
