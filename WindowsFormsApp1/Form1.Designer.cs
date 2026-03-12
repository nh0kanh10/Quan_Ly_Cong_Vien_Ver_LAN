namespace WindowsFormsApp1
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Các Panel chính
        private System.Windows.Forms.Panel pnlMenu; // Thanh menu bên trái
        private System.Windows.Forms.Panel pnlLogo; // Logo góc trên trái
        private System.Windows.Forms.Panel pnlTitleBar; // Thanh tiêu đề trên cùng
        private System.Windows.Forms.Panel pnlDesktop; // Panel chứa form con

        // Các Label
        private System.Windows.Forms.Label lblLogo;
        private System.Windows.Forms.Label lblTitle;

        // Các Button Menu
        private System.Windows.Forms.Button btnSach;
        private System.Windows.Forms.Button btnLinhVuc;
        private System.Windows.Forms.Button btnKhachHang;
        private System.Windows.Forms.Button btnTaiKhoan;
        private System.Windows.Forms.Button btnThoat;
      

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
            this.pnlMenu = new System.Windows.Forms.Panel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnTaiKhoan = new System.Windows.Forms.Button();
            this.btnKhachHang = new System.Windows.Forms.Button();
            this.btnLinhVuc = new System.Windows.Forms.Button();
            this.btnSach = new System.Windows.Forms.Button();
            this.pnlLogo = new System.Windows.Forms.Panel();
            this.lblLogo = new System.Windows.Forms.Label();
            this.pnlTitleBar = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlDesktop = new System.Windows.Forms.Panel();
            this.pnlMenu.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.MediumTurquoise;
            this.pnlMenu.Controls.Add(this.btnThoat);
            this.pnlMenu.Controls.Add(this.btnTaiKhoan);
            this.pnlMenu.Controls.Add(this.btnKhachHang);
            this.pnlMenu.Controls.Add(this.btnLinhVuc);
            this.pnlMenu.Controls.Add(this.btnSach);
            this.pnlMenu.Controls.Add(this.pnlLogo);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(165, 612);
            this.pnlMenu.TabIndex = 0;
            // 
            // btnThoat
            // 
            this.btnThoat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnThoat.FlatAppearance.BorderSize = 0;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.ForeColor = System.Drawing.Color.Black;
            this.btnThoat.Location = new System.Drawing.Point(0, 563);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btnThoat.Size = new System.Drawing.Size(165, 49);
            this.btnThoat.TabIndex = 7;
            this.btnThoat.Text = "   Thoát";
            this.btnThoat.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnThoat.UseVisualStyleBackColor = true;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnTaiKhoan
            // 
            this.btnTaiKhoan.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnTaiKhoan.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnTaiKhoan.FlatAppearance.BorderSize = 0;
            this.btnTaiKhoan.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTaiKhoan.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTaiKhoan.ForeColor = System.Drawing.Color.Black;
            this.btnTaiKhoan.Location = new System.Drawing.Point(0, 212);
            this.btnTaiKhoan.Margin = new System.Windows.Forms.Padding(2);
            this.btnTaiKhoan.Name = "btnTaiKhoan";
            this.btnTaiKhoan.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btnTaiKhoan.Size = new System.Drawing.Size(165, 49);
            this.btnTaiKhoan.TabIndex = 8;
            this.btnTaiKhoan.Text = "TÀI KHOẢN";
            this.btnTaiKhoan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnTaiKhoan.UseVisualStyleBackColor = false;
            this.btnTaiKhoan.Click += new System.EventHandler(this.btnTaiKhoan_Click);
            // 
            // btnKhachHang
            // 
            this.btnKhachHang.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnKhachHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnKhachHang.FlatAppearance.BorderSize = 0;
            this.btnKhachHang.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnKhachHang.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnKhachHang.ForeColor = System.Drawing.Color.Black;
            this.btnKhachHang.Location = new System.Drawing.Point(0, 163);
            this.btnKhachHang.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnKhachHang.Name = "btnKhachHang";
            this.btnKhachHang.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btnKhachHang.Size = new System.Drawing.Size(165, 49);
            this.btnKhachHang.TabIndex = 3;
            this.btnKhachHang.Text = "TRÒ CHƠI";
            this.btnKhachHang.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnKhachHang.UseVisualStyleBackColor = false;
            this.btnKhachHang.Click += new System.EventHandler(this.btnKhachHang_Click);
            // 
            // btnLinhVuc
            // 
            this.btnLinhVuc.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnLinhVuc.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnLinhVuc.FlatAppearance.BorderSize = 0;
            this.btnLinhVuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLinhVuc.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLinhVuc.ForeColor = System.Drawing.Color.Black;
            this.btnLinhVuc.Location = new System.Drawing.Point(0, 114);
            this.btnLinhVuc.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnLinhVuc.Name = "btnLinhVuc";
            this.btnLinhVuc.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btnLinhVuc.Size = new System.Drawing.Size(165, 49);
            this.btnLinhVuc.TabIndex = 2;
            this.btnLinhVuc.Text = "KHU VỰC";
            this.btnLinhVuc.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnLinhVuc.UseVisualStyleBackColor = false;
            this.btnLinhVuc.Click += new System.EventHandler(this.btnLinhVuc_Click);
            // 
            // btnSach
            // 
            this.btnSach.BackColor = System.Drawing.Color.MediumTurquoise;
            this.btnSach.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSach.FlatAppearance.BorderSize = 0;
            this.btnSach.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSach.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSach.ForeColor = System.Drawing.Color.Black;
            this.btnSach.Location = new System.Drawing.Point(0, 65);
            this.btnSach.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.btnSach.Name = "btnSach";
            this.btnSach.Padding = new System.Windows.Forms.Padding(9, 0, 0, 0);
            this.btnSach.Size = new System.Drawing.Size(165, 49);
            this.btnSach.TabIndex = 1;
            this.btnSach.Text = "QUẢN LÝ VÉ";
            this.btnSach.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnSach.UseVisualStyleBackColor = false;
            this.btnSach.Click += new System.EventHandler(this.btnSach_Click);
            // 
            // pnlLogo
            // 
            this.pnlLogo.BackColor = System.Drawing.Color.MediumTurquoise;
            this.pnlLogo.Controls.Add(this.lblLogo);
            this.pnlLogo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlLogo.ForeColor = System.Drawing.SystemColors.Desktop;
            this.pnlLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlLogo.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(165, 65);
            this.pnlLogo.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.Black;
            this.lblLogo.Location = new System.Drawing.Point(55, 20);
            this.lblLogo.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(46, 21);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "APM";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTitleBar
            // 
            this.pnlTitleBar.BackColor = System.Drawing.Color.DarkTurquoise;
            this.pnlTitleBar.Controls.Add(this.lblTitle);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(165, 0);
            this.pnlTitleBar.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(721, 65);
            this.pnlTitleBar.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(126, 14);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(483, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ỨNG DỤNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlDesktop
            // 
            this.pnlDesktop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDesktop.Location = new System.Drawing.Point(165, 65);
            this.pnlDesktop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.pnlDesktop.Name = "pnlDesktop";
            this.pnlDesktop.Size = new System.Drawing.Size(721, 547);
            this.pnlDesktop.TabIndex = 2;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(886, 612);
            this.Controls.Add(this.pnlDesktop);
            this.Controls.Add(this.pnlTitleBar);
            this.Controls.Add(this.pnlMenu);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống quản lý nhà sách";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlMenu.ResumeLayout(false);
            this.pnlLogo.ResumeLayout(false);
            this.pnlLogo.PerformLayout();
            this.pnlTitleBar.ResumeLayout(false);
            this.pnlTitleBar.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
    }
}

