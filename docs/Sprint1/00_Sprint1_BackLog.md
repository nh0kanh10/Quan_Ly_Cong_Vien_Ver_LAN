# Sprint 1 Backlog — Hệ thống quản lý Đại Nam
**Thời gian:** 04/03/2026 – 17/03/2026 (2 tuần)
**Mục tiêu:** Xây dựng xong phần đăng nhập, đa ngôn ngữ, phân quyền và chức năng quản lý danh mục cốt lõi (Sản phẩm, Vé, Combo, BOM nguyên liệu) để làm nền móng cho phần bán hàng ở Sprint sau.

---

## Danh sách công việc chi tiết (Tasks)

### TASK-00: Màn hình chính + Đăng nhập + Đa ngôn ngữ (20 giờ)
- [ ] Thiết kế màn hình chính (frmMain) có menu bên trái và vùng làm việc bên phải.
- [ ] Code chức năng đăng nhập, kiểm tra đúng sai, sai 5 lần thì khóa 15 phút.
- [ ] Code chức năng bấm vào menu thì mở màn hình con ở vùng bên phải.
- [ ] Làm nút đổi ngôn ngữ (Việt, Anh, Trung) đổi được chữ ngay lập tức không cần khởi động lại.

### US-01: Phân quyền theo vai trò (12 giờ)
- [ ] Tạo bảng Vai trò, Quyền hạn dưới cơ sở dữ liệu và thêm sẵn 3 quyền mẫu.
- [ ] Làm màn hình Quản lý vai trò (Thêm, sửa, xóa).
- [ ] Làm màn hình gán quyền (Tick chọn màn hình nào được phép vào).
- [ ] Code chặn người dùng: Ai không có quyền thì không thấy menu đó.

### US-02: Quản lý Sản phẩm, Vé cổng và BOM nguyên liệu (28 giờ)
- [ ] Làm màn hình xem danh sách, thêm, sửa, xóa sản phẩm.
- [ ] Phần bảng giá: Cho phép lưu giá khác nhau cho người lớn/trẻ em, ngày thường/ngày lễ.
- [ ] Phần vé: Sinh mã QR tự động cho từng vé để sau này quét qua cổng.
- [ ] Phần định mức (BOM): Cho phép chọn nguyên liệu nấu ăn và lưu số lượng tiêu hao.
- [ ] Chức năng tìm kiếm sản phẩm ngay khi đang gõ chữ.

### US-03: Quản lý Combo (18 giờ)
- [ ] Làm màn hình thêm Combo, cho phép chọn nhiều sản phẩm vào 1 gói.
- [ ] Viết đoạn code kiểm tra tổng phần trăm chia tiền phải đúng bằng 100 mới cho lưu.
- [ ] Vẽ biểu đồ cột để nhìn rõ tỷ lệ chia tiền của từng món trong Combo.

### TASK-TEST: Kiểm thử, Demo và Tài liệu (18 giờ)
- [ ] Viết bộ Test Cases: Tối thiểu **200 TCs** (đầy đủ tiêu đề, bước thực hiện, kết quả mong đợi rõ ràng).
- [ ] Chạy thử toàn bộ các tính năng trên, đối chiếu với tài liệu.
- [ ] Tạo file Defect List: Tối thiểu **25 lỗi** (20 lỗi mới + 5 lỗi hồi quy). **Bắt buộc có cột so sánh giữa Kết quả mong đợi và Kết quả thực tế**.
- [ ] Sửa dứt điểm các lỗi làm treo phần mềm.
- [ ] Hoàn thiện file SRS: Phải có mô tả bằng lời, UseCase, Activity Diagram, Prototype, Ràng buộc.
- [ ] Viết file Hướng dẫn cài đặt và sử dụng (dùng hình chụp sản phẩm thật của Sprint).
- [ ] Triển khai hệ thống lên mô hình Server-Client thật (hoặc máy ảo/hosting) để chấm điểm (không chạy Localhost).
- [ ] Lập biên bản họp nhóm / họp khách hàng (có chữ ký xác nhận).

---

## Yêu cầu Bắt Buộc (Theo Barem Chấm Điểm của Thầy)
> ⚠️ **Chú ý:** Nếu thiếu các mục này sẽ bị trừ điểm nặng hoặc 0 điểm phần code.
1. **Test Cases (1đ):** Phải đủ 200 TCs, tuyệt đối không viết sơ sài phần "Bước thực hiện" và "Kết quả mong đợi".
2. **Defect List (1đ):** Phải đủ 25 lỗi và BẮT BUỘC có cột so sánh *Kết quả mong đợi* vs *Kết quả thực tế*.
3. **Triển khai Code (5đ):** Phải chạy được trên môi trường mạng thật (Server - Client) hoặc Hosting. **Nếu chỉ chạy Localhost: 0 điểm code.**
4. **SRS (1.5đ):** Thiếu UseCase, Activity Diagram, Prototype hoặc bảng Ràng buộc sẽ bị trừ 0.3đ/phần.
5. **Biên bản họp:** Phải có biên bản họp khởi động và demo kèm chữ ký.

---

*Ghi chú: Làm đến đâu, đánh dấu [x] đến đó.*
