using BUS;
using DAL;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using ET;

namespace WindowsFormsApp1
{
    public partial class frmLoaiVe : Form
    {
        public frmLoaiVe()
        {
            InitializeComponent();
        }

        // ============== LOAD & REFRESH ==============
        private void loadDS()
        {
            dgvLoaiVe.DataSource = BUS_LoaiVe.Instance.LoadDS();
        }

        private void loadComboBoxes()
        {
            cboTrangThai.DataSource = new List<string> { "Hoạt động", "Ngừng bán" };

            cboDoiTuong.DataSource = new List<string>
            {
                "Người lớn", "Trẻ em", "Người cao tuổi", "Sinh viên", "Tất cả"
            };
        }

        private void loadComboVeCon()
        {
            var dsVeThuong = DAL_ChiTietCombo.Instance.LoadDSVeThuong();
            cboVeConChon.DataSource = dsVeThuong;
            cboVeConChon.DisplayMember = "TenLoaiVe";
            cboVeConChon.ValueMember = "MaLoaiVe";
        }

        private void StyleDataGridView(DataGridView dgv)
        {
            dgv.BorderStyle = BorderStyle.None;
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(238, 239, 249);
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.DefaultCellStyle.SelectionBackColor = Color.DarkTurquoise;
            dgv.DefaultCellStyle.SelectionForeColor = Color.WhiteSmoke;
            dgv.BackgroundColor = Color.White;

            dgv.EnableHeadersVisualStyles = false;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(20, 25, 72);
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 35;
        }

        private void frmLoaiVe_Load(object sender, EventArgs e)
        {
            StyleDataGridView(dgvLoaiVe);
            StyleDataGridView(dgvVeCon);
            loadComboBoxes();
            loadDS();
            loadComboVeCon();
            txtMaCode.Text = BUS_LoaiVe.Instance.LayMaCodeTiepTheo();
        }

        private void dgvLoaiVe_Click(object sender, EventArgs e)
        {
            if (dgvLoaiVe.CurrentRow == null) return;
            int dong = dgvLoaiVe.CurrentCell.RowIndex;

            txtMaLoaiVe.Text = dgvLoaiVe.Rows[dong].Cells["MaLoaiVe"].Value.ToString();
            txtMaCode.Text = dgvLoaiVe.Rows[dong].Cells["MaCode"].Value.ToString();
            txtTenLoaiVe.Text = dgvLoaiVe.Rows[dong].Cells["TenLoaiVe"].Value.ToString();
            txtGiaVe.Text = dgvLoaiVe.Rows[dong].Cells["GiaVe"].Value.ToString();

            var giaCuoiTuan = dgvLoaiVe.Rows[dong].Cells["GiaCuoiTuan"].Value;
            txtGiaCuoiTuan.Text = (giaCuoiTuan != null && giaCuoiTuan != DBNull.Value)
                ? giaCuoiTuan.ToString() : "";

            cboDoiTuong.Text = dgvLoaiVe.Rows[dong].Cells["DoiTuong"].Value.ToString();
            chkLaCombo.Checked = Convert.ToBoolean(dgvLoaiVe.Rows[dong].Cells["LaCombo"].Value);
            cboTrangThai.Text = dgvLoaiVe.Rows[dong].Cells["TrangThai"].Value.ToString();

            if (DateTime.TryParse(dgvLoaiVe.Rows[dong].Cells["NgayTao"].Value.ToString(), out DateTime ngayTao))
                dtpNgayTao.Value = ngayTao;
            if (DateTime.TryParse(dgvLoaiVe.Rows[dong].Cells["NgayCapNhat"].Value?.ToString(), out DateTime ngayCapNhat))
                dtpNgayCapNhat.Value = ngayCapNhat;

            if (chkLaCombo.Checked)
            {
                int maLoaiVe = Convert.ToInt32(dgvLoaiVe.Rows[dong].Cells["MaLoaiVe"].Value);
                loadDSVeCon(maLoaiVe);
            }
        }

        private void chkLaCombo_CheckedChanged(object sender, EventArgs e)
        {
            pnlCombo.Visible = chkLaCombo.Checked;

            if (chkLaCombo.Checked)
            {
                this.ClientSize = new System.Drawing.Size(1400, 1050);
                loadComboVeCon();
            }
            else
            {
                this.ClientSize = new System.Drawing.Size(1400, 800);
            }
        }

        private void loadDSVeCon(int maLoaiVeCha)
        {
            dgvVeCon.DataSource = DAL_ChiTietCombo.Instance.LoadVeConTheoCombo(maLoaiVeCha);
        }

