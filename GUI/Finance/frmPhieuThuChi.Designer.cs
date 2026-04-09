namespace GUI
{
    partial class frmPhieuThuChi
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
            this.pnlTop = new Guna.UI2.WinForms.Guna2Panel();
            this.pnlCardTonQuy = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTonQuyTitle = new System.Windows.Forms.Label();
            this.lblTonQuy = new System.Windows.Forms.Label();
            this.pnlCardTongChi = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTongChiTitle = new System.Windows.Forms.Label();
            this.lblTongChi = new System.Windows.Forms.Label();
            this.pnlCardTongThu = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTongThuTitle = new System.Windows.Forms.Label();
            this.lblTongThu = new System.Windows.Forms.Label();
            this.btnLoad = new Guna.UI2.WinForms.Guna2Button();
            this.dtpTo = new DevExpress.XtraEditors.DateEdit();
            this.lblTo = new System.Windows.Forms.Label();
            this.dtpFrom = new DevExpress.XtraEditors.DateEdit();
            this.lblFrom = new System.Windows.Forms.Label();
            this.cboKyBaoCao = new Guna.UI2.WinForms.Guna2ComboBox();
            this.pnlBody = new Guna.UI2.WinForms.Guna2Panel();
            this.splitMain = new DevExpress.XtraEditors.SplitContainerControl();
            this.gbThu = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridThu = new DevExpress.XtraGrid.GridControl();
            this.gridViewThu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gbChi = new Guna.UI2.WinForms.Guna2GroupBox();
            this.gridChi = new DevExpress.XtraGrid.GridControl();
            this.gridViewChi = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlTop.SuspendLayout();
            this.pnlCardTonQuy.SuspendLayout();
            this.pnlCardTongChi.SuspendLayout();
            this.pnlCardTongThu.SuspendLayout();
            this.pnlBody.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).BeginInit();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            this.gbThu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridThu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewThu)).BeginInit();
            this.gbChi.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridChi)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChi)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.Controls.Add(this.pnlCardTonQuy);
            this.pnlTop.Controls.Add(this.pnlCardTongChi);
            this.pnlTop.Controls.Add(this.pnlCardTongThu);
            this.pnlTop.Controls.Add(this.btnLoad);
            this.pnlTop.Controls.Add(this.dtpTo);
            this.pnlTop.Controls.Add(this.lblTo);
            this.pnlTop.Controls.Add(this.dtpFrom);
            this.pnlTop.Controls.Add(this.lblFrom);
            this.pnlTop.Controls.Add(this.cboKyBaoCao);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Padding = new System.Windows.Forms.Padding(15, 10, 15, 10);
            this.pnlTop.Size = new System.Drawing.Size(1300, 100);
            this.pnlTop.TabIndex = 0;
            // 
            // pnlCardTonQuy
            // 
            this.pnlCardTonQuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCardTonQuy.Controls.Add(this.lblTonQuyTitle);
            this.pnlCardTonQuy.Controls.Add(this.lblTonQuy);
            this.pnlCardTonQuy.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(41)))), ((int)(((byte)(128)))), ((int)(((byte)(185)))));
            this.pnlCardTonQuy.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.pnlCardTonQuy.Location = new System.Drawing.Point(1040, 10);
            this.pnlCardTonQuy.Name = "pnlCardTonQuy";
            this.pnlCardTonQuy.Size = new System.Drawing.Size(240, 80);
            this.pnlCardTonQuy.TabIndex = 9;
            // 
            // lblTonQuyTitle
            // 
            this.lblTonQuyTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTonQuyTitle.Name = "lblTonQuyTitle";
            this.lblTonQuyTitle.Size = new System.Drawing.Size(120, 23);
            this.lblTonQuyTitle.TabIndex = 0;
            this.lblTonQuyTitle.Text = "Tồn Quỹ Hiện Tại";
            // 
            // lblTonQuy
            // 
            this.lblTonQuy.AutoSize = true;
            this.lblTonQuy.Location = new System.Drawing.Point(15, 40);
            this.lblTonQuy.Name = "lblTonQuy";
            this.lblTonQuy.Size = new System.Drawing.Size(32, 30);
            this.lblTonQuy.TabIndex = 1;
            this.lblTonQuy.Text = "0 ";
            // 
            // pnlCardTongChi
            // 
            this.pnlCardTongChi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCardTongChi.Controls.Add(this.lblTongChiTitle);
            this.pnlCardTongChi.Controls.Add(this.lblTongChi);
            this.pnlCardTongChi.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.pnlCardTongChi.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.pnlCardTongChi.Location = new System.Drawing.Point(790, 10);
            this.pnlCardTongChi.Name = "pnlCardTongChi";
            this.pnlCardTongChi.Size = new System.Drawing.Size(240, 80);
            this.pnlCardTongChi.TabIndex = 8;
            // 
            // lblTongChiTitle
            // 
            this.lblTongChiTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTongChiTitle.Name = "lblTongChiTitle";
            this.lblTongChiTitle.Size = new System.Drawing.Size(120, 23);
            this.lblTongChiTitle.TabIndex = 0;
            this.lblTongChiTitle.Text = "Tổng Chi";
            // 
            // lblTongChi
            // 
            this.lblTongChi.AutoSize = true;
            this.lblTongChi.Location = new System.Drawing.Point(15, 40);
            this.lblTongChi.Name = "lblTongChi";
            this.lblTongChi.Size = new System.Drawing.Size(32, 30);
            this.lblTongChi.TabIndex = 1;
            this.lblTongChi.Text = "0 ";
            // 
            // pnlCardTongThu
            // 
            this.pnlCardTongThu.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlCardTongThu.Controls.Add(this.lblTongThuTitle);
            this.pnlCardTongThu.Controls.Add(this.lblTongThu);
            this.pnlCardTongThu.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.pnlCardTongThu.CustomBorderThickness = new System.Windows.Forms.Padding(0, 0, 0, 4);
            this.pnlCardTongThu.Location = new System.Drawing.Point(540, 10);
            this.pnlCardTongThu.Name = "pnlCardTongThu";
            this.pnlCardTongThu.Size = new System.Drawing.Size(240, 80);
            this.pnlCardTongThu.TabIndex = 7;
            // 
            // lblTongThuTitle
            // 
            this.lblTongThuTitle.Location = new System.Drawing.Point(15, 15);
            this.lblTongThuTitle.Name = "lblTongThuTitle";
            this.lblTongThuTitle.Size = new System.Drawing.Size(120, 23);
            this.lblTongThuTitle.TabIndex = 0;
            this.lblTongThuTitle.Text = "Tổng Thu";
            // 
            // lblTongThu
            // 
            this.lblTongThu.AutoSize = true;
            this.lblTongThu.Location = new System.Drawing.Point(15, 40);
            this.lblTongThu.Name = "lblTongThu";
            this.lblTongThu.Size = new System.Drawing.Size(32, 30);
            this.lblTongThu.TabIndex = 1;
            this.lblTongThu.Text = "0 ";
            // 
            // btnLoad
            // 
            this.btnLoad.Location = new System.Drawing.Point(505, 12);
            this.btnLoad.Name = "btnLoad";
            this.btnLoad.Size = new System.Drawing.Size(115, 36);
            this.btnLoad.TabIndex = 3;
            this.btnLoad.Text = "Lọc dữ liệu";
            this.btnLoad.Click += new System.EventHandler(this.btnLoad_Click);
            // 
            // dtpTo
            // 
            this.dtpTo.Location = new System.Drawing.Point(354, 12);
            this.dtpTo.Name = "dtpTo";
            this.dtpTo.Size = new System.Drawing.Size(140, 36);
            this.dtpTo.TabIndex = 2;
            this.dtpTo.DateTime = System.DateTime.Now;
            // 
            // lblTo
            // 
            this.lblTo.AutoSize = true;
            this.lblTo.Location = new System.Drawing.Point(315, 18);
            this.lblTo.Name = "lblTo";
            this.lblTo.Size = new System.Drawing.Size(37, 19);
            this.lblTo.TabIndex = 4;
            this.lblTo.Text = "Đến:";
            // 
            // dtpFrom
            // 
            this.dtpFrom.Location = new System.Drawing.Point(172, 12);
            this.dtpFrom.Name = "dtpFrom";
            this.dtpFrom.Size = new System.Drawing.Size(140, 36);
            this.dtpFrom.TabIndex = 1;
            this.dtpFrom.DateTime = System.DateTime.Now;
            // 
            // lblFrom
            // 
            this.lblFrom.AutoSize = true;
            this.lblFrom.Location = new System.Drawing.Point(135, 18);
            this.lblFrom.Name = "lblFrom";
            this.lblFrom.Size = new System.Drawing.Size(27, 19);
            this.lblFrom.TabIndex = 5;
            this.lblFrom.Text = "Từ:";
            // 
            // cboKyBaoCao
            // 
            this.cboKyBaoCao.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboKyBaoCao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKyBaoCao.ItemHeight = 30;
            this.cboKyBaoCao.Items.AddRange(new object[] {
            "Hôm nay",
            "Tuần này",
            "Tháng này",
            "Tháng trước",
            "Quý này",
            "Tùy chọn"});
            this.cboKyBaoCao.Location = new System.Drawing.Point(15, 12);
            this.cboKyBaoCao.Name = "cboKyBaoCao";
            this.cboKyBaoCao.Size = new System.Drawing.Size(120, 36);
            this.cboKyBaoCao.StartIndex = 2;
            this.cboKyBaoCao.TabIndex = 6;
            this.cboKyBaoCao.SelectedIndexChanged += new System.EventHandler(this.cboKyBaoCao_SelectedIndexChanged);
            // 
            // pnlBody
            // 
            this.pnlBody.Controls.Add(this.splitMain);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 100);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Size = new System.Drawing.Size(1300, 650);
            this.pnlBody.TabIndex = 1;
            // 
            // splitMain
            // 
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.Horizontal = false;
            this.splitMain.Location = new System.Drawing.Point(0, 0);
            this.splitMain.Name = "splitMain";
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.Controls.Add(this.gbThu);
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.Controls.Add(this.gbChi);
            this.splitMain.ShowSplitGlyph = DevExpress.Utils.DefaultBoolean.True;
            this.splitMain.Size = new System.Drawing.Size(1300, 650);
            this.splitMain.SplitterPosition = 340;
            this.splitMain.TabIndex = 0;
            // 
            // gbThu
            // 
            this.gbThu.Controls.Add(this.gridThu);
            this.gbThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbThu.Location = new System.Drawing.Point(0, 0);
            this.gbThu.Name = "gbThu";
            this.gbThu.Size = new System.Drawing.Size(1300, 340);
            this.gbThu.TabIndex = 0;
            this.gbThu.Text = "📥 Phiếu thu";
            // 
            // gridThu
            // 
            this.gridThu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridThu.Location = new System.Drawing.Point(0, 40);
            this.gridThu.MainView = this.gridViewThu;
            this.gridThu.Name = "gridThu";
            this.gridThu.Size = new System.Drawing.Size(1300, 300);
            this.gridThu.TabIndex = 0;
            this.gridThu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewThu});
            // 
            // gridViewThu
            // 
            this.gridViewThu.GridControl = this.gridThu;
            this.gridViewThu.Name = "gridViewThu";
            // 
            // gbChi
            // 
            this.gbChi.Controls.Add(this.gridChi);
            this.gbChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbChi.Location = new System.Drawing.Point(0, 0);
            this.gbChi.Name = "gbChi";
            this.gbChi.Size = new System.Drawing.Size(1300, 300);
            this.gbChi.TabIndex = 0;
            this.gbChi.Text = "📤 Phiếu chi";
            // 
            // gridChi
            // 
            this.gridChi.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridChi.Location = new System.Drawing.Point(0, 40);
            this.gridChi.MainView = this.gridViewChi;
            this.gridChi.Name = "gridChi";
            this.gridChi.Size = new System.Drawing.Size(1300, 260);
            this.gridChi.TabIndex = 0;
            this.gridChi.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewChi});
            // 
            // gridViewChi
            // 
            this.gridViewChi.GridControl = this.gridChi;
            this.gridViewChi.Name = "gridViewChi";
            // 
            // frmPhieuThuChi
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1300, 750);
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmPhieuThuChi";
            this.Text = "Quản Lý Thu Chi";
            this.Load += new System.EventHandler(this.frmPhieuThuChi_Load);
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            this.pnlCardTonQuy.ResumeLayout(false);
            this.pnlCardTonQuy.PerformLayout();
            this.pnlCardTongChi.ResumeLayout(false);
            this.pnlCardTongChi.PerformLayout();
            this.pnlCardTongThu.ResumeLayout(false);
            this.pnlCardTongThu.PerformLayout();
            this.pnlBody.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel1)).EndInit();
            this.splitMain.Panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain.Panel2)).EndInit();
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            this.gbThu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridThu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewThu)).EndInit();
            this.gbChi.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridChi)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewChi)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTop;
        private System.Windows.Forms.Label lblFrom;
        private DevExpress.XtraEditors.DateEdit dtpFrom;
        private System.Windows.Forms.Label lblTo;
        private DevExpress.XtraEditors.DateEdit dtpTo;
        private Guna.UI2.WinForms.Guna2Button btnLoad;
                private Guna.UI2.WinForms.Guna2Panel pnlCardTongThu;
        private System.Windows.Forms.Label lblTongThuTitle;
        private System.Windows.Forms.Label lblTongThu;
        private Guna.UI2.WinForms.Guna2Panel pnlCardTongChi;
        private System.Windows.Forms.Label lblTongChiTitle;
        private System.Windows.Forms.Label lblTongChi;
        private Guna.UI2.WinForms.Guna2Panel pnlCardTonQuy;
        private System.Windows.Forms.Label lblTonQuyTitle;
        private System.Windows.Forms.Label lblTonQuy;
        private Guna.UI2.WinForms.Guna2ComboBox cboKyBaoCao;
        private Guna.UI2.WinForms.Guna2Panel pnlBody;
        private DevExpress.XtraEditors.SplitContainerControl splitMain;
        private Guna.UI2.WinForms.Guna2GroupBox gbThu;
        private DevExpress.XtraGrid.GridControl gridThu;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewThu;
        private Guna.UI2.WinForms.Guna2GroupBox gbChi;
        private DevExpress.XtraGrid.GridControl gridChi;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewChi;
    }
}



