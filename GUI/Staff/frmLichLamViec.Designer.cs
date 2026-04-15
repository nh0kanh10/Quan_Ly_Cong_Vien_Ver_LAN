namespace GUI
{
    partial class frmLichLamViec
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
            this.pnlTopBar = new Guna.UI2.WinForms.Guna2Panel();
            this.btnTuanTruoc = new Guna.UI2.WinForms.Guna2Button();
            this.lblTuanHienTai = new System.Windows.Forms.Label();
            this.btnTuanSau = new Guna.UI2.WinForms.Guna2Button();
            this.lblCaLam = new System.Windows.Forms.Label();
            this.cboCaLam = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblCheDo = new System.Windows.Forms.Label();
            this.cboCheDo = new Guna.UI2.WinForms.Guna2ComboBox();
            this.btnCopyTuan = new Guna.UI2.WinForms.Guna2Button();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.gbLichTuan = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridLichTuan = new DevExpress.XtraGrid.GridControl();
            this.gridViewLichTuan = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gbNVChuaPhan = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lstNVChuaPhan = new DevExpress.XtraEditors.ListBoxControl();
            this.pnlButtons = new System.Windows.Forms.Panel();
            this.btnPhanCong = new Guna.UI2.WinForms.Guna2Button();
            this.btnGoBo = new Guna.UI2.WinForms.Guna2Button();
            this.gbNVDaPhan = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lstNVDaPhan = new DevExpress.XtraEditors.ListBoxControl();
            this.gbPhanCong = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblChonKhuVuc = new System.Windows.Forms.Label();
            this.cboKhuVuc = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblChonNgay = new System.Windows.Forms.Label();
            this.cboNgayTrongTuan = new Guna.UI2.WinForms.Guna2ComboBox();
            this.pnlTopBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.gbLichTuan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLichTuan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLichTuan)).BeginInit();
            this.gbNVChuaPhan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstNVChuaPhan)).BeginInit();
            this.pnlButtons.SuspendLayout();
            this.gbNVDaPhan.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lstNVDaPhan)).BeginInit();
            this.gbPhanCong.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTopBar
            // 
            this.pnlTopBar.Controls.Add(this.btnTuanTruoc);
            this.pnlTopBar.Controls.Add(this.lblTuanHienTai);
            this.pnlTopBar.Controls.Add(this.btnTuanSau);
            this.pnlTopBar.Controls.Add(this.lblCaLam);
            this.pnlTopBar.Controls.Add(this.cboCaLam);
            this.pnlTopBar.Controls.Add(this.lblCheDo);
            this.pnlTopBar.Controls.Add(this.cboCheDo);
            this.pnlTopBar.Controls.Add(this.btnCopyTuan);
            this.pnlTopBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopBar.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlTopBar.Location = new System.Drawing.Point(0, 0);
            this.pnlTopBar.Name = "pnlTopBar";
            this.pnlTopBar.Size = new System.Drawing.Size(1351, 60);
            this.pnlTopBar.TabIndex = 1;
            // 
            // btnTuanTruoc
            // 
            this.btnTuanTruoc.BackColor = System.Drawing.Color.Transparent;
            this.btnTuanTruoc.BorderRadius = 6;
            this.btnTuanTruoc.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnTuanTruoc.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTuanTruoc.ForeColor = System.Drawing.Color.White;
            this.btnTuanTruoc.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnTuanTruoc.ImageSize = new System.Drawing.Size(16, 16);
            this.btnTuanTruoc.Location = new System.Drawing.Point(15, 12);
            this.btnTuanTruoc.Name = "btnTuanTruoc";
            this.btnTuanTruoc.Size = new System.Drawing.Size(40, 36);
            this.btnTuanTruoc.TabIndex = 0;
            this.btnTuanTruoc.Text = "";
            this.btnTuanTruoc.Click += new System.EventHandler(this.BtnTuanTruoc_Click);
            // 
            // lblTuanHienTai
            // 
            this.lblTuanHienTai.AutoSize = true;
            this.lblTuanHienTai.BackColor = System.Drawing.Color.Transparent;
            this.lblTuanHienTai.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTuanHienTai.ForeColor = System.Drawing.Color.White;
            this.lblTuanHienTai.Location = new System.Drawing.Point(83, 18);
            this.lblTuanHienTai.Name = "lblTuanHienTai";
            this.lblTuanHienTai.Size = new System.Drawing.Size(201, 20);
            this.lblTuanHienTai.TabIndex = 1;
            this.lblTuanHienTai.Text = "Tuần: 07/04 -> 13/04/2025";
            // 
            // btnTuanSau
            // 
            this.btnTuanSau.BackColor = System.Drawing.Color.Transparent;
            this.btnTuanSau.BorderColor = System.Drawing.Color.Transparent;
            this.btnTuanSau.BorderRadius = 6;
            this.btnTuanSau.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnTuanSau.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTuanSau.ForeColor = System.Drawing.Color.White;
            this.btnTuanSau.ImageAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.btnTuanSau.ImageSize = new System.Drawing.Size(16, 16);
            this.btnTuanSau.Location = new System.Drawing.Point(340, 12);
            this.btnTuanSau.Name = "btnTuanSau";
            this.btnTuanSau.Size = new System.Drawing.Size(40, 36);
            this.btnTuanSau.TabIndex = 2;
            this.btnTuanSau.Text = "";
            this.btnTuanSau.Click += new System.EventHandler(this.BtnTuanSau_Click);
            // 
            // lblCaLam
            // 
            this.lblCaLam.AutoSize = true;
            this.lblCaLam.BackColor = System.Drawing.Color.Transparent;
            this.lblCaLam.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCaLam.ForeColor = System.Drawing.Color.White;
            this.lblCaLam.Location = new System.Drawing.Point(420, 20);
            this.lblCaLam.Name = "lblCaLam";
            this.lblCaLam.Size = new System.Drawing.Size(30, 19);
            this.lblCaLam.TabIndex = 3;
            this.lblCaLam.Text = "Ca:";
            // 
            // cboCaLam
            // 
            this.cboCaLam.BackColor = System.Drawing.Color.Transparent;
            this.cboCaLam.BorderColor = System.Drawing.Color.Transparent;
            this.cboCaLam.BorderRadius = 6;
            this.cboCaLam.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCaLam.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCaLam.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.cboCaLam.FocusedColor = System.Drawing.Color.Empty;
            this.cboCaLam.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCaLam.ForeColor = System.Drawing.Color.White;
            this.cboCaLam.ItemHeight = 30;
            this.cboCaLam.Location = new System.Drawing.Point(482, 12);
            this.cboCaLam.Name = "cboCaLam";
            this.cboCaLam.Size = new System.Drawing.Size(180, 36);
            this.cboCaLam.TabIndex = 4;
            this.cboCaLam.SelectedIndexChanged += new System.EventHandler(this.CboCaLam_SelectedIndexChanged);
            // 
            // lblCheDo
            // 
            this.lblCheDo.AutoSize = true;
            this.lblCheDo.BackColor = System.Drawing.Color.Transparent;
            this.lblCheDo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCheDo.ForeColor = System.Drawing.Color.White;
            this.lblCheDo.Location = new System.Drawing.Point(670, 20);
            this.lblCheDo.Name = "lblCheDo";
            this.lblCheDo.Size = new System.Drawing.Size(62, 19);
            this.lblCheDo.TabIndex = 6;
            this.lblCheDo.Text = "Chế độ:";
            // 
            // cboCheDo
            // 
            this.cboCheDo.BackColor = System.Drawing.Color.Transparent;
            this.cboCheDo.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboCheDo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCheDo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.cboCheDo.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboCheDo.ForeColor = System.Drawing.Color.White;
            this.cboCheDo.ItemHeight = 30;
            this.cboCheDo.Location = new System.Drawing.Point(735, 12);
            this.cboCheDo.Name = "cboCheDo";
            this.cboCheDo.Size = new System.Drawing.Size(120, 36);
            this.cboCheDo.TabIndex = 7;
            this.cboCheDo.SelectedIndexChanged += new System.EventHandler(this.CboCheDo_SelectedIndexChanged);
            // 
            // btnCopyTuan
            // 
            this.btnCopyTuan.BackColor = System.Drawing.Color.Transparent;
            this.btnCopyTuan.BorderRadius = 6;
            this.btnCopyTuan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnCopyTuan.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnCopyTuan.ForeColor = System.Drawing.Color.White;
            this.btnCopyTuan.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnCopyTuan.ImageOffset = new System.Drawing.Point(10, 0);
            this.btnCopyTuan.ImageSize = new System.Drawing.Size(16, 16);
            this.btnCopyTuan.Location = new System.Drawing.Point(865, 12);
            this.btnCopyTuan.Name = "btnCopyTuan";
            this.btnCopyTuan.Size = new System.Drawing.Size(220, 36);
            this.btnCopyTuan.TabIndex = 5;
            this.btnCopyTuan.Text = "  Copy sang tuần sau";
            this.btnCopyTuan.Click += new System.EventHandler(this.BtnCopyTuan_Click);
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitMain.Location = new System.Drawing.Point(0, 60);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.gbLichTuan);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.gbNVChuaPhan);
            this.splitMain.Panel2.Controls.Add(this.pnlButtons);
            this.splitMain.Panel2.Controls.Add(this.gbNVDaPhan);
            this.splitMain.Panel2.Controls.Add(this.gbPhanCong);
            this.splitMain.Size = new System.Drawing.Size(1351, 740);
            this.splitMain.SplitterDistance = 963;
            this.splitMain.TabIndex = 0;
            // 
            // gbLichTuan
            // 
            this.gbLichTuan.Controls.Add(this.gridLichTuan);
            this.gbLichTuan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbLichTuan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbLichTuan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbLichTuan.Location = new System.Drawing.Point(0, 0);
            this.gbLichTuan.Name = "gbLichTuan";
            this.gbLichTuan.Size = new System.Drawing.Size(963, 740);
            this.gbLichTuan.TabIndex = 0;
            this.gbLichTuan.Text = "LỊCH PHÂN CA TUẦN";
            // 
            // gridLichTuan
            // 
            this.gridLichTuan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLichTuan.Location = new System.Drawing.Point(0, 40);
            this.gridLichTuan.MainView = this.gridViewLichTuan;
            this.gridLichTuan.Name = "gridLichTuan";
            this.gridLichTuan.Size = new System.Drawing.Size(963, 700);
            this.gridLichTuan.TabIndex = 0;
            this.gridLichTuan.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLichTuan});
            // 
            // gridViewLichTuan
            // 
            this.gridViewLichTuan.GridControl = this.gridLichTuan;
            this.gridViewLichTuan.Name = "gridViewLichTuan";
            this.gridViewLichTuan.OptionsBehavior.Editable = false;
            // 
            // gbNVChuaPhan
            // 
            this.gbNVChuaPhan.Controls.Add(this.lstNVChuaPhan);
            this.gbNVChuaPhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbNVChuaPhan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbNVChuaPhan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbNVChuaPhan.Location = new System.Drawing.Point(0, 123);
            this.gbNVChuaPhan.Name = "gbNVChuaPhan";
            this.gbNVChuaPhan.Size = new System.Drawing.Size(384, 317);
            this.gbNVChuaPhan.TabIndex = 0;
            this.gbNVChuaPhan.Text = "NV CHƯA PHÂN CA (kéo thả sang ->)";
            // 
            // lstNVChuaPhan
            // 
            this.lstNVChuaPhan.AllowDrop = true;
            this.lstNVChuaPhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstNVChuaPhan.Location = new System.Drawing.Point(0, 40);
            this.lstNVChuaPhan.Name = "lstNVChuaPhan";
            this.lstNVChuaPhan.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.lstNVChuaPhan.Size = new System.Drawing.Size(384, 277);
            this.lstNVChuaPhan.TabIndex = 0;
            // Drag source: kéo NV chưa phân sang lstNVDaPhan
            this.lstNVChuaPhan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LstNVChuaPhan_MouseDown);
            // Drop target: nhận NV từ lstNVDaPhan (gỡ bỏ ngược lại)
            this.lstNVChuaPhan.DragOver += new System.Windows.Forms.DragEventHandler(this.LstNVChuaPhan_DragOver);
            this.lstNVChuaPhan.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstNVChuaPhan_DragDrop);
            // 
            // pnlButtons
            // 
            this.pnlButtons.Controls.Add(this.btnPhanCong);
            this.pnlButtons.Controls.Add(this.btnGoBo);
            this.pnlButtons.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlButtons.Location = new System.Drawing.Point(0, 440);
            this.pnlButtons.Name = "pnlButtons";
            this.pnlButtons.Size = new System.Drawing.Size(384, 50);
            this.pnlButtons.TabIndex = 1;
            // 
            // btnPhanCong
            // 
            this.btnPhanCong.BorderRadius = 6;
            this.btnPhanCong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnPhanCong.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnPhanCong.ForeColor = System.Drawing.Color.White;
            this.btnPhanCong.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnPhanCong.ImageOffset = new System.Drawing.Point(10, 0);
            this.btnPhanCong.ImageSize = new System.Drawing.Size(16, 16);
            this.btnPhanCong.Location = new System.Drawing.Point(10, 7);
            this.btnPhanCong.Name = "btnPhanCong";
            this.btnPhanCong.Size = new System.Drawing.Size(160, 36);
            this.btnPhanCong.TabIndex = 0;
            this.btnPhanCong.Text = "   Phân công";
            this.btnPhanCong.Click += new System.EventHandler(this.BtnPhanCong_Click);
            // 
            // btnGoBo
            // 
            this.btnGoBo.BorderRadius = 6;
            this.btnGoBo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnGoBo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGoBo.ForeColor = System.Drawing.Color.White;
            this.btnGoBo.ImageAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.btnGoBo.ImageOffset = new System.Drawing.Point(10, 0);
            this.btnGoBo.ImageSize = new System.Drawing.Size(16, 16);
            this.btnGoBo.Location = new System.Drawing.Point(180, 7);
            this.btnGoBo.Name = "btnGoBo";
            this.btnGoBo.Size = new System.Drawing.Size(160, 36);
            this.btnGoBo.TabIndex = 1;
            this.btnGoBo.Text = "   Gỡ bỏ";
            this.btnGoBo.Click += new System.EventHandler(this.BtnGoBo_Click);
            // 
            // gbNVDaPhan
            // 
            this.gbNVDaPhan.Controls.Add(this.lstNVDaPhan);
            this.gbNVDaPhan.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbNVDaPhan.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbNVDaPhan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbNVDaPhan.Location = new System.Drawing.Point(0, 490);
            this.gbNVDaPhan.Name = "gbNVDaPhan";
            this.gbNVDaPhan.Size = new System.Drawing.Size(384, 250);
            this.gbNVDaPhan.TabIndex = 2;
            this.gbNVDaPhan.Text = "NV ĐÃ PHÂN VÀO CA NÀY";
            // 
            // lstNVDaPhan
            // 
            this.lstNVDaPhan.AllowDrop = true;
            this.lstNVDaPhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstNVDaPhan.Location = new System.Drawing.Point(0, 40);
            this.lstNVDaPhan.Name = "lstNVDaPhan";
            this.lstNVDaPhan.Size = new System.Drawing.Size(384, 210);
            this.lstNVDaPhan.TabIndex = 0;
            // Drag source: kéo NV đã phân về lstNVChuaPhan (gỡ bỏ)
            this.lstNVDaPhan.MouseDown += new System.Windows.Forms.MouseEventHandler(this.LstNVDaPhan_MouseDown);
            // Drop target: nhận NV từ lstNVChuaPhan (phân công)
            this.lstNVDaPhan.DragOver += new System.Windows.Forms.DragEventHandler(this.LstNVDaPhan_DragOver);
            this.lstNVDaPhan.DragDrop += new System.Windows.Forms.DragEventHandler(this.LstNVDaPhan_DragDrop);
            // 
            // gbPhanCong
            // 
            this.gbPhanCong.Controls.Add(this.lblChonKhuVuc);
            this.gbPhanCong.Controls.Add(this.cboKhuVuc);
            this.gbPhanCong.Controls.Add(this.lblChonNgay);
            this.gbPhanCong.Controls.Add(this.cboNgayTrongTuan);
            this.gbPhanCong.Dock = System.Windows.Forms.DockStyle.Top;
            this.gbPhanCong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.gbPhanCong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbPhanCong.Location = new System.Drawing.Point(0, 0);
            this.gbPhanCong.Name = "gbPhanCong";
            this.gbPhanCong.Size = new System.Drawing.Size(384, 123);
            this.gbPhanCong.TabIndex = 3;
            this.gbPhanCong.Text = "BỘ LỌC PHÂN CÔNG";
            // 
            // lblChonKhuVuc
            // 
            this.lblChonKhuVuc.AutoSize = true;
            this.lblChonKhuVuc.Location = new System.Drawing.Point(10, 47);
            this.lblChonKhuVuc.Name = "lblChonKhuVuc";
            this.lblChonKhuVuc.Size = new System.Drawing.Size(53, 15);
            this.lblChonKhuVuc.TabIndex = 0;
            this.lblChonKhuVuc.Text = "Khu vực:";
            // 
            // cboKhuVuc
            // 
            this.cboKhuVuc.BackColor = System.Drawing.Color.Transparent;
            this.cboKhuVuc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboKhuVuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhuVuc.FocusedColor = System.Drawing.Color.Empty;
            this.cboKhuVuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboKhuVuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboKhuVuc.ItemHeight = 30;
            this.cboKhuVuc.Location = new System.Drawing.Point(80, 43);
            this.cboKhuVuc.Name = "cboKhuVuc";
            this.cboKhuVuc.Size = new System.Drawing.Size(290, 36);
            this.cboKhuVuc.TabIndex = 1;
            this.cboKhuVuc.SelectedIndexChanged += new System.EventHandler(this.CboKhuVuc_SelectedIndexChanged);
            // 
            // lblChonNgay
            // 
            this.lblChonNgay.AutoSize = true;
            this.lblChonNgay.Location = new System.Drawing.Point(10, 81);
            this.lblChonNgay.Name = "lblChonNgay";
            this.lblChonNgay.Size = new System.Drawing.Size(38, 15);
            this.lblChonNgay.TabIndex = 2;
            this.lblChonNgay.Text = "Ngày:";
            // 
            // cboNgayTrongTuan
            // 
            this.cboNgayTrongTuan.BackColor = System.Drawing.Color.Transparent;
            this.cboNgayTrongTuan.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboNgayTrongTuan.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboNgayTrongTuan.FocusedColor = System.Drawing.Color.Empty;
            this.cboNgayTrongTuan.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboNgayTrongTuan.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboNgayTrongTuan.ItemHeight = 30;
            this.cboNgayTrongTuan.Location = new System.Drawing.Point(80, 77);
            this.cboNgayTrongTuan.Name = "cboNgayTrongTuan";
            this.cboNgayTrongTuan.Size = new System.Drawing.Size(290, 36);
            this.cboNgayTrongTuan.TabIndex = 3;
            this.cboNgayTrongTuan.SelectedIndexChanged += new System.EventHandler(this.CboNgayTrongTuan_SelectedIndexChanged);
            // 
            // frmLichLamViec
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1351, 800);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlTopBar);
            this.Name = "frmLichLamViec";
            this.Text = "Lịch Làm Việc Nhân Viên";
            this.Load += new System.EventHandler(this.frmLichLamViec_Load);
            this.pnlTopBar.ResumeLayout(false);
            this.pnlTopBar.PerformLayout();
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.gbLichTuan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLichTuan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLichTuan)).EndInit();
            this.gbNVChuaPhan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstNVChuaPhan)).EndInit();
            this.pnlButtons.ResumeLayout(false);
            this.gbNVDaPhan.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lstNVDaPhan)).EndInit();
            this.gbPhanCong.ResumeLayout(false);
            this.gbPhanCong.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTopBar;
        private System.Windows.Forms.Label lblTuanHienTai;
        private Guna.UI2.WinForms.Guna2Button btnTuanTruoc;
        private Guna.UI2.WinForms.Guna2Button btnTuanSau;
        private System.Windows.Forms.Label lblCaLam;
        private Guna.UI2.WinForms.Guna2ComboBox cboCaLam;
        private Guna.UI2.WinForms.Guna2Button btnCopyTuan;
        private System.Windows.Forms.Label lblCheDo;
        private Guna.UI2.WinForms.Guna2ComboBox cboCheDo;

        private System.Windows.Forms.SplitContainer splitMain;
        private Guna.UI2.WinForms.Guna2GroupBox gbLichTuan;
        private DevExpress.XtraGrid.GridControl gridLichTuan;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLichTuan;

        private Guna.UI2.WinForms.Guna2GroupBox gbPhanCong;
        private System.Windows.Forms.Label lblChonKhuVuc;
        private Guna.UI2.WinForms.Guna2ComboBox cboKhuVuc;
        private System.Windows.Forms.Label lblChonNgay;
        private Guna.UI2.WinForms.Guna2ComboBox cboNgayTrongTuan;

        private Guna.UI2.WinForms.Guna2GroupBox gbNVChuaPhan;
        private DevExpress.XtraEditors.ListBoxControl lstNVChuaPhan;
        private Guna.UI2.WinForms.Guna2GroupBox gbNVDaPhan;
        private DevExpress.XtraEditors.ListBoxControl lstNVDaPhan;
        private Guna.UI2.WinForms.Guna2Button btnPhanCong;
        private Guna.UI2.WinForms.Guna2Button btnGoBo;
        private System.Windows.Forms.Panel pnlButtons;
    }
}
