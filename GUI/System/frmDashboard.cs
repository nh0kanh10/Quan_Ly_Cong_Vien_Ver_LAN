using System;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using BUS; // BUS_Dashboard disbanded but still need BUS for others
using Guna.UI2.WinForms;
using FontAwesome.Sharp;
using ET;

namespace GUI
{
    public partial class frmDashboard : Form, IBaseForm
    {
        // private BUS_Dashboard busDB = new BUS_Dashboard();

        public frmDashboard()
        {
            InitializeComponent();
            
            InitIcons();
            ApplyStyles();
            ApplyPermissions();

            SetupUI();
        }

        private void txtSearchInvoice_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) btnFilter_PerformClick();
        }

        private void frmDashboard_Resize(object sender, EventArgs e)
        {
            ArrangeCards();
        }

        public void ApplyPermissions()
        {
            var tk = ET.SessionManager.CurrentUser;
            if (tk == null) return;

            // Dashboard usually visible to all, but certain cards might be gated
            if (!BUS_QuyenHan.Instance.HasPermission(tk.IdVaiTro, "VIEW_REPORT"))
            {
                // If no report view, maybe disable private revenue cards
                cardRevenue.Visible = false;
            }
        }

        public void ApplyStyles()
        {
            ThemeManager.ApplyTheme(this);
            StyleDashboardCards();
        }

        public void InitIcons()
        {
            txtSearchInvoice.IconLeft = IconHelper.GetBitmap(IconChar.Search, Color.Gray, 18);
            btnFilter.Image = IconHelper.GetBitmap(IconChar.Filter, Color.White, 18);
        }


        private void SetupUI()
        {
            // Mặc định lọc tháng hiện tại
            dtpFrom.DateTime = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            dtpTo.DateTime = DateTime.Today;

            ArrangeCards();
            LoadData();
        }

        private void StyleDashboardCards()
        {
            cardRevenue.FillColor = ThemeManager.CardBlue1;
            cardRevenue.BorderRadius = 15;
            lblRevenue.ForeColor = Color.White;
            lblRevenueTitle.ForeColor = Color.FromArgb(200, 255, 255, 255);
            cardVisitors.FillColor = ThemeManager.CardGreen1;
            cardVisitors.BorderRadius = 15;
            lblVisitors.ForeColor = Color.White;
            lblVisitorsTitle.ForeColor = Color.FromArgb(200, 255, 255, 255);
            cardInvoices.FillColor = ThemeManager.CardOrange1;
            cardInvoices.BorderRadius = 15;
            lblInvoices.ForeColor = Color.White;
            lblInvoicesTitle.ForeColor = Color.FromArgb(200, 255, 255, 255);
            cardVisitorsMonth.FillColor = ThemeManager.CardViolet1;
            cardVisitorsMonth.BorderRadius = 15;
            lblVisitorsMonth.ForeColor = Color.White;
            lblVisitorsMonthTitle.ForeColor = Color.FromArgb(200, 255, 255, 255);

            AddCardIcon(cardRevenue, IconChar.Coins);
            AddCardIcon(cardVisitors, IconChar.TicketAlt);
            AddCardIcon(cardInvoices, IconChar.FileInvoiceDollar);
            AddCardIcon(cardVisitorsMonth, IconChar.Users);
        }

        private void AddCardIcon(Guna.UI2.WinForms.Guna2Panel card, IconChar icon)
        {
            PictureBox pb = new PictureBox();
            pb.Image = IconHelper.GetBitmap(icon, Color.FromArgb(60, 255, 255, 255), 60);
            pb.Size = new Size(60, 60);
            pb.SizeMode = PictureBoxSizeMode.Zoom;
            pb.BackColor = Color.Transparent;
            pb.Name = "icon_" + card.Name;
            
            card.Controls.Add(pb);
            pb.BringToFront();           
            pb.Location = new Point(card.Width - 75, (card.Height - 60) / 2);         
            card.SizeChanged += (s, e) => {
                pb.Location = new Point(card.Width - 75, (card.Height - 60) / 2);
            };
        }

        private void ArrangeCards()
        {
            if (pnlCards == null || pnlCards.Width < 100) return;

            int totalWidth = pnlCards.Width;
            int gap = 20;
            int cardWidth = (totalWidth - gap * 3) / 4;
            int cardHeight = 120;
            int y = 10;

            cardRevenue.SetBounds(0, y, cardWidth, cardHeight);
            cardVisitors.SetBounds(cardWidth + gap, y, cardWidth, cardHeight);
            cardInvoices.SetBounds((cardWidth + gap) * 2, y, cardWidth, cardHeight);
            cardVisitorsMonth.SetBounds((cardWidth + gap) * 3, y, cardWidth, cardHeight);
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
             ArrangeCards();
             LoadData();
        }

        public void LoadData()
        {
            LoadDataWithFilter(dtpFrom.DateTime.Date, dtpTo.DateTime.Date.AddDays(1).AddSeconds(-1));
        }

        private void LoadDataWithFilter(DateTime from, DateTime to)
        {
            try
            {
                var dsDH = BUS_DonHang.Instance.LoadDS()
                    .Where(x => x.ThoiGian >= from && x.ThoiGian <= to)
                    .ToList();

                // Card 1: Doanh thu trong khoảng
                decimal revenue = dsDH.Where(x => x.TrangThai == AppConstants.TrangThaiDonHang.DaThanhToan).Sum(x => x.TongTien);
                lblRevenue.Text = revenue.ToString("N0") + " đ";

                // Card 2: Tổng vé đã phát (VeDienTu created trong khoảng)
                var dsVe = BUS_VeDienTu.Instance.LoadDS()
                    .Where(x => x.CreatedAt >= from && x.CreatedAt <= to)
                    .ToList();
                lblVisitors.Text = dsVe.Count.ToString();

                // Card 3: Số đơn hàng
                lblInvoices.Text = dsDH.Count.ToString();

                // Card 4: Khách hàng (unique)
                int uniqueKH = dsDH.Where(x => x.IdKhachHang.HasValue).Select(x => x.IdKhachHang.Value).Distinct().Count();
                lblVisitorsMonth.Text = uniqueKH.ToString();

                // Grid: Hóa đơn gần nhất
                dgvRecentInvoices.ForceInitialize();
                dgvRecentInvoices.DataSource = dsDH.OrderByDescending(x => x.ThoiGian).Take(20).ToList();
                gridViewRecentInvoices.PopulateColumns();
                FormatInvoiceGrid();
            }
            catch { }
        }

        private void FormatInvoiceGrid()
        {
            var view = gridViewRecentInvoices;
            if (view.Columns.Count == 0) return;

            if (view.Columns["Id"] != null) view.Columns["Id"].Visible = false;
            if (view.Columns["MaCode"] != null) view.Columns["MaCode"].Caption = "Mã HĐ";
            if (view.Columns["TongTien"] != null)
            {
                view.Columns["TongTien"].Caption = "Thành Tiền";
                view.Columns["TongTien"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                view.Columns["TongTien"].DisplayFormat.FormatString = "N0";
            }
            if (view.Columns["ThoiGian"] != null)
            {
                view.Columns["ThoiGian"].Caption = "Ngày Lập";
                view.Columns["ThoiGian"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
                view.Columns["ThoiGian"].DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            }
            if (view.Columns["TrangThai"] != null) view.Columns["TrangThai"].Caption = "Trạng Thái";

            string[] hidden = { "IdKhachHang", "IdDoan", "IdKhuyenMai", "TienGiamGia", "GhiChu", "CreatedAt", "CreatedBy" };
            foreach (var col in hidden) if (view.Columns[col] != null) view.Columns[col].Visible = false;
            
            view.BestFitColumns();
        }

        private void gridViewRecentInvoices_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null || e.Column.FieldName != "TrangThai") return;

            object objStatus = view.GetRowCellValue(e.RowHandle, "TrangThai");
            string status = (objStatus != null) ? objStatus.ToString() : "";
            
            if (status.Contains("DaThanhToan") || status.Contains("Đã thanh toán"))
            {
                e.Appearance.ForeColor = Color.FromArgb(16, 185, 129);
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else if (status.Contains("Hu") || status.Contains("Hủy"))
            {
                e.Appearance.ForeColor = Color.FromArgb(239, 68, 68);
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
            else
            {
                e.Appearance.ForeColor = Color.FromArgb(245, 158, 11);
                e.Appearance.Font = new Font(e.Appearance.Font, FontStyle.Bold);
            }
        }

        private void FormatMonthlySummary()
        {
            if (gridViewMonthlySummary.Columns["Doanh Thu"] != null)
            {
                gridViewMonthlySummary.Columns["Doanh Thu"].DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
                gridViewMonthlySummary.Columns["Doanh Thu"].DisplayFormat.FormatString = "N0";
                gridViewMonthlySummary.Columns["Doanh Thu"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            }
            if (gridViewMonthlySummary.Columns["Số HĐ"] != null)
            {
                gridViewMonthlySummary.Columns["Số HĐ"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
            if (gridViewMonthlySummary.Columns["Số Vé"] != null)
            {
                gridViewMonthlySummary.Columns["Số Vé"].AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            }
        }

        private void btnFilter_Click(object sender, EventArgs e)
        {
            btnFilter_PerformClick();
        }

        private void btnFilter_PerformClick()
        {
            LoadDataWithFilter(dtpFrom.DateTime.Date, dtpTo.DateTime.Date.AddDays(1).AddSeconds(-1));
        }

        private void gridViewRecentInvoices_DoubleClick(object sender, EventArgs e)
        {
            var view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view == null || view.FocusedRowHandle < 0) return;

            object objMaHD = view.GetFocusedRowCellValue("Mã HD");
            string maCode = (objMaHD != null) ? objMaHD.ToString() : null;
            if (string.IsNullOrEmpty(maCode)) return;

            using (var frmDetails = new frmChiTietHoaDon(maCode))
            {
                if (ThemeManager.ShowAsPopup(frmDetails) == DialogResult.OK)
                {
                    btnFilter_PerformClick();
                }
            }
        }
    }
}




