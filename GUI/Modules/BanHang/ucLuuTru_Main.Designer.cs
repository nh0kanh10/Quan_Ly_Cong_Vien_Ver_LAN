using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Tile;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Columns;
using System.ComponentModel;
using System.Drawing;

namespace GUI.Modules.BanHang
{
    partial class ucLuuTru_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucLuuTru_Main));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btnCheckIn = new DevExpress.XtraBars.BarButtonItem();
            this.btnHuyDat = new DevExpress.XtraBars.BarButtonItem();
            this.btnMinibar = new DevExpress.XtraBars.BarButtonItem();
            this.btnDoiPhong = new DevExpress.XtraBars.BarButtonItem();
            this.btnGiaHan = new DevExpress.XtraBars.BarButtonItem();
            this.btnDonXong = new DevExpress.XtraBars.BarButtonItem();
            this.btnBaoTri = new DevExpress.XtraBars.BarButtonItem();
            this.btnSuaXong = new DevExpress.XtraBars.BarButtonItem();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.gridControlPhong = new DevExpress.XtraGrid.GridControl();
            this.tileViewPhong = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.colIdPhong = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colMaPhong = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colTenLoaiPhong = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colTrangThai = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colTenKhachHang = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colThoiGianO = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colIdChiTietDatPhong = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colIsLateCheckOut = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.layoutControlRight = new DevExpress.XtraLayout.LayoutControl();
            this.lblRoomTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblStatusBadge = new DevExpress.XtraEditors.LabelControl();
            this.lblHoTen = new DevExpress.XtraEditors.LabelControl();
            this.lblSDT = new DevExpress.XtraEditors.LabelControl();
            this.lblCheckIn = new DevExpress.XtraEditors.LabelControl();
            this.lblCheckOut = new DevExpress.XtraEditors.LabelControl();
            this.lblTienPhong = new DevExpress.XtraEditors.LabelControl();
            this.lblPhuThu = new DevExpress.XtraEditors.LabelControl();
            this.lblDaCoc = new DevExpress.XtraEditors.LabelControl();
            this.lblTongTien = new DevExpress.XtraEditors.LabelControl();
            this.btnActionCheckOut = new DevExpress.XtraEditors.SimpleButton();
            this.btnActionMinibar = new DevExpress.XtraEditors.SimpleButton();
            this.btnActionDoiPhong = new DevExpress.XtraEditors.SimpleButton();
            this.btnActionGiaHan = new DevExpress.XtraEditors.SimpleButton();
            this.btnActionThemDichVu = new DevExpress.XtraEditors.SimpleButton();
            this.btnActionOpenMinibar = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroupRight = new DevExpress.XtraLayout.LayoutControlGroup();
            this.groupInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciRoomTitle = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciStatusBadge = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupKhachHang = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciHoTen = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciSDT = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupThoiGian = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciCheckIn = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCheckOut = new DevExpress.XtraLayout.LayoutControlItem();
            this.groupTaiChinh = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciTienPhong = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciPhuThu = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciDaCoc = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciTongTien = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceRight = new DevExpress.XtraLayout.EmptySpaceItem();
            this.groupThaoTac = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciBtnCheckOut = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnMinibar = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnDoiPhong = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnGiaHan = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnThemDichVu = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnOpenMinibar = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnLamMoi = new DevExpress.XtraEditors.SimpleButton();
            this.btnChuong = new DevExpress.XtraEditors.SimpleButton();
            this.timerKiemTraDat = new System.Windows.Forms.Timer();
            this.panelTop = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.Panel2.SuspendLayout();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileViewPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlRight)).BeginInit();
            this.layoutControlRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRoomTitle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciStatusBadge)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupKhachHang)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciHoTen)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSDT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupThoiGian)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCheckIn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCheckOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupTaiChinh)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTienPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPhuThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDaCoc)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTongTien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceRight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupThaoTac)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnCheckOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnMinibar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnDoiPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnGiaHan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnThemDichVu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnOpenMinibar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).BeginInit();
            this.panelTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.btnCheckIn,
            this.btnHuyDat,
            this.btnMinibar,
            this.btnDoiPhong,
            this.btnGiaHan,
            this.btnDonXong,
            this.btnBaoTri,
            this.btnSuaXong});
            this.barManager1.MaxItemId = 8;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(1200, 0);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 700);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(1200, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 0);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 700);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1200, 0);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 700);
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.Caption = "Check-in (Nhận phòng)";
            this.btnCheckIn.Id = 0;
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnCheckInDatTruoc_ItemClick);
            // 
            // btnHuyDat
            // 
            this.btnHuyDat.Caption = "Hủy phiếu đặt trước";
            this.btnHuyDat.Id = 1;
            this.btnHuyDat.Name = "btnHuyDat";
            this.btnHuyDat.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnHuyDat_ItemClick);
            // 
            // btnMinibar
            // 
            this.btnMinibar.Caption = "Minibar";
            this.btnMinibar.Id = 2;
            this.btnMinibar.Name = "btnMinibar";
            this.btnMinibar.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnMinibar_ItemClick);
            // 
            // btnDoiPhong
            // 
            this.btnDoiPhong.Caption = "Đổi phòng";
            this.btnDoiPhong.Id = 3;
            this.btnDoiPhong.Name = "btnDoiPhong";
            this.btnDoiPhong.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnActionDoiPhong_Click);
            // 
            // btnGiaHan
            // 
            this.btnGiaHan.Caption = "Gia hạn phòng";
            this.btnGiaHan.Id = 4;
            this.btnGiaHan.Name = "btnGiaHan";
            this.btnGiaHan.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnActionGiaHan_Click);
            // 
            // btnDonXong
            // 
            this.btnDonXong.Caption = "Đã dọn dẹp xong";
            this.btnDonXong.Id = 5;
            this.btnDonXong.Name = "btnDonXong";
            this.btnDonXong.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnDonXong_ItemClick);
            // 
            // btnBaoTri
            // 
            this.btnBaoTri.Caption = "Đưa vào bảo trì";
            this.btnBaoTri.Id = 6;
            this.btnBaoTri.Name = "btnBaoTri";
            this.btnBaoTri.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnBaoTri_ItemClick);
            // 
            // btnSuaXong
            // 
            this.btnSuaXong.Caption = "Hoàn thành bảo trì";
            this.btnSuaXong.Id = 7;
            this.btnSuaXong.Name = "btnSuaXong";
            this.btnSuaXong.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnSuaXong_ItemClick);
            // 
            // popupMenu1
            // 
            this.popupMenu1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.btnCheckIn),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnHuyDat),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnMinibar),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDoiPhong),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnGiaHan),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnDonXong),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnBaoTri),
            new DevExpress.XtraBars.LinkPersistInfo(this.btnSuaXong)});
            this.popupMenu1.Manager = this.barManager1;
            this.popupMenu1.Name = "popupMenu1";
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.FixedPanel = DevExpress.XtraEditors.SplitFixedPanel.Panel2;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 48);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.gridControlPhong);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Controls.Add(this.layoutControlRight);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1200, 652);
            this.splitContainerControl1.SplitterPosition = 320;
            this.splitContainerControl1.TabIndex = 2;
            // 
            // gridControlPhong
            // 
            this.gridControlPhong.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControlPhong.Location = new System.Drawing.Point(0, 0);
            this.gridControlPhong.MainView = this.tileViewPhong;
            this.gridControlPhong.Name = "gridControlPhong";
            this.gridControlPhong.Size = new System.Drawing.Size(870, 652);
            this.gridControlPhong.TabIndex = 1;
            this.gridControlPhong.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tileViewPhong});
            // 
            // tileViewPhong
            // 
            this.tileViewPhong.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIdPhong,
            this.colMaPhong,
            this.colTenLoaiPhong,
            this.colTrangThai,
            this.colTenKhachHang,
            this.colThoiGianO,
            this.colIdChiTietDatPhong,
            this.colIsLateCheckOut});
            this.tileViewPhong.GridControl = this.gridControlPhong;
            this.tileViewPhong.Name = "tileViewPhong";
            this.tileViewPhong.OptionsTiles.IndentBetweenItems = 12;
            this.tileViewPhong.OptionsTiles.ItemSize = new System.Drawing.Size(220, 140);
            this.tileViewPhong.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tileViewPhong.OptionsTiles.RowCount = 0;
            this.tileViewPhong.TileHtmlTemplate.Styles = resources.GetString("tileViewPhong.TileHtmlTemplate.Styles");
            this.tileViewPhong.TileHtmlTemplate.Template = resources.GetString("tileViewPhong.TileHtmlTemplate.Template");
            this.tileViewPhong.ItemDoubleClick += new DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventHandler(this.TileViewPhong_ItemDoubleClick);
            this.tileViewPhong.ItemRightClick += new DevExpress.XtraGrid.Views.Tile.TileViewItemClickEventHandler(this.TileViewPhong_ItemRightClick);
            this.tileViewPhong.FocusedRowChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventHandler(this.TileViewPhong_FocusedRowChanged);
            // 
            // colIdPhong
            // 
            this.colIdPhong.FieldName = "IdPhong";
            this.colIdPhong.Name = "colIdPhong";
            // 
            // colMaPhong
            // 
            this.colMaPhong.FieldName = "MaPhong";
            this.colMaPhong.Name = "colMaPhong";
            this.colMaPhong.Visible = true;
            this.colMaPhong.VisibleIndex = 0;
            // 
            // colTenLoaiPhong
            // 
            this.colTenLoaiPhong.FieldName = "TenLoaiPhong";
            this.colTenLoaiPhong.Name = "colTenLoaiPhong";
            this.colTenLoaiPhong.Visible = true;
            this.colTenLoaiPhong.VisibleIndex = 1;
            // 
            // colTrangThai
            // 
            this.colTrangThai.FieldName = "TrangThaiPhong";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.Visible = true;
            this.colTrangThai.VisibleIndex = 2;
            // 
            // colTenKhachHang
            // 
            this.colTenKhachHang.FieldName = "TenKhachHang";
            this.colTenKhachHang.Name = "colTenKhachHang";
            this.colTenKhachHang.Visible = true;
            this.colTenKhachHang.VisibleIndex = 3;
            // 
            // colThoiGianO
            // 
            this.colThoiGianO.FieldName = "ThoiGianO";
            this.colThoiGianO.Name = "colThoiGianO";
            this.colThoiGianO.Visible = true;
            this.colThoiGianO.VisibleIndex = 4;
            // 
            // colIdChiTietDatPhong
            // 
            this.colIdChiTietDatPhong.FieldName = "IdChiTietDatPhong";
            this.colIdChiTietDatPhong.Name = "colIdChiTietDatPhong";
            // 
            // colIsLateCheckOut
            // 
            this.colIsLateCheckOut.FieldName = "IsLateCheckOut";
            this.colIsLateCheckOut.Name = "colIsLateCheckOut";
            // 
            // layoutControlRight
            // 
            this.layoutControlRight.Controls.Add(this.lblRoomTitle);
            this.layoutControlRight.Controls.Add(this.lblStatusBadge);
            this.layoutControlRight.Controls.Add(this.lblHoTen);
            this.layoutControlRight.Controls.Add(this.lblSDT);
            this.layoutControlRight.Controls.Add(this.lblCheckIn);
            this.layoutControlRight.Controls.Add(this.lblCheckOut);
            this.layoutControlRight.Controls.Add(this.lblTienPhong);
            this.layoutControlRight.Controls.Add(this.lblPhuThu);
            this.layoutControlRight.Controls.Add(this.lblDaCoc);
            this.layoutControlRight.Controls.Add(this.lblTongTien);
            this.layoutControlRight.Controls.Add(this.btnActionCheckOut);
            this.layoutControlRight.Controls.Add(this.btnActionMinibar);
            this.layoutControlRight.Controls.Add(this.btnActionDoiPhong);
            this.layoutControlRight.Controls.Add(this.btnActionGiaHan);
            this.layoutControlRight.Controls.Add(this.btnActionThemDichVu);
            this.layoutControlRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControlRight.Location = new System.Drawing.Point(0, 0);
            this.layoutControlRight.Name = "layoutControlRight";
            this.layoutControlRight.Root = this.layoutControlGroupRight;
            this.layoutControlRight.Size = new System.Drawing.Size(320, 652);
            this.layoutControlRight.TabIndex = 0;
            // 
            // lblRoomTitle
            // 
            this.lblRoomTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblRoomTitle.Appearance.Options.UseFont = true;
            this.lblRoomTitle.Location = new System.Drawing.Point(12, 12);
            this.lblRoomTitle.Name = "lblRoomTitle";
            this.lblRoomTitle.Size = new System.Drawing.Size(183, 30);
            this.lblRoomTitle.StyleController = this.layoutControlRight;
            this.lblRoomTitle.TabIndex = 9;
            this.lblRoomTitle.Text = "Chưa chọn phòng";
            // 
            // lblStatusBadge
            // 
            this.lblStatusBadge.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStatusBadge.Appearance.Options.UseFont = true;
            this.lblStatusBadge.Location = new System.Drawing.Point(12, 46);
            this.lblStatusBadge.Name = "lblStatusBadge";
            this.lblStatusBadge.Size = new System.Drawing.Size(10, 17);
            this.lblStatusBadge.StyleController = this.layoutControlRight;
            this.lblStatusBadge.TabIndex = 12;
            this.lblStatusBadge.Text = "--";
            // 
            // lblHoTen
            // 
            this.lblHoTen.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblHoTen.Appearance.Options.UseFont = true;
            this.lblHoTen.Location = new System.Drawing.Point(103, 101);
            this.lblHoTen.Name = "lblHoTen";
            this.lblHoTen.Size = new System.Drawing.Size(10, 17);
            this.lblHoTen.StyleController = this.layoutControlRight;
            this.lblHoTen.TabIndex = 13;
            this.lblHoTen.Text = "--";
            // 
            // lblSDT
            // 
            this.lblSDT.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSDT.Appearance.Options.UseFont = true;
            this.lblSDT.Location = new System.Drawing.Point(103, 122);
            this.lblSDT.Name = "lblSDT";
            this.lblSDT.Size = new System.Drawing.Size(10, 17);
            this.lblSDT.StyleController = this.layoutControlRight;
            this.lblSDT.TabIndex = 14;
            this.lblSDT.Text = "--";
            // 
            // lblCheckIn
            // 
            this.lblCheckIn.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCheckIn.Appearance.Options.UseFont = true;
            this.lblCheckIn.Location = new System.Drawing.Point(103, 190);
            this.lblCheckIn.Name = "lblCheckIn";
            this.lblCheckIn.Size = new System.Drawing.Size(10, 17);
            this.lblCheckIn.StyleController = this.layoutControlRight;
            this.lblCheckIn.TabIndex = 15;
            this.lblCheckIn.Text = "--";
            // 
            // lblCheckOut
            // 
            this.lblCheckOut.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCheckOut.Appearance.Options.UseFont = true;
            this.lblCheckOut.Location = new System.Drawing.Point(103, 211);
            this.lblCheckOut.Name = "lblCheckOut";
            this.lblCheckOut.Size = new System.Drawing.Size(10, 17);
            this.lblCheckOut.StyleController = this.layoutControlRight;
            this.lblCheckOut.TabIndex = 16;
            this.lblCheckOut.Text = "--";
            // 
            // lblTienPhong
            // 
            this.lblTienPhong.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTienPhong.Appearance.Options.UseFont = true;
            this.lblTienPhong.Location = new System.Drawing.Point(103, 279);
            this.lblTienPhong.Name = "lblTienPhong";
            this.lblTienPhong.Size = new System.Drawing.Size(10, 17);
            this.lblTienPhong.StyleController = this.layoutControlRight;
            this.lblTienPhong.TabIndex = 17;
            this.lblTienPhong.Text = "--";
            // 
            // lblPhuThu
            // 
            this.lblPhuThu.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblPhuThu.Appearance.Options.UseFont = true;
            this.lblPhuThu.Location = new System.Drawing.Point(103, 300);
            this.lblPhuThu.Name = "lblPhuThu";
            this.lblPhuThu.Size = new System.Drawing.Size(10, 17);
            this.lblPhuThu.StyleController = this.layoutControlRight;
            this.lblPhuThu.TabIndex = 18;
            this.lblPhuThu.Text = "--";
            // 
            // lblDaCoc
            // 
            this.lblDaCoc.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDaCoc.Appearance.Options.UseFont = true;
            this.lblDaCoc.Location = new System.Drawing.Point(103, 321);
            this.lblDaCoc.Name = "lblDaCoc";
            this.lblDaCoc.Size = new System.Drawing.Size(10, 17);
            this.lblDaCoc.StyleController = this.layoutControlRight;
            this.lblDaCoc.TabIndex = 19;
            this.lblDaCoc.Text = "--";
            // 
            // lblTongTien
            // 
            this.lblTongTien.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.Appearance.Options.UseFont = true;
            this.lblTongTien.Location = new System.Drawing.Point(103, 342);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.Size = new System.Drawing.Size(10, 17);
            this.lblTongTien.StyleController = this.layoutControlRight;
            this.lblTongTien.TabIndex = 20;
            this.lblTongTien.Text = "--";
            // 
            // btnActionCheckOut
            // 
            this.btnActionCheckOut.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnActionCheckOut.Appearance.Options.UseFont = true;
            this.btnActionCheckOut.ImageOptions.ImageUri.Uri = "Currency";
            this.btnActionCheckOut.Location = new System.Drawing.Point(25, 424);
            this.btnActionCheckOut.Name = "btnActionCheckOut";
            this.btnActionCheckOut.Size = new System.Drawing.Size(270, 65);
            this.btnActionCheckOut.StyleController = this.layoutControlRight;
            this.btnActionCheckOut.TabIndex = 1;
            this.btnActionCheckOut.Text = "Thanh Toán && Check-out";
            this.btnActionCheckOut.Click += new System.EventHandler(this.BtnActionCheckOut_Click);
            // 
            // btnActionMinibar
            // 
            this.btnActionMinibar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnActionMinibar.Appearance.Options.UseFont = true;
            this.btnActionMinibar.ImageOptions.ImageUri.Uri = "Currency";
            this.btnActionMinibar.Location = new System.Drawing.Point(25, 487);
            this.btnActionMinibar.Name = "btnActionMinibar";
            this.btnActionMinibar.Size = new System.Drawing.Size(122, 65);
            this.btnActionMinibar.StyleController = this.layoutControlRight;
            this.btnActionMinibar.TabIndex = 2;
            this.btnActionMinibar.Text = "Phụ thu";
            this.btnActionMinibar.Click += new System.EventHandler(this.BtnActionMinibar_Click);
            // 
            // btnActionDoiPhong
            // 
            this.btnActionDoiPhong.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnActionDoiPhong.Appearance.Options.UseFont = true;
            this.btnActionDoiPhong.ImageOptions.ImageUri.Uri = "Replace";
            this.btnActionDoiPhong.Location = new System.Drawing.Point(161, 487);
            this.btnActionDoiPhong.Name = "btnActionDoiPhong";
            this.btnActionDoiPhong.Size = new System.Drawing.Size(134, 65);
            this.btnActionDoiPhong.StyleController = this.layoutControlRight;
            this.btnActionDoiPhong.TabIndex = 3;
            this.btnActionDoiPhong.Text = "Đổi Phòng";
            this.btnActionDoiPhong.Click += new System.EventHandler(this.BtnActionDoiPhong_Click);
            // 
            // btnActionGiaHan
            // 
            this.btnActionGiaHan.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnActionGiaHan.Appearance.Options.UseFont = true;
            this.btnActionGiaHan.ImageOptions.ImageUri.Uri = "Forward";
            this.btnActionGiaHan.Location = new System.Drawing.Point(25, 561);
            this.btnActionGiaHan.Name = "btnActionGiaHan";
            this.btnActionGiaHan.Size = new System.Drawing.Size(122, 66);
            this.btnActionGiaHan.StyleController = this.layoutControlRight;
            this.btnActionGiaHan.TabIndex = 4;
            this.btnActionGiaHan.Text = "Gia Hạn";
            this.btnActionGiaHan.Click += new System.EventHandler(this.BtnActionGiaHan_Click);
            // 
            // btnActionThemDichVu
            // 
            this.btnActionThemDichVu.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnActionThemDichVu.Appearance.Options.UseFont = true;
            this.btnActionThemDichVu.ImageOptions.ImageUri.Uri = "Add";
            this.btnActionThemDichVu.Location = new System.Drawing.Point(161, 561);
            this.btnActionThemDichVu.Name = "btnActionThemDichVu";
            this.btnActionThemDichVu.Size = new System.Drawing.Size(134, 66);
            this.btnActionThemDichVu.StyleController = this.layoutControlRight;
            this.btnActionThemDichVu.TabIndex = 6;
            this.btnActionThemDichVu.Text = "In bill tạm";
            this.btnActionThemDichVu.Click += new System.EventHandler(this.BtnActionThemDichVu_Click);
            // 
            // layoutControlGroupRight
            // 
            this.layoutControlGroupRight.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroupRight.GroupBordersVisible = false;
            this.layoutControlGroupRight.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.groupInfo,
            this.groupKhachHang,
            this.groupThoiGian,
            this.groupTaiChinh,
            this.emptySpaceRight,
            this.groupThaoTac});
            this.layoutControlGroupRight.Name = "layoutControlGroupRight";
            this.layoutControlGroupRight.Size = new System.Drawing.Size(320, 652);
            this.layoutControlGroupRight.TextVisible = false;
            // 
            // groupInfo
            // 
            this.groupInfo.GroupBordersVisible = false;
            this.groupInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciRoomTitle,
            this.lciStatusBadge});
            this.groupInfo.Location = new System.Drawing.Point(0, 0);
            this.groupInfo.Name = "groupInfo";
            this.groupInfo.Size = new System.Drawing.Size(300, 55);
            this.groupInfo.TextVisible = false;
            // 
            // lciRoomTitle
            // 
            this.lciRoomTitle.Control = this.lblRoomTitle;
            this.lciRoomTitle.Location = new System.Drawing.Point(0, 0);
            this.lciRoomTitle.Name = "lciRoomTitle";
            this.lciRoomTitle.Size = new System.Drawing.Size(300, 34);
            this.lciRoomTitle.TextSize = new System.Drawing.Size(0, 0);
            this.lciRoomTitle.TextVisible = false;
            // 
            // lciStatusBadge
            // 
            this.lciStatusBadge.Control = this.lblStatusBadge;
            this.lciStatusBadge.Location = new System.Drawing.Point(0, 34);
            this.lciStatusBadge.Name = "lciStatusBadge";
            this.lciStatusBadge.Size = new System.Drawing.Size(300, 21);
            this.lciStatusBadge.TextSize = new System.Drawing.Size(0, 0);
            this.lciStatusBadge.TextVisible = false;
            // 
            // groupKhachHang
            // 
            this.groupKhachHang.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupKhachHang.AppearanceGroup.Options.UseFont = true;
            this.groupKhachHang.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciHoTen,
            this.lciSDT});
            this.groupKhachHang.Location = new System.Drawing.Point(0, 55);
            this.groupKhachHang.Name = "groupKhachHang";
            this.groupKhachHang.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            this.groupKhachHang.Size = new System.Drawing.Size(300, 89);
            this.groupKhachHang.Text = "KHÁCH HÀNG";
            // 
            // lciHoTen
            // 
            this.lciHoTen.Control = this.lblHoTen;
            this.lciHoTen.Location = new System.Drawing.Point(0, 0);
            this.lciHoTen.Name = "lciHoTen";
            this.lciHoTen.Size = new System.Drawing.Size(274, 21);
            this.lciHoTen.Text = "Họ tên:";
            this.lciHoTen.TextSize = new System.Drawing.Size(66, 13);
            // 
            // lciSDT
            // 
            this.lciSDT.Control = this.lblSDT;
            this.lciSDT.Location = new System.Drawing.Point(0, 21);
            this.lciSDT.Name = "lciSDT";
            this.lciSDT.Size = new System.Drawing.Size(274, 21);
            this.lciSDT.Text = "Số điện thoại:";
            this.lciSDT.TextSize = new System.Drawing.Size(66, 13);
            // 
            // groupThoiGian
            // 
            this.groupThoiGian.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupThoiGian.AppearanceGroup.Options.UseFont = true;
            this.groupThoiGian.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciCheckIn,
            this.lciCheckOut});
            this.groupThoiGian.Location = new System.Drawing.Point(0, 144);
            this.groupThoiGian.Name = "groupThoiGian";
            this.groupThoiGian.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            this.groupThoiGian.Size = new System.Drawing.Size(300, 89);
            this.groupThoiGian.Text = "THỜI GIAN";
            // 
            // lciCheckIn
            // 
            this.lciCheckIn.Control = this.lblCheckIn;
            this.lciCheckIn.Location = new System.Drawing.Point(0, 0);
            this.lciCheckIn.Name = "lciCheckIn";
            this.lciCheckIn.Size = new System.Drawing.Size(274, 21);
            this.lciCheckIn.Text = "Check-in:";
            this.lciCheckIn.TextSize = new System.Drawing.Size(66, 13);
            // 
            // lciCheckOut
            // 
            this.lciCheckOut.Control = this.lblCheckOut;
            this.lciCheckOut.Location = new System.Drawing.Point(0, 21);
            this.lciCheckOut.Name = "lciCheckOut";
            this.lciCheckOut.Size = new System.Drawing.Size(274, 21);
            this.lciCheckOut.Text = "Check-out:";
            this.lciCheckOut.TextSize = new System.Drawing.Size(66, 13);
            // 
            // groupTaiChinh
            // 
            this.groupTaiChinh.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupTaiChinh.AppearanceGroup.Options.UseFont = true;
            this.groupTaiChinh.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciTienPhong,
            this.lciPhuThu,
            this.lciDaCoc,
            this.lciTongTien});
            this.groupTaiChinh.Location = new System.Drawing.Point(0, 233);
            this.groupTaiChinh.Name = "groupTaiChinh";
            this.groupTaiChinh.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 10, 10, 10);
            this.groupTaiChinh.Size = new System.Drawing.Size(300, 131);
            this.groupTaiChinh.Text = "TÀI CHÍNH";
            // 
            // lciTienPhong
            // 
            this.lciTienPhong.Control = this.lblTienPhong;
            this.lciTienPhong.Location = new System.Drawing.Point(0, 0);
            this.lciTienPhong.Name = "lciTienPhong";
            this.lciTienPhong.Size = new System.Drawing.Size(274, 21);
            this.lciTienPhong.Text = "Tiền phòng:";
            this.lciTienPhong.TextSize = new System.Drawing.Size(66, 13);
            // 
            // lciPhuThu
            // 
            this.lciPhuThu.Control = this.lblPhuThu;
            this.lciPhuThu.Location = new System.Drawing.Point(0, 21);
            this.lciPhuThu.Name = "lciPhuThu";
            this.lciPhuThu.Size = new System.Drawing.Size(274, 21);
            this.lciPhuThu.Text = "Phụ thu:";
            this.lciPhuThu.TextSize = new System.Drawing.Size(66, 13);
            // 
            // lciDaCoc
            // 
            this.lciDaCoc.Control = this.lblDaCoc;
            this.lciDaCoc.Location = new System.Drawing.Point(0, 42);
            this.lciDaCoc.Name = "lciDaCoc";
            this.lciDaCoc.Size = new System.Drawing.Size(274, 21);
            this.lciDaCoc.Text = "Đã cọc:";
            this.lciDaCoc.TextSize = new System.Drawing.Size(66, 13);
            // 
            // lciTongTien
            // 
            this.lciTongTien.Control = this.lblTongTien;
            this.lciTongTien.Location = new System.Drawing.Point(0, 63);
            this.lciTongTien.Name = "lciTongTien";
            this.lciTongTien.Size = new System.Drawing.Size(274, 21);
            this.lciTongTien.Text = "Tổng tiền:";
            this.lciTongTien.TextSize = new System.Drawing.Size(66, 13);
            // 
            // emptySpaceRight
            // 
            this.emptySpaceRight.AllowHotTrack = false;
            this.emptySpaceRight.Location = new System.Drawing.Point(0, 364);
            this.emptySpaceRight.MinSize = new System.Drawing.Size(1, 10);
            this.emptySpaceRight.Name = "emptySpaceRight";
            this.emptySpaceRight.Size = new System.Drawing.Size(300, 14);
            this.emptySpaceRight.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceRight.TextSize = new System.Drawing.Size(0, 0);
            // 
            // groupThaoTac
            // 
            this.groupThaoTac.AppearanceGroup.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.groupThaoTac.AppearanceGroup.Options.UseFont = true;
            this.groupThaoTac.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciBtnCheckOut,
            this.lciBtnMinibar,
            this.lciBtnDoiPhong,
            this.lciBtnGiaHan,
            this.lciBtnThemDichVu,
            this.lciBtnOpenMinibar});
            this.groupThaoTac.Location = new System.Drawing.Point(0, 378);
            this.groupThaoTac.Name = "groupThaoTac";
            this.groupThaoTac.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 4, 4, 4);
            this.groupThaoTac.Size = new System.Drawing.Size(300, 130);
            this.groupThaoTac.Text = "THAO TÁC NHANH";
            // 
            // lciBtnCheckOut
            // 
            this.lciBtnCheckOut.Control = this.btnActionCheckOut;
            this.lciBtnCheckOut.Location = new System.Drawing.Point(0, 0);
            this.lciBtnCheckOut.MinSize = new System.Drawing.Size(1, 40);
            this.lciBtnCheckOut.MaxSize = new System.Drawing.Size(0, 40);
            this.lciBtnCheckOut.Name = "lciBtnCheckOut";
            this.lciBtnCheckOut.Size = new System.Drawing.Size(274, 50);
            this.lciBtnCheckOut.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnCheckOut.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 5);
            this.lciBtnCheckOut.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnCheckOut.TextVisible = false;
            // 
            // lciBtnMinibar
            // 
            this.lciBtnMinibar.Control = this.btnActionMinibar;
            this.lciBtnMinibar.Location = new System.Drawing.Point(0, 63);
            this.lciBtnMinibar.MinSize = new System.Drawing.Size(1, 40);
            this.lciBtnMinibar.MaxSize = new System.Drawing.Size(0, 40);
            this.lciBtnMinibar.Name = "lciBtnMinibar";
            this.lciBtnMinibar.Size = new System.Drawing.Size(131, 40);
            this.lciBtnMinibar.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnMinibar.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 5, 0, 5);
            this.lciBtnMinibar.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnMinibar.TextVisible = false;
            // 
            // lciBtnDoiPhong
            // 
            this.lciBtnDoiPhong.Control = this.btnActionDoiPhong;
            this.lciBtnDoiPhong.Location = new System.Drawing.Point(131, 63);
            this.lciBtnDoiPhong.MinSize = new System.Drawing.Size(1, 36);
            this.lciBtnDoiPhong.Name = "lciBtnDoiPhong";
            this.lciBtnDoiPhong.Size = new System.Drawing.Size(143, 36);
            this.lciBtnDoiPhong.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnDoiPhong.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 5);
            this.lciBtnDoiPhong.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnDoiPhong.TextVisible = false;
            // 
            // lciBtnGiaHan
            // 
            this.lciBtnGiaHan.Control = this.btnActionGiaHan;
            this.lciBtnGiaHan.Location = new System.Drawing.Point(0, 137);
            this.lciBtnGiaHan.MinSize = new System.Drawing.Size(1, 36);
            this.lciBtnGiaHan.Name = "lciBtnGiaHan";
            this.lciBtnGiaHan.Size = new System.Drawing.Size(131, 36);
            this.lciBtnGiaHan.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnGiaHan.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 5, 0, 0);
            this.lciBtnGiaHan.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnGiaHan.TextVisible = false;
            // 
            // lciBtnThemDichVu
            // 
            this.lciBtnThemDichVu.Control = this.btnActionThemDichVu;
            this.lciBtnThemDichVu.Location = new System.Drawing.Point(131, 137);
            this.lciBtnThemDichVu.MinSize = new System.Drawing.Size(1, 36);
            this.lciBtnThemDichVu.Name = "lciBtnThemDichVu";
            this.lciBtnThemDichVu.Size = new System.Drawing.Size(143, 36);
            this.lciBtnThemDichVu.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnThemDichVu.Spacing = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lciBtnThemDichVu.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnThemDichVu.TextVisible = false;
            // 
            // btnActionOpenMinibar
            // 
            this.btnActionOpenMinibar.Appearance.Font = new System.Drawing.Font("Segoe UI", 8.5F);
            this.btnActionOpenMinibar.Appearance.Options.UseFont = true;
            this.btnActionOpenMinibar.ImageOptions.ImageUri.Uri = "ShoppingCart";
            this.btnActionOpenMinibar.Location = new System.Drawing.Point(25, 630);
            this.btnActionOpenMinibar.Name = "btnActionOpenMinibar";
            this.btnActionOpenMinibar.Size = new System.Drawing.Size(270, 65);
            this.btnActionOpenMinibar.StyleController = this.layoutControlRight;
            this.btnActionOpenMinibar.TabIndex = 7;
            this.btnActionOpenMinibar.Text = "Minibar";
            this.btnActionOpenMinibar.Click += new System.EventHandler(this.BtnActionOpenMinibar_Click);
            // 
            // lciBtnOpenMinibar
            // 
            this.lciBtnOpenMinibar.Control = this.btnActionOpenMinibar;
            this.lciBtnOpenMinibar.Location = new System.Drawing.Point(0, 173);
            this.lciBtnOpenMinibar.MinSize = new System.Drawing.Size(1, 36);
            this.lciBtnOpenMinibar.Name = "lciBtnOpenMinibar";
            this.lciBtnOpenMinibar.Size = new System.Drawing.Size(274, 36);
            this.lciBtnOpenMinibar.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciBtnOpenMinibar.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 5, 0);
            this.lciBtnOpenMinibar.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnOpenMinibar.TextVisible = false;
            // 
            // btnLamMoi
            this.btnLamMoi.ImageOptions.ImageUri.Uri = "Refresh";
            this.btnLamMoi.Location = new System.Drawing.Point(10, 8);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(120, 32);
            this.btnLamMoi.TabIndex = 1;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.BtnLamMoi_Click);
            // 
            // btnChuong
            //
            this.btnChuong.ImageOptions.ImageUri.Uri = "Alert";
            this.btnChuong.Location = new System.Drawing.Point(140, 8);
            this.btnChuong.Name = "btnChuong";
            this.btnChuong.Size = new System.Drawing.Size(140, 32);
            this.btnChuong.TabIndex = 10;
            this.btnChuong.Text = "🔔 Đặt trước";
            this.btnChuong.Click += new System.EventHandler(this.BtnChuong_Click);
            //
            // timerKiemTraDat
            //
            this.timerKiemTraDat.Interval = 15000;

            this.timerKiemTraDat.Tick += new System.EventHandler(this.TimerKiemTraDat_Tick);
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnChuong);
            this.panelTop.Controls.Add(this.btnLamMoi);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1200, 48);
            this.panelTop.TabIndex = 0;
            // 
            // ucLuuTru_Main
            // 
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.panelTop);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ucLuuTru_Main";
            this.Size = new System.Drawing.Size(1200, 700);
            this.Load += new System.EventHandler(this.ucLuuTru_Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            this.splitContainerControl1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControlPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileViewPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlRight)).EndInit();
            this.layoutControlRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroupRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciRoomTitle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciStatusBadge)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupKhachHang)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciHoTen)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciSDT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupThoiGian)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCheckIn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCheckOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupTaiChinh)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTienPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciPhuThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciDaCoc)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTongTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceRight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupThaoTac)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnCheckOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnMinibar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnDoiPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnGiaHan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnThemDichVu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnOpenMinibar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelTop)).EndInit();
            this.panelTop.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridControlPhong;
        private DevExpress.XtraGrid.Views.Tile.TileView tileViewPhong;
        private DevExpress.XtraEditors.SimpleButton btnLamMoi;
        private DevExpress.XtraEditors.SimpleButton btnChuong;
        private System.Windows.Forms.Timer timerKiemTraDat;
        private DevExpress.XtraEditors.PanelControl panelTop;
        
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
                private DevExpress.XtraLayout.LayoutControl layoutControlRight;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroupRight;
        private DevExpress.XtraLayout.LayoutControlGroup groupInfo;
        private DevExpress.XtraLayout.LayoutControlItem lciRoomTitle;
        private DevExpress.XtraLayout.LayoutControlItem lciStatusBadge;
        private DevExpress.XtraLayout.LayoutControlGroup groupKhachHang;
        private DevExpress.XtraLayout.LayoutControlItem lciHoTen;
        private DevExpress.XtraLayout.LayoutControlItem lciSDT;
        private DevExpress.XtraLayout.LayoutControlGroup groupThoiGian;
        private DevExpress.XtraLayout.LayoutControlItem lciCheckIn;
        private DevExpress.XtraLayout.LayoutControlItem lciCheckOut;
        private DevExpress.XtraLayout.LayoutControlGroup groupTaiChinh;
        private DevExpress.XtraLayout.LayoutControlItem lciTienPhong;
        private DevExpress.XtraLayout.LayoutControlItem lciPhuThu;
        private DevExpress.XtraLayout.LayoutControlItem lciDaCoc;
        private DevExpress.XtraLayout.LayoutControlItem lciTongTien;
        private DevExpress.XtraLayout.LayoutControlGroup groupThaoTac;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnCheckOut;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnMinibar;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnDoiPhong;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnGiaHan;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnThemDichVu;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceRight;
        
        private DevExpress.XtraEditors.LabelControl lblRoomTitle;
        private DevExpress.XtraEditors.LabelControl lblStatusBadge;
        private DevExpress.XtraEditors.LabelControl lblHoTen;
        private DevExpress.XtraEditors.LabelControl lblSDT;
        private DevExpress.XtraEditors.LabelControl lblCheckIn;
        private DevExpress.XtraEditors.LabelControl lblCheckOut;
        private DevExpress.XtraEditors.LabelControl lblTienPhong;
        private DevExpress.XtraEditors.LabelControl lblPhuThu;
        private DevExpress.XtraEditors.LabelControl lblDaCoc;
        private DevExpress.XtraEditors.LabelControl lblTongTien;
        private DevExpress.XtraEditors.SimpleButton btnActionCheckOut;
        private DevExpress.XtraEditors.SimpleButton btnActionMinibar;
        private DevExpress.XtraEditors.SimpleButton btnActionThemDichVu;
        private DevExpress.XtraEditors.SimpleButton btnActionGiaHan;
        private DevExpress.XtraEditors.SimpleButton btnActionDoiPhong;
        private DevExpress.XtraEditors.SimpleButton btnActionOpenMinibar;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnOpenMinibar;

        private DevExpress.XtraGrid.Columns.TileViewColumn colIdPhong;
        private DevExpress.XtraGrid.Columns.TileViewColumn colMaPhong;
        private DevExpress.XtraGrid.Columns.TileViewColumn colTenLoaiPhong;
        private DevExpress.XtraGrid.Columns.TileViewColumn colTrangThai;
        private DevExpress.XtraGrid.Columns.TileViewColumn colTenKhachHang;
        private DevExpress.XtraGrid.Columns.TileViewColumn colThoiGianO;
        private DevExpress.XtraGrid.Columns.TileViewColumn colIdChiTietDatPhong;
        private DevExpress.XtraGrid.Columns.TileViewColumn colIsLateCheckOut;

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.PopupMenu popupMenu1;
        private DevExpress.XtraBars.BarButtonItem btnCheckIn;
        private DevExpress.XtraBars.BarButtonItem btnHuyDat;
        private DevExpress.XtraBars.BarButtonItem btnMinibar;
        private DevExpress.XtraBars.BarButtonItem btnDoiPhong;
        private DevExpress.XtraBars.BarButtonItem btnGiaHan;
        private DevExpress.XtraBars.BarButtonItem btnDonXong;
        private DevExpress.XtraBars.BarButtonItem btnBaoTri;
        private DevExpress.XtraBars.BarButtonItem btnSuaXong;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}
