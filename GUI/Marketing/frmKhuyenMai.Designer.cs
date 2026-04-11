namespace GUI
{
    partial class frmKhuyenMai
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
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.gbThongTin = new System.Windows.Forms.Panel();
            this.gbModuleThongTin = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblMaCode = new System.Windows.Forms.Label();
            this.txtMaCode = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenKM = new System.Windows.Forms.Label();
            this.txtTenKM = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblLoaiGiam = new System.Windows.Forms.Label();
            this.cboLoaiGiam = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblGiaTri = new System.Windows.Forms.Label();
            this.txtGiaTri = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDonToiThieu = new System.Windows.Forms.Label();
            this.txtDonToiThieu = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNgayBD = new System.Windows.Forms.Label();
            this.dtpNgayBD = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayBD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayBD.Properties.CalendarTimeProperties)).BeginInit();
            this.lblNgayKT = new System.Windows.Forms.Label();
            this.dtpNgayKT = new DevExpress.XtraEditors.DateEdit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayKT.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayKT.Properties.CalendarTimeProperties)).BeginInit();
            this.lblTrangThai = new System.Windows.Forms.Label();
            this.chkTrangThai = new Guna.UI2.WinForms.Guna2ToggleSwitch();
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
            this.pnlMain.SplitterPosition = 446;
            this.pnlMain.TabIndex = 0;
            // 
            // pnlLeft
            // 
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
            this.gbDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDanhSach.Location = new System.Drawing.Point(0, 0);
            this.gbDanhSach.Name = "gbDanhSach";
            this.gbDanhSach.Size = new System.Drawing.Size(844, 750);
            this.gbDanhSach.TabIndex = 0;
            this.gbDanhSach.Text = "Danh sách khuyến mãi";
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 90);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(844, 660);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.OnFocusedRowChanged);
            // 
            // pnlTimKiem
            // 
            this.pnlTimKiem.Controls.Add(this.txtTimKiem);
            this.pnlTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimKiem.Location = new System.Drawing.Point(0, 40);
            this.pnlTimKiem.Name = "pnlTimKiem";
            this.pnlTimKiem.Size = new System.Drawing.Size(844, 50);
            this.pnlTimKiem.TabIndex = 1;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Location = new System.Drawing.Point(197, 7);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Tìm theo tên hoặc mã khuyến mãi...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(431, 36);
            this.txtTimKiem.TabIndex = 0;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
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
            this.pnlRight.Size = new System.Drawing.Size(446, 750);
            this.pnlRight.TabIndex = 1;
            // 
            // gbThongTin
            // 
            this.gbThongTin.AutoScroll = true;
            this.gbThongTin.Controls.Add(this.gbModuleThongTin);
            this.gbThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThongTin.Location = new System.Drawing.Point(15, 15);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(416, 507);
            this.gbThongTin.TabIndex = 0;
            // 
            // gbModuleThongTin
            // 
            this.gbModuleThongTin.Controls.Add(this.lblMaCode);
            this.gbModuleThongTin.Controls.Add(this.txtMaCode);
            this.gbModuleThongTin.Controls.Add(this.lblTenKM);
            this.gbModuleThongTin.Controls.Add(this.txtTenKM);
            this.gbModuleThongTin.Controls.Add(this.lblLoaiGiam);
            this.gbModuleThongTin.Controls.Add(this.cboLoaiGiam);
            this.gbModuleThongTin.Controls.Add(this.lblGiaTri);
            this.gbModuleThongTin.Controls.Add(this.txtGiaTri);
            this.gbModuleThongTin.Controls.Add(this.lblDonToiThieu);
            this.gbModuleThongTin.Controls.Add(this.txtDonToiThieu);
            this.gbModuleThongTin.Controls.Add(this.lblNgayBD);
            this.gbModuleThongTin.Controls.Add(this.dtpNgayBD);
            this.gbModuleThongTin.Controls.Add(this.lblNgayKT);
            this.gbModuleThongTin.Controls.Add(this.dtpNgayKT);
            this.gbModuleThongTin.Controls.Add(this.lblTrangThai);
            this.gbModuleThongTin.Controls.Add(this.chkTrangThai);
            this.gbModuleThongTin.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbModuleThongTin.Location = new System.Drawing.Point(0, 0);
            this.gbModuleThongTin.Name = "gbModuleThongTin";
            this.gbModuleThongTin.Size = new System.Drawing.Size(416, 500);
            this.gbModuleThongTin.TabIndex = 0;
            this.gbModuleThongTin.Text = "Thông tin khuyến mãi";
            // 
            // lblMaCode
            // 
            this.lblMaCode.AutoSize = true;
            this.lblMaCode.Location = new System.Drawing.Point(20, 50);
            this.lblMaCode.Name = "lblMaCode";
            this.lblMaCode.Size = new System.Drawing.Size(71, 19);
            this.lblMaCode.TabIndex = 0;
            this.lblMaCode.Text = "Mã code:";
            // 
            // txtMaCode
            // 
            this.txtMaCode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaCode.DefaultText = "";
            this.txtMaCode.Location = new System.Drawing.Point(20, 72);
            this.txtMaCode.Name = "txtMaCode";
            this.txtMaCode.PlaceholderText = "";
            this.txtMaCode.SelectedText = "";
            this.txtMaCode.Size = new System.Drawing.Size(370, 36);
            this.txtMaCode.TabIndex = 1;
            // 
            // lblTenKM
            // 
            this.lblTenKM.AutoSize = true;
            this.lblTenKM.Location = new System.Drawing.Point(20, 115);
            this.lblTenKM.Name = "lblTenKM";
            this.lblTenKM.Size = new System.Drawing.Size(117, 19);
            this.lblTenKM.TabIndex = 2;
            this.lblTenKM.Text = "Tên khuyến mãi:";
            // 
            // txtTenKM
            // 
            this.txtTenKM.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenKM.DefaultText = "";
            this.txtTenKM.Location = new System.Drawing.Point(20, 137);
            this.txtTenKM.Name = "txtTenKM";
            this.txtTenKM.PlaceholderText = "";
            this.txtTenKM.SelectedText = "";
            this.txtTenKM.Size = new System.Drawing.Size(370, 36);
            this.txtTenKM.TabIndex = 2;
            // 
            // lblLoaiGiam
            // 
            this.lblLoaiGiam.AutoSize = true;
            this.lblLoaiGiam.Location = new System.Drawing.Point(20, 180);
            this.lblLoaiGiam.Name = "lblLoaiGiam";
            this.lblLoaiGiam.Size = new System.Drawing.Size(104, 19);
            this.lblLoaiGiam.TabIndex = 3;
            this.lblLoaiGiam.Text = "Loại giảm giá:";
            // 
            // cboLoaiGiam
            // 
            this.cboLoaiGiam.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiGiam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiGiam.ItemHeight = 30;
            this.cboLoaiGiam.Location = new System.Drawing.Point(20, 202);
            this.cboLoaiGiam.Name = "cboLoaiGiam";
            this.cboLoaiGiam.Size = new System.Drawing.Size(370, 36);
            this.cboLoaiGiam.TabIndex = 3;
            // 
            // lblGiaTri
            // 
            this.lblGiaTri.AutoSize = true;
            this.lblGiaTri.Location = new System.Drawing.Point(20, 245);
            this.lblGiaTri.Name = "lblGiaTri";
            this.lblGiaTri.Size = new System.Drawing.Size(92, 19);
            this.lblGiaTri.TabIndex = 4;
            this.lblGiaTri.Text = "Giá trị giảm:";
            // 
            // txtGiaTri
            // 
            this.txtGiaTri.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGiaTri.DefaultText = "";
            this.txtGiaTri.Location = new System.Drawing.Point(20, 267);
            this.txtGiaTri.Name = "txtGiaTri";
            this.txtGiaTri.PlaceholderText = "VD: 10 (%) hoặc 50000 (VNĐ)";
            this.txtGiaTri.SelectedText = "";
            this.txtGiaTri.Size = new System.Drawing.Size(370, 36);
            this.txtGiaTri.TabIndex = 4;
            // 
            // lblDonToiThieu
            // 
            this.lblDonToiThieu.AutoSize = true;
            this.lblDonToiThieu.Location = new System.Drawing.Point(20, 310);
            this.lblDonToiThieu.Name = "lblDonToiThieu";
            this.lblDonToiThieu.Size = new System.Drawing.Size(143, 19);
            this.lblDonToiThieu.TabIndex = 5;
            this.lblDonToiThieu.Text = "Đơn tối thiểu (VNĐ):";
            // 
            // txtDonToiThieu
            // 
            this.txtDonToiThieu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDonToiThieu.DefaultText = "";
            this.txtDonToiThieu.Location = new System.Drawing.Point(20, 332);
            this.txtDonToiThieu.Name = "txtDonToiThieu";
            this.txtDonToiThieu.PlaceholderText = "Để trống nếu không giới hạn";
            this.txtDonToiThieu.SelectedText = "";
            this.txtDonToiThieu.Size = new System.Drawing.Size(370, 36);
            this.txtDonToiThieu.TabIndex = 5;
            // 
            // lblNgayBD
            // 
            this.lblNgayBD.AutoSize = true;
            this.lblNgayBD.Location = new System.Drawing.Point(20, 375);
            this.lblNgayBD.Name = "lblNgayBD";
            this.lblNgayBD.Size = new System.Drawing.Size(104, 19);
            this.lblNgayBD.TabIndex = 6;
            this.lblNgayBD.Text = "Ngày bắt đầu:";
            // 
            // dtpNgayBD
            // 
                        this.dtpNgayBD.Location = new System.Drawing.Point(20, 397);
            this.dtpNgayBD.Name = "dtpNgayBD";
            this.dtpNgayBD.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayBD.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayBD.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpNgayBD.Properties.Appearance.Options.UseFont = true;
            this.dtpNgayBD.Size = new System.Drawing.Size(175, 36);
            this.dtpNgayBD.TabIndex = 6;
            // 
            // lblNgayKT
            // 
            this.lblNgayKT.AutoSize = true;
            this.lblNgayKT.Location = new System.Drawing.Point(210, 375);
            this.lblNgayKT.Name = "lblNgayKT";
            this.lblNgayKT.Size = new System.Drawing.Size(106, 19);
            this.lblNgayKT.TabIndex = 7;
            this.lblNgayKT.Text = "Ngày kết thúc:";
            // 
            // dtpNgayKT
            // 
                        this.dtpNgayKT.Location = new System.Drawing.Point(210, 397);
            this.dtpNgayKT.Name = "dtpNgayKT";
            this.dtpNgayKT.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayKT.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayKT.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpNgayKT.Properties.Appearance.Options.UseFont = true;
            this.dtpNgayKT.Size = new System.Drawing.Size(180, 36);
            this.dtpNgayKT.TabIndex = 7;
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSize = true;
            this.lblTrangThai.Location = new System.Drawing.Point(20, 445);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(121, 19);
            this.lblTrangThai.TabIndex = 8;
            this.lblTrangThai.Text = "Đang hoạt động:";
            // 
            // chkTrangThai
            // 
            this.chkTrangThai.Location = new System.Drawing.Point(20, 467);
            this.chkTrangThai.Name = "chkTrangThai";
            this.chkTrangThai.Size = new System.Drawing.Size(60, 25);
            this.chkTrangThai.TabIndex = 8;
            // 
            // pnlSpacer
            // 
            this.pnlSpacer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSpacer.Location = new System.Drawing.Point(15, 522);
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
            this.gbChucNang.Location = new System.Drawing.Point(15, 537);
            this.gbChucNang.Name = "gbChucNang";
            this.gbChucNang.Size = new System.Drawing.Size(416, 198);
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
            // frmKhuyenMai
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 750);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmKhuyenMai";
            this.Text = "Quản Lý Khuyến Mãi";
            this.Load += new System.EventHandler(this.frmKhuyenMai_Load);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel1)).EndInit();
            this.pnlMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel2)).EndInit();
            this.pnlMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlLeft.ResumeLayout(false);
            this.gbDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.pnlTimKiem.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.gbThongTin.ResumeLayout(false);
            this.gbModuleThongTin.ResumeLayout(false);
            this.gbModuleThongTin.PerformLayout();
            this.gbChucNang.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayBD.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayBD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayKT.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayKT.Properties)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlLeft;
        private Guna.UI2.WinForms.Guna2GroupBox gbDanhSach;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private Guna.UI2.WinForms.Guna2Panel pnlTimKiem;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private Guna.UI2.WinForms.Guna2Panel pnlRight;
        private System.Windows.Forms.Panel gbThongTin;
        private Guna.UI2.WinForms.Guna2GroupBox gbModuleThongTin;
        private System.Windows.Forms.Label lblMaCode;
        private Guna.UI2.WinForms.Guna2TextBox txtMaCode;
        private System.Windows.Forms.Label lblTenKM;
        private Guna.UI2.WinForms.Guna2TextBox txtTenKM;
        private System.Windows.Forms.Label lblLoaiGiam;
        private Guna.UI2.WinForms.Guna2ComboBox cboLoaiGiam;
        private System.Windows.Forms.Label lblGiaTri;
        private Guna.UI2.WinForms.Guna2TextBox txtGiaTri;
        private System.Windows.Forms.Label lblDonToiThieu;
        private Guna.UI2.WinForms.Guna2TextBox txtDonToiThieu;
        private System.Windows.Forms.Label lblNgayBD;
        private DevExpress.XtraEditors.DateEdit dtpNgayBD;
        private System.Windows.Forms.Label lblNgayKT;
        private DevExpress.XtraEditors.DateEdit dtpNgayKT;
        private System.Windows.Forms.Label lblTrangThai;
        private Guna.UI2.WinForms.Guna2ToggleSwitch chkTrangThai;
        private Guna.UI2.WinForms.Guna2Panel pnlSpacer;
        private Guna.UI2.WinForms.Guna2GroupBox gbChucNang;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
    }
}


