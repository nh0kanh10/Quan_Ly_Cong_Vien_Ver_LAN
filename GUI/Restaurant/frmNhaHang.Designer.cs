namespace GUI
{
    partial class frmNhaHang
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
                        this.txtTenNhaHang = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbl_txtTenNhaHang = new System.Windows.Forms.Label();
            this.cboLoaiHinh = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lbl_cboLoaiHinh = new System.Windows.Forms.Label();
            this.slkKhuVuc = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.lbl_slkKhuVuc = new System.Windows.Forms.Label();
            this.slkKhuVucView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.spnSoBanTong = new DevExpress.XtraEditors.SpinEdit();
            this.lbl_spnSoBanTong = new System.Windows.Forms.Label();
            this.cboTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lbl_cboTrangThai = new System.Windows.Forms.Label();
            this.txtMoTa = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbl_txtMoTa = new System.Windows.Forms.Label();
            this.pnlMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.gbDanhSach = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlTimKiem = new Guna.UI2.WinForms.Guna2Panel();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnTimKiem = new Guna.UI2.WinForms.Guna2Button();
            this.pnlBanAn = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridBanAn = new DevExpress.XtraGrid.GridControl();
            this.gridViewBanAn = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlBanAnButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnThemBan = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaBan = new Guna.UI2.WinForms.Guna2Button();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.gbThongTin = new System.Windows.Forms.Panel();
            this.pnlSpacer = new Guna.UI2.WinForms.Guna2Panel();
            this.gbChucNang = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.tabControlDetails = new Guna.UI2.WinForms.Guna2TabControl();
            this.tabNhaHang = new System.Windows.Forms.TabPage();
            this.tabBanAn = new System.Windows.Forms.TabPage();
            this.txtMaBan = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbl_txtMaBan = new System.Windows.Forms.Label();
            this.spnSucChuaBan = new DevExpress.XtraEditors.SpinEdit();
            this.lbl_spnSucChuaBan = new System.Windows.Forms.Label();
            this.cboTrangThaiBan = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lbl_cboTrangThaiBan = new System.Windows.Forms.Label();
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
            this.pnlBanAn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridBanAn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBanAn)).BeginInit();
            this.pnlBanAnButtons.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.gbChucNang.SuspendLayout();
            this.gbThongTin.SuspendLayout();
            this.tabControlDetails.SuspendLayout();
            this.tabNhaHang.SuspendLayout();
            this.tabBanAn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnSucChuaBan.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Panel1.Controls.Add(this.pnlLeft);
            this.pnlMain.Panel2.Controls.Add(this.pnlRight);
            this.pnlMain.ShowSplitGlyph = DevExpress.Utils.DefaultBoolean.True;
            this.pnlMain.Size = new System.Drawing.Size(1300, 750);
            this.pnlMain.SplitterPosition = 446;
            this.pnlMain.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.pnlBanAn);
            this.pnlLeft.Controls.Add(this.gbDanhSach);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(844, 750);
            this.pnlLeft.TabIndex = 0;
            // 
            // gbDanhSach
            // 
            this.gbDanhSach.Controls.Add(this.gridControl);
            this.gbDanhSach.Controls.Add(this.pnlTimKiem);
            this.gbDanhSach.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbDanhSach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbDanhSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbDanhSach.Location = new System.Drawing.Point(0, 0);
            this.gbDanhSach.Name = "gbDanhSach";
            this.gbDanhSach.Size = new System.Drawing.Size(844, 370);
            this.gbDanhSach.TabIndex = 0;
            this.gbDanhSach.Text = "Danh sách Nhà Hàng";
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 90);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(844, 280);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            // 
            // pnlTimKiem
            // 
            this.pnlTimKiem.Controls.Add(this.txtTimKiem);
            this.pnlTimKiem.Controls.Add(this.btnTimKiem);
            this.pnlTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimKiem.Location = new System.Drawing.Point(0, 40);
            this.pnlTimKiem.Name = "pnlTimKiem";
            this.pnlTimKiem.Size = new System.Drawing.Size(844, 50);
            this.pnlTimKiem.TabIndex = 1;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTimKiem.BorderRadius = 18;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Location = new System.Drawing.Point(213, 7);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Nhập từ khóa cần tìm...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(391, 36);
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
            // pnlBanAn
            // 
            this.pnlBanAn.Controls.Add(this.gridBanAn);
            this.pnlBanAn.Controls.Add(this.pnlBanAnButtons);
            this.pnlBanAn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBanAn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.pnlBanAn.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.pnlBanAn.Name = "pnlBanAn";
            this.pnlBanAn.Size = new System.Drawing.Size(844, 380);
            this.pnlBanAn.TabIndex = 5;
            this.pnlBanAn.Text = "Bàn Ăn (theo Nhà Hàng)";
            // 
            // gridBanAn
            // 
            this.gridBanAn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridBanAn.MainView = this.gridViewBanAn;
            this.gridBanAn.Name = "gridBanAn";
            this.gridBanAn.TabIndex = 0;
            this.gridBanAn.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewBanAn});
            // 
            // gridViewBanAn
            // 
            this.gridViewBanAn.GridControl = this.gridBanAn;
            this.gridViewBanAn.Name = "gridViewBanAn";
            // 
            // pnlBanAnButtons
            // 
            this.pnlBanAnButtons.Controls.Add(this.btnThemBan);
            this.pnlBanAnButtons.Controls.Add(this.btnXoaBan);
            this.pnlBanAnButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBanAnButtons.Name = "pnlBanAnButtons";
            this.pnlBanAnButtons.Padding = new System.Windows.Forms.Padding(5);
            this.pnlBanAnButtons.Size = new System.Drawing.Size(844, 40);
            // 
            // btnThemBan
            // 
            this.btnThemBan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThemBan.ForeColor = System.Drawing.Color.White;
            this.btnThemBan.Name = "btnThemBan";
            this.btnThemBan.Size = new System.Drawing.Size(130, 30);
            this.btnThemBan.Text = "+ Thêm Bàn";
            this.btnThemBan.Click += new System.EventHandler(this.btnThemBan_Click);
            // 
            // btnXoaBan
            // 
            this.btnXoaBan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoaBan.ForeColor = System.Drawing.Color.White;
            this.btnXoaBan.Name = "btnXoaBan";
            this.btnXoaBan.Size = new System.Drawing.Size(130, 30);
            this.btnXoaBan.Text = "Xóa Bàn";
            this.btnXoaBan.Click += new System.EventHandler(this.btnXoaBan_Click);
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.tabControlDetails);
            this.pnlRight.Controls.Add(this.pnlSpacer);
            this.pnlRight.Controls.Add(this.gbChucNang);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(0, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(15);
            this.pnlRight.Size = new System.Drawing.Size(446, 750);
            this.pnlRight.TabIndex = 1;
            // 
            // gbThongTin
            // 
            this.gbThongTin.AutoScroll = true;
            this.gbThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThongTin.Location = new System.Drawing.Point(0, 0);
            this.gbThongTin.Margin = new System.Windows.Forms.Padding(0);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(416, 500);
            this.gbThongTin.TabIndex = 0;
            // 
            // pnlSpacer
            // 
            this.pnlSpacer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSpacer.Location = new System.Drawing.Point(15, 548);
            this.pnlSpacer.Name = "pnlSpacer";
            this.pnlSpacer.Size = new System.Drawing.Size(416, 15);
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
            this.gbChucNang.Location = new System.Drawing.Point(15, 563);
            this.gbChucNang.Margin = new System.Windows.Forms.Padding(0);
            this.gbChucNang.Name = "gbChucNang";
            this.gbChucNang.Size = new System.Drawing.Size(416, 172);
            this.gbChucNang.TabIndex = 2;
            this.gbChucNang.Text = "Chức năng";
            this.gbChucNang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(25, 67);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(170, 37);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm mới";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(215, 67);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(170, 37);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Cập nhật";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(25, 117);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(170, 37);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(215, 117);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(170, 37);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // tabControlDetails
            // 
            this.tabControlDetails.Controls.Add(this.tabNhaHang);
            this.tabControlDetails.Controls.Add(this.tabBanAn);
            this.tabControlDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlDetails.Alignment = System.Windows.Forms.TabAlignment.Top;
            this.tabControlDetails.ItemSize = new System.Drawing.Size(180, 40);
            this.tabControlDetails.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabControlDetails.Location = new System.Drawing.Point(15, 15);
            this.tabControlDetails.Name = "tabControlDetails";
            this.tabControlDetails.SelectedIndex = 0;
            this.tabControlDetails.Size = new System.Drawing.Size(416, 533);
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
            this.tabControlDetails.TabButtonSelectedState.Font = new System.Drawing.Font("Segoe UI Semibold", 10F);
            this.tabControlDetails.TabButtonSelectedState.ForeColor = System.Drawing.Color.White;
            this.tabControlDetails.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.tabControlDetails.TabButtonSize = new System.Drawing.Size(180, 40);
            this.tabControlDetails.TabIndex = 0;
            // 
            // tabNhaHang
            // 
            this.tabNhaHang.Controls.Add(this.gbThongTin);
            this.tabNhaHang.Location = new System.Drawing.Point(4, 44);
            this.tabNhaHang.Name = "tabNhaHang";
            this.tabNhaHang.Padding = new System.Windows.Forms.Padding(3);
            this.tabNhaHang.Size = new System.Drawing.Size(408, 485);
            this.tabNhaHang.TabIndex = 0;
            this.tabNhaHang.Text = "Nhà Hàng";
            this.tabNhaHang.UseVisualStyleBackColor = true;
            // 
            // tabBanAn
            // 
            this.tabBanAn.Controls.Add(this.lbl_txtMaBan);
            this.tabBanAn.Controls.Add(this.txtMaBan);
            this.tabBanAn.Controls.Add(this.lbl_spnSucChuaBan);
            this.tabBanAn.Controls.Add(this.spnSucChuaBan);
            this.tabBanAn.Controls.Add(this.lbl_cboTrangThaiBan);
            this.tabBanAn.Controls.Add(this.cboTrangThaiBan);
            this.tabBanAn.Location = new System.Drawing.Point(4, 44);
            this.tabBanAn.Name = "tabBanAn";
            this.tabBanAn.Padding = new System.Windows.Forms.Padding(3);
            this.tabBanAn.Size = new System.Drawing.Size(408, 485);
            this.tabBanAn.TabIndex = 1;
            this.tabBanAn.Text = "Bàn Ăn";
            this.tabBanAn.UseVisualStyleBackColor = true;
            // 
            // txtMaBan
            // 
            this.txtMaBan.BorderRadius = 18;
            this.txtMaBan.Location = new System.Drawing.Point(20, 45);
            this.txtMaBan.Name = "txtMaBan";
            this.txtMaBan.Size = new System.Drawing.Size(370, 36);
            this.txtMaBan.TabIndex = 0;
            // 
            // lbl_txtMaBan
            // 
            this.lbl_txtMaBan.AutoSize = true;
            this.lbl_txtMaBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_txtMaBan.Location = new System.Drawing.Point(20, 20);
            this.lbl_txtMaBan.Name = "lbl_txtMaBan";
            this.lbl_txtMaBan.Text = "Mã Bàn:";
            // 
            // spnSucChuaBan
            // 
            this.spnSucChuaBan.Location = new System.Drawing.Point(20, 115);
            this.spnSucChuaBan.Name = "spnSucChuaBan";
            this.spnSucChuaBan.Size = new System.Drawing.Size(370, 30);
            this.spnSucChuaBan.TabIndex = 1;
            // 
            // lbl_spnSucChuaBan
            // 
            this.lbl_spnSucChuaBan.AutoSize = true;
            this.lbl_spnSucChuaBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_spnSucChuaBan.Location = new System.Drawing.Point(20, 90);
            this.lbl_spnSucChuaBan.Name = "lbl_spnSucChuaBan";
            this.lbl_spnSucChuaBan.Text = "Sức Chứa:";
            // 
            // cboTrangThaiBan
            // 
            this.cboTrangThaiBan.BorderRadius = 18;
            this.cboTrangThaiBan.Location = new System.Drawing.Point(20, 185);
            this.cboTrangThaiBan.Name = "cboTrangThaiBan";
            this.cboTrangThaiBan.Size = new System.Drawing.Size(370, 36);
            this.cboTrangThaiBan.TabIndex = 2;
            // 
            // lbl_cboTrangThaiBan
            // 
            this.lbl_cboTrangThaiBan.AutoSize = true;
            this.lbl_cboTrangThaiBan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_cboTrangThaiBan.Location = new System.Drawing.Point(20, 160);
            this.lbl_cboTrangThaiBan.Name = "lbl_cboTrangThaiBan";
            this.lbl_cboTrangThaiBan.Text = "Trạng Thái Bàn:";
            // 
            // frmNhaHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1300, 750);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmNhaHang";
            this.Text = "Quản Lý Nhà Hàng";
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
            this.pnlBanAn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridBanAn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewBanAn)).EndInit();
            this.pnlBanAnButtons.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.gbChucNang.ResumeLayout(false);
            this.gbThongTin.ResumeLayout(false);
            this.tabControlDetails.ResumeLayout(false);
            this.tabNhaHang.ResumeLayout(false);
            this.tabBanAn.ResumeLayout(false);
            this.tabBanAn.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnSucChuaBan.Properties)).EndInit();
            this.lbl_txtTenNhaHang.AutoSize = true;
            this.lbl_txtTenNhaHang.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_txtTenNhaHang.Location = new System.Drawing.Point(20, 20);
            this.lbl_txtTenNhaHang.Name = "lbl_txtTenNhaHang";
            this.lbl_txtTenNhaHang.Text = "Tên Nhà Hàng:";
            this.txtTenNhaHang.Location = new System.Drawing.Point(20, 45);
            this.txtTenNhaHang.Name = "txtTenNhaHang";
            this.txtTenNhaHang.Size = new System.Drawing.Size(370, 36);
            this.lbl_cboLoaiHinh.AutoSize = true;
            this.lbl_cboLoaiHinh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_cboLoaiHinh.Location = new System.Drawing.Point(20, 90);
            this.lbl_cboLoaiHinh.Name = "lbl_cboLoaiHinh";
            this.lbl_cboLoaiHinh.Text = "Loại Hình:";
            this.cboLoaiHinh.Location = new System.Drawing.Point(20, 115);
            this.cboLoaiHinh.Name = "cboLoaiHinh";
            this.cboLoaiHinh.Size = new System.Drawing.Size(370, 36);
            this.lbl_slkKhuVuc.AutoSize = true;
            this.lbl_slkKhuVuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_slkKhuVuc.Location = new System.Drawing.Point(20, 160);
            this.lbl_slkKhuVuc.Name = "lbl_slkKhuVuc";
            this.lbl_slkKhuVuc.Text = "Khu Vực:";
            this.slkKhuVuc.Location = new System.Drawing.Point(20, 185);
            this.slkKhuVuc.Name = "slkKhuVuc";
            this.slkKhuVuc.Size = new System.Drawing.Size(370, 30);
            this.slkKhuVuc.Properties.PopupView = this.slkKhuVucView;
            this.lbl_spnSoBanTong.AutoSize = true;
            this.lbl_spnSoBanTong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_spnSoBanTong.Location = new System.Drawing.Point(20, 275);
            this.lbl_spnSoBanTong.Name = "lbl_spnSoBanTong";
            this.lbl_spnSoBanTong.Text = "Tổng Số Bàn:";
            this.spnSoBanTong.Location = new System.Drawing.Point(20, 300);
            this.spnSoBanTong.Name = "spnSoBanTong";
            this.spnSoBanTong.Size = new System.Drawing.Size(370, 30);
            this.spnSoBanTong.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.lbl_cboTrangThai.AutoSize = true;
            this.lbl_cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_cboTrangThai.Location = new System.Drawing.Point(20, 345);
            this.lbl_cboTrangThai.Name = "lbl_cboTrangThai";
            this.lbl_cboTrangThai.Text = "Trạng Thái:";
            this.cboTrangThai.Location = new System.Drawing.Point(20, 370);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(370, 36);
            this.lbl_txtMoTa.AutoSize = true;
            this.lbl_txtMoTa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_txtMoTa.Location = new System.Drawing.Point(20, 415);
            this.lbl_txtMoTa.Name = "lbl_txtMoTa";
            this.lbl_txtMoTa.Text = "Mô Tả:";
            this.txtMoTa.Location = new System.Drawing.Point(20, 440);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(370, 80);
            this.txtMoTa.Multiline = true;
            this.gbThongTin.Controls.Add(this.lbl_txtTenNhaHang);
            this.gbThongTin.Controls.Add(this.txtTenNhaHang);
            this.gbThongTin.Controls.Add(this.lbl_cboLoaiHinh);
            this.gbThongTin.Controls.Add(this.cboLoaiHinh);
            this.gbThongTin.Controls.Add(this.lbl_slkKhuVuc);
            this.gbThongTin.Controls.Add(this.slkKhuVuc);
            this.gbThongTin.Controls.Add(this.lbl_spnSoBanTong);
            this.gbThongTin.Controls.Add(this.spnSoBanTong);
            this.gbThongTin.Controls.Add(this.lbl_cboTrangThai);
            this.gbThongTin.Controls.Add(this.cboTrangThai);
            this.gbThongTin.Controls.Add(this.lbl_txtMoTa);
            this.gbThongTin.Controls.Add(this.txtMoTa);
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        public Guna.UI2.WinForms.Guna2TextBox txtTenNhaHang;
        public System.Windows.Forms.Label lbl_txtTenNhaHang;
        public Guna.UI2.WinForms.Guna2ComboBox cboLoaiHinh;
        public System.Windows.Forms.Label lbl_cboLoaiHinh;
        public DevExpress.XtraEditors.SearchLookUpEdit slkKhuVuc;
        public System.Windows.Forms.Label lbl_slkKhuVuc;
        public DevExpress.XtraGrid.Views.Grid.GridView slkKhuVucView;
        public DevExpress.XtraEditors.SpinEdit spnSoBanTong;
        public System.Windows.Forms.Label lbl_spnSoBanTong;
        public Guna.UI2.WinForms.Guna2ComboBox cboTrangThai;
        public System.Windows.Forms.Label lbl_cboTrangThai;
        public Guna.UI2.WinForms.Guna2TextBox txtMoTa;
        public System.Windows.Forms.Label lbl_txtMoTa;

        protected internal DevExpress.XtraEditors.SplitContainerControl pnlMain;
        protected Guna.UI2.WinForms.Guna2Panel pnlLeft;
        protected Guna.UI2.WinForms.Guna2GroupBox gbDanhSach;
        protected DevExpress.XtraGrid.GridControl gridControl;
        protected DevExpress.XtraGrid.Views.Grid.GridView gridView;
        protected Guna.UI2.WinForms.Guna2Panel pnlTimKiem;
        protected Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        protected Guna.UI2.WinForms.Guna2Button btnTimKiem;
        protected Guna.UI2.WinForms.Guna2Panel pnlRight;
        protected System.Windows.Forms.Panel gbThongTin;
        protected Guna.UI2.WinForms.Guna2Panel pnlSpacer;
        protected Guna.UI2.WinForms.Guna2GroupBox gbChucNang;
        protected Guna.UI2.WinForms.Guna2Button btnThem;
        protected Guna.UI2.WinForms.Guna2Button btnSua;
        protected Guna.UI2.WinForms.Guna2Button btnXoa;
        protected Guna.UI2.WinForms.Guna2Button btnLamMoi;
        protected Guna.UI2.WinForms.Guna2GroupBox pnlBanAn;
        protected DevExpress.XtraGrid.GridControl gridBanAn;
        protected DevExpress.XtraGrid.Views.Grid.GridView gridViewBanAn;
        protected System.Windows.Forms.FlowLayoutPanel pnlBanAnButtons;
        protected Guna.UI2.WinForms.Guna2Button btnThemBan;
        protected Guna.UI2.WinForms.Guna2Button btnXoaBan;
        protected Guna.UI2.WinForms.Guna2TabControl tabControlDetails;
        protected System.Windows.Forms.TabPage tabNhaHang;
        protected System.Windows.Forms.TabPage tabBanAn;
        public Guna.UI2.WinForms.Guna2TextBox txtMaBan;
        public System.Windows.Forms.Label lbl_txtMaBan;
        public DevExpress.XtraEditors.SpinEdit spnSucChuaBan;
        public System.Windows.Forms.Label lbl_spnSucChuaBan;
        public Guna.UI2.WinForms.Guna2ComboBox cboTrangThaiBan;
        public System.Windows.Forms.Label lbl_cboTrangThaiBan;
    }
}
