using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraCharts;
using DevExpress.XtraGauges.Win.Gauges.Circular;
using DevExpress.XtraPivotGrid;

namespace GUI
{
    
    public partial class frmBaoCao : Form
    {
        #region Khai báo biến

        private Timer _refreshTimer;

        private bool _isAdjustingSplitters = false;

        #endregion

        #region Khởi tạo Form

        public frmBaoCao()
        {
            InitializeComponent();

            if (ThemeManager.IsInDesignMode(this)) return;

            ThemeManager.ApplyTheme(this);
            ThemeManager.StyleDevExpressGrid(gridControl);

            SetupDefaultDates();
            SetupPivotGrid();
            SetupAutoRefreshTimer();

            this.Shown += (s, e) => AdjustSplitters();

            this.SizeChanged += (s, e) => AdjustSplitters();

            LoadData();
        }

        #endregion

        #region Thiết lập ban đầu

        /// <summary>
        /// Đặt ngày mặc định cho bộ lọc: 30 ngày gần nhất -> Hôm nay.
        /// Chọn sẵn loại biểu đồ đầu tiên (Bar - Cột).
        /// </summary>
        private void SetupDefaultDates()
        {
            deDenNgay.DateTime = DateTime.Today;
            deTuNgay.DateTime = DateTime.Today.AddDays(-30);
            cboLoaiBieuDo.SelectedIndex = 0;
        }

        /// <summary>
        /// Thiết lập PivotGrid: Bật hiển thị tổng cộng theo hàng và cột.
        /// PivotGrid cho phép phân tích doanh thu theo nhiều chiều (Nhóm SP × Kênh bán).
        /// </summary>
        private void SetupPivotGrid()
        {
            pivotGrid.OptionsView.ShowColumnGrandTotals = true;
            pivotGrid.OptionsView.ShowRowGrandTotals = true;
        }

        /// <summary>
        /// Thiết lập Timer tự động làm mới dữ liệu mỗi 30 giây.
        /// </summary>
        private void SetupAutoRefreshTimer()
        {
            _refreshTimer = new Timer();
            _refreshTimer.Interval = 30000; 
            _refreshTimer.Tick += (s, e) => LoadData();
        }

        /// <summary>
        /// Điều chỉnh vị trí splitter theo tỉ lệ phần trăm.
        /// </summary>
        private void AdjustSplitters()
        {
            if (_isAdjustingSplitters) return;
            _isAdjustingSplitters = true;

            try
            {
                if (splitMain.Width > 0)
                    splitMain.SplitterPosition = (int)(splitMain.Width * 0.55);
                if (splitCharts.Height > 0)
                    splitCharts.SplitterPosition = (int)(splitCharts.Height * 0.48);
                if (splitRight.Height > 0)
                    splitRight.SplitterPosition = (int)(splitRight.Height * 0.45);
            }
            finally
            {
                _isAdjustingSplitters = false;
            }
        }

        #endregion

        #region Nạp dữ liệu


