namespace GUI
{
    partial class frmThueDo
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
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblFormTitle = new System.Windows.Forms.Label();
            this.cboTramChoThue = new System.Windows.Forms.ComboBox();
            this.lblTramIcon = new System.Windows.Forms.Label();
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabGiaoDo = new System.Windows.Forms.TabPage();
            this.splitGiaoDo = new DevExpress.XtraEditors.SplitContainerControl();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.gridControlSanPham = new DevExpress.XtraGrid.GridControl();
            this.gridViewSanPham = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlKhachHangGiao = new System.Windows.Forms.Panel();
            this.lblSoDuVi = new System.Windows.Forms.Label();
            this.lblTenKH = new System.Windows.Forms.Label();
            this.txtRfidGiao = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlTongTien = new System.Windows.Forms.Panel();
            this.btnHuyGiao = new Guna.UI2.WinForms.Guna2Button();
            this.btnThanhToanTienMat = new Guna.UI2.WinForms.Guna2Button();
            this.btnThanhToanRfid = new Guna.UI2.WinForms.Guna2Button();
            this.lblTongCong = new System.Windows.Forms.Label();
            this.lblTongCoc = new System.Windows.Forms.Label();
            this.lblTongThue = new System.Windows.Forms.Label();
            this.gridControlGioHang = new DevExpress.XtraGrid.GridControl();
            this.gridViewGioHang = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabNhanTra = new System.Windows.Forms.TabPage();
            this.splitNhanTra = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnlKhachHangTra = new System.Windows.Forms.Panel();
            this.lblSoDuViTra = new System.Windows.Forms.Label();
            this.lblTenKHTra = new System.Windows.Forms.Label();
            this.txtMaDonHang = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtRfidTra = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlActionTra = new System.Windows.Forms.Panel();
            this.btnBaoHong = new Guna.UI2.WinForms.Guna2Button();
            this.btnTraDu = new Guna.UI2.WinForms.Guna2Button();
            this.gridControlDangThue = new DevExpress.XtraGrid.GridControl();
            this.gridViewDangThue = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlHeader.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.tabGiaoDo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGiaoDo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitGiaoDo.Panel1)).BeginInit();
            this.splitGiaoDo.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitGiaoDo.Panel2)).BeginInit();
            this.splitGiaoDo.Panel2.SuspendLayout();
            this.splitGiaoDo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSanPham)).BeginInit();
            this.pnlKhachHangGiao.SuspendLayout();
            this.pnlTongTien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlGioHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGioHang)).BeginInit();
            this.tabNhanTra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitNhanTra)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitNhanTra.Panel1)).BeginInit();
            this.splitNhanTra.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitNhanTra.Panel2)).BeginInit();
            this.splitNhanTra.Panel2.SuspendLayout();
            this.splitNhanTra.SuspendLayout();
            this.pnlKhachHangTra.SuspendLayout();
            this.pnlActionTra.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDangThue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDangThue)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlHeader.Controls.Add(this.lblFormTitle);
            this.pnlHeader.Controls.Add(this.cboTramChoThue);
            this.pnlHeader.Controls.Add(this.lblTramIcon);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(13, 9, 13, 9);
            this.pnlHeader.Size = new System.Drawing.Size(1237, 52);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblFormTitle
            // 
            this.lblFormTitle.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblFormTitle.AutoSize = true;
            this.lblFormTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblFormTitle.ForeColor = System.Drawing.Color.White;
            this.lblFormTitle.Location = new System.Drawing.Point(1666, 12);
            this.lblFormTitle.Name = "lblFormTitle";
            this.lblFormTitle.Size = new System.Drawing.Size(222, 30);
            this.lblFormTitle.TabIndex = 0;
            this.lblFormTitle.Text = "CHO THUÊ DỊCH VỤ";
            // 
            // cboTramChoThue
            // 
            this.cboTramChoThue.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTramChoThue.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.cboTramChoThue.Location = new System.Drawing.Point(43, 12);
            this.cboTramChoThue.Name = "cboTramChoThue";
            this.cboTramChoThue.Size = new System.Drawing.Size(241, 28);
            this.cboTramChoThue.TabIndex = 1;
            this.cboTramChoThue.SelectedIndexChanged += new System.EventHandler(this.cboTramChoThue_SelectedIndexChanged);
            // 
            // lblTramIcon
            // 
            this.lblTramIcon.AutoSize = true;
            this.lblTramIcon.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblTramIcon.ForeColor = System.Drawing.Color.White;
            this.lblTramIcon.Location = new System.Drawing.Point(13, 13);
            this.lblTramIcon.Name = "lblTramIcon";
            this.lblTramIcon.Size = new System.Drawing.Size(24, 25);
            this.lblTramIcon.TabIndex = 2;
            this.lblTramIcon.Text = "📍";
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabGiaoDo);
            this.tabControl.Controls.Add(this.tabNhanTra);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.tabControl.Location = new System.Drawing.Point(0, 52);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(1237, 737);
            this.tabControl.TabIndex = 0;
            this.tabControl.SelectedIndexChanged += new System.EventHandler(this.tabControl_SelectedIndexChanged);
            // 
            // tabGiaoDo
            // 
            this.tabGiaoDo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tabGiaoDo.Controls.Add(this.splitGiaoDo);
            this.tabGiaoDo.Location = new System.Drawing.Point(4, 29);
            this.tabGiaoDo.Name = "tabGiaoDo";
            this.tabGiaoDo.Padding = new System.Windows.Forms.Padding(4);
            this.tabGiaoDo.Size = new System.Drawing.Size(1229, 704);
            this.tabGiaoDo.TabIndex = 0;
            this.tabGiaoDo.Text = "📤  GIAO ĐỒ CHO THUÊ";
            // 
            // splitGiaoDo
            // 
            this.splitGiaoDo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitGiaoDo.Location = new System.Drawing.Point(4, 4);
            this.splitGiaoDo.Name = "splitGiaoDo";
            // 
            // splitGiaoDo.Panel1
            // 
            this.splitGiaoDo.Panel1.Controls.Add(this.txtTimKiem);
            this.splitGiaoDo.Panel1.Controls.Add(this.gridControlSanPham);
            // 
            // splitGiaoDo.Panel2
            // 
            this.splitGiaoDo.Panel2.Controls.Add(this.pnlKhachHangGiao);
            this.splitGiaoDo.Panel2.Controls.Add(this.pnlTongTien);
            this.splitGiaoDo.Panel2.Controls.Add(this.gridControlGioHang);
            this.splitGiaoDo.ShowSplitGlyph = DevExpress.Utils.DefaultBoolean.True;
            this.splitGiaoDo.Size = new System.Drawing.Size(1221, 696);
            this.splitGiaoDo.SplitterPosition = 618;
            this.splitGiaoDo.TabIndex = 0;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderRadius = 8;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTimKiem.Location = new System.Drawing.Point(0, 0);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "🔍 Tìm sản phẩm cho thuê...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(618, 53);
            this.txtTimKiem.TabIndex = 1;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // gridControlSanPham
            // 
            this.gridControlSanPham.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlSanPham.Location = new System.Drawing.Point(0, 0);
            this.gridControlSanPham.MainView = this.gridViewSanPham;
            this.gridControlSanPham.Name = "gridControlSanPham";
            this.gridControlSanPham.Size = new System.Drawing.Size(618, 696);
            this.gridControlSanPham.TabIndex = 0;
            this.gridControlSanPham.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSanPham});
            // 
            // gridViewSanPham
            // 
            this.gridViewSanPham.DetailHeight = 303;
            this.gridViewSanPham.GridControl = this.gridControlSanPham;
            this.gridViewSanPham.Name = "gridViewSanPham";
            this.gridViewSanPham.OptionsBehavior.Editable = false;
            this.gridViewSanPham.OptionsBehavior.ReadOnly = true;
            this.gridViewSanPham.OptionsView.ShowGroupPanel = false;
            this.gridViewSanPham.RowHeight = 39;
            this.gridViewSanPham.DoubleClick += new System.EventHandler(this.gridViewSanPham_DoubleClick);
            // 
            // pnlKhachHangGiao
            // 
            this.pnlKhachHangGiao.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlKhachHangGiao.Controls.Add(this.lblSoDuVi);
            this.pnlKhachHangGiao.Controls.Add(this.lblTenKH);
            this.pnlKhachHangGiao.Controls.Add(this.txtRfidGiao);
            this.pnlKhachHangGiao.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKhachHangGiao.Location = new System.Drawing.Point(0, 0);
            this.pnlKhachHangGiao.Name = "pnlKhachHangGiao";
            this.pnlKhachHangGiao.Padding = new System.Windows.Forms.Padding(9);
            this.pnlKhachHangGiao.Size = new System.Drawing.Size(593, 138);
            this.pnlKhachHangGiao.TabIndex = 2;
            // 
            // lblSoDuVi
            // 
            this.lblSoDuVi.AutoSize = true;
            this.lblSoDuVi.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.lblSoDuVi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblSoDuVi.Location = new System.Drawing.Point(10, 109);
            this.lblSoDuVi.Name = "lblSoDuVi";
            this.lblSoDuVi.Size = new System.Drawing.Size(90, 20);
            this.lblSoDuVi.TabIndex = 0;
            this.lblSoDuVi.Text = "Số dư ví: ---";
            // 
            // lblTenKH
            // 
            this.lblTenKH.AutoSize = true;
            this.lblTenKH.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.lblTenKH.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTenKH.Location = new System.Drawing.Point(10, 75);
            this.lblTenKH.Name = "lblTenKH";
            this.lblTenKH.Size = new System.Drawing.Size(77, 20);
            this.lblTenKH.TabIndex = 1;
            this.lblTenKH.Text = "Khách: ---";
            // 
            // txtRfidGiao
            // 
            this.txtRfidGiao.BorderRadius = 8;
            this.txtRfidGiao.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRfidGiao.DefaultText = "";
            this.txtRfidGiao.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtRfidGiao.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtRfidGiao.Location = new System.Drawing.Point(9, 9);
            this.txtRfidGiao.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRfidGiao.Name = "txtRfidGiao";
            this.txtRfidGiao.PlaceholderText = "Quẹt vòng RFID khách hàng...";
            this.txtRfidGiao.SelectedText = "";
            this.txtRfidGiao.Size = new System.Drawing.Size(575, 51);
            this.txtRfidGiao.TabIndex = 2;
            this.txtRfidGiao.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRfidGiao_KeyDown);
            // 
            // pnlTongTien
            // 
            this.pnlTongTien.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlTongTien.Controls.Add(this.btnHuyGiao);
            this.pnlTongTien.Controls.Add(this.btnThanhToanTienMat);
            this.pnlTongTien.Controls.Add(this.btnThanhToanRfid);
            this.pnlTongTien.Controls.Add(this.lblTongCong);
            this.pnlTongTien.Controls.Add(this.lblTongCoc);
            this.pnlTongTien.Controls.Add(this.lblTongThue);
            this.pnlTongTien.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTongTien.Location = new System.Drawing.Point(0, 488);
            this.pnlTongTien.Name = "pnlTongTien";
            this.pnlTongTien.Padding = new System.Windows.Forms.Padding(9);
            this.pnlTongTien.Size = new System.Drawing.Size(593, 208);
            this.pnlTongTien.TabIndex = 1;
            // 
            // btnHuyGiao
            // 
            this.btnHuyGiao.BorderRadius = 8;
            this.btnHuyGiao.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnHuyGiao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnHuyGiao.ForeColor = System.Drawing.Color.White;
            this.btnHuyGiao.Location = new System.Drawing.Point(14, 167);
            this.btnHuyGiao.Name = "btnHuyGiao";
            this.btnHuyGiao.Size = new System.Drawing.Size(569, 35);
            this.btnHuyGiao.TabIndex = 0;
            this.btnHuyGiao.Text = "Hủy";
            this.btnHuyGiao.Click += new System.EventHandler(this.btnHuyGiao_Click);
            // 
            // btnThanhToanTienMat
            // 
            this.btnThanhToanTienMat.BorderRadius = 10;
            this.btnThanhToanTienMat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnThanhToanTienMat.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnThanhToanTienMat.ForeColor = System.Drawing.Color.White;
            this.btnThanhToanTienMat.Location = new System.Drawing.Point(300, 117);
            this.btnThanhToanTienMat.Name = "btnThanhToanTienMat";
            this.btnThanhToanTienMat.Size = new System.Drawing.Size(283, 43);
            this.btnThanhToanTienMat.TabIndex = 1;
            this.btnThanhToanTienMat.Text = "TIỀN MẶT";
            this.btnThanhToanTienMat.Click += new System.EventHandler(this.btnThanhToanTienMat_Click);
            // 
            // btnThanhToanRfid
            // 
            this.btnThanhToanRfid.BorderRadius = 10;
            this.btnThanhToanRfid.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnThanhToanRfid.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnThanhToanRfid.ForeColor = System.Drawing.Color.White;
            this.btnThanhToanRfid.Location = new System.Drawing.Point(11, 118);
            this.btnThanhToanRfid.Name = "btnThanhToanRfid";
            this.btnThanhToanRfid.Size = new System.Drawing.Size(283, 43);
            this.btnThanhToanRfid.TabIndex = 2;
            this.btnThanhToanRfid.Text = "THANH TOÁN RFID";
            this.btnThanhToanRfid.Click += new System.EventHandler(this.btnThanhToanRfid_Click);
            // 
            // lblTongCong
            // 
            this.lblTongCong.AutoSize = true;
            this.lblTongCong.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongCong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTongCong.Location = new System.Drawing.Point(9, 78);
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Size = new System.Drawing.Size(99, 25);
            this.lblTongCong.TabIndex = 3;
            this.lblTongCong.Text = "TỔNG: 0đ";
            // 
            // lblTongCoc
            // 
            this.lblTongCoc.AutoSize = true;
            this.lblTongCoc.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold);
            this.lblTongCoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.lblTongCoc.Location = new System.Drawing.Point(7, 52);
            this.lblTongCoc.Name = "lblTongCoc";
            this.lblTongCoc.Size = new System.Drawing.Size(106, 20);
            this.lblTongCoc.TabIndex = 4;
            this.lblTongCoc.Text = "Tiền cọc: 0đ";
            // 
            // lblTongThue
            // 
            this.lblTongThue.AutoSize = true;
            this.lblTongThue.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTongThue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblTongThue.Location = new System.Drawing.Point(12, 25);
            this.lblTongThue.Name = "lblTongThue";
            this.lblTongThue.Size = new System.Drawing.Size(94, 20);
            this.lblTongThue.TabIndex = 5;
            this.lblTongThue.Text = "Tiền thuê: 0đ";
            // 
            // gridControlGioHang
            // 
            this.gridControlGioHang.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControlGioHang.Location = new System.Drawing.Point(0, 132);
            this.gridControlGioHang.MainView = this.gridViewGioHang;
            this.gridControlGioHang.Name = "gridControlGioHang";
            this.gridControlGioHang.Size = new System.Drawing.Size(593, 358);
            this.gridControlGioHang.TabIndex = 0;
            this.gridControlGioHang.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewGioHang});
            // 
            // gridViewGioHang
            // 
            this.gridViewGioHang.DetailHeight = 303;
            this.gridViewGioHang.GridControl = this.gridControlGioHang;
            this.gridViewGioHang.Name = "gridViewGioHang";
            this.gridViewGioHang.OptionsView.ShowGroupPanel = false;
            this.gridViewGioHang.RowHeight = 35;
            // 
            // tabNhanTra
            // 
            this.tabNhanTra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tabNhanTra.Controls.Add(this.splitNhanTra);
            this.tabNhanTra.Location = new System.Drawing.Point(4, 29);
            this.tabNhanTra.Name = "tabNhanTra";
            this.tabNhanTra.Padding = new System.Windows.Forms.Padding(4);
            this.tabNhanTra.Size = new System.Drawing.Size(1229, 704);
            this.tabNhanTra.TabIndex = 1;
            this.tabNhanTra.Text = "📥  NHẬN TRẢ ĐỒ";
            // 
            // splitNhanTra
            // 
            this.splitNhanTra.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitNhanTra.Location = new System.Drawing.Point(4, 4);
            this.splitNhanTra.Name = "splitNhanTra";
            // 
            // splitNhanTra.Panel1
            // 
            this.splitNhanTra.Panel1.Controls.Add(this.pnlKhachHangTra);
            // 
            // splitNhanTra.Panel2
            // 
            this.splitNhanTra.Panel2.Controls.Add(this.pnlActionTra);
            this.splitNhanTra.Panel2.Controls.Add(this.gridControlDangThue);
            this.splitNhanTra.ShowSplitGlyph = DevExpress.Utils.DefaultBoolean.True;
            this.splitNhanTra.Size = new System.Drawing.Size(1221, 696);
            this.splitNhanTra.SplitterPosition = 600;
            this.splitNhanTra.TabIndex = 0;
            // 
            // pnlKhachHangTra
            // 
            this.pnlKhachHangTra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlKhachHangTra.Controls.Add(this.lblSoDuViTra);
            this.pnlKhachHangTra.Controls.Add(this.lblTenKHTra);
            this.pnlKhachHangTra.Controls.Add(this.txtMaDonHang);
            this.pnlKhachHangTra.Controls.Add(this.txtRfidTra);
            this.pnlKhachHangTra.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKhachHangTra.Location = new System.Drawing.Point(0, 0);
            this.pnlKhachHangTra.Name = "pnlKhachHangTra";
            this.pnlKhachHangTra.Padding = new System.Windows.Forms.Padding(13);
            this.pnlKhachHangTra.Size = new System.Drawing.Size(600, 226);
            this.pnlKhachHangTra.TabIndex = 0;
            // 
            // lblSoDuViTra
            // 
            this.lblSoDuViTra.AutoSize = true;
            this.lblSoDuViTra.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.lblSoDuViTra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblSoDuViTra.Location = new System.Drawing.Point(13, 167);
            this.lblSoDuViTra.Name = "lblSoDuViTra";
            this.lblSoDuViTra.Size = new System.Drawing.Size(90, 20);
            this.lblSoDuViTra.TabIndex = 0;
            this.lblSoDuViTra.Text = "Số dư ví: ---";
            // 
            // lblTenKHTra
            // 
            this.lblTenKHTra.AutoSize = true;
            this.lblTenKHTra.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.lblTenKHTra.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTenKHTra.Location = new System.Drawing.Point(13, 138);
            this.lblTenKHTra.Name = "lblTenKHTra";
            this.lblTenKHTra.Size = new System.Drawing.Size(77, 20);
            this.lblTenKHTra.TabIndex = 1;
            this.lblTenKHTra.Text = "Khách: ---";
            // 
            // txtMaDonHang
            // 
            this.txtMaDonHang.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.txtMaDonHang.BorderRadius = 8;
            this.txtMaDonHang.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaDonHang.DefaultText = "";
            this.txtMaDonHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtMaDonHang.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(251)))), ((int)(((byte)(235)))));
            this.txtMaDonHang.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtMaDonHang.Location = new System.Drawing.Point(13, 64);
            this.txtMaDonHang.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.txtMaDonHang.Name = "txtMaDonHang";
            this.txtMaDonHang.PlaceholderText = "Quét mã vạch Biên Lai (Mã Đơn Hàng)...";
            this.txtMaDonHang.SelectedText = "";
            this.txtMaDonHang.Size = new System.Drawing.Size(574, 51);
            this.txtMaDonHang.TabIndex = 4;
            this.txtMaDonHang.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaDonHang_KeyDown);
            // 
            // txtRfidTra
            // 
            this.txtRfidTra.BorderRadius = 8;
            this.txtRfidTra.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtRfidTra.DefaultText = "";
            this.txtRfidTra.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtRfidTra.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtRfidTra.Location = new System.Drawing.Point(13, 13);
            this.txtRfidTra.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtRfidTra.Name = "txtRfidTra";
            this.txtRfidTra.PlaceholderText = "Quẹt RFID khách trả đồ...";
            this.txtRfidTra.SelectedText = "";
            this.txtRfidTra.Size = new System.Drawing.Size(574, 51);
            this.txtRfidTra.TabIndex = 5;
            this.txtRfidTra.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtRfidTra_KeyDown);
            // 
            // pnlActionTra
            // 
            this.pnlActionTra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlActionTra.Controls.Add(this.btnBaoHong);
            this.pnlActionTra.Controls.Add(this.btnTraDu);
            this.pnlActionTra.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActionTra.Location = new System.Drawing.Point(0, 635);
            this.pnlActionTra.Name = "pnlActionTra";
            this.pnlActionTra.Padding = new System.Windows.Forms.Padding(9);
            this.pnlActionTra.Size = new System.Drawing.Size(611, 61);
            this.pnlActionTra.TabIndex = 1;
            // 
            // btnBaoHong
            // 
            this.btnBaoHong.BorderRadius = 10;
            this.btnBaoHong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnBaoHong.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnBaoHong.ForeColor = System.Drawing.Color.White;
            this.btnBaoHong.Location = new System.Drawing.Point(231, 9);
            this.btnBaoHong.Name = "btnBaoHong";
            this.btnBaoHong.Size = new System.Drawing.Size(197, 43);
            this.btnBaoHong.TabIndex = 0;
            this.btnBaoHong.Text = "BÁO HỎNG / MẤT";
            this.btnBaoHong.Click += new System.EventHandler(this.btnBaoHong_Click);
            // 
            // btnTraDu
            // 
            this.btnTraDu.BorderRadius = 10;
            this.btnTraDu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnTraDu.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnTraDu.ForeColor = System.Drawing.Color.White;
            this.btnTraDu.Location = new System.Drawing.Point(9, 9);
            this.btnTraDu.Name = "btnTraDu";
            this.btnTraDu.Size = new System.Drawing.Size(214, 43);
            this.btnTraDu.TabIndex = 1;
            this.btnTraDu.Text = "TRẢ ĐỦ - HOÀN CỌC";
            this.btnTraDu.Click += new System.EventHandler(this.btnTraDu_Click);
            // 
            // gridControlDangThue
            // 
            this.gridControlDangThue.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDangThue.Location = new System.Drawing.Point(0, 0);
            this.gridControlDangThue.MainView = this.gridViewDangThue;
            this.gridControlDangThue.Name = "gridControlDangThue";
            this.gridControlDangThue.Size = new System.Drawing.Size(611, 696);
            this.gridControlDangThue.TabIndex = 0;
            this.gridControlDangThue.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDangThue});
            // 
            // gridViewDangThue
            // 
            this.gridViewDangThue.DetailHeight = 303;
            this.gridViewDangThue.GridControl = this.gridControlDangThue;
            this.gridViewDangThue.Name = "gridViewDangThue";
            this.gridViewDangThue.OptionsBehavior.Editable = false;
            this.gridViewDangThue.OptionsView.ShowGroupPanel = false;
            this.gridViewDangThue.RowHeight = 39;
            // 
            // frmThueDo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1237, 789);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmThueDo";
            this.Text = "Cho thuê dịch vụ";
            this.Load += new System.EventHandler(this.frmThueDo_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.tabControl.ResumeLayout(false);
            this.tabGiaoDo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGiaoDo.Panel1)).EndInit();
            this.splitGiaoDo.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGiaoDo.Panel2)).EndInit();
            this.splitGiaoDo.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitGiaoDo)).EndInit();
            this.splitGiaoDo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSanPham)).EndInit();
            this.pnlKhachHangGiao.ResumeLayout(false);
            this.pnlKhachHangGiao.PerformLayout();
            this.pnlTongTien.ResumeLayout(false);
            this.pnlTongTien.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlGioHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewGioHang)).EndInit();
            this.tabNhanTra.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitNhanTra.Panel1)).EndInit();
            this.splitNhanTra.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitNhanTra.Panel2)).EndInit();
            this.splitNhanTra.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitNhanTra)).EndInit();
            this.splitNhanTra.ResumeLayout(false);
            this.pnlKhachHangTra.ResumeLayout(false);
            this.pnlKhachHangTra.PerformLayout();
            this.pnlActionTra.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDangThue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDangThue)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblFormTitle;
        private System.Windows.Forms.ComboBox cboTramChoThue;
        private System.Windows.Forms.Label lblTramIcon;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabGiaoDo;
        private System.Windows.Forms.TabPage tabNhanTra;
        private DevExpress.XtraEditors.SplitContainerControl splitGiaoDo;
        private DevExpress.XtraGrid.GridControl gridControlSanPham;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSanPham;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private System.Windows.Forms.Panel pnlKhachHangGiao;
        private Guna.UI2.WinForms.Guna2TextBox txtRfidGiao;
        private System.Windows.Forms.Label lblTenKH;
        private System.Windows.Forms.Label lblSoDuVi;
        private DevExpress.XtraGrid.GridControl gridControlGioHang;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewGioHang;
        private System.Windows.Forms.Panel pnlTongTien;
        private System.Windows.Forms.Label lblTongThue;
        private System.Windows.Forms.Label lblTongCoc;
        private System.Windows.Forms.Label lblTongCong;
        private Guna.UI2.WinForms.Guna2Button btnThanhToanRfid;
        private Guna.UI2.WinForms.Guna2Button btnThanhToanTienMat;
        private Guna.UI2.WinForms.Guna2Button btnHuyGiao;
        private DevExpress.XtraEditors.SplitContainerControl splitNhanTra;
        private System.Windows.Forms.Panel pnlKhachHangTra;
        private Guna.UI2.WinForms.Guna2TextBox txtRfidTra;
        private System.Windows.Forms.Label lblTenKHTra;
        private System.Windows.Forms.Label lblSoDuViTra;
        private DevExpress.XtraGrid.GridControl gridControlDangThue;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDangThue;
        private System.Windows.Forms.Panel pnlActionTra;
        private Guna.UI2.WinForms.Guna2Button btnTraDu;
        private Guna.UI2.WinForms.Guna2Button btnBaoHong;
        private Guna.UI2.WinForms.Guna2TextBox txtMaDonHang;
    }
}
