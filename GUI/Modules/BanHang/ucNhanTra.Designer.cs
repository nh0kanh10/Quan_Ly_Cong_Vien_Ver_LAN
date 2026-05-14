namespace GUI.Modules.BanHang
{
    partial class ucNhanTra
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
            this.gridTraDo = new DevExpress.XtraGrid.GridControl();
            this.viewTraDo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTD_TenSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTD_SoLuongThue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTD_ThoiGianThue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTD_TienCoc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTD_SoLuongTra = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinSoLuongTra = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colTD_SoLuongMat = new DevExpress.XtraGrid.Columns.GridColumn();
            this.spinSoLuongMat = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.pnlTongTra = new System.Windows.Forms.Panel();
            this.lblHoanCocValue = new DevExpress.XtraEditors.LabelControl();
            this.lblHoanCocLabel = new DevExpress.XtraEditors.LabelControl();
            this.lblPhatValue = new DevExpress.XtraEditors.LabelControl();
            this.lblPhatLabel = new DevExpress.XtraEditors.LabelControl();
            this.pnlNutTra = new System.Windows.Forms.FlowLayoutPanel();
            this.btnXacNhanTra = new DevExpress.XtraEditors.SimpleButton();
            this.btnTraHet = new DevExpress.XtraEditors.SimpleButton();
            this.pnlTimKiem = new System.Windows.Forms.Panel();
            this.lblKhachHang = new DevExpress.XtraEditors.LabelControl();
            this.btnTimRFID = new DevExpress.XtraEditors.SimpleButton();
            this.txtRFID = new DevExpress.XtraEditors.TextEdit();
            this.btnTimDon = new DevExpress.XtraEditors.SimpleButton();
            this.txtMaDon = new DevExpress.XtraEditors.TextEdit();
            this.pnlGiamSat = new System.Windows.Forms.Panel();
            this.gridChuaTra = new DevExpress.XtraGrid.GridControl();
            this.viewChuaTra = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlLocNgay = new System.Windows.Forms.FlowLayoutPanel();
            this.lblGiamSatTitle = new DevExpress.XtraEditors.LabelControl();
            this.dtTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.dtDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.btnXem = new DevExpress.XtraEditors.SimpleButton();
            this.pnlBanner = new System.Windows.Forms.Panel();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTraDo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTraDo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoLuongTra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoLuongMat)).BeginInit();
            this.pnlTongTra.SuspendLayout();
            this.pnlNutTra.SuspendLayout();
            this.pnlTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRFID.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDon.Properties)).BeginInit();
            this.pnlGiamSat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridChuaTra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewChuaTra)).BeginInit();
            this.pnlLocNgay.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).BeginInit();
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
            this.splitMain.Panel1.Controls.Add(this.gridTraDo);
            this.splitMain.Panel1.Controls.Add(this.pnlTongTra);
            this.splitMain.Panel1.Controls.Add(this.pnlNutTra);
            this.splitMain.Panel1.Controls.Add(this.pnlTimKiem);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlGiamSat);
            this.splitMain.Size = new System.Drawing.Size(1100, 655);
            this.splitMain.SplitterDistance = 380;
            this.splitMain.TabIndex = 1;
            // 
            // gridTraDo
            // 
            this.gridTraDo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTraDo.Location = new System.Drawing.Point(0, 50);
            this.gridTraDo.MainView = this.viewTraDo;
            this.gridTraDo.Name = "gridTraDo";
            this.gridTraDo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.spinSoLuongTra,
            this.spinSoLuongMat});
            this.gridTraDo.Size = new System.Drawing.Size(1100, 240);
            this.gridTraDo.TabIndex = 3;
            this.gridTraDo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewTraDo});
            // 
            // viewTraDo
            // 
            this.viewTraDo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTD_TenSanPham,
            this.colTD_SoLuongThue,
            this.colTD_ThoiGianThue,
            this.colTD_TienCoc,
            this.colTD_SoLuongTra,
            this.colTD_SoLuongMat});
            this.viewTraDo.GridControl = this.gridTraDo;
            this.viewTraDo.Name = "viewTraDo";
            this.viewTraDo.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.ViewTraDo_CellValueChanged);
            // 
            // colTD_TenSanPham
            // 
            this.colTD_TenSanPham.Caption = "Sản phẩm";
            this.colTD_TenSanPham.FieldName = "TenSanPham";
            this.colTD_TenSanPham.Name = "colTD_TenSanPham";
            this.colTD_TenSanPham.OptionsColumn.AllowEdit = false;
            this.colTD_TenSanPham.Visible = true;
            this.colTD_TenSanPham.VisibleIndex = 0;
            // 
            // colTD_SoLuongThue
            // 
            this.colTD_SoLuongThue.Caption = "SL thuê";
            this.colTD_SoLuongThue.FieldName = "SoLuong";
            this.colTD_SoLuongThue.Name = "colTD_SoLuongThue";
            this.colTD_SoLuongThue.OptionsColumn.AllowEdit = false;
            this.colTD_SoLuongThue.Visible = true;
            this.colTD_SoLuongThue.VisibleIndex = 1;
            this.colTD_SoLuongThue.Width = 60;
            // 
            // colTD_ThoiGianThue
            // 
            this.colTD_ThoiGianThue.Caption = "Bắt đầu";
            this.colTD_ThoiGianThue.DisplayFormat.FormatString = "dd/MM HH:mm";
            this.colTD_ThoiGianThue.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colTD_ThoiGianThue.FieldName = "ThoiGianThue";
            this.colTD_ThoiGianThue.Name = "colTD_ThoiGianThue";
            this.colTD_ThoiGianThue.OptionsColumn.AllowEdit = false;
            this.colTD_ThoiGianThue.Visible = true;
            this.colTD_ThoiGianThue.VisibleIndex = 2;
            // 
            // colTD_TienCoc
            // 
            this.colTD_TienCoc.Caption = "Cọc";
            this.colTD_TienCoc.DisplayFormat.FormatString = "N0";
            this.colTD_TienCoc.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTD_TienCoc.FieldName = "TienCoc";
            this.colTD_TienCoc.Name = "colTD_TienCoc";
            this.colTD_TienCoc.OptionsColumn.AllowEdit = false;
            this.colTD_TienCoc.Visible = true;
            this.colTD_TienCoc.VisibleIndex = 3;
            // 
            // colTD_SoLuongTra
            // 
            this.colTD_SoLuongTra.Caption = "Trả SL";
            this.colTD_SoLuongTra.ColumnEdit = this.spinSoLuongTra;
            this.colTD_SoLuongTra.FieldName = "SoLuongTra";
            this.colTD_SoLuongTra.Name = "colTD_SoLuongTra";
            this.colTD_SoLuongTra.Visible = true;
            this.colTD_SoLuongTra.VisibleIndex = 4;
            this.colTD_SoLuongTra.Width = 60;
            // 
            // spinSoLuongTra
            // 
            this.spinSoLuongTra.AutoHeight = false;
            this.spinSoLuongTra.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinSoLuongTra.IsFloatValue = false;
            this.spinSoLuongTra.MaskSettings.Set("mask", "N00");
            this.spinSoLuongTra.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.spinSoLuongTra.Name = "spinSoLuongTra";
            // 
            // colTD_SoLuongMat
            // 
            this.colTD_SoLuongMat.Caption = "Mất";
            this.colTD_SoLuongMat.ColumnEdit = this.spinSoLuongMat;
            this.colTD_SoLuongMat.FieldName = "SoLuongMat";
            this.colTD_SoLuongMat.Name = "colTD_SoLuongMat";
            this.colTD_SoLuongMat.Visible = true;
            this.colTD_SoLuongMat.VisibleIndex = 5;
            this.colTD_SoLuongMat.Width = 60;
            // 
            // spinSoLuongMat
            // 
            this.spinSoLuongMat.AutoHeight = false;
            this.spinSoLuongMat.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinSoLuongMat.IsFloatValue = false;
            this.spinSoLuongMat.MaskSettings.Set("mask", "N00");
            this.spinSoLuongMat.MaxValue = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.spinSoLuongMat.Name = "spinSoLuongMat";
            // 
            // pnlTongTra
            // 
            this.pnlTongTra.Controls.Add(this.lblHoanCocValue);
            this.pnlTongTra.Controls.Add(this.lblHoanCocLabel);
            this.pnlTongTra.Controls.Add(this.lblPhatValue);
            this.pnlTongTra.Controls.Add(this.lblPhatLabel);
            this.pnlTongTra.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTongTra.Location = new System.Drawing.Point(0, 290);
            this.pnlTongTra.Name = "pnlTongTra";
            this.pnlTongTra.Size = new System.Drawing.Size(1100, 40);
            this.pnlTongTra.TabIndex = 2;
            // 
            // lblHoanCocValue
            // 
            this.lblHoanCocValue.Location = new System.Drawing.Point(120, 10);
            this.lblHoanCocValue.Name = "lblHoanCocValue";
            this.lblHoanCocValue.Size = new System.Drawing.Size(6, 13);
            this.lblHoanCocValue.TabIndex = 1;
            this.lblHoanCocValue.Text = "0";
            // 
            // lblHoanCocLabel
            // 
            this.lblHoanCocLabel.Location = new System.Drawing.Point(10, 10);
            this.lblHoanCocLabel.Name = "lblHoanCocLabel";
            this.lblHoanCocLabel.Size = new System.Drawing.Size(48, 13);
            this.lblHoanCocLabel.TabIndex = 0;
            this.lblHoanCocLabel.Text = "Hoàn cọc:";
            // 
            // lblPhatValue
            // 
            this.lblPhatValue.Location = new System.Drawing.Point(320, 10);
            this.lblPhatValue.Name = "lblPhatValue";
            this.lblPhatValue.Size = new System.Drawing.Size(6, 13);
            this.lblPhatValue.TabIndex = 3;
            this.lblPhatValue.Text = "0";
            // 
            // lblPhatLabel
            // 
            this.lblPhatLabel.Location = new System.Drawing.Point(250, 10);
            this.lblPhatLabel.Name = "lblPhatLabel";
            this.lblPhatLabel.Size = new System.Drawing.Size(26, 13);
            this.lblPhatLabel.TabIndex = 2;
            this.lblPhatLabel.Text = "Phạt:";
            // 
            // pnlNutTra
            // 
            this.pnlNutTra.Controls.Add(this.btnXacNhanTra);
            this.pnlNutTra.Controls.Add(this.btnTraHet);
            this.pnlNutTra.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlNutTra.Location = new System.Drawing.Point(0, 330);
            this.pnlNutTra.Name = "pnlNutTra";
            this.pnlNutTra.Size = new System.Drawing.Size(1100, 50);
            this.pnlNutTra.TabIndex = 1;
            // 
            // btnXacNhanTra
            // 
            this.btnXacNhanTra.Location = new System.Drawing.Point(3, 3);
            this.btnXacNhanTra.Name = "btnXacNhanTra";
            this.btnXacNhanTra.Size = new System.Drawing.Size(140, 40);
            this.btnXacNhanTra.TabIndex = 0;
            this.btnXacNhanTra.Text = "Xác nhận trả";
            this.btnXacNhanTra.Click += new System.EventHandler(this.BtnXacNhanTra_Click);
            // 
            // btnTraHet
            // 
            this.btnTraHet.Location = new System.Drawing.Point(149, 3);
            this.btnTraHet.Name = "btnTraHet";
            this.btnTraHet.Size = new System.Drawing.Size(130, 40);
            this.btnTraHet.TabIndex = 1;
            this.btnTraHet.Text = "Trả hết (F12)";
            this.btnTraHet.Click += new System.EventHandler(this.BtnTraHet_Click);
            // 
            // pnlTimKiem
            // 
            this.pnlTimKiem.Controls.Add(this.lblKhachHang);
            this.pnlTimKiem.Controls.Add(this.btnTimRFID);
            this.pnlTimKiem.Controls.Add(this.txtRFID);
            this.pnlTimKiem.Controls.Add(this.btnTimDon);
            this.pnlTimKiem.Controls.Add(this.txtMaDon);
            this.pnlTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimKiem.Location = new System.Drawing.Point(0, 0);
            this.pnlTimKiem.Name = "pnlTimKiem";
            this.pnlTimKiem.Size = new System.Drawing.Size(1100, 50);
            this.pnlTimKiem.TabIndex = 0;
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.Location = new System.Drawing.Point(570, 15);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(0, 13);
            this.lblKhachHang.TabIndex = 4;
            // 
            // btnTimRFID
            // 
            this.btnTimRFID.Location = new System.Drawing.Point(495, 10);
            this.btnTimRFID.Name = "btnTimRFID";
            this.btnTimRFID.Size = new System.Drawing.Size(55, 28);
            this.btnTimRFID.TabIndex = 3;
            this.btnTimRFID.Text = "Tìm";
            this.btnTimRFID.Click += new System.EventHandler(this.BtnTimRFID_Click);
            // 
            // txtRFID
            // 
            this.txtRFID.Location = new System.Drawing.Point(290, 12);
            this.txtRFID.Name = "txtRFID";
            this.txtRFID.Properties.NullValuePrompt = "Quẹt RFID khách...";
            this.txtRFID.Size = new System.Drawing.Size(200, 20);
            this.txtRFID.TabIndex = 2;
            this.txtRFID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtRFID_KeyDown);
            // 
            // btnTimDon
            // 
            this.btnTimDon.Location = new System.Drawing.Point(215, 10);
            this.btnTimDon.Name = "btnTimDon";
            this.btnTimDon.Size = new System.Drawing.Size(55, 28);
            this.btnTimDon.TabIndex = 1;
            this.btnTimDon.Text = "Tìm";
            this.btnTimDon.Click += new System.EventHandler(this.BtnTimDon_Click);
            // 
            // txtMaDon
            // 
            this.txtMaDon.Location = new System.Drawing.Point(10, 12);
            this.txtMaDon.Name = "txtMaDon";
            this.txtMaDon.Properties.NullValuePrompt = "Nhập mã biên lai DT-xxx...";
            this.txtMaDon.Size = new System.Drawing.Size(200, 20);
            this.txtMaDon.TabIndex = 0;
            this.txtMaDon.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtMaDon_KeyDown);
            // 
            // pnlGiamSat
            // 
            this.pnlGiamSat.Controls.Add(this.gridChuaTra);
            this.pnlGiamSat.Controls.Add(this.pnlLocNgay);
            this.pnlGiamSat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlGiamSat.Location = new System.Drawing.Point(0, 0);
            this.pnlGiamSat.Name = "pnlGiamSat";
            this.pnlGiamSat.Size = new System.Drawing.Size(1100, 271);
            this.pnlGiamSat.TabIndex = 0;
            // 
            // gridChuaTra
            // 
            this.gridChuaTra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridChuaTra.Location = new System.Drawing.Point(0, 35);
            this.gridChuaTra.MainView = this.viewChuaTra;
            this.gridChuaTra.Name = "gridChuaTra";
            this.gridChuaTra.Size = new System.Drawing.Size(1100, 236);
            this.gridChuaTra.TabIndex = 1;
            this.gridChuaTra.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewChuaTra});
            // 
            // viewChuaTra
            // 
            this.viewChuaTra.GridControl = this.gridChuaTra;
            this.viewChuaTra.Name = "viewChuaTra";
            this.viewChuaTra.OptionsBehavior.Editable = false;
            this.viewChuaTra.OptionsBehavior.ReadOnly = true;
            this.viewChuaTra.DoubleClick += new System.EventHandler(this.ViewChuaTra_DoubleClick);
            // 
            // pnlLocNgay
            // 
            this.pnlLocNgay.Controls.Add(this.lblGiamSatTitle);
            this.pnlLocNgay.Controls.Add(this.dtTuNgay);
            this.pnlLocNgay.Controls.Add(this.dtDenNgay);
            this.pnlLocNgay.Controls.Add(this.btnXem);
            this.pnlLocNgay.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLocNgay.Location = new System.Drawing.Point(0, 0);
            this.pnlLocNgay.Name = "pnlLocNgay";
            this.pnlLocNgay.Size = new System.Drawing.Size(1100, 35);
            this.pnlLocNgay.TabIndex = 0;
            // 
            // lblGiamSatTitle
            // 
            this.lblGiamSatTitle.Location = new System.Drawing.Point(3, 3);
            this.lblGiamSatTitle.Name = "lblGiamSatTitle";
            this.lblGiamSatTitle.Padding = new System.Windows.Forms.Padding(5, 5, 10, 0);
            this.lblGiamSatTitle.Size = new System.Drawing.Size(85, 18);
            this.lblGiamSatTitle.TabIndex = 0;
            this.lblGiamSatTitle.Text = "Phiên chưa trả";
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.EditValue = new System.DateTime(2023, 10, 1, 0, 0, 0, 0);
            this.dtTuNgay.Location = new System.Drawing.Point(94, 3);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTuNgay.Size = new System.Drawing.Size(120, 20);
            this.dtTuNgay.TabIndex = 1;
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.EditValue = new System.DateTime(2023, 10, 1, 0, 0, 0, 0);
            this.dtDenNgay.Location = new System.Drawing.Point(220, 3);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Size = new System.Drawing.Size(120, 20);
            this.dtDenNgay.TabIndex = 2;
            // 
            // btnXem
            // 
            this.btnXem.Location = new System.Drawing.Point(346, 3);
            this.btnXem.Name = "btnXem";
            this.btnXem.Size = new System.Drawing.Size(60, 28);
            this.btnXem.TabIndex = 3;
            this.btnXem.Text = "Xem";
            this.btnXem.Click += new System.EventHandler(this.BtnXem_Click);
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
            this.lblTitle.Text = "NHẬN TRẢ ĐỒ";
            // 
            // ucNhanTra
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlBanner);
            this.Name = "ucNhanTra";
            this.Size = new System.Drawing.Size(1100, 700);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTraDo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTraDo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoLuongTra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoLuongMat)).EndInit();
            this.pnlTongTra.ResumeLayout(false);
            this.pnlTongTra.PerformLayout();
            this.pnlNutTra.ResumeLayout(false);
            this.pnlTimKiem.ResumeLayout(false);
            this.pnlTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtRFID.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDon.Properties)).EndInit();
            this.pnlGiamSat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridChuaTra)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewChuaTra)).EndInit();
            this.pnlLocNgay.ResumeLayout(false);
            this.pnlLocNgay.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).EndInit();
            this.pnlBanner.ResumeLayout(false);
            this.pnlBanner.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitMain;
        private System.Windows.Forms.Panel pnlBanner;
        private DevExpress.XtraEditors.LabelControl lblTitle;

        private System.Windows.Forms.Panel pnlTimKiem;
        private DevExpress.XtraEditors.TextEdit txtMaDon;
        private DevExpress.XtraEditors.SimpleButton btnTimDon;
        private DevExpress.XtraEditors.TextEdit txtRFID;
        private DevExpress.XtraEditors.SimpleButton btnTimRFID;
        private DevExpress.XtraEditors.LabelControl lblKhachHang;

        private DevExpress.XtraGrid.GridControl gridTraDo;
        private DevExpress.XtraGrid.Views.Grid.GridView viewTraDo;
        private DevExpress.XtraGrid.Columns.GridColumn colTD_TenSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colTD_SoLuongThue;
        private DevExpress.XtraGrid.Columns.GridColumn colTD_ThoiGianThue;
        private DevExpress.XtraGrid.Columns.GridColumn colTD_TienCoc;
        private DevExpress.XtraGrid.Columns.GridColumn colTD_SoLuongTra;
        private DevExpress.XtraGrid.Columns.GridColumn colTD_SoLuongMat;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinSoLuongTra;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit spinSoLuongMat;

        private System.Windows.Forms.Panel pnlTongTra;
        private DevExpress.XtraEditors.LabelControl lblHoanCocLabel;
        private DevExpress.XtraEditors.LabelControl lblHoanCocValue;
        private DevExpress.XtraEditors.LabelControl lblPhatLabel;
        private DevExpress.XtraEditors.LabelControl lblPhatValue;

        private System.Windows.Forms.FlowLayoutPanel pnlNutTra;
        private DevExpress.XtraEditors.SimpleButton btnXacNhanTra;
        private DevExpress.XtraEditors.SimpleButton btnTraHet;

        private System.Windows.Forms.Panel pnlGiamSat;
        private System.Windows.Forms.FlowLayoutPanel pnlLocNgay;
        private DevExpress.XtraEditors.DateEdit dtTuNgay;
        private DevExpress.XtraEditors.DateEdit dtDenNgay;
        private DevExpress.XtraEditors.SimpleButton btnXem;
        private DevExpress.XtraEditors.LabelControl lblGiamSatTitle;
        private DevExpress.XtraGrid.GridControl gridChuaTra;
        private DevExpress.XtraGrid.Views.Grid.GridView viewChuaTra;
    }
}
