using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using GUI.Infrastructure;

namespace GUI.AI
{
    // Panel chat AI nổi trên giao diện chính.
    // Cho phép người dùng hỏi đáp, lọc grid, xem hướng dẫn qua AI.
    public partial class AIChatPanel : XtraUserControl
    {
        private AIChatService _service;
        private CancellationTokenSource _cts;
        private DateTime _lastSendTime = DateTime.MinValue;
        private bool _isDragging;
        private Point _dragStart;
        private System.Windows.Forms.Timer _typingTimer;
        private int _typingDots;

        // Sự kiện để frmMain lắng nghe lệnh UI từ AI
        public event Action<string, Dictionary<string, object>> OnUICommandRequested;

        // Truy cập service để frmMain hook thêm event
        public AIChatService Service => _service;

        public AIChatPanel()
        {
            InitializeComponent();

            _service = new AIChatService();

            // Timer hiệu ứng "đang xử lý..."
            _typingTimer = new System.Windows.Forms.Timer { Interval = 400 };
            _typingTimer.Tick += TypingTimer_Tick;

            GanSuKien();
            DichNgonNgu();
            _onLanguageChanged = _ => DichNgonNgu();
            EventBus.Subscribe("LanguageChanged", _onLanguageChanged);
            this.HandleDestroyed += AIChatPanel_HandleDestroyed;
        }

        private readonly Action<object> _onLanguageChanged;

        // Dịch tất cả text UI theo ngôn ngữ hiện tại
        private void DichNgonNgu()
        {
            lblTitle.Text = LanguageManager.GetString("AI_TITLE") ?? "AI Assistant";
            btnSend.Text = LanguageManager.GetString("AI_BTN_SEND") ?? "Gửi";
            txtInput.Properties.NullValuePrompt = LanguageManager.GetString("AI_INPUT_HINT") ?? "Nhập câu hỏi...";
        }

        // Gắn sự kiện cho các nút và service
        private void GanSuKien()
        {
            btnSend.Click += BtnSend_Click;
            txtInput.KeyDown += TxtInput_KeyDown;

            btnClose.Click += BtnClose_Click;
            btnClear.Click += BtnClear_Click;
            btnSettings.Click += BtnSettings_Click;

            // Kéo thả header để di chuyển panel
            pnlHeader.MouseDown += PnlHeader_MouseDown;
            pnlHeader.MouseMove += PnlHeader_MouseMove;
            pnlHeader.MouseUp += PnlHeader_MouseUp;

            lblTitle.MouseDown += LblTitle_MouseDown;
            lblTitle.MouseMove += LblTitle_MouseMove;
            lblTitle.MouseUp += LblTitle_MouseUp;

            // Hiện/ẩn indicator khi AI xử lý
            _service.OnProcessingChanged += Service_OnProcessingChanged;

            // Chuyển tiếp lệnh UI từ AI ra ngoài
            _service.OnUICommandRequested += Service_OnUICommandRequested;
        }

        private void TypingTimer_Tick(object sender, EventArgs e)
        {
            _typingDots = (_typingDots + 1) % 4;
            string typingText = LanguageManager.GetString("AI_TYPING") ?? "AI đang xử lý";
            lblTyping.Text = "  " + typingText + new string('.', _typingDots);
        }

        private void AIChatPanel_HandleDestroyed(object sender, EventArgs e)
        {
            EventBus.Unsubscribe("LanguageChanged", _onLanguageChanged);
        }

        private async void BtnSend_Click(object sender, EventArgs e)
        {
            await GuiTinNhan();
        }

        private async void TxtInput_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.Handled = true;
                await GuiTinNhan();
            }
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void BtnClear_Click(object sender, EventArgs e)
        {
            txtLog.Text = "";
            _service.ClearHistory();
        }

        private void BtnSettings_Click(object sender, EventArgs e)
        {
            using (var frm = new frmAISettings()) { frm.ShowDialog(); }
        }

