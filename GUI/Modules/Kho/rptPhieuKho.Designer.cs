namespace GUI.Modules.Kho
{
    partial class rptPhieuKho
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

        #region Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.ReportHeader = new DevExpress.XtraReports.UI.ReportHeaderBand();
            this.ReportFooter = new DevExpress.XtraReports.UI.ReportFooterBand();
            this.lblTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.lblMaPhieu = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNgay = new DevExpress.XtraReports.UI.XRLabel();
            this.lblKho = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNguoiLap = new DevExpress.XtraReports.UI.XRLabel();
            this.xrTable1 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow1 = new DevExpress.XtraReports.UI.XRTableRow();
            this.xrTableCell1 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell2 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell3 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTableCell4 = new DevExpress.XtraReports.UI.XRTableCell();
            this.xrTable2 = new DevExpress.XtraReports.UI.XRTable();
            this.xrTableRow2 = new DevExpress.XtraReports.UI.XRTableRow();
            this.cellSTT = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellTenSP = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellDVT = new DevExpress.XtraReports.UI.XRTableCell();
            this.cellSoLuong = new DevExpress.XtraReports.UI.XRTableCell();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 50F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 50F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrTable2});
            this.Detail.HeightF = 30F;
            this.Detail.Name = "Detail";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTitle,
            this.lblMaPhieu,
            this.lblNgay,
            this.lblKho,
            this.lblNguoiLap,
            this.xrTable1});
            this.ReportHeader.HeightF = 180F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // ReportFooter
            // 
            this.ReportFooter.HeightF = 50F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Arial", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.SizeF = new System.Drawing.SizeF(727F, 30F);
            this.lblTitle.Text = "PHIẾU KHO";
            this.lblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblMaPhieu
            // 
            this.lblMaPhieu.Font = new System.Drawing.Font("Arial", 10F);
            this.lblMaPhieu.LocationFloat = new DevExpress.Utils.PointFloat(0F, 40F);
            this.lblMaPhieu.Name = "lblMaPhieu";
            this.lblMaPhieu.SizeF = new System.Drawing.SizeF(350F, 25F);
            this.lblMaPhieu.Text = "Số phiếu: ";
            // 
            // lblNgay
            // 
            this.lblNgay.Font = new System.Drawing.Font("Arial", 10F);
            this.lblNgay.LocationFloat = new DevExpress.Utils.PointFloat(377F, 40F);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.SizeF = new System.Drawing.SizeF(350F, 25F);
            this.lblNgay.Text = "Ngày lập: ";
            // 
            // lblKho
            // 
            this.lblKho.Font = new System.Drawing.Font("Arial", 10F);
            this.lblKho.LocationFloat = new DevExpress.Utils.PointFloat(0F, 70F);
            this.lblKho.Name = "lblKho";
            this.lblKho.SizeF = new System.Drawing.SizeF(350F, 25F);
            this.lblKho.Text = "Kho xuất/nhập: ";
            // 
            // lblNguoiLap
            // 
            this.lblNguoiLap.Font = new System.Drawing.Font("Arial", 10F);
            this.lblNguoiLap.LocationFloat = new DevExpress.Utils.PointFloat(377F, 70F);
            this.lblNguoiLap.Name = "lblNguoiLap";
            this.lblNguoiLap.SizeF = new System.Drawing.SizeF(350F, 25F);
            this.lblNguoiLap.Text = "Người lập: ";
            // 
            // xrTable1
            // 
            this.xrTable1.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable1.Font = new System.Drawing.Font("Arial", 10F, System.Drawing.FontStyle.Bold);
            this.xrTable1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 150F);
            this.xrTable1.Name = "xrTable1";
            this.xrTable1.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow1});
            this.xrTable1.SizeF = new System.Drawing.SizeF(727F, 30F);
            this.xrTable1.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrTableRow1
            // 
            this.xrTableRow1.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.xrTableCell1,
            this.xrTableCell2,
            this.xrTableCell3,
            this.xrTableCell4});
            this.xrTableRow1.Name = "xrTableRow1";
            this.xrTableRow1.Weight = 1D;
            // 
            // xrTableCell1
            // 
            this.xrTableCell1.Name = "xrTableCell1";
            this.xrTableCell1.Text = "STT";
            this.xrTableCell1.Weight = 0.5D;
            // 
            // xrTableCell2
            // 
            this.xrTableCell2.Name = "xrTableCell2";
            this.xrTableCell2.Text = "Tên Sản Phẩm";
            this.xrTableCell2.Weight = 3.5D;
            // 
            // xrTableCell3
            // 
            this.xrTableCell3.Name = "xrTableCell3";
            this.xrTableCell3.Text = "ĐVT";
            this.xrTableCell3.Weight = 1D;
            // 
            // xrTableCell4
            // 
            this.xrTableCell4.Name = "xrTableCell4";
            this.xrTableCell4.Text = "Số Lượng";
            this.xrTableCell4.Weight = 1D;
            // 
            // xrTable2
            // 
            this.xrTable2.Borders = ((DevExpress.XtraPrinting.BorderSide)(((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrTable2.Font = new System.Drawing.Font("Arial", 10F);
            this.xrTable2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrTable2.Name = "xrTable2";
            this.xrTable2.Rows.AddRange(new DevExpress.XtraReports.UI.XRTableRow[] {
            this.xrTableRow2});
            this.xrTable2.SizeF = new System.Drawing.SizeF(727F, 30F);
            this.xrTable2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleLeft;
            // 
            // xrTableRow2
            // 
            this.xrTableRow2.Cells.AddRange(new DevExpress.XtraReports.UI.XRTableCell[] {
            this.cellSTT,
            this.cellTenSP,
            this.cellDVT,
            this.cellSoLuong});
            this.xrTableRow2.Name = "xrTableRow2";
            this.xrTableRow2.Weight = 1D;
            // 
            // cellSTT
            // 
            this.cellSTT.Name = "cellSTT";
            this.cellSTT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.cellSTT.Weight = 0.5D;
            // 
            // cellTenSP
            // 
            this.cellTenSP.Name = "cellTenSP";
            this.cellTenSP.Padding = new DevExpress.XtraPrinting.PaddingInfo(5, 0, 0, 0, 100F);
            this.cellTenSP.Weight = 3.5D;
            // 
            // cellDVT
            // 
            this.cellDVT.Name = "cellDVT";
            this.cellDVT.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.cellDVT.Weight = 1D;
            // 
            // cellSoLuong
            // 
            this.cellSoLuong.Name = "cellSoLuong";
            this.cellSoLuong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            this.cellSoLuong.Weight = 1D;
            // 
            // rptPhieuKho
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.Detail,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
            this.Font = new System.Drawing.Font("Arial", 9.75F);
            this.Margins = new System.Drawing.Printing.Margins(50, 50, 50, 50);
            this.PageHeight = 1169;
            this.PageWidth = 827;
            this.PaperKind = System.Drawing.Printing.PaperKind.A4;
            this.Version = "22.2";
            ((System.ComponentModel.ISupportInitialize)(this.xrTable1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xrTable2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();
        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        public DevExpress.XtraReports.UI.XRLabel lblTitle;
        public DevExpress.XtraReports.UI.XRLabel lblMaPhieu;
        public DevExpress.XtraReports.UI.XRLabel lblNgay;
        public DevExpress.XtraReports.UI.XRLabel lblKho;
        public DevExpress.XtraReports.UI.XRLabel lblNguoiLap;
        private DevExpress.XtraReports.UI.XRTable xrTable1;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell1;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell2;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell3;
        private DevExpress.XtraReports.UI.XRTableCell xrTableCell4;
        private DevExpress.XtraReports.UI.XRTable xrTable2;
        private DevExpress.XtraReports.UI.XRTableRow xrTableRow2;
        public DevExpress.XtraReports.UI.XRTableCell cellSTT;
        public DevExpress.XtraReports.UI.XRTableCell cellTenSP;
        public DevExpress.XtraReports.UI.XRTableCell cellDVT;
        public DevExpress.XtraReports.UI.XRTableCell cellSoLuong;
    }
}
