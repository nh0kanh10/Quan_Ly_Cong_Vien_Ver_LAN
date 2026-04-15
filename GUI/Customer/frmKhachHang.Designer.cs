namespace GUI
{
    partial class frmKhachHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlTopBar = new Guna.UI2.WinForms.Guna2Panel();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnThemKhachMoi = new Guna.UI2.WinForms.Guna2Button();
            this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnlLeftCard = new Guna.UI2.WinForms.Guna2Panel();
            this.gridKhachHang = new DevExpress.XtraGrid.GridControl();
            this.gridViewKhachHang = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.tabDetails = new Guna.UI2.WinForms.Guna2TabControl();
            this.tabGiaoDich = new System.Windows.Forms.TabPage();
            this.gridGiaoDich = new DevExpress.XtraGrid.GridControl();
            this.gridViewGiaoDich = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabDiem = new System.Windows.Forms.TabPage();
            this.gridDiem = new DevExpress.XtraGrid.GridControl();
            this.gridViewDiem = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabSuCo = new System.Windows.Forms.TabPage();
            this.gridSuCo = new DevExpress.XtraGrid.GridControl();
            this.gridViewSuCo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlActions = new Guna.UI2.WinForms.Guna2Panel();
            this.btnCapVi = new Guna.UI2.WinForms.Guna2Button();
            this.btnKhoaThe = new Guna.UI2.WinForms.Guna2Button();
            this.btnNapTien = new Guna.UI2.WinForms.Guna2Button();
            this.btnSuaDiem = new Guna.UI2.WinForms.Guna2Button();
            this.pnlKpiStrip = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlKpiThe = new Guna.UI2.WinForms.Guna2Panel();
            this.lblMaRfid = new System.Windows.Forms.Label();
            this.lblTrangThaiThe = new System.Windows.Forms.Label();
            this.lblTheTitle = new System.Windows.Forms.Label();
            this.pnlKpiDiem = new Guna.UI2.WinForms.Guna2Panel();
            this.lblDiemTichLuy = new System.Windows.Forms.Label();
            this.lblDiemTitle = new System.Windows.Forms.Label();
            this.pnlKpiSoDu = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSoDu = new System.Windows.Forms.Label();
            this.lblSoDuTitle = new System.Windows.Forms.Label();
            this.pnlKpiChiTieu = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTongChiTieu = new System.Windows.Forms.Label();
            this.lblChiTieuTitle = new System.Windows.Forms.Label();
            this.pnlCustomerHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.btnSuaKH = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaKH = new Guna.UI2.WinForms.Guna2Button();
            this.lblLoaiKhach = new System.Windows.Forms.Label();
            this.lblMaKH = new System.Windows.Forms.Label();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.lblNoSelection = new System.Windows.Forms.Label();
            this.pnlTopBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).BeginInit();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnlLeftCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewKhachHang)).BeginInit();
            this.pnlDetail.SuspendLayout();
            this.tabDetails.SuspendLayout();
            this.tabGiaoDich.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGiaoDich)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGiaoDich)).BeginInit();
            this.tabDiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDiem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDiem)).BeginInit();
            this.tabSuCo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSuCo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSuCo)).BeginInit();
            this.pnlActions.SuspendLayout();
            this.pnlKpiStrip.SuspendLayout();
            this.pnlKpiThe.SuspendLayout();
            this.pnlKpiDiem.SuspendLayout();
            this.pnlKpiSoDu.SuspendLayout();
            this.pnlKpiChiTieu.SuspendLayout();
            this.pnlCustomerHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.Controls.Add(this.btnThemKhachMoi);
            this.pnlTopBar.Controls.Add(this.txtTimKiem);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.FillColor = System.Drawing.Color.Transparent;
            this.pnlTopBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlTopBar.Size = new System.Drawing.Size(408, 110);
            this.pnlTopBar.TabIndex = 1;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderRadius = 4;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.txtTimKiem.Location = new System.Drawing.Point(15, 10);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "  Nhập SĐT, Mã thẻ, hoặc Tên khách hàng...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(378, 42);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // btnThemKhachMoi
            // 
            this.btnThemKhachMoi.Animated = true;
            this.btnThemKhachMoi.BackColor = System.Drawing.Color.Transparent;
            this.btnThemKhachMoi.BorderColor = System.Drawing.Color.RoyalBlue;
            this.btnThemKhachMoi.BorderRadius = 4;
            this.btnThemKhachMoi.BorderThickness = 2;
            this.btnThemKhachMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemKhachMoi.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnThemKhachMoi.FillColor = System.Drawing.Color.Transparent;
            this.btnThemKhachMoi.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemKhachMoi.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnThemKhachMoi.Location = new System.Drawing.Point(15, 60);
            this.btnThemKhachMoi.Name = "btnThemKhachMoi";
            this.btnThemKhachMoi.Size = new System.Drawing.Size(378, 40);
            this.btnThemKhachMoi.TabIndex = 0;
            this.btnThemKhachMoi.Text = "+ THÊM MỚI";
            this.btnThemKhachMoi.Click += new System.EventHandler(this.BtnThemKhachMoi_Click);
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.pnlLeftCard);
            this.splitMain.Panel1.Padding = new System.Windows.Forms.Padding(15);
            // 
            // pnlLeftCard
            // 
            this.pnlLeftCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlLeftCard.BorderRadius = 8;
            this.pnlLeftCard.BorderThickness = 1;
            this.pnlLeftCard.Controls.Add(this.gridKhachHang);
            this.pnlLeftCard.Controls.Add(this.pnlTopBar);
            this.pnlLeftCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeftCard.FillColor = System.Drawing.Color.White;
            this.pnlLeftCard.Location = new System.Drawing.Point(15, 15);
            this.pnlLeftCard.Name = "pnlLeftCard";
            this.pnlLeftCard.Padding = new System.Windows.Forms.Padding(0, 0, 0, 10);
            this.pnlLeftCard.Size = new System.Drawing.Size(378, 994);
            this.pnlLeftCard.TabIndex = 0;
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlDetail);
            this.splitMain.Size = new System.Drawing.Size(1331, 1024);
            this.splitMain.SplitterPosition = 408;
            this.splitMain.TabIndex = 0;
            // 
            // gridKhachHang
            // 
            this.gridKhachHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridKhachHang.Location = new System.Drawing.Point(0, 110);
            this.gridKhachHang.MainView = this.gridViewKhachHang;
            this.gridKhachHang.Name = "gridKhachHang";
            this.gridKhachHang.Size = new System.Drawing.Size(408, 914);
            this.gridKhachHang.TabIndex = 0;
            this.gridKhachHang.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewKhachHang});
            // 
            // gridViewKhachHang
            // 
            this.gridViewKhachHang.ColumnPanelRowHeight = 38;
            this.gridViewKhachHang.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridViewKhachHang.GridControl = this.gridKhachHang;
            this.gridViewKhachHang.Name = "gridViewKhachHang";
            this.gridViewKhachHang.OptionsBehavior.Editable = false;
            this.gridViewKhachHang.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridViewKhachHang.OptionsView.ShowGroupPanel = false;
            this.gridViewKhachHang.OptionsView.ShowIndicator = false;
            this.gridViewKhachHang.RowHeight = 42;
            this.gridViewKhachHang.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewKhachHang_FocusedRowChanged);
            // 
            // pnlDetail
            // 
            this.pnlDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlDetail.Controls.Add(this.tabDetails);
            this.pnlDetail.Controls.Add(this.pnlActions);
            this.pnlDetail.Controls.Add(this.pnlKpiStrip);
            this.pnlDetail.Controls.Add(this.pnlCustomerHeader);
            this.pnlDetail.Controls.Add(this.lblNoSelection);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetail.Location = new System.Drawing.Point(0, 0);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Padding = new System.Windows.Forms.Padding(15);
            this.pnlDetail.Size = new System.Drawing.Size(913, 1024);
            this.pnlDetail.TabIndex = 0;
            // 
            // tabDetails
            // 
            this.tabDetails.Controls.Add(this.tabGiaoDich);
            this.tabDetails.Controls.Add(this.tabDiem);
            this.tabDetails.Controls.Add(this.tabSuCo);
            this.tabDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabDetails.ItemSize = new System.Drawing.Size(200, 40);
            this.tabDetails.Location = new System.Drawing.Point(15, 260);
            this.tabDetails.Name = "tabDetails";
            this.tabDetails.SelectedIndex = 0;
            this.tabDetails.Size = new System.Drawing.Size(883, 749);
            this.tabDetails.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.tabDetails.TabButtonHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tabDetails.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tabDetails.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.tabDetails.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tabDetails.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.tabDetails.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabDetails.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tabDetails.TabButtonIdleState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.tabDetails.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabDetails.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.tabDetails.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tabDetails.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.tabDetails.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.tabDetails.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.tabDetails.TabButtonSize = new System.Drawing.Size(200, 40);
            this.tabDetails.TabIndex = 0;
            this.tabDetails.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabDetails.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.HorizontalTop;
            // 
            // tabGiaoDich
            // 
            this.tabGiaoDich.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tabGiaoDich.Controls.Add(this.gridGiaoDich);
            this.tabGiaoDich.Location = new System.Drawing.Point(4, 44);
            this.tabGiaoDich.Name = "tabGiaoDich";
            this.tabGiaoDich.Size = new System.Drawing.Size(875, 701);
            this.tabGiaoDich.TabIndex = 0;
            this.tabGiaoDich.Text = "  LỊCH SỬ GIAO DỊCH";
            // 
            // gridGiaoDich
            // 
            this.gridGiaoDich.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridGiaoDich.Location = new System.Drawing.Point(0, 0);
            this.gridGiaoDich.MainView = this.gridViewGiaoDich;
            this.gridGiaoDich.Name = "gridGiaoDich";
            this.gridGiaoDich.Size = new System.Drawing.Size(875, 701);
            this.gridGiaoDich.TabIndex = 0;
            this.gridGiaoDich.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewGiaoDich});
            // 
            // gridViewGiaoDich
            // 
            this.gridViewGiaoDich.ColumnPanelRowHeight = 38;
            this.gridViewGiaoDich.GridControl = this.gridGiaoDich;
            this.gridViewGiaoDich.Name = "gridViewGiaoDich";
            this.gridViewGiaoDich.OptionsBehavior.Editable = false;
            this.gridViewGiaoDich.OptionsView.ShowGroupPanel = false;
            this.gridViewGiaoDich.OptionsView.ShowIndicator = false;
            this.gridViewGiaoDich.RowHeight = 35;
            this.gridViewGiaoDich.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewGiaoDich_RowCellStyle);
            this.gridViewGiaoDich.CustomColumnDisplayText += new DevExpress.XtraGrid.Views.Base.CustomColumnDisplayTextEventHandler(this.gridViewGiaoDich_CustomColumnDisplayText);
            // 
            // tabDiem
            // 
            this.tabDiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tabDiem.Controls.Add(this.gridDiem);
            this.tabDiem.Location = new System.Drawing.Point(4, 44);
            this.tabDiem.Name = "tabDiem";
            this.tabDiem.Size = new System.Drawing.Size(963, 641);
            this.tabDiem.TabIndex = 1;
            this.tabDiem.Text = "  LỊCH SỬ ĐIỂM";
            // 
            // gridDiem
            // 
            this.gridDiem.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDiem.Location = new System.Drawing.Point(0, 0);
            this.gridDiem.MainView = this.gridViewDiem;
            this.gridDiem.Name = "gridDiem";
            this.gridDiem.Size = new System.Drawing.Size(963, 641);
            this.gridDiem.TabIndex = 0;
            this.gridDiem.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDiem});
            // 
            // gridViewDiem
            // 
            this.gridViewDiem.ColumnPanelRowHeight = 38;
            this.gridViewDiem.GridControl = this.gridDiem;
            this.gridViewDiem.Name = "gridViewDiem";
            this.gridViewDiem.OptionsBehavior.Editable = false;
            this.gridViewDiem.OptionsView.ShowGroupPanel = false;
            this.gridViewDiem.OptionsView.ShowIndicator = false;
            this.gridViewDiem.RowHeight = 35;
            // 
            // tabSuCo
            // 
            this.tabSuCo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tabSuCo.Controls.Add(this.gridSuCo);
            this.tabSuCo.Location = new System.Drawing.Point(4, 44);
            this.tabSuCo.Name = "tabSuCo";
            this.tabSuCo.Size = new System.Drawing.Size(963, 641);
            this.tabSuCo.TabIndex = 2;
            this.tabSuCo.Text = "  SỰ CỐ";
            // 
            // gridSuCo
            // 
            this.gridSuCo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSuCo.Location = new System.Drawing.Point(0, 0);
            this.gridSuCo.MainView = this.gridViewSuCo;
            this.gridSuCo.Name = "gridSuCo";
            this.gridSuCo.Size = new System.Drawing.Size(963, 641);
            this.gridSuCo.TabIndex = 0;
            this.gridSuCo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSuCo});
            // 
            // gridViewSuCo
            // 
            this.gridViewSuCo.ColumnPanelRowHeight = 38;
            this.gridViewSuCo.GridControl = this.gridSuCo;
            this.gridViewSuCo.Name = "gridViewSuCo";
            this.gridViewSuCo.OptionsBehavior.Editable = false;
            this.gridViewSuCo.OptionsView.ShowGroupPanel = false;
            this.gridViewSuCo.OptionsView.ShowIndicator = false;
            this.gridViewSuCo.RowHeight = 35;
            // 
            // pnlActions
            // 
            this.pnlActions.Controls.Add(this.btnSuaDiem);
            this.pnlActions.Controls.Add(this.btnCapVi);
            this.pnlActions.Controls.Add(this.btnKhoaThe);
            this.pnlActions.Controls.Add(this.btnNapTien);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlActions.FillColor = System.Drawing.Color.Transparent;
            this.pnlActions.Location = new System.Drawing.Point(15, 194);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Padding = new System.Windows.Forms.Padding(12, 5, 12, 5);
            this.pnlActions.Size = new System.Drawing.Size(883, 66);
            this.pnlActions.TabIndex = 1;
            // 
            // btnCapVi
            // 
            this.btnCapVi.Animated = true;
            this.btnCapVi.BorderRadius = 4;
            this.btnCapVi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCapVi.Enabled = false;
            this.btnCapVi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.btnCapVi.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCapVi.ForeColor = System.Drawing.Color.White;
            this.btnCapVi.Location = new System.Drawing.Point(321, 6);
            this.btnCapVi.Name = "btnCapVi";
            this.btnCapVi.Size = new System.Drawing.Size(171, 38);
            this.btnCapVi.TabIndex = 0;
            this.btnCapVi.Text = "CẤP VÍ VÀ THẺ";
            this.btnCapVi.Click += new System.EventHandler(this.BtnCapVi_Click);
            // 
            // btnKhoaThe
            // 
            this.btnKhoaThe.Animated = true;
            this.btnKhoaThe.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnKhoaThe.BorderRadius = 4;
            this.btnKhoaThe.BorderThickness = 2;
            this.btnKhoaThe.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnKhoaThe.FillColor = System.Drawing.Color.Transparent;
            this.btnKhoaThe.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnKhoaThe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnKhoaThe.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnKhoaThe.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnKhoaThe.Location = new System.Drawing.Point(171, 6);
            this.btnKhoaThe.Name = "btnKhoaThe";
            this.btnKhoaThe.Size = new System.Drawing.Size(140, 38);
            this.btnKhoaThe.TabIndex = 1;
            this.btnKhoaThe.Text = "KHÓA THẺ";
            this.btnKhoaThe.Click += new System.EventHandler(this.BtnKhoaThe_Click);
            // 
            // btnNapTien
            // 
            this.btnNapTien.Animated = true;
            this.btnNapTien.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnNapTien.BorderRadius = 4;
            this.btnNapTien.BorderThickness = 2;
            this.btnNapTien.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnNapTien.FillColor = System.Drawing.Color.Transparent;
            this.btnNapTien.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNapTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnNapTien.Location = new System.Drawing.Point(1, 6);
            this.btnNapTien.Name = "btnNapTien";
            this.btnNapTien.Size = new System.Drawing.Size(160, 38);
            this.btnNapTien.TabIndex = 2;
            this.btnNapTien.Text = "NẠP TIỀN VÍ";
            this.btnNapTien.Click += new System.EventHandler(this.BtnNapTien_Click);
            // 
            // btnSuaDiem
            // 
            this.btnSuaDiem.Animated = true;
            this.btnSuaDiem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnSuaDiem.BorderRadius = 4;
            this.btnSuaDiem.BorderThickness = 2;
            this.btnSuaDiem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSuaDiem.FillColor = System.Drawing.Color.Transparent;
            this.btnSuaDiem.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnSuaDiem.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnSuaDiem.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnSuaDiem.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnSuaDiem.Location = new System.Drawing.Point(502, 6);
            this.btnSuaDiem.Name = "btnSuaDiem";
            this.btnSuaDiem.Size = new System.Drawing.Size(160, 38);
            this.btnSuaDiem.TabIndex = 3;
            this.btnSuaDiem.Text = "CHỈNH SỬA ĐIỂM";
            this.btnSuaDiem.Click += new System.EventHandler(this.BtnSuaDiem_Click);
            // 
            // pnlKpiStrip
            // 
            this.pnlKpiStrip.Controls.Add(this.pnlKpiChiTieu);
            this.pnlKpiStrip.Controls.Add(this.pnlKpiThe);
            this.pnlKpiStrip.Controls.Add(this.pnlKpiDiem);
            this.pnlKpiStrip.Controls.Add(this.pnlKpiSoDu);
            this.pnlKpiStrip.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpiStrip.FillColor = System.Drawing.Color.Transparent;
            this.pnlKpiStrip.Location = new System.Drawing.Point(15, 95);
            this.pnlKpiStrip.Name = "pnlKpiStrip";
            this.pnlKpiStrip.Padding = new System.Windows.Forms.Padding(0, 8, 0, 10);
            this.pnlKpiStrip.Size = new System.Drawing.Size(883, 99);
            this.pnlKpiStrip.TabIndex = 2;
            // 
            // pnlKpiThe
            // 
            this.pnlKpiThe.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlKpiThe.BorderThickness = 1;
            this.pnlKpiThe.Controls.Add(this.lblMaRfid);
            this.pnlKpiThe.Controls.Add(this.lblTrangThaiThe);
            this.pnlKpiThe.Controls.Add(this.lblTheTitle);
            this.pnlKpiThe.FillColor = System.Drawing.Color.White;
            this.pnlKpiThe.Location = new System.Drawing.Point(421, 8);
            this.pnlKpiThe.Name = "pnlKpiThe";
            this.pnlKpiThe.Size = new System.Drawing.Size(220, 74);
            this.pnlKpiThe.TabIndex = 0;
            // 
            // lblMaRfid
            // 
            this.lblMaRfid.AutoSize = true;
            this.lblMaRfid.Font = new System.Drawing.Font("Segoe UI Semibold", 7.5F);
            this.lblMaRfid.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMaRfid.Location = new System.Drawing.Point(12, 52);
            this.lblMaRfid.Name = "lblMaRfid";
            this.lblMaRfid.Size = new System.Drawing.Size(0, 12);
            this.lblMaRfid.TabIndex = 0;
            // 
            // lblTrangThaiThe
            // 
            this.lblTrangThaiThe.AutoSize = true;
            this.lblTrangThaiThe.BackColor = System.Drawing.Color.Transparent;
            this.lblTrangThaiThe.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblTrangThaiThe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTrangThaiThe.Location = new System.Drawing.Point(12, 28);
            this.lblTrangThaiThe.Name = "lblTrangThaiThe";
            this.lblTrangThaiThe.Size = new System.Drawing.Size(24, 20);
            this.lblTrangThaiThe.TabIndex = 1;
            this.lblTrangThaiThe.Text = "—";
            // 
            // lblTheTitle
            // 
            this.lblTheTitle.AutoSize = true;
            this.lblTheTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTheTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblTheTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTheTitle.Location = new System.Drawing.Point(12, 8);
            this.lblTheTitle.Name = "lblTheTitle";
            this.lblTheTitle.Size = new System.Drawing.Size(47, 12);
            this.lblTheTitle.TabIndex = 2;
            this.lblTheTitle.Text = "THẺ RFID";
            // 
            // pnlKpiDiem
            // 
            this.pnlKpiDiem.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlKpiDiem.BorderThickness = 1;
            this.pnlKpiDiem.Controls.Add(this.lblDiemTichLuy);
            this.pnlKpiDiem.Controls.Add(this.lblDiemTitle);
            this.pnlKpiDiem.FillColor = System.Drawing.Color.White;
            this.pnlKpiDiem.Location = new System.Drawing.Point(231, 8);
            this.pnlKpiDiem.Name = "pnlKpiDiem";
            this.pnlKpiDiem.Size = new System.Drawing.Size(180, 74);
            this.pnlKpiDiem.TabIndex = 1;
            // 
            // lblDiemTichLuy
            // 
            this.lblDiemTichLuy.AutoSize = true;
            this.lblDiemTichLuy.BackColor = System.Drawing.Color.Transparent;
            this.lblDiemTichLuy.Font = new System.Drawing.Font("Cascadia Code", 16F, System.Drawing.FontStyle.Bold);
            this.lblDiemTichLuy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblDiemTichLuy.Location = new System.Drawing.Point(12, 30);
            this.lblDiemTichLuy.Name = "lblDiemTichLuy";
            this.lblDiemTichLuy.Size = new System.Drawing.Size(26, 29);
            this.lblDiemTichLuy.TabIndex = 0;
            this.lblDiemTichLuy.Text = "—";
            // 
            // lblDiemTitle
            // 
            this.lblDiemTitle.AutoSize = true;
            this.lblDiemTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblDiemTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblDiemTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDiemTitle.Location = new System.Drawing.Point(12, 8);
            this.lblDiemTitle.Name = "lblDiemTitle";
            this.lblDiemTitle.Size = new System.Drawing.Size(75, 12);
            this.lblDiemTitle.TabIndex = 1;
            this.lblDiemTitle.Text = "ĐIỂM TÍCH LŨY";
            // 
            // pnlKpiSoDu
            // 
            this.pnlKpiSoDu.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlKpiSoDu.BorderThickness = 1;
            this.pnlKpiSoDu.Controls.Add(this.lblSoDu);
            this.pnlKpiSoDu.Controls.Add(this.lblSoDuTitle);
            this.pnlKpiSoDu.FillColor = System.Drawing.Color.White;
            this.pnlKpiSoDu.Location = new System.Drawing.Point(1, 8);
            this.pnlKpiSoDu.Name = "pnlKpiSoDu";
            this.pnlKpiSoDu.Size = new System.Drawing.Size(220, 74);
            this.pnlKpiSoDu.TabIndex = 2;
            // 
            // lblSoDu
            // 
            this.lblSoDu.AutoSize = true;
            this.lblSoDu.BackColor = System.Drawing.Color.Transparent;
            this.lblSoDu.Font = new System.Drawing.Font("Cascadia Code", 16F, System.Drawing.FontStyle.Bold);
            this.lblSoDu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblSoDu.Location = new System.Drawing.Point(12, 30);
            this.lblSoDu.Name = "lblSoDu";
            this.lblSoDu.Size = new System.Drawing.Size(26, 29);
            this.lblSoDu.TabIndex = 0;
            this.lblSoDu.Text = "—";
            // 
            // lblSoDuTitle
            // 
            this.lblSoDuTitle.AutoSize = true;
            this.lblSoDuTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSoDuTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblSoDuTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSoDuTitle.Location = new System.Drawing.Point(12, 8);
            this.lblSoDuTitle.Name = "lblSoDuTitle";
            this.lblSoDuTitle.Size = new System.Drawing.Size(90, 12);
            this.lblSoDuTitle.TabIndex = 1;
            this.lblSoDuTitle.Text = "SỐ DƯ KHẢ DỤNG";
            // 
            // pnlKpiChiTieu
            // 
            this.pnlKpiChiTieu.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlKpiChiTieu.BorderThickness = 1;
            this.pnlKpiChiTieu.Controls.Add(this.lblTongChiTieu);
            this.pnlKpiChiTieu.Controls.Add(this.lblChiTieuTitle);
            this.pnlKpiChiTieu.FillColor = System.Drawing.Color.White;
            this.pnlKpiChiTieu.Location = new System.Drawing.Point(651, 8);
            this.pnlKpiChiTieu.Name = "pnlKpiChiTieu";
            this.pnlKpiChiTieu.Size = new System.Drawing.Size(220, 74);
            this.pnlKpiChiTieu.TabIndex = 3;
            // 
            // lblTongChiTieu
            // 
            this.lblTongChiTieu.AutoSize = true;
            this.lblTongChiTieu.BackColor = System.Drawing.Color.Transparent;
            this.lblTongChiTieu.Font = new System.Drawing.Font("Cascadia Code", 16F, System.Drawing.FontStyle.Bold);
            this.lblTongChiTieu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTongChiTieu.Location = new System.Drawing.Point(12, 30);
            this.lblTongChiTieu.Name = "lblTongChiTieu";
            this.lblTongChiTieu.Size = new System.Drawing.Size(26, 29);
            this.lblTongChiTieu.TabIndex = 0;
            this.lblTongChiTieu.Text = "—";
            // 
            // lblChiTieuTitle
            // 
            this.lblChiTieuTitle.AutoSize = true;
            this.lblChiTieuTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblChiTieuTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 7.5F, System.Drawing.FontStyle.Bold);
            this.lblChiTieuTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblChiTieuTitle.Location = new System.Drawing.Point(12, 8);
            this.lblChiTieuTitle.Name = "lblChiTieuTitle";
            this.lblChiTieuTitle.Size = new System.Drawing.Size(89, 12);
            this.lblChiTieuTitle.TabIndex = 1;
            this.lblChiTieuTitle.Text = "TỔNG CHI TIÊU";
            // 
            // pnlCustomerHeader
            // 
            this.pnlCustomerHeader.Controls.Add(this.btnSuaKH);
            this.pnlCustomerHeader.Controls.Add(this.btnXoaKH);
            this.pnlCustomerHeader.Controls.Add(this.lblLoaiKhach);
            this.pnlCustomerHeader.Controls.Add(this.lblMaKH);
            this.pnlCustomerHeader.Controls.Add(this.lblTenKH);
            this.pnlCustomerHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCustomerHeader.FillColor = System.Drawing.Color.White;
            this.pnlCustomerHeader.Location = new System.Drawing.Point(15, 15);
            this.pnlCustomerHeader.Name = "pnlCustomerHeader";
            this.pnlCustomerHeader.Padding = new System.Windows.Forms.Padding(16, 10, 10, 10);
            this.pnlCustomerHeader.Size = new System.Drawing.Size(883, 80);
            this.pnlCustomerHeader.TabIndex = 3;
            // 
            // btnSuaKH
            // 
            this.btnSuaKH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSuaKH.Animated = true;
            this.btnSuaKH.BorderRadius = 4;
            this.btnSuaKH.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSuaKH.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnSuaKH.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnSuaKH.ForeColor = System.Drawing.Color.White;
            this.btnSuaKH.Location = new System.Drawing.Point(672, 23);
            this.btnSuaKH.Name = "btnSuaKH";
            this.btnSuaKH.Size = new System.Drawing.Size(80, 35);
            this.btnSuaKH.TabIndex = 1;
            this.btnSuaKH.Text = "SỬA";
            this.btnSuaKH.Click += new System.EventHandler(this.BtnSuaKH_Click);
            // 
            // btnXoaKH
            // 
            this.btnXoaKH.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXoaKH.Animated = true;
            this.btnXoaKH.BorderRadius = 4;
            this.btnXoaKH.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaKH.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoaKH.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoaKH.ForeColor = System.Drawing.Color.White;
            this.btnXoaKH.Location = new System.Drawing.Point(762, 23);
            this.btnXoaKH.Name = "btnXoaKH";
            this.btnXoaKH.Size = new System.Drawing.Size(80, 35);
            this.btnXoaKH.TabIndex = 0;
            this.btnXoaKH.Text = "XÓA";
            this.btnXoaKH.Click += new System.EventHandler(this.BtnXoaKH_Click);
            // 
            // lblLoaiKhach
            // 
            this.lblLoaiKhach.AutoSize = true;
            this.lblLoaiKhach.BackColor = System.Drawing.Color.Goldenrod;
            this.lblLoaiKhach.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblLoaiKhach.ForeColor = System.Drawing.Color.White;
            this.lblLoaiKhach.Location = new System.Drawing.Point(209, 23);
            this.lblLoaiKhach.Name = "lblLoaiKhach";
            this.lblLoaiKhach.Padding = new System.Windows.Forms.Padding(6, 2, 6, 2);
            this.lblLoaiKhach.Size = new System.Drawing.Size(12, 19);
            this.lblLoaiKhach.TabIndex = 2;
            this.lblLoaiKhach.Visible = false;
            // 
            // lblMaKH
            // 
            this.lblMaKH.AutoSize = true;
            this.lblMaKH.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblMaKH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMaKH.Location = new System.Drawing.Point(16, 40);
            this.lblMaKH.MaximumSize = new System.Drawing.Size(700, 0);
            this.lblMaKH.Name = "lblMaKH";
            this.lblMaKH.Size = new System.Drawing.Size(0, 15);
            this.lblMaKH.TabIndex = 3;
            // 
            // lblTenKH
            // 
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblTenKH.Location = new System.Drawing.Point(16, 23);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(187, 30);
            this.lblTenKH.TabIndex = 4;
            this.lblTenKH.Text = "Chọn khách hàng";
            // 
            // lblNoSelection
            // 
            this.lblNoSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoSelection.Font = new System.Drawing.Font("Segoe UI Semibold", 13F);
            this.lblNoSelection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblNoSelection.Location = new System.Drawing.Point(15, 15);
            this.lblNoSelection.Name = "lblNoSelection";
            this.lblNoSelection.Size = new System.Drawing.Size(883, 994);
            this.lblNoSelection.TabIndex = 4;
            this.lblNoSelection.Text = "← Chọn khách hàng từ danh sách bên trái";
            this.lblNoSelection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // frmKhachHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1331, 1024);
            this.Controls.Add(this.splitMain);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.Name = "frmKhachHang";
            this.Text = "QUẢN LÝ KHÁCH HÀNG";
            this.pnlTopBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).EndInit();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnlLeftCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewKhachHang)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            this.tabDetails.ResumeLayout(false);
            this.tabGiaoDich.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridGiaoDich)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGiaoDich)).EndInit();
            this.tabDiem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDiem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDiem)).EndInit();
            this.tabSuCo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSuCo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSuCo)).EndInit();
            this.pnlActions.ResumeLayout(false);
            this.pnlKpiStrip.ResumeLayout(false);
            this.pnlKpiThe.ResumeLayout(false);
            this.pnlKpiThe.PerformLayout();
            this.pnlKpiDiem.ResumeLayout(false);
            this.pnlKpiDiem.PerformLayout();
            this.pnlKpiSoDu.ResumeLayout(false);
            this.pnlKpiSoDu.PerformLayout();
            this.pnlKpiChiTieu.ResumeLayout(false);
            this.pnlKpiChiTieu.PerformLayout();
            this.pnlCustomerHeader.ResumeLayout(false);
            this.pnlCustomerHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        // Top Bar
        private Guna.UI2.WinForms.Guna2Panel pnlTopBar;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private Guna.UI2.WinForms.Guna2Button btnThemKhachMoi;

        // Split
        private DevExpress.XtraEditors.SplitContainerControl splitMain;
        
        // Left Card
        private Guna.UI2.WinForms.Guna2Panel pnlLeftCard;

        // Master Grid
        private DevExpress.XtraGrid.GridControl gridKhachHang;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewKhachHang;

        // Detail Panel
        private System.Windows.Forms.Panel pnlDetail;
        private System.Windows.Forms.Label lblNoSelection;

        // Header
        private Guna.UI2.WinForms.Guna2Panel pnlCustomerHeader;
        private System.Windows.Forms.Label lblTenKH;
        private System.Windows.Forms.Label lblMaKH;
        private System.Windows.Forms.Label lblLoaiKhach;
        private Guna.UI2.WinForms.Guna2Button btnSuaKH;
        private Guna.UI2.WinForms.Guna2Button btnXoaKH;

        // KPI Strip
        private Guna.UI2.WinForms.Guna2Panel pnlKpiStrip;
        private Guna.UI2.WinForms.Guna2Panel pnlKpiSoDu;
        private System.Windows.Forms.Label lblSoDuTitle;
        private System.Windows.Forms.Label lblSoDu;
        private Guna.UI2.WinForms.Guna2Panel pnlKpiDiem;
        private System.Windows.Forms.Label lblDiemTitle;
        private System.Windows.Forms.Label lblDiemTichLuy;
        private Guna.UI2.WinForms.Guna2Panel pnlKpiThe;
        private System.Windows.Forms.Label lblTheTitle;
        private System.Windows.Forms.Label lblTrangThaiThe;
        private System.Windows.Forms.Label lblMaRfid;
        private Guna.UI2.WinForms.Guna2Panel pnlKpiChiTieu;
        private System.Windows.Forms.Label lblChiTieuTitle;
        private System.Windows.Forms.Label lblTongChiTieu;

        // Action Buttons
        private Guna.UI2.WinForms.Guna2Panel pnlActions;
        private Guna.UI2.WinForms.Guna2Button btnNapTien;
        private Guna.UI2.WinForms.Guna2Button btnKhoaThe;
        private Guna.UI2.WinForms.Guna2Button btnCapVi;
        private Guna.UI2.WinForms.Guna2Button btnSuaDiem;

        // Tabs
        private Guna.UI2.WinForms.Guna2TabControl tabDetails;
        private System.Windows.Forms.TabPage tabGiaoDich;
        private System.Windows.Forms.TabPage tabDiem;
        private System.Windows.Forms.TabPage tabSuCo;

        // Tab Grids
        private DevExpress.XtraGrid.GridControl gridGiaoDich;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewGiaoDich;
        private DevExpress.XtraGrid.GridControl gridDiem;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDiem;
        private DevExpress.XtraGrid.GridControl gridSuCo;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSuCo;
    }
}