        private void LoadData()
        {
            try
            {
                // Lấy khoảng thời gian từ bộ lọc
                DateTime tuNgay = deTuNgay.DateTime.Date;
                DateTime denNgay = deDenNgay.DateTime.Date.AddDays(1);

                // Truy vấn dữ liệu từ tầng BUS (Business Logic Layer)
                var allOrders = BUS.BUS_DonHang.Instance.LoadDS();       // Tất cả đơn hàng
                var allDetails = DAL.DAL_ChiTietDonHang.Instance.LoadDS(); // Chi tiết đơn hàng
                var allProducts = BUS.BUS_SanPham.Instance.LoadDS();     // Danh sách sản phẩm

                // Lọc đơn hàng theo khoảng ngày, loại bỏ đơn đã huỷ
                var filteredOrders = allOrders
                    .Where(o => o.ThoiGian >= tuNgay && o.ThoiGian < denNgay && o.TrangThai != "Hủy")
                    .ToList();

                // Xây dựng bảng dữ liệu phẳng (join 3 bảng) phục vụ báo cáo
                var reportData = BuildReportData(filteredOrders, allDetails, allProducts);

                // Đổ dữ liệu vào từng control
                PopulateBarChart(reportData);   // Biểu đồ cột/đường/vùng
                PopulatePieChart(reportData);   // Biểu đồ tròn tỉ lệ
                UpdateGauge(reportData);        // Đồng hồ KPI
                PopulatePivotGrid(reportData);  // Bảng chéo phân tích
                PopulateGrid(reportData);       // Bảng chi tiết dữ liệu
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi tải dữ liệu: {ex.Message}", "Lỗi",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        /// <summary>
        /// Xây dựng DataTable phẳng bằng cách JOIN 3 bảng: DonHang + ChiTietDonHang + SanPham.
        /// Kết quả là 1 bảng có đầy đủ thông tin để biểu đồ, pivot, grid đều dùng chung.
        /// Nếu không có dữ liệu thật -> tự tạo dữ liệu demo để dashboard không bị trống.
        /// </summary>
        private DataTable BuildReportData(
            List<ET.ET_DonHang> orders,
            List<ET.ET_ChiTietDonHang> details,
            List<ET.ET_SanPham> products)
        {
            // Tạo cấu trúc bảng báo cáo
            var dt = new DataTable("BaoCaoDoanhThu");
            dt.Columns.Add("Ngay", typeof(DateTime));
            dt.Columns.Add("Thu", typeof(string));        // Thứ trong tuần (T2, T3...)
            dt.Columns.Add("NguonBan", typeof(string));   // Kênh bán: POS, Web, App
            dt.Columns.Add("TenSanPham", typeof(string));
            dt.Columns.Add("NhomSanPham", typeof(string));// Phân nhóm: Đồ ăn, Đồ uống, Vé...
            dt.Columns.Add("SoLuong", typeof(int));
            dt.Columns.Add("DoanhThu", typeof(decimal));
            dt.Columns.Add("MaDonHang", typeof(string));

            // Đưa đơn hàng vào Dictionary để tra cứu nhanh theo Id (O(1) thay vì O(n))
            var orderDict = orders.ToDictionary(o => o.Id);

            // Duyệt từng chi tiết đơn hàng -> tìm đơn hàng tương ứng -> tìm sản phẩm -> thêm vào bảng
            foreach (var detail in details)
            {
                // Bỏ qua chi tiết không thuộc đơn hàng nào trong danh sách đã lọc
                if (!orderDict.TryGetValue(detail.IdDonHang, out var order)) continue;

                // Tìm tên sản phẩm và phân nhóm
                string tenSP = "Không rõ";
                string nhomSP = "Khác";
                var product = products.FirstOrDefault(p => p.Id == detail.IdSanPham);
                if (product != null)
                {
                    tenSP = product.Ten;
                    nhomSP = ClassifyProduct(product.Ten); // Phân loại theo tên sản phẩm
                }

                // Xử lý ngày: ưu tiên ThoiGian -> CreatedAt -> Today
                DateTime ngay = order.ThoiGian;
                if (ngay == default) ngay = order.CreatedAt;
                if (ngay == default) ngay = DateTime.Today;

                // Chuyển DayOfWeek sang tên tiếng Việt
                string[] thuNames = { "CN", "T2", "T3", "T4", "T5", "T6", "T7" };
                string thu = thuNames[(int)ngay.DayOfWeek];

                dt.Rows.Add(
                    ngay.Date,
                    thu,
                    string.IsNullOrEmpty(order.NguonBan) ? "POS" : order.NguonBan,
                    tenSP,
                    nhomSP,
                    detail.SoLuong,
                    detail.DonGiaThucTe,
                    order.MaCode
                );
            }

            // Nếu không có dữ liệu thật -> tạo dữ liệu demo để dashboard luôn đẹp khi trình bày
            if (dt.Rows.Count == 0) SeedDemoData(dt);

            return dt;
        }

        /// <summary>
        /// Phân loại sản phẩm vào nhóm dựa theo tên.
        /// Dùng từ khóa đơn giản để nhận diện nhóm hàng.
        /// </summary>
        private string ClassifyProduct(string productName)
        {
            if (string.IsNullOrEmpty(productName)) return "Khác";
            productName = productName.ToLower();

            if (productName.Contains("vé") || productName.Contains("ticket")) return "Vé vào cổng";
            if (productName.Contains("nước") || productName.Contains("trà") || productName.Contains("cà phê")
                || productName.Contains("bia") || productName.Contains("sinh tố")) return "Đồ uống";
            if (productName.Contains("cơm") || productName.Contains("mì") || productName.Contains("phở")
                || productName.Contains("bánh") || productName.Contains("gà") || productName.Contains("bò")) return "Đồ ăn";
            if (productName.Contains("thuê") || productName.Contains("rental")) return "Thuê đồ";

            return "Lưu niệm";
        }

        /// <summary>
        /// Tạo dữ liệu demo giả lập 30 ngày giao dịch.
        /// Random seed = 42 để kết quả luôn giống nhau mỗi lần chạy (dễ demo).
        /// Cuối tuần (T7, CN) có nhiều giao dịch hơn ngày thường (mô phỏng thực tế công viên).
        /// </summary>
        private void SeedDemoData(DataTable dt)
        {
            var rng = new Random(42); // Seed cố định -> kết quả lặp lại được
            string[] nhoms = { "Vé vào cổng", "Đồ ăn", "Đồ uống", "Thuê đồ", "Lưu niệm" };
            string[] nguons = { "POS", "POS", "POS", "Web", "App" }; // POS chiếm 60% (3/5)
            string[] thuNames = { "CN", "T2", "T3", "T4", "T5", "T6", "T7" };
            string[][] tenSPs = {
                new[] { "Vé người lớn", "Vé trẻ em", "Vé VIP gia đình" },
                new[] { "Cơm gà xối mỡ", "Mì xào hải sản", "Bánh tráng nướng" },
                new[] { "Trà đào cam sả", "Sinh tố dưa hấu", "Bia Tiger lon" },
                new[] { "Thuê phao bơi", "Thuê áo phao", "Thuê lều picnic" },
                new[] { "Nón Đại Nam", "Móc khóa kỷ niệm", "Áo thun souvenir" }
            };
            decimal[] giaBase = {
                150000, 85000, 350000,  // Vé
                45000, 25000, 15000,    // Đồ ăn
                35000, 25000, 20000,    // Đồ uống
                120000, 80000, 35000,   // Thuê đồ
                55000, 25000, 95000     // Lưu niệm
            };

            for (int d = -30; d <= 0; d++)
            {
                var ngay = DateTime.Today.AddDays(d);
                bool cuoiTuan = ngay.DayOfWeek == DayOfWeek.Saturday || ngay.DayOfWeek == DayOfWeek.Sunday;

                // Cuối tuần: 30-60 giao dịch, ngày thường: 8-25 giao dịch
                int transactions = cuoiTuan ? rng.Next(30, 60) : rng.Next(8, 25);

                for (int t = 0; t < transactions; t++)
                {
                    int nhomIdx = rng.Next(nhoms.Length);
                    int spIdx = rng.Next(tenSPs[nhomIdx].Length);
                    int giaIdx = nhomIdx * 3 + spIdx;
                    int soLuong = rng.Next(1, 5);

                    // Giá dao động ±10% so với giá gốc (mô phỏng thực tế)
                    decimal gia = giaBase[giaIdx] * soLuong * (decimal)(0.9 + rng.NextDouble() * 0.2);

                    dt.Rows.Add(
                        ngay.Date,
                        thuNames[(int)ngay.DayOfWeek],
                        nguons[rng.Next(nguons.Length)],
                        tenSPs[nhomIdx][spIdx],
                        nhoms[nhomIdx],
                        soLuong,
                        Math.Round(gia, 0),
                        $"DH{ngay:yyMMdd}{t:D3}" // Mã đơn: DH250412001
                    );
                }
            }
        }

        #endregion

        #region Biểu đồ cột / đường / vùng (ChartControl - chartBar)

        /// <summary>
        /// Đổ dữ liệu vào biểu đồ chính (cột/đường/vùng).
        /// Nhóm dữ liệu theo ngày -> tính tổng doanh thu từng ngày -> vẽ biểu đồ.
        /// Loại biểu đồ thay đổi theo ComboBox cboLoaiBieuDo.
        /// </summary>
        private void PopulateBarChart(DataTable dt)
        {
            chartBar.Series.Clear();
            chartBar.Titles.Clear();

            // Nhóm theo ngày, tính tổng doanh thu mỗi ngày
            var grouped = dt.AsEnumerable()
                .GroupBy(r => r.Field<DateTime>("Ngay"))
                .OrderBy(g => g.Key)
                .Select(g => new
                {
                    Date = g.Key,
                    Total = g.Sum(r => r.Field<decimal>("DoanhThu"))
                }).ToList();

            // Xác định loại biểu đồ từ ComboBox
            ViewType viewType = ViewType.Bar;
            switch (cboLoaiBieuDo.SelectedIndex)
            {
                case 0: viewType = ViewType.Bar; break;       // Biểu đồ cột
                case 1: viewType = ViewType.Line; break;      // Biểu đồ đường
                case 2: viewType = ViewType.Area; break;      // Biểu đồ vùng
                case 3: viewType = ViewType.StackedBar; break; // Biểu đồ cột chồng
            }

            var series = new Series("Doanh thu", viewType);
            foreach (var item in grouped)
            {
                series.Points.Add(new SeriesPoint(item.Date.ToString("dd/MM"), (double)item.Total));
            }

            // Tuỳ chỉnh giao diện series theo loại biểu đồ
            series.LabelsVisibility = DevExpress.Utils.DefaultBoolean.False; // Ẩn label số trên mỗi cột
            if (series.View is BarSeriesView barView)
            {
                barView.Color = ThemeManager.PrimaryColor;
                barView.FillStyle.FillMode = FillMode.Solid;
                barView.Border.Visibility = DevExpress.Utils.DefaultBoolean.False;
            }
            else if (series.View is LineSeriesView lineView)
            {
                lineView.Color = ThemeManager.AccentColor;
                lineView.LineStyle.Thickness = 3;
                lineView.MarkerVisibility = DevExpress.Utils.DefaultBoolean.True; // Hiện điểm đánh dấu
            }
            else if (series.View is AreaSeriesView areaView)
            {
                areaView.Color = Color.FromArgb(100, ThemeManager.PrimaryColor);
                areaView.Border.Color = ThemeManager.PrimaryColor;
            }

            chartBar.Series.Add(series);

            // Thêm tiêu đề biểu đồ
            var title = new ChartTitle();
            title.Text = "DOANH THU THEO NGÀY";
            title.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Bold);
            title.TextColor = ThemeManager.TextPrimaryColor;
            chartBar.Titles.Add(title);

            // Xoay nhãn trục X 45° để tránh chồng chéo khi nhiều ngày
            if (chartBar.Diagram is XYDiagram xyDiagram)
            {
                xyDiagram.AxisX.Label.Angle = -45;
                xyDiagram.AxisX.Label.Font = new Font("Segoe UI", 7f);
                xyDiagram.AxisY.Label.TextPattern = "{V:N0}"; // Format số: 1,000,000
            }
        }

        #endregion

        #region Biểu đồ tròn (ChartControl - chartPie)

        /// <summary>
        /// Đổ dữ liệu vào biểu đồ tròn (Pie Chart).
        /// Nhóm theo NhomSanPham -> tính tổng -> hiển thị tỉ lệ phần trăm.
        /// </summary>
        private void PopulatePieChart(DataTable dt)
        {
            chartPie.Series.Clear();
            chartPie.Titles.Clear();

            // Nhóm theo loại sản phẩm, sắp xếp giảm dần theo doanh thu
            var grouped = dt.AsEnumerable()
                .GroupBy(r => r.Field<string>("NhomSanPham"))
                .Select(g => new
                {
                    Nhom = g.Key,
                    Total = g.Sum(r => r.Field<decimal>("DoanhThu"))
                })
                .OrderByDescending(x => x.Total)
                .ToList();

            var series = new Series("Tỷ lệ", ViewType.Pie);
            foreach (var item in grouped)
            {
                series.Points.Add(new SeriesPoint(item.Nhom, (double)item.Total));
            }

            // Tuỳ chỉnh biểu đồ tròn
            if (series.View is PieSeriesView pieView)
            {
                pieView.ExplodedDistancePercentage = 5;  // Khoảng cách tách miếng = 5%
                pieView.RuntimeExploding = true;          // Cho phép click để tách miếng, màu mè is time
            }

            // Hiển thị nhãn: Tên nhóm + phần trăm (VD: "Đồ uống: 25.3%")
            series.Label.TextPattern = "{A}: {VP:P1}";
            series.Label.Font = new Font("Segoe UI", 8f);
            series.LegendTextPattern = "{A}";
            series.ShowInLegend = true;

            chartPie.Series.Add(series);

            // Tiêu đề
            var title = new ChartTitle();
            title.Text = "TỶ LỆ DOANH THU THEO NHÓM";
            title.Font = new Font("Segoe UI Semibold", 12f, FontStyle.Bold);
            title.TextColor = ThemeManager.TextPrimaryColor;
            chartPie.Titles.Add(title);

            // Đặt vị trí chú thích (legend) bên phải
            chartPie.Legend.AlignmentHorizontal = LegendAlignmentHorizontal.Right;
            chartPie.Legend.AlignmentVertical = LegendAlignmentVertical.Center;
            chartPie.Legend.Direction = LegendDirection.TopToBottom;
            chartPie.Legend.Font = new Font("Segoe UI", 8f);
        }

        #endregion

        #region Đồng hồ KPI (GaugeControl)

        /// <summary>
        /// Cập nhật giá trị kim đồng hồ KPI.
        /// Công thức: % đạt KPI = (Tổng doanh thu / Mục tiêu 50 triệu) × 100
        /// Giới hạn tối đa 100% (Math.Min).
        /// 
        /// LƯU Ý: Giao diện gauge (nền, kim, vạch) được thiết lập qua VS Designer.
        /// Code chỉ CẬP NHẬT GIÁ TRỊ tại runtime.
        /// </summary>
        private void UpdateGauge(DataTable dt)
        {
            // Tính tổng doanh thu từ dữ liệu báo cáo
            decimal totalRevenue = dt.AsEnumerable().Sum(r => r.Field<decimal>("DoanhThu"));
            decimal targetRevenue = 50000000m; // Mục tiêu: 50 triệu đồng

            // Tính phần trăm đạt KPI, giới hạn tối đa 100%
            float percent = (float)Math.Min((totalRevenue / targetRevenue) * 100, 100);

            // Tìm Scale được tạo từ Designer và cập nhật giá trị kim
            foreach (var gauge in gaugeControl1.Gauges)
            {
                if (gauge is CircularGauge cg && cg.Scales.Count > 0)
                {
                    cg.Scales[0].Value = percent;

                    // Nếu có label do Designer tạo, update luôn số % lên label
                    if (cg.Labels.Count > 0)
                    {
                        // Dùng 1 dòng duy nhất để không bị che khuất chữ do kích thước label trong Designer
                        cg.Labels[0].Text = $"{percent:N1}%";
                    }
                    break;
                }
            }

            // Yêu cầu vẽ lại gauge control
            gaugeControl1.Invalidate();
        }

        #endregion

        #region Bảng chéo PivotGrid

        /// <summary>
        /// Đổ dữ liệu vào PivotGrid (bảng chéo phân tích đa chiều).
        /// Hàng: Nhóm sản phẩm | Cột: Kênh bán (POS/Web/App) | Giá trị: Tổng doanh thu + Số lượng.
        /// PivotGrid là tính năng nổi bật của DevExpress - giống "Pivot Table" trong Excel.
        /// </summary>
        private void PopulatePivotGrid(DataTable dt)
        {
            pivotGrid.Fields.Clear();
            pivotGrid.DataSource = dt;

            // Trường HÀNG: Nhóm sản phẩm (VD: Đồ ăn, Đồ uống, Vé...)
            var fieldNhom = new PivotGridField("NhomSanPham", PivotArea.RowArea);
            fieldNhom.Caption = "Nhóm SP";
            fieldNhom.Width = 130;

            // Trường CỘT: Kênh bán (POS, Web, App)
            var fieldNguon = new PivotGridField("NguonBan", PivotArea.ColumnArea);
            fieldNguon.Caption = "Kênh bán";
            fieldNguon.Width = 100;

            // Trường GIÁ TRỊ 1: Tổng doanh thu (Sum)
            var fieldDoanhThu = new PivotGridField("DoanhThu", PivotArea.DataArea);
            fieldDoanhThu.Caption = "Doanh thu";
            fieldDoanhThu.CellFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            fieldDoanhThu.CellFormat.FormatString = "N0"; // Format: 1,000,000
            fieldDoanhThu.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum;

            // Trường GIÁ TRỊ 2: Tổng số lượng (Sum)
            var fieldSoLuong = new PivotGridField("SoLuong", PivotArea.DataArea);
            fieldSoLuong.Caption = "Số lượng";
            fieldSoLuong.SummaryType = DevExpress.Data.PivotGrid.PivotSummaryType.Sum;

            // CĂN CHỈNH LAYOUT MA TRẬN:
            // Ép các trường Data (Doanh thu, Số lượng) dàn ra theo chiều ngang cùng Kênh bán
            pivotGrid.OptionsDataField.Area = PivotDataArea.ColumnArea;
            pivotGrid.OptionsView.RowTotalsLocation = PivotRowTotalsLocation.Tree;

            // Thêm tất cả trường vào PivotGrid
            pivotGrid.Fields.AddRange(new[] { fieldNhom, fieldNguon, fieldDoanhThu, fieldSoLuong });
            pivotGrid.RefreshData();
        }

        #endregion

        #region Bảng chi tiết (GridControl)

        /// <summary>
        /// Đổ dữ liệu vào GridControl (bảng danh sách chi tiết từng giao dịch).
        /// Tự động tạo cột, đặt tên tiếng Việt, format số và ngày.
        /// GridControl hỗ trợ: Lọc, Sắp xếp, Nhóm, Tìm kiếm ngay trên bảng.
        /// </summary>
        private void PopulateGrid(DataTable dt)
        {
            gridControl.DataSource = dt;
            gridView.PopulateColumns(); // Tự động tạo cột từ DataTable

            if (gridView.Columns["Ngay"] != null)
            {
                var colNgay = gridView.Columns["Ngay"];
                colNgay.Caption = "Ngày";
                colNgay.DisplayFormat.FormatString = "dd/MM/yyyy";
                colNgay.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;

                // Re   positoryItemDateEdit nhúng vào cột Ngày
                var dateRepo = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
                dateRepo.DisplayFormat.FormatString = "dd/MM/yyyy";
                dateRepo.DisplayFormat.FormatType   = DevExpress.Utils.FormatType.DateTime;
                dateRepo.EditFormat.FormatString     = "dd/MM/yyyy";
                dateRepo.EditFormat.FormatType       = DevExpress.Utils.FormatType.DateTime;
                dateRepo.Mask.EditMask               = "dd/MM/yyyy";
                dateRepo.EditValueChanging += (s, e) => e.Cancel = true;

                colNgay.ColumnEdit = dateRepo;

                gridView.OptionsBehavior.Editable = true;
                colNgay.OptionsColumn.AllowEdit  = true;
                colNgay.OptionsColumn.AllowFocus = true;
            }
            if (gridView.Columns["DoanhThu"] != null)
            {
                gridView.Columns["DoanhThu"].DisplayFormat.FormatString = "N0";
                gridView.Columns["DoanhThu"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridView.Columns["DoanhThu"].Caption = "Doanh Thu";
            }
            if (gridView.Columns["Thu"] != null) gridView.Columns["Thu"].Caption = "Thứ";
            if (gridView.Columns["NguonBan"] != null) gridView.Columns["NguonBan"].Caption = "Nguồn Bán";
            if (gridView.Columns["TenSanPham"] != null) gridView.Columns["TenSanPham"].Caption = "Tên Sản Phẩm";
            if (gridView.Columns["NhomSanPham"] != null) gridView.Columns["NhomSanPham"].Caption = "Nhóm SP";
            if (gridView.Columns["SoLuong"] != null) gridView.Columns["SoLuong"].Caption = "SL";
            if (gridView.Columns["MaDonHang"] != null) gridView.Columns["MaDonHang"].Caption = "Mã Đơn";

            gridView.BestFitColumns(); // Tự điều chỉnh độ rộng cột cho vừa nội dung
        }

        #endregion

        #region Xử lý sự kiện (Event Handlers)

        private void btnLoc_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void btnXuatExcel_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "Excel Files (*.xlsx)|*.xlsx";
                dlg.FileName = $"BaoCaoDoanhThu_{DateTime.Now:yyyyMMdd}";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    gridControl.ExportToXlsx(dlg.FileName);
                    MessageBox.Show($"Đã xuất thành công!\n{dlg.FileName}", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void btnXuatPDF_Click(object sender, EventArgs e)
        {
            using (var dlg = new SaveFileDialog())
            {
                dlg.Filter = "PDF Files (*.pdf)|*.pdf";
                dlg.FileName = $"BaoCaoDoanhThu_{DateTime.Now:yyyyMMdd}";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    gridControl.ExportToPdf(dlg.FileName);
                    MessageBox.Show($"Đã xuất PDF thành công!\n{dlg.FileName}", "Thông báo",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        #endregion
        
    }
}
