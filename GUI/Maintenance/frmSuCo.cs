using System;
using System.Collections.Generic;
using System.Windows.Forms;
using BUS;
using ET;

namespace GUI
{
    public partial class frmSuCo : Form
    {
        private ET_SuCo _currentSuCo;
        private ET_ThatLac _currentThatLac;

        public frmSuCo()
        {
            InitializeComponent();
            this.Load += FrmSuCo_Load;
            
            // Su Co Event
            gridSuCo.Click += GridSuCo_Click;
            btnThemSuCo.Click += BtnThemSuCo_Click;
            btnSuaSuCo.Click += BtnSuaSuCo_Click;
            btnXoaSuCo.Click += BtnXoaSuCo_Click;
            btnLamMoiSuCo.Click += BtnLamMoiSuCo_Click;

            // That Lac Events
            gridThatLac.Click += GridThatLac_Click;
            btnThemThatLac.Click += BtnThemThatLac_Click;
            btnSuaThatLac.Click += BtnSuaThatLac_Click;
            btnXoaThatLac.Click += BtnXoaThatLac_Click;
            btnLamMoiThatLac.Click += BtnLamMoiThatLac_Click;
            cboTrangThai.SelectedIndexChanged += CboTrangThai_SelectedIndexChanged;
        }

        private void FrmSuCo_Load(object sender, EventArgs e)
        {
            LoadDataSuCo();
            LoadDataThatLac();
            LoadComboboxes();
        }

        private void LoadComboboxes()
        {
            var loaiSuCoList = new Dictionary<string, string> {
                {"Thuong", "Khu vực thường"}, {"DuoiNuoc", "Đuối nước"}, 
                {"MatTre", "Lạc mất trẻ"}, {"DanhNhau", "Đánh nhau / Cãi vã"}, 
                {"ThietBi", "Hỏng thiết bị"}, {"Khac", "Khác"}
            };
            cboLoaiSuCo.DataSource = new BindingSource(loaiSuCoList, null);
            cboLoaiSuCo.DisplayMember = "Value";
            cboLoaiSuCo.ValueMember = "Key";

            // Combobox MucDo
            var mucDoList = new Dictionary<string, string> {
                {"Nhe", "Nhẹ"}, {"TrungBinh", "Trung bình"}, 
                {"NghiemTrong", "Nghiêm trọng"}, {"KhanCap", "Khẩn cấp"}
            };
            cboMucDo.DataSource = new BindingSource(mucDoList, null);
            cboMucDo.DisplayMember = "Value";
            cboMucDo.ValueMember = "Key";

            // Combobox Trang Thai That Lac
            var thatLacTrangThai = new Dictionary<string, string> {
                {"ChoNhan", "Chờ nhận"}, {"DaTra", "Đã trả khách"}, {"DaThanhLy", "Đã thanh lý"}
            };
            cboTrangThai.DataSource = new BindingSource(thatLacTrangThai, null);
            cboTrangThai.DisplayMember = "Value";
            cboTrangThai.ValueMember = "Key";

            var dsKhach = BUS_KhachHang.Instance.LoadDS();
            slkKhachHangSuCo.Properties.DataSource = dsKhach;
            slkKhachHangSuCo.Properties.DisplayMember = "HoTen";
            slkKhachHangSuCo.Properties.ValueMember = "Id";

            slkNguoiNhan.Properties.DataSource = dsKhach;
            slkNguoiNhan.Properties.DisplayMember = "HoTen";
            slkNguoiNhan.Properties.ValueMember = "Id";

            // Search lookup Nhan vien
            var dsNV = BUS_NhanVien.Instance.LoadDS();
            slkNVSuCo.Properties.DataSource = dsNV;
            slkNVSuCo.Properties.DisplayMember = "HoTen";
            slkNVSuCo.Properties.ValueMember = "Id";
        }

        #region TAB SỰ CỐ
        private void LoadDataSuCo()
        {
            gridSuCo.DataSource = BUS_SuCo.Instance.LoadDS();
            gridViewSuCo.PopulateColumns();
            // Hide some columns if needed
            if (gridViewSuCo.Columns["IdNhanVienXuLy"] != null) gridViewSuCo.Columns["IdNhanVienXuLy"].Visible = false;
            if (gridViewSuCo.Columns["IdKhachHang"] != null) gridViewSuCo.Columns["IdKhachHang"].Visible = false;
            if (gridViewSuCo.Columns["LoaiSuCo"] != null) gridViewSuCo.Columns["LoaiSuCo"].Visible = false;
            if (gridViewSuCo.Columns["MucDo"] != null) gridViewSuCo.Columns["MucDo"].Visible = false;
            if (gridViewSuCo.Columns["TenLoaiSuCo"] != null) gridViewSuCo.Columns["TenLoaiSuCo"].Caption = "Loại Sự Cố";
            if (gridViewSuCo.Columns["TenMucDo"] != null) gridViewSuCo.Columns["TenMucDo"].Caption = "Mức Độ";
        }

