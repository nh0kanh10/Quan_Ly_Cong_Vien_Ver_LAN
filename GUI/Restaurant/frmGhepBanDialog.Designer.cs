namespace GUI
{
    partial class frmGhepBanDialog
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
            this.lblHint = new System.Windows.Forms.Label();
            this.rdoBan1 = new System.Windows.Forms.RadioButton();
            this.rdoBan2 = new System.Windows.Forms.RadioButton();
            this.btnOK = new Guna.UI2.WinForms.Guna2Button();
            this.btnCancel = new Guna.UI2.WinForms.Guna2Button();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(184, 21);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Giữ lại Bill của bàn nào?";
            // 
            // lblHint
            // 
            this.lblHint.AutoSize = true;
            this.lblHint.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblHint.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.lblHint.Location = new System.Drawing.Point(20, 50);
            this.lblHint.Name = "lblHint";
            this.lblHint.Size = new System.Drawing.Size(262, 15);
            this.lblHint.TabIndex = 1;
            this.lblHint.Text = "(Bill bàn kia sẽ gộp vào và dọn thành bàn trống)";
            // 
            // rdoBan1
            // 
            this.rdoBan1.AutoSize = true;
            this.rdoBan1.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.rdoBan1.Location = new System.Drawing.Point(30, 85);
            this.rdoBan1.Name = "rdoBan1";
            this.rdoBan1.Size = new System.Drawing.Size(64, 24);
            this.rdoBan1.TabIndex = 2;
            this.rdoBan1.TabStop = true;
            this.rdoBan1.Text = "Bàn 1";
            this.rdoBan1.UseVisualStyleBackColor = true;
            // 
            // rdoBan2
            // 
            this.rdoBan2.AutoSize = true;
            this.rdoBan2.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.rdoBan2.Location = new System.Drawing.Point(30, 115);
            this.rdoBan2.Name = "rdoBan2";
            this.rdoBan2.Size = new System.Drawing.Size(64, 24);
            this.rdoBan2.TabIndex = 3;
            this.rdoBan2.TabStop = true;
            this.rdoBan2.Text = "Bàn 2";
            this.rdoBan2.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.BorderRadius = 4;
            this.btnOK.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(16)))), ((int)(((byte)(185)))), ((int)(((byte)(129)))));
            this.btnOK.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnOK.ForeColor = System.Drawing.Color.White;
            this.btnOK.Location = new System.Drawing.Point(180, 160);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 35);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "Ghép Bàn";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.BorderRadius = 4;
            this.btnCancel.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(68)))), ((int)(((byte)(68)))));
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnCancel.ForeColor = System.Drawing.Color.White;
            this.btnCancel.Location = new System.Drawing.Point(70, 160);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 35);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Hủy";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // frmGhepBanDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(324, 211);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.rdoBan2);
            this.Controls.Add(this.rdoBan1);
            this.Controls.Add(this.lblHint);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmGhepBanDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chọn Bàn Giữ Bill";
            this.Load += new System.EventHandler(this.frmGhepBanDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblHint;
        private System.Windows.Forms.RadioButton rdoBan1;
        private System.Windows.Forms.RadioButton rdoBan2;
        private Guna.UI2.WinForms.Guna2Button btnOK;
        private Guna.UI2.WinForms.Guna2Button btnCancel;
    }
}
