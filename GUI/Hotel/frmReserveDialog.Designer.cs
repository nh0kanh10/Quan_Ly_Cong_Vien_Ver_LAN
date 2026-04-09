namespace GUI
{
    partial class frmReserveDialog
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cboKhachHang = new Guna.UI2.WinForms.Guna2ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpNgayNhan = new DevExpress.XtraEditors.DateEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.dtpNgayTra = new DevExpress.XtraEditors.DateEdit();
            this.label4 = new System.Windows.Forms.Label();
            this.txtTienCoc = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.btnReserve = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.lblConflict = new System.Windows.Forms.Label();
            this.btnThemKhachNhanh = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.txtTienCoc)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Location = new System.Drawing.Point(52, 37);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(161, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Đặt trước Phòng";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(55, 92);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(71, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Khách hàng";
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhachHang.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.cboKhachHang.ItemHeight = 30;
            this.cboKhachHang.Location = new System.Drawing.Point(59, 117);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(327, 36);
            this.cboKhachHang.TabIndex = 2;
            // 
            // btnThemKhachNhanh
            // 
            this.btnThemKhachNhanh.BorderThickness = 1;
            this.btnThemKhachNhanh.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnThemKhachNhanh.Location = new System.Drawing.Point(392, 117);
            this.btnThemKhachNhanh.Name = "btnThemKhachNhanh";
            this.btnThemKhachNhanh.Size = new System.Drawing.Size(36, 36);
            this.btnThemKhachNhanh.TabIndex = 12;
            this.btnThemKhachNhanh.Text = "+";
            this.btnThemKhachNhanh.Click += new System.EventHandler(this.btnThemKhachNhanh_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(55, 167);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(103, 15);
            this.label2.TabIndex = 3;
            this.label2.Text = "Ngày nhận phòng";
            // 
            // dtpNgayNhan
            // 
            this.dtpNgayNhan.Location = new System.Drawing.Point(59, 192);
            this.dtpNgayNhan.Name = "dtpNgayNhan";
            this.dtpNgayNhan.Size = new System.Drawing.Size(369, 40);
            this.dtpNgayNhan.TabIndex = 4;
            this.dtpNgayNhan.DateTime = System.DateTime.Now;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(55, 242);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Ngày trả phòng";
            // 
            // dtpNgayTra
            // 
            this.dtpNgayTra.Location = new System.Drawing.Point(59, 267);
            this.dtpNgayTra.Name = "dtpNgayTra";
            this.dtpNgayTra.Size = new System.Drawing.Size(369, 40);
            this.dtpNgayTra.TabIndex = 6;
            this.dtpNgayTra.DateTime = System.DateTime.Now;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(55, 322);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(123, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Tiền đặt cọc (nếu có)";
            // 
            // txtTienCoc
            // 
            this.txtTienCoc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTienCoc.Location = new System.Drawing.Point(59, 347);
            this.txtTienCoc.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.txtTienCoc.Name = "txtTienCoc";
            this.txtTienCoc.Size = new System.Drawing.Size(369, 45);
            this.txtTienCoc.TabIndex = 8;
            this.txtTienCoc.ThousandsSeparator = true;
            // 
            // btnReserve
            // 
            this.btnReserve.BorderThickness = 2;
            this.btnReserve.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnReserve.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnReserve.DisabledState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnReserve.Location = new System.Drawing.Point(59, 417);
            this.btnReserve.Name = "btnReserve";
            this.btnReserve.Size = new System.Drawing.Size(165, 45);
            this.btnReserve.TabIndex = 9;
            this.btnReserve.Text = "XÁC NHẬN ĐẶT";
            this.btnReserve.Click += new System.EventHandler(this.btnReserve_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BorderThickness = 2;
            this.btnCancel.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnCancel.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnCancel.DisabledState.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnCancel.Location = new System.Drawing.Point(263, 417);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(165, 45);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "HỦY BỎ";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblConflict
            // 
            this.lblConflict.AutoSize = true;
            this.lblConflict.Location = new System.Drawing.Point(56, 72);
            this.lblConflict.Name = "lblConflict";
            this.lblConflict.Size = new System.Drawing.Size(0, 13);
            this.lblConflict.TabIndex = 11;
            this.lblConflict.Visible = false;
            // 
            // frmReserveDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 524);
            this.Controls.Add(this.lblConflict);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReserve);
            this.Controls.Add(this.txtTienCoc);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dtpNgayTra);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpNgayNhan);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnThemKhachNhanh);
            this.Controls.Add(this.cboKhachHang);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReserveDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đặt trước Phòng";
            ((System.ComponentModel.ISupportInitialize)(this.txtTienCoc)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private Guna.UI2.WinForms.Guna2ComboBox cboKhachHang;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit dtpNgayNhan;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.DateEdit dtpNgayTra;
        private System.Windows.Forms.Label label4;
        private Guna.UI2.WinForms.Guna2NumericUpDown txtTienCoc;
        private Guna.UI2.WinForms.Guna2Button btnReserve;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private System.Windows.Forms.Label lblConflict;
        private Guna.UI2.WinForms.Guna2Button btnThemKhachNhanh;
    }
}

