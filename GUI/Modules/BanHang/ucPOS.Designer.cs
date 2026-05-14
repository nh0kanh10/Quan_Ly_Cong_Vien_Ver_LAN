using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraTab;

namespace GUI.Modules.BanHang
{
    partial class ucPOS
    {
        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucPOS));
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pnlBanner = new DevExpress.XtraEditors.PanelControl();
            this.lblCa = new DevExpress.XtraEditors.LabelControl();
            this.lblThuNgan = new DevExpress.XtraEditors.LabelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.picCamera = new System.Windows.Forms.PictureBox();
            this.gridDanhMuc = new DevExpress.XtraGrid.GridControl();
            this.tileViewDanhMuc = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.colTile_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTile_MaSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTile_TenSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTile_LoaiSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTile_DonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTile_IdBangGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTile_LaVatTu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.tileViewColumn1 = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.pnlTimKiem = new DevExpress.XtraEditors.PanelControl();
            this.txtBarcode = new DevExpress.XtraEditors.ButtonEdit();
            this.tabDanhMuc = new DevExpress.XtraTab.XtraTabControl();
            this.tabTatCa = new DevExpress.XtraTab.XtraTabPage();
            this.tabVe = new DevExpress.XtraTab.XtraTabPage();
            this.tabCombo = new DevExpress.XtraTab.XtraTabPage();
            this.tabAnUong = new DevExpress.XtraTab.XtraTabPage();
            this.tabHangHoa = new DevExpress.XtraTab.XtraTabPage();
            this.gridGioHang = new DevExpress.XtraGrid.GridControl();
            this.viewGioHang = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGH_IdSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGH_MaSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGH_TenSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGH_TenDonVi = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGH_DonGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGH_SoLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repBtnSoLuong = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colGH_ThanhTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGH_Xoa = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repBtnXoa = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colGH_IdBangGia = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGH_LaVatTu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGH_LoaiSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGH_GhiChu = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repDonVi = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.pnlKhachHang = new DevExpress.XtraEditors.GroupControl();
            this.lblDiemKhach = new DevExpress.XtraEditors.LabelControl();
            this.lblHangKhach = new DevExpress.XtraEditors.LabelControl();
            this.lblViKhach = new DevExpress.XtraEditors.LabelControl();
            this.lblTenKhach = new DevExpress.XtraEditors.LabelControl();
            this.btnBoChonKhach = new DevExpress.XtraEditors.SimpleButton();
            this.btnTimKhach = new DevExpress.XtraEditors.SimpleButton();
            this.txtTimKhach = new DevExpress.XtraEditors.TextEdit();
            this.pnlTong = new DevExpress.XtraEditors.PanelControl();
            this.lblTongValue = new DevExpress.XtraEditors.LabelControl();
            this.lblTong = new DevExpress.XtraEditors.LabelControl();
            this.lblGiamGiaValue = new DevExpress.XtraEditors.LabelControl();
            this.lblGiamGia = new DevExpress.XtraEditors.LabelControl();
            this.lblMoTaGiamGia = new DevExpress.XtraEditors.LabelControl();
            this.btnXoaKM = new DevExpress.XtraEditors.SimpleButton();
            this.lblVATValue = new DevExpress.XtraEditors.LabelControl();
            this.lblVAT = new DevExpress.XtraEditors.LabelControl();
            this.lblTienHangValue = new DevExpress.XtraEditors.LabelControl();
            this.lblTienHang = new DevExpress.XtraEditors.LabelControl();
            this.pnlHanhDong = new DevExpress.XtraEditors.PanelControl();
            this.btnDongPhien = new DevExpress.XtraEditors.SimpleButton();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.btnThanhToan = new DevExpress.XtraEditors.SimpleButton();
            this.btnHoanTra = new DevExpress.XtraEditors.SimpleButton();
            this.lblSpacer = new DevExpress.XtraEditors.LabelControl();
            this.btnXoaGio = new DevExpress.XtraEditors.SimpleButton();
            this.pnlStatus = new DevExpress.XtraEditors.PanelControl();
            this.lblStatus = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBanner)).BeginInit();
            this.pnlBanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).BeginInit();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDanhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileViewDanhMuc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTimKiem)).BeginInit();
            this.pnlTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabDanhMuc)).BeginInit();
            this.tabDanhMuc.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridGioHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewGioHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repBtnSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repBtnXoa)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repDonVi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlKhachHang)).BeginInit();
            this.pnlKhachHang.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKhach.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTong)).BeginInit();
            this.pnlTong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHanhDong)).BeginInit();
            this.pnlHanhDong.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlStatus)).BeginInit();
            this.pnlStatus.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlBanner
            // 
            this.pnlBanner.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(60)))), ((int)(((byte)(114)))));
            this.pnlBanner.Appearance.Options.UseBackColor = true;
            this.pnlBanner.Controls.Add(this.lblCa);
            this.pnlBanner.Controls.Add(this.lblThuNgan);
            this.pnlBanner.Controls.Add(this.lblTitle);
            this.pnlBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBanner.Location = new System.Drawing.Point(0, 0);
            this.pnlBanner.Name = "pnlBanner";
            this.pnlBanner.Padding = new System.Windows.Forms.Padding(15, 0, 15, 0);
            this.pnlBanner.Size = new System.Drawing.Size(1200, 50);
            this.pnlBanner.TabIndex = 0;
            // 
            // lblCa
            // 
            this.lblCa.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblCa.Appearance.ForeColor = System.Drawing.Color.Lavender;
            this.lblCa.Appearance.Options.UseFont = true;
            this.lblCa.Appearance.Options.UseForeColor = true;
            this.lblCa.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblCa.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblCa.ImageOptions.Alignment = System.Drawing.ContentAlignment.MiddleLeft;
            this.lblCa.ImageOptions.SvgImageSize = new System.Drawing.Size(18, 18);
            this.lblCa.Location = new System.Drawing.Point(833, 2);
            this.lblCa.Name = "lblCa";
            this.lblCa.Size = new System.Drawing.Size(100, 46);
            this.lblCa.TabIndex = 2;
            this.lblCa.Text = "Ca: ---";
            // 
            // lblThuNgan
            // 
            this.lblThuNgan.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblThuNgan.Appearance.ForeColor = System.Drawing.Color.SeaShell;
            this.lblThuNgan.Appearance.Options.UseFont = true;
            this.lblThuNgan.Appearance.Options.UseForeColor = true;
            this.lblThuNgan.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblThuNgan.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblThuNgan.Location = new System.Drawing.Point(933, 2);
            this.lblThuNgan.Name = "lblThuNgan";
            this.lblThuNgan.Size = new System.Drawing.Size(250, 46);
            this.lblThuNgan.TabIndex = 1;
            this.lblThuNgan.Text = "Thu ngân: ---";
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblTitle.Location = new System.Drawing.Point(17, 2);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(250, 46);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "ĐẠI NAM POS";
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.None;
            this.splitMain.Location = new System.Drawing.Point(0, 50);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.picCamera);
            this.splitMain.Panel1.Controls.Add(this.gridDanhMuc);
            this.splitMain.Panel1.Controls.Add(this.pnlTimKiem);
            this.splitMain.Panel1.Controls.Add(this.tabDanhMuc);
            this.splitMain.Panel1.Text = "Panel1";
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.gridGioHang);
            this.splitMain.Panel2.Controls.Add(this.pnlKhachHang);
            this.splitMain.Panel2.Controls.Add(this.pnlTong);
            this.splitMain.Panel2.Controls.Add(this.pnlHanhDong);
            this.splitMain.Panel2.Text = "Panel2";
            this.splitMain.Size = new System.Drawing.Size(1200, 620);
            this.splitMain.SplitterPosition = 650;
            this.splitMain.TabIndex = 1;
            // 
            // picCamera
            // 
            this.picCamera.BackColor = System.Drawing.Color.Black;
            this.picCamera.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picCamera.Location = new System.Drawing.Point(472, 166);
            this.picCamera.Name = "picCamera";
            this.picCamera.Size = new System.Drawing.Size(260, 200);
            this.picCamera.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picCamera.TabIndex = 6;
            this.picCamera.TabStop = false;
            this.picCamera.Visible = false;
            // 
            // gridDanhMuc
            // 
            this.gridDanhMuc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridDanhMuc.Location = new System.Drawing.Point(0, 35);
            this.gridDanhMuc.MainView = this.tileViewDanhMuc;
            this.gridDanhMuc.Name = "gridDanhMuc";
            this.gridDanhMuc.Size = new System.Drawing.Size(650, 540);
            this.gridDanhMuc.TabIndex = 1;
            this.gridDanhMuc.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tileViewDanhMuc});
            // 
            // tileViewDanhMuc
            // 
            this.tileViewDanhMuc.Appearance.ItemHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(244)))), ((int)(((byte)(255)))));
            this.tileViewDanhMuc.Appearance.ItemHovered.Options.UseBackColor = true;
            this.tileViewDanhMuc.Appearance.ItemNormal.BackColor = System.Drawing.Color.White;
            this.tileViewDanhMuc.Appearance.ItemNormal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(200)))), ((int)(((byte)(210)))), ((int)(((byte)(230)))));
            this.tileViewDanhMuc.Appearance.ItemNormal.Options.UseBackColor = true;
            this.tileViewDanhMuc.Appearance.ItemNormal.Options.UseBorderColor = true;
            this.tileViewDanhMuc.Appearance.ItemPressed.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(80)))));
            this.tileViewDanhMuc.Appearance.ItemPressed.ForeColor = System.Drawing.Color.White;
            this.tileViewDanhMuc.Appearance.ItemPressed.Options.UseBackColor = true;
            this.tileViewDanhMuc.Appearance.ItemPressed.Options.UseForeColor = true;
            this.tileViewDanhMuc.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTile_Id,
            this.colTile_MaSanPham,
            this.colTile_TenSanPham,
            this.colTile_LoaiSanPham,
            this.colTile_DonGia,
            this.colTile_IdBangGia,
            this.colTile_LaVatTu,
            this.tileViewColumn1});
            this.tileViewDanhMuc.GridControl = this.gridDanhMuc;
            this.tileViewDanhMuc.Name = "tileViewDanhMuc";
            this.tileViewDanhMuc.OptionsTiles.IndentBetweenItems = 15;
            this.tileViewDanhMuc.OptionsTiles.ItemBorderVisibility = DevExpress.XtraEditors.TileItemBorderVisibility.Never;
            this.tileViewDanhMuc.OptionsTiles.ItemPadding = new System.Windows.Forms.Padding(0);
            this.tileViewDanhMuc.OptionsTiles.ItemSize = new System.Drawing.Size(180, 250);
            this.tileViewDanhMuc.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tileViewDanhMuc.OptionsTiles.Padding = new System.Windows.Forms.Padding(5);
            this.tileViewDanhMuc.OptionsTiles.RowCount = 0;
            this.tileViewDanhMuc.TileHtmlTemplate.Styles = resources.GetString("tileViewDanhMuc.TileHtmlTemplate.Styles");
            this.tileViewDanhMuc.TileHtmlTemplate.Template = resources.GetString("tileViewDanhMuc.TileHtmlTemplate.Template");
            this.tileViewDanhMuc.ItemClick += new DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventHandler(this.TileViewDanhMuc_ItemClick);
            // 
            // colTile_Id
            // 
            this.colTile_Id.FieldName = "Id";
            this.colTile_Id.Name = "colTile_Id";
            // 
            // colTile_MaSanPham
            // 
            this.colTile_MaSanPham.FieldName = "MaSanPham";
            this.colTile_MaSanPham.Name = "colTile_MaSanPham";
            this.colTile_MaSanPham.OptionsColumn.ShowCaption = false;
            this.colTile_MaSanPham.Visible = true;
            this.colTile_MaSanPham.VisibleIndex = 0;
            // 
            // colTile_TenSanPham
            // 
            this.colTile_TenSanPham.FieldName = "TenSanPham";
            this.colTile_TenSanPham.Name = "colTile_TenSanPham";
            this.colTile_TenSanPham.OptionsColumn.ShowCaption = false;
            this.colTile_TenSanPham.Visible = true;
            this.colTile_TenSanPham.VisibleIndex = 1;
            // 
            // colTile_LoaiSanPham
            // 
            this.colTile_LoaiSanPham.FieldName = "LoaiSanPham";
            this.colTile_LoaiSanPham.Name = "colTile_LoaiSanPham";
            this.colTile_LoaiSanPham.OptionsColumn.ShowCaption = false;
            this.colTile_LoaiSanPham.Visible = true;
            this.colTile_LoaiSanPham.VisibleIndex = 2;
            // 
            // colTile_DonGia
            // 
            this.colTile_DonGia.DisplayFormat.FormatString = "n0";
            this.colTile_DonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTile_DonGia.FieldName = "DonGia";
            this.colTile_DonGia.Name = "colTile_DonGia";
            this.colTile_DonGia.OptionsColumn.ShowCaption = false;
            this.colTile_DonGia.Visible = true;
            this.colTile_DonGia.VisibleIndex = 3;
            // 
            // colTile_IdBangGia
            // 
            this.colTile_IdBangGia.FieldName = "IdBangGia";
            this.colTile_IdBangGia.Name = "colTile_IdBangGia";
            // 
            // colTile_LaVatTu
            // 
            this.colTile_LaVatTu.FieldName = "LaVatTu";
            this.colTile_LaVatTu.Name = "colTile_LaVatTu";
            // 
            // tileViewColumn1
            // 
            this.tileViewColumn1.Caption = "tileViewColumn1";
            this.tileViewColumn1.FieldName = "AnhDaiDien ";
            this.tileViewColumn1.Name = "tileViewColumn1";
            this.tileViewColumn1.Visible = true;
            this.tileViewColumn1.VisibleIndex = 4;
            // 
            // pnlTimKiem
            // 
            this.pnlTimKiem.Controls.Add(this.txtBarcode);
            this.pnlTimKiem.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTimKiem.Location = new System.Drawing.Point(0, 575);
            this.pnlTimKiem.Name = "pnlTimKiem";
            this.pnlTimKiem.Padding = new System.Windows.Forms.Padding(10, 8, 10, 8);
            this.pnlTimKiem.Size = new System.Drawing.Size(650, 45);
            this.pnlTimKiem.TabIndex = 2;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtBarcode.Location = new System.Drawing.Point(12, 10);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtBarcode.Properties.Appearance.Options.UseFont = true;
            this.txtBarcode.Properties.AutoHeight = false;
            serializableAppearanceObject1.BackColor = System.Drawing.Color.LightGreen;
            serializableAppearanceObject1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            serializableAppearanceObject1.ForeColor = System.Drawing.Color.Black;
            serializableAppearanceObject1.Options.UseBackColor = true;
            serializableAppearanceObject1.Options.UseFont = true;
            serializableAppearanceObject1.Options.UseForeColor = true;
            this.txtBarcode.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "Bật Cam", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.txtBarcode.Properties.NullValuePrompt = "Quét mã vạch hoặc gõ tên sản phẩm...";
            this.txtBarcode.Size = new System.Drawing.Size(626, 25);
            this.txtBarcode.TabIndex = 0;
            this.txtBarcode.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.TxtBarcode_ButtonClick);
            this.txtBarcode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtBarcode_KeyDown);
            // 
            // tabDanhMuc
            // 
            this.tabDanhMuc.Dock = System.Windows.Forms.DockStyle.Top;
            this.tabDanhMuc.HeaderAutoFill = DevExpress.Utils.DefaultBoolean.True;
            this.tabDanhMuc.Location = new System.Drawing.Point(0, 0);
            this.tabDanhMuc.Name = "tabDanhMuc";
            this.tabDanhMuc.ShowTabHeader = DevExpress.Utils.DefaultBoolean.True;
            this.tabDanhMuc.Size = new System.Drawing.Size(650, 35);
            this.tabDanhMuc.TabIndex = 0;
            this.tabDanhMuc.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabTatCa,
            this.tabVe,
            this.tabCombo,
            this.tabAnUong,
            this.tabHangHoa});
            this.tabDanhMuc.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(this.TabDanhMuc_SelectedPageChanged);
            // 
            // tabTatCa
            // 
            this.tabTatCa.Name = "tabTatCa";
            this.tabTatCa.Size = new System.Drawing.Size(648, 10);
            this.tabTatCa.Text = "Tất cả";
            // 
            // tabVe
            // 
            this.tabVe.Name = "tabVe";
            this.tabVe.Size = new System.Drawing.Size(648, 10);
            this.tabVe.Text = "Vé";
            // 
            // tabCombo
            // 
            this.tabCombo.Name = "tabCombo";
            this.tabCombo.Size = new System.Drawing.Size(648, 10);
            this.tabCombo.Text = "Combo";
            // 
            // tabAnUong
            // 
            this.tabAnUong.Name = "tabAnUong";
            this.tabAnUong.Size = new System.Drawing.Size(648, 10);
            this.tabAnUong.Text = "F&&B";
            // 
            // tabHangHoa
            // 
            this.tabHangHoa.Name = "tabHangHoa";
            this.tabHangHoa.Size = new System.Drawing.Size(648, 10);
            this.tabHangHoa.Text = "Hàng hóa";
            // 
            // gridGioHang
            // 
            this.gridGioHang.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridGioHang.Location = new System.Drawing.Point(0, 100);
            this.gridGioHang.MainView = this.viewGioHang;
            this.gridGioHang.Name = "gridGioHang";
            this.gridGioHang.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repBtnSoLuong,
            this.repBtnXoa,
            this.repDonVi});
            this.gridGioHang.Size = new System.Drawing.Size(540, 320);
            this.gridGioHang.TabIndex = 0;
            this.gridGioHang.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewGioHang});
            // 
            // viewGioHang
            // 
            this.viewGioHang.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGH_IdSanPham,
            this.colGH_MaSanPham,
            this.colGH_TenSanPham,
            this.colGH_TenDonVi,
            this.colGH_DonGia,
            this.colGH_SoLuong,
            this.colGH_ThanhTien,
            this.colGH_Xoa,
            this.colGH_IdBangGia,
            this.colGH_LaVatTu,
            this.colGH_LoaiSanPham,
            this.colGH_GhiChu});
            this.viewGioHang.GridControl = this.gridGioHang;
            this.viewGioHang.Name = "viewGioHang";
            this.viewGioHang.OptionsView.ShowGroupPanel = false;
            this.viewGioHang.OptionsView.ShowIndicator = false;
            this.viewGioHang.CustomRowCellEditForEditing += new DevExpress.XtraGrid.Views.Grid.CustomRowCellEditEventHandler(this.ViewGioHang_CustomRowCellEditForEditing);
            this.viewGioHang.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.ViewGioHang_CellValueChanged);
            // 
            // colGH_IdSanPham
            // 
            this.colGH_IdSanPham.FieldName = "IdSanPham";
            this.colGH_IdSanPham.Name = "colGH_IdSanPham";
            // 
            // colGH_MaSanPham
            // 
            this.colGH_MaSanPham.FieldName = "MaSanPham";
            this.colGH_MaSanPham.Name = "colGH_MaSanPham";
            // 
            // colGH_TenSanPham
            // 
            this.colGH_TenSanPham.Caption = "Sản phẩm";
            this.colGH_TenSanPham.FieldName = "TenSanPham";
            this.colGH_TenSanPham.Name = "colGH_TenSanPham";
            this.colGH_TenSanPham.OptionsColumn.AllowEdit = false;
            this.colGH_TenSanPham.Visible = true;
            this.colGH_TenSanPham.VisibleIndex = 0;
            this.colGH_TenSanPham.Width = 180;
            // 
            // colGH_TenDonVi
            // 
            this.colGH_TenDonVi.Caption = "Đơn vị";
            this.colGH_TenDonVi.FieldName = "TenDonVi";
            this.colGH_TenDonVi.Name = "colGH_TenDonVi";
            this.colGH_TenDonVi.Visible = true;
            this.colGH_TenDonVi.VisibleIndex = 1;
            this.colGH_TenDonVi.Width = 60;
            // 
            // colGH_DonGia
            // 
            this.colGH_DonGia.Caption = "Đơn giá";
            this.colGH_DonGia.DisplayFormat.FormatString = "#,##0";
            this.colGH_DonGia.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGH_DonGia.FieldName = "DonGia";
            this.colGH_DonGia.Name = "colGH_DonGia";
            this.colGH_DonGia.OptionsColumn.AllowEdit = false;
            this.colGH_DonGia.Visible = true;
            this.colGH_DonGia.VisibleIndex = 2;
            this.colGH_DonGia.Width = 80;
            // 
            // colGH_SoLuong
            // 
            this.colGH_SoLuong.Caption = "SL";
            this.colGH_SoLuong.ColumnEdit = this.repBtnSoLuong;
            this.colGH_SoLuong.FieldName = "SoLuong";
            this.colGH_SoLuong.Name = "colGH_SoLuong";
            this.colGH_SoLuong.Visible = true;
            this.colGH_SoLuong.VisibleIndex = 3;
            this.colGH_SoLuong.Width = 90;
            // 
            // repBtnSoLuong
            // 
            this.repBtnSoLuong.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Minus),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)});
            this.repBtnSoLuong.Name = "repBtnSoLuong";
            this.repBtnSoLuong.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.BtnEditSL_ButtonClick);
            // 
            // colGH_ThanhTien
            // 
            this.colGH_ThanhTien.Caption = "Thành tiền";
            this.colGH_ThanhTien.DisplayFormat.FormatString = "#,##0";
            this.colGH_ThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGH_ThanhTien.FieldName = "ThanhTien";
            this.colGH_ThanhTien.Name = "colGH_ThanhTien";
            this.colGH_ThanhTien.OptionsColumn.AllowEdit = false;
            this.colGH_ThanhTien.Visible = true;
            this.colGH_ThanhTien.VisibleIndex = 4;
            this.colGH_ThanhTien.Width = 100;
            // 
            // colGH_Xoa
            // 
            this.colGH_Xoa.Caption = " ";
            this.colGH_Xoa.ColumnEdit = this.repBtnXoa;
            this.colGH_Xoa.Name = "colGH_Xoa";
            this.colGH_Xoa.OptionsColumn.FixedWidth = true;
            this.colGH_Xoa.ToolTip = "Xóa món này";
            this.colGH_Xoa.Visible = true;
            this.colGH_Xoa.VisibleIndex = 5;
            this.colGH_Xoa.Width = 35;
            // 
            // repBtnXoa
            // 
            this.repBtnXoa.AutoHeight = false;
            this.repBtnXoa.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Delete)});
            this.repBtnXoa.Name = "repBtnXoa";
            this.repBtnXoa.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repBtnXoa.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.RepBtnXoa_ButtonClick);
            // 
            // colGH_IdBangGia
            // 
            this.colGH_IdBangGia.FieldName = "IdBangGia";
            this.colGH_IdBangGia.Name = "colGH_IdBangGia";
            // 
            // colGH_LaVatTu
            // 
            this.colGH_LaVatTu.FieldName = "LaVatTu";
            this.colGH_LaVatTu.Name = "colGH_LaVatTu";
            // 
            // colGH_LoaiSanPham
            // 
            this.colGH_LoaiSanPham.FieldName = "LoaiSanPham";
            this.colGH_LoaiSanPham.Name = "colGH_LoaiSanPham";
            // 
            // colGH_GhiChu
            // 
            this.colGH_GhiChu.FieldName = "GhiChu";
            this.colGH_GhiChu.Name = "colGH_GhiChu";
            // 
            // repDonVi
            // 
            this.repDonVi.AutoHeight = false;
            this.repDonVi.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repDonVi.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("TenDonVi", "Tên ĐV"),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo("GiaBan", "Đơn giá", 60, DevExpress.Utils.FormatType.Numeric, "n0", true, DevExpress.Utils.HorzAlignment.Far, DevExpress.Data.ColumnSortOrder.None, DevExpress.Utils.DefaultBoolean.Default)});
            this.repDonVi.DisplayMember = "TenDonVi";
            this.repDonVi.Name = "repDonVi";
            this.repDonVi.NullText = "";
            this.repDonVi.ShowFooter = false;
            this.repDonVi.ShowHeader = false;
            this.repDonVi.ValueMember = "TenDonVi";
            // 
            // pnlKhachHang
            // 
            this.pnlKhachHang.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlKhachHang.Appearance.Options.UseBackColor = true;
            this.pnlKhachHang.Controls.Add(this.lblDiemKhach);
            this.pnlKhachHang.Controls.Add(this.lblHangKhach);
            this.pnlKhachHang.Controls.Add(this.lblViKhach);
            this.pnlKhachHang.Controls.Add(this.lblTenKhach);
            this.pnlKhachHang.Controls.Add(this.btnBoChonKhach);
            this.pnlKhachHang.Controls.Add(this.btnTimKhach);
            this.pnlKhachHang.Controls.Add(this.txtTimKhach);
            this.pnlKhachHang.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlKhachHang.Location = new System.Drawing.Point(0, 0);
            this.pnlKhachHang.Name = "pnlKhachHang";
            this.pnlKhachHang.Padding = new System.Windows.Forms.Padding(8, 5, 8, 5);
            this.pnlKhachHang.Size = new System.Drawing.Size(540, 100);
            this.pnlKhachHang.TabIndex = 10;
            this.pnlKhachHang.Text = "KHÁCH HÀNG";
            // 
            // lblDiemKhach
            // 
            this.lblDiemKhach.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblDiemKhach.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(60)))));
            this.lblDiemKhach.Appearance.Options.UseFont = true;
            this.lblDiemKhach.Appearance.Options.UseForeColor = true;
            this.lblDiemKhach.Location = new System.Drawing.Point(220, 70);
            this.lblDiemKhach.Name = "lblDiemKhach";
            this.lblDiemKhach.Size = new System.Drawing.Size(0, 15);
            this.lblDiemKhach.TabIndex = 5;
            // 
            // lblHangKhach
            // 
            this.lblHangKhach.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHangKhach.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(165)))), ((int)(((byte)(0)))));
            this.lblHangKhach.Appearance.Options.UseFont = true;
            this.lblHangKhach.Appearance.Options.UseForeColor = true;
            this.lblHangKhach.Location = new System.Drawing.Point(120, 70);
            this.lblHangKhach.Name = "lblHangKhach";
            this.lblHangKhach.Size = new System.Drawing.Size(0, 15);
            this.lblHangKhach.TabIndex = 4;
            // 
            // lblViKhach
            // 
            this.lblViKhach.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblViKhach.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(144)))), ((int)(((byte)(255)))));
            this.lblViKhach.Appearance.Options.UseFont = true;
            this.lblViKhach.Appearance.Options.UseForeColor = true;
            this.lblViKhach.Location = new System.Drawing.Point(340, 70);
            this.lblViKhach.Name = "lblViKhach";
            this.lblViKhach.Size = new System.Drawing.Size(0, 15);
            this.lblViKhach.TabIndex = 6;
            // 
            // lblTenKhach
            // 
            this.lblTenKhach.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTenKhach.Appearance.Options.UseFont = true;
            this.lblTenKhach.Location = new System.Drawing.Point(10, 68);
            this.lblTenKhach.Name = "lblTenKhach";
            this.lblTenKhach.Size = new System.Drawing.Size(0, 17);
            this.lblTenKhach.TabIndex = 3;
            // 
            // btnBoChonKhach
            // 
            this.btnBoChonKhach.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.btnBoChonKhach.Appearance.Options.UseForeColor = true;
            this.btnBoChonKhach.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnBoChonKhach.ImageOptions.SvgImage")));
            this.btnBoChonKhach.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnBoChonKhach.Location = new System.Drawing.Point(365, 30);
            this.btnBoChonKhach.Name = "btnBoChonKhach";
            this.btnBoChonKhach.Size = new System.Drawing.Size(74, 32);
            this.btnBoChonKhach.TabIndex = 2;
            this.btnBoChonKhach.Visible = false;
            this.btnBoChonKhach.Click += new System.EventHandler(this.BtnBoChonKhach_Click);
            // 
            // btnTimKhach
            // 
            this.btnTimKhach.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnTimKhach.ImageOptions.SvgImage")));
            this.btnTimKhach.ImageOptions.SvgImageSize = new System.Drawing.Size(20, 20);
            this.btnTimKhach.Location = new System.Drawing.Point(281, 30);
            this.btnTimKhach.Name = "btnTimKhach";
            this.btnTimKhach.Size = new System.Drawing.Size(78, 32);
            this.btnTimKhach.TabIndex = 1;
            this.btnTimKhach.Click += new System.EventHandler(this.BtnTimKhach_Click);
            // 
            // txtTimKhach
            // 
            this.txtTimKhach.Location = new System.Drawing.Point(10, 30);
            this.txtTimKhach.Name = "txtTimKhach";
            this.txtTimKhach.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtTimKhach.Properties.Appearance.Options.UseFont = true;
            this.txtTimKhach.Properties.NullValuePrompt = "SĐT / Mã KH / Quét RFID...";
            this.txtTimKhach.Size = new System.Drawing.Size(263, 24);
            this.txtTimKhach.TabIndex = 0;
            this.txtTimKhach.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TxtTimKhach_KeyDown);
            // 
            // pnlTong
            // 
            this.pnlTong.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(248)))), ((int)(((byte)(255)))));
            this.pnlTong.Appearance.Options.UseBackColor = true;
            this.pnlTong.Controls.Add(this.lblTongValue);
            this.pnlTong.Controls.Add(this.lblTong);
            this.pnlTong.Controls.Add(this.lblGiamGiaValue);
            this.pnlTong.Controls.Add(this.lblGiamGia);
            this.pnlTong.Controls.Add(this.lblMoTaGiamGia);
            this.pnlTong.Controls.Add(this.btnXoaKM);
            this.pnlTong.Controls.Add(this.lblVATValue);
            this.pnlTong.Controls.Add(this.lblVAT);
            this.pnlTong.Controls.Add(this.lblTienHangValue);
            this.pnlTong.Controls.Add(this.lblTienHang);
            this.pnlTong.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlTong.Location = new System.Drawing.Point(0, 420);
            this.pnlTong.Name = "pnlTong";
            this.pnlTong.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlTong.Size = new System.Drawing.Size(540, 130);
            this.pnlTong.TabIndex = 1;
            // 
            // lblTongValue
            // 
            this.lblTongValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTongValue.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTongValue.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(60)))));
            this.lblTongValue.Appearance.Options.UseFont = true;
            this.lblTongValue.Appearance.Options.UseForeColor = true;
            this.lblTongValue.Appearance.Options.UseTextOptions = true;
            this.lblTongValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblTongValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTongValue.Location = new System.Drawing.Point(120, 90);
            this.lblTongValue.Name = "lblTongValue";
            this.lblTongValue.Size = new System.Drawing.Size(400, 30);
            this.lblTongValue.TabIndex = 7;
            this.lblTongValue.Text = "0 ₫";
            // 
            // lblTong
            // 
            this.lblTong.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTong.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(60)))));
            this.lblTong.Appearance.Options.UseFont = true;
            this.lblTong.Appearance.Options.UseForeColor = true;
            this.lblTong.Location = new System.Drawing.Point(15, 90);
            this.lblTong.Name = "lblTong";
            this.lblTong.Size = new System.Drawing.Size(69, 30);
            this.lblTong.TabIndex = 6;
            this.lblTong.Text = "TỔNG:";
            // 
            // lblGiamGiaValue
            // 
            this.lblGiamGiaValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblGiamGiaValue.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblGiamGiaValue.Appearance.Options.UseFont = true;
            this.lblGiamGiaValue.Appearance.Options.UseTextOptions = true;
            this.lblGiamGiaValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblGiamGiaValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblGiamGiaValue.Location = new System.Drawing.Point(120, 61);
            this.lblGiamGiaValue.Name = "lblGiamGiaValue";
            this.lblGiamGiaValue.Size = new System.Drawing.Size(400, 20);
            this.lblGiamGiaValue.TabIndex = 5;
            this.lblGiamGiaValue.Text = "0";
            // 
            // lblGiamGia
            // 
            this.lblGiamGia.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblGiamGia.Appearance.Options.UseFont = true;
            this.lblGiamGia.Location = new System.Drawing.Point(15, 61);
            this.lblGiamGia.Name = "lblGiamGia";
            this.lblGiamGia.Size = new System.Drawing.Size(57, 19);
            this.lblGiamGia.TabIndex = 4;
            this.lblGiamGia.Text = "Giảm giá:";
            // 
            // lblMoTaGiamGia
            // 
            this.lblMoTaGiamGia.Appearance.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblMoTaGiamGia.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(50)))));
            this.lblMoTaGiamGia.Appearance.Options.UseFont = true;
            this.lblMoTaGiamGia.Appearance.Options.UseForeColor = true;
            this.lblMoTaGiamGia.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMoTaGiamGia.Location = new System.Drawing.Point(15, 83);
            this.lblMoTaGiamGia.Name = "lblMoTaGiamGia";
            this.lblMoTaGiamGia.Size = new System.Drawing.Size(400, 18);
            this.lblMoTaGiamGia.TabIndex = 10;
            this.lblMoTaGiamGia.Visible = false;
            // 
            // btnXoaKM
            // 
            this.btnXoaKM.Appearance.Font = new System.Drawing.Font("Segoe UI", 8F);
            this.btnXoaKM.Appearance.Options.UseFont = true;
            this.btnXoaKM.ImageOptions.ImageUri.Uri = "Cancel";
            this.btnXoaKM.Location = new System.Drawing.Point(420, 80);
            this.btnXoaKM.Name = "btnXoaKM";
            this.btnXoaKM.Size = new System.Drawing.Size(90, 22);
            this.btnXoaKM.TabIndex = 11;
            this.btnXoaKM.Text = "Xóa KM";
            this.btnXoaKM.Visible = false;
            this.btnXoaKM.Click += new System.EventHandler(this.BtnXoaKM_Click);
            // 
            // lblVATValue
            // 
            this.lblVATValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblVATValue.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblVATValue.Appearance.Options.UseFont = true;
            this.lblVATValue.Appearance.Options.UseTextOptions = true;
            this.lblVATValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblVATValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblVATValue.Location = new System.Drawing.Point(120, 38);
            this.lblVATValue.Name = "lblVATValue";
            this.lblVATValue.Size = new System.Drawing.Size(400, 20);
            this.lblVATValue.TabIndex = 3;
            this.lblVATValue.Text = "0";
            // 
            // lblVAT
            // 
            this.lblVAT.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblVAT.Appearance.Options.UseFont = true;
            this.lblVAT.Location = new System.Drawing.Point(15, 38);
            this.lblVAT.Name = "lblVAT";
            this.lblVAT.Size = new System.Drawing.Size(60, 19);
            this.lblVAT.TabIndex = 2;
            this.lblVAT.Text = "Thuế VAT:";
            // 
            // lblTienHangValue
            // 
            this.lblTienHangValue.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblTienHangValue.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTienHangValue.Appearance.Options.UseFont = true;
            this.lblTienHangValue.Appearance.Options.UseTextOptions = true;
            this.lblTienHangValue.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblTienHangValue.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTienHangValue.Location = new System.Drawing.Point(120, 15);
            this.lblTienHangValue.Name = "lblTienHangValue";
            this.lblTienHangValue.Size = new System.Drawing.Size(400, 20);
            this.lblTienHangValue.TabIndex = 1;
            this.lblTienHangValue.Text = "0";
            // 
            // lblTienHang
            // 
            this.lblTienHang.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTienHang.Appearance.Options.UseFont = true;
            this.lblTienHang.Location = new System.Drawing.Point(15, 15);
            this.lblTienHang.Name = "lblTienHang";
            this.lblTienHang.Size = new System.Drawing.Size(63, 19);
            this.lblTienHang.TabIndex = 0;
            this.lblTienHang.Text = "Tiền hàng:";
            // 
            // pnlHanhDong
            // 
            this.pnlHanhDong.Appearance.BackColor = System.Drawing.Color.RoyalBlue;
            this.pnlHanhDong.Appearance.ForeColor = System.Drawing.Color.RoyalBlue;
            this.pnlHanhDong.Appearance.Options.UseBackColor = true;
            this.pnlHanhDong.Appearance.Options.UseForeColor = true;
            this.pnlHanhDong.Controls.Add(this.btnDongPhien);
            this.pnlHanhDong.Controls.Add(this.labelControl1);
            this.pnlHanhDong.Controls.Add(this.btnThanhToan);
            this.pnlHanhDong.Controls.Add(this.btnHoanTra);
            this.pnlHanhDong.Controls.Add(this.lblSpacer);
            this.pnlHanhDong.Controls.Add(this.btnXoaGio);
            this.pnlHanhDong.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlHanhDong.Location = new System.Drawing.Point(0, 550);
            this.pnlHanhDong.Name = "pnlHanhDong";
            this.pnlHanhDong.Padding = new System.Windows.Forms.Padding(10);
            this.pnlHanhDong.Size = new System.Drawing.Size(540, 70);
            this.pnlHanhDong.TabIndex = 2;
            // 
            // btnDongPhien
            // 
            this.btnDongPhien.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnDongPhien.Location = new System.Drawing.Point(251, 12);
            this.btnDongPhien.Name = "btnDongPhien";
            this.btnDongPhien.Size = new System.Drawing.Size(121, 46);
            this.btnDongPhien.TabIndex = 5;
            this.btnDongPhien.Text = "Đóng phiên (F8)";
            this.btnDongPhien.Click += new System.EventHandler(this.btnDongPhien_Click_1);
            // 
            // labelControl1
            // 
            this.labelControl1.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.labelControl1.Dock = System.Windows.Forms.DockStyle.Left;
            this.labelControl1.Location = new System.Drawing.Point(237, 12);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(14, 46);
            this.labelControl1.TabIndex = 4;
            // 
            // btnThanhToan
            // 
            this.btnThanhToan.Appearance.BackColor = System.Drawing.Color.White;
            this.btnThanhToan.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(80)))));
            this.btnThanhToan.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.btnThanhToan.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(150)))), ((int)(((byte)(80)))));
            this.btnThanhToan.Appearance.Options.UseBackColor = true;
            this.btnThanhToan.Appearance.Options.UseBorderColor = true;
            this.btnThanhToan.Appearance.Options.UseFont = true;
            this.btnThanhToan.Appearance.Options.UseForeColor = true;
            this.btnThanhToan.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnThanhToan.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("btnThanhToan.ImageOptions.SvgImage")));
            this.btnThanhToan.Location = new System.Drawing.Point(356, 12);
            this.btnThanhToan.Name = "btnThanhToan";
            this.btnThanhToan.Size = new System.Drawing.Size(172, 46);
            this.btnThanhToan.TabIndex = 0;
            this.btnThanhToan.Text = "THANH TOÁN (F5)";
            this.btnThanhToan.Click += new System.EventHandler(this.BtnThanhToan_Click);
            // 
            // btnHoanTra
            // 
            this.btnHoanTra.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnHoanTra.Location = new System.Drawing.Point(116, 12);
            this.btnHoanTra.Name = "btnHoanTra";
            this.btnHoanTra.Size = new System.Drawing.Size(121, 46);
            this.btnHoanTra.TabIndex = 3;
            this.btnHoanTra.Text = "Hoàn trả (F9)";
            this.btnHoanTra.Click += new System.EventHandler(this.BtnHoanTra_Click);
            // 
            // lblSpacer
            // 
            this.lblSpacer.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblSpacer.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblSpacer.Location = new System.Drawing.Point(102, 12);
            this.lblSpacer.Name = "lblSpacer";
            this.lblSpacer.Size = new System.Drawing.Size(14, 46);
            this.lblSpacer.TabIndex = 3;
            // 
            // btnXoaGio
            // 
            this.btnXoaGio.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.btnXoaGio.Appearance.Options.UseForeColor = true;
            this.btnXoaGio.Dock = System.Windows.Forms.DockStyle.Left;
            this.btnXoaGio.Location = new System.Drawing.Point(12, 12);
            this.btnXoaGio.Name = "btnXoaGio";
            this.btnXoaGio.Size = new System.Drawing.Size(90, 46);
            this.btnXoaGio.TabIndex = 1;
            this.btnXoaGio.Text = "Xóa giỏ";
            this.btnXoaGio.Click += new System.EventHandler(this.BtnXoaGio_Click);
            // 
            // pnlStatus
            // 
            this.pnlStatus.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(240)))), ((int)(((byte)(240)))), ((int)(((byte)(240)))));
            this.pnlStatus.Appearance.Options.UseBackColor = true;
            this.pnlStatus.Controls.Add(this.lblStatus);
            this.pnlStatus.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlStatus.Location = new System.Drawing.Point(0, 670);
            this.pnlStatus.Name = "pnlStatus";
            this.pnlStatus.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.pnlStatus.Size = new System.Drawing.Size(1200, 30);
            this.pnlStatus.TabIndex = 2;
            // 
            // lblStatus
            // 
            this.lblStatus.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblStatus.Appearance.Options.UseFont = true;
            this.lblStatus.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblStatus.Location = new System.Drawing.Point(12, 2);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(133, 15);
            this.lblStatus.TabIndex = 0;
            this.lblStatus.Text = "Chưa mở phiên thu ngân";
            // 
            // ucPOS
            // 
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.pnlBanner);
            this.Controls.Add(this.pnlStatus);
            this.Name = "ucPOS";
            this.Size = new System.Drawing.Size(1200, 700);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBanner)).EndInit();
            this.pnlBanner.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).EndInit();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picCamera)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridDanhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileViewDanhMuc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTimKiem)).EndInit();
            this.pnlTimKiem.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtBarcode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tabDanhMuc)).EndInit();
            this.tabDanhMuc.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridGioHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewGioHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repBtnSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repBtnXoa)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repDonVi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlKhachHang)).EndInit();
            this.pnlKhachHang.ResumeLayout(false);
            this.pnlKhachHang.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKhach.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTong)).EndInit();
            this.pnlTong.ResumeLayout(false);
            this.pnlTong.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHanhDong)).EndInit();
            this.pnlHanhDong.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlStatus)).EndInit();
            this.pnlStatus.ResumeLayout(false);
            this.pnlStatus.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl pnlBanner;
        private DevExpress.XtraEditors.LabelControl lblCa;
        private DevExpress.XtraEditors.LabelControl lblThuNgan;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.SplitContainerControl splitMain;
        private DevExpress.XtraGrid.GridControl gridDanhMuc;
        private DevExpress.XtraGrid.Views.Tile.TileView tileViewDanhMuc;
        private DevExpress.XtraGrid.Columns.GridColumn colTile_Id;
        private DevExpress.XtraGrid.Columns.GridColumn colTile_MaSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colTile_TenSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colTile_LoaiSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colTile_DonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colTile_IdBangGia;
        private DevExpress.XtraGrid.Columns.GridColumn colTile_LaVatTu;
        private DevExpress.XtraGrid.Columns.TileViewColumn tileViewColumn1;
        private DevExpress.XtraEditors.PanelControl pnlTimKiem;
        private DevExpress.XtraEditors.ButtonEdit txtBarcode;
        private DevExpress.XtraTab.XtraTabControl tabDanhMuc;
        private DevExpress.XtraTab.XtraTabPage tabTatCa;
        private DevExpress.XtraTab.XtraTabPage tabVe;
        private DevExpress.XtraTab.XtraTabPage tabCombo;
        private DevExpress.XtraTab.XtraTabPage tabAnUong;
        private DevExpress.XtraTab.XtraTabPage tabHangHoa;
        private DevExpress.XtraGrid.GridControl gridGioHang;
        private DevExpress.XtraGrid.Views.Grid.GridView viewGioHang;
        private DevExpress.XtraGrid.Columns.GridColumn colGH_IdSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colGH_MaSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colGH_TenSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colGH_TenDonVi;
        private DevExpress.XtraGrid.Columns.GridColumn colGH_DonGia;
        private DevExpress.XtraGrid.Columns.GridColumn colGH_SoLuong;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repBtnSoLuong;
        private DevExpress.XtraGrid.Columns.GridColumn colGH_ThanhTien;
        private DevExpress.XtraGrid.Columns.GridColumn colGH_Xoa;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repBtnXoa;
        private DevExpress.XtraGrid.Columns.GridColumn colGH_IdBangGia;
        private DevExpress.XtraGrid.Columns.GridColumn colGH_LaVatTu;
        private DevExpress.XtraGrid.Columns.GridColumn colGH_LoaiSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colGH_GhiChu;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repDonVi;
        private DevExpress.XtraEditors.GroupControl pnlKhachHang;
        private DevExpress.XtraEditors.LabelControl lblDiemKhach;
        private DevExpress.XtraEditors.LabelControl lblHangKhach;
        private DevExpress.XtraEditors.LabelControl lblViKhach;
        private DevExpress.XtraEditors.LabelControl lblTenKhach;
        private DevExpress.XtraEditors.SimpleButton btnBoChonKhach;
        private DevExpress.XtraEditors.SimpleButton btnTimKhach;
        private DevExpress.XtraEditors.TextEdit txtTimKhach;
        private DevExpress.XtraEditors.PanelControl pnlTong;
        private DevExpress.XtraEditors.LabelControl lblTongValue;
        private DevExpress.XtraEditors.LabelControl lblTong;
        private DevExpress.XtraEditors.LabelControl lblGiamGiaValue;
        private DevExpress.XtraEditors.LabelControl lblGiamGia;
        private DevExpress.XtraEditors.LabelControl lblMoTaGiamGia;
        private DevExpress.XtraEditors.SimpleButton btnXoaKM;
        private DevExpress.XtraEditors.LabelControl lblVATValue;
        private DevExpress.XtraEditors.LabelControl lblVAT;
        private DevExpress.XtraEditors.LabelControl lblTienHangValue;
        private DevExpress.XtraEditors.LabelControl lblTienHang;
        private DevExpress.XtraEditors.PanelControl pnlHanhDong;
        private DevExpress.XtraEditors.SimpleButton btnDongPhien;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraEditors.SimpleButton btnThanhToan;
        private DevExpress.XtraEditors.SimpleButton btnHoanTra;
        private DevExpress.XtraEditors.LabelControl lblSpacer;
        private DevExpress.XtraEditors.SimpleButton btnXoaGio;
        private DevExpress.XtraEditors.PanelControl pnlStatus;
        private DevExpress.XtraEditors.LabelControl lblStatus;
        private System.Windows.Forms.PictureBox picCamera;
        private System.ComponentModel.IContainer components = null;






    }}