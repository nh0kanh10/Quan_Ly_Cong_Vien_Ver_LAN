using System;
using System.Drawing;
using System.Collections.Generic;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;
using DevExpress.XtraGrid.Views.Base;

namespace GUI
{
    public partial class frmKhuVuc : Form, IBaseForm
    {
        private IBaseBUS<ET_KhuVuc> _bus;
        private ET_KhuVuc _currentEntity;

        public frmKhuVuc()
        {
            InitializeComponent();
            _bus = BUS_KhuVuc.Instance;
            
            InitIcons();
            ApplyStyles();
            ApplyPermissions();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            gridView.ApplyFindFilter(txtTimKiem.Text.Trim());
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_REGION"))
            {
                this.Enabled = false;
                return;
            }

            bool canManage = BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "MANAGE_REGION");
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

        private void frmKhuVuc_Load(object sender, EventArgs e)
        {
            loadComboBoxes();
            txtMaCode.Text = BUS_KhuVuc.Instance.LayMaCodeTiepTheo();
            LoadData();
        }

        private void loadComboBoxes()
        {
            cboTrangThai.DataSource = new List<string> { "Mở cửa", "Bảo trì", "Đóng cửa" };
        }

        public void LoadData()
        {
            if (_bus == null) return;
            gridControl.DataSource = new System.ComponentModel.BindingList<ET.ET_KhuVuc>(_bus.LoadDS());
            gridView.PopulateColumns();
            FormatGrid();
        }

        private void FormatGrid()
        {
            var view = gridView;
            if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;
            if (view.Columns["MaCode"] != null)
            {
                view.Columns["MaCode"].Caption = "Mã Khu Vực";
                view.Columns["MaCode"].VisibleIndex = 0;
            }
            if (view.Columns["TenKhuVuc"] != null) view.Columns["TenKhuVuc"].Caption = "Tên Khu Vực";
            if (view.Columns["MoTa"] != null) view.Columns["MoTa"].Caption = "Mô Tả";
            if (view.Columns["TrangThai"] != null) view.Columns["TrangThai"].Caption = "Trạng Thái";          
            
            string[] hiddenCols = { "HinhAnh", "CreatedAt", "UpdatedAt", "CreatedBy", "IsDeleted" };
            foreach (var col in hiddenCols)
            {
                if (view.Columns[col] != null) view.Columns[col].Visible = false;
            }
            view.OptionsView.ColumnAutoWidth = true;
        }

        private void OnFocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            if (gridView.FocusedRowHandle >= 0)
            {
                _currentEntity = gridView.GetFocusedRow() as ET_KhuVuc;
                if (_currentEntity != null) ShowEntityToUI(_currentEntity);
            }
        }

        private void ShowEntityToUI(ET_KhuVuc row)
        {
            txtMaKV.Text = row.Id.ToString();
            txtMaCode.Text = row.MaCode ?? "";
            txtTenKV.Text = row.TenKhuVuc ?? "";
            txtMoTa.Text = row.MoTa ?? "";
            cboTrangThai.Text = row.TrangThai ?? "Mở cửa";
            
            if (!string.IsNullOrEmpty(row.HinhAnh))
            {
                picHinhAnh.ImageLocation = row.HinhAnh;
                picHinhAnh.Tag = row.HinhAnh;
            }
            else
            {
                picHinhAnh.Image = null;
                picHinhAnh.Tag = null;
            }
        }

        private ET_KhuVuc GetEntityFromUI()
        {
            int id = 0;
            int.TryParse(txtMaKV.Text, out id);

            return new ET_KhuVuc
            {
                Id = id,
                MaCode = txtMaCode.Text.Trim(),
                TenKhuVuc = txtTenKV.Text.Trim(),
                MoTa = txtMoTa.Text.Trim(),
                TrangThai = cboTrangThai.Text,
                HinhAnh = (picHinhAnh.Tag != null) ? picHinhAnh.Tag.ToString() : null
            };
        }

        private void ClearUI()
        {
            txtMaKV.Text = "";
            txtTenKV.Text = "";
            txtMoTa.Text = "";
            txtMaCode.Text = BUS_KhuVuc.Instance.LayMaCodeTiepTheo();
            cboTrangThai.SelectedIndex = 0;
            picHinhAnh.Image = null;
            picHinhAnh.Tag = null;
        }

        private bool ValidateInput()
        {
            ET_KhuVuc et = GetEntityFromUI();
            bool isThem = string.IsNullOrEmpty(txtMaKV.Text);
            string check = BUS_KhuVuc.Instance.ValidateKhuVuc(et, isThem);
            if (!string.IsNullOrEmpty(check))
            {
                TDCMessageBox.Show(check, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        private void btnChonAnh_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.bmp";
                if (ofd.ShowDialog() == DialogResult.OK)
                {
                    picHinhAnh.ImageLocation = ofd.FileName;
                    picHinhAnh.Tag = ofd.FileName;
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
                var idProp = typeof(ET_KhuVuc).GetProperty("Id");
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



