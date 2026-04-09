namespace GUI
{
    partial class frmMarketing
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
            this.tabControlMain = new Guna.UI2.WinForms.Guna2TabControl();
            this.tabSuKien = new System.Windows.Forms.TabPage();
            this.panelSuKienLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.gridSuKien = new DevExpress.XtraGrid.GridControl();
            this.gridViewSuKien = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelSuKienRight = new Guna.UI2.WinForms.Guna2Panel();
            this.lblMaSuKien = new System.Windows.Forms.Label();
            this.txtMaSuKien = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenSuKien = new System.Windows.Forms.Label();
            this.txtTenSuKien = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblMoTa = new System.Windows.Forms.Label();
            this.txtMoTa = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNgayBatDauSK = new System.Windows.Forms.Label();
            this.dtpNgayBatDauSK = new DevExpress.XtraEditors.DateEdit();
            this.lblNgayKetThucSK = new System.Windows.Forms.Label();
            this.dtpNgayKetThucSK = new DevExpress.XtraEditors.DateEdit();
            this.lblTrangThaiSK = new System.Windows.Forms.Label();
            this.cboTrangThaiSK = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnThemSuKien = new Guna.UI2.WinForms.Guna2Button();
            this.btnSuaSuKien = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaSuKien = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoiSuKien = new Guna.UI2.WinForms.Guna2Button();
            this.tabKhuyenMai = new System.Windows.Forms.TabPage();
            this.panelKhuyenMaiLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.gridKhuyenMai = new DevExpress.XtraGrid.GridControl();
            this.gridViewKhuyenMai = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelKhuyenMaiRight = new Guna.UI2.WinForms.Guna2Panel();
            this.lblMaKhuyenMai = new System.Windows.Forms.Label();
            this.txtMaKhuyenMai = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenKhuyenMai = new System.Windows.Forms.Label();
            this.txtTenKhuyenMai = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblLoaiGiamGia = new System.Windows.Forms.Label();
            this.cboLoaiGiamGia = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblGiaTriGiam = new System.Windows.Forms.Label();
            this.txtGiaTriGiam = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDonToiThieu = new System.Windows.Forms.Label();
            this.txtDonToiThieu = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblNgayBatDauKM = new System.Windows.Forms.Label();
            this.dtpNgayBatDauKM = new DevExpress.XtraEditors.DateEdit();
            this.lblNgayKetThucKM = new System.Windows.Forms.Label();
            this.dtpNgayKetThucKM = new DevExpress.XtraEditors.DateEdit();
            this.lblSuKien = new System.Windows.Forms.Label();
            this.slkSuKien = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.searchLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chkTrangThaiKM = new System.Windows.Forms.CheckBox();
            this.btnThemKhuyenMai = new Guna.UI2.WinForms.Guna2Button();
            this.btnSuaKhuyenMai = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoaKhuyenMai = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoiKhuyenMai = new Guna.UI2.WinForms.Guna2Button();
            this.tabControlMain.SuspendLayout();
            this.tabSuKien.SuspendLayout();
            this.panelSuKienLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSuKien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSuKien)).BeginInit();
            this.panelSuKienRight.SuspendLayout();
            this.tabKhuyenMai.SuspendLayout();
            this.panelKhuyenMaiLeft.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridKhuyenMai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewKhuyenMai)).BeginInit();
            this.panelKhuyenMaiRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slkSuKien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControlMain
            // 
            this.tabControlMain.Controls.Add(this.tabSuKien);
            this.tabControlMain.Controls.Add(this.tabKhuyenMai);
            this.tabControlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlMain.ItemSize = new System.Drawing.Size(180, 40);
            this.tabControlMain.Location = new System.Drawing.Point(0, 0);
            this.tabControlMain.Name = "tabControlMain";
            this.tabControlMain.SelectedIndex = 0;
            this.tabControlMain.Size = new System.Drawing.Size(1200, 700);
            this.tabControlMain.TabButtonHoverState.BorderColor = System.Drawing.Color.Empty;
            this.tabControlMain.TabButtonHoverState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(40)))), ((int)(((byte)(52)))), ((int)(((byte)(70)))));
            this.tabControlMain.TabButtonIdleState.BorderColor = System.Drawing.Color.Empty;
            this.tabControlMain.TabButtonIdleState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabControlMain.TabButtonSelectedState.BorderColor = System.Drawing.Color.Empty;
            this.tabControlMain.TabButtonSelectedState.InnerColor = System.Drawing.Color.FromArgb(((int)(((byte)(76)))), ((int)(((byte)(132)))), ((int)(((byte)(255)))));
            this.tabControlMain.TabButtonSize = new System.Drawing.Size(180, 40);
            this.tabControlMain.TabIndex = 0;
            this.tabControlMain.TabMenuBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(42)))), ((int)(((byte)(57)))));
            this.tabControlMain.TabMenuOrientation = Guna.UI2.WinForms.TabMenuOrientation.HorizontalTop;
            // 
            // tabSuKien
            // 
            this.tabSuKien.Controls.Add(this.panelSuKienLeft);
            this.tabSuKien.Controls.Add(this.panelSuKienRight);
            this.tabSuKien.Location = new System.Drawing.Point(4, 44);
            this.tabSuKien.Name = "tabSuKien";
            this.tabSuKien.Padding = new System.Windows.Forms.Padding(10);
            this.tabSuKien.Size = new System.Drawing.Size(1192, 652);
            this.tabSuKien.TabIndex = 0;
            this.tabSuKien.Text = "Sự Kiện Marketing";
            this.tabSuKien.UseVisualStyleBackColor = true;
            // 
            // panelSuKienLeft
            // 
            this.panelSuKienLeft.Controls.Add(this.gridSuKien);
            this.panelSuKienLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelSuKienLeft.Location = new System.Drawing.Point(10, 10);
            this.panelSuKienLeft.Name = "panelSuKienLeft";
            this.panelSuKienLeft.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.panelSuKienLeft.Size = new System.Drawing.Size(832, 632);
            this.panelSuKienLeft.TabIndex = 1;
            // 
            // gridSuKien
            // 
            this.gridSuKien.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridSuKien.Location = new System.Drawing.Point(0, 0);
            this.gridSuKien.MainView = this.gridViewSuKien;
            this.gridSuKien.Name = "gridSuKien";
            this.gridSuKien.Size = new System.Drawing.Size(822, 632);
            this.gridSuKien.TabIndex = 0;
            this.gridSuKien.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSuKien});
            // 
            // gridViewSuKien
            // 
            this.gridViewSuKien.GridControl = this.gridSuKien;
            this.gridViewSuKien.Name = "gridViewSuKien";
            this.gridViewSuKien.OptionsBehavior.Editable = false;
            this.gridViewSuKien.OptionsView.ShowGroupPanel = false;
            // 
            // panelSuKienRight
            // 
            this.panelSuKienRight.Controls.Add(this.lblMaSuKien);
            this.panelSuKienRight.Controls.Add(this.txtMaSuKien);
            this.panelSuKienRight.Controls.Add(this.lblTenSuKien);
            this.panelSuKienRight.Controls.Add(this.txtTenSuKien);
            this.panelSuKienRight.Controls.Add(this.lblMoTa);
            this.panelSuKienRight.Controls.Add(this.txtMoTa);
            this.panelSuKienRight.Controls.Add(this.lblNgayBatDauSK);
            this.panelSuKienRight.Controls.Add(this.dtpNgayBatDauSK);
            this.panelSuKienRight.Controls.Add(this.lblNgayKetThucSK);
            this.panelSuKienRight.Controls.Add(this.dtpNgayKetThucSK);
            this.panelSuKienRight.Controls.Add(this.lblTrangThaiSK);
            this.panelSuKienRight.Controls.Add(this.cboTrangThaiSK);
            this.panelSuKienRight.Controls.Add(this.btnThemSuKien);
            this.panelSuKienRight.Controls.Add(this.btnSuaSuKien);
            this.panelSuKienRight.Controls.Add(this.btnXoaSuKien);
            this.panelSuKienRight.Controls.Add(this.btnLamMoiSuKien);
            this.panelSuKienRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelSuKienRight.Location = new System.Drawing.Point(842, 10);
            this.panelSuKienRight.Name = "panelSuKienRight";
            this.panelSuKienRight.Size = new System.Drawing.Size(340, 632);
            this.panelSuKienRight.TabIndex = 0;
            // 
            // lblMaSuKien
            // 
            this.lblMaSuKien.AutoSize = true;
            this.lblMaSuKien.Location = new System.Drawing.Point(20, 20);
            this.lblMaSuKien.Name = "lblMaSuKien";
            this.lblMaSuKien.Size = new System.Drawing.Size(75, 17);
            this.lblMaSuKien.TabIndex = 0;
            this.lblMaSuKien.Text = "Mã Sự Kiện";
            // 
            // txtMaSuKien
            // 
            this.txtMaSuKien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaSuKien.DefaultText = "";
            this.txtMaSuKien.Location = new System.Drawing.Point(20, 40);
            this.txtMaSuKien.Name = "txtMaSuKien";
            this.txtMaSuKien.PlaceholderText = "";
            this.txtMaSuKien.ReadOnly = true;
            this.txtMaSuKien.SelectedText = "";
            this.txtMaSuKien.Size = new System.Drawing.Size(300, 36);
            this.txtMaSuKien.TabIndex = 1;
            // 
            // lblTenSuKien
            // 
            this.lblTenSuKien.AutoSize = true;
            this.lblTenSuKien.Location = new System.Drawing.Point(20, 90);
            this.lblTenSuKien.Name = "lblTenSuKien";
            this.lblTenSuKien.Size = new System.Drawing.Size(76, 17);
            this.lblTenSuKien.TabIndex = 2;
            this.lblTenSuKien.Text = "Tên Sự Kiện";
            // 
            // txtTenSuKien
            // 
            this.txtTenSuKien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenSuKien.DefaultText = "";
            this.txtTenSuKien.Location = new System.Drawing.Point(20, 110);
            this.txtTenSuKien.Name = "txtTenSuKien";
            this.txtTenSuKien.PlaceholderText = "";
            this.txtTenSuKien.SelectedText = "";
            this.txtTenSuKien.Size = new System.Drawing.Size(300, 36);
            this.txtTenSuKien.TabIndex = 3;
            // 
            // lblMoTa
            // 
            this.lblMoTa.AutoSize = true;
            this.lblMoTa.Location = new System.Drawing.Point(20, 160);
            this.lblMoTa.Name = "lblMoTa";
            this.lblMoTa.Size = new System.Drawing.Size(46, 17);
            this.lblMoTa.TabIndex = 4;
            this.lblMoTa.Text = "Mô Tả";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMoTa.DefaultText = "";
            this.txtMoTa.Location = new System.Drawing.Point(20, 180);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.PlaceholderText = "";
            this.txtMoTa.SelectedText = "";
            this.txtMoTa.Size = new System.Drawing.Size(300, 36);
            this.txtMoTa.TabIndex = 5;
            // 
            // lblNgayBatDauSK
            // 
            this.lblNgayBatDauSK.AutoSize = true;
            this.lblNgayBatDauSK.Location = new System.Drawing.Point(20, 230);
            this.lblNgayBatDauSK.Name = "lblNgayBatDauSK";
            this.lblNgayBatDauSK.Size = new System.Drawing.Size(88, 17);
            this.lblNgayBatDauSK.TabIndex = 6;
            this.lblNgayBatDauSK.Text = "Ngày Bắt Đầu";
            // 
            // dtpNgayBatDauSK
            // 
            this.dtpNgayBatDauSK.Location = new System.Drawing.Point(20, 250);
            this.dtpNgayBatDauSK.Name = "dtpNgayBatDauSK";
            this.dtpNgayBatDauSK.Size = new System.Drawing.Size(300, 36);
            this.dtpNgayBatDauSK.TabIndex = 7;
            this.dtpNgayBatDauSK.DateTime = System.DateTime.Now;
            // 
            // lblNgayKetThucSK
            // 
            this.lblNgayKetThucSK.AutoSize = true;
            this.lblNgayKetThucSK.Location = new System.Drawing.Point(20, 300);
            this.lblNgayKetThucSK.Name = "lblNgayKetThucSK";
            this.lblNgayKetThucSK.Size = new System.Drawing.Size(93, 17);
            this.lblNgayKetThucSK.TabIndex = 8;
            this.lblNgayKetThucSK.Text = "Ngày Kết Thúc";
            // 
            // dtpNgayKetThucSK
            // 
            this.dtpNgayKetThucSK.Location = new System.Drawing.Point(20, 320);
            this.dtpNgayKetThucSK.Name = "dtpNgayKetThucSK";
            this.dtpNgayKetThucSK.Size = new System.Drawing.Size(300, 36);
            this.dtpNgayKetThucSK.TabIndex = 9;
            this.dtpNgayKetThucSK.DateTime = System.DateTime.Now;
            // 
            // lblTrangThaiSK
            // 
            this.lblTrangThaiSK.AutoSize = true;
            this.lblTrangThaiSK.Location = new System.Drawing.Point(20, 370);
            this.lblTrangThaiSK.Name = "lblTrangThaiSK";
            this.lblTrangThaiSK.Size = new System.Drawing.Size(69, 17);
            this.lblTrangThaiSK.TabIndex = 10;
            this.lblTrangThaiSK.Text = "Trạng Thái";
            // 
            // cboTrangThaiSK
            // 
            this.cboTrangThaiSK.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboTrangThaiSK.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTrangThaiSK.ItemHeight = 30;
            this.cboTrangThaiSK.Items.AddRange(new object[] {
            "Sắp diễn ra",
            "Đang diễn ra",
            "Đã kết thúc",
            "Đã hủy"});
            this.cboTrangThaiSK.Location = new System.Drawing.Point(20, 390);
            this.cboTrangThaiSK.Name = "cboTrangThaiSK";
            this.cboTrangThaiSK.Size = new System.Drawing.Size(300, 36);
            this.cboTrangThaiSK.TabIndex = 11;
            // 
            // btnThemSuKien
            // 
            this.btnThemSuKien.Location = new System.Drawing.Point(20, 514);
            this.btnThemSuKien.Name = "btnThemSuKien";
            this.btnThemSuKien.Size = new System.Drawing.Size(140, 45);
            this.btnThemSuKien.TabIndex = 12;
            this.btnThemSuKien.Text = "Thêm";
            // 
            // btnSuaSuKien
            // 
            this.btnSuaSuKien.Location = new System.Drawing.Point(180, 514);
            this.btnSuaSuKien.Name = "btnSuaSuKien";
            this.btnSuaSuKien.Size = new System.Drawing.Size(140, 45);
            this.btnSuaSuKien.TabIndex = 13;
            this.btnSuaSuKien.Text = "Sửa";
            // 
            // btnXoaSuKien
            // 
            this.btnXoaSuKien.Location = new System.Drawing.Point(20, 574);
            this.btnXoaSuKien.Name = "btnXoaSuKien";
            this.btnXoaSuKien.Size = new System.Drawing.Size(140, 45);
            this.btnXoaSuKien.TabIndex = 14;
            this.btnXoaSuKien.Text = "Xóa";
            // 
            // btnLamMoiSuKien
            // 
            this.btnLamMoiSuKien.Location = new System.Drawing.Point(180, 574);
            this.btnLamMoiSuKien.Name = "btnLamMoiSuKien";
            this.btnLamMoiSuKien.Size = new System.Drawing.Size(140, 45);
            this.btnLamMoiSuKien.TabIndex = 15;
            this.btnLamMoiSuKien.Text = "Làm Mới";
            // 
            // tabKhuyenMai
            // 
            this.tabKhuyenMai.Controls.Add(this.panelKhuyenMaiLeft);
            this.tabKhuyenMai.Controls.Add(this.panelKhuyenMaiRight);
            this.tabKhuyenMai.Location = new System.Drawing.Point(4, 44);
            this.tabKhuyenMai.Name = "tabKhuyenMai";
            this.tabKhuyenMai.Padding = new System.Windows.Forms.Padding(10);
            this.tabKhuyenMai.Size = new System.Drawing.Size(1192, 652);
            this.tabKhuyenMai.TabIndex = 1;
            this.tabKhuyenMai.Text = "Mã Khuyến Mãi";
            this.tabKhuyenMai.UseVisualStyleBackColor = true;
            // 
            // panelKhuyenMaiLeft
            // 
            this.panelKhuyenMaiLeft.Controls.Add(this.gridKhuyenMai);
            this.panelKhuyenMaiLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelKhuyenMaiLeft.Location = new System.Drawing.Point(10, 10);
            this.panelKhuyenMaiLeft.Name = "panelKhuyenMaiLeft";
            this.panelKhuyenMaiLeft.Padding = new System.Windows.Forms.Padding(0, 0, 10, 0);
            this.panelKhuyenMaiLeft.Size = new System.Drawing.Size(832, 632);
            this.panelKhuyenMaiLeft.TabIndex = 1;
            // 
            // gridKhuyenMai
            // 
            this.gridKhuyenMai.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridKhuyenMai.Location = new System.Drawing.Point(0, 0);
            this.gridKhuyenMai.MainView = this.gridViewKhuyenMai;
            this.gridKhuyenMai.Name = "gridKhuyenMai";
            this.gridKhuyenMai.Size = new System.Drawing.Size(822, 632);
            this.gridKhuyenMai.TabIndex = 0;
            this.gridKhuyenMai.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewKhuyenMai});
            // 
            // gridViewKhuyenMai
            // 
            this.gridViewKhuyenMai.GridControl = this.gridKhuyenMai;
            this.gridViewKhuyenMai.Name = "gridViewKhuyenMai";
            this.gridViewKhuyenMai.OptionsBehavior.Editable = false;
            this.gridViewKhuyenMai.OptionsView.ShowGroupPanel = false;
            // 
            // panelKhuyenMaiRight
            // 
            this.panelKhuyenMaiRight.Controls.Add(this.lblMaKhuyenMai);
            this.panelKhuyenMaiRight.Controls.Add(this.txtMaKhuyenMai);
            this.panelKhuyenMaiRight.Controls.Add(this.lblTenKhuyenMai);
            this.panelKhuyenMaiRight.Controls.Add(this.txtTenKhuyenMai);
            this.panelKhuyenMaiRight.Controls.Add(this.lblLoaiGiamGia);
            this.panelKhuyenMaiRight.Controls.Add(this.cboLoaiGiamGia);
            this.panelKhuyenMaiRight.Controls.Add(this.lblGiaTriGiam);
            this.panelKhuyenMaiRight.Controls.Add(this.txtGiaTriGiam);
            this.panelKhuyenMaiRight.Controls.Add(this.lblDonToiThieu);
            this.panelKhuyenMaiRight.Controls.Add(this.txtDonToiThieu);
            this.panelKhuyenMaiRight.Controls.Add(this.lblNgayBatDauKM);
            this.panelKhuyenMaiRight.Controls.Add(this.dtpNgayBatDauKM);
            this.panelKhuyenMaiRight.Controls.Add(this.lblNgayKetThucKM);
            this.panelKhuyenMaiRight.Controls.Add(this.dtpNgayKetThucKM);
            this.panelKhuyenMaiRight.Controls.Add(this.lblSuKien);
            this.panelKhuyenMaiRight.Controls.Add(this.slkSuKien);
            this.panelKhuyenMaiRight.Controls.Add(this.chkTrangThaiKM);
            this.panelKhuyenMaiRight.Controls.Add(this.btnThemKhuyenMai);
            this.panelKhuyenMaiRight.Controls.Add(this.btnSuaKhuyenMai);
            this.panelKhuyenMaiRight.Controls.Add(this.btnXoaKhuyenMai);
            this.panelKhuyenMaiRight.Controls.Add(this.btnLamMoiKhuyenMai);
            this.panelKhuyenMaiRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panelKhuyenMaiRight.Location = new System.Drawing.Point(842, 10);
            this.panelKhuyenMaiRight.Name = "panelKhuyenMaiRight";
            this.panelKhuyenMaiRight.Size = new System.Drawing.Size(340, 632);
            this.panelKhuyenMaiRight.TabIndex = 0;
            // 
            // lblMaKhuyenMai
            // 
            this.lblMaKhuyenMai.AutoSize = true;
            this.lblMaKhuyenMai.Location = new System.Drawing.Point(20, 20);
            this.lblMaKhuyenMai.Name = "lblMaKhuyenMai";
            this.lblMaKhuyenMai.Size = new System.Drawing.Size(51, 17);
            this.lblMaKhuyenMai.TabIndex = 0;
            this.lblMaKhuyenMai.Text = "Mã KM";
            // 
            // txtMaKhuyenMai
            // 
            this.txtMaKhuyenMai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaKhuyenMai.DefaultText = "";
            this.txtMaKhuyenMai.Location = new System.Drawing.Point(20, 40);
            this.txtMaKhuyenMai.Name = "txtMaKhuyenMai";
            this.txtMaKhuyenMai.PlaceholderText = "";
            this.txtMaKhuyenMai.ReadOnly = true;
            this.txtMaKhuyenMai.SelectedText = "";
            this.txtMaKhuyenMai.Size = new System.Drawing.Size(300, 36);
            this.txtMaKhuyenMai.TabIndex = 1;
            // 
            // lblTenKhuyenMai
            // 
            this.lblTenKhuyenMai.AutoSize = true;
            this.lblTenKhuyenMai.Location = new System.Drawing.Point(20, 80);
            this.lblTenKhuyenMai.Name = "lblTenKhuyenMai";
            this.lblTenKhuyenMai.Size = new System.Drawing.Size(185, 17);
            this.lblTenKhuyenMai.TabIndex = 2;
            this.lblTenKhuyenMai.Text = "Tên Khuyến Mãi (VD: TET2026)";
            // 
            // txtTenKhuyenMai
            // 
            this.txtTenKhuyenMai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenKhuyenMai.DefaultText = "";
            this.txtTenKhuyenMai.Location = new System.Drawing.Point(20, 100);
            this.txtTenKhuyenMai.Name = "txtTenKhuyenMai";
            this.txtTenKhuyenMai.PlaceholderText = "";
            this.txtTenKhuyenMai.SelectedText = "";
            this.txtTenKhuyenMai.Size = new System.Drawing.Size(300, 36);
            this.txtTenKhuyenMai.TabIndex = 3;
            // 
            // lblLoaiGiamGia
            // 
            this.lblLoaiGiamGia.AutoSize = true;
            this.lblLoaiGiamGia.Location = new System.Drawing.Point(20, 140);
            this.lblLoaiGiamGia.Name = "lblLoaiGiamGia";
            this.lblLoaiGiamGia.Size = new System.Drawing.Size(87, 17);
            this.lblLoaiGiamGia.TabIndex = 4;
            this.lblLoaiGiamGia.Text = "Loại giảm giá";
            // 
            // cboLoaiGiamGia
            // 
            this.cboLoaiGiamGia.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoaiGiamGia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoaiGiamGia.ItemHeight = 30;
            this.cboLoaiGiamGia.Items.AddRange(new object[] {
            "PhanTram",
            "SoTien"});
            this.cboLoaiGiamGia.Location = new System.Drawing.Point(20, 160);
            this.cboLoaiGiamGia.Name = "cboLoaiGiamGia";
            this.cboLoaiGiamGia.Size = new System.Drawing.Size(300, 36);
            this.cboLoaiGiamGia.TabIndex = 5;
            // 
            // lblGiaTriGiam
            // 
            this.lblGiaTriGiam.AutoSize = true;
            this.lblGiaTriGiam.Location = new System.Drawing.Point(20, 200);
            this.lblGiaTriGiam.Name = "lblGiaTriGiam";
            this.lblGiaTriGiam.Size = new System.Drawing.Size(108, 17);
            this.lblGiaTriGiam.TabIndex = 6;
            this.lblGiaTriGiam.Text = "Giá trị giảm (> 0)";
            // 
            // txtGiaTriGiam
            // 
            this.txtGiaTriGiam.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGiaTriGiam.DefaultText = "";
            this.txtGiaTriGiam.Location = new System.Drawing.Point(20, 220);
            this.txtGiaTriGiam.Name = "txtGiaTriGiam";
            this.txtGiaTriGiam.PlaceholderText = "";
            this.txtGiaTriGiam.SelectedText = "";
            this.txtGiaTriGiam.Size = new System.Drawing.Size(300, 36);
            this.txtGiaTriGiam.TabIndex = 7;
            // 
            // lblDonToiThieu
            // 
            this.lblDonToiThieu.AutoSize = true;
            this.lblDonToiThieu.Location = new System.Drawing.Point(20, 260);
            this.lblDonToiThieu.Name = "lblDonToiThieu";
            this.lblDonToiThieu.Size = new System.Drawing.Size(122, 17);
            this.lblDonToiThieu.TabIndex = 8;
            this.lblDonToiThieu.Text = "Đơn tối thiểu (VNĐ)";
            // 
            // txtDonToiThieu
            // 
            this.txtDonToiThieu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDonToiThieu.DefaultText = "";
            this.txtDonToiThieu.Location = new System.Drawing.Point(20, 280);
            this.txtDonToiThieu.Name = "txtDonToiThieu";
            this.txtDonToiThieu.PlaceholderText = "";
            this.txtDonToiThieu.SelectedText = "";
            this.txtDonToiThieu.Size = new System.Drawing.Size(300, 36);
            this.txtDonToiThieu.TabIndex = 9;
            // 
            // lblNgayBatDauKM
            // 
            this.lblNgayBatDauKM.AutoSize = true;
            this.lblNgayBatDauKM.Location = new System.Drawing.Point(20, 320);
            this.lblNgayBatDauKM.Name = "lblNgayBatDauKM";
            this.lblNgayBatDauKM.Size = new System.Drawing.Size(76, 17);
            this.lblNgayBatDauKM.TabIndex = 10;
            this.lblNgayBatDauKM.Text = "Ngày b.đầu";
            // 
            // dtpNgayBatDauKM
            // 
            this.dtpNgayBatDauKM.Location = new System.Drawing.Point(20, 340);
            this.dtpNgayBatDauKM.Name = "dtpNgayBatDauKM";
            this.dtpNgayBatDauKM.Size = new System.Drawing.Size(140, 36);
            this.dtpNgayBatDauKM.TabIndex = 11;
            this.dtpNgayBatDauKM.DateTime = System.DateTime.Now;
            // 
            // lblNgayKetThucKM
            // 
            this.lblNgayKetThucKM.AutoSize = true;
            this.lblNgayKetThucKM.Location = new System.Drawing.Point(180, 320);
            this.lblNgayKetThucKM.Name = "lblNgayKetThucKM";
            this.lblNgayKetThucKM.Size = new System.Drawing.Size(76, 17);
            this.lblNgayKetThucKM.TabIndex = 12;
            this.lblNgayKetThucKM.Text = "Ngày k.thúc";
            // 
            // dtpNgayKetThucKM
            // 
            this.dtpNgayKetThucKM.Location = new System.Drawing.Point(180, 340);
            this.dtpNgayKetThucKM.Name = "dtpNgayKetThucKM";
            this.dtpNgayKetThucKM.Size = new System.Drawing.Size(140, 36);
            this.dtpNgayKetThucKM.TabIndex = 13;
            this.dtpNgayKetThucKM.DateTime = System.DateTime.Now;
            // 
            // lblSuKien
            // 
            this.lblSuKien.AutoSize = true;
            this.lblSuKien.Location = new System.Drawing.Point(20, 380);
            this.lblSuKien.Name = "lblSuKien";
            this.lblSuKien.Size = new System.Drawing.Size(167, 17);
            this.lblSuKien.TabIndex = 14;
            this.lblSuKien.Text = "Sự kiện áp dụng (Tùy chọn)";
            // 
            // slkSuKien
            // 
            this.slkSuKien.Location = new System.Drawing.Point(20, 400);
            this.slkSuKien.Name = "slkSuKien";
            this.slkSuKien.Properties.Appearance.Options.UseFont = true;
            this.slkSuKien.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slkSuKien.Properties.NullText = "[Không áp dụng]";
            this.slkSuKien.Properties.PopupView = this.searchLookUpEdit1View;
            this.slkSuKien.Size = new System.Drawing.Size(300, 26);
            this.slkSuKien.TabIndex = 15;
            // 
            // searchLookUpEdit1View
            // 
            this.searchLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.searchLookUpEdit1View.Name = "searchLookUpEdit1View";
            this.searchLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.searchLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // chkTrangThaiKM
            // 
            this.chkTrangThaiKM.AutoSize = true;
            this.chkTrangThaiKM.Location = new System.Drawing.Point(20, 440);
            this.chkTrangThaiKM.Name = "chkTrangThaiKM";
            this.chkTrangThaiKM.Size = new System.Drawing.Size(123, 21);
            this.chkTrangThaiKM.TabIndex = 16;
            this.chkTrangThaiKM.Text = "Đang hoạt động";
            // 
            // btnThemKhuyenMai
            // 
            this.btnThemKhuyenMai.Location = new System.Drawing.Point(20, 480);
            this.btnThemKhuyenMai.Name = "btnThemKhuyenMai";
            this.btnThemKhuyenMai.Size = new System.Drawing.Size(140, 45);
            this.btnThemKhuyenMai.TabIndex = 17;
            this.btnThemKhuyenMai.Text = "Thêm";
            // 
            // btnSuaKhuyenMai
            // 
            this.btnSuaKhuyenMai.Location = new System.Drawing.Point(180, 480);
            this.btnSuaKhuyenMai.Name = "btnSuaKhuyenMai";
            this.btnSuaKhuyenMai.Size = new System.Drawing.Size(140, 45);
            this.btnSuaKhuyenMai.TabIndex = 18;
            this.btnSuaKhuyenMai.Text = "Sửa";
            // 
            // btnXoaKhuyenMai
            // 
            this.btnXoaKhuyenMai.Location = new System.Drawing.Point(20, 535);
            this.btnXoaKhuyenMai.Name = "btnXoaKhuyenMai";
            this.btnXoaKhuyenMai.Size = new System.Drawing.Size(140, 45);
            this.btnXoaKhuyenMai.TabIndex = 19;
            this.btnXoaKhuyenMai.Text = "Xóa";
            // 
            // btnLamMoiKhuyenMai
            // 
            this.btnLamMoiKhuyenMai.Location = new System.Drawing.Point(180, 535);
            this.btnLamMoiKhuyenMai.Name = "btnLamMoiKhuyenMai";
            this.btnLamMoiKhuyenMai.Size = new System.Drawing.Size(140, 45);
            this.btnLamMoiKhuyenMai.TabIndex = 20;
            this.btnLamMoiKhuyenMai.Text = "Làm Mới";
            // 
            // frmMarketing
            // 
            this.tabKhuyenMai.ResumeLayout(false);
            this.panelKhuyenMaiLeft.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridKhuyenMai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewKhuyenMai)).EndInit();
            this.panelKhuyenMaiRight.ResumeLayout(false);
            this.panelKhuyenMaiRight.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slkSuKien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.searchLookUpEdit1View)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2TabControl tabControlMain;
        
        // Tab SuKien
        private System.Windows.Forms.TabPage tabSuKien;
        private Guna.UI2.WinForms.Guna2Panel panelSuKienLeft;
        private Guna.UI2.WinForms.Guna2Panel panelSuKienRight;
        private DevExpress.XtraGrid.GridControl gridSuKien;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSuKien;

        private System.Windows.Forms.Label lblMaSuKien;
        private Guna.UI2.WinForms.Guna2TextBox txtMaSuKien;
        private System.Windows.Forms.Label lblTenSuKien;
        private Guna.UI2.WinForms.Guna2TextBox txtTenSuKien;
        private System.Windows.Forms.Label lblMoTa;
        private Guna.UI2.WinForms.Guna2TextBox txtMoTa;
        private System.Windows.Forms.Label lblNgayBatDauSK;
        private DevExpress.XtraEditors.DateEdit dtpNgayBatDauSK;
        private System.Windows.Forms.Label lblNgayKetThucSK;
        private DevExpress.XtraEditors.DateEdit dtpNgayKetThucSK;
        private System.Windows.Forms.Label lblTrangThaiSK;
        private Guna.UI2.WinForms.Guna2ComboBox cboTrangThaiSK;
        
        private Guna.UI2.WinForms.Guna2Button btnThemSuKien;
        private Guna.UI2.WinForms.Guna2Button btnSuaSuKien;
        private Guna.UI2.WinForms.Guna2Button btnXoaSuKien;
        private Guna.UI2.WinForms.Guna2Button btnLamMoiSuKien;

        // Tab KhuyenMai
        private System.Windows.Forms.TabPage tabKhuyenMai;
        private Guna.UI2.WinForms.Guna2Panel panelKhuyenMaiLeft;
        private Guna.UI2.WinForms.Guna2Panel panelKhuyenMaiRight;
        private DevExpress.XtraGrid.GridControl gridKhuyenMai;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewKhuyenMai;

        private System.Windows.Forms.Label lblMaKhuyenMai;
        private Guna.UI2.WinForms.Guna2TextBox txtMaKhuyenMai;
        private System.Windows.Forms.Label lblTenKhuyenMai;
        private Guna.UI2.WinForms.Guna2TextBox txtTenKhuyenMai;
        private System.Windows.Forms.Label lblLoaiGiamGia;
        private Guna.UI2.WinForms.Guna2ComboBox cboLoaiGiamGia;
        private System.Windows.Forms.Label lblGiaTriGiam;
        private Guna.UI2.WinForms.Guna2TextBox txtGiaTriGiam;
        private System.Windows.Forms.Label lblDonToiThieu;
        private Guna.UI2.WinForms.Guna2TextBox txtDonToiThieu;
        private System.Windows.Forms.Label lblNgayBatDauKM;
        private DevExpress.XtraEditors.DateEdit dtpNgayBatDauKM;
        private System.Windows.Forms.Label lblNgayKetThucKM;
        private DevExpress.XtraEditors.DateEdit dtpNgayKetThucKM;
        private System.Windows.Forms.Label lblSuKien;
        private DevExpress.XtraEditors.SearchLookUpEdit slkSuKien;
        private DevExpress.XtraGrid.Views.Grid.GridView searchLookUpEdit1View;
        private System.Windows.Forms.CheckBox chkTrangThaiKM;

        private Guna.UI2.WinForms.Guna2Button btnThemKhuyenMai;
        private Guna.UI2.WinForms.Guna2Button btnSuaKhuyenMai;
        private Guna.UI2.WinForms.Guna2Button btnXoaKhuyenMai;
        private Guna.UI2.WinForms.Guna2Button btnLamMoiKhuyenMai;
    }
}

