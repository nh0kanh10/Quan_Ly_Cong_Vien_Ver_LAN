namespace GUI.Modules.TaiChinh
{
    partial class frmNapViRFID
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
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txtMaThe = new DevExpress.XtraEditors.TextEdit();
            this.btnQuet = new DevExpress.XtraEditors.SimpleButton();
            this.lblSoDu = new DevExpress.XtraEditors.LabelControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.spinSoTien = new DevExpress.XtraEditors.SpinEdit();
            this.labelControl3 = new DevExpress.XtraEditors.LabelControl();
            this.cboPhuongThuc = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnNapTien = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaThe.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoTien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPhuongThuc.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // labelControl1
            // 
            this.labelControl1.Location = new System.Drawing.Point(21, 23);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(64, 13);
            this.labelControl1.TabIndex = 0;
            this.labelControl1.Text = "Mã Thẻ RFID:";
            // 
            // txtMaThe
            // 
            this.txtMaThe.Location = new System.Drawing.Point(111, 20);
            this.txtMaThe.Name = "txtMaThe";
            this.txtMaThe.Size = new System.Drawing.Size(183, 20);
            this.txtMaThe.TabIndex = 1;
            // 
            // btnQuet
            // 
            this.btnQuet.Location = new System.Drawing.Point(300, 18);
            this.btnQuet.Name = "btnQuet";
            this.btnQuet.Size = new System.Drawing.Size(75, 23);
            this.btnQuet.TabIndex = 2;
            this.btnQuet.Text = "Quét / Tìm";
            this.btnQuet.Click += new System.EventHandler(this.btnQuet_Click);
            // 
            // lblSoDu
            // 
            this.lblSoDu.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblSoDu.Appearance.Options.UseFont = true;
            this.lblSoDu.Location = new System.Drawing.Point(111, 56);
            this.lblSoDu.Name = "lblSoDu";
            this.lblSoDu.Size = new System.Drawing.Size(0, 14);
            this.lblSoDu.TabIndex = 3;
            // 
            // labelControl2
            // 
            this.labelControl2.Location = new System.Drawing.Point(21, 91);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(61, 13);
            this.labelControl2.TabIndex = 4;
            this.labelControl2.Text = "Số Tiền Nạp:";
            // 
            // spinSoTien
            // 
            this.spinSoTien.EditValue = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            this.spinSoTien.Location = new System.Drawing.Point(111, 88);
            this.spinSoTien.Name = "spinSoTien";
            this.spinSoTien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinSoTien.Properties.DisplayFormat.FormatString = "N0";
            this.spinSoTien.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinSoTien.Size = new System.Drawing.Size(183, 20);
            this.spinSoTien.TabIndex = 5;
            // 
            // labelControl3
            // 
            this.labelControl3.Location = new System.Drawing.Point(21, 126);
            this.labelControl3.Name = "labelControl3";
            this.labelControl3.Size = new System.Drawing.Size(64, 13);
            this.labelControl3.TabIndex = 6;
            this.labelControl3.Text = "Phương thức:";
            // 
            // cboPhuongThuc
            // 
            this.cboPhuongThuc.Location = new System.Drawing.Point(111, 123);
            this.cboPhuongThuc.Name = "cboPhuongThuc";
            this.cboPhuongThuc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboPhuongThuc.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboPhuongThuc.Size = new System.Drawing.Size(183, 20);
            this.cboPhuongThuc.TabIndex = 7;
            // 
            // btnNapTien
            // 
            this.btnNapTien.Enabled = false;
            this.btnNapTien.Location = new System.Drawing.Point(111, 166);
            this.btnNapTien.Name = "btnNapTien";
            this.btnNapTien.Size = new System.Drawing.Size(183, 35);
            this.btnNapTien.TabIndex = 8;
            this.btnNapTien.Text = "XÁC NHẬN NẠP TIỀN";
            this.btnNapTien.Click += new System.EventHandler(this.btnNapTien_Click);
            // 
            // frmNapViRFID
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(394, 221);
            this.Controls.Add(this.btnNapTien);
            this.Controls.Add(this.cboPhuongThuc);
            this.Controls.Add(this.labelControl3);
            this.Controls.Add(this.spinSoTien);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.lblSoDu);
            this.Controls.Add(this.btnQuet);
            this.Controls.Add(this.txtMaThe);
            this.Controls.Add(this.labelControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmNapViRFID";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nạp Ví RFID";
            ((System.ComponentModel.ISupportInitialize)(this.txtMaThe.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spinSoTien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboPhuongThuc.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.TextEdit txtMaThe;
        private DevExpress.XtraEditors.SimpleButton btnQuet;
        private DevExpress.XtraEditors.LabelControl lblSoDu;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.SpinEdit spinSoTien;
        private DevExpress.XtraEditors.LabelControl labelControl3;
        private DevExpress.XtraEditors.ComboBoxEdit cboPhuongThuc;
        private DevExpress.XtraEditors.SimpleButton btnNapTien;
    }
}
