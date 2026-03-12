namespace WindowsFormsApp1
{
    partial class frmLoaiVe
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
            this.dgvLoaiVe = new System.Windows.Forms.DataGridView();
            this.pnlTimKiem = new System.Windows.Forms.Panel();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbThongTin = new System.Windows.Forms.GroupBox();
            this.chkLaCombo = new System.Windows.Forms.CheckBox();
            this.txtGiaCuoiTuan = new System.Windows.Forms.TextBox();
            this.lblGiaCuoiTuan = new System.Windows.Forms.Label();
            this.txtGiaVe = new System.Windows.Forms.TextBox();
            this.lblGiaVe = new System.Windows.Forms.Label();
            this.cboDoiTuong = new System.Windows.Forms.ComboBox();
            this.lblDoiTuong = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.dtpNgayTao = new System.Windows.Forms.DateTimePicker();
            this.lblNgayTao = new System.Windows.Forms.Label();
            this.dtpNgayCapNhat = new System.Windows.Forms.DateTimePicker();
            this.lblNgayCapNhat = new System.Windows.Forms.Label();
            this.txtMaCode = new System.Windows.Forms.TextBox();
            this.lblMaCode = new System.Windows.Forms.Label();
            this.txtMaLoaiVe = new System.Windows.Forms.TextBox();
            this.lblMaLoaiVe = new System.Windows.Forms.Label();
            this.txtTenLoaiVe = new System.Windows.Forms.TextBox();
            this.lblTenLoaiVe = new System.Windows.Forms.Label();
            this.pnlChucNang = new System.Windows.Forms.Panel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            this.pnlCombo = new System.Windows.Forms.Panel();
            this.gbCombo = new System.Windows.Forms.GroupBox();
            this.dgvVeCon = new System.Windows.Forms.DataGridView();
            this.pnlThemVeCon = new System.Windows.Forms.Panel();
            this.btnXoaVeCon = new System.Windows.Forms.Button();
            this.btnThemVeCon = new System.Windows.Forms.Button();
            this.nudSoLuot = new System.Windows.Forms.NumericUpDown();
            this.lblSoLuot = new System.Windows.Forms.Label();
            this.cboVeConChon = new System.Windows.Forms.ComboBox();
            this.lblChonVeCon = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiVe)).BeginInit();
            this.pnlTimKiem.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbThongTin.SuspendLayout();
            this.pnlChucNang.SuspendLayout();
            this.pnlCombo.SuspendLayout();
            this.gbCombo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvVeCon)).BeginInit();
            this.pnlThemVeCon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuot)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvLoaiVe
            // 
            this.dgvLoaiVe.AllowUserToAddRows = false;
            this.dgvLoaiVe.AllowUserToDeleteRows = false;
            this.dgvLoaiVe.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvLoaiVe.BackgroundColor = System.Drawing.Color.White;
            this.dgvLoaiVe.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvLoaiVe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvLoaiVe.Location = new System.Drawing.Point(0, 346);
            this.dgvLoaiVe.MultiSelect = false;
            this.dgvLoaiVe.Name = "dgvLoaiVe";
            this.dgvLoaiVe.ReadOnly = true;
            this.dgvLoaiVe.RowHeadersWidth = 51;
            this.dgvLoaiVe.RowTemplate.Height = 24;
            this.dgvLoaiVe.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvLoaiVe.Size = new System.Drawing.Size(1400, 400);
            this.dgvLoaiVe.TabIndex = 4;
            this.dgvLoaiVe.Click += new System.EventHandler(this.dgvLoaiVe_Click);
            // 
            // pnlTimKiem
            // 
            this.pnlTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlTimKiem.Controls.Add(this.txtTimKiem);
            this.pnlTimKiem.Controls.Add(this.btnTimKiem);
            this.pnlTimKiem.Controls.Add(this.lblTimKiem);
            this.pnlTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimKiem.Location = new System.Drawing.Point(0, 280);
            this.pnlTimKiem.Name = "pnlTimKiem";
            this.pnlTimKiem.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTimKiem.Size = new System.Drawing.Size(1400, 66);
            this.pnlTimKiem.TabIndex = 5;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(100, 18);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(400, 22);
            this.txtTimKiem.TabIndex = 0;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(520, 14);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 32);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "   Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // lblTimKiem
            // 
            this.lblTimKiem.AutoSize = true;
            this.lblTimKiem.Location = new System.Drawing.Point(20, 20);
            this.lblTimKiem.Name = "lblTimKiem";
            this.lblTimKiem.Size = new System.Drawing.Size(65, 16);
            this.lblTimKiem.TabIndex = 2;
            this.lblTimKiem.Text = "Tìm kiếm:";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.gbThongTin);
            this.panel1.Controls.Add(this.pnlChucNang);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Padding = new System.Windows.Forms.Padding(10);
            this.panel1.Size = new System.Drawing.Size(1400, 280);
            this.panel1.TabIndex = 6;
            // 
            // gbThongTin
            // 
            this.gbThongTin.Controls.Add(this.chkLaCombo);
            this.gbThongTin.Controls.Add(this.txtGiaCuoiTuan);
            this.gbThongTin.Controls.Add(this.lblGiaCuoiTuan);
            this.gbThongTin.Controls.Add(this.txtGiaVe);
            this.gbThongTin.Controls.Add(this.lblGiaVe);
            this.gbThongTin.Controls.Add(this.cboDoiTuong);
            this.gbThongTin.Controls.Add(this.lblDoiTuong);
            this.gbThongTin.Controls.Add(this.cboTrangThai);
            this.gbThongTin.Controls.Add(this.lblTrangThai);
            this.gbThongTin.Controls.Add(this.dtpNgayTao);
            this.gbThongTin.Controls.Add(this.lblNgayTao);
            this.gbThongTin.Controls.Add(this.dtpNgayCapNhat);
            this.gbThongTin.Controls.Add(this.lblNgayCapNhat);
            this.gbThongTin.Controls.Add(this.txtMaCode);
            this.gbThongTin.Controls.Add(this.lblMaCode);
            this.gbThongTin.Controls.Add(this.txtMaLoaiVe);
            this.gbThongTin.Controls.Add(this.lblMaLoaiVe);
            this.gbThongTin.Controls.Add(this.txtTenLoaiVe);
            this.gbThongTin.Controls.Add(this.lblTenLoaiVe);
            this.gbThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThongTin.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbThongTin.Location = new System.Drawing.Point(10, 10);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(1180, 260);
            this.gbThongTin.TabIndex = 0;
            this.gbThongTin.TabStop = false;
            this.gbThongTin.Text = "Thông tin loại vé";
            // 
            // chkLaCombo
            // 
            this.chkLaCombo.AutoSize = true;
            this.chkLaCombo.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.chkLaCombo.Location = new System.Drawing.Point(148, 200);
            this.chkLaCombo.Name = "chkLaCombo";
            this.chkLaCombo.Size = new System.Drawing.Size(100, 27);
            this.chkLaCombo.TabIndex = 20;
            this.chkLaCombo.Text = "Là Combo";
            this.chkLaCombo.CheckedChanged += new System.EventHandler(this.chkLaCombo_CheckedChanged);
            // 
            // txtGiaCuoiTuan
            // 
            this.txtGiaCuoiTuan.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtGiaCuoiTuan.Location = new System.Drawing.Point(560, 120);
            this.txtGiaCuoiTuan.Name = "txtGiaCuoiTuan";
            this.txtGiaCuoiTuan.Size = new System.Drawing.Size(218, 30);
            this.txtGiaCuoiTuan.TabIndex = 19;
            // 
            // lblGiaCuoiTuan
            // 
            this.lblGiaCuoiTuan.AutoSize = true;
            this.lblGiaCuoiTuan.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblGiaCuoiTuan.Location = new System.Drawing.Point(412, 123);
            this.lblGiaCuoiTuan.Name = "lblGiaCuoiTuan";
            this.lblGiaCuoiTuan.Size = new System.Drawing.Size(118, 23);
            this.lblGiaCuoiTuan.TabIndex = 18;
            this.lblGiaCuoiTuan.Text = "Giá cuối tuần:";
            // 
            // txtGiaVe
            // 
            this.txtGiaVe.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtGiaVe.Location = new System.Drawing.Point(148, 120);
            this.txtGiaVe.Name = "txtGiaVe";
            this.txtGiaVe.Size = new System.Drawing.Size(218, 30);
            this.txtGiaVe.TabIndex = 17;
            // 
            // lblGiaVe
            // 
            this.lblGiaVe.AutoSize = true;
            this.lblGiaVe.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblGiaVe.Location = new System.Drawing.Point(12, 123);
            this.lblGiaVe.Name = "lblGiaVe";
            this.lblGiaVe.Size = new System.Drawing.Size(60, 23);
            this.lblGiaVe.TabIndex = 16;
            this.lblGiaVe.Text = "Giá vé:";
            // 
            // cboDoiTuong
            // 
            this.cboDoiTuong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDoiTuong.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cboDoiTuong.FormattingEnabled = true;
            this.cboDoiTuong.Location = new System.Drawing.Point(560, 43);
            this.cboDoiTuong.Name = "cboDoiTuong";
            this.cboDoiTuong.Size = new System.Drawing.Size(218, 31);
            this.cboDoiTuong.TabIndex = 15;
            // 
            // lblDoiTuong
            // 
            this.lblDoiTuong.AutoSize = true;
            this.lblDoiTuong.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblDoiTuong.Location = new System.Drawing.Point(412, 47);
            this.lblDoiTuong.Name = "lblDoiTuong";
            this.lblDoiTuong.Size = new System.Drawing.Size(91, 23);
            this.lblDoiTuong.TabIndex = 14;
            this.lblDoiTuong.Text = "Đối tượng:";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(560, 160);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(218, 31);
            this.cboTrangThai.TabIndex = 13;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblTrangThai.Location = new System.Drawing.Point(412, 164);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(91, 23);
            this.lblTrangThai.TabIndex = 12;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // dtpNgayTao
            // 
            this.dtpNgayTao.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dtpNgayTao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayTao.Location = new System.Drawing.Point(960, 43);
            this.dtpNgayTao.Name = "dtpNgayTao";
            this.dtpNgayTao.Size = new System.Drawing.Size(180, 30);
            this.dtpNgayTao.TabIndex = 11;
            // 
            // lblNgayTao
            // 
            this.lblNgayTao.AutoSize = true;
            this.lblNgayTao.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblNgayTao.Location = new System.Drawing.Point(840, 47);
            this.lblNgayTao.Name = "lblNgayTao";
            this.lblNgayTao.Size = new System.Drawing.Size(84, 23);
            this.lblNgayTao.TabIndex = 10;
            this.lblNgayTao.Text = "Ngày tạo:";
            // 
            // dtpNgayCapNhat
            // 
            this.dtpNgayCapNhat.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.dtpNgayCapNhat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayCapNhat.Location = new System.Drawing.Point(960, 120);
            this.dtpNgayCapNhat.Name = "dtpNgayCapNhat";
            this.dtpNgayCapNhat.Size = new System.Drawing.Size(180, 30);
            this.dtpNgayCapNhat.TabIndex = 9;
            // 
            // lblNgayCapNhat
            // 
            this.lblNgayCapNhat.AutoSize = true;
            this.lblNgayCapNhat.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblNgayCapNhat.Location = new System.Drawing.Point(840, 123);
            this.lblNgayCapNhat.Name = "lblNgayCapNhat";
            this.lblNgayCapNhat.Size = new System.Drawing.Size(122, 23);
            this.lblNgayCapNhat.TabIndex = 8;
            this.lblNgayCapNhat.Text = "Ngày cập nhật:";
            // 
            // txtMaCode
            // 
            this.txtMaCode.Enabled = false;
            this.txtMaCode.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtMaCode.Location = new System.Drawing.Point(148, 160);
            this.txtMaCode.Name = "txtMaCode";
            this.txtMaCode.Size = new System.Drawing.Size(218, 30);
            this.txtMaCode.TabIndex = 7;
            // 
            // lblMaCode
            // 
            this.lblMaCode.AutoSize = true;
            this.lblMaCode.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblMaCode.Location = new System.Drawing.Point(12, 163);
            this.lblMaCode.Name = "lblMaCode";
            this.lblMaCode.Size = new System.Drawing.Size(80, 23);
            this.lblMaCode.TabIndex = 6;
            this.lblMaCode.Text = "Mã code:";
            // 
            // txtMaLoaiVe
            // 
            this.txtMaLoaiVe.Enabled = false;
            this.txtMaLoaiVe.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtMaLoaiVe.Location = new System.Drawing.Point(148, 40);
            this.txtMaLoaiVe.Name = "txtMaLoaiVe";
            this.txtMaLoaiVe.Size = new System.Drawing.Size(218, 30);
            this.txtMaLoaiVe.TabIndex = 1;
            // 
            // lblMaLoaiVe
            // 
            this.lblMaLoaiVe.AutoSize = true;
            this.lblMaLoaiVe.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblMaLoaiVe.Location = new System.Drawing.Point(12, 43);
            this.lblMaLoaiVe.Name = "lblMaLoaiVe";
            this.lblMaLoaiVe.Size = new System.Drawing.Size(96, 23);
            this.lblMaLoaiVe.TabIndex = 0;
            this.lblMaLoaiVe.Text = "Mã loại vé:";
            // 
            // txtTenLoaiVe
            // 
            this.txtTenLoaiVe.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.txtTenLoaiVe.Location = new System.Drawing.Point(148, 80);
            this.txtTenLoaiVe.Name = "txtTenLoaiVe";
            this.txtTenLoaiVe.Size = new System.Drawing.Size(218, 30);
            this.txtTenLoaiVe.TabIndex = 3;
            // 
            // lblTenLoaiVe
            // 
            this.lblTenLoaiVe.AutoSize = true;
            this.lblTenLoaiVe.Font = new System.Drawing.Font("Segoe UI", 10.2F);
            this.lblTenLoaiVe.Location = new System.Drawing.Point(12, 83);
            this.lblTenLoaiVe.Name = "lblTenLoaiVe";
            this.lblTenLoaiVe.Size = new System.Drawing.Size(106, 23);
            this.lblTenLoaiVe.TabIndex = 2;
            this.lblTenLoaiVe.Text = "Tên loại vé:";
            // 
            // pnlChucNang
            // 
            this.pnlChucNang.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlChucNang.Controls.Add(this.btnThoat);
            this.pnlChucNang.Controls.Add(this.btnLamMoi);
            this.pnlChucNang.Controls.Add(this.btnXoa);
            this.pnlChucNang.Controls.Add(this.btnSua);
            this.pnlChucNang.Controls.Add(this.btnThem);
            this.pnlChucNang.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlChucNang.Location = new System.Drawing.Point(1190, 10);
            this.pnlChucNang.Name = "pnlChucNang";
            this.pnlChucNang.Size = new System.Drawing.Size(200, 260);
            this.pnlChucNang.TabIndex = 1;
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(20, 210);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(160, 40);
            this.btnThoat.TabIndex = 4;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(20, 160);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(160, 40);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(193)))), ((int)(((byte)(7)))));
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.Black;
            this.btnXoa.Location = new System.Drawing.Point(20, 110);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(160, 40);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(20, 60);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(160, 40);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(20, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(160, 40);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // pnlCombo — panel chi tiết combo, ẩn mặc định
            // 
            this.pnlCombo.Controls.Add(this.gbCombo);
            this.pnlCombo.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCombo.Location = new System.Drawing.Point(0, 550);
            this.pnlCombo.Name = "pnlCombo";
            this.pnlCombo.Size = new System.Drawing.Size(1400, 250);
            this.pnlCombo.TabIndex = 7;
            this.pnlCombo.Visible = false;
            // 
            // gbCombo
            // 
            this.gbCombo.Controls.Add(this.dgvVeCon);
            this.gbCombo.Controls.Add(this.pnlThemVeCon);
            this.gbCombo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbCombo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.gbCombo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.gbCombo.Location = new System.Drawing.Point(0, 0);
            this.gbCombo.Name = "gbCombo";
            this.gbCombo.Padding = new System.Windows.Forms.Padding(10);
            this.gbCombo.Size = new System.Drawing.Size(1400, 250);
            this.gbCombo.TabIndex = 0;
            this.gbCombo.TabStop = false;
            this.gbCombo.Text = "Chi tiết Combo — Danh sách vé con";
            // 
            // dgvVeCon
            // 
            this.dgvVeCon.AllowUserToAddRows = false;
            this.dgvVeCon.AllowUserToDeleteRows = false;
            this.dgvVeCon.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvVeCon.BackgroundColor = System.Drawing.Color.White;
            this.dgvVeCon.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvVeCon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvVeCon.Location = new System.Drawing.Point(10, 60);
            this.dgvVeCon.MultiSelect = false;
            this.dgvVeCon.Name = "dgvVeCon";
            this.dgvVeCon.ReadOnly = true;
            this.dgvVeCon.RowHeadersWidth = 30;
            this.dgvVeCon.RowTemplate.Height = 24;
            this.dgvVeCon.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvVeCon.Size = new System.Drawing.Size(1380, 180);
            this.dgvVeCon.TabIndex = 1;
            // 
            // pnlThemVeCon
            // 
            this.pnlThemVeCon.Controls.Add(this.btnXoaVeCon);
            this.pnlThemVeCon.Controls.Add(this.btnThemVeCon);
            this.pnlThemVeCon.Controls.Add(this.nudSoLuot);
            this.pnlThemVeCon.Controls.Add(this.lblSoLuot);
            this.pnlThemVeCon.Controls.Add(this.cboVeConChon);
            this.pnlThemVeCon.Controls.Add(this.lblChonVeCon);
            this.pnlThemVeCon.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlThemVeCon.Location = new System.Drawing.Point(10, 26);
            this.pnlThemVeCon.Name = "pnlThemVeCon";
            this.pnlThemVeCon.Size = new System.Drawing.Size(1380, 34);
            this.pnlThemVeCon.TabIndex = 0;
            // 
            // btnXoaVeCon
            // 
            this.btnXoaVeCon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(53)))), ((int)(((byte)(69)))));
            this.btnXoaVeCon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoaVeCon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoaVeCon.ForeColor = System.Drawing.Color.White;
            this.btnXoaVeCon.Location = new System.Drawing.Point(760, 2);
            this.btnXoaVeCon.Name = "btnXoaVeCon";
            this.btnXoaVeCon.Size = new System.Drawing.Size(100, 30);
            this.btnXoaVeCon.TabIndex = 5;
            this.btnXoaVeCon.Text = "Xóa vé con";
            this.btnXoaVeCon.UseVisualStyleBackColor = false;
            this.btnXoaVeCon.Click += new System.EventHandler(this.btnXoaVeCon_Click);
            // 
            // btnThemVeCon
            // 
            this.btnThemVeCon.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(167)))), ((int)(((byte)(69)))));
            this.btnThemVeCon.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThemVeCon.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThemVeCon.ForeColor = System.Drawing.Color.White;
            this.btnThemVeCon.Location = new System.Drawing.Point(640, 2);
            this.btnThemVeCon.Name = "btnThemVeCon";
            this.btnThemVeCon.Size = new System.Drawing.Size(110, 30);
            this.btnThemVeCon.TabIndex = 4;
            this.btnThemVeCon.Text = "Thêm vé con";
            this.btnThemVeCon.UseVisualStyleBackColor = false;
            this.btnThemVeCon.Click += new System.EventHandler(this.btnThemVeCon_Click);
            // 
            // nudSoLuot
            // 
            this.nudSoLuot.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.nudSoLuot.Location = new System.Drawing.Point(530, 4);
            this.nudSoLuot.Maximum = new decimal(new int[] { 999, 0, 0, 0 });
            this.nudSoLuot.Minimum = new decimal(new int[] { 1, 0, 0, -2147483648 });
            this.nudSoLuot.Name = "nudSoLuot";
            this.nudSoLuot.Size = new System.Drawing.Size(80, 29);
            this.nudSoLuot.TabIndex = 3;
            this.nudSoLuot.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblSoLuot
            // 
            this.lblSoLuot.AutoSize = true;
            this.lblSoLuot.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblSoLuot.ForeColor = System.Drawing.Color.Black;
            this.lblSoLuot.Location = new System.Drawing.Point(440, 7);
            this.lblSoLuot.Name = "lblSoLuot";
            this.lblSoLuot.Size = new System.Drawing.Size(60, 23);
            this.lblSoLuot.TabIndex = 2;
            this.lblSoLuot.Text = "Số lượt:";
            // 
            // cboVeConChon
            // 
            this.cboVeConChon.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboVeConChon.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.cboVeConChon.FormattingEnabled = true;
            this.cboVeConChon.Location = new System.Drawing.Point(110, 3);
            this.cboVeConChon.Name = "cboVeConChon";
            this.cboVeConChon.Size = new System.Drawing.Size(310, 29);
            this.cboVeConChon.TabIndex = 1;
            // 
            // lblChonVeCon
            // 
            this.lblChonVeCon.AutoSize = true;
            this.lblChonVeCon.Font = new System.Drawing.Font("Segoe UI", 9.75F);
            this.lblChonVeCon.ForeColor = System.Drawing.Color.Black;
            this.lblChonVeCon.Location = new System.Drawing.Point(5, 7);
            this.lblChonVeCon.Name = "lblChonVeCon";
            this.lblChonVeCon.Size = new System.Drawing.Size(98, 23);
            this.lblChonVeCon.TabIndex = 0;
            this.lblChonVeCon.Text = "Chọn vé con:";
            // 
            // frmLoaiVe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 800);
            this.Controls.Add(this.dgvLoaiVe);
            this.Controls.Add(this.pnlCombo);
            this.Controls.Add(this.pnlTimKiem);
            this.Controls.Add(this.panel1);
            this.Name = "frmLoaiVe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lý loại vé";
            this.Load += new System.EventHandler(this.frmLoaiVe_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvLoaiVe)).EndInit();
            this.pnlTimKiem.ResumeLayout(false);
            this.pnlTimKiem.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            this.pnlChucNang.ResumeLayout(false);
            this.pnlCombo.ResumeLayout(false);
            this.gbCombo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvVeCon)).EndInit();
            this.pnlThemVeCon.ResumeLayout(false);
            this.pnlThemVeCon.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.nudSoLuot)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.DataGridView dgvLoaiVe;
        private System.Windows.Forms.Panel pnlTimKiem;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbThongTin;
        private System.Windows.Forms.CheckBox chkLaCombo;
        private System.Windows.Forms.TextBox txtGiaCuoiTuan;
        private System.Windows.Forms.Label lblGiaCuoiTuan;
        private System.Windows.Forms.TextBox txtGiaVe;
        private System.Windows.Forms.Label lblGiaVe;
        private System.Windows.Forms.ComboBox cboDoiTuong;
        private System.Windows.Forms.Label lblDoiTuong;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.DateTimePicker dtpNgayTao;
        private System.Windows.Forms.Label lblNgayTao;
        private System.Windows.Forms.DateTimePicker dtpNgayCapNhat;
        private System.Windows.Forms.Label lblNgayCapNhat;
        private System.Windows.Forms.TextBox txtMaCode;
        private System.Windows.Forms.Label lblMaCode;
        private System.Windows.Forms.TextBox txtMaLoaiVe;
        private System.Windows.Forms.Label lblMaLoaiVe;
        private System.Windows.Forms.TextBox txtTenLoaiVe;
        private System.Windows.Forms.Label lblTenLoaiVe;
        private System.Windows.Forms.Panel pnlChucNang;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.Panel pnlCombo;
        private System.Windows.Forms.GroupBox gbCombo;
        private System.Windows.Forms.DataGridView dgvVeCon;
        private System.Windows.Forms.Panel pnlThemVeCon;
        private System.Windows.Forms.Button btnXoaVeCon;
        private System.Windows.Forms.Button btnThemVeCon;
        private System.Windows.Forms.NumericUpDown nudSoLuot;
        private System.Windows.Forms.Label lblSoLuot;
        private System.Windows.Forms.ComboBox cboVeConChon;
        private System.Windows.Forms.Label lblChonVeCon;
    }
}
