function initQrScanner(dotnetHelper) {
    if (window.html5QrCode) {
        return; // Already initialized
    }
    
    // Create new instance of Html5Qrcode
    window.html5QrCode = new Html5Qrcode("reader");
    window.qrDotNetHelper = dotnetHelper;
}

function startScanner() {
    if (!window.html5QrCode) return;
    
    const config = { fps: 10, qrbox: { width: 250, height: 250 } };
    
    // Start scanning using rear camera (environment)
    window.html5QrCode.start(
        { facingMode: "environment" }, 
        config,
        (decodedText, decodedResult) => {
            // When a QR is successfully scanned, call the Blazor C# method
            if (window.qrDotNetHelper) {
                // Play a beep sound (optional, helps with UX)
                playBeep();
                window.qrDotNetHelper.invokeMethodAsync('OnQrCodeScanned', decodedText);
            }
        },
        (errorMessage) => {
            // Ignore ongoing read errors as they are just frame-by-frame missing QR
        }
    ).catch(err => {
        console.error("Lỗi khởi động Camera: ", err);
        alert("Không thể khởi động Camera. Vui lòng cấp quyền hoặc kiểm tra lại thiết bị.");
    });
}

function stopScanner() {
    if (window.html5QrCode) {
        window.html5QrCode.stop().then(ignore => {
            console.log("Scanner stopped.");
        }).catch(err => {
            console.error("Lỗi khi tắt Scanner: ", err);
        });
    }
}

function playBeep() {
    try {
        const audioCtx = new (window.AudioContext || window.webkitAudioContext)();
        const oscillator = audioCtx.createOscillator();
        const gainNode = audioCtx.createGain();
        oscillator.connect(gainNode);
        gainNode.connect(audioCtx.destination);
        oscillator.type = 'sine';
        oscillator.frequency.value = 800; // Beep frequency
        gainNode.gain.setValueAtTime(0.1, audioCtx.currentTime);
        oscillator.start(audioCtx.currentTime);
        oscillator.stop(audioCtx.currentTime + 0.1);
    } catch(e) { /* Ignore if Audio API not supported */ }
}

function generateQrCode(elementId, qrText) {
    document.getElementById(elementId).innerHTML = '';
    new QRCode(document.getElementById(elementId), {
        text: qrText,
        width: 100,
        height: 100,
        colorDark: '#0c1624',
        colorLight: '#ffffff',
        correctLevel: QRCode.CorrectLevel.M
    });
}

function scanImageFile(inputFileElement) {
    if (!inputFileElement || !inputFileElement.files || inputFileElement.files.length === 0) {
        return;
    }
    const file = inputFileElement.files[0];
    if (!window.html5QrCode) {
        window.html5QrCode = new Html5Qrcode("reader");
    }
    
    window.html5QrCode.scanFile(file, true)
    .then(decodedText => {
        if (window.qrDotNetHelper) {
            playBeep();
            window.qrDotNetHelper.invokeMethodAsync('OnQrCodeScanned', decodedText);
        }
        // clear input after scan
        inputFileElement.value = '';
    })
    .catch(err => {
        console.error("No QR found in image:", err);
        alert("Không tìm thấy mã QR trong ảnh này. Vui lòng chọn ảnh chứa mã rõ nét.");
        inputFileElement.value = '';
    });
}
