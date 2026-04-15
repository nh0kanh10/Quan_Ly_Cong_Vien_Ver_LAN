using System;
using System.Drawing;
using System.Windows.Forms;
using ET;
using BUS;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmKhuVucBien : Form
    {
        public frmKhuVucBien()
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);
            ThemeManager.StyleDevExpressGrid(gridChoiNghiMat);
            SetupBasicEvents();

            var trangThaiChoiList = new System.Collections.Generic.Dictionary<string, string> {
                { AppConstants.TrangThaiPhong.Trong, "Trống" },
                { AppConstants.TrangThaiPhong.DangSuDung, "Đang sử dụng" },
                { AppConstants.TrangThaiPhong.BaoTri, "Bảo trì" }
            };
            cboTrangThaiChoi.DataSource = new BindingSource(trangThaiChoiList, null);
            cboTrangThaiChoi.DisplayMember = "Value";
            cboTrangThaiChoi.ValueMember = "Key";
        }

        private void SetupBasicEvents()
        {
            txtTimKiem.TextChanged += (s, e) => gridView.ApplyFindFilter(txtTimKiem.Text);

            btnThem.Image = IconHelper.GetBitmap(IconChar.Plus, Color.White, 16);
            btnSua.Image = IconHelper.GetBitmap(IconChar.Edit, Color.White, 16);
            btnXoa.Image = IconHelper.GetBitmap(IconChar.Trash, Color.White, 16);
            btnLamMoi.Image = IconHelper.GetBitmap(IconChar.Sync, Color.White, 16);

            gridViewChoiNghiMat.FocusedRowChanged += gridViewChoiNghiMat_FocusedRowChanged;
            gridViewChoiNghiMat.RowClick += (s, e) => {
                if (e.Clicks == 2 && gridViewChoiNghiMat.GetFocusedRow() is ET_ChoiNghiMat)
                    tabControlDetails.SelectedTab = tabChoiNghiMat;
            };

            this.Load += (s, e) => LoadData();
        }

        // =
        // KHU VỰC BIỂN (MASTER)
        // =

        private void LoadData()
        {
            gridControl.DataSource = BUS_KhuVucBien.Instance.LoadDS();
            FormatGrid();
        }

        private void FormatGrid()
        {
            if (gridView.Columns["Id"] != null) gridView.Columns["Id"].Visible = false;
            if (gridView.Columns["MaCode"] != null) gridView.Columns["MaCode"].Caption = "Mã KV";
            if (gridView.Columns["TenKhuVuc"] != null) gridView.Columns["TenKhuVuc"].Caption = "Tên Vùng Biển";
            if (gridView.Columns["DoSauToiDa"] != null) gridView.Columns["DoSauToiDa"].Caption = "Độ Sâu (m)";
            if (gridView.Columns["YeuCauPhao"] != null) gridView.Columns["YeuCauPhao"].Caption = "Bắt Buộc Phao";
            if (gridView.Columns["MoTa"] != null) gridView.Columns["MoTa"].Caption = "Mô Tả";
            gridView.BestFitColumns();
        }

        private ET_KhuVucBien GetEntityFromUI()
        {
            if (string.IsNullOrWhiteSpace(txtTenKhuVuc.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập tên vùng biển!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int id = 0;
            if (gridView.GetFocusedRow() is ET_KhuVucBien focused) id = focused.Id;

            return new ET_KhuVucBien
            {
                Id = id,
                MaCode = txtMaCode.Text,
                TenKhuVuc = txtTenKhuVuc.Text,
                DoSauToiDa = spnDoSau.Value,
                YeuCauPhao = chkYeuCauPhao.Checked,
                MoTa = txtMoTa.Text
            };
        }

        private void ShowEntityToUI(ET_KhuVucBien entity)
        {
            if (entity == null)
            {
                txtMaCode.Text = "";
                txtTenKhuVuc.Text = "";
                spnDoSau.Value = 0;
                chkYeuCauPhao.Checked = false;
                txtMoTa.Text = "";
                return;
            }

            txtMaCode.Text = entity.MaCode;
            txtTenKhuVuc.Text = entity.TenKhuVuc;
            spnDoSau.Value = entity.DoSauToiDa ?? 0;
            chkYeuCauPhao.Checked = entity.YeuCauPhao;
            txtMoTa.Text = entity.MoTa;
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView.GetFocusedRow() is ET_KhuVucBien row)
            {
                ShowEntityToUI(row);
                LoadChoiNghiMat(row.Id);
            }
        }

        // =
        // CHÒI NGHỈ MÁT (DETAIL)
        // =
        private int _currentKhuVucBienId = 0;

        private void LoadChoiNghiMat(int idKhuVucBien)
        {
            _currentKhuVucBienId = idKhuVucBien;
            var ds = BUS_ChoiNghiMat.Instance.LoadTheoKhuVucBien(idKhuVucBien);
            gridChoiNghiMat.DataSource = ds;

            if (gridViewChoiNghiMat.Columns["Id"] != null) gridViewChoiNghiMat.Columns["Id"].Visible = false;
            if (gridViewChoiNghiMat.Columns["IdKhuVucBien"] != null) gridViewChoiNghiMat.Columns["IdKhuVucBien"].Visible = false;
            if (gridViewChoiNghiMat.Columns["IdSanPham"] != null) gridViewChoiNghiMat.Columns["IdSanPham"].Visible = false;
            if (gridViewChoiNghiMat.Columns["CreatedAt"] != null) gridViewChoiNghiMat.Columns["CreatedAt"].Visible = false;
            if (gridViewChoiNghiMat.Columns["CreatedBy"] != null) gridViewChoiNghiMat.Columns["CreatedBy"].Visible = false;
            if (gridViewChoiNghiMat.Columns["IsDeleted"] != null) gridViewChoiNghiMat.Columns["IsDeleted"].Visible = false;
            if (gridViewChoiNghiMat.Columns["RowVer"] != null) gridViewChoiNghiMat.Columns["RowVer"].Visible = false;
            if (gridViewChoiNghiMat.Columns["MaCode"] != null) gridViewChoiNghiMat.Columns["MaCode"].Caption = "Mã Chòi";
            if (gridViewChoiNghiMat.Columns["TenChoi"] != null) gridViewChoiNghiMat.Columns["TenChoi"].Caption = "Tên Chòi";
            if (gridViewChoiNghiMat.Columns["SucChua"] != null) gridViewChoiNghiMat.Columns["SucChua"].Caption = "Sức Chứa";
            if (gridViewChoiNghiMat.Columns["TrangThai"] != null) gridViewChoiNghiMat.Columns["TrangThai"].Visible = false;
            // Optionally map a virtual column if we wanted to display "TenTrangThai" on grid:
            if (gridViewChoiNghiMat.Columns["TenTrangThai"] != null) gridViewChoiNghiMat.Columns["TenTrangThai"].Caption = "Trạng Thái";
            gridViewChoiNghiMat.BestFitColumns();

            pnlChoiNghiMat.Text = string.Format("Chòi Nghỉ Mát — {0} chòi", ds.Count);
        }

        private void gridViewChoiNghiMat_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridViewChoiNghiMat.GetFocusedRow() is ET_ChoiNghiMat choi)
            {
                ShowChoiNghiMatToUI(choi);
            }
        }

        private void ShowChoiNghiMatToUI(ET_ChoiNghiMat entity)
        {
            if (entity == null)
            {
                txtMaChoi.Text = "";
                txtTenChoi.Text = "";
                spnSucChuaChoi.Value = 4;
                cboTrangThaiChoi.SelectedValue = AppConstants.TrangThaiPhong.Trong;
                return;
            }

            txtMaChoi.Text = entity.MaCode;
            txtTenChoi.Text = entity.TenChoi;
            spnSucChuaChoi.Value = entity.SucChua ?? 4;
            cboTrangThaiChoi.SelectedValue = entity.TrangThai;
        }

        private ET_ChoiNghiMat GetChoiNghiMatFromUI()
        {
            if (string.IsNullOrWhiteSpace(txtTenChoi.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập tên chòi!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
            if (_currentKhuVucBienId <= 0)
            {
                TDCMessageBox.Show("Vui lòng chọn khu vực biển trước!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int id = 0;
            if (gridViewChoiNghiMat.GetFocusedRow() is ET_ChoiNghiMat focused) id = focused.Id;

            return new ET_ChoiNghiMat
            {
                Id = id,
                IdKhuVucBien = _currentKhuVucBienId,
                MaCode = txtMaChoi.Text,
                TenChoi = txtTenChoi.Text,
                SucChua = Convert.ToInt32(spnSucChuaChoi.Value),
                TrangThai = cboTrangThaiChoi.SelectedValue?.ToString() ?? AppConstants.TrangThaiPhong.Trong
            };
        }

        // =
        // BUTTON HANDLERS (context-aware)
        // =

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControlDetails.SelectedTab == tabKhuBien)
                {
                    var et = GetEntityFromUI();
                    if (et == null) return;
                    et.Id = 0;
                    var r = BUS_KhuVucBien.Instance.Them(et);
                    if (r.IsSuccess) { TDCMessageBox.Show("Thêm khu biển thành công!"); LoadData(); }
                    else TDCMessageBox.Show(r.ErrorMessage);
                }
                else
                {
                    var et = GetChoiNghiMatFromUI();
                    if (et == null) return;
                    et.Id = 0;
                    var r = BUS_ChoiNghiMat.Instance.Them(et);
                    if (r.IsSuccess) { TDCMessageBox.Show("Thêm chòi thành công!"); LoadChoiNghiMat(_currentKhuVucBienId); }
                    else TDCMessageBox.Show(r.ErrorMessage);
                }
            }
            catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControlDetails.SelectedTab == tabKhuBien)
                {
                    var et = GetEntityFromUI();
                    if (et == null || et.Id <= 0) return;
                    var r = BUS_KhuVucBien.Instance.Sua(et);
                    if (r.IsSuccess) { TDCMessageBox.Show("Cập nhật khu biển thành công!"); LoadData(); }
                    else TDCMessageBox.Show(r.ErrorMessage);
                }
                else
                {
                    var et = GetChoiNghiMatFromUI();
                    if (et == null || et.Id <= 0) return;
                    var r = BUS_ChoiNghiMat.Instance.Sua(et);
                    if (r.IsSuccess) { TDCMessageBox.Show("Cập nhật chòi thành công!"); LoadChoiNghiMat(_currentKhuVucBienId); }
                    else TDCMessageBox.Show(r.ErrorMessage);
                }
            }
            catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (tabControlDetails.SelectedTab == tabKhuBien)
                {
                    if (gridView.GetFocusedRow() is ET_KhuVucBien focused)
                    {
                        if (TDCMessageBox.Show("Xóa khu vực biển này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            var r = BUS_KhuVucBien.Instance.Xoa(focused.Id);
                            if (r.IsSuccess) LoadData();
                            else TDCMessageBox.Show(r.ErrorMessage);
                        }
                    }
                }
                else
                {
                    if (gridViewChoiNghiMat.GetFocusedRow() is ET_ChoiNghiMat focused)
                    {
                        if (focused.TrangThai == AppConstants.TrangThaiPhong.DangSuDung)
                        {
                            TDCMessageBox.Show("Không thể xóa chòi đang sử dụng!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            return;
                        }
                        if (TDCMessageBox.Show("Xóa chòi này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                        {
                            var r = BUS_ChoiNghiMat.Instance.Xoa(focused.Id);
                            if (r.IsSuccess) LoadChoiNghiMat(_currentKhuVucBienId);
                            else TDCMessageBox.Show(r.ErrorMessage);
                        }
                    }
                }
            }
            catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            if (tabControlDetails.SelectedTab == tabKhuBien)
            {
                LoadData();
                ShowEntityToUI(null);
                txtMaCode.Focus();
            }
            else
            {
                if (_currentKhuVucBienId > 0) LoadChoiNghiMat(_currentKhuVucBienId);
                ShowChoiNghiMatToUI(null);
                txtMaChoi.Focus();
            }
        }

    }
}

