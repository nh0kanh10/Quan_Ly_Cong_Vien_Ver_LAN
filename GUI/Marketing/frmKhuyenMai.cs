using System;
using System.Drawing;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;
using DevExpress.XtraGrid.Views.Base;

namespace GUI
{
    public partial class frmKhuyenMai : Form, IBaseForm
    {
        private ET_KhuyenMai _currentEntity;

        public frmKhuyenMai()
        {
            InitializeComponent();

            InitIcons();
            ApplyStyles();
            ApplyPermissions();
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_PROMOTION"))
            {
                this.Enabled = false;
                return;
            }

            bool canManage = BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_PROMOTION");
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

        private void frmKhuyenMai_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            InitCombos();
            var data = BUS_KhuyenMai.Instance.LoadDS();
            gridControl.DataSource = new BindingList<ET_KhuyenMai>(data);
            gridView.PopulateColumns();
            FormatGrid();
        }

        private void InitCombos()
        {
            cboLoaiGiam.Items.Clear();
            cboLoaiGiam.Items.AddRange(new object[] { "Phần trăm (%)", "Số tiền (VNĐ)" });
            if (cboLoaiGiam.SelectedIndex < 0) cboLoaiGiam.SelectedIndex = 0;
        }

        private void FormatGrid()
        {
            var view = gridView;
            string[] hidden = { "Id", "IdSuKien", "CreatedAt", "CreatedBy", "IsDeleted" };
            foreach (var col in hidden)
                if (view.Columns[col] != null) view.Columns[col].Visible = false;

            if (view.Columns["MaCode"] != null) view.Columns["MaCode"].Caption = "Mã KM";
            if (view.Columns["TenKhuyenMai"] != null) view.Columns["TenKhuyenMai"].Caption = "Tên khuyến mãi";
            if (view.Columns["LoaiGiamGia"] != null) view.Columns["LoaiGiamGia"].Caption = "Loại giảm";
            if (view.Columns["GiaTriGiam"] != null)
            {
                view.Columns["GiaTriGiam"].Caption = "Giá trị giảm";
                view.Columns["GiaTriGiam"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                view.Columns["GiaTriGiam"].DisplayFormat.FormatString = "n0";
            }
            if (view.Columns["DonToiThieu"] != null)
            {
                view.Columns["DonToiThieu"].Caption = "Đơn tối thiểu";
                view.Columns["DonToiThieu"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                view.Columns["DonToiThieu"].DisplayFormat.FormatString = "n0";
            }
            if (view.Columns["NgayBatDau"] != null)
            {
                view.Columns["NgayBatDau"].Caption = "Ngày BĐ";
                view.Columns["NgayBatDau"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["NgayBatDau"].DisplayFormat.FormatString = "dd/MM/yyyy";
            }
            if (view.Columns["NgayKetThuc"] != null)
            {
                view.Columns["NgayKetThuc"].Caption = "Ngày KT";
                view.Columns["NgayKetThuc"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["NgayKetThuc"].DisplayFormat.FormatString = "dd/MM/yyyy";
            }
            if (view.Columns["TrangThai"] != null) view.Columns["TrangThai"].Caption = "Hoạt động";

            view.OptionsView.ColumnAutoWidth = true;
        }

        private void OnFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gridView.FocusedRowHandle >= 0)
            {
                _currentEntity = gridView.GetFocusedRow() as ET_KhuyenMai;
                if (_currentEntity != null) ShowEntityToUI(_currentEntity);
            }
        }

        private void ShowEntityToUI(ET_KhuyenMai row)
        {
            txtMaCode.Text = row.MaCode;
            txtTenKM.Text = row.TenKhuyenMai;
            cboLoaiGiam.SelectedIndex = row.LoaiGiamGia == ET.AppConstants.LoaiKhuyenMai.SoTien ? 1 : 0;
            txtGiaTri.Text = row.GiaTriGiam.ToString("n0");
            txtDonToiThieu.Text = row.DonToiThieu.HasValue ? row.DonToiThieu.Value.ToString("n0") : "";
            dtpNgayBD.DateTime = row.NgayBatDau;
            dtpNgayKT.DateTime = row.NgayKetThuc;
            chkTrangThai.Checked = row.TrangThai;
        }

        private ET_KhuyenMai GetEntityFromUI()
        {
            decimal giaTri = 0;
            decimal.TryParse(txtGiaTri.Text.Replace(".", "").Replace(",", ""), out giaTri);

            decimal? donMin = null;
            if (!string.IsNullOrWhiteSpace(txtDonToiThieu.Text))
            {
                decimal d = 0;
                if (decimal.TryParse(txtDonToiThieu.Text.Replace(".", "").Replace(",", ""), out d))
                    donMin = d;
            }

            return new ET_KhuyenMai
            {
                Id = _currentEntity?.Id ?? 0,
                MaCode = txtMaCode.Text.Trim(),
                TenKhuyenMai = txtTenKM.Text.Trim(),
                LoaiGiamGia = cboLoaiGiam.SelectedIndex == 1 ? ET.AppConstants.LoaiKhuyenMai.SoTien : ET.AppConstants.LoaiKhuyenMai.PhanTram,
                GiaTriGiam = giaTri,
                DonToiThieu = donMin,
                NgayBatDau = dtpNgayBD.DateTime.Date,
                NgayKetThuc = dtpNgayKT.DateTime.Date,
                TrangThai = chkTrangThai.Checked,
                CreatedAt = _currentEntity != null ? _currentEntity.CreatedAt : DateTime.Now,
                CreatedBy = _currentEntity != null ? _currentEntity.CreatedBy : null,
                IsDeleted = _currentEntity != null ? _currentEntity.IsDeleted : false
            };
        }

        private void ClearUI()
        {
            txtMaCode.Clear();
            txtTenKM.Clear();
            cboLoaiGiam.SelectedIndex = 0;
            txtGiaTri.Clear();
            txtDonToiThieu.Clear();
            dtpNgayBD.DateTime = DateTime.Today;
            dtpNgayKT.DateTime = DateTime.Today.AddMonths(1);
            chkTrangThai.Checked = true;
            _currentEntity = null;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            OnSearch();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTenKM.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập tên khuyến mãi!", "Thông báo");
                txtTenKM.Focus();
                return false;
            }

            // Giá trị giảm
            decimal giaTri = 0;
            string giaText = txtGiaTri.Text.Replace(".", "").Replace(",", "");
            if (!decimal.TryParse(giaText, out giaTri) || giaTri < 0)
            {
                TDCMessageBox.Show("Giá trị giảm phải là số >= 0!", "Thông báo");
                txtGiaTri.Focus();
                return false;
            }

            // Nếu loại giảm = Phần trăm -> phải 0-100
            if (cboLoaiGiam.Text.Contains("%") && (giaTri < 0 || giaTri > 100))
            {
                TDCMessageBox.Show("Phần trăm giảm giá phải từ 0 đến 100!", "Thông báo");
                txtGiaTri.Focus();
                return false;
            }

            // Đơn tối thiểu (nếu nhập) phải >= 0
            if (!string.IsNullOrWhiteSpace(txtDonToiThieu.Text))
            {
                decimal donMin;
                if (!decimal.TryParse(txtDonToiThieu.Text.Replace(".", "").Replace(",", ""), out donMin) || donMin < 0)
                {
                    TDCMessageBox.Show("Đơn tối thiểu phải là số >= 0!", "Thông báo");
                    txtDonToiThieu.Focus();
                    return false;
                }
            }

            // Ngày kết thúc phải sau ngày bắt đầu
            if (dtpNgayKT.DateTime <= dtpNgayBD.DateTime)
            {
                TDCMessageBox.Show("Ngày kết thúc phải sau ngày bắt đầu!", "Thông báo");
                return false;
            }
            return true;
        }

        private void OnSearch()
        {
            gridView.ApplyFindFilter(txtTimKiem.Text.Trim());
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;
            var et = GetEntityFromUI();
            var res = BUS_KhuyenMai.Instance.ThemKhuyenMai(et);
            HandleResult(res, "Thêm khuyến mãi thành công!");
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (_currentEntity == null) { TDCMessageBox.Show("Vui lòng chọn khuyến mãi cần sửa!", "Thông báo"); return; }
            if (!ValidateInput()) return;
            var et = GetEntityFromUI();
            var res = BUS_KhuyenMai.Instance.SuaKhuyenMai(et);
            HandleResult(res, "Cập nhật thành công!");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (_currentEntity == null) return;
            if (TDCMessageBox.Show("Bạn có chắc chắn muốn xóa khuyến mãi này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var res = BUS_KhuyenMai.Instance.XoaKhuyenMai(_currentEntity.Id);
                HandleResult(res, "Xóa thành công!");
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadData();
            gridView.ClearFindFilter();
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



