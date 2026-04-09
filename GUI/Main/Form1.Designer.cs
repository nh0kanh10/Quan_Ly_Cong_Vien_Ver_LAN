namespace GUI
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
            this.pnlMainBar = new Guna.UI2.WinForms.Guna2Panel();
            this.flowMainNav = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlLogoArea = new System.Windows.Forms.Panel();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.btnCat1 = new Guna.UI2.WinForms.Guna2Button();
            this.btnCat2 = new Guna.UI2.WinForms.Guna2Button();
            this.btnCat3 = new Guna.UI2.WinForms.Guna2Button();
            this.btnCat4 = new Guna.UI2.WinForms.Guna2Button();
            this.btnCat5 = new Guna.UI2.WinForms.Guna2Button();
            this.pnlUserArea = new System.Windows.Forms.Panel();
            this.lblUserName = new System.Windows.Forms.Label();
            this.btnSettings = new Guna.UI2.WinForms.Guna2Button();
            this.btnWindowMin = new Guna.UI2.WinForms.Guna2Button();
            this.btnWindowMax = new Guna.UI2.WinForms.Guna2Button();
            this.btnWindowClose = new Guna.UI2.WinForms.Guna2Button();
            this.pnlSubBar = new Guna.UI2.WinForms.Guna2Panel();
            this.flowSubNav = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlDesktop = new System.Windows.Forms.Panel();
            this.pnlStatusBar = new Guna.UI2.WinForms.Guna2Panel();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblClock = new System.Windows.Forms.Label();
            this.pnlMainBar.SuspendLayout();
            this.flowMainNav.SuspendLayout();
            this.pnlLogoArea.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            this.pnlUserArea.SuspendLayout();
            this.pnlSubBar.SuspendLayout();
            this.pnlStatusBar.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMainBar
            // 
            this.pnlMainBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlMainBar.Controls.Add(this.flowMainNav);
            this.pnlMainBar.Controls.Add(this.pnlUserArea);
            this.pnlMainBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlMainBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlMainBar.Location = new System.Drawing.Point(0, 0);
            this.pnlMainBar.Name = "pnlMainBar";
            this.pnlMainBar.Size = new System.Drawing.Size(1157, 44);
            this.pnlMainBar.TabIndex = 0;
            // 
            // flowMainNav
            // 
            this.flowMainNav.AutoSize = true;
            this.flowMainNav.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowMainNav.BackColor = System.Drawing.Color.Transparent;
            this.flowMainNav.Controls.Add(this.pnlLogoArea);
            this.flowMainNav.Controls.Add(this.btnCat1);
            this.flowMainNav.Controls.Add(this.btnCat2);
            this.flowMainNav.Controls.Add(this.btnCat3);
            this.flowMainNav.Controls.Add(this.btnCat4);
            this.flowMainNav.Controls.Add(this.btnCat5);
            this.flowMainNav.Dock = System.Windows.Forms.DockStyle.Left;
            this.flowMainNav.Location = new System.Drawing.Point(0, 0);
            this.flowMainNav.Name = "flowMainNav";
            this.flowMainNav.Size = new System.Drawing.Size(810, 44);
            this.flowMainNav.TabIndex = 0;
            this.flowMainNav.WrapContents = false;
            // 
            // pnlLogoArea
            // 
            this.pnlLogoArea.BackColor = System.Drawing.Color.Transparent;
            this.pnlLogoArea.Controls.Add(this.picLogo);
            this.pnlLogoArea.Location = new System.Drawing.Point(0, 0);
            this.pnlLogoArea.Margin = new System.Windows.Forms.Padding(0);
            this.pnlLogoArea.Name = "pnlLogoArea";
            this.pnlLogoArea.Size = new System.Drawing.Size(160, 44);
            this.pnlLogoArea.TabIndex = 0;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.Transparent;
            this.picLogo.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.picLogo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picLogo.Location = new System.Drawing.Point(0, 0);
            this.picLogo.Name = "picLogo";
            this.picLogo.Padding = new System.Windows.Forms.Padding(12, 4, 8, 4);
            this.picLogo.Size = new System.Drawing.Size(160, 44);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 0;
            this.picLogo.TabStop = false;
            // 
            // btnCat1
            // 
            this.btnCat1.Animated = true;
            this.btnCat1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCat1.FillColor = System.Drawing.Color.Transparent;
            this.btnCat1.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnCat1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnCat1.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCat1.ImageOffset = new System.Drawing.Point(6, 0);
            this.btnCat1.ImageSize = new System.Drawing.Size(18, 18);
            this.btnCat1.Location = new System.Drawing.Point(160, 0);
            this.btnCat1.Margin = new System.Windows.Forms.Padding(0);
            this.btnCat1.Name = "btnCat1";
            this.btnCat1.Size = new System.Drawing.Size(130, 44);
            this.btnCat1.TabIndex = 1;
            this.btnCat1.Text = "TIỀN SẢNH";
            this.btnCat1.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCat1.TextOffset = new System.Drawing.Point(8, 0);
            // 
            // btnCat2
            // 
            this.btnCat2.Animated = true;
            this.btnCat2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCat2.FillColor = System.Drawing.Color.Transparent;
            this.btnCat2.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnCat2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnCat2.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCat2.ImageOffset = new System.Drawing.Point(6, 0);
            this.btnCat2.ImageSize = new System.Drawing.Size(18, 18);
            this.btnCat2.Location = new System.Drawing.Point(290, 0);
            this.btnCat2.Margin = new System.Windows.Forms.Padding(0);
            this.btnCat2.Name = "btnCat2";
            this.btnCat2.Size = new System.Drawing.Size(130, 44);
            this.btnCat2.TabIndex = 2;
            this.btnCat2.Text = "QUẢN TRỊ";
            this.btnCat2.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCat2.TextOffset = new System.Drawing.Point(8, 0);
            // 
            // btnCat3
            // 
            this.btnCat3.Animated = true;
            this.btnCat3.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCat3.FillColor = System.Drawing.Color.Transparent;
            this.btnCat3.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnCat3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnCat3.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCat3.ImageOffset = new System.Drawing.Point(6, 0);
            this.btnCat3.ImageSize = new System.Drawing.Size(18, 18);
            this.btnCat3.Location = new System.Drawing.Point(420, 0);
            this.btnCat3.Margin = new System.Windows.Forms.Padding(0);
            this.btnCat3.Name = "btnCat3";
            this.btnCat3.Size = new System.Drawing.Size(130, 44);
            this.btnCat3.TabIndex = 3;
            this.btnCat3.Text = "VẬN HÀNH";
            this.btnCat3.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCat3.TextOffset = new System.Drawing.Point(8, 0);
            // 
            // btnCat4
            // 
            this.btnCat4.Animated = true;
            this.btnCat4.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCat4.FillColor = System.Drawing.Color.Transparent;
            this.btnCat4.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnCat4.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnCat4.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCat4.ImageOffset = new System.Drawing.Point(6, 0);
            this.btnCat4.ImageSize = new System.Drawing.Size(18, 18);
            this.btnCat4.Location = new System.Drawing.Point(550, 0);
            this.btnCat4.Margin = new System.Windows.Forms.Padding(0);
            this.btnCat4.Name = "btnCat4";
            this.btnCat4.Size = new System.Drawing.Size(130, 44);
            this.btnCat4.TabIndex = 4;
            this.btnCat4.Text = "BÁO CÁO";
            this.btnCat4.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCat4.TextOffset = new System.Drawing.Point(8, 0);
            // 
            // btnCat5
            // 
            this.btnCat5.Animated = true;
            this.btnCat5.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCat5.FillColor = System.Drawing.Color.Transparent;
            this.btnCat5.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.btnCat5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnCat5.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCat5.ImageOffset = new System.Drawing.Point(6, 0);
            this.btnCat5.ImageSize = new System.Drawing.Size(18, 18);
            this.btnCat5.Location = new System.Drawing.Point(680, 0);
            this.btnCat5.Margin = new System.Windows.Forms.Padding(0);
            this.btnCat5.Name = "btnCat5";
            this.btnCat5.Size = new System.Drawing.Size(130, 44);
            this.btnCat5.TabIndex = 5;
            this.btnCat5.Text = "HỆ THỐNG";
            this.btnCat5.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCat5.TextOffset = new System.Drawing.Point(8, 0);
            // 
            // pnlUserArea
            // 
            this.pnlUserArea.BackColor = System.Drawing.Color.Transparent;
            this.pnlUserArea.Controls.Add(this.lblUserName);
            this.pnlUserArea.Controls.Add(this.btnSettings);
            this.pnlUserArea.Controls.Add(this.btnWindowMin);
            this.pnlUserArea.Controls.Add(this.btnWindowMax);
            this.pnlUserArea.Controls.Add(this.btnWindowClose);
            this.pnlUserArea.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlUserArea.Location = new System.Drawing.Point(857, 0);
            this.pnlUserArea.Name = "pnlUserArea";
            this.pnlUserArea.Size = new System.Drawing.Size(300, 44);
            this.pnlUserArea.TabIndex = 1;
            // 
            // lblUserName
            // 
            this.lblUserName.BackColor = System.Drawing.Color.Transparent;
            this.lblUserName.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblUserName.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F);
            this.lblUserName.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.lblUserName.Location = new System.Drawing.Point(0, 0);
            this.lblUserName.Name = "lblUserName";
            this.lblUserName.Padding = new System.Windows.Forms.Padding(8, 0, 0, 0);
            this.lblUserName.Size = new System.Drawing.Size(140, 44);
            this.lblUserName.TabIndex = 0;
            this.lblUserName.Text = "Admin";
            this.lblUserName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSettings
            // 
            this.btnSettings.Animated = true;
            this.btnSettings.BorderRadius = 6;
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.FillColor = System.Drawing.Color.Transparent;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnSettings.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnSettings.ImageSize = new System.Drawing.Size(18, 18);
            this.btnSettings.Location = new System.Drawing.Point(148, 4);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(36, 36);
            this.btnSettings.TabIndex = 1;
            // 
            // btnWindowMin
            // 
            this.btnWindowMin.Animated = true;
            this.btnWindowMin.BorderRadius = 4;
            this.btnWindowMin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWindowMin.FillColor = System.Drawing.Color.Transparent;
            this.btnWindowMin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnWindowMin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnWindowMin.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnWindowMin.ImageSize = new System.Drawing.Size(14, 14);
            this.btnWindowMin.Location = new System.Drawing.Point(198, 4);
            this.btnWindowMin.Name = "btnWindowMin";
            this.btnWindowMin.Size = new System.Drawing.Size(32, 36);
            this.btnWindowMin.TabIndex = 2;
            // 
            // btnWindowMax
            // 
            this.btnWindowMax.Animated = true;
            this.btnWindowMax.BorderRadius = 4;
            this.btnWindowMax.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWindowMax.FillColor = System.Drawing.Color.Transparent;
            this.btnWindowMax.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnWindowMax.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnWindowMax.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnWindowMax.ImageSize = new System.Drawing.Size(14, 14);
            this.btnWindowMax.Location = new System.Drawing.Point(232, 4);
            this.btnWindowMax.Name = "btnWindowMax";
            this.btnWindowMax.Size = new System.Drawing.Size(32, 36);
            this.btnWindowMax.TabIndex = 3;
            // 
            // btnWindowClose
            // 
            this.btnWindowClose.Animated = true;
            this.btnWindowClose.BorderRadius = 4;
            this.btnWindowClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnWindowClose.FillColor = System.Drawing.Color.Transparent;
            this.btnWindowClose.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnWindowClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnWindowClose.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(232)))), ((int)(((byte)(65)))), ((int)(((byte)(65)))));
            this.btnWindowClose.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnWindowClose.ImageSize = new System.Drawing.Size(14, 14);
            this.btnWindowClose.Location = new System.Drawing.Point(266, 4);
            this.btnWindowClose.Name = "btnWindowClose";
            this.btnWindowClose.Size = new System.Drawing.Size(32, 36);
            this.btnWindowClose.TabIndex = 4;
            // 
            // pnlSubBar
            // 
            this.pnlSubBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlSubBar.Controls.Add(this.flowSubNav);
            this.pnlSubBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSubBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlSubBar.Location = new System.Drawing.Point(0, 44);
            this.pnlSubBar.Name = "pnlSubBar";
            this.pnlSubBar.Size = new System.Drawing.Size(1157, 36);
            this.pnlSubBar.TabIndex = 1;
            // 
            // flowSubNav
            // 
            this.flowSubNav.AutoScroll = true;
            this.flowSubNav.BackColor = System.Drawing.Color.Transparent;
            this.flowSubNav.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowSubNav.Location = new System.Drawing.Point(0, 0);
            this.flowSubNav.Name = "flowSubNav";
            this.flowSubNav.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.flowSubNav.Size = new System.Drawing.Size(1157, 36);
            this.flowSubNav.TabIndex = 0;
            this.flowSubNav.WrapContents = false;
            // 
            // pnlDesktop
            // 
            this.pnlDesktop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlDesktop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDesktop.Location = new System.Drawing.Point(0, 80);
            this.pnlDesktop.Name = "pnlDesktop";
            this.pnlDesktop.Size = new System.Drawing.Size(1157, 740);
            this.pnlDesktop.TabIndex = 2;
            // 
            // pnlStatusBar
            // 
            this.pnlStatusBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlStatusBar.Controls.Add(this.lblStatus);
            this.pnlStatusBar.Controls.Add(this.lblClock);
            this.pnlStatusBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatusBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlStatusBar.Location = new System.Drawing.Point(0, 820);
            this.pnlStatusBar.Name = "pnlStatusBar";
            this.pnlStatusBar.Size = new System.Drawing.Size(1157, 22);
            this.pnlStatusBar.TabIndex = 3;
            // 
            // lblStatus
            // 
            this.lblStatus.BackColor = System.Drawing.Color.Transparent;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblStatus.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStatus.Location = new System.Drawing.Point(0, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Padding = new System.Windows.Forms.Padding(12, 0, 0, 0);
            this.lblStatus.Size = new System.Drawing.Size(250, 22);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblClock
            // 
            this.lblClock.BackColor = System.Drawing.Color.Transparent;
            this.lblClock.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblClock.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.lblClock.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblClock.Location = new System.Drawing.Point(1057, 0);
            this.lblClock.Name = "lblClock";
            this.lblClock.Padding = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.lblClock.Size = new System.Drawing.Size(100, 22);
            this.lblClock.TabIndex = 1;
            this.lblClock.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(1157, 842);
            this.Controls.Add(this.pnlDesktop);
            this.Controls.Add(this.pnlStatusBar);
            this.Controls.Add(this.pnlSubBar);
            this.Controls.Add(this.pnlMainBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hệ thống quản lý khu vui chơi TDC";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.pnlMainBar.ResumeLayout(false);
            this.pnlMainBar.PerformLayout();
            this.flowMainNav.ResumeLayout(false);
            this.pnlLogoArea.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            this.pnlUserArea.ResumeLayout(false);
            this.pnlSubBar.ResumeLayout(false);
            this.pnlStatusBar.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlMainBar;
        private System.Windows.Forms.FlowLayoutPanel flowMainNav;
        private System.Windows.Forms.Panel pnlLogoArea;
        private System.Windows.Forms.PictureBox picLogo;
        private Guna.UI2.WinForms.Guna2Button btnCat1;
        private Guna.UI2.WinForms.Guna2Button btnCat2;
        private Guna.UI2.WinForms.Guna2Button btnCat3;
        private Guna.UI2.WinForms.Guna2Button btnCat4;
        private Guna.UI2.WinForms.Guna2Button btnCat5;
        private System.Windows.Forms.Panel pnlUserArea;
        private System.Windows.Forms.Label lblUserName;
        private Guna.UI2.WinForms.Guna2Button btnSettings;
        private Guna.UI2.WinForms.Guna2Button btnWindowMin;
        private Guna.UI2.WinForms.Guna2Button btnWindowMax;
        private Guna.UI2.WinForms.Guna2Button btnWindowClose;
        private Guna.UI2.WinForms.Guna2Panel pnlSubBar;
        private System.Windows.Forms.FlowLayoutPanel flowSubNav;
        private System.Windows.Forms.Panel pnlDesktop;
        private Guna.UI2.WinForms.Guna2Panel pnlStatusBar;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblClock;
    }
}
