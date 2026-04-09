namespace GUI
{
    partial class frmVaiTro
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
            this.gbThongTin = new Guna.UI2.WinForms.Guna2GroupBox();
            this.lblId = new System.Windows.Forms.Label();
            this.txtId = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTenVaiTro = new System.Windows.Forms.Label();
            this.txtTenVaiTro = new Guna.UI2.WinForms.Guna2TextBox();
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
            this.pnlMain.Panel1.Controls.Add(this.pnlLeft);
            this.pnlMain.Panel2.Controls.Add(this.pnlRight);
            this.pnlMain.Size = new System.Drawing.Size(1200, 700);
            this.pnlMain.SplitterPosition = 400;
            this.pnlMain.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gbDanhSach);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(790, 700);
            this.pnlLeft.TabIndex = 0;
            // 
            // gbDanhSach
            // 
            this.gbDanhSach.Controls.Add(this.gridControl);
            this.gbDanhSach.Controls.Add(this.pnlTimKiem);
            this.gbDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDanhSach.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbDanhSach.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbDanhSach.Location = new System.Drawing.Point(0, 0);
            this.gbDanhSach.Name = "gbDanhSach";
            this.gbDanhSach.Size = new System.Drawing.Size(790, 700);
            this.gbDanhSach.TabIndex = 0;
            this.gbDanhSach.Text = "Danh sách Vai Trò";
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 90);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(790, 610);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsView.ShowGroupPanel = false;
            // 
            // pnlTimKiem
            // 
            this.pnlTimKiem.Controls.Add(this.txtTimKiem);
            this.pnlTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimKiem.Location = new System.Drawing.Point(0, 40);
            this.pnlTimKiem.Name = "pnlTimKiem";
            this.pnlTimKiem.Size = new System.Drawing.Size(790, 50);
            this.pnlTimKiem.TabIndex = 1;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtTimKiem.BorderRadius = 15;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.Location = new System.Drawing.Point(245, 7);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Tìm kiếm vai trò...";
            this.txtTimKiem.Size = new System.Drawing.Size(300, 36);
            this.txtTimKiem.TabIndex = 0;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.gbThongTin);
            this.pnlRight.Controls.Add(this.pnlSpacer);
            this.pnlRight.Controls.Add(this.gbChucNang);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(0, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(10);
            this.pnlRight.Size = new System.Drawing.Size(400, 700);
            this.pnlRight.TabIndex = 1;
            // 
            // gbThongTin
            // 
            this.gbThongTin.Controls.Add(this.lblId);
            this.gbThongTin.Controls.Add(this.txtId);
            this.gbThongTin.Controls.Add(this.lblTenVaiTro);
            this.gbThongTin.Controls.Add(this.txtTenVaiTro);
            this.gbThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThongTin.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbThongTin.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbThongTin.Location = new System.Drawing.Point(10, 10);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(380, 480);
            this.gbThongTin.TabIndex = 0;
            this.gbThongTin.Text = "Thông tin chi tiết";
            // 
            // lblId
            // 
            this.lblId.AutoSize = true;
            this.lblId.Location = new System.Drawing.Point(20, 60);
            this.lblId.Name = "lblId";
            this.lblId.Size = new System.Drawing.Size(85, 19);
            this.lblId.TabIndex = 0;
            this.lblId.Text = "Mã Vai Trò:";
            // 
            // txtId
            // 
            this.txtId.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtId.DefaultText = "";
            this.txtId.Enabled = false;
            this.txtId.Location = new System.Drawing.Point(20, 85);
            this.txtId.Name = "txtId";
            this.txtId.Size = new System.Drawing.Size(340, 36);
            this.txtId.TabIndex = 1;
            // 
            // lblTenVaiTro
            // 
            this.lblTenVaiTro.AutoSize = true;
            this.lblTenVaiTro.Location = new System.Drawing.Point(20, 140);
            this.lblTenVaiTro.Name = "lblTenVaiTro";
            this.lblTenVaiTro.Size = new System.Drawing.Size(87, 19);
            this.lblTenVaiTro.TabIndex = 2;
            this.lblTenVaiTro.Text = "Tên Vai Trò:";
            // 
            // txtTenVaiTro
            // 
            this.txtTenVaiTro.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTenVaiTro.DefaultText = "";
            this.txtTenVaiTro.Location = new System.Drawing.Point(20, 165);
            this.txtTenVaiTro.Name = "txtTenVaiTro";
            this.txtTenVaiTro.Size = new System.Drawing.Size(340, 36);
            this.txtTenVaiTro.TabIndex = 3;
            // 
            // pnlSpacer
            // 
            this.pnlSpacer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSpacer.Location = new System.Drawing.Point(10, 490);
            this.pnlSpacer.Name = "pnlSpacer";
            this.pnlSpacer.Size = new System.Drawing.Size(380, 10);
            this.pnlSpacer.TabIndex = 1;
            // 
            // gbChucNang
            // 
            this.gbChucNang.Controls.Add(this.btnThem);
            this.gbChucNang.Controls.Add(this.btnSua);
            this.gbChucNang.Controls.Add(this.btnXoa);
            this.gbChucNang.Controls.Add(this.btnLamMoi);
            this.gbChucNang.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbChucNang.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbChucNang.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbChucNang.Location = new System.Drawing.Point(10, 500);
            this.gbChucNang.Name = "gbChucNang";
            this.gbChucNang.Size = new System.Drawing.Size(380, 190);
            this.gbChucNang.TabIndex = 2;
            this.gbChucNang.Text = "Chức năng";
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(20, 55);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(160, 45);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm mới";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(200, 55);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(160, 45);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Cập nhật";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(20, 115);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(160, 45);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(200, 115);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(160, 45);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // frmVaiTro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmVaiTro";
            this.Text = "Quản lý Vai Trò";
            this.Load += new System.EventHandler(this.frmVaiTro_Load);
            this.txtTimKiem.TextChanged += new System.EventHandler(this.txtTimKiem_TextChanged);
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.GridView_FocusedRowChanged);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel1)).EndInit();
            this.pnlMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain.Panel2)).EndInit();
            this.pnlMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlMain)).EndInit();
            this.pnlMain.ResumeLayout(false);
            this.pnlLeft.ResumeLayout(false);
            this.gbDanhSach.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.pnlTimKiem.ResumeLayout(false);
            this.pnlRight.ResumeLayout(false);
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            this.gbChucNang.ResumeLayout(false);
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
        private Guna.UI2.WinForms.Guna2GroupBox gbThongTin;
        private System.Windows.Forms.Label lblId;
        private Guna.UI2.WinForms.Guna2TextBox txtId;
        private System.Windows.Forms.Label lblTenVaiTro;
        private Guna.UI2.WinForms.Guna2TextBox txtTenVaiTro;
        private Guna.UI2.WinForms.Guna2Panel pnlSpacer;
        private Guna.UI2.WinForms.Guna2GroupBox gbChucNang;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button btnSua;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Button btnLamMoi;
    }
}
