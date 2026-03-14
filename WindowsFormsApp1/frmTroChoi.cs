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

        private void loadDS()
        {
            var ds = BUS_TroChoi.Instance.loadDS();
            dgvTroChoi.DataSource = ds;
            dgvTroChoi.Columns["MaTroChoi"].DisplayIndex = 0;
            dgvTroChoi.Columns["MaCode"].DisplayIndex = 1;
            dgvTroChoi.Columns["TenTroChoi"].DisplayIndex = 2;
            dgvTroChoi.Columns["LoaiTroChoi"].DisplayIndex = 3;
            dgvTroChoi.Columns["TrangThai"].DisplayIndex = 6;
        }

        private void loadComboBoxes()
        {
            cboTrangThai.DataSource = new List<string>
            {
                "Hoạt động", "Bảo trì", "Ngừng hoạt động"
            };

            cboLoaiTC.DataSource = new List<string>
            {
                "Cảm giác mạnh", "Trẻ em", "Phiêu lưu", "Nước",
                "Gia đình", "Thể thao", "Tham quan", "Trong nhà"
            };

            var dsKhuVuc = BUS_KhuVuc.Instance.LoadDSKhuVucHoatDong();
            cboKhuVuc.DisplayMember = "TenKhuVuc";
            cboKhuVuc.ValueMember = "MaKhuVuc";
            cboKhuVuc.DataSource = dsKhuVuc;

            // Khu vực cho bộ lọc — tất cả + option "Tất cả"
            var dsKhuVucLoc = BUS_KhuVuc.Instance.LoadDSKhuVuc();
            var locList = new List<ET.ET_KhuVuc>();
            locList.Add(new ET.ET_KhuVuc { MaKhuVuc = 0, TenKhuVuc = "-- Tất cả --" });
            foreach (var kv in dsKhuVucLoc)
            {
                locList.Add(new ET.ET_KhuVuc { MaKhuVuc = kv.MaKhuVuc, TenKhuVuc = kv.TenKhuVuc });
            }
            cboLocKhuVuc.DisplayMember = "TenKhuVuc";
            cboLocKhuVuc.ValueMember = "MaKhuVuc";
            cboLocKhuVuc.DataSource = locList;

            // Chiều cao mặc định
            cboChieuCao.SelectedIndex = 0;
        }

        private void frmTroChoi_Load(object sender, EventArgs e)
        {
            loadComboBoxes();
            loadDS();
            txtMaCode.Text = BUS_TroChoi.Instance.layMaCodeTiepTheo();
            txtMaTC.Text = BUS_TroChoi.Instance.layMaTroChoiTiepTheo().ToString();
        }

        private void dgvTroChoi_Click(object sender, EventArgs e)
        {
            if (dgvTroChoi.CurrentRow == null) return;
            int dong = dgvTroChoi.CurrentCell.RowIndex;

            txtMaTC.Text = dgvTroChoi.Rows[dong].Cells["MaTroChoi"].Value.ToString();
            txtMaCode.Text = dgvTroChoi.Rows[dong].Cells["MaCode"].Value.ToString();
            txtTenTC.Text = dgvTroChoi.Rows[dong].Cells["TenTroChoi"].Value.ToString();

            cboLoaiTC.Text = dgvTroChoi.Rows[dong].Cells["LoaiTroChoi"].Value.ToString();

            // Set khu vực bằng SelectedValue (MaKhuVuc)
            int maKV = Convert.ToInt32(dgvTroChoi.Rows[dong].Cells["MaKhuVuc"].Value);
            cboKhuVuc.SelectedValue = maKV;

            txtSucChua.Text = dgvTroChoi.Rows[dong].Cells["SucChua"].Value.ToString();
            cboTrangThai.Text = dgvTroChoi.Rows[dong].Cells["TrangThai"].Value.ToString();
            txtTuoi.Text = dgvTroChoi.Rows[dong].Cells["TuoiToiThieu"].Value.ToString();
            cboChieuCao.Text = dgvTroChoi.Rows[dong].Cells["ChieuCaoToiThieu"].Value.ToString();
            txtThoiGianLuot.Text = dgvTroChoi.Rows[dong].Cells["ThoiGianLuot"].Value.ToString();
            richTextBox1.Text = dgvTroChoi.Rows[dong].Cells["MoTa"].Value?.ToString() ?? "";

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
            txtMaTC.Clear();
            txtMaCode.Text = BUS_TroChoi.Instance.layMaCodeTiepTheo();
            txtMaTC.Text = BUS_TroChoi.Instance.layMaTroChoiTiepTheo().ToString();
            txtTenTC.Clear();
            cboKhuVuc.SelectedIndex = cboKhuVuc.Items.Count > 0 ? 0 : -1;
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

       
        private ET_TroChoi DocDuLieuTuForm()
        {
            try
            {
                ET_TroChoi et = new ET_TroChoi
                {
                    MaCode = txtMaCode.Text,
                    TenTroChoi = txtTenTC.Text,
                    MaKhuVuc = cboKhuVuc.SelectedValue != null ? Convert.ToInt32(cboKhuVuc.SelectedValue) : 0,
                    LoaiTroChoi = cboLoaiTC.Text,
                    SucChua = string.IsNullOrWhiteSpace(txtSucChua.Text) ? 0 : Convert.ToInt32(txtSucChua.Text),
                    TuoiToiThieu = string.IsNullOrWhiteSpace(txtTuoi.Text) ? 0 : Convert.ToInt32(txtTuoi.Text),
                    ChieuCaoToiThieu = string.IsNullOrWhiteSpace(cboChieuCao.Text) ? 0 : Convert.ToInt32(cboChieuCao.Text),
                    ThoiGianLuot = string.IsNullOrWhiteSpace(txtThoiGianLuot.Text) ? 0 : Convert.ToInt32(txtThoiGianLuot.Text),
                    MoTa = richTextBox1.Text,
                    TrangThai = cboTrangThai.Text,
                };
                return et;
            }
            catch (FormatException)
            {
                MessageBox.Show("Dữ liệu số không hợp lệ. Vui lòng kiểm tra Sức chứa, Tuổi, Thời gian lượt.",
                    "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ET_TroChoi et = DocDuLieuTuForm();
            if (et == null) return;

            string loiValidate = BUS_TroChoi.Instance.ValidateTroChoi(et, laThem: true);
            if (!string.IsNullOrEmpty(loiValidate))
            {
                MessageBox.Show(loiValidate, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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
                    MessageBox.Show("Xóa trò chơi thất bại! Trò chơi có thể đang được sử dụng.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                loadDS();
                lamMoi();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtMaCode.Text) || txtMaCode.Text == BUS_TroChoi.Instance.layMaCodeTiepTheo())
            {
                MessageBox.Show("Vui lòng chọn trò chơi cần sửa từ danh sách!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            ET_TroChoi et = DocDuLieuTuForm();
            if (et == null) return;

            string loiValidate = BUS_TroChoi.Instance.ValidateTroChoi(et, laThem: false);
            if (!string.IsNullOrEmpty(loiValidate))
            {
                MessageBox.Show(loiValidate, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string tuKhoa = txtTimKiem.Text.Trim();
            int maKhuVucLoc = 0;
            if (cboLocKhuVuc.SelectedValue != null)
            {
                if (cboLocKhuVuc.SelectedValue is int valInt)
                    maKhuVucLoc = valInt;
                else
                {
                    // Nếu dữ liệu chưa bind xong, SelectedValue có thể là obj ET_KhuVuc
                    if (cboLocKhuVuc.SelectedValue is ET.ET_KhuVuc kv)
                        maKhuVucLoc = kv.MaKhuVuc;
                }
            }

            List<ET_TroChoi> ketQua;

            if (string.IsNullOrEmpty(tuKhoa) && maKhuVucLoc == 0)
            {
                // Không có bộ lọc → load tất cả
                ketQua = BUS_TroChoi.Instance.loadDS();
            }
            else if (!string.IsNullOrEmpty(tuKhoa) && maKhuVucLoc > 0)
            {
                // Có cả tìm kiếm và lọc khu vực
                ketQua = BUS_TroChoi.Instance.timKiemTheoKhuVuc(tuKhoa, maKhuVucLoc);
            }
            else if (!string.IsNullOrEmpty(tuKhoa))
            {
                // Chỉ có tìm kiếm
                ketQua = BUS_TroChoi.Instance.timKiem(tuKhoa);
            }
            else
            {
                // Chỉ lọc khu vực
                ketQua = BUS_TroChoi.Instance.loadDSTheoKhuVuc(maKhuVucLoc);
            }

            dgvTroChoi.DataSource = ketQua;
        }

        private void cboLocKhuVuc_SelectedIndexChanged(object sender, EventArgs e)
        {
            btnTimKiem_Click(sender, e);
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
