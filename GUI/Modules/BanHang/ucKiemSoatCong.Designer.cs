using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace GUI.Modules.BanHang
{
    partial class ucKiemSoatCong
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlBanner = new DevExpress.XtraEditors.PanelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblNhanVien = new DevExpress.XtraEditors.LabelControl();
            this.cboKhuVuc = new DevExpress.XtraEditors.LookUpEdit();
            this.cboTroChoi = new DevExpress.XtraEditors.LookUpEdit();
            this.pnlScannerBar = new DevExpress.XtraEditors.PanelControl();
            this.lblScanIcon = new DevExpress.XtraEditors.LabelControl();
            this.txtScanInput = new DevExpress.XtraEditors.TextEdit();
            this.btnToggleCamera = new DevExpress.XtraEditors.SimpleButton();
            this.btnScanFile = new DevExpress.XtraEditors.SimpleButton();
            this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnlFeedback = new DevExpress.XtraEditors.PanelControl();
            this.picStatus = new System.Windows.Forms.PictureBox();
            this.lblStatusTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblStatusSub = new DevExpress.XtraEditors.LabelControl();
            this.picCamera = new System.Windows.Forms.PictureBox();
            this.pnlTicketInfo = new DevExpress.XtraEditors.PanelControl();
            this.grpTicketInfo = new DevExpress.XtraEditors.GroupControl();
            this.lblVerdict = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketMaVe = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketTenVe = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketLoaiVe = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketKhuVuc = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketLuot = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketNgayMua = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketHetHan = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketKhach = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketRFID = new DevExpress.XtraEditors.LabelControl();
            this.gridLichSu = new DevExpress.XtraGrid.GridControl();
            this.viewLichSu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGio = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaVe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKetQua = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenVe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKhuVuc = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLuotConLai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlCounter = new DevExpress.XtraEditors.PanelControl();
            this.lblCounter = new DevExpress.XtraEditors.LabelControl();
            this.timerReset = new System.Windows.Forms.Timer(this.components);
            this.timerFlash = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBanner)).BeginInit();
            this.pnlBanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboKhuVuc.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTroChoi.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlScannerBar)).BeginInit();
            this.pnlScannerBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtScanInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).BeginInit();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFeedback)).BeginInit();
            this.pnlFeedback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTicketInfo)).BeginInit();
            this.pnlTicketInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpTicketInfo)).BeginInit();
            this.grpTicketInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLichSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewLichSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCounter)).BeginInit();
            this.pnlCounter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBanner
            // 
            this.pnlBanner.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlBanner.Appearance.Options.UseBackColor = true;
            this.pnlBanner.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBanner.Controls.Add(this.lblTitle);
            this.pnlBanner.Controls.Add(this.lblNhanVien);
            this.pnlBanner.Controls.Add(this.cboKhuVuc);
            this.pnlBanner.Controls.Add(this.cboTroChoi);
            this.pnlBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBanner.Location = new System.Drawing.Point(0, 0);
            this.pnlBanner.Name = "pnlBanner";
            this.pnlBanner.Size = new System.Drawing.Size(1200, 50);
            this.pnlBanner.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.lblTitle.Size = new System.Drawing.Size(342, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CONG KIEM SOAT VE DAI NAM";
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblNhanVien.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(225)))), ((int)(((byte)(235)))));
            this.lblNhanVien.Appearance.Options.UseFont = true;
            this.lblNhanVien.Appearance.Options.UseForeColor = true;
            this.lblNhanVien.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblNhanVien.Location = new System.Drawing.Point(1130, 0);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Padding = new System.Windows.Forms.Padding(10, 15, 15, 10);
            this.lblNhanVien.Size = new System.Drawing.Size(70, 45);
            this.lblNhanVien.TabIndex = 1;
            this.lblNhanVien.Text = "NV: ---";
            // 
            // cboKhuVuc
            // 
            this.cboKhuVuc.Location = new System.Drawing.Point(400, 13);
            this.cboKhuVuc.Name = "cboKhuVuc";
            this.cboKhuVuc.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboKhuVuc.Properties.Appearance.Options.UseFont = true;
            this.cboKhuVuc.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboKhuVuc.Properties.NullText = "[Khu vuc]";
            this.cboKhuVuc.Size = new System.Drawing.Size(220, 26);
            this.cboKhuVuc.TabIndex = 2;
            this.cboKhuVuc.EditValueChanged += new System.EventHandler(this.CboKhuVuc_EditValueChanged);
            // 
            // cboTroChoi
            // 
            this.cboTroChoi.Location = new System.Drawing.Point(640, 13);
            this.cboTroChoi.Name = "cboTroChoi";
            this.cboTroChoi.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.cboTroChoi.Properties.Appearance.Options.UseFont = true;
            this.cboTroChoi.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboTroChoi.Properties.NullText = "[Tro choi ▾ Cong chung]";
            this.cboTroChoi.Size = new System.Drawing.Size(250, 26);
            this.cboTroChoi.TabIndex = 3;
            this.cboTroChoi.EditValueChanged += new System.EventHandler(this.CboTroChoi_EditValueChanged);
            // 
            // pnlScannerBar
            // 
            this.pnlScannerBar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlScannerBar.Appearance.Options.UseBackColor = true;
            this.pnlScannerBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlScannerBar.Controls.Add(this.lblScanIcon);
            this.pnlScannerBar.Controls.Add(this.txtScanInput);
            this.pnlScannerBar.Controls.Add(this.btnToggleCamera);
            this.pnlScannerBar.Controls.Add(this.btnScanFile);
            this.pnlScannerBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlScannerBar.Location = new System.Drawing.Point(0, 50);
            this.pnlScannerBar.Name = "pnlScannerBar";
            this.pnlScannerBar.Size = new System.Drawing.Size(1200, 50);
            this.pnlScannerBar.TabIndex = 1;
            // 
            // lblScanIcon
            // 
            this.lblScanIcon.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblScanIcon.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblScanIcon.Appearance.Options.UseFont = true;
            this.lblScanIcon.Appearance.Options.UseForeColor = true;
            this.lblScanIcon.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblScanIcon.Location = new System.Drawing.Point(0, 0);
            this.lblScanIcon.Name = "lblScanIcon";
            this.lblScanIcon.Padding = new System.Windows.Forms.Padding(15, 12, 10, 10);
            this.lblScanIcon.Size = new System.Drawing.Size(88, 43);
            this.lblScanIcon.TabIndex = 0;
            this.lblScanIcon.Text = "Quet ve:";
            // 
            // txtScanInput
            // 
            this.txtScanInput.Location = new System.Drawing.Point(110, 10);
            this.txtScanInput.Name = "txtScanInput";
            this.txtScanInput.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtScanInput.Properties.Appearance.Options.UseFont = true;
            this.txtScanInput.Properties.NullValuePrompt = "Nhap ma hoac quet barcode -> Enter...";
            this.txtScanInput.Size = new System.Drawing.Size(800, 28);
            this.txtScanInput.TabIndex = 1;
            this.txtScanInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtScanInput_KeyDown);
            // 
            // btnToggleCamera
            // 
            this.btnToggleCamera.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnToggleCamera.Appearance.Options.UseFont = true;
            this.btnToggleCamera.Location = new System.Drawing.Point(920, 10);
            this.btnToggleCamera.Name = "btnToggleCamera";
            this.btnToggleCamera.Size = new System.Drawing.Size(100, 28);
            this.btnToggleCamera.TabIndex = 2;
            this.btnToggleCamera.Text = "Bat Cam";
            this.btnToggleCamera.Click += new System.EventHandler(this.BtnToggleCamera_Click);
            // 
            // btnScanFile
            // 
            this.btnScanFile.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnScanFile.Appearance.Options.UseFont = true;
            this.btnScanFile.Location = new System.Drawing.Point(1030, 10);
            this.btnScanFile.Name = "btnScanFile";
            this.btnScanFile.Size = new System.Drawing.Size(36, 28);
            this.btnScanFile.TabIndex = 3;
            this.btnScanFile.Text = "...";
            this.btnScanFile.Click += new System.EventHandler(this.BtnScanFile_Click);
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 100);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.pnlFeedback);
            this.splitMain.Panel1.Text = "Panel1";
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.pnlTicketInfo);
            this.splitMain.Panel2.Text = "Panel2";
            this.splitMain.Size = new System.Drawing.Size(1200, 468);
            this.splitMain.SplitterPosition = 420;
            this.splitMain.TabIndex = 2;
            // 
            // pnlFeedback
            // 
            this.pnlFeedback.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(6)))), ((int)(((byte)(23)))));
            this.pnlFeedback.Appearance.Options.UseBackColor = true;
            this.pnlFeedback.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFeedback.Controls.Add(this.picStatus);
            this.pnlFeedback.Controls.Add(this.lblStatusTitle);
            this.pnlFeedback.Controls.Add(this.lblStatusSub);
            this.pnlFeedback.Controls.Add(this.picCamera);
            this.pnlFeedback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFeedback.Location = new System.Drawing.Point(0, 0);
            this.pnlFeedback.Name = "pnlFeedback";
            this.pnlFeedback.Size = new System.Drawing.Size(420, 468);
            this.pnlFeedback.TabIndex = 0;
            // 
            // picStatus
            // 
            this.picStatus.BackColor = System.Drawing.Color.Transparent;
            this.picStatus.Location = new System.Drawing.Point(150, 20);
            this.picStatus.Name = "picStatus";
            this.picStatus.Size = new System.Drawing.Size(120, 120);
            this.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picStatus.TabIndex = 0;
            this.picStatus.TabStop = false;
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 28F, System.Drawing.FontStyle.Bold);
            this.lblStatusTitle.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblStatusTitle.Appearance.Options.UseFont = true;
            this.lblStatusTitle.Appearance.Options.UseForeColor = true;
            this.lblStatusTitle.Appearance.Options.UseTextOptions = true;
            this.lblStatusTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblStatusTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblStatusTitle.Location = new System.Drawing.Point(10, 150);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(400, 65);
            this.lblStatusTitle.TabIndex = 1;
            this.lblStatusTitle.Text = "MOI VAO";
            // 
            // lblStatusSub
            // 
            this.lblStatusSub.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.lblStatusSub.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(130)))), ((int)(((byte)(153)))));
            this.lblStatusSub.Appearance.Options.UseFont = true;
            this.lblStatusSub.Appearance.Options.UseForeColor = true;
            this.lblStatusSub.Appearance.Options.UseTextOptions = true;
            this.lblStatusSub.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblStatusSub.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblStatusSub.Location = new System.Drawing.Point(10, 220);
            this.lblStatusSub.Name = "lblStatusSub";
            this.lblStatusSub.Size = new System.Drawing.Size(400, 35);
            this.lblStatusSub.TabIndex = 2;
            this.lblStatusSub.Text = "---";
            // 
            // picCamera
            // 
            this.picCamera.BackColor = System.Drawing.Color.Black;
            this.picCamera.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picCamera.Location = new System.Drawing.Point(0, 288);
            this.picCamera.Name = "picCamera";
            this.picCamera.Size = new System.Drawing.Size(420, 180);
            this.picCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCamera.TabIndex = 3;
            this.picCamera.TabStop = false;
            this.picCamera.Visible = false;
            // 
            // pnlTicketInfo
            // 
            this.pnlTicketInfo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlTicketInfo.Controls.Add(this.grpTicketInfo);
            this.pnlTicketInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTicketInfo.Location = new System.Drawing.Point(0, 0);
            this.pnlTicketInfo.Name = "pnlTicketInfo";
            this.pnlTicketInfo.Size = new System.Drawing.Size(770, 468);
            this.pnlTicketInfo.TabIndex = 0;
            // 
            // grpTicketInfo
            // 
            this.grpTicketInfo.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.grpTicketInfo.Appearance.Options.UseBackColor = true;
            this.grpTicketInfo.Controls.Add(this.lblVerdict);
            this.grpTicketInfo.Controls.Add(this.lblTicketMaVe);
            this.grpTicketInfo.Controls.Add(this.lblTicketTenVe);
            this.grpTicketInfo.Controls.Add(this.lblTicketLoaiVe);
            this.grpTicketInfo.Controls.Add(this.lblTicketKhuVuc);
            this.grpTicketInfo.Controls.Add(this.lblTicketLuot);
            this.grpTicketInfo.Controls.Add(this.lblTicketNgayMua);
            this.grpTicketInfo.Controls.Add(this.lblTicketHetHan);
            this.grpTicketInfo.Controls.Add(this.lblTicketKhach);
            this.grpTicketInfo.Controls.Add(this.lblTicketRFID);
            this.grpTicketInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTicketInfo.Location = new System.Drawing.Point(0, 0);
            this.grpTicketInfo.Name = "grpTicketInfo";
            this.grpTicketInfo.Size = new System.Drawing.Size(770, 468);
            this.grpTicketInfo.TabIndex = 0;
            this.grpTicketInfo.Text = "THONG TIN VE";
            // 
            // lblVerdict
            // 
            this.lblVerdict.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.lblVerdict.Appearance.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblVerdict.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblVerdict.Appearance.Options.UseBackColor = true;
            this.lblVerdict.Appearance.Options.UseFont = true;
            this.lblVerdict.Appearance.Options.UseForeColor = true;
            this.lblVerdict.Appearance.Options.UseTextOptions = true;
            this.lblVerdict.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblVerdict.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblVerdict.Location = new System.Drawing.Point(20, 40);
            this.lblVerdict.Name = "lblVerdict";
            this.lblVerdict.Size = new System.Drawing.Size(300, 40);
            this.lblVerdict.TabIndex = 0;
            this.lblVerdict.Text = "HOP LE";
            // 
            // lblTicketMaVe
            // 
            this.lblTicketMaVe.Appearance.Font = new System.Drawing.Font("Consolas", 13F, System.Drawing.FontStyle.Bold);
            this.lblTicketMaVe.Appearance.Options.UseFont = true;
            this.lblTicketMaVe.Location = new System.Drawing.Point(20, 100);
            this.lblTicketMaVe.Name = "lblTicketMaVe";
            this.lblTicketMaVe.Size = new System.Drawing.Size(90, 20);
            this.lblTicketMaVe.TabIndex = 1;
            this.lblTicketMaVe.Text = "Ma ve: ---";
            // 
            // lblTicketTenVe
            // 
            this.lblTicketTenVe.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTicketTenVe.Appearance.Options.UseFont = true;
            this.lblTicketTenVe.Location = new System.Drawing.Point(20, 130);
            this.lblTicketTenVe.Name = "lblTicketTenVe";
            this.lblTicketTenVe.Size = new System.Drawing.Size(68, 20);
            this.lblTicketTenVe.TabIndex = 2;
            this.lblTicketTenVe.Text = "Ten ve: ---";
            // 
            // lblTicketLoaiVe
            // 
            this.lblTicketLoaiVe.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Italic);
            this.lblTicketLoaiVe.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblTicketLoaiVe.Appearance.Options.UseFont = true;
            this.lblTicketLoaiVe.Appearance.Options.UseForeColor = true;
            this.lblTicketLoaiVe.Location = new System.Drawing.Point(20, 160);
            this.lblTicketLoaiVe.Name = "lblTicketLoaiVe";
            this.lblTicketLoaiVe.Size = new System.Drawing.Size(70, 20);
            this.lblTicketLoaiVe.TabIndex = 3;
            this.lblTicketLoaiVe.Text = "Loai ve: ---";
            // 
            // lblTicketKhuVuc
            // 
            this.lblTicketKhuVuc.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTicketKhuVuc.Appearance.Options.UseFont = true;
            this.lblTicketKhuVuc.Location = new System.Drawing.Point(20, 190);
            this.lblTicketKhuVuc.Name = "lblTicketKhuVuc";
            this.lblTicketKhuVuc.Size = new System.Drawing.Size(76, 20);
            this.lblTicketKhuVuc.TabIndex = 4;
            this.lblTicketKhuVuc.Text = "Khu vuc: ---";
            // 
            // lblTicketLuot
            // 
            this.lblTicketLuot.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTicketLuot.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(137)))), ((int)(((byte)(123)))));
            this.lblTicketLuot.Appearance.Options.UseFont = true;
            this.lblTicketLuot.Appearance.Options.UseForeColor = true;
            this.lblTicketLuot.Location = new System.Drawing.Point(20, 220);
            this.lblTicketLuot.Name = "lblTicketLuot";
            this.lblTicketLuot.Size = new System.Drawing.Size(87, 20);
            this.lblTicketLuot.TabIndex = 5;
            this.lblTicketLuot.Text = "Luot con: ---";
            // 
            // lblTicketNgayMua
            // 
            this.lblTicketNgayMua.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTicketNgayMua.Appearance.Options.UseFont = true;
            this.lblTicketNgayMua.Location = new System.Drawing.Point(20, 250);
            this.lblTicketNgayMua.Name = "lblTicketNgayMua";
            this.lblTicketNgayMua.Size = new System.Drawing.Size(93, 20);
            this.lblTicketNgayMua.TabIndex = 6;
            this.lblTicketNgayMua.Text = "Ngay mua: ---";
            // 
            // lblTicketHetHan
            // 
            this.lblTicketHetHan.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTicketHetHan.Appearance.Options.UseFont = true;
            this.lblTicketHetHan.Location = new System.Drawing.Point(20, 280);
            this.lblTicketHetHan.Name = "lblTicketHetHan";
            this.lblTicketHetHan.Size = new System.Drawing.Size(77, 20);
            this.lblTicketHetHan.TabIndex = 7;
            this.lblTicketHetHan.Text = "Het han: ---";
            // 
            // lblTicketKhach
            // 
            this.lblTicketKhach.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTicketKhach.Appearance.Options.UseFont = true;
            this.lblTicketKhach.Location = new System.Drawing.Point(20, 310);
            this.lblTicketKhach.Name = "lblTicketKhach";
            this.lblTicketKhach.Size = new System.Drawing.Size(102, 20);
            this.lblTicketKhach.TabIndex = 8;
            this.lblTicketKhach.Text = "Khach hang: ---";
            // 
            // lblTicketRFID
            // 
            this.lblTicketRFID.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTicketRFID.Appearance.Options.UseFont = true;
            this.lblTicketRFID.Location = new System.Drawing.Point(20, 340);
            this.lblTicketRFID.Name = "lblTicketRFID";
            this.lblTicketRFID.Size = new System.Drawing.Size(56, 20);
            this.lblTicketRFID.TabIndex = 9;
            this.lblTicketRFID.Text = "RFID: ---";
            // 
            // gridLichSu
            // 
            this.gridLichSu.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gridLichSu.Location = new System.Drawing.Point(0, 568);
            this.gridLichSu.MainView = this.viewLichSu;
            this.gridLichSu.Name = "gridLichSu";
            this.gridLichSu.Size = new System.Drawing.Size(1200, 200);
            this.gridLichSu.TabIndex = 3;
            this.gridLichSu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewLichSu});
            // 
            // viewLichSu
            // 
            this.viewLichSu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGio,
            this.colMaVe,
            this.colKetQua,
            this.colTenVe,
            this.colKhuVuc,
            this.colLuotConLai});
            this.viewLichSu.GridControl = this.gridLichSu;
            this.viewLichSu.Name = "viewLichSu";
            this.viewLichSu.OptionsBehavior.Editable = false;
            this.viewLichSu.OptionsView.ShowGroupPanel = false;
            this.viewLichSu.OptionsView.ShowIndicator = false;
            // 
            // colGio
            // 
            this.colGio.Caption = "Gio";
            this.colGio.DisplayFormat.FormatString = "HH:mm:ss";
            this.colGio.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colGio.FieldName = "ThoiGian";
            this.colGio.Name = "colGio";
            this.colGio.Visible = true;
            this.colGio.VisibleIndex = 0;
            this.colGio.Width = 80;
            // 
            // colMaVe
            // 
            this.colMaVe.Caption = "Ma Ve";
            this.colMaVe.FieldName = "MaVach";
            this.colMaVe.Name = "colMaVe";
            this.colMaVe.Visible = true;
            this.colMaVe.VisibleIndex = 1;
            this.colMaVe.Width = 220;
            // 
            // colKetQua
            // 
            this.colKetQua.Caption = "Ket Qua";
            this.colKetQua.FieldName = "KetQua";
            this.colKetQua.Name = "colKetQua";
            this.colKetQua.Visible = true;
            this.colKetQua.VisibleIndex = 2;
            this.colKetQua.Width = 100;
            // 
            // colTenVe
            // 
            this.colTenVe.Caption = "Ten Ve";
            this.colTenVe.FieldName = "TenSanPham";
            this.colTenVe.Name = "colTenVe";
            this.colTenVe.Visible = true;
            this.colTenVe.VisibleIndex = 3;
            this.colTenVe.Width = 180;
            // 
            // colKhuVuc
            // 
            this.colKhuVuc.Caption = "Khu Vuc";
            this.colKhuVuc.FieldName = "TenKhuVuc";
            this.colKhuVuc.Name = "colKhuVuc";
            this.colKhuVuc.Visible = true;
            this.colKhuVuc.VisibleIndex = 4;
            this.colKhuVuc.Width = 120;
            // 
            // colLuotConLai
            // 
            this.colLuotConLai.Caption = "Luot Con";
            this.colLuotConLai.FieldName = "SoLuotConLai";
            this.colLuotConLai.Name = "colLuotConLai";
            this.colLuotConLai.Visible = true;
            this.colLuotConLai.VisibleIndex = 5;
            this.colLuotConLai.Width = 70;
            // 
            // pnlCounter
            // 
            this.pnlCounter.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(24)))));
            this.pnlCounter.Appearance.Options.UseBackColor = true;
            this.pnlCounter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlCounter.Controls.Add(this.lblCounter);
            this.pnlCounter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlCounter.Location = new System.Drawing.Point(0, 768);
            this.pnlCounter.Name = "pnlCounter";
            this.pnlCounter.Size = new System.Drawing.Size(1200, 32);
            this.pnlCounter.TabIndex = 4;
            // 
            // lblCounter
            // 
            this.lblCounter.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblCounter.Appearance.ForeColor = System.Drawing.Color.White;
            this.lblCounter.Appearance.Options.UseFont = true;
            this.lblCounter.Appearance.Options.UseForeColor = true;
            this.lblCounter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblCounter.Location = new System.Drawing.Point(0, 0);
            this.lblCounter.Name = "lblCounter";
            this.lblCounter.Padding = new System.Windows.Forms.Padding(15, 5, 15, 5);
            this.lblCounter.Size = new System.Drawing.Size(289, 30);
            this.lblCounter.TabIndex = 0;
            this.lblCounter.Text = "Hop le: 0     Tu choi: 0     Tong quet: 0";
            // 
            // timerReset
            // 
            this.timerReset.Interval = 4000;
            this.timerReset.Tick += new System.EventHandler(this.TimerReset_Tick);
            // 
            // timerFlash
            // 
            this.timerFlash.Interval = 120;
            this.timerFlash.Tick += new System.EventHandler(this.TimerFlash_Tick);
            // 
            // ucKiemSoatCong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.gridLichSu);
            this.Controls.Add(this.pnlCounter);
            this.Controls.Add(this.pnlScannerBar);
            this.Controls.Add(this.pnlBanner);
            this.Name = "ucKiemSoatCong";
            this.Size = new System.Drawing.Size(1200, 800);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBanner)).EndInit();
            this.pnlBanner.ResumeLayout(false);
            this.pnlBanner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboKhuVuc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTroChoi.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlScannerBar)).EndInit();
            this.pnlScannerBar.ResumeLayout(false);
            this.pnlScannerBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtScanInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).EndInit();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFeedback)).EndInit();
            this.pnlFeedback.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTicketInfo)).EndInit();
            this.pnlTicketInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpTicketInfo)).EndInit();
            this.grpTicketInfo.ResumeLayout(false);
            this.grpTicketInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridLichSu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewLichSu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCounter)).EndInit();
            this.pnlCounter.ResumeLayout(false);
            this.pnlCounter.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlBanner;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblNhanVien;
        private DevExpress.XtraEditors.LookUpEdit cboKhuVuc;
        private DevExpress.XtraEditors.LookUpEdit cboTroChoi;
        
        private DevExpress.XtraEditors.PanelControl pnlScannerBar;
        private DevExpress.XtraEditors.LabelControl lblScanIcon;
        private DevExpress.XtraEditors.TextEdit txtScanInput;
        private DevExpress.XtraEditors.SimpleButton btnToggleCamera;
        private DevExpress.XtraEditors.SimpleButton btnScanFile;
        
        private DevExpress.XtraEditors.SplitContainerControl splitMain;
        private DevExpress.XtraEditors.PanelControl pnlFeedback;
        private System.Windows.Forms.PictureBox picStatus;
        private DevExpress.XtraEditors.LabelControl lblStatusTitle;
        private DevExpress.XtraEditors.LabelControl lblStatusSub;
        private System.Windows.Forms.PictureBox picCamera;
        
        private DevExpress.XtraEditors.PanelControl pnlTicketInfo;
        private DevExpress.XtraEditors.GroupControl grpTicketInfo;
        private DevExpress.XtraEditors.LabelControl lblVerdict;
        private DevExpress.XtraEditors.LabelControl lblTicketMaVe;
        private DevExpress.XtraEditors.LabelControl lblTicketTenVe;
        private DevExpress.XtraEditors.LabelControl lblTicketLoaiVe;
        private DevExpress.XtraEditors.LabelControl lblTicketKhuVuc;
        private DevExpress.XtraEditors.LabelControl lblTicketLuot;
        private DevExpress.XtraEditors.LabelControl lblTicketNgayMua;
        private DevExpress.XtraEditors.LabelControl lblTicketHetHan;
        private DevExpress.XtraEditors.LabelControl lblTicketKhach;
        private DevExpress.XtraEditors.LabelControl lblTicketRFID;
        
        private DevExpress.XtraGrid.GridControl gridLichSu;
        private DevExpress.XtraGrid.Views.Grid.GridView viewLichSu;
        private DevExpress.XtraGrid.Columns.GridColumn colGio;
        private DevExpress.XtraGrid.Columns.GridColumn colMaVe;
        private DevExpress.XtraGrid.Columns.GridColumn colKetQua;
        private DevExpress.XtraGrid.Columns.GridColumn colTenVe;
        private DevExpress.XtraGrid.Columns.GridColumn colKhuVuc;
        private DevExpress.XtraGrid.Columns.GridColumn colLuotConLai;
        
        private DevExpress.XtraEditors.PanelControl pnlCounter;
        private DevExpress.XtraEditors.LabelControl lblCounter;
        
        private System.Windows.Forms.Timer timerReset;
        private System.Windows.Forms.Timer timerFlash;
    }
}
