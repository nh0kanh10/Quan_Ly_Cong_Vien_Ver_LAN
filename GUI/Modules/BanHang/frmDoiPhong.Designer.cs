namespace GUI.Modules.BanHang
{
    partial class frmDoiPhong
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
            this.cboPhongMoi = new DevExpress.XtraEditors.LookUpEdit();
            this.txtLyDo = new DevExpress.XtraEditors.TextEdit();
            this.btnXacNhan = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutPhongMoi = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutLyDo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutBtnXacNhan = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutBtnHuy = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboPhongMoi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLyDo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutPhongMoi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLyDo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBtnXacNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBtnHuy)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.cboPhongMoi);
            this.layoutControl1.Controls.Add(this.txtLyDo);
            this.layoutControl1.Controls.Add(this.btnXacNhan);
            this.layoutControl1.Controls.Add(this.btnHuy);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(380, 180);
            this.layoutControl1.TabIndex = 0;
            // 
            // cboPhongMoi
            // 
            this.cboPhongMoi.Location = new System.Drawing.Point(112, 12);
            this.cboPhongMoi.Name = "cboPhongMoi";
            this.cboPhongMoi.Properties.DisplayMember = "MaPhong";
            this.cboPhongMoi.Properties.ValueMember = "IdPhong";
            this.cboPhongMoi.Properties.NullText = "";
            this.cboPhongMoi.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("MaPhong", "Mã Phòng"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenLoaiPhong", "Loại Phòng")});
            this.cboPhongMoi.Size = new System.Drawing.Size(256, 22);
            this.cboPhongMoi.StyleController = this.layoutControl1;
            this.cboPhongMoi.TabIndex = 4;
            // 
            // txtLyDo
            // 
            this.txtLyDo.Location = new System.Drawing.Point(112, 38);
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.Size = new System.Drawing.Size(256, 22);
            this.txtLyDo.StyleController = this.layoutControl1;
            this.txtLyDo.TabIndex = 5;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.Location = new System.Drawing.Point(12, 64);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(174, 26);
            this.btnXacNhan.StyleController = this.layoutControl1;
            this.btnXacNhan.TabIndex = 6;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.Click += new System.EventHandler(this.BtnXacNhan_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.Location = new System.Drawing.Point(190, 64);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(178, 26);
            this.btnHuy.StyleController = this.layoutControl1;
            this.btnHuy.TabIndex = 7;
            this.btnHuy.Text = "Hủy bỏ";
            this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutPhongMoi,
            this.layoutLyDo,
            this.layoutBtnXacNhan,
            this.layoutBtnHuy});
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Size = new System.Drawing.Size(380, 180);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutPhongMoi
            // 
            this.layoutPhongMoi.Control = this.cboPhongMoi;
            this.layoutPhongMoi.Location = new System.Drawing.Point(0, 0);
            this.layoutPhongMoi.Name = "layoutPhongMoi";
            this.layoutPhongMoi.Size = new System.Drawing.Size(360, 26);
            this.layoutPhongMoi.Text = "Phòng mới";
            this.layoutPhongMoi.TextSize = new System.Drawing.Size(90, 16);
            // 
            // layoutLyDo
            // 
            this.layoutLyDo.Control = this.txtLyDo;
            this.layoutLyDo.Location = new System.Drawing.Point(0, 26);
            this.layoutLyDo.Name = "layoutLyDo";
            this.layoutLyDo.Size = new System.Drawing.Size(360, 26);
            this.layoutLyDo.Text = "Lý do đổi";
            this.layoutLyDo.TextSize = new System.Drawing.Size(90, 16);
            // 
            // layoutBtnXacNhan
            // 
            this.layoutBtnXacNhan.Control = this.btnXacNhan;
            this.layoutBtnXacNhan.Location = new System.Drawing.Point(0, 52);
            this.layoutBtnXacNhan.Name = "layoutBtnXacNhan";
            this.layoutBtnXacNhan.Size = new System.Drawing.Size(178, 30);
            this.layoutBtnXacNhan.TextSize = new System.Drawing.Size(0, 0);
            this.layoutBtnXacNhan.TextVisible = false;
            // 
            // layoutBtnHuy
            // 
            this.layoutBtnHuy.Control = this.btnHuy;
            this.layoutBtnHuy.Location = new System.Drawing.Point(178, 52);
            this.layoutBtnHuy.Name = "layoutBtnHuy";
            this.layoutBtnHuy.Size = new System.Drawing.Size(182, 30);
            this.layoutBtnHuy.TextSize = new System.Drawing.Size(0, 0);
            this.layoutBtnHuy.TextVisible = false;
            // 
            // frmDoiPhong
            // 
            this.AcceptButton = this.btnXacNhan;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnHuy;
            this.ClientSize = new System.Drawing.Size(380, 180);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDoiPhong";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đổi Phòng";
            this.Load += new System.EventHandler(this.frmDoiPhong_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cboPhongMoi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLyDo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutPhongMoi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutLyDo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBtnXacNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutBtnHuy)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LookUpEdit cboPhongMoi;
        private DevExpress.XtraEditors.TextEdit txtLyDo;
        private DevExpress.XtraEditors.SimpleButton btnXacNhan;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutPhongMoi;
        private DevExpress.XtraLayout.LayoutControlItem layoutLyDo;
        private DevExpress.XtraLayout.LayoutControlItem layoutBtnXacNhan;
        private DevExpress.XtraLayout.LayoutControlItem layoutBtnHuy;
    }
}
