namespace GUI.Modules.Auth
{
    partial class frmLogin
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



        #region Windows Form Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMain = new DevExpress.XtraEditors.PanelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblSubtitle = new DevExpress.XtraEditors.LabelControl();
            this.lblUsername = new DevExpress.XtraEditors.LabelControl();
            this.txtUsername = new DevExpress.XtraEditors.TextEdit();
            this.lblPassword = new DevExpress.XtraEditors.LabelControl();
            this.txtPassword = new DevExpress.XtraEditors.ButtonEdit();
            this.lblLanguage = new DevExpress.XtraEditors.LabelControl();
            this.cboLanguage = new DevExpress.XtraEditors.ComboBoxEdit();
            this.chkGhiNho = new DevExpress.XtraEditors.CheckEdit();
            this.btnLogin = new DevExpress.XtraEditors.SimpleButton();
            this.btnExit = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLanguage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGhiNho.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Controls.Add(this.lblSubtitle);
            this.pnlMain.Controls.Add(this.lblUsername);
            this.pnlMain.Controls.Add(this.txtUsername);
            this.pnlMain.Controls.Add(this.lblPassword);
            this.pnlMain.Controls.Add(this.txtPassword);
            this.pnlMain.Controls.Add(this.lblLanguage);
            this.pnlMain.Controls.Add(this.cboLanguage);
            this.pnlMain.Controls.Add(this.chkGhiNho);
            this.pnlMain.Controls.Add(this.btnLogin);
            this.pnlMain.Controls.Add(this.btnExit);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.pnlMain.Size = new System.Drawing.Size(380, 420);
            this.pnlMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Location = new System.Drawing.Point(30, 25);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(182, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "KHU DU LỊCH ĐẠI NAM";
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSubtitle.Appearance.Options.UseFont = true;
            this.lblSubtitle.Location = new System.Drawing.Point(30, 60);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(175, 17);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "Hệ thống quản lý khu du lịch";
            // 
            // lblUsername
            // 
            this.lblUsername.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblUsername.Appearance.Options.UseFont = true;
            this.lblUsername.Location = new System.Drawing.Point(30, 100);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(81, 15);
            this.lblUsername.TabIndex = 2;
            this.lblUsername.Text = "Tên đăng nhập";
            // 
            // txtUsername
            // 
            this.txtUsername.Location = new System.Drawing.Point(30, 120);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtUsername.Properties.Appearance.Options.UseFont = true;
            this.txtUsername.Size = new System.Drawing.Size(320, 24);
            this.txtUsername.TabIndex = 3;
            // 
            // lblPassword
            // 
            this.lblPassword.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPassword.Appearance.Options.UseFont = true;
            this.lblPassword.Location = new System.Drawing.Point(30, 160);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(49, 15);
            this.lblPassword.TabIndex = 4;
            this.lblPassword.Text = "Mật khẩu";
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(30, 180);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtPassword.Properties.Appearance.Options.UseFont = true;
            this.txtPassword.Properties.PasswordChar = '*';
            this.txtPassword.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Ellipsis)});
            this.txtPassword.Size = new System.Drawing.Size(320, 24);
            this.txtPassword.TabIndex = 5;
            this.txtPassword.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.TxtPassword_ButtonClick);
            // 
            // lblLanguage
            // 
            this.lblLanguage.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblLanguage.Appearance.Options.UseFont = true;
            this.lblLanguage.Location = new System.Drawing.Point(30, 220);
            this.lblLanguage.Name = "lblLanguage";
            this.lblLanguage.Size = new System.Drawing.Size(53, 15);
            this.lblLanguage.TabIndex = 6;
            this.lblLanguage.Text = "Ngôn ngữ";
            // 
            // cboLanguage
            // 
            this.cboLanguage.Location = new System.Drawing.Point(30, 240);
            this.cboLanguage.Name = "cboLanguage";
            this.cboLanguage.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLanguage.Properties.Appearance.Options.UseFont = true;
            this.cboLanguage.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboLanguage.Size = new System.Drawing.Size(320, 24);
            this.cboLanguage.TabIndex = 7;
            this.cboLanguage.SelectedIndexChanged += new System.EventHandler(this.CboLanguage_SelectedIndexChanged);
            // 
            // chkGhiNho
            // 
            this.chkGhiNho.Location = new System.Drawing.Point(30, 280);
            this.chkGhiNho.Name = "chkGhiNho";
            this.chkGhiNho.Properties.Caption = "Nhớ tên đăng nhập";
            this.chkGhiNho.Size = new System.Drawing.Size(320, 20);
            this.chkGhiNho.TabIndex = 8;
            // 
            // btnLogin
            // 
            this.btnLogin.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLogin.Appearance.Options.UseFont = true;
            this.btnLogin.Location = new System.Drawing.Point(30, 320);
            this.btnLogin.Name = "btnLogin";
            this.btnLogin.Size = new System.Drawing.Size(320, 40);
            this.btnLogin.TabIndex = 9;
            this.btnLogin.Text = "Đăng nhập";
            this.btnLogin.Click += new System.EventHandler(this.BtnLogin_Click);
            // 
            // btnExit
            // 
            this.btnExit.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnExit.Appearance.Options.UseFont = true;
            this.btnExit.Location = new System.Drawing.Point(140, 370);
            this.btnExit.Name = "btnExit";
            this.btnExit.Size = new System.Drawing.Size(100, 30);
            this.btnExit.TabIndex = 10;
            this.btnExit.Text = "Thoát";
            this.btnExit.Click += new System.EventHandler(this.BtnExit_Click);
            // 
            // frmLogin
            // 
            this.AcceptButton = this.btnLogin;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(380, 420);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Đăng nhập hệ thống";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtUsername.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPassword.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboLanguage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGhiNho.Properties)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblSubtitle;
        private DevExpress.XtraEditors.LabelControl lblUsername;
        private DevExpress.XtraEditors.TextEdit txtUsername;
        private DevExpress.XtraEditors.LabelControl lblPassword;
        private DevExpress.XtraEditors.ButtonEdit txtPassword;
        private DevExpress.XtraEditors.LabelControl lblLanguage;
        private DevExpress.XtraEditors.ComboBoxEdit cboLanguage;
        private DevExpress.XtraEditors.CheckEdit chkGhiNho;
        private DevExpress.XtraEditors.SimpleButton btnLogin;
        private DevExpress.XtraEditors.SimpleButton btnExit;
        private System.ComponentModel.IContainer components = null;





    }}