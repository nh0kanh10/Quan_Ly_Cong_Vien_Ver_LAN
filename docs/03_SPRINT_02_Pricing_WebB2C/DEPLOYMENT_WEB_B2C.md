# TÀI LIỆU CHI TIẾT: TRIỂN KHAI WEB B2C LÊN VERCEL VÀ CẤU HÌNH TUNNEL
**Ngày thực hiện:** 09/04/2026
**Mục tiêu:** Đưa hệ thống Frontend (Blazor WebAssembly) lên Internet (Vercel) và kết nối an toàn về Local API/Database thông qua Tunneling.

---

## 1. Vấn Đề Gặp Phải (Quá Trình Debug)
Khi đưa Blazor WebAssembly lên Vercel bằng công cụ dòng lệnh (Vercel CLI), hệ thống gặp lỗi **404 Not Found** đối với file `blazor.webassembly.js`.

**Nguyên nhân gốc rễ (Root Cause):**
1. Cơ chế bảo mật mặc định của máy chủ Vercel tự động ẩn/bỏ qua toàn bộ các thư mục bắt đầu bằng dấu gạch dưới `_` (như thư mục `_framework` của hệ thống lõi Blazor).
2. Khi trình duyệt của người dùng yêu cầu file `_framework/blazor.webassembly.js`, Edge Network của Vercel không tìm thấy file này (do đã bị cắt bỏ lúc deploy), dẫn đến việc ứng dụng SPA không thể khởi động.

---

## 2. Giải Pháp Khắc Phục (Solution)

### 2.1. Cấu Hình Lại SPA Routing (`vercel.json`)
Bổ sung cấu hình `vercel.json` định tuyến cho ứng dụng Single-Page Application (SPA) của Blazor:
- Bật tính năng `cleanUrls`.
- Bắt mọi request lỗi 404 không tồn tại trả về `index.html` của Blazor để Blazor Router nội bộ tiếp quản.

### 2.2. Xây Dựng Kịch Bản Deploy Chuyên Dụng (`deploy.bat` PRO MAX V2)
Thay vì chọc sâu vào cấu hình webpack hay MSBuild của .NET, giải pháp được đưa ra là bẻ khóa ở cấp độ file tĩnh ngay trước khi đẩy lên Vercel. 
Kịch bản `deploy.bat` thực hiện chuỗi hành động sau:
1. **Xóa sạch bộ đệm publish cũ:** Dọn dẹp triệt để thư mục Output.
2. **Build Release (.NET Publish):** Dùng MSBuild công bố tĩnh toàn bộ Frontend.
3. **PowerShell Rename (Hack Vercel):** 
   - Đổi tên thư mục `_framework` thành `framework` (mất dấu gạch dưới).
   - Tự động dùng Regex dò quét và cập nhật lại đường dẫn nội bộ trong tất cả file tĩnh (`index.html`, `blazor.boot.json`...) từ `_framework/` sang `framework/`. Việc sử dụng `[System.IO.File]` thay cho `Get-Content`/`Set-Content` giúp xử lý hàng vạn file trong tích tắc mà không hỏng Encoding UTF-8.
4. **Phục Hồi Project Link:** Sao chép thư mục `.vercel` từ nguồn để đảm bảo bản build luôn đẩy chính xác vào tên miền `dainam-web-portal.vercel.app` ban đầu mà không bị sinh ra các tên miền rác (`wwwroot-xyz.vercel.app`).
5. **Đẩy lên Vercel:** Chạy lệnh `npx vercel --prod --yes`.

---

## 3. Cấu Hình Kết Nối API & Trạng Thái Hiện Tại
- **Trạng thái Frontend:** Đã lên sóng toàn cầu tại `https://dainam-web-portal.vercel.app/`. Truy cập mượt mà, tải nhanh (Zero-404).
- **Trạng thái API:** Hiện đang ở trạng thái kiểm thử ngẫu nhiên bằng tính năng **Quick Tunnel** của Cloudflare (Mỗi lần bật `run.bat` sẽ sinh ra một URL mới kiểu `https://[xyz].trycloudflare.com`).

Ngay khi Frontend lên mạng, hệ thống gặp lỗi `ERR_NAME_NOT_RESOLVED` trong màn hình đăng nhập. Lỗi này xác nhận Frontend gọi API rất chuẩn, nhưng URL của API đã bị chết do được sinh ngẫu nhiên từ ca làm việc trước.

---

## 4. Hành Động Tiếp Theo (Next Steps)
Chuyển đổi từ mô hình Tunnel "ngẫu nhiên" (Cloudflare Quick Tunnel) sang mô hình Tunnel "cố định" sử dụng dịch vụ của **Ngrok**. 
Mục đích: Không bao giờ bị mất kết nối Web B2C -> API nội bộ kể cả khi cúp điện, khởi động lại server. Đạt chuẩn mô hình **OMO (Online Merge Offline)**.
