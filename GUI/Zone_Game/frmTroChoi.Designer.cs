namespace GUI
{
    partial class frmTroChoi
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
                        this.txtTenTroChoi = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbl_txtTenTroChoi = new System.Windows.Forms.Label();
            this.slkKhuVuc = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.lbl_slkKhuVuc = new System.Windows.Forms.Label();
            this.slkKhuVucView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.cboTrangThai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lbl_cboTrangThai = new System.Windows.Forms.Label();
            this.txtMoTa = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbl_txtMoTa = new System.Windows.Forms.Label();
this.pnlMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.gbDanhSach = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlTimKiem = new Guna.UI2.WinForms.Guna2Panel();
            this.txtTimKiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnTimKiem = new Guna.UI2.WinForms.Guna2Button();
            this.pnlRight = new Guna.UI2.WinForms.Guna2Panel();
            this.gbThongTin = new System.Windows.Forms.Panel();
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
            this.gbChucNang.SuspendLayout();
            this.gbThongTin.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            // 
            // pnlMain.Panel1
            // 
            this.pnlMain.Panel1.Controls.Add(this.pnlLeft);
            // 
            // pnlMain.Panel2
            // 
            this.pnlMain.Panel2.Controls.Add(this.pnlRight);
            this.pnlMain.ShowSplitGlyph = DevExpress.Utils.DefaultBoolean.True;
            this.pnlMain.Size = new System.Drawing.Size(1300, 750);
            this.pnlMain.SplitterPosition = 446;
            this.pnlMain.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gbDanhSach);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(844, 750);
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
            this.gbDanhSach.Size = new System.Drawing.Size(844, 750);
            this.gbDanhSach.TabIndex = 0;
            this.gbDanhSach.Text = "Danh sách";
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 90);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(844, 660);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            // 
            // pnlTimKiem
            // 
            this.pnlTimKiem.Controls.Add(this.txtTimKiem);
            this.pnlTimKiem.Controls.Add(this.btnTimKiem);
            this.pnlTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTimKiem.Location = new System.Drawing.Point(0, 40);
            this.pnlTimKiem.Name = "pnlTimKiem";
            this.pnlTimKiem.Size = new System.Drawing.Size(844, 50);
            this.pnlTimKiem.TabIndex = 1;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtTimKiem.BorderRadius = 18;
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTimKiem.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Location = new System.Drawing.Point(213, 7);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Nhập từ khóa cần tìm...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(391, 36);
            this.txtTimKiem.TabIndex = 0;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnTimKiem.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Location = new System.Drawing.Point(710, 8);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(110, 34);
            this.btnTimKiem.TabIndex = 1;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.Visible = false;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.gbThongTin);
            this.pnlRight.Controls.Add(this.pnlSpacer);
            this.pnlRight.Controls.Add(this.gbChucNang);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Location = new System.Drawing.Point(0, 0);
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(15);
            this.pnlRight.Size = new System.Drawing.Size(446, 750);
            this.pnlRight.TabIndex = 1;
            // 
            // gbThongTin
            // 
            this.gbThongTin.AutoScroll = true;
            this.gbThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThongTin.Location = new System.Drawing.Point(15, 15);
            this.gbThongTin.Margin = new System.Windows.Forms.Padding(0);
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(416, 533);
            this.gbThongTin.TabIndex = 0;
            // 
            // pnlSpacer
            // 
            this.pnlSpacer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSpacer.Location = new System.Drawing.Point(15, 548);
            this.pnlSpacer.Name = "pnlSpacer";
            this.pnlSpacer.Size = new System.Drawing.Size(416, 15);
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
            this.gbChucNang.Location = new System.Drawing.Point(15, 563);
            this.gbChucNang.Margin = new System.Windows.Forms.Padding(0);
            this.gbChucNang.Name = "gbChucNang";
            this.gbChucNang.Size = new System.Drawing.Size(416, 172);
            this.gbChucNang.TabIndex = 2;
            this.gbChucNang.Text = "Chức năng";
            this.gbChucNang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnThem
            // 
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(25, 67);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(170, 37);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm mới";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSua.ForeColor = System.Drawing.Color.White;
            this.btnSua.Location = new System.Drawing.Point(215, 67);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(170, 37);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Cập nhật";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(25, 117);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(170, 37);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLamMoi.ForeColor = System.Drawing.Color.White;
            this.btnLamMoi.Location = new System.Drawing.Point(215, 117);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(170, 37);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // frmTroChoi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1300, 750);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmTroChoi";
            this.Text = "Quản Lý Trò Chơi";
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
                        this.lbl_txtTenTroChoi.AutoSize = true;
            this.lbl_txtTenTroChoi.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_txtTenTroChoi.Location = new System.Drawing.Point(20, 20);
            this.lbl_txtTenTroChoi.Name = "lbl_txtTenTroChoi";
            this.lbl_txtTenTroChoi.Text = "Tên Trò Chơi:";
            this.txtTenTroChoi.Location = new System.Drawing.Point(20, 45);
            this.txtTenTroChoi.Name = "txtTenTroChoi";
            this.txtTenTroChoi.Size = new System.Drawing.Size(370, 36);
            this.lbl_slkKhuVuc.AutoSize = true;
            this.lbl_slkKhuVuc.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_slkKhuVuc.Location = new System.Drawing.Point(20, 90);
            this.lbl_slkKhuVuc.Name = "lbl_slkKhuVuc";
            this.lbl_slkKhuVuc.Text = "Khu Vực:";
            this.slkKhuVuc.Location = new System.Drawing.Point(20, 115);
            this.slkKhuVuc.Name = "slkKhuVuc";
            this.slkKhuVuc.Size = new System.Drawing.Size(370, 30);
            this.slkKhuVuc.Properties.PopupView = this.slkKhuVucView;
            // this.slkKhuVucView.Location = new System.Drawing.Point(20, 160);
            // this.slkKhuVucView.Name = "slkKhuVucView";
            this.lbl_cboTrangThai.AutoSize = true;
            this.lbl_cboTrangThai.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_cboTrangThai.Location = new System.Drawing.Point(20, 205);
            this.lbl_cboTrangThai.Name = "lbl_cboTrangThai";
            this.lbl_cboTrangThai.Text = "Trạng Thái:";
            this.cboTrangThai.Location = new System.Drawing.Point(20, 230);
            this.cboTrangThai.Name = "cboTrangThai";
            this.cboTrangThai.Size = new System.Drawing.Size(370, 36);
            this.lbl_txtMoTa.AutoSize = true;
            this.lbl_txtMoTa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lbl_txtMoTa.Location = new System.Drawing.Point(20, 275);
            this.lbl_txtMoTa.Name = "lbl_txtMoTa";
            this.lbl_txtMoTa.Text = "Mô Tả:";
            this.txtMoTa.Location = new System.Drawing.Point(20, 300);
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.Size = new System.Drawing.Size(370, 80);
            this.txtMoTa.Multiline = true;
            this.gbThongTin.Controls.Add(this.lbl_txtTenTroChoi);
            this.gbThongTin.Controls.Add(this.txtTenTroChoi);
            this.gbThongTin.Controls.Add(this.lbl_slkKhuVuc);
            this.gbThongTin.Controls.Add(this.slkKhuVuc);
            this.gbThongTin.Controls.Add(this.lbl_cboTrangThai);
            this.gbThongTin.Controls.Add(this.cboTrangThai);
            this.gbThongTin.Controls.Add(this.lbl_txtMoTa);
            this.gbThongTin.Controls.Add(this.txtMoTa);
