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
        private ET_TroChoi layDuLieu()
        {
            return new ET_TroChoi
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
        }
        bool flag = false;
        private void dgvTroChoi_Click(object sender, EventArgs e)
        {
            if (dgvTroChoi.CurrentRow == null) return;
            flag = true; // chặn sự kiện lại
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
            //mở sự kiện
            flag = false;
        }
        private void lamMoi()
        {
            flag = true;
            txtMaTC.Text = (BUS_TroChoi.Instance.layMaTroChoiLonNhat() + 1).ToString();
            txtMaCode.Text = BUS_TroChoi.Instance.layMaCodeTiepTheo();
            txtMaKV.Clear();
            txtTenTC.Clear();
            cboLoaiTC.SelectedIndex = 0;
            cboChieuCao.SelectedIndex = 0;
            cboTrangThai.SelectedIndex = 0;
            txtSucChua.Clear();
            txtTuoi.Clear();
            txtThoiGianLuot.Clear();
            richTextBox1.Clear();
            dtpNgayTao.Value = DateTime.Now;
            dtpNgayCapNhat.Value = DateTime.Now;
            dgvTroChoi.ClearSelection();
            txtTenTC.Focus();
            errorProvider1.Clear();
            flag = false;
        }
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lamMoi();
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            // Kiểm tra nhập liệu (validation cơ bản)
            if (string.IsNullOrWhiteSpace(txtTenTC.Text) || string.IsNullOrWhiteSpace(txtTuoi.Text) || string.IsNullOrWhiteSpace(txtThoiGianLuot.Text)
                || string.IsNullOrWhiteSpace(txtSucChua.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc (Tên trò chơi, Tuổi tối thiểu, Thời gian mỗi lượt, Sức chứa tối đa)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!KiemTraNgayHopLe()) return;
            try
            {
                // tạo đối tượng dữ liệu từ form
                ET_TroChoi et = layDuLieu();
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
            catch(Exception ex)
            {
               MessageBox.Show("Đã xảy ra lỗi khi thêm trò chơi: " + ex.Message);
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
            // Kiểm tra nhập liệu (validation cơ bản)
            if (string.IsNullOrWhiteSpace(txtTenTC.Text) || string.IsNullOrWhiteSpace(txtTuoi.Text) || string.IsNullOrWhiteSpace(txtThoiGianLuot.Text)
                || string.IsNullOrWhiteSpace(txtSucChua.Text))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin bắt buộc (Tên trò chơi, Tuổi tối thiểu, Thời gian mỗi lượt, Sức chứa tối đa)!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!KiemTraNgayHopLe()) return;
            try
            {
                ET_TroChoi et = layDuLieu();
                et.MaCode = txtMaCode.Text;

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
            catch (Exception ex)
            {
                MessageBox.Show("Đã xảy ra lỗi khi thêm trò chơi: " + ex.Message);
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

        private void cboLoaiTC_SelectedIndexChanged(object sender, EventArgs e)
        {
           txtMaKV.Text = BUS_TroChoi.Instance.layMaKVTheoLoaiTC(cboLoaiTC.Text);
        }

        private void txtTenTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Cho phép Backspace
            if (char.IsControl(e.KeyChar)) return;

            // Chỉ cho chữ và khoảng trắng
            if (!char.IsLetter(e.KeyChar) && e.KeyChar != ' ')
            {
                e.Handled = true;
                MessageBox.Show("Tên trò chơi không được nhập số và kí tự đặc biệt!",
                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Không cho khoảng trắng đầu hoặc 2 khoảng trắng liên tiếp
            if (e.KeyChar == ' ' && (txtTenTC.Text.Length == 0 || txtTenTC.Text.EndsWith(" ")))
            {
                MessageBox.Show("Tên trò chơi không được có khoảng trắng ở đầu và 2 khoảng trắng liên tiếp!",
                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                e.Handled = true;
            }
        }

        private void txtTuoi_TextChanged(object sender, EventArgs e)
        {
            if(flag) return;
            if (string.IsNullOrWhiteSpace(txtTuoi.Text))
            {
                errorProvider1.SetError(txtTuoi, "Số tuổi tối thiểu để được chơi không được để trống!");
                return;
            }
        }

        private void txtTuoi_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(! char.IsControl(e.KeyChar) && ! char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Số tuổi chỉ được nhập số và không có khoảng trắng",
                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txtThoiGianLuot_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Số tuổi chỉ được nhập số và không có khoảng trắng",
                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txtSucChua_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Số tuổi chỉ được nhập số và không có khoảng trắng",
                    "Lỗi nhập liệu", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txtTenTC_TextChanged(object sender, EventArgs e)
        {
            if(flag) return;
            if (string.IsNullOrWhiteSpace(txtTenTC.Text))
            {
                errorProvider1.SetError(txtTenTC, "Tên trò chơi không được để trống!");
                return;
            }
            else if (txtTenTC.Text.Trim().Length < 5)
            {
                errorProvider1.SetError(txtTenTC, "Tên trò chơi phải có ít nhất 5 kí tự");
            }
            else
            {
                errorProvider1.Clear();
            }
        }

        private void txtThoiGianLuot_TextChanged(object sender, EventArgs e)
        {
            if(flag) return;
            if (string.IsNullOrWhiteSpace(txtThoiGianLuot.Text))
            {
                errorProvider1.SetError(txtThoiGianLuot, "Thời gian mỗi lượt trò chơi không được để trống!");
                return;
            }
        }

        private void txtSucChua_TextChanged(object sender, EventArgs e)
        {
            if(flag) return;
            if (string.IsNullOrWhiteSpace(txtSucChua.Text))
            {
                errorProvider1.SetError(txtSucChua, "Sức chứa người chơi của mỗi trò chơi không được để trống!");
                return;
            }
        }

        private bool KiemTraNgayHopLe()
        {
            errorProvider1.Clear();
            if (dtpNgayTao.Value.Date > dtpNgayCapNhat.Value.Date)
            {
                errorProvider1.SetError(dtpNgayTao, "Ngày cập nhật phải lớn hơn hoặc bằng ngày tạo!");
                return false;
            }
            
            
                return true;
        }

        private void dtpNgayCapNhat_ValueChanged(object sender, EventArgs e)
        {
            KiemTraNgayHopLe();
        }

        private void dtpNgayTao_ValueChanged(object sender, EventArgs e)
        {
            KiemTraNgayHopLe();
        }
    }
}