        private void GridSuCo_Click(object sender, EventArgs e)
        {
            if (gridViewSuCo.FocusedRowHandle >= 0)
            {
                var row = gridViewSuCo.GetRow(gridViewSuCo.FocusedRowHandle) as ET_SuCo;
                if (row != null)
                {
                    _currentSuCo = row;
                    slkKhachHangSuCo.EditValue = row.IdKhachHang;
                    slkNVSuCo.EditValue = row.IdNhanVienXuLy;
                    cboLoaiSuCo.SelectedValue = row.LoaiSuCo;
                    cboMucDo.SelectedValue = row.MucDo;
                    dtpThoiGianSuCo.DateTime = row.ThoiGian;
                    txtViTri.Text = row.ToaDoGps;
                    txtMoTaSuCo.Text = row.MoTa;
                }
            }
        }

        private void BtnLamMoiSuCo_Click(object sender, EventArgs e)
        {
            _currentSuCo = null;
            slkKhachHangSuCo.EditValue = null;
            slkNVSuCo.EditValue = null;
            cboLoaiSuCo.SelectedIndex = -1;
            cboMucDo.SelectedIndex = -1;
            dtpThoiGianSuCo.DateTime = DateTime.Now;
            txtViTri.Text = "";
            txtMoTaSuCo.Text = "";
        }

