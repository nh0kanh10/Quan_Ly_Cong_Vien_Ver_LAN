using System.Globalization;
using System.Resources;

namespace GUI.Infrastructure
{
    public static class LanguageManager
    {
        // Sử dụng ResourceManager chuẩn của Microsoft đọc trực tiếp từ các file .resx
        private static readonly ResourceManager rm = new ResourceManager("GUI.Properties.UIStrings", typeof(LanguageManager).Assembly);

        public static string GetString(string key)
        {
            string lang = SessionManager.CurrentLanguage;
            CultureInfo culture;
            
            try 
            {
                culture = new CultureInfo(lang);
            }
            catch 
            {
                culture = new CultureInfo("vi-VN");
            }

            string translated = rm.GetString(key, culture);
            
            return string.IsNullOrEmpty(translated) ? key : translated;
        }
    }
}