        private void btnThemVeCon_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaLoaiVe.Text))
            {
                MessageBox.Show("Vui lòng lưu loại vé combo trước khi thêm vé con!\n(Nhấn nút Thêm trước)",
                    "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cboVeConChon.SelectedValue == null)
            {
                MessageBox.Show("Vui lòng chọn vé con!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maLoaiVeCha = Convert.ToInt32(txtMaLoaiVe.Text);
            int maLoaiVeCon = Convert.ToInt32(cboVeConChon.SelectedValue);
            int soLuot = (int)nudSoLuot.Value;

            if (soLuot == 0)
            {
                MessageBox.Show("Số lượt phải khác 0! (-1 = vô hạn)", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Không cho thêm chính mình làm vé con
            if (maLoaiVeCha == maLoaiVeCon)
            {
                MessageBox.Show("Không thể thêm chính loại vé này làm vé con!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool kq = DAL_ChiTietCombo.Instance.ThemVeCon(maLoaiVeCha, maLoaiVeCon, soLuot);
            if (kq)
            {
                loadDSVeCon(maLoaiVeCha);
            }
            else
            {
                MessageBox.Show("Vé con này đã tồn tại trong combo!",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void btnXoaVeCon_Click(object sender, EventArgs e)
        {
            if (dgvVeCon.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn vé con cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            int maChiTiet = Convert.ToInt32(dgvVeCon.CurrentRow.Cells["MaChiTietCombo"].Value);
            int maLoaiVeCha = Convert.ToInt32(txtMaLoaiVe.Text);

            DAL_ChiTietCombo.Instance.XoaVeCon(maChiTiet);
            loadDSVeCon(maLoaiVeCha);
        }

        private void lamMoi()
        {
            txtMaLoaiVe.Clear();
            txtMaCode.Text = BUS_LoaiVe.Instance.LayMaCodeTiepTheo();
            txtTenLoaiVe.Clear();
            txtGiaVe.Clear();
            txtGiaCuoiTuan.Clear();
            cboDoiTuong.SelectedIndex = -1;
            cboTrangThai.SelectedIndex = -1;
            chkLaCombo.Checked = false;
            dtpNgayTao.Value = DateTime.Now;
            dtpNgayCapNhat.Value = DateTime.Now;
            dgvVeCon.DataSource = null;
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            lamMoi();
        }

        private ET_LoaiVe DocDuLieuTuForm()
        {
            try
            {
                ET_LoaiVe et = new ET_LoaiVe
                {
                    MaCode = txtMaCode.Text,
                    TenLoaiVe = txtTenLoaiVe.Text,
                    GiaVe = string.IsNullOrWhiteSpace(txtGiaVe.Text) ? 0 : decimal.Parse(txtGiaVe.Text),
                    GiaCuoiTuan = string.IsNullOrWhiteSpace(txtGiaCuoiTuan.Text)
                        ? (decimal?)null : decimal.Parse(txtGiaCuoiTuan.Text),
                    DoiTuong = cboDoiTuong.Text,
                    LaCombo = chkLaCombo.Checked,
                    TrangThai = cboTrangThai.Text,
                };
                return et;
            }
            catch (FormatException)
            {
                MessageBox.Show("Giá vé phải là số hợp lệ.", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ET_LoaiVe et = DocDuLieuTuForm();
            if (et == null) return;

            string loi = BUS_LoaiVe.Instance.ValidateLoaiVe(et, laThem: true);
            if (!string.IsNullOrEmpty(loi))
            {
                MessageBox.Show(loi, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (BUS_LoaiVe.Instance.ThemLoaiVe(et))
            {
                MessageBox.Show("Thêm loại vé thành công!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                loadDS();
                // Sau khi thêm, nếu là combo → giữ form để user thêm vé con
                if (chkLaCombo.Checked)
                {
                    // Tìm lại record vừa thêm để lấy MaLoaiVe
                    var dsNew = BUS_LoaiVe.Instance.LoadDS();
                    var last = dsNew[dsNew.Count - 1];
                    txtMaLoaiVe.Text = last.MaLoaiVe.ToString();
                    txtMaCode.Text = last.MaCode;
                    loadComboVeCon();
                    MessageBox.Show("Giờ bạn có thể thêm vé con vào combo!",
                        "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    lamMoi();
                }
            }
            else
            {
                MessageBox.Show("Thêm loại vé thất bại!", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCode.Text) || txtMaCode.Text == BUS_LoaiVe.Instance.LayMaCodeTiepTheo())
            {
                MessageBox.Show("Vui lòng chọn loại vé cần sửa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ET_LoaiVe et = DocDuLieuTuForm();
            if (et == null) return;

            string loi = BUS_LoaiVe.Instance.ValidateLoaiVe(et, laThem: false);
            if (!string.IsNullOrEmpty(loi))
            {
                MessageBox.Show(loi, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            DialogResult r = MessageBox.Show($"Sửa thông tin loại vé {et.MaCode}?",
                "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (r == DialogResult.Yes)
            {
                if (BUS_LoaiVe.Instance.SuaLoaiVe(et))
                    MessageBox.Show("Sửa thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Sửa thất bại!", "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                loadDS();
                lamMoi();
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (dgvLoaiVe.CurrentRow == null)
            {
                MessageBox.Show("Vui lòng chọn loại vé cần xóa!", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string maCode = dgvLoaiVe.CurrentRow.Cells["MaCode"].Value.ToString();
            int maLoaiVe = Convert.ToInt32(dgvLoaiVe.CurrentRow.Cells["MaLoaiVe"].Value);

            DialogResult r = MessageBox.Show($"Xóa loại vé {maCode}?\n(Sẽ xóa cả chi tiết combo nếu có)",
                "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (r == DialogResult.Yes)
            {
                // Xóa chi tiết combo trước (nếu là combo cha)
                DAL_ChiTietCombo.Instance.XoaTatCaVeCon(maLoaiVe);

                if (BUS_LoaiVe.Instance.XoaLoaiVe(maCode))
                    MessageBox.Show("Xóa thành công!", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                else
                    MessageBox.Show("Xóa thất bại! Loại vé có thể đang được sử dụng.",
                        "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                loadDS();
                lamMoi();
            }
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            if (string.IsNullOrEmpty(tuKhoa))
                dgvLoaiVe.DataSource = BUS_LoaiVe.Instance.LoadDS();
            else
                dgvLoaiVe.DataSource = BUS_LoaiVe.Instance.TimKiem(tuKhoa);
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            DialogResult a = MessageBox.Show("Bạn đang thoát màn hình?", "THÔNG BÁO",
                MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (a == DialogResult.Yes)
                this.Close();
        }
    }
}
