namespace GUI
{
    partial class frmKhuVuc
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
            this.pnlMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.gbDanhSach = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlTimKiem = new Guna.UI2.WinForms.Guna2Panel();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnTimKiem = new Guna.UI2.WinForms.Guna2Button();
            this.cboLocTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblLocTrangThai = new System.Windows.Forms.Label();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.gbThongTin = new System.Windows.Forms.Panel();
            this.gbModuleThongTin = new Guna.UI2.WinForms.Guna2GroupBox();
            this.pnlFieldsRight = new Guna.UI2.WinForms.Guna2Panel();
            this.txtMoTa = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.lblHinhAnh = new System.Windows.Forms.Label();
            this.picHinhAnh = new Guna.UI2.WinForms.Guna2PictureBox();
            this.btnChonAnh = new Guna.UI2.WinForms.Guna2Button();
            this.pnlFieldsCenter = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dtpNgayCapNhat = new DevExpress.XtraEditors.DateEdit();
            this.lblNgayCapNhat = new System.Windows.Forms.Label();
            this.dtpNgayTao = new DevExpress.XtraEditors.DateEdit();
            this.lblNgayTao = new System.Windows.Forms.Label();
            this.pnlFieldsLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.lblMaKV = new System.Windows.Forms.Label();
            this.txtMaKV = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenKV = new System.Windows.Forms.Label();
            this.txtTenKV = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMaCode = new System.Windows.Forms.Label();
            this.txtMaCode = new Guna.UI2.WinForms.Guna2TextBox();
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
            this.gbModuleThongTin.SuspendLayout();
            this.pnlFieldsRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHinhAnh)).BeginInit();
            this.pnlFieldsCenter.SuspendLayout();
            this.pnlFieldsLeft.SuspendLayout();
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
            this.pnlMain.SplitterPosition = 467;
            this.pnlMain.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gbDanhSach);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(823, 750);
            this.pnlLeft.TabIndex = 0;
            // 
            // gbDanhSach
            // 
            this.gbDanhSach.Controls.Add(this.gridControl);
            this.gbDanhSach.Controls.Add(this.pnlTimKiem);
            this.gbDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDanhSach.Location = new System.Drawing.Point(0, 0);
            this.gbDanhSach.Name = "gbDanhSach";
            this.gbDanhSach.Size = new System.Drawing.Size(860, 750);
            this.gbDanhSach.TabIndex = 0;
            this.gbDanhSach.Text = "Danh sách";
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 90);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(860, 660);
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
            this.pnlTimKiem.Controls.Add(this.cboLocTrangThai);
            this.pnlTimKiem.Controls.Add(this.lblLocTrangThai);
            this.pnlTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimKiem.Location = new System.Drawing.Point(0, 40);
            this.pnlTimKiem.Name = "pnlTimKiem";
            this.pnlTimKiem.Size = new System.Drawing.Size(860, 50);
            this.pnlTimKiem.TabIndex = 1;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Location = new System.Drawing.Point(280, 7);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Nhập từ khóa cần tìm...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(300, 36);
            this.txtTimKiem.TabIndex = 0;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(710, 8);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(110, 34);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.Visible = false;
            // 
            // cboLocTrangThai
            // 
            this.cboLocTrangThai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLocTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocTrangThai.ItemHeight = 30;
            this.cboLocTrangThai.Location = new System.Drawing.Point(520, 12);
            this.cboLocTrangThai.Name = "cboLocTrangThai";
            this.cboLocTrangThai.Size = new System.Drawing.Size(150, 36);
            this.cboLocTrangThai.TabIndex = 2;
            // 
            // lblLocTrangThai
            // 
            this.lblLocTrangThai.AutoSize = true;
            this.lblLocTrangThai.Location = new System.Drawing.Point(450, 17);
            this.lblLocTrangThai.Name = "lblLocTrangThai";
            this.lblLocTrangThai.Size = new System.Drawing.Size(80, 19);
            this.lblLocTrangThai.TabIndex = 3;
            this.lblLocTrangThai.Text = "Trạng thái:";
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
            this.pnlRight.Size = new System.Drawing.Size(467, 750);
            this.pnlRight.TabIndex = 1;
            // 
            // gbThongTin
            // 
            this.gbThongTin.AutoScroll = true;
            this.gbThongTin.Controls.Add(this.gbModuleThongTin);
            this.gbThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThongTin.Location = new System.Drawing.Point(15, 15);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(410, 507);
            this.gbThongTin.TabIndex = 0;
            // 
            // gbModuleThongTin
            // 
            this.gbModuleThongTin.Controls.Add(this.pnlFieldsLeft);
            this.gbModuleThongTin.Controls.Add(this.pnlFieldsCenter);
            this.gbModuleThongTin.Controls.Add(this.pnlFieldsRight);
            this.gbModuleThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbModuleThongTin.Location = new System.Drawing.Point(0, 0);
            this.gbModuleThongTin.Name = "gbModuleThongTin";
            this.gbModuleThongTin.Size = new System.Drawing.Size(410, 507);
            this.gbModuleThongTin.TabIndex = 0;
            this.gbModuleThongTin.Text = "Thông tin khu vực";
            this.gbModuleThongTin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // pnlFieldsRight
            // 
            this.pnlFieldsRight.Controls.Add(this.txtMoTa);
            this.pnlFieldsRight.Controls.Add(this.lblMoTa);
            this.pnlFieldsRight.Controls.Add(this.lblHinhAnh);
            this.pnlFieldsRight.Controls.Add(this.picHinhAnh);
            this.pnlFieldsRight.Controls.Add(this.btnChonAnh);
            this.pnlFieldsRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFieldsRight.Location = new System.Drawing.Point(400, 40);
            this.pnlFieldsRight.Name = "pnlFieldsRight";
            this.pnlFieldsRight.Size = new System.Drawing.Size(10, 467);
            this.pnlFieldsRight.TabIndex = 0;
            // 
            // txtMoTa
            // 
            this.txtMoTa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMoTa.DefaultText = "";
            this.txtMoTa.Location = new System.Drawing.Point(10, 40);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.PlaceholderText = "";
            this.txtMoTa.SelectedText = "";
            this.txtMoTa.Size = new System.Drawing.Size(180, 80);
            this.txtMoTa.TabIndex = 0;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(10, 20);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(52, 19);
            this.lblMoTa.TabIndex = 1;
            this.lblMoTa.Text = "Mô tả:";
            // 
            // lblHinhAnh
            // 
            this.lblHinhAnh.AutoSize = true;
            this.lblHinhAnh.Location = new System.Drawing.Point(10, 130);
            this.lblHinhAnh.Name = "lblHinhAnh";
            this.lblHinhAnh.Size = new System.Drawing.Size(72, 19);
            this.lblHinhAnh.TabIndex = 2;
            this.lblHinhAnh.Text = "Hình ảnh:";
            // 
            // picHinhAnh
            // 
            this.picHinhAnh.ImageRotate = 0F;
            this.picHinhAnh.Location = new System.Drawing.Point(10, 150);
            this.picHinhAnh.Name = "picHinhAnh";
            this.picHinhAnh.Size = new System.Drawing.Size(180, 100);
            this.picHinhAnh.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picHinhAnh.TabIndex = 3;
            this.picHinhAnh.TabStop = false;
            // 
            // btnChonAnh
            // 
            this.btnChonAnh.Location = new System.Drawing.Point(10, 255);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new System.Drawing.Size(180, 30);
            this.btnChonAnh.TabIndex = 4;
            this.btnChonAnh.Text = "Chọn hình ảnh";
            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);
            // 
            // pnlFieldsCenter
            // 
            this.pnlFieldsCenter.Controls.Add(this.lblTrangThai);
            this.pnlFieldsCenter.Controls.Add(this.cboTrangThai);
            this.pnlFieldsCenter.Controls.Add(this.dtpNgayCapNhat);
            this.pnlFieldsCenter.Controls.Add(this.lblNgayCapNhat);
            this.pnlFieldsCenter.Controls.Add(this.dtpNgayTao);
            this.pnlFieldsCenter.Controls.Add(this.lblNgayTao);
            this.pnlFieldsCenter.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlFieldsCenter.Location = new System.Drawing.Point(200, 40);
            this.pnlFieldsCenter.Name = "pnlFieldsCenter";
            this.pnlFieldsCenter.Size = new System.Drawing.Size(200, 467);
            this.pnlFieldsCenter.TabIndex = 1;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(10, 20);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(80, 19);
            this.lblTrangThai.TabIndex = 0;
            this.lblTrangThai.Text = "Trạng thái:";
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.ItemHeight = 30;
            this.cboTrangThai.Location = new System.Drawing.Point(10, 40);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(180, 36);
            this.cboTrangThai.TabIndex = 1;
            // 
            // dtpNgayCapNhat
            // 
            this.dtpNgayCapNhat.Location = new System.Drawing.Point(10, 160);
            this.dtpNgayCapNhat.Name = "dtpNgayCapNhat";
            this.dtpNgayCapNhat.Size = new System.Drawing.Size(180, 36);
            this.dtpNgayCapNhat.TabIndex = 2;
            this.dtpNgayCapNhat.DateTime = System.DateTime.Now;
            // 
            // lblNgayCapNhat
            // 
            this.lblNgayCapNhat.AutoSize = true;
            this.lblNgayCapNhat.Location = new System.Drawing.Point(10, 140);
            this.lblNgayCapNhat.Name = "lblNgayCapNhat";
            this.lblNgayCapNhat.Size = new System.Drawing.Size(110, 19);
            this.lblNgayCapNhat.TabIndex = 3;
            this.lblNgayCapNhat.Text = "Ngày cập nhật:";
            // 
            // dtpNgayTao
            // 
            this.dtpNgayTao.Location = new System.Drawing.Point(10, 100);
            this.dtpNgayTao.Name = "dtpNgayTao";
            this.dtpNgayTao.Size = new System.Drawing.Size(180, 36);
            this.dtpNgayTao.TabIndex = 4;
            this.dtpNgayTao.DateTime = System.DateTime.Now;
            // 
            // lblNgayTao
            // 
            this.lblNgayTao.AutoSize = true;
            this.lblNgayTao.Location = new System.Drawing.Point(10, 80);
            this.lblNgayTao.Name = "lblNgayTao";
            this.lblNgayTao.Size = new System.Drawing.Size(75, 19);
            this.lblNgayTao.TabIndex = 5;
            this.lblNgayTao.Text = "Ngày tạo:";
            // 
            // pnlFieldsLeft
            // 
            this.pnlFieldsLeft.Controls.Add(this.lblMaKV);
            this.pnlFieldsLeft.Controls.Add(this.txtMaKV);
            this.pnlFieldsLeft.Controls.Add(this.lblTenKV);
            this.pnlFieldsLeft.Controls.Add(this.txtTenKV);
            this.pnlFieldsLeft.Controls.Add(this.lblMaCode);
            this.pnlFieldsLeft.Controls.Add(this.txtMaCode);
            this.pnlFieldsLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlFieldsLeft.Location = new System.Drawing.Point(0, 40);
            this.pnlFieldsLeft.Name = "pnlFieldsLeft";
            this.pnlFieldsLeft.Size = new System.Drawing.Size(200, 467);
            this.pnlFieldsLeft.TabIndex = 2;
            // 
            // lblMaKV
            // 
            this.lblMaKV.AutoSize = true;
            this.lblMaKV.Location = new System.Drawing.Point(10, 20);
            this.lblMaKV.Name = "lblMaKV";
            this.lblMaKV.Size = new System.Drawing.Size(90, 19);
            this.lblMaKV.TabIndex = 0;
            this.lblMaKV.Text = "Mã khu vực:";
            // 
            // txtMaKV
            // 
            this.txtMaKV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaKV.DefaultText = "";
            this.txtMaKV.Location = new System.Drawing.Point(10, 40);
            this.txtMaKV.Name = "txtMaKV";
            this.txtMaKV.PlaceholderText = "";
            this.txtMaKV.ReadOnly = true;
            this.txtMaKV.SelectedText = "";
            this.txtMaKV.Size = new System.Drawing.Size(180, 34);
            this.txtMaKV.TabIndex = 1;
            // 
            // lblTenKV
            // 
            this.lblTenKV.AutoSize = true;
            this.lblTenKV.Location = new System.Drawing.Point(10, 80);
            this.lblTenKV.Name = "lblTenKV";
            this.lblTenKV.Size = new System.Drawing.Size(92, 19);
            this.lblTenKV.TabIndex = 2;
            this.lblTenKV.Text = "Tên khu vực:";
            // 
            // txtTenKV
            // 
            this.txtTenKV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenKV.DefaultText = "";
            this.txtTenKV.Location = new System.Drawing.Point(10, 100);
            this.txtTenKV.Name = "txtTenKV";
            this.txtTenKV.PlaceholderText = "";
            this.txtTenKV.SelectedText = "";
            this.txtTenKV.Size = new System.Drawing.Size(180, 34);
            this.txtTenKV.TabIndex = 3;
            // 
            // lblMaCode
            // 
            this.lblMaCode.AutoSize = true;
            this.lblMaCode.Location = new System.Drawing.Point(10, 140);
            this.lblMaCode.Name = "lblMaCode";
            this.lblMaCode.Size = new System.Drawing.Size(71, 19);
            this.lblMaCode.TabIndex = 4;
            this.lblMaCode.Text = "Mã code:";
            // 
            // txtMaCode
            // 
            this.txtMaCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaCode.DefaultText = "";
            this.txtMaCode.Location = new System.Drawing.Point(10, 160);
            this.txtMaCode.Name = "txtMaCode";
            this.txtMaCode.PlaceholderText = "";
            this.txtMaCode.ReadOnly = true;
            this.txtMaCode.SelectedText = "";
            this.txtMaCode.Size = new System.Drawing.Size(180, 34);
            this.txtMaCode.TabIndex = 5;
            // 
            // pnlSpacer
            // 
            this.pnlSpacer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSpacer.Location = new System.Drawing.Point(15, 522);
            this.pnlSpacer.Name = "pnlSpacer";
            this.pnlSpacer.Size = new System.Drawing.Size(410, 15);
            this.pnlSpacer.TabIndex = 1;
            // 
            // gbChucNang
            // 
            this.gbChucNang.Controls.Add(this.btnThem);
            this.gbChucNang.Controls.Add(this.btnSua);
            this.gbChucNang.Controls.Add(this.btnXoa);
            this.gbChucNang.Controls.Add(this.btnLamMoi);
            this.gbChucNang.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbChucNang.Location = new System.Drawing.Point(15, 537);
            this.gbChucNang.Name = "gbChucNang";
            this.gbChucNang.Size = new System.Drawing.Size(410, 198);
            this.gbChucNang.TabIndex = 2;
            this.gbChucNang.Text = "Chức năng";
            this.gbChucNang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(25, 55);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(170, 40);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm mới";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(215, 55);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(170, 40);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Cập nhật";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(25, 105);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(170, 40);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(215, 105);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(170, 40);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // frmKhuVuc
            // 
            this.ClientSize = new System.Drawing.Size(1300, 750);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmKhuVuc";
            this.Text = "AREA MANAGEMENT";
            this.Load += new System.EventHandler(this.frmKhuVuc_Load);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
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
            this.pnlTimKiem.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.gbThongTin.ResumeLayout(false);
            this.gbModuleThongTin.ResumeLayout(false);
            this.pnlFieldsRight.ResumeLayout(false);
            this.pnlFieldsRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHinhAnh)).EndInit();
            this.pnlFieldsCenter.ResumeLayout(false);
            this.pnlFieldsCenter.PerformLayout();
            this.pnlFieldsLeft.ResumeLayout(false);
            this.pnlFieldsLeft.PerformLayout();
            this.gbChucNang.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion


        private Guna.UI2.WinForms.Guna2GroupBox gbModuleThongTin;
        private Guna.UI2.WinForms.Guna2ComboBox cboLocTrangThai;
        private System.Windows.Forms.Label lblLocTrangThai;
        private Guna.UI2.WinForms.Guna2Panel pnlFieldsRight;
        private Guna.UI2.WinForms.Guna2TextBox txtMoTa;
        private System.Windows.Forms.Label lblMoTa;
        private Guna.UI2.WinForms.Guna2Panel pnlFieldsCenter;
        private System.Windows.Forms.Label lblTrangThai;
        private Guna.UI2.WinForms.Guna2ComboBox cboTrangThai;
        private DevExpress.XtraEditors.DateEdit dtpNgayCapNhat;
        private System.Windows.Forms.Label lblNgayCapNhat;
        private DevExpress.XtraEditors.DateEdit dtpNgayTao;
        private System.Windows.Forms.Label lblNgayTao;
        private Guna.UI2.WinForms.Guna2Panel pnlFieldsLeft;
        private System.Windows.Forms.Label lblMaKV;
        private Guna.UI2.WinForms.Guna2TextBox txtMaKV;
        private System.Windows.Forms.Label lblTenKV;
        private Guna.UI2.WinForms.Guna2TextBox txtTenKV;
        private System.Windows.Forms.Label lblMaCode;
        private Guna.UI2.WinForms.Guna2TextBox txtMaCode;
        private System.Windows.Forms.Label lblHinhAnh;
        private Guna.UI2.WinForms.Guna2PictureBox picHinhAnh;
        private Guna.UI2.WinForms.Guna2Button btnChonAnh;

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











