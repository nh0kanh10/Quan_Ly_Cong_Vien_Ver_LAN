namespace GUI.Modules.DanhMuc
{
    partial class ucCauHinhFnB
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
            this.chkDiUng = new DevExpress.XtraEditors.CheckEdit();
            this.cboPhanLoai = new DevExpress.XtraEditors.ImageComboBoxEdit();
            this.lblPhanLoai = new DevExpress.XtraEditors.LabelControl();
            this.slkNhaHang = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.lblNhaHang = new DevExpress.XtraEditors.LabelControl();
            this.lblGridTitle = new DevExpress.XtraEditors.LabelControl();
            this.gcBOM = new DevExpress.XtraGrid.GridControl();
            this.gvBOM = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIdNguyenLieu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoSlkNL = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.colTenDonVi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoSL = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colXoa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoBtnXoa = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.btnThemDong = new DevExpress.XtraEditors.SimpleButton();
            this.pnlFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkDiUng.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPhanLoai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkNhaHang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBOM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBOM)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoSlkNL)).BeginInit();
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
            this.lblTitle.Text = "Dành riêng cho: BẾP & TUYẾN PHA CHẾ";
            // 
            // pnlFields
            // 
            this.pnlFields.Controls.Add(this.chkDiUng);
            this.pnlFields.Controls.Add(this.cboPhanLoai);
            this.pnlFields.Controls.Add(this.lblPhanLoai);
            this.pnlFields.Controls.Add(this.slkNhaHang);
            this.pnlFields.Controls.Add(this.lblNhaHang);
            this.pnlFields.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFields.Location = new System.Drawing.Point(0, 35);
            this.pnlFields.Name = "pnlFields";
            this.pnlFields.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.pnlFields.Size = new System.Drawing.Size(935, 110);
            this.pnlFields.TabIndex = 2;
            // 
            // chkDiUng
            // 
            this.chkDiUng.Location = new System.Drawing.Point(12, 74);
            this.chkDiUng.Name = "chkDiUng";
            this.chkDiUng.Properties.Caption = "Có cảnh báo dị ứng (đậu phộng, hải sản, gluten...)";
            this.chkDiUng.Size = new System.Drawing.Size(366, 20);
            this.chkDiUng.TabIndex = 0;
            // 
            // cboPhanLoai
            // 
            this.cboPhanLoai.Location = new System.Drawing.Point(200, 40);
            this.cboPhanLoai.Name = "cboPhanLoai";
            this.cboPhanLoai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPhanLoai.Size = new System.Drawing.Size(350, 20);
            this.cboPhanLoai.TabIndex = 1;
            // 
            // lblPhanLoai
            // 
            this.lblPhanLoai.Location = new System.Drawing.Point(12, 42);
            this.lblPhanLoai.Name = "lblPhanLoai";
            this.lblPhanLoai.Size = new System.Drawing.Size(70, 13);
            this.lblPhanLoai.TabIndex = 2;
            this.lblPhanLoai.Text = "Phân loại món:";
            // 
            // slkNhaHang
            // 
            this.slkNhaHang.Location = new System.Drawing.Point(200, 8);
            this.slkNhaHang.Name = "slkNhaHang";
            this.slkNhaHang.Size = new System.Drawing.Size(350, 20);
            this.slkNhaHang.TabIndex = 3;
            // 
            // lblNhaHang
            // 
            this.lblNhaHang.Location = new System.Drawing.Point(12, 10);
            this.lblNhaHang.Name = "lblNhaHang";
            this.lblNhaHang.Size = new System.Drawing.Size(81, 13);
            this.lblNhaHang.TabIndex = 4;
            this.lblNhaHang.Text = "Gia công tại bếp:";
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblGridTitle.Appearance.Options.UseFont = true;
            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTitle.Location = new System.Drawing.Point(0, 145);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Padding = new System.Windows.Forms.Padding(12, 6, 0, 4);
            this.lblGridTitle.Size = new System.Drawing.Size(171, 25);
            this.lblGridTitle.TabIndex = 1;
            this.lblGridTitle.Text = "Định mức nguyên liệu (BOM):";
            // 
            // gcBOM
            // 
            this.gcBOM.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcBOM.Location = new System.Drawing.Point(0, 170);
            this.gcBOM.MainView = this.gvBOM;
            this.gcBOM.Name = "gcBOM";
            this.gcBOM.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoSlkNL,
            this.repoSL,
            this.repoBtnXoa});
            this.gcBOM.Size = new System.Drawing.Size(935, 341);
            this.gcBOM.TabIndex = 0;
            this.gcBOM.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBOM});
            // 
            // gvBOM
            // 
            this.gvBOM.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIdNguyenLieu,
            this.colTenDonVi,
            this.colSoLuong,
            this.colXoa});
            this.gvBOM.GridControl = this.gcBOM;
            this.gvBOM.Name = "gvBOM";
            this.gvBOM.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvBOM_RowCellStyle);
            this.gvBOM.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvBOM_CellValueChanged);
            this.gvBOM.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvBOM_InvalidRowException);
            this.gvBOM.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvBOM_ValidateRow);
            // 
            // colIdNguyenLieu
            // 
            this.colIdNguyenLieu.Caption = "Nguyên liệu";
            this.colIdNguyenLieu.ColumnEdit = this.repoSlkNL;
            this.colIdNguyenLieu.FieldName = "IdNguyenLieu";
            this.colIdNguyenLieu.Name = "colIdNguyenLieu";
            this.colIdNguyenLieu.Visible = true;
            this.colIdNguyenLieu.VisibleIndex = 0;
            // 
            // repoSlkNL
            // 
            this.repoSlkNL.AutoHeight = false;
            this.repoSlkNL.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoSlkNL.Name = "repoSlkNL";
            this.repoSlkNL.NullText = "(Chọn nguyên liệu)";
            // 
            // colTenDonVi
            // 
            this.colTenDonVi.AppearanceCell.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.colTenDonVi.AppearanceCell.Options.UseBackColor = true;
            this.colTenDonVi.Caption = "ĐVT Gốc";
            this.colTenDonVi.FieldName = "TenDonVi";
            this.colTenDonVi.Name = "colTenDonVi";
            this.colTenDonVi.OptionsColumn.AllowEdit = false;
            this.colTenDonVi.Visible = true;
            this.colTenDonVi.VisibleIndex = 1;
            // 
            // colSoLuong
            // 
            this.colSoLuong.Caption = "Số lượng";
            this.colSoLuong.ColumnEdit = this.repoSL;
            this.colSoLuong.FieldName = "SoLuong";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Visible = true;
            this.colSoLuong.VisibleIndex = 2;
            // 
            // repoSL
            // 
            this.repoSL.AutoHeight = false;
            this.repoSL.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoSL.DisplayFormat.FormatString = "N3";
            this.repoSL.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.repoSL.EditFormat.FormatString = "N3";
            this.repoSL.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
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
            this.colXoa.UnboundDataType = typeof(object);
            this.colXoa.Visible = true;
            this.colXoa.VisibleIndex = 3;
            this.colXoa.Width = 40;
            // 
            // repoBtnXoa
            // 
            this.repoBtnXoa.AutoHeight = false;
            this.repoBtnXoa.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.repoBtnXoa.Name = "repoBtnXoa";
            this.repoBtnXoa.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repoBtnXoa.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.RepBtn_ButtonClick);
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
            this.btnThemDong.TabIndex = 4;
            this.btnThemDong.Text = "Thêm dòng";
            this.btnThemDong.Click += new System.EventHandler(this.BtnThemNguyenLieu_Click);
            // 
            // ucCauHinhFnB
            // 
            this.Controls.Add(this.gcBOM);
            this.Controls.Add(this.lblGridTitle);
            this.Controls.Add(this.pnlFields);
            this.Controls.Add(this.btnThemDong);
            this.Controls.Add(this.lblTitle);
            this.Name = "ucCauHinhFnB";
            this.Size = new System.Drawing.Size(935, 539);
            this.pnlFields.ResumeLayout(false);
            this.pnlFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkDiUng.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPhanLoai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkNhaHang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcBOM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBOM)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoSlkNL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoSL)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoBtnXoa)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private System.Windows.Forms.Panel pnlFields;
        private DevExpress.XtraEditors.CheckEdit chkDiUng;
        private DevExpress.XtraEditors.ImageComboBoxEdit cboPhanLoai;
        private DevExpress.XtraEditors.LabelControl lblPhanLoai;
        private DevExpress.XtraEditors.SearchLookUpEdit slkNhaHang;
        private DevExpress.XtraEditors.LabelControl lblNhaHang;
        private DevExpress.XtraEditors.LabelControl lblGridTitle;
        private DevExpress.XtraGrid.GridControl gcBOM;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBOM;
        private DevExpress.XtraGrid.Columns.GridColumn colIdNguyenLieu;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repoSlkNL;
        private DevExpress.XtraGrid.Columns.GridColumn colTenDonVi;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuong;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repoSL;
        private DevExpress.XtraGrid.Columns.GridColumn colXoa;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repoBtnXoa;
        private DevExpress.XtraEditors.SimpleButton btnThemDong;
        private System.ComponentModel.IContainer components = null;





    }}