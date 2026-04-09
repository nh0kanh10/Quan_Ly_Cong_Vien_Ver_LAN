using System;
using System.Drawing;
using System.Windows.Forms;
using BUS;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmTroChoi : Form
    {
        public frmTroChoi()
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);
            SetupBasicEvents();
        }

        private void SetupBasicEvents()
        {
            txtTimKiem.TextChanged += (s, e) => gridView.ApplyFindFilter(txtTimKiem.Text);
            
            cboTrangThai.Items.Clear();
            cboTrangThai.Items.AddRange(new object[] { 
                ET.AppConstants.TrangThaiSanPham.DangBan, 
                ET.AppConstants.TrangThaiSanPham.TamNgung, 
                ET.AppConstants.TrangThaiSanPham.NgungBan, 
                ET.AppConstants.TrangThaiSanPham.HetHang 
            });
            
            // Icon initialization
            btnThem.Image = IconHelper.GetBitmap(IconChar.Plus, Color.White, 16);
            btnSua.Image = IconHelper.GetBitmap(IconChar.Edit, Color.White, 16);
            btnXoa.Image = IconHelper.GetBitmap(IconChar.Trash, Color.White, 16);
            btnLamMoi.Image = IconHelper.GetBitmap(IconChar.Sync, Color.White, 16);
            
            this.Load += (s, e) => LoadData();
        }

        private void LoadData()
        {
            gridControl.DataSource = BUS_TroChoi.Instance.LoadDS();
            gridView.PopulateColumns();
            FormatGrid();
        }

        private void FormatGrid()
        {
            var view = gridView;
            if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;

            if (view.Columns["MaCode"] != null) view.Columns["MaCode"].Caption = "Mã Trò Chơi";
            if (view.Columns["TenTroChoi"] != null) view.Columns["TenTroChoi"].Caption = "Tên Trò Chơi";
            if (view.Columns["MoTa"] != null) view.Columns["MoTa"].Caption = "Mô Tả";
            if (view.Columns["TrangThai"] != null) view.Columns["TrangThai"].Caption = "Trạng Thái";

            var repKhuVuc = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            repKhuVuc.DataSource = BUS.BUS_KhuVuc.Instance.LoadDS();
            repKhuVuc.DisplayMember = "TenKhuVuc";
            repKhuVuc.ValueMember = "Id";
            repKhuVuc.NullText = "";
            if(view.Columns["IdKhuVuc"] != null) {
                view.Columns["IdKhuVuc"].ColumnEdit = repKhuVuc;
                view.Columns["IdKhuVuc"].Visible = true;
                view.Columns["IdKhuVuc"].Caption = "Khu Vực";
            }
            
            view.OptionsView.ColumnAutoWidth = true;
        }

        private ET.ET_TroChoi GetEntityFromUI()
        {
            if (string.IsNullOrWhiteSpace(txtTenTroChoi.Text) || slkKhuVuc.EditValue == null)
            {
                TDCMessageBox.Show("Vui lòng nhập tên trò chơi và chọn khu vực!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            int id = _currentEntity != null ? _currentEntity.Id : 0;
            return new ET.ET_TroChoi
            {
                Id = id,
                TenTroChoi = txtTenTroChoi.Text,
                IdKhuVuc = Convert.ToInt32(slkKhuVuc.EditValue),
                MoTa = txtMoTa.Text,
                TrangThai = cboTrangThai.Text
            };
        }

        private void ShowEntityToUI(ET.ET_TroChoi et)
        {
            if (et == null) {
                txtTenTroChoi.Text = "";
                slkKhuVuc.EditValue = null;
                txtMoTa.Text = "";
                cboTrangThai.SelectedIndex = -1;
                return;
            }
            txtTenTroChoi.Text = et.TenTroChoi;
            slkKhuVuc.EditValue = et.IdKhuVuc;
            txtMoTa.Text = et.MoTa;
            cboTrangThai.Text = et.TrangThai;
        }

        private void btnThem_Click(object sender, EventArgs e) 
        { 
            try {
                var et = GetEntityFromUI();
                if (et == null) return;
                et.Id = 0;
                var r = BUS_TroChoi.Instance.Them(et);
                if (r.IsSuccess) { TDCMessageBox.Show("Thêm thành công!"); LoadData(); }
                else TDCMessageBox.Show(r.ErrorMessage);
            } catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }
        
        private void btnSua_Click(object sender, EventArgs e) 
        { 
            try {
                var et = GetEntityFromUI();
                if (et == null || et.Id <= 0) return;
                var r = BUS_TroChoi.Instance.Sua(et);
                if (r.IsSuccess) { TDCMessageBox.Show("Cập nhật thành công!"); LoadData(); }
                else TDCMessageBox.Show(r.ErrorMessage);
            } catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }
        
        private void btnXoa_Click(object sender, EventArgs e) 
        { 
            if (_currentEntity == null) return;
            if (TDCMessageBox.Show("Chắc chắn xóa?", "Xóa", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                var r = BUS_TroChoi.Instance.Xoa(_currentEntity.Id);
                if (r.IsSuccess) { TDCMessageBox.Show("Xóa thành công!"); LoadData(); }
                else TDCMessageBox.Show(r.ErrorMessage);
            }
        }
        
        private void btnLamMoi_Click(object sender, EventArgs e) 
        { 
            txtTimKiem.Clear();
            LoadData(); 
            gridView.ClearFindFilter();
            _currentEntity = null;
            ShowEntityToUI(null);
            txtTenTroChoi.Focus();
        }
        
        private ET.ET_TroChoi _currentEntity;
        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView.FocusedRowHandle >= 0)
            {
                _currentEntity = gridView.GetFocusedRow() as ET.ET_TroChoi;
                ShowEntityToUI(_currentEntity);
            }
        }
    }
}
