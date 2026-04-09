namespace GUI
{
    partial class frmThemBaoTri
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
            this.lblThietBi = new System.Windows.Forms.Label();
            this.cboLoai = new Guna.UI2.WinForms.Guna2ComboBox();
            this.txtNoiDung = new Guna.UI2.WinForms.Guna2TextBox();
            this.txtChiPhi = new Guna.UI2.WinForms.Guna2TextBox();
            this.dtpNgay = new DevExpress.XtraEditors.DateEdit();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblThietBi
            // 
            this.lblThietBi.AutoSize = true;
            this.lblThietBi.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold);
            this.lblThietBi.Location = new System.Drawing.Point(30, 20);
            this.lblThietBi.Name = "lblThietBi";
            this.lblThietBi.Size = new System.Drawing.Size(90, 28);
            this.lblThietBi.TabIndex = 0;
            this.lblThietBi.Text = "Thiết bị:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 70);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Ngày BD:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 130);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 0;
            this.label2.Text = "Loại BT:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 190);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(89, 20);
            this.label3.TabIndex = 0;
            this.label3.Text = "Nội dung:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(30, 290);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(89, 20);
            this.label4.TabIndex = 0;
            this.label4.Text = "Chi phí:";
            // 
            // dtpNgay
            // 
            this.dtpNgay.Location = new System.Drawing.Point(140, 65);
            this.dtpNgay.Name = "dtpNgay";
            this.dtpNgay.Size = new System.Drawing.Size(220, 36);
            this.dtpNgay.TabIndex = 1;
            // 
            // cboLoai
            // 
            this.cboLoai.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboLoai.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboLoai.Location = new System.Drawing.Point(140, 125);
            this.cboLoai.Name = "cboLoai";
            this.cboLoai.Size = new System.Drawing.Size(220, 36);
            this.cboLoai.TabIndex = 2;
            // 
            // txtNoiDung
            // 
            this.txtNoiDung.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNoiDung.DefaultText = "";
            this.txtNoiDung.Location = new System.Drawing.Point(140, 185);
            this.txtNoiDung.Multiline = true;
            this.txtNoiDung.Name = "txtNoiDung";
            this.txtNoiDung.Size = new System.Drawing.Size(220, 80);
            this.txtNoiDung.TabIndex = 3;
            // 
            // txtChiPhi
            // 
            this.txtChiPhi.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtChiPhi.DefaultText = "0";
            this.txtChiPhi.Location = new System.Drawing.Point(140, 285);
            this.txtChiPhi.Name = "txtChiPhi";
            this.txtChiPhi.Size = new System.Drawing.Size(220, 36);
            this.txtChiPhi.TabIndex = 4;
            // 
            // btnLuu
            // 
            this.btnLuu.FillColor = System.Drawing.Color.MediumSeaGreen;
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(90, 360);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(120, 45);
            this.btnLuu.TabIndex = 5;
            this.btnLuu.Text = "Lưu";
            this.btnLuu.Click += new System.EventHandler(this.btnLuu_Click);
            // 
            // btnHuy
            // 
            this.btnHuy.FillColor = System.Drawing.Color.Gray;
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(230, 360);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(120, 45);
            this.btnHuy.TabIndex = 6;
            this.btnHuy.Text = "Hủy";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // frmThemBaoTri
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(420, 440);
            this.Controls.Add(this.btnHuy);
            this.Controls.Add(this.btnLuu);
            this.Controls.Add(this.txtChiPhi);
            this.Controls.Add(this.txtNoiDung);
            this.Controls.Add(this.cboLoai);
            this.Controls.Add(this.dtpNgay);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblThietBi);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmThemBaoTri";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Thêm Lịch Bảo Trì";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblThietBi;
        private Guna.UI2.WinForms.Guna2ComboBox cboLoai;
        private Guna.UI2.WinForms.Guna2TextBox txtNoiDung;
        private Guna.UI2.WinForms.Guna2TextBox txtChiPhi;
        private DevExpress.XtraEditors.DateEdit dtpNgay;
        private Guna.UI2.WinForms.Guna2Button btnLuu;
        private Guna.UI2.WinForms.Guna2Button btnHuy;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
    }
}
