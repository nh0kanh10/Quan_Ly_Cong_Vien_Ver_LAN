using System;
using System.Drawing;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;
using DevExpress.XtraGrid.Views.Base;

namespace GUI
{
    public partial class frmNhanVien : Form, IBaseForm
    {
        private IBaseBUS<ET_NhanVien> _bus;
        private ET_NhanVien _currentEntity;

        public frmNhanVien()
        {
            InitializeComponent();
            _bus = BUS_NhanVien.Instance;
            
            InitIcons();
            ApplyStyles();
            ApplyPermissions();
            
            InitIcons();
            ApplyStyles();
            ApplyPermissions();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void slkChucVu_EditValueChanged(object sender, EventArgs e)
        {
            if (slkAccountRole.EditValue != slkChucVu.EditValue)
                slkAccountRole.EditValue = slkChucVu.EditValue;
        }

        private void slkAccountRole_EditValueChanged(object sender, EventArgs e)
        {
            if (slkChucVu.EditValue != slkAccountRole.EditValue)
                slkChucVu.EditValue = slkAccountRole.EditValue;
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            // Nếu không có quyền xem thì không cho thao tác mở form.
            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_STAFF"))
            {
                this.Enabled = false;
                return;
            }

            bool canManage = BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_STAFF");
            btnThem.Enabled = canManage;
            btnSua.Enabled = canManage;
            btnXoa.Enabled = canManage;
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);
        }

        public void InitIcons()
        {
            btnThem.Image = IconHelper.GetBitmap(IconChar.Plus, Color.White, 16);
            btnSua.Image = IconHelper.GetBitmap(IconChar.PenToSquare, Color.White, 16);
            btnXoa.Image = IconHelper.GetBitmap(IconChar.TrashCan, Color.White, 16);
            btnLamMoi.Image = IconHelper.GetBitmap(IconChar.ArrowsRotate, Color.White, 16);
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            loadComboBoxes();
            LoadData();
        }

        private void loadComboBoxes()
        {
            cboGioiTinh.DataSource = new List<string> { "Nam", "Nữ" };
            cboTrangThai.DataSource = new List<string> { "Đang làm việc", "Đã nghỉ việc" };

            var dsKhuVuc = BUS_KhuVuc.Instance.LoadDS();
            slkKhuVuc.Properties.DataSource = dsKhuVuc;
            slkKhuVuc.Properties.DisplayMember = "TenKhuVuc";
            slkKhuVuc.Properties.ValueMember = "Id";
            ThemeManager.StyleSearchLookUpEdit(slkKhuVuc, new[] { "TenKhuVuc" }, new[] { "Chi nhánh/Khu vực" });

            var listRoles = BUS_VaiTro.Instance.LoadDS()
                .Select(x => new { Id = x.Id, Ten = x.TenVaiTro })
                .ToList();
            slkChucVu.Properties.DataSource = listRoles;
            slkChucVu.Properties.DisplayMember = "Ten";
            slkChucVu.Properties.ValueMember = "Id";
            ThemeManager.StyleSearchLookUpEdit(slkChucVu, new[] { "Ten" }, new[] { "Chức vụ" });

            slkAccountRole.Properties.DataSource = listRoles;
            slkAccountRole.Properties.DisplayMember = "Ten";
            slkAccountRole.Properties.ValueMember = "Id";
            ThemeManager.StyleSearchLookUpEdit(slkAccountRole, new[] { "Ten" }, new[] { "Vai trò hệ thống" });

            slkLocChucVu.Properties.DataSource = listRoles;
            slkLocChucVu.Properties.DisplayMember = "Ten";
            slkLocChucVu.Properties.ValueMember = "Id";
            ThemeManager.StyleSearchLookUpEdit(slkLocChucVu, new[] { "Ten" }, new[] { "Lọc Chức vụ" });
        }

        public void LoadData()
        {
            if (_bus == null) return;
            gridControl.DataSource = new System.ComponentModel.BindingList<ET.ET_NhanVien>(_bus.LoadDS());
            gridView.PopulateColumns();
            FormatGrid();
        }

