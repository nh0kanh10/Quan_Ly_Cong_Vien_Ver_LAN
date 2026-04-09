namespace GUI
{
    partial class frmConfigConnect
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
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pnlContent = new Guna.UI2.WinForms.Guna2Panel();
            this.lblServer = new System.Windows.Forms.Label();
            this.cboServer = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnRefreshServer = new Guna.UI2.WinForms.Guna2Button();
            this.lblDatabase = new System.Windows.Forms.Label();
            this.cboDatabase = new Guna.UI2.WinForms.Guna2ComboBox();
            this.chkWindowsAuth = new Guna.UI2.WinForms.Guna2CheckBox();
            this.lblUser = new System.Windows.Forms.Label();
            this.txtUser = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblPassword = new System.Windows.Forms.Label();
            this.txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlButtonsClient = new Guna.UI2.WinForms.Guna2Panel();
            this.btnTest = new Guna.UI2.WinForms.Guna2Button();
            this.btnSave = new Guna.UI2.WinForms.Guna2Button();
            this.lblLoading = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            this.pnlContent.SuspendLayout();
            this.pnlButtonsClient.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.pnlHeader.Controls.Add(this.lblHeader);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(501, 60);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblHeader
            // 
            this.lblHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblHeader.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(0, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(501, 60);
            this.lblHeader.TabIndex = 0;
            this.lblHeader.Text = "CẤU HÌNH KẾT NỐI SERVER";
            this.lblHeader.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlContent
            // 
            this.pnlContent.Controls.Add(this.lblServer);
            this.pnlContent.Controls.Add(this.cboServer);
            this.pnlContent.Controls.Add(this.btnRefreshServer);
            this.pnlContent.Controls.Add(this.lblDatabase);
            this.pnlContent.Controls.Add(this.cboDatabase);
            this.pnlContent.Controls.Add(this.chkWindowsAuth);
            this.pnlContent.Controls.Add(this.lblUser);
            this.pnlContent.Controls.Add(this.txtUser);
            this.pnlContent.Controls.Add(this.lblPassword);
            this.pnlContent.Controls.Add(this.txtPassword);
            this.pnlContent.Controls.Add(this.pnlButtonsClient);
            this.pnlContent.Controls.Add(this.lblLoading);
            this.pnlContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlContent.Location = new System.Drawing.Point(0, 60);
            this.pnlContent.Name = "pnlContent";
            this.pnlContent.Padding = new System.Windows.Forms.Padding(30);
            this.pnlContent.Size = new System.Drawing.Size(501, 434);
            this.pnlContent.TabIndex = 1;
            // 
            // lblServer
            // 
            this.lblServer.AutoSize = true;
            this.lblServer.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblServer.ForeColor = System.Drawing.Color.DimGray;
            this.lblServer.Location = new System.Drawing.Point(30, 20);
            this.lblServer.Name = "lblServer";
            this.lblServer.Size = new System.Drawing.Size(84, 15);
            this.lblServer.TabIndex = 0;
            this.lblServer.Text = "Server Name:";
            // 
            // cboServer
            // 
            this.cboServer.BackColor = System.Drawing.Color.Transparent;
            this.cboServer.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboServer.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboServer.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.cboServer.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.cboServer.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboServer.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboServer.ItemHeight = 22;
            this.cboServer.Location = new System.Drawing.Point(33, 39);
            this.cboServer.Name = "cboServer";
            this.cboServer.Size = new System.Drawing.Size(390, 28);
            this.cboServer.TabIndex = 1;
            this.cboServer.SelectedIndexChanged += new System.EventHandler(this.cboServer_SelectedIndexChanged);
            // 
            // btnRefreshServer
            // 
            this.btnRefreshServer.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnRefreshServer.BorderRadius = 5;
            this.btnRefreshServer.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnRefreshServer.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnRefreshServer.ForeColor = System.Drawing.Color.White;
            this.btnRefreshServer.Location = new System.Drawing.Point(430, 39);
            this.btnRefreshServer.Name = "btnRefreshServer";
            this.btnRefreshServer.Size = new System.Drawing.Size(37, 28);
            this.btnRefreshServer.TabIndex = 10;
            this.btnRefreshServer.Click += new System.EventHandler(this.btnRefreshServer_Click);
            // 
            // lblDatabase
            // 
            this.lblDatabase.AutoSize = true;
            this.lblDatabase.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblDatabase.ForeColor = System.Drawing.Color.DimGray;
            this.lblDatabase.Location = new System.Drawing.Point(30, 79);
            this.lblDatabase.Name = "lblDatabase";
            this.lblDatabase.Size = new System.Drawing.Size(61, 15);
            this.lblDatabase.TabIndex = 2;
            this.lblDatabase.Text = "Database:";
            // 
            // cboDatabase
            // 
            this.cboDatabase.BackColor = System.Drawing.Color.Transparent;
            this.cboDatabase.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDatabase.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDatabase.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.cboDatabase.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.cboDatabase.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.cboDatabase.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboDatabase.ItemHeight = 22;
            this.cboDatabase.Location = new System.Drawing.Point(33, 98);
            this.cboDatabase.Name = "cboDatabase";
            this.cboDatabase.Size = new System.Drawing.Size(434, 28);
            this.cboDatabase.TabIndex = 3;
            // 
            // chkWindowsAuth
            // 
            this.chkWindowsAuth.AutoSize = true;
            this.chkWindowsAuth.Checked = true;
            this.chkWindowsAuth.CheckedState.BorderRadius = 0;
            this.chkWindowsAuth.CheckedState.BorderThickness = 0;
            this.chkWindowsAuth.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkWindowsAuth.Cursor = System.Windows.Forms.Cursors.Hand;
            this.chkWindowsAuth.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.chkWindowsAuth.Location = new System.Drawing.Point(33, 139);
            this.chkWindowsAuth.Name = "chkWindowsAuth";
            this.chkWindowsAuth.Size = new System.Drawing.Size(200, 23);
            this.chkWindowsAuth.TabIndex = 4;
            this.chkWindowsAuth.Text = "Sử dụng tài khoản Windows";
            this.chkWindowsAuth.UncheckedState.BorderRadius = 0;
            this.chkWindowsAuth.UncheckedState.BorderThickness = 0;
            this.chkWindowsAuth.UseVisualStyleBackColor = true;
            this.chkWindowsAuth.CheckedChanged += new System.EventHandler(this.chkWindowsAuth_CheckedChanged);
            // 
            // lblUser
            // 
            this.lblUser.AutoSize = true;
            this.lblUser.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblUser.ForeColor = System.Drawing.Color.DimGray;
            this.lblUser.Location = new System.Drawing.Point(30, 169);
            this.lblUser.Name = "lblUser";
            this.lblUser.Size = new System.Drawing.Size(62, 15);
            this.lblUser.TabIndex = 5;
            this.lblUser.Text = "Tài khoản:";
            // 
            // txtUser
            // 
            this.txtUser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.txtUser.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUser.DefaultText = "";
            this.txtUser.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUser.Location = new System.Drawing.Point(33, 191);
            this.txtUser.Name = "txtUser";
            this.txtUser.Padding = new System.Windows.Forms.Padding(7);
            this.txtUser.PlaceholderText = "";
            this.txtUser.ReadOnly = true;
            this.txtUser.SelectedText = "";
            this.txtUser.Size = new System.Drawing.Size(434, 34);
            this.txtUser.TabIndex = 6;
            // 
            // lblPassword
            // 
            this.lblPassword.AutoSize = true;
            this.lblPassword.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblPassword.ForeColor = System.Drawing.Color.DimGray;
            this.lblPassword.Location = new System.Drawing.Point(30, 235);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(62, 15);
            this.lblPassword.TabIndex = 7;
            this.lblPassword.Text = "Mật khẩu:";
            // 
            // txtPassword
            // 
            this.txtPassword.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(244)))), ((int)(((byte)(246)))));
            this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassword.DefaultText = "";
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPassword.Location = new System.Drawing.Point(33, 255);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Padding = new System.Windows.Forms.Padding(7);
            this.txtPassword.PlaceholderText = "";
            this.txtPassword.ReadOnly = true;
            this.txtPassword.SelectedText = "";
            this.txtPassword.Size = new System.Drawing.Size(434, 34);
            this.txtPassword.TabIndex = 8;
            this.txtPassword.UseSystemPasswordChar = true;
            // 
            // pnlButtonsClient
            // 
            this.pnlButtonsClient.Controls.Add(this.btnTest);
            this.pnlButtonsClient.Controls.Add(this.btnSave);
            this.pnlButtonsClient.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtonsClient.Location = new System.Drawing.Point(30, 354);
            this.pnlButtonsClient.Name = "pnlButtonsClient";
            this.pnlButtonsClient.Size = new System.Drawing.Size(441, 50);
            this.pnlButtonsClient.TabIndex = 9;
            // 
            // btnTest
            // 
            this.btnTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnTest.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnTest.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnTest.Location = new System.Drawing.Point(0, 5);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(200, 40);
            this.btnTest.TabIndex = 0;
            this.btnTest.Text = "Kiểm tra kết nối";
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // btnSave
            // 
            this.btnSave.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnSave.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.btnSave.Location = new System.Drawing.Point(240, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(200, 40);
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "Lưu cấu hình";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lblLoading
            // 
            this.lblLoading.AutoSize = true;
            this.lblLoading.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblLoading.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(136)))));
            this.lblLoading.Location = new System.Drawing.Point(30, 305);
            this.lblLoading.Name = "lblLoading";
            this.lblLoading.Size = new System.Drawing.Size(0, 13);
            this.lblLoading.TabIndex = 11;
            this.lblLoading.Visible = false;
            // 
            // frmConfigConnect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(501, 494);
            this.Controls.Add(this.pnlContent);
            this.Controls.Add(this.pnlHeader);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmConfigConnect";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Môi trường hệ thống";
            this.Load += new System.EventHandler(this.frmConfigConnect_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlContent.ResumeLayout(false);
            this.pnlContent.PerformLayout();
            this.pnlButtonsClient.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private System.Windows.Forms.Label lblHeader;
        private Guna.UI2.WinForms.Guna2Panel pnlContent;
        private System.Windows.Forms.Label lblServer;
        private Guna.UI2.WinForms.Guna2ComboBox cboServer;
        private Guna.UI2.WinForms.Guna2Button btnRefreshServer;
        private System.Windows.Forms.Label lblDatabase;
        private Guna.UI2.WinForms.Guna2ComboBox cboDatabase;
        private Guna.UI2.WinForms.Guna2CheckBox chkWindowsAuth;
        private System.Windows.Forms.Label lblUser;
        private Guna.UI2.WinForms.Guna2TextBox txtUser;
        private System.Windows.Forms.Label lblPassword;
        private Guna.UI2.WinForms.Guna2TextBox txtPassword;
        private Guna.UI2.WinForms.Guna2Panel pnlButtonsClient;
        private Guna.UI2.WinForms.Guna2Button btnTest;
        private Guna.UI2.WinForms.Guna2Button btnSave;
        private System.Windows.Forms.Label lblLoading;
    }
}
