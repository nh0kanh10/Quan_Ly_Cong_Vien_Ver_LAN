using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using ET.Constants;
using GUI.Infrastructure;

namespace GUI.Modules.Kho
{
    public partial class ucLichSu : DevExpress.XtraEditors.XtraUserControl
    {
        public event Action<int> YeuCauXemPhieu;

        public ucLichSu()
        {
            InitializeComponent();
            KhoiTao();
        }

        public void KhoiTao()
        {
            viewLichSu.Columns.Add(new GridColumn { Name = "NgayChungTu", FieldName = "NgayChungTu", Visible = true, DisplayFormat = { FormatType = DevExpress.Utils.FormatType.DateTime, FormatString = "dd/MM/yyyy HH:mm" } });
            viewLichSu.Columns.Add(new GridColumn { Name = "MaChungTu", FieldName = "MaChungTu", Visible = true });
            viewLichSu.Columns.Add(new GridColumn { Name = "LoaiChungTu", FieldName = "LoaiChungTu", Visible = true });
            viewLichSu.Columns.Add(new GridColumn { Name = "TrangThai", FieldName = "TrangThai", Visible = true });
            viewLichSu.Columns.Add(new GridColumn { Name = "GhiChu", FieldName = "GhiChu", Visible = true });

            ThucHienDichNgonNgu();

            dtTuNgay.DateTime = DateTime.Now.AddDays(-30);
            dtDenNgay.DateTime = DateTime.Now;

            viewLichSu.RowStyle += ViewLichSu_RowStyle;
            viewLichSu.CustomColumnDisplayText += ViewLichSu_CustomColumnDisplayText;
            viewLichSu.DoubleClick += ViewLichSu_DoubleClick;

            LoadDuLieu();
        }

        public void ThucHienDichNgonNgu()
        {
            if (viewLichSu.Columns["NgayChungTu"] != null) viewLichSu.Columns["NgayChungTu"].Caption = LanguageManager.GetString("COL_NGAYLAP") ?? "Ngày";
            if (viewLichSu.Columns["MaChungTu"] != null) viewLichSu.Columns["MaChungTu"].Caption = LanguageManager.GetString("COL_MAPHIEU") ?? "Mã Phiếu";
            if (viewLichSu.Columns["LoaiChungTu"] != null) viewLichSu.Columns["LoaiChungTu"].Caption = LanguageManager.GetString("COL_LOAIPHIEU") ?? "Loại";
            if (viewLichSu.Columns["TrangThai"] != null) viewLichSu.Columns["TrangThai"].Caption = LanguageManager.GetString("COL_TRANGTHAI") ?? "Trạng Thái";
            if (viewLichSu.Columns["GhiChu"] != null) viewLichSu.Columns["GhiChu"].Caption = LanguageManager.GetString("LBL_GHICHU") ?? "Ghi Chú";

            lblKho.Text = LanguageManager.GetString("LBL_KHO") ?? "Kho:";
            lblTuNgay.Text = LanguageManager.GetString("LBL_TUNGAY") ?? "Từ Ngày:";
            lblDenNgay.Text = LanguageManager.GetString("LBL_DENNGAY") ?? "Đến Ngày:";
            btnTimKiem.Text = LanguageManager.GetString("BTN_TIMKIEM") ?? "TÌM KIẾM";

            var dsKho = BUS.Services.Kho.BUS_Kho.Instance.GetAllKho(SessionManager.CurrentLanguage);
            slkKho.Properties.DataSource = dsKho;
            slkKho.Properties.NullText = LanguageManager.GetString("COL_KHOXUAT") ?? "-- Chọn --";
            if (slkKho.Properties.View.Columns["TenKho"] != null) slkKho.Properties.View.Columns["TenKho"].Caption = LanguageManager.GetString("COL_TEN") ?? "Tên Kho";
        }

        private void LoadDuLieu()
        {
            try
            {
                DateTime tuNgay = dtTuNgay.DateTime;
                DateTime denNgay = dtDenNgay.DateTime;

                int? idKho = null;
                if (slkKho.EditValue != null && int.TryParse(slkKho.EditValue.ToString(), out int parsedId) && parsedId > 0)
                {
                    idKho = parsedId;
                }

                var data = BUS.Services.Kho.BUS_ChungTuKho.Instance.GetDanhSachChungTu(null, tuNgay, denNgay, idKho);
                gridLichSu.DataSource = data;
            }
            catch (Exception ex)
            {
                UIHelper.Loi(ex.Message);
            }
        }

        private void BtnTimKiem_Click(object sender, EventArgs e)
        {
            if (dtTuNgay.DateTime > dtDenNgay.DateTime)
            {
                UIHelper.Loi(LanguageManager.GetString("ERR_CONSTRAINT_DATE_FROMTO") ?? "\"Từ ngày\" không được lớn hơn \"Đến ngày\"!");
                return;
            }
            LoadDuLieu();
        }

        private void ViewLichSu_CustomColumnDisplayText(object sender, DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventArgs e)
        {
            if (e.Value == null) return;
            string val = e.Value.ToString();
            if (e.Column.FieldName == "LoaiChungTu" || e.Column.FieldName == "TrangThai")
            {
                string translated = LanguageManager.GetString(val);
                if (!string.IsNullOrEmpty(translated) && translated != val)
                    e.DisplayText = translated;
            }
        }

        private void ViewLichSu_RowStyle(object sender, RowStyleEventArgs e)
        {
            var view = sender as GridView;
            if (e.RowHandle < 0) return;

            string status = view.GetRowCellValue(e.RowHandle, "TrangThai")?.ToString();
            if (status == AppConstants.TrangThaiChungTuKho.DaDuyet)
            {
                e.Appearance.ForeColor = Color.FromArgb(76, 175, 80);
            }
            else if (status == AppConstants.TrangThaiChungTuKho.DaHuy)
            {
                e.Appearance.ForeColor = Color.FromArgb(244, 67, 54);
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Strikeout);
            }
        }

        private void ViewLichSu_DoubleClick(object sender, EventArgs e)
        {
            var phieu = viewLichSu.GetFocusedRow() as ET.Models.Kho.ET_ChungTuKho;
            if (phieu != null)
            {
                YeuCauXemPhieu?.Invoke(phieu.Id);
            }
        }
    }
}
