namespace GUI.POS
{
    partial class frmXuatVeDoan
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlTop = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlSearch = new System.Windows.Forms.Panel();
            this.btnTimDoan = new Guna.UI2.WinForms.Guna2Button();
            this.txtBookingCode = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlBookingInfo = new System.Windows.Forms.Panel();
            this.btnXuatVe = new Guna.UI2.WinForms.Guna2Button();
            this.lblValidation = new System.Windows.Forms.Label();
            this.lblCombo = new System.Windows.Forms.Label();
            this.lblCaptionCombo = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblCaptionTrangThai = new System.Windows.Forms.Label();
            this.lblNgayDen = new System.Windows.Forms.Label();
            this.lblCaptionNgayDen = new System.Windows.Forms.Label();
            this.lblChietKhau = new System.Windows.Forms.Label();
            this.lblCaptionChietKhau = new System.Windows.Forms.Label();
            this.lblSoLuongVe = new System.Windows.Forms.Label();
            this.lblCaptionSoLuongVe = new System.Windows.Forms.Label();
            this.lblSdt = new System.Windows.Forms.Label();
            this.lblCaptionSdt = new System.Windows.Forms.Label();
            this.lblNguoiDaiDien = new System.Windows.Forms.Label();
            this.lblCaptionNguoiDaiDien = new System.Windows.Forms.Label();
            this.lblTenDoan = new System.Windows.Forms.Label();
            this.lblCaptionTenDoan = new System.Windows.Forms.Label();
            this.pnlTop.SuspendLayout();
            this.pnlSearch.SuspendLayout();
            this.pnlBookingInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(603, 50);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(603, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "🎟 XUẤT VÉ KHÁCH ĐOÀN (B2B)";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlSearch
            // 
            this.pnlSearch.Controls.Add(this.btnTimDoan);
            this.pnlSearch.Controls.Add(this.txtBookingCode);
            this.pnlSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearch.Location = new System.Drawing.Point(0, 50);
            this.pnlSearch.Name = "pnlSearch";
            this.pnlSearch.Padding = new System.Windows.Forms.Padding(16, 12, 16, 12);
            this.pnlSearch.Size = new System.Drawing.Size(603, 60);
            this.pnlSearch.TabIndex = 1;
            // 
            // btnTimDoan
            // 
            this.btnTimDoan.BorderRadius = 8;
            this.btnTimDoan.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnTimDoan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnTimDoan.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.btnTimDoan.ForeColor = System.Drawing.Color.White;
            this.btnTimDoan.Location = new System.Drawing.Point(457, 12);
            this.btnTimDoan.Name = "btnTimDoan";
            this.btnTimDoan.Size = new System.Drawing.Size(130, 36);
            this.btnTimDoan.TabIndex = 1;
            this.btnTimDoan.Text = "TÌM ĐOÀN";
            this.btnTimDoan.Click += new System.EventHandler(this.btnTimDoan_Click);
            // 
            // txtBookingCode
            // 
            this.txtBookingCode.BorderRadius = 8;
            this.txtBookingCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBookingCode.DefaultText = "";
            this.txtBookingCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBookingCode.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBookingCode.Location = new System.Drawing.Point(16, 12);
            this.txtBookingCode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBookingCode.Name = "txtBookingCode";
            this.txtBookingCode.PlaceholderText = "Nhập Mã Booking (VD: BK-ABC-001) hoặc SĐT trưởng đoàn...";
            this.txtBookingCode.SelectedText = "";
            this.txtBookingCode.Size = new System.Drawing.Size(571, 36);
            this.txtBookingCode.TabIndex = 0;
            this.txtBookingCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtBookingCode_KeyDown);
            // 
            // pnlBookingInfo
            // 
            this.pnlBookingInfo.AutoScroll = true;
            this.pnlBookingInfo.Controls.Add(this.btnXuatVe);
            this.pnlBookingInfo.Controls.Add(this.lblValidation);
            this.pnlBookingInfo.Controls.Add(this.lblCombo);
            this.pnlBookingInfo.Controls.Add(this.lblCaptionCombo);
            this.pnlBookingInfo.Controls.Add(this.lblTrangThai);
            this.pnlBookingInfo.Controls.Add(this.lblCaptionTrangThai);
            this.pnlBookingInfo.Controls.Add(this.lblNgayDen);
            this.pnlBookingInfo.Controls.Add(this.lblCaptionNgayDen);
            this.pnlBookingInfo.Controls.Add(this.lblChietKhau);
            this.pnlBookingInfo.Controls.Add(this.lblCaptionChietKhau);
            this.pnlBookingInfo.Controls.Add(this.lblSoLuongVe);
            this.pnlBookingInfo.Controls.Add(this.lblCaptionSoLuongVe);
            this.pnlBookingInfo.Controls.Add(this.lblSdt);
            this.pnlBookingInfo.Controls.Add(this.lblCaptionSdt);
            this.pnlBookingInfo.Controls.Add(this.lblNguoiDaiDien);
            this.pnlBookingInfo.Controls.Add(this.lblCaptionNguoiDaiDien);
            this.pnlBookingInfo.Controls.Add(this.lblTenDoan);
            this.pnlBookingInfo.Controls.Add(this.lblCaptionTenDoan);
            this.pnlBookingInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBookingInfo.Location = new System.Drawing.Point(0, 110);
            this.pnlBookingInfo.Name = "pnlBookingInfo";
            this.pnlBookingInfo.Padding = new System.Windows.Forms.Padding(20);
            this.pnlBookingInfo.Size = new System.Drawing.Size(603, 380);
            this.pnlBookingInfo.TabIndex = 2;
            this.pnlBookingInfo.Visible = false;
            // 
            // btnXuatVe
            // 
            this.btnXuatVe.BorderRadius = 10;
            this.btnXuatVe.Enabled = false;
            this.btnXuatVe.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnXuatVe.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.btnXuatVe.ForeColor = System.Drawing.Color.White;
            this.btnXuatVe.Location = new System.Drawing.Point(20, 324);
            this.btnXuatVe.Name = "btnXuatVe";
            this.btnXuatVe.Size = new System.Drawing.Size(567, 48);
            this.btnXuatVe.TabIndex = 17;
            this.btnXuatVe.Text = "✅ XÁC NHẬN XUẤT VÉ";
            this.btnXuatVe.Click += new System.EventHandler(this.btnXuatVe_Click);
            // 
            // lblValidation
            // 
            this.lblValidation.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.lblValidation.Location = new System.Drawing.Point(20, 284);
            this.lblValidation.Name = "lblValidation";
            this.lblValidation.Size = new System.Drawing.Size(500, 30);
            this.lblValidation.TabIndex = 16;
            // 
            // lblCombo
            // 
            this.lblCombo.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblCombo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(72)))), ((int)(((byte)(153)))));
            this.lblCombo.Location = new System.Drawing.Point(165, 244);
            this.lblCombo.Name = "lblCombo";
            this.lblCombo.Size = new System.Drawing.Size(350, 28);
            this.lblCombo.TabIndex = 15;
            this.lblCombo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCaptionCombo
            // 
            this.lblCaptionCombo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCaptionCombo.Location = new System.Drawing.Point(20, 244);
            this.lblCaptionCombo.Name = "lblCaptionCombo";
            this.lblCaptionCombo.Size = new System.Drawing.Size(140, 28);
            this.lblCaptionCombo.TabIndex = 14;
            this.lblCaptionCombo.Text = "Gói Combo:";
            this.lblCaptionCombo.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblTrangThai.Location = new System.Drawing.Point(165, 212);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(350, 28);
            this.lblTrangThai.TabIndex = 13;
            this.lblTrangThai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCaptionTrangThai
            // 
            this.lblCaptionTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCaptionTrangThai.Location = new System.Drawing.Point(20, 212);
            this.lblCaptionTrangThai.Name = "lblCaptionTrangThai";
            this.lblCaptionTrangThai.Size = new System.Drawing.Size(140, 28);
            this.lblCaptionTrangThai.TabIndex = 12;
            this.lblCaptionTrangThai.Text = "Trạng thái:";
            this.lblCaptionTrangThai.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNgayDen
            // 
            this.lblNgayDen.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNgayDen.Location = new System.Drawing.Point(165, 180);
            this.lblNgayDen.Name = "lblNgayDen";
            this.lblNgayDen.Size = new System.Drawing.Size(350, 28);
            this.lblNgayDen.TabIndex = 11;
            this.lblNgayDen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCaptionNgayDen
            // 
            this.lblCaptionNgayDen.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCaptionNgayDen.Location = new System.Drawing.Point(20, 180);
            this.lblCaptionNgayDen.Name = "lblCaptionNgayDen";
            this.lblCaptionNgayDen.Size = new System.Drawing.Size(140, 28);
            this.lblCaptionNgayDen.TabIndex = 10;
            this.lblCaptionNgayDen.Text = "Ngày đến:";
            this.lblCaptionNgayDen.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblChietKhau
            // 
            this.lblChietKhau.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblChietKhau.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.lblChietKhau.Location = new System.Drawing.Point(165, 148);
            this.lblChietKhau.Name = "lblChietKhau";
            this.lblChietKhau.Size = new System.Drawing.Size(350, 28);
            this.lblChietKhau.TabIndex = 9;
            this.lblChietKhau.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCaptionChietKhau
            // 
            this.lblCaptionChietKhau.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCaptionChietKhau.Location = new System.Drawing.Point(20, 148);
            this.lblCaptionChietKhau.Name = "lblCaptionChietKhau";
            this.lblCaptionChietKhau.Size = new System.Drawing.Size(140, 28);
            this.lblCaptionChietKhau.TabIndex = 8;
            this.lblCaptionChietKhau.Text = "Chiết khấu:";
            this.lblCaptionChietKhau.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSoLuongVe
            // 
            this.lblSoLuongVe.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold);
            this.lblSoLuongVe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblSoLuongVe.Location = new System.Drawing.Point(165, 116);
            this.lblSoLuongVe.Name = "lblSoLuongVe";
            this.lblSoLuongVe.Size = new System.Drawing.Size(350, 28);
            this.lblSoLuongVe.TabIndex = 7;
            this.lblSoLuongVe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCaptionSoLuongVe
            // 
            this.lblCaptionSoLuongVe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCaptionSoLuongVe.Location = new System.Drawing.Point(20, 116);
            this.lblCaptionSoLuongVe.Name = "lblCaptionSoLuongVe";
            this.lblCaptionSoLuongVe.Size = new System.Drawing.Size(140, 28);
            this.lblCaptionSoLuongVe.TabIndex = 6;
            this.lblCaptionSoLuongVe.Text = "Số lượng vé:";
            this.lblCaptionSoLuongVe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblSdt
            // 
            this.lblSdt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSdt.Location = new System.Drawing.Point(165, 84);
            this.lblSdt.Name = "lblSdt";
            this.lblSdt.Size = new System.Drawing.Size(350, 28);
            this.lblSdt.TabIndex = 5;
            this.lblSdt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCaptionSdt
            // 
            this.lblCaptionSdt.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCaptionSdt.Location = new System.Drawing.Point(20, 84);
            this.lblCaptionSdt.Name = "lblCaptionSdt";
            this.lblCaptionSdt.Size = new System.Drawing.Size(140, 28);
            this.lblCaptionSdt.TabIndex = 4;
            this.lblCaptionSdt.Text = "SĐT liên hệ:";
            this.lblCaptionSdt.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblNguoiDaiDien
            // 
            this.lblNguoiDaiDien.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblNguoiDaiDien.Location = new System.Drawing.Point(165, 52);
            this.lblNguoiDaiDien.Name = "lblNguoiDaiDien";
            this.lblNguoiDaiDien.Size = new System.Drawing.Size(350, 28);
            this.lblNguoiDaiDien.TabIndex = 3;
            this.lblNguoiDaiDien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCaptionNguoiDaiDien
            // 
            this.lblCaptionNguoiDaiDien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCaptionNguoiDaiDien.Location = new System.Drawing.Point(20, 52);
            this.lblCaptionNguoiDaiDien.Name = "lblCaptionNguoiDaiDien";
            this.lblCaptionNguoiDaiDien.Size = new System.Drawing.Size(140, 28);
            this.lblCaptionNguoiDaiDien.TabIndex = 2;
            this.lblCaptionNguoiDaiDien.Text = "Người đại diện:";
            this.lblCaptionNguoiDaiDien.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblTenDoan
            // 
            this.lblTenDoan.Font = new System.Drawing.Font("Segoe UI Semibold", 11F, System.Drawing.FontStyle.Bold);
            this.lblTenDoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.lblTenDoan.Location = new System.Drawing.Point(165, 20);
            this.lblTenDoan.Name = "lblTenDoan";
            this.lblTenDoan.Size = new System.Drawing.Size(350, 28);
            this.lblTenDoan.TabIndex = 1;
            this.lblTenDoan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lblCaptionTenDoan
            // 
            this.lblCaptionTenDoan.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCaptionTenDoan.Location = new System.Drawing.Point(20, 20);
            this.lblCaptionTenDoan.Name = "lblCaptionTenDoan";
            this.lblCaptionTenDoan.Size = new System.Drawing.Size(140, 28);
            this.lblCaptionTenDoan.TabIndex = 0;
            this.lblCaptionTenDoan.Text = "Tên đoàn:";
            this.lblCaptionTenDoan.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmXuatVeDoan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(603, 490);
            this.Controls.Add(this.pnlBookingInfo);
            this.Controls.Add(this.pnlSearch);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmXuatVeDoan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Xuất Vé Khách Đoàn";
            this.Load += new System.EventHandler(this.frmXuatVeDoan_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlSearch.ResumeLayout(false);
            this.pnlBookingInfo.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnlSearch;
        private Guna.UI2.WinForms.Guna2TextBox txtBookingCode;
        private Guna.UI2.WinForms.Guna2Button btnTimDoan;
        private System.Windows.Forms.Panel pnlBookingInfo;
        private System.Windows.Forms.Label lblCaptionTenDoan;
        private System.Windows.Forms.Label lblTenDoan;
        private System.Windows.Forms.Label lblCaptionNguoiDaiDien;
        private System.Windows.Forms.Label lblNguoiDaiDien;
        private System.Windows.Forms.Label lblCaptionSdt;
        private System.Windows.Forms.Label lblSdt;
        private System.Windows.Forms.Label lblCaptionSoLuongVe;
        private System.Windows.Forms.Label lblSoLuongVe;
        private System.Windows.Forms.Label lblCaptionChietKhau;
        private System.Windows.Forms.Label lblChietKhau;
        private System.Windows.Forms.Label lblCaptionNgayDen;
        private System.Windows.Forms.Label lblNgayDen;
        private System.Windows.Forms.Label lblCaptionTrangThai;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.Label lblCaptionCombo;
        private System.Windows.Forms.Label lblCombo;
        private System.Windows.Forms.Label lblValidation;
        private Guna.UI2.WinForms.Guna2Button btnXuatVe;
    }
}
