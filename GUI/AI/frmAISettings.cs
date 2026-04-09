using System;
using System.Drawing;
using System.Windows.Forms;
using FontAwesome.Sharp;

namespace GUI.AI
{
    /// <summary>
    /// Form cấu hình AI Settings — cho phép chỉnh sửa API Key, Model, MaxLoops...
    /// </summary>
    public class frmAISettings : Form
    {
        private TextBox txtApiKey, txtModel, txtMaxLoops, txtMaxRows, txtTimeout, txtMaxTurns;
        private Label lblFilePath;

        public frmAISettings()
        {
            InitUI();
            LoadValues();
        }

        private void InitUI()
        {
            this.Text = "Cấu hình AI Chatbox";
            this.Size = new Size(520, 420);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.FromArgb(30, 32, 40);
            this.ForeColor = Color.FromArgb(226, 232, 240);
            this.Font = new Font("Segoe UI", 10f);

            int y = 16;
            int lblW = 160;
            int txtW = 310;
            int gap = 42;

            // ── API Key ──
            AddLabel("Gemini API Key:", y, lblW);
            int btnPasteW = 32;
            txtApiKey = AddTextBox(y, lblW, txtW - btnPasteW - 8);
            txtApiKey.PasswordChar = '•';

            var btnPaste = new Button
            {
                Location = new Point(lblW + 16 + txtW - btnPasteW, y),
                Size = new Size(btnPasteW, 28),
                BackColor = Color.FromArgb(55, 58, 70),
                FlatStyle = FlatStyle.Flat,
                Image = IconHelper.GetBitmap(IconChar.Paste, Color.White, 14),
                Cursor = Cursors.Hand
            };
            btnPaste.FlatAppearance.BorderSize = 0;
            btnPaste.Click += (s, e) => { if (Clipboard.ContainsText()) txtApiKey.Text = Clipboard.GetText(); };
            this.Controls.Add(btnPaste);

            y += gap;

            // ── Model ──
            AddLabel("Model:", y, lblW);
            txtModel = AddTextBox(y, lblW, txtW);
            y += gap;

            // ── Max ReAct Loops ──
            AddLabel("Max ReAct Loops:", y, lblW);
            txtMaxLoops = AddTextBox(y, lblW, 80);
            y += gap;

            // ── Max Data Rows ──
            AddLabel("Max Data Rows:", y, lblW);
            txtMaxRows = AddTextBox(y, lblW, 80);
            y += gap;

            // ── Timeout ──
            AddLabel("Timeout (giây):", y, lblW);
            txtTimeout = AddTextBox(y, lblW, 80);
            y += gap;

            // ── Max Turns ──
            AddLabel("Max Chat Turns:", y, lblW);
            txtMaxTurns = AddTextBox(y, lblW, 80);
            y += gap;

            // ── File path info ──
            lblFilePath = new Label
            {
                Text = $" File: {AIConfig.SettingsFilePath}",
                ForeColor = Color.FromArgb(120, 130, 150),
                Font = new Font("Segoe UI", 8.5f),
                Location = new Point(16, y),
                AutoSize = true,
                Cursor = Cursors.Hand
            };
            lblFilePath.Click += (s, e) =>
            {
                try { System.Diagnostics.Process.Start("notepad.exe", AIConfig.SettingsFilePath); }
                catch { }
            };
            this.Controls.Add(lblFilePath);
            y += 30;

            // ── Save Button ──
            var btnSave = new Button
            {
                Text = "  Lưu cấu hình",
                Image = IconHelper.GetBitmap(IconChar.Save, Color.White, 16),
                TextImageRelation = TextImageRelation.ImageBeforeText,
                Location = new Point(16, y),
                Size = new Size(txtW + lblW, 40),
                BackColor = Color.FromArgb(212, 175, 55),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI Semibold", 11f),
                Cursor = Cursors.Hand
            };
            btnSave.FlatAppearance.BorderSize = 0;
            btnSave.Click += BtnSave_Click;
            this.Controls.Add(btnSave);
        }

        private void LoadValues()
        {
            AIConfig.Reload();
            txtApiKey.Text = AIConfig.GeminiApiKey;
            txtModel.Text = AIConfig.Model;
            txtMaxLoops.Text = AIConfig.MaxReActLoops.ToString();
            txtMaxRows.Text = AIConfig.MaxDataRows.ToString();
            txtTimeout.Text = AIConfig.TimeoutSeconds.ToString();
            txtMaxTurns.Text = AIConfig.MaxConversationTurns.ToString();
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
            AIConfig.Set("GeminiApiKey", txtApiKey.Text.Trim());
            AIConfig.Set("Model", txtModel.Text.Trim());

            if (int.TryParse(txtMaxLoops.Text, out int loops)) AIConfig.Set("MaxReActLoops", loops);
            if (int.TryParse(txtMaxRows.Text, out int rows)) AIConfig.Set("MaxDataRows", rows);
            if (int.TryParse(txtTimeout.Text, out int timeout)) AIConfig.Set("TimeoutSeconds", timeout);
            if (int.TryParse(txtMaxTurns.Text, out int turns)) AIConfig.Set("MaxConversationTurns", turns);

            AIConfig.Save();
            TDCMessageBox.Show("Đã lưu cấu hình AI thành công!", "Thành công");
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        // ══════════════════════════════════════════════════════════════
        //  HELPERS
        // ══════════════════════════════════════════════════════════════

        private void AddLabel(string text, int y, int width)
        {
            var lbl = new Label
            {
                Text = text,
                Location = new Point(16, y + 4),
                Size = new Size(width, 24),
                ForeColor = Color.FromArgb(180, 190, 210)
            };
            this.Controls.Add(lbl);
        }

        private TextBox AddTextBox(int y, int x, int width)
        {
            var txt = new TextBox
            {
                Location = new Point(x + 16, y),
                Size = new Size(width, 28),
                Font = new Font("Consolas", 10f),
                BackColor = Color.FromArgb(44, 46, 56),
                ForeColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            this.Controls.Add(txt);
            return txt;
        }
    }
}
