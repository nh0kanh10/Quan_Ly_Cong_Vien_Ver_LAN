namespace GUI
{
    partial class frmQuayVe_LeTan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.txtMaBooking = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnTimDoan = new Guna.UI2.WinForms.Guna2Button();
            this.pnlDoanInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblTenDoan = new System.Windows.Forms.Label();
            this.lblSubText = new System.Windows.Forms.Label();
            this.lblTongQuota = new System.Windows.Forms.Label();
            this.gcQuota = new DevExpress.XtraGrid.GridControl();
            this.gvQuota = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlToolbar = new System.Windows.Forms.Panel();
            this.btnKhoaSo = new Guna.UI2.WinForms.Guna2Button();
            this.btnHoanHuy = new Guna.UI2.WinForms.Guna2Button();
            this.btnMuaThem = new Guna.UI2.WinForms.Guna2Button();
            this.pnlDoanInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcQuota)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvQuota)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(17, 13);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(373, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "LỄ TÂN — XỬ LÝ PHÁT SINH ĐOÀN";
            // 
            // txtMaBooking
            // 
            this.txtMaBooking.BorderRadius = 8;
            this.txtMaBooking.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaBooking.DefaultText = "";
            this.txtMaBooking.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaBooking.Location = new System.Drawing.Point(21, 52);
            this.txtMaBooking.Name = "txtMaBooking";
            this.txtMaBooking.PlaceholderText = "Nhập mã booking (BK-xxx) hoặc tên đoàn...";
            this.txtMaBooking.SelectedText = "";
            this.txtMaBooking.Size = new System.Drawing.Size(343, 35);
            this.txtMaBooking.TabIndex = 1;
            this.txtMaBooking.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaBooking_KeyDown);
            // 
            // btnTimDoan
            // 
            this.btnTimDoan.BorderRadius = 8;
            this.btnTimDoan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnTimDoan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTimDoan.ForeColor = System.Drawing.Color.White;
            this.btnTimDoan.Location = new System.Drawing.Point(373, 52);
            this.btnTimDoan.Name = "btnTimDoan";
            this.btnTimDoan.Size = new System.Drawing.Size(103, 35);
            this.btnTimDoan.TabIndex = 2;
            this.btnTimDoan.Text = "Tìm";
            this.btnTimDoan.Click += new System.EventHandler(this.btnTimDoan_Click);
            // 
            // pnlDoanInfo
            // 
            this.pnlDoanInfo.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlDoanInfo.BorderRadius = 10;
            this.pnlDoanInfo.BorderThickness = 1;
            this.pnlDoanInfo.Controls.Add(this.lblTrangThai);
            this.pnlDoanInfo.Controls.Add(this.lblTenDoan);
            this.pnlDoanInfo.Controls.Add(this.lblSubText);
            this.pnlDoanInfo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlDoanInfo.Location = new System.Drawing.Point(21, 100);
            this.pnlDoanInfo.Name = "pnlDoanInfo";
            this.pnlDoanInfo.Size = new System.Drawing.Size(797, 69);
            this.pnlDoanInfo.TabIndex = 3;
            this.pnlDoanInfo.Visible = false;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrangThai.BackColor = System.Drawing.Color.Transparent;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTrangThai.Location = new System.Drawing.Point(583, 22);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(197, 26);
            this.lblTrangThai.TabIndex = 0;
            this.lblTrangThai.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblTenDoan
            // 
            this.lblTenDoan.AutoSize = true;
            this.lblTenDoan.BackColor = System.Drawing.Color.Transparent;
            this.lblTenDoan.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTenDoan.Location = new System.Drawing.Point(17, 13);
            this.lblTenDoan.Name = "lblTenDoan";
            this.lblTenDoan.Size = new System.Drawing.Size(0, 30);
            this.lblTenDoan.TabIndex = 1;
            // 
            // lblSubText
            // 
            this.lblSubText.AutoSize = true;
            this.lblSubText.BackColor = System.Drawing.Color.Transparent;
            this.lblSubText.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubText.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSubText.Location = new System.Drawing.Point(17, 42);
            this.lblSubText.Name = "lblSubText";
            this.lblSubText.Size = new System.Drawing.Size(0, 19);
            this.lblSubText.TabIndex = 2;
            // 
            // lblTongQuota
            // 
            this.lblTongQuota.AutoSize = true;
            this.lblTongQuota.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongQuota.Location = new System.Drawing.Point(13, 10);
            this.lblTongQuota.Name = "lblTongQuota";
            this.lblTongQuota.Size = new System.Drawing.Size(0, 19);
            this.lblTongQuota.TabIndex = 0;
            // 
            // gcQuota
            // 
            this.gcQuota.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gcQuota.Location = new System.Drawing.Point(21, 225);
            this.gcQuota.MainView = this.gvQuota;
            this.gcQuota.Name = "gcQuota";
            this.gcQuota.Size = new System.Drawing.Size(797, 273);
            this.gcQuota.TabIndex = 5;
            this.gcQuota.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvQuota});
            // 
            // gvQuota
            // 
            this.gvQuota.DetailHeight = 303;
            this.gvQuota.GridControl = this.gcQuota;
            this.gvQuota.Name = "gvQuota";
            this.gvQuota.OptionsBehavior.Editable = false;
            this.gvQuota.OptionsView.ShowGroupPanel = false;
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlToolbar.Controls.Add(this.lblTongQuota);
            this.pnlToolbar.Controls.Add(this.btnKhoaSo);
            this.pnlToolbar.Controls.Add(this.btnHoanHuy);
            this.pnlToolbar.Controls.Add(this.btnMuaThem);
            this.pnlToolbar.Location = new System.Drawing.Point(21, 178);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(797, 39);
            this.pnlToolbar.TabIndex = 4;
            this.pnlToolbar.Visible = false;
            // 
            // btnKhoaSo
            // 
            this.btnKhoaSo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKhoaSo.BorderRadius = 6;
            this.btnKhoaSo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.btnKhoaSo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnKhoaSo.ForeColor = System.Drawing.Color.White;
            this.btnKhoaSo.Location = new System.Drawing.Point(612, 0);
            this.btnKhoaSo.Name = "btnKhoaSo";
            this.btnKhoaSo.Size = new System.Drawing.Size(185, 35);
            this.btnKhoaSo.TabIndex = 1;
            this.btnKhoaSo.Text = "KHÓA SỔ LỄ TÂN";
            this.btnKhoaSo.Click += new System.EventHandler(this.btnChotDoan_Click);
            // 
            // btnHoanHuy
            // 
            this.btnHoanHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHoanHuy.BorderRadius = 6;
            this.btnHoanHuy.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnHoanHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHoanHuy.ForeColor = System.Drawing.Color.White;
            this.btnHoanHuy.Location = new System.Drawing.Point(404, 0);
            this.btnHoanHuy.Name = "btnHoanHuy";
            this.btnHoanHuy.Size = new System.Drawing.Size(203, 35);
            this.btnHoanHuy.TabIndex = 2;
            this.btnHoanHuy.Text = "➖ HOÀN / HỦY DỊCH VỤ";
            this.btnHoanHuy.Click += new System.EventHandler(this.btnRutBot_Click);
            // 
            // btnMuaThem
            // 
            this.btnMuaThem.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMuaThem.BorderRadius = 6;
            this.btnMuaThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnMuaThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnMuaThem.ForeColor = System.Drawing.Color.White;
            this.btnMuaThem.Location = new System.Drawing.Point(195, 1);
            this.btnMuaThem.Name = "btnMuaThem";
            this.btnMuaThem.Size = new System.Drawing.Size(202, 35);
            this.btnMuaThem.TabIndex = 3;
            this.btnMuaThem.Text = "➕ MUA THÊM DỊCH VỤ";
            this.btnMuaThem.Click += new System.EventHandler(this.btnBomThem_Click);
            // 
            // frmQuayVe_LeTan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(840, 520);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.txtMaBooking);
            this.Controls.Add(this.btnTimDoan);
            this.Controls.Add(this.pnlDoanInfo);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.gcQuota);
            this.Name = "frmQuayVe_LeTan";
            this.pnlDoanInfo.ResumeLayout(false);
            this.pnlDoanInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcQuota)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvQuota)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2TextBox txtMaBooking;
        private Guna.UI2.WinForms.Guna2Button btnTimDoan;
        private System.Windows.Forms.Label lblTenDoan;
        private System.Windows.Forms.Label lblSubText;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblTongQuota;
        private Guna.UI2.WinForms.Guna2Panel pnlDoanInfo;
        private DevExpress.XtraGrid.GridControl gcQuota;
        private DevExpress.XtraGrid.Views.Grid.GridView gvQuota;
        private System.Windows.Forms.Panel pnlToolbar;
        private Guna.UI2.WinForms.Guna2Button btnMuaThem;
        private Guna.UI2.WinForms.Guna2Button btnHoanHuy;
        private Guna.UI2.WinForms.Guna2Button btnKhoaSo;
    }
}
