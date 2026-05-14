namespace GUI.Modules.BanHang
{
    partial class frmMinibar
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
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gridSanPham = new DevExpress.XtraGrid.GridControl();
            this.gridViewSanPham = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenSanPham = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGiaBan = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSoLuong = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colThanhTien = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnXacNhan = new DevExpress.XtraEditors.SimpleButton();
            this.btnHuy = new DevExpress.XtraEditors.SimpleButton();
            this.lblTongCong = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciGrid = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnXacNhan = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBtnHuy = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciTongCong = new DevExpress.XtraLayout.LayoutControlItem();
            this.repositoryItemSpinEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSanPham)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciGrid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnXacNhan)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnHuy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTongCong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gridSanPham);
            this.layoutControl1.Controls.Add(this.btnXacNhan);
            this.layoutControl1.Controls.Add(this.btnHuy);
            this.layoutControl1.Controls.Add(this.lblTongCong);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(550, 480);
            this.layoutControl1.TabIndex = 0;
            // 
            // gridSanPham
            // 
            this.gridSanPham.Location = new System.Drawing.Point(12, 12);
            this.gridSanPham.MainView = this.gridViewSanPham;
            this.gridSanPham.Name = "gridSanPham";
            this.gridSanPham.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repositoryItemSpinEdit1});
            this.gridSanPham.Size = new System.Drawing.Size(526, 390);
            this.gridSanPham.TabIndex = 0;
            this.gridSanPham.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewSanPham});
            // 
            // gridViewSanPham
            // 
            this.gridViewSanPham.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenSanPham,
            this.colGiaBan,
            this.colSoLuong,
            this.colThanhTien});
            this.gridViewSanPham.GridControl = this.gridSanPham;
            this.gridViewSanPham.Name = "gridViewSanPham";
            this.gridViewSanPham.OptionsView.ShowGroupPanel = false;
            this.gridViewSanPham.OptionsView.ShowIndicator = false;
            this.gridViewSanPham.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.GridViewSanPham_CellValueChanged);
            // 
            // colTenSanPham
            // 
            this.colTenSanPham.Caption = "Sản phẩm";
            this.colTenSanPham.FieldName = "TenSanPham";
            this.colTenSanPham.Name = "colTenSanPham";
            this.colTenSanPham.OptionsColumn.AllowEdit = false;
            this.colTenSanPham.Visible = true;
            this.colTenSanPham.VisibleIndex = 0;
            this.colTenSanPham.Width = 200;
            // 
            // colGiaBan
            // 
            this.colGiaBan.Caption = "Đơn giá";
            this.colGiaBan.DisplayFormat.FormatString = "N0";
            this.colGiaBan.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colGiaBan.FieldName = "GiaBan";
            this.colGiaBan.Name = "colGiaBan";
            this.colGiaBan.OptionsColumn.AllowEdit = false;
            this.colGiaBan.Visible = true;
            this.colGiaBan.VisibleIndex = 1;
            this.colGiaBan.Width = 100;
            // 
            // colSoLuong
            // 
            this.colSoLuong.Caption = "SL";
            this.colSoLuong.ColumnEdit = this.repositoryItemSpinEdit1;
            this.colSoLuong.FieldName = "SoLuongChon";
            this.colSoLuong.Name = "colSoLuong";
            this.colSoLuong.Visible = true;
            this.colSoLuong.VisibleIndex = 2;
            this.colSoLuong.Width = 60;
            // 
            // colThanhTien
            // 
            this.colThanhTien.Caption = "Thành tiền";
            this.colThanhTien.DisplayFormat.FormatString = "N0";
            this.colThanhTien.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colThanhTien.FieldName = "ThanhTien";
            this.colThanhTien.Name = "colThanhTien";
            this.colThanhTien.OptionsColumn.AllowEdit = false;
            this.colThanhTien.Visible = true;
            this.colThanhTien.VisibleIndex = 3;
            this.colThanhTien.Width = 120;
            // 
            // repositoryItemSpinEdit1
            // 
            this.repositoryItemSpinEdit1.AutoHeight = false;
            this.repositoryItemSpinEdit1.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repositoryItemSpinEdit1.IsFloatValue = false;
            this.repositoryItemSpinEdit1.MaxValue = new decimal(new int[] {999, 0, 0, 0});
            this.repositoryItemSpinEdit1.MinValue = new decimal(new int[] {0, 0, 0, 0});
            this.repositoryItemSpinEdit1.Name = "repositoryItemSpinEdit1";
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.ImageOptions.ImageUri.Uri = "Save";
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(260, 36);
            this.btnXacNhan.TabIndex = 1;
            this.btnXacNhan.Text = "Xác nhận";
            this.btnXacNhan.Click += new System.EventHandler(this.BtnXacNhan_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.ImageOptions.ImageUri.Uri = "Cancel";
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(260, 36);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.BtnHuy_Click);
            // 
            // lblTongCong
            // 
            this.lblTongCong.Appearance.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblTongCong.Appearance.Options.UseFont = true;
            this.lblTongCong.Name = "lblTongCong";
            this.lblTongCong.Size = new System.Drawing.Size(526, 20);
            this.lblTongCong.StyleController = this.layoutControl1;
            this.lblTongCong.Text = "0đ";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciGrid,
            this.lciTongCong,
            this.lciBtnXacNhan,
            this.lciBtnHuy});
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Size = new System.Drawing.Size(550, 480);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lciGrid
            // 
            this.lciGrid.Control = this.gridSanPham;
            this.lciGrid.Location = new System.Drawing.Point(0, 0);
            this.lciGrid.Name = "lciGrid";
            this.lciGrid.Size = new System.Drawing.Size(530, 394);
            this.lciGrid.TextSize = new System.Drawing.Size(0, 0);
            this.lciGrid.TextVisible = false;
            // 
            // lciTongCong
            // 
            this.lciTongCong.Control = this.lblTongCong;
            this.lciTongCong.Location = new System.Drawing.Point(0, 394);
            this.lciTongCong.Name = "lciTongCong";
            this.lciTongCong.Size = new System.Drawing.Size(530, 26);
            this.lciTongCong.Text = "Tổng cộng:";
            this.lciTongCong.TextSize = new System.Drawing.Size(80, 17);
            // 
            // lciBtnXacNhan
            // 
            this.lciBtnXacNhan.Control = this.btnXacNhan;
            this.lciBtnXacNhan.Location = new System.Drawing.Point(0, 420);
            this.lciBtnXacNhan.Name = "lciBtnXacNhan";
            this.lciBtnXacNhan.Size = new System.Drawing.Size(265, 40);
            this.lciBtnXacNhan.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnXacNhan.TextVisible = false;
            // 
            // lciBtnHuy
            // 
            this.lciBtnHuy.Control = this.btnHuy;
            this.lciBtnHuy.Location = new System.Drawing.Point(265, 420);
            this.lciBtnHuy.Name = "lciBtnHuy";
            this.lciBtnHuy.Size = new System.Drawing.Size(265, 40);
            this.lciBtnHuy.TextSize = new System.Drawing.Size(0, 0);
            this.lciBtnHuy.TextVisible = false;
            // 
            // frmMinibar
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(550, 480);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMinibar";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Minibar - Đồ ăn & Thức uống";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewSanPham)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciGrid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnXacNhan)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBtnHuy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciTongCong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemSpinEdit1)).EndInit();
            this.ResumeLayout(false);
        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gridSanPham;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colTenSanPham;
        private DevExpress.XtraGrid.Columns.GridColumn colGiaBan;
        private DevExpress.XtraGrid.Columns.GridColumn colSoLuong;
        private DevExpress.XtraGrid.Columns.GridColumn colThanhTien;
        private DevExpress.XtraEditors.SimpleButton btnXacNhan;
        private DevExpress.XtraEditors.SimpleButton btnHuy;
        private DevExpress.XtraEditors.LabelControl lblTongCong;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lciGrid;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnXacNhan;
        private DevExpress.XtraLayout.LayoutControlItem lciBtnHuy;
        private DevExpress.XtraLayout.LayoutControlItem lciTongCong;
        private DevExpress.XtraEditors.Repository.RepositoryItemSpinEdit repositoryItemSpinEdit1;
    }
}
