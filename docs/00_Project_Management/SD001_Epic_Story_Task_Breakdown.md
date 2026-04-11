# BẢNG PHÂN RÃ CẤU TRÚC: EPIC ➔ STORY ➔ TASK
*(Dùng để giảng giải và chứng minh tư duy quản trị Agile/Scrum của nhóm)*

Tài liệu này trích xuất một vài Module tiêu biểu của dự án để minh họa cách nhóm băm nhỏ một cục công việc khổng lồ (Epic) thành các tính năng người dùng (Story) và cuối cùng là nhiệm vụ lập trình (Task).

---

## 🏗️ EPIC-01: Quản Trị Nền Tảng & Danh Mục
**Định nghĩa Epic:** Xây dựng khung xương cơ bản cho toàn bộ hệ thống (Đăng nhập, Phân quyền, Thêm/Sửa/Xóa các danh mục cốt lõi như Khu vực, Trò chơi, Nhân viên).

### 📖 Story 1.1: Đăng nhập & Xác thực
> *"**Là một** nhân viên, **Tôi muốn** có màn hình Đăng nhập bảo mật **Để** hệ thống biết tôi là ai và phân quyền tương ứng."*

*Được phân rã thành các Task (Việc của Coder):*
- 📋 **Task 1.1.1 (Database):** Cấu trúc bảng `TaiKhoan` và `VaiTro`, thực hiện băm mật khẩu (Hash MD5) cho password.
- 📋 **Task 1.1.2 (Frontend):** Thiết kế Form UI Đăng nhập (Winform).
- 📋 **Task 1.1.3 (Backend):** Viết logic kiểm tra Tài khoản/Mật khẩu bị sai 3 lần thì khóa nick.
- 📋 **Task 1.1.4 (QA/QC):** Viết Unit Test cho lớp `BUS_NhanVien.DangNhap()`.

### 📖 Story 1.2: Quản lý Khu Vực (CRUD)
> *"**Là một** Admin, **Tôi muốn** Thêm/Sửa/Xóa các Khu Vực trong Công viên (Ví dụ: Khu Biển, Khu Thú) **Để** dễ dàng gắn trò chơi vào các khu này."*

*Được phân rã thành các Task:*
- 📋 **Task 1.2.1 (Frontend):** Thiết kế GridView hiển thị danh sách Khu Vực.
- 📋 **Task 1.2.2 (Backend):** Viết Validation quy định "Tên khu vực không được dưới 3 ký tự".
- 📋 **Task 1.2.3 (Backend):** Chặn không cho Xóa Khu vực nếu bên trong vẫn đang có Trò chơi hoạt động.

---

## 🛒 EPIC-03: Điểm Bán Hàng Số (Core POS)
**Định nghĩa Epic:** Màn hình thu ngân cực nhanh cho Lễ tân, xử lý mua vé, giỏ hàng, áp dụng Voucher và in mã QR.

### 📖 Story 3.1: Giao diện máy POS Cảm ứng
> *"**Là một** thu ngân, **Tôi muốn** màn hình bán vé dạng nút bấm siêu bự **Để** có thể thao tác bằng tay trên màn hình POS 15.6 inch cực kỳ nhanh thay vì dùng chuột."*

*Được phân rã thành các Task:*
- 📋 **Task 3.1.1 (Frontend):** Ứng dụng thư viện Guna2 / DevExpress để vẽ các nút bấm bự, bo góc.
- 📋 **Task 3.1.2 (Backend):** Viết cấu trúc Giỏ Hàng tạm (Shopping Cart) trên RAM để không bị lag khi chọn nhiều vé.
- 📋 **Task 3.1.3 (Backend):** Code thuật toán tự động tính toán tổng tiền khi bấm tăng/giảm số lượng.

### 📖 Story 3.2: Soát vé bằng Mã QR
> *"**Là một** bảo vệ cổng, **Tôi muốn** dùng súng bắn tia lazer quét mã QR in trên vé **Để** cổng điện từ tự động mở nếu vé đúng."*

*Được phân rã thành các Task:*
- 📋 **Task 3.2.1 (API):** Tích hợp thư viện sinh tự động mã QR Code đính kèm vào mã vé.
- 📋 **Task 3.2.2 (Backend):** Cấu hình focus Textbox luôn đón nhận dữ liệu từ súng bắn Barcode (giả lập thao tác Enter).
- 📋 **Task 3.2.3 (Database):** Cập nhật trạng thái vé từ "Chưa Sử Dụng" sang "Đã Quét" để chống vé giả/vé xài lại.

---

## 🏨 EPIC-02: Dịch Vụ Mở Rộng KDL Đại Nam
**Định nghĩa Epic:** Các dịch vụ chuyên sâu của Đại Nam mà các khu vui chơi khác không có (Khách sạn, Biển nhân tạo, Vườn thú Safari, Quản lý kho).

### 📖 Story 2.1: Quản lý Phòng Khách sạn
> *"**Là một** lễ tân khách sạn, **Tôi muốn** xem Biểu đồ sơ đồ Phòng trực quan **Để** biết phòng nào đang trống, phòng nào đang dọn dẹp."*

*Được phân rã thành các Task:*
- 📋 **Task 2.1.1 (Frontend):** Vẽ sơ đồ 200+ phòng hiển thị theo 3 màu: Xanh (Trống), Đỏ (Đang ở), Vàng (Đang dọn).
- 📋 **Task 2.1.2 (Backend):** Code tính năng Chuyển Phòng (Đổi từ phòng A sang phòng B mang theo toàn bộ hóa đơn phụ).
- 📋 **Task 2.1.3 (Database):** Khởi tạo bảng `Phong` và `DatPhong`.

### 📖 Story 2.2: Hệ thống Thẻ VIP Tích Điểm (Loyalty)
> *"**Là một** khách VIP, **Tôi muốn** được cộng điểm hệ số nhân 2 khi vào ngày lễ **Để** có động lực chi tiêu nhiều hơn tại Công viên."*

*Được phân rã thành các Task:*
- 📋 **Task 2.2.1 (Backend):** Viết thuật toán check ngày hiện tại có phải Lễ không (Lấy từ bảng `BangGia`).
- 📋 **Task 2.2.2 (Frontend):** Khi Mua hàng báo giá, hiện dòng chữ Xanh lá "Bạn được cộng +50 điểm VIP từ giao dịch này".
