using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace GUI.Modules.Kho
{
    partial class ucTonKho
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        /// 




        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlFilter = new System.Windows.Forms.FlowLayoutPanel();
            this.lblKho = new DevExpress.XtraEditors.LabelControl();
            this.slkKho = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.slkKhoView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblNhomSP = new DevExpress.XtraEditors.LabelControl();
            this.cboNhomSP = new DevExpress.XtraEditors.ComboBoxEdit();
            this.btnLoc = new DevExpress.XtraEditors.SimpleButton();
            this.btnExcel = new DevExpress.XtraEditors.SimpleButton();
            this.gridTonKho = new DevExpress.XtraGrid.GridControl();
            this.viewTonKho = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colMaHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTenHang = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDVT = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTonKho = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMucCanhBao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTrangThai = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slkKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhoView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNhomSP.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTonKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTonKho)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.lblKho);
            this.pnlFilter.Controls.Add(this.slkKho);
            this.pnlFilter.Controls.Add(this.lblNhomSP);
            this.pnlFilter.Controls.Add(this.cboNhomSP);
            this.pnlFilter.Controls.Add(this.btnLoc);
            this.pnlFilter.Controls.Add(this.btnExcel);
            this.pnlFilter.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlFilter.Location = new System.Drawing.Point(0, 0);
            this.pnlFilter.Name = "pnlFilter";
            this.pnlFilter.Padding = new System.Windows.Forms.Padding(10, 15, 10, 10);
            this.pnlFilter.Size = new System.Drawing.Size(800, 60);
            this.pnlFilter.TabIndex = 1;
            // 
            // lblKho
            // 
            this.lblKho.Location = new System.Drawing.Point(15, 22);
            this.lblKho.Margin = new System.Windows.Forms.Padding(5, 7, 5, 0);
            this.lblKho.Name = "lblKho";
            this.lblKho.Size = new System.Drawing.Size(22, 13);
            this.lblKho.TabIndex = 0;
            this.lblKho.Text = "Kho:";
            // 
            // slkKho
            // 
            this.slkKho.Location = new System.Drawing.Point(42, 19);
            this.slkKho.Margin = new System.Windows.Forms.Padding(0, 4, 15, 0);
            this.slkKho.Name = "slkKho";
            this.slkKho.Properties.PopupView = this.slkKhoView;
            this.slkKho.Size = new System.Drawing.Size(200, 20);
            this.slkKho.TabIndex = 1;
            // 
            // slkKhoView
            // 
            this.slkKhoView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.slkKhoView.Name = "slkKhoView";
            this.slkKhoView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.slkKhoView.OptionsView.ShowGroupPanel = false;
            // 
            // lblNhomSP
            // 
            this.lblNhomSP.Location = new System.Drawing.Point(262, 22);
            this.lblNhomSP.Margin = new System.Windows.Forms.Padding(5, 7, 5, 0);
            this.lblNhomSP.Name = "lblNhomSP";
            this.lblNhomSP.Size = new System.Drawing.Size(46, 13);
            this.lblNhomSP.TabIndex = 2;
            this.lblNhomSP.Text = "Nhóm SP:";
            // 
            // cboNhomSP
            // 
            this.cboNhomSP.Location = new System.Drawing.Point(313, 19);
            this.cboNhomSP.Margin = new System.Windows.Forms.Padding(0, 4, 15, 0);
            this.cboNhomSP.Name = "cboNhomSP";
            this.cboNhomSP.Size = new System.Drawing.Size(150, 20);
            this.cboNhomSP.TabIndex = 3;
            // 
            // btnLoc
            // 
            this.btnLoc.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(152)))), ((int)(((byte)(203)))));
            this.btnLoc.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnLoc.Appearance.Options.UseBackColor = true;
            this.btnLoc.Appearance.Options.UseForeColor = true;
            this.btnLoc.Location = new System.Drawing.Point(478, 18);
            this.btnLoc.Margin = new System.Windows.Forms.Padding(0, 3, 5, 0);
            this.btnLoc.Name = "btnLoc";
            this.btnLoc.Size = new System.Drawing.Size(80, 26);
            this.btnLoc.TabIndex = 4;
            this.btnLoc.Text = "LỌC";
            // 
            // btnExcel
            // 
            this.btnExcel.Location = new System.Drawing.Point(563, 18);
            this.btnExcel.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.btnExcel.Name = "btnExcel";
            this.btnExcel.Size = new System.Drawing.Size(100, 26);
            this.btnExcel.TabIndex = 5;
            this.btnExcel.Text = "Xuất Excel";
            // 
            // gridTonKho
            // 
            this.gridTonKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTonKho.Location = new System.Drawing.Point(0, 60);
            this.gridTonKho.MainView = this.viewTonKho;
            this.gridTonKho.Name = "gridTonKho";
            this.gridTonKho.Size = new System.Drawing.Size(800, 540);
            this.gridTonKho.TabIndex = 0;
            this.gridTonKho.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewTonKho});
            // 
            // viewTonKho
            // 
            this.viewTonKho.GridControl = this.gridTonKho;
            this.viewTonKho.Name = "viewTonKho";
            this.viewTonKho.OptionsBehavior.Editable = false;
            this.viewTonKho.OptionsView.ShowGroupPanel = false;
            this.viewTonKho.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colMaHang,
            this.colTenHang,
            this.colDVT,
            this.colTonKho,
            this.colMucCanhBao,
            this.colTrangThai});
            // 
            // colMaHang
            // 
            this.colMaHang.Caption = "Mã SP";
            this.colMaHang.FieldName = "MaSanPham";
            this.colMaHang.Name = "colMaHang";
            this.colMaHang.Visible = true;
            this.colMaHang.VisibleIndex = 0;
            // 
            // colTenHang
            // 
            this.colTenHang.Caption = "Tên Sản Phẩm";
            this.colTenHang.FieldName = "TenSanPham";
            this.colTenHang.Name = "colTenHang";
            this.colTenHang.Visible = true;
            this.colTenHang.VisibleIndex = 1;
            // 
            // colDVT
            // 
            this.colDVT.Caption = "ĐVT";
            this.colDVT.FieldName = "DVT";
            this.colDVT.Name = "colDVT";
            this.colDVT.Visible = true;
            this.colDVT.VisibleIndex = 2;
            // 
            // colTonKho
            // 
            this.colTonKho.Caption = "Tồn Kho";
            this.colTonKho.FieldName = "TonHienTai";
            this.colTonKho.Name = "colTonKho";
            this.colTonKho.Visible = true;
            this.colTonKho.VisibleIndex = 3;
            // 
            // colMucCanhBao
            // 
            this.colMucCanhBao.Caption = "Mức C.Báo";
            this.colMucCanhBao.FieldName = "MucCanhBao";
            this.colMucCanhBao.Name = "colMucCanhBao";
            this.colMucCanhBao.Visible = true;
            this.colMucCanhBao.VisibleIndex = 4;
            // 
            // colTrangThai
            // 
            this.colTrangThai.Caption = "Trạng Thái";
            this.colTrangThai.FieldName = "TrangThai";
            this.colTrangThai.Name = "colTrangThai";
            this.colTrangThai.Visible = true;
            this.colTrangThai.VisibleIndex = 5;
            // 
            // ucTonKho
            // 
            this.Controls.Add(this.gridTonKho);
            this.Controls.Add(this.pnlFilter);
            this.Name = "ucTonKho";
            this.Size = new System.Drawing.Size(800, 600);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slkKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhoView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboNhomSP.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTonKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTonKho)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel pnlFilter;
        private DevExpress.XtraEditors.LabelControl lblKho;
        private DevExpress.XtraEditors.SearchLookUpEdit slkKho;
        private DevExpress.XtraGrid.Views.Grid.GridView slkKhoView;
        private DevExpress.XtraEditors.LabelControl lblNhomSP;
        private DevExpress.XtraEditors.ComboBoxEdit cboNhomSP;
        private DevExpress.XtraEditors.SimpleButton btnLoc;
        private DevExpress.XtraEditors.SimpleButton btnExcel;
        private DevExpress.XtraGrid.GridControl gridTonKho;
        private DevExpress.XtraGrid.Views.Grid.GridView viewTonKho;
        private DevExpress.XtraGrid.Columns.GridColumn colMaHang;
        private DevExpress.XtraGrid.Columns.GridColumn colTenHang;
        private DevExpress.XtraGrid.Columns.GridColumn colDVT;
        private DevExpress.XtraGrid.Columns.GridColumn colTonKho;
        private DevExpress.XtraGrid.Columns.GridColumn colMucCanhBao;
        private DevExpress.XtraGrid.Columns.GridColumn colTrangThai;
        private System.ComponentModel.IContainer components = null;





    }}