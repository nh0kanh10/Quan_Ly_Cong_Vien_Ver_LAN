using System;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;

namespace GUI.Modules.Kho
{
    partial class ucCanhBao
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
            this.tabControl = new DevExpress.XtraTab.XtraTabControl();
            this.tabHSD = new DevExpress.XtraTab.XtraTabPage();
            this.gridHSD = new DevExpress.XtraGrid.GridControl();
            this.viewHSD = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.tabTon = new DevExpress.XtraTab.XtraTabPage();
            this.gridTon = new DevExpress.XtraGrid.GridControl();
            this.viewTon = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.lblSummaryHSD = new DevExpress.XtraEditors.LabelControl();
            this.lblSummaryTon = new DevExpress.XtraEditors.LabelControl();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).BeginInit();
            this.tabControl.SuspendLayout();
            this.tabHSD.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridHSD)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewHSD)).BeginInit();
            this.tabTon.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridTon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTon)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(0, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedTabPage = this.tabTon;
            this.tabControl.Size = new System.Drawing.Size(800, 600);
            this.tabControl.TabIndex = 0;
            this.tabControl.TabPages.AddRange(new DevExpress.XtraTab.XtraTabPage[] {
            this.tabHSD,
            this.tabTon});
            // 
            // tabHSD
            // 
            this.tabHSD.Controls.Add(this.gridHSD);
            this.tabHSD.Controls.Add(this.lblSummaryHSD);
            this.tabHSD.Name = "tabHSD";
            this.tabHSD.Size = new System.Drawing.Size(798, 575);
            this.tabHSD.Text = "HẠN SỬ DỤNG";
            // 
            // gridHSD
            // 
            this.gridHSD.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridHSD.Location = new System.Drawing.Point(0, 0);
            this.gridHSD.MainView = this.viewHSD;
            this.gridHSD.Name = "gridHSD";
            this.gridHSD.Size = new System.Drawing.Size(798, 575);
            this.gridHSD.TabIndex = 0;
            this.gridHSD.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewHSD});
            // 
            // viewHSD
            // 
            this.viewHSD.GridControl = this.gridHSD;
            this.viewHSD.Name = "viewHSD";
            this.viewHSD.OptionsBehavior.Editable = false;
            this.viewHSD.OptionsView.ShowGroupPanel = false;
            // 
            // tabTon
            // 
            this.tabTon.Controls.Add(this.gridTon);
            this.tabTon.Controls.Add(this.lblSummaryTon);
            this.tabTon.Name = "tabTon";
            this.tabTon.Size = new System.Drawing.Size(798, 575);
            this.tabTon.Text = "TỒN TỐI THIỂU";
            // 
            // gridTon
            // 
            this.gridTon.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTon.Location = new System.Drawing.Point(0, 0);
            this.gridTon.MainView = this.viewTon;
            this.gridTon.Name = "gridTon";
            this.gridTon.Size = new System.Drawing.Size(798, 575);
            this.gridTon.TabIndex = 0;
            this.gridTon.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.viewTon});
            // 
            // viewTon
            // 
            this.viewTon.GridControl = this.gridTon;
            this.viewTon.Name = "viewTon";
            this.viewTon.OptionsBehavior.Editable = false;
            this.viewTon.OptionsView.ShowGroupPanel = false;
            //
            // lblSummaryHSD
            //
            this.lblSummaryHSD.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblSummaryHSD.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(211)))), ((int)(((byte)(47)))), ((int)(((byte)(47)))));
            this.lblSummaryHSD.Appearance.Options.UseFont = true;
            this.lblSummaryHSD.Appearance.Options.UseForeColor = true;
            this.lblSummaryHSD.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSummaryHSD.Name = "lblSummaryHSD";
            this.lblSummaryHSD.Padding = new System.Windows.Forms.Padding(10, 8, 0, 8);
            this.lblSummaryHSD.Size = new System.Drawing.Size(200, 33);
            this.lblSummaryHSD.Text = "";
            //
            // lblSummaryTon
            //
            this.lblSummaryTon.Appearance.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold);
            this.lblSummaryTon.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(81)))), ((int)(((byte)(0)))));
            this.lblSummaryTon.Appearance.Options.UseFont = true;
            this.lblSummaryTon.Appearance.Options.UseForeColor = true;
            this.lblSummaryTon.Dock = System.Windows.Forms.DockStyle.Top;
            this.lblSummaryTon.Name = "lblSummaryTon";
            this.lblSummaryTon.Padding = new System.Windows.Forms.Padding(10, 8, 0, 8);
            this.lblSummaryTon.Size = new System.Drawing.Size(200, 33);
            this.lblSummaryTon.Text = "";
            // 
            // ucCanhBao
            // 
            this.Controls.Add(this.tabControl);
            this.Name = "ucCanhBao";
            this.Size = new System.Drawing.Size(800, 600);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl)).EndInit();
            this.tabControl.ResumeLayout(false);
            this.tabHSD.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridHSD)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewHSD)).EndInit();
            this.tabTon.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridTon)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.viewTon)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraTab.XtraTabControl tabControl;
        private DevExpress.XtraTab.XtraTabPage tabHSD;
        private DevExpress.XtraGrid.GridControl gridHSD;
        private DevExpress.XtraGrid.Views.Grid.GridView viewHSD;
        private DevExpress.XtraTab.XtraTabPage tabTon;
        private DevExpress.XtraGrid.GridControl gridTon;
        private DevExpress.XtraGrid.Views.Grid.GridView viewTon;
        private DevExpress.XtraEditors.LabelControl lblSummaryHSD;
        private DevExpress.XtraEditors.LabelControl lblSummaryTon;
        private System.ComponentModel.IContainer components = null;




    }}