namespace GUI
{
    partial class frmNhanVien
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
            this.pnlToolbar = new Guna.UI2.WinForms.Guna2Panel();
            this.lblUserInfo = new System.Windows.Forms.Label();
            this.btnXuatExcel = new Guna.UI2.WinForms.Guna2Button();
            this.btnInHoSo = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnlLeftCard = new Guna.UI2.WinForms.Guna2Panel();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlSearchArea = new Guna.UI2.WinForms.Guna2Panel();
            this.cboLocKhoi = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlDetail = new System.Windows.Forms.Panel();
            this.tabControlDetails = new Guna.UI2.WinForms.Guna2TabControl();
            this.tabHoSo = new System.Windows.Forms.TabPage();
            this.pnlHoSoScroll = new System.Windows.Forms.Panel();
            this.tblHoSo = new System.Windows.Forms.TableLayoutPanel();
            this.tabChamCong = new System.Windows.Forms.TabPage();
            this.splitChamCong = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridLichLamViec = new DevExpress.XtraGrid.GridControl();
            this.gridViewLichLamViec = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblLichLamViecTitle = new System.Windows.Forms.Label();
            this.gridChamCong = new DevExpress.XtraGrid.GridControl();
            this.gridViewChamCong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblChamCongHistoryTitle = new System.Windows.Forms.Label();
            this.pnlCCToolbar = new System.Windows.Forms.Panel();
            this.btnMoLichLamViec = new Guna.UI2.WinForms.Guna2Button();
            this.dtpKyDoiSoat = new DevExpress.XtraEditors.DateEdit();
            this.lblKyDoiSoat = new System.Windows.Forms.Label();
            this.tabNgayNghi = new System.Windows.Forms.TabPage();
            this.gridDonXinNghi = new DevExpress.XtraGrid.GridControl();
            this.gridViewDonXinNghi = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlDXNHeader = new System.Windows.Forms.Panel();
            this.btnXoaDXN = new Guna.UI2.WinForms.Guna2Button();
            this.btnThemDXN = new Guna.UI2.WinForms.Guna2Button();
            this.lblDonXinNghiTitle = new System.Windows.Forms.Label();
            this.pnlNghiBuBanner = new Guna.UI2.WinForms.Guna2Panel();
            this.lblNghiBuInfo = new System.Windows.Forms.Label();
            this.pnlKpiPhep = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlPhepConLai = new Guna.UI2.WinForms.Guna2Panel();
            this.lblPhepConLaiNote = new System.Windows.Forms.Label();
            this.lblPhepConLaiSo = new System.Windows.Forms.Label();
            this.lblPhepConLaiTitle = new System.Windows.Forms.Label();
            this.pnlPhepDaDung = new Guna.UI2.WinForms.Guna2Panel();
            this.lblPhepDaDungNote = new System.Windows.Forms.Label();
            this.lblPhepDaDungSo = new System.Windows.Forms.Label();
            this.lblPhepDaDungTitle = new System.Windows.Forms.Label();
            this.pnlPhepTong = new Guna.UI2.WinForms.Guna2Panel();
            this.lblPhepTongNote = new System.Windows.Forms.Label();
            this.lblPhepTongSo = new System.Windows.Forms.Label();
            this.lblPhepTongTitle = new System.Windows.Forms.Label();
            this.pnlNNToolbar = new System.Windows.Forms.Panel();
            this.cboNamTaiChinh = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblNamTaiChinh = new System.Windows.Forms.Label();
            this.tabRuiRo = new System.Windows.Forms.TabPage();
            this.tableLayoutRuiRo = new System.Windows.Forms.TableLayoutPanel();
            this.pnlTaiNan = new System.Windows.Forms.Panel();
            this.gridTaiNanLaoDong = new DevExpress.XtraGrid.GridControl();
            this.gridViewTaiNanLaoDong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlTNHeader = new System.Windows.Forms.Panel();
            this.btnXoaTN = new Guna.UI2.WinForms.Guna2Button();
            this.btnThemTN = new Guna.UI2.WinForms.Guna2Button();
            this.lblTaiNanTitle = new System.Windows.Forms.Label();
            this.pnlKyLuat = new System.Windows.Forms.Panel();
            this.gridKyLuat = new DevExpress.XtraGrid.GridControl();
            this.gridViewKyLuat = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlKLHeader = new System.Windows.Forms.Panel();
            this.btnXoaKL = new Guna.UI2.WinForms.Guna2Button();
            this.btnThemKL = new Guna.UI2.WinForms.Guna2Button();
            this.lblKyLuatTitle = new System.Windows.Forms.Label();
            this.gridChungChi = new DevExpress.XtraGrid.GridControl();
            this.gridViewChungChi = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlCCHeader = new System.Windows.Forms.Panel();
            this.btnXoaCC = new Guna.UI2.WinForms.Guna2Button();
            this.btnThemCC = new Guna.UI2.WinForms.Guna2Button();
            this.lblChungChiTitle = new System.Windows.Forms.Label();
            this.pnlNVHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSDTEmailInfo = new System.Windows.Forms.Label();
            this.lblLuongInfo = new System.Windows.Forms.Label();
            this.lblHopDongInfo = new System.Windows.Forms.Label();
            this.lblChucVuInfo = new System.Windows.Forms.Label();
            this.lblTrangThaiNV = new System.Windows.Forms.Label();
            this.lblMaNV = new System.Windows.Forms.Label();
            this.lblTenNV = new System.Windows.Forms.Label();
            this.btnChonAnh = new Guna.UI2.WinForms.Guna2Button();
            this.picAvatar = new System.Windows.Forms.PictureBox();
            this.lblNoSelection = new System.Windows.Forms.Label();
            this.lblGrpCaNhan = new System.Windows.Forms.Label();
            this.lblGrpCongTac = new System.Windows.Forms.Label();
            this.lblGrpLuong = new System.Windows.Forms.Label();
            this.lblGrpTaiKhoan = new System.Windows.Forms.Label();
            this.lblGrpGhiChu = new System.Windows.Forms.Label();
            this.lblNgayPhep = new System.Windows.Forms.Label();
            this.lblHoTen = new System.Windows.Forms.Label();
            this.lblMaCode = new System.Windows.Forms.Label();
            this.lblGioiTinh = new System.Windows.Forms.Label();
            this.lblNgaySinh = new System.Windows.Forms.Label();
            this.lblCCCD = new System.Windows.Forms.Label();
            this.lblSDT = new System.Windows.Forms.Label();
            this.lblEmail = new System.Windows.Forms.Label();
            this.lblDiaChi = new System.Windows.Forms.Label();
            this.lblChucVulbl = new System.Windows.Forms.Label();
            this.lblNguoiQuanLy = new System.Windows.Forms.Label();
            this.lblNgayVaoLam = new System.Windows.Forms.Label();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.lblLoaiHopDong = new System.Windows.Forms.Label();
            this.lblNhomCongViec = new System.Windows.Forms.Label();
            this.lblLuongCoBan = new System.Windows.Forms.Label();
            this.lblLuongTheoGio = new System.Windows.Forms.Label();
            this.lblUsername = new System.Windows.Forms.Label();
            this.lblPassword = new System.Windows.Forms.Label();
            this.lblAccountRole = new System.Windows.Forms.Label();
            this.txtHoTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMaCode = new Guna.UI2.WinForms.Guna2TextBox();
            this.cboGioiTinh = new Guna.UI2.WinForms.Guna2ComboBox();
            this.dtpNgaySinh = new DevExpress.XtraEditors.DateEdit();
            this.txtCCCD = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSDT = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtEmail = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtDiaChi = new Guna.UI2.WinForms.Guna2TextBox();
            this.slkChucVu = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.slkNguoiQuanLy = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.dtpNgayVaoLam = new DevExpress.XtraEditors.DateEdit();
            this.cboTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboLoaiHopDong = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboNhomCongViec = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtLuongCoBan = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtLuongTheoGio = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtUsername = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtPassword = new Guna.UI2.WinForms.Guna2TextBox();
            this.slkAccountRole = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.txtGhiChu = new DevExpress.XtraEditors.MemoEdit();
            this.pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).BeginInit();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnlLeftCard.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.pnlSearchArea.SuspendLayout();
            this.pnlDetail.SuspendLayout();
            this.tabControlDetails.SuspendLayout();
            this.tabHoSo.SuspendLayout();
            this.pnlHoSoScroll.SuspendLayout();
            this.tabChamCong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitChamCong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitChamCong.Panel1)).BeginInit();
            this.splitChamCong.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitChamCong.Panel2)).BeginInit();
            this.splitChamCong.Panel2.SuspendLayout();
            this.splitChamCong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLichLamViec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLichLamViec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridChamCong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChamCong)).BeginInit();
            this.pnlCCToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpKyDoiSoat.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpKyDoiSoat.Properties.CalendarTimeProperties)).BeginInit();
            this.tabNgayNghi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDonXinNghi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDonXinNghi)).BeginInit();
            this.pnlDXNHeader.SuspendLayout();
            this.pnlNghiBuBanner.SuspendLayout();
            this.pnlKpiPhep.SuspendLayout();
            this.pnlPhepConLai.SuspendLayout();
            this.pnlPhepDaDung.SuspendLayout();
            this.pnlPhepTong.SuspendLayout();
            this.pnlNNToolbar.SuspendLayout();
            this.tabRuiRo.SuspendLayout();
            this.tableLayoutRuiRo.SuspendLayout();
            this.pnlTaiNan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTaiNanLaoDong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTaiNanLaoDong)).BeginInit();
            this.pnlTNHeader.SuspendLayout();
            this.pnlKyLuat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridKyLuat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewKyLuat)).BeginInit();
            this.pnlKLHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridChungChi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChungChi)).BeginInit();
            this.pnlCCHeader.SuspendLayout();
            this.pnlNVHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgaySinh.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgaySinh.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkChucVu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkNguoiQuanLy.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayVaoLam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayVaoLam.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkAccountRole.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGhiChu.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.Controls.Add(this.lblUserInfo);
            this.pnlToolbar.Controls.Add(this.btnXuatExcel);
            this.pnlToolbar.Controls.Add(this.btnInHoSo);
            this.pnlToolbar.Controls.Add(this.btnLamMoi);
            this.pnlToolbar.Controls.Add(this.btnXoa);
            this.pnlToolbar.Controls.Add(this.btnSua);
            this.pnlToolbar.Controls.Add(this.btnThem);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(19)))), ((int)(((byte)(26)))), ((int)(((byte)(37)))));
            this.pnlToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Size = new System.Drawing.Size(1400, 55);
            this.pnlToolbar.TabIndex = 0;
            // 
            // lblUserInfo
            // 
            this.lblUserInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblUserInfo.AutoSize = true;
            this.lblUserInfo.BackColor = System.Drawing.Color.Transparent;
            this.lblUserInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblUserInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblUserInfo.Location = new System.Drawing.Point(1180, 19);
            this.lblUserInfo.Name = "lblUserInfo";
            this.lblUserInfo.Size = new System.Drawing.Size(43, 15);
            this.lblUserInfo.TabIndex = 0;
            this.lblUserInfo.Text = "Admin";
            // 
            // btnXuatExcel
            // 
            this.btnXuatExcel.Animated = true;
            this.btnXuatExcel.BackColor = System.Drawing.Color.Transparent;
            this.btnXuatExcel.BorderRadius = 4;
            this.btnXuatExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatExcel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.btnXuatExcel.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnXuatExcel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.btnXuatExcel.Location = new System.Drawing.Point(637, 10);
            this.btnXuatExcel.Name = "btnXuatExcel";
            this.btnXuatExcel.Size = new System.Drawing.Size(125, 35);
            this.btnXuatExcel.TabIndex = 5;
            this.btnXuatExcel.Text = "  XUẤT EXCEL";
            // 
            // btnInHoSo
            // 
            this.btnInHoSo.Animated = true;
            this.btnInHoSo.BackColor = System.Drawing.Color.Transparent;
            this.btnInHoSo.BorderRadius = 4;
            this.btnInHoSo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInHoSo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.btnInHoSo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnInHoSo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.btnInHoSo.Location = new System.Drawing.Point(512, 10);
            this.btnInHoSo.Name = "btnInHoSo";
            this.btnInHoSo.Size = new System.Drawing.Size(115, 35);
            this.btnInHoSo.TabIndex = 4;
            this.btnInHoSo.Text = "  IN HỒ SƠ";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Animated = true;
            this.btnLamMoi.BackColor = System.Drawing.Color.Transparent;
            this.btnLamMoi.BorderRadius = 4;
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.btnLamMoi.Location = new System.Drawing.Point(387, 10);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(115, 35);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "  LÀM MỚI";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Animated = true;
            this.btnXoa.BackColor = System.Drawing.Color.Transparent;
            this.btnXoa.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoa.BorderRadius = 4;
            this.btnXoa.BorderThickness = 2;
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FillColor = System.Drawing.Color.Transparent;
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoa.HoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoa.HoverState.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(277, 10);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 35);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "  XÓA";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnSua
            // 
            this.btnSua.Animated = true;
            this.btnSua.BackColor = System.Drawing.Color.Transparent;
            this.btnSua.BorderRadius = 4;
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(167, 10);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(100, 35);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "  SỬA";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnThem
            // 
            this.btnThem.Animated = true;
            this.btnThem.BackColor = System.Drawing.Color.Transparent;
            this.btnThem.BorderRadius = 4;
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(12, 10);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(145, 35);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "  THÊM MỚI NV";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 55);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.pnlLeftCard);
            this.splitMain.Panel1.Padding = new System.Windows.Forms.Padding(12);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlDetail);
            this.splitMain.ShowSplitGlyph = DevExpress.Utils.DefaultBoolean.True;
            this.splitMain.Size = new System.Drawing.Size(1400, 969);
            this.splitMain.SplitterPosition = 420;
            this.splitMain.TabIndex = 1;
            // 
            // pnlLeftCard
            // 
            this.pnlLeftCard.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(65)))), ((int)(((byte)(81)))));
            this.pnlLeftCard.BorderRadius = 8;
            this.pnlLeftCard.BorderThickness = 1;
            this.pnlLeftCard.Controls.Add(this.gridControl);
            this.pnlLeftCard.Controls.Add(this.pnlSearchArea);
            this.pnlLeftCard.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeftCard.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.pnlLeftCard.Location = new System.Drawing.Point(12, 12);
            this.pnlLeftCard.Name = "pnlLeftCard";
            this.pnlLeftCard.Size = new System.Drawing.Size(396, 945);
            this.pnlLeftCard.TabIndex = 0;
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 112);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(396, 833);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.ColumnPanelRowHeight = 38;
            this.gridView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.None;
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsFind.AlwaysVisible = true;
            this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.OptionsView.ShowIndicator = false;
            this.gridView.RowHeight = 48;
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.OnFocusedRowChanged);
            // 
            // pnlSearchArea
            // 
            this.pnlSearchArea.Controls.Add(this.cboLocKhoi);
            this.pnlSearchArea.Controls.Add(this.txtTimKiem);
            this.pnlSearchArea.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSearchArea.FillColor = System.Drawing.Color.Transparent;
            this.pnlSearchArea.Location = new System.Drawing.Point(0, 0);
            this.pnlSearchArea.Name = "pnlSearchArea";
            this.pnlSearchArea.Padding = new System.Windows.Forms.Padding(10, 10, 10, 6);
            this.pnlSearchArea.Size = new System.Drawing.Size(396, 112);
            this.pnlSearchArea.TabIndex = 1;
            // 
            // cboLocKhoi
            // 
            this.cboLocKhoi.BackColor = System.Drawing.Color.Transparent;
            this.cboLocKhoi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.cboLocKhoi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLocKhoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLocKhoi.FocusedColor = System.Drawing.Color.Empty;
            this.cboLocKhoi.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.cboLocKhoi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.cboLocKhoi.ItemHeight = 30;
            this.cboLocKhoi.Items.AddRange(new object[] {
            "Tat ca khoi",
            "Chi Van Hanh",
            "Chi Hanh Chinh"});
            this.cboLocKhoi.Location = new System.Drawing.Point(10, 70);
            this.cboLocKhoi.Name = "cboLocKhoi";
            this.cboLocKhoi.Size = new System.Drawing.Size(376, 36);
            this.cboLocKhoi.TabIndex = 1;
            this.cboLocKhoi.SelectedIndexChanged += new System.EventHandler(this.cboLocKhoi_SelectedIndexChanged);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderRadius = 4;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.txtTimKiem.Location = new System.Drawing.Point(10, 10);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "  Tim Ma NV, Ten, SDT...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(376, 42);
            this.txtTimKiem.TabIndex = 0;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // pnlDetail
            // 
            this.pnlDetail.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlDetail.Controls.Add(this.tabControlDetails);
            this.pnlDetail.Controls.Add(this.pnlNVHeader);
            this.pnlDetail.Controls.Add(this.lblNoSelection);
            this.pnlDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlDetail.Location = new System.Drawing.Point(0, 0);
            this.pnlDetail.Name = "pnlDetail";
            this.pnlDetail.Padding = new System.Windows.Forms.Padding(15);
            this.pnlDetail.Size = new System.Drawing.Size(970, 969);
            this.pnlDetail.TabIndex = 0;
            // 
            // tabControlDetails
            // 
            this.tabControlDetails.Controls.Add(this.tabHoSo);
            this.tabControlDetails.Controls.Add(this.tabChamCong);
            this.tabControlDetails.Controls.Add(this.tabNgayNghi);
            this.tabControlDetails.Controls.Add(this.tabRuiRo);
            this.tabControlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDetails.ItemSize = new System.Drawing.Size(220, 40);
            this.tabControlDetails.Location = new System.Drawing.Point(15, 189);
            this.tabControlDetails.Name = "tabControlDetails";
            this.tabControlDetails.SelectedIndex = 0;
            this.tabControlDetails.Size = new System.Drawing.Size(940, 765);
            this.tabControlDetails.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.tabControlDetails.TabButtonHoverState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tabControlDetails.TabButtonHoverState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tabControlDetails.TabButtonHoverState.ForeColor = System.Drawing.Color.White;
            this.tabControlDetails.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tabControlDetails.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.tabControlDetails.TabButtonIdleState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabControlDetails.TabButtonIdleState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tabControlDetails.TabButtonIdleState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.tabControlDetails.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabControlDetails.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.tabControlDetails.TabButtonSelectedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tabControlDetails.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F, System.Drawing.FontStyle.Bold);
            this.tabControlDetails.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.tabControlDetails.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.tabControlDetails.TabButtonSize = new System.Drawing.Size(220, 40);
            this.tabControlDetails.TabIndex = 1;
            this.tabControlDetails.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabControlDetails.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.HorizontalTop;
            this.tabControlDetails.Visible = false;
            this.tabControlDetails.SelectedIndexChanged += new System.EventHandler(this.tabControlDetails_SelectedIndexChanged);
            // 
            // tabHoSo
            // 
            this.tabHoSo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tabHoSo.Controls.Add(this.pnlHoSoScroll);
            this.tabHoSo.Location = new System.Drawing.Point(4, 44);
            this.tabHoSo.Name = "tabHoSo";
            this.tabHoSo.Size = new System.Drawing.Size(932, 717);
            this.tabHoSo.TabIndex = 0;
            this.tabHoSo.Text = "  HỒ SƠ GỐC";
            // 
            // pnlHoSoScroll
            // 
            this.pnlHoSoScroll.AutoScroll = true;
            this.pnlHoSoScroll.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.pnlHoSoScroll.Controls.Add(this.tblHoSo);
            this.pnlHoSoScroll.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlHoSoScroll.Location = new System.Drawing.Point(0, 0);
            this.pnlHoSoScroll.Name = "pnlHoSoScroll";
            this.pnlHoSoScroll.Padding = new System.Windows.Forms.Padding(16);
            this.pnlHoSoScroll.Size = new System.Drawing.Size(932, 717);
            this.pnlHoSoScroll.TabIndex = 0;
            // 
            // tblHoSo
            // 
            this.tblHoSo.AutoSize = true;
            this.tblHoSo.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tblHoSo.BackColor = System.Drawing.Color.Transparent;
            this.tblHoSo.ColumnCount = 2;
            this.tblHoSo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblHoSo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tblHoSo.Dock = System.Windows.Forms.DockStyle.Top;
            this.tblHoSo.Location = new System.Drawing.Point(16, 16);
            this.tblHoSo.Name = "tblHoSo";
            this.tblHoSo.Padding = new System.Windows.Forms.Padding(4);
            this.tblHoSo.RowCount = 27;
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tblHoSo.Size = new System.Drawing.Size(900, 548);
            this.tblHoSo.TabIndex = 0;
            // 
            // tabChamCong
            // 
            this.tabChamCong.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tabChamCong.Controls.Add(this.splitChamCong);
            this.tabChamCong.Controls.Add(this.pnlCCToolbar);
            this.tabChamCong.Location = new System.Drawing.Point(4, 44);
            this.tabChamCong.Name = "tabChamCong";
            this.tabChamCong.Size = new System.Drawing.Size(932, 717);
            this.tabChamCong.TabIndex = 1;
            this.tabChamCong.Text = "  CHẤM CÔNG";
            // 
            // splitChamCong
            // 
            this.splitChamCong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitChamCong.Horizontal = false;
            this.splitChamCong.Location = new System.Drawing.Point(0, 52);
            this.splitChamCong.Name = "splitChamCong";
            // 
            // splitChamCong.Panel1
            // 
            this.splitChamCong.Panel1.Controls.Add(this.gridLichLamViec);
            this.splitChamCong.Panel1.Controls.Add(this.lblLichLamViecTitle);
            // 
            // splitChamCong.Panel2
            // 
            this.splitChamCong.Panel2.Controls.Add(this.gridChamCong);
            this.splitChamCong.Panel2.Controls.Add(this.lblChamCongHistoryTitle);
            this.splitChamCong.ShowSplitGlyph = DevExpress.Utils.DefaultBoolean.True;
            this.splitChamCong.Size = new System.Drawing.Size(932, 665);
            this.splitChamCong.SplitterPosition = 330;
            this.splitChamCong.TabIndex = 1;
            // 
            // gridLichLamViec
            // 
            this.gridLichLamViec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLichLamViec.Location = new System.Drawing.Point(0, 28);
            this.gridLichLamViec.MainView = this.gridViewLichLamViec;
            this.gridLichLamViec.Name = "gridLichLamViec";
            this.gridLichLamViec.Size = new System.Drawing.Size(932, 302);
            this.gridLichLamViec.TabIndex = 0;
            this.gridLichLamViec.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLichLamViec});
            // 
            // gridViewLichLamViec
            // 
            this.gridViewLichLamViec.ColumnPanelRowHeight = 38;
            this.gridViewLichLamViec.GridControl = this.gridLichLamViec;
            this.gridViewLichLamViec.Name = "gridViewLichLamViec";
            this.gridViewLichLamViec.OptionsBehavior.Editable = false;
            this.gridViewLichLamViec.OptionsView.ShowGroupPanel = false;
            this.gridViewLichLamViec.OptionsView.ShowIndicator = false;
            this.gridViewLichLamViec.RowHeight = 35;
            // 
            // lblLichLamViecTitle
            // 
            this.lblLichLamViecTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblLichLamViecTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblLichLamViecTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblLichLamViecTitle.Location = new System.Drawing.Point(0, 0);
            this.lblLichLamViecTitle.Name = "lblLichLamViecTitle";
            this.lblLichLamViecTitle.Padding = new System.Windows.Forms.Padding(8, 5, 0, 0);
            this.lblLichLamViecTitle.Size = new System.Drawing.Size(932, 28);
            this.lblLichLamViecTitle.TabIndex = 1;
            this.lblLichLamViecTitle.Text = "LICH LAM VIEC DU KIEN";
            // 
            // gridChamCong
            // 
            this.gridChamCong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridChamCong.Location = new System.Drawing.Point(0, 28);
            this.gridChamCong.MainView = this.gridViewChamCong;
            this.gridChamCong.Name = "gridChamCong";
            this.gridChamCong.Size = new System.Drawing.Size(932, 297);
            this.gridChamCong.TabIndex = 0;
            this.gridChamCong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewChamCong});
            // 
            // gridViewChamCong
            // 
            this.gridViewChamCong.ColumnPanelRowHeight = 38;
            this.gridViewChamCong.GridControl = this.gridChamCong;
            this.gridViewChamCong.Name = "gridViewChamCong";
            this.gridViewChamCong.OptionsBehavior.Editable = false;
            this.gridViewChamCong.OptionsView.ShowGroupPanel = false;
            this.gridViewChamCong.OptionsView.ShowIndicator = false;
            this.gridViewChamCong.RowHeight = 35;
            // 
            // lblChamCongHistoryTitle
            // 
            this.lblChamCongHistoryTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblChamCongHistoryTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblChamCongHistoryTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblChamCongHistoryTitle.Location = new System.Drawing.Point(0, 0);
            this.lblChamCongHistoryTitle.Name = "lblChamCongHistoryTitle";
            this.lblChamCongHistoryTitle.Padding = new System.Windows.Forms.Padding(8, 5, 0, 0);
            this.lblChamCongHistoryTitle.Size = new System.Drawing.Size(932, 28);
            this.lblChamCongHistoryTitle.TabIndex = 1;
            this.lblChamCongHistoryTitle.Text = "LICH SU QUET THE THUC TE";
            // 
            // pnlCCToolbar
            // 
            this.pnlCCToolbar.BackColor = System.Drawing.Color.Transparent;
            this.pnlCCToolbar.Controls.Add(this.btnMoLichLamViec);
            this.pnlCCToolbar.Controls.Add(this.dtpKyDoiSoat);
            this.pnlCCToolbar.Controls.Add(this.lblKyDoiSoat);
            this.pnlCCToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCCToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlCCToolbar.Name = "pnlCCToolbar";
            this.pnlCCToolbar.Size = new System.Drawing.Size(932, 52);
            this.pnlCCToolbar.TabIndex = 0;
            // 
            // btnMoLichLamViec
            // 
            this.btnMoLichLamViec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnMoLichLamViec.Animated = true;
            this.btnMoLichLamViec.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.btnMoLichLamViec.BorderRadius = 4;
            this.btnMoLichLamViec.BorderThickness = 2;
            this.btnMoLichLamViec.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnMoLichLamViec.FillColor = System.Drawing.Color.Transparent;
            this.btnMoLichLamViec.Font = new System.Drawing.Font("Segoe UI Semibold", 9F, System.Drawing.FontStyle.Bold);
            this.btnMoLichLamViec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.btnMoLichLamViec.Location = new System.Drawing.Point(762, 11);
            this.btnMoLichLamViec.Name = "btnMoLichLamViec";
            this.btnMoLichLamViec.Size = new System.Drawing.Size(155, 30);
            this.btnMoLichLamViec.TabIndex = 0;
            this.btnMoLichLamViec.Text = "Mo Lich Lam Viec";
            this.btnMoLichLamViec.Click += new System.EventHandler(this.btnMoLichLamViec_Click);
            // 
            // dtpKyDoiSoat
            // 
            this.dtpKyDoiSoat.EditValue = new System.DateTime(2026, 4, 14, 0, 0, 0, 0);
            this.dtpKyDoiSoat.Location = new System.Drawing.Point(105, 13);
            this.dtpKyDoiSoat.Name = "dtpKyDoiSoat";
            this.dtpKyDoiSoat.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpKyDoiSoat.Size = new System.Drawing.Size(140, 20);
            this.dtpKyDoiSoat.TabIndex = 1;
            // 
            // lblKyDoiSoat
            // 
            this.lblKyDoiSoat.AutoSize = true;
            this.lblKyDoiSoat.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblKyDoiSoat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.lblKyDoiSoat.Location = new System.Drawing.Point(15, 17);
            this.lblKyDoiSoat.Name = "lblKyDoiSoat";
            this.lblKyDoiSoat.Size = new System.Drawing.Size(67, 15);
            this.lblKyDoiSoat.TabIndex = 2;
            this.lblKyDoiSoat.Text = "Ky doi soat:";
            // 
            // tabNgayNghi
            // 
            this.tabNgayNghi.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tabNgayNghi.Controls.Add(this.gridDonXinNghi);
            this.tabNgayNghi.Controls.Add(this.pnlDXNHeader);
            this.tabNgayNghi.Controls.Add(this.pnlNghiBuBanner);
            this.tabNgayNghi.Controls.Add(this.pnlKpiPhep);
            this.tabNgayNghi.Controls.Add(this.pnlNNToolbar);
            this.tabNgayNghi.Location = new System.Drawing.Point(4, 44);
            this.tabNgayNghi.Name = "tabNgayNghi";
            this.tabNgayNghi.Size = new System.Drawing.Size(932, 717);
            this.tabNgayNghi.TabIndex = 2;
            this.tabNgayNghi.Text = "  NGÀY NGHỈ";
            // 
            // gridDonXinNghi
            // 
            this.gridDonXinNghi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDonXinNghi.Location = new System.Drawing.Point(0, 268);
            this.gridDonXinNghi.MainView = this.gridViewDonXinNghi;
            this.gridDonXinNghi.Name = "gridDonXinNghi";
            this.gridDonXinNghi.Size = new System.Drawing.Size(932, 449);
            this.gridDonXinNghi.TabIndex = 0;
            this.gridDonXinNghi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDonXinNghi});
            // 
            // gridViewDonXinNghi
            // 
            this.gridViewDonXinNghi.ColumnPanelRowHeight = 38;
            this.gridViewDonXinNghi.GridControl = this.gridDonXinNghi;
            this.gridViewDonXinNghi.Name = "gridViewDonXinNghi";
            this.gridViewDonXinNghi.OptionsBehavior.Editable = false;
            this.gridViewDonXinNghi.OptionsView.ShowGroupPanel = false;
            this.gridViewDonXinNghi.OptionsView.ShowIndicator = false;
            this.gridViewDonXinNghi.RowHeight = 35;
            // 
            // pnlDXNHeader
            // 
            this.pnlDXNHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlDXNHeader.Controls.Add(this.btnXoaDXN);
            this.pnlDXNHeader.Controls.Add(this.btnThemDXN);
            this.pnlDXNHeader.Controls.Add(this.lblDonXinNghiTitle);
            this.pnlDXNHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDXNHeader.Location = new System.Drawing.Point(0, 222);
            this.pnlDXNHeader.Name = "pnlDXNHeader";
            this.pnlDXNHeader.Size = new System.Drawing.Size(932, 46);
            this.pnlDXNHeader.TabIndex = 3;
            // 
            // btnXoaDXN
            // 
            this.btnXoaDXN.Animated = true;
            this.btnXoaDXN.BorderRadius = 4;
            this.btnXoaDXN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaDXN.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoaDXN.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnXoaDXN.ForeColor = System.Drawing.Color.White;
            this.btnXoaDXN.Location = new System.Drawing.Point(815, 8);
            this.btnXoaDXN.Name = "btnXoaDXN";
            this.btnXoaDXN.Size = new System.Drawing.Size(72, 30);
            this.btnXoaDXN.TabIndex = 1;
            this.btnXoaDXN.Text = "Xoa";
            this.btnXoaDXN.Click += new System.EventHandler(this.BtnXoaDXN_Click);
            // 
            // btnThemDXN
            // 
            this.btnThemDXN.Animated = true;
            this.btnThemDXN.BorderRadius = 4;
            this.btnThemDXN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemDXN.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnThemDXN.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnThemDXN.ForeColor = System.Drawing.Color.White;
            this.btnThemDXN.Location = new System.Drawing.Point(700, 8);
            this.btnThemDXN.Name = "btnThemDXN";
            this.btnThemDXN.Size = new System.Drawing.Size(110, 30);
            this.btnThemDXN.TabIndex = 0;
            this.btnThemDXN.Text = "+ Tao Don Moi";
            this.btnThemDXN.Click += new System.EventHandler(this.BtnThemDXN_Click);
            // 
            // lblDonXinNghiTitle
            // 
            this.lblDonXinNghiTitle.AutoSize = true;
            this.lblDonXinNghiTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblDonXinNghiTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblDonXinNghiTitle.Location = new System.Drawing.Point(12, 14);
            this.lblDonXinNghiTitle.Name = "lblDonXinNghiTitle";
            this.lblDonXinNghiTitle.Size = new System.Drawing.Size(137, 15);
            this.lblDonXinNghiTitle.TabIndex = 2;
            this.lblDonXinNghiTitle.Text = "LICH SU DON XIN NGHI";
            // 
            // pnlNghiBuBanner
            // 
            this.pnlNghiBuBanner.Controls.Add(this.lblNghiBuInfo);
            this.pnlNghiBuBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNghiBuBanner.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(53)))), ((int)(((byte)(15)))));
            this.pnlNghiBuBanner.Location = new System.Drawing.Point(0, 184);
            this.pnlNghiBuBanner.Name = "pnlNghiBuBanner";
            this.pnlNghiBuBanner.Padding = new System.Windows.Forms.Padding(15, 6, 15, 6);
            this.pnlNghiBuBanner.Size = new System.Drawing.Size(932, 38);
            this.pnlNghiBuBanner.TabIndex = 2;
            // 
            // lblNghiBuInfo
            // 
            this.lblNghiBuInfo.AutoSize = true;
            this.lblNghiBuInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNghiBuInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblNghiBuInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(253)))), ((int)(((byte)(230)))), ((int)(((byte)(138)))));
            this.lblNghiBuInfo.Location = new System.Drawing.Point(15, 6);
            this.lblNghiBuInfo.Name = "lblNghiBuInfo";
            this.lblNghiBuInfo.Size = new System.Drawing.Size(185, 15);
            this.lblNghiBuInfo.TabIndex = 0;
            this.lblNghiBuInfo.Text = "NGHI BU TICH LUY TU OT: - ngay";
            // 
            // pnlKpiPhep
            // 
            this.pnlKpiPhep.Controls.Add(this.pnlPhepConLai);
            this.pnlKpiPhep.Controls.Add(this.pnlPhepDaDung);
            this.pnlKpiPhep.Controls.Add(this.pnlPhepTong);
            this.pnlKpiPhep.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKpiPhep.FillColor = System.Drawing.Color.Transparent;
            this.pnlKpiPhep.Location = new System.Drawing.Point(0, 48);
            this.pnlKpiPhep.Name = "pnlKpiPhep";
            this.pnlKpiPhep.Padding = new System.Windows.Forms.Padding(12, 8, 12, 8);
            this.pnlKpiPhep.Size = new System.Drawing.Size(932, 136);
            this.pnlKpiPhep.TabIndex = 1;
            // 
            // pnlPhepConLai
            // 
            this.pnlPhepConLai.BorderRadius = 8;
            this.pnlPhepConLai.Controls.Add(this.lblPhepConLaiNote);
            this.pnlPhepConLai.Controls.Add(this.lblPhepConLaiSo);
            this.pnlPhepConLai.Controls.Add(this.lblPhepConLaiTitle);
            this.pnlPhepConLai.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPhepConLai.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.pnlPhepConLai.Location = new System.Drawing.Point(552, 8);
            this.pnlPhepConLai.Name = "pnlPhepConLai";
            this.pnlPhepConLai.Padding = new System.Windows.Forms.Padding(16, 12, 16, 8);
            this.pnlPhepConLai.Size = new System.Drawing.Size(270, 120);
            this.pnlPhepConLai.TabIndex = 2;
            // 
            // lblPhepConLaiNote
            // 
            this.lblPhepConLaiNote.BackColor = System.Drawing.Color.Transparent;
            this.lblPhepConLaiNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhepConLaiNote.Font = new System.Drawing.Font("Segoe UI Semibold", 8F);
            this.lblPhepConLaiNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblPhepConLaiNote.Location = new System.Drawing.Point(16, 72);
            this.lblPhepConLaiNote.Name = "lblPhepConLaiNote";
            this.lblPhepConLaiNote.Size = new System.Drawing.Size(238, 40);
            this.lblPhepConLaiNote.TabIndex = 0;
            this.lblPhepConLaiNote.Text = "ngay";
            // 
            // lblPhepConLaiSo
            // 
            this.lblPhepConLaiSo.BackColor = System.Drawing.Color.Transparent;
            this.lblPhepConLaiSo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPhepConLaiSo.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            this.lblPhepConLaiSo.ForeColor = System.Drawing.Color.White;
            this.lblPhepConLaiSo.Location = new System.Drawing.Point(16, 34);
            this.lblPhepConLaiSo.Name = "lblPhepConLaiSo";
            this.lblPhepConLaiSo.Size = new System.Drawing.Size(238, 38);
            this.lblPhepConLaiSo.TabIndex = 1;
            this.lblPhepConLaiSo.Text = "--";
            // 
            // lblPhepConLaiTitle
            // 
            this.lblPhepConLaiTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPhepConLaiTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPhepConLaiTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPhepConLaiTitle.ForeColor = System.Drawing.Color.White;
            this.lblPhepConLaiTitle.Location = new System.Drawing.Point(16, 12);
            this.lblPhepConLaiTitle.Name = "lblPhepConLaiTitle";
            this.lblPhepConLaiTitle.Size = new System.Drawing.Size(238, 22);
            this.lblPhepConLaiTitle.TabIndex = 2;
            this.lblPhepConLaiTitle.Text = "CON LAI";
            // 
            // pnlPhepDaDung
            // 
            this.pnlPhepDaDung.BorderRadius = 8;
            this.pnlPhepDaDung.Controls.Add(this.lblPhepDaDungNote);
            this.pnlPhepDaDung.Controls.Add(this.lblPhepDaDungSo);
            this.pnlPhepDaDung.Controls.Add(this.lblPhepDaDungTitle);
            this.pnlPhepDaDung.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPhepDaDung.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.pnlPhepDaDung.Location = new System.Drawing.Point(282, 8);
            this.pnlPhepDaDung.Name = "pnlPhepDaDung";
            this.pnlPhepDaDung.Padding = new System.Windows.Forms.Padding(16, 12, 16, 8);
            this.pnlPhepDaDung.Size = new System.Drawing.Size(270, 120);
            this.pnlPhepDaDung.TabIndex = 1;
            // 
            // lblPhepDaDungNote
            // 
            this.lblPhepDaDungNote.BackColor = System.Drawing.Color.Transparent;
            this.lblPhepDaDungNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhepDaDungNote.Font = new System.Drawing.Font("Segoe UI Semibold", 8F);
            this.lblPhepDaDungNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblPhepDaDungNote.Location = new System.Drawing.Point(16, 72);
            this.lblPhepDaDungNote.Name = "lblPhepDaDungNote";
            this.lblPhepDaDungNote.Size = new System.Drawing.Size(238, 40);
            this.lblPhepDaDungNote.TabIndex = 0;
            this.lblPhepDaDungNote.Text = "ngay";
            // 
            // lblPhepDaDungSo
            // 
            this.lblPhepDaDungSo.BackColor = System.Drawing.Color.Transparent;
            this.lblPhepDaDungSo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPhepDaDungSo.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            this.lblPhepDaDungSo.ForeColor = System.Drawing.Color.White;
            this.lblPhepDaDungSo.Location = new System.Drawing.Point(16, 34);
            this.lblPhepDaDungSo.Name = "lblPhepDaDungSo";
            this.lblPhepDaDungSo.Size = new System.Drawing.Size(238, 38);
            this.lblPhepDaDungSo.TabIndex = 1;
            this.lblPhepDaDungSo.Text = "--";
            // 
            // lblPhepDaDungTitle
            // 
            this.lblPhepDaDungTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPhepDaDungTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPhepDaDungTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPhepDaDungTitle.ForeColor = System.Drawing.Color.White;
            this.lblPhepDaDungTitle.Location = new System.Drawing.Point(16, 12);
            this.lblPhepDaDungTitle.Name = "lblPhepDaDungTitle";
            this.lblPhepDaDungTitle.Size = new System.Drawing.Size(238, 22);
            this.lblPhepDaDungTitle.TabIndex = 2;
            this.lblPhepDaDungTitle.Text = "DA SU DUNG";
            // 
            // pnlPhepTong
            // 
            this.pnlPhepTong.BorderRadius = 8;
            this.pnlPhepTong.Controls.Add(this.lblPhepTongNote);
            this.pnlPhepTong.Controls.Add(this.lblPhepTongSo);
            this.pnlPhepTong.Controls.Add(this.lblPhepTongTitle);
            this.pnlPhepTong.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlPhepTong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.pnlPhepTong.Location = new System.Drawing.Point(12, 8);
            this.pnlPhepTong.Name = "pnlPhepTong";
            this.pnlPhepTong.Padding = new System.Windows.Forms.Padding(16, 12, 16, 8);
            this.pnlPhepTong.Size = new System.Drawing.Size(270, 120);
            this.pnlPhepTong.TabIndex = 0;
            // 
            // lblPhepTongNote
            // 
            this.lblPhepTongNote.BackColor = System.Drawing.Color.Transparent;
            this.lblPhepTongNote.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblPhepTongNote.Font = new System.Drawing.Font("Segoe UI Semibold", 8F);
            this.lblPhepTongNote.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.lblPhepTongNote.Location = new System.Drawing.Point(16, 72);
            this.lblPhepTongNote.Name = "lblPhepTongNote";
            this.lblPhepTongNote.Size = new System.Drawing.Size(238, 40);
            this.lblPhepTongNote.TabIndex = 0;
            this.lblPhepTongNote.Text = "ngay";
            // 
            // lblPhepTongSo
            // 
            this.lblPhepTongSo.BackColor = System.Drawing.Color.Transparent;
            this.lblPhepTongSo.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPhepTongSo.Font = new System.Drawing.Font("Segoe UI Semibold", 22F, System.Drawing.FontStyle.Bold);
            this.lblPhepTongSo.ForeColor = System.Drawing.Color.White;
            this.lblPhepTongSo.Location = new System.Drawing.Point(16, 34);
            this.lblPhepTongSo.Name = "lblPhepTongSo";
            this.lblPhepTongSo.Size = new System.Drawing.Size(238, 38);
            this.lblPhepTongSo.TabIndex = 1;
            this.lblPhepTongSo.Text = "--";
            // 
            // lblPhepTongTitle
            // 
            this.lblPhepTongTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblPhepTongTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblPhepTongTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblPhepTongTitle.ForeColor = System.Drawing.Color.White;
            this.lblPhepTongTitle.Location = new System.Drawing.Point(16, 12);
            this.lblPhepTongTitle.Name = "lblPhepTongTitle";
            this.lblPhepTongTitle.Size = new System.Drawing.Size(238, 22);
            this.lblPhepTongTitle.TabIndex = 2;
            this.lblPhepTongTitle.Text = "TONG PHEP NAM";
            // 
            // pnlNNToolbar
            // 
            this.pnlNNToolbar.BackColor = System.Drawing.Color.Transparent;
            this.pnlNNToolbar.Controls.Add(this.cboNamTaiChinh);
            this.pnlNNToolbar.Controls.Add(this.lblNamTaiChinh);
            this.pnlNNToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNNToolbar.Location = new System.Drawing.Point(0, 0);
            this.pnlNNToolbar.Name = "pnlNNToolbar";
            this.pnlNNToolbar.Size = new System.Drawing.Size(932, 48);
            this.pnlNNToolbar.TabIndex = 0;
            // 
            // cboNamTaiChinh
            // 
            this.cboNamTaiChinh.BackColor = System.Drawing.Color.Transparent;
            this.cboNamTaiChinh.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNamTaiChinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNamTaiChinh.FocusedColor = System.Drawing.Color.Empty;
            this.cboNamTaiChinh.Font = new System.Drawing.Font("Segoe UI Semibold", 9.5F);
            this.cboNamTaiChinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboNamTaiChinh.ItemHeight = 30;
            this.cboNamTaiChinh.Location = new System.Drawing.Point(120, 10);
            this.cboNamTaiChinh.Name = "cboNamTaiChinh";
            this.cboNamTaiChinh.Size = new System.Drawing.Size(278, 36);
            this.cboNamTaiChinh.TabIndex = 0;
            this.cboNamTaiChinh.SelectedIndexChanged += new System.EventHandler(this.cboNamTaiChinh_SelectedIndexChanged);
            // 
            // lblNamTaiChinh
            // 
            this.lblNamTaiChinh.AutoSize = true;
            this.lblNamTaiChinh.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblNamTaiChinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(160)))), ((int)(((byte)(167)))));
            this.lblNamTaiChinh.Location = new System.Drawing.Point(15, 15);
            this.lblNamTaiChinh.Name = "lblNamTaiChinh";
            this.lblNamTaiChinh.Size = new System.Drawing.Size(85, 15);
            this.lblNamTaiChinh.TabIndex = 1;
            this.lblNamTaiChinh.Text = "Nam tai chinh:";
            // 
            // tabRuiRo
            // 
            this.tabRuiRo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(29)))), ((int)(((byte)(37)))), ((int)(((byte)(49)))));
            this.tabRuiRo.Controls.Add(this.tableLayoutRuiRo);
            this.tabRuiRo.Controls.Add(this.gridChungChi);
            this.tabRuiRo.Controls.Add(this.pnlCCHeader);
            this.tabRuiRo.Location = new System.Drawing.Point(4, 44);
            this.tabRuiRo.Name = "tabRuiRo";
            this.tabRuiRo.Size = new System.Drawing.Size(932, 717);
            this.tabRuiRo.TabIndex = 3;
            this.tabRuiRo.Text = "  RỦI RO";
            // 
            // tableLayoutRuiRo
            // 
            this.tableLayoutRuiRo.ColumnCount = 2;
            this.tableLayoutRuiRo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutRuiRo.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutRuiRo.Controls.Add(this.pnlTaiNan, 0, 0);
            this.tableLayoutRuiRo.Controls.Add(this.pnlKyLuat, 1, 0);
            this.tableLayoutRuiRo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutRuiRo.Location = new System.Drawing.Point(0, 46);
            this.tableLayoutRuiRo.Name = "tableLayoutRuiRo";
            this.tableLayoutRuiRo.RowCount = 1;
            this.tableLayoutRuiRo.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutRuiRo.Size = new System.Drawing.Size(932, 671);
            this.tableLayoutRuiRo.TabIndex = 2;
            // 
            // pnlTaiNan
            // 
            this.pnlTaiNan.BackColor = System.Drawing.Color.Transparent;
            this.pnlTaiNan.Controls.Add(this.gridTaiNanLaoDong);
            this.pnlTaiNan.Controls.Add(this.pnlTNHeader);
            this.pnlTaiNan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTaiNan.Location = new System.Drawing.Point(3, 3);
            this.pnlTaiNan.Name = "pnlTaiNan";
            this.pnlTaiNan.Padding = new System.Windows.Forms.Padding(0, 0, 4, 0);
            this.pnlTaiNan.Size = new System.Drawing.Size(460, 665);
            this.pnlTaiNan.TabIndex = 0;
            // 
            // gridTaiNanLaoDong
            // 
            this.gridTaiNanLaoDong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTaiNanLaoDong.Location = new System.Drawing.Point(0, 46);
            this.gridTaiNanLaoDong.MainView = this.gridViewTaiNanLaoDong;
            this.gridTaiNanLaoDong.Name = "gridTaiNanLaoDong";
            this.gridTaiNanLaoDong.Size = new System.Drawing.Size(456, 619);
            this.gridTaiNanLaoDong.TabIndex = 0;
            this.gridTaiNanLaoDong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTaiNanLaoDong});
            // 
            // gridViewTaiNanLaoDong
            // 
            this.gridViewTaiNanLaoDong.ColumnPanelRowHeight = 38;
            this.gridViewTaiNanLaoDong.GridControl = this.gridTaiNanLaoDong;
            this.gridViewTaiNanLaoDong.Name = "gridViewTaiNanLaoDong";
            this.gridViewTaiNanLaoDong.OptionsBehavior.Editable = false;
            this.gridViewTaiNanLaoDong.OptionsView.ShowGroupPanel = false;
            this.gridViewTaiNanLaoDong.OptionsView.ShowIndicator = false;
            this.gridViewTaiNanLaoDong.RowHeight = 35;
            // 
            // pnlTNHeader
            // 
            this.pnlTNHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlTNHeader.Controls.Add(this.btnXoaTN);
            this.pnlTNHeader.Controls.Add(this.btnThemTN);
            this.pnlTNHeader.Controls.Add(this.lblTaiNanTitle);
            this.pnlTNHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTNHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlTNHeader.Name = "pnlTNHeader";
            this.pnlTNHeader.Size = new System.Drawing.Size(456, 46);
            this.pnlTNHeader.TabIndex = 0;
            // 
            // btnXoaTN
            // 
            this.btnXoaTN.Animated = true;
            this.btnXoaTN.BorderRadius = 4;
            this.btnXoaTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaTN.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoaTN.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnXoaTN.ForeColor = System.Drawing.Color.White;
            this.btnXoaTN.Location = new System.Drawing.Point(370, 8);
            this.btnXoaTN.Name = "btnXoaTN";
            this.btnXoaTN.Size = new System.Drawing.Size(72, 30);
            this.btnXoaTN.TabIndex = 1;
            this.btnXoaTN.Text = "Xoa";
            this.btnXoaTN.Click += new System.EventHandler(this.BtnXoaTN_Click);
            // 
            // btnThemTN
            // 
            this.btnThemTN.Animated = true;
            this.btnThemTN.BorderRadius = 4;
            this.btnThemTN.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemTN.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(147)))), ((int)(((byte)(51)))), ((int)(((byte)(234)))));
            this.btnThemTN.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnThemTN.ForeColor = System.Drawing.Color.White;
            this.btnThemTN.Location = new System.Drawing.Point(260, 8);
            this.btnThemTN.Name = "btnThemTN";
            this.btnThemTN.Size = new System.Drawing.Size(100, 30);
            this.btnThemTN.TabIndex = 0;
            this.btnThemTN.Text = "+ Khai Bao";
            this.btnThemTN.Click += new System.EventHandler(this.BtnThemTN_Click);
            // 
            // lblTaiNanTitle
            // 
            this.lblTaiNanTitle.AutoSize = true;
            this.lblTaiNanTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTaiNanTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTaiNanTitle.Location = new System.Drawing.Point(8, 14);
            this.lblTaiNanTitle.Name = "lblTaiNanTitle";
            this.lblTaiNanTitle.Size = new System.Drawing.Size(118, 15);
            this.lblTaiNanTitle.TabIndex = 2;
            this.lblTaiNanTitle.Text = "TAI NAN LAO DONG";
            // 
            // pnlKyLuat
            // 
            this.pnlKyLuat.BackColor = System.Drawing.Color.Transparent;
            this.pnlKyLuat.Controls.Add(this.gridKyLuat);
            this.pnlKyLuat.Controls.Add(this.pnlKLHeader);
            this.pnlKyLuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKyLuat.Location = new System.Drawing.Point(469, 3);
            this.pnlKyLuat.Name = "pnlKyLuat";
            this.pnlKyLuat.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.pnlKyLuat.Size = new System.Drawing.Size(460, 665);
            this.pnlKyLuat.TabIndex = 1;
            // 
            // gridKyLuat
            // 
            this.gridKyLuat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridKyLuat.Location = new System.Drawing.Point(4, 46);
            this.gridKyLuat.MainView = this.gridViewKyLuat;
            this.gridKyLuat.Name = "gridKyLuat";
            this.gridKyLuat.Size = new System.Drawing.Size(456, 619);
            this.gridKyLuat.TabIndex = 0;
            this.gridKyLuat.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewKyLuat});
            // 
            // gridViewKyLuat
            // 
            this.gridViewKyLuat.ColumnPanelRowHeight = 38;
            this.gridViewKyLuat.GridControl = this.gridKyLuat;
            this.gridViewKyLuat.Name = "gridViewKyLuat";
            this.gridViewKyLuat.OptionsBehavior.Editable = false;
            this.gridViewKyLuat.OptionsView.ShowGroupPanel = false;
            this.gridViewKyLuat.OptionsView.ShowIndicator = false;
            this.gridViewKyLuat.RowHeight = 35;
            this.gridViewKyLuat.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewKyLuat_RowCellStyle);
            // 
            // pnlKLHeader
            // 
            this.pnlKLHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlKLHeader.Controls.Add(this.btnXoaKL);
            this.pnlKLHeader.Controls.Add(this.btnThemKL);
            this.pnlKLHeader.Controls.Add(this.lblKyLuatTitle);
            this.pnlKLHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKLHeader.Location = new System.Drawing.Point(4, 0);
            this.pnlKLHeader.Name = "pnlKLHeader";
            this.pnlKLHeader.Size = new System.Drawing.Size(456, 46);
            this.pnlKLHeader.TabIndex = 0;
            // 
            // btnXoaKL
            // 
            this.btnXoaKL.Animated = true;
            this.btnXoaKL.BorderRadius = 4;
            this.btnXoaKL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaKL.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoaKL.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnXoaKL.ForeColor = System.Drawing.Color.White;
            this.btnXoaKL.Location = new System.Drawing.Point(370, 8);
            this.btnXoaKL.Name = "btnXoaKL";
            this.btnXoaKL.Size = new System.Drawing.Size(72, 30);
            this.btnXoaKL.TabIndex = 1;
            this.btnXoaKL.Text = "Xoa";
            this.btnXoaKL.Click += new System.EventHandler(this.BtnXoaKL_Click);
            // 
            // btnThemKL
            // 
            this.btnThemKL.Animated = true;
            this.btnThemKL.BorderRadius = 4;
            this.btnThemKL.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemKL.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(234)))), ((int)(((byte)(88)))), ((int)(((byte)(12)))));
            this.btnThemKL.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnThemKL.ForeColor = System.Drawing.Color.White;
            this.btnThemKL.Location = new System.Drawing.Point(245, 8);
            this.btnThemKL.Name = "btnThemKL";
            this.btnThemKL.Size = new System.Drawing.Size(110, 30);
            this.btnThemKL.TabIndex = 0;
            this.btnThemKL.Text = "+ Lap Bien Ban";
            this.btnThemKL.Click += new System.EventHandler(this.BtnThemKL_Click);
            // 
            // lblKyLuatTitle
            // 
            this.lblKyLuatTitle.AutoSize = true;
            this.lblKyLuatTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblKyLuatTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblKyLuatTitle.Location = new System.Drawing.Point(8, 14);
            this.lblKyLuatTitle.Name = "lblKyLuatTitle";
            this.lblKyLuatTitle.Size = new System.Drawing.Size(52, 15);
            this.lblKyLuatTitle.TabIndex = 2;
            this.lblKyLuatTitle.Text = "KY LUAT";
            // 
            // gridChungChi
            // 
            this.gridChungChi.Location = new System.Drawing.Point(0, 46);
            this.gridChungChi.MainView = this.gridViewChungChi;
            this.gridChungChi.Name = "gridChungChi";
            this.gridChungChi.Size = new System.Drawing.Size(930, 200);
            this.gridChungChi.TabIndex = 1;
            this.gridChungChi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewChungChi});
            // 
            // gridViewChungChi
            // 
            this.gridViewChungChi.ColumnPanelRowHeight = 38;
            this.gridViewChungChi.GridControl = this.gridChungChi;
            this.gridViewChungChi.Name = "gridViewChungChi";
            this.gridViewChungChi.OptionsBehavior.Editable = false;
            this.gridViewChungChi.OptionsView.ShowGroupPanel = false;
            this.gridViewChungChi.OptionsView.ShowIndicator = false;
            this.gridViewChungChi.RowHeight = 38;
            this.gridViewChungChi.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gridViewChungChi_RowCellStyle);
            // 
            // pnlCCHeader
            // 
            this.pnlCCHeader.BackColor = System.Drawing.Color.Transparent;
            this.pnlCCHeader.Controls.Add(this.btnXoaCC);
            this.pnlCCHeader.Controls.Add(this.btnThemCC);
            this.pnlCCHeader.Controls.Add(this.lblChungChiTitle);
            this.pnlCCHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCCHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlCCHeader.Name = "pnlCCHeader";
            this.pnlCCHeader.Size = new System.Drawing.Size(932, 46);
            this.pnlCCHeader.TabIndex = 0;
            // 
            // btnXoaCC
            // 
            this.btnXoaCC.Animated = true;
            this.btnXoaCC.BorderRadius = 4;
            this.btnXoaCC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaCC.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoaCC.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnXoaCC.ForeColor = System.Drawing.Color.White;
            this.btnXoaCC.Location = new System.Drawing.Point(815, 8);
            this.btnXoaCC.Name = "btnXoaCC";
            this.btnXoaCC.Size = new System.Drawing.Size(72, 30);
            this.btnXoaCC.TabIndex = 1;
            this.btnXoaCC.Text = "Xoa";
            this.btnXoaCC.Click += new System.EventHandler(this.BtnXoaCC_Click);
            // 
            // btnThemCC
            // 
            this.btnThemCC.Animated = true;
            this.btnThemCC.BorderRadius = 4;
            this.btnThemCC.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemCC.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(5)))), ((int)(((byte)(150)))), ((int)(((byte)(105)))));
            this.btnThemCC.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.btnThemCC.ForeColor = System.Drawing.Color.White;
            this.btnThemCC.Location = new System.Drawing.Point(660, 8);
            this.btnThemCC.Name = "btnThemCC";
            this.btnThemCC.Size = new System.Drawing.Size(110, 30);
            this.btnThemCC.TabIndex = 0;
            this.btnThemCC.Text = "+ Them Chung Chi";
            this.btnThemCC.Click += new System.EventHandler(this.BtnThemCC_Click);
            // 
            // lblChungChiTitle
            // 
            this.lblChungChiTitle.AutoSize = true;
            this.lblChungChiTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblChungChiTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblChungChiTitle.Location = new System.Drawing.Point(12, 14);
            this.lblChungChiTitle.Name = "lblChungChiTitle";
            this.lblChungChiTitle.Size = new System.Drawing.Size(203, 15);
            this.lblChungChiTitle.TabIndex = 2;
            this.lblChungChiTitle.Text = "CHUNG CHI HANH NGHE & AN TOAN";
            // 
            // pnlNVHeader
            // 
            this.pnlNVHeader.Controls.Add(this.lblSDTEmailInfo);
            this.pnlNVHeader.Controls.Add(this.lblLuongInfo);
            this.pnlNVHeader.Controls.Add(this.lblHopDongInfo);
            this.pnlNVHeader.Controls.Add(this.lblChucVuInfo);
            this.pnlNVHeader.Controls.Add(this.lblTrangThaiNV);
            this.pnlNVHeader.Controls.Add(this.lblMaNV);
            this.pnlNVHeader.Controls.Add(this.lblTenNV);
            this.pnlNVHeader.Controls.Add(this.btnChonAnh);
            this.pnlNVHeader.Controls.Add(this.picAvatar);
            this.pnlNVHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlNVHeader.FillColor = System.Drawing.Color.White;
            this.pnlNVHeader.Location = new System.Drawing.Point(15, 15);
            this.pnlNVHeader.Name = "pnlNVHeader";
            this.pnlNVHeader.Size = new System.Drawing.Size(940, 174);
            this.pnlNVHeader.TabIndex = 2;
            this.pnlNVHeader.Visible = false;
            // 
            // lblSDTEmailInfo
            // 
            this.lblSDTEmailInfo.AutoSize = true;
            this.lblSDTEmailInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblSDTEmailInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSDTEmailInfo.Location = new System.Drawing.Point(120, 119);
            this.lblSDTEmailInfo.Name = "lblSDTEmailInfo";
            this.lblSDTEmailInfo.Size = new System.Drawing.Size(108, 15);
            this.lblSDTEmailInfo.TabIndex = 0;
            this.lblSDTEmailInfo.Text = "SĐT: -    |    Email: -";
            // 
            // lblLuongInfo
            // 
            this.lblLuongInfo.AutoSize = true;
            this.lblLuongInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblLuongInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblLuongInfo.Location = new System.Drawing.Point(120, 101);
            this.lblLuongInfo.Name = "lblLuongInfo";
            this.lblLuongInfo.Size = new System.Drawing.Size(148, 15);
            this.lblLuongInfo.TabIndex = 1;
            this.lblLuongInfo.Text = "Lương CB: -    |    Quản lý: -";
            // 
            // lblHopDongInfo
            // 
            this.lblHopDongInfo.AutoSize = true;
            this.lblHopDongInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblHopDongInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblHopDongInfo.Location = new System.Drawing.Point(120, 83);
            this.lblHopDongInfo.Name = "lblHopDongInfo";
            this.lblHopDongInfo.Size = new System.Drawing.Size(162, 15);
            this.lblHopDongInfo.TabIndex = 2;
            this.lblHopDongInfo.Text = "Hợp đồng: -    |    Nhóm CV: -";
            // 
            // lblChucVuInfo
            // 
            this.lblChucVuInfo.AutoSize = true;
            this.lblChucVuInfo.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblChucVuInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblChucVuInfo.Location = new System.Drawing.Point(120, 65);
            this.lblChucVuInfo.Name = "lblChucVuInfo";
            this.lblChucVuInfo.Size = new System.Drawing.Size(123, 15);
            this.lblChucVuInfo.TabIndex = 3;
            this.lblChucVuInfo.Text = "Chức vụ: -    |    Khối: -";
            // 
            // lblTrangThaiNV
            // 
            this.lblTrangThaiNV.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTrangThaiNV.AutoSize = true;
            this.lblTrangThaiNV.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.lblTrangThaiNV.Font = new System.Drawing.Font("Segoe UI Semibold", 8.5F, System.Drawing.FontStyle.Bold);
            this.lblTrangThaiNV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(101)))), ((int)(((byte)(52)))));
            this.lblTrangThaiNV.Location = new System.Drawing.Point(802, 20);
            this.lblTrangThaiNV.Name = "lblTrangThaiNV";
            this.lblTrangThaiNV.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.lblTrangThaiNV.Size = new System.Drawing.Size(99, 23);
            this.lblTrangThaiNV.TabIndex = 4;
            this.lblTrangThaiNV.Text = "Đang làm việc";
            // 
            // lblMaNV
            // 
            this.lblMaNV.AutoSize = true;
            this.lblMaNV.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.lblMaNV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMaNV.Location = new System.Drawing.Point(120, 46);
            this.lblMaNV.Name = "lblMaNV";
            this.lblMaNV.Size = new System.Drawing.Size(12, 15);
            this.lblMaNV.TabIndex = 5;
            this.lblMaNV.Text = "-";
            // 
            // lblTenNV
            // 
            this.lblTenNV.AutoSize = true;
            this.lblTenNV.Font = new System.Drawing.Font("Segoe UI Semibold", 15F, System.Drawing.FontStyle.Bold);
            this.lblTenNV.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTenNV.Location = new System.Drawing.Point(118, 15);
            this.lblTenNV.Name = "lblTenNV";
            this.lblTenNV.Size = new System.Drawing.Size(156, 28);
            this.lblTenNV.TabIndex = 6;
            this.lblTenNV.Text = "Chọn nhân viên";
            // 
            // btnChonAnh
            // 
            this.btnChonAnh.Animated = true;
            this.btnChonAnh.BorderRadius = 3;
            this.btnChonAnh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnChonAnh.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.btnChonAnh.Font = new System.Drawing.Font("Segoe UI Semibold", 7.5F);
            this.btnChonAnh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnChonAnh.Location = new System.Drawing.Point(15, 126);
            this.btnChonAnh.Name = "btnChonAnh";
            this.btnChonAnh.Size = new System.Drawing.Size(90, 30);
            this.btnChonAnh.TabIndex = 1;
            this.btnChonAnh.Text = "Chọn ảnh";
            this.btnChonAnh.Click += new System.EventHandler(this.btnChonAnh_Click);
            // 
            // picAvatar
            // 
            this.picAvatar.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picAvatar.Location = new System.Drawing.Point(15, 15);
            this.picAvatar.Name = "picAvatar";
            this.picAvatar.Size = new System.Drawing.Size(90, 108);
            this.picAvatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picAvatar.TabIndex = 0;
            this.picAvatar.TabStop = false;
            // 
            // lblNoSelection
            // 
            this.lblNoSelection.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblNoSelection.Font = new System.Drawing.Font("Segoe UI Semibold", 13F);
            this.lblNoSelection.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblNoSelection.Location = new System.Drawing.Point(15, 15);
            this.lblNoSelection.Name = "lblNoSelection";
            this.lblNoSelection.Size = new System.Drawing.Size(940, 939);
            this.lblNoSelection.TabIndex = 0;
            this.lblNoSelection.Text = "Chon nhan vien tu danh sach ben trai de xem ho so 360";
            this.lblNoSelection.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblGrpCaNhan
            // 
            this.lblGrpCaNhan.Location = new System.Drawing.Point(0, 0);
            this.lblGrpCaNhan.Name = "lblGrpCaNhan";
            this.lblGrpCaNhan.Size = new System.Drawing.Size(100, 23);
            this.lblGrpCaNhan.TabIndex = 0;
            // 
            // lblGrpCongTac
            // 
            this.lblGrpCongTac.Location = new System.Drawing.Point(0, 0);
            this.lblGrpCongTac.Name = "lblGrpCongTac";
            this.lblGrpCongTac.Size = new System.Drawing.Size(100, 23);
            this.lblGrpCongTac.TabIndex = 0;
            // 
            // lblGrpLuong
            // 
            this.lblGrpLuong.Location = new System.Drawing.Point(0, 0);
            this.lblGrpLuong.Name = "lblGrpLuong";
            this.lblGrpLuong.Size = new System.Drawing.Size(100, 23);
            this.lblGrpLuong.TabIndex = 0;
            // 
            // lblGrpTaiKhoan
            // 
            this.lblGrpTaiKhoan.Location = new System.Drawing.Point(0, 0);
            this.lblGrpTaiKhoan.Name = "lblGrpTaiKhoan";
            this.lblGrpTaiKhoan.Size = new System.Drawing.Size(100, 23);
            this.lblGrpTaiKhoan.TabIndex = 0;
            // 
            // lblGrpGhiChu
            // 
            this.lblGrpGhiChu.Location = new System.Drawing.Point(0, 0);
            this.lblGrpGhiChu.Name = "lblGrpGhiChu";
            this.lblGrpGhiChu.Size = new System.Drawing.Size(100, 23);
            this.lblGrpGhiChu.TabIndex = 0;
            // 
            // lblNgayPhep
            // 
            this.lblNgayPhep.Location = new System.Drawing.Point(0, 0);
            this.lblNgayPhep.Name = "lblNgayPhep";
            this.lblNgayPhep.Size = new System.Drawing.Size(100, 23);
            this.lblNgayPhep.TabIndex = 0;
            // 
            // lblHoTen
            // 
            this.lblHoTen.Location = new System.Drawing.Point(0, 0);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(100, 23);
            this.lblHoTen.TabIndex = 0;
            // 
            // lblMaCode
            // 
            this.lblMaCode.Location = new System.Drawing.Point(0, 0);
            this.lblMaCode.Name = "lblMaCode";
            this.lblMaCode.Size = new System.Drawing.Size(100, 23);
            this.lblMaCode.TabIndex = 0;
            // 
            // lblGioiTinh
            // 
            this.lblGioiTinh.Location = new System.Drawing.Point(0, 0);
            this.lblGioiTinh.Name = "lblGioiTinh";
            this.lblGioiTinh.Size = new System.Drawing.Size(100, 23);
            this.lblGioiTinh.TabIndex = 0;
            // 
            // lblNgaySinh
            // 
            this.lblNgaySinh.Location = new System.Drawing.Point(0, 0);
            this.lblNgaySinh.Name = "lblNgaySinh";
            this.lblNgaySinh.Size = new System.Drawing.Size(100, 23);
            this.lblNgaySinh.TabIndex = 0;
            // 
            // lblCCCD
            // 
            this.lblCCCD.Location = new System.Drawing.Point(0, 0);
            this.lblCCCD.Name = "lblCCCD";
            this.lblCCCD.Size = new System.Drawing.Size(100, 23);
            this.lblCCCD.TabIndex = 0;
            // 
            // lblSDT
            // 
            this.lblSDT.Location = new System.Drawing.Point(0, 0);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(100, 23);
            this.lblSDT.TabIndex = 0;
            // 
            // lblEmail
            // 
            this.lblEmail.Location = new System.Drawing.Point(0, 0);
            this.lblEmail.Name = "lblEmail";
            this.lblEmail.Size = new System.Drawing.Size(100, 23);
            this.lblEmail.TabIndex = 0;
            // 
            // lblDiaChi
            // 
            this.lblDiaChi.Location = new System.Drawing.Point(0, 0);
            this.lblDiaChi.Name = "lblDiaChi";
            this.lblDiaChi.Size = new System.Drawing.Size(100, 23);
            this.lblDiaChi.TabIndex = 0;
            // 
            // lblChucVulbl
            // 
            this.lblChucVulbl.Location = new System.Drawing.Point(0, 0);
            this.lblChucVulbl.Name = "lblChucVulbl";
            this.lblChucVulbl.Size = new System.Drawing.Size(100, 23);
            this.lblChucVulbl.TabIndex = 0;
            // 
            // lblNguoiQuanLy
            // 
            this.lblNguoiQuanLy.Location = new System.Drawing.Point(0, 0);
            this.lblNguoiQuanLy.Name = "lblNguoiQuanLy";
            this.lblNguoiQuanLy.Size = new System.Drawing.Size(100, 23);
            this.lblNguoiQuanLy.TabIndex = 0;
            // 
            // lblNgayVaoLam
            // 
            this.lblNgayVaoLam.Location = new System.Drawing.Point(0, 0);
            this.lblNgayVaoLam.Name = "lblNgayVaoLam";
            this.lblNgayVaoLam.Size = new System.Drawing.Size(100, 23);
            this.lblNgayVaoLam.TabIndex = 0;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.Location = new System.Drawing.Point(0, 0);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(100, 23);
            this.lblTrangThai.TabIndex = 0;
            // 
            // lblLoaiHopDong
            // 
            this.lblLoaiHopDong.Location = new System.Drawing.Point(0, 0);
            this.lblLoaiHopDong.Name = "lblLoaiHopDong";
            this.lblLoaiHopDong.Size = new System.Drawing.Size(100, 23);
            this.lblLoaiHopDong.TabIndex = 0;
            // 
            // lblNhomCongViec
            // 
            this.lblNhomCongViec.Location = new System.Drawing.Point(0, 0);
            this.lblNhomCongViec.Name = "lblNhomCongViec";
            this.lblNhomCongViec.Size = new System.Drawing.Size(100, 23);
            this.lblNhomCongViec.TabIndex = 0;
            // 
            // lblLuongCoBan
            // 
            this.lblLuongCoBan.Location = new System.Drawing.Point(0, 0);
            this.lblLuongCoBan.Name = "lblLuongCoBan";
            this.lblLuongCoBan.Size = new System.Drawing.Size(100, 23);
            this.lblLuongCoBan.TabIndex = 0;
            // 
            // lblLuongTheoGio
            // 
            this.lblLuongTheoGio.Location = new System.Drawing.Point(0, 0);
            this.lblLuongTheoGio.Name = "lblLuongTheoGio";
            this.lblLuongTheoGio.Size = new System.Drawing.Size(100, 23);
            this.lblLuongTheoGio.TabIndex = 0;
            // 
            // lblUsername
            // 
            this.lblUsername.Location = new System.Drawing.Point(0, 0);
            this.lblUsername.Name = "lblUsername";
            this.lblUsername.Size = new System.Drawing.Size(100, 23);
            this.lblUsername.TabIndex = 0;
            // 
            // lblPassword
            // 
            this.lblPassword.Location = new System.Drawing.Point(0, 0);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(100, 23);
            this.lblPassword.TabIndex = 0;
            // 
            // lblAccountRole
            // 
            this.lblAccountRole.Location = new System.Drawing.Point(0, 0);
            this.lblAccountRole.Name = "lblAccountRole";
            this.lblAccountRole.Size = new System.Drawing.Size(100, 23);
            this.lblAccountRole.TabIndex = 0;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtHoTen.DefaultText = "";
            this.txtHoTen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtHoTen.Location = new System.Drawing.Point(0, 0);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.PlaceholderText = "";
            this.txtHoTen.SelectedText = "";
            this.txtHoTen.Size = new System.Drawing.Size(200, 36);
            this.txtHoTen.TabIndex = 0;
            // 
            // txtMaCode
            // 
            this.txtMaCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaCode.DefaultText = "";
            this.txtMaCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaCode.Location = new System.Drawing.Point(0, 0);
            this.txtMaCode.Name = "txtMaCode";
            this.txtMaCode.PlaceholderText = "";
            this.txtMaCode.SelectedText = "";
            this.txtMaCode.Size = new System.Drawing.Size(200, 36);
            this.txtMaCode.TabIndex = 0;
            // 
            // cboGioiTinh
            // 
            this.cboGioiTinh.BackColor = System.Drawing.Color.Transparent;
            this.cboGioiTinh.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboGioiTinh.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboGioiTinh.FocusedColor = System.Drawing.Color.Empty;
            this.cboGioiTinh.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboGioiTinh.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboGioiTinh.ItemHeight = 30;
            this.cboGioiTinh.Location = new System.Drawing.Point(0, 0);
            this.cboGioiTinh.Name = "cboGioiTinh";
            this.cboGioiTinh.Size = new System.Drawing.Size(140, 36);
            this.cboGioiTinh.TabIndex = 0;
            // 
            // dtpNgaySinh
            // 
            this.dtpNgaySinh.EditValue = new System.DateTime(2026, 4, 14, 0, 0, 0, 0);
            this.dtpNgaySinh.Location = new System.Drawing.Point(0, 0);
            this.dtpNgaySinh.Name = "dtpNgaySinh";
            this.dtpNgaySinh.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgaySinh.Size = new System.Drawing.Size(100, 20);
            this.dtpNgaySinh.TabIndex = 0;
            // 
            // txtCCCD
            // 
            this.txtCCCD.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCCCD.DefaultText = "";
            this.txtCCCD.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCCCD.Location = new System.Drawing.Point(0, 0);
            this.txtCCCD.Name = "txtCCCD";
            this.txtCCCD.PlaceholderText = "";
            this.txtCCCD.SelectedText = "";
            this.txtCCCD.Size = new System.Drawing.Size(200, 36);
            this.txtCCCD.TabIndex = 0;
            // 
            // txtSDT
            // 
            this.txtSDT.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSDT.DefaultText = "";
            this.txtSDT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSDT.Location = new System.Drawing.Point(0, 0);
            this.txtSDT.Name = "txtSDT";
            this.txtSDT.PlaceholderText = "";
            this.txtSDT.SelectedText = "";
            this.txtSDT.Size = new System.Drawing.Size(200, 36);
            this.txtSDT.TabIndex = 0;
            // 
            // txtEmail
            // 
            this.txtEmail.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEmail.DefaultText = "";
            this.txtEmail.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtEmail.Location = new System.Drawing.Point(0, 0);
            this.txtEmail.Name = "txtEmail";
            this.txtEmail.PlaceholderText = "";
            this.txtEmail.SelectedText = "";
            this.txtEmail.Size = new System.Drawing.Size(200, 36);
            this.txtEmail.TabIndex = 0;
            // 
            // txtDiaChi
            // 
            this.txtDiaChi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiaChi.DefaultText = "";
            this.txtDiaChi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiaChi.Location = new System.Drawing.Point(0, 0);
            this.txtDiaChi.Name = "txtDiaChi";
            this.txtDiaChi.PlaceholderText = "";
            this.txtDiaChi.SelectedText = "";
            this.txtDiaChi.Size = new System.Drawing.Size(200, 36);
            this.txtDiaChi.TabIndex = 0;
            // 
            // slkChucVu
            // 
            this.slkChucVu.Location = new System.Drawing.Point(0, 0);
            this.slkChucVu.Name = "slkChucVu";
            this.slkChucVu.Size = new System.Drawing.Size(100, 20);
            this.slkChucVu.TabIndex = 0;
            // 
            // slkNguoiQuanLy
            // 
            this.slkNguoiQuanLy.Location = new System.Drawing.Point(0, 0);
            this.slkNguoiQuanLy.Name = "slkNguoiQuanLy";
            this.slkNguoiQuanLy.Size = new System.Drawing.Size(100, 20);
            this.slkNguoiQuanLy.TabIndex = 0;
            // 
            // dtpNgayVaoLam
            // 
            this.dtpNgayVaoLam.EditValue = new System.DateTime(2026, 4, 14, 0, 0, 0, 0);
            this.dtpNgayVaoLam.Location = new System.Drawing.Point(0, 0);
            this.dtpNgayVaoLam.Name = "dtpNgayVaoLam";
            this.dtpNgayVaoLam.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayVaoLam.Size = new System.Drawing.Size(100, 20);
            this.dtpNgayVaoLam.TabIndex = 0;
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.BackColor = System.Drawing.Color.Transparent;
            this.cboTrangThai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTrangThai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThai.FocusedColor = System.Drawing.Color.Empty;
            this.cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTrangThai.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboTrangThai.ItemHeight = 30;
            this.cboTrangThai.Location = new System.Drawing.Point(0, 0);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(140, 36);
            this.cboTrangThai.TabIndex = 0;
            // 
            // cboLoaiHopDong
            // 
            this.cboLoaiHopDong.BackColor = System.Drawing.Color.Transparent;
            this.cboLoaiHopDong.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiHopDong.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiHopDong.FocusedColor = System.Drawing.Color.Empty;
            this.cboLoaiHopDong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiHopDong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboLoaiHopDong.ItemHeight = 30;
            this.cboLoaiHopDong.Location = new System.Drawing.Point(0, 0);
            this.cboLoaiHopDong.Name = "cboLoaiHopDong";
            this.cboLoaiHopDong.Size = new System.Drawing.Size(140, 36);
            this.cboLoaiHopDong.TabIndex = 0;
            // 
            // cboNhomCongViec
            // 
            this.cboNhomCongViec.BackColor = System.Drawing.Color.Transparent;
            this.cboNhomCongViec.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNhomCongViec.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNhomCongViec.FocusedColor = System.Drawing.Color.Empty;
            this.cboNhomCongViec.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNhomCongViec.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboNhomCongViec.ItemHeight = 30;
            this.cboNhomCongViec.Location = new System.Drawing.Point(0, 0);
            this.cboNhomCongViec.Name = "cboNhomCongViec";
            this.cboNhomCongViec.Size = new System.Drawing.Size(140, 36);
            this.cboNhomCongViec.TabIndex = 0;
            // 
            // txtLuongCoBan
            // 
            this.txtLuongCoBan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLuongCoBan.DefaultText = "";
            this.txtLuongCoBan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLuongCoBan.Location = new System.Drawing.Point(0, 0);
            this.txtLuongCoBan.Name = "txtLuongCoBan";
            this.txtLuongCoBan.PlaceholderText = "";
            this.txtLuongCoBan.SelectedText = "";
            this.txtLuongCoBan.Size = new System.Drawing.Size(200, 36);
            this.txtLuongCoBan.TabIndex = 0;
            // 
            // txtLuongTheoGio
            // 
            this.txtLuongTheoGio.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLuongTheoGio.DefaultText = "";
            this.txtLuongTheoGio.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLuongTheoGio.Location = new System.Drawing.Point(0, 0);
            this.txtLuongTheoGio.Name = "txtLuongTheoGio";
            this.txtLuongTheoGio.PlaceholderText = "";
            this.txtLuongTheoGio.SelectedText = "";
            this.txtLuongTheoGio.Size = new System.Drawing.Size(200, 36);
            this.txtLuongTheoGio.TabIndex = 0;
            // 
            // txtUsername
            // 
            this.txtUsername.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUsername.DefaultText = "";
            this.txtUsername.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtUsername.Location = new System.Drawing.Point(0, 0);
            this.txtUsername.Name = "txtUsername";
            this.txtUsername.PlaceholderText = "";
            this.txtUsername.SelectedText = "";
            this.txtUsername.Size = new System.Drawing.Size(200, 36);
            this.txtUsername.TabIndex = 0;
            // 
            // txtPassword
            // 
            this.txtPassword.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPassword.DefaultText = "";
            this.txtPassword.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtPassword.Location = new System.Drawing.Point(0, 0);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PlaceholderText = "";
            this.txtPassword.SelectedText = "";
            this.txtPassword.Size = new System.Drawing.Size(200, 36);
            this.txtPassword.TabIndex = 0;
            // 
            // slkAccountRole
            // 
            this.slkAccountRole.Location = new System.Drawing.Point(0, 0);
            this.slkAccountRole.Name = "slkAccountRole";
            this.slkAccountRole.Size = new System.Drawing.Size(100, 20);
            this.slkAccountRole.TabIndex = 0;
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.Location = new System.Drawing.Point(0, 0);
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.Size = new System.Drawing.Size(100, 96);
            this.txtGhiChu.TabIndex = 0;
            // 
            // frmNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1400, 1024);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlToolbar);
            this.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F);
            this.MinimumSize = new System.Drawing.Size(1280, 768);
            this.Name = "frmNhanVien";
            this.Text = "QUẢN LÝ HỒ SƠ NHÂN SỰ TOÀN DIỆN 360";
            this.Load += new System.EventHandler(this.frmNhanVien_Load);
            this.pnlToolbar.ResumeLayout(false);
            this.pnlToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).EndInit();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnlLeftCard.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.pnlSearchArea.ResumeLayout(false);
            this.pnlDetail.ResumeLayout(false);
            this.tabControlDetails.ResumeLayout(false);
            this.tabHoSo.ResumeLayout(false);
            this.pnlHoSoScroll.ResumeLayout(false);
            this.pnlHoSoScroll.PerformLayout();
            this.tabChamCong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitChamCong.Panel1)).EndInit();
            this.splitChamCong.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitChamCong.Panel2)).EndInit();
            this.splitChamCong.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitChamCong)).EndInit();
            this.splitChamCong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLichLamViec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLichLamViec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridChamCong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChamCong)).EndInit();
            this.pnlCCToolbar.ResumeLayout(false);
            this.pnlCCToolbar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpKyDoiSoat.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpKyDoiSoat.Properties)).EndInit();
            this.tabNgayNghi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDonXinNghi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDonXinNghi)).EndInit();
            this.pnlDXNHeader.ResumeLayout(false);
            this.pnlDXNHeader.PerformLayout();
            this.pnlNghiBuBanner.ResumeLayout(false);
            this.pnlNghiBuBanner.PerformLayout();
            this.pnlKpiPhep.ResumeLayout(false);
            this.pnlPhepConLai.ResumeLayout(false);
            this.pnlPhepDaDung.ResumeLayout(false);
            this.pnlPhepTong.ResumeLayout(false);
            this.pnlNNToolbar.ResumeLayout(false);
            this.pnlNNToolbar.PerformLayout();
            this.tabRuiRo.ResumeLayout(false);
            this.tableLayoutRuiRo.ResumeLayout(false);
            this.pnlTaiNan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTaiNanLaoDong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTaiNanLaoDong)).EndInit();
            this.pnlTNHeader.ResumeLayout(false);
            this.pnlTNHeader.PerformLayout();
            this.pnlKyLuat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridKyLuat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewKyLuat)).EndInit();
            this.pnlKLHeader.ResumeLayout(false);
            this.pnlKLHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridChungChi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChungChi)).EndInit();
            this.pnlCCHeader.ResumeLayout(false);
            this.pnlCCHeader.PerformLayout();
            this.pnlNVHeader.ResumeLayout(false);
            this.pnlNVHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picAvatar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgaySinh.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgaySinh.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkChucVu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkNguoiQuanLy.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayVaoLam.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayVaoLam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkAccountRole.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGhiChu.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel       pnlToolbar;
        private Guna.UI2.WinForms.Guna2Button      btnThem;
        private Guna.UI2.WinForms.Guna2Button      btnSua;
        private Guna.UI2.WinForms.Guna2Button      btnXoa;
        private Guna.UI2.WinForms.Guna2Button      btnLamMoi;
        private Guna.UI2.WinForms.Guna2Button      btnInHoSo;
        private Guna.UI2.WinForms.Guna2Button      btnXuatExcel;
        private System.Windows.Forms.Label         lblUserInfo;
        private DevExpress.XtraEditors.SplitContainerControl splitMain;
        private Guna.UI2.WinForms.Guna2Panel       pnlLeftCard;
        private Guna.UI2.WinForms.Guna2Panel       pnlSearchArea;
        private Guna.UI2.WinForms.Guna2TextBox     txtTimKiem;
        private Guna.UI2.WinForms.Guna2ComboBox    cboLocKhoi;
        private DevExpress.XtraGrid.GridControl    gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private System.Windows.Forms.Panel         pnlDetail;
        private System.Windows.Forms.Label         lblNoSelection;
        private Guna.UI2.WinForms.Guna2Panel       pnlNVHeader;
        private System.Windows.Forms.PictureBox    picAvatar;
        private Guna.UI2.WinForms.Guna2Button      btnChonAnh;
        private System.Windows.Forms.Label         lblTenNV;
        private System.Windows.Forms.Label         lblMaNV;
        private System.Windows.Forms.Label         lblTrangThaiNV;
        private System.Windows.Forms.Label         lblChucVuInfo;
        private System.Windows.Forms.Label         lblHopDongInfo;
        private System.Windows.Forms.Label         lblLuongInfo;
        private System.Windows.Forms.Label         lblSDTEmailInfo;
        private Guna.UI2.WinForms.Guna2TabControl  tabControlDetails;
        private System.Windows.Forms.TabPage       tabHoSo;
        private System.Windows.Forms.TabPage       tabChamCong;
        private System.Windows.Forms.TabPage       tabNgayNghi;
        private System.Windows.Forms.TabPage       tabRuiRo;
        private System.Windows.Forms.Panel         pnlHoSoScroll;
        private System.Windows.Forms.TableLayoutPanel tblHoSo;
        private System.Windows.Forms.Label         lblGrpCaNhan;
        private System.Windows.Forms.Label         lblGrpCongTac;
        private System.Windows.Forms.Label         lblGrpLuong;
        private System.Windows.Forms.Label         lblGrpTaiKhoan;
        private System.Windows.Forms.Label         lblGrpGhiChu;
        private System.Windows.Forms.Label         lblNgayPhep;
        private System.Windows.Forms.Label         lblHoTen;
        private System.Windows.Forms.Label         lblMaCode;
        private System.Windows.Forms.Label         lblGioiTinh;
        private System.Windows.Forms.Label         lblNgaySinh;
        private System.Windows.Forms.Label         lblCCCD;
        private System.Windows.Forms.Label         lblSDT;
        private System.Windows.Forms.Label         lblEmail;
        private System.Windows.Forms.Label         lblDiaChi;
        private System.Windows.Forms.Label         lblChucVulbl;
        private System.Windows.Forms.Label         lblNguoiQuanLy;
        private System.Windows.Forms.Label         lblNgayVaoLam;
        private System.Windows.Forms.Label         lblTrangThai;
        private System.Windows.Forms.Label         lblLoaiHopDong;
        private System.Windows.Forms.Label         lblNhomCongViec;
        private System.Windows.Forms.Label         lblLuongCoBan;
        private System.Windows.Forms.Label         lblLuongTheoGio;
        private System.Windows.Forms.Label         lblUsername;
        private System.Windows.Forms.Label         lblPassword;
        private System.Windows.Forms.Label         lblAccountRole;
        private Guna.UI2.WinForms.Guna2TextBox     txtHoTen;
        private Guna.UI2.WinForms.Guna2TextBox     txtMaCode;
        private Guna.UI2.WinForms.Guna2ComboBox    cboGioiTinh;
        private DevExpress.XtraEditors.DateEdit    dtpNgaySinh;
        private Guna.UI2.WinForms.Guna2TextBox     txtCCCD;
        private Guna.UI2.WinForms.Guna2TextBox     txtSDT;
        private Guna.UI2.WinForms.Guna2TextBox     txtEmail;
        private Guna.UI2.WinForms.Guna2TextBox     txtDiaChi;
        private DevExpress.XtraEditors.SearchLookUpEdit slkChucVu;
        private DevExpress.XtraEditors.SearchLookUpEdit slkNguoiQuanLy;
        private DevExpress.XtraEditors.DateEdit    dtpNgayVaoLam;
        private Guna.UI2.WinForms.Guna2ComboBox    cboTrangThai;
        private Guna.UI2.WinForms.Guna2ComboBox    cboLoaiHopDong;
        private Guna.UI2.WinForms.Guna2ComboBox    cboNhomCongViec;
        private Guna.UI2.WinForms.Guna2TextBox     txtLuongCoBan;
        private Guna.UI2.WinForms.Guna2TextBox     txtLuongTheoGio;
        private Guna.UI2.WinForms.Guna2TextBox     txtUsername;
        private Guna.UI2.WinForms.Guna2TextBox     txtPassword;
        private DevExpress.XtraEditors.SearchLookUpEdit slkAccountRole;
        private DevExpress.XtraEditors.MemoEdit    txtGhiChu;
        private System.Windows.Forms.Panel         pnlCCToolbar;
        private System.Windows.Forms.Label         lblKyDoiSoat;
        private DevExpress.XtraEditors.DateEdit    dtpKyDoiSoat;
        private Guna.UI2.WinForms.Guna2Button      btnMoLichLamViec;
        private DevExpress.XtraEditors.SplitContainerControl splitChamCong;
        private System.Windows.Forms.Label         lblLichLamViecTitle;
        private DevExpress.XtraGrid.GridControl    gridLichLamViec;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLichLamViec;
        private System.Windows.Forms.Label         lblChamCongHistoryTitle;
        private DevExpress.XtraGrid.GridControl    gridChamCong;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewChamCong;
        private System.Windows.Forms.Panel         pnlNNToolbar;
        private System.Windows.Forms.Label         lblNamTaiChinh;
        private Guna.UI2.WinForms.Guna2ComboBox    cboNamTaiChinh;
        private Guna.UI2.WinForms.Guna2Panel       pnlKpiPhep;
        private Guna.UI2.WinForms.Guna2Panel       pnlPhepTong;
        private System.Windows.Forms.Label         lblPhepTongTitle;
        private System.Windows.Forms.Label         lblPhepTongSo;
        private System.Windows.Forms.Label         lblPhepTongNote;
        private Guna.UI2.WinForms.Guna2Panel       pnlPhepDaDung;
        private System.Windows.Forms.Label         lblPhepDaDungTitle;
        private System.Windows.Forms.Label         lblPhepDaDungSo;
        private System.Windows.Forms.Label         lblPhepDaDungNote;
        private Guna.UI2.WinForms.Guna2Panel       pnlPhepConLai;
        private System.Windows.Forms.Label         lblPhepConLaiTitle;
        private System.Windows.Forms.Label         lblPhepConLaiSo;
        private System.Windows.Forms.Label         lblPhepConLaiNote;
        private Guna.UI2.WinForms.Guna2Panel       pnlNghiBuBanner;
        private System.Windows.Forms.Label         lblNghiBuInfo;
        private System.Windows.Forms.Panel         pnlDXNHeader;
        private System.Windows.Forms.Label         lblDonXinNghiTitle;
        private Guna.UI2.WinForms.Guna2Button      btnThemDXN;
        private Guna.UI2.WinForms.Guna2Button      btnXoaDXN;
        private DevExpress.XtraGrid.GridControl    gridDonXinNghi;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDonXinNghi;
        private System.Windows.Forms.Panel         pnlCCHeader;
        private System.Windows.Forms.Label         lblChungChiTitle;
        private Guna.UI2.WinForms.Guna2Button      btnThemCC;
        private Guna.UI2.WinForms.Guna2Button      btnXoaCC;
        private DevExpress.XtraGrid.GridControl    gridChungChi;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewChungChi;
        private System.Windows.Forms.TableLayoutPanel tableLayoutRuiRo;
        private System.Windows.Forms.Panel         pnlTaiNan;
        private System.Windows.Forms.Panel         pnlTNHeader;
        private System.Windows.Forms.Label         lblTaiNanTitle;
        private Guna.UI2.WinForms.Guna2Button      btnThemTN;
        private Guna.UI2.WinForms.Guna2Button      btnXoaTN;
        private DevExpress.XtraGrid.GridControl    gridTaiNanLaoDong;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTaiNanLaoDong;
        private System.Windows.Forms.Panel         pnlKyLuat;
        private System.Windows.Forms.Panel         pnlKLHeader;
        private System.Windows.Forms.Label         lblKyLuatTitle;
        private Guna.UI2.WinForms.Guna2Button      btnThemKL;
        private Guna.UI2.WinForms.Guna2Button      btnXoaKL;
        private DevExpress.XtraGrid.GridControl    gridKyLuat;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewKyLuat;
    }
}
