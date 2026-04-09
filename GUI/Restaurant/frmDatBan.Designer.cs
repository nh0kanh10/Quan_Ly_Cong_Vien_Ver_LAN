namespace GUI
{
    partial class frmDatBan
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
            this.components = new System.ComponentModel.Container();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement2 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement3 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement4 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            this.colMaBan = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colSucChua = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colTrangThai = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colIcon = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblStats = new System.Windows.Forms.Label();
            this.cboNhaHang = new Guna.UI2.WinForms.Guna2ComboBox();
            this.pnlLegend = new System.Windows.Forms.FlowLayoutPanel();
            this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridTableMap = new DevExpress.XtraGrid.GridControl();
            this.tileViewTable = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.colIdBan = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this._ctxMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miMoBan = new System.Windows.Forms.ToolStripMenuItem();
            this.miDatBanTruoc = new System.Windows.Forms.ToolStripMenuItem();
            this.miNhanBan = new System.Windows.Forms.ToolStripMenuItem();
            this.sep1 = new System.Windows.Forms.ToolStripSeparator();
            this.miGhepBan = new System.Windows.Forms.ToolStripMenuItem();
            this.miGoiMon = new System.Windows.Forms.ToolStripMenuItem();
            this.sep2 = new System.Windows.Forms.ToolStripSeparator();
            this.miThanhToan = new System.Windows.Forms.ToolStripMenuItem();
            this.miPhuThu = new System.Windows.Forms.ToolStripMenuItem();
            this.sep3 = new System.Windows.Forms.ToolStripSeparator();
            this.miTraBan = new System.Windows.Forms.ToolStripMenuItem();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.gridBill = new DevExpress.XtraGrid.GridControl();
            this.gridViewBill = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblBillTitle = new System.Windows.Forms.Label();
            this.pnlActions = new System.Windows.Forms.FlowLayoutPanel();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnDoiBan = new Guna.UI2.WinForms.Guna2Button();
            this.btnPhuThu = new Guna.UI2.WinForms.Guna2Button();
            this.btnGoiMon = new Guna.UI2.WinForms.Guna2Button();
            this.btnMoBan = new Guna.UI2.WinForms.Guna2Button();
            this.btnTraBan = new Guna.UI2.WinForms.Guna2Button();
            this.pnlBanContext = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTrangThaiBan = new System.Windows.Forms.Label();
            this.lblBanInfo = new System.Windows.Forms.Label();
            this.pnlTotal = new Guna.UI2.WinForms.Guna2Panel();
            this.btnThanhToan = new Guna.UI2.WinForms.Guna2Button();
            this.btnInTamTinh = new Guna.UI2.WinForms.Guna2Button();
            this.lblThanhTien = new System.Windows.Forms.Label();
            this.lblChietKhau = new System.Windows.Forms.Label();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.timerClock = new System.Windows.Forms.Timer(this.components);
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).BeginInit();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTableMap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileViewTable)).BeginInit();
            this._ctxMenu.SuspendLayout();
            this.pnlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBill)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBill)).BeginInit();
            this.pnlActions.SuspendLayout();
            this.pnlBanContext.SuspendLayout();
            this.pnlTotal.SuspendLayout();
            this.SuspendLayout();
            // 
            // colMaBan
            // 
            this.colMaBan.FieldName = "MaBan";
            this.colMaBan.Name = "colMaBan";
            this.colMaBan.Visible = true;
            this.colMaBan.VisibleIndex = 0;
            // 
            // colSucChua
            // 
            this.colSucChua.FieldName = "SucChuaText";
            this.colSucChua.Name = "colSucChua";
            this.colSucChua.Visible = true;
            this.colSucChua.VisibleIndex = 1;
            // 
            // colTrangThai
            // 
            this.colTrangThai.FieldName = "TrangThaiText";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.Visible = true;
            this.colTrangThai.VisibleIndex = 2;
            // 
            // colIcon
            // 
            this.colIcon.FieldName = "Icon";
            this.colIcon.Name = "colIcon";
            this.colIcon.Visible = true;
            this.colIcon.VisibleIndex = 4;
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblStats);
            this.pnlTop.Controls.Add(this.cboNhaHang);
            this.pnlTop.Controls.Add(this.pnlLegend);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.FillColor = System.Drawing.Color.White;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1200, 56);
            this.pnlTop.TabIndex = 0;
            // 
            // lblStats
            // 
            this.lblStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStats.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStats.Location = new System.Drawing.Point(900, 16);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(290, 25);
            this.lblStats.TabIndex = 2;
            this.lblStats.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboNhaHang
            // 
            this.cboNhaHang.BackColor = System.Drawing.Color.Transparent;
            this.cboNhaHang.BorderRadius = 8;
            this.cboNhaHang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNhaHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhaHang.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboNhaHang.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cboNhaHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNhaHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboNhaHang.ItemHeight = 28;
            this.cboNhaHang.Location = new System.Drawing.Point(15, 10);
            this.cboNhaHang.Name = "cboNhaHang";
            this.cboNhaHang.Size = new System.Drawing.Size(280, 34);
            this.cboNhaHang.TabIndex = 0;
            this.cboNhaHang.SelectedIndexChanged += new System.EventHandler(this.cboNhaHang_SelectedIndexChanged);
            // 
            // pnlLegend
            // 
            this.pnlLegend.AutoSize = true;
            this.pnlLegend.Location = new System.Drawing.Point(310, 13);
            this.pnlLegend.Name = "pnlLegend";
            this.pnlLegend.Size = new System.Drawing.Size(580, 30);
            this.pnlLegend.TabIndex = 1;
            this.pnlLegend.WrapContents = false;
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitMain.Location = new System.Drawing.Point(0, 56);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.gridTableMap);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlRight);
            this.splitMain.Size = new System.Drawing.Size(1200, 644);
            this.splitMain.SplitterPosition = 460;
            this.splitMain.TabIndex = 1;
            // 
            // gridTableMap
            // 
            this.gridTableMap.ContextMenuStrip = this._ctxMenu;
            this.gridTableMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTableMap.Location = new System.Drawing.Point(0, 0);
            this.gridTableMap.MainView = this.tileViewTable;
            this.gridTableMap.Name = "gridTableMap";
            this.gridTableMap.Size = new System.Drawing.Size(730, 644);
            this.gridTableMap.TabIndex = 0;
            this.gridTableMap.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tileViewTable});
            // 
            // tileViewTable
            // 
            this.tileViewTable.Appearance.ItemHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.tileViewTable.Appearance.ItemNormal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tileViewTable.Appearance.ItemNormal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.tileViewTable.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaBan,
            this.colSucChua,
            this.colTrangThai,
            this.colIdBan,
            this.colIcon});
            this.tileViewTable.GridControl = this.gridTableMap;
            this.tileViewTable.Name = "tileViewTable";
            this.tileViewTable.OptionsTiles.IndentBetweenItems = 10;
            this.tileViewTable.OptionsTiles.ItemPadding = new System.Windows.Forms.Padding(8);
            this.tileViewTable.OptionsTiles.ItemSize = new System.Drawing.Size(130, 100);
            this.tileViewTable.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tileViewTable.OptionsTiles.RowCount = 0;
            tileViewItemElement1.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            tileViewItemElement1.Appearance.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            tileViewItemElement1.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement1.Appearance.Normal.Options.UseForeColor = true;
            tileViewItemElement1.Column = this.colMaBan;
            tileViewItemElement1.Text = "colMaBan";
            tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopLeft;
            tileViewItemElement2.AnchorElementIndex = 0;
            tileViewItemElement2.AnchorIndent = 4;
            tileViewItemElement2.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 9F);
            tileViewItemElement2.Appearance.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            tileViewItemElement2.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement2.Appearance.Normal.Options.UseForeColor = true;
            tileViewItemElement2.Column = this.colSucChua;
            tileViewItemElement2.Text = "colSucChua";
            tileViewItemElement3.AnchorElementIndex = 1;
            tileViewItemElement3.AnchorIndent = 4;
            tileViewItemElement3.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            tileViewItemElement3.Appearance.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            tileViewItemElement3.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement3.Appearance.Normal.Options.UseForeColor = true;
            tileViewItemElement3.Column = this.colTrangThai;
            tileViewItemElement3.Text = "colTrangThai";
            tileViewItemElement4.Column = this.colIcon;
            tileViewItemElement4.ImageOptions.ImageAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopRight;
            tileViewItemElement4.ImageOptions.ImageScaleMode = DevExpress.XtraEditors.TileItemImageScaleMode.ZoomInside;
            tileViewItemElement4.ImageOptions.ImageSize = new System.Drawing.Size(28, 28);
            tileViewItemElement4.Text = "colIcon";
            tileViewItemElement4.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.TopRight;
            this.tileViewTable.TileTemplate.Add(tileViewItemElement1);
            this.tileViewTable.TileTemplate.Add(tileViewItemElement2);
            this.tileViewTable.TileTemplate.Add(tileViewItemElement3);
            this.tileViewTable.TileTemplate.Add(tileViewItemElement4);
            this.tileViewTable.ItemClick += new DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventHandler(this.tileViewTable_ItemClick);
            this.tileViewTable.ItemDoubleClick += new DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventHandler(this.tileViewTable_ItemDoubleClick);
            this.tileViewTable.ItemCustomize += new DevExpress.XtraGrid.Views.Tile.TileViewItemCustomizeEventHandler(this.tileViewTable_ItemCustomize);
            // 
            // colIdBan
            // 
            this.colIdBan.FieldName = "Id";
            this.colIdBan.Name = "colIdBan";
            this.colIdBan.Visible = true;
            this.colIdBan.VisibleIndex = 3;
            // 
            // _ctxMenu
            // 
            this._ctxMenu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this._ctxMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miMoBan,
            this.miDatBanTruoc,
            this.miNhanBan,
            this.sep1,
            this.miGhepBan,
            this.miGoiMon,
            this.sep2,
            this.miThanhToan,
            this.miPhuThu,
            this.sep3,
            this.miTraBan});
            this._ctxMenu.Name = "_ctxMenu";
            this._ctxMenu.Size = new System.Drawing.Size(214, 214);
            // 
            // miMoBan
            // 
            this.miMoBan.Name = "miMoBan";
            this.miMoBan.Size = new System.Drawing.Size(213, 24);
            this.miMoBan.Text = "🟢 Mở bàn";
            this.miMoBan.Click += new System.EventHandler(this.btnMoBan_Click);
            // 
            // miDatBanTruoc
            // 
            this.miDatBanTruoc.Name = "miDatBanTruoc";
            this.miDatBanTruoc.Size = new System.Drawing.Size(213, 24);
            this.miDatBanTruoc.Text = "🛎️ Đặt bàn trước (F5)";
            this.miDatBanTruoc.Click += new System.EventHandler(this.btnDatBanTruoc_Click);
            // 
            // miNhanBan
            // 
            this.miNhanBan.Name = "miNhanBan";
            this.miNhanBan.Size = new System.Drawing.Size(213, 24);
            this.miNhanBan.Text = "🔑 Nhận bàn đã đặt";
            this.miNhanBan.Click += new System.EventHandler(this.btnNhanBan_Click);
            // 
            // sep1
            // 
            this.sep1.Name = "sep1";
            this.sep1.Size = new System.Drawing.Size(210, 6);
            // 
            // miGhepBan
            // 
            this.miGhepBan.Name = "miGhepBan";
            this.miGhepBan.Size = new System.Drawing.Size(213, 24);
            this.miGhepBan.Text = "⚡ Ghép Bàn";
            this.miGhepBan.Click += new System.EventHandler(this.btnGhepBan_Click);
            // 
            // miGoiMon
            // 
            this.miGoiMon.Name = "miGoiMon";
            this.miGoiMon.Size = new System.Drawing.Size(213, 24);
            this.miGoiMon.Text = "➕ Gọi món (F3)";
            this.miGoiMon.Click += new System.EventHandler(this.btnGoiMon_Click);
            // 
            // sep2
            // 
            this.sep2.Name = "sep2";
            this.sep2.Size = new System.Drawing.Size(210, 6);
            // 
            // miThanhToan
            // 
            this.miThanhToan.Name = "miThanhToan";
            this.miThanhToan.Size = new System.Drawing.Size(213, 24);
            this.miThanhToan.Text = "💳 Thanh toán (F9)";
            this.miThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // miPhuThu
            // 
            this.miPhuThu.Name = "miPhuThu";
            this.miPhuThu.Size = new System.Drawing.Size(213, 24);
            this.miPhuThu.Text = "🔴 Phụ thu / Phạt";
            this.miPhuThu.Click += new System.EventHandler(this.btnPhuThu_Click);
            // 
            // sep3
            // 
            this.sep3.Name = "sep3";
            this.sep3.Size = new System.Drawing.Size(210, 6);
            // 
            // miTraBan
            // 
            this.miTraBan.Name = "miTraBan";
            this.miTraBan.Size = new System.Drawing.Size(213, 24);
            this.miTraBan.Text = "⚪ Trả bàn";
            this.miTraBan.Click += new System.EventHandler(this.btnTraBan_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.BackColor = System.Drawing.Color.White;
            this.pnlRight.Controls.Add(this.gridBill);
            this.pnlRight.Controls.Add(this.lblBillTitle);
            this.pnlRight.Controls.Add(this.pnlActions);
            this.pnlRight.Controls.Add(this.pnlBanContext);
            this.pnlRight.Controls.Add(this.pnlTotal);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.FillColor = System.Drawing.Color.White;
            this.pnlRight.Location = new System.Drawing.Point(0, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Size = new System.Drawing.Size(460, 644);
            this.pnlRight.TabIndex = 0;
            // 
            // gridBill
            // 
            this.gridBill.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBill.Location = new System.Drawing.Point(0, 181);
            this.gridBill.MainView = this.gridViewBill;
            this.gridBill.Name = "gridBill";
            this.gridBill.Size = new System.Drawing.Size(460, 283);
            this.gridBill.TabIndex = 3;
            this.gridBill.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBill});
            // 
            // gridViewBill
            // 
            this.gridViewBill.GridControl = this.gridBill;
            this.gridViewBill.Name = "gridViewBill";
            this.gridViewBill.OptionsBehavior.Editable = false;
            this.gridViewBill.OptionsView.ShowGroupPanel = false;
            // 
            // lblBillTitle
            // 
            this.lblBillTitle.BackColor = System.Drawing.Color.White;
            this.lblBillTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBillTitle.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblBillTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblBillTitle.Location = new System.Drawing.Point(0, 153);
            this.lblBillTitle.Name = "lblBillTitle";
            this.lblBillTitle.Padding = new System.Windows.Forms.Padding(12, 4, 0, 0);
            this.lblBillTitle.Size = new System.Drawing.Size(460, 28);
            this.lblBillTitle.TabIndex = 2;
            this.lblBillTitle.Text = "🛒 BILL — Chưa chọn bàn";
            // 
            // pnlActions
            // 
            this.pnlActions.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlActions.Controls.Add(this.btnLamMoi);
            this.pnlActions.Controls.Add(this.btnDoiBan);
            this.pnlActions.Controls.Add(this.btnPhuThu);
            this.pnlActions.Controls.Add(this.btnGoiMon);
            this.pnlActions.Controls.Add(this.btnMoBan);
            this.pnlActions.Controls.Add(this.btnTraBan);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActions.Location = new System.Drawing.Point(0, 62);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Padding = new System.Windows.Forms.Padding(6, 5, 6, 5);
            this.pnlActions.Size = new System.Drawing.Size(460, 91);
            this.pnlActions.TabIndex = 1;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnLamMoi.BorderRadius = 6;
            this.btnLamMoi.BorderThickness = 1;
            this.btnLamMoi.FillColor = System.Drawing.Color.White;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnLamMoi.Location = new System.Drawing.Point(9, 8);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(130, 34);
            this.btnLamMoi.TabIndex = 6;
            this.btnLamMoi.Text = "LÀM MỚI";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnDoiBan
            // 
            this.btnDoiBan.BorderRadius = 6;
            this.btnDoiBan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(139)))), ((int)(((byte)(92)))), ((int)(((byte)(246)))));
            this.btnDoiBan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDoiBan.ForeColor = System.Drawing.Color.White;
            this.btnDoiBan.Location = new System.Drawing.Point(145, 8);
            this.btnDoiBan.Name = "btnDoiBan";
            this.btnDoiBan.Size = new System.Drawing.Size(119, 34);
            this.btnDoiBan.TabIndex = 3;
            this.btnDoiBan.Text = "ĐỔI BÀN";
            this.btnDoiBan.Click += new System.EventHandler(this.btnDoiBan_Click);
            // 
            // btnPhuThu
            // 
            this.btnPhuThu.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnPhuThu.BorderRadius = 6;
            this.btnPhuThu.BorderThickness = 1;
            this.btnPhuThu.FillColor = System.Drawing.Color.White;
            this.btnPhuThu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnPhuThu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnPhuThu.Location = new System.Drawing.Point(270, 8);
            this.btnPhuThu.Name = "btnPhuThu";
            this.btnPhuThu.Size = new System.Drawing.Size(116, 34);
            this.btnPhuThu.TabIndex = 4;
            this.btnPhuThu.Text = "PHỤ THU";
            this.btnPhuThu.Click += new System.EventHandler(this.btnPhuThu_Click);
            // 
            // btnGoiMon
            // 
            this.btnGoiMon.BorderRadius = 6;
            this.btnGoiMon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnGoiMon.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnGoiMon.ForeColor = System.Drawing.Color.White;
            this.btnGoiMon.Location = new System.Drawing.Point(9, 48);
            this.btnGoiMon.Name = "btnGoiMon";
            this.btnGoiMon.Size = new System.Drawing.Size(130, 34);
            this.btnGoiMon.TabIndex = 0;
            this.btnGoiMon.Text = "GỌI MÓN (F3)";
            this.btnGoiMon.Click += new System.EventHandler(this.btnGoiMon_Click);
            // 
            // btnMoBan
            // 
            this.btnMoBan.BorderRadius = 6;
            this.btnMoBan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnMoBan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnMoBan.ForeColor = System.Drawing.Color.White;
            this.btnMoBan.Location = new System.Drawing.Point(145, 48);
            this.btnMoBan.Name = "btnMoBan";
            this.btnMoBan.Size = new System.Drawing.Size(119, 34);
            this.btnMoBan.TabIndex = 2;
            this.btnMoBan.Text = "MỞ BÀN";
            this.btnMoBan.Click += new System.EventHandler(this.btnMoBan_Click);
            // 
            // btnTraBan
            // 
            this.btnTraBan.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnTraBan.BorderRadius = 6;
            this.btnTraBan.BorderThickness = 1;
            this.btnTraBan.FillColor = System.Drawing.Color.White;
            this.btnTraBan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTraBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnTraBan.Location = new System.Drawing.Point(270, 48);
            this.btnTraBan.Name = "btnTraBan";
            this.btnTraBan.Size = new System.Drawing.Size(116, 34);
            this.btnTraBan.TabIndex = 5;
            this.btnTraBan.Text = "TRẢ BÀN";
            this.btnTraBan.Click += new System.EventHandler(this.btnTraBan_Click);
            // 
            // pnlBanContext
            // 
            this.pnlBanContext.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlBanContext.Controls.Add(this.lblTrangThaiBan);
            this.pnlBanContext.Controls.Add(this.lblBanInfo);
            this.pnlBanContext.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBanContext.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlBanContext.Location = new System.Drawing.Point(0, 0);
            this.pnlBanContext.Name = "pnlBanContext";
            this.pnlBanContext.Size = new System.Drawing.Size(460, 62);
            this.pnlBanContext.TabIndex = 0;
            // 
            // lblTrangThaiBan
            // 
            this.lblTrangThaiBan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTrangThaiBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblTrangThaiBan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(134)))), ((int)(((byte)(239)))), ((int)(((byte)(172)))));
            this.lblTrangThaiBan.Location = new System.Drawing.Point(0, 38);
            this.lblTrangThaiBan.Name = "lblTrangThaiBan";
            this.lblTrangThaiBan.Padding = new System.Windows.Forms.Padding(12, 0, 0, 4);
            this.lblTrangThaiBan.Size = new System.Drawing.Size(460, 24);
            this.lblTrangThaiBan.TabIndex = 1;
            // 
            // lblBanInfo
            // 
            this.lblBanInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblBanInfo.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblBanInfo.ForeColor = System.Drawing.Color.White;
            this.lblBanInfo.Location = new System.Drawing.Point(0, 0);
            this.lblBanInfo.Name = "lblBanInfo";
            this.lblBanInfo.Padding = new System.Windows.Forms.Padding(12, 6, 0, 0);
            this.lblBanInfo.Size = new System.Drawing.Size(460, 34);
            this.lblBanInfo.TabIndex = 0;
            this.lblBanInfo.Text = "Chưa chọn bàn";
            // 
            // pnlTotal
            // 
            this.pnlTotal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTotal.Controls.Add(this.btnThanhToan);
            this.pnlTotal.Controls.Add(this.btnInTamTinh);
            this.pnlTotal.Controls.Add(this.lblThanhTien);
            this.pnlTotal.Controls.Add(this.lblChietKhau);
            this.pnlTotal.Controls.Add(this.lblTongCong);
            this.pnlTotal.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTotal.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTotal.Location = new System.Drawing.Point(0, 464);
            this.pnlTotal.Name = "pnlTotal";
            this.pnlTotal.Size = new System.Drawing.Size(460, 180);
            this.pnlTotal.TabIndex = 4;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnThanhToan.BorderRadius = 8;
            this.btnThanhToan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnThanhToan.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnThanhToan.ForeColor = System.Drawing.Color.White;
            this.btnThanhToan.Location = new System.Drawing.Point(234, 115);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(214, 52);
            this.btnThanhToan.TabIndex = 4;
            this.btnThanhToan.Text = "💳 THANH TOÁN (F9)";
            this.btnThanhToan.Click += new System.EventHandler(this.btnThanhToan_Click);
            // 
            // btnInTamTinh
            // 
            this.btnInTamTinh.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnInTamTinh.BorderRadius = 8;
            this.btnInTamTinh.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnInTamTinh.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnInTamTinh.ForeColor = System.Drawing.Color.White;
            this.btnInTamTinh.Location = new System.Drawing.Point(12, 115);
            this.btnInTamTinh.Name = "btnInTamTinh";
            this.btnInTamTinh.Size = new System.Drawing.Size(210, 52);
            this.btnInTamTinh.TabIndex = 3;
            this.btnInTamTinh.Text = "🖨 IN TẠM TÍNH";
            this.btnInTamTinh.Click += new System.EventHandler(this.btnInTamTinh_Click);
            // 
            // lblThanhTien
            // 
            this.lblThanhTien.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblThanhTien.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblThanhTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblThanhTien.Location = new System.Drawing.Point(12, 58);
            this.lblThanhTien.Name = "lblThanhTien";
            this.lblThanhTien.Size = new System.Drawing.Size(436, 40);
            this.lblThanhTien.TabIndex = 2;
            this.lblThanhTien.Text = "THÀNH TIỀN:   0 VNĐ";
            this.lblThanhTien.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblChietKhau
            // 
            this.lblChietKhau.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblChietKhau.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblChietKhau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblChietKhau.Location = new System.Drawing.Point(12, 32);
            this.lblChietKhau.Name = "lblChietKhau";
            this.lblChietKhau.Size = new System.Drawing.Size(436, 22);
            this.lblChietKhau.TabIndex = 1;
            this.lblChietKhau.Text = "CHIẾT KHẤU:                                                            0 đ";
            this.lblChietKhau.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTongCong
            // 
            this.lblTongCong.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongCong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongCong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTongCong.Location = new System.Drawing.Point(12, 8);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Size = new System.Drawing.Size(436, 22);
            this.lblTongCong.TabIndex = 0;
            this.lblTongCong.Text = "TỔNG CỘNG:                                                           0 đ";
            this.lblTongCong.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // timerClock
            // 
            this.timerClock.Interval = 1000;
            this.timerClock.Tick += new System.EventHandler(this.timerClock_Tick);
            // 
            // frmDatBan
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmDatBan";
            this.Text = "Quản lý Đặt Bàn Nhà Hàng";
            this.Load += new System.EventHandler(this.frmDatBan_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmDatBan_KeyDown);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).EndInit();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTableMap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileViewTable)).EndInit();
            this._ctxMenu.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridBill)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBill)).EndInit();
            this.pnlActions.ResumeLayout(false);
            this.pnlBanContext.ResumeLayout(false);
            this.pnlTotal.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        // Top
        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private System.Windows.Forms.Label lblStats;
        private Guna.UI2.WinForms.Guna2ComboBox cboNhaHang;
        private System.Windows.Forms.FlowLayoutPanel pnlLegend;
        // Split
        private DevExpress.XtraEditors.SplitContainerControl splitMain;
        // Table map
        private DevExpress.XtraGrid.GridControl gridTableMap;
        private DevExpress.XtraGrid.Views.Tile.TileView tileViewTable;
        private DevExpress.XtraGrid.Columns.TileViewColumn colMaBan;
        private DevExpress.XtraGrid.Columns.TileViewColumn colSucChua;
        private DevExpress.XtraGrid.Columns.TileViewColumn colTrangThai;
        private DevExpress.XtraGrid.Columns.TileViewColumn colIdBan;
        private DevExpress.XtraGrid.Columns.TileViewColumn colIcon;
        // Right panel
        private Guna.UI2.WinForms.Guna2Panel pnlRight;
        // Context
        private Guna.UI2.WinForms.Guna2Panel pnlBanContext;
        private System.Windows.Forms.Label lblBanInfo;
        private System.Windows.Forms.Label lblTrangThaiBan;
        // Actions
        private System.Windows.Forms.FlowLayoutPanel pnlActions;
        private Guna.UI2.WinForms.Guna2Button btnGoiMon;
        private Guna.UI2.WinForms.Guna2Button btnMoBan;
        private Guna.UI2.WinForms.Guna2Button btnDoiBan;
        private Guna.UI2.WinForms.Guna2Button btnPhuThu;
        private Guna.UI2.WinForms.Guna2Button btnTraBan;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        // Bill
        private System.Windows.Forms.Label lblBillTitle;
        private DevExpress.XtraGrid.GridControl gridBill;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBill;
        // Total
        private Guna.UI2.WinForms.Guna2Panel pnlTotal;
        private System.Windows.Forms.Label lblTongCong;
        private System.Windows.Forms.Label lblChietKhau;
        private System.Windows.Forms.Label lblThanhTien;
        private Guna.UI2.WinForms.Guna2Button btnInTamTinh;
        private Guna.UI2.WinForms.Guna2Button btnThanhToan;
        private System.Windows.Forms.Timer timerClock;
        private System.Windows.Forms.ContextMenuStrip _ctxMenu;
        private System.Windows.Forms.ToolStripMenuItem miMoBan;
        private System.Windows.Forms.ToolStripMenuItem miDatBanTruoc;
        private System.Windows.Forms.ToolStripMenuItem miNhanBan;
        private System.Windows.Forms.ToolStripMenuItem miGhepBan;
        private System.Windows.Forms.ToolStripMenuItem miGoiMon;
        private System.Windows.Forms.ToolStripMenuItem miThanhToan;
        private System.Windows.Forms.ToolStripMenuItem miPhuThu;
        private System.Windows.Forms.ToolStripMenuItem miTraBan;
        private System.Windows.Forms.ToolStripSeparator sep1;
        private System.Windows.Forms.ToolStripSeparator sep2;
        private System.Windows.Forms.ToolStripSeparator sep3;
    }
}
