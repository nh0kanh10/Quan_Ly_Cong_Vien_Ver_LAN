namespace GUI
{
    partial class frmKiemKho
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
            this.btnHoanTat = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.btnResetAll = new Guna.UI2.WinForms.Guna2Button();
            this.lblStats = new System.Windows.Forms.Label();
            this.pnlFooter = new Guna.UI2.WinForms.Guna2Panel();
            this.txtBarcode = new Guna.UI2.WinForms.Guna2TextBox();
            this.chkBlindMode = new Guna.UI2.WinForms.Guna2CheckBox();
            this.lblNhanVien = new System.Windows.Forms.Label();
            this.lblNgay = new System.Windows.Forms.Label();
            this.cboKho = new Guna.UI2.WinForms.Guna2ComboBox();
            this.pnlTopControls = new Guna.UI2.WinForms.Guna2Panel();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridControl1 = new DevExpress.XtraGrid.GridControl();
            this.pnlFooter.SuspendLayout();
            this.pnlTopControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnHoanTat
            // 
            this.btnHoanTat.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHoanTat.BorderRadius = 4;
            this.btnHoanTat.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(74)))), ((int)(((byte)(137)))), ((int)(((byte)(115)))));
            this.btnHoanTat.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHoanTat.ForeColor = System.Drawing.Color.White;
            this.btnHoanTat.Location = new System.Drawing.Point(620, 12);
            this.btnHoanTat.Name = "btnHoanTat";
            this.btnHoanTat.Size = new System.Drawing.Size(180, 38);
            this.btnHoanTat.TabIndex = 1;
            this.btnHoanTat.Text = "HOÀN TẤT KIỂM KÊ";
            // 
            // btnHuy
            // 
            this.btnHuy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnHuy.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(203)))), ((int)(((byte)(213)))), ((int)(((byte)(225)))));
            this.btnHuy.BorderRadius = 4;
            this.btnHuy.BorderThickness = 1;
            this.btnHuy.FillColor = System.Drawing.Color.White;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnHuy.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.btnHuy.Location = new System.Drawing.Point(815, 12);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(80, 38);
            this.btnHuy.TabIndex = 2;
            this.btnHuy.Text = "ĐÓNG";
            // 
            // btnResetAll
            // 
            this.btnResetAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnResetAll.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnResetAll.BorderRadius = 4;
            this.btnResetAll.BorderThickness = 1;
            this.btnResetAll.FillColor = System.Drawing.Color.White;
            this.btnResetAll.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnResetAll.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(83)))), ((int)(((byte)(83)))));
            this.btnResetAll.Location = new System.Drawing.Point(908, 12);
            this.btnResetAll.Name = "btnResetAll";
            this.btnResetAll.Size = new System.Drawing.Size(75, 38);
            this.btnResetAll.TabIndex = 3;
            this.btnResetAll.Text = "RESET";
            // 
            // lblStats
            // 
            this.lblStats.AutoSize = true;
            this.lblStats.BackColor = System.Drawing.Color.Transparent;
            this.lblStats.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblStats.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblStats.Location = new System.Drawing.Point(20, 20);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(209, 15);
            this.lblStats.TabIndex = 0;
            this.lblStats.Text = "Tổng SP: 0 | Lệch tăng: 0 | Lệch giảm: 0";
            // 
            // pnlFooter
            // 
            this.pnlFooter.Controls.Add(this.lblStats);
            this.pnlFooter.Controls.Add(this.btnResetAll);
            this.pnlFooter.Controls.Add(this.btnHuy);
            this.pnlFooter.Controls.Add(this.btnHoanTat);
            this.pnlFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlFooter.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlFooter.Location = new System.Drawing.Point(0, 550);
            this.pnlFooter.Name = "pnlFooter";
            this.pnlFooter.Size = new System.Drawing.Size(1000, 60);
            this.pnlFooter.TabIndex = 2;
            // 
            // txtBarcode
            // 
            this.txtBarcode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBarcode.BorderRadius = 4;
            this.txtBarcode.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtBarcode.DefaultText = "";
            this.txtBarcode.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.txtBarcode.ForeColor = System.Drawing.Color.Black;
            this.txtBarcode.Location = new System.Drawing.Point(20, 60);
            this.txtBarcode.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.txtBarcode.Name = "txtBarcode";
            this.txtBarcode.PlaceholderText = "Quét mã vạch / Tìm tên sản phẩm...";
            this.txtBarcode.SelectedText = "";
            this.txtBarcode.Size = new System.Drawing.Size(960, 38);
            this.txtBarcode.TabIndex = 5;
            // 
            // chkBlindMode
            // 
            this.chkBlindMode.AutoSize = true;
            this.chkBlindMode.BackColor = System.Drawing.Color.Transparent;
            this.chkBlindMode.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.chkBlindMode.CheckedState.BorderRadius = 2;
            this.chkBlindMode.CheckedState.BorderThickness = 0;
            this.chkBlindMode.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.chkBlindMode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.chkBlindMode.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(71)))), ((int)(((byte)(85)))), ((int)(((byte)(105)))));
            this.chkBlindMode.Location = new System.Drawing.Point(800, 16);
            this.chkBlindMode.Name = "chkBlindMode";
            this.chkBlindMode.Size = new System.Drawing.Size(145, 19);
            this.chkBlindMode.TabIndex = 4;
            this.chkBlindMode.Text = "Ẩn tồn hệ thống (Mù)";
            this.chkBlindMode.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.chkBlindMode.UncheckedState.BorderRadius = 2;
            this.chkBlindMode.UncheckedState.BorderThickness = 2;
            this.chkBlindMode.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.chkBlindMode.UseVisualStyleBackColor = false;
            // 
            // lblNhanVien
            // 
            this.lblNhanVien.AutoSize = true;
            this.lblNhanVien.BackColor = System.Drawing.Color.Transparent;
            this.lblNhanVien.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNhanVien.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblNhanVien.Location = new System.Drawing.Point(620, 18);
            this.lblNhanVien.Name = "lblNhanVien";
            this.lblNhanVien.Size = new System.Drawing.Size(44, 15);
            this.lblNhanVien.TabIndex = 3;
            this.lblNhanVien.Text = "NV: ---";
            // 
            // lblNgay
            // 
            this.lblNgay.AutoSize = true;
            this.lblNgay.BackColor = System.Drawing.Color.Transparent;
            this.lblNgay.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblNgay.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblNgay.Location = new System.Drawing.Point(440, 18);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(91, 15);
            this.lblNgay.TabIndex = 2;
            this.lblNgay.Text = "Ngày: --/--/----";
            // 
            // cboKho
            // 
            this.cboKho.BackColor = System.Drawing.Color.Transparent;
            this.cboKho.BorderRadius = 4;
            this.cboKho.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboKho.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboKho.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.cboKho.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.cboKho.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboKho.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(51)))), ((int)(((byte)(65)))), ((int)(((byte)(85)))));
            this.cboKho.ItemHeight = 28;
            this.cboKho.Location = new System.Drawing.Point(200, 10);
            this.cboKho.Name = "cboKho";
            this.cboKho.Size = new System.Drawing.Size(220, 34);
            this.cboKho.TabIndex = 1;
            // 
            // pnlTopControls
            // 
            this.pnlTopControls.Controls.Add(this.cboKho);
            this.pnlTopControls.Controls.Add(this.lblNgay);
            this.pnlTopControls.Controls.Add(this.lblNhanVien);
            this.pnlTopControls.Controls.Add(this.chkBlindMode);
            this.pnlTopControls.Controls.Add(this.txtBarcode);
            this.pnlTopControls.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTopControls.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.pnlTopControls.Location = new System.Drawing.Point(0, 0);
            this.pnlTopControls.Name = "pnlTopControls";
            this.pnlTopControls.Size = new System.Drawing.Size(1000, 110);
            this.pnlTopControls.TabIndex = 0;
            // 
            // gridView1
            // 
            this.gridView1.GridControl = this.gridControl1;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // gridControl1
            // 
            this.gridControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gridControl1.Location = new System.Drawing.Point(20, 110);
            this.gridControl1.MainView = this.gridView1;
            this.gridControl1.Name = "gridControl1";
            this.gridControl1.Size = new System.Drawing.Size(960, 440);
            this.gridControl1.TabIndex = 1;
            this.gridControl1.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView1});
            // 
            // frmKiemKho
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1000, 610);
            this.Controls.Add(this.gridControl1);
            this.Controls.Add(this.pnlFooter);
            this.Controls.Add(this.pnlTopControls);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmKiemKho";
            this.Text = "Kiểm Kê Kho";
            this.pnlFooter.ResumeLayout(false);
            this.pnlFooter.PerformLayout();
            this.pnlTopControls.ResumeLayout(false);
            this.pnlTopControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Button btnHoanTat;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private Guna.UI2.WinForms.Guna2Button btnResetAll;
        private System.Windows.Forms.Label lblStats;
        private Guna.UI2.WinForms.Guna2Panel pnlFooter;
        private Guna.UI2.WinForms.Guna2TextBox txtBarcode;
        private Guna.UI2.WinForms.Guna2CheckBox chkBlindMode;
        private System.Windows.Forms.Label lblNhanVien;
        private System.Windows.Forms.Label lblNgay;
        private Guna.UI2.WinForms.Guna2ComboBox cboKho;
        private Guna.UI2.WinForms.Guna2Panel pnlTopControls;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.GridControl gridControl1;
    }
}

