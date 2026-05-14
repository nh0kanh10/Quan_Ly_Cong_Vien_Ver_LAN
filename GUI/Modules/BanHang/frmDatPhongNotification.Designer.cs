using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Grid;
using ET.DTOs;

namespace GUI.Modules.BanHang
{
    partial class frmDatPhongNotification
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
                components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.pnlTop        = new DevExpress.XtraEditors.PanelControl();
            this.lblTieuDe     = new DevExpress.XtraEditors.LabelControl();
            this.btnTatCa     = new DevExpress.XtraEditors.SimpleButton();
            this.btnTuanNay    = new DevExpress.XtraEditors.SimpleButton();
            this.btnHomNay     = new DevExpress.XtraEditors.SimpleButton();
            this.gridDatPhong  = new DevExpress.XtraGrid.GridControl();
            this.gridViewMain  = new DevExpress.XtraGrid.Views.Grid.GridView();

            this.colMaDatPhong    = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenKhachHang  = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoDienThoai   = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenLoaiPhong  = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThoiGianO     = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoNgay        = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTienCoc       = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThoiGianDat   = new DevExpress.XtraGrid.Columns.GridColumn();

            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDatPhong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).BeginInit();
            this.SuspendLayout();

            //
            // pnlTop
            //
            this.pnlTop.Controls.Add(this.lblTieuDe);
            this.pnlTop.Controls.Add(this.btnTatCa);
            this.pnlTop.Controls.Add(this.btnTuanNay);
            this.pnlTop.Controls.Add(this.btnHomNay);
            this.pnlTop.Dock     = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name     = "pnlTop";
            this.pnlTop.Size     = new System.Drawing.Size(780, 48);
            this.pnlTop.TabIndex = 0;

            //
            // lblTieuDe
            //
            this.lblTieuDe.Appearance.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTieuDe.Appearance.Options.UseFont = true;
            this.lblTieuDe.Location = new System.Drawing.Point(12, 13);
            this.lblTieuDe.Name     = "lblTieuDe";
            this.lblTieuDe.Text     = "Phiếu đặt phòng chờ nhận";

            //
            // btnTatCa
            //
            this.btnTatCa.Location = new System.Drawing.Point(472, 10);
            this.btnTatCa.Name     = "btnTatCa";
            this.btnTatCa.Size     = new System.Drawing.Size(95, 28);
            this.btnTatCa.TabIndex = 3;
            this.btnTatCa.Text     = "Tất cả";
            this.btnTatCa.Click  += new System.EventHandler(this.BtnTatCa_Click);

            //
            // btnTuanNay
            //
            this.btnTuanNay.Location = new System.Drawing.Point(572, 10);
            this.btnTuanNay.Name     = "btnTuanNay";
            this.btnTuanNay.Size     = new System.Drawing.Size(95, 28);
            this.btnTuanNay.TabIndex = 1;
            this.btnTuanNay.Text     = "7 ngày tới";
            this.btnTuanNay.Click  += new System.EventHandler(this.BtnTuanNay_Click);

            //
            // btnHomNay
            //
            this.btnHomNay.Location = new System.Drawing.Point(672, 10);
            this.btnHomNay.Name     = "btnHomNay";
            this.btnHomNay.Size     = new System.Drawing.Size(95, 28);
            this.btnHomNay.TabIndex = 2;
            this.btnHomNay.Text     = "Hôm nay";
            this.btnHomNay.Click  += new System.EventHandler(this.BtnHomNay_Click);

            //
            // colMaDatPhong
            //
            this.colMaDatPhong.FieldName    = "MaDatPhong";
            this.colMaDatPhong.Caption      = "Mã đặt";
            this.colMaDatPhong.Visible      = true;
            this.colMaDatPhong.VisibleIndex = 0;
            this.colMaDatPhong.Width        = 110;

            //
            // colTenKhachHang
            //
            this.colTenKhachHang.FieldName    = "TenKhachHang";
            this.colTenKhachHang.Caption      = "Khách hàng";
            this.colTenKhachHang.Visible      = true;
            this.colTenKhachHang.VisibleIndex = 1;
            this.colTenKhachHang.Width        = 155;

            //
            // colSoDienThoai
            //
            this.colSoDienThoai.FieldName    = "SoDienThoai";
            this.colSoDienThoai.Caption      = "SĐT";
            this.colSoDienThoai.Visible      = true;
            this.colSoDienThoai.VisibleIndex = 2;
            this.colSoDienThoai.Width        = 110;

            //
            // colTenLoaiPhong
            //
            this.colTenLoaiPhong.FieldName    = "TenLoaiPhong";
            this.colTenLoaiPhong.Caption      = "Loại phòng";
            this.colTenLoaiPhong.Visible      = true;
            this.colTenLoaiPhong.VisibleIndex = 3;
            this.colTenLoaiPhong.Width        = 130;

            //
            // colThoiGianO
            //
            this.colThoiGianO.FieldName    = "ThoiGianO";
            this.colThoiGianO.Caption      = "Ngày ở";
            this.colThoiGianO.Visible      = true;
            this.colThoiGianO.VisibleIndex = 4;
            this.colThoiGianO.Width        = 135;

            //
            // colSoNgay
            //
            this.colSoNgay.FieldName    = "SoNgay";
            this.colSoNgay.Caption      = "Số đêm";
            this.colSoNgay.Visible      = true;
            this.colSoNgay.VisibleIndex = 5;
            this.colSoNgay.Width        = 65;

            //
            // colTienCoc
            //
            this.colTienCoc.FieldName    = "TienCoc";
            this.colTienCoc.Caption      = "Đã cọc";
            this.colTienCoc.Visible      = true;
            this.colTienCoc.VisibleIndex = 6;
            this.colTienCoc.Width        = 90;
            this.colTienCoc.DisplayFormat.FormatType   = DevExpress.Utils.FormatType.Numeric;
            this.colTienCoc.DisplayFormat.FormatString = "N0";

            //
            // colThoiGianDat
            //
            this.colThoiGianDat.FieldName    = "ThoiGianDat";
            this.colThoiGianDat.Caption      = "Đặt lúc";
            this.colThoiGianDat.Visible      = true;
            this.colThoiGianDat.VisibleIndex = 7;
            this.colThoiGianDat.Width        = 85;

            //
            // gridViewMain
            //
            this.gridViewMain.Columns.Add(this.colMaDatPhong);
            this.gridViewMain.Columns.Add(this.colTenKhachHang);
            this.gridViewMain.Columns.Add(this.colSoDienThoai);
            this.gridViewMain.Columns.Add(this.colTenLoaiPhong);
            this.gridViewMain.Columns.Add(this.colThoiGianO);
            this.gridViewMain.Columns.Add(this.colSoNgay);
            this.gridViewMain.Columns.Add(this.colTienCoc);
            this.gridViewMain.Columns.Add(this.colThoiGianDat);
            this.gridViewMain.Name = "gridViewMain";
            this.gridViewMain.OptionsBehavior.Editable  = false;
            this.gridViewMain.OptionsView.ShowGroupPanel = false;
            this.gridViewMain.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.GridViewMain_RowCellStyle);

            //
            // gridDatPhong
            //
            this.gridDatPhong.MainView = this.gridViewMain;
            this.gridDatPhong.ViewCollection.Add(this.gridViewMain);
            this.gridDatPhong.Dock     = System.Windows.Forms.DockStyle.Fill;
            this.gridDatPhong.Location = new System.Drawing.Point(0, 48);
            this.gridDatPhong.Name     = "gridDatPhong";
            this.gridDatPhong.TabIndex = 1;

            //
            // frmDatPhongNotification
            //
            this.ClientSize      = new System.Drawing.Size(780, 480);
            this.Controls.Add(this.gridDatPhong);
            this.Controls.Add(this.pnlTop);
            this.Name            = "frmDatPhongNotification";
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text            = "Phiếu đặt phòng chờ nhận";

            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridDatPhong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewMain)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraEditors.PanelControl    pnlTop;
        private DevExpress.XtraEditors.LabelControl    lblTieuDe;
        private DevExpress.XtraEditors.SimpleButton    btnTatCa;
        private DevExpress.XtraEditors.SimpleButton    btnTuanNay;
        private DevExpress.XtraEditors.SimpleButton    btnHomNay;
        private DevExpress.XtraGrid.GridControl        gridDatPhong;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewMain;

        private DevExpress.XtraGrid.Columns.GridColumn colMaDatPhong;
        private DevExpress.XtraGrid.Columns.GridColumn colTenKhachHang;
        private DevExpress.XtraGrid.Columns.GridColumn colSoDienThoai;
        private DevExpress.XtraGrid.Columns.GridColumn colTenLoaiPhong;
        private DevExpress.XtraGrid.Columns.GridColumn colThoiGianO;
        private DevExpress.XtraGrid.Columns.GridColumn colSoNgay;
        private DevExpress.XtraGrid.Columns.GridColumn colTienCoc;
        private DevExpress.XtraGrid.Columns.GridColumn colThoiGianDat;
    }
}
