namespace GUI
{
    partial class frmSuaKhachHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {

            this.pnlBody = new System.Windows.Forms.Panel();
            this.pnlFooter = new Guna.UI2.WinForms.Guna2Panel();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();

            // Fields
            this.lblMaKH = new System.Windows.Forms.Label();
            this.txtMaKH = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.txtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.cboGioiTinh = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.dtpNgaySinh = new DevExpress.XtraEditors.DateEdit();
            this.lblSDT = new System.Windows.Forms.Label();
            this.txtSDT = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblEmail = new System.Windows.Forms.Label();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblLoaiKhach = new System.Windows.Forms.Label();
            this.cboLoaiKhach = new Guna.UI2.WinForms.Guna2ComboBox();

            this.SuspendLayout();

            int y = 20;
            int lblX = 20;
            int txtX = 140;
            int txtW = 340;
            int rowH = 55;

            // ── Body ──
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Padding = new System.Windows.Forms.Padding(10);
            this.pnlBody.BackColor = System.Drawing.Color.White;

            // Row: Mã KH
            AddLabel(this.lblMaKH, "Mã KH:", lblX, y);
            this.txtMaKH.Location = new System.Drawing.Point(txtX, y);
            this.txtMaKH.Size = new System.Drawing.Size(txtW, 36);
            this.txtMaKH.Enabled = false;
            this.pnlBody.Controls.Add(this.txtMaKH);
            this.pnlBody.Controls.Add(this.lblMaKH);
            y += rowH;

            // Row: Họ Tên
            AddLabel(this.lblHoTen, "Họ tên *:", lblX, y);
            this.txtHoTen.Location = new System.Drawing.Point(txtX, y);
            this.txtHoTen.Size = new System.Drawing.Size(txtW, 36);
            this.txtHoTen.PlaceholderText = "Nguyễn Văn A";
            this.pnlBody.Controls.Add(this.txtHoTen);
            this.pnlBody.Controls.Add(this.lblHoTen);
            y += rowH;

            // Row: Giới tính
            AddLabel(this.lblGioiTinh, "Giới tính:", lblX, y);
            this.cboGioiTinh.Location = new System.Drawing.Point(txtX, y);
            this.cboGioiTinh.Size = new System.Drawing.Size(150, 36);
            this.cboGioiTinh.Items.AddRange(new object[] { "Nam", "Nữ", "Khác" });
            this.cboGioiTinh.SelectedIndex = 0;
            this.pnlBody.Controls.Add(this.cboGioiTinh);
            this.pnlBody.Controls.Add(this.lblGioiTinh);
            y += rowH;

            // Row: Ngày sinh
            AddLabel(this.lblNgaySinh, "Ngày sinh:", lblX, y);
            this.dtpNgaySinh.Location = new System.Drawing.Point(txtX, y);
            this.dtpNgaySinh.Size = new System.Drawing.Size(200, 36);
            this.pnlBody.Controls.Add(this.dtpNgaySinh);
            this.pnlBody.Controls.Add(this.lblNgaySinh);
            y += rowH;

            // Row: SĐT
            AddLabel(this.lblSDT, "SĐT *:", lblX, y);
            this.txtSDT.Location = new System.Drawing.Point(txtX, y);
            this.txtSDT.Size = new System.Drawing.Size(200, 36);
            this.txtSDT.PlaceholderText = "0909123456";
            this.pnlBody.Controls.Add(this.txtSDT);
            this.pnlBody.Controls.Add(this.lblSDT);
            y += rowH;

            // Row: Email
            AddLabel(this.lblEmail, "Email:", lblX, y);
            this.txtEmail.Location = new System.Drawing.Point(txtX, y);
            this.txtEmail.Size = new System.Drawing.Size(txtW, 36);
            this.txtEmail.PlaceholderText = "email@example.com";
            this.pnlBody.Controls.Add(this.txtEmail);
            this.pnlBody.Controls.Add(this.lblEmail);
            y += rowH;

            // Row: Địa chỉ
            AddLabel(this.lblDiaChi, "Địa chỉ:", lblX, y);
            this.txtDiaChi.Location = new System.Drawing.Point(txtX, y);
            this.txtDiaChi.Size = new System.Drawing.Size(txtW, 36);
            this.pnlBody.Controls.Add(this.txtDiaChi);
            this.pnlBody.Controls.Add(this.lblDiaChi);
            y += rowH;

