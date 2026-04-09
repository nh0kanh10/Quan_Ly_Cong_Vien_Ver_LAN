namespace GUI
{
    partial class frmLogin
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

        private void InitializeComponent()
        {
            this.pnlSideLogo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblBranding = new System.Windows.Forms.Label();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.pnlLoginFields = new Guna.UI2.WinForms.Guna2Panel();
            this.iconPass = new Guna.UI2.WinForms.Guna2PictureBox();
            this.iconUser = new Guna.UI2.WinForms.Guna2PictureBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblPass = new System.Windows.Forms.Label();
            this.txtPass = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnDangNhap = new Guna.UI2.WinForms.Guna2Button();
            this.btnThoat = new Guna.UI2.WinForms.Guna2Button();
            this.pnlSideLogo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlLoginFields.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPass)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconUser)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlSideLogo
            // 
            this.pnlSideLogo.BackColor = System.Drawing.Color.AntiqueWhite;
            this.pnlSideLogo.Controls.Add(this.lblBranding);
            this.pnlSideLogo.Controls.Add(this.picLogo);
            this.pnlSideLogo.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlSideLogo.Location = new System.Drawing.Point(0, 0);
            this.pnlSideLogo.Name = "pnlSideLogo";
            this.pnlSideLogo.Size = new System.Drawing.Size(250, 400);
            this.pnlSideLogo.TabIndex = 0;
            // 
            // lblBranding
            // 
            this.lblBranding.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblBranding.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.lblBranding.Location = new System.Drawing.Point(0, 240);
            this.lblBranding.Name = "lblBranding";
            this.lblBranding.Size = new System.Drawing.Size(250, 80);
            this.lblBranding.TabIndex = 1;
            this.lblBranding.Text = "TDC PARK\nMANAGEMENT";
            this.lblBranding.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.AntiqueWhite;
            this.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.picLogo.Location = new System.Drawing.Point(50, 80);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(150, 150);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // pnlLoginFields
            // 
            this.pnlLoginFields.BackColor = System.Drawing.Color.White;
            this.pnlLoginFields.Controls.Add(this.iconPass);
            this.pnlLoginFields.Controls.Add(this.iconUser);
            this.pnlLoginFields.Controls.Add(this.lblTitle);
            this.pnlLoginFields.Controls.Add(this.lblUser);
            this.pnlLoginFields.Controls.Add(this.txtUser);
            this.pnlLoginFields.Controls.Add(this.lblPass);
            this.pnlLoginFields.Controls.Add(this.txtPass);
            this.pnlLoginFields.Controls.Add(this.btnDangNhap);
            this.pnlLoginFields.Controls.Add(this.btnThoat);
            this.pnlLoginFields.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLoginFields.Location = new System.Drawing.Point(250, 0);
            this.pnlLoginFields.Name = "pnlLoginFields";
            this.pnlLoginFields.Padding = new System.Windows.Forms.Padding(30);
            this.pnlLoginFields.Size = new System.Drawing.Size(400, 400);
            this.pnlLoginFields.TabIndex = 1;
            // 
            // iconPass
            // 
            this.iconPass.BackColor = System.Drawing.Color.White;
            this.iconPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.iconPass.ImageRotate = 0F;
            this.iconPass.Location = new System.Drawing.Point(30, 205);
            this.iconPass.Name = "iconPass";
            this.iconPass.Size = new System.Drawing.Size(25, 29);
            this.iconPass.TabIndex = 8;
            this.iconPass.TabStop = false;
            // 
            // iconUser
            // 
            this.iconUser.BackColor = System.Drawing.Color.White;
            this.iconUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.iconUser.ImageRotate = 0F;
            this.iconUser.Location = new System.Drawing.Point(30, 134);
            this.iconUser.Name = "iconUser";
            this.iconUser.Size = new System.Drawing.Size(25, 29);
            this.iconUser.TabIndex = 7;
            this.iconUser.TabStop = false;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(31)))), ((int)(((byte)(30)))), ((int)(((byte)(68)))));
            this.lblTitle.Location = new System.Drawing.Point(169, 44);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(99, 37);
            this.lblTitle.TabIndex = 6;
            this.lblTitle.Text = "LOGIN";
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Cascadia Code SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblUser.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblUser.Location = new System.Drawing.Point(60, 110);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(135, 20);
            this.lblUser.TabIndex = 0;
            this.lblUser.Text = "Tên đăng nhập:";
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.White;
            this.txtUser.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUser.DefaultText = "";
            this.txtUser.Font = new System.Drawing.Font("Cascadia Code", 11.25F);
            this.txtUser.Location = new System.Drawing.Point(64, 130);
            this.txtUser.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtUser.Name = "txtUser";
            this.txtUser.Padding = new System.Windows.Forms.Padding(7);
            this.txtUser.PlaceholderText = "";
            this.txtUser.SelectedText = "";
            this.txtUser.Size = new System.Drawing.Size(306, 34);
            this.txtUser.TabIndex = 2;
            // 
            // lblPass
            // 
            this.lblPass.AutoSize = true;
            this.lblPass.Font = new System.Drawing.Font("Cascadia Code SemiBold", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPass.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblPass.Location = new System.Drawing.Point(60, 180);
            this.lblPass.Name = "lblPass";
            this.lblPass.Size = new System.Drawing.Size(90, 20);
            this.lblPass.TabIndex = 1;
            this.lblPass.Text = "Mật khẩu:";
            // 
            // txtPass
            // 
            this.txtPass.BackColor = System.Drawing.Color.White;
            this.txtPass.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPass.DefaultText = "";
            this.txtPass.Font = new System.Drawing.Font("Cascadia Code", 11.25F);
            this.txtPass.Location = new System.Drawing.Point(64, 200);
            this.txtPass.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtPass.Name = "txtPass";
            this.txtPass.Padding = new System.Windows.Forms.Padding(7);
            this.txtPass.PlaceholderText = "";
            this.txtPass.SelectedText = "";
            this.txtPass.Size = new System.Drawing.Size(306, 34);
            this.txtPass.TabIndex = 3;
            this.txtPass.UseSystemPasswordChar = true;
            // 
            // btnDangNhap
            // 
            this.btnDangNhap.BackColor = System.Drawing.Color.White;
            this.btnDangNhap.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnDangNhap.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.btnDangNhap.Font = new System.Drawing.Font("Cascadia Code", 11.25F);
            this.btnDangNhap.ForeColor = System.Drawing.Color.White;
            this.btnDangNhap.Location = new System.Drawing.Point(64, 270);
            this.btnDangNhap.Name = "btnDangNhap";
            this.btnDangNhap.Size = new System.Drawing.Size(296, 45);
            this.btnDangNhap.TabIndex = 4;
            this.btnDangNhap.Text = "Đăng nhập";
            this.btnDangNhap.Click += new System.EventHandler(this.btnDangNhap_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.White;
            this.btnThoat.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThoat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.btnThoat.Font = new System.Drawing.Font("Cascadia Code", 11.25F);
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(64, 330);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(296, 45);
            this.btnThoat.TabIndex = 5;
            this.btnThoat.Text = "Thoát chương trình";
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(650, 400);
            this.Controls.Add(this.pnlLoginFields);
            this.Controls.Add(this.pnlSideLogo);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Login System";
            this.pnlSideLogo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlLoginFields.ResumeLayout(false);
            this.pnlLoginFields.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.iconPass)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.iconUser)).EndInit();
            this.ResumeLayout(false);

        }


        private Guna.UI2.WinForms.Guna2Panel pnlSideLogo;
        private System.Windows.Forms.PictureBox picLogo;
        private System.Windows.Forms.Label lblBranding;
        private Guna.UI2.WinForms.Guna2Panel pnlLoginFields;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblUser;
        private Guna.UI2.WinForms.Guna2TextBox txtUser;
        private System.Windows.Forms.Label lblPass;
        private Guna.UI2.WinForms.Guna2TextBox txtPass;
        private Guna.UI2.WinForms.Guna2Button btnDangNhap;
        private Guna.UI2.WinForms.Guna2Button btnThoat;
        private Guna.UI2.WinForms.Guna2PictureBox iconUser;
        private Guna.UI2.WinForms.Guna2PictureBox iconPass;
    }
}
