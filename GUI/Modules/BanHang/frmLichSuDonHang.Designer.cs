namespace GUI.Modules.BanHang
{
    partial class frmLichSuDonHang
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.gridLichSu = new DevExpress.XtraGrid.GridControl();
            this.viewLichSu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaDonHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNgayTao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTongThanhToan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colKhachHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNhanVien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuongMon = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.gridLichSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewLichSu)).BeginInit();
            this.SuspendLayout();
            // 
            // gridLichSu
            // 
            this.gridLichSu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLichSu.Location = new System.Drawing.Point(0, 0);
            this.gridLichSu.MainView = this.viewLichSu;
            this.gridLichSu.Name = "gridLichSu";
            this.gridLichSu.Size = new System.Drawing.Size(784, 461);
            this.gridLichSu.TabIndex = 0;
            this.gridLichSu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewLichSu});
            // 
            // viewLichSu
            // 
            this.viewLichSu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaDonHang,
            this.colNgayTao,
            this.colKhachHang,
            this.colNhanVien,
            this.colSoLuongMon,
            this.colTongThanhToan});
            this.viewLichSu.GridControl = this.gridLichSu;
            this.viewLichSu.Name = "viewLichSu";
            this.viewLichSu.OptionsBehavior.Editable = false;
            this.viewLichSu.OptionsView.ShowGroupPanel = false;
            this.viewLichSu.DoubleClick += new System.EventHandler(this.viewLichSu_DoubleClick);
            // 
            // colMaDonHang
            // 
            this.colMaDonHang.Caption = "Mã Đơn Hàng";
            this.colMaDonHang.FieldName = "MaDonHang";
            this.colMaDonHang.Name = "colMaDonHang";
            this.colMaDonHang.Visible = true;
            this.colMaDonHang.VisibleIndex = 0;
            // 
            // colNgayTao
            // 
            this.colNgayTao.Caption = "Ngày Tạo";
            this.colNgayTao.FieldName = "NgayTao";
            this.colNgayTao.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colNgayTao.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.colNgayTao.Name = "colNgayTao";
            this.colNgayTao.Visible = true;
            this.colNgayTao.VisibleIndex = 1;
            // 
            // colKhachHang
            // 
            this.colKhachHang.Caption = "Khách Hàng";
            this.colKhachHang.FieldName = "KhachHang";
            this.colKhachHang.Name = "colKhachHang";
            this.colKhachHang.Visible = true;
            this.colKhachHang.VisibleIndex = 2;
            // 
            // colNhanVien
            // 
            this.colNhanVien.Caption = "Thu Ngân";
            this.colNhanVien.FieldName = "NhanVien";
            this.colNhanVien.Name = "colNhanVien";
            this.colNhanVien.Visible = true;
            this.colNhanVien.VisibleIndex = 3;
            // 
            // colSoLuongMon
            // 
            this.colSoLuongMon.Caption = "SL Món";
            this.colSoLuongMon.FieldName = "SoLuongMon";
            this.colSoLuongMon.Name = "colSoLuongMon";
            this.colSoLuongMon.Visible = true;
            this.colSoLuongMon.VisibleIndex = 4;
            // 
            // colTongThanhToan
            // 
            this.colTongThanhToan.Caption = "Tổng Tiền";
            this.colTongThanhToan.FieldName = "TongThanhToan";
            this.colTongThanhToan.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colTongThanhToan.DisplayFormat.FormatString = "N0";
            this.colTongThanhToan.Name = "colTongThanhToan";
            this.colTongThanhToan.Visible = true;
            this.colTongThanhToan.VisibleIndex = 5;
            // 
            // frmLichSuDonHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.gridLichSu);
            this.Name = "frmLichSuDonHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lịch Sử Đơn Hàng (Phiên Hiện Tại)";
            this.Load += new System.EventHandler(this.frmLichSuDonHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gridLichSu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewLichSu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl gridLichSu;
        private DevExpress.XtraGrid.Views.Grid.GridView viewLichSu;
        private DevExpress.XtraGrid.Columns.GridColumn colMaDonHang;
        private DevExpress.XtraGrid.Columns.GridColumn colNgayTao;
        private DevExpress.XtraGrid.Columns.GridColumn colTongThanhToan;
        private DevExpress.XtraGrid.Columns.GridColumn colKhachHang;
        private DevExpress.XtraGrid.Columns.GridColumn colNhanVien;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuongMon;
    }
}
