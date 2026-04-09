using System;
using System.Drawing;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;
using ZXing;

namespace GUI
{
    /// <summary>
    /// Module quét mã vạch/QR code qua webcam laptop.
    /// Dùng chung cho frmKiemSoatVe và frmBanHang.
    /// Dependencies: ZXing.Net 0.16.x, AForge.Video, AForge.Video.DirectShow
    /// </summary>
    public class CameraScanner : IDisposable
    {
        private VideoCaptureDevice _camera;
        private FilterInfoCollection _devices;
        private PictureBox _preview;
        private Timer _scanTimer;
        private Bitmap _currentFrame;
        private readonly BarcodeReader _reader;
        private DateTime _lastScanTime = DateTime.MinValue;
        private string _lastScannedCode = "";
        private bool _isRunning;
        private readonly object _frameLock = new object();

        // Cooldown 2 giây giữa 2 lần quét cùng mã (anti-duplicate)
        private const int COOLDOWN_MS = 2000;
        // Decode mỗi 400ms (tránh CPU quá tải)
        private const int SCAN_INTERVAL_MS = 400;

        /// <summary>
        /// Callback khi decode thành công 1 mã barcode/QR.
        /// Trả về chuỗi text đã decode.
        /// </summary>
        public event Action<string> OnBarcodeDetected;

        /// <summary>
        /// Callback khi có lỗi camera (không tìm thấy webcam, etc.)
        /// </summary>
        public event Action<string> OnError;

        public bool IsRunning => _isRunning;

        public CameraScanner()
        {
            _reader = new BarcodeReader
            {
                AutoRotate = true,
                Options = new ZXing.Common.DecodingOptions
                {
                    TryHarder = true,
                    PossibleFormats = new[]
                    {
                        BarcodeFormat.QR_CODE,
                        BarcodeFormat.CODE_128,
                        BarcodeFormat.CODE_39,
                        BarcodeFormat.EAN_13,
                        BarcodeFormat.EAN_8,
                        BarcodeFormat.UPC_A
                    }
                }
            };
        }

