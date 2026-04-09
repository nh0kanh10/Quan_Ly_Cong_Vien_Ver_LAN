using System;
using System.Drawing;
using System.Windows.Forms;
using ET;
using BUS;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmKhuVucThu : Form
    {
        public frmKhuVucThu()
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);
            ThemeManager.StyleDevExpressGrid(gridChuongTrai);
            SetupBasicEvents();

            cboTrangThaiChuong.Items.AddRange(new string[] { "Hoạt động", "Bảo trì", "Đóng" });
            cboTrangThaiChuong.SelectedIndex = 0;

            LoadDongVatLookup();
        }

        private void SetupBasicEvents()
        {
            txtTimKiem.TextChanged += (s, e) => gridView.ApplyFindFilter(txtTimKiem.Text);

            btnThem.Image = IconHelper.GetBitmap(IconChar.Plus, Color.White, 16);
            btnSua.Image = IconHelper.GetBitmap(IconChar.Edit, Color.White, 16);
            btnXoa.Image = IconHelper.GetBitmap(IconChar.Trash, Color.White, 16);
            btnLamMoi.Image = IconHelper.GetBitmap(IconChar.Sync, Color.White, 16);

            gridViewChuongTrai.FocusedRowChanged += gridViewChuongTrai_FocusedRowChanged;
            gridViewChuongTrai.RowClick += (s, e) => {
                if (e.Clicks == 2 && gridViewChuongTrai.GetFocusedRow() is ET_ChuongTrai)
                    tabControlDetails.SelectedTab = tabChuongTrai;
            };

            this.Load += (s, e) => LoadData();
        }

        private void LoadDongVatLookup()
        {
            var ds = DAL.DAL_DongVat.Instance.LoadDS();
            slkDongVat.Properties.DataSource = ds;
            slkDongVat.Properties.DisplayMember = "Ten";
            slkDongVat.Properties.ValueMember = "Id";
            slkDongVatView.Columns.Clear();
            slkDongVatView.Columns.AddVisible("Ten", "Tên Động Vật");
            slkDongVatView.Columns.AddVisible("Loai", "Loài");
        }

        

        private void LoadData()
        {
            gridControl.DataSource = BUS_KhuVucThu.Instance.LoadDS();
            FormatGrid();
        }

        private void FormatGrid()
        {
            if (gridView.Columns["Id"] != null) gridView.Columns["Id"].Visible = false;
            if (gridView.Columns["MaCode"] != null) gridView.Columns["MaCode"].Caption = "Mã KV";
            if (gridView.Columns["TenKhuVuc"] != null) gridView.Columns["TenKhuVuc"].Caption = "Tên Khu Thú";
            if (gridView.Columns["MoTa"] != null) gridView.Columns["MoTa"].Caption = "Mô Tả";
            gridView.BestFitColumns();
        }

        private ET_KhuVucThu GetEntityFromUI()
        {
            if (string.IsNullOrWhiteSpace(txtTenKhuVuc.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập tên khu vực!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int id = 0;
            if (gridView.GetFocusedRow() is ET_KhuVucThu focused) id = focused.Id;

            return new ET_KhuVucThu
            {
                Id = id,
                MaCode = txtMaCode.Text,
                TenKhuVuc = txtTenKhuVuc.Text,
                MoTa = txtMoTa.Text
            };
        }

        private void ShowEntityToUI(ET_KhuVucThu entity)
        {
            if (entity == null)
            {
                txtMaCode.Text = "";
                txtTenKhuVuc.Text = "";
                txtMoTa.Text = "";
                return;
            }

            txtMaCode.Text = entity.MaCode;
            txtTenKhuVuc.Text = entity.TenKhuVuc;
            txtMoTa.Text = entity.MoTa;
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView.GetFocusedRow() is ET_KhuVucThu row)
            {
                ShowEntityToUI(row);
                LoadChuongTrai(row.Id);
            }
        }

       
        private int _currentKhuVucThuId = 0;

        private void LoadChuongTrai(int idKhuVucThu)
        {
            _currentKhuVucThuId = idKhuVucThu;
            var ds = BUS_ChuongTrai.Instance.LoadTheoKhuVucThu(idKhuVucThu);
            gridChuongTrai.DataSource = ds;

            if (gridViewChuongTrai.Columns["Id"] != null) gridViewChuongTrai.Columns["Id"].Visible = false;
            if (gridViewChuongTrai.Columns["IdKhuVucThu"] != null) gridViewChuongTrai.Columns["IdKhuVucThu"].Visible = false;
            if (gridViewChuongTrai.Columns["IdDongVat"] != null) gridViewChuongTrai.Columns["IdDongVat"].Visible = false;
            if (gridViewChuongTrai.Columns["TenChuong"] != null) gridViewChuongTrai.Columns["TenChuong"].Caption = "Tên Chuồng";
            if (gridViewChuongTrai.Columns["SucChua"] != null) gridViewChuongTrai.Columns["SucChua"].Caption = "Sức Chứa";
            if (gridViewChuongTrai.Columns["TrangThai"] != null) gridViewChuongTrai.Columns["TrangThai"].Caption = "Trạng Thái";
            gridViewChuongTrai.BestFitColumns();

            var colDV = gridViewChuongTrai.Columns["IdDongVat"];
            if (colDV != null)
            {
                colDV.Visible = true;
                colDV.Caption = "Động Vật";
                var repoDV = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
                repoDV.DataSource = DAL.DAL_DongVat.Instance.LoadDS();
                repoDV.DisplayMember = "Ten";
                repoDV.ValueMember = "Id";
                var popupView = new DevExpress.XtraGrid.Views.Grid.GridView();
                popupView.Columns.AddVisible("Ten", "Tên");
                popupView.Columns.AddVisible("Loai", "Loài");
                repoDV.View = popupView;
                gridChuongTrai.RepositoryItems.Add(repoDV);
                colDV.ColumnEdit = repoDV;
            }

            pnlChuongTrai.Text = string.Format("Chuồng Trại — {0} chuồng", ds.Count);
        }

        private void gridViewChuongTrai_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewChuongTrai.GetFocusedRow() is ET_ChuongTrai ct)
            {
                ShowChuongTraiToUI(ct);
            }
        }

        private void ShowChuongTraiToUI(ET_ChuongTrai entity)
        {
            if (entity == null)
            {
                txtTenChuong.Text = "";
                spnSucChuaChuong.Value = 10;
                cboTrangThaiChuong.Text = "Hoạt động";
                slkDongVat.EditValue = null;
                return;
            }

            txtTenChuong.Text = entity.TenChuong;
            spnSucChuaChuong.Value = entity.SucChua ?? 10;
            cboTrangThaiChuong.Text = entity.TrangThai;
            slkDongVat.EditValue = entity.IdDongVat;
        }

        private ET_ChuongTrai GetChuongTraiFromUI()
        {
            if (string.IsNullOrWhiteSpace(txtTenChuong.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập tên chuồng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if (_currentKhuVucThuId <= 0)
            {
                TDCMessageBox.Show("Vui lòng chọn khu vực thú trước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int id = 0;
            if (gridViewChuongTrai.GetFocusedRow() is ET_ChuongTrai focused) id = focused.Id;

            int idDV = 0;
            if (slkDongVat.EditValue != null && slkDongVat.EditValue != DBNull.Value)
                idDV = Convert.ToInt32(slkDongVat.EditValue);

            return new ET_ChuongTrai
            {
                Id = id,
                IdKhuVucThu = _currentKhuVucThuId,
                IdDongVat = idDV,
                TenChuong = txtTenChuong.Text,
                SucChua = Convert.ToInt32(spnSucChuaChuong.Value),
                TrangThai = cboTrangThaiChuong.Text
            };
        }

       

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControlDetails.SelectedTab == tabKhuThu)
                {
                    var et = GetEntityFromUI();
                    if (et == null) return;
                    et.Id = 0;
                    var r = BUS_KhuVucThu.Instance.Them(et);
                    if (r.IsSuccess) { TDCMessageBox.Show("Thêm khu thú thành công!"); LoadData(); }
                    else TDCMessageBox.Show(r.ErrorMessage);
                }
                else
                {
                    var et = GetChuongTraiFromUI();
                    if (et == null) return;
                    et.Id = 0;
                    var r = BUS_ChuongTrai.Instance.Them(et);
                    if (r.IsSuccess) { TDCMessageBox.Show("Thêm chuồng thành công!"); LoadChuongTrai(_currentKhuVucThuId); }
                    else TDCMessageBox.Show(r.ErrorMessage);
                }
            }
            catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControlDetails.SelectedTab == tabKhuThu)
                {
                    var et = GetEntityFromUI();
                    if (et == null || et.Id <= 0) return;
                    var r = BUS_KhuVucThu.Instance.Sua(et);
                    if (r.IsSuccess) { TDCMessageBox.Show("Cập nhật khu thú thành công!"); LoadData(); }
                    else TDCMessageBox.Show(r.ErrorMessage);
                }
                else
                {
                    var et = GetChuongTraiFromUI();
                    if (et == null || et.Id <= 0) return;
                    var r = BUS_ChuongTrai.Instance.Sua(et);
                    if (r.IsSuccess) { TDCMessageBox.Show("Cập nhật chuồng thành công!"); LoadChuongTrai(_currentKhuVucThuId); }
                    else TDCMessageBox.Show(r.ErrorMessage);
                }
            }
            catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControlDetails.SelectedTab == tabKhuThu)
                {
                    if (gridView.GetFocusedRow() is ET_KhuVucThu focused)
                    {
                        if (TDCMessageBox.Show("Xóa khu vực thú này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            var r = BUS_KhuVucThu.Instance.Xoa(focused.Id);
                            if (r.IsSuccess) LoadData();
                            else TDCMessageBox.Show(r.ErrorMessage);
                        }
                    }
                }
                else
                {
                    if (gridViewChuongTrai.GetFocusedRow() is ET_ChuongTrai focused)
                    {
                        if (TDCMessageBox.Show("Xóa chuồng trại này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            var r = BUS_ChuongTrai.Instance.Xoa(focused.Id);
                            if (r.IsSuccess) LoadChuongTrai(_currentKhuVucThuId);
                            else TDCMessageBox.Show(r.ErrorMessage);
                        }
                    }
                }
            }
            catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            if (tabControlDetails.SelectedTab == tabKhuThu)
            {
                LoadData();
                ShowEntityToUI(null);
                txtMaCode.Focus();
            }
            else
            {
                if (_currentKhuVucThuId > 0) LoadChuongTrai(_currentKhuVucThuId);
                ShowChuongTraiToUI(null);
                txtTenChuong.Focus();
            }
        }

    }
}
