using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DevExpress.XtraEditors;
using GUI.Infrastructure;

namespace GUI.AI
{
    // Form cấu hình API Key và Model cho AI.
    // Có nút Test Connection để kiểm tra key trước khi lưu.
    public partial class frmAISettings : XtraForm
    {
        public frmAISettings()
        {
            InitializeComponent();
            LoadSettings();
            DichNgonNgu();
            GanSuKien();
        }

        private void LoadSettings()
        {
            txtApiKey.Text = AIConfig.GeminiApiKey;
            txtModel.Text = AIConfig.Model;
        }

        private void GanSuKien()
        {
            btnSave.Click += (s, e) =>
            {
                AIConfig.Set("GeminiApiKey", txtApiKey.Text.Trim());
                AIConfig.Set("Model", txtModel.Text.Trim());
                AIConfig.Save();
                lblStatus.Text = LanguageManager.GetString("AI_SETTINGS_SAVED") ?? "Đã lưu!";
                lblStatus.Appearance.ForeColor = System.Drawing.Color.FromArgb(166, 227, 161);
            };

            btnTestConnection.Click += async (s, e) => await TestConnection();
        }

        // Dịch UI theo ngôn ngữ hiện tại
        private void DichNgonNgu()
        {
            this.Text = LanguageManager.GetString("AI_SETTINGS_TITLE") ?? "AI Settings";
            lblApiKey.Text = LanguageManager.GetString("AI_SETTINGS_APIKEY") ?? "Gemini API Key:";
            lblModel.Text = LanguageManager.GetString("AI_SETTINGS_MODEL") ?? "Model:";
            btnSave.Text = LanguageManager.GetString("AI_SETTINGS_SAVE") ?? "Lưu";
            btnTestConnection.Text = LanguageManager.GetString("AI_SETTINGS_TEST") ?? "Test Connection";
        }

        // Gửi 1 request nhỏ để kiểm tra API key có hoạt động không
        private async Task TestConnection()
        {
            string key = txtApiKey.Text.Trim();
            if (string.IsNullOrEmpty(key))
            {
                lblStatus.Text = LanguageManager.GetString("AI_SETTINGS_NO_KEY") ?? "Chưa nhập API Key.";
                lblStatus.Appearance.ForeColor = System.Drawing.Color.FromArgb(243, 139, 168);
                return;
            }

            lblStatus.Text = LanguageManager.GetString("AI_SETTINGS_TESTING") ?? "Đang kiểm tra...";
            lblStatus.Appearance.ForeColor = System.Drawing.Color.FromArgb(137, 180, 250);
            btnTestConnection.Enabled = false;

            try
            {
                string model = txtModel.Text.Trim();
                if (string.IsNullOrEmpty(model)) model = "gemini-2.5-flash";

                string url = $"https://generativelanguage.googleapis.com/v1beta/models/{model}:generateContent?key={key}";
                string body = "{\"contents\":[{\"parts\":[{\"text\":\"Hi\"}]}]}";

                using (var http = new HttpClient())
                {
                    http.Timeout = TimeSpan.FromSeconds(10);
                    var response = await http.PostAsync(url, new StringContent(body, Encoding.UTF8, "application/json"));

                    if (response.IsSuccessStatusCode)
                    {
                        lblStatus.Text = LanguageManager.GetString("AI_SETTINGS_OK") ?? "Kết nối thành công!";
                        lblStatus.Appearance.ForeColor = System.Drawing.Color.FromArgb(166, 227, 161);
                    }
                    else
                    {
                        string errPrefix = LanguageManager.GetString("AI_SETTINGS_ERR") ?? "Lỗi";
                        lblStatus.Text = $"{errPrefix}: {(int)response.StatusCode} {response.ReasonPhrase}";
                        lblStatus.Appearance.ForeColor = System.Drawing.Color.FromArgb(243, 139, 168);
                    }
                }
            }
            catch (Exception ex)
            {
                string errPrefix = LanguageManager.GetString("AI_SETTINGS_ERR") ?? "Lỗi";
                lblStatus.Text = errPrefix + ": " + ex.Message;
                lblStatus.Appearance.ForeColor = System.Drawing.Color.FromArgb(243, 139, 168);
            }
            finally
            {
                btnTestConnection.Enabled = true;
            }
        }
    }
}