        private void BtnThemSuCo_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMoTaSuCo.Text)) { MessageBox.Show("Vui lòng nhập mô tả!"); return; }
            if (string.IsNullOrWhiteSpace(cboLoaiSuCo.Text)) { MessageBox.Show("Vui lòng chọn Loại sự cố!"); return; }
            if (string.IsNullOrWhiteSpace(cboMucDo.Text)) { MessageBox.Show("Vui lòng chọn Mức độ!"); return; }

            ET_SuCo et = new ET_SuCo
            {
                IdKhachHang = slkKhachHangSuCo.EditValue != null ? (int?)slkKhachHangSuCo.EditValue : null,
                IdNhanVienXuLy = slkNVSuCo.EditValue != null ? (int?)slkNVSuCo.EditValue : null,
                MoTa = txtMoTaSuCo.Text.Trim(),
                LoaiSuCo = cboLoaiSuCo.SelectedValue?.ToString(),
                MucDo = cboMucDo.SelectedValue?.ToString(),
                ToaDoGps = txtViTri.Text.Trim(),
                ThoiGian = dtpThoiGianSuCo.DateTime
            };

            if (BUS_SuCo.Instance.Them(et))
            {
                MessageBox.Show("Thêm thành công!");
                LoadDataSuCo();
                BtnLamMoiSuCo_Click(null, null);
            }
            else MessageBox.Show("Thêm thất bại!");
        }

        private void BtnSuaSuCo_Click(object sender, EventArgs e)
        {
            if (_currentSuCo == null) { MessageBox.Show("Vui lòng chọn sự cố để sửa!"); return; }
            
            _currentSuCo.IdKhachHang = slkKhachHangSuCo.EditValue != null ? (int?)slkKhachHangSuCo.EditValue : null;
            _currentSuCo.IdNhanVienXuLy = slkNVSuCo.EditValue != null ? (int?)slkNVSuCo.EditValue : null;
            _currentSuCo.MoTa = txtMoTaSuCo.Text.Trim();
            _currentSuCo.LoaiSuCo = cboLoaiSuCo.SelectedValue?.ToString();
            _currentSuCo.MucDo = cboMucDo.SelectedValue?.ToString();
            _currentSuCo.ToaDoGps = txtViTri.Text.Trim();
            _currentSuCo.ThoiGian = dtpThoiGianSuCo.DateTime;

            if (BUS_SuCo.Instance.Sua(_currentSuCo))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadDataSuCo();
            }
            else MessageBox.Show("Cập nhật thất bại!");
        }

        private void BtnXoaSuCo_Click(object sender, EventArgs e)
        {
            if (_currentSuCo == null) { MessageBox.Show("Vui lòng chọn sự cố để xóa!"); return; }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (BUS_SuCo.Instance.Xoa(_currentSuCo.Id))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadDataSuCo();
                    BtnLamMoiSuCo_Click(null, null);
                }
                else MessageBox.Show("Xóa thất bại!");
            }
        }
        #endregion

        #region TAB THẤT LẠC
        private void LoadDataThatLac()
        {
            gridThatLac.DataSource = BUS_ThatLac.Instance.LoadDS();
            gridViewThatLac.PopulateColumns();
            if (gridViewThatLac.Columns["IdKhachHangNhan"] != null) gridViewThatLac.Columns["IdKhachHangNhan"].Visible = false;
            if (gridViewThatLac.Columns["TrangThai"] != null) gridViewThatLac.Columns["TrangThai"].Visible = false;
            if (gridViewThatLac.Columns["TenTrangThai"] != null) gridViewThatLac.Columns["TenTrangThai"].Caption = "Trạng Thái";
        }

        private void CboTrangThai_SelectedIndexChanged(object sender, EventArgs e)
        {
            // Bắt buộc nhập người nhận nếu "DaTra"
            if (cboTrangThai.SelectedValue?.ToString() == "DaTra")
            {
                slkNguoiNhan.Enabled = true;
            }
            else
            {
                slkNguoiNhan.Enabled = false;
                slkNguoiNhan.EditValue = null;
            }
        }

        private void GridThatLac_Click(object sender, EventArgs e)
        {
            if (gridViewThatLac.FocusedRowHandle >= 0)
            {
                var row = gridViewThatLac.GetRow(gridViewThatLac.FocusedRowHandle) as ET_ThatLac;
                if (row != null)
                {
                    _currentThatLac = row;
                    txtMoTaDoVat.Text = row.MoTaDoVat;
                    txtNoiTimThay.Text = row.NoiTimThay;
                    cboTrangThai.SelectedValue = row.TrangThai;
                    dtpThoiGianThatLac.DateTime = row.ThoiGian;
                    slkNguoiNhan.EditValue = row.IdKhachHangNhan;
                }
            }
        }

        private void BtnLamMoiThatLac_Click(object sender, EventArgs e)
        {
            _currentThatLac = null;
            txtMoTaDoVat.Text = "";
            txtNoiTimThay.Text = "";
            cboTrangThai.SelectedIndex = -1;
            dtpThoiGianThatLac.DateTime = DateTime.Now;
            slkNguoiNhan.EditValue = null;
        }

        private void BtnThemThatLac_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMoTaDoVat.Text)) { MessageBox.Show("Vui lòng nhập mô tả!"); return; }
            if (cboTrangThai.SelectedValue?.ToString() == "DaTra" && slkNguoiNhan.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn (Tạo mới) khách hàng nhận đồ!"); return;
            }

            ET_ThatLac et = new ET_ThatLac
            {
                MoTaDoVat = txtMoTaDoVat.Text.Trim(),
                NoiTimThay = txtNoiTimThay.Text.Trim(),
                TrangThai = cboTrangThai.SelectedValue?.ToString() ?? "ChoNhan",
                ThoiGian = dtpThoiGianThatLac.DateTime,
                IdKhachHangNhan = slkNguoiNhan.EditValue != null ? (int?)slkNguoiNhan.EditValue : null
            };

            if (BUS_ThatLac.Instance.Them(et))
            {
                MessageBox.Show("Thêm thành công!");
                LoadDataThatLac();
                BtnLamMoiThatLac_Click(null, null);
            }
            else MessageBox.Show("Thêm thất bại!");
        }

        private void BtnSuaThatLac_Click(object sender, EventArgs e)
        {
            if (_currentThatLac == null) { MessageBox.Show("Vui lòng chọn dòng thất lạc để sửa!"); return; }
            if (cboTrangThai.SelectedValue?.ToString() == "DaTra" && slkNguoiNhan.EditValue == null)
            {
                MessageBox.Show("Vui lòng chọn khách hàng nhận đồ!"); return;
            }

            _currentThatLac.MoTaDoVat = txtMoTaDoVat.Text.Trim();
            _currentThatLac.NoiTimThay = txtNoiTimThay.Text.Trim();
            _currentThatLac.TrangThai = cboTrangThai.SelectedValue?.ToString();
            _currentThatLac.ThoiGian = dtpThoiGianThatLac.DateTime;
            _currentThatLac.IdKhachHangNhan = slkNguoiNhan.EditValue != null ? (int?)slkNguoiNhan.EditValue : null;

            if (BUS_ThatLac.Instance.Sua(_currentThatLac))
            {
                MessageBox.Show("Cập nhật thành công!");
                LoadDataThatLac();
            }
            else MessageBox.Show("Cập nhật thất bại!");
        }

        private void BtnXoaThatLac_Click(object sender, EventArgs e)
        {
            if (_currentThatLac == null) { MessageBox.Show("Vui lòng chọn dòng thất lạc để xóa!"); return; }
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (BUS_ThatLac.Instance.Xoa(_currentThatLac.Id))
                {
                    MessageBox.Show("Xóa thành công!");
                    LoadDataThatLac();
                    BtnLamMoiThatLac_Click(null, null);
                }
                else MessageBox.Show("Xóa thất bại!");
            }
        }
        #endregion
    }
}

