using System;
using System.Drawing;
using ZXing;
using ZXing.Common;
using ZXing.QrCode;

namespace GUI
{
    public static class BarcodeHelper
    {
        /// <summary>
        /// Tạo một hình ảnh QR Code từ chuỗi văn bản (VD: Mã vé xe)
        /// </summary>
        public static Bitmap GenerateQrCode(string content, int width = 300, int height = 300)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.QR_CODE,
                Options = new QrCodeEncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = 1
                }
            };

            return writer.Write(content);
        }

        /// <summary>
        /// Tạo một hình ảnh Barcode 1D (Mã vạch sọc đen trắng) dùng cho Sản Phẩm Siêu Thị
        /// Dùng chuẩn CODE_128 vì hỗ trợ cả số và chữ
        /// </summary>
        public static Bitmap Generate1DBarcode(string content, int width = 400, int height = 150)
        {
            var writer = new BarcodeWriter
            {
                Format = BarcodeFormat.CODE_128,
                Options = new EncodingOptions
                {
                    Height = height,
                    Width = width,
                    Margin = 2,
                    PureBarcode = false // false để có thể hiển thị dòng số siêu thị dưới mã vạch
                }
            };

            return writer.Write(content);
        }

        /// <summary>
        /// Đọc nội dung barcode/QR code từ một file ảnh
        /// </summary>
        public static string ReadBarcodeFromFile(string imagePath)
        {
            try
            {
                using (var bitmap = (Bitmap)Image.FromFile(imagePath))
                {
                    return ReadBarcode(bitmap);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi đọc file ảnh: " + ex.Message);
            }
        }

        /// <summary>
        /// Đọc nội dung barcode/QR code từ đối tượng Bitmap
        /// </summary>
        public static string ReadBarcode(Bitmap bitmap)
        {
            var reader = new BarcodeReader
            {
                AutoRotate = true,
                Options = new DecodingOptions
                {
                    TryHarder = true,
                    PossibleFormats = new[] { BarcodeFormat.QR_CODE, BarcodeFormat.CODE_128, BarcodeFormat.EAN_13 }
                }
            };

            var result = reader.Decode(bitmap);
            return result != null ? result.Text : string.Empty;
        }
    }
}
