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
            this.button1 = new System.Windows.Forms.Button();
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
            this.button2 = new System.Windows.Forms.Button();
            this.pnlMenu.SuspendLayout();
            this.pnlLogo.SuspendLayout();
            this.pnlTitleBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMenu
            // 
            this.pnlMenu.BackColor = System.Drawing.Color.MediumTurquoise;
            this.pnlMenu.Controls.Add(this.button2);
            this.pnlMenu.Controls.Add(this.button1);
            this.pnlMenu.Controls.Add(this.btnThoat);
            this.pnlMenu.Controls.Add(this.btnTaiKhoan);
            this.pnlMenu.Controls.Add(this.btnKhachHang);
            this.pnlMenu.Controls.Add(this.btnLinhVuc);
            this.pnlMenu.Controls.Add(this.btnSach);
            this.pnlMenu.Controls.Add(this.pnlLogo);
            this.pnlMenu.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlMenu.Location = new System.Drawing.Point(0, 0);
            this.pnlMenu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlMenu.Name = "pnlMenu";
            this.pnlMenu.Size = new System.Drawing.Size(220, 753);
            this.pnlMenu.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.MediumTurquoise;
            this.button1.Dock = System.Windows.Forms.DockStyle.Top;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.ForeColor = System.Drawing.Color.Black;
            this.button1.Location = new System.Drawing.Point(0, 320);
            this.button1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button1.Name = "button1";
            this.button1.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.button1.Size = new System.Drawing.Size(220, 60);
            this.button1.TabIndex = 9;
            this.button1.Text = "DỊCH VỤ";
            this.button1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnThoat.FlatAppearance.BorderSize = 0;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.ForeColor = System.Drawing.Color.Black;
            this.btnThoat.Location = new System.Drawing.Point(0, 693);
            this.btnThoat.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnThoat.Size = new System.Drawing.Size(220, 60);
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
            this.btnTaiKhoan.Location = new System.Drawing.Point(0, 260);
            this.btnTaiKhoan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnTaiKhoan.Name = "btnTaiKhoan";
            this.btnTaiKhoan.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnTaiKhoan.Size = new System.Drawing.Size(220, 60);
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
            this.btnKhachHang.Location = new System.Drawing.Point(0, 200);
            this.btnKhachHang.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnKhachHang.Name = "btnKhachHang";
            this.btnKhachHang.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnKhachHang.Size = new System.Drawing.Size(220, 60);
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
            this.btnLinhVuc.Location = new System.Drawing.Point(0, 140);
            this.btnLinhVuc.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLinhVuc.Name = "btnLinhVuc";
            this.btnLinhVuc.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnLinhVuc.Size = new System.Drawing.Size(220, 60);
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
            this.btnSach.Location = new System.Drawing.Point(0, 80);
            this.btnSach.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSach.Name = "btnSach";
            this.btnSach.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.btnSach.Size = new System.Drawing.Size(220, 60);
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
            this.pnlLogo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlLogo.Name = "pnlLogo";
            this.pnlLogo.Size = new System.Drawing.Size(220, 80);
            this.pnlLogo.TabIndex = 0;
            // 
            // lblLogo
            // 
            this.lblLogo.AutoSize = true;
            this.lblLogo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLogo.ForeColor = System.Drawing.Color.Black;
            this.lblLogo.Location = new System.Drawing.Point(73, 25);
            this.lblLogo.Name = "lblLogo";
            this.lblLogo.Size = new System.Drawing.Size(57, 28);
            this.lblLogo.TabIndex = 0;
            this.lblLogo.Text = "APM";
            this.lblLogo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlTitleBar
            // 
            this.pnlTitleBar.BackColor = System.Drawing.Color.DarkTurquoise;
            this.pnlTitleBar.Controls.Add(this.lblTitle);
            this.pnlTitleBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitleBar.Location = new System.Drawing.Point(220, 0);
            this.pnlTitleBar.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlTitleBar.Name = "pnlTitleBar";
            this.pnlTitleBar.Size = new System.Drawing.Size(961, 80);
            this.pnlTitleBar.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16.2F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.Black;
            this.lblTitle.Location = new System.Drawing.Point(168, 17);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(618, 38);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ỨNG DỤNG QUẢN LÝ KHU VUI CHƠI GIẢI TRÍ";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlDesktop
            // 
            this.pnlDesktop.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDesktop.Location = new System.Drawing.Point(220, 80);
            this.pnlDesktop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlDesktop.Name = "pnlDesktop";
            this.pnlDesktop.Size = new System.Drawing.Size(961, 673);
            this.pnlDesktop.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.Color.MediumTurquoise;
            this.button2.Dock = System.Windows.Forms.DockStyle.Top;
            this.button2.FlatAppearance.BorderSize = 0;
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button2.ForeColor = System.Drawing.Color.Black;
            this.button2.Location = new System.Drawing.Point(0, 380);
            this.button2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.button2.Name = "button2";
            this.button2.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.button2.Size = new System.Drawing.Size(220, 60);
            this.button2.TabIndex = 10;
            this.button2.Text = "POS_BÁN VÉ";
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1181, 753);
            this.Controls.Add(this.pnlDesktop);
            this.Controls.Add(this.pnlTitleBar);
            this.Controls.Add(this.pnlMenu);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
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

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
    }
}

