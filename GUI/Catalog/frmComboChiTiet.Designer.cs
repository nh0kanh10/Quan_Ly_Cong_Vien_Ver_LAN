namespace GUI
{
    partial class frmComboChiTiet
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
            this.pnlTitle = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlCenter = new Guna.UI2.WinForms.Guna2Panel();
            this.gridData = new DevExpress.XtraGrid.GridControl();
            this.gridView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.pnlInput = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSanPham = new System.Windows.Forms.Label();
            this.cboSanPham = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblSoLuong = new System.Windows.Forms.Label();
            this.spnSoLuong = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.lblTyLe = new System.Windows.Forms.Label();
            this.spnTyLe = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.btnThem = new Guna.UI2.WinForms.Guna2Button();
            this.btnXoa = new Guna.UI2.WinForms.Guna2Button();
            this.pnlInfo = new Guna.UI2.WinForms.Guna2Panel();
            this.lblTongTyLe = new System.Windows.Forms.Label();
            
            this.pnlTitle.SuspendLayout();
            this.pnlCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).BeginInit();
            this.pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTyLe)).BeginInit();
            this.pnlInfo.SuspendLayout();
            this.SuspendLayout();

            // 
            // pnlTitle
            // 
            this.pnlTitle.Controls.Add(this.lblTitle);
            this.pnlTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlTitle.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(30)))), ((int)(((byte)(54)))));
            this.pnlTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlTitle.Name = "pnlTitle";
            this.pnlTitle.Size = new System.Drawing.Size(800, 50);
            this.pnlTitle.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(20, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(188, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "CẤU HÌNH CHI TIẾT COMBO";

            // 
            // pnlInfo
            // 
            this.pnlInfo.Controls.Add(this.lblTongTyLe);
            this.pnlInfo.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInfo.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(241)))), ((int)(((byte)(245)))), ((int)(((byte)(249)))));
            this.pnlInfo.Location = new System.Drawing.Point(0, 50);
            this.pnlInfo.Name = "pnlInfo";
            this.pnlInfo.Size = new System.Drawing.Size(800, 40);
            this.pnlInfo.TabIndex = 1;
            // 
            // lblTongTyLe
            // 
            this.lblTongTyLe.BackColor = System.Drawing.Color.Transparent;
            this.lblTongTyLe.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTongTyLe.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.lblTongTyLe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTongTyLe.Location = new System.Drawing.Point(0, 0);
            this.lblTongTyLe.Name = "lblTongTyLe";
            this.lblTongTyLe.Padding = new System.Windows.Forms.Padding(20, 0, 0, 0);
            this.lblTongTyLe.Size = new System.Drawing.Size(800, 40);
            this.lblTongTyLe.TabIndex = 0;
            this.lblTongTyLe.Text = "Tổng tỷ lệ phân bổ: 0% / 100%";
            this.lblTongTyLe.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlInput
            // 
            this.pnlInput.Controls.Add(this.lblSanPham);
            this.pnlInput.Controls.Add(this.cboSanPham);
            this.pnlInput.Controls.Add(this.lblSoLuong);
            this.pnlInput.Controls.Add(this.spnSoLuong);
            this.pnlInput.Controls.Add(this.lblTyLe);
            this.pnlInput.Controls.Add(this.spnTyLe);
            this.pnlInput.Controls.Add(this.btnThem);
            this.pnlInput.Controls.Add(this.btnXoa);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlInput.FillColor = System.Drawing.Color.White;
            this.pnlInput.Location = new System.Drawing.Point(0, 90);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(800, 70);
            this.pnlInput.TabIndex = 2;
            // 
            // lblSanPham
            // 
            this.lblSanPham.AutoSize = true;
            this.lblSanPham.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSanPham.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSanPham.Location = new System.Drawing.Point(20, 10);
            this.lblSanPham.Name = "lblSanPham";
            this.lblSanPham.Size = new System.Drawing.Size(63, 15);
            this.lblSanPham.TabIndex = 0;
            this.lblSanPham.Text = "Sản phẩm:";
            // 
            // cboSanPham
            // 
            this.cboSanPham.BackColor = System.Drawing.Color.Transparent;
            this.cboSanPham.BorderRadius = 5;
            this.cboSanPham.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboSanPham.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSanPham.FocusedColor = System.Drawing.Color.Empty;
            this.cboSanPham.Font = new System.Drawing.Font("Segoe UI", 9.5F);
            this.cboSanPham.ItemHeight = 28;
            this.cboSanPham.Location = new System.Drawing.Point(20, 27);
            this.cboSanPham.Name = "cboSanPham";
            this.cboSanPham.Size = new System.Drawing.Size(220, 34);
            this.cboSanPham.TabIndex = 1;
            // 
            // lblSoLuong
            // 
            this.lblSoLuong.AutoSize = true;
            this.lblSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSoLuong.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblSoLuong.Location = new System.Drawing.Point(260, 10);
            this.lblSoLuong.Name = "lblSoLuong";
            this.lblSoLuong.Size = new System.Drawing.Size(57, 15);
            this.lblSoLuong.TabIndex = 2;
            this.lblSoLuong.Text = "Số lượng:";
            // 
            // spnSoLuong
            // 
            this.spnSoLuong.BackColor = System.Drawing.Color.Transparent;
            this.spnSoLuong.BorderRadius = 5;
            this.spnSoLuong.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.spnSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.spnSoLuong.Location = new System.Drawing.Point(260, 27);
            this.spnSoLuong.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            this.spnSoLuong.Name = "spnSoLuong";
            this.spnSoLuong.Size = new System.Drawing.Size(100, 34);
            this.spnSoLuong.TabIndex = 3;
            this.spnSoLuong.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblTyLe
            // 
            this.lblTyLe.AutoSize = true;
            this.lblTyLe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblTyLe.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblTyLe.Location = new System.Drawing.Point(380, 10);
            this.lblTyLe.Name = "lblTyLe";
            this.lblTyLe.Size = new System.Drawing.Size(53, 15);
            this.lblTyLe.TabIndex = 4;
            this.lblTyLe.Text = "Tỷ lệ (%):";
            // 
            // spnTyLe
            // 
            this.spnTyLe.BackColor = System.Drawing.Color.Transparent;
            this.spnTyLe.BorderRadius = 5;
            this.spnTyLe.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.spnTyLe.DecimalPlaces = 2;
            this.spnTyLe.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.spnTyLe.Location = new System.Drawing.Point(380, 27);
            this.spnTyLe.Maximum = new decimal(new int[] { 100, 0, 0, 0 });
            this.spnTyLe.Name = "spnTyLe";
            this.spnTyLe.Size = new System.Drawing.Size(100, 34);
            this.spnTyLe.TabIndex = 5;
            this.spnTyLe.Value = new decimal(new int[] { 0, 0, 0, 0 });
            // 
            // btnThem
            // 
            this.btnThem.BorderRadius = 5;
            this.btnThem.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnThem.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThem.ForeColor = System.Drawing.Color.White;
            this.btnThem.Location = new System.Drawing.Point(520, 25);
            this.btnThem.Name = "btnThem";
            this.btnThem.Size = new System.Drawing.Size(120, 36);
            this.btnThem.TabIndex = 6;
            this.btnThem.Text = "Thêm chi tiết";
            this.btnThem.Click += new System.EventHandler(this.BtnThem_Click);
            // 
            // btnXoa
            // 
            this.btnXoa.BorderRadius = 5;
            this.btnXoa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnXoa.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnXoa.ForeColor = System.Drawing.Color.White;
            this.btnXoa.Location = new System.Drawing.Point(650, 25);
            this.btnXoa.Name = "btnXoa";
            this.btnXoa.Size = new System.Drawing.Size(120, 36);
            this.btnXoa.TabIndex = 7;
            this.btnXoa.Text = "Xóa chọn (Del)";
            this.btnXoa.Click += new System.EventHandler(this.BtnXoa_Click);
            // 
            // pnlCenter
            // 
            this.pnlCenter.Controls.Add(this.gridData);
            this.pnlCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCenter.Location = new System.Drawing.Point(0, 160);
            this.pnlCenter.Name = "pnlCenter";
            this.pnlCenter.Padding = new System.Windows.Forms.Padding(15);
            this.pnlCenter.Size = new System.Drawing.Size(800, 340);
            this.pnlCenter.TabIndex = 3;
            // 
            // gridData
            // 
            this.gridData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridData.MainView = this.gridView;
            this.gridData.Name = "gridData";
            this.gridData.Size = new System.Drawing.Size(770, 310);
            this.gridData.TabIndex = 0;
            this.gridData.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView});
            // 
            // gridView
            // 
            this.gridView.GridControl = this.gridData;
            this.gridView.Name = "gridView";
            this.gridView.OptionsBehavior.Editable = false;
            this.gridView.OptionsView.ShowGroupPanel = false;
            this.gridView.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gridView_KeyDown);
            // 
            // frmComboChiTiet
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(800, 500);
            this.Controls.Add(this.pnlCenter);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.pnlInfo);
            this.Controls.Add(this.pnlTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmComboChiTiet";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.pnlTitle.ResumeLayout(false);
            this.pnlTitle.PerformLayout();
            this.pnlCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridData)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView)).EndInit();
            this.pnlInput.ResumeLayout(false);
            this.pnlInput.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.spnSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.spnTyLe)).EndInit();
            this.pnlInfo.ResumeLayout(false);
            this.ResumeLayout(false);
        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlTitle;
        private System.Windows.Forms.Label lblTitle;
        private Guna.UI2.WinForms.Guna2Panel pnlInfo;
        private System.Windows.Forms.Label lblTongTyLe;
        private Guna.UI2.WinForms.Guna2Panel pnlInput;
        private System.Windows.Forms.Label lblSanPham;
        private Guna.UI2.WinForms.Guna2ComboBox cboSanPham;
        private System.Windows.Forms.Label lblSoLuong;
        private Guna.UI2.WinForms.Guna2NumericUpDown spnSoLuong;
        private System.Windows.Forms.Label lblTyLe;
        private Guna.UI2.WinForms.Guna2NumericUpDown spnTyLe;
        private Guna.UI2.WinForms.Guna2Button btnThem;
        private Guna.UI2.WinForms.Guna2Button btnXoa;
        private Guna.UI2.WinForms.Guna2Panel pnlCenter;
        private DevExpress.XtraGrid.GridControl gridData;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView;
    }
}
