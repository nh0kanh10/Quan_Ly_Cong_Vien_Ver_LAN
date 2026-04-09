using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace GUI
{
    public partial class frmCustomMessageBox : Form
    {
        private Color colorPrimary = ThemeManager.PrimaryColor;
        private Color colorSuccess = ThemeManager.SuccessColor;
        private Color colorWarning = ThemeManager.WarningColor;
        private Color colorError = ThemeManager.DangerColor;
        private Color colorInfo = ThemeManager.SecondaryColor;

        public frmCustomMessageBox(string message, string title, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            InitializeComponent();
            this.lblMessage.Text = message;
            this.lblTitle.Text = title;
            
            ThemeManager.ApplyTheme(this);
            
            // Thiết lập font mỏng (Regular) sau khi ApplyTheme để không bị đè font Bold
            this.lblTitle.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular);
            this.lblTitle.ForeColor = ThemeManager.TextPrimaryColor; // Chuyển sang màu tối cho nền sáng
            this.lblMessage.Font = new Font("Segoe UI", 11.25F, FontStyle.Regular);
            this.lblMessage.ForeColor = ThemeManager.TextPrimaryColor;
            
            this.pnlBody.FillColor = Color.White;
            this.pnlFooter.FillColor = Color.FromArgb(248, 250, 252);
            
            // Tự động kéo dãn Form nếu nội dung quá dài
            int requiredHeight = TextRenderer.MeasureText(message, this.lblMessage.Font, new Size(this.lblMessage.Width, 0), TextFormatFlags.WordBreak).Height;
            if (requiredHeight > this.lblMessage.Height)
            {
                // Thêm một khoản padding để không bị quá sát lề dưới
                this.Height += (requiredHeight - this.lblMessage.Height) + 20; 
            }

            SetIcon(icon);
            SetButtons(buttons);
        }

        private void SetIcon(MessageBoxIcon icon)
        {
            // Sử dụng màu GradientActiveCaption cho tất cả các loại thông báo theo yêu cầu
            pnlHeader.BackColor = SystemColors.GradientActiveCaption;
            picIcon.ForeColor = Color.Black;

            switch (icon)
            {
                case MessageBoxIcon.Information:
                    picIcon.Image = IconHelper.GetBitmap(IconHelper.GetIconFromMessageBox(icon), colorInfo, 40);
                    break;
                case MessageBoxIcon.Error:
                    picIcon.Image = IconHelper.GetBitmap(IconHelper.GetIconFromMessageBox(icon), colorError, 40);
                    break;
                case MessageBoxIcon.Warning:
                    picIcon.Image = IconHelper.GetBitmap(IconHelper.GetIconFromMessageBox(icon), colorWarning, 40);
                    break;
                case MessageBoxIcon.Question:
                    picIcon.Image = IconHelper.GetBitmap(IconHelper.GetIconFromMessageBox(icon), colorPrimary, 40);
                    break;
                default:
                    picIcon.Image = IconHelper.GetBitmap(IconChar.Bell, colorPrimary, 40);
                    break;
            }

            // Thêm viền cho form vì FormBorderStyle = None
            this.Paint += (s, e) => {
                e.Graphics.DrawRectangle(new Pen(pnlHeader.BackColor, 2), 0, 0, this.Width - 1, this.Height - 1);
            };
        }

        private void SetButtons(MessageBoxButtons buttons)
        {
            btnOk.Visible = false;
            btnYes.Visible = false;
            btnNo.Visible = false;
            btnCancel.Visible = false;

            // Style chung cho nút
            foreach (var btn in new[] { btnOk, btnYes, btnNo, btnCancel })
            {
                btn.BorderRadius = ThemeManager.BorderRadius; // Thiết lập vuông theo yêu cầu
                btn.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
                btn.Height = 32;
                btn.BorderThickness = 1;
                btn.BorderColor = Color.FromArgb(226, 232, 240);
                btn.FillColor = Color.White;
                btn.ForeColor = ThemeManager.TextPrimaryColor;
            }

            // Nút ưu tiên (Primary) - Sử dụng PrimaryColor cho nút hành động chính
            btnOk.FillColor = ThemeManager.PrimaryColor;
            btnOk.ForeColor = Color.White;
            btnYes.FillColor = ThemeManager.PrimaryColor;
            btnYes.ForeColor = Color.White;

            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    btnOk.Visible = true;
                    btnOk.Location = new Point(300, 9);
                    break;
                case MessageBoxButtons.OKCancel:
                    btnOk.Visible = true;
                    btnCancel.Visible = true;
                    btnOk.Location = new Point(210, 9);
                    btnCancel.Location = new Point(300, 9);
                    break;
                case MessageBoxButtons.YesNo:
                    btnYes.Visible = true;
                    btnNo.Visible = true;
                    btnYes.Location = new Point(210, 9);
                    btnNo.Location = new Point(300, 9);
                    break;
                case MessageBoxButtons.YesNoCancel:
                    btnYes.Visible = true;
                    btnNo.Visible = true;
                    btnCancel.Visible = true;
                    btnYes.Location = new Point(120, 9);
                    btnNo.Location = new Point(210, 9);
                    btnCancel.Location = new Point(300, 9);
                    break;
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        private void btnNo_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


    }
}
