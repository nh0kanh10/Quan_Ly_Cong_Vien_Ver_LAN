namespace GUI.Modules.DanhMuc
{
    partial class ucCauHinhChoThue
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblGridTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnlQuetMa = new System.Windows.Forms.Panel();
            this.lblQuetMa = new DevExpress.XtraEditors.LabelControl();
            this.txtMaVach = new DevExpress.XtraEditors.TextEdit();
            this.gcTaiSan = new DevExpress.XtraGrid.GridControl();
            this.gvTaiSan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaVach = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenTaiSan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKhuVuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoSlkKhuVuc = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.colTrangThai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colXoa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoBtnXoa = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.pnlQuetMa.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaVach.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTaiSan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTaiSan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoSlkKhuVuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoBtnXoa)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(137)))), ((int)(((byte)(123)))));
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(12, 10, 0, 6);
            this.lblTitle.Size = new System.Drawing.Size(935, 35);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Dành riêng cho: HÀNG CHO THUÊ (XE ĐẠP, TỦ ĐỒ...)";
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblGridTitle.Appearance.Options.UseFont = true;
            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTitle.Location = new System.Drawing.Point(0, 75);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Padding = new System.Windows.Forms.Padding(12, 6, 0, 4);
            this.lblGridTitle.Size = new System.Drawing.Size(218, 25);
            this.lblGridTitle.TabIndex = 2;
            this.lblGridTitle.Text = "Danh sách tài sản vật lý (Bãi xe, Tủ đồ):";
            // 
            // pnlQuetMa
            // 
            this.pnlQuetMa.Controls.Add(this.lblQuetMa);
            this.pnlQuetMa.Controls.Add(this.txtMaVach);
            this.pnlQuetMa.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlQuetMa.Location = new System.Drawing.Point(0, 35);
            this.pnlQuetMa.Name = "pnlQuetMa";
            this.pnlQuetMa.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.pnlQuetMa.Size = new System.Drawing.Size(935, 40);
            this.pnlQuetMa.TabIndex = 1;
            // 
            // lblQuetMa
            // 
            this.lblQuetMa.Location = new System.Drawing.Point(12, 12);
            this.lblQuetMa.Name = "lblQuetMa";
            this.lblQuetMa.Size = new System.Drawing.Size(118, 13);
            this.lblQuetMa.TabIndex = 0;
            this.lblQuetMa.Text = "Quét mã vạch nhập kho:";
            // 
            // txtMaVach
            // 
            this.txtMaVach.Location = new System.Drawing.Point(140, 9);
            this.txtMaVach.Name = "txtMaVach";
            this.txtMaVach.Size = new System.Drawing.Size(500, 20);
            this.txtMaVach.TabIndex = 1;
            this.txtMaVach.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtMaVach_KeyDown);
            // 
            // gcTaiSan
            // 
            this.gcTaiSan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcTaiSan.Location = new System.Drawing.Point(0, 100);
            this.gcTaiSan.MainView = this.gvTaiSan;
            this.gcTaiSan.Name = "gcTaiSan";
            this.gcTaiSan.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoSlkKhuVuc,
            this.repoBtnXoa});
            this.gcTaiSan.Size = new System.Drawing.Size(935, 439);
            this.gcTaiSan.TabIndex = 3;
            this.gcTaiSan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTaiSan});
            // 
            // gvTaiSan
            // 
            this.gvTaiSan.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaVach,
            this.colTenTaiSan,
            this.colKhuVuc,
            this.colTrangThai,
            this.colXoa});
            this.gvTaiSan.GridControl = this.gcTaiSan;
            this.gvTaiSan.Name = "gvTaiSan";
            this.gvTaiSan.OptionsView.ShowGroupPanel = false;
            // 
            // colMaVach
            // 
            this.colMaVach.Caption = "Mã Vạch";
            this.colMaVach.FieldName = "MaVachThietBi";
            this.colMaVach.Name = "colMaVach";
            this.colMaVach.OptionsColumn.AllowEdit = false;
            this.colMaVach.Visible = true;
            this.colMaVach.VisibleIndex = 0;
            this.colMaVach.Width = 150;
            // 
            // colTenTaiSan
            // 
            this.colTenTaiSan.Caption = "Tên nhận diện (VD: Xe số 1)";
            this.colTenTaiSan.FieldName = "TenTaiSan";
            this.colTenTaiSan.Name = "colTenTaiSan";
            this.colTenTaiSan.Visible = true;
            this.colTenTaiSan.VisibleIndex = 1;
            this.colTenTaiSan.Width = 300;
            // 
            // colKhuVuc
            // 
            this.colKhuVuc.Caption = "Vị trí bãi / Khu vực";
            this.colKhuVuc.ColumnEdit = this.repoSlkKhuVuc;
            this.colKhuVuc.FieldName = "IdKhuVuc";
            this.colKhuVuc.Name = "colKhuVuc";
            this.colKhuVuc.Visible = true;
            this.colKhuVuc.VisibleIndex = 2;
            this.colKhuVuc.Width = 200;
            // 
            // repoSlkKhuVuc
            // 
            this.repoSlkKhuVuc.AutoHeight = false;
            this.repoSlkKhuVuc.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoSlkKhuVuc.Name = "repoSlkKhuVuc";
            this.repoSlkKhuVuc.NullText = "(Chưa xếp bãi)";
            // 
            // colTrangThai
            // 
            this.colTrangThai.Caption = "Trạng Thái";
            this.colTrangThai.FieldName = "TrangThai";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.OptionsColumn.AllowEdit = false;
            this.colTrangThai.Visible = true;
            this.colTrangThai.VisibleIndex = 3;
            // 
            // colXoa
            // 
            this.colXoa.Caption = " ";
            this.colXoa.ColumnEdit = this.repoBtnXoa;
            this.colXoa.FieldName = "_colXoa";
            this.colXoa.Fixed = DevExpress.XtraGrid.Columns.FixedStyle.Right;
            this.colXoa.MaxWidth = 40;
            this.colXoa.Name = "colXoa";
            this.colXoa.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colXoa.OptionsFilter.AllowFilter = false;
            this.colXoa.UnboundType = DevExpress.Data.UnboundColumnType.Object;
            this.colXoa.Visible = true;
            this.colXoa.VisibleIndex = 4;
            this.colXoa.Width = 40;
            // 
            // repoBtnXoa
            // 
            this.repoBtnXoa.AutoHeight = false;
            this.repoBtnXoa.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.repoBtnXoa.Name = "repoBtnXoa";
            this.repoBtnXoa.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repoBtnXoa.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.RepBtnXoa_ButtonClick);
            // 
            // ucCauHinhChoThue
            // 
            this.Controls.Add(this.gcTaiSan);
            this.Controls.Add(this.lblGridTitle);
            this.Controls.Add(this.pnlQuetMa);
            this.Controls.Add(this.lblTitle);
            this.Name = "ucCauHinhChoThue";
            this.Size = new System.Drawing.Size(935, 539);
            this.VisibleChanged += new System.EventHandler(this.ucCauHinhChoThue_VisibleChanged);
            this.pnlQuetMa.ResumeLayout(false);
            this.pnlQuetMa.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaVach.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTaiSan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTaiSan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoSlkKhuVuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoBtnXoa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTitle;
        private System.Windows.Forms.Panel pnlQuetMa;
        private DevExpress.XtraEditors.LabelControl lblQuetMa;
        private DevExpress.XtraEditors.TextEdit txtMaVach;
        private DevExpress.XtraEditors.LabelControl lblGridTitle;
        private DevExpress.XtraGrid.GridControl gcTaiSan;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTaiSan;
        private DevExpress.XtraGrid.Columns.GridColumn colMaVach;
        private DevExpress.XtraGrid.Columns.GridColumn colTenTaiSan;
        private DevExpress.XtraGrid.Columns.GridColumn colKhuVuc;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repoSlkKhuVuc;
        private DevExpress.XtraGrid.Columns.GridColumn colTrangThai;
        private DevExpress.XtraGrid.Columns.GridColumn colXoa;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repoBtnXoa;
    }
}
