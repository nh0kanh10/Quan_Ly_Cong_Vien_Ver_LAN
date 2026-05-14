namespace GUI.Modules.BanHang
{
    partial class rptHoaDonK80
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
            this.lblSubTitle = new DevExpress.XtraReports.UI.XRLabel();
            this.lblMaDon = new DevExpress.XtraReports.UI.XRLabel();
            this.lblNgay = new DevExpress.XtraReports.UI.XRLabel();
            this.line1 = new DevExpress.XtraReports.UI.XRLine();
            this.line2 = new DevExpress.XtraReports.UI.XRLine();
            this.lblL1 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTongTien = new DevExpress.XtraReports.UI.XRLabel();
            this.lblL2 = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTienThua = new DevExpress.XtraReports.UI.XRLabel();
            this.line3 = new DevExpress.XtraReports.UI.XRLine();
            this.lblThanks = new DevExpress.XtraReports.UI.XRLabel();
            this.lblTenMon = new DevExpress.XtraReports.UI.XRLabel();
            this.lblSoLuong = new DevExpress.XtraReports.UI.XRLabel();
            this.lblThanhTien = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 10F;
            this.TopMargin.Name = "TopMargin";
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 20F;
            this.BottomMargin.Name = "BottomMargin";
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTenMon,
            this.lblSoLuong,
            this.lblThanhTien});
            this.Detail.HeightF = 25F;
            this.Detail.Name = "Detail";
            // 
            // ReportHeader
            // 
            this.ReportHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.lblTitle,
            this.lblSubTitle,
            this.lblMaDon,
            this.lblNgay,
            this.line1});
            this.ReportHeader.HeightF = 125F;
            this.ReportHeader.Name = "ReportHeader";
            // 
            // lblTitle
            // 
            this.lblTitle.Font = new System.Drawing.Font("Consolas", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.SizeF = new System.Drawing.SizeF(295F, 30F);
            this.lblTitle.Text = "KDL ĐẠI NAM";
            this.lblTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblSubTitle
            // 
            this.lblSubTitle.Font = new System.Drawing.Font("Consolas", 11F, System.Drawing.FontStyle.Bold);
            this.lblSubTitle.LocationFloat = new DevExpress.Utils.PointFloat(0F, 30F);
            this.lblSubTitle.Name = "lblSubTitle";
            this.lblSubTitle.SizeF = new System.Drawing.SizeF(295F, 25F);
            this.lblSubTitle.Text = "PHIẾU THANH TOÁN";
            this.lblSubTitle.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblMaDon
            // 
            this.lblMaDon.Font = new System.Drawing.Font("Consolas", 10F);
            this.lblMaDon.LocationFloat = new DevExpress.Utils.PointFloat(0F, 65F);
            this.lblMaDon.Name = "lblMaDon";
            this.lblMaDon.SizeF = new System.Drawing.SizeF(295F, 20F);
            this.lblMaDon.Text = "Mã đơn: ";
            // 
            // lblNgay
            // 
            this.lblNgay.Font = new System.Drawing.Font("Consolas", 10F);
            this.lblNgay.LocationFloat = new DevExpress.Utils.PointFloat(0F, 85F);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.SizeF = new System.Drawing.SizeF(295F, 20F);
            this.lblNgay.Text = "Ngày: ";
            // 
            // line1
            // 
            this.line1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 110F);
            this.line1.Name = "line1";
            this.line1.SizeF = new System.Drawing.SizeF(295F, 10F);
            // 
            // ReportFooter
            // 
            this.ReportFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.line2,
            this.lblL1,
            this.lblTongTien,
            this.lblL2,
            this.lblTienThua,
            this.line3,
            this.lblThanks});
            this.ReportFooter.HeightF = 200F;
            this.ReportFooter.Name = "ReportFooter";
            // 
            // line2
            // 
            this.line2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 5F);
            this.line2.Name = "line2";
            this.line2.SizeF = new System.Drawing.SizeF(295F, 10F);
            // 
            // lblL1
            // 
            this.lblL1.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblL1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 20F);
            this.lblL1.Name = "lblL1";
            this.lblL1.SizeF = new System.Drawing.SizeF(120F, 80F);
            this.lblL1.Multiline = true;
            this.lblL1.Text = "Tổng tiền hàng:\r\nGiảm giá:\r\nTrừ điểm:\r\nCẦN THANH TOÁN:";
            // 
            // lblTongTien
            // 
            this.lblTongTien.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongTien.LocationFloat = new DevExpress.Utils.PointFloat(120F, 20F);
            this.lblTongTien.Name = "lblTongTien";
            this.lblTongTien.SizeF = new System.Drawing.SizeF(175F, 80F);
            this.lblTongTien.Multiline = true;
            this.lblTongTien.Text = "0 ₫\r\n0 ₫\r\n0 ₫\r\n0 ₫";
            this.lblTongTien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopRight;
            // 
            // lblL2
            // 
            this.lblL2.Font = new System.Drawing.Font("Consolas", 10F);
            this.lblL2.LocationFloat = new DevExpress.Utils.PointFloat(0F, 105F);
            this.lblL2.Name = "lblL2";
            this.lblL2.SizeF = new System.Drawing.SizeF(100F, 20F);
            this.lblL2.Text = "Tiền thừa:";
            // 
            // lblTienThua
            // 
            this.lblTienThua.Font = new System.Drawing.Font("Consolas", 10F);
            this.lblTienThua.LocationFloat = new DevExpress.Utils.PointFloat(100F, 105F);
            this.lblTienThua.Name = "lblTienThua";
            this.lblTienThua.SizeF = new System.Drawing.SizeF(195F, 20F);
            this.lblTienThua.Text = "0 ₫";
            this.lblTienThua.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // line3
            // 
            this.line3.LocationFloat = new DevExpress.Utils.PointFloat(0F, 130F);
            this.line3.Name = "line3";
            this.line3.SizeF = new System.Drawing.SizeF(295F, 10F);
            // 
            // lblThanks
            // 
            this.lblThanks.Font = new System.Drawing.Font("Consolas", 10F, System.Drawing.FontStyle.Italic);
            this.lblThanks.LocationFloat = new DevExpress.Utils.PointFloat(0F, 150F);
            this.lblThanks.Name = "lblThanks";
            this.lblThanks.SizeF = new System.Drawing.SizeF(295F, 30F);
            this.lblThanks.Text = "Cảm ơn Quý Khách & Hẹn Gặp Lại!";
            this.lblThanks.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblTenMon
            // 
            this.lblTenMon.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[TenMon]")});
            this.lblTenMon.Font = new System.Drawing.Font("Consolas", 10F);
            this.lblTenMon.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.lblTenMon.Name = "lblTenMon";
            this.lblTenMon.SizeF = new System.Drawing.SizeF(150F, 25F);
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[SoLuong]")});
            this.lblSoLuong.Font = new System.Drawing.Font("Consolas", 10F);
            this.lblSoLuong.LocationFloat = new DevExpress.Utils.PointFloat(150F, 0F);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.SizeF = new System.Drawing.SizeF(45F, 25F);
            this.lblSoLuong.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // lblThanhTien
            // 
            this.lblThanhTien.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "FormatString(\'{0:#,##0} ₫\', [ThanhTien])")});
            this.lblThanhTien.Font = new System.Drawing.Font("Consolas", 10F);
            this.lblThanhTien.LocationFloat = new DevExpress.Utils.PointFloat(195F, 0F);
            this.lblThanhTien.Name = "lblThanhTien";
            this.lblThanhTien.SizeF = new System.Drawing.SizeF(100F, 25F);
            this.lblThanhTien.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleRight;
            // 
            // rptHoaDonK80
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.TopMargin,
            this.Detail,
            this.BottomMargin,
            this.ReportHeader,
            this.ReportFooter});
            this.Margins = new System.Drawing.Printing.Margins(10, 10, 10, 20);
            this.PageHeight = 1000;
            this.PageWidth = 315;
            this.PaperKind = System.Drawing.Printing.PaperKind.Custom;
            this.RollPaper = true;
            this.ShowPrintMarginsWarning = false;
            this.Version = "22.2";
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

        }

        #endregion

        private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
        private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
        private DevExpress.XtraReports.UI.DetailBand Detail;
        private DevExpress.XtraReports.UI.ReportHeaderBand ReportHeader;
        private DevExpress.XtraReports.UI.ReportFooterBand ReportFooter;
        private DevExpress.XtraReports.UI.XRLabel lblTitle;
        private DevExpress.XtraReports.UI.XRLabel lblSubTitle;
        public DevExpress.XtraReports.UI.XRLabel lblMaDon;
        public DevExpress.XtraReports.UI.XRLabel lblNgay;
        private DevExpress.XtraReports.UI.XRLine line1;
        private DevExpress.XtraReports.UI.XRLine line2;
        public DevExpress.XtraReports.UI.XRLabel lblL1;
        public DevExpress.XtraReports.UI.XRLabel lblTongTien;
        private DevExpress.XtraReports.UI.XRLabel lblL2;
        public DevExpress.XtraReports.UI.XRLabel lblTienThua;
        private DevExpress.XtraReports.UI.XRLine line3;
        private DevExpress.XtraReports.UI.XRLabel lblThanks;
        public DevExpress.XtraReports.UI.XRLabel lblTenMon;
        public DevExpress.XtraReports.UI.XRLabel lblSoLuong;
        public DevExpress.XtraReports.UI.XRLabel lblThanhTien;
    }
}
