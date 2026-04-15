namespace GUI
{
    partial class frmDonXinNghiDialog
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.lblLoaiNghi = new System.Windows.Forms.Label();
            this.cboLoaiNghi = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblNgayBatDau = new System.Windows.Forms.Label();
            this.dtpNgayBatDau = new DevExpress.XtraEditors.DateEdit();
            this.lblNgayKetThuc = new System.Windows.Forms.Label();
            this.dtpNgayKetThuc = new DevExpress.XtraEditors.DateEdit();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtLyDo = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTiLe = new System.Windows.Forms.Label();
            this.numTiLe = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.lblNguonChiTra = new System.Windows.Forms.Label();
            this.cboNguonChiTra = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();

            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayBatDau.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayBatDau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayKetThuc.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayKetThuc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTiLe)).BeginInit();
            this.SuspendLayout();

            // lblLoaiNghi
            this.lblLoaiNghi.AutoSize = true;
            this.lblLoaiNghi.Location = new System.Drawing.Point(20, 20);
            this.lblLoaiNghi.Name = "lblLoaiNghi";
            this.lblLoaiNghi.Size = new System.Drawing.Size(73, 17);
            this.lblLoaiNghi.TabIndex = 0;
            this.lblLoaiNghi.Text = "Loại nghỉ:";

            // cboLoaiNghi
            this.cboLoaiNghi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiNghi.Location = new System.Drawing.Point(150, 15);
            this.cboLoaiNghi.Name = "cboLoaiNghi";
            this.cboLoaiNghi.Size = new System.Drawing.Size(250, 36);
            this.cboLoaiNghi.TabIndex = 1;

            // lblNgayBatDau
            this.lblNgayBatDau.AutoSize = true;
            this.lblNgayBatDau.Location = new System.Drawing.Point(20, 70);
            this.lblNgayBatDau.Name = "lblNgayBatDau";
            this.lblNgayBatDau.Size = new System.Drawing.Size(95, 17);
            this.lblNgayBatDau.TabIndex = 2;
            this.lblNgayBatDau.Text = "Ngày bắt đầu:";

            // dtpNgayBatDau
            this.dtpNgayBatDau.Location = new System.Drawing.Point(150, 65);
            this.dtpNgayBatDau.Name = "dtpNgayBatDau";
            this.dtpNgayBatDau.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayBatDau.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayBatDau.Size = new System.Drawing.Size(250, 36);
            this.dtpNgayBatDau.TabIndex = 3;

            // lblNgayKetThuc
            this.lblNgayKetThuc.AutoSize = true;
            this.lblNgayKetThuc.Location = new System.Drawing.Point(20, 120);
            this.lblNgayKetThuc.Name = "lblNgayKetThuc";
            this.lblNgayKetThuc.Size = new System.Drawing.Size(99, 17);
            this.lblNgayKetThuc.TabIndex = 4;
            this.lblNgayKetThuc.Text = "Ngày kết thúc:";

            // dtpNgayKetThuc
            this.dtpNgayKetThuc.Location = new System.Drawing.Point(150, 115);
            this.dtpNgayKetThuc.Name = "dtpNgayKetThuc";
            this.dtpNgayKetThuc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayKetThuc.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayKetThuc.Size = new System.Drawing.Size(250, 36);
            this.dtpNgayKetThuc.TabIndex = 5;

            // lblTiLe
            this.lblTiLe.AutoSize = true;
            this.lblTiLe.Location = new System.Drawing.Point(20, 170);
            this.lblTiLe.Name = "lblTiLe";
            this.lblTiLe.Size = new System.Drawing.Size(126, 17);
            this.lblTiLe.TabIndex = 6;
            this.lblTiLe.Text = "Tỉ lệ lương hưởng:";

            // numTiLe
            this.numTiLe.Location = new System.Drawing.Point(150, 165);
            this.numTiLe.Name = "numTiLe";
            this.numTiLe.Size = new System.Drawing.Size(250, 36);
            this.numTiLe.TabIndex = 7;
            this.numTiLe.Value = new decimal(new int[] { 0, 0, 0, 0 });
            this.numTiLe.Maximum = new decimal(new int[] { 100, 0, 0, 0 });

            // lblNguonChiTra
            this.lblNguonChiTra.AutoSize = true;
            this.lblNguonChiTra.Location = new System.Drawing.Point(20, 220);
            this.lblNguonChiTra.Name = "lblNguonChiTra";
            this.lblNguonChiTra.Size = new System.Drawing.Size(98, 17);
            this.lblNguonChiTra.TabIndex = 8;
            this.lblNguonChiTra.Text = "Nguồn chi trả:";

            // cboNguonChiTra
            this.cboNguonChiTra.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNguonChiTra.Location = new System.Drawing.Point(150, 215);
            this.cboNguonChiTra.Name = "cboNguonChiTra";
            this.cboNguonChiTra.Size = new System.Drawing.Size(250, 36);
            this.cboNguonChiTra.TabIndex = 9;

            // lblLyDo
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Location = new System.Drawing.Point(20, 270);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(47, 17);
            this.lblLyDo.TabIndex = 10;
            this.lblLyDo.Text = "Lý do:";

            // txtLyDo
            this.txtLyDo.Location = new System.Drawing.Point(150, 265);
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.Size = new System.Drawing.Size(250, 36);
            this.txtLyDo.TabIndex = 11;

            // btnLuu
            this.btnLuu.Location = new System.Drawing.Point(150, 320);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(120, 40);
            this.btnLuu.TabIndex = 12;
            this.btnLuu.Text = "Lưu (F2)";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);

            // btnHuy
            this.btnHuy.Location = new System.Drawing.Point(280, 320);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(120, 40);
            this.btnHuy.TabIndex = 13;
            this.btnHuy.Text = "Đóng";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);

            // frmDonXinNghiDialog
            this.ClientSize = new System.Drawing.Size(430, 390);
            this.Controls.Add(this.lblLoaiNghi);
            this.Controls.Add(this.cboLoaiNghi);
            this.Controls.Add(this.lblNgayBatDau);
            this.Controls.Add(this.dtpNgayBatDau);
            this.Controls.Add(this.lblNgayKetThuc);
            this.Controls.Add(this.dtpNgayKetThuc);
            this.Controls.Add(this.lblTiLe);
            this.Controls.Add(this.numTiLe);
            this.Controls.Add(this.lblNguonChiTra);
            this.Controls.Add(this.cboNguonChiTra);
            this.Controls.Add(this.lblLyDo);
            this.Controls.Add(this.txtLyDo);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.btnHuy);
            this.Name = "frmDonXinNghiDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm Đơn Xin Nghỉ";
            this.Load += new System.EventHandler(this.frmDonXinNghiDialog_Load);

            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayBatDau.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayBatDau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayKetThuc.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayKetThuc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numTiLe)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblLoaiNghi;
        private Guna.UI2.WinForms.Guna2ComboBox cboLoaiNghi;
        private System.Windows.Forms.Label lblNgayBatDau;
        private DevExpress.XtraEditors.DateEdit dtpNgayBatDau;
        private System.Windows.Forms.Label lblNgayKetThuc;
        private DevExpress.XtraEditors.DateEdit dtpNgayKetThuc;
        private System.Windows.Forms.Label lblLyDo;
        private Guna.UI2.WinForms.Guna2TextBox txtLyDo;
        private System.Windows.Forms.Label lblTiLe;
        private Guna.UI2.WinForms.Guna2NumericUpDown numTiLe;
        private System.Windows.Forms.Label lblNguonChiTra;
        private Guna.UI2.WinForms.Guna2ComboBox cboNguonChiTra;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
    }
}
