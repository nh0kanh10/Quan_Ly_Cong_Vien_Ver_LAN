using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace GUI.Modules.Kho
{
    partial class ucLichSu
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
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }


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
            this.lblTuNgay = new DevExpress.XtraEditors.LabelControl();
            this.dtTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.lblDenNgay = new DevExpress.XtraEditors.LabelControl();
            this.dtDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.btnTimKiem = new DevExpress.XtraEditors.SimpleButton();
            this.gridLichSu = new DevExpress.XtraGrid.GridControl();
            this.viewLichSu = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlFilter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slkKho.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhoView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLichSu)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewLichSu)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFilter
            // 
            this.pnlFilter.Controls.Add(this.lblKho);
            this.pnlFilter.Controls.Add(this.slkKho);
            this.pnlFilter.Controls.Add(this.lblTuNgay);
            this.pnlFilter.Controls.Add(this.dtTuNgay);
            this.pnlFilter.Controls.Add(this.lblDenNgay);
            this.pnlFilter.Controls.Add(this.dtDenNgay);
            this.pnlFilter.Controls.Add(this.btnTimKiem);
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
            this.slkKho.Size = new System.Drawing.Size(180, 20);
            this.slkKho.TabIndex = 1;
            // 
            // slkKhoView
            // 
            this.slkKhoView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.slkKhoView.Name = "slkKhoView";
            this.slkKhoView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.slkKhoView.OptionsView.ShowGroupPanel = false;
            // 
            // lblTuNgay
            // 
            this.lblTuNgay.Location = new System.Drawing.Point(242, 22);
            this.lblTuNgay.Margin = new System.Windows.Forms.Padding(5, 7, 5, 0);
            this.lblTuNgay.Name = "lblTuNgay";
            this.lblTuNgay.Size = new System.Drawing.Size(17, 13);
            this.lblTuNgay.TabIndex = 2;
            this.lblTuNgay.Text = "Từ:";
            // 
            // dtTuNgay
            // 
            this.dtTuNgay.EditValue = new System.DateTime(2026, 4, 19, 0, 0, 0, 0);
            this.dtTuNgay.Location = new System.Drawing.Point(264, 19);
            this.dtTuNgay.Margin = new System.Windows.Forms.Padding(0, 4, 15, 0);
            this.dtTuNgay.Name = "dtTuNgay";
            this.dtTuNgay.Size = new System.Drawing.Size(120, 20);
            this.dtTuNgay.TabIndex = 3;
            // 
            // lblDenNgay
            // 
            this.lblDenNgay.Location = new System.Drawing.Point(404, 22);
            this.lblDenNgay.Margin = new System.Windows.Forms.Padding(5, 7, 5, 0);
            this.lblDenNgay.Name = "lblDenNgay";
            this.lblDenNgay.Size = new System.Drawing.Size(24, 13);
            this.lblDenNgay.TabIndex = 4;
            this.lblDenNgay.Text = "Đến:";
            // 
            // dtDenNgay
            // 
            this.dtDenNgay.EditValue = new System.DateTime(2026, 4, 19, 0, 0, 0, 0);
            this.dtDenNgay.Location = new System.Drawing.Point(433, 19);
            this.dtDenNgay.Margin = new System.Windows.Forms.Padding(0, 4, 15, 0);
            this.dtDenNgay.Name = "dtDenNgay";
            this.dtDenNgay.Size = new System.Drawing.Size(120, 20);
            this.dtDenNgay.TabIndex = 5;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(63)))), ((int)(((byte)(152)))), ((int)(((byte)(203)))));
            this.btnTimKiem.Appearance.ForeColor = System.Drawing.Color.White;
            this.btnTimKiem.Appearance.Options.UseBackColor = true;
            this.btnTimKiem.Appearance.Options.UseForeColor = true;
            this.btnTimKiem.Location = new System.Drawing.Point(568, 18);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(0, 3, 5, 0);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(100, 26);
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "TÌM KIẾM";
            this.btnTimKiem.Click += new System.EventHandler(this.BtnTimKiem_Click);
            // 
            // gridLichSu
            // 
            this.gridLichSu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridLichSu.Location = new System.Drawing.Point(0, 60);
            this.gridLichSu.MainView = this.viewLichSu;
            this.gridLichSu.Name = "gridLichSu";
            this.gridLichSu.Size = new System.Drawing.Size(800, 540);
            this.gridLichSu.TabIndex = 0;
            this.gridLichSu.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewLichSu});
            // 
            // viewLichSu
            // 
            this.viewLichSu.GridControl = this.gridLichSu;
            this.viewLichSu.Name = "viewLichSu";
            this.viewLichSu.OptionsBehavior.Editable = false;
            this.viewLichSu.OptionsView.ShowGroupPanel = false;
            // 
            // ucLichSu
            // 
            this.Controls.Add(this.gridLichSu);
            this.Controls.Add(this.pnlFilter);
            this.Name = "ucLichSu";
            this.Size = new System.Drawing.Size(800, 600);
            this.pnlFilter.ResumeLayout(false);
            this.pnlFilter.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.slkKho.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhoView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLichSu)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewLichSu)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.FlowLayoutPanel pnlFilter;
        private DevExpress.XtraEditors.LabelControl lblKho;
        private DevExpress.XtraEditors.SearchLookUpEdit slkKho;
        private DevExpress.XtraGrid.Views.Grid.GridView slkKhoView;
        private DevExpress.XtraEditors.LabelControl lblTuNgay;
        private DevExpress.XtraEditors.DateEdit dtTuNgay;
        private DevExpress.XtraEditors.LabelControl lblDenNgay;
        private DevExpress.XtraEditors.DateEdit dtDenNgay;
        private DevExpress.XtraEditors.SimpleButton btnTimKiem;
        private DevExpress.XtraGrid.GridControl gridLichSu;
        private DevExpress.XtraGrid.Views.Grid.GridView viewLichSu;
        private System.ComponentModel.IContainer components = null;





    }}