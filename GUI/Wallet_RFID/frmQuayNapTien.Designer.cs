namespace GUI
{
    partial class frmQuayNapTien
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
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlBody = new Guna.UI2.WinForms.Guna2Panel();
            this.gridLichSu = new DevExpress.XtraGrid.GridControl();
            this.gridViewLichSu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gbNapTien = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblSoTienLabel = new System.Windows.Forms.Label();
            this.spnSoTien = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.lblPhuongThucLabel = new System.Windows.Forms.Label();
            this.cboPhuongThuc = new Guna.UI2.WinForms.Guna2ComboBox();
            this.pnlQuickAmount = new Guna.UI2.WinForms.Guna2Panel();
            this.btnNap50k = new Guna.UI2.WinForms.Guna2Button();
            this.btnNap100k = new Guna.UI2.WinForms.Guna2Button();
            this.btnNap200k = new Guna.UI2.WinForms.Guna2Button();
            this.btnNap500k = new Guna.UI2.WinForms.Guna2Button();
            this.btnNap1M = new Guna.UI2.WinForms.Guna2Button();
            this.gbThongTin = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblTenKhach = new System.Windows.Forms.Label();
            this.lblTenKhachValue = new System.Windows.Forms.Label();
            this.lblSoDu = new System.Windows.Forms.Label();
            this.lblSoDuValue = new System.Windows.Forms.Label();
            this.gbTraCuu = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblMaRfidLabel = new System.Windows.Forms.Label();
            this.txtMaRfid = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnTraCuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnNapTien = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLichSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLichSu)).BeginInit();
            this.gbNapTien.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnSoTien)).BeginInit();
            this.pnlQuickAmount.SuspendLayout();
            this.gbThongTin.SuspendLayout();
            this.gbTraCuu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(99)))));
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(680, 60);
            this.pnlHeader.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(680, 60);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "TRẠM NẠP TIỀN RFID";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.gridLichSu);
            this.pnlBody.Controls.Add(this.gbNapTien);
            this.pnlBody.Controls.Add(this.gbThongTin);
            this.pnlBody.Controls.Add(this.gbTraCuu);
            this.pnlBody.Controls.Add(this.btnNapTien);
            this.pnlBody.Controls.Add(this.btnLamMoi);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.pnlBody.Location = new System.Drawing.Point(0, 60);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(15);
            this.pnlBody.Size = new System.Drawing.Size(680, 723);
            this.pnlBody.TabIndex = 0;
            // 
            // gridLichSu
            // 
            this.gridLichSu.Location = new System.Drawing.Point(15, 529);
            this.gridLichSu.MainView = this.gridViewLichSu;
            this.gridLichSu.Name = "gridLichSu";
            this.gridLichSu.Size = new System.Drawing.Size(650, 185);
            this.gridLichSu.TabIndex = 0;
            this.gridLichSu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLichSu});
            // 
            // gridViewLichSu
            // 
            this.gridViewLichSu.GridControl = this.gridLichSu;
            this.gridViewLichSu.Name = "gridViewLichSu";
            this.gridViewLichSu.OptionsView.ShowGroupPanel = false;
            // 
            // gbNapTien
            // 
            this.gbNapTien.Controls.Add(this.lblSoTienLabel);
            this.gbNapTien.Controls.Add(this.spnSoTien);
            this.gbNapTien.Controls.Add(this.lblPhuongThucLabel);
            this.gbNapTien.Controls.Add(this.cboPhuongThuc);
            this.gbNapTien.Controls.Add(this.pnlQuickAmount);
            this.gbNapTien.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbNapTien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.gbNapTien.Location = new System.Drawing.Point(15, 281);
            this.gbNapTien.Name = "gbNapTien";
            this.gbNapTien.Size = new System.Drawing.Size(650, 182);
            this.gbNapTien.TabIndex = 1;
            this.gbNapTien.Text = "Nạp Tiền";
            // 
            // lblSoTienLabel
            // 
            this.lblSoTienLabel.AutoSize = true;
            this.lblSoTienLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoTienLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSoTienLabel.Location = new System.Drawing.Point(15, 70);
            this.lblSoTienLabel.Name = "lblSoTienLabel";
            this.lblSoTienLabel.Size = new System.Drawing.Size(46, 15);
            this.lblSoTienLabel.TabIndex = 0;
            this.lblSoTienLabel.Text = "Số tiền:";
            // 
            // spnSoTien
            // 
            this.spnSoTien.BackColor = System.Drawing.Color.Transparent;
            this.spnSoTien.BorderRadius = 5;
            this.spnSoTien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.spnSoTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.spnSoTien.Increment = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spnSoTien.Location = new System.Drawing.Point(100, 63);
            this.spnSoTien.Maximum = new decimal(new int[] {
            50000000,
            0,
            0,
            0});
            this.spnSoTien.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spnSoTien.Name = "spnSoTien";
            this.spnSoTien.Size = new System.Drawing.Size(250, 40);
            this.spnSoTien.TabIndex = 1;
            this.spnSoTien.ThousandsSeparator = true;
            this.spnSoTien.Value = new decimal(new int[] {
            100000,
            0,
            0,
            0});
            // 
            // lblPhuongThucLabel
            // 
            this.lblPhuongThucLabel.AutoSize = true;
            this.lblPhuongThucLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblPhuongThucLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblPhuongThucLabel.Location = new System.Drawing.Point(380, 70);
            this.lblPhuongThucLabel.Name = "lblPhuongThucLabel";
            this.lblPhuongThucLabel.Size = new System.Drawing.Size(79, 15);
            this.lblPhuongThucLabel.TabIndex = 2;
            this.lblPhuongThucLabel.Text = "Phương thức:";
            // 
            // cboPhuongThuc
            // 
            this.cboPhuongThuc.BackColor = System.Drawing.Color.Transparent;
            this.cboPhuongThuc.BorderRadius = 5;
            this.cboPhuongThuc.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboPhuongThuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboPhuongThuc.FocusedColor = System.Drawing.Color.Empty;
            this.cboPhuongThuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboPhuongThuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.cboPhuongThuc.ItemHeight = 28;
            this.cboPhuongThuc.Items.AddRange(new object[] {
            "TienMat",
            "ChuyenKhoan",
            "The",
            "MoMo",
            "VnPay"});
            this.cboPhuongThuc.Location = new System.Drawing.Point(480, 63);
            this.cboPhuongThuc.Name = "cboPhuongThuc";
            this.cboPhuongThuc.Size = new System.Drawing.Size(155, 34);
            this.cboPhuongThuc.TabIndex = 3;
            // 
            // pnlQuickAmount
            // 
            this.pnlQuickAmount.Controls.Add(this.btnNap50k);
            this.pnlQuickAmount.Controls.Add(this.btnNap100k);
            this.pnlQuickAmount.Controls.Add(this.btnNap200k);
            this.pnlQuickAmount.Controls.Add(this.btnNap500k);
            this.pnlQuickAmount.Controls.Add(this.btnNap1M);
            this.pnlQuickAmount.FillColor = System.Drawing.Color.Transparent;
            this.pnlQuickAmount.Location = new System.Drawing.Point(15, 118);
            this.pnlQuickAmount.Name = "pnlQuickAmount";
            this.pnlQuickAmount.Size = new System.Drawing.Size(620, 50);
            this.pnlQuickAmount.TabIndex = 4;
            // 
            // btnNap50k
            // 
            this.btnNap50k.BorderRadius = 5;
            this.btnNap50k.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnNap50k.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNap50k.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnNap50k.Location = new System.Drawing.Point(14, 7);
            this.btnNap50k.Name = "btnNap50k";
            this.btnNap50k.Size = new System.Drawing.Size(110, 36);
            this.btnNap50k.TabIndex = 0;
            this.btnNap50k.Text = "50K";
            this.btnNap50k.Click += new System.EventHandler(this.btnNap50k_Click);
            // 
            // btnNap100k
            // 
            this.btnNap100k.BorderRadius = 5;
            this.btnNap100k.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnNap100k.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNap100k.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnNap100k.Location = new System.Drawing.Point(134, 7);
            this.btnNap100k.Name = "btnNap100k";
            this.btnNap100k.Size = new System.Drawing.Size(110, 36);
            this.btnNap100k.TabIndex = 1;
            this.btnNap100k.Text = "100K";
            this.btnNap100k.Click += new System.EventHandler(this.btnNap100k_Click);
            // 
            // btnNap200k
            // 
            this.btnNap200k.BorderRadius = 5;
            this.btnNap200k.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnNap200k.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNap200k.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnNap200k.Location = new System.Drawing.Point(254, 7);
            this.btnNap200k.Name = "btnNap200k";
            this.btnNap200k.Size = new System.Drawing.Size(110, 36);
            this.btnNap200k.TabIndex = 2;
            this.btnNap200k.Text = "200K";
            this.btnNap200k.Click += new System.EventHandler(this.btnNap200k_Click);
            // 
            // btnNap500k
            // 
            this.btnNap500k.BorderRadius = 5;
            this.btnNap500k.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnNap500k.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNap500k.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnNap500k.Location = new System.Drawing.Point(374, 7);
            this.btnNap500k.Name = "btnNap500k";
            this.btnNap500k.Size = new System.Drawing.Size(110, 36);
            this.btnNap500k.TabIndex = 3;
            this.btnNap500k.Text = "500K";
            this.btnNap500k.Click += new System.EventHandler(this.btnNap500k_Click);
            // 
            // btnNap1M
            // 
            this.btnNap1M.BorderRadius = 5;
            this.btnNap1M.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.btnNap1M.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnNap1M.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.btnNap1M.Location = new System.Drawing.Point(494, 7);
            this.btnNap1M.Name = "btnNap1M";
            this.btnNap1M.Size = new System.Drawing.Size(110, 36);
            this.btnNap1M.TabIndex = 4;
            this.btnNap1M.Text = "1M";
            this.btnNap1M.Click += new System.EventHandler(this.btnNap1M_Click);
            // 
            // gbThongTin
            // 
            this.gbThongTin.Controls.Add(this.lblTenKhach);
            this.gbThongTin.Controls.Add(this.lblTenKhachValue);
            this.gbThongTin.Controls.Add(this.lblSoDu);
            this.gbThongTin.Controls.Add(this.lblSoDuValue);
            this.gbThongTin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbThongTin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.gbThongTin.Location = new System.Drawing.Point(15, 141);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(650, 117);
            this.gbThongTin.TabIndex = 2;
            this.gbThongTin.Text = "Thông Tin Ví";
            // 
            // lblTenKhach
            // 
            this.lblTenKhach.AutoSize = true;
            this.lblTenKhach.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTenKhach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTenKhach.Location = new System.Drawing.Point(15, 53);
            this.lblTenKhach.Name = "lblTenKhach";
            this.lblTenKhach.Size = new System.Drawing.Size(73, 15);
            this.lblTenKhach.TabIndex = 0;
            this.lblTenKhach.Text = "Khách hàng:";
            // 
            // lblTenKhachValue
            // 
            this.lblTenKhachValue.AutoSize = true;
            this.lblTenKhachValue.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTenKhachValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTenKhachValue.Location = new System.Drawing.Point(120, 51);
            this.lblTenKhachValue.Name = "lblTenKhachValue";
            this.lblTenKhachValue.Size = new System.Drawing.Size(27, 20);
            this.lblTenKhachValue.TabIndex = 1;
            this.lblTenKhachValue.Text = "---";
            // 
            // lblSoDu
            // 
            this.lblSoDu.AutoSize = true;
            this.lblSoDu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoDu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSoDu.Location = new System.Drawing.Point(15, 88);
            this.lblSoDu.Name = "lblSoDu";
            this.lblSoDu.Size = new System.Drawing.Size(40, 15);
            this.lblSoDu.TabIndex = 2;
            this.lblSoDu.Text = "Số dư:";
            // 
            // lblSoDuValue
            // 
            this.lblSoDuValue.AutoSize = true;
            this.lblSoDuValue.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblSoDuValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.lblSoDuValue.Location = new System.Drawing.Point(120, 81);
            this.lblSoDuValue.Name = "lblSoDuValue";
            this.lblSoDuValue.Size = new System.Drawing.Size(40, 25);
            this.lblSoDuValue.TabIndex = 3;
            this.lblSoDuValue.Text = "0 đ";
            // 
            // gbTraCuu
            // 
            this.gbTraCuu.Controls.Add(this.lblMaRfidLabel);
            this.gbTraCuu.Controls.Add(this.txtMaRfid);
            this.gbTraCuu.Controls.Add(this.btnTraCuu);
            this.gbTraCuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbTraCuu.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(51)))), ((int)(((byte)(76)))));
            this.gbTraCuu.Location = new System.Drawing.Point(15, 15);
            this.gbTraCuu.Name = "gbTraCuu";
            this.gbTraCuu.Size = new System.Drawing.Size(650, 108);
            this.gbTraCuu.TabIndex = 3;
            this.gbTraCuu.Text = "Tra Cứu Thẻ";
            // 
            // lblMaRfidLabel
            // 
            this.lblMaRfidLabel.AutoSize = true;
            this.lblMaRfidLabel.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblMaRfidLabel.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblMaRfidLabel.Location = new System.Drawing.Point(15, 66);
            this.lblMaRfidLabel.Name = "lblMaRfidLabel";
            this.lblMaRfidLabel.Size = new System.Drawing.Size(54, 15);
            this.lblMaRfidLabel.TabIndex = 0;
            this.lblMaRfidLabel.Text = "Mã RFID:";
            // 
            // txtMaRfid
            // 
            this.txtMaRfid.BorderRadius = 5;
            this.txtMaRfid.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMaRfid.DefaultText = "";
            this.txtMaRfid.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtMaRfid.Location = new System.Drawing.Point(100, 56);
            this.txtMaRfid.Name = "txtMaRfid";
            this.txtMaRfid.PlaceholderText = "Quẹt hoặc nhập mã thẻ RFID...";
            this.txtMaRfid.SelectedText = "";
            this.txtMaRfid.Size = new System.Drawing.Size(370, 36);
            this.txtMaRfid.TabIndex = 1;
            this.txtMaRfid.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtMaRfid_KeyDown);
            // 
            // btnTraCuu
            // 
            this.btnTraCuu.BorderRadius = 5;
            this.btnTraCuu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(71)))), ((int)(((byte)(99)))));
            this.btnTraCuu.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnTraCuu.ForeColor = System.Drawing.Color.White;
            this.btnTraCuu.Location = new System.Drawing.Point(485, 56);
            this.btnTraCuu.Name = "btnTraCuu";
            this.btnTraCuu.Size = new System.Drawing.Size(150, 36);
            this.btnTraCuu.TabIndex = 2;
            this.btnTraCuu.Text = "Tra Cứu";
            this.btnTraCuu.Click += new System.EventHandler(this.BtnTraCuu_Click);
            // 
            // btnNapTien
            // 
            this.btnNapTien.BorderRadius = 8;
            this.btnNapTien.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnNapTien.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.btnNapTien.ForeColor = System.Drawing.Color.White;
            this.btnNapTien.Location = new System.Drawing.Point(15, 469);
            this.btnNapTien.Name = "btnNapTien";
            this.btnNapTien.Size = new System.Drawing.Size(480, 50);
            this.btnNapTien.TabIndex = 4;
            this.btnNapTien.Text = "NẠP TIỀN";
            this.btnNapTien.Click += new System.EventHandler(this.BtnNapTien_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 8;
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(510, 469);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(155, 50);
            this.btnLamMoi.TabIndex = 5;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // frmQuayNapTien
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(680, 783);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQuayNapTien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Trạm Nạp Tiền RFID";
            this.Load += new System.EventHandler(this.frmQuayNapTien_Load);
            this.pnlHeader.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridLichSu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLichSu)).EndInit();
            this.gbNapTien.ResumeLayout(false);
            this.gbNapTien.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnSoTien)).EndInit();
            this.pnlQuickAmount.ResumeLayout(false);
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            this.gbTraCuu.ResumeLayout(false);
            this.gbTraCuu.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlBody;
        private Guna.UI2.WinForms.Guna2GroupBox gbTraCuu;
        private System.Windows.Forms.Label lblMaRfidLabel;
        private Guna.UI2.WinForms.Guna2TextBox txtMaRfid;
        private Guna.UI2.WinForms.Guna2Button btnTraCuu;
        private Guna.UI2.WinForms.Guna2GroupBox gbThongTin;
        private System.Windows.Forms.Label lblTenKhach;
        private System.Windows.Forms.Label lblTenKhachValue;
        private System.Windows.Forms.Label lblSoDu;
        private System.Windows.Forms.Label lblSoDuValue;
        private Guna.UI2.WinForms.Guna2GroupBox gbNapTien;
        private System.Windows.Forms.Label lblSoTienLabel;
        private Guna.UI2.WinForms.Guna2NumericUpDown spnSoTien;
        private System.Windows.Forms.Label lblPhuongThucLabel;
        private Guna.UI2.WinForms.Guna2ComboBox cboPhuongThuc;
        private Guna.UI2.WinForms.Guna2Panel pnlQuickAmount;
        private Guna.UI2.WinForms.Guna2Button btnNap50k;
        private Guna.UI2.WinForms.Guna2Button btnNap100k;
        private Guna.UI2.WinForms.Guna2Button btnNap200k;
        private Guna.UI2.WinForms.Guna2Button btnNap500k;
        private Guna.UI2.WinForms.Guna2Button btnNap1M;
        private Guna.UI2.WinForms.Guna2Button btnNapTien;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
        private DevExpress.XtraGrid.GridControl gridLichSu;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewLichSu;
    }
}
