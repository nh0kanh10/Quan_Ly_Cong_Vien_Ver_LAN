namespace GUI
{
    partial class frmKiemSoatVe
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _cameraScanner?.Dispose();
                if (components != null) components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.picCamera = new System.Windows.Forms.PictureBox();
            this.btnToggleCamera = new Guna.UI2.WinForms.Guna2Button();
            this.btnScanFile = new Guna.UI2.WinForms.Guna2Button();
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.cboTroChoi = new System.Windows.Forms.ComboBox();
            this.cboKhuVuc = new System.Windows.Forms.ComboBox();
            this.picGateIcon = new System.Windows.Forms.PictureBox();
            this.lblGateTitle = new System.Windows.Forms.Label();
            this.pnlScannerBar = new System.Windows.Forms.Panel();
            this.txtScanInput = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblScanIcon = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlTicketInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTicketNgayMua = new System.Windows.Forms.Label();
            this.lblTicketLuot = new System.Windows.Forms.Label();
            this.lblTicketKhuVuc = new System.Windows.Forms.Label();
            this.lblTicketTenDV = new System.Windows.Forms.Label();
            this.lblTicketMaVe = new System.Windows.Forms.Label();
            this.lblTicketVerdict = new System.Windows.Forms.Label();
            this.lblTicketHeader = new System.Windows.Forms.Label();
            this.pnlFeedback = new Guna.UI2.WinForms.Guna2Panel();
            this.lblStatusSub = new System.Windows.Forms.Label();
            this.lblStatusTitle = new System.Windows.Forms.Label();
            this.picStatus = new System.Windows.Forms.PictureBox();
            this.lblCounter = new System.Windows.Forms.Label();
            this.lstHistory = new System.Windows.Forms.ListView();
            this.colTime = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colCode = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.colStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtScanner = new Guna.UI2.WinForms.Guna2TextBox();
            this.timerReset = new System.Windows.Forms.Timer(this.components);
            this.timerFlash = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picGateIcon)).BeginInit();
            this.pnlScannerBar.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.pnlTicketInfo.SuspendLayout();
            this.pnlFeedback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            this.SuspendLayout();
            // 
            // picCamera
            // 
            this.picCamera.BackColor = System.Drawing.Color.Black;
            this.picCamera.Location = new System.Drawing.Point(127, 0);
            this.picCamera.Name = "picCamera";
            this.picCamera.Size = new System.Drawing.Size(151, 140);
            this.picCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picCamera.TabIndex = 10;
            this.picCamera.TabStop = false;
            this.picCamera.Visible = false;
            // 
            // btnToggleCamera
            // 
            this.btnToggleCamera.BorderRadius = 8;
            this.btnToggleCamera.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnToggleCamera.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.btnToggleCamera.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnToggleCamera.ForeColor = System.Drawing.Color.White;
            this.btnToggleCamera.Location = new System.Drawing.Point(135, 10);
            this.btnToggleCamera.Name = "btnToggleCamera";
            this.btnToggleCamera.Size = new System.Drawing.Size(120, 36);
            this.btnToggleCamera.TabIndex = 2;
            this.btnToggleCamera.Text = "Bật Cam";
            this.btnToggleCamera.Click += new System.EventHandler(this.btnToggleCamera_Click);
            // 
            // btnScanFile
            // 
            this.btnScanFile.BorderRadius = 8;
            this.btnScanFile.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnScanFile.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnScanFile.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnScanFile.ForeColor = System.Drawing.Color.White;
            this.btnScanFile.Location = new System.Drawing.Point(255, 10);
            this.btnScanFile.Name = "btnScanFile";
            this.btnScanFile.Size = new System.Drawing.Size(120, 36);
            this.btnScanFile.TabIndex = 3;
            this.btnScanFile.Text = "Chọn Ảnh";
            this.btnScanFile.Click += new System.EventHandler(this.btnScanFile_Click);
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pnlHeader.Controls.Add(this.cboTroChoi);
            this.pnlHeader.Controls.Add(this.cboKhuVuc);
            this.pnlHeader.Controls.Add(this.picGateIcon);
            this.pnlHeader.Controls.Add(this.lblGateTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlHeader.Size = new System.Drawing.Size(1432, 50);
            this.pnlHeader.TabIndex = 4;
            // 
            // cboTroChoi
            // 
            this.cboTroChoi.BackColor = System.Drawing.Color.CornflowerBlue;
            this.cboTroChoi.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTroChoi.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboTroChoi.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cboTroChoi.ForeColor = System.Drawing.Color.White;
            this.cboTroChoi.Location = new System.Drawing.Point(460, 10);
            this.cboTroChoi.Name = "cboTroChoi";
            this.cboTroChoi.Size = new System.Drawing.Size(280, 25);
            this.cboTroChoi.TabIndex = 3;
            // 
            // cboKhuVuc
            // 
            this.cboKhuVuc.BackColor = System.Drawing.Color.CornflowerBlue;
            this.cboKhuVuc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKhuVuc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboKhuVuc.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.cboKhuVuc.ForeColor = System.Drawing.Color.White;
            this.cboKhuVuc.Location = new System.Drawing.Point(230, 10);
            this.cboKhuVuc.Name = "cboKhuVuc";
            this.cboKhuVuc.Size = new System.Drawing.Size(220, 25);
            this.cboKhuVuc.TabIndex = 2;
            // 
            // picGateIcon
            // 
            this.picGateIcon.BackColor = System.Drawing.Color.Transparent;
            this.picGateIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.picGateIcon.Location = new System.Drawing.Point(10, 8);
            this.picGateIcon.Name = "picGateIcon";
            this.picGateIcon.Size = new System.Drawing.Size(36, 34);
            this.picGateIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picGateIcon.TabIndex = 2;
            this.picGateIcon.TabStop = false;
            // 
            // lblGateTitle
            // 
            this.lblGateTitle.ForeColor = System.Drawing.Color.White;
            this.lblGateTitle.Location = new System.Drawing.Point(48, 0);
            this.lblGateTitle.Name = "lblGateTitle";
            this.lblGateTitle.Size = new System.Drawing.Size(280, 55);
            this.lblGateTitle.TabIndex = 1;
            this.lblGateTitle.Text = "  CỔNG KIỂM SOÁT VÉ";
            this.lblGateTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlScannerBar
            // 
            this.pnlScannerBar.BackColor = System.Drawing.Color.CornflowerBlue;
            this.pnlScannerBar.Controls.Add(this.txtScanInput);
            this.pnlScannerBar.Controls.Add(this.btnScanFile);
            this.pnlScannerBar.Controls.Add(this.btnToggleCamera);
            this.pnlScannerBar.Controls.Add(this.lblScanIcon);
            this.pnlScannerBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlScannerBar.Location = new System.Drawing.Point(0, 50);
            this.pnlScannerBar.Name = "pnlScannerBar";
            this.pnlScannerBar.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlScannerBar.Size = new System.Drawing.Size(1432, 56);
            this.pnlScannerBar.TabIndex = 3;
            // 
            // txtScanInput
            // 
            this.txtScanInput.BorderColor = System.Drawing.Color.White;
            this.txtScanInput.BorderRadius = 8;
            this.txtScanInput.BorderThickness = 2;
            this.txtScanInput.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtScanInput.DefaultText = "";
            this.txtScanInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtScanInput.FillColor = System.Drawing.Color.Gainsboro;
            this.txtScanInput.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(130)))), ((int)(((byte)(246)))));
            this.txtScanInput.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            this.txtScanInput.ForeColor = System.Drawing.Color.White;
            this.txtScanInput.Location = new System.Drawing.Point(375, 10);
            this.txtScanInput.Margin = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.txtScanInput.Name = "txtScanInput";
            this.txtScanInput.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(80)))), ((int)(((byte)(85)))), ((int)(((byte)(100)))));
            this.txtScanInput.PlaceholderText = "Nhập hoặc quét mã vé -> Enter...";
            this.txtScanInput.SelectedText = "";
            this.txtScanInput.Size = new System.Drawing.Size(1042, 36);
            this.txtScanInput.TabIndex = 0;
            this.txtScanInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScanInput_KeyDown);
            // 
            // lblScanIcon
            // 
            this.lblScanIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblScanIcon.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblScanIcon.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(0)))), ((int)(((byte)(64)))));
            this.lblScanIcon.Location = new System.Drawing.Point(15, 10);
            this.lblScanIcon.Name = "lblScanIcon";
            this.lblScanIcon.Size = new System.Drawing.Size(120, 36);
            this.lblScanIcon.TabIndex = 1;
            this.lblScanIcon.Text = " Quét vé:";
            this.lblScanIcon.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlMain
            // 
            this.pnlMain.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(24)))));
            this.pnlMain.Controls.Add(this.pnlTicketInfo);
            this.pnlMain.Controls.Add(this.pnlFeedback);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 106);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20, 20, 20, 10);
            this.pnlMain.Size = new System.Drawing.Size(1432, 457);
            this.pnlMain.TabIndex = 0;
            // 
            // pnlTicketInfo
            // 
            this.pnlTicketInfo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlTicketInfo.BorderColor = System.Drawing.Color.Transparent;
            this.pnlTicketInfo.BorderRadius = 12;
            this.pnlTicketInfo.BorderThickness = 1;
            this.pnlTicketInfo.Controls.Add(this.lblTicketNgayMua);
            this.pnlTicketInfo.Controls.Add(this.lblTicketLuot);
            this.pnlTicketInfo.Controls.Add(this.lblTicketKhuVuc);
            this.pnlTicketInfo.Controls.Add(this.lblTicketTenDV);
            this.pnlTicketInfo.Controls.Add(this.lblTicketMaVe);
            this.pnlTicketInfo.Controls.Add(this.lblTicketVerdict);
            this.pnlTicketInfo.Controls.Add(this.lblTicketHeader);
            this.pnlTicketInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTicketInfo.FillColor = System.Drawing.Color.White;
            this.pnlTicketInfo.Location = new System.Drawing.Point(440, 20);
            this.pnlTicketInfo.Name = "pnlTicketInfo";
            this.pnlTicketInfo.Padding = new System.Windows.Forms.Padding(20, 15, 20, 15);
            this.pnlTicketInfo.Size = new System.Drawing.Size(972, 427);
            this.pnlTicketInfo.TabIndex = 0;
            // 
            // lblTicketNgayMua
            // 
            this.lblTicketNgayMua.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTicketNgayMua.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.lblTicketNgayMua.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(155)))));
            this.lblTicketNgayMua.Location = new System.Drawing.Point(20, 187);
            this.lblTicketNgayMua.Name = "lblTicketNgayMua";
            this.lblTicketNgayMua.Size = new System.Drawing.Size(932, 22);
            this.lblTicketNgayMua.TabIndex = 0;
            // 
            // lblTicketLuot
            // 
            this.lblTicketLuot.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTicketLuot.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTicketLuot.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.lblTicketLuot.Location = new System.Drawing.Point(20, 159);
            this.lblTicketLuot.Name = "lblTicketLuot";
            this.lblTicketLuot.Size = new System.Drawing.Size(932, 28);
            this.lblTicketLuot.TabIndex = 1;
            // 
            // lblTicketKhuVuc
            // 
            this.lblTicketKhuVuc.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTicketKhuVuc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTicketKhuVuc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(155)))));
            this.lblTicketKhuVuc.Location = new System.Drawing.Point(20, 135);
            this.lblTicketKhuVuc.Name = "lblTicketKhuVuc";
            this.lblTicketKhuVuc.Size = new System.Drawing.Size(932, 24);
            this.lblTicketKhuVuc.TabIndex = 2;
            // 
            // lblTicketTenDV
            // 
            this.lblTicketTenDV.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTicketTenDV.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTicketTenDV.ForeColor = System.Drawing.Color.White;
            this.lblTicketTenDV.Location = new System.Drawing.Point(20, 107);
            this.lblTicketTenDV.Name = "lblTicketTenDV";
            this.lblTicketTenDV.Size = new System.Drawing.Size(932, 28);
            this.lblTicketTenDV.TabIndex = 3;
            // 
            // lblTicketMaVe
            // 
            this.lblTicketMaVe.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTicketMaVe.Font = new System.Drawing.Font("Consolas", 13F, System.Drawing.FontStyle.Bold);
            this.lblTicketMaVe.ForeColor = System.Drawing.Color.White;
            this.lblTicketMaVe.Location = new System.Drawing.Point(20, 77);
            this.lblTicketMaVe.Name = "lblTicketMaVe";
            this.lblTicketMaVe.Size = new System.Drawing.Size(932, 30);
            this.lblTicketMaVe.TabIndex = 4;
            this.lblTicketMaVe.Text = "---";
            // 
            // lblTicketVerdict
            // 
            this.lblTicketVerdict.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.lblTicketVerdict.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTicketVerdict.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTicketVerdict.ForeColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblTicketVerdict.Location = new System.Drawing.Point(20, 45);
            this.lblTicketVerdict.Margin = new System.Windows.Forms.Padding(0, 5, 0, 10);
            this.lblTicketVerdict.Name = "lblTicketVerdict";
            this.lblTicketVerdict.Size = new System.Drawing.Size(932, 32);
            this.lblTicketVerdict.TabIndex = 5;
            this.lblTicketVerdict.Text = "N/A";
            this.lblTicketVerdict.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lblTicketHeader
            // 
            this.lblTicketHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTicketHeader.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTicketHeader.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(155)))));
            this.lblTicketHeader.Location = new System.Drawing.Point(20, 15);
            this.lblTicketHeader.Name = "lblTicketHeader";
            this.lblTicketHeader.Size = new System.Drawing.Size(932, 30);
            this.lblTicketHeader.TabIndex = 6;
            this.lblTicketHeader.Text = "🎫 THÔNG TIN VÉ";
            // 
            // pnlFeedback
            // 
            this.pnlFeedback.BorderColor = System.Drawing.Color.WhiteSmoke;
            this.pnlFeedback.BorderRadius = 16;
            this.pnlFeedback.BorderThickness = 2;
            this.pnlFeedback.Controls.Add(this.picCamera);
            this.pnlFeedback.Controls.Add(this.lblStatusSub);
            this.pnlFeedback.Controls.Add(this.lblStatusTitle);
            this.pnlFeedback.Controls.Add(this.picStatus);
            this.pnlFeedback.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlFeedback.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(224)))), ((int)(((byte)(224)))), ((int)(((byte)(224)))));
            this.pnlFeedback.Location = new System.Drawing.Point(20, 20);
            this.pnlFeedback.Margin = new System.Windows.Forms.Padding(0, 0, 15, 0);
            this.pnlFeedback.Name = "pnlFeedback";
            this.pnlFeedback.Size = new System.Drawing.Size(420, 427);
            this.pnlFeedback.TabIndex = 1;
            // 
            // lblStatusSub
            // 
            this.lblStatusSub.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatusSub.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblStatusSub.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(155)))));
            this.lblStatusSub.Location = new System.Drawing.Point(0, 205);
            this.lblStatusSub.Name = "lblStatusSub";
            this.lblStatusSub.Size = new System.Drawing.Size(420, 35);
            this.lblStatusSub.TabIndex = 0;
            this.lblStatusSub.Text = "Quét mã Barcode / QR trên vé";
            this.lblStatusSub.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblStatusTitle.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblStatusTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(155)))));
            this.lblStatusTitle.Location = new System.Drawing.Point(0, 140);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(420, 65);
            this.lblStatusTitle.TabIndex = 1;
            this.lblStatusTitle.Text = "SẴN SÀNG";
            this.lblStatusTitle.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // picStatus
            // 
            this.picStatus.BackColor = System.Drawing.Color.Black;
            this.picStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.picStatus.Location = new System.Drawing.Point(0, 0);
            this.picStatus.Name = "picStatus";
            this.picStatus.Size = new System.Drawing.Size(420, 140);
            this.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.picStatus.TabIndex = 2;
            this.picStatus.TabStop = false;
            // 
            // lblCounter
            // 
            this.lblCounter.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(26)))), ((int)(((byte)(36)))));
            this.lblCounter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblCounter.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCounter.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(155)))));
            this.lblCounter.Location = new System.Drawing.Point(0, 723);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Size = new System.Drawing.Size(1432, 32);
            this.lblCounter.TabIndex = 2;
            this.lblCounter.Text = "   ✅ Hợp lệ: 0     ❌ Từ chối: 0     📊 Tổng quét: 0";
            this.lblCounter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // lstHistory
            // 
            this.lstHistory.BackColor = System.Drawing.Color.Gainsboro;
            this.lstHistory.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstHistory.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.colTime,
            this.colCode,
            this.colStatus});
            this.lstHistory.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lstHistory.Font = new System.Drawing.Font("Consolas", 9.5F);
            this.lstHistory.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(155)))));
            this.lstHistory.FullRowSelect = true;
            this.lstHistory.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lstHistory.HideSelection = false;
            this.lstHistory.Location = new System.Drawing.Point(0, 563);
            this.lstHistory.Name = "lstHistory";
            this.lstHistory.Size = new System.Drawing.Size(1432, 160);
            this.lstHistory.TabIndex = 1;
            this.lstHistory.UseCompatibleStateImageBehavior = false;
            this.lstHistory.View = System.Windows.Forms.View.Details;
            // 
            // colTime
            // 
            this.colTime.Text = "Giờ";
            this.colTime.Width = 90;
            // 
            // colCode
            // 
            this.colCode.Text = "Mã Vé";
            this.colCode.Width = 280;
            // 
            // colStatus
            // 
            this.colStatus.Text = "Kết Quả";
            this.colStatus.Width = 200;
            // 
            // txtScanner
            // 
            this.txtScanner.BorderThickness = 0;
            this.txtScanner.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtScanner.DefaultText = "";
            this.txtScanner.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(24)))));
            this.txtScanner.Font = new System.Drawing.Font("Segoe UI", 1F);
            this.txtScanner.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(24)))));
            this.txtScanner.Location = new System.Drawing.Point(0, 0);
            this.txtScanner.Margin = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.txtScanner.Name = "txtScanner";
            this.txtScanner.PlaceholderText = "";
            this.txtScanner.SelectedText = "";
            this.txtScanner.Size = new System.Drawing.Size(1, 1);
            this.txtScanner.TabIndex = 0;
            this.txtScanner.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtScanner_KeyDown);
            // 
            // timerReset
            // 
            this.timerReset.Interval = 4000;
            this.timerReset.Tick += new System.EventHandler(this.timerReset_Tick);
            // 
            // timerFlash
            // 
            this.timerFlash.Interval = 120;
            this.timerFlash.Tick += new System.EventHandler(this.timerFlash_Tick);
            // 
            // frmKiemSoatVe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1432, 755);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.lstHistory);
            this.Controls.Add(this.lblCounter);
            this.Controls.Add(this.pnlScannerBar);
            this.Controls.Add(this.pnlHeader);
            this.Controls.Add(this.txtScanner);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmKiemSoatVe";
            this.Text = "KIỂM SOÁT VÉ";
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picGateIcon)).EndInit();
            this.pnlScannerBar.ResumeLayout(false);
            this.pnlMain.ResumeLayout(false);
            this.pnlTicketInfo.ResumeLayout(false);
            this.pnlFeedback.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.PictureBox picGateIcon;
        private System.Windows.Forms.Label lblGateTitle;
        private System.Windows.Forms.ComboBox cboKhuVuc;
        private System.Windows.Forms.ComboBox cboTroChoi;

        private System.Windows.Forms.Panel pnlMain;
        private Guna.UI2.WinForms.Guna2Panel pnlFeedback;
        private System.Windows.Forms.PictureBox picStatus;
        private System.Windows.Forms.Label lblStatusTitle;
        private System.Windows.Forms.Label lblStatusSub;

        private Guna.UI2.WinForms.Guna2Panel pnlTicketInfo;
        private System.Windows.Forms.Label lblTicketHeader;
        private System.Windows.Forms.Label lblTicketMaVe;
        private System.Windows.Forms.Label lblTicketTenDV;
        private System.Windows.Forms.Label lblTicketKhuVuc;
        private System.Windows.Forms.Label lblTicketLuot;
        private System.Windows.Forms.Label lblTicketNgayMua;
        private System.Windows.Forms.Label lblTicketVerdict;

        private System.Windows.Forms.Label lblCounter;
        private System.Windows.Forms.ListView lstHistory;
        private System.Windows.Forms.ColumnHeader colTime;
        private System.Windows.Forms.ColumnHeader colCode;
        private System.Windows.Forms.ColumnHeader colStatus;

        private Guna.UI2.WinForms.Guna2TextBox txtScanner;
        private System.Windows.Forms.Panel pnlScannerBar;
        private System.Windows.Forms.Label lblScanIcon;
        private Guna.UI2.WinForms.Guna2TextBox txtScanInput;
        private System.Windows.Forms.Timer timerReset;
        private System.Windows.Forms.Timer timerFlash;
        private System.Windows.Forms.PictureBox picCamera;
        private Guna.UI2.WinForms.Guna2Button btnToggleCamera;
        private Guna.UI2.WinForms.Guna2Button btnScanFile;
    }
}
