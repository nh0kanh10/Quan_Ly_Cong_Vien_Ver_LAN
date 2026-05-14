namespace GUI.Modules.DanhMuc
{
    partial class ucCauHinhVe
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
            this.cboLoaiVe = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblLoaiVe = new DevExpress.XtraEditors.LabelControl();
            this.cboDoiTuong = new DevExpress.XtraEditors.ComboBoxEdit();
            this.lblDoiTuong = new DevExpress.XtraEditors.LabelControl();
            this.chkTaoToken = new DevExpress.XtraEditors.CheckEdit();
            this.gcQuyenCong = new DevExpress.XtraGrid.GridControl();
            this.gvQuyenCong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIdKhuVuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoSlkKV = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.colIdTroChoi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoSlkTC = new DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit();
            this.colSoLuotChoPhep = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoLuot = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            this.colGhiChu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colXoa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repoBtnXoa = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.lblGridTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnlFields = new System.Windows.Forms.Panel();
            this.btnThemCong = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.cboLoaiVe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDoiTuong.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTaoToken.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcQuyenCong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvQuyenCong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoSlkKV)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoSlkTC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoLuot)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoBtnXoa)).BeginInit();
            this.pnlFields.SuspendLayout();
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
            this.lblTitle.Size = new System.Drawing.Size(949, 35);
            this.lblTitle.TabIndex = 3;
            this.lblTitle.Text = "Dành riêng cho: LOẠI VÉ";
            // 
            // cboLoaiVe
            // 
            this.cboLoaiVe.Location = new System.Drawing.Point(140, 8);
            this.cboLoaiVe.Name = "cboLoaiVe";
            this.cboLoaiVe.Size = new System.Drawing.Size(250, 20);
            this.cboLoaiVe.TabIndex = 3;
            // 
            // lblLoaiVe
            // 
            this.lblLoaiVe.Location = new System.Drawing.Point(12, 10);
            this.lblLoaiVe.Name = "lblLoaiVe";
            this.lblLoaiVe.Size = new System.Drawing.Size(62, 13);
            this.lblLoaiVe.TabIndex = 4;
            this.lblLoaiVe.Text = "Phân loại vé:";
            // 
            // cboDoiTuong
            // 
            this.cboDoiTuong.Location = new System.Drawing.Point(140, 40);
            this.cboDoiTuong.Name = "cboDoiTuong";
            this.cboDoiTuong.Size = new System.Drawing.Size(250, 20);
            this.cboDoiTuong.TabIndex = 1;
            // 
            // lblDoiTuong
            // 
            this.lblDoiTuong.Location = new System.Drawing.Point(12, 42);
            this.lblDoiTuong.Name = "lblDoiTuong";
            this.lblDoiTuong.Size = new System.Drawing.Size(67, 13);
            this.lblDoiTuong.TabIndex = 2;
            this.lblDoiTuong.Text = "Đối tượng vé:";
            // 
            // chkTaoToken
            // 
            this.chkTaoToken.Location = new System.Drawing.Point(12, 74);
            this.chkTaoToken.Name = "chkTaoToken";
            this.chkTaoToken.Properties.Caption = "Cần tạo token (vé điện tử / mã QR)";
            this.chkTaoToken.Size = new System.Drawing.Size(274, 20);
            this.chkTaoToken.TabIndex = 0;
            // 
            // gcQuyenCong
            // 
            this.gcQuyenCong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcQuyenCong.Location = new System.Drawing.Point(0, 170);
            this.gcQuyenCong.MainView = this.gvQuyenCong;
            this.gcQuyenCong.Name = "gcQuyenCong";
            this.gcQuyenCong.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repoSlkKV,
            this.repoSlkTC,
            this.repoLuot,
            this.repoBtnXoa});
            this.gcQuyenCong.Size = new System.Drawing.Size(949, 327);
            this.gcQuyenCong.TabIndex = 0;
            this.gcQuyenCong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvQuyenCong});
            // 
            // gvQuyenCong
            // 
            this.gvQuyenCong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIdKhuVuc,
            this.colIdTroChoi,
            this.colSoLuotChoPhep,
            this.colGhiChu,
            this.colXoa});
            this.gvQuyenCong.GridControl = this.gcQuyenCong;
            this.gvQuyenCong.Name = "gvQuyenCong";
            this.gvQuyenCong.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.Gv_RowCellStyle);
            this.gvQuyenCong.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.Gv_CellValueChanged);
            this.gvQuyenCong.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.Gv_InvalidRowException);
            this.gvQuyenCong.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.Gv_ValidateRow);
            // 
            // colIdKhuVuc
            // 
            this.colIdKhuVuc.Caption = "Khu vực";
            this.colIdKhuVuc.ColumnEdit = this.repoSlkKV;
            this.colIdKhuVuc.FieldName = "IdKhuVuc";
            this.colIdKhuVuc.Name = "colIdKhuVuc";
            this.colIdKhuVuc.Visible = true;
            this.colIdKhuVuc.VisibleIndex = 0;
            // 
            // repoSlkKV
            // 
            this.repoSlkKV.AutoHeight = false;
            this.repoSlkKV.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoSlkKV.Name = "repoSlkKV";
            this.repoSlkKV.NullText = "(Chọn khu vực)";
            // 
            // colIdTroChoi
            // 
            this.colIdTroChoi.Caption = "Cổng / Trò chơi";
            this.colIdTroChoi.ColumnEdit = this.repoSlkTC;
            this.colIdTroChoi.FieldName = "IdTroChoi";
            this.colIdTroChoi.Name = "colIdTroChoi";
            this.colIdTroChoi.Visible = true;
            this.colIdTroChoi.VisibleIndex = 1;
            // 
            // repoSlkTC
            // 
            this.repoSlkTC.AutoHeight = false;
            this.repoSlkTC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoSlkTC.Name = "repoSlkTC";
            this.repoSlkTC.NullText = "(Chọn trò chơi)";
            // 
            // colSoLuotChoPhep
            // 
            this.colSoLuotChoPhep.Caption = "Số lượt cho phép";
            this.colSoLuotChoPhep.ColumnEdit = this.repoLuot;
            this.colSoLuotChoPhep.FieldName = "SoLuotChoPhep";
            this.colSoLuotChoPhep.Name = "colSoLuotChoPhep";
            this.colSoLuotChoPhep.Visible = true;
            this.colSoLuotChoPhep.VisibleIndex = 2;
            // 
            // repoLuot
            // 
            this.repoLuot.AutoHeight = false;
            this.repoLuot.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repoLuot.IsFloatValue = false;
            this.repoLuot.MaskSettings.Set("mask", "N00");
            this.repoLuot.MaxValue = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.repoLuot.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.repoLuot.Name = "repoLuot";
            // 
            // colGhiChu
            // 
            this.colGhiChu.Caption = "Ghi chú";
            this.colGhiChu.FieldName = "GhiChu";
            this.colGhiChu.Name = "colGhiChu";
            this.colGhiChu.Visible = true;
            this.colGhiChu.VisibleIndex = 3;
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
            this.repoBtnXoa.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.RepoBtn_ButtonClick);
            // 
            // lblGridTitle
            // 
            this.lblGridTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblGridTitle.Appearance.Options.UseFont = true;
            this.lblGridTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblGridTitle.Location = new System.Drawing.Point(0, 145);
            this.lblGridTitle.Name = "lblGridTitle";
            this.lblGridTitle.Padding = new System.Windows.Forms.Padding(12, 6, 0, 4);
            this.lblGridTitle.Size = new System.Drawing.Size(263, 25);
            this.lblGridTitle.TabIndex = 1;
            this.lblGridTitle.Text = "Danh sách cổng quẹt / khu vực được phép vào:";
            // 
            // pnlFields
            // 
            this.pnlFields.Controls.Add(this.chkTaoToken);
            this.pnlFields.Controls.Add(this.cboDoiTuong);
            this.pnlFields.Controls.Add(this.lblDoiTuong);
            this.pnlFields.Controls.Add(this.cboLoaiVe);
            this.pnlFields.Controls.Add(this.lblLoaiVe);
            this.pnlFields.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFields.Location = new System.Drawing.Point(0, 35);
            this.pnlFields.Name = "pnlFields";
            this.pnlFields.Padding = new System.Windows.Forms.Padding(12, 4, 12, 4);
            this.pnlFields.Size = new System.Drawing.Size(949, 110);
            this.pnlFields.TabIndex = 2;
            // 
            // btnThemCong
            // 
            this.btnThemCong.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnThemCong.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnThemCong.Appearance.Options.UseBackColor = true;
            this.btnThemCong.Appearance.Options.UseForeColor = true;
            this.btnThemCong.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnThemCong.Location = new System.Drawing.Point(0, 497);
            this.btnThemCong.Name = "btnThemCong";
            this.btnThemCong.Size = new System.Drawing.Size(949, 28);
            this.btnThemCong.TabIndex = 4;
            this.btnThemCong.Text = "Thêm cổng";
            this.btnThemCong.Click += new System.EventHandler(this.BtnThemDong_Click);
            // 
            // ucCauHinhVe
            // 
            this.Controls.Add(this.gcQuyenCong);
            this.Controls.Add(this.lblGridTitle);
            this.Controls.Add(this.pnlFields);
            this.Controls.Add(this.btnThemCong);
            this.Controls.Add(this.lblTitle);
            this.Name = "ucCauHinhVe";
            this.Size = new System.Drawing.Size(949, 525);
            ((System.ComponentModel.ISupportInitialize)(this.cboLoaiVe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboDoiTuong.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTaoToken.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcQuyenCong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvQuyenCong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoSlkKV)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoSlkTC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoLuot)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repoBtnXoa)).EndInit();
            this.pnlFields.ResumeLayout(false);
            this.pnlFields.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.ComboBoxEdit cboLoaiVe;
        private DevExpress.XtraEditors.LabelControl lblLoaiVe;
        private DevExpress.XtraEditors.ComboBoxEdit cboDoiTuong;
        private DevExpress.XtraEditors.LabelControl lblDoiTuong;
        private DevExpress.XtraEditors.CheckEdit chkTaoToken;
        private DevExpress.XtraGrid.GridControl gcQuyenCong;
        private DevExpress.XtraGrid.Views.Grid.GridView gvQuyenCong;
        private DevExpress.XtraEditors.LabelControl lblGridTitle;
        private System.Windows.Forms.Panel pnlFields;
        private DevExpress.XtraGrid.Columns.GridColumn colIdKhuVuc;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repoSlkKV;
        private DevExpress.XtraGrid.Columns.GridColumn colIdTroChoi;
        private DevExpress.XtraEditors.Repository.RepositoryItemSearchLookUpEdit repoSlkTC;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuotChoPhep;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repoLuot;
        private DevExpress.XtraGrid.Columns.GridColumn colGhiChu;
        private DevExpress.XtraGrid.Columns.GridColumn colXoa;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repoBtnXoa;
        private DevExpress.XtraEditors.SimpleButton btnThemCong;
        private System.ComponentModel.IContainer components = null;





    }}