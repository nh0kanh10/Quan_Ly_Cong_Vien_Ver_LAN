using System;
using System.Drawing;
using System.Windows.Forms;
using ET;
using BUS;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmChatLuongNuoc : Form
    {
        public frmChatLuongNuoc()
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);
            SetupBasicEvents();

            cboVeSinh.Items.AddRange(new string[] { "Đạt", "Chấp nhận", "Không đạt", "Cần xử lý" });
            cboVeSinh.SelectedIndex = 0;

            LoadKhuVucBienLookup();
        }

        private void SetupBasicEvents()
        {
            btnThem.Image = IconHelper.GetBitmap(IconChar.Plus, Color.White, 16);
            btnSua.Image = IconHelper.GetBitmap(IconChar.Edit, Color.White, 16);
            btnXoa.Image = IconHelper.GetBitmap(IconChar.Trash, Color.White, 16);
            btnLamMoi.Image = IconHelper.GetBitmap(IconChar.Sync, Color.White, 16);

            // When user selects a beach area, filter the grid
            slkKhuVucBien.EditValueChanged += (s, e) => LoadData();

            this.Load += (s, e) => LoadData();
        }

        private void LoadKhuVucBienLookup()
        {
            var ds = BUS_KhuVucBien.Instance.LoadDS();
            slkKhuVucBien.Properties.DataSource = ds;
            slkKhuVucBien.Properties.DisplayMember = "TenKhuVuc";
            slkKhuVucBien.Properties.ValueMember = "Id";
            slkKhuVucBienView.Columns.Clear();
            slkKhuVucBienView.Columns.AddVisible("MaCode", "Mã KV");
            slkKhuVucBienView.Columns.AddVisible("TenKhuVuc", "Tên Vùng Biển");
        }

        private int GetSelectedKhuVucBienId()
        {
            if (slkKhuVucBien.EditValue != null && slkKhuVucBien.EditValue != DBNull.Value)
                return Convert.ToInt32(slkKhuVucBien.EditValue);
            return 0;
        }

        private void LoadData()
        {
            int idKV = GetSelectedKhuVucBienId();
            if (idKV > 0)
            {
                gridControl.DataSource = BUS_ChatLuongNuoc.Instance.LoadTheoKhuVucBien(idKV);
            }
            else
            {
                gridControl.DataSource = BUS_ChatLuongNuoc.Instance.LoadDS();
            }
            FormatGrid();
        }

        private void FormatGrid()
        {
            if (gridView.Columns["Id"] != null) gridView.Columns["Id"].Visible = false;
            if (gridView.Columns["IdKhuVucBien"] != null) gridView.Columns["IdKhuVucBien"].Visible = false;
            if (gridView.Columns["Ngay"] != null) { gridView.Columns["Ngay"].Caption = "Ngày Đo"; gridView.Columns["Ngay"].DisplayFormat.FormatString = "dd/MM/yyyy"; gridView.Columns["Ngay"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime; }
            if (gridView.Columns["DoMan"] != null) gridView.Columns["DoMan"].Caption = "Độ Mặn (‰)";
            if (gridView.Columns["PH"] != null) gridView.Columns["PH"].Caption = "pH";
            if (gridView.Columns["NhietDo"] != null) gridView.Columns["NhietDo"].Caption = "Nhiệt Độ (°C)";
            if (gridView.Columns["DoTrong"] != null) gridView.Columns["DoTrong"].Caption = "Độ Trong (cm)";
            if (gridView.Columns["TrangThaiVeSinh"] != null) gridView.Columns["TrangThaiVeSinh"].Caption = "Vệ Sinh";

            // Sort by date descending (newest first)
            if (gridView.Columns["Ngay"] != null)
                gridView.Columns["Ngay"].SortOrder = DevExpress.Data.ColumnSortOrder.Descending;

            gridView.BestFitColumns();
        }

        private ET_ChatLuongNuoc GetEntityFromUI()
        {
            int idKV = GetSelectedKhuVucBienId();
            if (idKV <= 0)
            {
                TDCMessageBox.Show("Vui lòng chọn khu vực biển!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            int id = 0;
            if (gridView.GetFocusedRow() is ET_ChatLuongNuoc focused) id = focused.Id;

            return new ET_ChatLuongNuoc
            {
                Id = id,
                IdKhuVucBien = idKV,
                Ngay = dtNgay.DateTime.Date,
                DoMan = spnDoMan.Value,
                PH = spnPH.Value,
                NhietDo = spnNhietDo.Value,
                DoTrong = Convert.ToInt32(spnDoTrong.Value),
                TrangThaiVeSinh = cboVeSinh.Text
            };
        }

        private void ShowEntityToUI(ET_ChatLuongNuoc entity)
        {
            if (entity == null)
            {
                dtNgay.DateTime = DateTime.Today;
                spnDoMan.Value = 0;
                spnPH.Value = 7;
                spnNhietDo.Value = 28;
                spnDoTrong.Value = 100;
                cboVeSinh.SelectedIndex = 0;
                return;
            }

            dtNgay.DateTime = entity.Ngay;
            spnDoMan.Value = entity.DoMan ?? 0;
            spnPH.Value = entity.PH ?? 7;
            spnNhietDo.Value = entity.NhietDo ?? 28;
            spnDoTrong.Value = entity.DoTrong ?? 100;
            cboVeSinh.Text = entity.TrangThaiVeSinh;
        }

        private void gridView_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            if (gridView.GetFocusedRow() is ET_ChatLuongNuoc row)
                ShowEntityToUI(row);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                var et = GetEntityFromUI();
                if (et == null) return;
                et.Id = 0;
                var r = BUS_ChatLuongNuoc.Instance.Them(et);
                if (r.IsSuccess) { TDCMessageBox.Show("Ghi nhận thành công!"); LoadData(); }
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
                var r = BUS_ChatLuongNuoc.Instance.Sua(et);
                if (r.IsSuccess) { TDCMessageBox.Show("Cập nhật thành công!"); LoadData(); }
                else TDCMessageBox.Show(r.ErrorMessage);
            }
            catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridView.GetFocusedRow() is ET_ChatLuongNuoc focused)
                {
                    if (TDCMessageBox.Show("Xóa bản ghi này?", "Xác nhận", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        var r = BUS_ChatLuongNuoc.Instance.Xoa(focused.Id);
                        if (r.IsSuccess) LoadData();
                        else TDCMessageBox.Show(r.ErrorMessage);
                    }
                }
            }
            catch (Exception ex) { TDCMessageBox.Show("Lỗi: " + ex.Message); }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
            ShowEntityToUI(null);
        }
    }
}

