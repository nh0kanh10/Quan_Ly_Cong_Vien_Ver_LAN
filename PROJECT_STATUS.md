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
- **Chuẩn hóa Kiến trúc & Nghiệp vụ lõi (Architecture & Core Business):**
  - [x] Áp dụng Dependency Injection toàn diện cho toàn bộ 100% các class `BUS_*.cs`.
  - [x] Xây dựng bộ khung Gateway trong `BUS_Dependencies.cs` chuẩn bị cho Unit Tests / TDD.
  - [x] Đã xử lý triệt để xóa sổ mô hình gọi Data Access tĩnh (`DAL_*.Instance`) -> Môi trường Zero-dependency thành công.
  - [x] Các luồng giao dịch tài chính gắt gao nằm tại 2 nghiệp vụ `BUS_TichDiem` (Loyalty Engine) và `BUS_ThueDo` (Deposit & Phạt) đã bọc Gateway cùng TransactionScope an toàn 100%.
  - [x] Fix lỗi thiết kế Overbooking trong nghiệp vụ Đặt bàn (`MoBan / DatBanTruoc`).
  - [x] Fix khẩn cấp lỗi "Rò rỉ bốc hơi tiền cọc" trong tính năng `GhepBan` (đã thêm code hoàn tiền `GiaiToaTienCoc`).
  - [x] Ngăn chặn hacker bắt gói tin đổi giá (Server-side Recalculation) tại `BUS_DonHang`.
  - [x] Chốt áo giáp TransactionScope cho hàm `HoanTatKiemKe` (Inventory) chống mất đối xứng chênh lệch dữ liệu Kho.
  - [ ] Phát triển AI Chatbot / Cấu hình tính năng Web / App.

## 3. Các hạng mục CHỜ XỬ LÝ (Pending & Next Phases)
- **Giao diện Nghiệp vụ lõi (Nghiệp vụ phức tạp):**
  - Quản lý bán hàng POS (Hợp nhất giỏ hàng).
  - Trạm kiểm soát RFID cổng & Kiosk (frmKiemSoatVe).
  - Tối ưu hiệu năng nạp thẻ / trừ tiền theo batch.
