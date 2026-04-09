namespace GUI.Inventory
{
    partial class frmTheKho
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
            this.panelTop = new System.Windows.Forms.Panel();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpDenNgay = new DevExpress.XtraEditors.DateEdit();
            this.label1 = new System.Windows.Forms.Label();
            this.dtpTuNgay = new DevExpress.XtraEditors.DateEdit();
            this.btnInBaoCao = new System.Windows.Forms.Button();
            this.gridTheKho = new DevExpress.XtraGrid.GridControl();
            this.gridViewTheKho = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.panelTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTheKho)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTheKho)).BeginInit();
            this.SuspendLayout();
            // 
            // panelTop
            // 
            this.panelTop.Controls.Add(this.btnTimKiem);
            this.panelTop.Controls.Add(this.label2);
            this.panelTop.Controls.Add(this.dtpDenNgay);
            this.panelTop.Controls.Add(this.label1);
            this.panelTop.Controls.Add(this.dtpTuNgay);
            this.panelTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelTop.Location = new System.Drawing.Point(0, 0);
            this.panelTop.Name = "panelTop";
            this.panelTop.Size = new System.Drawing.Size(1000, 71);
            this.panelTop.TabIndex = 0;
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Location = new System.Drawing.Point(410, 15);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(101, 30);
            this.btnTimKiem.TabIndex = 6;
            this.btnTimKiem.Text = "Tìm kiếm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(200, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 15);
            this.label2.TabIndex = 4;
            this.label2.Text = "Đến ngày:";
            // 
            // dtpDenNgay
            // 
            this.dtpDenNgay.EditValue = new System.DateTime(2026, 4, 4, 0, 0, 0, 0);
            this.dtpDenNgay.Location = new System.Drawing.Point(269, 12);
            this.dtpDenNgay.Name = "dtpDenNgay";
            this.dtpDenNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDenNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpDenNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtpDenNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpDenNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtpDenNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpDenNgay.Properties.MaskSettings.Set("mask", "dd/MM/yyyy");
            this.dtpDenNgay.Size = new System.Drawing.Size(120, 20);
            this.dtpDenNgay.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 15);
            this.label1.TabIndex = 2;
            this.label1.Text = "Từ ngày:";
            // 
            // dtpTuNgay
            // 
            this.dtpTuNgay.EditValue = new System.DateTime(2026, 4, 4, 0, 0, 0, 0);
            this.dtpTuNgay.Location = new System.Drawing.Point(72, 12);
            this.dtpTuNgay.Name = "dtpTuNgay";
            this.dtpTuNgay.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTuNgay.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpTuNgay.Properties.DisplayFormat.FormatString = "dd/MM/yyyy";
            this.dtpTuNgay.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpTuNgay.Properties.EditFormat.FormatString = "dd/MM/yyyy";
            this.dtpTuNgay.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpTuNgay.Properties.MaskSettings.Set("mask", "dd/MM/yyyy");
            this.dtpTuNgay.Size = new System.Drawing.Size(120, 20);
            this.dtpTuNgay.TabIndex = 3;
            // 
            // btnInBaoCao
            // 
            this.btnInBaoCao.Location = new System.Drawing.Point(490, 70);
            this.btnInBaoCao.Name = "btnInBaoCao";
            this.btnInBaoCao.Size = new System.Drawing.Size(120, 25);
            this.btnInBaoCao.TabIndex = 7;
            this.btnInBaoCao.Text = "In báo cáo (F10)";
            this.btnInBaoCao.UseVisualStyleBackColor = true;
            this.btnInBaoCao.Click += new System.EventHandler(this.btnInBaoCao_Click);
            // 
            // gridTheKho
            // 
            this.gridTheKho.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridTheKho.Location = new System.Drawing.Point(0, 71);
            this.gridTheKho.MainView = this.gridViewTheKho;
            this.gridTheKho.Name = "gridTheKho";
            this.gridTheKho.Size = new System.Drawing.Size(1000, 457);
            this.gridTheKho.TabIndex = 1;
            this.gridTheKho.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViewTheKho});
            // 
            // gridViewTheKho
            // 
            this.gridViewTheKho.GridControl = this.gridTheKho;
            this.gridViewTheKho.Name = "gridViewTheKho";
            this.gridViewTheKho.OptionsBehavior.Editable = false;
            this.gridViewTheKho.OptionsView.ShowGroupPanel = false;
            // 
            // frmTheKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1000, 528);
            this.Controls.Add(this.gridTheKho);
            this.Controls.Add(this.panelTop);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "frmTheKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Sổ Thẻ Kho Từng Mặt Hàng";
            this.Load += new System.EventHandler(this.frmTheKho_Load);
            this.panelTop.ResumeLayout(false);
            this.panelTop.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpDenNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpTuNgay.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridTheKho)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViewTheKho)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panelTop;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.Button btnInBaoCao;
        private DevExpress.XtraEditors.DateEdit dtpDenNgay;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit dtpTuNgay;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl gridTheKho;
        private DevExpress.XtraGrid.Views.Grid.GridView gridViewTheKho;
    }
}
