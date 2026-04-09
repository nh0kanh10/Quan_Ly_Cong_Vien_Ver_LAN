namespace GUI
{
    partial class frmNhaCungCap
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
            this.pnlMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.gbDanhSach = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlTimKiem = new Guna.UI2.WinForms.Guna2Panel();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnTimKiem = new Guna.UI2.WinForms.Guna2Button();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.gbThongTin = new System.Windows.Forms.Panel();
            this.gbThongTinNhaCungCap = new Guna.UI2.WinForms.Guna2GroupBox();
            this.txtDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.txtNguoiLienHe = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNguoiLienHe = new System.Windows.Forms.Label();
            this.txtDienThoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDienThoai = new System.Windows.Forms.Label();
            this.txtMaSoThue = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMaSoThue = new System.Windows.Forms.Label();
            this.txtTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTen = new System.Windows.Forms.Label();
            this.pnlSpacer = new Guna.UI2.WinForms.Guna2Panel();
            this.gbChucNang = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel1)).BeginInit();
            this.pnlMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel2)).BeginInit();
            this.pnlMain.Panel2.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.gbDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.pnlTimKiem.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.gbThongTin.SuspendLayout();
            this.gbThongTinNhaCungCap.SuspendLayout();
            this.gbChucNang.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            // 
            // pnlMain.Panel1
            // 
            this.pnlMain.Panel1.Controls.Add(this.pnlLeft);
            // 
            // pnlMain.Panel2
            // 
            this.pnlMain.Panel2.Controls.Add(this.pnlRight);
            this.pnlMain.ShowSplitGlyph = DevExpress.Utils.DefaultBoolean.True;
            this.pnlMain.Size = new System.Drawing.Size(1300, 750);
            this.pnlMain.SplitterPosition = 399;
            this.pnlMain.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gbDanhSach);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(891, 750);
            this.pnlLeft.TabIndex = 0;
            // 
            // gbDanhSach
            // 
            this.gbDanhSach.Controls.Add(this.gridControl);
            this.gbDanhSach.Controls.Add(this.pnlTimKiem);
            this.gbDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDanhSach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbDanhSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbDanhSach.Location = new System.Drawing.Point(0, 0);
            this.gbDanhSach.Name = "gbDanhSach";
            this.gbDanhSach.Size = new System.Drawing.Size(891, 750);
            this.gbDanhSach.TabIndex = 0;
            this.gbDanhSach.Text = "Danh sách";
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 90);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(891, 660);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            // 
            // pnlTimKiem
            // 
            this.pnlTimKiem.Controls.Add(this.txtTimKiem);
            this.pnlTimKiem.Controls.Add(this.btnTimKiem);
            this.pnlTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimKiem.Location = new System.Drawing.Point(0, 40);
            this.pnlTimKiem.Name = "pnlTimKiem";
            this.pnlTimKiem.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.pnlTimKiem.Size = new System.Drawing.Size(891, 50);
            this.pnlTimKiem.TabIndex = 1;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderRadius = 18;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Location = new System.Drawing.Point(161, 7);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Nhập từ khóa cần tìm...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(503, 36);
            this.txtTimKiem.TabIndex = 0;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(710, 8);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(110, 34);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.Visible = false;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.gbThongTin);
            this.pnlRight.Controls.Add(this.pnlSpacer);
            this.pnlRight.Controls.Add(this.gbChucNang);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(0, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(15);
            this.pnlRight.Size = new System.Drawing.Size(399, 750);
            this.pnlRight.TabIndex = 1;
            // 
            // gbThongTin
            // 
            this.gbThongTin.AutoScroll = true;
            this.gbThongTin.Controls.Add(this.gbThongTinNhaCungCap);
            this.gbThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThongTin.Location = new System.Drawing.Point(15, 15);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(369, 540);
            this.gbThongTin.TabIndex = 0;
            // 
            // gbThongTinNhaCungCap
            // 
            this.gbThongTinNhaCungCap.Controls.Add(this.txtDiaChi);
            this.gbThongTinNhaCungCap.Controls.Add(this.lblDiaChi);
            this.gbThongTinNhaCungCap.Controls.Add(this.txtNguoiLienHe);
            this.gbThongTinNhaCungCap.Controls.Add(this.lblNguoiLienHe);
            this.gbThongTinNhaCungCap.Controls.Add(this.txtDienThoai);
            this.gbThongTinNhaCungCap.Controls.Add(this.lblDienThoai);
            this.gbThongTinNhaCungCap.Controls.Add(this.txtMaSoThue);
            this.gbThongTinNhaCungCap.Controls.Add(this.lblMaSoThue);
            this.gbThongTinNhaCungCap.Controls.Add(this.txtTen);
            this.gbThongTinNhaCungCap.Controls.Add(this.lblTen);
            this.gbThongTinNhaCungCap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThongTinNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.gbThongTinNhaCungCap.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.gbThongTinNhaCungCap.Location = new System.Drawing.Point(0, 0);
            this.gbThongTinNhaCungCap.Name = "gbThongTinNhaCungCap";
            this.gbThongTinNhaCungCap.Size = new System.Drawing.Size(369, 540);
            this.gbThongTinNhaCungCap.TabIndex = 0;
            this.gbThongTinNhaCungCap.Text = "Thông Tin Trọng Yếu";
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.BorderRadius = 5;
            this.txtDiaChi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiaChi.DefaultText = "";
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDiaChi.Location = new System.Drawing.Point(30, 360);
            this.txtDiaChi.Multiline = true;
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.PlaceholderText = "";
            this.txtDiaChi.SelectedText = "";
            this.txtDiaChi.Size = new System.Drawing.Size(312, 119);
            this.txtDiaChi.TabIndex = 9;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.AutoSize = true;
            this.lblDiaChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDiaChi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDiaChi.Location = new System.Drawing.Point(30, 340);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(46, 15);
            this.lblDiaChi.TabIndex = 8;
            this.lblDiaChi.Text = "Địa chỉ:";
            // 
            // txtNguoiLienHe
            // 
            this.txtNguoiLienHe.BorderRadius = 5;
            this.txtNguoiLienHe.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNguoiLienHe.DefaultText = "";
            this.txtNguoiLienHe.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtNguoiLienHe.Location = new System.Drawing.Point(30, 290);
            this.txtNguoiLienHe.Name = "txtNguoiLienHe";
            this.txtNguoiLienHe.PlaceholderText = "";
            this.txtNguoiLienHe.SelectedText = "";
            this.txtNguoiLienHe.Size = new System.Drawing.Size(312, 31);
            this.txtNguoiLienHe.TabIndex = 7;
            // 
            // lblNguoiLienHe
            // 
            this.lblNguoiLienHe.AutoSize = true;
            this.lblNguoiLienHe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNguoiLienHe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblNguoiLienHe.Location = new System.Drawing.Point(30, 270);
            this.lblNguoiLienHe.Name = "lblNguoiLienHe";
            this.lblNguoiLienHe.Size = new System.Drawing.Size(81, 15);
            this.lblNguoiLienHe.TabIndex = 6;
            this.lblNguoiLienHe.Text = "Người liên hệ:";
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.BorderRadius = 5;
            this.txtDienThoai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDienThoai.DefaultText = "";
            this.txtDienThoai.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtDienThoai.Location = new System.Drawing.Point(30, 220);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.PlaceholderText = "";
            this.txtDienThoai.SelectedText = "";
            this.txtDienThoai.Size = new System.Drawing.Size(312, 31);
            this.txtDienThoai.TabIndex = 5;
            // 
            // lblDienThoai
            // 
            this.lblDienThoai.AutoSize = true;
            this.lblDienThoai.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDienThoai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDienThoai.Location = new System.Drawing.Point(30, 200);
            this.lblDienThoai.Name = "lblDienThoai";
            this.lblDienThoai.Size = new System.Drawing.Size(64, 15);
            this.lblDienThoai.TabIndex = 4;
            this.lblDienThoai.Text = "Điện thoại:";
            // 
            // txtMaSoThue
            // 
            this.txtMaSoThue.BorderRadius = 5;
            this.txtMaSoThue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaSoThue.DefaultText = "";
            this.txtMaSoThue.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtMaSoThue.Location = new System.Drawing.Point(30, 150);
            this.txtMaSoThue.Name = "txtMaSoThue";
            this.txtMaSoThue.PlaceholderText = "";
            this.txtMaSoThue.SelectedText = "";
            this.txtMaSoThue.Size = new System.Drawing.Size(312, 31);
            this.txtMaSoThue.TabIndex = 3;
            // 
            // lblMaSoThue
            // 
            this.lblMaSoThue.AutoSize = true;
            this.lblMaSoThue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaSoThue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMaSoThue.Location = new System.Drawing.Point(30, 130);
            this.lblMaSoThue.Name = "lblMaSoThue";
            this.lblMaSoThue.Size = new System.Drawing.Size(69, 15);
            this.lblMaSoThue.TabIndex = 2;
            this.lblMaSoThue.Text = "Mã số thuế:";
            // 
            // txtTen
            // 
            this.txtTen.BorderRadius = 5;
            this.txtTen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTen.DefaultText = "";
            this.txtTen.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.txtTen.Location = new System.Drawing.Point(30, 80);
            this.txtTen.Name = "txtTen";
            this.txtTen.PlaceholderText = "";
            this.txtTen.SelectedText = "";
            this.txtTen.Size = new System.Drawing.Size(312, 31);
            this.txtTen.TabIndex = 1;
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTen.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTen.Location = new System.Drawing.Point(30, 60);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(108, 15);
            this.lblTen.TabIndex = 0;
            this.lblTen.Text = "Tên nhà cung cấp:*";
            // 
            // pnlSpacer
            // 
            this.pnlSpacer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSpacer.Location = new System.Drawing.Point(15, 555);
            this.pnlSpacer.Name = "pnlSpacer";
            this.pnlSpacer.Size = new System.Drawing.Size(369, 15);
            this.pnlSpacer.TabIndex = 1;
            // 
            // gbChucNang
            // 
            this.gbChucNang.Controls.Add(this.btnThem);
            this.gbChucNang.Controls.Add(this.btnSua);
            this.gbChucNang.Controls.Add(this.btnXoa);
            this.gbChucNang.Controls.Add(this.btnLamMoi);
            this.gbChucNang.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbChucNang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbChucNang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbChucNang.Location = new System.Drawing.Point(15, 570);
            this.gbChucNang.Name = "gbChucNang";
            this.gbChucNang.Size = new System.Drawing.Size(369, 165);
            this.gbChucNang.TabIndex = 2;
            this.gbChucNang.Text = "Chức năng";
            this.gbChucNang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(17, 55);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(147, 40);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm mới";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(195, 55);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(147, 40);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Cập nhật";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(17, 105);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(147, 40);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(195, 105);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(147, 40);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // frmNhaCungCap
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1300, 750);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmNhaCungCap";
            this.Text = "frmNhaCungCap";
            this.Load += new System.EventHandler(this.frmNhaCungCap_Load);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.OnFocusedRowChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel1)).EndInit();
            this.pnlMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel2)).EndInit();
            this.pnlMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.gbDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.pnlTimKiem.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTinNhaCungCap.ResumeLayout(false);
            this.gbThongTinNhaCungCap.PerformLayout();
            this.gbChucNang.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion


        private Guna.UI2.WinForms.Guna2GroupBox gbThongTinNhaCungCap;
        private System.Windows.Forms.Label lblTen;
        private Guna.UI2.WinForms.Guna2TextBox txtTen;
        private System.Windows.Forms.Label lblMaSoThue;
        private Guna.UI2.WinForms.Guna2TextBox txtMaSoThue;
        private System.Windows.Forms.Label lblDienThoai;
        private Guna.UI2.WinForms.Guna2TextBox txtDienThoai;
        private System.Windows.Forms.Label lblNguoiLienHe;
        private Guna.UI2.WinForms.Guna2TextBox txtNguoiLienHe;
        private System.Windows.Forms.Label lblDiaChi;
        private Guna.UI2.WinForms.Guna2TextBox txtDiaChi;

        protected internal DevExpress.XtraEditors.SplitContainerControl pnlMain;
        protected internal Guna.UI2.WinForms.Guna2Panel pnlLeft;
        protected internal Guna.UI2.WinForms.Guna2GroupBox gbDanhSach;
        protected internal DevExpress.XtraGrid.GridControl gridControl;
        protected internal DevExpress.XtraGrid.Views.Grid.GridView gridView;
        protected internal Guna.UI2.WinForms.Guna2Panel pnlTimKiem;
        protected internal Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        protected internal Guna.UI2.WinForms.Guna2Button btnTimKiem;
        protected internal Guna.UI2.WinForms.Guna2Panel pnlRight;
        protected internal System.Windows.Forms.Panel gbThongTin;
        protected internal Guna.UI2.WinForms.Guna2Panel pnlSpacer;
        protected internal Guna.UI2.WinForms.Guna2GroupBox gbChucNang;
        protected internal Guna.UI2.WinForms.Guna2Button btnThem;
        protected internal Guna.UI2.WinForms.Guna2Button btnSua;
        protected internal Guna.UI2.WinForms.Guna2Button btnXoa;
        protected internal Guna.UI2.WinForms.Guna2Button btnLamMoi;
    }
}










