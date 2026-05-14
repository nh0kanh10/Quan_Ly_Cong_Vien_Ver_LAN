namespace GUI.Modules.BanHang
{
    partial class frmHoanHang
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>

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
            this.txtMaDonHang = new DevExpress.XtraEditors.TextEdit();
            this.btnTim = new DevExpress.XtraEditors.SimpleButton();
            this.lblDonHangInfo = new DevExpress.XtraEditors.LabelControl();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenSP = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuongMua = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDaHoan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGiaThucTe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuongMuonHoan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLyDo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnHoan = new DevExpress.XtraEditors.SimpleButton();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDonHang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMaDonHang
            // 
            this.txtMaDonHang.Location = new System.Drawing.Point(12, 12);
            this.txtMaDonHang.Name = "txtMaDonHang";
            this.txtMaDonHang.Properties.NullValuePrompt = "Mã Đơn Hàng cần hoàn...";
            this.txtMaDonHang.Size = new System.Drawing.Size(300, 24);
            this.txtMaDonHang.TabIndex = 0;
            // 
            // btnTim
            // 
            this.btnTim.Location = new System.Drawing.Point(320, 11);
            this.btnTim.Name = "btnTim";
            this.btnTim.Size = new System.Drawing.Size(75, 26);
            this.btnTim.TabIndex = 1;
            this.btnTim.Text = "Tìm Kiếm";
            this.btnTim.Click += new System.EventHandler(this.btnTim_Click);
            // 
            // lblDonHangInfo
            // 
            this.lblDonHangInfo.Appearance.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold);
            this.lblDonHangInfo.Appearance.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblDonHangInfo.Appearance.Options.UseFont = true;
            this.lblDonHangInfo.Appearance.Options.UseForeColor = true;
            this.lblDonHangInfo.Location = new System.Drawing.Point(12, 45);
            this.lblDonHangInfo.Name = "lblDonHangInfo";
            this.lblDonHangInfo.Size = new System.Drawing.Size(0, 14);
            this.lblDonHangInfo.TabIndex = 2;
            // 
            // gridControl
            // 
            this.gridControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl.Location = new System.Drawing.Point(12, 70);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(780, 480);
            this.gridControl.TabIndex = 3;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenSP,
            this.colLoai,
            this.colSoLuongMua,
            this.colDaHoan,
            this.colGiaThucTe,
            this.colSoLuongMuonHoan,
            this.colLyDo});
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsView.ShowGroupPanel = false;
            // 
            // colTenSP
            // 
            this.colTenSP.Caption = "Sản Phẩm";
            this.colTenSP.FieldName = "TenSanPham";
            this.colTenSP.Name = "colTenSP";
            this.colTenSP.OptionsColumn.AllowEdit = false;
            this.colTenSP.Visible = true;
            this.colTenSP.VisibleIndex = 0;
            this.colTenSP.Width = 150;
            // 
            // colLoai
            // 
            this.colLoai.Caption = "Loại";
            this.colLoai.FieldName = "LoaiSanPham";
            this.colLoai.Name = "colLoai";
            this.colLoai.OptionsColumn.AllowEdit = false;
            this.colLoai.Visible = true;
            this.colLoai.VisibleIndex = 1;
            this.colLoai.Width = 90;
            // 
            // colSoLuongMua
            // 
            this.colSoLuongMua.Caption = "Đã Mua";
            this.colSoLuongMua.FieldName = "SoLuongMua";
            this.colSoLuongMua.Name = "colSoLuongMua";
            this.colSoLuongMua.OptionsColumn.AllowEdit = false;
            this.colSoLuongMua.Visible = true;
            this.colSoLuongMua.VisibleIndex = 2;
            this.colSoLuongMua.Width = 80;
            // 
            // colDaHoan
            // 
            this.colDaHoan.Caption = "Đã Hoàn";
            this.colDaHoan.FieldName = "SoLuongDaHoan";
            this.colDaHoan.Name = "colDaHoan";
            this.colDaHoan.OptionsColumn.AllowEdit = false;
            this.colDaHoan.Visible = true;
            this.colDaHoan.VisibleIndex = 3;
            this.colDaHoan.Width = 80;
            // 
            // colGiaThucTe
            // 
            this.colGiaThucTe.Caption = "Đơn Giá Trả";
            this.colGiaThucTe.DisplayFormat.FormatString = "N0";
            this.colGiaThucTe.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGiaThucTe.FieldName = "DonGiaThucTe";
            this.colGiaThucTe.Name = "colGiaThucTe";
            this.colGiaThucTe.OptionsColumn.AllowEdit = false;
            this.colGiaThucTe.Visible = true;
            this.colGiaThucTe.VisibleIndex = 4;
            this.colGiaThucTe.Width = 120;
            // 
            // colSoLuongMuonHoan
            // 
            this.colSoLuongMuonHoan.Caption = "SL Muốn Hoàn";
            this.colSoLuongMuonHoan.FieldName = "SoLuongMuonHoan";
            this.colSoLuongMuonHoan.Name = "colSoLuongMuonHoan";
            this.colSoLuongMuonHoan.AppearanceCell.BackColor = System.Drawing.Color.Lavender;
            this.colSoLuongMuonHoan.Visible = true;
            this.colSoLuongMuonHoan.VisibleIndex = 5;
            this.colSoLuongMuonHoan.Width = 100;
            // 
            // colLyDo
            // 
            this.colLyDo.Caption = "Lý Do";
            this.colLyDo.FieldName = "LyDoHoan";
            this.colLyDo.Name = "colLyDo";
            this.colLyDo.AppearanceCell.BackColor = System.Drawing.Color.Lavender;
            this.colLyDo.Visible = true;
            this.colLyDo.VisibleIndex = 6;
            this.colLyDo.Width = 150;
            // 
            // btnHoan
            // 
            this.btnHoan.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHoan.Appearance.BackColor = System.Drawing.Color.Tomato;
            this.btnHoan.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.btnHoan.Appearance.Options.UseBackColor = true;
            this.btnHoan.Appearance.Options.UseFont = true;
            this.btnHoan.Location = new System.Drawing.Point(645, 560);
            this.btnHoan.Name = "btnHoan";
            this.btnHoan.Size = new System.Drawing.Size(147, 40);
            this.btnHoan.TabIndex = 4;
            this.btnHoan.Text = "XÁC NHẬN HOÀN";
            this.btnHoan.Click += new System.EventHandler(this.btnHoan_Click);
            // 
            // btnDong
            // 
            this.btnDong.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDong.Location = new System.Drawing.Point(540, 560);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(95, 40);
            this.btnDong.TabIndex = 5;
            this.btnDong.Text = "ĐÓNG";
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // frmHoanHang
            // 
            this.ClientSize = new System.Drawing.Size(804, 612);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnHoan);
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.lblDonHangInfo);
            this.Controls.Add(this.btnTim);
            this.Controls.Add(this.txtMaDonHang);
            this.Name = "frmHoanHang";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Nghiệp Vụ - Hoàn Trả Giao Dịch Bán Lẻ";
            ((System.ComponentModel.ISupportInitialize)(this.txtMaDonHang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraEditors.TextEdit txtMaDonHang;
        private DevExpress.XtraEditors.SimpleButton btnTim;
        private DevExpress.XtraEditors.LabelControl lblDonHangInfo;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraGrid.Columns.GridColumn colTenSP;
        private DevExpress.XtraGrid.Columns.GridColumn colLoai;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuongMua;
        private DevExpress.XtraGrid.Columns.GridColumn colDaHoan;
        private DevExpress.XtraGrid.Columns.GridColumn colGiaThucTe;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuongMuonHoan;
        private DevExpress.XtraGrid.Columns.GridColumn colLyDo;
        private DevExpress.XtraEditors.SimpleButton btnHoan;
        private DevExpress.XtraEditors.SimpleButton btnDong;
        private System.ComponentModel.IContainer components = null;





    }}