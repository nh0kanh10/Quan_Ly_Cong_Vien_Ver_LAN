using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using BUS.Services.HeThong;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using ET.DTOs;
using GUI.Infrastructure;

namespace GUI.Modules.HeThong
{
    public partial class frmNhatKyThaoTac : XtraForm
    {
        private class DiffRow
        {
            public string Field    { get; set; }
            public string OldValue { get; set; }
            public string NewValue { get; set; }
            public bool   Changed  => OldValue != NewValue;
        }

        #region Khởi tạo

        public frmNhatKyThaoTac()
        {
            InitializeComponent();
            ApplyLanguage();
            KhoiTaoMacDinh();
        }

        private void ApplyLanguage()
        {
            this.Text          = LanguageManager.GetString("FRM_NHATKY_TITLE") ?? "Nhật ký thao tác hệ thống";
            lblTuNgay.Text     = LanguageManager.GetString("LBL_TUNGAY")        ?? "Từ ngày:";
            lblDenNgay.Text    = LanguageManager.GetString("LBL_DENNGAY")       ?? "Đến ngày:";
            lblModule.Text     = LanguageManager.GetString("LBL_NHATKY_MODULE") ?? "Module:";
            btnTimKiem.Text    = LanguageManager.GetString("BTN_TIMKIEM")       ?? "TÌM KIẾM";
            btnXuatExcel.Text  = LanguageManager.GetString("BTN_EXCEL")         ?? "Xuất Excel";
            lblDetailTitle.Text = "Chi tiết thay đổi — chọn một dòng log phía trên";

            colThoiGian.Caption      = LanguageManager.GetString("COL_NHATKY_THOIGIAN")  ?? "Thời gian";
            colNguoiThucHien.Caption = LanguageManager.GetString("COL_NHATKY_NGUOI")     ?? "Người thực hiện";
            colThucThe.Caption       = LanguageManager.GetString("COL_NHATKY_MODULE")    ?? "Module";
            colHanhDong.Caption      = LanguageManager.GetString("COL_NHATKY_HANHDONG")  ?? "Hành động";
            colIdThucThe.Caption     = "ID";
            colGhiChu.Caption        = LanguageManager.GetString("COL_NHATKY_GHICHU")   ?? "Ghi chú";

            colDiffField.Caption = LanguageManager.GetString("COL_DIFF_FIELD")   ?? "Thuộc tính";
            colDiffOld.Caption   = LanguageManager.GetString("COL_DIFF_OLD")     ?? "Giá trị cũ";
            colDiffNew.Caption   = LanguageManager.GetString("COL_DIFF_NEW")     ?? "Giá trị mới";
        }

        private void KhoiTaoMacDinh()
        {
            dtTuNgay.EditValue  = DateTime.Today;
            dtDenNgay.EditValue = DateTime.Today;

            cboModule.Properties.Items.Clear();
            cboModule.Properties.Items.Add(LanguageManager.GetString("FILTER_ALL") ?? "Tất cả");

            var rs = BUS_NhatKy.Instance.LayDanhSachModule();
            if (rs.Success && rs.Data is List<string> modules)
                foreach (var m in modules)
                    cboModule.Properties.Items.Add(m);

            cboModule.SelectedIndex = 0;
            TimKiem();
        }

        #endregion

        #region Sự kiện

        private void BtnTimKiem_Click(object sender, EventArgs e) => TimKiem();

        private void BtnXuatExcel_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter   = "Excel Files (*.xlsx)|*.xlsx";
                dlg.FileName = $"NhatKy_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    gridViewLog.ExportToXlsx(dlg.FileName);
                    XtraMessageBox.Show(
                        LanguageManager.GetString("MSG_EXPORT_OK") ?? "Xuất file thành công!",
                        LanguageManager.GetString("TITLE_THONGBAO") ?? "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void GridViewLog_FocusedRowChanged(object sender, FocusedRowChangedEventArgs e)
        {
            var row = gridViewLog.GetFocusedRow() as DTO_NhatKyView;
            if (row == null)
            {
                gridDiff.DataSource = null;
                lblDetailTitle.Text = "Chi tiết thay đổi — chọn một dòng log phía trên";
                return;
            }

            lblDetailTitle.Text = $"{row.HanhDong}  ·  Module: {row.ThucThe}  ·  ID: {row.IdThucThe}  ·  {row.ThoiGian:dd/MM/yyyy HH:mm}";
            gridDiff.DataSource = ParseDiff(row.GiaTriCu, row.GiaTriMoi);
            gridViewDiff.BestFitColumns();
        }

        
        private void GridViewDiff_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            var diff = gridViewDiff.GetRow(e.RowHandle) as DiffRow;
            if (diff == null || !diff.Changed) return;

            e.Appearance.BackColor = Color.FromArgb(255, 251, 204); 
            if (e.Column.FieldName == "OldValue")
                e.Appearance.ForeColor = Color.FromArgb(160, 50, 50); 
            else if (e.Column.FieldName == "NewValue")
                e.Appearance.ForeColor = Color.FromArgb(30, 120, 60); 
        }