        private void PnlHeader_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { _isDragging = true; _dragStart = e.Location; }
        }

        private void PnlHeader_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && Parent != null) { Left += e.X - _dragStart.X; Top += e.Y - _dragStart.Y; }
        }

        private void PnlHeader_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        private void LblTitle_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left) { _isDragging = true; _dragStart = e.Location; }
        }

        private void LblTitle_MouseMove(object sender, MouseEventArgs e)
        {
            if (_isDragging && Parent != null) { Left += e.X - _dragStart.X; Top += e.Y - _dragStart.Y; }
        }

        private void LblTitle_MouseUp(object sender, MouseEventArgs e)
        {
            _isDragging = false;
        }

        private void Service_OnProcessingChanged(bool dangXuLy)
        {
            if (InvokeRequired)
                Invoke(new Action(() => CapNhatTrangThai(dangXuLy)));
            else
                CapNhatTrangThai(dangXuLy);
        }

        private void Service_OnUICommandRequested(string cmd, Dictionary<string, object> args)
        {
            OnUICommandRequested?.Invoke(cmd, args);
        }

        // Gửi tin nhắn cho AI
        private async Task GuiTinNhan()
        {
            string text = txtInput.Text?.Trim();
            if (string.IsNullOrEmpty(text)) return;

            // Chống gửi liên tục (1.5 giây)
            if ((DateTime.Now - _lastSendTime).TotalMilliseconds < 1500) return;
            _lastSendTime = DateTime.Now;

            // Kiểm tra API Key
            if (!AIConfig.HasApiKey())
            {
                string sysLabel = LanguageManager.GetString("AI_LABEL_SYSTEM") ?? "Hệ thống";
                string noKey = LanguageManager.GetString("AI_ERR_NO_KEY") ?? "Chưa có API Key. Mở Settings để thiết lập.";
                ThemDongChat(sysLabel, noKey);
                using (var frm = new frmAISettings()) { frm.ShowDialog(); }
                return;
            }

            string youLabel = LanguageManager.GetString("AI_LABEL_YOU") ?? "Bạn";

            ThemDongChat(youLabel, text);
            txtInput.Text = "";

            _cts?.Cancel();
            _cts = new CancellationTokenSource();

            var response = await _service.SendMessage(text, _cts.Token);
            ThemDongChat("AI", response.Text ?? "...");
        }

        // Thêm 1 dòng vào khung chat
        private void ThemDongChat(string nguoiGui, string noiDung)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => ThemDongChat(nguoiGui, noiDung)));
                return;
            }

            string youLabel = LanguageManager.GetString("AI_LABEL_YOU") ?? "Bạn";
            string sysLabel = LanguageManager.GetString("AI_LABEL_SYSTEM") ?? "Hệ thống";
            string prefix = nguoiGui == youLabel ? ">> " : nguoiGui == "AI" ? "AI: " : "[!] ";
            txtLog.Text += prefix + noiDung + "\r\n\r\n";
            txtLog.SelectionStart = txtLog.Text.Length;
            txtLog.ScrollToCaret();
        }

        // Bật/tắt hiệu ứng "AI đang xử lý"
        private void CapNhatTrangThai(bool dangXuLy)
        {
            lblTyping.Visible = dangXuLy;
            btnSend.Enabled = !dangXuLy;
            txtInput.Enabled = !dangXuLy;

            if (dangXuLy) { _typingDots = 0; _typingTimer.Start(); }
            else { _typingTimer.Stop(); lblTyping.Text = ""; }
        }

        // Đổi ngữ cảnh AI khi chuyển module
        public void SwitchContext(string contextName, string moTa, string[] goiY = null)
        {
            _service.SwitchContext(contextName, moTa);
            HienGoiY(goiY);
        }

        // Hiện các nút gợi ý câu hỏi
        private void HienGoiY(string[] danhSach)
        {
            if (InvokeRequired) { Invoke(new Action(() => HienGoiY(danhSach))); return; }

            pnlSuggestions.Controls.Clear();

            if (danhSach == null || danhSach.Length == 0)
            {
                pnlSuggestions.Visible = false;
                return;
            }

            int x = 6;
            int soLuong = Math.Min(danhSach.Length, 3);
            for (int i = 0; i < soLuong; i++)
            {
                string cauHoi = danhSach[i];
                var btn = new SimpleButton();
                btn.Text = cauHoi;
                btn.Font = new Font("Segoe UI", 8f);
                btn.Appearance.BackColor = Color.FromArgb(49, 50, 68);
                btn.Appearance.ForeColor = Color.FromArgb(166, 227, 161);
                btn.Appearance.BorderColor = Color.FromArgb(69, 71, 90);
                btn.Height = 24;
                btn.Width = TextRenderer.MeasureText(cauHoi, btn.Font).Width + 20;
                btn.Location = new Point(x, 6);
                btn.Click += async (s, e) => { txtInput.Text = cauHoi; await GuiTinNhan(); };
                pnlSuggestions.Controls.Add(btn);
                x += btn.Width + 6;
            }
            pnlSuggestions.Visible = true;
        }
    }
}
