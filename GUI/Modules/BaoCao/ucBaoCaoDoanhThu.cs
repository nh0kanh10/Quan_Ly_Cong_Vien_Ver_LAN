using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraPivotGrid;
using DevExpress.XtraCharts;
using DevExpress.Utils;
using BUS.Services.BaoCao;
using ET.DTOs;
using GUI.Infrastructure;

namespace GUI.Modules.BaoCao
{
    public partial class ucBaoCaoDoanhThu : XtraUserControl
    {
        private PivotGridField fieldNgayGiaoDich;
        private PivotGridField fieldMaGiaoDich;
        private PivotGridField fieldTenKhachHang;
        private PivotGridField fieldLoaiGiaoDich;
        private PivotGridField fieldNhomSP;
        private PivotGridField fieldTenSP;
        private PivotGridField fieldDoanhThu;
        private PivotGridField fieldGhiChuChiTiet;
        private PivotGridField fieldNhanVienThuNgan;
        private PivotGridField fieldPhuongThuc;

        private ChartControl _chartControl;

        public ucBaoCaoDoanhThu()
        {
            InitializeComponent();
            SetupPivotGrid();
            SetupChart();
            SetupLanguage();
            RegisterEvents();
        }

        private void SetupPivotGrid()
        {
            fieldNgayGiaoDich = new PivotGridField("NgayGiaoDich", PivotArea.RowArea) { Caption = LanguageManager.GetString("COL_NGAY_GIAO_DICH") };
            fieldNgayGiaoDich.ValueFormat.FormatType = FormatType.DateTime;
            fieldNgayGiaoDich.ValueFormat.FormatString = "dd/MM/yyyy";

            fieldMaGiaoDich = new PivotGridField("MaGiaoDich", PivotArea.FilterArea) { Caption = LanguageManager.GetString("COL_MA_GIAO_DICH") };
            fieldTenKhachHang = new PivotGridField("TenKhachHang", PivotArea.FilterArea) { Caption = LanguageManager.GetString("COL_KHACH_HANG") };
            
            fieldLoaiGiaoDich = new PivotGridField("LoaiGiaoDich", PivotArea.RowArea) { Caption = LanguageManager.GetString("COL_LOAI_GIAO_DICH"), AreaIndex = 0 };
            fieldNhomSP = new PivotGridField("NhomSanPham", PivotArea.RowArea) { Caption = LanguageManager.GetString("COL_NHOM_SAN_PHAM"), AreaIndex = 1 };

            // TenSP và NgayGiaoDich: tắt subtotal — chỉ 1 dòng/nhóm nên
            fieldTenSP = new PivotGridField("TenSanPham", PivotArea.RowArea) { Caption = LanguageManager.GetString("COL_TEN_SAN_PHAM"), AreaIndex = 2, TotalsVisibility = PivotTotalsVisibility.None };
            fieldNgayGiaoDich.TotalsVisibility = PivotTotalsVisibility.None;

            
            fieldDoanhThu = new PivotGridField("DoanhThu", PivotArea.DataArea) { Caption = LanguageManager.GetString("COL_DOANH_THU") };
            fieldDoanhThu.CellFormat.FormatType = FormatType.Numeric;
            fieldDoanhThu.CellFormat.FormatString = "N0";

            fieldGhiChuChiTiet = new PivotGridField("GhiChuChiTiet", PivotArea.FilterArea) { Caption = LanguageManager.GetString("COL_GHI_CHU_CHI_TIET") };
            fieldNhanVienThuNgan = new PivotGridField("NhanVienThuNgan", PivotArea.FilterArea) { Caption = LanguageManager.GetString("COL_NHAN_VIEN_THU_NGAN") };
            fieldPhuongThuc = new PivotGridField("PhuongThucThanhToan", PivotArea.ColumnArea) { Caption = LanguageManager.GetString("COL_PHUONG_THUC") };

            pivotGridControl.Fields.AddRange(new PivotGridField[] {
                fieldNgayGiaoDich, fieldLoaiGiaoDich, fieldNhomSP, fieldTenSP, fieldMaGiaoDich, 
                fieldTenKhachHang, fieldNhanVienThuNgan, fieldDoanhThu,
                fieldPhuongThuc, fieldGhiChuChiTiet
            });

            // Bật tính năng tùy biến  (Excel style)
            pivotGridControl.OptionsCustomization.AllowDrag = true;
            pivotGridControl.OptionsCustomization.AllowDragInCustomizationForm = true;
            pivotGridControl.OptionsCustomization.AllowSort = true;
            pivotGridControl.OptionsCustomization.AllowFilter = true;
            
            pivotGridControl.OptionsView.ShowFilterHeaders = true;
            pivotGridControl.OptionsView.ShowColumnHeaders = true;
            pivotGridControl.OptionsView.ShowDataHeaders = true;
            pivotGridControl.OptionsView.ShowRowHeaders = true;
            
            pivotGridControl.OptionsView.ShowColumnGrandTotals = true;
            pivotGridControl.OptionsView.ShowRowGrandTotals = true;
            pivotGridControl.OptionsView.ShowGrandTotalsForSingleValues = true;
            pivotGridControl.OptionsView.ShowTotalsForSingleValues = true;
            
            // Hiện cửa sổ chọn trường (Field List)
            pivotGridControl.OptionsCustomization.CustomizationFormStyle = DevExpress.XtraPivotGrid.Customization.CustomizationFormStyle.Excel2007;
        }

