using System;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;
using DevExpress.XtraGrid.Views.Base;

namespace GUI
{
    public partial class frmNhaCungCap : Form, IBaseForm
    {
        private IBaseBUS<ET_NhaCungCap> _bus;
        private ET_NhaCungCap _currentEntity;

        public frmNhaCungCap()
        {
            InitializeComponent();
            _bus = BUS_NhaCungCap.Instance;
            this.gbDanhSach.Text = "Danh Sách Nhà Cung Cấp";
            
            InitIcons();
            ApplyStyles();
            ApplyPermissions();
        }

        private void frmNhaCungCap_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            gridView.ApplyFindFilter(txtTimKiem.Text.Trim());
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            // Dùng chung quyền INVENTORY hoặc quyền riêng SUPPLIER
            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_INVENTORY"))
            {
                this.Enabled = false;
                return;
            }

            bool canManage = BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_INVENTORY");
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

        public void LoadData()
        {
            if (_bus == null) return;
            gridControl.DataSource = new System.ComponentModel.BindingList<ET.ET_NhaCungCap>(_bus.LoadDS());
            gridView.PopulateColumns();
            FormatGrid();
        }

        private void FormatGrid()
        {
            if (gridView.Columns.Count > 0)
            {
                string[] hide = { "Id", "CreatedAt", "UpdatedAt", "CreatedBy", "IsDeleted" };
                foreach (var col in hide)
                {
                    if (gridView.Columns[col] != null) gridView.Columns[col].Visible = false;
                }

                if (gridView.Columns["Ten"] != null) { gridView.Columns["Ten"].Caption = "Tên NCC"; gridView.Columns["Ten"].Width = 200; }
                if (gridView.Columns["MaSoThue"] != null) { gridView.Columns["MaSoThue"].Caption = "Mã Số Thuế"; gridView.Columns["MaSoThue"].Width = 120; }
                if (gridView.Columns["DienThoai"] != null) { gridView.Columns["DienThoai"].Caption = "Điện Thoại"; gridView.Columns["DienThoai"].Width = 120; }
                if (gridView.Columns["NguoiLienHe"] != null) { gridView.Columns["NguoiLienHe"].Caption = "Người Liên Hệ"; gridView.Columns["NguoiLienHe"].Width = 150; }
                if (gridView.Columns["DiaChi"] != null) { gridView.Columns["DiaChi"].Caption = "Địa Chỉ"; gridView.Columns["DiaChi"].Width = 250; }
            }
        }

        private void OnFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gridView.FocusedRowHandle >= 0)
            {
                _currentEntity = gridView.GetFocusedRow() as ET_NhaCungCap;
                if (_currentEntity != null) ShowEntityToUI(_currentEntity);
            }
        }

        private void ShowEntityToUI(ET_NhaCungCap row)
        {
            txtTen.Text = row.Ten;
            txtMaSoThue.Text = row.MaSoThue;
            txtDienThoai.Text = row.DienThoai;
            txtNguoiLienHe.Text = row.NguoiLienHe;
            txtDiaChi.Text = row.DiaChi;
        }

        private ET_NhaCungCap GetEntityFromUI()
        {
            return new ET_NhaCungCap
            {
                Id = _currentEntity?.Id ?? 0,
                Ten = txtTen.Text.Trim(),
                MaSoThue = txtMaSoThue.Text.Trim(),
                DienThoai = txtDienThoai.Text.Trim(),
                NguoiLienHe = txtNguoiLienHe.Text.Trim(),
                DiaChi = txtDiaChi.Text.Trim()
            };
        }

        private void ClearUI()
        {
            txtTen.Clear();
            txtMaSoThue.Clear();
            txtDienThoai.Clear();
            txtNguoiLienHe.Clear();
            txtDiaChi.Clear();
            txtTen.Focus();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập tên nhà cung cấp!", "Thông báo");
                txtTen.Focus();
                return false;
            }

            // SĐT: nếu có nhập phải 10-11 chữ số
            if (!string.IsNullOrWhiteSpace(txtDienThoai.Text) &&
                !System.Text.RegularExpressions.Regex.IsMatch(txtDienThoai.Text.Trim(), @"^\d{10,11}$"))
            {
                TDCMessageBox.Show("Số điện thoại phải gồm 10-11 chữ số!", "Thông báo");
                txtDienThoai.Focus();
                return false;
            }

            // Mã số thuế: nếu có nhập phải 10 hoặc 13 chữ số
            if (!string.IsNullOrWhiteSpace(txtMaSoThue.Text) &&
                !System.Text.RegularExpressions.Regex.IsMatch(txtMaSoThue.Text.Trim(), @"^\d{10}(\d{3})?$"))
            {
                TDCMessageBox.Show("Mã số thuế phải gồm 10 hoặc 13 chữ số!", "Thông báo");
                txtMaSoThue.Focus();
                return false;
            }

            return true;
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
                var idProp = typeof(ET_NhaCungCap).GetProperty("Id");
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



