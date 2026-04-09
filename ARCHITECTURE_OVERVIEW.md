# ARCHITECTURE OVERVIEW - UI, PERFORMANCE & DEPLOYMENT

## 1. Global Theme System (`ThemeManager.cs`)
- **Centralized Styling**: Toàn bộ UI (Colors, Fonts, BorderRadius) được quản lý tập trung.
- **Auto-Styling**: Tự động duyệt qua Controls trong Form để áp dụng Styles đồng nhất.
- **Modern Typography**: Ép hệ thống dùng `Segoe UI` hoặc `Tahoma` trên toàn bộ Grid/Label/Button để tăng độ sắc nét.
- **Flat Design Principle**: Chuyển dịch từ thiết kế bo góc (Radius) sang thiết kế phẳng (Flat) cho các nút chức năng để tăng tính đồng bộ.

## 2. Navigation Architecture (`Form1.cs`)
- **Modern Sidebar**: Sử dụng `Guna2Button` tối ưu (BorderRadius=0, Animated=false) để đạt tốc độ phản hồi tối đa (zero lag).
- **Custom MenuStrip**: Sử dụng `PremiumMenuRenderer` để hiện đại hóa thanh menu hệ thống.
- **Left-Accent Bars**: Hệ thống chỉ báo màu (Accent Colors) đại diện cho từng module chức năng (Blue: Tickets, Amber: Areas, Green: Games, etc.).

## 3. Performance Optimization (Form Caching)
- **Generic Pattern**: `OpenChildForm<T>(object sender) where T : Form, new()`.
- **Caching Mechanism**:
  - Mỗi form chức năng (Loại Vé, Khu Vực,...) chỉ được khởi tạo **một lần duy nhất**.
  - Các lần chuyển Tab sau chỉ sử dụng `Hide()` và `Show()`.
  - Kết quả: Phản hồi tức thì, không tốn tài nguyên RAM khởi tạo lại liên tục.

## 4. Resource Management
- **IconHelper**: Centralized point for FontAwesome icon bitmaps.
- **Double Buffering**: Kích hoạt trên `pnlDesktop` và toàn bộ Child Forms để chống giật/nháy hình (Flicker).

---

## 5. Kiến Trúc Triển Khai Thực Tế (OMO System - B2C Web Portal)

Hệ thống B2C Web Portal của dự án sử dụng kiến trúc **OMO (Online Merge Offline)** - Kết hợp sức mạnh Mạng toàn cầu và Cơ sở dữ liệu Cục bộ máy tính.

### 5.1. Tại sao lại chọn Vercel + Ngrok Tunnel? (Why?)
- **Bài toán:** Yêu cầu dự án là làm web bán vé cho khách trên Internet (`dainam-web-portal.vercel.app`), nhưng cơ sở dữ liệu (`SQL Server`) và phần mềm quản lý Desktop (`WinForms`) lại đang chạy Local trên máy tính sinh viên/laptop, không có VPS (máy chủ đám mây) nào để đưa Database lên mây.
- **Giải pháp - FrontEnd Vercel:** 
  - Vercel cung cấp nền tảng hosting Frontend tĩnh hoàn toàn **miễn phí, tốc độ cao (Global Edge Network)**.
  - Tại sao cần Kịch bản Hack `deploy.bat`: Vercel có tính năng bảo vệ tự động xoá/ẩn mọi thư mục bắt đầu bằng dấu gạch dưới `_`. Tuy nhiên lõi `Blazor WebAssembly` của Microsoft bắt buộc phải tải core engine từ thư mục `_framework`. Do đó, kịch bản `deploy.bat (PRO MAX V2)` dùng PowerShell để thao túng và bẻ khóa cơ chế bảo vệ của Vercel (đổi tên và sửa file Regex thành `framework`) nhằm buộc Vercel phải phục vụ Blazor WASM.
- **Giải pháp - Ngrok Tunnel (Backend & SQL):**
  - Tránh triệt để việc phải đi thuê Host SQL / Thuê VPS vốn tốn nhiều tiền bạc và cấu hình vất vả.
  - Mở một "Đường hầm" (Tunnel) kết nối mã hóa trực tiếp từ Web tĩnh (Vercel) xuyên thủng Firewall/Router vào thẳng `Localhost API` và `SQL Server` nội bộ.
  - Sử dụng **Ngrok Static Domain (Tên miền tĩnh)** để hệ thống vĩnh viễn không bị rớt mạng và không bị reset link API mỗi khi khởi động lại.

### 5.2. Cách Vận Hành Và Sử Dụng (How-to-Run)

Kiến trúc OMO này cực kỳ "ăn liền", toàn quyền kiểm soát nằm ở Desktop:

1. **Khi cần cập nhật Giao Diện Web (Push Code B2C):**
   - Lập trình viên thiết kế và sửa code giao diện C# Razor nội bộ.
   - Khi hoàn thiện, chỉ việc nhấn đúp chạy tệp `deploy.bat`. 
   - Hệ thống sẽ tự làm mọi công đoạn: Biên dịch -> Bẻ khóa Vercel `_framework` -> Đẩy thẳng code lên Internet.

2. **Khi công viên hoạt động (Daily Run):**
   - Quản trị viên khởi động máy bàn. Nháy đúp tệp `run.bat`.
   - Hệ thống sẽ đánh thức `Web API` ngầm và tự động bật `Ngrok Tunnel` kích hoạt kết nối bảo mật.
   - Lúc này, hàng trăm khách hàng ở ngoài Internet có thể thoải mái quét QR đăng nhập vào `dainam-web-portal.vercel.app`, dữ liệu sẽ tự động tuồn về SQL Server bên trong `run.bat` một cách mượt mà và an toàn.
