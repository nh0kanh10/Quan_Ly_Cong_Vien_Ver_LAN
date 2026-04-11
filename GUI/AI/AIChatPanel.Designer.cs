using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.AI
{
    partial class AIChatPanel
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

        #region Component Designer generated code

        private void InitializeComponent()
        {
            this.pnlHeader = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnSettings = new System.Windows.Forms.Button();
            this.btnClear = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.pnlBody = new System.Windows.Forms.Panel();
            this.rtbChatLog = new System.Windows.Forms.RichTextBox();
            this.lblTyping = new System.Windows.Forms.Label();
            this.pnlInputBar = new System.Windows.Forms.Panel();
            this.pnlTxtWrap = new System.Windows.Forms.Panel();
            this.txtInput = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.pnlHeader.SuspendLayout();
            this.pnlBody.SuspendLayout();
            this.pnlInputBar.SuspendLayout();
            this.pnlTxtWrap.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlHeader
            // 
            this.pnlHeader.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(22)))), ((int)(((byte)(30)))));
            this.pnlHeader.Controls.Add(this.lblTitle);
            this.pnlHeader.Controls.Add(this.btnSettings);
            this.pnlHeader.Controls.Add(this.btnClear);
            this.pnlHeader.Controls.Add(this.btnClose);
            this.pnlHeader.Cursor = System.Windows.Forms.Cursors.SizeAll;
            this.pnlHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlHeader.Location = new System.Drawing.Point(0, 0);
            this.pnlHeader.Name = "pnlHeader";
            this.pnlHeader.Size = new System.Drawing.Size(400, 44);
            this.pnlHeader.TabIndex = 2;
            // 
            // lblTitle
            // 
            this.lblTitle.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI Semibold", 11F);
            this.lblTitle.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(292, 44);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = " AI Đại Nam — Điều hướng";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnSettings
            // 
            this.btnSettings.BackColor = System.Drawing.Color.Transparent;
            this.btnSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSettings.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSettings.FlatAppearance.BorderSize = 0;
            this.btnSettings.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(70)))));
            this.btnSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSettings.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnSettings.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnSettings.Location = new System.Drawing.Point(292, 0);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(36, 44);
            this.btnSettings.TabIndex = 3;
            this.btnSettings.Text = "⚙";
            this.btnSettings.UseVisualStyleBackColor = false;
            // 
            // btnClear
            // 
            this.btnClear.BackColor = System.Drawing.Color.Transparent;
            this.btnClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClear.FlatAppearance.BorderSize = 0;
            this.btnClear.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(58)))), ((int)(((byte)(70)))));
            this.btnClear.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font = new System.Drawing.Font("Segoe UI", 11F);
            this.btnClear.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClear.Location = new System.Drawing.Point(328, 0);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(36, 44);
            this.btnClear.TabIndex = 2;
            this.btnClear.Text = "🗑";
            this.btnClear.UseVisualStyleBackColor = false;
            // 
            // btnClose
            // 
            this.btnClose.BackColor = System.Drawing.Color.Transparent;
            this.btnClose.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnClose.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClose.FlatAppearance.BorderSize = 0;
            this.btnClose.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(220)))), ((int)(((byte)(38)))), ((int)(((byte)(38)))));
            this.btnClose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnClose.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.btnClose.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(148)))), ((int)(((byte)(163)))), ((int)(((byte)(184)))));
            this.btnClose.Location = new System.Drawing.Point(364, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(36, 44);
            this.btnClose.TabIndex = 1;
            this.btnClose.Text = "✕";
            this.btnClose.UseVisualStyleBackColor = false;
            // 
            // pnlBody
            // 
            this.pnlBody.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.pnlBody.Controls.Add(this.rtbChatLog);
            this.pnlBody.Controls.Add(this.lblTyping);
            this.pnlBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBody.Location = new System.Drawing.Point(0, 44);
            this.pnlBody.Name = "pnlBody";
            this.pnlBody.Padding = new System.Windows.Forms.Padding(8, 4, 8, 4);
            this.pnlBody.Size = new System.Drawing.Size(400, 448);
            this.pnlBody.TabIndex = 3;
            // 
            // rtbChatLog
            // 
            this.rtbChatLog.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(232)))), ((int)(((byte)(240)))));
            this.rtbChatLog.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtbChatLog.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbChatLog.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.rtbChatLog.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.rtbChatLog.Location = new System.Drawing.Point(8, 4);
            this.rtbChatLog.Name = "rtbChatLog";
            this.rtbChatLog.ReadOnly = true;
            this.rtbChatLog.Size = new System.Drawing.Size(384, 418);
            this.rtbChatLog.TabIndex = 1;
            this.rtbChatLog.Text = "";
            // 
            // lblTyping
            // 
            this.lblTyping.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblTyping.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Italic);
            this.lblTyping.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(32)))), ((int)(((byte)(40)))));
            this.lblTyping.Location = new System.Drawing.Point(8, 422);
            this.lblTyping.Name = "lblTyping";
            this.lblTyping.Padding = new System.Windows.Forms.Padding(4, 0, 0, 0);
            this.lblTyping.Size = new System.Drawing.Size(384, 22);
            this.lblTyping.TabIndex = 0;
            this.lblTyping.Text = "⏳ Đang suy nghĩ...";
            this.lblTyping.Visible = false;
            // 
            // pnlInputBar
            // 
            this.pnlInputBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(20)))), ((int)(((byte)(22)))), ((int)(((byte)(30)))));
            this.pnlInputBar.Controls.Add(this.pnlTxtWrap);
            this.pnlInputBar.Controls.Add(this.btnSend);
            this.pnlInputBar.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlInputBar.Location = new System.Drawing.Point(0, 492);
            this.pnlInputBar.Name = "pnlInputBar";
            this.pnlInputBar.Padding = new System.Windows.Forms.Padding(8, 6, 8, 6);
            this.pnlInputBar.Size = new System.Drawing.Size(400, 48);
            this.pnlInputBar.TabIndex = 1;
            // 
            // pnlTxtWrap
            // 
            this.pnlTxtWrap.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(46)))), ((int)(((byte)(56)))));
            this.pnlTxtWrap.Controls.Add(this.txtInput);
            this.pnlTxtWrap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlTxtWrap.Location = new System.Drawing.Point(8, 6);
            this.pnlTxtWrap.Name = "pnlTxtWrap";
            this.pnlTxtWrap.Padding = new System.Windows.Forms.Padding(8, 4, 4, 4);
            this.pnlTxtWrap.Size = new System.Drawing.Size(344, 36);
            this.pnlTxtWrap.TabIndex = 1;
            // 
            // txtInput
            // 
            this.txtInput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(46)))), ((int)(((byte)(56)))));
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInput.Font = new System.Drawing.Font("Segoe UI", 10.5F);
            this.txtInput.ForeColor = System.Drawing.Color.White;
            this.txtInput.Location = new System.Drawing.Point(8, 4);
            this.txtInput.Name = "txtInput";
            this.txtInput.Size = new System.Drawing.Size(332, 19);
            this.txtInput.TabIndex = 0;
            // 
            // btnSend
            // 
            this.btnSend.BackColor = System.Drawing.Color.Gold;
            this.btnSend.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnSend.FlatAppearance.BorderSize = 0;
            this.btnSend.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.btnSend.ForeColor = System.Drawing.Color.White;
            this.btnSend.Location = new System.Drawing.Point(352, 6);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(40, 36);
            this.btnSend.TabIndex = 0;
            this.btnSend.Text = "↑";
            this.btnSend.UseVisualStyleBackColor = false;
            // 
            // AIChatPanel
            // 
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.Controls.Add(this.pnlBody);
            this.Controls.Add(this.pnlInputBar);
            this.Controls.Add(this.pnlHeader);
            this.Name = "AIChatPanel";
            this.Size = new System.Drawing.Size(400, 540);
            this.pnlHeader.ResumeLayout(false);
            this.pnlBody.ResumeLayout(false);
            this.pnlInputBar.ResumeLayout(false);
            this.pnlTxtWrap.ResumeLayout(false);
            this.pnlTxtWrap.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlHeader;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.Panel pnlBody;
        private System.Windows.Forms.RichTextBox rtbChatLog;
        private System.Windows.Forms.Label lblTyping;
        private System.Windows.Forms.Panel pnlInputBar;
        private System.Windows.Forms.Panel pnlTxtWrap;
        private System.Windows.Forms.TextBox txtInput;
        private System.Windows.Forms.Button btnSend;
    }
}
