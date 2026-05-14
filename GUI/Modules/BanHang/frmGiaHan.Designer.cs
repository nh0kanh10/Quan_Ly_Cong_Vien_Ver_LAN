namespace GUI.Modules.BanHang
{
    partial class frmGiaHan
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

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dtpNgayMoi = new DevExpress.XtraEditors.DateEdit();
            this.btnXacNhan = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutNgayMoi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutBtnXacNhan = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutBtnHuy = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayMoi.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayMoi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutNgayMoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBtnXacNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBtnHuy)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.dtpNgayMoi);
            this.layoutControl1.Controls.Add(this.btnXacNhan);
            this.layoutControl1.Controls.Add(this.btnHuy);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(380, 140);
            this.layoutControl1.TabIndex = 0;
            // 
            // dtpNgayMoi
            // 
            this.dtpNgayMoi.EditValue = null;
            this.dtpNgayMoi.Location = new System.Drawing.Point(112, 12);
            this.dtpNgayMoi.Name = "dtpNgayMoi";
            this.dtpNgayMoi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayMoi.Size = new System.Drawing.Size(256, 22);
            this.dtpNgayMoi.StyleController = this.layoutControl1;
            this.dtpNgayMoi.TabIndex = 4;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Location = new System.Drawing.Point(12, 38);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(174, 26);
            this.btnXacNhan.StyleController = this.layoutControl1;
            this.btnXacNhan.TabIndex = 5;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.Click += new System.EventHandler(this.BtnXacNhan_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(190, 38);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(178, 26);
            this.btnHuy.StyleController = this.layoutControl1;
            this.btnHuy.TabIndex = 6;
            this.btnHuy.Text = "Hủy bỏ";
            this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutNgayMoi,
            this.layoutBtnXacNhan,
            this.layoutBtnHuy});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(380, 140);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutNgayMoi
            // 
            this.layoutNgayMoi.Control = this.dtpNgayMoi;
            this.layoutNgayMoi.Location = new System.Drawing.Point(0, 0);
            this.layoutNgayMoi.Name = "layoutNgayMoi";
            this.layoutNgayMoi.Size = new System.Drawing.Size(360, 26);
            this.layoutNgayMoi.Text = "Ngày trả mới";
            this.layoutNgayMoi.TextSize = new System.Drawing.Size(90, 16);
            // 
            // layoutBtnXacNhan
            // 
            this.layoutBtnXacNhan.Control = this.btnXacNhan;
            this.layoutBtnXacNhan.Location = new System.Drawing.Point(0, 26);
            this.layoutBtnXacNhan.Name = "layoutBtnXacNhan";
            this.layoutBtnXacNhan.Size = new System.Drawing.Size(178, 30);
            this.layoutBtnXacNhan.TextSize = new System.Drawing.Size(0, 0);
            this.layoutBtnXacNhan.TextVisible = false;
            // 
            // layoutBtnHuy
            // 
            this.layoutBtnHuy.Control = this.btnHuy;
            this.layoutBtnHuy.Location = new System.Drawing.Point(178, 26);
            this.layoutBtnHuy.Name = "layoutBtnHuy";
            this.layoutBtnHuy.Size = new System.Drawing.Size(182, 30);
            this.layoutBtnHuy.TextSize = new System.Drawing.Size(0, 0);
            this.layoutBtnHuy.TextVisible = false;
            // 
            // frmGiaHan
            // 
            this.AcceptButton = this.btnXacNhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(380, 140);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGiaHan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Gia Hạn Phòng";
            this.Load += new System.EventHandler(this.frmGiaHan_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayMoi.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayMoi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutNgayMoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBtnXacNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBtnHuy)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.DateEdit dtpNgayMoi;
        private DevExpress.XtraEditors.SimpleButton btnXacNhan;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutNgayMoi;
        private DevExpress.XtraLayout.LayoutControlItem layoutBtnXacNhan;
        private DevExpress.XtraLayout.LayoutControlItem layoutBtnHuy;
    }
}
