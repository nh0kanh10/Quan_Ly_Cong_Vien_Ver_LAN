namespace GUI
{
    partial class frmTaoPhieuKho
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblMode = new System.Windows.Forms.Label();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.pnlInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.lbl_cboKho = new System.Windows.Forms.Label();
            this.cboKho = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.cboKhoView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lbl_cboNhaCungCap = new System.Windows.Forms.Label();
            this.cboNhaCungCap = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.cboNhaCungCapView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lbl_txtSoChungTu = new System.Windows.Forms.Label();
            this.txtSoChungTu = new DevExpress.XtraEditors.TextEdit();
            this.lbl_dtNgay = new System.Windows.Forms.Label();
            this.dtNgay = new DevExpress.XtraEditors.DateEdit();
            this.lbl_txtLyDo = new System.Windows.Forms.Label();
            this.txtLyDo = new DevExpress.XtraEditors.TextEdit();
            this.gridChiTiet = new DevExpress.XtraGrid.GridControl();
            this.gridViewChiTiet = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlFooter = new Guna.UI2.WinForms.Guna2Panel();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuuPhieu = new Guna.UI2.WinForms.Guna2Button();
            this.lblTongTienValue = new System.Windows.Forms.Label();
            this.lblTongTien = new System.Windows.Forms.Label();
            this.btnThemDong = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaDong = new Guna.UI2.WinForms.Guna2Button();
            this.txtScanner = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnToggleCamera = new Guna.UI2.WinForms.Guna2Button();
            this.picCamera = new System.Windows.Forms.PictureBox();
            this.pnlHeader.SuspendLayout();
            this.pnlInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboKhoView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNhaCungCap.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNhaCungCapView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoChungTu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLyDo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridChiTiet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChiTiet)).BeginInit();
            this.pnlFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblMode);
            this.pnlHeader.Controls.Add(this.lblFormTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(900, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblMode
            // 
            this.lblMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMode.AutoSize = true;
            this.lblMode.BackColor = System.Drawing.Color.Transparent;
            this.lblMode.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.lblMode.Location = new System.Drawing.Point(750, 20);
            this.lblMode.Name = "lblMode";
            this.lblMode.Size = new System.Drawing.Size(85, 19);
            this.lblMode.TabIndex = 1;
            this.lblMode.Text = "NHẬP KHO";
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(20, 15);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(218, 25);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "TẠO PHIẾU NHẬP KHO";
            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.lbl_cboKho);
            this.pnlInfo.Controls.Add(this.cboKho);
            this.pnlInfo.Controls.Add(this.lbl_cboNhaCungCap);
            this.pnlInfo.Controls.Add(this.cboNhaCungCap);
            this.pnlInfo.Controls.Add(this.lbl_txtSoChungTu);
            this.pnlInfo.Controls.Add(this.txtSoChungTu);
            this.pnlInfo.Controls.Add(this.lbl_dtNgay);
            this.pnlInfo.Controls.Add(this.dtNgay);
            this.pnlInfo.Controls.Add(this.lbl_txtLyDo);
            this.pnlInfo.Controls.Add(this.txtLyDo);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInfo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlInfo.Location = new System.Drawing.Point(0, 60);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Padding = new System.Windows.Forms.Padding(15, 10, 15, 5);
            this.pnlInfo.Size = new System.Drawing.Size(900, 186);
            this.pnlInfo.TabIndex = 1;
            // 
            // lbl_cboKho
            // 
            this.lbl_cboKho.AutoSize = true;
            this.lbl_cboKho.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_cboKho.Location = new System.Drawing.Point(20, 15);
            this.lbl_cboKho.Name = "lbl_cboKho";
            this.lbl_cboKho.Size = new System.Drawing.Size(32, 15);
            this.lbl_cboKho.TabIndex = 0;
            this.lbl_cboKho.Text = "Kho:";
            // 
            // cboKho
            // 
            this.cboKho.Location = new System.Drawing.Point(120, 12);
            this.cboKho.Name = "cboKho";
            this.cboKho.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboKho.Properties.NullText = "--- Chọn kho nguồn ---";
            this.cboKho.Properties.PopupView = this.cboKhoView;
            this.cboKho.Size = new System.Drawing.Size(250, 20);
            this.cboKho.TabIndex = 1;
            // 
            // cboKhoView
            // 
            this.cboKhoView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.cboKhoView.Name = "cboKhoView";
            this.cboKhoView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.cboKhoView.OptionsView.ShowGroupPanel = false;
            // 
            // lbl_cboNhaCungCap
            // 
            this.lbl_cboNhaCungCap.AutoSize = true;
            this.lbl_cboNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_cboNhaCungCap.Location = new System.Drawing.Point(400, 15);
            this.lbl_cboNhaCungCap.Name = "lbl_cboNhaCungCap";
            this.lbl_cboNhaCungCap.Size = new System.Drawing.Size(84, 15);
            this.lbl_cboNhaCungCap.TabIndex = 2;
            this.lbl_cboNhaCungCap.Text = "Nhà cung cấp:";
            // 
            // cboNhaCungCap
            // 
            this.cboNhaCungCap.Location = new System.Drawing.Point(520, 12);
            this.cboNhaCungCap.Name = "cboNhaCungCap";
            this.cboNhaCungCap.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboNhaCungCap.Properties.NullText = "--- Chọn nhà cung cấp ---";
            this.cboNhaCungCap.Properties.PopupView = this.cboNhaCungCapView;
            this.cboNhaCungCap.Size = new System.Drawing.Size(350, 20);
            this.cboNhaCungCap.TabIndex = 3;
            // 
            // cboNhaCungCapView
            // 
            this.cboNhaCungCapView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.cboNhaCungCapView.Name = "cboNhaCungCapView";
            this.cboNhaCungCapView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.cboNhaCungCapView.OptionsView.ShowGroupPanel = false;
            // 
            // lbl_txtSoChungTu
            // 
            this.lbl_txtSoChungTu.AutoSize = true;
            this.lbl_txtSoChungTu.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_txtSoChungTu.Location = new System.Drawing.Point(20, 56);
            this.lbl_txtSoChungTu.Name = "lbl_txtSoChungTu";
            this.lbl_txtSoChungTu.Size = new System.Drawing.Size(78, 15);
            this.lbl_txtSoChungTu.TabIndex = 4;
            this.lbl_txtSoChungTu.Text = "Số chứng từ:";
            // 
            // txtSoChungTu
            // 
            this.txtSoChungTu.Location = new System.Drawing.Point(120, 53);
            this.txtSoChungTu.Name = "txtSoChungTu";
            this.txtSoChungTu.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.txtSoChungTu.Properties.Appearance.Options.UseBackColor = true;
            this.txtSoChungTu.Properties.ReadOnly = true;
            this.txtSoChungTu.Size = new System.Drawing.Size(250, 20);
            this.txtSoChungTu.TabIndex = 5;
            // 
            // lbl_dtNgay
            // 
            this.lbl_dtNgay.AutoSize = true;
            this.lbl_dtNgay.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_dtNgay.Location = new System.Drawing.Point(400, 56);
            this.lbl_dtNgay.Name = "lbl_dtNgay";
            this.lbl_dtNgay.Size = new System.Drawing.Size(38, 15);
            this.lbl_dtNgay.TabIndex = 6;
            this.lbl_dtNgay.Text = "Ngày:";
            // 
            // dtNgay
            // 
            this.dtNgay.EditValue = null;
            this.dtNgay.Location = new System.Drawing.Point(520, 53);
            this.dtNgay.Name = "dtNgay";
            this.dtNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgay.Size = new System.Drawing.Size(350, 20);
            this.dtNgay.TabIndex = 7;
            // 
            // lbl_txtLyDo
            // 
            this.lbl_txtLyDo.AutoSize = true;
            this.lbl_txtLyDo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_txtLyDo.Location = new System.Drawing.Point(20, 97);
            this.lbl_txtLyDo.Name = "lbl_txtLyDo";
            this.lbl_txtLyDo.Size = new System.Drawing.Size(86, 15);
            this.lbl_txtLyDo.TabIndex = 8;
            this.lbl_txtLyDo.Text = "Lý do/Ghi chú:";
            // 
            // txtLyDo
            // 
            this.txtLyDo.Location = new System.Drawing.Point(120, 94);
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.Size = new System.Drawing.Size(750, 20);
            this.txtLyDo.TabIndex = 9;
            // 
            // gridChiTiet
            // 
            this.gridChiTiet.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridChiTiet.Location = new System.Drawing.Point(0, 246);
            this.gridChiTiet.MainView = this.gridViewChiTiet;
            this.gridChiTiet.Name = "gridChiTiet";
            this.gridChiTiet.Size = new System.Drawing.Size(900, 309);
            this.gridChiTiet.TabIndex = 12;
            this.gridChiTiet.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewChiTiet});
            // 
            // gridViewChiTiet
            // 
            this.gridViewChiTiet.GridControl = this.gridChiTiet;
            this.gridViewChiTiet.Name = "gridViewChiTiet";
            this.gridViewChiTiet.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gridViewChiTiet.OptionsView.ShowGroupPanel = false;
            this.gridViewChiTiet.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GridViewChiTiet_CellValueChanged);
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnHuy);
            this.pnlFooter.Controls.Add(this.btnLuuPhieu);
            this.pnlFooter.Controls.Add(this.lblTongTienValue);
            this.pnlFooter.Controls.Add(this.lblTongTien);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlFooter.Location = new System.Drawing.Point(0, 555);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(900, 65);
            this.pnlFooter.TabIndex = 13;
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuy.BackColor = System.Drawing.Color.Transparent;
            this.btnHuy.BorderRadius = 15;
            this.btnHuy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(149)))), ((int)(((byte)(165)))), ((int)(((byte)(166)))));
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(780, 10);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 45);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Text = "HỦY";
            this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            // 
            // btnLuuPhieu
            // 
            this.btnLuuPhieu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLuuPhieu.BackColor = System.Drawing.Color.Transparent;
            this.btnLuuPhieu.BorderRadius = 15;
            this.btnLuuPhieu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnLuuPhieu.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLuuPhieu.ForeColor = System.Drawing.Color.White;
            this.btnLuuPhieu.Location = new System.Drawing.Point(534, 10);
            this.btnLuuPhieu.Name = "btnLuuPhieu";
            this.btnLuuPhieu.Size = new System.Drawing.Size(170, 45);
            this.btnLuuPhieu.TabIndex = 2;
            this.btnLuuPhieu.Text = "LƯU PHIẾU";
            this.btnLuuPhieu.Click += new System.EventHandler(this.BtnLuuPhieu_Click);
            // 
            // lblTongTienValue
            // 
            this.lblTongTienValue.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTienValue.AutoSize = true;
            this.lblTongTienValue.BackColor = System.Drawing.Color.Transparent;
            this.lblTongTienValue.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTongTienValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.lblTongTienValue.Location = new System.Drawing.Point(449, 16);
            this.lblTongTienValue.Name = "lblTongTienValue";
            this.lblTongTienValue.Size = new System.Drawing.Size(46, 30);
            this.lblTongTienValue.TabIndex = 1;
            this.lblTongTienValue.Text = "0 đ";
            // 
            // lblTongTien
            // 
            this.lblTongTien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongTien.AutoSize = true;
            this.lblTongTien.BackColor = System.Drawing.Color.Transparent;
            this.lblTongTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTongTien.Location = new System.Drawing.Point(339, 22);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(99, 21);
            this.lblTongTien.TabIndex = 0;
            this.lblTongTien.Text = "TỔNG TIỀN:";
            // 
            // btnThemDong
            // 
            this.btnThemDong.BorderRadius = 8;
            this.btnThemDong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(204)))), ((int)(((byte)(113)))));
            this.btnThemDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemDong.ForeColor = System.Drawing.Color.White;
            this.btnThemDong.Location = new System.Drawing.Point(15, 205);
            this.btnThemDong.Name = "btnThemDong";
            this.btnThemDong.Size = new System.Drawing.Size(120, 32);
            this.btnThemDong.TabIndex = 10;
            this.btnThemDong.Text = "Thêm dòng";
            this.btnThemDong.Click += new System.EventHandler(this.BtnThemDong_Click);
            // 
            // btnXoaDong
            // 
            this.btnXoaDong.BorderRadius = 8;
            this.btnXoaDong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnXoaDong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoaDong.ForeColor = System.Drawing.Color.White;
            this.btnXoaDong.Location = new System.Drawing.Point(145, 205);
            this.btnXoaDong.Name = "btnXoaDong";
            this.btnXoaDong.Size = new System.Drawing.Size(120, 32);
            this.btnXoaDong.TabIndex = 11;
            this.btnXoaDong.Text = "Xóa dòng";
            this.btnXoaDong.Click += new System.EventHandler(this.BtnXoaDong_Click);
            // 
            // txtScanner
            // 
            this.txtScanner.BorderRadius = 4;
            this.txtScanner.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtScanner.DefaultText = "";
            this.txtScanner.Font = new System.Drawing.Font("Consolas", 10F);
            this.txtScanner.Location = new System.Drawing.Point(280, 205);
            this.txtScanner.Name = "txtScanner";
            this.txtScanner.PlaceholderText = "[NHẬP HOẶC QUÉT MÃ SẢN PHẨM...]";
            this.txtScanner.SelectedText = "";
            this.txtScanner.Size = new System.Drawing.Size(280, 32);
            this.txtScanner.TabIndex = 14;
            this.txtScanner.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScanner_KeyDown);
            // 
            // btnToggleCamera
            // 
            this.btnToggleCamera.BorderRadius = 8;
            this.btnToggleCamera.FillColor = System.Drawing.Color.RoyalBlue;
            this.btnToggleCamera.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnToggleCamera.ForeColor = System.Drawing.Color.White;
            this.btnToggleCamera.Location = new System.Drawing.Point(570, 205);
            this.btnToggleCamera.Name = "btnToggleCamera";
            this.btnToggleCamera.Size = new System.Drawing.Size(100, 32);
            this.btnToggleCamera.TabIndex = 15;
            this.btnToggleCamera.Text = "Mã vạch";
            this.btnToggleCamera.Click += new System.EventHandler(this.btnToggleCamera_Click);
            // 
            // picCamera
            // 
            this.picCamera.BackColor = System.Drawing.Color.Black;
            this.picCamera.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCamera.Location = new System.Drawing.Point(570, 245);
            this.picCamera.Name = "picCamera";
            this.picCamera.Size = new System.Drawing.Size(205, 140);
            this.picCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCamera.TabIndex = 17;
            this.picCamera.TabStop = false;
            this.picCamera.Visible = false;
            // 
            // frmTaoPhieuKho
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(900, 620);
            this.Controls.Add(this.gridChiTiet);
            this.Controls.Add(this.picCamera);
            this.Controls.Add(this.btnThemDong);
            this.Controls.Add(this.btnXoaDong);
            this.Controls.Add(this.txtScanner);
            this.Controls.Add(this.btnToggleCamera);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTaoPhieuKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Tạo Phiếu Nhập/Xuất Kho";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlInfo.ResumeLayout(false);
            this.pnlInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboKhoView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNhaCungCap.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNhaCungCapView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoChungTu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLyDo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridChiTiet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChiTiet)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.Label lblMode;
        private Guna.UI2.WinForms.Guna2Panel pnlInfo;
        private System.Windows.Forms.Label lbl_cboKho;
        private DevExpress.XtraEditors.SearchLookUpEdit cboKho;
        private DevExpress.XtraGrid.Views.Grid.GridView cboKhoView;
        private System.Windows.Forms.Label lbl_cboNhaCungCap;
        private DevExpress.XtraEditors.SearchLookUpEdit cboNhaCungCap;
        private DevExpress.XtraGrid.Views.Grid.GridView cboNhaCungCapView;
        private System.Windows.Forms.Label lbl_txtSoChungTu;
        private DevExpress.XtraEditors.TextEdit txtSoChungTu;
        private System.Windows.Forms.Label lbl_dtNgay;
        private DevExpress.XtraEditors.DateEdit dtNgay;
        private System.Windows.Forms.Label lbl_txtLyDo;
        private DevExpress.XtraEditors.TextEdit txtLyDo;
        private DevExpress.XtraGrid.GridControl gridChiTiet;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewChiTiet;
        private Guna.UI2.WinForms.Guna2Panel pnlFooter;
        private System.Windows.Forms.Label lblTongTien;
        private System.Windows.Forms.Label lblTongTienValue;
        private Guna.UI2.WinForms.Guna2Button btnLuuPhieu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private Guna.UI2.WinForms.Guna2Button btnThemDong;
        private Guna.UI2.WinForms.Guna2Button btnXoaDong;
        private Guna.UI2.WinForms.Guna2TextBox txtScanner;
        private Guna.UI2.WinForms.Guna2Button btnToggleCamera;
        private System.Windows.Forms.PictureBox picCamera;
    }
}

