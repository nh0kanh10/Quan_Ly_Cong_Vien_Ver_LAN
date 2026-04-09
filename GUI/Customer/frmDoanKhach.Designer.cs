namespace GUI
{
    partial class frmDoanKhach
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
            this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControlDoan = new DevExpress.XtraGrid.GridControl();
            this.gridViewDoan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.grpThongTinDoan = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblTenDoan = new DevExpress.XtraEditors.LabelControl();
            this.lblNguoiDD = new DevExpress.XtraEditors.LabelControl();
            this.lblSdt = new DevExpress.XtraEditors.LabelControl();
            this.lblMST = new DevExpress.XtraEditors.LabelControl();
            this.lblBooking = new DevExpress.XtraEditors.LabelControl();
            this.lblCK = new DevExpress.XtraEditors.LabelControl();
            this.lblSLKhach = new DevExpress.XtraEditors.LabelControl();
            this.lblNgayDen = new DevExpress.XtraEditors.LabelControl();
            this.lblNgayDi = new DevExpress.XtraEditors.LabelControl();
            this.lblTrangThai = new DevExpress.XtraEditors.LabelControl();
            this.txtTenDoan = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtNguoiDaiDien = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtSdt = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMaSoThue = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtMaBooking = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnCopyMaBooking = new Guna.UI2.WinForms.Guna2Button();
            this.spnChietKhau = new DevExpress.XtraEditors.SpinEdit();
            this.spnSoLuongKhach = new DevExpress.XtraEditors.SpinEdit();
            this.dtpNgayDen = new DevExpress.XtraEditors.DateEdit();
            this.dtpNgayDi = new DevExpress.XtraEditors.DateEdit();
            this.cboTrangThai = new DevExpress.XtraEditors.ComboBoxEdit();
            this.grpDichVu = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridControlDichVu = new DevExpress.XtraGrid.GridControl();
            this.gridViewDichVu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlDichVuButtons = new System.Windows.Forms.FlowLayoutPanel();
            this.btnThemCombo = new Guna.UI2.WinForms.Guna2Button();
            this.btnThemPhong = new Guna.UI2.WinForms.Guna2Button();
            this.btnThemBanAn = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaDichVu = new Guna.UI2.WinForms.Guna2Button();
            this.pnlTotals = new Guna.UI2.WinForms.Guna2Panel();
            this.lblCaptionTong = new DevExpress.XtraEditors.LabelControl();
            this.lblTongTruocCK = new DevExpress.XtraEditors.LabelControl();
            this.lblCaptionSauCK = new DevExpress.XtraEditors.LabelControl();
            this.lblTongSauCK = new DevExpress.XtraEditors.LabelControl();
            this.btnXuatHoaDon = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).BeginInit();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDoan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDoan)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.grpThongTinDoan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnChietKhau.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnSoLuongKhach.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayDen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayDen.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayDi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayDi.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTrangThai.Properties)).BeginInit();
            this.grpDichVu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDichVu)).BeginInit();
            this.pnlDichVuButtons.SuspendLayout();
            this.pnlTotals.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitMain
            // 
            this.splitMain.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel1;
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Margin = new System.Windows.Forms.Padding(2);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.gridControlDoan);
            this.splitMain.Panel1.Controls.Add(this.pnlButtons);
            this.splitMain.Panel1.Controls.Add(this.grpThongTinDoan);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.grpDichVu);
            this.splitMain.Panel2.Controls.Add(this.pnlTotals);
            this.splitMain.Size = new System.Drawing.Size(1246, 569);
            this.splitMain.SplitterPosition = 651;
            this.splitMain.TabIndex = 0;
            // 
            // gridControlDoan
            // 
            this.gridControlDoan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDoan.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControlDoan.Location = new System.Drawing.Point(0, 272);
            this.gridControlDoan.MainView = this.gridViewDoan;
            this.gridControlDoan.Margin = new System.Windows.Forms.Padding(2);
            this.gridControlDoan.Name = "gridControlDoan";
            this.gridControlDoan.Size = new System.Drawing.Size(651, 297);
            this.gridControlDoan.TabIndex = 0;
            this.gridControlDoan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDoan});
            // 
            // gridViewDoan
            // 
            this.gridViewDoan.DetailHeight = 284;
            this.gridViewDoan.GridControl = this.gridControlDoan;
            this.gridViewDoan.Name = "gridViewDoan";
            this.gridViewDoan.OptionsView.ShowGroupPanel = false;
            this.gridViewDoan.RowClick += new DevExpress.XtraGrid.Views.Grid.RowClickEventHandler(this.GridViewDoan_RowClick);
            this.gridViewDoan.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewDoan_FocusedRowChanged);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnThem);
            this.pnlButtons.Controls.Add(this.btnSua);
            this.pnlButtons.Controls.Add(this.btnXoa);
            this.pnlButtons.Controls.Add(this.btnLamMoi);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlButtons.Location = new System.Drawing.Point(0, 235);
            this.pnlButtons.Margin = new System.Windows.Forms.Padding(2);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlButtons.Size = new System.Drawing.Size(651, 37);
            this.pnlButtons.TabIndex = 1;
            // 
            // btnThem
            // 
            this.btnThem.Animated = true;
            this.btnThem.BorderRadius = 6;
            this.btnThem.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(7, 8);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(105, 26);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm Đoàn";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Animated = true;
            this.btnSua.BorderRadius = 6;
            this.btnSua.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSua.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(118, 8);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(115, 26);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Cập Nhật";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Animated = true;
            this.btnXoa.BorderRadius = 6;
            this.btnXoa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(239, 8);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(92, 26);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Animated = true;
            this.btnLamMoi.BorderRadius = 6;
            this.btnLamMoi.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(337, 8);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(95, 26);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // grpThongTinDoan
            // 
            this.grpThongTinDoan.Controls.Add(this.lblTenDoan);
            this.grpThongTinDoan.Controls.Add(this.lblNguoiDD);
            this.grpThongTinDoan.Controls.Add(this.lblSdt);
            this.grpThongTinDoan.Controls.Add(this.lblMST);
            this.grpThongTinDoan.Controls.Add(this.lblBooking);
            this.grpThongTinDoan.Controls.Add(this.lblCK);
            this.grpThongTinDoan.Controls.Add(this.lblSLKhach);
            this.grpThongTinDoan.Controls.Add(this.lblNgayDen);
            this.grpThongTinDoan.Controls.Add(this.lblNgayDi);
            this.grpThongTinDoan.Controls.Add(this.lblTrangThai);
            this.grpThongTinDoan.Controls.Add(this.txtTenDoan);
            this.grpThongTinDoan.Controls.Add(this.txtNguoiDaiDien);
            this.grpThongTinDoan.Controls.Add(this.txtSdt);
            this.grpThongTinDoan.Controls.Add(this.txtMaSoThue);
            this.grpThongTinDoan.Controls.Add(this.btnCopyMaBooking);
            this.grpThongTinDoan.Controls.Add(this.txtMaBooking);
            this.grpThongTinDoan.Controls.Add(this.spnChietKhau);
            this.grpThongTinDoan.Controls.Add(this.spnSoLuongKhach);
            this.grpThongTinDoan.Controls.Add(this.dtpNgayDen);
            this.grpThongTinDoan.Controls.Add(this.dtpNgayDi);
            this.grpThongTinDoan.Controls.Add(this.cboTrangThai);
            this.grpThongTinDoan.Dock = System.Windows.Forms.DockStyle.Top;
            this.grpThongTinDoan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpThongTinDoan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.grpThongTinDoan.Location = new System.Drawing.Point(0, 0);
            this.grpThongTinDoan.Margin = new System.Windows.Forms.Padding(2);
            this.grpThongTinDoan.Name = "grpThongTinDoan";
            this.grpThongTinDoan.Size = new System.Drawing.Size(651, 235);
            this.grpThongTinDoan.TabIndex = 2;
            this.grpThongTinDoan.Text = "THÔNG TIN ĐOÀN";
            // 
            // lblTenDoan
            // 
            this.lblTenDoan.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTenDoan.Location = new System.Drawing.Point(11, 59);
            this.lblTenDoan.Margin = new System.Windows.Forms.Padding(2);
            this.lblTenDoan.Name = "lblTenDoan";
            this.lblTenDoan.Size = new System.Drawing.Size(86, 16);
            this.lblTenDoan.TabIndex = 0;
            this.lblTenDoan.Text = "Tên đoàn *";
            // 
            // lblNguoiDD
            // 
            this.lblNguoiDD.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblNguoiDD.Location = new System.Drawing.Point(11, 90);
            this.lblNguoiDD.Margin = new System.Windows.Forms.Padding(2);
            this.lblNguoiDD.Name = "lblNguoiDD";
            this.lblNguoiDD.Size = new System.Drawing.Size(86, 16);
            this.lblNguoiDD.TabIndex = 1;
            this.lblNguoiDD.Text = "Người đại diện";
            // 
            // lblSdt
            // 
            this.lblSdt.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSdt.Location = new System.Drawing.Point(11, 120);
            this.lblSdt.Margin = new System.Windows.Forms.Padding(2);
            this.lblSdt.Name = "lblSdt";
            this.lblSdt.Size = new System.Drawing.Size(86, 16);
            this.lblSdt.TabIndex = 2;
            this.lblSdt.Text = "SĐT liên hệ";
            // 
            // lblMST
            // 
            this.lblMST.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMST.Location = new System.Drawing.Point(11, 151);
            this.lblMST.Margin = new System.Windows.Forms.Padding(2);
            this.lblMST.Name = "lblMST";
            this.lblMST.Size = new System.Drawing.Size(86, 16);
            this.lblMST.TabIndex = 3;
            this.lblMST.Text = "Mã số thuế";
            // 
            // lblBooking
            // 
            this.lblBooking.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblBooking.Location = new System.Drawing.Point(11, 182);
            this.lblBooking.Margin = new System.Windows.Forms.Padding(2);
            this.lblBooking.Name = "lblBooking";
            this.lblBooking.Size = new System.Drawing.Size(86, 16);
            this.lblBooking.TabIndex = 4;
            this.lblBooking.Text = "Mã Booking";
            // 
            // lblCK
            // 
            this.lblCK.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCK.Location = new System.Drawing.Point(376, 59);
            this.lblCK.Margin = new System.Windows.Forms.Padding(2);
            this.lblCK.Name = "lblCK";
            this.lblCK.Size = new System.Drawing.Size(86, 16);
            this.lblCK.TabIndex = 5;
            this.lblCK.Text = "Chiết khấu %";
            // 
            // lblSLKhach
            // 
            this.lblSLKhach.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSLKhach.Location = new System.Drawing.Point(376, 90);
            this.lblSLKhach.Margin = new System.Windows.Forms.Padding(2);
            this.lblSLKhach.Name = "lblSLKhach";
            this.lblSLKhach.Size = new System.Drawing.Size(86, 16);
            this.lblSLKhach.TabIndex = 6;
            this.lblSLKhach.Text = "Số khách (Pax)";
            // 
            // lblNgayDen
            // 
            this.lblNgayDen.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblNgayDen.Location = new System.Drawing.Point(376, 121);
            this.lblNgayDen.Margin = new System.Windows.Forms.Padding(2);
            this.lblNgayDen.Name = "lblNgayDen";
            this.lblNgayDen.Size = new System.Drawing.Size(86, 16);
            this.lblNgayDen.TabIndex = 7;
            this.lblNgayDen.Text = "Ngày đến";
            // 
            // lblNgayDi
            // 
            this.lblNgayDi.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblNgayDi.Location = new System.Drawing.Point(376, 152);
            this.lblNgayDi.Margin = new System.Windows.Forms.Padding(2);
            this.lblNgayDi.Name = "lblNgayDi";
            this.lblNgayDi.Size = new System.Drawing.Size(86, 16);
            this.lblNgayDi.TabIndex = 8;
            this.lblNgayDi.Text = "Ngày đi";
            // 
            // lblTrangThai
            // 
            this.lblTrangThai.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTrangThai.Location = new System.Drawing.Point(376, 183);
            this.lblTrangThai.Margin = new System.Windows.Forms.Padding(2);
            this.lblTrangThai.Name = "lblTrangThai";
            this.lblTrangThai.Size = new System.Drawing.Size(86, 16);
            this.lblTrangThai.TabIndex = 9;
            this.lblTrangThai.Text = "Trạng thái";
            // 
            // txtTenDoan
            // 
            this.txtTenDoan.BorderRadius = 4;
            this.txtTenDoan.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenDoan.DefaultText = "";
            this.txtTenDoan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTenDoan.Location = new System.Drawing.Point(107, 56);
            this.txtTenDoan.Margin = new System.Windows.Forms.Padding(2);
            this.txtTenDoan.Name = "txtTenDoan";
            this.txtTenDoan.PlaceholderText = "";
            this.txtTenDoan.SelectedText = "";
            this.txtTenDoan.Size = new System.Drawing.Size(218, 28);
            this.txtTenDoan.TabIndex = 10;
            // 
            // txtNguoiDaiDien
            // 
            this.txtNguoiDaiDien.BorderRadius = 4;
            this.txtNguoiDaiDien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNguoiDaiDien.DefaultText = "";
            this.txtNguoiDaiDien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtNguoiDaiDien.Location = new System.Drawing.Point(107, 87);
            this.txtNguoiDaiDien.Margin = new System.Windows.Forms.Padding(2);
            this.txtNguoiDaiDien.Name = "txtNguoiDaiDien";
            this.txtNguoiDaiDien.PlaceholderText = "";
            this.txtNguoiDaiDien.SelectedText = "";
            this.txtNguoiDaiDien.Size = new System.Drawing.Size(218, 28);
            this.txtNguoiDaiDien.TabIndex = 11;
            // 
            // txtSdt
            // 
            this.txtSdt.BorderRadius = 4;
            this.txtSdt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSdt.DefaultText = "";
            this.txtSdt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSdt.Location = new System.Drawing.Point(107, 119);
            this.txtSdt.Margin = new System.Windows.Forms.Padding(2);
            this.txtSdt.Name = "txtSdt";
            this.txtSdt.PlaceholderText = "";
            this.txtSdt.SelectedText = "";
            this.txtSdt.Size = new System.Drawing.Size(218, 28);
            this.txtSdt.TabIndex = 12;
            // 
            // txtMaSoThue
            // 
            this.txtMaSoThue.BorderRadius = 4;
            this.txtMaSoThue.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaSoThue.DefaultText = "";
            this.txtMaSoThue.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaSoThue.Location = new System.Drawing.Point(107, 149);
            this.txtMaSoThue.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaSoThue.Name = "txtMaSoThue";
            this.txtMaSoThue.PlaceholderText = "";
            this.txtMaSoThue.SelectedText = "";
            this.txtMaSoThue.Size = new System.Drawing.Size(218, 28);
            this.txtMaSoThue.TabIndex = 13;
            // 
            // txtMaBooking
            // 
            this.txtMaBooking.BorderRadius = 4;
            this.txtMaBooking.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaBooking.DefaultText = "";
            this.txtMaBooking.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.txtMaBooking.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtMaBooking.Location = new System.Drawing.Point(107, 180);
            this.txtMaBooking.Margin = new System.Windows.Forms.Padding(2);
            this.txtMaBooking.Name = "txtMaBooking";
            this.txtMaBooking.PlaceholderText = "Hệ thống tự động cấp";
            this.txtMaBooking.ReadOnly = true;
            this.txtMaBooking.SelectedText = "";
            this.txtMaBooking.Size = new System.Drawing.Size(183, 28);
            this.txtMaBooking.TabIndex = 14;
            // 
            // btnCopyMaBooking
            // 
            this.btnCopyMaBooking.BorderRadius = 4;
            this.btnCopyMaBooking.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCopyMaBooking.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnCopyMaBooking.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnCopyMaBooking.ForeColor = System.Drawing.Color.White;
            this.btnCopyMaBooking.Location = new System.Drawing.Point(293, 180);
            this.btnCopyMaBooking.Name = "btnCopyMaBooking";
            this.btnCopyMaBooking.Size = new System.Drawing.Size(32, 28);
            this.btnCopyMaBooking.TabIndex = 20;
            this.btnCopyMaBooking.Text = "📋";
            this.btnCopyMaBooking.Click += new System.EventHandler(this.btnCopyMaBooking_Click);
            // 
            // spnChietKhau
            // 
            this.spnChietKhau.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnChietKhau.Location = new System.Drawing.Point(472, 57);
            this.spnChietKhau.Margin = new System.Windows.Forms.Padding(2);
            this.spnChietKhau.Name = "spnChietKhau";
            this.spnChietKhau.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spnChietKhau.Properties.MaxValue = new decimal(new int[] {
            100,
            0,
            0,
            0});
            this.spnChietKhau.Size = new System.Drawing.Size(141, 20);
            this.spnChietKhau.TabIndex = 15;
            // 
            // spnSoLuongKhach
            // 
            this.spnSoLuongKhach.EditValue = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.spnSoLuongKhach.Location = new System.Drawing.Point(472, 88);
            this.spnSoLuongKhach.Margin = new System.Windows.Forms.Padding(2);
            this.spnSoLuongKhach.Name = "spnSoLuongKhach";
            this.spnSoLuongKhach.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.spnSoLuongKhach.Properties.MaxValue = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spnSoLuongKhach.Properties.MinValue = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnSoLuongKhach.Size = new System.Drawing.Size(141, 20);
            this.spnSoLuongKhach.TabIndex = 16;
            // 
            // dtpNgayDen
            // 
            this.dtpNgayDen.EditValue = new System.DateTime(2026, 4, 5, 0, 0, 0, 0);
            this.dtpNgayDen.Location = new System.Drawing.Point(472, 118);
            this.dtpNgayDen.Margin = new System.Windows.Forms.Padding(2);
            this.dtpNgayDen.Name = "dtpNgayDen";
            this.dtpNgayDen.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayDen.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayDen.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtpNgayDen.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpNgayDen.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtpNgayDen.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpNgayDen.Size = new System.Drawing.Size(141, 20);
            this.dtpNgayDen.TabIndex = 17;
            // 
            // dtpNgayDi
            // 
            this.dtpNgayDi.EditValue = new System.DateTime(2026, 4, 5, 0, 0, 0, 0);
            this.dtpNgayDi.Location = new System.Drawing.Point(472, 149);
            this.dtpNgayDi.Margin = new System.Windows.Forms.Padding(2);
            this.dtpNgayDi.Name = "dtpNgayDi";
            this.dtpNgayDi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayDi.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayDi.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtpNgayDi.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpNgayDi.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtpNgayDi.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpNgayDi.Size = new System.Drawing.Size(141, 20);
            this.dtpNgayDi.TabIndex = 18;
            // 
            // cboTrangThai
            // 
            this.cboTrangThai.Location = new System.Drawing.Point(472, 180);
            this.cboTrangThai.Margin = new System.Windows.Forms.Padding(2);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTrangThai.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboTrangThai.Size = new System.Drawing.Size(141, 20);
            this.cboTrangThai.TabIndex = 19;
            // 
            // grpDichVu
            // 
            this.grpDichVu.Controls.Add(this.gridControlDichVu);
            this.grpDichVu.Controls.Add(this.pnlDichVuButtons);
            this.grpDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpDichVu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.grpDichVu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.grpDichVu.Location = new System.Drawing.Point(0, 0);
            this.grpDichVu.Margin = new System.Windows.Forms.Padding(2);
            this.grpDichVu.Name = "grpDichVu";
            this.grpDichVu.Size = new System.Drawing.Size(585, 488);
            this.grpDichVu.TabIndex = 0;
            this.grpDichVu.Text = "DỊCH VỤ ĐOÀN";
            // 
            // gridControlDichVu
            // 
            this.gridControlDichVu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlDichVu.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(2);
            this.gridControlDichVu.Location = new System.Drawing.Point(0, 77);
            this.gridControlDichVu.MainView = this.gridViewDichVu;
            this.gridControlDichVu.Margin = new System.Windows.Forms.Padding(2);
            this.gridControlDichVu.Name = "gridControlDichVu";
            this.gridControlDichVu.Size = new System.Drawing.Size(585, 411);
            this.gridControlDichVu.TabIndex = 0;
            this.gridControlDichVu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDichVu});
            // 
            // gridViewDichVu
            // 
            this.gridViewDichVu.DetailHeight = 284;
            this.gridViewDichVu.GridControl = this.gridControlDichVu;
            this.gridViewDichVu.Name = "gridViewDichVu";
            this.gridViewDichVu.OptionsView.ShowGroupPanel = false;
            // 
            // pnlDichVuButtons
            // 
            this.pnlDichVuButtons.Controls.Add(this.btnThemCombo);
            this.pnlDichVuButtons.Controls.Add(this.btnThemPhong);
            this.pnlDichVuButtons.Controls.Add(this.btnThemBanAn);
            this.pnlDichVuButtons.Controls.Add(this.btnXoaDichVu);
            this.pnlDichVuButtons.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlDichVuButtons.Location = new System.Drawing.Point(0, 40);
            this.pnlDichVuButtons.Margin = new System.Windows.Forms.Padding(2);
            this.pnlDichVuButtons.Name = "pnlDichVuButtons";
            this.pnlDichVuButtons.Padding = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pnlDichVuButtons.Size = new System.Drawing.Size(585, 37);
            this.pnlDichVuButtons.TabIndex = 1;
            // 
            // btnThemCombo
            // 
            this.btnThemCombo.Animated = true;
            this.btnThemCombo.BorderRadius = 6;
            this.btnThemCombo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemCombo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnThemCombo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThemCombo.ForeColor = System.Drawing.Color.White;
            this.btnThemCombo.Location = new System.Drawing.Point(7, 8);
            this.btnThemCombo.Name = "btnThemCombo";
            this.btnThemCombo.Size = new System.Drawing.Size(148, 26);
            this.btnThemCombo.TabIndex = 0;
            this.btnThemCombo.Text = "Thêm Combo";
            this.btnThemCombo.Click += new System.EventHandler(this.btnThemCombo_Click);
            // 
            // btnThemPhong
            // 
            this.btnThemPhong.Animated = true;
            this.btnThemPhong.BorderRadius = 6;
            this.btnThemPhong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemPhong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnThemPhong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThemPhong.ForeColor = System.Drawing.Color.White;
            this.btnThemPhong.Location = new System.Drawing.Point(161, 8);
            this.btnThemPhong.Name = "btnThemPhong";
            this.btnThemPhong.Size = new System.Drawing.Size(140, 26);
            this.btnThemPhong.TabIndex = 1;
            this.btnThemPhong.Text = "Thêm Phòng";
            this.btnThemPhong.Click += new System.EventHandler(this.btnThemPhong_Click);
            // 
            // btnThemBanAn
            // 
            this.btnThemBanAn.Animated = true;
            this.btnThemBanAn.BorderRadius = 6;
            this.btnThemBanAn.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnThemBanAn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnThemBanAn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnThemBanAn.ForeColor = System.Drawing.Color.White;
            this.btnThemBanAn.Location = new System.Drawing.Point(307, 8);
            this.btnThemBanAn.Name = "btnThemBanAn";
            this.btnThemBanAn.Size = new System.Drawing.Size(121, 26);
            this.btnThemBanAn.TabIndex = 2;
            this.btnThemBanAn.Text = "Đặt Bàn ăn";
            this.btnThemBanAn.Click += new System.EventHandler(this.btnThemBanAn_Click);
            // 
            // btnXoaDichVu
            // 
            this.btnXoaDichVu.Animated = true;
            this.btnXoaDichVu.BorderRadius = 6;
            this.btnXoaDichVu.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXoaDichVu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoaDichVu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXoaDichVu.ForeColor = System.Drawing.Color.White;
            this.btnXoaDichVu.Location = new System.Drawing.Point(434, 8);
            this.btnXoaDichVu.Name = "btnXoaDichVu";
            this.btnXoaDichVu.Size = new System.Drawing.Size(141, 26);
            this.btnXoaDichVu.TabIndex = 3;
            this.btnXoaDichVu.Text = "Xóa Dịch vụ";
            this.btnXoaDichVu.Click += new System.EventHandler(this.btnXoaDichVu_Click);
            // 
            // pnlTotals
            // 
            this.pnlTotals.Controls.Add(this.lblCaptionTong);
            this.pnlTotals.Controls.Add(this.lblTongTruocCK);
            this.pnlTotals.Controls.Add(this.lblCaptionSauCK);
            this.pnlTotals.Controls.Add(this.lblTongSauCK);
            this.pnlTotals.Controls.Add(this.btnXuatHoaDon);
            this.pnlTotals.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTotals.Location = new System.Drawing.Point(0, 488);
            this.pnlTotals.Margin = new System.Windows.Forms.Padding(2);
            this.pnlTotals.Name = "pnlTotals";
            this.pnlTotals.Padding = new System.Windows.Forms.Padding(9, 6, 9, 6);
            this.pnlTotals.Size = new System.Drawing.Size(585, 81);
            this.pnlTotals.TabIndex = 1;
            // 
            // lblCaptionTong
            // 
            this.lblCaptionTong.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCaptionTong.Location = new System.Drawing.Point(9, 22);
            this.lblCaptionTong.Margin = new System.Windows.Forms.Padding(2);
            this.lblCaptionTong.Name = "lblCaptionTong";
            this.lblCaptionTong.Size = new System.Drawing.Size(90, 16);
            this.lblCaptionTong.TabIndex = 0;
            this.lblCaptionTong.Text = "Tổng trước CK:";
            // 
            // lblTongTruocCK
            // 
            this.lblTongTruocCK.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.lblTongTruocCK.Appearance.Options.UseFont = true;
            this.lblTongTruocCK.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTongTruocCK.Location = new System.Drawing.Point(101, 22);
            this.lblTongTruocCK.Margin = new System.Windows.Forms.Padding(2);
            this.lblTongTruocCK.Name = "lblTongTruocCK";
            this.lblTongTruocCK.Size = new System.Drawing.Size(135, 16);
            this.lblTongTruocCK.TabIndex = 1;
            this.lblTongTruocCK.Text = "0 đ";
            // 
            // lblCaptionSauCK
            // 
            this.lblCaptionSauCK.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCaptionSauCK.Location = new System.Drawing.Point(9, 42);
            this.lblCaptionSauCK.Margin = new System.Windows.Forms.Padding(2);
            this.lblCaptionSauCK.Name = "lblCaptionSauCK";
            this.lblCaptionSauCK.Size = new System.Drawing.Size(90, 16);
            this.lblCaptionSauCK.TabIndex = 2;
            this.lblCaptionSauCK.Text = "Tổng sau CK:";
            // 
            // lblTongSauCK
            // 
            this.lblTongSauCK.Appearance.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold);
            this.lblTongSauCK.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblTongSauCK.Appearance.Options.UseFont = true;
            this.lblTongSauCK.Appearance.Options.UseForeColor = true;
            this.lblTongSauCK.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTongSauCK.Location = new System.Drawing.Point(101, 42);
            this.lblTongSauCK.Margin = new System.Windows.Forms.Padding(2);
            this.lblTongSauCK.Name = "lblTongSauCK";
            this.lblTongSauCK.Size = new System.Drawing.Size(135, 20);
            this.lblTongSauCK.TabIndex = 3;
            this.lblTongSauCK.Text = "0 đ";
            // 
            // btnXuatHoaDon
            // 
            this.btnXuatHoaDon.Animated = true;
            this.btnXuatHoaDon.BorderRadius = 6;
            this.btnXuatHoaDon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXuatHoaDon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(22)))), ((int)(((byte)(163)))), ((int)(((byte)(74)))));
            this.btnXuatHoaDon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.btnXuatHoaDon.ForeColor = System.Drawing.Color.White;
            this.btnXuatHoaDon.Location = new System.Drawing.Point(342, 16);
            this.btnXuatHoaDon.Margin = new System.Windows.Forms.Padding(2);
            this.btnXuatHoaDon.Name = "btnXuatHoaDon";
            this.btnXuatHoaDon.Size = new System.Drawing.Size(232, 45);
            this.btnXuatHoaDon.TabIndex = 4;
            this.btnXuatHoaDon.Text = "XUẤT HÓA ĐƠN / THU TIỀN";
            this.btnXuatHoaDon.Click += new System.EventHandler(this.btnXuatHoaDon_Click);
            // 
            // frmDoanKhach
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1246, 569);
            this.Controls.Add(this.splitMain);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmDoanKhach";
            this.Text = "QUẢN LÝ ĐOÀN KHÁCH";
            this.Load += new System.EventHandler(this.frmDoanKhach_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).EndInit();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDoan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDoan)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.grpThongTinDoan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.spnChietKhau.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnSoLuongKhach.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayDen.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayDen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayDi.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayDi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTrangThai.Properties)).EndInit();
            this.grpDichVu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDichVu)).EndInit();
            this.pnlDichVuButtons.ResumeLayout(false);
            this.pnlTotals.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        // ── Controls ──
        private DevExpress.XtraEditors.SplitContainerControl splitMain;
        private Guna.UI2.WinForms.Guna2GroupBox grpThongTinDoan;
        private Guna.UI2.WinForms.Guna2TextBox txtTenDoan;
        private Guna.UI2.WinForms.Guna2TextBox txtNguoiDaiDien;
        private Guna.UI2.WinForms.Guna2TextBox txtSdt;
        private Guna.UI2.WinForms.Guna2TextBox txtMaSoThue;
        private Guna.UI2.WinForms.Guna2TextBox txtMaBooking;
        private Guna.UI2.WinForms.Guna2Button btnCopyMaBooking;
        private DevExpress.XtraEditors.SpinEdit spnChietKhau;
        private DevExpress.XtraEditors.SpinEdit spnSoLuongKhach;
        private DevExpress.XtraEditors.DateEdit dtpNgayDen;
        private DevExpress.XtraEditors.DateEdit dtpNgayDi;
        private DevExpress.XtraEditors.ComboBoxEdit cboTrangThai;
        private DevExpress.XtraEditors.LabelControl lblTenDoan;
        private DevExpress.XtraEditors.LabelControl lblNguoiDD;
        private DevExpress.XtraEditors.LabelControl lblSdt;
        private DevExpress.XtraEditors.LabelControl lblMST;
        private DevExpress.XtraEditors.LabelControl lblBooking;
        private DevExpress.XtraEditors.LabelControl lblCK;
        private DevExpress.XtraEditors.LabelControl lblSLKhach;
        private DevExpress.XtraEditors.LabelControl lblNgayDen;
        private DevExpress.XtraEditors.LabelControl lblNgayDi;
        private DevExpress.XtraEditors.LabelControl lblTrangThai;
        private System.Windows.Forms.FlowLayoutPanel pnlButtons;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private DevExpress.XtraGrid.GridControl gridControlDoan;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDoan;

        internal Guna.UI2.WinForms.Guna2GroupBox grpDichVu;
        private DevExpress.XtraGrid.GridControl gridControlDichVu;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDichVu;
        private System.Windows.Forms.FlowLayoutPanel pnlDichVuButtons;
        private Guna.UI2.WinForms.Guna2Button btnThemCombo;
        private Guna.UI2.WinForms.Guna2Button btnThemPhong;
        private Guna.UI2.WinForms.Guna2Button btnThemBanAn;
        private Guna.UI2.WinForms.Guna2Button btnXoaDichVu;
        private Guna.UI2.WinForms.Guna2Panel pnlTotals;
        private DevExpress.XtraEditors.LabelControl lblCaptionTong;
        private DevExpress.XtraEditors.LabelControl lblTongTruocCK;
        private DevExpress.XtraEditors.LabelControl lblCaptionSauCK;
        private DevExpress.XtraEditors.LabelControl lblTongSauCK;
        private Guna.UI2.WinForms.Guna2Button btnXuatHoaDon;
    }
}
