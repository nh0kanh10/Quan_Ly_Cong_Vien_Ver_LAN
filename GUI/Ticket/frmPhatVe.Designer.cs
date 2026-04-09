namespace GUI
{
    partial class frmPhatVe
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlHeader = new Guna.UI2.WinForms.Guna2Panel();
            this.lblSubtitle = new System.Windows.Forms.Label();
            this.lblTitle = new System.Windows.Forms.Label();
            this.flowCards = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlActions = new Guna.UI2.WinForms.Guna2Panel();
            this.flowBtns = new System.Windows.Forms.FlowLayoutPanel();
            this.btnXong = new Guna.UI2.WinForms.Guna2Button();
            this.btnLuuQR = new Guna.UI2.WinForms.Guna2Button();
            this.btnInLai = new Guna.UI2.WinForms.Guna2Button();
            this.btnInTatCa = new Guna.UI2.WinForms.Guna2Button();
            this.pnlHeader.SuspendLayout();
            this.pnlActions.SuspendLayout();
            this.flowBtns.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Controls.Add(this.lblSubtitle);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Margin = new System.Windows.Forms.Padding(2);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(793, 57);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblSubtitle
            // 
            this.lblSubtitle.AutoSize = true;
            this.lblSubtitle.BackColor = System.Drawing.Color.Transparent;
            this.lblSubtitle.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.lblSubtitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.lblSubtitle.Location = new System.Drawing.Point(15, 34);
            this.lblSubtitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSubtitle.Name = "lblSubtitle";
            this.lblSubtitle.Size = new System.Drawing.Size(22, 15);
            this.lblSubtitle.TabIndex = 1;
            this.lblSubtitle.Text = "---";
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.BackColor = System.Drawing.Color.Transparent;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 13F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(15, 10);
            this.lblTitle.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(87, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "PHÁT VÉ";
            // 
            // flowCards
            // 
            this.flowCards.AutoScroll = true;
            this.flowCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.flowCards.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowCards.Location = new System.Drawing.Point(0, 57);
            this.flowCards.Margin = new System.Windows.Forms.Padding(2);
            this.flowCards.Name = "flowCards";
            this.flowCards.Padding = new System.Windows.Forms.Padding(15, 12, 15, 12);
            this.flowCards.Size = new System.Drawing.Size(793, 497);
            this.flowCards.TabIndex = 2;
            // 
            // pnlActions
            // 
            this.pnlActions.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.pnlActions.BorderThickness = 1;
            this.pnlActions.Controls.Add(this.flowBtns);
            this.pnlActions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlActions.FillColor = System.Drawing.Color.White;
            this.pnlActions.Location = new System.Drawing.Point(0, 554);
            this.pnlActions.Margin = new System.Windows.Forms.Padding(2);
            this.pnlActions.Name = "pnlActions";
            this.pnlActions.Size = new System.Drawing.Size(793, 57);
            this.pnlActions.TabIndex = 1;
            // 
            // flowBtns
            // 
            this.flowBtns.AutoSize = true;
            this.flowBtns.BackColor = System.Drawing.Color.Transparent;
            this.flowBtns.Controls.Add(this.btnXong);
            this.flowBtns.Controls.Add(this.btnLuuQR);
            this.flowBtns.Controls.Add(this.btnInLai);
            this.flowBtns.Controls.Add(this.btnInTatCa);
            this.flowBtns.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowBtns.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowBtns.Location = new System.Drawing.Point(0, 0);
            this.flowBtns.Margin = new System.Windows.Forms.Padding(2);
            this.flowBtns.Name = "flowBtns";
            this.flowBtns.Padding = new System.Windows.Forms.Padding(8, 10, 8, 0);
            this.flowBtns.Size = new System.Drawing.Size(793, 57);
            this.flowBtns.TabIndex = 0;
            // 
            // btnXong
            // 
            this.btnXong.Animated = true;
            this.btnXong.BorderRadius = 8;
            this.btnXong.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnXong.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(23)))), ((int)(((byte)(42)))));
            this.btnXong.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnXong.ForeColor = System.Drawing.Color.White;
            this.btnXong.Location = new System.Drawing.Point(668, 10);
            this.btnXong.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnXong.Name = "btnXong";
            this.btnXong.Size = new System.Drawing.Size(105, 37);
            this.btnXong.TabIndex = 2;
            this.btnXong.Text = "✅ XONG";
            // 
            // btnLuuQR
            // 
            this.btnLuuQR.Animated = true;
            this.btnLuuQR.BorderRadius = 8;
            this.btnLuuQR.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLuuQR.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(34)))), ((int)(((byte)(197)))), ((int)(((byte)(94)))));
            this.btnLuuQR.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnLuuQR.ForeColor = System.Drawing.Color.White;
            this.btnLuuQR.Location = new System.Drawing.Point(510, 10);
            this.btnLuuQR.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnLuuQR.Name = "btnLuuQR";
            this.btnLuuQR.Size = new System.Drawing.Size(150, 37);
            this.btnLuuQR.TabIndex = 3;
            this.btnLuuQR.Text = "💾 LƯU QR CODE";
            // 
            // btnInLai
            // 
            this.btnInLai.Animated = true;
            this.btnInLai.BorderRadius = 8;
            this.btnInLai.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInLai.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(116)))), ((int)(((byte)(139)))));
            this.btnInLai.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnInLai.ForeColor = System.Drawing.Color.White;
            this.btnInLai.Location = new System.Drawing.Point(397, 10);
            this.btnInLai.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnInLai.Name = "btnInLai";
            this.btnInLai.Size = new System.Drawing.Size(105, 37);
            this.btnInLai.TabIndex = 1;
            this.btnInLai.Text = "🔄 IN LẠI";
            // 
            // btnInTatCa
            // 
            this.btnInTatCa.Animated = true;
            this.btnInTatCa.BorderRadius = 8;
            this.btnInTatCa.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInTatCa.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(37)))), ((int)(((byte)(99)))), ((int)(((byte)(235)))));
            this.btnInTatCa.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Bold);
            this.btnInTatCa.ForeColor = System.Drawing.Color.White;
            this.btnInTatCa.Location = new System.Drawing.Point(209, 10);
            this.btnInTatCa.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.btnInTatCa.Name = "btnInTatCa";
            this.btnInTatCa.Size = new System.Drawing.Size(180, 37);
            this.btnInTatCa.TabIndex = 0;
            this.btnInTatCa.Text = "🖨️ IN TẤT CẢ VÉ";
            // 
            // frmPhatVe
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(248)))), ((int)(((byte)(250)))), ((int)(((byte)(252)))));
            this.ClientSize = new System.Drawing.Size(793, 611);
            this.Controls.Add(this.flowCards);
            this.Controls.Add(this.pnlActions);
            this.Controls.Add(this.pnlHeader);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "frmPhatVe";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Phát Vé Điện Tử";
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            this.pnlActions.ResumeLayout(false);
            this.pnlActions.PerformLayout();
            this.flowBtns.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private Guna.UI2.WinForms.Guna2Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblSubtitle;
        private System.Windows.Forms.FlowLayoutPanel flowCards;
        private Guna.UI2.WinForms.Guna2Panel pnlActions;
        private System.Windows.Forms.FlowLayoutPanel flowBtns;
        private Guna.UI2.WinForms.Guna2Button btnInTatCa;
        private Guna.UI2.WinForms.Guna2Button btnInLai;
        private Guna.UI2.WinForms.Guna2Button btnXong;
        private Guna.UI2.WinForms.Guna2Button btnLuuQR;
    }
}
