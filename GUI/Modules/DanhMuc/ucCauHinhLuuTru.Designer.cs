namespace GUI.Modules.DanhMuc
{
    partial class ucCauHinhLuuTru
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnlFields = new System.Windows.Forms.Panel();
            this.lblNguoiLon = new DevExpress.XtraEditors.LabelControl();
            this.spinNguoiLon = new DevExpress.XtraEditors.SpinEdit();
            this.lblTreEm = new DevExpress.XtraEditors.LabelControl();
            this.spinTreEm = new DevExpress.XtraEditors.SpinEdit();
            this.lblDienTich = new DevExpress.XtraEditors.LabelControl();
            this.txtDienTich = new DevExpress.XtraEditors.TextEdit();
            this.lblTienNghi = new DevExpress.XtraEditors.LabelControl();
            this.txtTienNghi = new DevExpress.XtraEditors.MemoEdit();
            this.lblGridTitle = new DevExpress.XtraEditors.LabelControl();
            this.gcVatTu = new DevExpress.XtraGrid.GridControl();
            this.gvVatTu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIdSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoSlkSanPham = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.colSoLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoSL = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colXoa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoBtnXoa = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnThemDong = new DevExpress.XtraEditors.SimpleButton();
            
            this.pnlFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinNguoiLon.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTreEm.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienTich.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTienNghi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcVatTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVatTu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoSlkSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoSL)).BeginInit();
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
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Dành riêng cho: LOẠI PHÒNG LƯU TRÚ";
            
            // 
            // pnlFields
            // 
            this.pnlFields.Controls.Add(this.lblNguoiLon);
            this.pnlFields.Controls.Add(this.spinNguoiLon);
            this.pnlFields.Controls.Add(this.lblTreEm);
            this.pnlFields.Controls.Add(this.spinTreEm);
            this.pnlFields.Controls.Add(this.lblDienTich);
            this.pnlFields.Controls.Add(this.txtDienTich);
            this.pnlFields.Controls.Add(this.lblTienNghi);
            this.pnlFields.Controls.Add(this.txtTienNghi);
            this.pnlFields.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFields.Location = new System.Drawing.Point(0, 35);
            this.pnlFields.Name = "pnlFields";
            this.pnlFields.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.pnlFields.Size = new System.Drawing.Size(935, 140);
            this.pnlFields.TabIndex = 2;
            
            // 
            // lblNguoiLon
            // 
            this.lblNguoiLon.Location = new System.Drawing.Point(12, 14);
            this.lblNguoiLon.Name = "lblNguoiLon";
            this.lblNguoiLon.Size = new System.Drawing.Size(110, 13);
            this.lblNguoiLon.TabIndex = 0;
            this.lblNguoiLon.Text = "Sức chứa (Người lớn):";
            
            // 
            // spinNguoiLon
            // 
            this.spinNguoiLon.EditValue = new decimal(new int[] { 2, 0, 0, 0 });
            this.spinNguoiLon.Location = new System.Drawing.Point(130, 11);
            this.spinNguoiLon.Name = "spinNguoiLon";
            this.spinNguoiLon.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinNguoiLon.Properties.IsFloatValue = false;
            this.spinNguoiLon.Size = new System.Drawing.Size(80, 20);
            this.spinNguoiLon.TabIndex = 1;
            
            // 
            // lblTreEm
            // 
            this.lblTreEm.Location = new System.Drawing.Point(240, 14);
            this.lblTreEm.Name = "lblTreEm";
            this.lblTreEm.Size = new System.Drawing.Size(80, 13);
            this.lblTreEm.TabIndex = 2;
            this.lblTreEm.Text = "Trẻ em tối đa:";
            
            // 
            // spinTreEm
            // 
            this.spinTreEm.EditValue = new decimal(new int[] { 1, 0, 0, 0 });
            this.spinTreEm.Location = new System.Drawing.Point(330, 11);
            this.spinTreEm.Name = "spinTreEm";
            this.spinTreEm.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinTreEm.Properties.IsFloatValue = false;
            this.spinTreEm.Size = new System.Drawing.Size(80, 20);
            this.spinTreEm.TabIndex = 3;
            
            // 
            // lblDienTich
            // 
            this.lblDienTich.Location = new System.Drawing.Point(440, 14);
            this.lblDienTich.Name = "lblDienTich";
            this.lblDienTich.Size = new System.Drawing.Size(80, 13);
            this.lblDienTich.TabIndex = 4;
            this.lblDienTich.Text = "Diện tích (m2):";
            
            // 
            // txtDienTich
            // 
            this.txtDienTich.Location = new System.Drawing.Point(530, 11);
            this.txtDienTich.Name = "txtDienTich";
            this.txtDienTich.Size = new System.Drawing.Size(110, 20);
            this.txtDienTich.TabIndex = 5;
            
            // 
            // lblTienNghi
            // 
            this.lblTienNghi.Location = new System.Drawing.Point(12, 45);
            this.lblTienNghi.Name = "lblTienNghi";
            this.lblTienNghi.Size = new System.Drawing.Size(80, 13);
            this.lblTienNghi.TabIndex = 6;
            this.lblTienNghi.Text = "Mô tả tiện nghi:";
            
            // 
            // txtTienNghi
            // 
            this.txtTienNghi.Location = new System.Drawing.Point(130, 43);
            this.txtTienNghi.Name = "txtTienNghi";
            this.txtTienNghi.Size = new System.Drawing.Size(510, 80);
            this.txtTienNghi.TabIndex = 7;
            
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblGridTitle.Appearance.Options.UseFont = true;
            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTitle.Location = new System.Drawing.Point(0, 175);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Padding = new System.Windows.Forms.Padding(12, 6, 0, 4);
            this.lblGridTitle.Size = new System.Drawing.Size(250, 25);
            this.lblGridTitle.TabIndex = 8;
            this.lblGridTitle.Text = "Vật tư phòng mặc định (Setup ban đầu):";
            
            // 
            // gcVatTu
            // 
            this.gcVatTu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcVatTu.Location = new System.Drawing.Point(0, 200);
            this.gcVatTu.MainView = this.gvVatTu;
            this.gcVatTu.Name = "gcVatTu";
            this.gcVatTu.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoSlkSanPham,
            this.repoSL,
            this.repoBtnXoa});
            this.gcVatTu.Size = new System.Drawing.Size(935, 311);
            this.gcVatTu.TabIndex = 9;
            this.gcVatTu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvVatTu});
            
            // 
            // gvVatTu
            // 
            this.gvVatTu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIdSanPham,
            this.colSoLuong,
            this.colXoa});
            this.gvVatTu.GridControl = this.gcVatTu;
            this.gvVatTu.Name = "gvVatTu";
            
            // 
            // colIdSanPham
            // 
            this.colIdSanPham.Caption = "Vật tư (Khăn, Nước...)";
            this.colIdSanPham.ColumnEdit = this.repoSlkSanPham;
            this.colIdSanPham.FieldName = "IdSanPham";
            this.colIdSanPham.Name = "colIdSanPham";
            this.colIdSanPham.Visible = true;
            this.colIdSanPham.VisibleIndex = 0;
            
            // 
            // repoSlkSanPham
            // 
            this.repoSlkSanPham.AutoHeight = false;
            this.repoSlkSanPham.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoSlkSanPham.Name = "repoSlkSanPham";
            this.repoSlkSanPham.NullText = "(Chọn vật tư tiêu hao)";
            
            // 
            // colSoLuong
            // 
            this.colSoLuong.Caption = "Số lượng Setup";
            this.colSoLuong.ColumnEdit = this.repoSL;
            this.colSoLuong.FieldName = "SoLuong";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Visible = true;
            this.colSoLuong.VisibleIndex = 1;
            
            // 
            // repoSL
            // 
            this.repoSL.AutoHeight = false;
            this.repoSL.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoSL.DisplayFormat.FormatString = "N0";
            this.repoSL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repoSL.EditFormat.FormatString = "N0";
            this.repoSL.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repoSL.IsFloatValue = false;
            this.repoSL.Name = "repoSL";
            
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
            this.colXoa.VisibleIndex = 2;
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
            // btnThemDong
            // 
            this.btnThemDong.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnThemDong.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnThemDong.Appearance.Options.UseBackColor = true;
            this.btnThemDong.Appearance.Options.UseForeColor = true;
            this.btnThemDong.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnThemDong.Location = new System.Drawing.Point(0, 511);
            this.btnThemDong.Name = "btnThemDong";
            this.btnThemDong.Size = new System.Drawing.Size(935, 28);
            this.btnThemDong.TabIndex = 10;
            this.btnThemDong.Text = "Thêm Vật tư";
            this.btnThemDong.Click += new System.EventHandler(this.BtnThemVatTu_Click);
            
            // 
            // ucCauHinhLuuTru
            // 
            this.Controls.Add(this.gcVatTu);
            this.Controls.Add(this.lblGridTitle);
            this.Controls.Add(this.pnlFields);
            this.Controls.Add(this.btnThemDong);
            this.Controls.Add(this.lblTitle);
            this.Name = "ucCauHinhLuuTru";
            this.Size = new System.Drawing.Size(935, 539);
            this.pnlFields.ResumeLayout(false);
            this.pnlFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spinNguoiLon.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinTreEm.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienTich.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTienNghi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcVatTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVatTu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoSlkSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoSL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoBtnXoa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblTitle;
        private System.Windows.Forms.Panel pnlFields;
        private DevExpress.XtraEditors.LabelControl lblNguoiLon;
        private DevExpress.XtraEditors.SpinEdit spinNguoiLon;
        private DevExpress.XtraEditors.LabelControl lblTreEm;
        private DevExpress.XtraEditors.SpinEdit spinTreEm;
        private DevExpress.XtraEditors.LabelControl lblDienTich;
        private DevExpress.XtraEditors.TextEdit txtDienTich;
        private DevExpress.XtraEditors.LabelControl lblTienNghi;
        private DevExpress.XtraEditors.MemoEdit txtTienNghi;
        private DevExpress.XtraEditors.LabelControl lblGridTitle;
        private DevExpress.XtraGrid.GridControl gcVatTu;
        private DevExpress.XtraGrid.Views.Grid.GridView gvVatTu;
        private DevExpress.XtraGrid.Columns.GridColumn colIdSanPham;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repoSlkSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuong;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repoSL;
        private DevExpress.XtraGrid.Columns.GridColumn colXoa;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repoBtnXoa;
        private DevExpress.XtraEditors.SimpleButton btnThemDong;
    }
}