        /// <summary>
        /// Khởi động camera và bắt đầu quét.
        /// </summary>
        /// <param name="preview">PictureBox hiển thị live feed</param>
        /// <param name="deviceIndex">Index webcam (0 = mặc định)</param>
        public bool Start(PictureBox preview, int deviceIndex = 0)
        {
            if (_isRunning) return true;

            _preview = preview;

            try
            {
                _devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
                if (_devices.Count == 0)
                {
                    OnError?.Invoke("Không tìm thấy webcam nào trên máy!");
                    return false;
                }

                if (deviceIndex >= _devices.Count)
                    deviceIndex = 0;

                _camera = new VideoCaptureDevice(_devices[deviceIndex].MonikerString);

                // Chọn resolution phù hợp (640x480 là đủ cho barcode)
                if (_camera.VideoCapabilities.Length > 0)
                {
                    VideoCapabilities bestCap = _camera.VideoCapabilities[0];
                    foreach (var cap in _camera.VideoCapabilities)
                    {
                        if (cap.FrameSize.Width == 640 && cap.FrameSize.Height == 480)
                        {
                            bestCap = cap;
                            break;
                        }
                        if (cap.FrameSize.Width <= 800 && cap.FrameSize.Width > bestCap.FrameSize.Width)
                            bestCap = cap;
                    }
                    _camera.VideoResolution = bestCap;
                }

                _camera.NewFrame += Camera_NewFrame;
                _camera.Start();

                // Timer decode định kỳ (không decode mỗi frame để tiết kiệm CPU)
                _scanTimer = new Timer { Interval = SCAN_INTERVAL_MS };
                _scanTimer.Tick += ScanTimer_Tick;
                _scanTimer.Start();

                _isRunning = true;
                return true;
            }
            catch (Exception ex)
            {
                OnError?.Invoke("Lỗi khởi động camera: " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Dừng camera và giải phóng tài nguyên.
        /// </summary>
        public void Stop()
        {
            _isRunning = false;

            if (_scanTimer != null)
            {
                _scanTimer.Stop();
                _scanTimer.Dispose();
                _scanTimer = null;
            }

            if (_camera != null && _camera.IsRunning)
            {
                _camera.SignalToStop();
                _camera.NewFrame -= Camera_NewFrame;
                _camera = null;
            }

            lock (_frameLock)
            {
                if (_currentFrame != null)
                {
                    _currentFrame.Dispose();
                    _currentFrame = null;
                }
            }

            if (_preview != null)
            {
                _preview.Image = null;
            }
        }

        /// <summary>
        /// Lấy danh sách tên webcam có sẵn trên máy.
        /// </summary>
        public string[] GetAvailableDevices()
        {
            var devices = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            var names = new string[devices.Count];
            for (int i = 0; i < devices.Count; i++)
                names[i] = devices[i].Name;
            return names;
        }

        // === PRIVATE ===

        private void Camera_NewFrame(object sender, NewFrameEventArgs eventArgs)
        {
            lock (_frameLock)
            {
                _currentFrame?.Dispose();
                _currentFrame = (Bitmap)eventArgs.Frame.Clone();
            }

            // Update PictureBox trên UI thread
            try
            {
                if (_preview != null && !_preview.IsDisposed && _preview.IsHandleCreated)
                {
                    _preview.BeginInvoke((MethodInvoker)(() =>
                    {
                        lock (_frameLock)
                        {
                            if (_currentFrame != null && _preview != null && !_preview.IsDisposed)
                            {
                                _preview.Image?.Dispose();
                                _preview.Image = (Bitmap)_currentFrame.Clone();
                            }
                        }
                    }));
                }
            }
            catch (ObjectDisposedException) { }
            catch (InvalidOperationException) { }
        }

        private void ScanTimer_Tick(object sender, EventArgs e)
        {
            Bitmap frameToScan = null;
            lock (_frameLock)
            {
                if (_currentFrame != null)
                    frameToScan = (Bitmap)_currentFrame.Clone();
            }

            if (frameToScan == null) return;

            try
            {
                var result = _reader.Decode(frameToScan);
                if (result != null && !string.IsNullOrEmpty(result.Text))
                {
                    string code = result.Text.Trim();

                    // Anti-duplicate: cùng mã trong 2 giây -> bỏ qua
                    if (code == _lastScannedCode &&
                        (DateTime.Now - _lastScanTime).TotalMilliseconds < COOLDOWN_MS)
                    {
                        return;
                    }

                    _lastScannedCode = code;
                    _lastScanTime = DateTime.Now;

                    // Fire event trên UI thread
                    if (_preview != null && _preview.IsHandleCreated)
                    {
                        _preview.BeginInvoke((MethodInvoker)(() =>
                        {
                            OnBarcodeDetected?.Invoke(code);
                        }));
                    }
                }
            }
            catch { /* Decode fail = frame không có barcode, bỏ qua */ }
            finally
            {
                frameToScan.Dispose();
            }
        }

        /// <summary>
        /// [PHƯƠNG ÁN B - DEMO]: Quét barcode/QR từ file ảnh thay vì camera.
        /// Mở hộp thoại chọn ảnh -> decode -> fire event.
        /// Dùng khi: không có webcam, DroidCam lag, hoặc demo bảo vệ đồ án.
        /// </summary>
        public void ScanFromFile()
        {
            using (var ofd = new OpenFileDialog())
            {
                ofd.Title = "Chọn ảnh Barcode / QR Code";
                ofd.Filter = "Ảnh|*.png;*.jpg;*.jpeg;*.bmp;*.gif|Tất cả|*.*";
                if (ofd.ShowDialog() != DialogResult.OK) return;

                try
                {
                    using (var bmp = new Bitmap(ofd.FileName))
                    {
                        var result = _reader.Decode(bmp);
                        if (result != null && !string.IsNullOrEmpty(result.Text))
                        {
                            string code = result.Text.Trim();
                            OnBarcodeDetected?.Invoke(code);
                        }
                        else
                        {
                            OnError?.Invoke("Không tìm thấy mã vạch/QR trong ảnh này!");
                        }
                    }
                }
                catch (Exception ex)
                {
                    OnError?.Invoke("Lỗi đọc ảnh: " + ex.Message);
                }
            }
        }

        public void Dispose()
        {
            Stop();
            _reader?.Options?.Hints?.Clear();
        }
    }
}
