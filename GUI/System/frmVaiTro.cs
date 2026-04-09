using System;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using ET;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmVaiTro : Form, IBaseForm
    {
        public frmVaiTro()
        {
            InitializeComponent();
            if (ThemeManager.IsInDesignMode(this)) return;

            InitIcons();
            ApplyStyles();
            ApplyPermissions();
            LoadData();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            gridView.ApplyFindFilter(txtTimKiem.Text);
        }

        public void ApplyPermissions()
        {
            // RBAC logic if needed, e.g. disabling btnXoa if not admin
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);
        }

        public void InitIcons()
        {
            btnThem.Image = IconHelper.GetBitmap(IconChar.Plus, Color.White, 20);
            btnSua.Image = IconHelper.GetBitmap(IconChar.Edit, Color.White, 20);
            btnXoa.Image = IconHelper.GetBitmap(IconChar.Trash, Color.White, 20);
            btnLamMoi.Image = IconHelper.GetBitmap(IconChar.Sync, Color.White, 20);
        }



        private void frmVaiTro_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            gridControl.DataSource = BUS_VaiTro.Instance.LoadDS();
            if (gridView.Columns.Count > 0)
            {
                if (gridView.Columns["Id"] != null) gridView.Columns["Id"].Width = 100;
                if (gridView.Columns["TenVaiTro"] != null) gridView.Columns["TenVaiTro"].Caption = "Tên Vai Trò";
            }
        }

        private void GridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView.FocusedRowHandle >= 0)
            {
                var row = gridView.GetFocusedRow() as ET_VaiTro;
                if (row != null)
                {
                    txtId.Text = row.Id.ToString();
                    txtTenVaiTro.Text = row.TenVaiTro;
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTenVaiTro.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập tên vai trò!", "Thông báo");
                return;
            }

            var et = new ET_VaiTro { TenVaiTro = txtTenVaiTro.Text.Trim() };
            if (BUS_VaiTro.Instance.Them(et))
            {
                TDCMessageBox.Show("Thêm thành công!", "Thông báo");
                LoadData();
            }
            else
            {
                TDCMessageBox.Show("Thêm thất bại!", "Lỗi");
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text)) return;

            var et = new ET_VaiTro { 
                Id = int.Parse(txtId.Text),
                TenVaiTro = txtTenVaiTro.Text.Trim() 
            };
            
            if (BUS_VaiTro.Instance.Sua(et))
            {
                TDCMessageBox.Show("Cập nhật thành công!", "Thông báo");
                LoadData();
            }
            else
            {
                TDCMessageBox.Show("Cập nhật thất bại!", "Lỗi");
            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtId.Text)) return;

            if (TDCMessageBox.Show("Bạn có chắc chắn muốn xóa vai trò này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                if (BUS_VaiTro.Instance.Xoa(int.Parse(txtId.Text)))
                {
                    TDCMessageBox.Show("Xóa thành công!", "Thông báo");
                    LoadData();
                }
                else
                {
                    TDCMessageBox.Show("Xóa thất bại! Vai trò có thể đang được sử dụng.", "Lỗi");
                }
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtId.Clear();
            txtTenVaiTro.Clear();
            LoadData();
        }
    }
}

