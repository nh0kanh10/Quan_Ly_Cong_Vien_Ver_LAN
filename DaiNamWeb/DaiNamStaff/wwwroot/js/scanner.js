// Barcode scanner bằng camera (html5-qrcode)
let scanner = null;
let dotnetRef = null;

function initScanner(ref) {
    dotnetRef = ref;
    const el = document.getElementById('qr-reader');
    if (!el || !window.Html5Qrcode) return;

    scanner = new Html5Qrcode('qr-reader');
    scanner.start(
        { facingMode: 'environment' },
        { fps: 10, qrbox: { width: 280, height: 150 } },
        (decodedText) => {
            // Quét thành công, gửi mã về Blazor
            if (dotnetRef) {
                dotnetRef.invokeMethodAsync('OnBarcodeScanned', decodedText);
            }
            // Tạm dừng 2 giây tránh quét trùng liên tục
            scanner.pause(true);
            setTimeout(() => {
                try { scanner.resume(); } catch (e) { }
            }, 2000);
        },
        () => { }
    ).catch(err => console.warn('Camera error:', err));
}

function stopScanner() {
    if (scanner) {
        try { scanner.stop(); } catch (e) { }
        scanner = null;
    }
}
