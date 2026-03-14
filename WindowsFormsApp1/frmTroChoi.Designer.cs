namespace WindowsFormsApp1
{
    partial class frmTroChoi
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.dgvTroChoi = new System.Windows.Forms.DataGridView();
            this.pnlTimKiem = new System.Windows.Forms.Panel();
            this.cboLocKhuVuc = new System.Windows.Forms.ComboBox();
            this.lblLocKhuVuc = new System.Windows.Forms.Label();
            this.txtTimKiem = new System.Windows.Forms.TextBox();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.lblTimKiem = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.gbThongTin = new System.Windows.Forms.GroupBox();
            this.cboKhuVuc = new System.Windows.Forms.ComboBox();
            this.txtSucChua = new System.Windows.Forms.TextBox();
            this.lblNgayTao = new System.Windows.Forms.Label();
            this.cboLoaiTC = new System.Windows.Forms.ComboBox();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.cboTrangThai = new System.Windows.Forms.ComboBox();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.txtThoiGianLuot = new System.Windows.Forms.TextBox();
            this.lblThoiGianLuot = new System.Windows.Forms.Label();
            this.cboChieuCao = new System.Windows.Forms.ComboBox();
            this.dtpNgayTao = new System.Windows.Forms.DateTimePicker();
            this.txtMaCode = new System.Windows.Forms.TextBox();
            this.txtTuoi = new System.Windows.Forms.TextBox();
            this.lblMaTroChoi = new System.Windows.Forms.Label();
            this.txtMaTC = new System.Windows.Forms.TextBox();
            this.lblTenTroChoi = new System.Windows.Forms.Label();
            this.txtTenTC = new System.Windows.Forms.TextBox();
            this.lblMaCode = new System.Windows.Forms.Label();
            this.lblNgayCapNhat = new System.Windows.Forms.Label();
            this.lblKhuVuc = new System.Windows.Forms.Label();
            this.lblTuoiToiThieu = new System.Windows.Forms.Label();
            this.lblSucChua = new System.Windows.Forms.Label();
            this.lblLoaiTroChoi = new System.Windows.Forms.Label();
            this.lblChieuCao = new System.Windows.Forms.Label();
            this.dtpNgayCapNhat = new System.Windows.Forms.DateTimePicker();
            this.pnlChucNang = new System.Windows.Forms.Panel();
            this.btnThoat = new System.Windows.Forms.Button();
            this.btnLamMoi = new System.Windows.Forms.Button();
            this.btnXoa = new System.Windows.Forms.Button();
            this.btnSua = new System.Windows.Forms.Button();
            this.btnThem = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTroChoi)).BeginInit();
            this.pnlTimKiem.SuspendLayout();
            this.panel1.SuspendLayout();
            this.gbThongTin.SuspendLayout();
            this.pnlChucNang.SuspendLayout();
            this.SuspendLayout();
            // 
            // dgvTroChoi
            // 
            this.dgvTroChoi.AllowUserToAddRows = false;
            this.dgvTroChoi.AllowUserToDeleteRows = false;
            this.dgvTroChoi.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvTroChoi.BackgroundColor = System.Drawing.Color.White;
            this.dgvTroChoi.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvTroChoi.ColumnHeadersHeight = 29;
            this.dgvTroChoi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvTroChoi.Location = new System.Drawing.Point(0, 386);
            this.dgvTroChoi.Name = "dgvTroChoi";
            this.dgvTroChoi.ReadOnly = true;
            this.dgvTroChoi.RowHeadersVisible = false;
            this.dgvTroChoi.RowHeadersWidth = 51;
            this.dgvTroChoi.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvTroChoi.Size = new System.Drawing.Size(1765, 349);
            this.dgvTroChoi.TabIndex = 4;
            this.dgvTroChoi.Click += new System.EventHandler(this.dgvTroChoi_Click);
            // 
            // pnlTimKiem
            // 
            this.pnlTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlTimKiem.Controls.Add(this.txtTimKiem);
            this.pnlTimKiem.Controls.Add(this.btnTimKiem);
            this.pnlTimKiem.Controls.Add(this.lblTimKiem);
            this.pnlTimKiem.Controls.Add(this.cboLocKhuVuc);
            this.pnlTimKiem.Controls.Add(this.lblLocKhuVuc);
            this.pnlTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimKiem.Location = new System.Drawing.Point(0, 320);
            this.pnlTimKiem.Name = "pnlTimKiem";
            this.pnlTimKiem.Padding = new System.Windows.Forms.Padding(10);
            this.pnlTimKiem.Size = new System.Drawing.Size(1765, 66);
            this.pnlTimKiem.TabIndex = 5;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Location = new System.Drawing.Point(100, 17);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Size = new System.Drawing.Size(600, 22);
            this.txtTimKiem.TabIndex = 0;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(123)))), ((int)(((byte)(255)))));
            this.btnTimKiem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(710, 15);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 30);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "   Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = false;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // lblLocKhuVuc
            // 
            this.lblLocKhuVuc.AutoSize = true;
            this.lblLocKhuVuc.Location = new System.Drawing.Point(830, 20);
            this.lblLocKhuVuc.Name = "lblLocKhuVuc";
            this.lblLocKhuVuc.Size = new System.Drawing.Size(65, 16);
            this.lblLocKhuVuc.TabIndex = 3;
            this.lblLocKhuVuc.Text = "Khu vực:";
            // 
            // cboLocKhuVuc
            // 
            this.cboLocKhuVuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocKhuVuc.FormattingEnabled = true;
            this.cboLocKhuVuc.Location = new System.Drawing.Point(900, 17);
            this.cboLocKhuVuc.Name = "cboLocKhuVuc";
            this.cboLocKhuVuc.Size = new System.Drawing.Size(250, 24);
            this.cboLocKhuVuc.TabIndex = 4;
            this.cboLocKhuVuc.SelectedIndexChanged += new System.EventHandler(this.cboLocKhuVuc_SelectedIndexChanged);
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
            this.panel1.Size = new System.Drawing.Size(1765, 320);
            this.panel1.TabIndex = 6;
            // 
            // gbThongTin
            // 
            this.gbThongTin.Controls.Add(this.cboKhuVuc);
            this.gbThongTin.Controls.Add(this.txtSucChua);
            this.gbThongTin.Controls.Add(this.lblNgayTao);
            this.gbThongTin.Controls.Add(this.cboLoaiTC);
            this.gbThongTin.Controls.Add(this.richTextBox1);
            this.gbThongTin.Controls.Add(this.lblMoTa);
            this.gbThongTin.Controls.Add(this.cboTrangThai);
            this.gbThongTin.Controls.Add(this.lblTrangThai);
            this.gbThongTin.Controls.Add(this.txtThoiGianLuot);
            this.gbThongTin.Controls.Add(this.lblThoiGianLuot);
            this.gbThongTin.Controls.Add(this.cboChieuCao);
            this.gbThongTin.Controls.Add(this.dtpNgayTao);
            this.gbThongTin.Controls.Add(this.txtMaCode);
            this.gbThongTin.Controls.Add(this.txtTuoi);
            this.gbThongTin.Controls.Add(this.lblMaTroChoi);
            this.gbThongTin.Controls.Add(this.txtMaTC);
            this.gbThongTin.Controls.Add(this.lblTenTroChoi);
            this.gbThongTin.Controls.Add(this.txtTenTC);
            this.gbThongTin.Controls.Add(this.lblMaCode);
            this.gbThongTin.Controls.Add(this.lblNgayCapNhat);
            this.gbThongTin.Controls.Add(this.lblKhuVuc);
            this.gbThongTin.Controls.Add(this.lblTuoiToiThieu);
            this.gbThongTin.Controls.Add(this.lblSucChua);
            this.gbThongTin.Controls.Add(this.lblLoaiTroChoi);
            this.gbThongTin.Controls.Add(this.lblChieuCao);
            this.gbThongTin.Controls.Add(this.dtpNgayCapNhat);
            this.gbThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThongTin.Location = new System.Drawing.Point(10, 10);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(1545, 300);
            this.gbThongTin.TabIndex = 0;
            this.gbThongTin.TabStop = false;
            this.gbThongTin.Text = "Thông tin trò chơi";
            // 
            // cboKhuVuc
            // 
            this.cboKhuVuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhuVuc.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboKhuVuc.FormattingEnabled = true;
            this.cboKhuVuc.Location = new System.Drawing.Point(994, 46);
            this.cboKhuVuc.Name = "cboKhuVuc";
            this.cboKhuVuc.Size = new System.Drawing.Size(218, 30);
            this.cboKhuVuc.TabIndex = 43;
            // 
            // txtSucChua
            // 
            this.txtSucChua.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSucChua.Location = new System.Drawing.Point(994, 105);
            this.txtSucChua.Name = "txtSucChua";
            this.txtSucChua.Size = new System.Drawing.Size(218, 30);
            this.txtSucChua.TabIndex = 42;
            // 
            // lblNgayTao
            // 
            this.lblNgayTao.AutoSize = true;
            this.lblNgayTao.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayTao.Location = new System.Drawing.Point(839, 238);
            this.lblNgayTao.Name = "lblNgayTao";
            this.lblNgayTao.Size = new System.Drawing.Size(84, 23);
            this.lblNgayTao.TabIndex = 41;
            this.lblNgayTao.Text = "Ngày tạo:";
            // 
            // cboLoaiTC
            // 
            this.cboLoaiTC.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiTC.FormattingEnabled = true;
            this.cboLoaiTC.Location = new System.Drawing.Point(582, 108);
            this.cboLoaiTC.Name = "cboLoaiTC";
            this.cboLoaiTC.Size = new System.Drawing.Size(218, 24);
            this.cboLoaiTC.TabIndex = 40;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(1274, 71);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(218, 194);
            this.richTextBox1.TabIndex = 39;
            this.richTextBox1.Text = "";
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMoTa.Location = new System.Drawing.Point(1270, 47);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(59, 23);
            this.lblMoTa.TabIndex = 38;
            this.lblMoTa.Text = "Mô tả:";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FormattingEnabled = true;
            this.cboTrangThai.Location = new System.Drawing.Point(582, 241);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(218, 24);
            this.cboTrangThai.TabIndex = 37;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTrangThai.Location = new System.Drawing.Point(402, 235);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(91, 23);
            this.lblTrangThai.TabIndex = 36;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // txtThoiGianLuot
            // 
            this.txtThoiGianLuot.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtThoiGianLuot.Location = new System.Drawing.Point(148, 235);
            this.txtThoiGianLuot.Name = "txtThoiGianLuot";
            this.txtThoiGianLuot.Size = new System.Drawing.Size(218, 30);
            this.txtThoiGianLuot.TabIndex = 35;
            // 
            // lblThoiGianLuot
            // 
            this.lblThoiGianLuot.AutoSize = true;
            this.lblThoiGianLuot.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblThoiGianLuot.Location = new System.Drawing.Point(12, 238);
            this.lblThoiGianLuot.Name = "lblThoiGianLuot";
            this.lblThoiGianLuot.Size = new System.Drawing.Size(120, 23);
            this.lblThoiGianLuot.TabIndex = 34;
            this.lblThoiGianLuot.Text = "Thời gian lượt:";
            // 
            // cboChieuCao
            // 
            this.cboChieuCao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboChieuCao.FormattingEnabled = true;
            this.cboChieuCao.Items.AddRange(new object[] {
            "0",
            "80",
            "90",
            "100",
            "110",
            "120",
            "130",
            "140",
            "150",
            "160",
            "170",
            "180",
            "190",
            "200"});
            this.cboChieuCao.Location = new System.Drawing.Point(582, 173);
            this.cboChieuCao.Name = "cboChieuCao";
            this.cboChieuCao.Size = new System.Drawing.Size(218, 24);
            this.cboChieuCao.TabIndex = 33;
            // 
            // dtpNgayTao
            // 
            this.dtpNgayTao.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayTao.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayTao.Location = new System.Drawing.Point(994, 235);
            this.dtpNgayTao.Name = "dtpNgayTao";
            this.dtpNgayTao.Size = new System.Drawing.Size(218, 30);
            this.dtpNgayTao.TabIndex = 31;
            // 
            // txtMaCode
            // 
            this.txtMaCode.Enabled = false;
            this.txtMaCode.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaCode.Location = new System.Drawing.Point(582, 47);
            this.txtMaCode.Name = "txtMaCode";
            this.txtMaCode.Size = new System.Drawing.Size(218, 30);
            this.txtMaCode.TabIndex = 30;
            // 
            // txtTuoi
            // 
            this.txtTuoi.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTuoi.Location = new System.Drawing.Point(148, 170);
            this.txtTuoi.Name = "txtTuoi";
            this.txtTuoi.Size = new System.Drawing.Size(218, 30);
            this.txtTuoi.TabIndex = 29;
            // 
            // lblMaTroChoi
            // 
            this.lblMaTroChoi.AutoSize = true;
            this.lblMaTroChoi.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaTroChoi.Location = new System.Drawing.Point(12, 47);
            this.lblMaTroChoi.Name = "lblMaTroChoi";
            this.lblMaTroChoi.Size = new System.Drawing.Size(102, 23);
            this.lblMaTroChoi.TabIndex = 0;
            this.lblMaTroChoi.Text = "Mã trò chơi:";
            // 
            // txtMaTC
            // 
            this.txtMaTC.Enabled = false;
            this.txtMaTC.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaTC.Location = new System.Drawing.Point(148, 44);
            this.txtMaTC.Name = "txtMaTC";
            this.txtMaTC.Size = new System.Drawing.Size(218, 30);
            this.txtMaTC.TabIndex = 1;
            // 
            // lblTenTroChoi
            // 
            this.lblTenTroChoi.AutoSize = true;
            this.lblTenTroChoi.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTenTroChoi.Location = new System.Drawing.Point(12, 112);
            this.lblTenTroChoi.Name = "lblTenTroChoi";
            this.lblTenTroChoi.Size = new System.Drawing.Size(104, 23);
            this.lblTenTroChoi.TabIndex = 2;
            this.lblTenTroChoi.Text = "Tên trò chơi:";
            // 
            // txtTenTC
            // 
            this.txtTenTC.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTenTC.Location = new System.Drawing.Point(148, 109);
            this.txtTenTC.Name = "txtTenTC";
            this.txtTenTC.Size = new System.Drawing.Size(218, 30);
            this.txtTenTC.TabIndex = 3;
            // 
            // lblMaCode
            // 
            this.lblMaCode.AutoSize = true;
            this.lblMaCode.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMaCode.Location = new System.Drawing.Point(402, 46);
            this.lblMaCode.Name = "lblMaCode";
            this.lblMaCode.Size = new System.Drawing.Size(76, 23);
            this.lblMaCode.TabIndex = 4;
            this.lblMaCode.Text = "Mã code:";
            // 
            // lblNgayCapNhat
            // 
            this.lblNgayCapNhat.AutoSize = true;
            this.lblNgayCapNhat.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNgayCapNhat.Location = new System.Drawing.Point(839, 169);
            this.lblNgayCapNhat.Name = "lblNgayCapNhat";
            this.lblNgayCapNhat.Size = new System.Drawing.Size(122, 23);
            this.lblNgayCapNhat.TabIndex = 6;
            this.lblNgayCapNhat.Text = "Ngày cập nhật:";
            // 
            // lblKhuVuc
            // 
            this.lblKhuVuc.AutoSize = true;
            this.lblKhuVuc.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKhuVuc.Location = new System.Drawing.Point(839, 51);
            this.lblKhuVuc.Name = "lblKhuVuc";
            this.lblKhuVuc.Size = new System.Drawing.Size(102, 23);
            this.lblKhuVuc.TabIndex = 8;
            this.lblKhuVuc.Text = "Khu vực:";
            // 
            // lblTuoiToiThieu
            // 
            this.lblTuoiToiThieu.AutoSize = true;
            this.lblTuoiToiThieu.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTuoiToiThieu.Location = new System.Drawing.Point(12, 173);
            this.lblTuoiToiThieu.Name = "lblTuoiToiThieu";
            this.lblTuoiToiThieu.Size = new System.Drawing.Size(116, 23);
            this.lblTuoiToiThieu.TabIndex = 10;
            this.lblTuoiToiThieu.Text = "Tuổi tối thiểu:";
            // 
            // lblSucChua
            // 
            this.lblSucChua.AutoSize = true;
            this.lblSucChua.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucChua.Location = new System.Drawing.Point(839, 107);
            this.lblSucChua.Name = "lblSucChua";
            this.lblSucChua.Size = new System.Drawing.Size(83, 23);
            this.lblSucChua.TabIndex = 12;
            this.lblSucChua.Text = "Sức chứa:";
            // 
            // lblLoaiTroChoi
            // 
            this.lblLoaiTroChoi.AutoSize = true;
            this.lblLoaiTroChoi.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLoaiTroChoi.Location = new System.Drawing.Point(400, 108);
            this.lblLoaiTroChoi.Name = "lblLoaiTroChoi";
            this.lblLoaiTroChoi.Size = new System.Drawing.Size(109, 23);
            this.lblLoaiTroChoi.TabIndex = 16;
            this.lblLoaiTroChoi.Text = "Loại trò chơi:";
            // 
            // lblChieuCao
            // 
            this.lblChieuCao.AutoSize = true;
            this.lblChieuCao.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblChieuCao.Location = new System.Drawing.Point(402, 173);
            this.lblChieuCao.Name = "lblChieuCao";
            this.lblChieuCao.Size = new System.Drawing.Size(159, 23);
            this.lblChieuCao.TabIndex = 18;
            this.lblChieuCao.Text = "Chiều cao tối thiểu:";
            // 
            // dtpNgayCapNhat
            // 
            this.dtpNgayCapNhat.Font = new System.Drawing.Font("Segoe UI", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dtpNgayCapNhat.Format = System.Windows.Forms.DateTimePickerFormat.Short;
            this.dtpNgayCapNhat.Location = new System.Drawing.Point(994, 167);
            this.dtpNgayCapNhat.Name = "dtpNgayCapNhat";
            this.dtpNgayCapNhat.Size = new System.Drawing.Size(218, 30);
            this.dtpNgayCapNhat.TabIndex = 25;
            // 
            // pnlChucNang
            // 
            this.pnlChucNang.Controls.Add(this.btnThoat);
            this.pnlChucNang.Controls.Add(this.btnLamMoi);
            this.pnlChucNang.Controls.Add(this.btnXoa);
            this.pnlChucNang.Controls.Add(this.btnSua);
            this.pnlChucNang.Controls.Add(this.btnThem);
            this.pnlChucNang.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlChucNang.Location = new System.Drawing.Point(1555, 10);
            this.pnlChucNang.Name = "pnlChucNang";
            this.pnlChucNang.Size = new System.Drawing.Size(200, 300);
            this.pnlChucNang.TabIndex = 1;
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.Red;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThoat.ForeColor = System.Drawing.Color.Black;
            this.btnThoat.Location = new System.Drawing.Point(10, 221);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(180, 40);
            this.btnThoat.TabIndex = 4;
            this.btnThoat.Text = "Thoát";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnLamMoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnLamMoi.ForeColor = System.Drawing.Color.Black;
            this.btnLamMoi.Location = new System.Drawing.Point(10, 170);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(180, 40);
            this.btnLamMoi.TabIndex = 0;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.UseVisualStyleBackColor = false;
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnXoa.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnXoa.ForeColor = System.Drawing.Color.Black;
            this.btnXoa.Location = new System.Drawing.Point(10, 120);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(180, 40);
            this.btnXoa.TabIndex = 1;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.UseVisualStyleBackColor = false;
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.BackColor = System.Drawing.SystemColors.ButtonShadow;
            this.btnSua.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSua.ForeColor = System.Drawing.Color.Black;
            this.btnSua.Location = new System.Drawing.Point(10, 70);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(180, 40);
            this.btnSua.TabIndex = 2;
            this.btnSua.Text = "Sửa";
            this.btnSua.UseVisualStyleBackColor = false;
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.BackColor = System.Drawing.SystemColors.ControlDark;
            this.btnThem.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10.8F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnThem.ForeColor = System.Drawing.Color.Black;
            this.btnThem.Location = new System.Drawing.Point(10, 20);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(180, 40);
            this.btnThem.TabIndex = 3;
            this.btnThem.Text = "Thêm";
            this.btnThem.UseVisualStyleBackColor = false;
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // frmTroChoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1765, 735);
            this.Controls.Add(this.dgvTroChoi);
            this.Controls.Add(this.pnlTimKiem);
            this.Controls.Add(this.panel1);
            this.Name = "frmTroChoi";
            this.Text = "frmTroChoi";
            this.Load += new System.EventHandler(this.frmTroChoi_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTroChoi)).EndInit();
            this.pnlTimKiem.ResumeLayout(false);
            this.pnlTimKiem.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            this.pnlChucNang.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvTroChoi;
        private System.Windows.Forms.Panel pnlTimKiem;
        private System.Windows.Forms.ComboBox cboLocKhuVuc;
        private System.Windows.Forms.Label lblLocKhuVuc;
        private System.Windows.Forms.TextBox txtTimKiem;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Label lblTimKiem;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox gbThongTin;
        private System.Windows.Forms.ComboBox cboChieuCao;
        private System.Windows.Forms.DateTimePicker dtpNgayTao;
        private System.Windows.Forms.TextBox txtMaCode;
        private System.Windows.Forms.TextBox txtTuoi;
        private System.Windows.Forms.Label lblMaTroChoi;
        private System.Windows.Forms.TextBox txtMaTC;
        private System.Windows.Forms.Label lblTenTroChoi;
        private System.Windows.Forms.TextBox txtTenTC;
        private System.Windows.Forms.Label lblMaCode;
        private System.Windows.Forms.Label lblNgayCapNhat;
        private System.Windows.Forms.Label lblKhuVuc;
        private System.Windows.Forms.Label lblTuoiToiThieu;
        private System.Windows.Forms.Label lblSucChua;
        private System.Windows.Forms.Label lblLoaiTroChoi;
        private System.Windows.Forms.Label lblChieuCao;
        private System.Windows.Forms.DateTimePicker dtpNgayCapNhat;
        private System.Windows.Forms.Panel pnlChucNang;
        private System.Windows.Forms.Button btnThoat;
        private System.Windows.Forms.Button btnLamMoi;
        private System.Windows.Forms.Button btnXoa;
        private System.Windows.Forms.Button btnSua;
        private System.Windows.Forms.Button btnThem;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label lblMoTa;
        private System.Windows.Forms.ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblTrangThai;
        private System.Windows.Forms.TextBox txtThoiGianLuot;
        private System.Windows.Forms.Label lblThoiGianLuot;
        private System.Windows.Forms.ComboBox cboLoaiTC;
        private System.Windows.Forms.TextBox txtSucChua;
        private System.Windows.Forms.Label lblNgayTao;
        private System.Windows.Forms.ComboBox cboKhuVuc;
    }
}