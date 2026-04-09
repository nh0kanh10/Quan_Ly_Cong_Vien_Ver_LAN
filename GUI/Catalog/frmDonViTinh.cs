using System;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;
using DevExpress.XtraGrid.Views.Base;

namespace GUI
{
    public partial class frmDonViTinh : Form, IBaseForm
    {
        private IBaseBUS<ET_DonViTinh> _bus;
        private ET_DonViTinh _currentEntity;

        public frmDonViTinh()
        {
            InitializeComponent();
            _bus = BUS_DonViTinh.Instance;
            
            InitIcons();
            ApplyStyles();
            ApplyPermissions();
        }

        private void frmDonViTinh_Load(object sender, EventArgs e)
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

            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_UOM"))
            {
                this.Enabled = false;
                return;
            }

            bool canManage = BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_UOM");
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
            gridControl.DataSource = new System.ComponentModel.BindingList<ET.ET_DonViTinh>(_bus.LoadDS());
            gridView.PopulateColumns();
            FormatGrid();
        }

        private void FormatGrid()
        {
            var view = gridView;
            if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;
            if (view.Columns["KyHieu"] != null) view.Columns["KyHieu"].Caption = "Ký hiệu";
            if (view.Columns["Ten"] != null) view.Columns["Ten"].Caption = "Tên đơn vị tính";
            
            string[] hidden = { "CreatedAt", "UpdatedAt", "CreatedBy", "IsDeleted" };
            foreach (var col in hidden) if (view.Columns[col] != null) view.Columns[col].Visible = false;
            
            view.OptionsView.ColumnAutoWidth = true;
        }

        private void OnFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gridView.FocusedRowHandle >= 0)
            {
                _currentEntity = gridView.GetFocusedRow() as ET_DonViTinh;
                if (_currentEntity != null) ShowEntityToUI(_currentEntity);
            }
        }

        private void ShowEntityToUI(ET_DonViTinh row)
        {
            txtId.Text = row.Id.ToString();
            txtTen.Text = row.Ten;
            txtKyHieu.Text = row.KyHieu;
        }

        private ET_DonViTinh GetEntityFromUI()
        {
            return new ET_DonViTinh
            {
                Id = _currentEntity?.Id ?? 0,
                Ten = txtTen.Text.Trim(),
                KyHieu = txtKyHieu.Text.Trim()
            };
        }

        private void ClearUI()
        {
            txtId.Clear();
            txtTen.Clear();
            txtKyHieu.Clear();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập tên đơn vị tính!", "Thông báo");
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
                var idProp = typeof(ET_DonViTinh).GetProperty("Id");
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



