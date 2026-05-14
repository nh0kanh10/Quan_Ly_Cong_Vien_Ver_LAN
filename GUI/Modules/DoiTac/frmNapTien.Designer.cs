namespace GUI.Modules.DoiTac
{
    partial class frmNapTien
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
            this.lblSoTien = new DevExpress.XtraEditors.LabelControl();
            this.numSoTien = new DevExpress.XtraEditors.SpinEdit();
            this.lblPhuongThuc = new DevExpress.XtraEditors.LabelControl();
            this.cboPhuongThuc = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnXacNhan = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            this.pnlMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoTien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPhuongThuc.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnHuy);
            this.pnlMain.Controls.Add(this.btnXacNhan);
            this.pnlMain.Controls.Add(this.cboPhuongThuc);
            this.pnlMain.Controls.Add(this.lblPhuongThuc);
            this.pnlMain.Controls.Add(this.numSoTien);
            this.pnlMain.Controls.Add(this.lblSoTien);
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20, 16, 20, 16);
            this.pnlMain.Size = new System.Drawing.Size(404, 241);
            this.pnlMain.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 12F);
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Horizontal;
            this.lblTitle.Location = new System.Drawing.Point(20, 16);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(188, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "NẠP TIỀN VÍ ĐIỆN TỬ";
            // 
            // lblSoTien
            // 
            this.lblSoTien.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoTien.Appearance.Options.UseFont = true;
            this.lblSoTien.Location = new System.Drawing.Point(20, 52);
            this.lblSoTien.Name = "lblSoTien";
            this.lblSoTien.Size = new System.Drawing.Size(82, 15);
            this.lblSoTien.TabIndex = 1;
            this.lblSoTien.Text = "Số tiền nạp (*)";
            // 
            // numSoTien
            // 
            this.numSoTien.EditValue = new decimal(100000);
            this.numSoTien.Location = new System.Drawing.Point(20, 72);
            this.numSoTien.Name = "numSoTien";
            this.numSoTien.Properties.DisplayFormat.FormatString = "N0";
            this.numSoTien.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numSoTien.Properties.EditFormat.FormatString = "N0";
            this.numSoTien.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.numSoTien.Properties.Increment = new decimal(50000);
            this.numSoTien.Properties.IsFloatValue = false;
            this.numSoTien.Properties.Mask.EditMask = "N0";
            this.numSoTien.Properties.Mask.UseMaskAsDisplayFormat = true;
            this.numSoTien.Properties.MaxValue = new decimal(100000000);
            this.numSoTien.Properties.MinValue = new decimal(1000);
            this.numSoTien.Size = new System.Drawing.Size(360, 20);
            this.numSoTien.TabIndex = 2;
            // 
            // lblPhuongThuc
            // 
            this.lblPhuongThuc.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPhuongThuc.Appearance.Options.UseFont = true;
            this.lblPhuongThuc.Location = new System.Drawing.Point(20, 106);
            this.lblPhuongThuc.Name = "lblPhuongThuc";
            this.lblPhuongThuc.Size = new System.Drawing.Size(140, 15);
            this.lblPhuongThuc.TabIndex = 3;
            this.lblPhuongThuc.Text = "Hình thức thanh toán (*)";
            // 
            // cboPhuongThuc
            // 
            this.cboPhuongThuc.Location = new System.Drawing.Point(20, 126);
            this.cboPhuongThuc.Name = "cboPhuongThuc";
            this.cboPhuongThuc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboPhuongThuc.Size = new System.Drawing.Size(360, 20);
            this.cboPhuongThuc.TabIndex = 4;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Location = new System.Drawing.Point(20, 170);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(175, 38);
            this.btnXacNhan.TabIndex = 5;
            this.btnXacNhan.Text = "  Xác nhận nạp tiền";
            this.btnXacNhan.Click += new System.EventHandler(this.BtnXacNhan_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(205, 170);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(100, 38);
            this.btnHuy.TabIndex = 6;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            // 
            // frmNapTien
            // 
            this.AcceptButton = this.btnXacNhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(404, 241);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNapTien";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nạp tiền ví";
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numSoTien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPhuongThuc.Properties)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion
        private DevExpress.XtraEditors.PanelControl pnlMain;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblSoTien;
        private DevExpress.XtraEditors.SpinEdit numSoTien;
        private DevExpress.XtraEditors.LabelControl lblPhuongThuc;
        private DevExpress.XtraEditors.ComboBoxEdit cboPhuongThuc;
        private DevExpress.XtraEditors.SimpleButton btnXacNhan;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private System.ComponentModel.IContainer components = null;





    }}