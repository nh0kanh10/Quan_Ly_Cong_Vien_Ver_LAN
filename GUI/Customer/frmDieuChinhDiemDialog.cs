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
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblDiem = new System.Windows.Forms.Label();
            this.txtDiem = new Guna.UI2.WinForms.Guna2TextBox();
            this.lblLyDo = new System.Windows.Forms.Label();
            this.txtLyDo = new Guna.UI2.WinForms.Guna2TextBox();
            this.btnLuu = new Guna.UI2.WinForms.Guna2Button();
            this.btnHuy = new Guna.UI2.WinForms.Guna2Button();
            
            this.pnlMain.SuspendLayout();
            this.SuspendLayout();
            
            // pnlMain
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Padding = new System.Windows.Forms.Padding(20);
            this.pnlMain.Controls.Add(this.btnHuy);
            this.pnlMain.Controls.Add(this.btnLuu);
            this.pnlMain.Controls.Add(this.txtLyDo);
            this.pnlMain.Controls.Add(this.lblLyDo);
            this.pnlMain.Controls.Add(this.txtDiem);
            this.pnlMain.Controls.Add(this.lblDiem);
            this.pnlMain.Controls.Add(this.lblTitle);
            
            // lblTitle
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.Location = new System.Drawing.Point(20, 20);
            
            // lblDiem
            this.lblDiem.AutoSize = true;
            this.lblDiem.Location = new System.Drawing.Point(20, 70);
            this.lblDiem.Text = "Số điểm điều chỉnh (+ hoặc -):";
            
            // txtDiem
            this.txtDiem.Location = new System.Drawing.Point(24, 95);
            this.txtDiem.Size = new System.Drawing.Size(350, 40);
            this.txtDiem.BorderRadius = 4;
            
            // lblLyDo
            this.lblLyDo.AutoSize = true;
            this.lblLyDo.Location = new System.Drawing.Point(20, 150);
            this.lblLyDo.Text = "Lý do (Bắt buộc):";
            
            // txtLyDo
            this.txtLyDo.Location = new System.Drawing.Point(24, 175);
            this.txtLyDo.Size = new System.Drawing.Size(350, 40);
            this.txtLyDo.BorderRadius = 4;
            
            // btnLuu
            this.btnLuu.Location = new System.Drawing.Point(24, 240);
            this.btnLuu.Size = new System.Drawing.Size(165, 40);
            this.btnLuu.Text = "XÁC NHẬN";
            this.btnLuu.BorderRadius = 4;
            this.btnLuu.Click += this.BtnLuu_Click;
            
            // btnHuy
            this.btnHuy.Location = new System.Drawing.Point(209, 240);
            this.btnHuy.Size = new System.Drawing.Size(165, 40);
            this.btnHuy.Text = "HỦY";
            this.btnHuy.BorderRadius = 4;
            this.btnHuy.FillColor = System.Drawing.Color.Gray;
            this.btnHuy.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };
            
            // frmDieuChinhDiemDialog
            this.ClientSize = new System.Drawing.Size(400, 310);
            this.Controls.Add(this.pnlMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Font = new System.Drawing.Font("Segoe UI", 10F);
            
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
    }
}
