using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;
using GUI.Infrastructure;

namespace GUI.AI
{
    // Cấu hình AI. Lưu vào file ai_settings.json tại thư mục app.
    // Hỗ trợ đa ngôn ngữ: AI tự trả lời theo ngôn ngữ người dùng đang chọn.
    public static class AIConfig
    {
        private static readonly JavaScriptSerializer _json = new JavaScriptSerializer();
        private static readonly string _settingsPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ai_settings.json");
        private static Dictionary<string, object> _cache;

        // Các thuộc tính cấu hình
        public static string GeminiApiKey => GetString("GeminiApiKey", "");
        public static string Model => GetString("Model", "gemini-2.5-flash");
        public static int MaxReActLoops => GetInt("MaxReActLoops", 5);
        public static int MaxDataRows => GetInt("MaxDataRows", 50);
        public static int TimeoutSeconds => GetInt("TimeoutSeconds", 20);
        public static int MaxConversationTurns => GetInt("MaxConversationTurns", 20);
        public static int RetryCount => GetInt("RetryCount", 1);
        public static int RetryDelayMs => GetInt("RetryDelayMs", 2000);

        public static string Endpoint =>
            $"https://generativelanguage.googleapis.com/v1beta/models/{Model}:generateContent?key={GeminiApiKey}";

        public static string SettingsFilePath => _settingsPath;

        // Ngôn ngữ hiện tại của user (vi-VN, en-US, zh-CN)
        public static string SystemLanguage => SessionManager.CurrentLanguage ?? "vi-VN";

        // Tên ngôn ngữ gọn cho system prompt
        public static string LanguageLabel
        {
            get
            {
                switch (SystemLanguage)
                {
                    case "en-US": return "English";
                    case "zh-CN": return "中文";
                    default: return "Tiếng Việt";
                }
            }
        }

        public static bool HasApiKey() => !string.IsNullOrWhiteSpace(GeminiApiKey);

        public static void SaveApiKey(string key)
        {
            Set("GeminiApiKey", key);
            Save();
        }

        public static string GetString(string key, string defaultVal)
        {
            EnsureLoaded();
            if (_cache.TryGetValue(key, out object val) && val != null)
                return val.ToString();
            return defaultVal;
        }

        public static int GetInt(string key, int defaultVal)
        {
            string s = GetString(key, null);
            if (s != null && int.TryParse(s, out int result)) return result;
            return defaultVal;
        }

        public static void Set(string key, object value)
        {
            EnsureLoaded();
            _cache[key] = value;
        }

        public static void Save()
        {
            EnsureLoaded();
            string json = _json.Serialize(_cache);
            File.WriteAllText(_settingsPath, json);
        }

        public static void Reload()
        {
            _cache = null;
            EnsureLoaded();
        }

        private static void EnsureLoaded()
        {
            if (_cache != null) return;

            if (File.Exists(_settingsPath))
            {
                try
                {
                    string json = File.ReadAllText(_settingsPath);
                    _cache = _json.Deserialize<Dictionary<string, object>>(json);
                }
                catch { _cache = new Dictionary<string, object>(); }
            }
            else
            {
                _cache = new Dictionary<string, object>
                {
                    ["GeminiApiKey"] = "",
                    ["Model"] = "gemini-2.5-flash",
                    ["MaxReActLoops"] = 5,
                    ["MaxDataRows"] = 50,
                    ["TimeoutSeconds"] = 20,
                    ["MaxConversationTurns"] = 20,
                    ["RetryCount"] = 1,
                    ["RetryDelayMs"] = 2000
                };
                Save();
            }
        }
    }
}
