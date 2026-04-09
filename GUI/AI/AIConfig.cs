using System;
using System.Collections.Generic;
using System.IO;
using System.Web.Script.Serialization;

namespace GUI.AI
{
    /// <summary>
    /// Cấu hình AI — Lưu vào file JSON trong thư mục app, dễ đọc/sửa.
    /// File: {AppDir}\ai_settings.json
    /// </summary>
    public static class AIConfig
    {
        private static readonly JavaScriptSerializer _json = new JavaScriptSerializer();

        private static readonly string _settingsPath =
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ai_settings.json");

        private static Dictionary<string, object> _cache;

        // ══════════════════════════════════════════════════════════════
        //  PUBLIC PROPERTIES
        // ══════════════════════════════════════════════════════════════

        public static string GeminiApiKey => GetString("GeminiApiKey", "");
        public static string Model => GetString("Model", "gemini-2.5-flash");
        public static int MaxReActLoops => GetInt("MaxReActLoops", 3);
        public static int MaxDataRows => GetInt("MaxDataRows", 50);
        public static int TimeoutSeconds => GetInt("TimeoutSeconds", 15);
        public static int MaxConversationTurns => GetInt("MaxConversationTurns", 20);

        public static string Endpoint =>
            $"https://generativelanguage.googleapis.com/v1beta/models/{Model}:generateContent?key={GeminiApiKey}";

        public static string SettingsFilePath => _settingsPath;

        // ══════════════════════════════════════════════════════════════
        //  QUICK HELPERS
        // ══════════════════════════════════════════════════════════════

        public static bool HasApiKey() => !string.IsNullOrWhiteSpace(GeminiApiKey);
        public static string GetApiKey() => GeminiApiKey;

        public static void SaveApiKey(string key)
        {
            Set("GeminiApiKey", key);
            Save();
        }

        // ══════════════════════════════════════════════════════════════
        //  GENERIC GET / SET
        // ══════════════════════════════════════════════════════════════

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

        // ══════════════════════════════════════════════════════════════
        //  FILE I/O
        // ══════════════════════════════════════════════════════════════

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
                    ["Model"] = "gemini-2.0-flash",
                    ["MaxReActLoops"] = 3,
                    ["MaxDataRows"] = 50,
                    ["TimeoutSeconds"] = 15,
                    ["MaxConversationTurns"] = 20
                };
                Save();
            }
        }
    }
}
