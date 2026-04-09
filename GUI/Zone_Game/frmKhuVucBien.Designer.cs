namespace GUI
{
    partial class frmKhuVucBien
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
            this.txtTenKhuVuc = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbl_txtTenKhuVuc = new System.Windows.Forms.Label();
            this.txtMaCode = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbl_txtMaCode = new System.Windows.Forms.Label();
            this.spnDoSau = new DevExpress.XtraEditors.SpinEdit();
            this.lbl_spnDoSau = new System.Windows.Forms.Label();
            this.chkYeuCauPhao = new Guna.UI2.WinForms.Guna2CheckBox();
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
            this.pnlChoiNghiMat = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridChoiNghiMat = new DevExpress.XtraGrid.GridControl();
            this.gridViewChoiNghiMat = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.gbThongTin = new System.Windows.Forms.Panel();
            this.pnlSpacer = new Guna.UI2.WinForms.Guna2Panel();
            this.gbChucNang = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.tabControlDetails = new Guna.UI2.WinForms.Guna2TabControl();
            this.tabKhuBien = new System.Windows.Forms.TabPage();
            this.tabChoiNghiMat = new System.Windows.Forms.TabPage();
            this.txtMaChoi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbl_txtMaChoi = new System.Windows.Forms.Label();
            this.txtTenChoi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbl_txtTenChoi = new System.Windows.Forms.Label();
            this.spnSucChuaChoi = new DevExpress.XtraEditors.SpinEdit();
            this.lbl_spnSucChuaChoi = new System.Windows.Forms.Label();
            this.cboTrangThaiChoi = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lbl_cboTrangThaiChoi = new System.Windows.Forms.Label();
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
            this.pnlChoiNghiMat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridChoiNghiMat)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChoiNghiMat)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.gbChucNang.SuspendLayout();
            this.gbThongTin.SuspendLayout();
            this.tabControlDetails.SuspendLayout();
            this.tabKhuBien.SuspendLayout();
            this.tabChoiNghiMat.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnDoSau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnSucChuaChoi.Properties)).BeginInit();
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
            this.pnlLeft.Controls.Add(this.pnlChoiNghiMat);
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
            this.gbDanhSach.Text = "Danh sách Khu Vực Biển";
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
            // pnlChoiNghiMat
            // 
            this.pnlChoiNghiMat.Controls.Add(this.gridChoiNghiMat);
            this.pnlChoiNghiMat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlChoiNghiMat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.pnlChoiNghiMat.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.pnlChoiNghiMat.Name = "pnlChoiNghiMat";
            this.pnlChoiNghiMat.Size = new System.Drawing.Size(844, 380);
            this.pnlChoiNghiMat.TabIndex = 5;
            this.pnlChoiNghiMat.Text = "Chòi Nghỉ Mát (theo Khu Biển)";
            // 
            // gridChoiNghiMat
            // 
            this.gridChoiNghiMat.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridChoiNghiMat.MainView = this.gridViewChoiNghiMat;
            this.gridChoiNghiMat.Name = "gridChoiNghiMat";
            this.gridChoiNghiMat.TabIndex = 0;
            this.gridChoiNghiMat.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewChoiNghiMat});
            // 
            // gridViewChoiNghiMat
            // 
            this.gridViewChoiNghiMat.GridControl = this.gridChoiNghiMat;
            this.gridViewChoiNghiMat.Name = "gridViewChoiNghiMat";
            this.gridViewChoiNghiMat.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewChoiNghiMat_FocusedRowChanged);

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
            // tabControlDetails
            // 
            this.tabControlDetails.Controls.Add(this.tabKhuBien);
            this.tabControlDetails.Controls.Add(this.tabChoiNghiMat);
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
            // tabKhuBien
            // 
            this.tabKhuBien.Controls.Add(this.gbThongTin);
            this.tabKhuBien.Location = new System.Drawing.Point(4, 44);
            this.tabKhuBien.Name = "tabKhuBien";
            this.tabKhuBien.Padding = new System.Windows.Forms.Padding(3);
            this.tabKhuBien.Size = new System.Drawing.Size(408, 485);
            this.tabKhuBien.TabIndex = 0;
            this.tabKhuBien.Text = "Khu Biển";
            this.tabKhuBien.UseVisualStyleBackColor = true;
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
            // tabChoiNghiMat
            // 
            this.tabChoiNghiMat.Controls.Add(this.lbl_txtMaChoi);
            this.tabChoiNghiMat.Controls.Add(this.txtMaChoi);
            this.tabChoiNghiMat.Controls.Add(this.lbl_txtTenChoi);
            this.tabChoiNghiMat.Controls.Add(this.txtTenChoi);
            this.tabChoiNghiMat.Controls.Add(this.lbl_spnSucChuaChoi);
            this.tabChoiNghiMat.Controls.Add(this.spnSucChuaChoi);
            this.tabChoiNghiMat.Controls.Add(this.lbl_cboTrangThaiChoi);
            this.tabChoiNghiMat.Controls.Add(this.cboTrangThaiChoi);
            this.tabChoiNghiMat.Location = new System.Drawing.Point(4, 44);
            this.tabChoiNghiMat.Name = "tabChoiNghiMat";
            this.tabChoiNghiMat.Padding = new System.Windows.Forms.Padding(3);
            this.tabChoiNghiMat.Size = new System.Drawing.Size(408, 485);
            this.tabChoiNghiMat.TabIndex = 1;
            this.tabChoiNghiMat.Text = "Chòi Nghỉ Mát";
            this.tabChoiNghiMat.UseVisualStyleBackColor = true;
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
            // Tab Chòi Nghỉ Mát controls
            // 
            this.lbl_txtMaChoi.AutoSize = true;
            this.lbl_txtMaChoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_txtMaChoi.Location = new System.Drawing.Point(20, 20);
            this.lbl_txtMaChoi.Name = "lbl_txtMaChoi";
            this.lbl_txtMaChoi.Text = "Mã Chòi:";
            this.txtMaChoi.BorderRadius = 18;
            this.txtMaChoi.Location = new System.Drawing.Point(20, 45);
            this.txtMaChoi.Name = "txtMaChoi";
            this.txtMaChoi.Size = new System.Drawing.Size(370, 36);
            this.txtMaChoi.TabIndex = 0;
            this.lbl_txtTenChoi.AutoSize = true;
            this.lbl_txtTenChoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_txtTenChoi.Location = new System.Drawing.Point(20, 90);
            this.lbl_txtTenChoi.Name = "lbl_txtTenChoi";
            this.lbl_txtTenChoi.Text = "Tên Chòi:";
            this.txtTenChoi.BorderRadius = 18;
            this.txtTenChoi.Location = new System.Drawing.Point(20, 115);
            this.txtTenChoi.Name = "txtTenChoi";
            this.txtTenChoi.Size = new System.Drawing.Size(370, 36);
            this.txtTenChoi.TabIndex = 1;
            this.lbl_spnSucChuaChoi.AutoSize = true;
            this.lbl_spnSucChuaChoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_spnSucChuaChoi.Location = new System.Drawing.Point(20, 160);
            this.lbl_spnSucChuaChoi.Name = "lbl_spnSucChuaChoi";
            this.lbl_spnSucChuaChoi.Text = "Sức Chứa:";
            this.spnSucChuaChoi.Location = new System.Drawing.Point(20, 185);
            this.spnSucChuaChoi.Name = "spnSucChuaChoi";
            this.spnSucChuaChoi.Size = new System.Drawing.Size(370, 30);
            this.spnSucChuaChoi.TabIndex = 2;
            this.lbl_cboTrangThaiChoi.AutoSize = true;
            this.lbl_cboTrangThaiChoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_cboTrangThaiChoi.Location = new System.Drawing.Point(20, 230);
            this.lbl_cboTrangThaiChoi.Name = "lbl_cboTrangThaiChoi";
            this.lbl_cboTrangThaiChoi.Text = "Trạng Thái:";
            this.cboTrangThaiChoi.BorderRadius = 18;
            this.cboTrangThaiChoi.Location = new System.Drawing.Point(20, 255);
            this.cboTrangThaiChoi.Name = "cboTrangThaiChoi";
            this.cboTrangThaiChoi.Size = new System.Drawing.Size(370, 36);
            this.cboTrangThaiChoi.TabIndex = 3;
            // 
            // Tab Khu Biển controls
            // 
            this.lbl_txtMaCode.AutoSize = true;
            this.lbl_txtMaCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_txtMaCode.Location = new System.Drawing.Point(20, 20);
            this.lbl_txtMaCode.Name = "lbl_txtMaCode";
            this.lbl_txtMaCode.Text = "Mã Khu Vực:";
            this.txtMaCode.Location = new System.Drawing.Point(20, 45);
            this.txtMaCode.Name = "txtMaCode";
            this.txtMaCode.Size = new System.Drawing.Size(370, 36);
            this.lbl_txtTenKhuVuc.AutoSize = true;
            this.lbl_txtTenKhuVuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_txtTenKhuVuc.Location = new System.Drawing.Point(20, 90);
            this.lbl_txtTenKhuVuc.Name = "lbl_txtTenKhuVuc";
            this.lbl_txtTenKhuVuc.Text = "Tên Vùng Biển:";
            this.txtTenKhuVuc.Location = new System.Drawing.Point(20, 115);
            this.txtTenKhuVuc.Name = "txtTenKhuVuc";
            this.txtTenKhuVuc.Size = new System.Drawing.Size(370, 36);
            this.lbl_spnDoSau.AutoSize = true;
            this.lbl_spnDoSau.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_spnDoSau.Location = new System.Drawing.Point(20, 160);
            this.lbl_spnDoSau.Name = "lbl_spnDoSau";
            this.lbl_spnDoSau.Text = "Độ Sâu Tối Đa (m):";
            this.spnDoSau.Location = new System.Drawing.Point(20, 185);
            this.spnDoSau.Name = "spnDoSau";
            this.spnDoSau.Size = new System.Drawing.Size(370, 30);
            this.spnDoSau.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.spnDoSau.Properties.Mask.EditMask = "n2";
            this.chkYeuCauPhao.Location = new System.Drawing.Point(20, 230);
            this.chkYeuCauPhao.Name = "chkYeuCauPhao";
            this.chkYeuCauPhao.Size = new System.Drawing.Size(370, 30);
            this.chkYeuCauPhao.Text = "Bắt buộc phao cứu sinh";
            this.lbl_txtMoTa.AutoSize = true;
            this.lbl_txtMoTa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_txtMoTa.Location = new System.Drawing.Point(20, 270);
            this.lbl_txtMoTa.Name = "lbl_txtMoTa";
            this.lbl_txtMoTa.Text = "Mô Tả:";
            this.txtMoTa.Location = new System.Drawing.Point(20, 295);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(370, 80);
            this.txtMoTa.Multiline = true;
            this.gbThongTin.Controls.Add(this.lbl_txtMaCode);
            this.gbThongTin.Controls.Add(this.txtMaCode);
            this.gbThongTin.Controls.Add(this.lbl_txtTenKhuVuc);
            this.gbThongTin.Controls.Add(this.txtTenKhuVuc);
            this.gbThongTin.Controls.Add(this.lbl_spnDoSau);
            this.gbThongTin.Controls.Add(this.spnDoSau);
            this.gbThongTin.Controls.Add(this.chkYeuCauPhao);
            this.gbThongTin.Controls.Add(this.lbl_txtMoTa);
            this.gbThongTin.Controls.Add(this.txtMoTa);
            // 
            // frmKhuVucBien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1300, 750);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmKhuVucBien";
            this.Text = "Quản Lý Khu Vực Biển";
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
            this.pnlChoiNghiMat.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridChoiNghiMat)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChoiNghiMat)).EndInit();

            this.pnlRight.ResumeLayout(false);
            this.gbChucNang.ResumeLayout(false);
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            this.tabControlDetails.ResumeLayout(false);
            this.tabKhuBien.ResumeLayout(false);
            this.tabChoiNghiMat.ResumeLayout(false);
            this.tabChoiNghiMat.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnDoSau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnSucChuaChoi.Properties)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

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
        protected Guna.UI2.WinForms.Guna2GroupBox pnlChoiNghiMat;
        protected DevExpress.XtraGrid.GridControl gridChoiNghiMat;
        protected DevExpress.XtraGrid.Views.Grid.GridView gridViewChoiNghiMat;

        protected Guna.UI2.WinForms.Guna2TabControl tabControlDetails;
        protected System.Windows.Forms.TabPage tabKhuBien;
        protected System.Windows.Forms.TabPage tabChoiNghiMat;
        public Guna.UI2.WinForms.Guna2TextBox txtTenKhuVuc;
        public System.Windows.Forms.Label lbl_txtTenKhuVuc;
        public Guna.UI2.WinForms.Guna2TextBox txtMaCode;
        public System.Windows.Forms.Label lbl_txtMaCode;
        public DevExpress.XtraEditors.SpinEdit spnDoSau;
        public System.Windows.Forms.Label lbl_spnDoSau;
        public Guna.UI2.WinForms.Guna2CheckBox chkYeuCauPhao;
        public Guna.UI2.WinForms.Guna2TextBox txtMoTa;
        public System.Windows.Forms.Label lbl_txtMoTa;
        public Guna.UI2.WinForms.Guna2TextBox txtMaChoi;
        public System.Windows.Forms.Label lbl_txtMaChoi;
        public Guna.UI2.WinForms.Guna2TextBox txtTenChoi;
        public System.Windows.Forms.Label lbl_txtTenChoi;
        public DevExpress.XtraEditors.SpinEdit spnSucChuaChoi;
        public System.Windows.Forms.Label lbl_spnSucChuaChoi;
        public Guna.UI2.WinForms.Guna2ComboBox cboTrangThaiChoi;
        public System.Windows.Forms.Label lbl_cboTrangThaiChoi;
    }
}
