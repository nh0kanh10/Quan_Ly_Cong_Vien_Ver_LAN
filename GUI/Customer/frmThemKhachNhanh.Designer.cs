namespace GUI
{
    partial class frmThemKhachNhanh
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
            this.lblTen = new System.Windows.Forms.Label();
            this.txtTen = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblSdt = new System.Windows.Forms.Label();
            this.txtSdt = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblCccd = new System.Windows.Forms.Label();
            this.txtCccd = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // lblTen
            // 
            this.lblTen.AutoSize = true;
            this.lblTen.Location = new System.Drawing.Point(20, 20);
            this.lblTen.Name = "lblTen";
            this.lblTen.Size = new System.Drawing.Size(107, 16);
            this.lblTen.TabIndex = 0;
            this.lblTen.Text = "Tên Khách Hàng:";
            // 
            // txtTen
            // 
            this.txtTen.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTen.DefaultText = "";
            this.txtTen.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtTen.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtTen.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTen.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtTen.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTen.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtTen.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtTen.Location = new System.Drawing.Point(23, 39);
            this.txtTen.Name = "txtTen";
            this.txtTen.PasswordChar = '\0';
            this.txtTen.PlaceholderText = "Nhập tên khách hàng...";
            this.txtTen.SelectedText = "";
            this.txtTen.Size = new System.Drawing.Size(300, 36);
            this.txtTen.TabIndex = 1;
            // 
            // lblSdt
            // 
            this.lblSdt.AutoSize = true;
            this.lblSdt.Location = new System.Drawing.Point(20, 85);
            this.lblSdt.Name = "lblSdt";
            this.lblSdt.Size = new System.Drawing.Size(95, 16);
            this.lblSdt.TabIndex = 2;
            this.lblSdt.Text = "Số Điện Thoại:";
            // 
            // txtSdt
            // 
            this.txtSdt.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtSdt.DefaultText = "";
            this.txtSdt.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtSdt.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtSdt.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSdt.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtSdt.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSdt.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtSdt.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtSdt.Location = new System.Drawing.Point(23, 104);
            this.txtSdt.Name = "txtSdt";
            this.txtSdt.PasswordChar = '\0';
            this.txtSdt.PlaceholderText = "Nhập số điện thoại...";
            this.txtSdt.SelectedText = "";
            this.txtSdt.Size = new System.Drawing.Size(300, 36);
            this.txtSdt.TabIndex = 3;
            // 
            // btnLuu
            // 
            this.btnLuu.BackColor = System.Drawing.Color.Transparent;
            this.btnLuu.BorderRadius = 6;
            this.btnLuu.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI Semibold", 9.75F, System.Drawing.FontStyle.Bold);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(203, 218);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(120, 40);
            this.btnLuu.TabIndex = 6;
            this.btnLuu.Text = "LƯU (ENTER)";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // lblCccd
            // 
            this.lblCccd.AutoSize = true;
            this.lblCccd.Location = new System.Drawing.Point(20, 150);
            this.lblCccd.Name = "lblCccd";
            this.lblCccd.Size = new System.Drawing.Size(90, 16);
            this.lblCccd.TabIndex = 4;
            this.lblCccd.Text = "CCCD/CMND:";
            // 
            // txtCccd
            // 
            this.txtCccd.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCccd.DefaultText = "";
            this.txtCccd.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtCccd.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtCccd.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCccd.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtCccd.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCccd.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtCccd.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtCccd.Location = new System.Drawing.Point(23, 169);
            this.txtCccd.Name = "txtCccd";
            this.txtCccd.PasswordChar = '\0';
            this.txtCccd.PlaceholderText = "Nhập CCCD/CMND...";
            this.txtCccd.SelectedText = "";
            this.txtCccd.Size = new System.Drawing.Size(300, 36);
            this.txtCccd.TabIndex = 5;
            // 
            // frmThemKhachNhanh
            // 
            this.AcceptButton = this.btnLuu;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(344, 276);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.txtCccd);
            this.Controls.Add(this.lblCccd);
            this.Controls.Add(this.txtSdt);
            this.Controls.Add(this.lblSdt);
            this.Controls.Add(this.txtTen);
            this.Controls.Add(this.lblTen);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThemKhachNhanh";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm Khách Hàng Nhanh";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private System.Windows.Forms.Label lblTen;
        private Guna.UI2.WinForms.Guna2TextBox txtTen;
        private System.Windows.Forms.Label lblSdt;
        private Guna.UI2.WinForms.Guna2TextBox txtSdt;
        private System.Windows.Forms.Label lblCccd;
        private Guna.UI2.WinForms.Guna2TextBox txtCccd;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
    }
}
