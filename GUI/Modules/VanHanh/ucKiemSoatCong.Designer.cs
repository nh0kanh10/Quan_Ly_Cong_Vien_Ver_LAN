using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace GUI.Modules.VanHanh
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
            if (disposing)
            {
                if (_onLanguageChanged != null)
                {
                    GUI.Infrastructure.EventBus.Unsubscribe("LanguageChanged", _onLanguageChanged);
                }
                if (components != null)
                {
                    components.Dispose();
                }
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
            this.pnlKPI = new System.Windows.Forms.TableLayoutPanel();
            this.pnlKPI_Guests = new DevExpress.XtraEditors.PanelControl();
            this.lblKPI_GuestsSub = new DevExpress.XtraEditors.LabelControl();
            this.lblKPI_GuestsValue = new DevExpress.XtraEditors.LabelControl();
            this.lblKPI_GuestsTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnlKPI_Success = new DevExpress.XtraEditors.PanelControl();
            this.lblKPI_SuccessSub = new DevExpress.XtraEditors.LabelControl();
            this.lblKPI_SuccessValue = new DevExpress.XtraEditors.LabelControl();
            this.lblKPI_SuccessTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnlKPI_Rejected = new DevExpress.XtraEditors.PanelControl();
            this.lblKPI_RejectedSub = new DevExpress.XtraEditors.LabelControl();
            this.lblKPI_RejectedValue = new DevExpress.XtraEditors.LabelControl();
            this.lblKPI_RejectedTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnlScannerBar = new DevExpress.XtraEditors.PanelControl();
            this.txtScanInput = new DevExpress.XtraEditors.TextEdit();
            this.btnToggleCamera = new DevExpress.XtraEditors.SimpleButton();
            this.btnScanFile = new DevExpress.XtraEditors.SimpleButton();
            this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.tlpLeftContent = new System.Windows.Forms.TableLayoutPanel();
            this.pnlFeedback = new DevExpress.XtraEditors.PanelControl();
            this.lblStatusTitle = new DevExpress.XtraEditors.LabelControl();
            this.picStatus = new System.Windows.Forms.PictureBox();
            this.lblStatusSub = new DevExpress.XtraEditors.LabelControl();
            this.pnlTicketInfo = new DevExpress.XtraEditors.PanelControl();
            this.grpTicketInfo = new DevExpress.XtraEditors.GroupControl();
            this.tlpTicketDetails = new System.Windows.Forms.TableLayoutPanel();
            this.lblTitle_MaVe = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketMaVe = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle_TenVe = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketTenVe = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle_LoaiVe = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketLoaiVe = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle_KhuVuc = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketKhuVuc = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle_Luot = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketLuot = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle_NgayMua = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketNgayMua = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle_HetHan = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketHetHan = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle_Khach = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketKhach = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle_RFID = new DevExpress.XtraEditors.LabelControl();
            this.lblTicketRFID = new DevExpress.XtraEditors.LabelControl();
            this.pnlTicketHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblTicketHeaderTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblVerdict = new DevExpress.XtraEditors.LabelControl();
            this.picCamera = new System.Windows.Forms.PictureBox();
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
            this.pnlKPI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlKPI_Guests)).BeginInit();
            this.pnlKPI_Guests.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlKPI_Success)).BeginInit();
            this.pnlKPI_Success.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlKPI_Rejected)).BeginInit();
            this.pnlKPI_Rejected.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlScannerBar)).BeginInit();
            this.pnlScannerBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtScanInput.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).BeginInit();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.tlpLeftContent.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFeedback)).BeginInit();
            this.pnlFeedback.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTicketInfo)).BeginInit();
            this.pnlTicketInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grpTicketInfo)).BeginInit();
            this.grpTicketInfo.SuspendLayout();
            this.tlpTicketDetails.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTicketHeader)).BeginInit();
            this.pnlTicketHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLichSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewLichSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCounter)).BeginInit();
            this.pnlCounter.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBanner
            // 
            this.pnlBanner.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
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
            this.lblTitle.Size = new System.Drawing.Size(351, 50);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CỔNG KIỂM SOÁT VÉ ĐẠI NAM";
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
            this.cboKhuVuc.Properties.NullText = "[Khu vực]";
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
            this.cboTroChoi.Properties.NullText = "[Trò chơi ▾ Cổng chung]";
            this.cboTroChoi.Size = new System.Drawing.Size(250, 26);
            this.cboTroChoi.TabIndex = 3;
            this.cboTroChoi.EditValueChanged += new System.EventHandler(this.CboTroChoi_EditValueChanged);
            // 
            // pnlKPI
            // 
            this.pnlKPI.ColumnCount = 3;
            this.pnlKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.pnlKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.pnlKPI.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 33.33F));
            this.pnlKPI.Controls.Add(this.pnlKPI_Guests, 0, 0);
            this.pnlKPI.Controls.Add(this.pnlKPI_Success, 1, 0);
            this.pnlKPI.Controls.Add(this.pnlKPI_Rejected, 2, 0);
            this.pnlKPI.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKPI.Location = new System.Drawing.Point(0, 50);
            this.pnlKPI.Name = "pnlKPI";
            this.pnlKPI.RowCount = 1;
            this.pnlKPI.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.pnlKPI.Size = new System.Drawing.Size(1200, 78);
            this.pnlKPI.TabIndex = 5;
            // 
            // pnlKPI_Guests
            // 
            this.pnlKPI_Guests.Appearance.BackColor = System.Drawing.Color.White;
            this.pnlKPI_Guests.Appearance.Options.UseBackColor = true;
            this.pnlKPI_Guests.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlKPI_Guests.Controls.Add(this.lblKPI_GuestsSub);
            this.pnlKPI_Guests.Controls.Add(this.lblKPI_GuestsValue);
            this.pnlKPI_Guests.Controls.Add(this.lblKPI_GuestsTitle);
            this.pnlKPI_Guests.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKPI_Guests.Location = new System.Drawing.Point(3, 3);
            this.pnlKPI_Guests.Name = "pnlKPI_Guests";
            this.pnlKPI_Guests.Size = new System.Drawing.Size(394, 72);
            this.pnlKPI_Guests.TabIndex = 0;
            // 
            // lblKPI_GuestsSub
            // 
            this.lblKPI_GuestsSub.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblKPI_GuestsSub.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblKPI_GuestsSub.Appearance.Options.UseFont = true;
            this.lblKPI_GuestsSub.Appearance.Options.UseForeColor = true;
            this.lblKPI_GuestsSub.Location = new System.Drawing.Point(145, 49);
            this.lblKPI_GuestsSub.Name = "lblKPI_GuestsSub";
            this.lblKPI_GuestsSub.Size = new System.Drawing.Size(90, 15);
            this.lblKPI_GuestsSub.TabIndex = 2;
            this.lblKPI_GuestsSub.Text = "Cập nhật liên tục";
            this.lblKPI_GuestsSub.Visible = false;
            // 
            // lblKPI_GuestsValue
            // 
            this.lblKPI_GuestsValue.Appearance.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblKPI_GuestsValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(35)))), ((int)(((byte)(50)))));
            this.lblKPI_GuestsValue.Appearance.Options.UseFont = true;
            this.lblKPI_GuestsValue.Appearance.Options.UseForeColor = true;
            this.lblKPI_GuestsValue.Location = new System.Drawing.Point(15, 30);
            this.lblKPI_GuestsValue.Name = "lblKPI_GuestsValue";
            this.lblKPI_GuestsValue.Size = new System.Drawing.Size(16, 37);
            this.lblKPI_GuestsValue.TabIndex = 1;
            this.lblKPI_GuestsValue.Text = "0";
            // 
            // lblKPI_GuestsTitle
            // 
            this.lblKPI_GuestsTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblKPI_GuestsTitle.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblKPI_GuestsTitle.Appearance.Options.UseFont = true;
            this.lblKPI_GuestsTitle.Appearance.Options.UseForeColor = true;
            this.lblKPI_GuestsTitle.Location = new System.Drawing.Point(15, 10);
            this.lblKPI_GuestsTitle.Name = "lblKPI_GuestsTitle";
            this.lblKPI_GuestsTitle.Size = new System.Drawing.Size(114, 17);
            this.lblKPI_GuestsTitle.TabIndex = 0;
            this.lblKPI_GuestsTitle.Text = "Khách vào hôm nay";
            // 
            // pnlKPI_Success
            // 
            this.pnlKPI_Success.Appearance.BackColor = System.Drawing.Color.White;
            this.pnlKPI_Success.Appearance.Options.UseBackColor = true;
            this.pnlKPI_Success.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlKPI_Success.Controls.Add(this.lblKPI_SuccessSub);
            this.pnlKPI_Success.Controls.Add(this.lblKPI_SuccessValue);
            this.pnlKPI_Success.Controls.Add(this.lblKPI_SuccessTitle);
            this.pnlKPI_Success.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKPI_Success.Location = new System.Drawing.Point(403, 3);
            this.pnlKPI_Success.Name = "pnlKPI_Success";
            this.pnlKPI_Success.Size = new System.Drawing.Size(394, 72);
            this.pnlKPI_Success.TabIndex = 1;
            // 
            // lblKPI_SuccessSub
            // 
            this.lblKPI_SuccessSub.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblKPI_SuccessSub.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblKPI_SuccessSub.Appearance.Options.UseFont = true;
            this.lblKPI_SuccessSub.Appearance.Options.UseForeColor = true;
            this.lblKPI_SuccessSub.Location = new System.Drawing.Point(134, 51);
            this.lblKPI_SuccessSub.Name = "lblKPI_SuccessSub";
            this.lblKPI_SuccessSub.Size = new System.Drawing.Size(15, 13);
            this.lblKPI_SuccessSub.TabIndex = 2;
            this.lblKPI_SuccessSub.Text = "0%";
            // 
            // lblKPI_SuccessValue
            // 
            this.lblKPI_SuccessValue.Appearance.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblKPI_SuccessValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(67)))), ((int)(((byte)(160)))), ((int)(((byte)(71)))));
            this.lblKPI_SuccessValue.Appearance.Options.UseFont = true;
            this.lblKPI_SuccessValue.Appearance.Options.UseForeColor = true;
            this.lblKPI_SuccessValue.Location = new System.Drawing.Point(15, 30);
            this.lblKPI_SuccessValue.Name = "lblKPI_SuccessValue";
            this.lblKPI_SuccessValue.Size = new System.Drawing.Size(16, 37);
            this.lblKPI_SuccessValue.TabIndex = 1;
            this.lblKPI_SuccessValue.Text = "0";
            // 
            // lblKPI_SuccessTitle
            // 
            this.lblKPI_SuccessTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblKPI_SuccessTitle.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblKPI_SuccessTitle.Appearance.Options.UseFont = true;
            this.lblKPI_SuccessTitle.Appearance.Options.UseForeColor = true;
            this.lblKPI_SuccessTitle.Location = new System.Drawing.Point(15, 10);
            this.lblKPI_SuccessTitle.Name = "lblKPI_SuccessTitle";
            this.lblKPI_SuccessTitle.Size = new System.Drawing.Size(97, 17);
            this.lblKPI_SuccessTitle.TabIndex = 0;
            this.lblKPI_SuccessTitle.Text = "Quét thành công";
            // 
            // pnlKPI_Rejected
            // 
            this.pnlKPI_Rejected.Appearance.BackColor = System.Drawing.Color.White;
            this.pnlKPI_Rejected.Appearance.Options.UseBackColor = true;
            this.pnlKPI_Rejected.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlKPI_Rejected.Controls.Add(this.lblKPI_RejectedSub);
            this.pnlKPI_Rejected.Controls.Add(this.lblKPI_RejectedValue);
            this.pnlKPI_Rejected.Controls.Add(this.lblKPI_RejectedTitle);
            this.pnlKPI_Rejected.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlKPI_Rejected.Location = new System.Drawing.Point(803, 3);
            this.pnlKPI_Rejected.Name = "pnlKPI_Rejected";
            this.pnlKPI_Rejected.Size = new System.Drawing.Size(394, 72);
            this.pnlKPI_Rejected.TabIndex = 2;
            // 
            // lblKPI_RejectedSub
            // 
            this.lblKPI_RejectedSub.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.lblKPI_RejectedSub.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblKPI_RejectedSub.Appearance.Options.UseFont = true;
            this.lblKPI_RejectedSub.Appearance.Options.UseForeColor = true;
            this.lblKPI_RejectedSub.Location = new System.Drawing.Point(130, 49);
            this.lblKPI_RejectedSub.Name = "lblKPI_RejectedSub";
            this.lblKPI_RejectedSub.Size = new System.Drawing.Size(98, 15);
            this.lblKPI_RejectedSub.TabIndex = 2;
            this.lblKPI_RejectedSub.Text = "Hết hạn / Sai cổng";
            // 
            // lblKPI_RejectedValue
            // 
            this.lblKPI_RejectedValue.Appearance.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblKPI_RejectedValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(83)))), ((int)(((byte)(80)))));
            this.lblKPI_RejectedValue.Appearance.Options.UseFont = true;
            this.lblKPI_RejectedValue.Appearance.Options.UseForeColor = true;
            this.lblKPI_RejectedValue.Location = new System.Drawing.Point(15, 30);
            this.lblKPI_RejectedValue.Name = "lblKPI_RejectedValue";
            this.lblKPI_RejectedValue.Size = new System.Drawing.Size(16, 37);
            this.lblKPI_RejectedValue.TabIndex = 1;
            this.lblKPI_RejectedValue.Text = "0";
            // 
            // lblKPI_RejectedTitle
            // 
            this.lblKPI_RejectedTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblKPI_RejectedTitle.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblKPI_RejectedTitle.Appearance.Options.UseFont = true;
            this.lblKPI_RejectedTitle.Appearance.Options.UseForeColor = true;
            this.lblKPI_RejectedTitle.Location = new System.Drawing.Point(15, 10);
            this.lblKPI_RejectedTitle.Name = "lblKPI_RejectedTitle";
            this.lblKPI_RejectedTitle.Size = new System.Drawing.Size(57, 19);
            this.lblKPI_RejectedTitle.TabIndex = 0;
            this.lblKPI_RejectedTitle.Text = "Bị từ chối";
            // 
            // pnlScannerBar
            // 
            this.pnlScannerBar.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(240)))));
            this.pnlScannerBar.Appearance.Options.UseBackColor = true;
            this.pnlScannerBar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlScannerBar.Controls.Add(this.txtScanInput);
            this.pnlScannerBar.Controls.Add(this.btnToggleCamera);
            this.pnlScannerBar.Controls.Add(this.btnScanFile);
            this.pnlScannerBar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlScannerBar.Location = new System.Drawing.Point(18, 3);
            this.pnlScannerBar.Name = "pnlScannerBar";
            this.pnlScannerBar.Size = new System.Drawing.Size(384, 41);
            this.pnlScannerBar.TabIndex = 1;
            // 
            // txtScanInput
            // 
            this.txtScanInput.Location = new System.Drawing.Point(10, 10);
            this.txtScanInput.Name = "txtScanInput";
            this.txtScanInput.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtScanInput.Properties.Appearance.Options.UseFont = true;
            this.txtScanInput.Properties.NullValuePrompt = "Nhập mã hoặc quét barcode -> Enter...";
            this.txtScanInput.Size = new System.Drawing.Size(220, 24);
            this.txtScanInput.TabIndex = 1;
            this.txtScanInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtScanInput_KeyDown);
            // 
            // btnToggleCamera
            // 
            this.btnToggleCamera.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnToggleCamera.Appearance.Options.UseFont = true;
            this.btnToggleCamera.Location = new System.Drawing.Point(240, 10);
            this.btnToggleCamera.Name = "btnToggleCamera";
            this.btnToggleCamera.Size = new System.Drawing.Size(93, 24);
            this.btnToggleCamera.TabIndex = 2;
            this.btnToggleCamera.Text = "Bật cam";
            this.btnToggleCamera.Click += new System.EventHandler(this.BtnToggleCamera_Click);
            // 
            // btnScanFile
            // 
            this.btnScanFile.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnScanFile.Appearance.Options.UseFont = true;
            this.btnScanFile.Location = new System.Drawing.Point(339, 10);
            this.btnScanFile.Name = "btnScanFile";
            this.btnScanFile.Size = new System.Drawing.Size(36, 24);
            this.btnScanFile.TabIndex = 3;
            this.btnScanFile.Text = "...";
            this.btnScanFile.Click += new System.EventHandler(this.BtnScanFile_Click);
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Location = new System.Drawing.Point(0, 128);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.tlpLeftContent);
            this.splitMain.Panel1.Text = "Panel1";
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.picCamera);
            this.splitMain.Panel2.Controls.Add(this.gridLichSu);
            this.splitMain.Panel2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.splitMain.Panel2.Text = "Panel2";
            this.splitMain.Size = new System.Drawing.Size(1200, 672);
            this.splitMain.SplitterPosition = 420;
            this.splitMain.TabIndex = 2;
            // 
            // tlpLeftContent
            // 
            this.tlpLeftContent.ColumnCount = 1;
            this.tlpLeftContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLeftContent.Controls.Add(this.pnlScannerBar, 0, 0);
            this.tlpLeftContent.Controls.Add(this.pnlFeedback, 0, 1);
            this.tlpLeftContent.Controls.Add(this.pnlTicketInfo, 0, 2);
            this.tlpLeftContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpLeftContent.Location = new System.Drawing.Point(0, 0);
            this.tlpLeftContent.Name = "tlpLeftContent";
            this.tlpLeftContent.Padding = new System.Windows.Forms.Padding(15, 0, 15, 15);
            this.tlpLeftContent.RowCount = 3;
            this.tlpLeftContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 47F));
            this.tlpLeftContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 133F));
            this.tlpLeftContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpLeftContent.Size = new System.Drawing.Size(420, 672);
            this.tlpLeftContent.TabIndex = 0;
            // 
            // pnlFeedback
            // 
            this.pnlFeedback.Appearance.BackColor = System.Drawing.Color.White;
            this.pnlFeedback.Appearance.Options.UseBackColor = true;
            this.pnlFeedback.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.pnlFeedback.Controls.Add(this.lblStatusTitle);
            this.pnlFeedback.Controls.Add(this.picStatus);
            this.pnlFeedback.Controls.Add(this.lblStatusSub);
            this.pnlFeedback.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFeedback.Location = new System.Drawing.Point(18, 50);
            this.pnlFeedback.Name = "pnlFeedback";
            this.pnlFeedback.Size = new System.Drawing.Size(384, 127);
            this.pnlFeedback.TabIndex = 0;
            // 
            // lblStatusTitle
            // 
            this.lblStatusTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatusTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(60)))), ((int)(((byte)(114)))));
            this.lblStatusTitle.Appearance.Options.UseFont = true;
            this.lblStatusTitle.Appearance.Options.UseForeColor = true;
            this.lblStatusTitle.Appearance.Options.UseTextOptions = true;
            this.lblStatusTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblStatusTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblStatusTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatusTitle.Location = new System.Drawing.Point(2, 68);
            this.lblStatusTitle.Name = "lblStatusTitle";
            this.lblStatusTitle.Size = new System.Drawing.Size(380, 19);
            this.lblStatusTitle.TabIndex = 3;
            this.lblStatusTitle.Text = "MỜI QUÉT VÉ";
            // 
            // picStatus
            // 
            this.picStatus.BackColor = System.Drawing.Color.Transparent;
            this.picStatus.Dock = System.Windows.Forms.DockStyle.Top;
            this.picStatus.Location = new System.Drawing.Point(2, 2);
            this.picStatus.Name = "picStatus";
            this.picStatus.Size = new System.Drawing.Size(380, 66);
            this.picStatus.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picStatus.TabIndex = 0;
            this.picStatus.TabStop = false;
            // 
            // lblStatusSub
            // 
            this.lblStatusSub.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblStatusSub.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.lblStatusSub.Appearance.Options.UseFont = true;
            this.lblStatusSub.Appearance.Options.UseForeColor = true;
            this.lblStatusSub.Appearance.Options.UseTextOptions = true;
            this.lblStatusSub.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblStatusSub.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblStatusSub.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatusSub.Location = new System.Drawing.Point(2, 87);
            this.lblStatusSub.Name = "lblStatusSub";
            this.lblStatusSub.Size = new System.Drawing.Size(380, 38);
            this.lblStatusSub.TabIndex = 2;
            this.lblStatusSub.Text = "---";
            // 
            // pnlTicketInfo
            // 
            this.pnlTicketInfo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlTicketInfo.Controls.Add(this.grpTicketInfo);
            this.pnlTicketInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTicketInfo.Location = new System.Drawing.Point(18, 183);
            this.pnlTicketInfo.Name = "pnlTicketInfo";
            this.pnlTicketInfo.Size = new System.Drawing.Size(384, 471);
            this.pnlTicketInfo.TabIndex = 0;
            // 
            // grpTicketInfo
            // 
            this.grpTicketInfo.Appearance.BackColor = System.Drawing.Color.White;
            this.grpTicketInfo.Appearance.Options.UseBackColor = true;
            this.grpTicketInfo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.grpTicketInfo.Controls.Add(this.tlpTicketDetails);
            this.grpTicketInfo.Controls.Add(this.pnlTicketHeader);
            this.grpTicketInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grpTicketInfo.Location = new System.Drawing.Point(0, 0);
            this.grpTicketInfo.Name = "grpTicketInfo";
            this.grpTicketInfo.ShowCaption = false;
            this.grpTicketInfo.Size = new System.Drawing.Size(384, 471);
            this.grpTicketInfo.TabIndex = 0;
            // 
            // tlpTicketDetails
            // 
            this.tlpTicketDetails.ColumnCount = 2;
            this.tlpTicketDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 120F));
            this.tlpTicketDetails.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpTicketDetails.Controls.Add(this.lblTitle_MaVe, 0, 0);
            this.tlpTicketDetails.Controls.Add(this.lblTicketMaVe, 1, 0);
            this.tlpTicketDetails.Controls.Add(this.lblTitle_TenVe, 0, 1);
            this.tlpTicketDetails.Controls.Add(this.lblTicketTenVe, 1, 1);
            this.tlpTicketDetails.Controls.Add(this.lblTitle_LoaiVe, 0, 2);
            this.tlpTicketDetails.Controls.Add(this.lblTicketLoaiVe, 1, 2);
            this.tlpTicketDetails.Controls.Add(this.lblTitle_KhuVuc, 0, 3);
            this.tlpTicketDetails.Controls.Add(this.lblTicketKhuVuc, 1, 3);
            this.tlpTicketDetails.Controls.Add(this.lblTitle_Luot, 0, 4);
            this.tlpTicketDetails.Controls.Add(this.lblTicketLuot, 1, 4);
            this.tlpTicketDetails.Controls.Add(this.lblTitle_NgayMua, 0, 5);
            this.tlpTicketDetails.Controls.Add(this.lblTicketNgayMua, 1, 5);
            this.tlpTicketDetails.Controls.Add(this.lblTitle_HetHan, 0, 6);
            this.tlpTicketDetails.Controls.Add(this.lblTicketHetHan, 1, 6);
            this.tlpTicketDetails.Controls.Add(this.lblTitle_Khach, 0, 7);
            this.tlpTicketDetails.Controls.Add(this.lblTicketKhach, 1, 7);
            this.tlpTicketDetails.Controls.Add(this.lblTitle_RFID, 0, 8);
            this.tlpTicketDetails.Controls.Add(this.lblTicketRFID, 1, 8);
            this.tlpTicketDetails.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpTicketDetails.Location = new System.Drawing.Point(2, 40);
            this.tlpTicketDetails.Name = "tlpTicketDetails";
            this.tlpTicketDetails.Padding = new System.Windows.Forms.Padding(15);
            this.tlpTicketDetails.RowCount = 9;
            this.tlpTicketDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpTicketDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpTicketDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpTicketDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpTicketDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpTicketDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpTicketDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpTicketDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpTicketDetails.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 36F));
            this.tlpTicketDetails.Size = new System.Drawing.Size(380, 429);
            this.tlpTicketDetails.TabIndex = 1;
            // 
            // lblTitle_MaVe
            // 
            this.lblTitle_MaVe.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle_MaVe.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblTitle_MaVe.Appearance.Options.UseFont = true;
            this.lblTitle_MaVe.Appearance.Options.UseForeColor = true;
            this.lblTitle_MaVe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle_MaVe.Location = new System.Drawing.Point(18, 18);
            this.lblTitle_MaVe.Name = "lblTitle_MaVe";
            this.lblTitle_MaVe.Size = new System.Drawing.Size(114, 30);
            this.lblTitle_MaVe.TabIndex = 0;
            this.lblTitle_MaVe.Text = "Mã vé:";
            // 
            // lblTicketMaVe
            // 
            this.lblTicketMaVe.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTicketMaVe.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblTicketMaVe.Appearance.Options.UseFont = true;
            this.lblTicketMaVe.Appearance.Options.UseForeColor = true;
            this.lblTicketMaVe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTicketMaVe.Location = new System.Drawing.Point(138, 18);
            this.lblTicketMaVe.Name = "lblTicketMaVe";
            this.lblTicketMaVe.Size = new System.Drawing.Size(224, 30);
            this.lblTicketMaVe.TabIndex = 1;
            this.lblTicketMaVe.Text = "---";
            // 
            // lblTitle_TenVe
            // 
            this.lblTitle_TenVe.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle_TenVe.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblTitle_TenVe.Appearance.Options.UseFont = true;
            this.lblTitle_TenVe.Appearance.Options.UseForeColor = true;
            this.lblTitle_TenVe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle_TenVe.Location = new System.Drawing.Point(18, 54);
            this.lblTitle_TenVe.Name = "lblTitle_TenVe";
            this.lblTitle_TenVe.Size = new System.Drawing.Size(114, 30);
            this.lblTitle_TenVe.TabIndex = 2;
            this.lblTitle_TenVe.Text = "Tên vé:";
            // 
            // lblTicketTenVe
            // 
            this.lblTicketTenVe.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTicketTenVe.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblTicketTenVe.Appearance.Options.UseFont = true;
            this.lblTicketTenVe.Appearance.Options.UseForeColor = true;
            this.lblTicketTenVe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTicketTenVe.Location = new System.Drawing.Point(138, 54);
            this.lblTicketTenVe.Name = "lblTicketTenVe";
            this.lblTicketTenVe.Size = new System.Drawing.Size(224, 30);
            this.lblTicketTenVe.TabIndex = 3;
            this.lblTicketTenVe.Text = "---";
            // 
            // lblTitle_LoaiVe
            // 
            this.lblTitle_LoaiVe.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle_LoaiVe.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblTitle_LoaiVe.Appearance.Options.UseFont = true;
            this.lblTitle_LoaiVe.Appearance.Options.UseForeColor = true;
            this.lblTitle_LoaiVe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle_LoaiVe.Location = new System.Drawing.Point(18, 90);
            this.lblTitle_LoaiVe.Name = "lblTitle_LoaiVe";
            this.lblTitle_LoaiVe.Size = new System.Drawing.Size(114, 30);
            this.lblTitle_LoaiVe.TabIndex = 4;
            this.lblTitle_LoaiVe.Text = "Loại vé:";
            // 
            // lblTicketLoaiVe
            // 
            this.lblTicketLoaiVe.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTicketLoaiVe.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblTicketLoaiVe.Appearance.Options.UseFont = true;
            this.lblTicketLoaiVe.Appearance.Options.UseForeColor = true;
            this.lblTicketLoaiVe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTicketLoaiVe.Location = new System.Drawing.Point(138, 90);
            this.lblTicketLoaiVe.Name = "lblTicketLoaiVe";
            this.lblTicketLoaiVe.Size = new System.Drawing.Size(224, 30);
            this.lblTicketLoaiVe.TabIndex = 5;
            this.lblTicketLoaiVe.Text = "---";
            // 
            // lblTitle_KhuVuc
            // 
            this.lblTitle_KhuVuc.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle_KhuVuc.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblTitle_KhuVuc.Appearance.Options.UseFont = true;
            this.lblTitle_KhuVuc.Appearance.Options.UseForeColor = true;
            this.lblTitle_KhuVuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle_KhuVuc.Location = new System.Drawing.Point(18, 126);
            this.lblTitle_KhuVuc.Name = "lblTitle_KhuVuc";
            this.lblTitle_KhuVuc.Size = new System.Drawing.Size(114, 30);
            this.lblTitle_KhuVuc.TabIndex = 6;
            this.lblTitle_KhuVuc.Text = "Khu vực:";
            // 
            // lblTicketKhuVuc
            // 
            this.lblTicketKhuVuc.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTicketKhuVuc.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblTicketKhuVuc.Appearance.Options.UseFont = true;
            this.lblTicketKhuVuc.Appearance.Options.UseForeColor = true;
            this.lblTicketKhuVuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTicketKhuVuc.Location = new System.Drawing.Point(138, 126);
            this.lblTicketKhuVuc.Name = "lblTicketKhuVuc";
            this.lblTicketKhuVuc.Size = new System.Drawing.Size(224, 30);
            this.lblTicketKhuVuc.TabIndex = 7;
            this.lblTicketKhuVuc.Text = "---";
            // 
            // lblTitle_Luot
            // 
            this.lblTitle_Luot.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle_Luot.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblTitle_Luot.Appearance.Options.UseFont = true;
            this.lblTitle_Luot.Appearance.Options.UseForeColor = true;
            this.lblTitle_Luot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle_Luot.Location = new System.Drawing.Point(18, 162);
            this.lblTitle_Luot.Name = "lblTitle_Luot";
            this.lblTitle_Luot.Size = new System.Drawing.Size(114, 30);
            this.lblTitle_Luot.TabIndex = 8;
            this.lblTitle_Luot.Text = "Lượt còn:";
            // 
            // lblTicketLuot
            // 
            this.lblTicketLuot.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTicketLuot.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(137)))), ((int)(((byte)(123)))));
            this.lblTicketLuot.Appearance.Options.UseFont = true;
            this.lblTicketLuot.Appearance.Options.UseForeColor = true;
            this.lblTicketLuot.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTicketLuot.Location = new System.Drawing.Point(138, 162);
            this.lblTicketLuot.Name = "lblTicketLuot";
            this.lblTicketLuot.Size = new System.Drawing.Size(224, 30);
            this.lblTicketLuot.TabIndex = 9;
            this.lblTicketLuot.Text = "---";
            // 
            // lblTitle_NgayMua
            // 
            this.lblTitle_NgayMua.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle_NgayMua.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblTitle_NgayMua.Appearance.Options.UseFont = true;
            this.lblTitle_NgayMua.Appearance.Options.UseForeColor = true;
            this.lblTitle_NgayMua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle_NgayMua.Location = new System.Drawing.Point(18, 198);
            this.lblTitle_NgayMua.Name = "lblTitle_NgayMua";
            this.lblTitle_NgayMua.Size = new System.Drawing.Size(114, 30);
            this.lblTitle_NgayMua.TabIndex = 10;
            this.lblTitle_NgayMua.Text = "Ngày mua:";
            // 
            // lblTicketNgayMua
            // 
            this.lblTicketNgayMua.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTicketNgayMua.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblTicketNgayMua.Appearance.Options.UseFont = true;
            this.lblTicketNgayMua.Appearance.Options.UseForeColor = true;
            this.lblTicketNgayMua.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTicketNgayMua.Location = new System.Drawing.Point(138, 198);
            this.lblTicketNgayMua.Name = "lblTicketNgayMua";
            this.lblTicketNgayMua.Size = new System.Drawing.Size(224, 30);
            this.lblTicketNgayMua.TabIndex = 11;
            this.lblTicketNgayMua.Text = "---";
            // 
            // lblTitle_HetHan
            // 
            this.lblTitle_HetHan.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle_HetHan.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblTitle_HetHan.Appearance.Options.UseFont = true;
            this.lblTitle_HetHan.Appearance.Options.UseForeColor = true;
            this.lblTitle_HetHan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle_HetHan.Location = new System.Drawing.Point(18, 234);
            this.lblTitle_HetHan.Name = "lblTitle_HetHan";
            this.lblTitle_HetHan.Size = new System.Drawing.Size(114, 30);
            this.lblTitle_HetHan.TabIndex = 12;
            this.lblTitle_HetHan.Text = "Hết hạn:";
            // 
            // lblTicketHetHan
            // 
            this.lblTicketHetHan.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTicketHetHan.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblTicketHetHan.Appearance.Options.UseFont = true;
            this.lblTicketHetHan.Appearance.Options.UseForeColor = true;
            this.lblTicketHetHan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTicketHetHan.Location = new System.Drawing.Point(138, 234);
            this.lblTicketHetHan.Name = "lblTicketHetHan";
            this.lblTicketHetHan.Size = new System.Drawing.Size(224, 30);
            this.lblTicketHetHan.TabIndex = 13;
            this.lblTicketHetHan.Text = "---";
            // 
            // lblTitle_Khach
            // 
            this.lblTitle_Khach.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle_Khach.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblTitle_Khach.Appearance.Options.UseFont = true;
            this.lblTitle_Khach.Appearance.Options.UseForeColor = true;
            this.lblTitle_Khach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle_Khach.Location = new System.Drawing.Point(18, 270);
            this.lblTitle_Khach.Name = "lblTitle_Khach";
            this.lblTitle_Khach.Size = new System.Drawing.Size(114, 30);
            this.lblTitle_Khach.TabIndex = 14;
            this.lblTitle_Khach.Text = "Khách hàng:";
            // 
            // lblTicketKhach
            // 
            this.lblTicketKhach.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTicketKhach.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblTicketKhach.Appearance.Options.UseFont = true;
            this.lblTicketKhach.Appearance.Options.UseForeColor = true;
            this.lblTicketKhach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTicketKhach.Location = new System.Drawing.Point(138, 270);
            this.lblTicketKhach.Name = "lblTicketKhach";
            this.lblTicketKhach.Size = new System.Drawing.Size(224, 30);
            this.lblTicketKhach.TabIndex = 15;
            this.lblTicketKhach.Text = "---";
            // 
            // lblTitle_RFID
            // 
            this.lblTitle_RFID.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTitle_RFID.Appearance.ForeColor = System.Drawing.Color.DimGray;
            this.lblTitle_RFID.Appearance.Options.UseFont = true;
            this.lblTitle_RFID.Appearance.Options.UseForeColor = true;
            this.lblTitle_RFID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle_RFID.Location = new System.Drawing.Point(18, 306);
            this.lblTitle_RFID.Name = "lblTitle_RFID";
            this.lblTitle_RFID.Size = new System.Drawing.Size(114, 105);
            this.lblTitle_RFID.TabIndex = 16;
            this.lblTitle_RFID.Text = "RFID:";
            // 
            // lblTicketRFID
            // 
            this.lblTicketRFID.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.lblTicketRFID.Appearance.ForeColor = System.Drawing.Color.Black;
            this.lblTicketRFID.Appearance.Options.UseFont = true;
            this.lblTicketRFID.Appearance.Options.UseForeColor = true;
            this.lblTicketRFID.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTicketRFID.Location = new System.Drawing.Point(138, 306);
            this.lblTicketRFID.Name = "lblTicketRFID";
            this.lblTicketRFID.Size = new System.Drawing.Size(224, 105);
            this.lblTicketRFID.TabIndex = 17;
            this.lblTicketRFID.Text = "---";
            // 
            // pnlTicketHeader
            // 
            this.pnlTicketHeader.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(248)))), ((int)(((byte)(240)))));
            this.pnlTicketHeader.Appearance.Options.UseBackColor = true;
            this.pnlTicketHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlTicketHeader.Controls.Add(this.lblTicketHeaderTitle);
            this.pnlTicketHeader.Controls.Add(this.lblVerdict);
            this.pnlTicketHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTicketHeader.Location = new System.Drawing.Point(2, 2);
            this.pnlTicketHeader.Name = "pnlTicketHeader";
            this.pnlTicketHeader.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlTicketHeader.Size = new System.Drawing.Size(380, 38);
            this.pnlTicketHeader.TabIndex = 2;
            // 
            // lblTicketHeaderTitle
            // 
            this.lblTicketHeaderTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTicketHeaderTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(30)))));
            this.lblTicketHeaderTitle.Appearance.Options.UseFont = true;
            this.lblTicketHeaderTitle.Appearance.Options.UseForeColor = true;
            this.lblTicketHeaderTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTicketHeaderTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTicketHeaderTitle.Name = "lblTicketHeaderTitle";
            this.lblTicketHeaderTitle.Size = new System.Drawing.Size(96, 21);
            this.lblTicketHeaderTitle.TabIndex = 0;
            this.lblTicketHeaderTitle.Text = "Thông tin vé";
            // 
            // lblVerdict
            // 
            this.lblVerdict.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(245)))), ((int)(((byte)(225)))));
            this.lblVerdict.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblVerdict.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(120)))), ((int)(((byte)(50)))));
            this.lblVerdict.Appearance.Options.UseBackColor = true;
            this.lblVerdict.Appearance.Options.UseFont = true;
            this.lblVerdict.Appearance.Options.UseForeColor = true;
            this.lblVerdict.Appearance.Options.UseTextOptions = true;
            this.lblVerdict.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblVerdict.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblVerdict.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblVerdict.Location = new System.Drawing.Point(285, 10);
            this.lblVerdict.Name = "lblVerdict";
            this.lblVerdict.Size = new System.Drawing.Size(80, 18);
            this.lblVerdict.TabIndex = 1;
            this.lblVerdict.Text = "HỢP LỆ";
            // 
            // picCamera
            // 
            this.picCamera.BackColor = System.Drawing.Color.Transparent;
            this.picCamera.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picCamera.Location = new System.Drawing.Point(0, 536);
            this.picCamera.Name = "picCamera";
            this.picCamera.Size = new System.Drawing.Size(770, 136);
            this.picCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCamera.TabIndex = 4;
            this.picCamera.TabStop = false;
            this.picCamera.Visible = false;
            // 
            // gridLichSu
            // 
            this.gridLichSu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLichSu.Location = new System.Drawing.Point(0, 3);
            this.gridLichSu.MainView = this.viewLichSu;
            this.gridLichSu.Name = "gridLichSu";
            this.gridLichSu.Size = new System.Drawing.Size(770, 669);
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
            this.colGio.Caption = "Giờ";
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
            this.colMaVe.Caption = "Mã Vé";
            this.colMaVe.FieldName = "MaVach";
            this.colMaVe.Name = "colMaVe";
            this.colMaVe.Visible = true;
            this.colMaVe.VisibleIndex = 1;
            this.colMaVe.Width = 220;
            // 
            // colKetQua
            // 
            this.colKetQua.Caption = "Kết Quả";
            this.colKetQua.FieldName = "KetQua";
            this.colKetQua.Name = "colKetQua";
            this.colKetQua.Visible = true;
            this.colKetQua.VisibleIndex = 2;
            this.colKetQua.Width = 100;
            // 
            // colTenVe
            // 
            this.colTenVe.Caption = "Tên Vé";
            this.colTenVe.FieldName = "TenSanPham";
            this.colTenVe.Name = "colTenVe";
            this.colTenVe.Visible = true;
            this.colTenVe.VisibleIndex = 3;
            this.colTenVe.Width = 180;
            // 
            // colKhuVuc
            // 
            this.colKhuVuc.Caption = "Khu Vực";
            this.colKhuVuc.FieldName = "TenKhuVuc";
            this.colKhuVuc.Name = "colKhuVuc";
            this.colKhuVuc.Visible = true;
            this.colKhuVuc.VisibleIndex = 4;
            this.colKhuVuc.Width = 120;
            // 
            // colLuotConLai
            // 
            this.colLuotConLai.Caption = "Lượt Còn";
            this.colLuotConLai.FieldName = "SoLuotConLai";
            this.colLuotConLai.Name = "colLuotConLai";
            this.colLuotConLai.Visible = true;
            this.colLuotConLai.VisibleIndex = 5;
            this.colLuotConLai.Width = 70;
            // 
            // pnlCounter
            // 
            this.pnlCounter.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(60)))), ((int)(((byte)(114)))));
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
            this.lblCounter.Size = new System.Drawing.Size(290, 30);
            this.lblCounter.TabIndex = 0;
            this.lblCounter.Text = "Hợp lệ: 0     Từ chối: 0     Tổng quét: 0";
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
            this.Controls.Add(this.pnlKPI);
            this.Controls.Add(this.pnlBanner);
            this.Name = "ucKiemSoatCong";
            this.Size = new System.Drawing.Size(1200, 800);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBanner)).EndInit();
            this.pnlBanner.ResumeLayout(false);
            this.pnlBanner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cboKhuVuc.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboTroChoi.Properties)).EndInit();
            this.pnlKPI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlKPI_Guests)).EndInit();
            this.pnlKPI_Guests.ResumeLayout(false);
            this.pnlKPI_Guests.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlKPI_Success)).EndInit();
            this.pnlKPI_Success.ResumeLayout(false);
            this.pnlKPI_Success.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlKPI_Rejected)).EndInit();
            this.pnlKPI_Rejected.ResumeLayout(false);
            this.pnlKPI_Rejected.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlScannerBar)).EndInit();
            this.pnlScannerBar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtScanInput.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).EndInit();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.tlpLeftContent.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFeedback)).EndInit();
            this.pnlFeedback.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picStatus)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTicketInfo)).EndInit();
            this.pnlTicketInfo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grpTicketInfo)).EndInit();
            this.grpTicketInfo.ResumeLayout(false);
            this.tlpTicketDetails.ResumeLayout(false);
            this.tlpTicketDetails.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTicketHeader)).EndInit();
            this.pnlTicketHeader.ResumeLayout(false);
            this.pnlTicketHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).EndInit();
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
        
        private System.Windows.Forms.TableLayoutPanel pnlKPI;
        private DevExpress.XtraEditors.PanelControl pnlKPI_Guests;
        private DevExpress.XtraEditors.LabelControl lblKPI_GuestsTitle;
        private DevExpress.XtraEditors.LabelControl lblKPI_GuestsValue;
        private DevExpress.XtraEditors.LabelControl lblKPI_GuestsSub;
        
        private DevExpress.XtraEditors.PanelControl pnlKPI_Success;
        private DevExpress.XtraEditors.LabelControl lblKPI_SuccessTitle;
        private DevExpress.XtraEditors.LabelControl lblKPI_SuccessValue;
        private DevExpress.XtraEditors.LabelControl lblKPI_SuccessSub;
        
        private DevExpress.XtraEditors.PanelControl pnlKPI_Rejected;
        private DevExpress.XtraEditors.LabelControl lblKPI_RejectedTitle;
        private DevExpress.XtraEditors.LabelControl lblKPI_RejectedValue;
        private DevExpress.XtraEditors.LabelControl lblKPI_RejectedSub;
        
        private DevExpress.XtraEditors.PanelControl pnlScannerBar;
        private DevExpress.XtraEditors.TextEdit txtScanInput;
        private DevExpress.XtraEditors.SimpleButton btnToggleCamera;
        private DevExpress.XtraEditors.SimpleButton btnScanFile;
        
        private DevExpress.XtraEditors.SplitContainerControl splitMain;
        
        private System.Windows.Forms.TableLayoutPanel tlpLeftContent;
        private DevExpress.XtraEditors.PanelControl pnlTicketInfo;
        private DevExpress.XtraEditors.GroupControl grpTicketInfo;
        private DevExpress.XtraEditors.PanelControl pnlTicketHeader;
        private DevExpress.XtraEditors.LabelControl lblTicketHeaderTitle;
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

        private System.Windows.Forms.TableLayoutPanel tlpTicketDetails;
        private DevExpress.XtraEditors.LabelControl lblTitle_MaVe;
        private DevExpress.XtraEditors.LabelControl lblTitle_TenVe;
        private DevExpress.XtraEditors.LabelControl lblTitle_LoaiVe;
        private DevExpress.XtraEditors.LabelControl lblTitle_KhuVuc;
        private DevExpress.XtraEditors.LabelControl lblTitle_Luot;
        private DevExpress.XtraEditors.LabelControl lblTitle_NgayMua;
        private DevExpress.XtraEditors.LabelControl lblTitle_HetHan;
        private DevExpress.XtraEditors.LabelControl lblTitle_Khach;
        private DevExpress.XtraEditors.LabelControl lblTitle_RFID;
        private PictureBox picCamera;
        private PanelControl pnlFeedback;
        private PictureBox picStatus;
        private LabelControl lblStatusSub;
        private LabelControl lblStatusTitle;
    }
}
