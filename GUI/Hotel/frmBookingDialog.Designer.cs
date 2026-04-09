namespace GUI
{
    partial class frmBookingDialog
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

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.slkKhachHang = new DevExpress.XtraEditors.SearchLookUpEdit();
            this.slkKhachHangView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.label2 = new System.Windows.Forms.Label();
            this.dtpNgayTra = new DevExpress.XtraEditors.DateEdit();
            this.label3 = new System.Windows.Forms.Label();
            this.txtSoTien = new Guna.UI2.WinForms.Guna2NumericUpDown();
            this.rbTienMat = new Guna.UI2.WinForms.Guna2RadioButton();
            this.rbRFID = new Guna.UI2.WinForms.Guna2RadioButton();
            this.btnCheckIn = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.lblConstraint = new System.Windows.Forms.Label();
            this.btnThemKhachNhanh = new Guna.UI2.WinForms.Guna2Button();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayTra.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayTra.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTien)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhachHang.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhachHangView)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 57);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(153, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Check-in Phòng";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(23, 113);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(134, 19);
            this.label1.TabIndex = 1;
            this.label1.Text = "Khách hàng (nếu có)";
            // 
            // slkKhachHang
            // 
            this.slkKhachHang.Location = new System.Drawing.Point(27, 139);
            this.slkKhachHang.Name = "slkKhachHang";
            this.slkKhachHang.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.slkKhachHang.Properties.Appearance.Options.UseFont = true;
            this.slkKhachHang.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.slkKhachHang.Properties.PopupView = this.slkKhachHangView;
            this.slkKhachHang.Size = new System.Drawing.Size(304, 36);
            this.slkKhachHang.TabIndex = 2;
            // 
            // btnThemKhachNhanh
            // 
            this.btnThemKhachNhanh.BackColor = System.Drawing.Color.Transparent;
            this.btnThemKhachNhanh.BorderRadius = 4;
            this.btnThemKhachNhanh.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnThemKhachNhanh.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnThemKhachNhanh.ForeColor = System.Drawing.Color.White;
            this.btnThemKhachNhanh.Location = new System.Drawing.Point(337, 139);
            this.btnThemKhachNhanh.Name = "btnThemKhachNhanh";
            this.btnThemKhachNhanh.Size = new System.Drawing.Size(36, 36);
            this.btnThemKhachNhanh.TabIndex = 12;
            this.btnThemKhachNhanh.Text = "+";
            this.btnThemKhachNhanh.Click += new System.EventHandler(this.btnThemKhachNhanh_Click);
            // 
            // slkKhachHangView
            // 
            this.slkKhachHangView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.slkKhachHangView.Name = "slkKhachHangView";
            this.slkKhachHangView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.slkKhachHangView.OptionsView.ShowGroupPanel = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(23, 188);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 19);
            this.label2.TabIndex = 3;
            this.label2.Text = "Dự kiến Thời gian Trả";
            // 
            // dtpNgayTra
            // 
            this.dtpNgayTra.EditValue = new System.DateTime(2026, 4, 3, 6, 22, 1, 114);
            this.dtpNgayTra.Location = new System.Drawing.Point(27, 214);
            this.dtpNgayTra.Name = "dtpNgayTra";
            this.dtpNgayTra.Properties.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.dtpNgayTra.Properties.Appearance.Options.UseBackColor = true;
            this.dtpNgayTra.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayTra.Properties.CalendarTimeEditing = DevExpress.Utils.DefaultBoolean.True;
            this.dtpNgayTra.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtpNgayTra.Properties.DisplayFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.dtpNgayTra.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpNgayTra.Properties.EditFormat.FormatString = "dd/MM/yyyy HH:mm";
            this.dtpNgayTra.Properties.EditFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.dtpNgayTra.Properties.MaskSettings.Set("mask", "dd/MM/yyyy HH:mm");
            this.dtpNgayTra.Size = new System.Drawing.Size(346, 20);
            this.dtpNgayTra.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.Transparent;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.label3.Location = new System.Drawing.Point(23, 267);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(152, 15);
            this.label3.TabIndex = 5;
            this.label3.Text = "Số tiền thanh toán TRƯỚC";
            // 
            // txtSoTien
            // 
            this.txtSoTien.BackColor = System.Drawing.Color.Transparent;
            this.txtSoTien.BorderRadius = 8;
            this.txtSoTien.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSoTien.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.txtSoTien.Location = new System.Drawing.Point(27, 293);
            this.txtSoTien.Maximum = new decimal(new int[] {
            100000000,
            0,
            0,
            0});
            this.txtSoTien.Name = "txtSoTien";
            this.txtSoTien.Size = new System.Drawing.Size(346, 45);
            this.txtSoTien.TabIndex = 6;
            this.txtSoTien.ThousandsSeparator = true;
            // 
            // rbTienMat
            // 
            this.rbTienMat.AutoSize = true;
            this.rbTienMat.BackColor = System.Drawing.Color.Transparent;
            this.rbTienMat.Checked = true;
            this.rbTienMat.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rbTienMat.CheckedState.BorderThickness = 0;
            this.rbTienMat.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rbTienMat.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rbTienMat.CheckedState.InnerOffset = -4;
            this.rbTienMat.Location = new System.Drawing.Point(27, 357);
            this.rbTienMat.Name = "rbTienMat";
            this.rbTienMat.Size = new System.Drawing.Size(110, 23);
            this.rbTienMat.TabIndex = 7;
            this.rbTienMat.TabStop = true;
            this.rbTienMat.Text = "Tiền mặt / CK";
            this.rbTienMat.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rbTienMat.UncheckedState.BorderThickness = 2;
            this.rbTienMat.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rbTienMat.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.rbTienMat.UseVisualStyleBackColor = false;
            // 
            // rbRFID
            // 
            this.rbRFID.AutoSize = true;
            this.rbRFID.BackColor = System.Drawing.Color.Transparent;
            this.rbRFID.CheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rbRFID.CheckedState.BorderThickness = 0;
            this.rbRFID.CheckedState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.rbRFID.CheckedState.InnerColor = System.Drawing.Color.White;
            this.rbRFID.CheckedState.InnerOffset = -4;
            this.rbRFID.Location = new System.Drawing.Point(220, 357);
            this.rbRFID.Name = "rbRFID";
            this.rbRFID.Size = new System.Drawing.Size(72, 23);
            this.rbRFID.TabIndex = 8;
            this.rbRFID.Text = "Ví RFID";
            this.rbRFID.UncheckedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.rbRFID.UncheckedState.BorderThickness = 2;
            this.rbRFID.UncheckedState.FillColor = System.Drawing.Color.Transparent;
            this.rbRFID.UncheckedState.InnerColor = System.Drawing.Color.Transparent;
            this.rbRFID.UseVisualStyleBackColor = false;
            // 
            // btnCheckIn
            // 
            this.btnCheckIn.BackColor = System.Drawing.Color.Transparent;
            this.btnCheckIn.BorderRadius = 10;
            this.btnCheckIn.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCheckIn.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCheckIn.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCheckIn.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCheckIn.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(46)))), ((int)(((byte)(125)))), ((int)(((byte)(50)))));
            this.btnCheckIn.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCheckIn.ForeColor = System.Drawing.Color.White;
            this.btnCheckIn.Location = new System.Drawing.Point(27, 407);
            this.btnCheckIn.Name = "btnCheckIn";
            this.btnCheckIn.Size = new System.Drawing.Size(160, 45);
            this.btnCheckIn.TabIndex = 9;
            this.btnCheckIn.Text = "XÁC NHẬN";
            this.btnCheckIn.Click += new System.EventHandler(this.btnCheckIn_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BackColor = System.Drawing.Color.Transparent;
            this.btnCancel.BorderRadius = 10;
            this.btnCancel.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnCancel.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnCancel.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(158)))), ((int)(((byte)(158)))), ((int)(((byte)(158)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(213, 407);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(160, 45);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "HỦY BỎ";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // lblConstraint
            // 
            this.lblConstraint.AutoSize = true;
            this.lblConstraint.BackColor = System.Drawing.Color.Transparent;
            this.lblConstraint.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Italic);
            this.lblConstraint.ForeColor = System.Drawing.Color.Crimson;
            this.lblConstraint.Location = new System.Drawing.Point(24, 86);
            this.lblConstraint.Name = "lblConstraint";
            this.lblConstraint.Size = new System.Drawing.Size(57, 13);
            this.lblConstraint.TabIndex = 11;
            this.lblConstraint.Text = "Constraint";
            this.lblConstraint.Visible = false;
            // 
            // frmBookingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(404, 459);
            this.Controls.Add(this.lblConstraint);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCheckIn);
            this.Controls.Add(this.btnThemKhachNhanh);
            this.Controls.Add(this.rbRFID);
            this.Controls.Add(this.rbTienMat);
            this.Controls.Add(this.txtSoTien);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dtpNgayTra);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.slkKhachHang);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmBookingDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayTra.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpNgayTra.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoTien)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhachHang.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.slkKhachHangView)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SearchLookUpEdit slkKhachHang;
        private DevExpress.XtraGrid.Views.Grid.GridView slkKhachHangView;
        private System.Windows.Forms.Label label2;
        private DevExpress.XtraEditors.DateEdit dtpNgayTra;
        private System.Windows.Forms.Label label3;
        private Guna.UI2.WinForms.Guna2NumericUpDown txtSoTien;
        private Guna.UI2.WinForms.Guna2RadioButton rbTienMat;
        private Guna.UI2.WinForms.Guna2RadioButton rbRFID;
        private Guna.UI2.WinForms.Guna2Button btnCheckIn;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
        private System.Windows.Forms.Label lblConstraint;
        private Guna.UI2.WinForms.Guna2Button btnThemKhachNhanh;
    }
}
