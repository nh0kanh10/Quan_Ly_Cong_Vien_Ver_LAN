using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using ET;

namespace GUI
{
    public partial class frmDieuChinhDiemDialog : Form
    {
        public int DiemDieuChinh { get; private set; }
        public string LyDo { get; private set; }

        private Guna2TextBox txtDiem;
        private Guna2TextBox txtLyDo;
        private Guna2Button btnLuu;
        private Guna2Button btnHuy;
        private Label lblTitle;
        private Label lblDiem;
        private Label lblLyDo;
        private Guna2Panel pnlMain;

        public frmDieuChinhDiemDialog(int currentPoints)
        {
            InitializeComponent();
            ThemeManager.ApplyTheme(this);
            lblTitle.Text = $"ĐIỀU CHỈNH ĐIỂM (HIỆN CÓ: {currentPoints:N0})";
            txtDiem.Focus();
        }

        private void InitializeComponent()
        {
            this.pnlMain = new Guna.UI2.WinForms.Guna2Panel();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.txtLyDo = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtDiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblDiem = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlMain
            // 
            this.pnlMain.Controls.Add(this.btnHuy);
            this.pnlMain.Controls.Add(this.btnLuu);
            this.pnlMain.Controls.Add(this.txtLyDo);
            this.pnlMain.Controls.Add(this.lblLyDo);
            this.pnlMain.Controls.Add(this.txtDiem);
            this.pnlMain.Controls.Add(this.lblDiem);
            this.pnlMain.Controls.Add(this.lblTitle);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(0, 0);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Size = new System.Drawing.Size(400, 310);
            this.pnlMain.TabIndex = 0;
            // 
            // btnHuy
            // 
            this.btnHuy.BorderRadius = 4;
            this.btnHuy.FillColor = System.Drawing.Color.Gray;
            this.btnHuy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnHuy.ForeColor = System.Drawing.Color.White;
            this.btnHuy.Location = new System.Drawing.Point(209, 240);
            this.btnHuy.Name = "btnHuy";
            this.btnHuy.Size = new System.Drawing.Size(165, 40);
            this.btnHuy.TabIndex = 0;
            this.btnHuy.Text = "HỦY";
            this.btnHuy.Click += new System.EventHandler(this.btnHuy_Click);
            // 
            // btnLuu
            // 
            this.btnLuu.BorderRadius = 4;
            this.btnLuu.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnLuu.ForeColor = System.Drawing.Color.White;
            this.btnLuu.Location = new System.Drawing.Point(24, 240);
            this.btnLuu.Name = "btnLuu";
            this.btnLuu.Size = new System.Drawing.Size(165, 40);
            this.btnLuu.TabIndex = 1;
            this.btnLuu.Text = "XÁC NHẬN";
            // 
            // txtLyDo
            // 
            this.txtLyDo.BorderRadius = 4;
            this.txtLyDo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLyDo.DefaultText = "";
            this.txtLyDo.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtLyDo.Location = new System.Drawing.Point(24, 175);
            this.txtLyDo.Name = "txtLyDo";
            this.txtLyDo.PlaceholderText = "";
            this.txtLyDo.SelectedText = "";
            this.txtLyDo.Size = new System.Drawing.Size(350, 40);
            this.txtLyDo.TabIndex = 2;
            // 
            // lblLyDo
            // 
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Location = new System.Drawing.Point(20, 150);
            this.lblLyDo.Name = "lblLyDo";
            this.lblLyDo.Size = new System.Drawing.Size(111, 19);
            this.lblLyDo.TabIndex = 3;
            this.lblLyDo.Text = "Lý do (Bắt buộc):";
            // 
            // txtDiem
            // 
            this.txtDiem.BorderRadius = 4;
            this.txtDiem.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDiem.DefaultText = "";
            this.txtDiem.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtDiem.Location = new System.Drawing.Point(24, 95);
            this.txtDiem.Name = "txtDiem";
            this.txtDiem.PlaceholderText = "";
            this.txtDiem.SelectedText = "";
            this.txtDiem.Size = new System.Drawing.Size(350, 40);
            this.txtDiem.TabIndex = 4;
            // 
            // lblDiem
            // 
            this.lblDiem.AutoSize = true;
            this.lblDiem.Location = new System.Drawing.Point(20, 70);
            this.lblDiem.Name = "lblDiem";
            this.lblDiem.Size = new System.Drawing.Size(193, 19);
            this.lblDiem.TabIndex = 5;
            this.lblDiem.Text = "Số điểm điều chỉnh (+ hoặc -):";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(0, 25);
            this.lblTitle.TabIndex = 6;
            // 
            // frmDieuChinhDiemDialog
            // 
            this.ClientSize = new System.Drawing.Size(400, 310);
            this.Controls.Add(this.pnlMain);
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "frmDieuChinhDiemDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.pnlMain.ResumeLayout(false);
            this.pnlMain.PerformLayout();
            this.ResumeLayout(false);

        }

        private void BtnLuu_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtDiem.Text.Trim(), out int diem) || diem == 0)
            {
                TDCMessageBox.Show("Vui lòng nhập số điểm hợp lệ (khác 0).", "Lỗi");
                return;
            }
            if (string.IsNullOrWhiteSpace(txtLyDo.Text))
            {
                TDCMessageBox.Show("Vui lòng nhập lý do điều chỉnh.", "Lỗi");
                return;
            }
            
            DiemDieuChinh = diem;
            LyDo = txtLyDo.Text.Trim();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel; 
            this.Close();
        }
    }
}
