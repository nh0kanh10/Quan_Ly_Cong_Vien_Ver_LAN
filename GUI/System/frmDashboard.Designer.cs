namespace GUI
{
    partial class frmDashboard
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlFilter = new Guna.UI2.WinForms.Guna2Panel();
            this.btnFilter = new Guna.UI2.WinForms.Guna2Button();
            this.cboInvoiceStatus = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtSearchInvoice = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpTo = new DevExpress.XtraEditors.DateEdit();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpFrom = new DevExpress.XtraEditors.DateEdit();
            this.lblFrom = new System.Windows.Forms.Label();
            this.lblFilterTitle = new System.Windows.Forms.Label();
            this.pnlCards = new Guna.UI2.WinForms.Guna2Panel();
            this.cardVisitorsMonth = new Guna.UI2.WinForms.Guna2Panel();
            this.lblVisitorsMonth = new System.Windows.Forms.Label();
            this.lblVisitorsMonthTitle = new System.Windows.Forms.Label();
            this.cardInvoices = new Guna.UI2.WinForms.Guna2Panel();
            this.lblInvoices = new System.Windows.Forms.Label();
            this.lblInvoicesTitle = new System.Windows.Forms.Label();
            this.cardVisitors = new Guna.UI2.WinForms.Guna2Panel();
            this.lblVisitors = new System.Windows.Forms.Label();
            this.lblVisitorsTitle = new System.Windows.Forms.Label();
            this.cardRevenue = new Guna.UI2.WinForms.Guna2Panel();
            this.lblRevenue = new System.Windows.Forms.Label();
            this.lblRevenueTitle = new System.Windows.Forms.Label();
            this.pnlContent = new System.Windows.Forms.Panel();
            this.pnlGridMain = new Guna.UI2.WinForms.Guna2Panel();
            this.gbInvoices = new Guna.UI2.WinForms.Guna2GroupBox();
            this.dgvRecentInvoices = new DevExpress.XtraGrid.GridControl();
            this.gridViewRecentInvoices = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlSummary = new Guna.UI2.WinForms.Guna2Panel();
            this.gbSummary = new Guna.UI2.WinForms.Guna2GroupBox();
            this.dgvMonthlySummary = new DevExpress.XtraGrid.GridControl();
            this.gridViewMonthlySummary = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlFilter.SuspendLayout();
            this.pnlCards.SuspendLayout();
            this.cardVisitorsMonth.SuspendLayout();
            this.cardInvoices.SuspendLayout();
            this.cardVisitors.SuspendLayout();
            this.cardRevenue.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlGridMain.SuspendLayout();
            this.gbInvoices.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentInvoices)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRecentInvoices)).BeginInit();
            this.pnlSummary.SuspendLayout();
            this.gbSummary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthlySummary)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMonthlySummary)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.btnFilter);
            this.pnlFilter.Controls.Add(this.cboInvoiceStatus);
            this.pnlFilter.Controls.Add(this.txtSearchInvoice);
            this.pnlFilter.Controls.Add(this.dtpTo);
            this.pnlFilter.Controls.Add(this.lblTo);
            this.pnlFilter.Controls.Add(this.dtpFrom);
            this.pnlFilter.Controls.Add(this.lblFrom);
            this.pnlFilter.Controls.Add(this.lblFilterTitle);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(15, 15);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlFilter.Size = new System.Drawing.Size(1370, 55);
            this.pnlFilter.TabIndex = 0;
            // 
            // btnFilter
            // 
            this.btnFilter.Location = new System.Drawing.Point(931, 10);
            this.btnFilter.Name = "btnFilter";
            this.btnFilter.Size = new System.Drawing.Size(120, 32);
            this.btnFilter.TabIndex = 5;
            this.btnFilter.Text = "Lọc dữ liệu";
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            // 
            // cboInvoiceStatus
            // 
            this.cboInvoiceStatus.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboInvoiceStatus.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboInvoiceStatus.ItemHeight = 26;
            this.cboInvoiceStatus.Items.AddRange(new object[] {
            "Tất cả",
            "Đã thanh toán",
            "Đã hủy"});
            this.cboInvoiceStatus.Location = new System.Drawing.Point(1070, 10);
            this.cboInvoiceStatus.Name = "cboInvoiceStatus";
            this.cboInvoiceStatus.Size = new System.Drawing.Size(160, 32);
            this.cboInvoiceStatus.StartIndex = 0;
            this.cboInvoiceStatus.TabIndex = 7;
            // 
            // txtSearchInvoice
            // 
            this.txtSearchInvoice.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearchInvoice.DefaultText = "";
            this.txtSearchInvoice.Location = new System.Drawing.Point(725, 10);
            this.txtSearchInvoice.Name = "txtSearchInvoice";
            this.txtSearchInvoice.PlaceholderText = "Mã HĐ hoặc Tên KH...";
            this.txtSearchInvoice.SelectedText = "";
            this.txtSearchInvoice.Size = new System.Drawing.Size(200, 32);
            this.txtSearchInvoice.TabIndex = 6;
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(545, 13);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(160, 25);
            this.dtpTo.TabIndex = 4;
            this.dtpTo.DateTime = System.DateTime.Now;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(470, 17);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(76, 19);
            this.lblTo.TabIndex = 3;
            this.lblTo.Text = "Đến ngày:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(290, 13);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(160, 25);
            this.dtpFrom.TabIndex = 2;
            this.dtpFrom.DateTime = System.DateTime.Now;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(220, 17);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(67, 19);
            this.lblFrom.TabIndex = 1;
            this.lblFrom.Text = "Từ ngày:";
            // 
            // lblFilterTitle
            // 
            this.lblFilterTitle.AutoSize = true;
            this.lblFilterTitle.Location = new System.Drawing.Point(18, 16);
            this.lblFilterTitle.Name = "lblFilterTitle";
            this.lblFilterTitle.Size = new System.Drawing.Size(119, 20);
            this.lblFilterTitle.TabIndex = 0;
            this.lblFilterTitle.Text = "Bộ lọc giao dịch";
            // 
            // pnlCards
            // 
            this.pnlCards.Controls.Add(this.cardVisitorsMonth);
            this.pnlCards.Controls.Add(this.cardInvoices);
            this.pnlCards.Controls.Add(this.cardVisitors);
            this.pnlCards.Controls.Add(this.cardRevenue);
            this.pnlCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCards.Location = new System.Drawing.Point(15, 70);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Padding = new System.Windows.Forms.Padding(0, 10, 0, 10);
            this.pnlCards.Size = new System.Drawing.Size(1370, 150);
            this.pnlCards.TabIndex = 1;
            // 
            // cardVisitorsMonth
            // 
            this.cardVisitorsMonth.Controls.Add(this.lblVisitorsMonth);
            this.cardVisitorsMonth.Controls.Add(this.lblVisitorsMonthTitle);
            this.cardVisitorsMonth.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cardVisitorsMonth.Location = new System.Drawing.Point(1020, 10);
            this.cardVisitorsMonth.Name = "cardVisitorsMonth";
            this.cardVisitorsMonth.Size = new System.Drawing.Size(320, 120);
            this.cardVisitorsMonth.TabIndex = 3;
            this.cardVisitorsMonth.Tag = "monthvisitors";
            // 
            // lblVisitorsMonth
            // 
            this.lblVisitorsMonth.AutoSize = true;
            this.lblVisitorsMonth.Location = new System.Drawing.Point(18, 55);
            this.lblVisitorsMonth.Name = "lblVisitorsMonth";
            this.lblVisitorsMonth.Size = new System.Drawing.Size(35, 41);
            this.lblVisitorsMonth.TabIndex = 0;
            this.lblVisitorsMonth.Text = "0";
            // 
            // lblVisitorsMonthTitle
            // 
            this.lblVisitorsMonthTitle.AutoSize = true;
            this.lblVisitorsMonthTitle.Location = new System.Drawing.Point(18, 18);
            this.lblVisitorsMonthTitle.Name = "lblVisitorsMonthTitle";
            this.lblVisitorsMonthTitle.Size = new System.Drawing.Size(176, 20);
            this.lblVisitorsMonthTitle.TabIndex = 1;
            this.lblVisitorsMonthTitle.Text = "KHÁCH TRONG THÁNG";
            // 
            // cardInvoices
            // 
            this.cardInvoices.Controls.Add(this.lblInvoices);
            this.cardInvoices.Controls.Add(this.lblInvoicesTitle);
            this.cardInvoices.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cardInvoices.Location = new System.Drawing.Point(680, 10);
            this.cardInvoices.Name = "cardInvoices";
            this.cardInvoices.Size = new System.Drawing.Size(320, 120);
            this.cardInvoices.TabIndex = 2;
            this.cardInvoices.Tag = "invoices";
            // 
            // lblInvoices
            // 
            this.lblInvoices.AutoSize = true;
            this.lblInvoices.Location = new System.Drawing.Point(18, 55);
            this.lblInvoices.Name = "lblInvoices";
            this.lblInvoices.Size = new System.Drawing.Size(35, 41);
            this.lblInvoices.TabIndex = 0;
            this.lblInvoices.Text = "0";
            // 
            // lblInvoicesTitle
            // 
            this.lblInvoicesTitle.AutoSize = true;
            this.lblInvoicesTitle.Location = new System.Drawing.Point(18, 18);
            this.lblInvoicesTitle.Name = "lblInvoicesTitle";
            this.lblInvoicesTitle.Size = new System.Drawing.Size(156, 20);
            this.lblInvoicesTitle.TabIndex = 1;
            this.lblInvoicesTitle.Text = "HÓA ĐƠN HÔM NAY";
            // 
            // cardVisitors
            // 
            this.cardVisitors.Controls.Add(this.lblVisitors);
            this.cardVisitors.Controls.Add(this.lblVisitorsTitle);
            this.cardVisitors.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cardVisitors.Location = new System.Drawing.Point(340, 10);
            this.cardVisitors.Name = "cardVisitors";
            this.cardVisitors.Size = new System.Drawing.Size(320, 120);
            this.cardVisitors.TabIndex = 1;
            this.cardVisitors.Tag = "visitors";
            // 
            // lblVisitors
            // 
            this.lblVisitors.AutoSize = true;
            this.lblVisitors.Location = new System.Drawing.Point(18, 55);
            this.lblVisitors.Name = "lblVisitors";
            this.lblVisitors.Size = new System.Drawing.Size(35, 41);
            this.lblVisitors.TabIndex = 0;
            this.lblVisitors.Text = "0";
            // 
            // lblVisitorsTitle
            // 
            this.lblVisitorsTitle.AutoSize = true;
            this.lblVisitorsTitle.Location = new System.Drawing.Point(18, 18);
            this.lblVisitorsTitle.Name = "lblVisitorsTitle";
            this.lblVisitorsTitle.Size = new System.Drawing.Size(139, 20);
            this.lblVisitorsTitle.TabIndex = 1;
            this.lblVisitorsTitle.Text = "VÉ BÁN HÔM NAY";
            // 
            // cardRevenue
            // 
            this.cardRevenue.Controls.Add(this.lblRevenue);
            this.cardRevenue.Controls.Add(this.lblRevenueTitle);
            this.cardRevenue.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.cardRevenue.Location = new System.Drawing.Point(0, 10);
            this.cardRevenue.Name = "cardRevenue";
            this.cardRevenue.Size = new System.Drawing.Size(320, 120);
            this.cardRevenue.TabIndex = 0;
            this.cardRevenue.Tag = "revenue";
            // 
            // lblRevenue
            // 
            this.lblRevenue.AutoSize = true;
            this.lblRevenue.Location = new System.Drawing.Point(18, 55);
            this.lblRevenue.Name = "lblRevenue";
            this.lblRevenue.Size = new System.Drawing.Size(109, 41);
            this.lblRevenue.TabIndex = 0;
            this.lblRevenue.Text = "0 VNĐ";
            // 
            // lblRevenueTitle
            // 
            this.lblRevenueTitle.AutoSize = true;
            this.lblRevenueTitle.Location = new System.Drawing.Point(18, 18);
            this.lblRevenueTitle.Name = "lblRevenueTitle";
            this.lblRevenueTitle.Size = new System.Drawing.Size(175, 20);
            this.lblRevenueTitle.TabIndex = 1;
            this.lblRevenueTitle.Text = "DOANH THU HÔM NAY";
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.pnlGridMain);
            this.pnlContent.Controls.Add(this.pnlSummary);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(15, 220);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.pnlContent.Size = new System.Drawing.Size(1370, 425);
            this.pnlContent.TabIndex = 2;
            // 
            // pnlGridMain
            // 
            this.pnlGridMain.Controls.Add(this.gbInvoices);
            this.pnlGridMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGridMain.Location = new System.Drawing.Point(0, 5);
            this.pnlGridMain.Name = "pnlGridMain";
            this.pnlGridMain.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.pnlGridMain.Size = new System.Drawing.Size(931, 420);
            this.pnlGridMain.TabIndex = 0;
            // 
            // gbInvoices
            // 
            this.gbInvoices.Controls.Add(this.dgvRecentInvoices);
            this.gbInvoices.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.gbInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbInvoices.Location = new System.Drawing.Point(0, 0);
            this.gbInvoices.Name = "gbInvoices";
            this.gbInvoices.Size = new System.Drawing.Size(921, 420);
            this.gbInvoices.TabIndex = 0;
            this.gbInvoices.Text = "Danh sách hoá đơn gần nhất";
            // 
            // dgvRecentInvoices
            // 
            this.dgvRecentInvoices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvRecentInvoices.Location = new System.Drawing.Point(0, 40);
            this.dgvRecentInvoices.MainView = this.gridViewRecentInvoices;
            this.dgvRecentInvoices.Name = "dgvRecentInvoices";
            this.dgvRecentInvoices.Size = new System.Drawing.Size(921, 380);
            this.dgvRecentInvoices.TabIndex = 0;
            this.dgvRecentInvoices.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewRecentInvoices});
            // 
            // gridViewRecentInvoices
            // 
            this.gridViewRecentInvoices.GridControl = this.dgvRecentInvoices;
            this.gridViewRecentInvoices.Name = "gridViewRecentInvoices";
            this.gridViewRecentInvoices.OptionsBehavior.Editable = false;
            this.gridViewRecentInvoices.OptionsView.ShowGroupPanel = false;
            this.gridViewRecentInvoices.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewRecentInvoices_RowCellStyle);
            // 
            // pnlSummary
            // 
            this.pnlSummary.Controls.Add(this.gbSummary);
            this.pnlSummary.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlSummary.Location = new System.Drawing.Point(931, 5);
            this.pnlSummary.Name = "pnlSummary";
            this.pnlSummary.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
            this.pnlSummary.Size = new System.Drawing.Size(439, 420);
            this.pnlSummary.TabIndex = 1;
            // 
            // gbSummary
            // 
            this.gbSummary.Controls.Add(this.dgvMonthlySummary);
            this.gbSummary.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.gbSummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbSummary.Location = new System.Drawing.Point(10, 0);
            this.gbSummary.Name = "gbSummary";
            this.gbSummary.Size = new System.Drawing.Size(429, 420);
            this.gbSummary.TabIndex = 0;
            this.gbSummary.Text = "Tổng hợp 6 tháng gần nhất";
            // 
            // dgvMonthlySummary
            // 
            this.dgvMonthlySummary.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvMonthlySummary.Location = new System.Drawing.Point(0, 40);
            this.dgvMonthlySummary.MainView = this.gridViewMonthlySummary;
            this.dgvMonthlySummary.Name = "dgvMonthlySummary";
            this.dgvMonthlySummary.Size = new System.Drawing.Size(429, 380);
            this.dgvMonthlySummary.TabIndex = 0;
            this.dgvMonthlySummary.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMonthlySummary});
            // 
            // gridViewMonthlySummary
            // 
            this.gridViewMonthlySummary.GridControl = this.dgvMonthlySummary;
            this.gridViewMonthlySummary.Name = "gridViewMonthlySummary";
            this.gridViewMonthlySummary.OptionsBehavior.Editable = false;
            this.gridViewMonthlySummary.OptionsView.ShowGroupPanel = false;
            this.gridViewMonthlySummary.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewRecentInvoices_RowCellStyle);
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 660);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlCards);
            this.Controls.Add(this.pnlFilter);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDashboard";
            this.Padding = new System.Windows.Forms.Padding(15);
            this.Text = "BÁO CÁO THỐNG KÊ";
            this.Text = "Dashboard Overview";
            this.Load += new System.EventHandler(this.frmDashboard_Load);
            this.Resize += new System.EventHandler(this.frmDashboard_Resize);
            this.txtSearchInvoice.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtSearchInvoice_KeyDown);
            this.btnFilter.Click += new System.EventHandler(this.btnFilter_Click);
            this.gridViewRecentInvoices.DoubleClick += new System.EventHandler(this.gridViewRecentInvoices_DoubleClick);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlCards.ResumeLayout(false);
            this.cardVisitorsMonth.ResumeLayout(false);
            this.cardVisitorsMonth.PerformLayout();
            this.cardInvoices.ResumeLayout(false);
            this.cardInvoices.PerformLayout();
            this.cardVisitors.ResumeLayout(false);
            this.cardVisitors.PerformLayout();
            this.cardRevenue.ResumeLayout(false);
            this.cardRevenue.PerformLayout();
            this.pnlContent.ResumeLayout(false);
            this.pnlGridMain.ResumeLayout(false);
            this.gbInvoices.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRecentInvoices)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewRecentInvoices)).EndInit();
            this.pnlSummary.ResumeLayout(false);
            this.gbSummary.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvMonthlySummary)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMonthlySummary)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlFilter;
        private Guna.UI2.WinForms.Guna2Button btnFilter;
        private DevExpress.XtraEditors.DateEdit dtpTo;
        private System.Windows.Forms.Label lblTo;
        private DevExpress.XtraEditors.DateEdit dtpFrom;
        private System.Windows.Forms.Label lblFrom;
        private System.Windows.Forms.Label lblFilterTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlCards;
        private Guna.UI2.WinForms.Guna2Panel cardRevenue;
        private System.Windows.Forms.Label lblRevenueTitle;
        private System.Windows.Forms.Label lblRevenue;
        private Guna.UI2.WinForms.Guna2Panel cardVisitors;
        private System.Windows.Forms.Label lblVisitorsTitle;
        private System.Windows.Forms.Label lblVisitors;
        private Guna.UI2.WinForms.Guna2Panel cardInvoices;
        private System.Windows.Forms.Label lblInvoicesTitle;
        private System.Windows.Forms.Label lblInvoices;
        private Guna.UI2.WinForms.Guna2Panel cardVisitorsMonth;
        private System.Windows.Forms.Label lblVisitorsMonthTitle;
        private System.Windows.Forms.Label lblVisitorsMonth;
        private System.Windows.Forms.Panel pnlContent;
        private Guna.UI2.WinForms.Guna2Panel pnlGridMain;
        private Guna.UI2.WinForms.Guna2GroupBox gbInvoices;
        private Guna.UI2.WinForms.Guna2GroupBox gbSummary;
        private Guna.UI2.WinForms.Guna2Panel pnlSummary;
        private DevExpress.XtraGrid.GridControl dgvRecentInvoices;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewRecentInvoices;
        private DevExpress.XtraGrid.GridControl dgvMonthlySummary;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMonthlySummary;
        private Guna.UI2.WinForms.Guna2TextBox txtSearchInvoice;
        private Guna.UI2.WinForms.Guna2ComboBox cboInvoiceStatus;
    }
}

