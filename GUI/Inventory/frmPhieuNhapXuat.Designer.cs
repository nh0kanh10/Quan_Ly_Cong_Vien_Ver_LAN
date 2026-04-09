namespace GUI
{
    partial class frmPhieuNhapXuat
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
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnToggleMode = new Guna.UI2.WinForms.Guna2Button();
            this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.gbMaster = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridMaster = new DevExpress.XtraGrid.GridControl();
            this.gridViewMaster = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gbDetail = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridDetail = new DevExpress.XtraGrid.GridControl();
            this.gridViewDetail = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.btnSua = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.btnLamMoi = new Guna.UI2.WinForms.Guna2Button();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).BeginInit();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.gbMaster.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMaster)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMaster)).BeginInit();
            this.gbDetail.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDetail)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetail)).BeginInit();
            this.pnlRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Controls.Add(this.btnToggleMode);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.FillColor = System.Drawing.Color.White;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(1200, 70);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(62)))), ((int)(((byte)(80)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(309, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN LÝ XUẤT NHẬP KHO";
            // 
            // btnToggleMode
            // 
            this.btnToggleMode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToggleMode.BorderRadius = 15;
            this.btnToggleMode.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(155)))), ((int)(((byte)(89)))), ((int)(((byte)(182)))));
            this.btnToggleMode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnToggleMode.ForeColor = System.Drawing.Color.White;
            this.btnToggleMode.Location = new System.Drawing.Point(1000, 15);
            this.btnToggleMode.Name = "btnToggleMode";
            this.btnToggleMode.Size = new System.Drawing.Size(180, 40);
            this.btnToggleMode.TabIndex = 1;
            this.btnToggleMode.Text = "Sang Chế Độ Xuất";
            this.btnToggleMode.Click += new System.EventHandler(this.btnToggleMode_Click);
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Horizontal = false;
            this.splitMain.Location = new System.Drawing.Point(0, 70);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.gbMaster);
            this.splitMain.Panel1.Padding = new System.Windows.Forms.Padding(10);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.gbDetail);
            this.splitMain.Panel2.Padding = new System.Windows.Forms.Padding(10);
            this.splitMain.Size = new System.Drawing.Size(1020, 630);
            this.splitMain.SplitterPosition = 300;
            this.splitMain.TabIndex = 1;
            // 
            // gbMaster
            // 
            this.gbMaster.Controls.Add(this.gridMaster);
            this.gbMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbMaster.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbMaster.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbMaster.Location = new System.Drawing.Point(10, 10);
            this.gbMaster.Name = "gbMaster";
            this.gbMaster.Size = new System.Drawing.Size(1000, 280);
            this.gbMaster.TabIndex = 0;
            this.gbMaster.Text = "Danh sách phiếu (Master)";
            // 
            // gridMaster
            // 
            this.gridMaster.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMaster.Location = new System.Drawing.Point(0, 40);
            this.gridMaster.MainView = this.gridViewMaster;
            this.gridMaster.Name = "gridMaster";
            this.gridMaster.Size = new System.Drawing.Size(1000, 240);
            this.gridMaster.TabIndex = 0;
            this.gridMaster.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewMaster});
            // 
            // gridViewMaster
            // 
            this.gridViewMaster.GridControl = this.gridMaster;
            this.gridViewMaster.Name = "gridViewMaster";
            this.gridViewMaster.OptionsView.ShowGroupPanel = false;
            this.gridViewMaster.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridViewMaster_FocusedRowChanged);
            // 
            // gbDetail
            // 
            this.gbDetail.Controls.Add(this.gridDetail);
            this.gbDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDetail.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbDetail.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbDetail.Location = new System.Drawing.Point(10, 10);
            this.gbDetail.Name = "gbDetail";
            this.gbDetail.Size = new System.Drawing.Size(1000, 300);
            this.gbDetail.TabIndex = 0;
            this.gbDetail.Text = "Chi tiết phiếu (Detail)";
            // 
            // gridDetail
            // 
            this.gridDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDetail.Location = new System.Drawing.Point(0, 40);
            this.gridDetail.MainView = this.gridViewDetail;
            this.gridDetail.Name = "gridDetail";
            this.gridDetail.Size = new System.Drawing.Size(1000, 260);
            this.gridDetail.TabIndex = 0;
            this.gridDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewDetail});
            // 
            // gridViewDetail
            // 
            this.gridViewDetail.GridControl = this.gridDetail;
            this.gridViewDetail.Name = "gridViewDetail";
            this.gridViewDetail.OptionsView.ShowGroupPanel = false;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.btnThem);
            this.pnlRight.Controls.Add(this.btnSua);
            this.pnlRight.Controls.Add(this.btnXoa);
            this.pnlRight.Controls.Add(this.btnLamMoi);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlRight.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(249)))), ((int)(((byte)(250)))));
            this.pnlRight.Location = new System.Drawing.Point(1020, 70);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(15);
            this.pnlRight.Size = new System.Drawing.Size(180, 630);
            this.pnlRight.TabIndex = 2;
            // 
            // btnThem
            // 
            this.btnThem.BorderRadius = 15;
            this.btnThem.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(152)))), ((int)(((byte)(219)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(15, 15);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(150, 45);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm Phiếu";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.BorderRadius = 15;
            this.btnSua.Dock = System.Windows.Forms.DockStyle.Top;
            this.btnSua.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(126)))), ((int)(((byte)(34)))));
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(15, 60);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(150, 45);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Sửa Phiếu";
            this.btnSua.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BorderRadius = 15;
            this.btnXoa.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(231)))), ((int)(((byte)(76)))), ((int)(((byte)(60)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(15, 525);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(150, 45);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa Phiếu";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.BorderRadius = 15;
            this.btnLamMoi.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.btnLamMoi.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(127)))), ((int)(((byte)(140)))), ((int)(((byte)(141)))));
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(15, 570);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(150, 45);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm Mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // frmPhieuNhapXuat
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlRight);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPhieuNhapXuat";
            this.Text = "Quản Lý Kho";
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).EndInit();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.gbMaster.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridMaster)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMaster)).EndInit();
            this.gbDetail.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridDetail)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewDetail)).EndInit();
            this.pnlRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Button btnToggleMode;
        private DevExpress.XtraEditors.SplitContainerControl splitMain;
        private Guna.UI2.WinForms.Guna2GroupBox gbMaster;
        private DevExpress.XtraGrid.GridControl gridMaster;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMaster;
        private Guna.UI2.WinForms.Guna2GroupBox gbDetail;
        private DevExpress.XtraGrid.GridControl gridDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewDetail;
        private Guna.UI2.WinForms.Guna2Panel pnlRight;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
    }
}
