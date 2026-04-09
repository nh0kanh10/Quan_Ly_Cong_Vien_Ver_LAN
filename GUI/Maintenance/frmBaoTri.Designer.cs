namespace GUI
{
    partial class frmBaoTri
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.gbDanhSach = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridThietBi = new DevExpress.XtraGrid.GridControl();
            this.gridViewThietBi = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlFilter = new Guna.UI2.WinForms.Guna2Panel();
            this.cboTrangThaiLoc = new Guna.UI2.WinForms.Guna2ComboBox();
            this.cboLoaiThietBi = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.gbLichBaoTri = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridBaoTri = new DevExpress.XtraGrid.GridControl();
            this.gridViewBaoTri = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlBaoTriActions = new Guna.UI2.WinForms.Guna2Panel();
            this.btnThemBT = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaBT = new Guna.UI2.WinForms.Guna2Button();
            this.btnHoanTatBT = new Guna.UI2.WinForms.Guna2Button();
            this.pnlSpacer = new Guna.UI2.WinForms.Guna2Panel();
            this.gbChucNangTB = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btnThemTB = new Guna.UI2.WinForms.Guna2Button();
            this.btnSuaTB = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaTB = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.gbThongTin = new System.Windows.Forms.Panel();
            this.lblTenThietBi = new System.Windows.Forms.Label();
            this.txtTenThietBi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMaCode = new System.Windows.Forms.Label();
            this.txtMaCode = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblLoaiThietBi = new System.Windows.Forms.Label();
            this.cboLoaiTB = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblKhuVuc = new System.Windows.Forms.Label();
            this.slkKhuVuc = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.slkKhuVucView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.cboTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblNgayMua = new System.Windows.Forms.Label();
            this.dtpNgayMua = new DevExpress.XtraEditors.DateEdit();
            this.lblGiaTri = new System.Windows.Forms.Label();
            this.txtGiaTri = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblChuKy = new System.Windows.Forms.Label();
            this.spnChuKy = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtMoTa = new Guna.UI2.WinForms.Guna2TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel1)).BeginInit();
            this.pnlMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel2)).BeginInit();
            this.pnlMain.Panel2.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.gbDanhSach.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridThietBi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewThietBi)).BeginInit();
            this.pnlFilter.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.gbLichBaoTri.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBaoTri)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBaoTri)).BeginInit();
            this.pnlBaoTriActions.SuspendLayout();
            this.gbChucNangTB.SuspendLayout();
            this.gbThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhuVuc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhuVucView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayMua.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayMua.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnChuKy)).BeginInit();
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
            this.pnlMain.SplitterPosition = 500;
            this.pnlMain.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gbDanhSach);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(790, 750);
            this.pnlLeft.TabIndex = 0;
            // 
            // gbDanhSach
            // 
            this.gbDanhSach.Controls.Add(this.gridThietBi);
            this.gbDanhSach.Controls.Add(this.pnlFilter);
            this.gbDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDanhSach.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbDanhSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbDanhSach.Location = new System.Drawing.Point(0, 0);
            this.gbDanhSach.Name = "gbDanhSach";
            this.gbDanhSach.Size = new System.Drawing.Size(790, 750);
            this.gbDanhSach.TabIndex = 0;
            this.gbDanhSach.Text = "  Danh sách thiết bị";
            // 
            // gridThietBi
            // 
            this.gridThietBi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridThietBi.Location = new System.Drawing.Point(0, 90);
            this.gridThietBi.MainView = this.gridViewThietBi;
            this.gridThietBi.Name = "gridThietBi";
            this.gridThietBi.Size = new System.Drawing.Size(790, 660);
            this.gridThietBi.TabIndex = 0;
            this.gridThietBi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewThietBi});
            // 
            // gridViewThietBi
            // 
            this.gridViewThietBi.GridControl = this.gridThietBi;
            this.gridViewThietBi.Name = "gridViewThietBi";
            this.gridViewThietBi.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewThietBi_FocusedRowChanged);
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.cboTrangThaiLoc);
            this.pnlFilter.Controls.Add(this.cboLoaiThietBi);
            this.pnlFilter.Controls.Add(this.txtTimKiem);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 40);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(5);
            this.pnlFilter.Size = new System.Drawing.Size(790, 50);
            this.pnlFilter.TabIndex = 1;
            // 
            // cboTrangThaiLoc
            // 
            this.cboTrangThaiLoc.BackColor = System.Drawing.Color.Transparent;
            this.cboTrangThaiLoc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTrangThaiLoc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThaiLoc.FocusedColor = System.Drawing.Color.Empty;
            this.cboTrangThaiLoc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboTrangThaiLoc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboTrangThaiLoc.ItemHeight = 30;
            this.cboTrangThaiLoc.Location = new System.Drawing.Point(510, 7);
            this.cboTrangThaiLoc.Name = "cboTrangThaiLoc";
            this.cboTrangThaiLoc.Size = new System.Drawing.Size(160, 36);
            this.cboTrangThaiLoc.TabIndex = 0;
            this.cboTrangThaiLoc.SelectedIndexChanged += new System.EventHandler(this.cboTrangThaiLoc_SelectedIndexChanged);
            // 
            // cboLoaiThietBi
            // 
            this.cboLoaiThietBi.BackColor = System.Drawing.Color.Transparent;
            this.cboLoaiThietBi.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiThietBi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiThietBi.FocusedColor = System.Drawing.Color.Empty;
            this.cboLoaiThietBi.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiThietBi.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboLoaiThietBi.ItemHeight = 30;
            this.cboLoaiThietBi.Location = new System.Drawing.Point(320, 7);
            this.cboLoaiThietBi.Name = "cboLoaiThietBi";
            this.cboLoaiThietBi.Size = new System.Drawing.Size(180, 36);
            this.cboLoaiThietBi.TabIndex = 1;
            this.cboLoaiThietBi.SelectedIndexChanged += new System.EventHandler(this.cboLoaiThietBi_SelectedIndexChanged);
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTimKiem.Location = new System.Drawing.Point(10, 7);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Tìm kiếm thiết bị...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(300, 36);
            this.txtTimKiem.TabIndex = 2;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.gbLichBaoTri);
            this.pnlRight.Controls.Add(this.pnlSpacer);
            this.pnlRight.Controls.Add(this.gbChucNangTB);
            this.pnlRight.Controls.Add(this.gbThongTin);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(0, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10);
            this.pnlRight.Size = new System.Drawing.Size(500, 750);
            this.pnlRight.TabIndex = 0;
            // 
            // gbLichBaoTri
            // 
            this.gbLichBaoTri.Controls.Add(this.gridBaoTri);
            this.gbLichBaoTri.Controls.Add(this.pnlBaoTriActions);
            this.gbLichBaoTri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLichBaoTri.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbLichBaoTri.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbLichBaoTri.Location = new System.Drawing.Point(10, 531);
            this.gbLichBaoTri.Name = "gbLichBaoTri";
            this.gbLichBaoTri.Size = new System.Drawing.Size(480, 209);
            this.gbLichBaoTri.TabIndex = 0;
            this.gbLichBaoTri.Text = "  Lịch bảo trì";
            // 
            // gridBaoTri
            // 
            this.gridBaoTri.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBaoTri.Location = new System.Drawing.Point(0, 40);
            this.gridBaoTri.MainView = this.gridViewBaoTri;
            this.gridBaoTri.Name = "gridBaoTri";
            this.gridBaoTri.Size = new System.Drawing.Size(480, 124);
            this.gridBaoTri.TabIndex = 0;
            this.gridBaoTri.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBaoTri});
            // 
            // gridViewBaoTri
            // 
            this.gridViewBaoTri.GridControl = this.gridBaoTri;
            this.gridViewBaoTri.Name = "gridViewBaoTri";
            // 
            // pnlBaoTriActions
            // 
            this.pnlBaoTriActions.Controls.Add(this.btnThemBT);
            this.pnlBaoTriActions.Controls.Add(this.btnXoaBT);
            this.pnlBaoTriActions.Controls.Add(this.btnHoanTatBT);
            this.pnlBaoTriActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBaoTriActions.Location = new System.Drawing.Point(0, 164);
            this.pnlBaoTriActions.Name = "pnlBaoTriActions";
            this.pnlBaoTriActions.Size = new System.Drawing.Size(480, 45);
            this.pnlBaoTriActions.TabIndex = 1;
            // 
            // btnThemBT
            // 
            this.btnThemBT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThemBT.ForeColor = System.Drawing.Color.White;
            this.btnThemBT.Location = new System.Drawing.Point(8, 5);
            this.btnThemBT.Name = "btnThemBT";
            this.btnThemBT.Size = new System.Drawing.Size(140, 34);
            this.btnThemBT.TabIndex = 0;
            this.btnThemBT.Text = "Thêm bảo trì";
            this.btnThemBT.Click += new System.EventHandler(this.btnThemBT_Click);
            // 
            // btnXoaBT
            // 
            this.btnXoaBT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoaBT.ForeColor = System.Drawing.Color.White;
            this.btnXoaBT.Location = new System.Drawing.Point(308, 5);
            this.btnXoaBT.Name = "btnXoaBT";
            this.btnXoaBT.Size = new System.Drawing.Size(140, 34);
            this.btnXoaBT.TabIndex = 1;
            this.btnXoaBT.Text = "Xóa";
            this.btnXoaBT.Click += new System.EventHandler(this.btnXoaBT_Click);
            // 
            // btnHoanTatBT
            // 
            this.btnHoanTatBT.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHoanTatBT.ForeColor = System.Drawing.Color.White;
            this.btnHoanTatBT.Location = new System.Drawing.Point(158, 5);
            this.btnHoanTatBT.Name = "btnHoanTatBT";
            this.btnHoanTatBT.Size = new System.Drawing.Size(140, 34);
            this.btnHoanTatBT.TabIndex = 2;
            this.btnHoanTatBT.Text = "Hoàn tất";
            this.btnHoanTatBT.Click += new System.EventHandler(this.btnHoanTatBT_Click);
            // 
            // pnlSpacer
            // 
            this.pnlSpacer.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlSpacer.Location = new System.Drawing.Point(10, 475);
            this.pnlSpacer.Name = "pnlSpacer";
            this.pnlSpacer.Size = new System.Drawing.Size(480, 56);
            this.pnlSpacer.TabIndex = 1;
            // 
            // gbChucNangTB
            // 
            this.gbChucNangTB.Controls.Add(this.btnThemTB);
            this.gbChucNangTB.Controls.Add(this.btnSuaTB);
            this.gbChucNangTB.Controls.Add(this.btnXoaTB);
            this.gbChucNangTB.Controls.Add(this.btnLamMoi);
            this.gbChucNangTB.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbChucNangTB.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbChucNangTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbChucNangTB.Location = new System.Drawing.Point(10, 337);
            this.gbChucNangTB.Name = "gbChucNangTB";
            this.gbChucNangTB.Size = new System.Drawing.Size(480, 138);
            this.gbChucNangTB.TabIndex = 2;
            this.gbChucNangTB.Text = "  Thiết bị";
            this.gbChucNangTB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnThemTB
            // 
            this.btnThemTB.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThemTB.ForeColor = System.Drawing.Color.White;
            this.btnThemTB.Location = new System.Drawing.Point(8, 49);
            this.btnThemTB.Name = "btnThemTB";
            this.btnThemTB.Size = new System.Drawing.Size(110, 34);
            this.btnThemTB.TabIndex = 0;
            this.btnThemTB.Text = "Thêm";
            this.btnThemTB.Click += new System.EventHandler(this.btnThemTB_Click);
            // 
            // btnSuaTB
            // 
            this.btnSuaTB.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSuaTB.ForeColor = System.Drawing.Color.White;
            this.btnSuaTB.Location = new System.Drawing.Point(126, 49);
            this.btnSuaTB.Name = "btnSuaTB";
            this.btnSuaTB.Size = new System.Drawing.Size(110, 34);
            this.btnSuaTB.TabIndex = 1;
            this.btnSuaTB.Text = "Cập nhật";
            this.btnSuaTB.Click += new System.EventHandler(this.btnSuaTB_Click);
            // 
            // btnXoaTB
            // 
            this.btnXoaTB.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoaTB.ForeColor = System.Drawing.Color.White;
            this.btnXoaTB.Location = new System.Drawing.Point(244, 49);
            this.btnXoaTB.Name = "btnXoaTB";
            this.btnXoaTB.Size = new System.Drawing.Size(110, 34);
            this.btnXoaTB.TabIndex = 2;
            this.btnXoaTB.Text = "Xóa";
            this.btnXoaTB.Click += new System.EventHandler(this.btnXoaTB_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(362, 49);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(110, 34);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // gbThongTin
            // 
            this.gbThongTin.AutoScroll = true;
            this.gbThongTin.Controls.Add(this.lblTenThietBi);
            this.gbThongTin.Controls.Add(this.txtTenThietBi);
            this.gbThongTin.Controls.Add(this.lblMaCode);
            this.gbThongTin.Controls.Add(this.txtMaCode);
            this.gbThongTin.Controls.Add(this.lblLoaiThietBi);
            this.gbThongTin.Controls.Add(this.cboLoaiTB);
            this.gbThongTin.Controls.Add(this.lblKhuVuc);
            this.gbThongTin.Controls.Add(this.slkKhuVuc);
            this.gbThongTin.Controls.Add(this.lblTrangThai);
            this.gbThongTin.Controls.Add(this.cboTrangThai);
            this.gbThongTin.Controls.Add(this.lblNgayMua);
            this.gbThongTin.Controls.Add(this.dtpNgayMua);
            this.gbThongTin.Controls.Add(this.lblGiaTri);
            this.gbThongTin.Controls.Add(this.txtGiaTri);
            this.gbThongTin.Controls.Add(this.lblChuKy);
            this.gbThongTin.Controls.Add(this.spnChuKy);
            this.gbThongTin.Controls.Add(this.lblMoTa);
            this.gbThongTin.Controls.Add(this.txtMoTa);
            this.gbThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbThongTin.Location = new System.Drawing.Point(10, 10);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(480, 327);
            this.gbThongTin.TabIndex = 3;
            // 
            // lblTenThietBi
            // 
            this.lblTenThietBi.AutoSize = true;
            this.lblTenThietBi.Location = new System.Drawing.Point(15, 10);
            this.lblTenThietBi.Name = "lblTenThietBi";
            this.lblTenThietBi.Size = new System.Drawing.Size(65, 13);
            this.lblTenThietBi.TabIndex = 0;
            this.lblTenThietBi.Text = "Tên thiết bị:";
            // 
            // txtTenThietBi
            // 
            this.txtTenThietBi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenThietBi.DefaultText = "";
            this.txtTenThietBi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenThietBi.Location = new System.Drawing.Point(15, 30);
            this.txtTenThietBi.Name = "txtTenThietBi";
            this.txtTenThietBi.PlaceholderText = "";
            this.txtTenThietBi.SelectedText = "";
            this.txtTenThietBi.Size = new System.Drawing.Size(450, 36);
            this.txtTenThietBi.TabIndex = 1;
            // 
            // lblMaCode
            // 
            this.lblMaCode.AutoSize = true;
            this.lblMaCode.Location = new System.Drawing.Point(15, 72);
            this.lblMaCode.Name = "lblMaCode";
            this.lblMaCode.Size = new System.Drawing.Size(51, 13);
            this.lblMaCode.TabIndex = 2;
            this.lblMaCode.Text = "Mã code:";
            // 
            // txtMaCode
            // 
            this.txtMaCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaCode.DefaultText = "";
            this.txtMaCode.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaCode.Location = new System.Drawing.Point(15, 92);
            this.txtMaCode.Name = "txtMaCode";
            this.txtMaCode.PlaceholderText = "";
            this.txtMaCode.SelectedText = "";
            this.txtMaCode.Size = new System.Drawing.Size(210, 36);
            this.txtMaCode.TabIndex = 3;
            // 
            // lblLoaiThietBi
            // 
            this.lblLoaiThietBi.AutoSize = true;
            this.lblLoaiThietBi.Location = new System.Drawing.Point(240, 72);
            this.lblLoaiThietBi.Name = "lblLoaiThietBi";
            this.lblLoaiThietBi.Size = new System.Drawing.Size(66, 13);
            this.lblLoaiThietBi.TabIndex = 4;
            this.lblLoaiThietBi.Text = "Loại thiết bị:";
            // 
            // cboLoaiTB
            // 
            this.cboLoaiTB.BackColor = System.Drawing.Color.Transparent;
            this.cboLoaiTB.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiTB.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiTB.FocusedColor = System.Drawing.Color.Empty;
            this.cboLoaiTB.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboLoaiTB.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboLoaiTB.ItemHeight = 30;
            this.cboLoaiTB.Location = new System.Drawing.Point(240, 92);
            this.cboLoaiTB.Name = "cboLoaiTB";
            this.cboLoaiTB.Size = new System.Drawing.Size(225, 36);
            this.cboLoaiTB.TabIndex = 5;
            // 
            // lblKhuVuc
            // 
            this.lblKhuVuc.AutoSize = true;
            this.lblKhuVuc.Location = new System.Drawing.Point(15, 134);
            this.lblKhuVuc.Name = "lblKhuVuc";
            this.lblKhuVuc.Size = new System.Drawing.Size(50, 13);
            this.lblKhuVuc.TabIndex = 6;
            this.lblKhuVuc.Text = "Khu vực:";
            // 
            // slkKhuVuc
            // 
            this.slkKhuVuc.Location = new System.Drawing.Point(15, 154);
            this.slkKhuVuc.Name = "slkKhuVuc";
            this.slkKhuVuc.Properties.PopupView = this.slkKhuVucView;
            this.slkKhuVuc.Size = new System.Drawing.Size(210, 20);
            this.slkKhuVuc.TabIndex = 7;
            // 
            // slkKhuVucView
            // 
            this.slkKhuVucView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.slkKhuVucView.Name = "slkKhuVucView";
            this.slkKhuVucView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.slkKhuVucView.OptionsView.ShowGroupPanel = false;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(240, 134);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(60, 13);
            this.lblTrangThai.TabIndex = 8;
            this.lblTrangThai.Text = "Trạng thái:";
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
            this.cboTrangThai.Location = new System.Drawing.Point(240, 154);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(225, 36);
            this.cboTrangThai.TabIndex = 9;
            // 
            // lblNgayMua
            // 
            this.lblNgayMua.AutoSize = true;
            this.lblNgayMua.Location = new System.Drawing.Point(15, 196);
            this.lblNgayMua.Name = "lblNgayMua";
            this.lblNgayMua.Size = new System.Drawing.Size(59, 13);
            this.lblNgayMua.TabIndex = 10;
            this.lblNgayMua.Text = "Ngày mua:";
            // 
            // dtpNgayMua
            // 
            this.dtpNgayMua.EditValue = new System.DateTime(2026, 4, 4, 0, 0, 0, 0);
            this.dtpNgayMua.Location = new System.Drawing.Point(15, 216);
            this.dtpNgayMua.Name = "dtpNgayMua";
            this.dtpNgayMua.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayMua.Size = new System.Drawing.Size(210, 20);
            this.dtpNgayMua.TabIndex = 11;
            // 
            // lblGiaTri
            // 
            this.lblGiaTri.AutoSize = true;
            this.lblGiaTri.Location = new System.Drawing.Point(240, 196);
            this.lblGiaTri.Name = "lblGiaTri";
            this.lblGiaTri.Size = new System.Drawing.Size(62, 13);
            this.lblGiaTri.TabIndex = 12;
            this.lblGiaTri.Text = "Giá trị mua:";
            // 
            // txtGiaTri
            // 
            this.txtGiaTri.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGiaTri.DefaultText = "";
            this.txtGiaTri.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtGiaTri.Location = new System.Drawing.Point(240, 216);
            this.txtGiaTri.Name = "txtGiaTri";
            this.txtGiaTri.PlaceholderText = "";
            this.txtGiaTri.SelectedText = "";
            this.txtGiaTri.Size = new System.Drawing.Size(225, 36);
            this.txtGiaTri.TabIndex = 13;
            // 
            // lblChuKy
            // 
            this.lblChuKy.AutoSize = true;
            this.lblChuKy.Location = new System.Drawing.Point(15, 258);
            this.lblChuKy.Name = "lblChuKy";
            this.lblChuKy.Size = new System.Drawing.Size(98, 13);
            this.lblChuKy.TabIndex = 14;
            this.lblChuKy.Text = "Chu kỳ BT (tháng):";
            // 
            // spnChuKy
            // 
            this.spnChuKy.BackColor = System.Drawing.Color.Transparent;
            this.spnChuKy.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.spnChuKy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.spnChuKy.Location = new System.Drawing.Point(15, 278);
            this.spnChuKy.Maximum = new decimal(new int[] {
            120,
            0,
            0,
            0});
            this.spnChuKy.Name = "spnChuKy";
            this.spnChuKy.Size = new System.Drawing.Size(210, 30);
            this.spnChuKy.TabIndex = 15;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(240, 258);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(38, 13);
            this.lblMoTa.TabIndex = 16;
            this.lblMoTa.Text = "Mô tả:";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMoTa.DefaultText = "";
            this.txtMoTa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMoTa.Location = new System.Drawing.Point(240, 278);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.PlaceholderText = "";
            this.txtMoTa.SelectedText = "";
            this.txtMoTa.Size = new System.Drawing.Size(225, 36);
            this.txtMoTa.TabIndex = 17;
            // 
            // frmBaoTri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 750);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmBaoTri";
            this.Text = "Quản Lý Bảo Trì Thiết Bị";
            this.Load += new System.EventHandler(this.frmBaoTri_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel1)).EndInit();
            this.pnlMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel2)).EndInit();
            this.pnlMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.gbDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridThietBi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewThietBi)).EndInit();
            this.pnlFilter.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.gbLichBaoTri.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridBaoTri)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBaoTri)).EndInit();
            this.pnlBaoTriActions.ResumeLayout(false);
            this.gbChucNangTB.ResumeLayout(false);
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhuVuc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhuVucView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayMua.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayMua.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnChuKy)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.SplitContainerControl pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlLeft;
        private Guna.UI2.WinForms.Guna2GroupBox gbDanhSach;
        private DevExpress.XtraGrid.GridControl gridThietBi;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewThietBi;
        private Guna.UI2.WinForms.Guna2Panel pnlFilter;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private Guna.UI2.WinForms.Guna2ComboBox cboLoaiThietBi;
        private Guna.UI2.WinForms.Guna2ComboBox cboTrangThaiLoc;
        private Guna.UI2.WinForms.Guna2Panel pnlRight;
        private System.Windows.Forms.Panel gbThongTin;
        private System.Windows.Forms.Label lblTenThietBi;
        private Guna.UI2.WinForms.Guna2TextBox txtTenThietBi;
        private System.Windows.Forms.Label lblLoaiThietBi;
        private Guna.UI2.WinForms.Guna2ComboBox cboLoaiTB;
        private System.Windows.Forms.Label lblKhuVuc;
        private DevExpress.XtraEditors.SearchLookUpEdit slkKhuVuc;
        private DevExpress.XtraGrid.Views.Grid.GridView slkKhuVucView;
        private System.Windows.Forms.Label lblMaCode;
        private Guna.UI2.WinForms.Guna2TextBox txtMaCode;
        private System.Windows.Forms.Label lblMoTa;
        private Guna.UI2.WinForms.Guna2TextBox txtMoTa;
        private System.Windows.Forms.Label lblTrangThai;
        private Guna.UI2.WinForms.Guna2ComboBox cboTrangThai;
        private System.Windows.Forms.Label lblNgayMua;
        private DevExpress.XtraEditors.DateEdit dtpNgayMua;
        private System.Windows.Forms.Label lblGiaTri;
        private Guna.UI2.WinForms.Guna2TextBox txtGiaTri;
        private System.Windows.Forms.Label lblChuKy;
        private Guna.UI2.WinForms.Guna2NumericUpDown spnChuKy;
        private Guna.UI2.WinForms.Guna2GroupBox gbChucNangTB;
        private Guna.UI2.WinForms.Guna2Button btnThemTB;
        private Guna.UI2.WinForms.Guna2Button btnSuaTB;
        private Guna.UI2.WinForms.Guna2Button btnXoaTB;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private Guna.UI2.WinForms.Guna2Panel pnlSpacer;
        private Guna.UI2.WinForms.Guna2GroupBox gbLichBaoTri;
        private DevExpress.XtraGrid.GridControl gridBaoTri;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewBaoTri;
        private Guna.UI2.WinForms.Guna2Panel pnlBaoTriActions;
        private Guna.UI2.WinForms.Guna2Button btnThemBT;
        private Guna.UI2.WinForms.Guna2Button btnXoaBT;
        private Guna.UI2.WinForms.Guna2Button btnHoanTatBT;
    }
}

