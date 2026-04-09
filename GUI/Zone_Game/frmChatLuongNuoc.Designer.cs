namespace GUI
{
    partial class frmChatLuongNuoc
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
            this.slkKhuVucBien = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.lbl_slkKhuVucBien = new System.Windows.Forms.Label();
            this.slkKhuVucBienView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.spnDoMan = new DevExpress.XtraEditors.SpinEdit();
            this.lbl_spnDoMan = new System.Windows.Forms.Label();
            this.spnPH = new DevExpress.XtraEditors.SpinEdit();
            this.lbl_spnPH = new System.Windows.Forms.Label();
            this.spnNhietDo = new DevExpress.XtraEditors.SpinEdit();
            this.lbl_spnNhietDo = new System.Windows.Forms.Label();
            this.spnDoTrong = new DevExpress.XtraEditors.SpinEdit();
            this.lbl_spnDoTrong = new System.Windows.Forms.Label();
            this.cboVeSinh = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lbl_cboVeSinh = new System.Windows.Forms.Label();
            this.dtNgay = new DevExpress.XtraEditors.DateEdit();
            this.lbl_dtNgay = new System.Windows.Forms.Label();
            this.pnlMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnlLeft = new Guna.UI2.WinForms.Guna2Panel();
            this.gbDanhSach = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlFilter = new Guna.UI2.WinForms.Guna2Panel();
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
            this.pnlFilter.SuspendLayout();
            this.pnlRight.SuspendLayout();
            this.gbChucNang.SuspendLayout();
            this.gbThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhuVucBien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDoMan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnPH.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnNhietDo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDoTrong.Properties)).BeginInit();
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
            this.pnlMain.ShowSplitGlyph = DevExpress.Utils.DefaultBoolean.True;
            this.pnlMain.Size = new System.Drawing.Size(1300, 750);
            this.pnlMain.SplitterPosition = 446;
            this.pnlMain.TabIndex = 0;
            // 
            // pnlLeft
            // 
            this.pnlLeft.Controls.Add(this.gbDanhSach);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(844, 750);
            // 
            // gbDanhSach
            // 
            this.gbDanhSach.Controls.Add(this.gridControl);
            this.gbDanhSach.Controls.Add(this.pnlFilter);
            this.gbDanhSach.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbDanhSach.Name = "gbDanhSach";
            this.gbDanhSach.Size = new System.Drawing.Size(844, 750);
            this.gbDanhSach.Text = "Lịch Sử Chất Lượng Nước";
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { this.gridView });
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.gridView_FocusedRowChanged);
            // 
            // pnlFilter — filter bar with SearchLookUp for selecting beach
            // 
            this.pnlFilter.Controls.Add(this.lbl_slkKhuVucBien);
            this.pnlFilter.Controls.Add(this.slkKhuVucBien);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 40);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Size = new System.Drawing.Size(844, 55);
            this.lbl_slkKhuVucBien.AutoSize = true;
            this.lbl_slkKhuVucBien.Location = new System.Drawing.Point(15, 18);
            this.lbl_slkKhuVucBien.Text = "Khu Biển:";
            this.slkKhuVucBien.Location = new System.Drawing.Point(100, 14);
            this.slkKhuVucBien.Name = "slkKhuVucBien";
            this.slkKhuVucBien.Size = new System.Drawing.Size(350, 30);
            this.slkKhuVucBien.Properties.PopupView = this.slkKhuVucBienView;
            // 
            // pnlRight
            // 
            this.pnlRight.Controls.Add(this.gbThongTin);
            this.pnlRight.Controls.Add(this.pnlSpacer);
            this.pnlRight.Controls.Add(this.gbChucNang);
            this.pnlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlRight.Name = "pnlRight";
            this.pnlRight.Padding = new System.Windows.Forms.Padding(15);
            this.pnlRight.Size = new System.Drawing.Size(446, 750);
            // 
            // gbThongTin
            // 
            this.gbThongTin.AutoScroll = true;
            this.gbThongTin.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThongTin.Name = "gbThongTin";
            this.gbThongTin.Size = new System.Drawing.Size(416, 533);
            // pnlSpacer
            this.pnlSpacer.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSpacer.Name = "pnlSpacer";
            this.pnlSpacer.Size = new System.Drawing.Size(416, 15);
            // gbChucNang
            this.gbChucNang.Controls.Add(this.btnThem);
            this.gbChucNang.Controls.Add(this.btnSua);
            this.gbChucNang.Controls.Add(this.btnXoa);
            this.gbChucNang.Controls.Add(this.btnLamMoi);
            this.gbChucNang.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.gbChucNang.Name = "gbChucNang";
            this.gbChucNang.Size = new System.Drawing.Size(416, 172);
            this.gbChucNang.Text = "Chức năng";
            this.gbChucNang.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // buttons
            this.btnThem.Location = new System.Drawing.Point(25, 67);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(170, 37);
            this.btnThem.Text = "Ghi nhận";
            this.btnThem.Click += new System.EventHandler(this.btnThem_Click);
            this.btnSua.Location = new System.Drawing.Point(215, 67);
            this.btnSua.Name = "btnSua";
            this.btnSua.Size = new System.Drawing.Size(170, 37);
            this.btnSua.Text = "Cập nhật";
            this.btnSua.Click += new System.EventHandler(this.btnSua_Click);
            this.btnXoa.Location = new System.Drawing.Point(25, 117);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(170, 37);
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            this.btnLamMoi.Location = new System.Drawing.Point(215, 117);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(170, 37);
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // Info fields
            // 
            this.lbl_dtNgay.AutoSize = true;
            this.lbl_dtNgay.Location = new System.Drawing.Point(20, 20);
            this.lbl_dtNgay.Text = "Ngày Đo:";
            this.dtNgay.Location = new System.Drawing.Point(20, 45);
            this.dtNgay.Name = "dtNgay";
            this.dtNgay.Size = new System.Drawing.Size(370, 36);
            this.lbl_spnDoMan.AutoSize = true;
            this.lbl_spnDoMan.Location = new System.Drawing.Point(20, 90);
            this.lbl_spnDoMan.Text = "Độ Mặn (‰):";
            this.spnDoMan.Location = new System.Drawing.Point(20, 115);
            this.spnDoMan.Name = "spnDoMan";
            this.spnDoMan.Size = new System.Drawing.Size(370, 30);
            this.spnDoMan.Properties.Mask.EditMask = "n2";
            this.spnDoMan.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.lbl_spnPH.AutoSize = true;
            this.lbl_spnPH.Location = new System.Drawing.Point(20, 160);
            this.lbl_spnPH.Text = "Độ pH:";
            this.spnPH.Location = new System.Drawing.Point(20, 185);
            this.spnPH.Name = "spnPH";
            this.spnPH.Size = new System.Drawing.Size(370, 30);
            this.spnPH.Properties.Mask.EditMask = "n2";
            this.spnPH.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.lbl_spnNhietDo.AutoSize = true;
            this.lbl_spnNhietDo.Location = new System.Drawing.Point(20, 230);
            this.lbl_spnNhietDo.Text = "Nhiệt Độ (°C):";
            this.spnNhietDo.Location = new System.Drawing.Point(20, 255);
            this.spnNhietDo.Name = "spnNhietDo";
            this.spnNhietDo.Size = new System.Drawing.Size(370, 30);
            this.spnNhietDo.Properties.Mask.EditMask = "n1";
            this.spnNhietDo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] { new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo) });
            this.lbl_spnDoTrong.AutoSize = true;
            this.lbl_spnDoTrong.Location = new System.Drawing.Point(20, 300);
            this.lbl_spnDoTrong.Text = "Độ Trong (cm):";
            this.spnDoTrong.Location = new System.Drawing.Point(20, 325);
            this.spnDoTrong.Name = "spnDoTrong";
            this.spnDoTrong.Size = new System.Drawing.Size(370, 30);
            this.lbl_cboVeSinh.AutoSize = true;
            this.lbl_cboVeSinh.Location = new System.Drawing.Point(20, 370);
            this.lbl_cboVeSinh.Text = "Trạng Thái Vệ Sinh:";
            this.cboVeSinh.Location = new System.Drawing.Point(20, 395);
            this.cboVeSinh.Name = "cboVeSinh";
            this.cboVeSinh.Size = new System.Drawing.Size(370, 36);
            this.gbThongTin.Controls.Add(this.lbl_dtNgay);
            this.gbThongTin.Controls.Add(this.dtNgay);
            this.gbThongTin.Controls.Add(this.lbl_spnDoMan);
            this.gbThongTin.Controls.Add(this.spnDoMan);
            this.gbThongTin.Controls.Add(this.lbl_spnPH);
            this.gbThongTin.Controls.Add(this.spnPH);
            this.gbThongTin.Controls.Add(this.lbl_spnNhietDo);
            this.gbThongTin.Controls.Add(this.spnNhietDo);
            this.gbThongTin.Controls.Add(this.lbl_spnDoTrong);
            this.gbThongTin.Controls.Add(this.spnDoTrong);
            this.gbThongTin.Controls.Add(this.lbl_cboVeSinh);
            this.gbThongTin.Controls.Add(this.cboVeSinh);
            // 
            // frmChatLuongNuoc
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 750);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmChatLuongNuoc";
            this.Text = "Giám Sát Chất Lượng Nước";
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
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            this.pnlRight.ResumeLayout(false);
            this.gbChucNang.ResumeLayout(false);
            this.gbThongTin.ResumeLayout(false);
            this.gbThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhuVucBien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDoMan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnPH.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnNhietDo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnDoTrong.Properties)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        protected internal DevExpress.XtraEditors.SplitContainerControl pnlMain;
        protected Guna.UI2.WinForms.Guna2Panel pnlLeft;
        protected Guna.UI2.WinForms.Guna2GroupBox gbDanhSach;
        protected DevExpress.XtraGrid.GridControl gridControl;
        protected DevExpress.XtraGrid.Views.Grid.GridView gridView;
        protected Guna.UI2.WinForms.Guna2Panel pnlFilter;
        protected Guna.UI2.WinForms.Guna2Panel pnlRight;
        protected System.Windows.Forms.Panel gbThongTin;
        protected Guna.UI2.WinForms.Guna2Panel pnlSpacer;
        protected Guna.UI2.WinForms.Guna2GroupBox gbChucNang;
        protected Guna.UI2.WinForms.Guna2Button btnThem;
        protected Guna.UI2.WinForms.Guna2Button btnSua;
        protected Guna.UI2.WinForms.Guna2Button btnXoa;
        protected Guna.UI2.WinForms.Guna2Button btnLamMoi;
        public DevExpress.XtraEditors.SearchLookUpEdit slkKhuVucBien;
        public System.Windows.Forms.Label lbl_slkKhuVucBien;
        public DevExpress.XtraGrid.Views.Grid.GridView slkKhuVucBienView;
        public DevExpress.XtraEditors.DateEdit dtNgay;
        public System.Windows.Forms.Label lbl_dtNgay;
        public DevExpress.XtraEditors.SpinEdit spnDoMan;
        public System.Windows.Forms.Label lbl_spnDoMan;
        public DevExpress.XtraEditors.SpinEdit spnPH;
        public System.Windows.Forms.Label lbl_spnPH;
        public DevExpress.XtraEditors.SpinEdit spnNhietDo;
        public System.Windows.Forms.Label lbl_spnNhietDo;
        public DevExpress.XtraEditors.SpinEdit spnDoTrong;
        public System.Windows.Forms.Label lbl_spnDoTrong;
        public Guna.UI2.WinForms.Guna2ComboBox cboVeSinh;
        public System.Windows.Forms.Label lbl_cboVeSinh;
    }
}

