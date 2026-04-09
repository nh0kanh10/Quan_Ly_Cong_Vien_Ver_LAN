using System;
using System.Drawing;
using System.Windows.Forms;

namespace GUI.AI
{
    partial class AIChatPanel
    {
        // ══════════════════════════════════════════════════════════════
        //  UI CONTROLS
        // ══════════════════════════════════════════════════════════════
        private Panel pnlHeader;
        private Label lblTitle;
        private Button btnClose, btnClear, btnSettings;
        private RichTextBox rtbChatLog;
        private TextBox txtInput;
        private Button btnSend;
        private Label lblTyping;
        private Panel pnlInputBar;
        private Panel pnlBody;

        private void InitializeDesign()
        {
            this.Size = new Size(400, 540);
            this.BackColor = Color.WhiteSmoke;

            BuildHeader();
            BuildChatLog();
            BuildInputBar();

            // ── Assemble (order matters: last added = top in Dock) ──
            this.Controls.Add(pnlBody);
            this.Controls.Add(pnlInputBar);
            this.Controls.Add(pnlHeader);
        }

        // ══════════════════════════════════════════════════════════════
        //  HEADER
        // ══════════════════════════════════════════════════════════════

        private void BuildHeader()
        {
            pnlHeader = new Panel
            {
                Dock = DockStyle.Top,
                Height = 44,
                BackColor = Color.FromArgb(20, 22, 30),
                Cursor = Cursors.SizeAll
            };

            lblTitle = new Label
            {
                Text = " AI Đại Nam — Điều hướng",
                ForeColor = Color.FromArgb(226, 232, 240),
                Font = new Font("Segoe UI Semibold", 11f),
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            btnClose = CreateHeaderButton("✕", 36);
            btnClose.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 38, 38);

            btnClear = CreateHeaderButton("🗑", 36);
            btnClear.Font = new Font("Segoe UI", 11f);

            btnSettings = CreateHeaderButton("⚙", 36);
            btnSettings.Font = new Font("Segoe UI", 12f);

            pnlHeader.Controls.Add(lblTitle);
            pnlHeader.Controls.Add(btnClose);
            pnlHeader.Controls.Add(btnClear);
            pnlHeader.Controls.Add(btnSettings);
        }

        private Button CreateHeaderButton(string text, int width)
        {
            var btn = new Button
            {
                Text = text,
                FlatStyle = FlatStyle.Flat,
                ForeColor = Color.FromArgb(148, 163, 184),
                BackColor = Color.Transparent,
                Size = new Size(width, 44),
                Dock = DockStyle.Right,
                Cursor = Cursors.Hand,
                Font = new Font("Segoe UI", 12f)
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(55, 58, 70);
            return btn;
        }

        // ══════════════════════════════════════════════════════════════
        //  CHAT LOG AREA
        // ══════════════════════════════════════════════════════════════

        private void BuildChatLog()
        {
            pnlBody = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(8, 4, 8, 4),
                BackColor = Color.FromArgb(30, 32, 40)
            };

            rtbChatLog = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackColor = Color.FromArgb(226, 232, 240),
                ForeColor = Color.FromArgb(30, 32, 40),
                Font = new Font("Segoe UI", 10f),
                BorderStyle = BorderStyle.None,
                ScrollBars = RichTextBoxScrollBars.Vertical,
                WordWrap = true
            };

            lblTyping = new Label
            {
                Text = "⏳ Đang suy nghĩ...",
                ForeColor = Color.FromArgb(30, 32, 40),
                Font = new Font("Segoe UI", 9f, FontStyle.Italic),
                Dock = DockStyle.Bottom,
                Height = 22,
                Visible = false,
                Padding = new Padding(4, 0, 0, 0)
            };

            pnlBody.Controls.Add(rtbChatLog);
            pnlBody.Controls.Add(lblTyping);
        }

        // ══════════════════════════════════════════════════════════════
        //  INPUT BAR
        // ══════════════════════════════════════════════════════════════

        private void BuildInputBar()
        {
            pnlInputBar = new Panel
            {
                Dock = DockStyle.Bottom,
                Height = 48,
                BackColor = Color.FromArgb(20, 22, 30),
                Padding = new Padding(8, 6, 8, 6)
            };

            btnSend = new Button
            {
                Text = "↑",
                Dock = DockStyle.Right,
                Width = 40,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(212, 175, 55),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 14f, FontStyle.Bold),
                Cursor = Cursors.Hand
            };
            btnSend.FlatAppearance.BorderSize = 0;

            txtInput = new TextBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(44, 46, 56),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10.5f),
                BorderStyle = BorderStyle.None
            };

            var pnlTxtWrap = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(8, 4, 4, 4),
                BackColor = Color.FromArgb(44, 46, 56)
            };
            pnlTxtWrap.Controls.Add(txtInput);

            pnlInputBar.Controls.Add(btnSend);
            pnlInputBar.Controls.Add(pnlTxtWrap);
        }
    }
}