this.pnlRight.ResumeLayout(false);
            this.gbChucNang.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        public Guna.UI2.WinForms.Guna2TextBox txtTenTroChoi;
        public System.Windows.Forms.Label lbl_txtTenTroChoi;
        public DevExpress.XtraEditors.SearchLookUpEdit slkKhuVuc;
        public System.Windows.Forms.Label lbl_slkKhuVuc;
        public DevExpress.XtraGrid.Views.Grid.GridView slkKhuVucView;
        public Guna.UI2.WinForms.Guna2ComboBox cboTrangThai;
        public System.Windows.Forms.Label lbl_cboTrangThai;
        public Guna.UI2.WinForms.Guna2TextBox txtMoTa;
        public System.Windows.Forms.Label lbl_txtMoTa;


        protected internal DevExpress.XtraEditors.SplitContainerControl pnlMain;
        protected Guna.UI2.WinForms.Guna2Panel pnlLeft;
        protected Guna.UI2.WinForms.Guna2GroupBox gbDanhSach;
        protected DevExpress.XtraGrid.GridControl gridControl;
        protected DevExpress.XtraGrid.Views.Grid.GridView gridView;
        protected Guna.UI2.WinForms.Guna2Panel pnlTimKiem;
        protected Guna.UI2.WinForms.Guna2TextBox txtTimKiem;
        protected Guna.UI2.WinForms.Guna2Button btnTimKiem;
        protected Guna.UI2.WinForms.Guna2Panel pnlRight;
        protected System.Windows.Forms.Panel gbThongTin;
        protected Guna.UI2.WinForms.Guna2Panel pnlSpacer;
        protected Guna.UI2.WinForms.Guna2GroupBox gbChucNang;
        protected Guna.UI2.WinForms.Guna2Button btnThem;
        protected Guna.UI2.WinForms.Guna2Button btnSua;
        protected Guna.UI2.WinForms.Guna2Button btnXoa;
        protected Guna.UI2.WinForms.Guna2Button btnLamMoi;
    }
}









