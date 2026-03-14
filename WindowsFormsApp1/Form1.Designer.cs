namespace WindowsFormsApp1
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnDangXuat = new System.Windows.Forms.Button();
            this.btnTaiKhoan = new System.Windows.Forms.Button();
            this.btnKhachHang = new System.Windows.Forms.Button();
            this.btnLinhVuc = new System.Windows.Forms.Button();
            this.btnSach = new System.Windows.Forms.Button();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.lblUserRole = new System.Windows.Forms.Label();
            this.pnlDesktop = new System.Windows.Forms.Panel();
            this.mainMenuStrip = new System.Windows.Forms.MenuStrip();
            this.menuDanhMuc = new System.Windows.Forms.ToolStripMenuItem();
            this.menuBaoCao = new System.Windows.Forms.ToolStripMenuItem();
            this.menuHeThong = new System.Windows.Forms.ToolStripMenuItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.pnlMenu.SuspendLayout();
            this.pnlTitleBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.mainMenuStrip.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.pnlMenu.Controls.Add(this.btnThoat);
            this.pnlMenu.Controls.Add(this.btnDangXuat);
            this.pnlMenu.Controls.Add(this.btnTaiKhoan);
            this.pnlMenu.Controls.Add(this.btnKhachHang);
            this.pnlMenu.Controls.Add(this.btnLinhVuc);
            this.pnlMenu.Controls.Add(this.btnSach);
            this.pnlMenu.Controls.Add(this.pnlLogo);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 27);
            this.pnlMenu.Margin = new System.Windows.Forms.Padding(2);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(220, 585);
            this.pnlMenu.TabIndex = 0;
            // 
            // btnThoat
            // 
            this.btnThoat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnThoat.FlatAppearance.BorderSize = 0;
            this.btnThoat.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(128)))));
            this.btnThoat.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Red;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnThoat.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnThoat.Location = new System.Drawing.Point(0, 525);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnThoat.Size = new System.Drawing.Size(220, 60);
            this.btnThoat.TabIndex = 7;
            this.btnThoat.Text = "🚪  Thoát";
            this.btnThoat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnDangXuat
            // 
            this.btnDangXuat.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnDangXuat.FlatAppearance.BorderSize = 0;
            this.btnDangXuat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDangXuat.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnDangXuat.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnDangXuat.Location = new System.Drawing.Point(0, 305);
            this.btnDangXuat.Margin = new System.Windows.Forms.Padding(2);
            this.btnDangXuat.Name = "btnDangXuat";
            this.btnDangXuat.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnDangXuat.Size = new System.Drawing.Size(220, 60);
            this.btnDangXuat.TabIndex = 6;
            this.btnDangXuat.Text = "🔓  ĐĂNG XUẤT";
            this.btnDangXuat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnDangXuat.UseVisualStyleBackColor = false;
            this.btnDangXuat.Click += new System.EventHandler(this.btnDangXuat_Click);
            // 
            // btnTaiKhoan
            // 
            this.btnTaiKhoan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTaiKhoan.FlatAppearance.BorderSize = 0;
            this.btnTaiKhoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiKhoan.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 14.25F, System.Drawing.FontStyle.Bold);
            this.btnTaiKhoan.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnTaiKhoan.Location = new System.Drawing.Point(0, 245);
            this.btnTaiKhoan.Margin = new System.Windows.Forms.Padding(2);
            this.btnTaiKhoan.Name = "btnTaiKhoan";
            this.btnTaiKhoan.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnTaiKhoan.Size = new System.Drawing.Size(220, 60);
            this.btnTaiKhoan.TabIndex = 4;
            this.btnTaiKhoan.Text = "👥  TÀI KHOẢN";
            this.btnTaiKhoan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaiKhoan.UseVisualStyleBackColor = false;
            this.btnTaiKhoan.Click += new System.EventHandler(this.btnTaiKhoan_Click);
            // 
            // btnKhachHang
            // 
            this.btnKhachHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnKhachHang.FlatAppearance.BorderSize = 0;
            this.btnKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKhachHang.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKhachHang.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnKhachHang.Location = new System.Drawing.Point(0, 185);
            this.btnKhachHang.Margin = new System.Windows.Forms.Padding(2);
            this.btnKhachHang.Name = "btnKhachHang";
            this.btnKhachHang.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnKhachHang.Size = new System.Drawing.Size(220, 60);
            this.btnKhachHang.TabIndex = 3;
            this.btnKhachHang.Text = "🎡  TRÒ CHƠI";
            this.btnKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKhachHang.UseVisualStyleBackColor = false;
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);
            // 
            // btnLinhVuc
            // 
            this.btnLinhVuc.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLinhVuc.FlatAppearance.BorderSize = 0;
            this.btnLinhVuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLinhVuc.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLinhVuc.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnLinhVuc.Location = new System.Drawing.Point(0, 125);
            this.btnLinhVuc.Margin = new System.Windows.Forms.Padding(2);
            this.btnLinhVuc.Name = "btnLinhVuc";
            this.btnLinhVuc.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnLinhVuc.Size = new System.Drawing.Size(220, 60);
            this.btnLinhVuc.TabIndex = 2;
            this.btnLinhVuc.Text = "🗺️  KHU VỰC";
            this.btnLinhVuc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLinhVuc.UseVisualStyleBackColor = false;
            this.btnLinhVuc.Click += new System.EventHandler(this.btnLinhVuc_Click);
            // 
            // btnSach
            // 
            this.btnSach.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSach.FlatAppearance.BorderSize = 0;
            this.btnSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSach.Font = new System.Drawing.Font("Cascadia Mono SemiBold", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSach.ForeColor = System.Drawing.Color.Gainsboro;
            this.btnSach.Location = new System.Drawing.Point(0, 65);
            this.btnSach.Margin = new System.Windows.Forms.Padding(2);
            this.btnSach.Name = "btnSach";
            this.btnSach.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnSach.Size = new System.Drawing.Size(220, 60);
            this.btnSach.TabIndex = 1;
            this.btnSach.Text = "🎫  QUẢN LÝ VÉ";
            this.btnSach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSach.UseVisualStyleBackColor = false;
            this.btnSach.Click += new System.EventHandler(this.btnSach_Click);
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(39)))), ((int)(((byte)(39)))), ((int)(((byte)(58)))));
            this.pnlLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.ForeColor = System.Drawing.SystemColors.Desktop;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Margin = new System.Windows.Forms.Padding(2);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(220, 65);
            this.pnlLogo.TabIndex = 0;
            // 
            // pnlTitleBar
            // 
            this.pnlTitleBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.pnlTitleBar.Controls.Add(this.lblTitle);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(220, 27);
            this.pnlTitleBar.Margin = new System.Windows.Forms.Padding(2);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(909, 65);
            this.pnlTitleBar.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20.25F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(143, 14);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(596, 37);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ỨNG DỤNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panelControl1
            // 
            this.panelControl1.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(242)))), ((int)(((byte)(245)))));
            this.panelControl1.Appearance.Options.UseBackColor = true;
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.lblUserInfo);
            this.panelControl1.Controls.Add(this.lblUserRole);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(220, 577);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(909, 35);
            this.panelControl1.TabIndex = 0;
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblUserInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.lblUserInfo.Location = new System.Drawing.Point(760, 8);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(86, 17);
            this.lblUserInfo.TabIndex = 1;
            this.lblUserInfo.Text = "Chào: admin";
            // 
            // lblUserRole
            // 
            this.lblUserRole.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserRole.AutoSize = true;
            this.lblUserRole.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUserRole.ForeColor = System.Drawing.Color.SlateGray;
            this.lblUserRole.Location = new System.Drawing.Point(650, 8);
            this.lblUserRole.Name = "lblUserRole";
            this.lblUserRole.Size = new System.Drawing.Size(89, 17);
            this.lblUserRole.TabIndex = 2;
            this.lblUserRole.Text = "Quyền: Admin";
            // 
            // pnlDesktop
            // 
            this.pnlDesktop.BackColor = System.Drawing.Color.White;
            this.pnlDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDesktop.Location = new System.Drawing.Point(220, 92);
            this.pnlDesktop.Margin = new System.Windows.Forms.Padding(2);
            this.pnlDesktop.Name = "pnlDesktop";
            this.pnlDesktop.Size = new System.Drawing.Size(909, 485);
            this.pnlDesktop.TabIndex = 2;
            // 
            // mainMenuStrip
            // 
            this.mainMenuStrip.BackColor = System.Drawing.Color.White;
            this.mainMenuStrip.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.mainMenuStrip.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.mainMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuDanhMuc,
            this.menuBaoCao,
            this.menuHeThong});
            this.mainMenuStrip.Location = new System.Drawing.Point(0, 0);
            this.mainMenuStrip.Name = "mainMenuStrip";
            this.mainMenuStrip.Size = new System.Drawing.Size(1129, 27);
            this.mainMenuStrip.TabIndex = 3;
            this.mainMenuStrip.Text = "menuStrip1";
            // 
            // menuDanhMuc
            // 
            this.menuDanhMuc.Name = "menuDanhMuc";
            this.menuDanhMuc.Size = new System.Drawing.Size(121, 23);
            this.menuDanhMuc.Text = "📁 DANH MỤC";
            this.menuDanhMuc.Click += new System.EventHandler(this.menuDanhMuc_Click);
            // 
            // menuBaoCao
            // 
            this.menuBaoCao.Name = "menuBaoCao";
            this.menuBaoCao.Size = new System.Drawing.Size(105, 23);
            this.menuBaoCao.Text = "📊 BÁO CÁO";
            this.menuBaoCao.Click += new System.EventHandler(this.menuBaoCao_Click);
            // 
            // menuHeThong
            // 
            this.menuHeThong.Name = "menuHeThong";
            this.menuHeThong.Size = new System.Drawing.Size(116, 23);
            this.menuHeThong.Text = "⚙️ HỆ THỐNG";
            this.menuHeThong.Click += new System.EventHandler(this.menuHeThong_Click);
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1129, 612);
            this.Controls.Add(this.pnlDesktop);
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.pnlTitleBar);
            this.Controls.Add(this.pnlMenu);
            this.Controls.Add(this.mainMenuStrip);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống quản lý khu vui chơi TDC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlMenu.ResumeLayout(false);
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlTitleBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.panelControl1.PerformLayout();
            this.mainMenuStrip.ResumeLayout(false);
            this.mainMenuStrip.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel pnlMenu;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnDangXuat;
        private System.Windows.Forms.Button btnTaiKhoan;
        private System.Windows.Forms.Button btnKhachHang;
        private System.Windows.Forms.Button btnLinhVuc;
        private System.Windows.Forms.Button btnSach;
        private System.Windows.Forms.Panel pnlLogo;
        private System.Windows.Forms.Panel pnlTitleBar;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlDesktop;
        private System.Windows.Forms.Label lblUserInfo;
        private System.Windows.Forms.Label lblUserRole;
        private System.Windows.Forms.MenuStrip mainMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem menuDanhMuc;
        private System.Windows.Forms.ToolStripMenuItem menuBaoCao;
        private System.Windows.Forms.ToolStripMenuItem menuHeThong;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
    }
}
