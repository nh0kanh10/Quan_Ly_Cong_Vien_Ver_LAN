namespace GUI
{
    partial class frmDatPhong
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
            this.components = new System.ComponentModel.Container();
            this.pnlGroupMode = new Guna.UI2.WinForms.Guna2Panel();
            this.lblGroupModeHint = new System.Windows.Forms.Label();
            this.tgGroupMode = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            this.pnlBottomAction = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSelectedCount = new System.Windows.Forms.Label();
            this.lblTempTotal = new System.Windows.Forms.Label();
            this.btnCancelSelection = new Guna.UI2.WinForms.Guna2Button();
            this.btnReserveGroup = new Guna.UI2.WinForms.Guna2Button();
            this.btnCheckInGroup = new Guna.UI2.WinForms.Guna2Button();
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblStats = new System.Windows.Forms.Label();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.pnlAreaFilters = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlStatusFilters = new System.Windows.Forms.FlowLayoutPanel();
            this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnlFloorMap = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlSidebar = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSidebarHint = new System.Windows.Forms.Label();
            this.lblSidebarTitle = new System.Windows.Forms.Label();
            this.lblSidebarLoaiPhong = new System.Windows.Forms.Label();
            this.lblSidebarKhuVuc = new System.Windows.Forms.Label();
            this.lblSidebarTrangThai = new System.Windows.Forms.Label();
            this.lblSidebarKhach = new System.Windows.Forms.Label();
            this.lblSidebarSdt = new System.Windows.Forms.Label();
            this.lblSidebarGioVao = new System.Windows.Forms.Label();
            this.lblSidebarGioRa = new System.Windows.Forms.Label();
            this.lblSidebarSucChua = new System.Windows.Forms.Label();
            this.lblSidebarDonGia = new System.Windows.Forms.Label();
            this.lblSidebarTongTien = new System.Windows.Forms.Label();
            this.lblSidebarDivider = new System.Windows.Forms.Label();
            this.pnlSidebarActions = new System.Windows.Forms.FlowLayoutPanel();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.pnlGroupMode.SuspendLayout();
            this.pnlBottomAction.SuspendLayout();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).BeginInit();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.pnlSidebar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlGroupMode
            // 
            this.pnlGroupMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlGroupMode.BackColor = System.Drawing.Color.Transparent;
            this.pnlGroupMode.Controls.Add(this.lblGroupModeHint);
            this.pnlGroupMode.Controls.Add(this.tgGroupMode);
            this.pnlGroupMode.Location = new System.Drawing.Point(946, 17);
            this.pnlGroupMode.Name = "pnlGroupMode";
            this.pnlGroupMode.Size = new System.Drawing.Size(270, 40);
            this.pnlGroupMode.TabIndex = 4;
            // 
            // lblGroupModeHint
            // 
            this.lblGroupModeHint.AutoSize = true;
            this.lblGroupModeHint.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGroupModeHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblGroupModeHint.Location = new System.Drawing.Point(60, 10);
            this.lblGroupModeHint.Name = "lblGroupModeHint";
            this.lblGroupModeHint.Size = new System.Drawing.Size(171, 19);
            this.lblGroupModeHint.TabIndex = 1;
            this.lblGroupModeHint.Text = "[ CHẾ ĐỘ CHỌN ĐOÀN ]";
            // 
            // tgGroupMode
            // 
            this.tgGroupMode.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.tgGroupMode.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.tgGroupMode.CheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.tgGroupMode.CheckedState.InnerColor = System.Drawing.Color.White;
            this.tgGroupMode.Location = new System.Drawing.Point(10, 8);
            this.tgGroupMode.Name = "tgGroupMode";
            this.tgGroupMode.Size = new System.Drawing.Size(40, 22);
            this.tgGroupMode.TabIndex = 0;
            this.tgGroupMode.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.tgGroupMode.UncheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.tgGroupMode.UncheckedState.InnerBorderColor = System.Drawing.Color.White;
            this.tgGroupMode.UncheckedState.InnerColor = System.Drawing.Color.White;
            this.tgGroupMode.CheckedChanged += new System.EventHandler(this.tgGroupMode_CheckedChanged);
            // 
            // pnlBottomAction
            // 
            this.pnlBottomAction.Controls.Add(this.lblSelectedCount);
            this.pnlBottomAction.Controls.Add(this.lblTempTotal);
            this.pnlBottomAction.Controls.Add(this.btnCancelSelection);
            this.pnlBottomAction.Controls.Add(this.btnReserveGroup);
            this.pnlBottomAction.Controls.Add(this.btnCheckInGroup);
            this.pnlBottomAction.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottomAction.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlBottomAction.Location = new System.Drawing.Point(0, 530);
            this.pnlBottomAction.Name = "pnlBottomAction";
            this.pnlBottomAction.Size = new System.Drawing.Size(940, 60);
            this.pnlBottomAction.TabIndex = 2;
            this.pnlBottomAction.Visible = false;
            // 
            // lblSelectedCount
            // 
            this.lblSelectedCount.AutoSize = true;
            this.lblSelectedCount.BackColor = System.Drawing.Color.Transparent;
            this.lblSelectedCount.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSelectedCount.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSelectedCount.Location = new System.Drawing.Point(15, 20);
            this.lblSelectedCount.Name = "lblSelectedCount";
            this.lblSelectedCount.Size = new System.Drawing.Size(206, 20);
            this.lblSelectedCount.TabIndex = 0;
            this.lblSelectedCount.Text = "ĐÃ CHỌN 0 PHÒNG TRỐNG";
            // 
            // lblTempTotal
            // 
            this.lblTempTotal.AutoSize = true;
            this.lblTempTotal.BackColor = System.Drawing.Color.Transparent;
            this.lblTempTotal.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTempTotal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTempTotal.Location = new System.Drawing.Point(350, 20);
            this.lblTempTotal.Name = "lblTempTotal";
            this.lblTempTotal.Size = new System.Drawing.Size(178, 20);
            this.lblTempTotal.TabIndex = 1;
            this.lblTempTotal.Text = "|  Tổng tiền tạm: 0 đ/đêm";
            // 
            // btnCancelSelection
            // 
            this.btnCancelSelection.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancelSelection.BackColor = System.Drawing.Color.WhiteSmoke;
            this.btnCancelSelection.BorderColor = System.Drawing.Color.Transparent;
            this.btnCancelSelection.BorderRadius = 4;
            this.btnCancelSelection.BorderThickness = 1;
            this.btnCancelSelection.FillColor = System.Drawing.Color.Transparent;
            this.btnCancelSelection.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancelSelection.ForeColor = System.Drawing.Color.White;
            this.btnCancelSelection.Location = new System.Drawing.Point(479, 12);
            this.btnCancelSelection.Name = "btnCancelSelection";
            this.btnCancelSelection.Size = new System.Drawing.Size(157, 36);
            this.btnCancelSelection.TabIndex = 2;
            this.btnCancelSelection.Text = "X  BỎ CHỌN (ESC)";
            this.btnCancelSelection.Click += new System.EventHandler(this.btnCancelSelection_Click);
            // 
            // btnReserveGroup
            // 
            this.btnReserveGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReserveGroup.BackColor = System.Drawing.Color.Transparent;
            this.btnReserveGroup.BorderRadius = 4;
            this.btnReserveGroup.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(158)))), ((int)(((byte)(11)))));
            this.btnReserveGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnReserveGroup.ForeColor = System.Drawing.Color.White;
            this.btnReserveGroup.Location = new System.Drawing.Point(645, 12);
            this.btnReserveGroup.Name = "btnReserveGroup";
            this.btnReserveGroup.Size = new System.Drawing.Size(134, 36);
            this.btnReserveGroup.TabIndex = 3;
            this.btnReserveGroup.Text = "ĐẶT TRƯỚC";
            this.btnReserveGroup.Click += new System.EventHandler(this.btnReserveGroup_Click);
            // 
            // btnCheckInGroup
            // 
            this.btnCheckInGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCheckInGroup.BackColor = System.Drawing.Color.Transparent;
            this.btnCheckInGroup.BorderRadius = 4;
            this.btnCheckInGroup.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnCheckInGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCheckInGroup.ForeColor = System.Drawing.Color.White;
            this.btnCheckInGroup.Location = new System.Drawing.Point(785, 12);
            this.btnCheckInGroup.Name = "btnCheckInGroup";
            this.btnCheckInGroup.Size = new System.Drawing.Size(144, 36);
            this.btnCheckInGroup.TabIndex = 4;
            this.btnCheckInGroup.Text = "NHẬN PHÒNG";
            this.btnCheckInGroup.Click += new System.EventHandler(this.btnCheckInGroup_Click);
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.pnlGroupMode);
            this.pnlTop.Controls.Add(this.lblStats);
            this.pnlTop.Controls.Add(this.txtTimKiem);
            this.pnlTop.Controls.Add(this.pnlAreaFilters);
            this.pnlTop.Controls.Add(this.pnlStatusFilters);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1200, 110);
            this.pnlTop.TabIndex = 0;
            // 
            // lblStats
            // 
            this.lblStats.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblStats.BackColor = System.Drawing.Color.Transparent;
            this.lblStats.Font = new System.Drawing.Font("Segoe UI Semibold", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStats.Location = new System.Drawing.Point(814, 61);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(383, 35);
            this.lblStats.TabIndex = 3;
            this.lblStats.Text = "Công suất: 0%";
            this.lblStats.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.BorderRadius = 4;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTimKiem.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTimKiem.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTimKiem.ForeColor = System.Drawing.Color.Black;
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Location = new System.Drawing.Point(20, 12);
            this.txtTimKiem.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "🔍  Tìm phòng, tên khách, SĐT...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(320, 38);
            this.txtTimKiem.TabIndex = 0;
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            // 
            // pnlAreaFilters
            // 
            this.pnlAreaFilters.AutoSize = true;
            this.pnlAreaFilters.Location = new System.Drawing.Point(360, 12);
            this.pnlAreaFilters.Name = "pnlAreaFilters";
            this.pnlAreaFilters.Size = new System.Drawing.Size(580, 46);
            this.pnlAreaFilters.TabIndex = 1;
            this.pnlAreaFilters.WrapContents = false;
            // 
            // pnlStatusFilters
            // 
            this.pnlStatusFilters.AutoSize = true;
            this.pnlStatusFilters.Location = new System.Drawing.Point(20, 60);
            this.pnlStatusFilters.Name = "pnlStatusFilters";
            this.pnlStatusFilters.Size = new System.Drawing.Size(742, 38);
            this.pnlStatusFilters.TabIndex = 2;
            this.pnlStatusFilters.WrapContents = false;
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitMain.Location = new System.Drawing.Point(0, 110);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.pnlFloorMap);
            this.splitMain.Panel1.Controls.Add(this.pnlBottomAction);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlSidebar);
            this.splitMain.Size = new System.Drawing.Size(1200, 590);
            this.splitMain.SplitterPosition = 250;
            this.splitMain.TabIndex = 1;
            // 
            // pnlFloorMap
            // 
            this.pnlFloorMap.AutoScroll = true;
            this.pnlFloorMap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlFloorMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFloorMap.Location = new System.Drawing.Point(0, 0);
            this.pnlFloorMap.Name = "pnlFloorMap";
            this.pnlFloorMap.Padding = new System.Windows.Forms.Padding(15);
            this.pnlFloorMap.Size = new System.Drawing.Size(940, 530);
            this.pnlFloorMap.TabIndex = 0;
            // 
            // pnlSidebar
            // 
            this.pnlSidebar.BackColor = System.Drawing.Color.White;
            this.pnlSidebar.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlSidebar.BorderThickness = 1;
            this.pnlSidebar.Controls.Add(this.lblSidebarHint);
            this.pnlSidebar.Controls.Add(this.lblSidebarTitle);
            this.pnlSidebar.Controls.Add(this.lblSidebarLoaiPhong);
            this.pnlSidebar.Controls.Add(this.lblSidebarKhuVuc);
            this.pnlSidebar.Controls.Add(this.lblSidebarTrangThai);
            this.pnlSidebar.Controls.Add(this.lblSidebarKhach);
            this.pnlSidebar.Controls.Add(this.lblSidebarSdt);
            this.pnlSidebar.Controls.Add(this.lblSidebarGioVao);
            this.pnlSidebar.Controls.Add(this.lblSidebarGioRa);
            this.pnlSidebar.Controls.Add(this.lblSidebarSucChua);
            this.pnlSidebar.Controls.Add(this.lblSidebarDonGia);
            this.pnlSidebar.Controls.Add(this.lblSidebarTongTien);
            this.pnlSidebar.Controls.Add(this.lblSidebarDivider);
            this.pnlSidebar.Controls.Add(this.pnlSidebarActions);
            this.pnlSidebar.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlSidebar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlSidebar.Location = new System.Drawing.Point(0, 0);
            this.pnlSidebar.Name = "pnlSidebar";
            this.pnlSidebar.Padding = new System.Windows.Forms.Padding(16);
            this.pnlSidebar.Size = new System.Drawing.Size(250, 590);
            this.pnlSidebar.TabIndex = 0;
            // 
            // lblSidebarHint
            // 
            this.lblSidebarHint.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblSidebarHint.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblSidebarHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSidebarHint.Location = new System.Drawing.Point(16, 16);
            this.lblSidebarHint.Name = "lblSidebarHint";
            this.lblSidebarHint.Size = new System.Drawing.Size(218, 558);
            this.lblSidebarHint.TabIndex = 0;
            this.lblSidebarHint.Text = "← Click vào một phòng để xem chi tiết";
            this.lblSidebarHint.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblSidebarTitle
            // 
            this.lblSidebarTitle.AutoSize = true;
            this.lblSidebarTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSidebarTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSidebarTitle.Location = new System.Drawing.Point(16, 20);
            this.lblSidebarTitle.Name = "lblSidebarTitle";
            this.lblSidebarTitle.Size = new System.Drawing.Size(0, 25);
            this.lblSidebarTitle.TabIndex = 1;
            this.lblSidebarTitle.Visible = false;
            // 
            // lblSidebarLoaiPhong
            // 
            this.lblSidebarLoaiPhong.AutoSize = true;
            this.lblSidebarLoaiPhong.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSidebarLoaiPhong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSidebarLoaiPhong.Location = new System.Drawing.Point(16, 52);
            this.lblSidebarLoaiPhong.Name = "lblSidebarLoaiPhong";
            this.lblSidebarLoaiPhong.Size = new System.Drawing.Size(0, 19);
            this.lblSidebarLoaiPhong.TabIndex = 2;
            this.lblSidebarLoaiPhong.Visible = false;
            // 
            // lblSidebarKhuVuc
            // 
            this.lblSidebarKhuVuc.AutoSize = true;
            this.lblSidebarKhuVuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSidebarKhuVuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSidebarKhuVuc.Location = new System.Drawing.Point(16, 74);
            this.lblSidebarKhuVuc.Name = "lblSidebarKhuVuc";
            this.lblSidebarKhuVuc.Size = new System.Drawing.Size(0, 19);
            this.lblSidebarKhuVuc.TabIndex = 3;
            this.lblSidebarKhuVuc.Visible = false;
            // 
            // lblSidebarTrangThai
            // 
            this.lblSidebarTrangThai.AutoSize = true;
            this.lblSidebarTrangThai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblSidebarTrangThai.Location = new System.Drawing.Point(16, 104);
            this.lblSidebarTrangThai.Name = "lblSidebarTrangThai";
            this.lblSidebarTrangThai.Size = new System.Drawing.Size(0, 19);
            this.lblSidebarTrangThai.TabIndex = 4;
            this.lblSidebarTrangThai.Visible = false;
            // 
            // lblSidebarKhach
            // 
            this.lblSidebarKhach.AutoSize = true;
            this.lblSidebarKhach.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSidebarKhach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSidebarKhach.Location = new System.Drawing.Point(16, 140);
            this.lblSidebarKhach.Name = "lblSidebarKhach";
            this.lblSidebarKhach.Size = new System.Drawing.Size(0, 19);
            this.lblSidebarKhach.TabIndex = 5;
            this.lblSidebarKhach.Visible = false;
            // 
            // lblSidebarSdt
            // 
            this.lblSidebarSdt.AutoSize = true;
            this.lblSidebarSdt.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSidebarSdt.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSidebarSdt.Location = new System.Drawing.Point(16, 162);
            this.lblSidebarSdt.Name = "lblSidebarSdt";
            this.lblSidebarSdt.Size = new System.Drawing.Size(0, 19);
            this.lblSidebarSdt.TabIndex = 6;
            this.lblSidebarSdt.Visible = false;
            // 
            // lblSidebarGioVao
            // 
            this.lblSidebarGioVao.AutoSize = true;
            this.lblSidebarGioVao.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSidebarGioVao.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSidebarGioVao.Location = new System.Drawing.Point(16, 192);
            this.lblSidebarGioVao.Name = "lblSidebarGioVao";
            this.lblSidebarGioVao.Size = new System.Drawing.Size(0, 19);
            this.lblSidebarGioVao.TabIndex = 7;
            this.lblSidebarGioVao.Visible = false;
            // 
            // lblSidebarGioRa
            // 
            this.lblSidebarGioRa.AutoSize = true;
            this.lblSidebarGioRa.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSidebarGioRa.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSidebarGioRa.Location = new System.Drawing.Point(16, 214);
            this.lblSidebarGioRa.Name = "lblSidebarGioRa";
            this.lblSidebarGioRa.Size = new System.Drawing.Size(0, 19);
            this.lblSidebarGioRa.TabIndex = 8;
            this.lblSidebarGioRa.Visible = false;
            // 
            // lblSidebarSucChua
            // 
            this.lblSidebarSucChua.AutoSize = true;
            this.lblSidebarSucChua.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSidebarSucChua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSidebarSucChua.Location = new System.Drawing.Point(16, 244);
            this.lblSidebarSucChua.Name = "lblSidebarSucChua";
            this.lblSidebarSucChua.Size = new System.Drawing.Size(0, 19);
            this.lblSidebarSucChua.TabIndex = 9;
            this.lblSidebarSucChua.Visible = false;
            // 
            // lblSidebarDonGia
            // 
            this.lblSidebarDonGia.AutoSize = true;
            this.lblSidebarDonGia.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSidebarDonGia.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblSidebarDonGia.Location = new System.Drawing.Point(16, 266);
            this.lblSidebarDonGia.Name = "lblSidebarDonGia";
            this.lblSidebarDonGia.Size = new System.Drawing.Size(0, 19);
            this.lblSidebarDonGia.TabIndex = 10;
            this.lblSidebarDonGia.Visible = false;
            // 
            // lblSidebarTongTien
            // 
            this.lblSidebarTongTien.AutoSize = true;
            this.lblSidebarTongTien.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblSidebarTongTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.lblSidebarTongTien.Location = new System.Drawing.Point(16, 296);
            this.lblSidebarTongTien.Name = "lblSidebarTongTien";
            this.lblSidebarTongTien.Size = new System.Drawing.Size(0, 20);
            this.lblSidebarTongTien.TabIndex = 11;
            this.lblSidebarTongTien.Visible = false;
            // 
            // lblSidebarDivider
            // 
            this.lblSidebarDivider.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.lblSidebarDivider.Location = new System.Drawing.Point(16, 128);
            this.lblSidebarDivider.Name = "lblSidebarDivider";
            this.lblSidebarDivider.Size = new System.Drawing.Size(218, 2);
            this.lblSidebarDivider.TabIndex = 12;
            this.lblSidebarDivider.Visible = false;
            // 
            // pnlSidebarActions
            // 
            this.pnlSidebarActions.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlSidebarActions.Location = new System.Drawing.Point(16, 330);
            this.pnlSidebarActions.Name = "pnlSidebarActions";
            this.pnlSidebarActions.Size = new System.Drawing.Size(218, 240);
            this.pnlSidebarActions.TabIndex = 13;
            this.pnlSidebarActions.Visible = false;
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // frmDatPhong
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDatPhong";
            this.Text = "Sơ đồ Phòng";
            this.pnlGroupMode.ResumeLayout(false);
            this.pnlGroupMode.PerformLayout();
            this.pnlBottomAction.ResumeLayout(false);
            this.pnlBottomAction.PerformLayout();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).EndInit();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.pnlSidebar.ResumeLayout(false);
            this.pnlSidebar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        private System.Windows.Forms.FlowLayoutPanel pnlAreaFilters;
        private System.Windows.Forms.FlowLayoutPanel pnlStatusFilters;
        private System.Windows.Forms.Label lblStats;
        private DevExpress.XtraEditors.SplitContainerControl splitMain;
        private System.Windows.Forms.FlowLayoutPanel pnlFloorMap;
        private Guna.UI2.WinForms.Guna2Panel pnlSidebar;
        private System.Windows.Forms.Label lblSidebarHint;
        private System.Windows.Forms.Label lblSidebarTitle;
        private System.Windows.Forms.Label lblSidebarLoaiPhong;
        private System.Windows.Forms.Label lblSidebarKhuVuc;
        private System.Windows.Forms.Label lblSidebarTrangThai;
        private System.Windows.Forms.Label lblSidebarKhach;
        private System.Windows.Forms.Label lblSidebarSdt;
        private System.Windows.Forms.Label lblSidebarGioVao;
        private System.Windows.Forms.Label lblSidebarGioRa;
        private System.Windows.Forms.Label lblSidebarSucChua;
        private System.Windows.Forms.Label lblSidebarDonGia;
        private System.Windows.Forms.Label lblSidebarTongTien;
        private System.Windows.Forms.Label lblSidebarDivider;
        private System.Windows.Forms.FlowLayoutPanel pnlSidebarActions;
        private Guna.UI2.WinForms.Guna2Panel pnlGroupMode;
        private Guna.UI2.WinForms.Guna2ToggleSwitch tgGroupMode;
        private System.Windows.Forms.Label lblGroupModeHint;
        private Guna.UI2.WinForms.Guna2Panel pnlBottomAction;
        private System.Windows.Forms.Label lblSelectedCount;
        private System.Windows.Forms.Label lblTempTotal;
        private Guna.UI2.WinForms.Guna2Button btnCancelSelection;
        private Guna.UI2.WinForms.Guna2Button btnReserveGroup;
        private Guna.UI2.WinForms.Guna2Button btnCheckInGroup;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
    }
}
