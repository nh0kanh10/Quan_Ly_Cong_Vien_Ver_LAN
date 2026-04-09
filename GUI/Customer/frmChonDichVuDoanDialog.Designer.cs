namespace GUI
{
    partial class frmChonDichVuDoanDialog
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
            this.btnOK = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.lblDV = new System.Windows.Forms.Label();
            this.lblSL = new System.Windows.Forms.Label();
            this.lblNgay = new System.Windows.Forms.Label();
            this.lblNote = new System.Windows.Forms.Label();
            this.txtGhiChu = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpNgaySuDung = new Guna.UI2.WinForms.Guna2DateTimePicker();
            this.spnSoLuong = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.slkDichVu = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.slkDichVuView = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.spnSoLuong)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkDichVu.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkDichVuView)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.BorderRadius = 6;
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(150, 238);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(110, 35);
            this.btnOK.TabIndex = 8;
            this.btnOK.Text = "Xác nhận";
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BorderRadius = 6;
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(114)))), ((int)(((byte)(128)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(270, 238);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 9;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // lblDV
            // 
            this.lblDV.AutoSize = true;
            this.lblDV.Location = new System.Drawing.Point(20, 63);
            this.lblDV.Name = "lblDV";
            this.lblDV.Size = new System.Drawing.Size(73, 13);
            this.lblDV.TabIndex = 0;
            this.lblDV.Text = "Chọn dịch vụ:";
            // 
            // lblSL
            // 
            this.lblSL.AutoSize = true;
            this.lblSL.Location = new System.Drawing.Point(20, 103);
            this.lblSL.Name = "lblSL";
            this.lblSL.Size = new System.Drawing.Size(52, 13);
            this.lblSL.TabIndex = 2;
            this.lblSL.Text = "Số lượng:";
            // 
            // lblNgay
            // 
            this.lblNgay.AutoSize = true;
            this.lblNgay.Location = new System.Drawing.Point(20, 143);
            this.lblNgay.Name = "lblNgay";
            this.lblNgay.Size = new System.Drawing.Size(76, 13);
            this.lblNgay.TabIndex = 4;
            this.lblNgay.Text = "Ngày sử dụng:";
            // 
            // lblNote
            // 
            this.lblNote.AutoSize = true;
            this.lblNote.Location = new System.Drawing.Point(20, 183);
            this.lblNote.Name = "lblNote";
            this.lblNote.Size = new System.Drawing.Size(47, 13);
            this.lblNote.TabIndex = 6;
            this.lblNote.Text = "Ghi chú:";
            // 
            // txtGhiChu
            // 
            this.txtGhiChu.BorderRadius = 4;
            this.txtGhiChu.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtGhiChu.DefaultText = "";
            this.txtGhiChu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtGhiChu.Location = new System.Drawing.Point(120, 178);
            this.txtGhiChu.Margin = new System.Windows.Forms.Padding(2);
            this.txtGhiChu.Multiline = true;
            this.txtGhiChu.Name = "txtGhiChu";
            this.txtGhiChu.PlaceholderText = "";
            this.txtGhiChu.SelectedText = "";
            this.txtGhiChu.Size = new System.Drawing.Size(250, 50);
            this.txtGhiChu.TabIndex = 7;
            // 
            // dtpNgaySuDung
            // 
            this.dtpNgaySuDung.BorderRadius = 4;
            this.dtpNgaySuDung.Checked = true;
            this.dtpNgaySuDung.CustomFormat = "dd/MM/yyyy";
            this.dtpNgaySuDung.FillColor = System.Drawing.Color.White;
            this.dtpNgaySuDung.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.dtpNgaySuDung.Format = System.Windows.Forms.DateTimePickerFormat.Custom;
            this.dtpNgaySuDung.Location = new System.Drawing.Point(120, 138);
            this.dtpNgaySuDung.MaxDate = new System.DateTime(9998, 12, 31, 0, 0, 0, 0);
            this.dtpNgaySuDung.MinDate = new System.DateTime(1753, 1, 1, 0, 0, 0, 0);
            this.dtpNgaySuDung.Name = "dtpNgaySuDung";
            this.dtpNgaySuDung.Size = new System.Drawing.Size(250, 30);
            this.dtpNgaySuDung.TabIndex = 5;
            this.dtpNgaySuDung.Value = new System.DateTime(2026, 4, 4, 0, 0, 0, 0);
            // 
            // spnSoLuong
            // 
            this.spnSoLuong.BackColor = System.Drawing.Color.Transparent;
            this.spnSoLuong.BorderRadius = 4;
            this.spnSoLuong.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.spnSoLuong.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.spnSoLuong.Location = new System.Drawing.Point(120, 98);
            this.spnSoLuong.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.spnSoLuong.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.spnSoLuong.Name = "spnSoLuong";
            this.spnSoLuong.Size = new System.Drawing.Size(250, 30);
            this.spnSoLuong.TabIndex = 3;
            this.spnSoLuong.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // slkDichVu
            // 
            this.slkDichVu.Location = new System.Drawing.Point(120, 58);
            this.slkDichVu.Name = "slkDichVu";
            this.slkDichVu.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slkDichVu.Properties.NullText = "[Chọn dịch vụ...]";
            this.slkDichVu.Properties.PopupView = this.slkDichVuView;
            this.slkDichVu.Size = new System.Drawing.Size(250, 20);
            this.slkDichVu.TabIndex = 1;
            // 
            // slkDichVuView
            // 
            this.slkDichVuView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.slkDichVuView.Name = "slkDichVuView";
            this.slkDichVuView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.slkDichVuView.OptionsView.ShowGroupPanel = false;
            // 
            // frmChonDichVuDoanDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(400, 310);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.txtGhiChu);
            this.Controls.Add(this.lblNote);
            this.Controls.Add(this.dtpNgaySuDung);
            this.Controls.Add(this.lblNgay);
            this.Controls.Add(this.spnSoLuong);
            this.Controls.Add(this.lblSL);
            this.Controls.Add(this.slkDichVu);
            this.Controls.Add(this.lblDV);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmChonDichVuDoanDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chọn Dịch Vụ";
            this.Load += new System.EventHandler(this.frmChonDichVuDoanDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmChonDichVuDoanDialog_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.spnSoLuong)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkDichVu.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkDichVuView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SearchLookUpEdit slkDichVu;
        private DevExpress.XtraGrid.Views.Grid.GridView slkDichVuView;
        private Guna.UI2.WinForms.Guna2NumericUpDown spnSoLuong;
        private Guna.UI2.WinForms.Guna2DateTimePicker dtpNgaySuDung;
        private Guna.UI2.WinForms.Guna2TextBox txtGhiChu;
        private Guna.UI2.WinForms.Guna2Button btnOK;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private System.Windows.Forms.Label lblDV;
        private System.Windows.Forms.Label lblSL;
        private System.Windows.Forms.Label lblNgay;
        private System.Windows.Forms.Label lblNote;
    }
}
