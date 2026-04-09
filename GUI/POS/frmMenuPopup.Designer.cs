namespace GUI
{
    partial class frmMenuPopup
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
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement1 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            DevExpress.XtraGrid.Views.Tile.TileViewItemElement tileViewItemElement2 = new DevExpress.XtraGrid.Views.Tile.TileViewItemElement();
            this.colTen = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colGia = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.txtTimMon = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            this.gridMenu = new DevExpress.XtraGrid.GridControl();
            this.tileViewMenu = new DevExpress.XtraGrid.Views.Tile.TileView();
            this.colId = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.colDonGia = new DevExpress.XtraGrid.Columns.TileViewColumn();
            this.pnlCart = new Guna.UI2.WinForms.Guna2Panel();
            this.gridCart = new DevExpress.XtraGrid.GridControl();
            this.gridViewCart = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.pnlBtnWrap = new System.Windows.Forms.Panel();
            this.btnXacNhan = new Guna.UI2.WinForms.Guna2Button();
            this.lblCartTitle = new System.Windows.Forms.Label();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMenu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileViewMenu)).BeginInit();
            this.pnlCart.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridCart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCart)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.pnlBtnWrap.SuspendLayout();
            this.SuspendLayout();
            // 
            // colTen
            // 
            this.colTen.FieldName = "Ten";
            this.colTen.Name = "colTen";
            this.colTen.Visible = true;
            this.colTen.VisibleIndex = 0;
            // 
            // colGia
            // 
            this.colGia.FieldName = "DonGiaText";
            this.colGia.Name = "colGia";
            this.colGia.Visible = true;
            this.colGia.VisibleIndex = 1;
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.txtTimMon);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(1200, 70);
            this.pnlHeader.TabIndex = 0;
            // 
            // txtTimMon
            // 
            this.txtTimMon.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtTimMon.BackColor = System.Drawing.Color.Transparent;
            this.txtTimMon.BorderRadius = 8;
            this.txtTimMon.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTimMon.DefaultText = "";
            this.txtTimMon.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.txtTimMon.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtTimMon.ForeColor = System.Drawing.Color.White;
            this.txtTimMon.Location = new System.Drawing.Point(280, 16);
            this.txtTimMon.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtTimMon.Name = "txtTimMon";
            this.txtTimMon.PlaceholderText = " Tìm món ăn... (gõ tên hoặc mã)";
            this.txtTimMon.SelectedText = "";
            this.txtTimMon.Size = new System.Drawing.Size(721, 38);
            this.txtTimMon.TabIndex = 1;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 18);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(184, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = " MENU MÓN ĂN";
            // 
            // gridMenu
            // 
            this.gridMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridMenu.Location = new System.Drawing.Point(0, 70);
            this.gridMenu.MainView = this.tileViewMenu;
            this.gridMenu.Name = "gridMenu";
            this.gridMenu.Size = new System.Drawing.Size(650, 430);
            this.gridMenu.TabIndex = 1;
            this.gridMenu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.tileViewMenu});
            // 
            // tileViewMenu
            // 
            this.tileViewMenu.Appearance.ItemHovered.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(252)))), ((int)(((byte)(231)))));
            this.tileViewMenu.Appearance.ItemHovered.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.tileViewMenu.Appearance.ItemNormal.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.tileViewMenu.Appearance.ItemNormal.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.tileViewMenu.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTen,
            this.colGia,
            this.colId,
            this.colDonGia});
            this.tileViewMenu.GridControl = this.gridMenu;
            this.tileViewMenu.Name = "tileViewMenu";
            this.tileViewMenu.OptionsTiles.ItemPadding = new System.Windows.Forms.Padding(12, 10, 12, 10);
            this.tileViewMenu.OptionsTiles.ItemSize = new System.Drawing.Size(240, 60);
            this.tileViewMenu.OptionsTiles.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.tileViewMenu.OptionsTiles.RowCount = 0;
            tileViewItemElement1.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            tileViewItemElement1.Appearance.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            tileViewItemElement1.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement1.Appearance.Normal.Options.UseForeColor = true;
            tileViewItemElement1.Column = this.colTen;
            tileViewItemElement1.Text = "colTen";
            tileViewItemElement1.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleLeft;
            tileViewItemElement2.Appearance.Normal.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            tileViewItemElement2.Appearance.Normal.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            tileViewItemElement2.Appearance.Normal.Options.UseFont = true;
            tileViewItemElement2.Appearance.Normal.Options.UseForeColor = true;
            tileViewItemElement2.Column = this.colGia;
            tileViewItemElement2.Text = "colGia";
            tileViewItemElement2.TextAlignment = DevExpress.XtraEditors.TileItemContentAlignment.MiddleRight;
            this.tileViewMenu.TileTemplate.Add(tileViewItemElement1);
            this.tileViewMenu.TileTemplate.Add(tileViewItemElement2);
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            this.colId.Visible = true;
            this.colId.VisibleIndex = 2;
            // 
            // colDonGia
            // 
            this.colDonGia.FieldName = "DonGia";
            this.colDonGia.Name = "colDonGia";
            this.colDonGia.Visible = true;
            this.colDonGia.VisibleIndex = 3;
            // 
            // pnlCart
            // 
            this.pnlCart.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlCart.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.pnlCart.BorderThickness = 1;
            this.pnlCart.Controls.Add(this.gridCart);
            this.pnlCart.Controls.Add(this.pnlBottom);
            this.pnlCart.Controls.Add(this.lblCartTitle);
            this.pnlCart.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnlCart.Location = new System.Drawing.Point(650, 70);
            this.pnlCart.Name = "pnlCart";
            this.pnlCart.Padding = new System.Windows.Forms.Padding(10);
            this.pnlCart.Size = new System.Drawing.Size(550, 430);
            this.pnlCart.TabIndex = 2;
            // 
            // gridCart
            // 
            this.gridCart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridCart.Location = new System.Drawing.Point(10, 47);
            this.gridCart.MainView = this.gridViewCart;
            this.gridCart.Name = "gridCart";
            this.gridCart.Size = new System.Drawing.Size(530, 313);
            this.gridCart.TabIndex = 2;
            this.gridCart.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewCart});
            // 
            // gridViewCart
            // 
            this.gridViewCart.GridControl = this.gridCart;
            this.gridViewCart.Name = "gridViewCart";
            this.gridViewCart.OptionsView.ShowGroupPanel = false;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.pnlBtnWrap);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(10, 360);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(530, 60);
            this.pnlBottom.TabIndex = 1;
            // 
            // pnlBtnWrap
            // 
            this.pnlBtnWrap.Controls.Add(this.btnXacNhan);
            this.pnlBtnWrap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBtnWrap.Location = new System.Drawing.Point(0, 0);
            this.pnlBtnWrap.Name = "pnlBtnWrap";
            this.pnlBtnWrap.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.pnlBtnWrap.Size = new System.Drawing.Size(530, 60);
            this.pnlBtnWrap.TabIndex = 0;
            // 
            // btnXacNhan
            // 
            this.btnXacNhan.BorderRadius = 8;
            this.btnXacNhan.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnXacNhan.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(2)))), ((int)(((byte)(136)))), ((int)(((byte)(209)))));
            this.btnXacNhan.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnXacNhan.ForeColor = System.Drawing.Color.White;
            this.btnXacNhan.Location = new System.Drawing.Point(0, 10);
            this.btnXacNhan.Name = "btnXacNhan";
            this.btnXacNhan.Size = new System.Drawing.Size(530, 50);
            this.btnXacNhan.TabIndex = 0;
            this.btnXacNhan.Text = "XÁC NHẬN CHỌN";
            // 
            // lblCartTitle
            // 
            this.lblCartTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblCartTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblCartTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.lblCartTitle.Location = new System.Drawing.Point(10, 10);
            this.lblCartTitle.Name = "lblCartTitle";
            this.lblCartTitle.Size = new System.Drawing.Size(530, 37);
            this.lblCartTitle.TabIndex = 0;
            this.lblCartTitle.Text = " CÁC MÓN ĐÃ CHỌN (0)";
            this.lblCartTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // frmMenuPopup
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1200, 500);
            this.Controls.Add(this.gridMenu);
            this.Controls.Add(this.pnlCart);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmMenuPopup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Menu Món Ăn";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridMenu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.tileViewMenu)).EndInit();
            this.pnlCart.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridCart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewCart)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.pnlBtnWrap.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2TextBox txtTimMon;
        private DevExpress.XtraGrid.GridControl gridMenu;
        private DevExpress.XtraGrid.Views.Tile.TileView tileViewMenu;
        private DevExpress.XtraGrid.Columns.TileViewColumn colTen;
        private DevExpress.XtraGrid.Columns.TileViewColumn colGia;
        private DevExpress.XtraGrid.Columns.TileViewColumn colId;
        private DevExpress.XtraGrid.Columns.TileViewColumn colDonGia;
        public Guna.UI2.WinForms.Guna2Panel pnlCart;
        public System.Windows.Forms.Label lblCartTitle;
        public System.Windows.Forms.Panel pnlBottom;
        public Guna.UI2.WinForms.Guna2Button btnXacNhan;
        public System.Windows.Forms.Panel pnlBtnWrap;
        public DevExpress.XtraGrid.GridControl gridCart;
        public DevExpress.XtraGrid.Views.Grid.GridView gridViewCart;

        
    }
}
