namespace GUI
{
    partial class frmDongVat
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
            this.txtTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbl_txtTen = new System.Windows.Forms.Label();
            this.txtLoai = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbl_txtLoai = new System.Windows.Forms.Label();
            this.txtMoTa = new Guna.UI2.WinForms.Guna2TextBox();
            this.lbl_txtMoTa = new System.Windows.Forms.Label();
            this.dtNgaySinh = new DevExpress.XtraEditors.DateEdit();
            this.lbl_dtNgaySinh = new System.Windows.Forms.Label();
            this.cboSucKhoe = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lbl_cboSucKhoe = new System.Windows.Forms.Label();
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
            this.pnlLichChoAn = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridControlLCA = new DevExpress.XtraGrid.GridControl();
            this.gridViewLCA = new DevExpress.XtraGrid.Views.Grid.GridView();
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
            this.pnlLichChoAn.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLCA)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLCA)).BeginInit();
            this.SuspendLayout();
            // 
            // txtTen
            // 
            this.txtTen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTen.DefaultText = "";
            this.txtTen.Location = new System.Drawing.Point(20, 45);
            this.txtTen.Name = "txtTen";
            this.txtTen.PlaceholderText = "";
            this.txtTen.SelectedText = "";
            this.txtTen.Size = new System.Drawing.Size(370, 36);
            this.txtTen.TabIndex = 1;
            // 
            // lbl_txtTen
            // 
            this.lbl_txtTen.AutoSize = true;
            this.lbl_txtTen.Location = new System.Drawing.Point(20, 20);
            this.lbl_txtTen.Name = "lbl_txtTen";
            this.lbl_txtTen.Size = new System.Drawing.Size(85, 15);
            this.lbl_txtTen.TabIndex = 0;
            this.lbl_txtTen.Text = "Tên Động Vật:";
            // 
            // txtLoai
            // 
            this.txtLoai.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLoai.DefaultText = "";
            this.txtLoai.Location = new System.Drawing.Point(20, 115);
            this.txtLoai.Name = "txtLoai";
            this.txtLoai.PlaceholderText = "";
            this.txtLoai.SelectedText = "";
            this.txtLoai.Size = new System.Drawing.Size(370, 36);
            this.txtLoai.TabIndex = 3;
            // 
            // lbl_txtLoai
            // 
            this.lbl_txtLoai.AutoSize = true;
            this.lbl_txtLoai.Location = new System.Drawing.Point(20, 90);
            this.lbl_txtLoai.Name = "lbl_txtLoai";
            this.lbl_txtLoai.Size = new System.Drawing.Size(32, 15);
            this.lbl_txtLoai.TabIndex = 2;
            this.lbl_txtLoai.Text = "Loài:";
            // 
            // txtMoTa
            // 
            this.txtMoTa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtMoTa.DefaultText = "";
            this.txtMoTa.Location = new System.Drawing.Point(20, 325);
            this.txtMoTa.Multiline = true;
            this.txtMoTa.Name = "txtMoTa";
            this.txtMoTa.PlaceholderText = "";
            this.txtMoTa.SelectedText = "";
            this.txtMoTa.Size = new System.Drawing.Size(370, 80);
            this.txtMoTa.TabIndex = 9;
            // 
            // lbl_txtMoTa
            // 
            this.lbl_txtMoTa.AutoSize = true;
            this.lbl_txtMoTa.Location = new System.Drawing.Point(20, 300);
            this.lbl_txtMoTa.Name = "lbl_txtMoTa";
            this.lbl_txtMoTa.Size = new System.Drawing.Size(44, 15);
            this.lbl_txtMoTa.TabIndex = 8;
            this.lbl_txtMoTa.Text = "Mô Tả:";
            // 
            // dtNgaySinh
            // 
            this.dtNgaySinh.Location = new System.Drawing.Point(20, 185);
            this.dtNgaySinh.Name = "dtNgaySinh";
            this.dtNgaySinh.Size = new System.Drawing.Size(370, 36);
            this.dtNgaySinh.TabIndex = 5;
            this.dtNgaySinh.DateTime = System.DateTime.Now;
            // 
            // lbl_dtNgaySinh
            // 
            this.lbl_dtNgaySinh.AutoSize = true;
            this.lbl_dtNgaySinh.Location = new System.Drawing.Point(20, 160);
            this.lbl_dtNgaySinh.Name = "lbl_dtNgaySinh";
            this.lbl_dtNgaySinh.Size = new System.Drawing.Size(65, 15);
            this.lbl_dtNgaySinh.TabIndex = 4;
            this.lbl_dtNgaySinh.Text = "Ngày Sinh:";
            // 
            // cboSucKhoe
            // 
            this.cboSucKhoe.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSucKhoe.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSucKhoe.ItemHeight = 30;
            this.cboSucKhoe.Location = new System.Drawing.Point(20, 255);
            this.cboSucKhoe.Name = "cboSucKhoe";
            this.cboSucKhoe.Size = new System.Drawing.Size(370, 36);
            this.cboSucKhoe.TabIndex = 7;
            // 
            // lbl_cboSucKhoe
            // 
            this.lbl_cboSucKhoe.AutoSize = true;
            this.lbl_cboSucKhoe.Location = new System.Drawing.Point(20, 230);
            this.lbl_cboSucKhoe.Name = "lbl_cboSucKhoe";
            this.lbl_cboSucKhoe.Size = new System.Drawing.Size(124, 15);
            this.lbl_cboSucKhoe.TabIndex = 6;
            this.lbl_cboSucKhoe.Text = "Tình Trạng Sức Khỏe:";
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
            this.gbDanhSach.Controls.Add(this.pnlLichChoAn);
            this.gbDanhSach.Controls.Add(this.pnlTimKiem);
            this.gbDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDanhSach.Location = new System.Drawing.Point(0, 0);
            this.gbDanhSach.Name = "gbDanhSach";
            this.gbDanhSach.Size = new System.Drawing.Size(844, 750);
            this.gbDanhSach.TabIndex = 0;
            this.gbDanhSach.Text = "Danh sách Động Vật";
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 90);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(844, 430);
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
            // pnlLichChoAn
            // 
            this.pnlLichChoAn.Controls.Add(this.gridControlLCA);
            this.pnlLichChoAn.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlLichChoAn.Location = new System.Drawing.Point(0, 520);
            this.pnlLichChoAn.Name = "pnlLichChoAn";
            this.pnlLichChoAn.Size = new System.Drawing.Size(844, 230);
            this.pnlLichChoAn.TabIndex = 3;
            this.pnlLichChoAn.Text = "Lịch Cho Ăn";
            // 
            // gridControlLCA
            // 
            this.gridControlLCA.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlLCA.MainView = this.gridViewLCA;
            this.gridControlLCA.Name = "gridControlLCA";
            this.gridControlLCA.Size = new System.Drawing.Size(844, 190);
            this.gridControlLCA.TabIndex = 0;
            this.gridControlLCA.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewLCA});
            // 
            // gridViewLCA
            // 
            this.gridViewLCA.GridControl = this.gridControlLCA;
            this.gridViewLCA.Name = "gridViewLCA";
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
            this.txtTimKiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimKiem.DefaultText = "";
            this.txtTimKiem.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTimKiem.Location = new System.Drawing.Point(213, 7);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.PlaceholderText = "Nhập từ khóa cần tìm...";
            this.txtTimKiem.SelectedText = "";
            this.txtTimKiem.Size = new System.Drawing.Size(391, 36);
            this.txtTimKiem.TabIndex = 0;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(27, 8);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(180, 36);
            this.btnTimKiem.TabIndex = 1;
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
            this.gbThongTin.Controls.Add(this.lbl_txtTen);
            this.gbThongTin.Controls.Add(this.txtTen);
            this.gbThongTin.Controls.Add(this.lbl_txtLoai);
            this.gbThongTin.Controls.Add(this.txtLoai);
            this.gbThongTin.Controls.Add(this.lbl_dtNgaySinh);
            this.gbThongTin.Controls.Add(this.dtNgaySinh);
            this.gbThongTin.Controls.Add(this.lbl_cboSucKhoe);
            this.gbThongTin.Controls.Add(this.cboSucKhoe);
            this.gbThongTin.Controls.Add(this.lbl_txtMoTa);
            this.gbThongTin.Controls.Add(this.txtMoTa);
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
            this.gbChucNang.Location = new System.Drawing.Point(15, 563);
            this.gbChucNang.Name = "gbChucNang";
            this.gbChucNang.Size = new System.Drawing.Size(416, 172);
            this.gbChucNang.TabIndex = 2;
            this.gbChucNang.Text = "Chức năng";
            this.gbChucNang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // btnThem
            // 
            this.btnThem.Location = new System.Drawing.Point(25, 67);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(170, 37);
            this.btnThem.TabIndex = 0;
            this.btnThem.Text = "Thêm mới";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            // 
            // btnSua
            // 
            this.btnSua.Location = new System.Drawing.Point(215, 67);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(170, 37);
            this.btnSua.TabIndex = 1;
            this.btnSua.Text = "Cập nhật";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.Location = new System.Drawing.Point(25, 117);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(170, 37);
            this.btnXoa.TabIndex = 2;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Location = new System.Drawing.Point(215, 117);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(170, 37);
            this.btnLamMoi.TabIndex = 3;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // frmDongVat
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 750);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDongVat";
            this.Text = "Quản Lý Động Vật";
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
            this.pnlLichChoAn.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlLCA)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewLCA)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

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
        public Guna.UI2.WinForms.Guna2TextBox txtTen;
        public System.Windows.Forms.Label lbl_txtTen;
        public Guna.UI2.WinForms.Guna2TextBox txtLoai;
        public System.Windows.Forms.Label lbl_txtLoai;
        public Guna.UI2.WinForms.Guna2TextBox txtMoTa;
        public System.Windows.Forms.Label lbl_txtMoTa;
        public DevExpress.XtraEditors.DateEdit dtNgaySinh;
        public System.Windows.Forms.Label lbl_dtNgaySinh;
        public Guna.UI2.WinForms.Guna2ComboBox cboSucKhoe;
        public System.Windows.Forms.Label lbl_cboSucKhoe;
        private Guna.UI2.WinForms.Guna2GroupBox pnlLichChoAn;
        protected internal DevExpress.XtraGrid.GridControl gridControlLCA;
        protected internal DevExpress.XtraGrid.Views.Grid.GridView gridViewLCA;
    }
}

