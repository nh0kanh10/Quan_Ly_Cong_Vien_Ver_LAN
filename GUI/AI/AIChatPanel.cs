using System;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections.Generic;

namespace GUI.AI
{
    /// <summary>
    /// Floating Chat Panel — Logic only. UI ở AIChatPanel.Designer.cs
    /// </summary>
    public partial class AIChatPanel : UserControl
    {
        public event Action<string, Dictionary<string, object>> OnUICommandRequested;

        private readonly AIChatService _aiService = new AIChatService();
        private bool _isProcessing;
        private DateTime _lastSendTime = DateTime.MinValue;
        private const int DEBOUNCE_MS = 1500;

        public AIChatPanel()
        {
            InitializeComponent();
            WireEvents();

            AppendBotMessage("Xin chào! Tôi là trợ lý AI Đại Nam.\nBạn có thể hỏi tôi điều gì hoặc nói tên chức năng cần mở.");

            _aiService.OnProcessingChanged += (isProcessing) =>
            {
                if (InvokeRequired) { Invoke(new Action(() => SetTyping(isProcessing))); return; }
                SetTyping(isProcessing);
            };

            _aiService.OnOpenFormRequested += (formName) =>
            {
                if (InvokeRequired) { Invoke(new Action(() => HandleOpenForm(formName))); return; }
                HandleOpenForm(formName);
            };

            _aiService.OnUICommandRequested += (cmd, args) =>
            {
                if (InvokeRequired) { Invoke(new Action(() => OnUICommandRequested?.Invoke(cmd, args))); return; }
                OnUICommandRequested?.Invoke(cmd, args);
            };
        }

        // 
        //  PUBLIC API
        // 

        public void SwitchContext(string contextName, string description)
        {
            _aiService.SwitchContext(contextName, description);
            string ctx = string.IsNullOrEmpty(contextName) || contextName == "navigation"
                ? "Điều hướng" : contextName.Replace("frm", "");
            lblTitle.Text = $"   AI Đại Nam — {ctx}";
        }

        public void ClearChat()
        {
            rtbChatLog.Clear();
            _aiService.ClearHistory();
            AppendBotMessage("Đã xoá lịch sử. Tôi sẵn sàng giúp bạn!");
        }

        // ══════════════════════════════════════════════════════════════
        //  EVENT WIRING
        // ══════════════════════════════════════════════════════════════

        private void WireEvents()
        {
            btnSend.Click += async (s, e) => await DoSend();
            btnClose.Click += (s, e) => this.Visible = false;
            btnClear.Click += (s, e) => ClearChat();
            btnSettings.Click += (s, e) => OpenSettings();

            txtInput.KeyDown += async (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    e.SuppressKeyPress = true;
                    await DoSend();
                }
            };

            // Header drag
            SetupDrag(pnlHeader);
            SetupDrag(lblTitle);
        }

        private void SetupDrag(Control ctrl)
        {
            Point start = Point.Empty;
            bool dragging = false;
            ctrl.MouseDown += (s, e) => { dragging = true; start = e.Location; };
            ctrl.MouseMove += (s, e) => { if (dragging) { this.Left += e.X - start.X; this.Top += e.Y - start.Y; } };
            ctrl.MouseUp += (s, e) => { dragging = false; };
        }

        // ══════════════════════════════════════════════════════════════
        //  SEND MESSAGE
        // ══════════════════════════════════════════════════════════════

        private async Task DoSend()
        {
            string msg = txtInput.Text.Trim();
            if (string.IsNullOrEmpty(msg) || _isProcessing) return;

            if ((DateTime.Now - _lastSendTime).TotalMilliseconds < DEBOUNCE_MS) return;
            _lastSendTime = DateTime.Now;

            if (!AIConfig.HasApiKey())
            {
                OpenSettings();
                return;
            }

            // Lock ngay tại đây để chặn double-send
            _isProcessing = true;
            btnSend.Enabled = false;

            AppendUserMessage(msg);
            txtInput.Clear();
            txtInput.Focus();

            try
            {
                var response = await _aiService.SendMessage(msg);
                if (response != null && !string.IsNullOrEmpty(response.Text))
                    AppendBotMessage(response.Text);
            }
            finally
            {
                _isProcessing = false;
                btnSend.Enabled = true;
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  CHAT RENDERING
        // ══════════════════════════════════════════════════════════════

        private void AppendBotMessage(string text) =>
            AppendMessage("AI: ", text, Color.FromArgb(56, 189, 248), Color.FromArgb(30, 32, 40));

        private void AppendUserMessage(string text) =>
            AppendMessage("Bạn: ", text, Color.FromArgb(212, 175, 55), Color.FromArgb(30, 32, 40));

        private void AppendMessage(string prefix, string text, Color prefixColor, Color textColor)
        {
            rtbChatLog.SelectionStart = rtbChatLog.TextLength;
            rtbChatLog.SelectionColor = prefixColor;
            rtbChatLog.SelectionFont = new Font("Segoe UI", 10f, FontStyle.Bold);
            rtbChatLog.AppendText(prefix);

            rtbChatLog.SelectionColor = textColor;
            rtbChatLog.SelectionFont = new Font("Segoe UI", 10f);
            rtbChatLog.AppendText(text + "\n\n");
            rtbChatLog.ScrollToCaret();
        }

        private void SetTyping(bool isTyping)
        {
            _isProcessing = isTyping;
            lblTyping.Visible = isTyping;
            btnSend.Enabled = !isTyping;
            txtInput.Enabled = !isTyping;
        }

        // ══════════════════════════════════════════════════════════════
        //  HANDLERS
        // ══════════════════════════════════════════════════════════════

        private void HandleOpenForm(string formName)
        {
            try { Form1.Instance?.NavigateToFormByAI(formName); }
            catch (Exception ex) { AppendBotMessage($"Không thể mở form: {ex.Message}"); }
        }

        private void OpenSettings()
        {
            using (var frm = new frmAISettings())
            {
                ThemeManager.ShowAsPopup(frm);
            }
        }

        // ══════════════════════════════════════════════════════════════
        //  PAINT
        // ══════════════════════════════════════════════════════════════

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            using (var pen = new Pen(Color.FromArgb(60, 62, 75), 1))
                e.Graphics.DrawRectangle(pen, 0, 0, Width - 1, Height - 1);
        }


    }
}