        private void SetupChart()
        {
            _chartControl = new ChartControl
            {
                Dock      = DockStyle.Top,
                Height    = 220,
                BackColor = System.Drawing.Color.White
            };

            _chartControl.Legend.Visibility = DefaultBoolean.False;

            // Nhúng chart vào UC trước layoutControl
            this.Controls.Add(_chartControl);
            _chartControl.BringToFront();

            layoutControl.Dock   = DockStyle.None;
            layoutControl.Anchor = System.Windows.Forms.AnchorStyles.Top
                                 | System.Windows.Forms.AnchorStyles.Bottom
                                 | System.Windows.Forms.AnchorStyles.Left
                                 | System.Windows.Forms.AnchorStyles.Right;
            this.Resize += (s, e) => AdjustChartLayout();
            AdjustChartLayout();
        }


        private void AdjustChartLayout()
        {
            if (_chartControl == null) return;
            _chartControl.Location = new System.Drawing.Point(0, 0);
            _chartControl.Width    = this.ClientSize.Width;

            layoutControl.Location = new System.Drawing.Point(0, _chartControl.Height);
            layoutControl.Width    = this.ClientSize.Width;
            layoutControl.Height   = this.ClientSize.Height - _chartControl.Height;
        }

        private void RefreshChart(System.Collections.Generic.List<DTO_BaoCaoDoanhThu> data)
        {
            if (_chartControl == null || data == null || data.Count == 0) return;

            var dt = new System.Data.DataTable();
            dt.Columns.Add("Nhom", typeof(string));
            dt.Columns.Add("Tien", typeof(double));

            foreach (var g in data.GroupBy(x => x.NhomSanPham ?? "Khác")
                                   .OrderByDescending(g => g.Sum(x => x.DoanhThu ?? 0)))
            {
                dt.Rows.Add(g.Key, (double)g.Sum(x => x.DoanhThu ?? 0));
            }

            _chartControl.Series.Clear();
            var series = new Series("Doanh Thu", ViewType.Bar)
            {
                DataSource        = dt,
                ArgumentDataMember = "Nhom"
            };
            series.ValueDataMembers.AddRange("Tien");

            if (series.View is BarSeriesView bv)
            {
                bv.FillStyle.FillMode = FillMode.Gradient;
                bv.Border.Visibility  = DefaultBoolean.False;
            }
            _chartControl.Series.Add(series);
            if (_chartControl.Diagram is XYDiagram xy)
            {
                xy.AxisY.Label.TextPattern = "{V:N0}";
                xy.AxisX.Label.Angle       = -30;
                xy.AxisX.Label.Font        = new System.Drawing.Font("Segoe UI", 7f);
            }
        }


        private void SetupLanguage()
        {
            lciTuNgay.Text = LanguageManager.GetString("LBL_TU_NGAY");
            lciDenNgay.Text = LanguageManager.GetString("LBL_DEN_NGAY");
            layoutControlGroupFilters.Text = LanguageManager.GetString("LBL_BO_LOC_DU_LIEU");
            btnLamMoi.Text = LanguageManager.GetString("BTN_LAM_MOI");
            btnXuatExcel.Text = LanguageManager.GetString("BTN_XUAT_EXCEL");
        }

        private void RegisterEvents()
        {
            this.Load += UcBaoCaoDoanhThu_Load;
            btnLamMoi.Click += BtnLamMoi_Click;
            btnXuatExcel.Click += BtnXuatExcel_Click;
        }

        private void UcBaoCaoDoanhThu_Load(object sender, EventArgs e)
        {
            dtTuNgay.DateTime = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            dtDenNgay.DateTime = DateTime.Now;
            LoadData();
        }

        private void BtnLamMoi_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            var result = BUS_BaoCao.Instance.LayBaoCaoDoanhThu(dtTuNgay.DateTime.Date, dtDenNgay.DateTime.Date.AddDays(1).AddTicks(-1));
            if (result.Success)
            {
                decimal total = 0;
                var list = result.Data as System.Collections.Generic.List<DTO_BaoCaoDoanhThu>;
                if (list != null)
                {
                    foreach (var item in list) total += item.DoanhThu ?? 0;
                }

                string title = string.Format(LanguageManager.GetString("LBL_TONG_DOANH_THU"), total);
                layoutControlGroupFilters.Text = title;
                pivotGridControl.DataSource = result.Data;
                pivotGridControl.RefreshData();

                RefreshChart(result.Data as System.Collections.Generic.List<DTO_BaoCaoDoanhThu>);
            }
            else
            {
                XtraMessageBox.Show(LanguageManager.GetString(result.ErrorMessage), "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnXuatExcel_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "Excel Workbook|*.xlsx", FileName = "BaoCaoDoanhThu.xlsx" })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    pivotGridControl.ExportToXlsx(sfd.FileName);
                    XtraMessageBox.Show(LanguageManager.GetString("MSG_XUAT_EXCEL_THANH_CONG"), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
    }
}
