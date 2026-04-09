using System;
using System.Drawing;
using System.Windows.Forms;
using ET;
using BUS;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmDongVat : Form
    {
        public frmDongVat()
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);
            ThemeManager.StyleDevExpressGrid(gridControlLCA);
            SetupBasicEvents();

            cboSucKhoe.Items.AddRange(new string[] { "Khỏe mạnh", "Đang điều trị", "Cách ly", "Yếu" });
            cboSucKhoe.SelectedIndex = 0;
        }

        private void SetupBasicEvents()
        {
            txtTimKiem.TextChanged += (s, e) => gridView.ApplyFindFilter(txtTimKiem.Text);

            btnThem.Image = IconHelper.GetBitmap(IconChar.Plus, Color.White, 16);
            btnSua.Image = IconHelper.GetBitmap(IconChar.Edit, Color.White, 16);
            btnXoa.Image = IconHelper.GetBitmap(IconChar.Trash, Color.White, 16);
            btnLamMoi.Image = IconHelper.GetBitmap(IconChar.Sync, Color.White, 16);

            this.Load += (s, e) => LoadData();
        }

        private void LoadData()
        {
            gridControl.DataSource = BUS_DongVat.Instance.LoadDS();
            FormatGrid();
        }

        private void FormatGrid()
        {
            if (gridView.Columns["Id"] != null) gridView.Columns["Id"].Visible = false;
            if (gridView.Columns["Ten"] != null) gridView.Columns["Ten"].Caption = "Tên Động Vật";
            if (gridView.Columns["Loai"] != null) gridView.Columns["Loai"].Caption = "Loài";
            if (gridView.Columns["NgaySinh"] != null) { gridView.Columns["NgaySinh"].Caption = "Ngày Sinh"; gridView.Columns["NgaySinh"].DisplayFormat.FormatString = "dd/MM/yyyy"; gridView.Columns["NgaySinh"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime; }
            if (gridView.Columns["TinhTrangSucKhoe"] != null) gridView.Columns["TinhTrangSucKhoe"].Caption = "Sức Khỏe";
            if (gridView.Columns["MoTa"] != null) gridView.Columns["MoTa"].Caption = "Mô Tả";
            gridView.BestFitColumns();
        }

        private ET_DongVat GetEntityFromUI()
        {
            if (string.IsNullOrWhiteSpace(txtTen.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập tên động vật!", "Thông báo");
                txtTen.Focus();
                return null;
            }
            if (string.IsNullOrWhiteSpace(txtLoai.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập loài!", "Thông báo");
                txtLoai.Focus();
                return null;
            }

            int id = 0;
            if (gridView.GetFocusedRow() is ET_DongVat focused) id = focused.Id;

            DateTime? ngaySinh = null;
            if (dtNgaySinh.EditValue != null)
            {
                if (dtNgaySinh.DateTime.Date > DateTime.Today)
                {
                    TDCMessageBox.Show("Ngày sinh không thể là ngày tương lai!", "Thông báo");
                    return null;
                }
                ngaySinh = dtNgaySinh.DateTime;
            }

            return new ET_DongVat
            {
                Id = id,
                Ten = txtTen.Text,
                Loai = txtLoai.Text,
                MoTa = txtMoTa.Text,
                NgaySinh = ngaySinh,
                TinhTrangSucKhoe = cboSucKhoe.Text
            };
        }

        private void ShowEntityToUI(ET_DongVat entity)
        {
            if (entity == null)
            {
                txtTen.Text = "";
                txtLoai.Text = "";
                txtMoTa.Text = "";
                dtNgaySinh.EditValue = null;
                cboSucKhoe.SelectedIndex = 0;
                return;
            }

            txtTen.Text = entity.Ten;
            txtLoai.Text = entity.Loai;
            txtMoTa.Text = entity.MoTa;
            if (entity.NgaySinh.HasValue)
            {
                dtNgaySinh.DateTime = entity.NgaySinh.Value;
                // dtNgaySinh.Checked = true;
            }
            else
            {
                dtNgaySinh.EditValue = null;
            }
            cboSucKhoe.Text = entity.TinhTrangSucKhoe;
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView.GetFocusedRow() is ET_DongVat row)
            {
                ShowEntityToUI(row);
                LoadLichChoAn(row.Id);
            }
        }

        private int _currentDongVatId = 0;

        private void LoadLichChoAn(int idDongVat)
        {
            _currentDongVatId = idDongVat;
            var ds = BUS_LichChoAn.Instance.LoadTheoDongVat(idDongVat);
            gridControlLCA.DataSource = ds;
            gridViewLCA.PopulateColumns();

            if (gridViewLCA.Columns["Id"] != null) gridViewLCA.Columns["Id"].Visible = false;
            if (gridViewLCA.Columns["IdDongVat"] != null) gridViewLCA.Columns["IdDongVat"].Visible = false;
            if (gridViewLCA.Columns["ThoiGian"] != null)
            {
                gridViewLCA.Columns["ThoiGian"].Caption = "Thời Gian";
                gridViewLCA.Columns["ThoiGian"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                gridViewLCA.Columns["ThoiGian"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            }
            if (gridViewLCA.Columns["ThucAn"] != null) gridViewLCA.Columns["ThucAn"].Caption = "Thức Ăn";
            if (gridViewLCA.Columns["NguoiPhuTrach"] != null) gridViewLCA.Columns["NguoiPhuTrach"].Caption = "NV Phụ Trách";
            gridViewLCA.BestFitColumns();

            pnlLichChoAn.Text = string.Format("Lịch Cho Ăn — {0} bản ghi", ds.Count);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                var et = GetEntityFromUI();
                if (et == null) return;
                et.Id = 0;
                var r = BUS_DongVat.Instance.Them(et);
                if (r.IsSuccess) { TDCMessageBox.Show("Thêm thành công!"); LoadData(); }
                else TDCMessageBox.Show(r.ErrorMessage);
            }
            catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                var et = GetEntityFromUI();
                if (et == null || et.Id <= 0) return;
                var r = BUS_DongVat.Instance.Sua(et);
                if (r.IsSuccess) { TDCMessageBox.Show("Cập nhật thành công!"); LoadData(); }
                else TDCMessageBox.Show(r.ErrorMessage);
            }
            catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView.GetFocusedRow() is ET_DongVat focused)
                {
                    if (TDCMessageBox.Show("Xóa động vật này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var r = BUS_DongVat.Instance.Xoa(focused.Id);
                        if (r.IsSuccess) LoadData();
                        else TDCMessageBox.Show(r.ErrorMessage);
                    }
                }
            }
            catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            txtTimKiem.Clear();
            LoadData();
            gridView.ClearFindFilter();
            ShowEntityToUI(null);
            txtTen.Focus();
        }
    }
}

