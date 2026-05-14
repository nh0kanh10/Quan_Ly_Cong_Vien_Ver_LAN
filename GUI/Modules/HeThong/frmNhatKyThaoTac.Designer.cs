namespace GUI.Modules.HeThong
{
    partial class frmNhatKyThaoTac
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlFilter = new DevExpress.XtraEditors.PanelControl();
            this.tblFilter = new System.Windows.Forms.TableLayoutPanel();
            this.lblTuNgay = new DevExpress.XtraEditors.LabelControl();
            this.dtTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.lblDenNgay = new DevExpress.XtraEditors.LabelControl();
            this.dtDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.lblModule = new DevExpress.XtraEditors.LabelControl();
            this.cboModule = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnTimKiem = new DevExpress.XtraEditors.SimpleButton();
            this.btnXuatExcel = new DevExpress.XtraEditors.SimpleButton();
            this.lblSoDong = new DevExpress.XtraEditors.LabelControl();
            this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridLog = new DevExpress.XtraGrid.GridControl();
            this.gridViewLog = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colThoiGian = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNguoiThucHien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThucThe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHanhDong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIdThucThe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGhiChu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlDetail = new DevExpress.XtraEditors.PanelControl();
            this.gridDiff = new DevExpress.XtraGrid.GridControl();
            this.gridViewDiff = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDiffField = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiffOld = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDiffNew = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblDetailTitle = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFilter)).BeginInit();
            this.pnlFilter.SuspendLayout();
            this.tblFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboModule.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).BeginInit();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLog)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDetail)).BeginInit();
            this.pnlDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDiff)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDiff)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.tblFilter);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(1200, 45);
            this.pnlFilter.TabIndex = 0;
            // 
            // tblFilter
            // 
            this.tblFilter.ColumnCount = 9;
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tblFilter.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFilter.Controls.Add(this.lblTuNgay, 0, 0);
            this.tblFilter.Controls.Add(this.dtTuNgay, 1, 0);
            this.tblFilter.Controls.Add(this.lblDenNgay, 2, 0);
            this.tblFilter.Controls.Add(this.dtDenNgay, 3, 0);
            this.tblFilter.Controls.Add(this.lblModule, 4, 0);
            this.tblFilter.Controls.Add(this.cboModule, 5, 0);
            this.tblFilter.Controls.Add(this.btnTimKiem, 6, 0);
            this.tblFilter.Controls.Add(this.btnXuatExcel, 7, 0);
            this.tblFilter.Controls.Add(this.lblSoDong, 8, 0);
            this.tblFilter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tblFilter.Location = new System.Drawing.Point(2, 2);
            this.tblFilter.Name = "tblFilter";
            this.tblFilter.Padding = new System.Windows.Forms.Padding(5, 0, 5, 0);
            this.tblFilter.RowCount = 1;
            this.tblFilter.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tblFilter.Size = new System.Drawing.Size(1196, 41);
            this.tblFilter.TabIndex = 0;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTuNgay.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTuNgay.Appearance.Options.UseFont = true;
            this.lblTuNgay.Location = new System.Drawing.Point(13, 12);
            this.lblTuNgay.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(50, 17);
            this.lblTuNgay.TabIndex = 8;
            this.lblTuNgay.Text = "Từ ngày:";
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtTuNgay.EditValue = null;
            this.dtTuNgay.Location = new System.Drawing.Point(265, 8);
            this.dtTuNgay.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtTuNgay.Properties.Appearance.Options.UseFont = true;
            this.dtTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtTuNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtTuNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtTuNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtTuNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtTuNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dtTuNgay.Size = new System.Drawing.Size(120, 24);
            this.dtTuNgay.TabIndex = 1;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblDenNgay.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblDenNgay.Appearance.Options.UseFont = true;
            this.lblDenNgay.Location = new System.Drawing.Point(204, 12);
            this.lblDenNgay.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(58, 17);
            this.lblDenNgay.TabIndex = 7;
            this.lblDenNgay.Text = "Đến ngày:";
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.dtDenNgay.EditValue = null;
            this.dtDenNgay.Location = new System.Drawing.Point(66, 8);
            this.dtDenNgay.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.dtDenNgay.Properties.Appearance.Options.UseFont = true;
            this.dtDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtDenNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtDenNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtDenNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtDenNgay.Properties.Mask.EditMask = "dd/MM/yyyy";
            this.dtDenNgay.Size = new System.Drawing.Size(120, 24);
            this.dtDenNgay.TabIndex = 2;
            // 
            // lblModule
            // 
            this.lblModule.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblModule.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblModule.Appearance.Options.UseFont = true;
            this.lblModule.Location = new System.Drawing.Point(403, 12);
            this.lblModule.Margin = new System.Windows.Forms.Padding(8, 0, 3, 0);
            this.lblModule.Name = "lblModule";
            this.lblModule.Size = new System.Drawing.Size(48, 17);
            this.lblModule.TabIndex = 6;
            this.lblModule.Text = "Module:";
            // 
            // cboModule
            // 
            this.cboModule.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.cboModule.Location = new System.Drawing.Point(454, 8);
            this.cboModule.Margin = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.cboModule.Name = "cboModule";
            this.cboModule.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboModule.Properties.Appearance.Options.UseFont = true;
            this.cboModule.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboModule.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboModule.Size = new System.Drawing.Size(140, 24);
            this.cboModule.TabIndex = 3;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnTimKiem.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnTimKiem.Appearance.Options.UseFont = true;
            this.btnTimKiem.ImageOptions.ImageUri.Uri = "Zoom";
            this.btnTimKiem.Location = new System.Drawing.Point(614, 6);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(10, 0, 5, 0);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(110, 28);
            this.btnTimKiem.TabIndex = 4;
            this.btnTimKiem.Text = "TÌM KIẾM";
            this.btnTimKiem.Click += new System.EventHandler(this.BtnTimKiem_Click);
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnXuatExcel.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXuatExcel.Appearance.Options.UseFont = true;
            this.btnXuatExcel.ImageOptions.ImageUri.Uri = "ExportToXlsx";
            this.btnXuatExcel.Location = new System.Drawing.Point(734, 6);
            this.btnXuatExcel.Margin = new System.Windows.Forms.Padding(5, 0, 10, 0);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(110, 28);
            this.btnXuatExcel.TabIndex = 5;
            this.btnXuatExcel.Text = "Xuất Excel";
            this.btnXuatExcel.Click += new System.EventHandler(this.BtnXuatExcel_Click);
            // 
            // lblSoDong
            // 
            this.lblSoDong.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblSoDong.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblSoDong.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblSoDong.Appearance.Options.UseFont = true;
            this.lblSoDong.Appearance.Options.UseForeColor = true;
            this.lblSoDong.Location = new System.Drawing.Point(864, 13);
            this.lblSoDong.Margin = new System.Windows.Forms.Padding(10, 0, 5, 0);
            this.lblSoDong.Name = "lblSoDong";
            this.lblSoDong.Size = new System.Drawing.Size(0, 15);
            this.lblSoDong.TabIndex = 0;
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Horizontal = false;
            this.splitMain.Location = new System.Drawing.Point(0, 45);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.gridLog);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlDetail);
            this.splitMain.Size = new System.Drawing.Size(1200, 655);
            this.splitMain.SplitterPosition = 400;
            this.splitMain.TabIndex = 1;
            // 
            // gridLog
            // 
            this.gridLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLog.Location = new System.Drawing.Point(0, 0);
            this.gridLog.MainView = this.gridViewLog;
            this.gridLog.Name = "gridLog";
            this.gridLog.Size = new System.Drawing.Size(1200, 400);
            this.gridLog.TabIndex = 0;
            this.gridLog.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLog});
            // 
            // gridViewLog
            // 
            this.gridViewLog.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colThoiGian,
            this.colNguoiThucHien,
            this.colThucThe,
            this.colHanhDong,
            this.colIdThucThe,
            this.colGhiChu});
            this.gridViewLog.GridControl = this.gridLog;
            this.gridViewLog.GroupCount = 1;
            this.gridViewLog.GroupFormat = "{1} {2}";
            this.gridViewLog.Name = "gridViewLog";
            this.gridViewLog.OptionsBehavior.Editable = false;
            this.gridViewLog.OptionsBehavior.ReadOnly = true;
            this.gridViewLog.OptionsView.RowAutoHeight = true;
            this.gridViewLog.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colThucThe, DevExpress.Data.ColumnSortOrder.Ascending)});
            this.gridViewLog.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GridViewLog_FocusedRowChanged);
            // 
            // colThoiGian
            // 
            this.colThoiGian.Caption = "Thời gian";
            this.colThoiGian.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.colThoiGian.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colThoiGian.FieldName = "ThoiGian";
            this.colThoiGian.Name = "colThoiGian";
            this.colThoiGian.Visible = true;
            this.colThoiGian.VisibleIndex = 0;
            this.colThoiGian.Width = 140;
            // 
            // colNguoiThucHien
            // 
            this.colNguoiThucHien.Caption = "Người thực hiện";
            this.colNguoiThucHien.FieldName = "TenNguoiThucHien";
            this.colNguoiThucHien.Name = "colNguoiThucHien";
            this.colNguoiThucHien.Visible = true;
            this.colNguoiThucHien.VisibleIndex = 1;
            this.colNguoiThucHien.Width = 150;
            // 
            // colThucThe
            // 
            this.colThucThe.Caption = "Module";
            this.colThucThe.FieldName = "ThucThe";
            this.colThucThe.Name = "colThucThe";
            this.colThucThe.Visible = true;
            this.colThucThe.VisibleIndex = 2;
            this.colThucThe.Width = 120;
            // 
            // colHanhDong
            // 
            this.colHanhDong.Caption = "Hành động";
            this.colHanhDong.FieldName = "HanhDong";
            this.colHanhDong.Name = "colHanhDong";
            this.colHanhDong.Visible = true;
            this.colHanhDong.VisibleIndex = 2;
            this.colHanhDong.Width = 160;
            // 
            // colIdThucThe
            // 
            this.colIdThucThe.Caption = "ID";
            this.colIdThucThe.FieldName = "IdThucThe";
            this.colIdThucThe.Name = "colIdThucThe";
            this.colIdThucThe.Visible = true;
            this.colIdThucThe.VisibleIndex = 3;
            this.colIdThucThe.Width = 60;
            // 
            // colGhiChu
            // 
            this.colGhiChu.Caption = "Ghi chú";
            this.colGhiChu.FieldName = "GhiChu";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.Visible = true;
            this.colGhiChu.VisibleIndex = 4;
            this.colGhiChu.Width = 300;
            // 
            // pnlDetail
            // 
            this.pnlDetail.Controls.Add(this.gridDiff);
            this.pnlDetail.Controls.Add(this.lblDetailTitle);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetail.Location = new System.Drawing.Point(0, 0);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Padding = new System.Windows.Forms.Padding(8, 8, 8, 4);
            this.pnlDetail.Size = new System.Drawing.Size(1200, 245);
            this.pnlDetail.TabIndex = 0;
            // 
            // gridDiff
            // 
            this.gridDiff.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDiff.Location = new System.Drawing.Point(10, 35);
            this.gridDiff.MainView = this.gridViewDiff;
            this.gridDiff.Name = "gridDiff";
            this.gridDiff.Size = new System.Drawing.Size(1180, 204);
            this.gridDiff.TabIndex = 2;
            this.gridDiff.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDiff});
            // 
            // gridViewDiff
            // 
            this.gridViewDiff.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDiffField,
            this.colDiffOld,
            this.colDiffNew});
            this.gridViewDiff.GridControl = this.gridDiff;
            this.gridViewDiff.Name = "gridViewDiff";
            this.gridViewDiff.OptionsBehavior.Editable = false;
            this.gridViewDiff.OptionsBehavior.ReadOnly = true;
            this.gridViewDiff.OptionsView.RowAutoHeight = true;
            this.gridViewDiff.OptionsView.ShowGroupPanel = false;
            this.gridViewDiff.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GridViewDiff_RowCellStyle);
            // 
            // colDiffField
            // 
            this.colDiffField.AppearanceCell.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.colDiffField.AppearanceCell.Options.UseFont = true;
            this.colDiffField.Caption = "Thuộc tính";
            this.colDiffField.FieldName = "Field";
            this.colDiffField.Name = "colDiffField";
            this.colDiffField.Visible = true;
            this.colDiffField.VisibleIndex = 0;
            this.colDiffField.Width = 160;
            // 
            // colDiffOld
            // 
            this.colDiffOld.Caption = "Giá trị cũ";
            this.colDiffOld.FieldName = "OldValue";
            this.colDiffOld.Name = "colDiffOld";
            this.colDiffOld.Visible = true;
            this.colDiffOld.VisibleIndex = 1;
            this.colDiffOld.Width = 480;
            // 
            // colDiffNew
            // 
            this.colDiffNew.Caption = "Giá trị mới";
            this.colDiffNew.FieldName = "NewValue";
            this.colDiffNew.Name = "colDiffNew";
            this.colDiffNew.Visible = true;
            this.colDiffNew.VisibleIndex = 2;
            this.colDiffNew.Width = 480;
            // 
            // lblDetailTitle
            // 
            this.lblDetailTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblDetailTitle.Appearance.Options.UseFont = true;
            this.lblDetailTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblDetailTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblDetailTitle.Location = new System.Drawing.Point(10, 10);
            this.lblDetailTitle.Name = "lblDetailTitle";
            this.lblDetailTitle.Padding = new System.Windows.Forms.Padding(0, 0, 0, 6);
            this.lblDetailTitle.Size = new System.Drawing.Size(1180, 25);
            this.lblDetailTitle.TabIndex = 1;
            this.lblDetailTitle.Text = "Chi tiết thay đổi — chọn một dòng log phía trên";
            // 
            // frmNhatKyThaoTac
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlFilter);
            this.Name = "frmNhatKyThaoTac";
            this.Text = "Nhật ký thao tác";
            ((System.ComponentModel.ISupportInitialize)(this.pnlFilter)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.tblFilter.ResumeLayout(false);
            this.tblFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboModule.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).EndInit();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLog)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlDetail)).EndInit();
            this.pnlDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDiff)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDiff)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlFilter;
        private System.Windows.Forms.TableLayoutPanel tblFilter;
        private DevExpress.XtraEditors.LabelControl lblTuNgay;
        private DevExpress.XtraEditors.DateEdit dtTuNgay;
        private DevExpress.XtraEditors.LabelControl lblDenNgay;
        private DevExpress.XtraEditors.DateEdit dtDenNgay;
        private DevExpress.XtraEditors.LabelControl lblModule;
        private DevExpress.XtraEditors.ComboBoxEdit cboModule;
        private DevExpress.XtraEditors.SimpleButton btnTimKiem;
        private DevExpress.XtraEditors.SimpleButton btnXuatExcel;
        private DevExpress.XtraEditors.LabelControl lblSoDong;
        private DevExpress.XtraEditors.SplitContainerControl splitMain;
        private DevExpress.XtraGrid.GridControl gridLog;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLog;
        private DevExpress.XtraGrid.Columns.GridColumn colThoiGian;
        private DevExpress.XtraGrid.Columns.GridColumn colNguoiThucHien;
        private DevExpress.XtraGrid.Columns.GridColumn colThucThe;
        private DevExpress.XtraGrid.Columns.GridColumn colHanhDong;
        private DevExpress.XtraGrid.Columns.GridColumn colIdThucThe;
        private DevExpress.XtraGrid.Columns.GridColumn colGhiChu;
        private DevExpress.XtraEditors.PanelControl pnlDetail;
        private DevExpress.XtraEditors.LabelControl lblDetailTitle;
        private DevExpress.XtraGrid.GridControl gridDiff;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDiff;
        private DevExpress.XtraGrid.Columns.GridColumn colDiffField;
        private DevExpress.XtraGrid.Columns.GridColumn colDiffOld;
        private DevExpress.XtraGrid.Columns.GridColumn colDiffNew;
    }
}
