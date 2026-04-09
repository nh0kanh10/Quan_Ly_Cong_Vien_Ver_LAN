namespace GUI
{
    partial class frmAppDatVeMoPhong
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
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblDesc = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlRole = new Guna.UI2.WinForms.Guna2Panel();
            this.cboRole = new Guna.UI2.WinForms.Guna2ComboBox();
            this.lblRole = new System.Windows.Forms.Label();
            this.gbFeatures = new Guna.UI2.WinForms.Guna2GroupBox();
            this.pnlFeatures = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlHeader.SuspendLayout();
            this.pnlRole.SuspendLayout();
            this.gbFeatures.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblDesc);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Padding = new System.Windows.Forms.Padding(30, 20, 30, 20);
            this.pnlHeader.Size = new System.Drawing.Size(1300, 90);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblDesc
            // 
            this.lblDesc.AutoSize = true;
            this.lblDesc.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblDesc.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblDesc.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(180)))), ((int)(((byte)(200)))), ((int)(((byte)(220)))));
            this.lblDesc.Location = new System.Drawing.Point(30, 52);
            this.lblDesc.Name = "lblDesc";
            this.lblDesc.Size = new System.Drawing.Size(510, 19);
            this.lblDesc.TabIndex = 0;
            this.lblDesc.Text = "Chọn vai trò khách hàng để xem các tính năng tương ứng trên ứng dụng di động.";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(41)))), ((int)(((byte)(59)))));
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(30, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(354, 30);
            this.lblTitle.TabIndex = 1;
            this.lblTitle.Text = "🎫 Mô phỏng App Đặt Vé Đại Nam";
            // 
            // pnlRole
            // 
            this.pnlRole.Controls.Add(this.cboRole);
            this.pnlRole.Controls.Add(this.lblRole);
            this.pnlRole.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlRole.Location = new System.Drawing.Point(0, 90);
            this.pnlRole.Name = "pnlRole";
            this.pnlRole.Padding = new System.Windows.Forms.Padding(30, 15, 30, 15);
            this.pnlRole.Size = new System.Drawing.Size(1300, 65);
            this.pnlRole.TabIndex = 1;
            // 
            // cboRole
            // 
            this.cboRole.BackColor = System.Drawing.Color.Transparent;
            this.cboRole.BorderRadius = 8;
            this.cboRole.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboRole.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRole.FocusedColor = System.Drawing.Color.Empty;
            this.cboRole.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.cboRole.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(88)))), ((int)(((byte)(112)))));
            this.cboRole.ItemHeight = 30;
            this.cboRole.Location = new System.Drawing.Point(110, 15);
            this.cboRole.Name = "cboRole";
            this.cboRole.Size = new System.Drawing.Size(250, 36);
            this.cboRole.TabIndex = 1;
            // 
            // lblRole
            // 
            this.lblRole.AutoSize = true;
            this.lblRole.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Bold);
            this.lblRole.Location = new System.Drawing.Point(30, 22);
            this.lblRole.Name = "lblRole";
            this.lblRole.Size = new System.Drawing.Size(59, 20);
            this.lblRole.TabIndex = 2;
            this.lblRole.Text = "Vai trò:";
            // 
            // gbFeatures
            // 
            this.gbFeatures.Controls.Add(this.pnlFeatures);
            this.gbFeatures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gbFeatures.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.gbFeatures.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(125)))), ((int)(((byte)(137)))), ((int)(((byte)(149)))));
            this.gbFeatures.Location = new System.Drawing.Point(0, 155);
            this.gbFeatures.Name = "gbFeatures";
            this.gbFeatures.Padding = new System.Windows.Forms.Padding(20);
            this.gbFeatures.Size = new System.Drawing.Size(1300, 595);
            this.gbFeatures.TabIndex = 2;
            this.gbFeatures.Text = "Tính năng khả dụng";
            // 
            // pnlFeatures
            // 
            this.pnlFeatures.AutoScroll = true;
            this.pnlFeatures.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlFeatures.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.pnlFeatures.Location = new System.Drawing.Point(20, 60);
            this.pnlFeatures.Name = "pnlFeatures";
            this.pnlFeatures.Padding = new System.Windows.Forms.Padding(10);
            this.pnlFeatures.Size = new System.Drawing.Size(1260, 515);
            this.pnlFeatures.TabIndex = 0;
            this.pnlFeatures.WrapContents = false;
            // 
            // frmAppDatVeMoPhong
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1300, 750);
            this.Controls.Add(this.gbFeatures);
            this.Controls.Add(this.pnlRole);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmAppDatVeMoPhong";
            this.Text = "Mô phỏng App Đặt Vé";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlRole.ResumeLayout(false);
            this.pnlRole.PerformLayout();
            this.gbFeatures.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblDesc;
        private Guna.UI2.WinForms.Guna2Panel pnlRole;
        private System.Windows.Forms.Label lblRole;
        private Guna.UI2.WinForms.Guna2ComboBox cboRole;
        private Guna.UI2.WinForms.Guna2GroupBox gbFeatures;
        private System.Windows.Forms.FlowLayoutPanel pnlFeatures;
    }
}