        #endregion

        #region Nghiệp vụ

        private void TimKiem()
        {
            if (!(dtTuNgay.EditValue is DateTime tuNgay) || !(dtDenNgay.EditValue is DateTime denNgay))
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString("ERR_FORMAT_DATE") ?? "Định dạng ngày không hợp lệ!",
                    LanguageManager.GetString("TITLE_LOI") ?? "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (tuNgay > denNgay)
            {
                XtraMessageBox.Show(
                    LanguageManager.GetString("ERR_CONSTRAINT_DATE_FROMTO") ?? "\"Từ ngày\" không được lớn hơn \"Đến ngày\"!",
                    LanguageManager.GetString("TITLE_LOI") ?? "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string module = cboModule.SelectedIndex > 0 ? cboModule.Text : "";

            var rs = BUS_NhatKy.Instance.TraCuuLog(tuNgay, denNgay, module);
            if (rs.Success && rs.Data is List<DTO_NhatKyView> data)
            {
                gridLog.DataSource = data;
                lblSoDong.Text = string.Format(
                    LanguageManager.GetString("LBL_NHATKY_SODONG") ?? "Hiển thị: {0} dòng", data.Count);

                gridViewLog.ExpandAllGroups();
            }
            else
            {
                gridLog.DataSource = null;
                lblSoDong.Text = "";
                if (!rs.Success)
                    XtraMessageBox.Show(rs.Message,
                        LanguageManager.GetString("TITLE_LOI") ?? "Lỗi",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            gridDiff.DataSource = null;
            lblDetailTitle.Text = "Chi tiết thay đổi — chọn một dòng log phía trên";
        }


        private static List<DiffRow> ParseDiff(string giaTriCu, string giaTriMoi)
        {
            string src = !string.IsNullOrWhiteSpace(giaTriCu) ? giaTriCu : giaTriMoi;
            if (string.IsNullOrWhiteSpace(src))
            {
                return new List<DiffRow>
                {
                    new DiffRow { Field = "(raw)", OldValue = giaTriCu ?? "", NewValue = giaTriMoi ?? "" }
                };
            }

            if (!src.Contains('|') && !src.Contains(':'))
            {
                return new List<DiffRow>
                {
                    new DiffRow { Field = "Chi tiết", OldValue = giaTriCu ?? "", NewValue = giaTriMoi ?? "" }
                };
            }

            var cuParts  = SplitKeyValue(giaTriCu);
            var moiParts = SplitKeyValue(giaTriMoi);

            // Gộp tất cả key từ cả 2 phía
            var allKeys = cuParts.Keys.Union(moiParts.Keys).ToList();

            return allKeys.Select(k => new DiffRow
            {
                Field    = k,
                OldValue = cuParts.ContainsKey(k)  ? cuParts[k]  : "",
                NewValue = moiParts.ContainsKey(k) ? moiParts[k] : ""
            }).ToList();
        }

        // "Tên: ABC | Giá: 500,000" → { "Tên": "ABC", "Giá": "500,000" }
        private static Dictionary<string, string> SplitKeyValue(string raw)
        {
            var result = new Dictionary<string, string>();
            if (string.IsNullOrWhiteSpace(raw)) return result;

            foreach (var part in raw.Split('|'))
            {
                int colonIdx = part.IndexOf(':');
                if (colonIdx <= 0) continue;
                string key = part.Substring(0, colonIdx).Trim();
                string val = part.Substring(colonIdx + 1).Trim();
                if (!string.IsNullOrEmpty(key))
                    result[key] = val;
            }
            return result;
        }

        #endregion
    }
}
