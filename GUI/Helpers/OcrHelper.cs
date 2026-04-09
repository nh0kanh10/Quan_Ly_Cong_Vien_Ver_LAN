using System;
using System.Drawing;
using System.IO;
using System.Text.RegularExpressions;
using Tesseract;

namespace GUI
{
    /// <summary>
    /// Trích xuất biển số xe từ ảnh sử dụng thư viện Tesseract (đã gói kèm trong app).
    /// Hỗ trợ đọc offline qua model `eng.traineddata` trong thư mục `tessdata`.
    /// </summary>
    public static class OcrHelper
    {
        private static string TessDataPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "tessdata");

        /// <summary>
        /// Đọc text từ ảnh bằng Tesseract Engine.
        /// </summary>
        public static string RecognizeText(string imagePath)
        {
            try
            {
                if (string.IsNullOrEmpty(imagePath) || !File.Exists(imagePath))
                    return null;

                // Cần file eng.traineddata trong thư mục tessdata
                if (!Directory.Exists(TessDataPath) || !File.Exists(Path.Combine(TessDataPath, "eng.traineddata")))
                {
                    return null;
                }

                using (var engine = new TesseractEngine(TessDataPath, "eng", EngineMode.Default))
                {
                    // Cho phép chữ và số
                    engine.SetVariable("tessedit_char_whitelist", "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-.");
                    
                    using (var img = Pix.LoadFromFile(imagePath))
                    {
                        // 1. Scale up 2x (làm to ảnh để dễ đọc)
                        using (var scaledImg = img.Scale(2.0f, 2.0f))
                        {
                            // 2. Chuyển sang Grayscale (trắng đen)
                            using (var grayImg = scaledImg.ConvertRGBToGray())
                            {
                                // 3. Đọc text rải rác
                                using (var page = engine.Process(grayImg, PageSegMode.SparseText))
                                {
                                    return page.GetText();
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Tesseract Engine error: " + ex.Message, ex);
            }
        }

        /// <summary>
        /// Trích xuất biển số xe Việt Nam từ raw OCR text.
        /// Dựa trên pattern: XX[A-Z][0-9]? - XXX.XX hoặc XXXXX
        /// </summary>
        public static string ExtractLicensePlate(string ocrText)
        {
            if (string.IsNullOrWhiteSpace(ocrText)) return null;

            // Normalize
            string cleaned = ocrText.Replace("\n", " ").Replace("\r", " ").Trim();

            // Nhóm 1: 2 số
            // Nhóm 2: 1-2 chữ (mã chữ cái tỉnh/loại xe) - có thể có gạch nối phía trước
            // Nhóm 3: Tùy chọn 1 số
            // Nhóm 4: Dãy số đuôi (3 -> 5 chữ số)
            // Nhóm 5: Tùy chọn số phẩy (như .45)
            var patterns = new[]
            {
                @"(\d{2})\s*[\-\.]?\s*([A-Z]{1,2})\s*(\d?)\s*[\-\.\s]*\s*(\d{3,5})\s*[\.\s]*(\d{0,2})"
            };

            foreach (var pattern in patterns)
            {
                var match = Regex.Match(cleaned, pattern, RegexOptions.IgnoreCase);
                if (match.Success)
                {
                    string part1 = match.Groups[1].Value;
                    string part2 = match.Groups[2].Value.ToUpper();
                    string part3 = match.Groups[3].Value;
                    string part4 = match.Groups[4].Value;
                    string part5 = match.Groups[5].Value;

                    string prefix = part1 + part2 + part3;
                    string suffix = part4;
                    if (!string.IsNullOrEmpty(part5))
                        suffix += "." + part5;

                    return prefix + "-" + suffix;
                }
            }

            // Fallback (nhận diện dãy mã dài)
            var fallback = Regex.Match(cleaned, @"([A-Z0-9][\sA-Z0-9\-\.]{4,}[A-Z0-9])", RegexOptions.IgnoreCase);
            if (fallback.Success)
            {
                return fallback.Groups[1].Value.Trim().ToUpper();
            }

            return null;
        }
    }
}
