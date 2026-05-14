namespace GUI.AI
{
    partial class AIChatPanel
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
            this.pnlHeader = new DevExpress.XtraEditors.PanelControl();
            this.lblTitle = new DevExpress.XtraEditors.LabelControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSettings = new DevExpress.XtraEditors.SimpleButton();
            this.btnClear = new DevExpress.XtraEditors.SimpleButton();
            this.txtLog = new DevExpress.XtraEditors.MemoEdit();
            this.lblTyping = new DevExpress.XtraEditors.LabelControl();
            this.pnlSuggestions = new DevExpress.XtraEditors.PanelControl();
            this.pnlInput = new DevExpress.XtraEditors.PanelControl();
            this.txtInput = new DevExpress.XtraEditors.TextEdit();
            this.btnSend = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).BeginInit();
            this.pnlHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSuggestions)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlInput)).BeginInit();
            this.pnlInput.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtInput.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(37)))));
            this.pnlHeader.Appearance.Options.UseBackColor = true;
            this.pnlHeader.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Controls.Add(this.btnSettings);
            this.pnlHeader.Controls.Add(this.btnClear);
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(420, 40);
            this.pnlHeader.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.lblTitle.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(244)))));
            this.lblTitle.Appearance.Options.UseFont = true;
            this.lblTitle.Appearance.Options.UseForeColor = true;
            this.lblTitle.Location = new System.Drawing.Point(12, 10);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(85, 20);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "AI Assistant";
            // 
            // btnClose
            // 
            this.btnClose.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClose.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Appearance.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnClose.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(139)))), ((int)(((byte)(168)))));
            this.btnClose.Appearance.Options.UseBackColor = true;
            this.btnClose.Appearance.Options.UseFont = true;
            this.btnClose.Appearance.Options.UseForeColor = true;
            this.btnClose.Location = new System.Drawing.Point(390, 8);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(24, 24);
            this.btnClose.TabIndex = 3;
            this.btnClose.Text = "X";
            // 
            // btnSettings
            // 
            this.btnSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSettings.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.Appearance.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnSettings.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(166)))), ((int)(((byte)(227)))), ((int)(((byte)(161)))));
            this.btnSettings.Appearance.Options.UseBackColor = true;
            this.btnSettings.Appearance.Options.UseFont = true;
            this.btnSettings.Appearance.Options.UseForeColor = true;
            this.btnSettings.Location = new System.Drawing.Point(366, 8);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(24, 24);
            this.btnSettings.TabIndex = 2;
            this.btnSettings.Text = "S";
            // 
            // btnClear
            // 
            this.btnClear.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClear.Appearance.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.Appearance.Font = new System.Drawing.Font("Segoe UI", 8F, System.Drawing.FontStyle.Bold);
            this.btnClear.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(179)))), ((int)(((byte)(135)))));
            this.btnClear.Appearance.Options.UseBackColor = true;
            this.btnClear.Appearance.Options.UseFont = true;
            this.btnClear.Appearance.Options.UseForeColor = true;
            this.btnClear.Location = new System.Drawing.Point(342, 8);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(24, 24);
            this.btnClear.TabIndex = 1;
            this.btnClear.Text = "C";
            // 
            // txtLog
            // 
            this.txtLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtLog.Location = new System.Drawing.Point(0, 40);
            this.txtLog.Name = "txtLog";
            this.txtLog.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.txtLog.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtLog.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(244)))));
            this.txtLog.Properties.Appearance.Options.UseBackColor = true;
            this.txtLog.Properties.Appearance.Options.UseFont = true;
            this.txtLog.Properties.Appearance.Options.UseForeColor = true;
            this.txtLog.Properties.AppearanceReadOnly.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.txtLog.Properties.AppearanceReadOnly.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(244)))));
            this.txtLog.Properties.AppearanceReadOnly.Options.UseBackColor = true;
            this.txtLog.Properties.AppearanceReadOnly.Options.UseForeColor = true;
            this.txtLog.Properties.ReadOnly = true;
            this.txtLog.Properties.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtLog.Size = new System.Drawing.Size(420, 384);
            this.txtLog.TabIndex = 1;
            // 
            // lblTyping
            // 
            this.lblTyping.Appearance.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblTyping.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(180)))), ((int)(((byte)(250)))));
            this.lblTyping.Appearance.Options.UseFont = true;
            this.lblTyping.Appearance.Options.UseForeColor = true;
            this.lblTyping.AutoSizeMode = DevExpress.XtraEditors.LabelAutoSizeMode.None;
            this.lblTyping.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTyping.Location = new System.Drawing.Point(0, 424);
            this.lblTyping.Name = "lblTyping";
            this.lblTyping.Padding = new System.Windows.Forms.Padding(12, 2, 0, 2);
            this.lblTyping.Size = new System.Drawing.Size(420, 20);
            this.lblTyping.TabIndex = 2;
            this.lblTyping.Visible = false;
            // 
            // pnlSuggestions
            // 
            this.pnlSuggestions.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(37)))));
            this.pnlSuggestions.Appearance.Options.UseBackColor = true;
            this.pnlSuggestions.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlSuggestions.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSuggestions.Location = new System.Drawing.Point(0, 444);
            this.pnlSuggestions.Name = "pnlSuggestions";
            this.pnlSuggestions.Size = new System.Drawing.Size(420, 36);
            this.pnlSuggestions.TabIndex = 3;
            this.pnlSuggestions.Visible = false;
            // 
            // pnlInput
            // 
            this.pnlInput.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(24)))), ((int)(((byte)(24)))), ((int)(((byte)(37)))));
            this.pnlInput.Appearance.Options.UseBackColor = true;
            this.pnlInput.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlInput.Controls.Add(this.btnSend);
            this.pnlInput.Controls.Add(this.txtInput);
            this.pnlInput.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInput.Location = new System.Drawing.Point(0, 480);
            this.pnlInput.Name = "pnlInput";
            this.pnlInput.Size = new System.Drawing.Size(420, 40);
            this.pnlInput.TabIndex = 4;
            // 
            // txtInput
            // 
            this.txtInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) | System.Windows.Forms.AnchorStyles.Right)));
            this.txtInput.Location = new System.Drawing.Point(8, 6);
            this.txtInput.Name = "txtInput";
            this.txtInput.Properties.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(49)))), ((int)(((byte)(50)))), ((int)(((byte)(68)))));
            this.txtInput.Properties.Appearance.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.txtInput.Properties.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(205)))), ((int)(((byte)(214)))), ((int)(((byte)(244)))));
            this.txtInput.Properties.Appearance.Options.UseBackColor = true;
            this.txtInput.Properties.Appearance.Options.UseFont = true;
            this.txtInput.Properties.Appearance.Options.UseForeColor = true;
            this.txtInput.Properties.NullValuePrompt = "Type your question...";
            this.txtInput.Properties.NullValuePromptShowForEmptyValue = true;
            this.txtInput.Size = new System.Drawing.Size(340, 28);
            this.txtInput.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSend.Appearance.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(137)))), ((int)(((byte)(180)))), ((int)(((byte)(250)))));
            this.btnSend.Appearance.Font = new System.Drawing.Font("Segoe UI Semibold", 9F);
            this.btnSend.Appearance.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.btnSend.Appearance.Options.UseBackColor = true;
            this.btnSend.Appearance.Options.UseFont = true;
            this.btnSend.Appearance.Options.UseForeColor = true;
            this.btnSend.Location = new System.Drawing.Point(354, 6);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(58, 28);
            this.btnSend.TabIndex = 1;
            this.btnSend.Text = "Send";
            // 
            // AIChatPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(30)))), ((int)(((byte)(46)))));
            this.Controls.Add(this.txtLog);
            this.Controls.Add(this.lblTyping);
            this.Controls.Add(this.pnlSuggestions);
            this.Controls.Add(this.pnlInput);
            this.Controls.Add(this.pnlHeader);
            this.Name = "AIChatPanel";
            this.Size = new System.Drawing.Size(420, 520);
            ((System.ComponentModel.ISupportInitialize)(this.pnlHeader)).EndInit();
            this.pnlHeader.ResumeLayout(false);
            this.pnlHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLog.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlSuggestions)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pnlInput)).EndInit();
            this.pnlInput.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtInput.Properties)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private DevExpress.XtraEditors.PanelControl pnlHeader;
        private DevExpress.XtraEditors.LabelControl lblTitle;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSettings;
        private DevExpress.XtraEditors.SimpleButton btnClear;
        private DevExpress.XtraEditors.MemoEdit txtLog;
        private DevExpress.XtraEditors.LabelControl lblTyping;
        private DevExpress.XtraEditors.PanelControl pnlSuggestions;
        private DevExpress.XtraEditors.PanelControl pnlInput;
        private DevExpress.XtraEditors.TextEdit txtInput;
        private DevExpress.XtraEditors.SimpleButton btnSend;
        private System.ComponentModel.IContainer components = null;





    }}