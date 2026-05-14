using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace GUI.Modules.BanHang
{
    partial class frmKetQuaThanhToan
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
            this.pnlTop = new DevExpress.XtraEditors.PanelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.lblMaDon = new DevExpress.XtraEditors.LabelControl();
            this.lblTienThuaText = new DevExpress.XtraEditors.LabelControl();
            this.lblTienThuaValue = new DevExpress.XtraEditors.LabelControl();
            this.pnlMid = new DevExpress.XtraEditors.PanelControl();
            this.gridVe = new DevExpress.XtraGrid.GridControl();
            this.viewVe = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTenVe = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaVach = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlBottom = new DevExpress.XtraEditors.PanelControl();
            this.btnInHoaDon = new DevExpress.XtraEditors.SimpleButton();
            this.btnInVe = new DevExpress.XtraEditors.SimpleButton();
            this.btnDong = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).BeginInit();
            this.pnlTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMid)).BeginInit();
            this.pnlMid.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridVe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewVe)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).BeginInit();
            this.pnlBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlTop
            // 
            this.pnlTop.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlTop.Controls.Add(this.lblTienThuaValue);
            this.pnlTop.Controls.Add(this.lblTienThuaText);
            this.pnlTop.Controls.Add(this.lblMaDon);
            this.pnlTop.Controls.Add(this.lblTitle);
            this.pnlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Location = new System.Drawing.Point(0, 0);
            this.pnlTop.Name = "pnlTop";
            this.pnlTop.Size = new System.Drawing.Size(480, 110);
            this.pnlTop.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI", 16F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.MediumSeaGreen;
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Appearance.Options.UseTextOptions = true;
            this.lblTitle.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblTitle.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblTitle.Location = new System.Drawing.Point(0, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(480, 30);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "THANH TOÁN THÀNH CÔNG";
            // 
            // lblMaDon
            // 
            this.lblMaDon.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblMaDon.Appearance.Options.UseFont = true;
            this.lblMaDon.Appearance.Options.UseTextOptions = true;
            this.lblMaDon.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.lblMaDon.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblMaDon.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblMaDon.Location = new System.Drawing.Point(0, 40);
            this.lblMaDon.Name = "lblMaDon";
            this.lblMaDon.Size = new System.Drawing.Size(480, 20);
            this.lblMaDon.TabIndex = 1;
            this.lblMaDon.Text = "Mã đơn: DH-123456";
            // 
            // lblTienThuaText
            // 
            this.lblTienThuaText.Appearance.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.lblTienThuaText.Appearance.Options.UseFont = true;
            this.lblTienThuaText.Location = new System.Drawing.Point(20, 75);
            this.lblTienThuaText.Name = "lblTienThuaText";
            this.lblTienThuaText.Size = new System.Drawing.Size(74, 21);
            this.lblTienThuaText.TabIndex = 2;
            this.lblTienThuaText.Text = "Tiền thừa:";
            // 
            // lblTienThuaValue
            // 
            this.lblTienThuaValue.Appearance.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold);
            this.lblTienThuaValue.Appearance.ForeColor = System.Drawing.Color.Crimson;
            this.lblTienThuaValue.Appearance.Options.UseFont = true;
            this.lblTienThuaValue.Appearance.Options.UseForeColor = true;
            this.lblTienThuaValue.Location = new System.Drawing.Point(100, 68);
            this.lblTienThuaValue.Name = "lblTienThuaValue";
            this.lblTienThuaValue.Size = new System.Drawing.Size(48, 32);
            this.lblTienThuaValue.TabIndex = 3;
            this.lblTienThuaValue.Text = "0 ₫";
            // 
            // pnlMid
            // 
            this.pnlMid.Controls.Add(this.gridVe);
            this.pnlMid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMid.Location = new System.Drawing.Point(0, 110);
            this.pnlMid.Name = "pnlMid";
            this.pnlMid.Padding = new System.Windows.Forms.Padding(10);
            this.pnlMid.Size = new System.Drawing.Size(480, 240);
            this.pnlMid.TabIndex = 1;
            // 
            // gridVe
            // 
            this.gridVe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridVe.Location = new System.Drawing.Point(12, 12);
            this.gridVe.MainView = this.viewVe;
            this.gridVe.Name = "gridVe";
            this.gridVe.Size = new System.Drawing.Size(456, 216);
            this.gridVe.TabIndex = 0;
            this.gridVe.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewVe});
            // 
            // viewVe
            // 
            this.viewVe.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTenVe,
            this.colMaVach});
            this.viewVe.GridControl = this.gridVe;
            this.viewVe.Name = "viewVe";
            this.viewVe.OptionsBehavior.Editable = false;
            this.viewVe.OptionsView.ShowGroupPanel = false;
            this.viewVe.OptionsView.ShowIndicator = false;
            // 
            // colTenVe
            // 
            this.colTenVe.Caption = "Tên loại vé";
            this.colTenVe.FieldName = "TenVe";
            this.colTenVe.Name = "colTenVe";
            this.colTenVe.Visible = true;
            this.colTenVe.VisibleIndex = 0;
            this.colTenVe.Width = 200;
            // 
            // colMaVach
            // 
            this.colMaVach.Caption = "Danh sách mã vạch Vé đã phát hành";
            this.colMaVach.FieldName = "MaVach";
            this.colMaVach.Name = "colMaVach";
            this.colMaVach.Visible = true;
            this.colMaVach.VisibleIndex = 1;
            // 
            // pnlBottom
            // 
            this.pnlBottom.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlBottom.Controls.Add(this.btnDong);
            this.pnlBottom.Controls.Add(this.btnInVe);
            this.pnlBottom.Controls.Add(this.btnInHoaDon);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Location = new System.Drawing.Point(0, 350);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(480, 60);
            this.pnlBottom.TabIndex = 2;
            // 
            // btnInHoaDon
            // 
            this.btnInHoaDon.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnInHoaDon.Appearance.Options.UseFont = true;
            this.btnInHoaDon.Location = new System.Drawing.Point(20, 10);
            this.btnInHoaDon.Name = "btnInHoaDon";
            this.btnInHoaDon.Size = new System.Drawing.Size(120, 40);
            this.btnInHoaDon.TabIndex = 0;
            this.btnInHoaDon.Text = "In Hóa Đơn";
            this.btnInHoaDon.Click += new System.EventHandler(this.BtnInHoaDon_Click);
            // 
            // btnInVe
            // 
            this.btnInVe.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnInVe.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnInVe.Appearance.BackColor = System.Drawing.Color.DodgerBlue;
            this.btnInVe.Appearance.Options.UseFont = true;
            this.btnInVe.Appearance.Options.UseForeColor = true;
            this.btnInVe.Appearance.Options.UseBackColor = true;
            this.btnInVe.Location = new System.Drawing.Point(150, 10);
            this.btnInVe.Name = "btnInVe";
            this.btnInVe.Size = new System.Drawing.Size(120, 40);
            this.btnInVe.TabIndex = 1;
            this.btnInVe.Text = "In Vé";
            this.btnInVe.Click += new System.EventHandler(this.BtnInVe_Click);
            // 
            // btnDong
            // 
            this.btnDong.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnDong.Appearance.Options.UseFont = true;
            this.btnDong.Location = new System.Drawing.Point(360, 10);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(100, 40);
            this.btnDong.TabIndex = 2;
            this.btnDong.Text = "Đóng (Enter)";
            this.btnDong.Click += new System.EventHandler(this.BtnDong_Click);
            // 
            // frmKetQuaThanhToan
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 410);
            this.Controls.Add(this.pnlMid);
            this.Controls.Add(this.pnlBottom);
            this.Controls.Add(this.pnlTop);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmKetQuaThanhToan";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Kết Quả Giao Dịch";
            ((System.ComponentModel.ISupportInitialize)(this.pnlTop)).EndInit();
            this.pnlTop.ResumeLayout(false);
            this.pnlTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlMid)).EndInit();
            this.pnlMid.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridVe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewVe)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlBottom)).EndInit();
            this.pnlBottom.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlTop;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.LabelControl lblMaDon;
        private DevExpress.XtraEditors.LabelControl lblTienThuaText;
        private DevExpress.XtraEditors.LabelControl lblTienThuaValue;

        private DevExpress.XtraEditors.PanelControl pnlMid;
        private DevExpress.XtraGrid.GridControl gridVe;
        private DevExpress.XtraGrid.Views.Grid.GridView viewVe;
        private DevExpress.XtraGrid.Columns.GridColumn colMaVach;
        private DevExpress.XtraGrid.Columns.GridColumn colTenVe;

        private DevExpress.XtraEditors.PanelControl pnlBottom;
        private DevExpress.XtraEditors.SimpleButton btnInHoaDon;
        private DevExpress.XtraEditors.SimpleButton btnInVe;
        private DevExpress.XtraEditors.SimpleButton btnDong;
    }
}
