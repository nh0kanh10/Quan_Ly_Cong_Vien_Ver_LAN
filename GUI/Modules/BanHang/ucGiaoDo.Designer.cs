namespace GUI.Modules.BanHang
{
    partial class ucGiaoDo
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;


        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.splitThaoTac = new System.Windows.Forms.SplitContainer();
            this.gridSanPham = new DevExpress.XtraGrid.GridControl();
            this.viewSanPham = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSP_TenSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSP_TienThue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSP_TienCoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtBarcode = new DevExpress.XtraEditors.TextEdit();
            this.slkKho = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.slkKhoView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridGioThue = new DevExpress.XtraGrid.GridControl();
            this.viewGioThue = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGT_TenSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGT_SoLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGT_TienThue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGT_TienCoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGT_Tong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGT_Xoa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repBtnXoa = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.pnlTong = new System.Windows.Forms.Panel();
            this.lblTongValue = new DevExpress.XtraEditors.LabelControl();
            this.lblTongLabel = new DevExpress.XtraEditors.LabelControl();
            this.lblTienCocValue = new DevExpress.XtraEditors.LabelControl();
            this.lblTienCocLabel = new DevExpress.XtraEditors.LabelControl();
            this.lblTienThueValue = new DevExpress.XtraEditors.LabelControl();
            this.lblTienThueLabel = new DevExpress.XtraEditors.LabelControl();
            this.pnlNut = new System.Windows.Forms.FlowLayoutPanel();
            this.btnRFID = new DevExpress.XtraEditors.SimpleButton();
            this.btnTienMat = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.pnlKhach = new System.Windows.Forms.Panel();
            this.lblSoDuVi = new DevExpress.XtraEditors.LabelControl();
            this.lblTenKhach = new DevExpress.XtraEditors.LabelControl();
            this.btnBoChonKhach = new DevExpress.XtraEditors.SimpleButton();
            this.btnTimKhach = new DevExpress.XtraEditors.SimpleButton();
            this.txtTimKhach = new DevExpress.XtraEditors.TextEdit();
            this.pnlGiamSat = new System.Windows.Forms.Panel();
            this.gridChuaTra = new DevExpress.XtraGrid.GridControl();
            this.viewChuaTra = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.btnRefresh = new DevExpress.XtraEditors.SimpleButton();
            this.lblGiamSatTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnlBanner = new System.Windows.Forms.Panel();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitThaoTac)).BeginInit();
            this.splitThaoTac.Panel1.SuspendLayout();
            this.splitThaoTac.Panel2.SuspendLayout();
            this.splitThaoTac.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhoView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGioThue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewGioThue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repBtnXoa)).BeginInit();
            this.pnlTong.SuspendLayout();
            this.pnlNut.SuspendLayout();
            this.pnlKhach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKhach.Properties)).BeginInit();
            this.pnlGiamSat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridChuaTra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewChuaTra)).BeginInit();
            this.pnlBanner.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 45);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.splitThaoTac);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlGiamSat);
            this.splitMain.Size = new System.Drawing.Size(1100, 655);
            this.splitMain.SplitterDistance = 400;
            this.splitMain.TabIndex = 1;
            // 
            // splitThaoTac
            // 
            this.splitThaoTac.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitThaoTac.Location = new System.Drawing.Point(0, 0);
            this.splitThaoTac.Name = "splitThaoTac";
            // 
            // splitThaoTac.Panel1
            // 
            this.splitThaoTac.Panel1.Controls.Add(this.gridSanPham);
            this.splitThaoTac.Panel1.Controls.Add(this.txtBarcode);
            this.splitThaoTac.Panel1.Controls.Add(this.slkKho);
            // 
            // splitThaoTac.Panel2
            // 
            this.splitThaoTac.Panel2.Controls.Add(this.gridGioThue);
            this.splitThaoTac.Panel2.Controls.Add(this.pnlTong);
            this.splitThaoTac.Panel2.Controls.Add(this.pnlNut);
            this.splitThaoTac.Panel2.Controls.Add(this.pnlKhach);
            this.splitThaoTac.Size = new System.Drawing.Size(1100, 400);
            this.splitThaoTac.SplitterDistance = 350;
            this.splitThaoTac.TabIndex = 0;
            // 
            // gridSanPham
            // 
            this.gridSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSanPham.Location = new System.Drawing.Point(0, 40);
            this.gridSanPham.MainView = this.viewSanPham;
            this.gridSanPham.Name = "gridSanPham";
            this.gridSanPham.Size = new System.Drawing.Size(350, 360);
            this.gridSanPham.TabIndex = 1;
            this.gridSanPham.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewSanPham});
            // 
            // viewSanPham
            // 
            this.viewSanPham.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSP_TenSanPham,
            this.colSP_TienThue,
            this.colSP_TienCoc});
            this.viewSanPham.GridControl = this.gridSanPham;
            this.viewSanPham.Name = "viewSanPham";
            this.viewSanPham.OptionsBehavior.Editable = false;
            this.viewSanPham.OptionsBehavior.ReadOnly = true;
            this.viewSanPham.DoubleClick += new System.EventHandler(this.ViewSanPham_DoubleClick);
            // 
            // colSP_TenSanPham
            // 
            this.colSP_TenSanPham.Caption = "Sản phẩm";
            this.colSP_TenSanPham.FieldName = "TenSanPham";
            this.colSP_TenSanPham.Name = "colSP_TenSanPham";
            this.colSP_TenSanPham.Visible = true;
            this.colSP_TenSanPham.VisibleIndex = 0;
            this.colSP_TenSanPham.Width = 180;
            // 
            // colSP_TienThue
            // 
            this.colSP_TienThue.Caption = "Tiền thuê";
            this.colSP_TienThue.DisplayFormat.FormatString = "N0";
            this.colSP_TienThue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSP_TienThue.FieldName = "TienThue";
            this.colSP_TienThue.Name = "colSP_TienThue";
            this.colSP_TienThue.Visible = true;
            this.colSP_TienThue.VisibleIndex = 1;
            // 
            // colSP_TienCoc
            // 
            this.colSP_TienCoc.Caption = "Tiền cọc";
            this.colSP_TienCoc.DisplayFormat.FormatString = "N0";
            this.colSP_TienCoc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colSP_TienCoc.FieldName = "TienCoc";
            this.colSP_TienCoc.Name = "colSP_TienCoc";
            this.colSP_TienCoc.Visible = true;
            this.colSP_TienCoc.VisibleIndex = 2;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtBarcode.Location = new System.Drawing.Point(0, 20);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Properties.NullValuePrompt = "Quét barcode hoặc tìm SP...";
            this.txtBarcode.Size = new System.Drawing.Size(350, 20);
            this.txtBarcode.TabIndex = 0;
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBarcode_KeyDown);
            // 
            // slkKho
            // 
            this.slkKho.Dock = System.Windows.Forms.DockStyle.Top;
            this.slkKho.Location = new System.Drawing.Point(0, 0);
            this.slkKho.Name = "slkKho";
            this.slkKho.Properties.DisplayMember = "TenKho";
            this.slkKho.Properties.NullValuePrompt = "Chọn kho xuất...";
            this.slkKho.Properties.PopupFormMinSize = new System.Drawing.Size(300, 150);
            this.slkKho.Properties.PopupView = this.slkKhoView;
            this.slkKho.Properties.ValueMember = "Id";
            this.slkKho.Size = new System.Drawing.Size(350, 20);
            this.slkKho.TabIndex = 10;
            this.slkKho.EditValueChanged += new System.EventHandler(this.SlkKho_EditValueChanged);
            // 
            // slkKhoView
            // 
            this.slkKhoView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2});
            this.slkKhoView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.slkKhoView.Name = "slkKhoView";
            this.slkKhoView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.slkKhoView.OptionsView.ShowGroupPanel = false;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Name = "gridColumn1";
            // 
            // gridColumn2
            // 
            this.gridColumn2.Name = "gridColumn2";
            // 
            // gridGioThue
            // 
            this.gridGioThue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridGioThue.Location = new System.Drawing.Point(0, 0);
            this.gridGioThue.MainView = this.viewGioThue;
            this.gridGioThue.Name = "gridGioThue";
            this.gridGioThue.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repBtnXoa});
            this.gridGioThue.Size = new System.Drawing.Size(746, 235);
            this.gridGioThue.TabIndex = 3;
            this.gridGioThue.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewGioThue});
            // 
            // viewGioThue
            // 
            this.viewGioThue.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGT_TenSanPham,
            this.colGT_SoLuong,
            this.colGT_TienThue,
            this.colGT_TienCoc,
            this.colGT_Tong,
            this.colGT_Xoa});
            this.viewGioThue.GridControl = this.gridGioThue;
            this.viewGioThue.Name = "viewGioThue";
            this.viewGioThue.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.ViewGioThue_CellValueChanged);
            // 
            // colGT_TenSanPham
            // 
            this.colGT_TenSanPham.Caption = "Sản phẩm";
            this.colGT_TenSanPham.FieldName = "TenSanPham";
            this.colGT_TenSanPham.Name = "colGT_TenSanPham";
            this.colGT_TenSanPham.OptionsColumn.AllowEdit = false;
            this.colGT_TenSanPham.Visible = true;
            this.colGT_TenSanPham.VisibleIndex = 0;
            // 
            // colGT_SoLuong
            // 
            this.colGT_SoLuong.Caption = "SL";
            this.colGT_SoLuong.FieldName = "SoLuong";
            this.colGT_SoLuong.Name = "colGT_SoLuong";
            this.colGT_SoLuong.Visible = true;
            this.colGT_SoLuong.VisibleIndex = 1;
            this.colGT_SoLuong.Width = 50;
            // 
            // colGT_TienThue
            // 
            this.colGT_TienThue.Caption = "Tiền thuê";
            this.colGT_TienThue.DisplayFormat.FormatString = "N0";
            this.colGT_TienThue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGT_TienThue.FieldName = "TienThue";
            this.colGT_TienThue.Name = "colGT_TienThue";
            this.colGT_TienThue.OptionsColumn.AllowEdit = false;
            this.colGT_TienThue.Visible = true;
            this.colGT_TienThue.VisibleIndex = 2;
            // 
            // colGT_TienCoc
            // 
            this.colGT_TienCoc.Caption = "Cọc";
            this.colGT_TienCoc.DisplayFormat.FormatString = "N0";
            this.colGT_TienCoc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGT_TienCoc.FieldName = "TienCoc";
            this.colGT_TienCoc.Name = "colGT_TienCoc";
            this.colGT_TienCoc.OptionsColumn.AllowEdit = false;
            this.colGT_TienCoc.Visible = true;
            this.colGT_TienCoc.VisibleIndex = 3;
            // 
            // colGT_Tong
            // 
            this.colGT_Tong.Caption = "Tổng";
            this.colGT_Tong.DisplayFormat.FormatString = "N0";
            this.colGT_Tong.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGT_Tong.FieldName = "Tong";
            this.colGT_Tong.Name = "colGT_Tong";
            this.colGT_Tong.OptionsColumn.AllowEdit = false;
            this.colGT_Tong.Visible = true;
            this.colGT_Tong.VisibleIndex = 4;
            // 
            // colGT_Xoa
            // 
            this.colGT_Xoa.Caption = " ";
            this.colGT_Xoa.ColumnEdit = this.repBtnXoa;
            this.colGT_Xoa.FieldName = "Xoa";
            this.colGT_Xoa.Name = "colGT_Xoa";
            this.colGT_Xoa.Visible = true;
            this.colGT_Xoa.VisibleIndex = 5;
            this.colGT_Xoa.Width = 40;
            // 
            // repBtnXoa
            // 
            this.repBtnXoa.AutoHeight = false;
            this.repBtnXoa.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.repBtnXoa.Name = "repBtnXoa";
            this.repBtnXoa.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repBtnXoa.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.RepBtnXoa_ButtonClick);
            // 
            // pnlTong
            // 
            this.pnlTong.Controls.Add(this.lblTongValue);
            this.pnlTong.Controls.Add(this.lblTongLabel);
            this.pnlTong.Controls.Add(this.lblTienCocValue);
            this.pnlTong.Controls.Add(this.lblTienCocLabel);
            this.pnlTong.Controls.Add(this.lblTienThueValue);
            this.pnlTong.Controls.Add(this.lblTienThueLabel);
            this.pnlTong.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTong.Location = new System.Drawing.Point(0, 235);
            this.pnlTong.Name = "pnlTong";
            this.pnlTong.Size = new System.Drawing.Size(746, 70);
            this.pnlTong.TabIndex = 2;
            // 
            // lblTongValue
            // 
            this.lblTongValue.Location = new System.Drawing.Point(200, 45);
            this.lblTongValue.Name = "lblTongValue";
            this.lblTongValue.Size = new System.Drawing.Size(15, 13);
            this.lblTongValue.TabIndex = 5;
            this.lblTongValue.Text = "0 ₫";
            // 
            // lblTongLabel
            // 
            this.lblTongLabel.Location = new System.Drawing.Point(10, 45);
            this.lblTongLabel.Name = "lblTongLabel";
            this.lblTongLabel.Size = new System.Drawing.Size(32, 13);
            this.lblTongLabel.TabIndex = 4;
            this.lblTongLabel.Text = "TỔNG:";
            // 
            // lblTienCocValue
            // 
            this.lblTienCocValue.Location = new System.Drawing.Point(200, 25);
            this.lblTienCocValue.Name = "lblTienCocValue";
            this.lblTienCocValue.Size = new System.Drawing.Size(6, 13);
            this.lblTienCocValue.TabIndex = 3;
            this.lblTienCocValue.Text = "0";
            // 
            // lblTienCocLabel
            // 
            this.lblTienCocLabel.Location = new System.Drawing.Point(10, 25);
            this.lblTienCocLabel.Name = "lblTienCocLabel";
            this.lblTienCocLabel.Size = new System.Drawing.Size(43, 13);
            this.lblTienCocLabel.TabIndex = 2;
            this.lblTienCocLabel.Text = "Tiền cọc:";
            // 
            // lblTienThueValue
            // 
            this.lblTienThueValue.Location = new System.Drawing.Point(200, 5);
            this.lblTienThueValue.Name = "lblTienThueValue";
            this.lblTienThueValue.Size = new System.Drawing.Size(6, 13);
            this.lblTienThueValue.TabIndex = 1;
            this.lblTienThueValue.Text = "0";
            // 
            // lblTienThueLabel
            // 
            this.lblTienThueLabel.Location = new System.Drawing.Point(10, 5);
            this.lblTienThueLabel.Name = "lblTienThueLabel";
            this.lblTienThueLabel.Size = new System.Drawing.Size(49, 13);
            this.lblTienThueLabel.TabIndex = 0;
            this.lblTienThueLabel.Text = "Tiền thuê:";
            // 
            // pnlNut
            // 
            this.pnlNut.Controls.Add(this.btnRFID);
            this.pnlNut.Controls.Add(this.btnTienMat);
            this.pnlNut.Controls.Add(this.btnHuy);
            this.pnlNut.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNut.Location = new System.Drawing.Point(0, 305);
            this.pnlNut.Name = "pnlNut";
            this.pnlNut.Size = new System.Drawing.Size(746, 50);
            this.pnlNut.TabIndex = 1;
            // 
            // btnRFID
            // 
            this.btnRFID.Location = new System.Drawing.Point(3, 3);
            this.btnRFID.Name = "btnRFID";
            this.btnRFID.Size = new System.Drawing.Size(120, 40);
            this.btnRFID.TabIndex = 0;
            this.btnRFID.Text = "RFID";
            this.btnRFID.Click += new System.EventHandler(this.BtnRFID_Click);
            // 
            // btnTienMat
            // 
            this.btnTienMat.Location = new System.Drawing.Point(129, 3);
            this.btnTienMat.Name = "btnTienMat";
            this.btnTienMat.Size = new System.Drawing.Size(120, 40);
            this.btnTienMat.TabIndex = 1;
            this.btnTienMat.Text = "Tiền mặt";
            this.btnTienMat.Click += new System.EventHandler(this.BtnTienMat_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(255, 3);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(80, 40);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            // 
            // pnlKhach
            // 
            this.pnlKhach.Controls.Add(this.lblSoDuVi);
            this.pnlKhach.Controls.Add(this.lblTenKhach);
            this.pnlKhach.Controls.Add(this.btnBoChonKhach);
            this.pnlKhach.Controls.Add(this.btnTimKhach);
            this.pnlKhach.Controls.Add(this.txtTimKhach);
            this.pnlKhach.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlKhach.Location = new System.Drawing.Point(0, 355);
            this.pnlKhach.Name = "pnlKhach";
            this.pnlKhach.Size = new System.Drawing.Size(746, 45);
            this.pnlKhach.TabIndex = 0;
            // 
            // lblSoDuVi
            // 
            this.lblSoDuVi.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSoDuVi.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSoDuVi.Location = new System.Drawing.Point(440, 0);
            this.lblSoDuVi.Name = "lblSoDuVi";
            this.lblSoDuVi.Size = new System.Drawing.Size(120, 45);
            this.lblSoDuVi.TabIndex = 4;
            // 
            // lblTenKhach
            // 
            this.lblTenKhach.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTenKhach.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTenKhach.Location = new System.Drawing.Point(290, 0);
            this.lblTenKhach.Name = "lblTenKhach";
            this.lblTenKhach.Size = new System.Drawing.Size(150, 45);
            this.lblTenKhach.TabIndex = 3;
            // 
            // btnBoChonKhach
            // 
            this.btnBoChonKhach.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnBoChonKhach.Location = new System.Drawing.Point(260, 0);
            this.btnBoChonKhach.Name = "btnBoChonKhach";
            this.btnBoChonKhach.Size = new System.Drawing.Size(30, 45);
            this.btnBoChonKhach.TabIndex = 2;
            this.btnBoChonKhach.Text = "X";
            this.btnBoChonKhach.Visible = false;
            this.btnBoChonKhach.Click += new System.EventHandler(this.BtnBoChonKhach_Click);
            // 
            // btnTimKhach
            // 
            this.btnTimKhach.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnTimKhach.Location = new System.Drawing.Point(200, 0);
            this.btnTimKhach.Name = "btnTimKhach";
            this.btnTimKhach.Size = new System.Drawing.Size(60, 45);
            this.btnTimKhach.TabIndex = 1;
            this.btnTimKhach.Text = "Tìm";
            this.btnTimKhach.Click += new System.EventHandler(this.BtnTimKhach_Click);
            // 
            // txtTimKhach
            // 
            this.txtTimKhach.Dock = System.Windows.Forms.DockStyle.Left;
            this.txtTimKhach.Location = new System.Drawing.Point(0, 0);
            this.txtTimKhach.Name = "txtTimKhach";
            this.txtTimKhach.Properties.AutoHeight = false;
            this.txtTimKhach.Properties.NullValuePrompt = "SĐT / Quét RFID...";
            this.txtTimKhach.Size = new System.Drawing.Size(200, 45);
            this.txtTimKhach.TabIndex = 0;
            this.txtTimKhach.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtTimKhach_KeyDown);
            // 
            // pnlGiamSat
            // 
            this.pnlGiamSat.Controls.Add(this.gridChuaTra);
            this.pnlGiamSat.Controls.Add(this.btnRefresh);
            this.pnlGiamSat.Controls.Add(this.lblGiamSatTitle);
            this.pnlGiamSat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGiamSat.Location = new System.Drawing.Point(0, 0);
            this.pnlGiamSat.Name = "pnlGiamSat";
            this.pnlGiamSat.Size = new System.Drawing.Size(1100, 251);
            this.pnlGiamSat.TabIndex = 0;
            // 
            // gridChuaTra
            // 
            this.gridChuaTra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridChuaTra.Location = new System.Drawing.Point(0, 53);
            this.gridChuaTra.MainView = this.viewChuaTra;
            this.gridChuaTra.Name = "gridChuaTra";
            this.gridChuaTra.Size = new System.Drawing.Size(1100, 198);
            this.gridChuaTra.TabIndex = 2;
            this.gridChuaTra.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewChuaTra});
            // 
            // viewChuaTra
            // 
            this.viewChuaTra.GridControl = this.gridChuaTra;
            this.viewChuaTra.Name = "viewChuaTra";
            this.viewChuaTra.OptionsBehavior.Editable = false;
            this.viewChuaTra.OptionsBehavior.ReadOnly = true;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnRefresh.Location = new System.Drawing.Point(0, 23);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(1100, 30);
            this.btnRefresh.TabIndex = 1;
            this.btnRefresh.Text = "Làm mới";
            this.btnRefresh.Click += new System.EventHandler(this.BtnRefresh_Click);
            // 
            // lblGiamSatTitle
            // 
            this.lblGiamSatTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGiamSatTitle.Location = new System.Drawing.Point(0, 0);
            this.lblGiamSatTitle.Name = "lblGiamSatTitle";
            this.lblGiamSatTitle.Padding = new System.Windows.Forms.Padding(5);
            this.lblGiamSatTitle.Size = new System.Drawing.Size(121, 23);
            this.lblGiamSatTitle.TabIndex = 0;
            this.lblGiamSatTitle.Text = "Đang cho thuê tại trạm";
            // 
            // pnlBanner
            // 
            this.pnlBanner.Controls.Add(this.lblTitle);
            this.pnlBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBanner.Location = new System.Drawing.Point(0, 0);
            this.pnlBanner.Name = "pnlBanner";
            this.pnlBanner.Size = new System.Drawing.Size(1100, 45);
            this.pnlBanner.TabIndex = 0;
            this.pnlBanner.Visible = false;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(70, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CHO THUÊ ĐỒ";
            // 
            // ucGiaoDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlBanner);
            this.Name = "ucGiaoDo";
            this.Size = new System.Drawing.Size(1100, 700);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.splitThaoTac.Panel1.ResumeLayout(false);
            this.splitThaoTac.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitThaoTac)).EndInit();
            this.splitThaoTac.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhoView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridGioThue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewGioThue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repBtnXoa)).EndInit();
            this.pnlTong.ResumeLayout(false);
            this.pnlTong.PerformLayout();
            this.pnlNut.ResumeLayout(false);
            this.pnlKhach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKhach.Properties)).EndInit();
            this.pnlGiamSat.ResumeLayout(false);
            this.pnlGiamSat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridChuaTra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewChuaTra)).EndInit();
            this.pnlBanner.ResumeLayout(false);
            this.pnlBanner.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion


        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.SplitContainer splitThaoTac;
        private System.Windows.Forms.Panel pnlBanner;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.SearchLookUpEdit slkKho;
        private DevExpress.XtraGrid.Views.Grid.GridView slkKhoView;
        private DevExpress.XtraEditors.TextEdit txtBarcode;

        private DevExpress.XtraGrid.GridControl gridSanPham;
        private DevExpress.XtraGrid.Views.Grid.GridView viewSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colSP_TenSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colSP_TienThue;
        private DevExpress.XtraGrid.Columns.GridColumn colSP_TienCoc;

        private DevExpress.XtraGrid.GridControl gridGioThue;
        private DevExpress.XtraGrid.Views.Grid.GridView viewGioThue;
        private DevExpress.XtraGrid.Columns.GridColumn colGT_TenSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colGT_SoLuong;
        private DevExpress.XtraGrid.Columns.GridColumn colGT_TienThue;
        private DevExpress.XtraGrid.Columns.GridColumn colGT_TienCoc;
        private DevExpress.XtraGrid.Columns.GridColumn colGT_Tong;
        private DevExpress.XtraGrid.Columns.GridColumn colGT_Xoa;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repBtnXoa;

        private System.Windows.Forms.Panel pnlTong;
        private DevExpress.XtraEditors.LabelControl lblTienThueLabel;
        private DevExpress.XtraEditors.LabelControl lblTienThueValue;
        private DevExpress.XtraEditors.LabelControl lblTienCocLabel;
        private DevExpress.XtraEditors.LabelControl lblTienCocValue;
        private DevExpress.XtraEditors.LabelControl lblTongLabel;
        private DevExpress.XtraEditors.LabelControl lblTongValue;

        private System.Windows.Forms.FlowLayoutPanel pnlNut;
        private DevExpress.XtraEditors.SimpleButton btnRFID;
        private DevExpress.XtraEditors.SimpleButton btnTienMat;
        private DevExpress.XtraEditors.SimpleButton btnHuy;

        private System.Windows.Forms.Panel pnlKhach;
        private DevExpress.XtraEditors.TextEdit txtTimKhach;
        private DevExpress.XtraEditors.SimpleButton btnTimKhach;
        private DevExpress.XtraEditors.SimpleButton btnBoChonKhach;
        private DevExpress.XtraEditors.LabelControl lblTenKhach;
        private DevExpress.XtraEditors.LabelControl lblSoDuVi;

        private System.Windows.Forms.Panel pnlGiamSat;
        private DevExpress.XtraEditors.LabelControl lblGiamSatTitle;
        private DevExpress.XtraEditors.SimpleButton btnRefresh;
        private DevExpress.XtraGrid.GridControl gridChuaTra;
        private DevExpress.XtraGrid.Views.Grid.GridView viewChuaTra;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
    }
}
