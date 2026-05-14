namespace GUI.Modules.BanHang
{
    partial class ucVeDienTu
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraPrinting.BarCode.QRCodeGenerator qrCodeGenerator1 = new DevExpress.XtraPrinting.BarCode.QRCodeGenerator();
            this.grpCard = new DevExpress.XtraEditors.GroupControl();
            this.qrCode = new DevExpress.XtraEditors.BarCodeControl();
            this.lblMa = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.grpCard)).BeginInit();
            this.grpCard.SuspendLayout();
            this.SuspendLayout();
            // 
            // grpCard
            // 
            this.grpCard.AppearanceCaption.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.grpCard.AppearanceCaption.ForeColor = System.Drawing.Color.DodgerBlue;
            this.grpCard.AppearanceCaption.Options.UseFont = true;
            this.grpCard.AppearanceCaption.Options.UseForeColor = true;
            this.grpCard.Controls.Add(this.qrCode);
            this.grpCard.Controls.Add(this.lblMa);
            this.grpCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpCard.Location = new System.Drawing.Point(0, 0);
            this.grpCard.Name = "grpCard";
            this.grpCard.Size = new System.Drawing.Size(340, 340);
            this.grpCard.TabIndex = 0;
            this.grpCard.Text = "Tên Vé";
            // 
            // qrCode
            // 
            qrCodeGenerator1.CompactionMode = DevExpress.XtraPrinting.BarCode.QRCodeCompactionMode.Byte;
            qrCodeGenerator1.ErrorCorrectionLevel = DevExpress.XtraPrinting.BarCode.QRCodeErrorCorrectionLevel.H;
            qrCodeGenerator1.Version = DevExpress.XtraPrinting.BarCode.QRCodeVersion.AutoVersion;
            this.qrCode.AutoModule = true;
            this.qrCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.qrCode.HorizontalAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.qrCode.HorizontalTextAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.qrCode.Location = new System.Drawing.Point(2, 28);
            this.qrCode.Name = "qrCode";
            this.qrCode.Padding = new System.Windows.Forms.Padding(20);
            this.qrCode.ShowText = false;
            this.qrCode.Size = new System.Drawing.Size(336, 270);
            this.qrCode.Symbology = qrCodeGenerator1;
            this.qrCode.TabIndex = 0;
            this.qrCode.VerticalAlignment = DevExpress.Utils.VertAlignment.Center;
            // 
            // lblMa
            // 
            this.lblMa.Appearance.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            this.lblMa.Appearance.Options.UseFont = true;
            this.lblMa.Appearance.Options.UseTextOptions = true;
            this.lblMa.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblMa.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMa.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblMa.Location = new System.Drawing.Point(2, 298);
            this.lblMa.Name = "lblMa";
            this.lblMa.Size = new System.Drawing.Size(336, 40);
            this.lblMa.TabIndex = 1;
            this.lblMa.Text = "MAVACH";
            // 
            // ucVeDienTu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grpCard);
            this.Margin = new System.Windows.Forms.Padding(10, 10, 10, 20);
            this.Name = "ucVeDienTu";
            this.Size = new System.Drawing.Size(340, 340);
            ((System.ComponentModel.ISupportInitialize)(this.grpCard)).EndInit();
            this.grpCard.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.GroupControl grpCard;
        private DevExpress.XtraEditors.BarCodeControl qrCode;
        private DevExpress.XtraEditors.LabelControl lblMa;
    }
}
