namespace GUI.Modules.BanHang
{
    partial class frmPhatMatDo
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblMoTa = new DevExpress.XtraEditors.LabelControl();
            this.spinTienPhat = new DevExpress.XtraEditors.SpinEdit();
            this.btnDongY = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.spinTienPhat.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.Vertical;
            this.lblMoTa.Location = new System.Drawing.Point(15, 15);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(300, 0);
            this.lblMoTa.TabIndex = 0;
            // 
            // spinTienPhat
            // 
            this.spinTienPhat.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spinTienPhat.Location = new System.Drawing.Point(15, 60);
            this.spinTienPhat.Name = "spinTienPhat";
            this.spinTienPhat.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spinTienPhat.Properties.DisplayFormat.FormatString = "#,##0";
            this.spinTienPhat.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.spinTienPhat.Properties.IsFloatValue = false;
            this.spinTienPhat.Properties.MaskSettings.Set("mask", "N00");
            this.spinTienPhat.Properties.MaxValue = new decimal(new int[] {
            999999999,
            0,
            0,
            0});
            this.spinTienPhat.Size = new System.Drawing.Size(200, 20);
            this.spinTienPhat.TabIndex = 1;
            // 
            // btnDongY
            // 
            this.btnDongY.Location = new System.Drawing.Point(15, 100);
            this.btnDongY.Name = "btnDongY";
            this.btnDongY.Size = new System.Drawing.Size(90, 30);
            this.btnDongY.TabIndex = 2;
            this.btnDongY.Text = "Đồng ý";
            this.btnDongY.Click += new System.EventHandler(this.BtnDongY_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnHuy.Location = new System.Drawing.Point(115, 100);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(90, 30);
            this.btnHuy.TabIndex = 3;
            this.btnHuy.Text = "Hủy";
            // 
            // frmPhatMatDo
            // 
            this.AcceptButton = this.btnDongY;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(258, 145);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnDongY);
            this.Controls.Add(this.spinTienPhat);
            this.Controls.Add(this.lblMoTa);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPhatMatDo";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Nhập tiền phạt";
            ((System.ComponentModel.ISupportInitialize)(this.spinTienPhat.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.LabelControl lblMoTa;
        private DevExpress.XtraEditors.SpinEdit spinTienPhat;
        private DevExpress.XtraEditors.SimpleButton btnDongY;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
    }
}