            // Row: Loại khách
            AddLabel(this.lblLoaiKhach, "Loại KH:", lblX, y);
            this.cboLoaiKhach.Location = new System.Drawing.Point(txtX, y);
            this.cboLoaiKhach.Size = new System.Drawing.Size(200, 36);
            this.cboLoaiKhach.Items.AddRange(new object[] { "CaNhan", "HocSinhSinhVien", "VIP", "VVIP", "Doan", "DoanhNghiep", "NoiBo" });
            this.cboLoaiKhach.SelectedIndex = 0;
            this.pnlBody.Controls.Add(this.cboLoaiKhach);
            this.pnlBody.Controls.Add(this.lblLoaiKhach);

            // ── Footer ──
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Height = 55;
            this.pnlFooter.FillColor = System.Drawing.Color.FromArgb(248, 250, 252);
            this.pnlFooter.Padding = new System.Windows.Forms.Padding(15, 8, 15, 8);

            this.btnLuu.Text = "💾  LƯU";
            this.btnLuu.Size = new System.Drawing.Size(120, 38);
            this.btnLuu.Location = new System.Drawing.Point(365, 8);
            this.btnLuu.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI Semibold", 10f, System.Drawing.FontStyle.Bold);
            this.btnLuu.FillColor = System.Drawing.Color.FromArgb(30, 64, 175); // Blue (Primary)
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.BorderRadius = 4;
            this.btnLuu.Animated = true;
            this.btnLuu.Cursor = System.Windows.Forms.Cursors.Hand;

            this.btnHuy.Text = "HỦY";
            this.btnHuy.Size = new System.Drawing.Size(90, 38);
            this.btnHuy.Location = new System.Drawing.Point(265, 8);
            this.btnHuy.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI Semibold", 10f, System.Drawing.FontStyle.Bold);
            this.btnHuy.BorderRadius = 4;
            this.btnHuy.FillColor = System.Drawing.Color.FromArgb(241, 245, 249);
            this.btnHuy.ForeColor = System.Drawing.Color.FromArgb(15, 23, 42);
            this.btnHuy.Animated = true;
            this.btnHuy.Cursor = System.Windows.Forms.Cursors.Hand;

            this.pnlFooter.Controls.Add(this.btnHuy);
            this.pnlFooter.Controls.Add(this.btnLuu);

            // ── Form ──
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlFooter);

            this.ClientSize = new System.Drawing.Size(520, 520); // Adjusted size
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Name = "frmSuaKhachHang";
            this.Text = "Thông tin khách hàng";
            this.ResumeLayout(false);
        }

        private void AddLabel(System.Windows.Forms.Label lbl, string text, int x, int y)
        {
            lbl.Text = text;
            lbl.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5f);
            lbl.Location = new System.Drawing.Point(x, y + 8);
            lbl.AutoSize = true;
        }

        #endregion

        private System.Windows.Forms.Panel pnlBody;
        private Guna.UI2.WinForms.Guna2Panel pnlFooter;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;

        private System.Windows.Forms.Label lblMaKH;
        private Guna.UI2.WinForms.Guna2TextBox txtMaKH;
        private System.Windows.Forms.Label lblHoTen;
        private Guna.UI2.WinForms.Guna2TextBox txtHoTen;
        private System.Windows.Forms.Label lblGioiTinh;
        private Guna.UI2.WinForms.Guna2ComboBox cboGioiTinh;
        private System.Windows.Forms.Label lblNgaySinh;
        private DevExpress.XtraEditors.DateEdit dtpNgaySinh;
        private System.Windows.Forms.Label lblSDT;
        private Guna.UI2.WinForms.Guna2TextBox txtSDT;
        private System.Windows.Forms.Label lblEmail;
        private Guna.UI2.WinForms.Guna2TextBox txtEmail;
        private System.Windows.Forms.Label lblDiaChi;
        private Guna.UI2.WinForms.Guna2TextBox txtDiaChi;
        private System.Windows.Forms.Label lblLoaiKhach;
        private Guna.UI2.WinForms.Guna2ComboBox cboLoaiKhach;
    }
}
