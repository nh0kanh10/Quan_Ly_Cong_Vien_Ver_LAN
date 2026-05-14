namespace GUI.Modules.Kho
{
    partial class frmKhoHang
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
                components.Dispose();
            base.Dispose(disposing);
        }


        #region Windows Form Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.pnlBanner = new DevExpress.XtraEditors.PanelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.pnlToolbar = new DevExpress.XtraEditors.PanelControl();
            this.btnLamMoi = new DevExpress.XtraEditors.SimpleButton();
            this.btnXoa = new DevExpress.XtraEditors.SimpleButton();
            this.btnThemMoi = new DevExpress.XtraEditors.SimpleButton();
            this.gridControl = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.txtTimKiem = new DevExpress.XtraEditors.SearchControl();
            this.popupMenu1 = new DevExpress.XtraBars.PopupMenu(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBanner)).BeginInit();
            this.pnlBanner.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolbar)).BeginInit();
            this.pnlToolbar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlBanner
            // 
            this.pnlBanner.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBanner.Controls.Add(this.lblTitle);
            this.pnlBanner.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlBanner.Location = new System.Drawing.Point(0, 0);
            this.pnlBanner.Name = "pnlBanner";
            this.pnlBanner.Size = new System.Drawing.Size(1000, 45);
            this.pnlBanner.TabIndex = 3;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(129, 13);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "QUẢN TRỊ DANH MỤC KHO";
            // 
            // pnlToolbar
            // 
            this.pnlToolbar.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlToolbar.Controls.Add(this.btnLamMoi);
            this.pnlToolbar.Controls.Add(this.btnXoa);
            this.pnlToolbar.Controls.Add(this.btnThemMoi);
            this.pnlToolbar.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlToolbar.Location = new System.Drawing.Point(0, 45);
            this.pnlToolbar.Name = "pnlToolbar";
            this.pnlToolbar.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.pnlToolbar.Size = new System.Drawing.Size(1000, 42);
            this.pnlToolbar.TabIndex = 2;
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.ImageOptions.ImageUri.Uri = "Refresh";
            this.btnLamMoi.Location = new System.Drawing.Point(229, 6);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(123, 30);
            this.btnLamMoi.TabIndex = 0;
            this.btnLamMoi.Text = "Nạp lại";
            this.btnLamMoi.Click += new System.EventHandler(this.btnLamMoi_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.ImageOptions.ImageUri.Uri = "Delete";
            this.btnXoa.Location = new System.Drawing.Point(129, 6);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(100, 30);
            this.btnXoa.TabIndex = 1;
            this.btnXoa.Text = "Xóa";
            this.btnXoa.Click += new System.EventHandler(this.btnXoa_Click);
            // 
            // btnThemMoi
            // 
            this.btnThemMoi.ImageOptions.ImageUri.Uri = "Add";
            this.btnThemMoi.Location = new System.Drawing.Point(8, 6);
            this.btnThemMoi.Name = "btnThemMoi";
            this.btnThemMoi.Size = new System.Drawing.Size(121, 30);
            this.btnThemMoi.TabIndex = 2;
            this.btnThemMoi.Text = "Thêm mới";
            this.btnThemMoi.Click += new System.EventHandler(this.btnThemMoi_Click);
            // 
            // gridControl
            // 
            this.gridControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl.Location = new System.Drawing.Point(0, 107);
            this.gridControl.MainView = this.gridView;
            this.gridControl.Name = "gridControl";
            this.gridControl.Size = new System.Drawing.Size(1000, 493);
            this.gridControl.TabIndex = 0;
            this.gridControl.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridControl;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            // 
            // txtTimKiem
            // 
            this.txtTimKiem.Client = this.gridControl;
            this.txtTimKiem.Dock = System.Windows.Forms.DockStyle.Top;
            this.txtTimKiem.Location = new System.Drawing.Point(0, 87);
            this.txtTimKiem.Name = "txtTimKiem";
            this.txtTimKiem.Properties.Client = this.gridControl;
            this.txtTimKiem.Properties.ShowClearButton = false;
            this.txtTimKiem.Properties.ShowSearchButton = false;
            this.txtTimKiem.Size = new System.Drawing.Size(1000, 20);
            this.txtTimKiem.TabIndex = 1;
            // 
            // popupMenu1
            // 
            this.popupMenu1.Name = "popupMenu1";
            // 
            // frmKhoHang
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gridControl);
            this.Controls.Add(this.txtTimKiem);
            this.Controls.Add(this.pnlToolbar);
            this.Controls.Add(this.pnlBanner);
            this.Name = "frmKhoHang";
            this.Size = new System.Drawing.Size(1000, 600);
            this.Load += new System.EventHandler(this.frmKhoHang_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pnlBanner)).EndInit();
            this.pnlBanner.ResumeLayout(false);
            this.pnlBanner.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlToolbar)).EndInit();
            this.pnlToolbar.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridControl)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtTimKiem.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.popupMenu1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.ComponentModel.IContainer components = null;
        private DevExpress.XtraEditors.PanelControl pnlBanner;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.PanelControl pnlToolbar;
        private DevExpress.XtraEditors.SimpleButton btnLamMoi;
        private DevExpress.XtraEditors.SimpleButton btnXoa;
        private DevExpress.XtraEditors.SimpleButton btnThemMoi;
        private DevExpress.XtraGrid.GridControl gridControl;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
        private DevExpress.XtraEditors.SearchControl txtTimKiem;
        private DevExpress.XtraBars.PopupMenu popupMenu1;





    }}