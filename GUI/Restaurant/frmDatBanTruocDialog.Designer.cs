namespace GUI
{
    partial class frmDatBanTruocDialog
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
            this.txtSearchBooking = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnTimDoan = new Guna.UI2.WinForms.Guna2Button();
            this.lblTimKetQua = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblBanDaChon = new System.Windows.Forms.Label();
            this.lblKhachHang = new System.Windows.Forms.Label();
            this.cboKhachHang = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnThemKhachNhanh = new Guna.UI2.WinForms.Guna2Button();
            this.lblSoKhach = new System.Windows.Forms.Label();
            this.numSoKhach = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.lblGioDen = new System.Windows.Forms.Label();
            this.dtpGioDen = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.lblGhiChu = new System.Windows.Forms.Label();
            this.txtGhiChu = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTienCoc = new System.Windows.Forms.Label();
            this.txtTienCoc = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblSdt = new System.Windows.Forms.Label();
            this.txtSoDienThoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnReserve = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.lblHinhThuc = new System.Windows.Forms.Label();
            this.rdoTienMat = new System.Windows.Forms.RadioButton();
            this.rdoRFID = new System.Windows.Forms.RadioButton();
            this.txtMaRFID = new Guna.UI2.WinForms.Guna2TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numSoKhach)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ĐẶT BÀN TRƯỚC (RESERVATION)";
            // 
            // txtSearchBooking
            // 
            this.txtSearchBooking.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.txtSearchBooking.BorderRadius = 4;
            this.txtSearchBooking.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearchBooking.DefaultText = "";
            this.txtSearchBooking.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSearchBooking.ForeColor = System.Drawing.Color.Black;
            this.txtSearchBooking.Location = new System.Drawing.Point(20, 50);
            this.txtSearchBooking.Name = "txtSearchBooking";
            this.txtSearchBooking.PlaceholderText = "Nhập BK-xxx để liên kết đoàn (hoặc bỏ trống)...";
            this.txtSearchBooking.SelectedText = "";
            this.txtSearchBooking.Size = new System.Drawing.Size(290, 30);
            this.txtSearchBooking.TabIndex = 30;
            // 
            // btnTimDoan
            // 
            this.btnTimDoan.BorderRadius = 4;
            this.btnTimDoan.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnTimDoan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnTimDoan.Font = new System.Drawing.Font("Segoe UI", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnTimDoan.ForeColor = System.Drawing.Color.White;
            this.btnTimDoan.Location = new System.Drawing.Point(315, 50);
            this.btnTimDoan.Name = "btnTimDoan";
            this.btnTimDoan.Size = new System.Drawing.Size(100, 30);
            this.btnTimDoan.TabIndex = 31;
            this.btnTimDoan.Text = "🔍 TÌM ĐOÀN";
            this.btnTimDoan.Click += new System.EventHandler(this.btnTimDoan_Click);
            // 
            // lblTimKetQua
            // 
            this.lblTimKetQua.AutoSize = true;
            this.lblTimKetQua.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblTimKetQua.ForeColor = System.Drawing.Color.Gray;
            this.lblTimKetQua.Location = new System.Drawing.Point(20, 83);
            this.lblTimKetQua.Name = "lblTimKetQua";
            this.lblTimKetQua.Size = new System.Drawing.Size(200, 13);
            this.lblTimKetQua.TabIndex = 32;
            this.lblTimKetQua.Text = "Bỏ trống -> không liên kết đoàn.";
            // 
            // lblBanDaChon
            // 
            this.lblBanDaChon.AutoSize = true;
            this.lblBanDaChon.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblBanDaChon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.lblBanDaChon.Location = new System.Drawing.Point(20, 105);
            this.lblBanDaChon.Name = "lblBanDaChon";
            this.lblBanDaChon.Size = new System.Drawing.Size(100, 19);
            this.lblBanDaChon.TabIndex = 1;
            this.lblBanDaChon.Text = "Bàn đã chọn:";
            // 
            // lblKhachHang
            // 
            this.lblKhachHang.AutoSize = true;
            this.lblKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblKhachHang.Location = new System.Drawing.Point(20, 140);
            this.lblKhachHang.Name = "lblKhachHang";
            this.lblKhachHang.Size = new System.Drawing.Size(126, 19);
            this.lblKhachHang.TabIndex = 2;
            this.lblKhachHang.Text = "Tên KH/Đoàn (*)";
            // 
            // cboKhachHang
            // 
            this.cboKhachHang.BackColor = System.Drawing.Color.Transparent;
            this.cboKhachHang.BorderRadius = 4;
            this.cboKhachHang.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboKhachHang.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhachHang.FocusedColor = System.Drawing.Color.Empty;
            this.cboKhachHang.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboKhachHang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboKhachHang.FormattingEnabled = true;
            this.cboKhachHang.ItemHeight = 30;
            this.cboKhachHang.Location = new System.Drawing.Point(170, 132);
            this.cboKhachHang.Name = "cboKhachHang";
            this.cboKhachHang.Size = new System.Drawing.Size(200, 36);
            this.cboKhachHang.TabIndex = 3;
            // 
            // btnThemKhachNhanh
            // 
            this.btnThemKhachNhanh.BorderRadius = 4;
            this.btnThemKhachNhanh.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnThemKhachNhanh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemKhachNhanh.ForeColor = System.Drawing.Color.White;
            this.btnThemKhachNhanh.Location = new System.Drawing.Point(380, 132);
            this.btnThemKhachNhanh.Name = "btnThemKhachNhanh";
            this.btnThemKhachNhanh.Size = new System.Drawing.Size(40, 36);
            this.btnThemKhachNhanh.TabIndex = 4;
            this.btnThemKhachNhanh.Text = "+";
            this.btnThemKhachNhanh.Click += new System.EventHandler(this.btnThemKhachNhanh_Click);
            // 
            // lblSdt
            // 
            this.lblSdt.AutoSize = true;
            this.lblSdt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSdt.Location = new System.Drawing.Point(20, 190);
            this.lblSdt.Name = "lblSdt";
            this.lblSdt.Size = new System.Drawing.Size(89, 19);
            this.lblSdt.TabIndex = 101;
            this.lblSdt.Text = "Số điện thoại";
            // 
            // txtSoDienThoai
            // 
            this.txtSoDienThoai.BorderRadius = 4;
            this.txtSoDienThoai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoDienThoai.DefaultText = "";
            this.txtSoDienThoai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSoDienThoai.Location = new System.Drawing.Point(170, 182);
            this.txtSoDienThoai.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtSoDienThoai.Name = "txtSoDienThoai";
            this.txtSoDienThoai.PasswordChar = '\0';
            this.txtSoDienThoai.PlaceholderText = "SĐT khách hàng...";
            this.txtSoDienThoai.SelectedText = "";
            this.txtSoDienThoai.Size = new System.Drawing.Size(200, 36);
            this.txtSoDienThoai.TabIndex = 102;
            // 
            // lblSoKhach
            // 
            this.lblSoKhach.AutoSize = true;
            this.lblSoKhach.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSoKhach.Location = new System.Drawing.Point(20, 230);
            this.lblSoKhach.Name = "lblSoKhach";
            this.lblSoKhach.Size = new System.Drawing.Size(104, 19);
            this.lblSoKhach.TabIndex = 5;
            this.lblSoKhach.Text = "Số lượng khách";
            // 
            // numSoKhach
            // 
            this.numSoKhach.BackColor = System.Drawing.Color.Transparent;
            this.numSoKhach.BorderRadius = 4;
            this.numSoKhach.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.numSoKhach.Location = new System.Drawing.Point(170, 222);
            this.numSoKhach.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.numSoKhach.Name = "numSoKhach";
            this.numSoKhach.Size = new System.Drawing.Size(100, 36);
            this.numSoKhach.TabIndex = 6;
            this.numSoKhach.Value = new decimal(new int[] { 2, 0, 0, 0 });
            // 
            // lblGioDen
            // 
            this.lblGioDen.AutoSize = true;
            this.lblGioDen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGioDen.Location = new System.Drawing.Point(20, 280);
            this.lblGioDen.Name = "lblGioDen";
            this.lblGioDen.Size = new System.Drawing.Size(108, 19);
            this.lblGioDen.TabIndex = 7;
            this.lblGioDen.Text = "Giờ đến dự kiến";
            // 
            // dtpGioDen
            // 
            this.dtpGioDen.BorderRadius = 4;
            this.dtpGioDen.CustomFormat = "HH:mm - dd/MM/yyyy";
            this.dtpGioDen.FillColor = System.Drawing.Color.White;
            this.dtpGioDen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.dtpGioDen.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpGioDen.Location = new System.Drawing.Point(170, 272);
            this.dtpGioDen.Name = "dtpGioDen";
            this.dtpGioDen.Size = new System.Drawing.Size(200, 36);
            this.dtpGioDen.TabIndex = 8;
            this.dtpGioDen.Value = new System.DateTime(2026, 4, 4, 12, 0, 0, 0);
            // 
            // lblGhiChu
            // 
            this.lblGhiChu.AutoSize = true;
            this.lblGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGhiChu.Location = new System.Drawing.Point(20, 330);
            this.lblGhiChu.Name = "lblGhiChu";
            this.lblGhiChu.Size = new System.Drawing.Size(130, 19);
            this.lblGhiChu.TabIndex = 9;
            this.lblGhiChu.Text = "Ghi chú (Chay, Dị ứng)";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.BorderRadius = 4;
            this.txtGhiChu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGhiChu.DefaultText = "";
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtGhiChu.Location = new System.Drawing.Point(170, 322);
            this.txtGhiChu.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.PasswordChar = '\0';
            this.txtGhiChu.PlaceholderText = "Nhập ghi chú vào đây...";
            this.txtGhiChu.SelectedText = "";
            this.txtGhiChu.Size = new System.Drawing.Size(250, 60);
            this.txtGhiChu.TabIndex = 10;
            // 
            // lblTienCoc
            // 
            this.lblTienCoc.AutoSize = true;
            this.lblTienCoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTienCoc.Location = new System.Drawing.Point(20, 405);
            this.lblTienCoc.Name = "lblTienCoc";
            this.lblTienCoc.Size = new System.Drawing.Size(133, 19);
            this.lblTienCoc.TabIndex = 11;
            this.lblTienCoc.Text = "ĐẶT CỌC (DEPOSIT)";
            // 
            // txtTienCoc
            // 
            this.txtTienCoc.BorderRadius = 4;
            this.txtTienCoc.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTienCoc.DefaultText = "0";
            this.txtTienCoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTienCoc.Location = new System.Drawing.Point(170, 397);
            this.txtTienCoc.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTienCoc.Name = "txtTienCoc";
            this.txtTienCoc.PasswordChar = '\0';
            this.txtTienCoc.PlaceholderText = "Nhập số tiền...";
            this.txtTienCoc.SelectedText = "";
            this.txtTienCoc.Size = new System.Drawing.Size(150, 36);
            this.txtTienCoc.TabIndex = 12;
            this.txtTienCoc.TextChanged += new System.EventHandler(this.txtTienCoc_TextChanged);
            // 
            // btnReserve
            // 
            this.btnReserve.BorderRadius = 10;
            this.btnReserve.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnReserve.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnReserve.ForeColor = System.Drawing.Color.White;
            this.btnReserve.Location = new System.Drawing.Point(270, 530);
            this.btnReserve.Name = "btnReserve";
            this.btnReserve.Size = new System.Drawing.Size(150, 45);
            this.btnReserve.TabIndex = 13;
            this.btnReserve.Text = "LƯU ĐẶT BÀN (F9)";
            this.btnReserve.Click += new System.EventHandler(this.btnReserve_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BorderRadius = 10;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(110, 530);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(150, 45);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "HỦY BỎ (ESC)";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblHinhThuc
            // 
            this.lblHinhThuc.AutoSize = true;
            this.lblHinhThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHinhThuc.Location = new System.Drawing.Point(20, 445);
            this.lblHinhThuc.Name = "lblHinhThuc";
            this.lblHinhThuc.Size = new System.Drawing.Size(120, 19);
            this.lblHinhThuc.TabIndex = 15;
            this.lblHinhThuc.Text = "Hình thức cọc";
            // 
            // rdoTienMat
            // 
            this.rdoTienMat.AutoSize = true;
            this.rdoTienMat.Checked = true;
            this.rdoTienMat.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoTienMat.Location = new System.Drawing.Point(170, 443);
            this.rdoTienMat.Name = "rdoTienMat";
            this.rdoTienMat.Size = new System.Drawing.Size(84, 23);
            this.rdoTienMat.TabIndex = 16;
            this.rdoTienMat.TabStop = true;
            this.rdoTienMat.Text = "Tiền mặt";
            this.rdoTienMat.UseVisualStyleBackColor = true;
            this.rdoTienMat.CheckedChanged += new System.EventHandler(this.RdoHinhThuc_CheckedChanged);
            // 
            // rdoRFID
            // 
            this.rdoRFID.AutoSize = true;
            this.rdoRFID.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rdoRFID.Location = new System.Drawing.Point(260, 443);
            this.rdoRFID.Name = "rdoRFID";
            this.rdoRFID.Size = new System.Drawing.Size(73, 23);
            this.rdoRFID.TabIndex = 17;
            this.rdoRFID.Text = "Ví RFID";
            this.rdoRFID.UseVisualStyleBackColor = true;
            this.rdoRFID.CheckedChanged += new System.EventHandler(this.RdoHinhThuc_CheckedChanged);
            // 
            // txtMaRFID
            // 
            this.txtMaRFID.BorderRadius = 4;
            this.txtMaRFID.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaRFID.DefaultText = "";
            this.txtMaRFID.Enabled = false;
            this.txtMaRFID.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaRFID.Location = new System.Drawing.Point(170, 475);
            this.txtMaRFID.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtMaRFID.Name = "txtMaRFID";
            this.txtMaRFID.PasswordChar = '\0';
            this.txtMaRFID.PlaceholderText = "Quẹt thẻ RFID để cọc...";
            this.txtMaRFID.SelectedText = "";
            this.txtMaRFID.Size = new System.Drawing.Size(250, 36);
            this.txtMaRFID.TabIndex = 18;
            // 
            // frmDatBanTruocDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.ClientSize = new System.Drawing.Size(434, 595);
            this.Controls.Add(this.txtMaRFID);
            this.Controls.Add(this.rdoRFID);
            this.Controls.Add(this.rdoTienMat);
            this.Controls.Add(this.lblHinhThuc);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnReserve);
            this.Controls.Add(this.txtTienCoc);
            this.Controls.Add(this.lblTienCoc);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.lblGhiChu);
            this.Controls.Add(this.dtpGioDen);
            this.Controls.Add(this.lblGioDen);
            this.Controls.Add(this.numSoKhach);
            this.Controls.Add(this.lblSoKhach);
            this.Controls.Add(this.txtSoDienThoai);
            this.Controls.Add(this.lblSdt);
            this.Controls.Add(this.btnThemKhachNhanh);
            this.Controls.Add(this.cboKhachHang);
            this.Controls.Add(this.lblKhachHang);
            this.Controls.Add(this.lblBanDaChon);
            this.Controls.Add(this.lblTimKetQua);
            this.Controls.Add(this.btnTimDoan);
            this.Controls.Add(this.txtSearchBooking);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDatBanTruocDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Đặt Bàn Trước";
            this.Load += new System.EventHandler(this.FrmDatBanTruocDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FrmDatBanTruocDialog_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.numSoKhach)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblBanDaChon;
        private System.Windows.Forms.Label lblKhachHang;
        private Guna.UI2.WinForms.Guna2ComboBox cboKhachHang;
        private Guna.UI2.WinForms.Guna2Button btnThemKhachNhanh;
        private System.Windows.Forms.Label lblSoKhach;
        private Guna.UI2.WinForms.Guna2NumericUpDown numSoKhach;
        private System.Windows.Forms.Label lblGioDen;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpGioDen;
        private System.Windows.Forms.Label lblGhiChu;
        private Guna.UI2.WinForms.Guna2TextBox txtGhiChu;
        private System.Windows.Forms.Label lblTienCoc;
        private Guna.UI2.WinForms.Guna2TextBox txtTienCoc;
        private Guna.UI2.WinForms.Guna2Button btnReserve;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private System.Windows.Forms.Label lblHinhThuc;
        private System.Windows.Forms.RadioButton rdoTienMat;
        private System.Windows.Forms.RadioButton rdoRFID;
        private Guna.UI2.WinForms.Guna2TextBox txtMaRFID;
        private System.Windows.Forms.Label lblSdt;
        private Guna.UI2.WinForms.Guna2TextBox txtSoDienThoai;

        private Guna.UI2.WinForms.Guna2TextBox txtSearchBooking;
        private Guna.UI2.WinForms.Guna2Button btnTimDoan;
        private System.Windows.Forms.Label lblTimKetQua;
    }
}
