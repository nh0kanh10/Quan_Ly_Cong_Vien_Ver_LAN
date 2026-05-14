namespace GUI.Modules.NhanSu
{
    partial class frmHoSoNhanVien
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
            this.tcMain = new DevExpress.XtraTab.XtraTabControl();
            this.tabHoSo = new DevExpress.XtraTab.XtraTabPage();
            this.layoutHoSo = new DevExpress.XtraLayout.LayoutControl();
            this.txtMaNhanVien = new DevExpress.XtraEditors.TextEdit();
            this.txtHoTen = new DevExpress.XtraEditors.TextEdit();
            this.txtDienThoai = new DevExpress.XtraEditors.TextEdit();
            this.txtPhongBan = new DevExpress.XtraEditors.TextEdit();
            this.txtChucVu = new DevExpress.XtraEditors.TextEdit();
            this.dtNgayVaoLam = new DevExpress.XtraEditors.DateEdit();
            this.Root = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciMaNhanVien = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciHoTen = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDienThoai = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciPhongBan = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciChucVu = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciNgayVaoLam = new DevExpress.XtraLayout.LayoutControlItem();
            this.tabHopDong = new DevExpress.XtraTab.XtraTabPage();
            this.gcHopDong = new DevExpress.XtraGrid.GridControl();
            this.gvHopDong = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabLichLamViec = new DevExpress.XtraTab.XtraTabPage();
            this.gcLichLamViec = new DevExpress.XtraGrid.GridControl();
            this.gvLichLamViec = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabKPI = new DevExpress.XtraTab.XtraTabPage();
            this.gcKPI = new DevExpress.XtraGrid.GridControl();
            this.gvKPI = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlFooter = new DevExpress.XtraEditors.PanelControl();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.btnChinhSua = new DevExpress.XtraEditors.SimpleButton();
            this.btnLuu = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.tcMain)).BeginInit();
            this.tcMain.SuspendLayout();
            this.tabHoSo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutHoSo)).BeginInit();
            this.layoutHoSo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaNhanVien.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoTen.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienThoai.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhongBan.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChucVu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayVaoLam.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayVaoLam.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMaNhanVien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciHoTen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDienThoai)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPhongBan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciChucVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNgayVaoLam)).BeginInit();
            this.tabHopDong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcHopDong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHopDong)).BeginInit();
            this.tabLichLamViec.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcLichLamViec)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLichLamViec)).BeginInit();
            this.tabKPI.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcKPI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvKPI)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).BeginInit();
            this.pnlFooter.SuspendLayout();
            this.SuspendLayout();
            // 
            // tcMain
            // 
            this.tcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tcMain.Location = new System.Drawing.Point(0, 0);
            this.tcMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tcMain.Name = "tcMain";
            this.tcMain.SelectedTabPage = this.tabHoSo;
            this.tcMain.Size = new System.Drawing.Size(686, 407);
            this.tcMain.TabIndex = 0;
            this.tcMain.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabHoSo,
            this.tabHopDong,
            this.tabLichLamViec,
            this.tabKPI});
            // 
            // tabHoSo
            // 
            this.tabHoSo.Controls.Add(this.layoutHoSo);
            this.tabHoSo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabHoSo.Name = "tabHoSo";
            this.tabHoSo.Size = new System.Drawing.Size(684, 382);
            this.tabHoSo.Text = "Hồ sơ cá nhân";
            // 
            // layoutHoSo
            // 
            this.layoutHoSo.Controls.Add(this.txtMaNhanVien);
            this.layoutHoSo.Controls.Add(this.txtHoTen);
            this.layoutHoSo.Controls.Add(this.txtDienThoai);
            this.layoutHoSo.Controls.Add(this.txtPhongBan);
            this.layoutHoSo.Controls.Add(this.txtChucVu);
            this.layoutHoSo.Controls.Add(this.dtNgayVaoLam);
            this.layoutHoSo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutHoSo.Location = new System.Drawing.Point(0, 0);
            this.layoutHoSo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.layoutHoSo.Name = "layoutHoSo";
            this.layoutHoSo.Root = this.Root;
            this.layoutHoSo.Size = new System.Drawing.Size(684, 382);
            this.layoutHoSo.TabIndex = 0;
            // 
            // txtMaNhanVien
            // 
            this.txtMaNhanVien.Location = new System.Drawing.Point(90, 10);
            this.txtMaNhanVien.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtMaNhanVien.Name = "txtMaNhanVien";
            this.txtMaNhanVien.Properties.ReadOnly = true;
            this.txtMaNhanVien.Size = new System.Drawing.Size(250, 20);
            this.txtMaNhanVien.StyleController = this.layoutHoSo;
            this.txtMaNhanVien.TabIndex = 4;
            // 
            // txtHoTen
            // 
            this.txtHoTen.Location = new System.Drawing.Point(423, 10);
            this.txtHoTen.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtHoTen.Name = "txtHoTen";
            this.txtHoTen.Properties.ReadOnly = true;
            this.txtHoTen.Size = new System.Drawing.Size(250, 20);
            this.txtHoTen.StyleController = this.layoutHoSo;
            this.txtHoTen.TabIndex = 5;
            // 
            // txtDienThoai
            // 
            this.txtDienThoai.Location = new System.Drawing.Point(90, 34);
            this.txtDienThoai.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtDienThoai.Name = "txtDienThoai";
            this.txtDienThoai.Properties.ReadOnly = true;
            this.txtDienThoai.Size = new System.Drawing.Size(250, 20);
            this.txtDienThoai.StyleController = this.layoutHoSo;
            this.txtDienThoai.TabIndex = 6;
            // 
            // txtPhongBan
            // 
            this.txtPhongBan.Location = new System.Drawing.Point(423, 34);
            this.txtPhongBan.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtPhongBan.Name = "txtPhongBan";
            this.txtPhongBan.Properties.ReadOnly = true;
            this.txtPhongBan.Size = new System.Drawing.Size(250, 20);
            this.txtPhongBan.StyleController = this.layoutHoSo;
            this.txtPhongBan.TabIndex = 7;
            // 
            // txtChucVu
            // 
            this.txtChucVu.Location = new System.Drawing.Point(90, 58);
            this.txtChucVu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtChucVu.Name = "txtChucVu";
            this.txtChucVu.Properties.ReadOnly = true;
            this.txtChucVu.Size = new System.Drawing.Size(250, 20);
            this.txtChucVu.StyleController = this.layoutHoSo;
            this.txtChucVu.TabIndex = 8;
            // 
            // dtNgayVaoLam
            // 
            this.dtNgayVaoLam.EditValue = null;
            this.dtNgayVaoLam.Location = new System.Drawing.Point(423, 58);
            this.dtNgayVaoLam.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.dtNgayVaoLam.Name = "dtNgayVaoLam";
            this.dtNgayVaoLam.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayVaoLam.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtNgayVaoLam.Properties.ReadOnly = true;
            this.dtNgayVaoLam.Size = new System.Drawing.Size(250, 20);
            this.dtNgayVaoLam.StyleController = this.layoutHoSo;
            this.dtNgayVaoLam.TabIndex = 9;
            // 
            // Root
            // 
            this.Root.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.Root.GroupBordersVisible = false;
            this.Root.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciMaNhanVien,
            this.lciHoTen,
            this.lciDienThoai,
            this.lciPhongBan,
            this.lciChucVu,
            this.lciNgayVaoLam});
            this.Root.Name = "Root";
            this.Root.Size = new System.Drawing.Size(684, 382);
            this.Root.TextVisible = false;
            // 
            // lciMaNhanVien
            // 
            this.lciMaNhanVien.Control = this.txtMaNhanVien;
            this.lciMaNhanVien.Location = new System.Drawing.Point(0, 0);
            this.lciMaNhanVien.Name = "lciMaNhanVien";
            this.lciMaNhanVien.Size = new System.Drawing.Size(333, 24);
            this.lciMaNhanVien.Text = "Mã nhân viên:";
            this.lciMaNhanVien.TextSize = new System.Drawing.Size(69, 13);
            // 
            // lciHoTen
            // 
            this.lciHoTen.Control = this.txtHoTen;
            this.lciHoTen.Location = new System.Drawing.Point(333, 0);
            this.lciHoTen.Name = "lciHoTen";
            this.lciHoTen.Size = new System.Drawing.Size(333, 24);
            this.lciHoTen.Text = "Họ tên:";
            this.lciHoTen.TextSize = new System.Drawing.Size(69, 13);
            // 
            // lciDienThoai
            // 
            this.lciDienThoai.Control = this.txtDienThoai;
            this.lciDienThoai.Location = new System.Drawing.Point(0, 24);
            this.lciDienThoai.Name = "lciDienThoai";
            this.lciDienThoai.Size = new System.Drawing.Size(333, 24);
            this.lciDienThoai.Text = "Điện thoại:";
            this.lciDienThoai.TextSize = new System.Drawing.Size(69, 13);
            // 
            // lciPhongBan
            // 
            this.lciPhongBan.Control = this.txtPhongBan;
            this.lciPhongBan.Location = new System.Drawing.Point(333, 24);
            this.lciPhongBan.Name = "lciPhongBan";
            this.lciPhongBan.Size = new System.Drawing.Size(333, 24);
            this.lciPhongBan.Text = "Phòng ban:";
            this.lciPhongBan.TextSize = new System.Drawing.Size(69, 13);
            // 
            // lciChucVu
            // 
            this.lciChucVu.Control = this.txtChucVu;
            this.lciChucVu.Location = new System.Drawing.Point(0, 48);
            this.lciChucVu.Name = "lciChucVu";
            this.lciChucVu.Size = new System.Drawing.Size(333, 318);
            this.lciChucVu.Text = "Chức vụ:";
            this.lciChucVu.TextSize = new System.Drawing.Size(69, 13);
            // 
            // lciNgayVaoLam
            // 
            this.lciNgayVaoLam.Control = this.dtNgayVaoLam;
            this.lciNgayVaoLam.Location = new System.Drawing.Point(333, 48);
            this.lciNgayVaoLam.Name = "lciNgayVaoLam";
            this.lciNgayVaoLam.Size = new System.Drawing.Size(333, 318);
            this.lciNgayVaoLam.Text = "Ngày vào làm:";
            this.lciNgayVaoLam.TextSize = new System.Drawing.Size(69, 13);
            // 
            // tabHopDong
            // 
            this.tabHopDong.Controls.Add(this.gcHopDong);
            this.tabHopDong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabHopDong.Name = "tabHopDong";
            this.tabHopDong.Size = new System.Drawing.Size(684, 382);
            this.tabHopDong.Text = "Hợp đồng lao động";
            // 
            // gcHopDong
            // 
            this.gcHopDong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcHopDong.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcHopDong.Location = new System.Drawing.Point(0, 0);
            this.gcHopDong.MainView = this.gvHopDong;
            this.gcHopDong.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcHopDong.Name = "gcHopDong";
            this.gcHopDong.Size = new System.Drawing.Size(684, 382);
            this.gcHopDong.TabIndex = 0;
            this.gcHopDong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvHopDong});
            // 
            // gvHopDong
            // 
            this.gvHopDong.DetailHeight = 284;
            this.gvHopDong.GridControl = this.gcHopDong;
            this.gvHopDong.Name = "gvHopDong";
            this.gvHopDong.OptionsBehavior.Editable = false;
            this.gvHopDong.OptionsView.ShowGroupPanel = false;
            // 
            // tabLichLamViec
            // 
            this.tabLichLamViec.Controls.Add(this.gcLichLamViec);
            this.tabLichLamViec.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabLichLamViec.Name = "tabLichLamViec";
            this.tabLichLamViec.Size = new System.Drawing.Size(684, 382);
            this.tabLichLamViec.Text = "Lịch làm việc";
            // 
            // gcLichLamViec
            // 
            this.gcLichLamViec.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcLichLamViec.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcLichLamViec.Location = new System.Drawing.Point(0, 0);
            this.gcLichLamViec.MainView = this.gvLichLamViec;
            this.gcLichLamViec.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcLichLamViec.Name = "gcLichLamViec";
            this.gcLichLamViec.Size = new System.Drawing.Size(684, 382);
            this.gcLichLamViec.TabIndex = 0;
            this.gcLichLamViec.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvLichLamViec});
            // 
            // gvLichLamViec
            // 
            this.gvLichLamViec.DetailHeight = 284;
            this.gvLichLamViec.GridControl = this.gcLichLamViec;
            this.gvLichLamViec.Name = "gvLichLamViec";
            this.gvLichLamViec.OptionsBehavior.Editable = false;
            this.gvLichLamViec.OptionsView.ShowGroupPanel = false;
            // 
            // tabKPI
            // 
            this.tabKPI.Controls.Add(this.gcKPI);
            this.tabKPI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tabKPI.Name = "tabKPI";
            this.tabKPI.Size = new System.Drawing.Size(684, 382);
            this.tabKPI.Text = "Đánh giá KPIs";
            // 
            // gcKPI
            // 
            this.gcKPI.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gcKPI.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcKPI.Location = new System.Drawing.Point(0, 0);
            this.gcKPI.MainView = this.gvKPI;
            this.gcKPI.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.gcKPI.Name = "gcKPI";
            this.gcKPI.Size = new System.Drawing.Size(684, 382);
            this.gcKPI.TabIndex = 0;
            this.gcKPI.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvKPI});
            // 
            // gvKPI
            // 
            this.gvKPI.DetailHeight = 284;
            this.gvKPI.GridControl = this.gcKPI;
            this.gvKPI.Name = "gvKPI";
            this.gvKPI.OptionsBehavior.Editable = false;
            this.gvKPI.OptionsView.ShowGroupPanel = false;
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.btnHuy);
            this.pnlFooter.Controls.Add(this.btnChinhSua);
            this.pnlFooter.Controls.Add(this.btnLuu);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.Location = new System.Drawing.Point(0, 407);
            this.pnlFooter.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(686, 32);
            this.pnlFooter.TabIndex = 1;
            // 
            // btnHuy
            // 
            this.btnHuy.ImageOptions.ImageUri.Uri = "Cancel";
            this.btnHuy.Location = new System.Drawing.Point(432, 6);
            this.btnHuy.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(77, 21);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Visible = false;
            this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            // 
            // btnChinhSua
            // 
            this.btnChinhSua.ImageOptions.ImageUri.Uri = "Edit";
            this.btnChinhSua.Location = new System.Drawing.Point(597, 6);
            this.btnChinhSua.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnChinhSua.Name = "btnChinhSua";
            this.btnChinhSua.Size = new System.Drawing.Size(77, 21);
            this.btnChinhSua.TabIndex = 1;
            this.btnChinhSua.Text = "Chỉnh sửa";
            this.btnChinhSua.Click += new System.EventHandler(this.BtnChinhSua_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.ImageOptions.ImageUri.Uri = "Save";
            this.btnLuu.Location = new System.Drawing.Point(514, 6);
            this.btnLuu.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(77, 21);
            this.btnLuu.TabIndex = 0;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Visible = false;
            this.btnLuu.Click += new System.EventHandler(this.BtnLuu_Click);
            // 
            // frmHoSoNhanVien
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(686, 439);
            this.Controls.Add(this.tcMain);
            this.Controls.Add(this.pnlFooter);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "frmHoSoNhanVien";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Hồ Sơ Nhân Viên";
            this.Load += new System.EventHandler(this.FrmHoSoNhanVien_Load);
            ((System.ComponentModel.ISupportInitialize)(this.tcMain)).EndInit();
            this.tcMain.ResumeLayout(false);
            this.tabHoSo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutHoSo)).EndInit();
            this.layoutHoSo.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtMaNhanVien.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtHoTen.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtDienThoai.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPhongBan.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtChucVu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayVaoLam.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtNgayVaoLam.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Root)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciMaNhanVien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciHoTen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDienThoai)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPhongBan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciChucVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciNgayVaoLam)).EndInit();
            this.tabHopDong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcHopDong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHopDong)).EndInit();
            this.tabLichLamViec.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcLichLamViec)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvLichLamViec)).EndInit();
            this.tabKPI.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcKPI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvKPI)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFooter)).EndInit();
            this.pnlFooter.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraTab.XtraTabControl tcMain;
        private DevExpress.XtraTab.XtraTabPage tabHoSo;
        private DevExpress.XtraTab.XtraTabPage tabHopDong;
        private DevExpress.XtraTab.XtraTabPage tabLichLamViec;
        private DevExpress.XtraTab.XtraTabPage tabKPI;
        
        // HoSo layout
        private DevExpress.XtraLayout.LayoutControl layoutHoSo;
        private DevExpress.XtraLayout.LayoutControlGroup Root;
        private DevExpress.XtraEditors.TextEdit txtMaNhanVien;
        private DevExpress.XtraEditors.TextEdit txtHoTen;
        private DevExpress.XtraEditors.TextEdit txtDienThoai;
        private DevExpress.XtraEditors.TextEdit txtPhongBan;
        private DevExpress.XtraEditors.TextEdit txtChucVu;
        private DevExpress.XtraEditors.DateEdit dtNgayVaoLam;
        
        private DevExpress.XtraLayout.LayoutControlItem lciMaNhanVien;
        private DevExpress.XtraLayout.LayoutControlItem lciHoTen;
        private DevExpress.XtraLayout.LayoutControlItem lciDienThoai;
        private DevExpress.XtraLayout.LayoutControlItem lciPhongBan;
        private DevExpress.XtraLayout.LayoutControlItem lciChucVu;
        private DevExpress.XtraLayout.LayoutControlItem lciNgayVaoLam;

        // HopDong layout
        private DevExpress.XtraGrid.GridControl gcHopDong;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHopDong;

        // LichLamViec layout
        private DevExpress.XtraGrid.GridControl gcLichLamViec;
        private DevExpress.XtraGrid.Views.Grid.GridView gvLichLamViec;
        // KPI layout
        private DevExpress.XtraGrid.GridControl gcKPI;
        private DevExpress.XtraGrid.Views.Grid.GridView gvKPI;

        // Footer
        private DevExpress.XtraEditors.PanelControl pnlFooter;
        private DevExpress.XtraEditors.SimpleButton btnLuu;
        private DevExpress.XtraEditors.SimpleButton btnChinhSua;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
    }
}
