using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;

namespace WindowsFormsApp1
{
    public partial class frmKhuVuc : Form
    {
        public frmKhuVuc()
        {
            InitializeComponent();
        }

        private void frmKhuVuc_Load(object sender, EventArgs e)
        {
            loadDS();
            loadComboBoxes();
            txtMaCode.Text = BUS_KhuVuc.Instance.LayMaCodeTiepTheo();
        }

        private void loadDS()
        {
            dgvKhuVuc.DataSource = BUS_KhuVuc.Instance.LoadDSKhuVuc();
        }

        private void loadComboBoxes()
        {
            cboTrangThai.DataSource = new List<string> { "Hoạt động", "Ngừng hoạt động" };
        }

        private void dgvKhuVuc_Click(object sender, EventArgs e)
        {
            if (dgvKhuVuc.CurrentRow != null)
            {
                int dong = dgvKhuVuc.CurrentCell.RowIndex;
                txtMaKV.Text = dgvKhuVuc.Rows[dong].Cells["MaKhuVuc"].Value.ToString();
                txtMaCode.Text = dgvKhuVuc.Rows[dong].Cells["MaCode"].Value.ToString();
                txtTenKV.Text = dgvKhuVuc.Rows[dong].Cells["TenKhuVuc"].Value.ToString();
                txtMoTa.Text = dgvKhuVuc.Rows[dong].Cells["MoTa"].Value?.ToString();
                cboTrangThai.Text = dgvKhuVuc.Rows[dong].Cells["TrangThai"].Value.ToString();
                dtpNgayTao.Value = Convert.ToDateTime(dgvKhuVuc.Rows[dong].Cells["NgayTao"].Value);
                
                var ngayCapNhat = dgvKhuVuc.Rows[dong].Cells["NgayCapNhat"].Value;
                if (ngayCapNhat != null && ngayCapNhat != DBNull.Value)
                    dtpNgayCapNhat.Value = Convert.ToDateTime(ngayCapNhat);
            }
        }

        private void lamMoi()
        {
            txtMaKV.Clear();
            txtMaCode.Text = BUS_KhuVuc.Instance.LayMaCodeTiepTheo();
            txtTenKV.Clear();
            txtMoTa.Clear();
            cboTrangThai.SelectedIndex = 0;
            dtpNgayTao.Value = DateTime.Now;
            dtpNgayCapNhat.Value = DateTime.Now;
            txtTimKiem.Clear();
            loadDS();
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lamMoi();
        }

        private ET_KhuVuc getET()
        {
            return new ET_KhuVuc
            {
                MaCode = txtMaCode.Text,
                TenKhuVuc = txtTenKV.Text,
                MoTa = txtMoTa.Text,
                TrangThai = cboTrangThai.Text
            };
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ET_KhuVuc et = getET();
            string check = BUS_KhuVuc.Instance.ValidateKhuVuc(et, true);
            if (!string.IsNullOrEmpty(check))
            {
                MessageBox.Show(check, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (BUS_KhuVuc.Instance.ThemKhuVuc(et))
            {
                MessageBox.Show("Thêm khu vực thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lamMoi();
            }
            else
            {
                MessageBox.Show("Thêm thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            ET_KhuVuc et = getET();
            string check = BUS_KhuVuc.Instance.ValidateKhuVuc(et, false);
            if (!string.IsNullOrEmpty(check))
            {
                MessageBox.Show(check, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (BUS_KhuVuc.Instance.SuaKhuVuc(et))
            {
                MessageBox.Show("Cập nhật thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDS();
            }
            else
            {
                MessageBox.Show("Cập nhật thất bại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCode.Text)) return;

            DialogResult r = MessageBox.Show("Bạn có chắc muốn xóa khu vực này?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                if (BUS_KhuVuc.Instance.XoaKhuVuc(txtMaCode.Text))
                {
                    MessageBox.Show("Xóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    lamMoi();
                }
                else
                {
                    MessageBox.Show("Xóa thất bại! Khu vực có thể đang chứa trò chơi.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa))
            {
                loadDS();
            }
            else
            {
                dgvKhuVuc.DataSource = BUS_KhuVuc.Instance.TimKiem(tuKhoa);
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
