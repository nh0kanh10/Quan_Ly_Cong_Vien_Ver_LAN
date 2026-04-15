using System;
using System.Drawing;
using System.Windows.Forms;
using ET;
using BUS;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmNhaHang : Form
    {
        public frmNhaHang()
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);
            ThemeManager.StyleDevExpressGrid(gridBanAn);
            SetupBasicEvents();

            cboLoaiHinh.Items.AddRange(new string[] { "Buffet", "A La Carte", "Fast Food", "Cafe" });
            cboTrangThai.Items.AddRange(new string[] { AppConstants.TrangThaiSanPham.DangBan, AppConstants.TrangThaiSanPham.TamNgung, AppConstants.TrangThaiSanPham.NgungBan });
            
            var dsKhuVuc = BUS_KhuVuc.Instance.LoadDS();
            slkKhuVuc.Properties.DataSource = dsKhuVuc;
            slkKhuVuc.Properties.DisplayMember = "TenKhuVuc";
            slkKhuVuc.Properties.ValueMember = "Id";
            slkKhuVuc.Properties.NullText = "Chọn khu vực...";
            ThemeManager.StyleSearchLookUpEdit(slkKhuVuc, new[] { "TenKhuVuc" }, new[] { "Khu Vực" });
        }

        private void SetupBasicEvents()
        {
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;
            
            btnThem.Image = IconHelper.GetBitmap(IconChar.Plus, Color.White, 16);
            btnSua.Image = IconHelper.GetBitmap(IconChar.Edit, Color.White, 16);
            btnXoa.Image = IconHelper.GetBitmap(IconChar.Trash, Color.White, 16);
            btnLamMoi.Image = IconHelper.GetBitmap(IconChar.Sync, Color.White, 16);
            
            gridViewBanAn.FocusedRowChanged += gridViewBanAn_FocusedRowChanged;
            gridViewBanAn.RowClick += gridViewBanAn_RowClick;

            var trangThaiBanList = new System.Collections.Generic.Dictionary<string, string> {
                { AppConstants.TrangThaiPhong.Trong, "Trống" },
                { AppConstants.TrangThaiPhong.DangSuDung, "Đang sử dụng" },
                { AppConstants.TrangThaiPhong.BaoTri, "Bảo trì" }
            };
            cboTrangThaiBan.DataSource = new BindingSource(trangThaiBanList, null);
            cboTrangThaiBan.DisplayMember = "Value";
            cboTrangThaiBan.ValueMember = "Key";

            this.Load += frmNhaHang_Load;
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            gridView.ApplyFindFilter(txtTimKiem.Text);
        }

        private void frmNhaHang_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void gridViewBanAn_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewBanAn.GetFocusedRow() is ET_BanAn ban) {
                ShowBanAnToUI(ban);
            }
        }

        private void gridViewBanAn_RowClick(object sender, DevExpress.XtraGrid.Views.Grid.RowClickEventArgs e)
        {
            if (e.Clicks == 2 && gridViewBanAn.GetFocusedRow() is ET_BanAn ban) {
                tabControlDetails.SelectedTab = tabBanAn;
            }
        }

        private void LoadData()
        {
            gridControl.DataSource = BUS_NhaHang.Instance.LoadDS();
            FormatGrid();
        }
        
        private void FormatGrid()
        {
            if (gridView.Columns["Id"] != null) gridView.Columns["Id"].Visible = false;
            
            if (gridView.Columns["TenNhaHang"] != null) gridView.Columns["TenNhaHang"].Caption = "Tên Nhà Hàng";
            if (gridView.Columns["IdKhuVuc"] != null) gridView.Columns["IdKhuVuc"].Caption = "Khu Vực";
            if (gridView.Columns["SucChua"] != null) gridView.Columns["SucChua"].Caption = "Sức Chứa (Bàn)";
            if (gridView.Columns["MoTa"] != null) gridView.Columns["MoTa"].Caption = "Mô Tả";
            
            var repKhuVuc = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            repKhuVuc.DataSource = BUS_KhuVuc.Instance.LoadDS();
            repKhuVuc.DisplayMember = "TenKhuVuc";
            repKhuVuc.ValueMember = "Id";
            repKhuVuc.NullText = "Chưa chọn khu vực";
            if(gridView.Columns["IdKhuVuc"] != null) {
                gridView.Columns["IdKhuVuc"].ColumnEdit = repKhuVuc;
                gridView.Columns["IdKhuVuc"].Visible = true;
            }

            gridView.BestFitColumns();
        }

        private ET_NhaHang GetEntityFromUI()
        {
            if (string.IsNullOrWhiteSpace(txtTenNhaHang.Text) || slkKhuVuc.EditValue == null)
            {
                TDCMessageBox.Show("Vui lòng nhập tên nhà hàng và chọn khu vực!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            
            int id = 0;
            if (gridView.GetFocusedRow() is ET_NhaHang focused) id = focused.Id;
            
            return new ET_NhaHang
            {
                Id = id,
                TenNhaHang = txtTenNhaHang.Text,
                IdKhuVuc = Convert.ToInt32(slkKhuVuc.EditValue),
                SucChua = Convert.ToInt32(spnSoBanTong.Value),
                MoTa = txtMoTa.Text
            };
        }

        private void ShowEntityToUI(ET_NhaHang entity)
        {
            if (entity == null) {
                txtTenNhaHang.Text = "";
                slkKhuVuc.EditValue = null;
                spnSoBanTong.Value = 0;
                txtMoTa.Text = "";
                return;
            }
            
            txtTenNhaHang.Text = entity.TenNhaHang;
            slkKhuVuc.EditValue = entity.IdKhuVuc;
            spnSoBanTong.Value = entity.SucChua ?? 0;
            txtMoTa.Text = entity.MoTa;
        }

        private void btnThem_Click(object sender, EventArgs e) 
        { 
            try {
                if (tabControlDetails.SelectedTab == tabNhaHang) {
                    var et = GetEntityFromUI();
                    if (et == null) return;
                    et.Id = 0;
                    var r = BUS_NhaHang.Instance.Them(et);
                    if (r.IsSuccess) { TDCMessageBox.Show("Thêm nhà hàng thành công!"); LoadData(); }
                    else TDCMessageBox.Show(r.ErrorMessage);
                } else {
                    var nhaHang = gridView.GetFocusedRow() as ET_NhaHang;
                    if (nhaHang != null) {
                        int maxBan = nhaHang.SucChua ?? 0;
                        int currentCount = gridViewBanAn.RowCount;
                        if (maxBan > 0 && currentCount >= maxBan) {
                            TDCMessageBox.Show(string.Format("Nhà hàng này đã đạt tối đa giới hạn bàn ({0} bàn)!", maxBan), "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    var et = GetBanAnFromUI();
                    if (et == null) return;
                    et.Id = 0;
                    var r = BUS_BanAn.Instance.Them(et);
                    if (r.IsSuccess) { TDCMessageBox.Show("Thêm bàn thành công!"); LoadBanAn(_currentNhaHangId); }
                    else TDCMessageBox.Show(r.ErrorMessage);
                }
            } catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnSua_Click(object sender, EventArgs e) 
        { 
            try {
                if (tabControlDetails.SelectedTab == tabNhaHang) {
                    var et = GetEntityFromUI();
                    if (et == null || et.Id <= 0) return;
                    var r = BUS_NhaHang.Instance.Sua(et);
                    if (r.IsSuccess) { TDCMessageBox.Show("Cập nhật nhà hàng thành công!"); LoadData(); }
                    else TDCMessageBox.Show(r.ErrorMessage);
                } else {
                    var et = GetBanAnFromUI();
                    if (et == null || et.Id <= 0) return;
                    var r = BUS_BanAn.Instance.Sua(et);
                    if (r.IsSuccess) { TDCMessageBox.Show("Cập nhật bàn thành công!"); LoadBanAn(_currentNhaHangId); }
                    else TDCMessageBox.Show(r.ErrorMessage);
                }
            } catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnXoa_Click(object sender, EventArgs e) 
        { 
            try {
                if (tabControlDetails.SelectedTab == tabNhaHang) {
                    if (gridView.GetFocusedRow() is ET_NhaHang focused) {
                        if (TDCMessageBox.Show("Xóa nhà hàng này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                             var r = BUS_NhaHang.Instance.Xoa(focused.Id);
                             if (r.IsSuccess) LoadData();
                             else TDCMessageBox.Show(r.ErrorMessage);
                        }
                    }
                } else {
                    if (gridViewBanAn.GetFocusedRow() is ET_BanAn focused) {
                        if (focused.TrangThai == AppConstants.TrangThaiPhong.DangSuDung) {
                            TDCMessageBox.Show("Không thể xóa bàn đang sử dụng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (TDCMessageBox.Show("Xóa bàn này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                             var r = BUS_BanAn.Instance.Xoa(focused.Id);
                             if (r.IsSuccess) LoadBanAn(_currentNhaHangId);
                             else TDCMessageBox.Show(r.ErrorMessage);
                        }
                    }
                }
            } catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e) 
        { 
            if (tabControlDetails.SelectedTab == tabNhaHang) {
                LoadData(); 
                ShowEntityToUI(null);
                txtTenNhaHang.Focus();
            } else {
                if (_currentNhaHangId > 0) LoadBanAn(_currentNhaHangId);
                ShowBanAnToUI(null);
                txtMaBan.Focus();
            }
        }
        
        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView.GetFocusedRow() is ET_NhaHang row)
            {
                ShowEntityToUI(row);
                LoadBanAn(row.Id);
            }
        }

        // =
        // BÀN ĂN
        // =
        private int _currentNhaHangId = 0;

        private void LoadBanAn(int idNhaHang)
        {
            _currentNhaHangId = idNhaHang;
            var ds = BUS_BanAn.Instance.LoadTheoNhaHang(idNhaHang);
            gridBanAn.DataSource = ds;

            if (gridViewBanAn.Columns["Id"] != null) gridViewBanAn.Columns["Id"].Visible = false;
            if (gridViewBanAn.Columns["IdNhaHang"] != null) gridViewBanAn.Columns["IdNhaHang"].Visible = false;
            if (gridViewBanAn.Columns["MaBan"] != null) gridViewBanAn.Columns["MaBan"].Caption = "Mã Bàn";
            if (gridViewBanAn.Columns["SucChua"] != null) gridViewBanAn.Columns["SucChua"].Caption = "Sức Chứa";
            if (gridViewBanAn.Columns["TrangThai"] != null) gridViewBanAn.Columns["TrangThai"].Visible = false;
            // Optionally map a virtual column if we wanted to display "TenTrangThai" on grid:
            if (gridViewBanAn.Columns["TenTrangThai"] != null) gridViewBanAn.Columns["TenTrangThai"].Caption = "Trạng Thái";
            gridViewBanAn.BestFitColumns();

            pnlBanAn.Text = string.Format("Bàn Ăn — {0} bàn", ds.Count);
        }

        private void ShowBanAnToUI(ET_BanAn entity)
        {
            if (entity == null) {
                txtMaBan.Text = "";
                spnSucChuaBan.Value = 4;
                cboTrangThaiBan.SelectedValue = AppConstants.TrangThaiPhong.Trong;
                return;
            }
            
            txtMaBan.Text = entity.MaBan;
            spnSucChuaBan.Value = entity.SucChua;
            cboTrangThaiBan.SelectedValue = entity.TrangThai;
        }

        private ET_BanAn GetBanAnFromUI()
        {
            if (string.IsNullOrWhiteSpace(txtMaBan.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập mã bàn!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if (_currentNhaHangId <= 0)
            {
                TDCMessageBox.Show("Vui lòng chọn nhà hàng trước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int id = 0;
            if (gridViewBanAn.GetFocusedRow() is ET_BanAn focused) id = focused.Id;

            return new ET_BanAn
            {
                Id = id,
                IdNhaHang = _currentNhaHangId,
                MaBan = txtMaBan.Text,
                SucChua = Convert.ToInt32(spnSucChuaBan.Value),
                TrangThai = cboTrangThaiBan.SelectedValue?.ToString() ?? AppConstants.TrangThaiPhong.Trong
            };
        }

        private void btnThemBan_Click(object sender, EventArgs e)
        {
            ShowBanAnToUI(null);
            tabControlDetails.SelectedTab = tabBanAn;
            txtMaBan.Focus();
        }

        private void btnXoaBan_Click(object sender, EventArgs e)
        {
            tabControlDetails.SelectedTab = tabBanAn;
            btnXoa_Click(sender, e);
        }
    }
}

