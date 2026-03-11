using BUS;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ET;

namespace WindowsFormsApp1
{
    public partial class frmTroChoi : Form
    {
        public frmTroChoi()
        {
            InitializeComponent();
        }

        private void dgvLoaiVe_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void loadDS()
        {
            var ds = BUS_TroChoi.Instance.loadDS();
            dgvTroChoi.DataSource = ds;
            dgvTroChoi.Columns["MaTroChoi"].DisplayIndex = 0;
            dgvTroChoi.Columns["MaCode"].DisplayIndex = 1;
            dgvTroChoi.Columns["TenTroChoi"].DisplayIndex = 2;
            dgvTroChoi.Columns["LoaiTroChoi"].DisplayIndex = 3;
            dgvTroChoi.Columns["TrangThai"].DisplayIndex = 6;
            cboChieuCao.SelectedIndex = 0;

            cboLoaiTC.DataSource = ds.Select(x => x.LoaiTroChoi).Distinct().ToList();
            cboTrangThai.DataSource = ds.Select(x => x.TrangThai).Distinct().ToList();
        }

        private void frmTroChoi_Load(object sender, EventArgs e)
        {
            loadDS();
            txtMaCode.Text = BUS_TroChoi.Instance.layMaCodeTiepTheo();
            txtMaTC.Text = (BUS_TroChoi.Instance.layMaTroChoiLonNhat() + 1).ToString();

        }

        private void dgvTroChoi_Click(object sender, EventArgs e)
        {
            if (dgvTroChoi.CurrentRow == null) return;
            int dong = dgvTroChoi.CurrentCell.RowIndex;

            txtMaTC.Text = dgvTroChoi.Rows[dong].Cells["MaKhuVuc"].Value.ToString();
            txtMaCode.Text = dgvTroChoi.Rows[dong].Cells["MaCode"].Value.ToString();
            txtTenTC.Text = dgvTroChoi.Rows[dong].Cells["TenTroChoi"].Value.ToString();

            cboLoaiTC.Text = dgvTroChoi.Rows[dong].Cells["LoaiTroChoi"].Value.ToString();

            txtMaKV.Text = dgvTroChoi.Rows[dong].Cells["MaKhuVuc"].Value.ToString();
            txtSucChua.Text = dgvTroChoi.Rows[dong].Cells["SucChua"].Value.ToString();
            cboTrangThai.Text = dgvTroChoi.Rows[dong].Cells["TrangThai"].Value.ToString();
            txtTuoi.Text = dgvTroChoi.Rows[dong].Cells["TuoiToiThieu"].Value.ToString();
            cboChieuCao.Text = dgvTroChoi.Rows[dong].Cells["ChieuCaoToiThieu"].Value.ToString();
            txtThoiGianLuot.Text = dgvTroChoi.Rows[dong].Cells["ThoiGianLuot"].Value.ToString();
            richTextBox1.Text = dgvTroChoi.Rows[dong].Cells["MoTa"].Value.ToString();
            if (DateTime.TryParse(dgvTroChoi.Rows[dong].Cells["NgayTao"].Value.ToString(), out DateTime ngayTao))
            {
                dtpNgayTao.Value = ngayTao;
            }
            if (DateTime.TryParse(dgvTroChoi.Rows[dong].Cells["NgayCapNhat"].Value?.ToString(), out DateTime ngayCapNhat))
            {
                dtpNgayCapNhat.Value = ngayCapNhat;
            }
        }
        private void lamMoi()
        {
            txtMaTC.Text = (BUS_TroChoi.Instance.layMaTroChoiLonNhat() + 1).ToString();
            txtMaCode.Text = BUS_TroChoi.Instance.layMaCodeTiepTheo();
            txtMaKV.Clear();
            txtTenTC.Clear();
            cboLoaiTC.SelectedIndex = -1;
            cboChieuCao.SelectedIndex = -1;
            cboTrangThai.SelectedIndex = -1;
            txtSucChua.Clear();
            txtTuoi.Clear();
            txtThoiGianLuot.Clear();
            richTextBox1.Clear();
            dtpNgayTao.Value = DateTime.Now;
            dtpNgayCapNhat.Value = DateTime.Now;

        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lamMoi();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // tạo đối tượng dữ liệu từ form
            ET_TroChoi et = new ET_TroChoi
            {

                TenTroChoi = txtTenTC.Text,
                MaKhuVuc = Convert.ToInt32(txtMaKV.Text),
                LoaiTroChoi = cboLoaiTC.Text,
                SucChua = Convert.ToInt32(txtSucChua.Text),
                TuoiToiThieu = Convert.ToInt32(txtTuoi.Text),
                ChieuCaoToiThieu = Convert.ToInt32(cboChieuCao.Text),
                ThoiGianLuot = Convert.ToInt32(txtThoiGianLuot.Text),
                MoTa = richTextBox1.Text,
                TrangThai = cboTrangThai.Text,
                NgayTao = dtpNgayTao.Value,
                NgayCapNhat = dtpNgayCapNhat.Value,
            };

            bool kq = BUS_TroChoi.Instance.themTroChoi(et);
            if (kq)
            {
                MessageBox.Show("Thêm trò chơi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDS();
                lamMoi();
            }
            else
            {
                MessageBox.Show("Thêm trò chơi thất bại! Vui lòng kiểm tra dữ liệu", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvTroChoi.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn trò chơi cần xóa!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            // lấy mã trò chơi hiện tại
            string maCodeCanXoa = dgvTroChoi.CurrentRow.Cells["MaCode"].Value.ToString();

            DialogResult r = MessageBox.Show($"Bạn có chắc chắn muốn xóa trò chơi có mã {maCodeCanXoa}?", "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                bool kq = BUS_TroChoi.Instance.xoaTroChoi(maCodeCanXoa);
                if (kq)
                {
                    MessageBox.Show("Xóa trò chơi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show("Xóa trò chơi thất bại! Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
                loadDS();
                lamMoi();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            ET_TroChoi et = new ET_TroChoi
            {
                MaCode = txtMaCode.Text,
                TenTroChoi = txtTenTC.Text,
                MaKhuVuc = Convert.ToInt32(txtMaKV.Text),
                LoaiTroChoi = cboLoaiTC.Text,
                SucChua = Convert.ToInt32(txtSucChua.Text),
                TuoiToiThieu = Convert.ToInt32(txtTuoi.Text),
                ChieuCaoToiThieu = Convert.ToInt32(cboChieuCao.Text),
                ThoiGianLuot = Convert.ToInt32(txtThoiGianLuot.Text),
                MoTa = richTextBox1.Text,
                TrangThai = cboTrangThai.Text,
                NgayTao = dtpNgayTao.Value,
                NgayCapNhat = dtpNgayCapNhat.Value,
            };
            DialogResult r = MessageBox.Show($"Bạn có chắc chắn muốn sửa thông tin trò chơi có mã {et.MaCode}?", "Xác nhận sửa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                bool kq = BUS_TroChoi.Instance.capNhatTroChoi(et);
                if (kq)
                {
                    MessageBox.Show("Sửa thông tin trò chơi thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Sửa thông tin trò chơi thất bại! Vui lòng thử lại sau.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                loadDS();
                lamMoi();
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("Bạn đang thoát màn hình ?", "THÔNG BÁO", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
