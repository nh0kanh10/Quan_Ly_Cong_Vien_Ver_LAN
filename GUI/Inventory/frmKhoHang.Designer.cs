namespace GUI
{
    partial class frmKhoHang
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.pnlFilters = new Guna.UI2.WinForms.Guna2Panel();
            this.btnDongBo = new Guna.UI2.WinForms.Guna2Button();
            this.btnKeToanNhaCungCap = new Guna.UI2.WinForms.Guna2Button();
            this.cboKho = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblKho = new System.Windows.Forms.Label();
            this.pnlCards = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlCardSapHet = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSapHetTitle = new System.Windows.Forms.Label();
            this.lblSapHetValue = new System.Windows.Forms.Label();
            this.pnlCardTongVon = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTongVonTitle = new System.Windows.Forms.Label();
            this.lblTongVonValue = new System.Windows.Forms.Label();
            this.pnlCardCanhBao = new Guna.UI2.WinForms.Guna2Panel();
            this.lblCanhBaoTitle = new System.Windows.Forms.Label();
            this.lblCanhBaoValue = new System.Windows.Forms.Label();
            this.txtSearch = new Guna.UI2.WinForms.Guna2TextBox();
            this.gridTonKho = new DevExpress.XtraGrid.GridControl();
            this.gridViewTonKho = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.pnlActions = new Guna.UI2.WinForms.Guna2Panel();
            this.btnNhapKho = new Guna.UI2.WinForms.Guna2Button();
            this.btnXuatKho = new Guna.UI2.WinForms.Guna2Button();
            this.btnKiemKe = new Guna.UI2.WinForms.Guna2Button();
            this.pnlFilters.SuspendLayout();
            this.pnlCards.SuspendLayout();
            this.pnlCardSapHet.SuspendLayout();
            this.pnlCardTongVon.SuspendLayout();
            this.pnlCardCanhBao.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTonKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTonKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).BeginInit();
            this.splitContainerControl1.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlFilters
            // 
            this.pnlFilters.Controls.Add(this.btnDongBo);
            this.pnlFilters.Controls.Add(this.btnKeToanNhaCungCap);
            this.pnlFilters.Controls.Add(this.cboKho);
            this.pnlFilters.Controls.Add(this.lblKho);
            this.pnlFilters.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilters.FillColor = System.Drawing.Color.White;
            this.pnlFilters.Location = new System.Drawing.Point(0, 0);
            this.pnlFilters.Name = "pnlFilters";
            this.pnlFilters.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlFilters.Size = new System.Drawing.Size(1200, 60);
            this.pnlFilters.TabIndex = 2;
            // 
            // btnDongBo
            // 
            this.btnDongBo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDongBo.BorderRadius = 4;
            this.btnDongBo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(108)))), ((int)(((byte)(117)))), ((int)(((byte)(125)))));
            this.btnDongBo.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnDongBo.ForeColor = System.Drawing.Color.White;
            this.btnDongBo.Location = new System.Drawing.Point(994, 15);
            this.btnDongBo.Name = "btnDongBo";
            this.btnDongBo.Size = new System.Drawing.Size(160, 36);
            this.btnDongBo.TabIndex = 4;
            this.btnDongBo.Text = "Đồng bộ [F5]";
            this.btnDongBo.Click += new System.EventHandler(this.btnDongBo_Click);
            // 
            // btnKeToanNhaCungCap
            // 
            this.btnKeToanNhaCungCap.BorderRadius = 4;
            this.btnKeToanNhaCungCap.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnKeToanNhaCungCap.ForeColor = System.Drawing.Color.White;
            this.btnKeToanNhaCungCap.Location = new System.Drawing.Point(370, 15);
            this.btnKeToanNhaCungCap.Name = "btnKeToanNhaCungCap";
            this.btnKeToanNhaCungCap.Size = new System.Drawing.Size(180, 36);
            this.btnKeToanNhaCungCap.TabIndex = 0;
            this.btnKeToanNhaCungCap.Text = "DS Nhà Cung Cấp";
            this.btnKeToanNhaCungCap.Click += new System.EventHandler(this.btnKeToanNhaCungCap_Click);
            // 
            // cboKho
            // 
            this.cboKho.BackColor = System.Drawing.Color.Transparent;
            this.cboKho.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKho.FocusedColor = System.Drawing.Color.Empty;
            this.cboKho.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboKho.ItemHeight = 30;
            this.cboKho.Location = new System.Drawing.Point(100, 15);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(250, 36);
            this.cboKho.TabIndex = 1;
            this.cboKho.SelectedIndexChanged += new System.EventHandler(this.cboKho_SelectedIndexChanged);
            // 
            // lblKho
            // 
            this.lblKho.AutoSize = true;
            this.lblKho.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblKho.Location = new System.Drawing.Point(20, 23);
            this.lblKho.Name = "lblKho";
            this.lblKho.Size = new System.Drawing.Size(77, 19);
            this.lblKho.TabIndex = 2;
            this.lblKho.Text = "Chọn Kho:";
            // 
            // pnlCards
            // 
            this.pnlCards.BackColor = System.Drawing.Color.WhiteSmoke;
            this.pnlCards.Controls.Add(this.pnlCardSapHet);
            this.pnlCards.Controls.Add(this.pnlCardTongVon);
            this.pnlCards.Controls.Add(this.pnlCardCanhBao);
            this.pnlCards.Controls.Add(this.txtSearch);
            this.pnlCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCards.Location = new System.Drawing.Point(0, 60);
            this.pnlCards.Name = "pnlCards";
            this.pnlCards.Padding = new System.Windows.Forms.Padding(20, 10, 20, 10);
            this.pnlCards.Size = new System.Drawing.Size(1200, 150);
            this.pnlCards.TabIndex = 1;
            // 
            // pnlCardSapHet
            // 
            this.pnlCardSapHet.BorderRadius = 4;
            this.pnlCardSapHet.Controls.Add(this.lblSapHetTitle);
            this.pnlCardSapHet.Controls.Add(this.lblSapHetValue);
            this.pnlCardSapHet.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(100)))), ((int)(((byte)(4)))));
            this.pnlCardSapHet.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.pnlCardSapHet.FillColor = System.Drawing.Color.White;
            this.pnlCardSapHet.Location = new System.Drawing.Point(20, 10);
            this.pnlCardSapHet.Name = "pnlCardSapHet";
            this.pnlCardSapHet.Size = new System.Drawing.Size(370, 80);
            this.pnlCardSapHet.TabIndex = 0;
            // 
            // lblSapHetTitle
            // 
            this.lblSapHetTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSapHetTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblSapHetTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(100)))), ((int)(((byte)(4)))));
            this.lblSapHetTitle.Location = new System.Drawing.Point(15, 15);
            this.lblSapHetTitle.Name = "lblSapHetTitle";
            this.lblSapHetTitle.Size = new System.Drawing.Size(100, 23);
            this.lblSapHetTitle.TabIndex = 0;
            this.lblSapHetTitle.Text = "Hàng Sắp Hết";
            // 
            // lblSapHetValue
            // 
            this.lblSapHetValue.AutoSize = true;
            this.lblSapHetValue.BackColor = System.Drawing.Color.Transparent;
            this.lblSapHetValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblSapHetValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(133)))), ((int)(((byte)(100)))), ((int)(((byte)(4)))));
            this.lblSapHetValue.Location = new System.Drawing.Point(15, 40);
            this.lblSapHetValue.Name = "lblSapHetValue";
            this.lblSapHetValue.Size = new System.Drawing.Size(82, 32);
            this.lblSapHetValue.TabIndex = 1;
            this.lblSapHetValue.Text = "0 Loại";
            // 
            // pnlCardTongVon
            // 
            this.pnlCardTongVon.BorderRadius = 4;
            this.pnlCardTongVon.Controls.Add(this.lblTongVonTitle);
            this.pnlCardTongVon.Controls.Add(this.lblTongVonValue);
            this.pnlCardTongVon.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(133)))));
            this.pnlCardTongVon.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.pnlCardTongVon.FillColor = System.Drawing.Color.White;
            this.pnlCardTongVon.Location = new System.Drawing.Point(415, 10);
            this.pnlCardTongVon.Name = "pnlCardTongVon";
            this.pnlCardTongVon.Size = new System.Drawing.Size(370, 80);
            this.pnlCardTongVon.TabIndex = 1;
            // 
            // lblTongVonTitle
            // 
            this.lblTongVonTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTongVonTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblTongVonTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(133)))));
            this.lblTongVonTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTongVonTitle.Name = "lblTongVonTitle";
            this.lblTongVonTitle.Size = new System.Drawing.Size(100, 23);
            this.lblTongVonTitle.TabIndex = 0;
            this.lblTongVonTitle.Text = "Tổng Giá Trị Tồn";
            // 
            // lblTongVonValue
            // 
            this.lblTongVonValue.AutoSize = true;
            this.lblTongVonValue.BackColor = System.Drawing.Color.Transparent;
            this.lblTongVonValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTongVonValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(64)))), ((int)(((byte)(133)))));
            this.lblTongVonValue.Location = new System.Drawing.Point(15, 40);
            this.lblTongVonValue.Name = "lblTongVonValue";
            this.lblTongVonValue.Size = new System.Drawing.Size(51, 32);
            this.lblTongVonValue.TabIndex = 1;
            this.lblTongVonValue.Text = "0 ₫";
            // 
            // pnlCardCanhBao
            // 
            this.pnlCardCanhBao.BorderRadius = 4;
            this.pnlCardCanhBao.Controls.Add(this.lblCanhBaoTitle);
            this.pnlCardCanhBao.Controls.Add(this.lblCanhBaoValue);
            this.pnlCardCanhBao.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.pnlCardCanhBao.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.pnlCardCanhBao.FillColor = System.Drawing.Color.White;
            this.pnlCardCanhBao.Location = new System.Drawing.Point(810, 10);
            this.pnlCardCanhBao.Name = "pnlCardCanhBao";
            this.pnlCardCanhBao.Size = new System.Drawing.Size(370, 80);
            this.pnlCardCanhBao.TabIndex = 2;
            // 
            // lblCanhBaoTitle
            // 
            this.lblCanhBaoTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblCanhBaoTitle.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblCanhBaoTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.lblCanhBaoTitle.Location = new System.Drawing.Point(15, 15);
            this.lblCanhBaoTitle.Name = "lblCanhBaoTitle";
            this.lblCanhBaoTitle.Size = new System.Drawing.Size(100, 23);
            this.lblCanhBaoTitle.TabIndex = 0;
            this.lblCanhBaoTitle.Text = "Sản Phẩm Âm Kho";
            // 
            // lblCanhBaoValue
            // 
            this.lblCanhBaoValue.AutoSize = true;
            this.lblCanhBaoValue.BackColor = System.Drawing.Color.Transparent;
            this.lblCanhBaoValue.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblCanhBaoValue.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(114)))), ((int)(((byte)(28)))), ((int)(((byte)(36)))));
            this.lblCanhBaoValue.Location = new System.Drawing.Point(15, 40);
            this.lblCanhBaoValue.Name = "lblCanhBaoValue";
            this.lblCanhBaoValue.Size = new System.Drawing.Size(82, 32);
            this.lblCanhBaoValue.TabIndex = 1;
            this.lblCanhBaoValue.Text = "0 Loại";
            // 
            // txtSearch
            // 
            this.txtSearch.BorderRadius = 4;
            this.txtSearch.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSearch.DefaultText = "";
            this.txtSearch.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtSearch.ForeColor = System.Drawing.Color.Black;
            this.txtSearch.Location = new System.Drawing.Point(20, 100);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.PlaceholderText = "Quét Barcode hoặc nhập từ khóa để lọc sản phẩm...";
            this.txtSearch.SelectedText = "";
            this.txtSearch.Size = new System.Drawing.Size(1160, 36);
            this.txtSearch.TabIndex = 3;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // gridTonKho
            // 
            this.gridTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTonKho.Location = new System.Drawing.Point(0, 0);
            this.gridTonKho.MainView = this.gridViewTonKho;
            this.gridTonKho.Name = "gridTonKho";
            this.gridTonKho.Size = new System.Drawing.Size(1200, 260);
            this.gridTonKho.TabIndex = 0;
            this.gridTonKho.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTonKho});
            // 
            // gridViewTonKho
            // 
            this.gridViewTonKho.GridControl = this.gridTonKho;
            this.gridViewTonKho.Name = "gridViewTonKho";
            this.gridViewTonKho.OptionsBehavior.Editable = false;
            this.gridViewTonKho.OptionsFind.FindNullPrompt = "Quét Barcode hoặc nhập từ khóa...";
            this.gridViewTonKho.OptionsView.ShowGroupPanel = false;
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.CollapsePanel = DevExpress.XtraEditors.SplitCollapsePanel.Panel2;
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Horizontal = false;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 210);
            this.splitContainerControl1.Name = "splitContainerControl1";
            // 
            // splitContainerControl1.Panel1
            // 
            this.splitContainerControl1.Panel1.Controls.Add(this.gridTonKho);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            // 
            // splitContainerControl1.Panel2
            // 
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1200, 420);
            this.splitContainerControl1.SplitterPosition = 260;
            this.splitContainerControl1.TabIndex = 4;
            // 
            // pnlActions
            // 
            this.pnlActions.BorderColor = System.Drawing.Color.LightGray;
            this.pnlActions.BorderThickness = 1;
            this.pnlActions.Controls.Add(this.btnNhapKho);
            this.pnlActions.Controls.Add(this.btnXuatKho);
            this.pnlActions.Controls.Add(this.btnKiemKe);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.FillColor = System.Drawing.Color.White;
            this.pnlActions.Location = new System.Drawing.Point(0, 630);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(1200, 70);
            this.pnlActions.TabIndex = 3;
            // 
            // btnNhapKho
            // 
            this.btnNhapKho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNhapKho.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.btnNhapKho.BorderThickness = 1;
            this.btnNhapKho.FillColor = System.Drawing.Color.White;
            this.btnNhapKho.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnNhapKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnNhapKho.Location = new System.Drawing.Point(680, 13);
            this.btnNhapKho.Name = "btnNhapKho";
            this.btnNhapKho.Size = new System.Drawing.Size(160, 45);
            this.btnNhapKho.TabIndex = 0;
            this.btnNhapKho.Text = "[F2] Nhập Kho";
            this.btnNhapKho.Click += new System.EventHandler(this.btnNhapKho_Click);
            // 
            // btnXuatKho
            // 
            this.btnXuatKho.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnXuatKho.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(213)))), ((int)(((byte)(218)))), ((int)(((byte)(223)))));
            this.btnXuatKho.BorderThickness = 1;
            this.btnXuatKho.FillColor = System.Drawing.Color.White;
            this.btnXuatKho.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnXuatKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnXuatKho.Location = new System.Drawing.Point(850, 13);
            this.btnXuatKho.Name = "btnXuatKho";
            this.btnXuatKho.Size = new System.Drawing.Size(160, 45);
            this.btnXuatKho.TabIndex = 1;
            this.btnXuatKho.Text = "[F3] Xuất Kho";
            this.btnXuatKho.Click += new System.EventHandler(this.btnXuatKho_Click);
            // 
            // btnKiemKe
            // 
            this.btnKiemKe.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnKiemKe.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.btnKiemKe.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.btnKiemKe.ForeColor = System.Drawing.Color.White;
            this.btnKiemKe.Location = new System.Drawing.Point(1020, 13);
            this.btnKiemKe.Name = "btnKiemKe";
            this.btnKiemKe.Size = new System.Drawing.Size(160, 45);
            this.btnKiemKe.TabIndex = 2;
            this.btnKiemKe.Text = "[F4] Chốt Kiểm Kê";
            this.btnKiemKe.Click += new System.EventHandler(this.btnKiemKe_Click);
            // 
            // frmKhoHang
            // 
            this.ClientSize = new System.Drawing.Size(1200, 700);
            this.Controls.Add(this.splitContainerControl1);
            this.Controls.Add(this.pnlCards);
            this.Controls.Add(this.pnlFilters);
            this.Controls.Add(this.pnlActions);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "frmKhoHang";
            this.Text = "Quản Lý Kho Cockpit";
            this.Load += new System.EventHandler(this.frmKhoHang_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmKhoHang_KeyDown);
            this.pnlFilters.ResumeLayout(false);
            this.pnlFilters.PerformLayout();
            this.pnlCards.ResumeLayout(false);
            this.pnlCardSapHet.ResumeLayout(false);
            this.pnlCardSapHet.PerformLayout();
            this.pnlCardTongVon.ResumeLayout(false);
            this.pnlCardTongVon.PerformLayout();
            this.pnlCardCanhBao.ResumeLayout(false);
            this.pnlCardCanhBao.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTonKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTonKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel1)).EndInit();
            this.splitContainerControl1.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1.Panel2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            this.pnlActions.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlFilters;
        private System.Windows.Forms.Label lblKho;
        private Guna.UI2.WinForms.Guna2ComboBox cboKho;
        private Guna.UI2.WinForms.Guna2Button btnKeToanNhaCungCap;
        private Guna.UI2.WinForms.Guna2Button btnDongBo;

        private Guna.UI2.WinForms.Guna2Panel pnlCards;
        private Guna.UI2.WinForms.Guna2Panel pnlCardSapHet;
        private System.Windows.Forms.Label lblSapHetTitle;
        private System.Windows.Forms.Label lblSapHetValue;
        private Guna.UI2.WinForms.Guna2Panel pnlCardTongVon;
        private System.Windows.Forms.Label lblTongVonTitle;
        private System.Windows.Forms.Label lblTongVonValue;
        private Guna.UI2.WinForms.Guna2Panel pnlCardCanhBao;
        private System.Windows.Forms.Label lblCanhBaoTitle;
        private System.Windows.Forms.Label lblCanhBaoValue;

        private DevExpress.XtraGrid.GridControl gridTonKho;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTonKho;
        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;

        private Guna.UI2.WinForms.Guna2Panel pnlActions;
        private Guna.UI2.WinForms.Guna2Button btnNhapKho;
        private Guna.UI2.WinForms.Guna2Button btnXuatKho;
        private Guna.UI2.WinForms.Guna2Button btnKiemKe;
        private Guna.UI2.WinForms.Guna2TextBox txtSearch;
    }
}



