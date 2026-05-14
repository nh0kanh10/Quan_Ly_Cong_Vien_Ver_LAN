namespace GUI.Modules.DanhMuc
{
    partial class frmSanPham
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }



        #region Windows Form Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pnlBanner = new System.Windows.Forms.Panel();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.txtTimKiem = new DevExpress.XtraEditors.TextEdit();
            this.btnLamMoi = new DevExpress.XtraEditors.SimpleButton();
            this.btnThemMoi = new DevExpress.XtraEditors.SimpleButton();
            this.pnlFilter = new System.Windows.Forms.FlowLayoutPanel();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoaiSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrangThai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHanhDong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoBtnXoa = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.lblTongSP = new DevExpress.XtraEditors.LabelControl();
            this.pnlBanner.SuspendLayout();
            this.pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoBtnXoa)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBanner
            // 
            this.pnlBanner.Controls.Add(this.lblTitle);
            this.pnlBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBanner.Location = new System.Drawing.Point(0, 0);
            this.pnlBanner.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlBanner.Name = "pnlBanner";
            this.pnlBanner.Padding = new System.Windows.Forms.Padding(17, 10, 17, 10);
            this.pnlBanner.Size = new System.Drawing.Size(1029, 42);
            this.pnlBanner.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(17, 10);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(995, 22);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ SẢN PHẨM & DỊCH VỤ ĐẠI NAM";
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.txtTimKiem);
            this.pnlToolbar.Controls.Add(this.btnLamMoi);
            this.pnlToolbar.Controls.Add(this.btnThemMoi);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 42);
            this.pnlToolbar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(14, 8, 14, 8);
            this.pnlToolbar.Size = new System.Drawing.Size(1029, 42);
            this.pnlToolbar.TabIndex = 2;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Dock = System.Windows.Forms.DockStyle.Right;
            this.txtTimKiem.Location = new System.Drawing.Point(741, 8);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Properties.NullValuePrompt = "Tìm theo tên, mã sản phẩm...";
            this.txtTimKiem.Size = new System.Drawing.Size(274, 20);
            this.txtTimKiem.TabIndex = 0;
            this.txtTimKiem.EditValueChanged += new System.EventHandler(this.txtTimKiem_EditValueChanged);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.ImageOptions.ImageUri.Uri = "Refresh";
            this.btnLamMoi.Location = new System.Drawing.Point(185, 8);
            this.btnLamMoi.Margin = new System.Windows.Forms.Padding(7, 0, 0, 0);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(94, 26);
            this.btnLamMoi.TabIndex = 1;
            this.btnLamMoi.Text = "  Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.ImageOptions.ImageUri.Uri = "Add";
            this.btnThemMoi.Location = new System.Drawing.Point(14, 8);
            this.btnThemMoi.Margin = new System.Windows.Forms.Padding(0, 0, 7, 0);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(171, 26);
            this.btnThemMoi.TabIndex = 2;
            this.btnThemMoi.Text = "  + Thêm sản phẩm mới";
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            // 
            // pnlFilter
            // 
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 84);
            this.pnlFilter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(12, 5, 12, 5);
            this.pnlFilter.Size = new System.Drawing.Size(1029, 36);
            this.pnlFilter.TabIndex = 1;
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridControl.Location = new System.Drawing.Point(0, 120);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gridControl.Name = "gridControl";
            this.gridControl.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoBtnXoa});
            this.gridControl.Size = new System.Drawing.Size(1029, 421);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaSanPham,
            this.colTenSanPham,
            this.colDonGia,
            this.colLoaiSanPham,
            this.colTrangThai,
            this.colHanhDong});
            this.gridView.DetailHeight = 284;
            this.gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridView_RowCellStyle);
            this.gridView.DoubleClick += new System.EventHandler(this.gridView_DoubleClick);
            // 
            // colMaSanPham
            // 
            this.colMaSanPham.Caption = "Mã sản phẩm";
            this.colMaSanPham.FieldName = "MaSanPham";
            this.colMaSanPham.Name = "colMaSanPham";
            this.colMaSanPham.OptionsColumn.AllowEdit = false;
            this.colMaSanPham.Visible = true;
            this.colMaSanPham.VisibleIndex = 0;
            this.colMaSanPham.Width = 100;
            // 
            // colTenSanPham
            // 
            this.colTenSanPham.Caption = "Tên sản phẩm";
            this.colTenSanPham.FieldName = "TenSanPham";
            this.colTenSanPham.Name = "colTenSanPham";
            this.colTenSanPham.OptionsColumn.AllowEdit = false;
            this.colTenSanPham.Visible = true;
            this.colTenSanPham.VisibleIndex = 1;
            this.colTenSanPham.Width = 260;
            // 
            // colDonGia
            // 
            this.colDonGia.Caption = "Đơn giá";
            this.colDonGia.DisplayFormat.FormatString = "N0";
            this.colDonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.OptionsColumn.AllowEdit = false;
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 2;
            this.colDonGia.Width = 100;
            // 
            // colLoaiSanPham
            // 
            this.colLoaiSanPham.Caption = "Loại sản phẩm";
            this.colLoaiSanPham.FieldName = "LoaiSanPham";
            this.colLoaiSanPham.Name = "colLoaiSanPham";
            this.colLoaiSanPham.OptionsColumn.AllowEdit = false;
            this.colLoaiSanPham.Visible = true;
            this.colLoaiSanPham.VisibleIndex = 3;
            this.colLoaiSanPham.Width = 120;
            // 
            // colTrangThai
            // 
            this.colTrangThai.Caption = "Trạng thái";
            this.colTrangThai.FieldName = "TrangThai";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.OptionsColumn.AllowEdit = false;
            this.colTrangThai.Visible = true;
            this.colTrangThai.VisibleIndex = 4;
            this.colTrangThai.Width = 110;
            // 
            // colHanhDong
            // 
            this.colHanhDong.Caption = "Hành động";
            this.colHanhDong.ColumnEdit = this.repoBtnXoa;
            this.colHanhDong.FieldName = "Id";
            this.colHanhDong.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.colHanhDong.Name = "colHanhDong";
            this.colHanhDong.OptionsFilter.AllowFilter = false;
            this.colHanhDong.Visible = true;
            this.colHanhDong.VisibleIndex = 5;
            this.colHanhDong.Width = 60;
            // 
            // repoBtnXoa
            // 
            this.repoBtnXoa.AutoHeight = false;
            serializableAppearanceObject1.ForeColor = System.Drawing.Color.Firebrick;
            serializableAppearanceObject1.Options.UseForeColor = true;
            this.repoBtnXoa.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Xóa", 50, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", "DELETE", null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repoBtnXoa.Name = "repoBtnXoa";
            this.repoBtnXoa.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repoBtnXoa.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.RepoBtn_ButtonClick);
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.lblTongSP);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 541);
            this.pnlBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Padding = new System.Windows.Forms.Padding(14, 6, 14, 6);
            this.pnlBottom.Size = new System.Drawing.Size(1029, 28);
            this.pnlBottom.TabIndex = 4;
            // 
            // lblTongSP
            // 
            this.lblTongSP.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblTongSP.Location = new System.Drawing.Point(929, 6);
            this.lblTongSP.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblTongSP.Name = "lblTongSP";
            this.lblTongSP.Size = new System.Drawing.Size(86, 13);
            this.lblTongSP.TabIndex = 0;
            this.lblTongSP.Text = "Tổng: 0 sản phẩm";
            // 
            // frmSanPham
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.pnlFilter);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlBanner);
            this.Controls.Add(this.pnlBottom);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmSanPham";
            this.Size = new System.Drawing.Size(1029, 569);
            this.Load += new System.EventHandler(this.frmSanPham_Load);
            this.pnlBanner.ResumeLayout(false);
            this.pnlToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoBtnXoa)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel pnlBanner;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private System.Windows.Forms.Panel pnlToolbar;
        private DevExpress.XtraEditors.TextEdit txtTimKiem;
        private DevExpress.XtraEditors.SimpleButton btnLamMoi;
        private DevExpress.XtraEditors.SimpleButton btnThemMoi;
        private System.Windows.Forms.FlowLayoutPanel pnlFilter;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colMaSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colTenSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colDonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colLoaiSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colTrangThai;
        private DevExpress.XtraGrid.Columns.GridColumn colHanhDong;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repoBtnXoa;
        private System.Windows.Forms.Panel pnlBottom;
        private DevExpress.XtraEditors.LabelControl lblTongSP;
        private System.ComponentModel.IContainer components = null;





    }}