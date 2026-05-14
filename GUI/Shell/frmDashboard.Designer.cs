namespace GUI.Shell
{
    partial class frmDashboard
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>

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


        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.lblWelcome = new DevExpress.XtraEditors.LabelControl();
            this.lblStats = new DevExpress.XtraEditors.LabelControl();
            this.btnLamMoi = new DevExpress.XtraEditors.SimpleButton();
            this.scrollablePanel = new DevExpress.XtraEditors.XtraScrollableControl();
            this.flowCards = new System.Windows.Forms.FlowLayoutPanel();
            this.panelHeader = new DevExpress.XtraEditors.PanelControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).BeginInit();
            this.panelHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panelHeader
            // 
            this.panelHeader.Appearance.BackColor = System.Drawing.Color.White;
            this.panelHeader.Appearance.Options.UseBackColor = true;
            this.panelHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelHeader.Controls.Add(this.btnLamMoi);
            this.panelHeader.Controls.Add(this.lblStats);
            this.panelHeader.Controls.Add(this.lblWelcome);
            this.panelHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panelHeader.Location = new System.Drawing.Point(0, 0);
            this.panelHeader.Name = "panelHeader";
            this.panelHeader.Size = new System.Drawing.Size(1000, 90);
            this.panelHeader.TabIndex = 0;
            // 
            // lblWelcome
            // 
            this.lblWelcome.Appearance.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Bold);
            this.lblWelcome.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(25)))), ((int)(((byte)(42)))), ((int)(((byte)(86)))));
            this.lblWelcome.Appearance.Options.UseFont = true;
            this.lblWelcome.Appearance.Options.UseForeColor = true;
            this.lblWelcome.Location = new System.Drawing.Point(24, 16);
            this.lblWelcome.Name = "lblWelcome";
            this.lblWelcome.Size = new System.Drawing.Size(300, 37);
            this.lblWelcome.TabIndex = 0;
            this.lblWelcome.Text = "BẢNG ĐIỀU KHIỂN";
            // 
            // lblStats
            // 
            this.lblStats.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.lblStats.Appearance.ForeColor = System.Drawing.Color.Gray;
            this.lblStats.Appearance.Options.UseFont = true;
            this.lblStats.Appearance.Options.UseForeColor = true;
            this.lblStats.Location = new System.Drawing.Point(24, 58);
            this.lblStats.Name = "lblStats";
            this.lblStats.Size = new System.Drawing.Size(200, 19);
            this.lblStats.TabIndex = 1;
            this.lblStats.Text = "Dữ liệu từ đầu tháng";
            // 
            // btnLamMoi
            // 
            this.btnLamMoi.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLamMoi.ImageOptions.ImageUri.Uri = "Refresh";
            this.btnLamMoi.Location = new System.Drawing.Point(880, 24);
            this.btnLamMoi.Name = "btnLamMoi";
            this.btnLamMoi.Size = new System.Drawing.Size(100, 36);
            this.btnLamMoi.TabIndex = 2;
            this.btnLamMoi.Text = "Làm mới";
            this.btnLamMoi.Click += new System.EventHandler(this.BtnLamMoi_Click);
            // 
            // scrollablePanel
            // 
            this.scrollablePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.scrollablePanel.Location = new System.Drawing.Point(0, 90);
            this.scrollablePanel.Name = "scrollablePanel";
            this.scrollablePanel.Size = new System.Drawing.Size(1000, 410);
            this.scrollablePanel.TabIndex = 1;
            this.scrollablePanel.Controls.Add(this.flowCards);
            // 
            // flowCards
            // 
            this.flowCards.AutoSize = true;
            this.flowCards.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowCards.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.flowCards.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowCards.FlowDirection = System.Windows.Forms.FlowDirection.LeftToRight;
            this.flowCards.Location = new System.Drawing.Point(0, 0);
            this.flowCards.Name = "flowCards";
            this.flowCards.Padding = new System.Windows.Forms.Padding(16, 8, 16, 8);
            this.flowCards.Size = new System.Drawing.Size(1000, 0);
            this.flowCards.TabIndex = 0;
            this.flowCards.WrapContents = true;
            // 
            // frmDashboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(250)))));
            this.Size = new System.Drawing.Size(1000, 500);
            this.Controls.Add(this.scrollablePanel);
            this.Controls.Add(this.panelHeader);
            this.Name = "frmDashboard";
            this.Text = "Bảng Điều Khiển";
            ((System.ComponentModel.ISupportInitialize)(this.panelHeader)).EndInit();
            this.panelHeader.ResumeLayout(false);
            this.panelHeader.PerformLayout();
            this.ResumeLayout(false);
        }

        #endregion
        private DevExpress.XtraEditors.LabelControl lblWelcome;
        private DevExpress.XtraEditors.LabelControl lblStats;
        private DevExpress.XtraEditors.SimpleButton btnLamMoi;
        private DevExpress.XtraEditors.XtraScrollableControl scrollablePanel;
        private System.Windows.Forms.FlowLayoutPanel flowCards;
        private DevExpress.XtraEditors.PanelControl panelHeader;
        private System.ComponentModel.IContainer components = null;
    }
}