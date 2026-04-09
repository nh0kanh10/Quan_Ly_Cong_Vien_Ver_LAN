# TÌNH TRẠNG DỰ ÁN (PROJECT STATUS)
**Cập nhật lần cuối:** Hoàn thiện hạ tầng Triển khai Web B2C qua Tunneling (Vercel + Ngrok/Cloudflare).

## 1. Các hạng mục ĐÃ HOÀN THÀNH (Completed)
- **Triển khai Hạ tầng Web B2C Portal (OMO Setup):**
  - Đưa thành công Frontend (Blazor WebAssembly) lên nền tảng **Vercel** (`dainam-web-portal.vercel.app`).
  - Viết hệ thống kịch bản tự động `deploy.bat` (PRO MAX V2) tích hợp PowerShell để bypass cơ chế ẩn thư mục `_framework` của Vercel (tự động mapping lại cấu hình SPA).
  - Thiết lập Tunneling (Cloudflare/Ngrok) cho phép Frontend trên mạng toàn cầu gọi API trực tiếp về CSDL SQL Server trên máy Local (Không cần thuê VPS/Host CSDL).
- **Kiến trúc Database (Schema):**
  - Tách bảng `VeDienTu` ra khỏi `ViTriNgoi` (Sinh `VeKhanDai`).
  - Định hình lại `SanPham`, loại bỏ các cột hard-code (`CanTaoToken`, `IdTroChoi`). Sinh bảng riêng `TroChoi` và `SanPham_Ve`.
  - Thay Discriminator Pattern bằng Exclusive Arcs (Khóa ngoại chuẩn) cho cụm Đua xe/Ngựa (`KetQuaDua`, `BaoTriPhuongTienDua`).
- **Fake Data (Seed Script):**
  - Vá hoàn toàn 3 lỗ hổng Audit: Nạp Ví (Sinh `PhieuThu`), Nhập Kho (Sinh `PhieuChi`), Xuất POS (Sinh `TheKho`). Hệ thống kế toán cân bằng tuyệt đối.
- **Tầng Entities (ET):**
  - Cập nhật thủ công (Fix tay) các file `ET_SanPham.cs`, `ET_VeDienTu.cs`, `ET_ChiTietDonHang.cs` để khớp 100% với CSDL mới. Tránh dùng script ghi đè để bảo lưu code custom của dự án.
  
## 2. Các hạng mục ĐANG THỰC HIỆN (In Progress)
- **Thiết lập Tên miền Cố định (Static Tunnel URL):**
  - Tích hợp tài khoản Ngrok miễn phí vào file `run.bat` để cố định Public URL cho Web API vĩnh viễn, khắc phục lỗi mất kết nối mỗi khi khởi động lại máy tính.
- **Chuẩn hóa tầng GUI (CRUD Forms):**
  - Sửa lỗi build tại các form bị ảnh hưởng bởi thay đổi ET (Đặc biệt: `frmSanPham`).
  - Nâng cấp trải nghiệm UX/UI (Tinh tế hóa): Focus input, hệ thống Tabbing, ẩn cột Audit trong GridView.
  - Xử lý UX hiển thị dữ liệu (Nối bảng): Hiển thị Tên thay vì hiển thị Mã ID trên GridView (ví dụ hiển thị Tên Khu Vực thay vì hiển thị số 1, 2).

## 3. Các hạng mục CHỜ XỬ LÝ (Pending & Next Phases)
- **Giao diện Nghiệp vụ lõi (Nghiệp vụ phức tạp):**
  - Quản lý bán hàng POS (Hợp nhất giỏ hàng).
  - Trạm kiểm soát RFID cổng & Kiosk (frmKiemSoatVe).
  - Tối ưu hiệu năng nạp thẻ / trừ tiền theo batch.
