namespace GUI
{
    partial class frmTaiNanLaoDongDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblNgayTaiNan = new System.Windows.Forms.Label();
            this.dtpNgayTaiNan = new DevExpress.XtraEditors.DateEdit();
            this.lblLoaiTaiNan = new System.Windows.Forms.Label();
            this.cboLoaiTaiNan = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblMucDo = new System.Windows.Forms.Label();
            this.cboMucDo = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtMoTa = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();

            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayTaiNan.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayTaiNan.Properties)).BeginInit();
            this.SuspendLayout();

            // lblNgayTaiNan
            this.lblNgayTaiNan.AutoSize = true;
            this.lblNgayTaiNan.Location = new System.Drawing.Point(20, 20);
            this.lblNgayTaiNan.Name = "lblNgayTaiNan";
            this.lblNgayTaiNan.Size = new System.Drawing.Size(91, 17);
            this.lblNgayTaiNan.TabIndex = 0;
            this.lblNgayTaiNan.Text = "Ngày tai nạn:";

            // dtpNgayTaiNan
            this.dtpNgayTaiNan.Location = new System.Drawing.Point(150, 15);
            this.dtpNgayTaiNan.Name = "dtpNgayTaiNan";
            this.dtpNgayTaiNan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayTaiNan.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayTaiNan.Size = new System.Drawing.Size(250, 36);
            this.dtpNgayTaiNan.TabIndex = 1;

            // lblLoaiTaiNan
            this.lblLoaiTaiNan.AutoSize = true;
            this.lblLoaiTaiNan.Location = new System.Drawing.Point(20, 70);
            this.lblLoaiTaiNan.Name = "lblLoaiTaiNan";
            this.lblLoaiTaiNan.Size = new System.Drawing.Size(86, 17);
            this.lblLoaiTaiNan.TabIndex = 2;
            this.lblLoaiTaiNan.Text = "Loại tai nạn:";

            // cboLoaiTaiNan
            this.cboLoaiTaiNan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiTaiNan.Location = new System.Drawing.Point(150, 65);
            this.cboLoaiTaiNan.Name = "cboLoaiTaiNan";
            this.cboLoaiTaiNan.Size = new System.Drawing.Size(250, 36);
            this.cboLoaiTaiNan.TabIndex = 3;

            // lblMucDo
            this.lblMucDo.AutoSize = true;
            this.lblMucDo.Location = new System.Drawing.Point(20, 120);
            this.lblMucDo.Name = "lblMucDo";
            this.lblMucDo.Size = new System.Drawing.Size(58, 17);
            this.lblMucDo.TabIndex = 4;
            this.lblMucDo.Text = "Mức độ:";

            // cboMucDo
            this.cboMucDo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboMucDo.Location = new System.Drawing.Point(150, 115);
            this.cboMucDo.Name = "cboMucDo";
            this.cboMucDo.Size = new System.Drawing.Size(250, 36);
            this.cboMucDo.TabIndex = 5;

            // lblMoTa
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(20, 170);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(47, 17);
            this.lblMoTa.TabIndex = 6;
            this.lblMoTa.Text = "Mô tả:";

            // txtMoTa
            this.txtMoTa.Location = new System.Drawing.Point(150, 165);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(250, 36);
            this.txtMoTa.TabIndex = 7;

            // btnLuu
            this.btnLuu.Location = new System.Drawing.Point(150, 220);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(120, 40);
            this.btnLuu.TabIndex = 8;
            this.btnLuu.Text = "Lưu (F2)";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);

            // btnHuy
            this.btnHuy.Location = new System.Drawing.Point(280, 220);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(120, 40);
            this.btnHuy.TabIndex = 9;
            this.btnHuy.Text = "Đóng";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);

            // frmTaiNanLaoDongDialog
            this.ClientSize = new System.Drawing.Size(430, 290);
            this.Controls.Add(this.lblNgayTaiNan);
            this.Controls.Add(this.dtpNgayTaiNan);
            this.Controls.Add(this.lblLoaiTaiNan);
            this.Controls.Add(this.cboLoaiTaiNan);
            this.Controls.Add(this.lblMucDo);
            this.Controls.Add(this.cboMucDo);
            this.Controls.Add(this.lblMoTa);
            this.Controls.Add(this.txtMoTa);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.Name = "frmTaiNanLaoDongDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Ghi Nhận Tai Nạn Lao Động";
            this.Load += new System.EventHandler(this.frmTaiNanLaoDongDialog_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayTaiNan.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayTaiNan.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblNgayTaiNan;
        private DevExpress.XtraEditors.DateEdit dtpNgayTaiNan;
        private System.Windows.Forms.Label lblLoaiTaiNan;
        private Guna.UI2.WinForms.Guna2ComboBox cboLoaiTaiNan;
        private System.Windows.Forms.Label lblMucDo;
        private Guna.UI2.WinForms.Guna2ComboBox cboMucDo;
        private System.Windows.Forms.Label lblMoTa;
        private Guna.UI2.WinForms.Guna2TextBox txtMoTa;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
    }
}
