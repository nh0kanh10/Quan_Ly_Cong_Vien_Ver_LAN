using System;
using System.Drawing;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using GUI.Infrastructure;

namespace GUI.Modules.Kho
{
    public partial class ucCanhBao : DevExpress.XtraEditors.XtraUserControl
    {
        public ucCanhBao()
        {
            InitializeComponent();
            KhoiTao();
        }

        public void KhoiTao()
        {
            viewHSD.Columns.Add(new GridColumn { Name = "MaLoHang", FieldName = "MaLoHang", Visible = true });
            viewHSD.Columns.Add(new GridColumn { Name = "TenSanPham", FieldName = "TenSanPham", Visible = true });
            viewHSD.Columns.Add(new GridColumn { Name = "NgayHetHan", FieldName = "NgayHetHan", Visible = true, DisplayFormat = { FormatType = DevExpress.Utils.FormatType.DateTime, FormatString = "dd/MM/yyyy" } });
            viewHSD.Columns.Add(new GridColumn { Name = "SoNgayConLai", FieldName = "SoNgayConLai", Visible = true });
            viewHSD.Columns.Add(new GridColumn { Name = "SoLuongNhap", FieldName = "SoLuongNhap", Visible = true });

            viewTon.Columns.Add(new GridColumn { Name = "MaSanPham", FieldName = "MaSanPham", Visible = true });
            viewTon.Columns.Add(new GridColumn { Name = "TenSanPham", FieldName = "TenSanPham", Visible = true });
            viewTon.Columns.Add(new GridColumn { Name = "TenKho", FieldName = "TenKho", Visible = true });
            viewTon.Columns.Add(new GridColumn { Name = "TonHienTai", FieldName = "TonHienTai", Visible = true });
            viewTon.Columns.Add(new GridColumn { Name = "MucCanhBao", FieldName = "MucCanhBao", Visible = true });
            viewTon.Columns.Add(new GridColumn { Name = "Thieu", FieldName = "Thieu", Visible = true });

            ThucHienDichNgonNgu();

            viewHSD.RowStyle += ViewHSD_RowStyle;
            viewTon.RowStyle += ViewTon_RowStyle;

            LoadDuLieu();
        }

        public void ThucHienDichNgonNgu()
        {
            tabHSD.Text = LanguageManager.GetString("TAB_HSD") ?? "HẠN SỬ DỤNG";
            tabTon.Text = LanguageManager.GetString("TAB_TONTOITHIEU") ?? "TỒN TỐI THIỂU";

            if (viewHSD.Columns["MaLoHang"] != null) viewHSD.Columns["MaLoHang"].Caption = LanguageManager.GetString("COL_LOHANG") ?? "Lô Hàng";
            if (viewHSD.Columns["TenSanPham"] != null) viewHSD.Columns["TenSanPham"].Caption = LanguageManager.GetString("COL_TENHANG") ?? "Tên Sản Phẩm";
            if (viewHSD.Columns["NgayHetHan"] != null) viewHSD.Columns["NgayHetHan"].Caption = LanguageManager.GetString("COL_HANSD") ?? "Hạn SD";
            if (viewHSD.Columns["SoNgayConLai"] != null) viewHSD.Columns["SoNgayConLai"].Caption = LanguageManager.GetString("COL_CONLAI") ?? "Còn Lại (ngày)";
            if (viewHSD.Columns["SoLuongNhap"] != null) viewHSD.Columns["SoLuongNhap"].Caption = LanguageManager.GetString("COL_SOLUONG") ?? "Số Lượng";

            if (viewTon.Columns["MaSanPham"] != null) viewTon.Columns["MaSanPham"].Caption = LanguageManager.GetString("COL_MAHANG") ?? "Mã SP";
            if (viewTon.Columns["TenSanPham"] != null) viewTon.Columns["TenSanPham"].Caption = LanguageManager.GetString("COL_TENHANG") ?? "Tên Sản Phẩm";
            if (viewTon.Columns["TenKho"] != null) viewTon.Columns["TenKho"].Caption = LanguageManager.GetString("COL_KHO") ?? "Kho";
            if (viewTon.Columns["TonHienTai"] != null) viewTon.Columns["TonHienTai"].Caption = LanguageManager.GetString("COL_TONKHO") ?? "Tồn Kho";
            if (viewTon.Columns["MucCanhBao"] != null) viewTon.Columns["MucCanhBao"].Caption = LanguageManager.GetString("COL_MUCCANHBAO") ?? "Mức C.Báo";
            if (viewTon.Columns["Thieu"] != null) viewTon.Columns["Thieu"].Caption = LanguageManager.GetString("COL_THIEU") ?? "THIẾU";
        }

        private void LoadDuLieu(int soNgayHSD = 7)
        {
            try
            {
                var dsHSD = BUS.Services.Kho.BUS_LoHang.Instance.GetLoSapHetHan(soNgayHSD);
                gridHSD.DataSource = dsHSD;

                var dsTon = BUS.Services.Kho.BUS_SoCai.Instance.GetCanhBaoTonToiThieu();
                gridTon.DataSource = dsTon;

                if (lblSummaryHSD != null)
                    lblSummaryHSD.Text = $"{dsHSD.Count} lô sắp hết HSD trong {soNgayHSD} ngày";
                if (lblSummaryTon != null)
                    lblSummaryTon.Text = $"{dsTon.Count} mặt hàng dưới mức tối thiểu";
            }
            catch (Exception ex)
            {
                GUI.Infrastructure.UIHelper.Loi(ex.Message);
            }
        }

        private void ViewHSD_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                int.TryParse(view.GetRowCellValue(e.RowHandle, "SoNgayConLai")?.ToString(), out int conLai);
                if (conLai <= 3) {
                    e.Appearance.BackColor = Color.FromArgb(255, 235, 238); // Đỏ
                    e.Appearance.ForeColor = Color.DarkRed;
                } else if (conLai <= 7) {
                    e.Appearance.BackColor = Color.FromArgb(255, 243, 224); // Vàng
                    e.Appearance.ForeColor = Color.DarkOrange;
                }
            }
        }

        private void ViewTon_RowStyle(object sender, RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                // chỉ tô đỏ khi thực sự thiếu (Thieu > 0)
                decimal.TryParse(view.GetRowCellValue(e.RowHandle, "Thieu")?.ToString(), out decimal thieu);
                if (thieu > 0)
                {
                    e.Appearance.BackColor = Color.FromArgb(255, 235, 238);
                    e.Appearance.ForeColor = Color.DarkRed;
                }
            }
        }

        public void ReloadWithDays(int soNgay)
        {
            LoadDuLieu(soNgay);
        }
    }
}