        private void FormatGrid()
        {
            var view = gridView;
            if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;
            if (view.Columns["MaCode"] != null) view.Columns["MaCode"].Caption = "Mã NV";
            if (view.Columns["HoTen"] != null) view.Columns["HoTen"].Caption = "Họ Tên";
            if (view.Columns["DienThoai"] != null) view.Columns["DienThoai"].Caption = "SĐT";
            if (view.Columns["ChucVu"] != null) view.Columns["ChucVu"].Caption = "Chức vụ";
            
            string[] hidden = { "MatKhau", "HinhAnh", "CreatedAt", "UpdatedAt", "CreatedBy", "IsDeleted" };
            foreach (var col in hidden) if (view.Columns[col] != null) view.Columns[col].Visible = false;
            
            view.OptionsView.ColumnAutoWidth = true;
        }

        private void OnFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gridView.FocusedRowHandle >= 0)
            {
                _currentEntity = gridView.GetFocusedRow() as ET_NhanVien;
                if (_currentEntity != null) ShowEntityToUI(_currentEntity);
            }
        }

        private void ShowEntityToUI(ET_NhanVien row)
        {
            txtMaCode.Text = row.MaCode;
            txtHoTen.Text = row.HoTen;
            cboGioiTinh.Text = row.GioiTinh;
            dtpNgaySinh.DateTime = row.NgaySinh ?? DateTime.Today;
            txtCCCD.Text = row.Cccd;
            txtSDT.Text = row.DienThoai;
            txtEmail.Text = row.Email;
            txtDiaChi.Text = row.DiaChi;
            slkChucVu.EditValue = row.IdVaiTro;
            slkKhuVuc.EditValue = row.IdKhuVuc;
            dtpNgayVaoLam.DateTime = row.NgayVaoLam ?? DateTime.Today;
            cboTrangThai.Text = row.TrangThai;

            // Update Account Tab
            txtUsername.Text = row.MaCode;
            txtPassword.Text = row.MatKhau;
            slkAccountRole.EditValue = row.IdVaiTro;

            if (!string.IsNullOrEmpty(row.HinhAnh))
            {
                picAvatar.ImageLocation = row.HinhAnh;
                picAvatar.Tag = row.HinhAnh;
            }
            else
            {
                picAvatar.Image = null;
                picAvatar.Tag = null;
            }
        }

        private ET_NhanVien GetEntityFromUI()
        {
            return new ET_NhanVien
            {
                Id = _currentEntity?.Id ?? 0,
                MaCode = txtMaCode.Text,
                HoTen = txtHoTen.Text.Trim(),
                GioiTinh = cboGioiTinh.Text,
                NgaySinh = dtpNgaySinh.DateTime.Date,
                Cccd = txtCCCD.Text.Trim(),
                DienThoai = txtSDT.Text.Trim(),
                Email = txtEmail.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim(),
                IdVaiTro = Convert.ToInt32(slkChucVu.EditValue),
                IdKhuVuc = Convert.ToInt32(slkKhuVuc.EditValue),
                NgayVaoLam = dtpNgayVaoLam.DateTime.Date,
                TrangThai = cboTrangThai.Text,
                MatKhau = txtPassword.Text.Trim(),
                HinhAnh = picAvatar.Tag?.ToString()
            };
        }

        private void ClearUI()
        {
            txtMaCode.Clear();
            txtHoTen.Clear();
            cboGioiTinh.SelectedIndex = 0;
            dtpNgaySinh.DateTime = DateTime.Today;
            txtCCCD.Clear();
            txtSDT.Clear();
            txtEmail.Clear();
            txtDiaChi.Clear();
            slkChucVu.EditValue = null;
            slkKhuVuc.EditValue = null;
            dtpNgayVaoLam.DateTime = DateTime.Today;
            cboTrangThai.SelectedIndex = 0;
            txtUsername.Clear();
            txtPassword.Clear();
            slkAccountRole.EditValue = null;
            picAvatar.Image = null;
            picAvatar.Tag = null;
        }

        private bool ValidateInput()
        {
            // Họ tên
            if (string.IsNullOrWhiteSpace(txtHoTen.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập họ tên nhân viên!", "Thông báo");
                txtHoTen.Focus();
                return false;
            }
            if (txtHoTen.Text.Trim().Length < 2)
            {
                TDCMessageBox.Show("Họ tên phải có ít nhất 2 ký tự!", "Thông báo");
                txtHoTen.Focus();
                return false;
            }

            // Ngày sinh — tuổi lao động: 18-65
            int tuoi = DateTime.Today.Year - dtpNgaySinh.DateTime.Year;
            if (dtpNgaySinh.DateTime.Date > DateTime.Today.AddYears(-tuoi)) tuoi--;
            if (tuoi < 18 || tuoi > 65)
            {
                TDCMessageBox.Show($"Tuổi nhân viên phải từ 18 đến 65! (Hiện tại: {tuoi} tuổi)", "Thông báo");
                return false;
            }

            // CCCD — 12 chữ số
            if (!string.IsNullOrWhiteSpace(txtCCCD.Text) &&
                !System.Text.RegularExpressions.Regex.IsMatch(txtCCCD.Text.Trim(), @"^\d{12}$"))
            {
                TDCMessageBox.Show("CCCD phải gồm đúng 12 chữ số!", "Thông báo");
                txtCCCD.Focus();
                return false;
            }

            // SĐT — 10 chữ số
            if (string.IsNullOrWhiteSpace(txtSDT.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập số điện thoại!", "Thông báo");
                txtSDT.Focus();
                return false;
            }
            if (!System.Text.RegularExpressions.Regex.IsMatch(txtSDT.Text.Trim(), @"^\d{10}$"))
            {
                TDCMessageBox.Show("Số điện thoại phải gồm đúng 10 chữ số!", "Thông báo");
                txtSDT.Focus();
                return false;
            }

            // Email format (nếu có nhập)
            if (!string.IsNullOrWhiteSpace(txtEmail.Text) &&
                !System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text.Trim(), @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                TDCMessageBox.Show("Định dạng Email không hợp lệ!", "Thông báo");
                txtEmail.Focus();
                return false;
            }

            // Chức vụ + Khu vực bắt buộc
            if (slkChucVu.EditValue == null)
            {
                TDCMessageBox.Show("Vui lòng chọn chức vụ!", "Thông báo");
                return false;
            }
            if (slkKhuVuc.EditValue == null)
            {
                TDCMessageBox.Show("Vui lòng chọn khu vực làm việc!", "Thông báo");
                return false;
            }

            // Ngày vào làm — không được tương lai
            if (dtpNgayVaoLam.DateTime.Date > DateTime.Today)
            {
                TDCMessageBox.Show("Ngày vào làm không thể là ngày tương lai!", "Thông báo");
                return false;
            }

            // Mật khẩu — tối thiểu 4 ký tự nếu tạo mới
            if (_currentEntity == null && !string.IsNullOrEmpty(txtPassword.Text) && txtPassword.Text.Trim().Length < 4)
            {
                TDCMessageBox.Show("Mật khẩu phải có ít nhất 4 ký tự!", "Thông báo");
                txtPassword.Focus();
                return false;
            }

            return true;
        }

        private void slkLocChucVu_EditValueChanged(object sender, EventArgs e)
        {
            OnSearch();
        }

        private void OnSearch()
        {
            // Dùng DevExpress FindPanel API thay vì BUS custom
            gridView.ApplyFindFilter(txtTimKiem.Text.Trim());
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Images|*.jpg;*.png;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picAvatar.ImageLocation = ofd.FileName;
                    picAvatar.Tag = ofd.FileName;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            var et = GetEntityFromUI();
            var res = _bus.Them(et);
            HandleResult(res, "Thêm mới thành công!");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_currentEntity == null) return;
            if (!ValidateInput()) return;
            var et = GetEntityFromUI();
            var res = _bus.Sua(et);
            HandleResult(res, "Cập nhật thành công!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_currentEntity == null) return;
            if (TDCMessageBox.Show("Bạn có chắc chắn muốn xóa?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var idProp = typeof(ET_NhanVien).GetProperty("Id");
                if (idProp != null)
                {
                    int id = (int)idProp.GetValue(_currentEntity);
                    var res = _bus.Xoa(id);
                    HandleResult(res, "Xóa thành công!");
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadData();
            gridView.ClearFindFilter();
            _currentEntity = null;
            ClearUI();
        }

        private void HandleResult(ResponseResult res, string successMsg)
        {
            if (res.IsSuccess)
            {
                TDCMessageBox.Show(successMsg, "Thông báo");
                btnLamMoi_Click(null, null);
            }
            else TDCMessageBox.Show(res.ErrorMessage, "Lỗi");
        }
    }
}




