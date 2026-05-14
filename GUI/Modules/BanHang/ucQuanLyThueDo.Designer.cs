namespace GUI.Modules.BanHang
{
    partial class ucQuanLyThueDo
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
            this.pnlBanner = new DevExpress.XtraEditors.PanelControl();
            this.lblThongTinPhien = new DevExpress.XtraEditors.LabelControl();
            this.btnDongPhien = new DevExpress.XtraEditors.SimpleButton();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.tabMain = new DevExpress.XtraTab.XtraTabControl();
            this.tabGiaoDo = new DevExpress.XtraTab.XtraTabPage();
            this.tabNhanTra = new DevExpress.XtraTab.XtraTabPage();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBanner)).BeginInit();
            this.pnlBanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).BeginInit();
            this.tabMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBanner
            // 
            this.pnlBanner.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(37)))), ((int)(((byte)(41)))));
            this.pnlBanner.Appearance.Options.UseBackColor = true;
            this.pnlBanner.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBanner.Controls.Add(this.lblThongTinPhien);
            this.pnlBanner.Controls.Add(this.btnDongPhien);
            this.pnlBanner.Controls.Add(this.lblTitle);
            this.pnlBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBanner.Location = new System.Drawing.Point(0, 0);
            this.pnlBanner.Name = "pnlBanner";
            this.pnlBanner.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlBanner.Size = new System.Drawing.Size(1100, 50);
            this.pnlBanner.TabIndex = 0;
            // 
            // lblThongTinPhien
            // 
            this.lblThongTinPhien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblThongTinPhien.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblThongTinPhien.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(173)))), ((int)(((byte)(181)))), ((int)(((byte)(189)))));
            this.lblThongTinPhien.Appearance.Options.UseFont = true;
            this.lblThongTinPhien.Appearance.Options.UseForeColor = true;
            this.lblThongTinPhien.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblThongTinPhien.Location = new System.Drawing.Point(661, 16);
            this.lblThongTinPhien.Name = "lblThongTinPhien";
            this.lblThongTinPhien.Size = new System.Drawing.Size(250, 20);
            this.lblThongTinPhien.TabIndex = 2;
            // 
            // btnDongPhien
            // 
            this.btnDongPhien.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDongPhien.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.btnDongPhien.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDongPhien.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnDongPhien.Appearance.Options.UseBackColor = true;
            this.btnDongPhien.Appearance.Options.UseFont = true;
            this.btnDongPhien.Appearance.Options.UseForeColor = true;
            this.btnDongPhien.ImageOptions.ImageUri.Uri = "Close";
            this.btnDongPhien.Location = new System.Drawing.Point(922, 8);
            this.btnDongPhien.Name = "btnDongPhien";
            this.btnDongPhien.Size = new System.Drawing.Size(166, 34);
            this.btnDongPhien.TabIndex = 1;
            this.btnDongPhien.Text = "Đóng Phiên (F8)";
            this.btnDongPhien.Click += new System.EventHandler(this.BtnDongPhien_Click);
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Location = new System.Drawing.Point(12, 8);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(300, 34);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ THUÊ ĐỒ";
            // 
            // tabMain
            // 
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 50);
            this.tabMain.Name = "tabMain";
            this.tabMain.SelectedTabPage = this.tabGiaoDo;
            this.tabMain.Size = new System.Drawing.Size(1100, 650);
            this.tabMain.TabIndex = 1;
            this.tabMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabGiaoDo,
            this.tabNhanTra});
            // 
            // tabGiaoDo
            // 
            this.tabGiaoDo.Name = "tabGiaoDo";
            this.tabGiaoDo.Size = new System.Drawing.Size(1098, 625);
            this.tabGiaoDo.Text = "Giao Đồ (Cho Thuê)  ";
            // 
            // tabNhanTra
            // 
            this.tabNhanTra.Name = "tabNhanTra";
            this.tabNhanTra.Size = new System.Drawing.Size(1098, 625);
            this.tabNhanTra.Text = "Nhận Trả Đồ & Hoàn Cọc  ";
            // 
            // ucQuanLyThueDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabMain);
            this.Controls.Add(this.pnlBanner);
            this.Name = "ucQuanLyThueDo";
            this.Size = new System.Drawing.Size(1100, 700);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBanner)).EndInit();
            this.pnlBanner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabMain)).EndInit();
            this.tabMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlBanner;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.SimpleButton btnDongPhien;
        private DevExpress.XtraEditors.LabelControl lblThongTinPhien;
        private DevExpress.XtraTab.XtraTabControl tabMain;
        private DevExpress.XtraTab.XtraTabPage tabGiaoDo;
        private DevExpress.XtraTab.XtraTabPage tabNhanTra;
    }
}
