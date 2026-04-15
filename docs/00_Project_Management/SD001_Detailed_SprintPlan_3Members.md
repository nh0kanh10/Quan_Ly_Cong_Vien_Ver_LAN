# KẾ HOẠCH TRIỂN KHAI SPRINT CHI TIẾT
## Dự án: Hệ thống Quản lý Khu Du lịch Đại Nam — Mã dự án SD001

**Ngày lập:** 03/03/2026 · **Phiên bản:** 3.1 · **Người lập:** Nguyên (Trưởng nhóm)

---

### Thông tin chung

| Hạng mục | Nội dung |
|:---------|:---------|
| **Đội dự án** | 3 thành viên chính + 1 GVHD + 1 đại diện KH |
| **Thời lượng Sprint** | 2 tuần (10 ngày làm việc × 8 giờ = 80 giờ/người) |
| **Năng lực mỗi Sprint** | 240 giờ (80 × 3 người) |
| **Tổng số Sprint** | 5 Sprint liên tiếp |
| **Chi phí nguồn lực** | 200.000 VNĐ/giờ (tất cả vị trí) |
| **Ngày bắt đầu** | 04/03/2026 |
| **Ngày kết thúc dự kiến** | 12/05/2026 |

### Thành viên & Vai trò

| Mã | Họ tên | MSSV | Vai trò |
|:--:|:-------|:-----|:--------|
| **NG** | Bùi Trí Nguyên | 24211TT2838 | Trưởng nhóm · Lập trình viên chính (Backend, CSDL, kiến trúc) |
| **TN** | Đỗ Duy Tấn | 23211TT3054 | Lập trình viên giao diện (Frontend, UX, nối dữ liệu) |
| **NH** | Bùi Thị Yến Nhi | 24211TT2675 | Phân tích nghiệp vụ · Kiểm thử viên (SRS, Test Case, QA) |
| **GV** | Th. Phan Gia Phước | — | Giảng viên hướng dẫn (Duyệt, tư vấn, đánh giá) |
| **KH** | Đại diện KDL Đại Nam | — | Khách hàng (Cung cấp yêu cầu, nghiệm thu) |

**Lớp:** 25211CNC10762001 · **Ngành:** Công Nghệ Thông Tin · **Trường:** Cao đẳng Công nghệ Thủ Đức

### Ma trận RASCI — Quy ước

| Ký hiệu | Ý nghĩa |
|:--------:|:--------|
| **R** | Responsible — Người trực tiếp thực hiện |
| **A** | Accountable — Người phê duyệt / chịu trách nhiệm cuối cùng |
| **S** | Support — Người hỗ trợ |
| **C** | Consulted — Người được tham vấn ý kiến |
| **I** | Informed — Người được thông báo kết quả |

### Luồng công việc chuẩn cho MỖI chức năng trong Sprint

```
① NH: Phân tích nghiệp vụ → Viết Acceptance Criteria (AC)
                    ↓
② Song song:  NH viết SRS + Test Case  |  NG code BUS (+ Unit Test)  |  TN dựng UI
                    ↓
③ TN nối giao diện ↔ dữ liệu
                    ↓
④ NH chạy Test Case trên giao diện → Ghi Defect List
                    ↓
⑤ NG + TN sửa lỗi
                    ↓
⑥ NH chuẩn hóa SRS tổng hợp + Cập nhật HDSD (cuối Sprint)
```

> **Về CSDL:** Thiết kế toàn bộ (~50 bảng) ngay Sprint 1. Sprint 2→5 chỉ chỉnh sửa nhỏ.
> **Về SRS:** Viết song song với code. Cuối Sprint chuẩn hóa thành Word.
> **Về Test Case:** Viết trước/song song với code (dựa trên AC). Không viết sau khi test.

---
---

## 🚀 SPRINT 1 — NỀN TẢNG & QUẢN LÝ DANH MỤC

**Thời gian:** 04/03/2026 → 17/03/2026 (10 ngày)
**Mục tiêu:** Thiết kế CSDL toàn bộ hệ thống, xây dựng khung ứng dụng và 4 nhóm chức năng danh mục cốt lõi.

### Lộ trình Sprint 1

| Giai đoạn | Ngày | Nội dung |
|:---------:|:-----|:---------|
| Khởi động | 04/03 | Họp Planning, thiết kế CSDL toàn bộ |
| Phân tích | 04–05/03 | NH viết AC + TN phác thảo Wireframe |
| Song song | 06–12/03 | NH viết SRS+TC · NG code BUS · TN dựng UI |
| Tích hợp | 13/03 | TN nối giao diện ↔ dữ liệu |
| Kiểm thử | 13–14/03 | NH chạy TC → ghi Defect |
| Sửa lỗi | 16/03 | NG + TN sửa lỗi |
| Tổng kết | 17/03 | SRS Word, HDSD, Demo, Retro |

### Bảng công việc Sprint 1

| ID | Công việc | Giờ | Bắt đầu | Kết thúc | Resource Name | Chi phí (VNĐ) | NG | TN | NH | GV | KH | Tiêu chí chất lượng (Acceptance Criteria) | Phụ thuộc |
|:---|:----------|:---:|:-------:|:--------:|:--------------|:-------------:|:--:|:--:|:--:|:--:|:--:|:------------------------------------------|:----------|
| **S1.0** | **Khởi động & Thiết kế CSDL** | **12** | **04/03** | **05/03** | | **2.400.000** | | | | | | | |
| S1.0.1 | Họp Sprint Planning — mục tiêu, phân công, chốt scope | 4 | 04/03 | 04/03 | Phòng họp, Trello | 800.000 | A | R | R | C | I | Biên bản họp có chữ ký 3 thành viên, mục tiêu Sprint rõ ràng | — |
| S1.0.2 | Thiết kế CSDL toàn bộ hệ thống (~50 bảng cho 5 Sprint) | 8 | 04/03 | 05/03 | SQL Server, ERD Tool | 1.600.000 | R | S | I | C | I | Sơ đồ ERD hoàn chỉnh, FK đúng, script chạy không lỗi | S1.0.1 |
| **S1.1** | **Phân tích nghiệp vụ & Viết AC Sprint 1** | **12** | **04/03** | **05/03** | | **2.400.000** | | | | | | | |
| S1.1.1 | Phân tích 4 chức năng S1 → Viết Acceptance Criteria chi tiết | 8 | 04/03 | 05/03 | Word, Trello | 1.600.000 | S | S | R | I | C | Mỗi chức năng có ≥ 5 tiêu chí nghiệm thu rõ ràng | S1.0.1 |
| S1.1.2 | Phác thảo Wireframe giao diện cho 4 chức năng | 4 | 04/03 | 05/03 | Figma, Giấy nháp | 800.000 | I | R | S | I | I | Mỗi form có bản vẽ phác thảo bố cục, size, vị trí nút | S1.0.1 |
| **S1.2** | **Đăng nhập & Khung ứng dụng** | **20** | **06/03** | **10/03** | | **4.000.000** | | | | | | | |
| S1.2.1 | Xử lý nghiệp vụ đăng nhập, mã hóa mật khẩu | 6 | 06/03 | 06/03 | C#, SHA256 | 1.200.000 | R | I | I | I | I | Đăng nhập đúng→vào hệ thống, sai→báo lỗi, mật khẩu mã hóa | S1.0.2 |
| S1.2.2 | Thiết kế màn hình đăng nhập + khung chính (Ribbon Bar) | 8 | 06/03 | 09/03 | Guna UI2, WinForms | 1.600.000 | I | R | I | I | I | Giao diện chuyên nghiệp, logo, Ribbon menu điều hướng đúng | S1.0.2 |
| S1.2.3 | Cấu hình kết nối LAN (Client-Server) | 4 | 09/03 | 09/03 | ConnectionManager | 800.000 | R | I | I | I | I | Lưu chuỗi kết nối vào file config, kết nối SQL Server thành công | S1.2.1 |
| S1.2.4 | _(Song song)_ Viết SRS + TC Đăng nhập (~30 TC) | 2 | 06/03 | 09/03 | Word, Excel | 400.000 | I | I | R | I | I | SRS có Workflow, Prototype, Validation Rules. TC đủ bước | S1.1.1 |
| **S1.3** | **Quản lý Khu Vực & Trò Chơi** | **28** | **06/03** | **12/03** | | **5.600.000** | | | | | | | |
| S1.3.1 | Xử lý nghiệp vụ Khu Vực: thêm/sửa/xóa/tìm | 6 | 06/03 | 09/03 | C#, DAL, BUS | 1.200.000 | R | I | I | I | I | CRUD đúng, không trùng tên, có mã tự sinh (A01, A02...) | S1.0.2 |
| S1.3.2 | Xử lý nghiệp vụ Trò Chơi: CRUD + lọc theo khu vực | 4 | 09/03 | 10/03 | C#, DAL, BUS | 800.000 | R | I | I | I | I | Lọc theo ComboBox khu vực chính xác, trò chơi gắn đúng khu | S1.3.1 |
| S1.3.3 | Thiết kế giao diện Khu Vực (Grid trái + Form phải) | 4 | 06/03 | 09/03 | Guna UI2, Master-Detail | 800.000 | I | R | I | I | I | Giao diện 2 phần: danh sách — chi tiết, responsive khi resize | S1.0.2 |
| S1.3.4 | Thiết kế giao diện Trò Chơi (Grid + dropdown lọc + chi tiết) | 4 | 09/03 | 10/03 | Guna UI2 | 800.000 | I | R | I | I | I | Lọc khu vực → Grid cập nhật đúng, chọn dòng → hiện chi tiết | S1.3.3 |
| S1.3.5 | Nối giao diện Khu Vực + Trò Chơi ↔ dữ liệu | 4 | 10/03 | 11/03 | Backend + Frontend | 800.000 | S | R | I | I | I | Dữ liệu hiển thị đúng, sửa→lưu→refresh không mất | S1.3.1, S1.3.3 |
| S1.3.6 | _(Song song)_ Viết SRS chức năng KV & TC | 4 | 06/03 | 11/03 | Word | 800.000 | I | I | R | I | I | SRS có Use Case, Workflow, Prototype, Business Rules, Validation | S1.1.1 |
| S1.3.7 | _(Song song)_ Viết Test Case KV & TC (~105 TC) | 2 | 06/03 | 11/03 | Excel | 400.000 | I | I | R | I | I | TC đủ: hợp lệ, rỗng, trùng, ký tự đặc biệt, biên | S1.1.1 |
| **S1.4** | **Quản lý Sản Phẩm, ĐVT, Combo & Bảng Giá** | **52** | **06/03** | **12/03** | | **10.400.000** | | | | | | | |
| S1.4.1 | Xử lý nghiệp vụ Sản Phẩm: CRUD, tải hình, phân loại | 6 | 06/03 | 09/03 | C#, DAL, BUS | 1.200.000 | R | I | I | I | I | SP lưu đúng loại (Vé/Ăn uống/Thuê), hình ảnh hiển thị | S1.0.2 |
| S1.4.2 | Xử lý nghiệp vụ Quy đổi ĐVT (1 Thùng = 24 Lon) | 4 | 09/03 | 10/03 | C#, BUS | 800.000 | R | I | I | I | I | Hệ số > 1, không trùng ĐVT, quy đổi chính xác | S1.4.1 |
| S1.4.3 | Xử lý nghiệp vụ Bảng Giá (Thường/Cuối tuần/Lễ) | 4 | 10/03 | 10/03 | C#, BUS | 800.000 | R | I | I | I | I | Giá theo loại ngày, liên kết đúng ngày lễ đã cấu hình | S1.4.1 |
| S1.4.4 | Xử lý nghiệp vụ Combo (ghép SP thành gói) | 4 | 10/03 | 11/03 | C#, BUS | 800.000 | R | I | I | I | I | Combo cha-con liên kết đúng, giá gói ≤ tổng giá lẻ | S1.4.1 |
| S1.4.5 | Thiết kế giao diện SP 3 Tab (Thông tin / Quy đổi / Bảng giá) | 10 | 06/03 | 11/03 | Guna UI2, TabControl | 2.000.000 | I | R | I | I | I | 3 Tab rõ ràng, chuyển Tab mượt, dữ liệu không mất | S1.0.2 |
| S1.4.6 | Thiết kế giao diện Combo + ĐVT + Ngày Lễ (popup) | 6 | 09/03 | 11/03 | Guna UI2, Popup | 1.200.000 | I | R | S | I | I | Popup mở/đóng không mất dữ liệu form cha | S1.4.5 |
| S1.4.7 | Nối giao diện SP 3 Tab + Combo ↔ dữ liệu | 6 | 11/03 | 12/03 | Backend + Frontend | 1.200.000 | S | R | I | I | I | Tạo SP → Tab 2 nhập quy đổi → Tab 3 nhập giá → lưu OK | S1.4.1, S1.4.5 |
| S1.4.8 | Unit Test nghiệp vụ phức tạp tầng BUS (quy đổi ĐVT, tính giá) | 2 | 10/03 | 11/03 | xUnit, Moq | 400.000 | R | I | I | I | I | Tất cả test case pass, coverage hàm tính toán ≥ 80% | S1.4.2, S1.4.3 |
| S1.4.9 | _(Song song)_ Viết SRS chức năng SP, ĐVT, Combo, Bảng Giá | 6 | 06/03 | 12/03 | Word | 1.200.000 | I | I | R | I | I | SRS đầy đủ theo template chuẩn cho từng chức năng | S1.1.1 |
| S1.4.10 | _(Song song)_ Viết Test Case SP + Combo + ĐVT (~112 TC) | 4 | 06/03 | 12/03 | Excel | 800.000 | I | I | R | I | I | TC đủ trường hợp biên: giá 0, giá âm, combo rỗng | S1.1.1 |
| S1.4.11 | Thiết kế giao diện Cấu Hình Ngày Lễ (popup) | 4 | 09/03 | 11/03 | Guna UI2 | 800.000 | I | S | R | I | I | Bảng ngày lễ cho phép thêm/sửa/xóa, bảng giá liên kết đúng | S1.4.3 |
| **S1.5** | **Quản lý Nhân Viên & Lịch Ca** | **28** | **06/03** | **12/03** | | **5.600.000** | | | | | | | |
| S1.5.1 | Xử lý nghiệp vụ Nhân Viên: CRUD, tải ảnh, phân chức vụ | 4 | 06/03 | 09/03 | C#, DAL, BUS | 800.000 | R | I | I | I | I | Không trùng SĐT/CCCD, hình ảnh lưu đúng đường dẫn | S1.0.2 |
| S1.5.2 | Xử lý nghiệp vụ Phân Ca: kiểm tra trùng, xung đột | 4 | 09/03 | 10/03 | C#, BUS | 800.000 | R | I | I | I | I | 1 NV không được 2 ca cùng ngày, cảnh báo trùng rõ | S1.5.1 |
| S1.5.3 | Thiết kế giao diện NV (lưới + ảnh) + Lịch Ca (tuần) | 8 | 06/03 | 11/03 | Guna UI2, Calendar | 1.600.000 | I | R | I | I | I | NV có ảnh góc phải, Lịch Ca hiển thị dạng tuần, chuyển tuần OK | S1.0.2 |
| S1.5.4 | Nối giao diện NV + Lịch Ca ↔ dữ liệu | 4 | 11/03 | 12/03 | Backend + Frontend | 800.000 | S | R | I | I | I | Tạo NV → phân ca → lịch tuần hiển thị đúng | S1.5.1, S1.5.3 |
| S1.5.5 | _(Song song)_ Viết SRS NV & Lịch Ca | 4 | 06/03 | 12/03 | Word | 800.000 | I | I | R | I | I | SRS đủ Use Case, Workflow, Prototype, Validation | S1.1.1 |
| S1.5.6 | _(Song song)_ Viết Test Case NV + Lịch Ca (~108 TC) | 4 | 06/03 | 12/03 | Excel | 800.000 | I | I | R | I | I | TC gồm: trùng SĐT, ca xung đột, xóa NV đang có ca | S1.1.1 |
| **S1.6** | **Phân Quyền RBAC** | **16** | **09/03** | **12/03** | | **3.200.000** | | | | | | | |
| S1.6.1 | Xử lý nghiệp vụ: cấp quyền, kiểm tra, ẩn/hiện nút | 6 | 09/03 | 11/03 | C#, BUS | 1.200.000 | R | I | I | I | I | Admin thấy hết, Thu ngân chỉ thấy POS, vai trò đúng chức năng | S1.0.2, S1.5.1 |
| S1.6.2 | Thiết kế giao diện Vai Trò + Phân Quyền (checkbox matrix) | 4 | 09/03 | 11/03 | Guna UI2 | 800.000 | I | R | I | I | I | Danh sách quyền dạng checkbox, tick/bỏ tick rõ ràng | S1.6.1 |
| S1.6.3 | Tích hợp kiểm tra quyền vào tất cả form đã xây dựng | 2 | 11/03 | 12/03 | C#, BUS | 400.000 | R | S | I | I | I | Mỗi form gọi kiểm tra quyền khi mở, ẩn nút bị cấm | S1.6.1 |
| S1.6.4 | _(Song song)_ Viết SRS + TC Phân Quyền (~32 TC) | 4 | 09/03 | 12/03 | Word, Excel | 800.000 | I | I | R | I | I | TC gồm: Admin full, Thu ngân hạn chế, NV mới chặn hết | S1.1.1 |
| **S1.7** | **Kiểm thử & Tổng kết Sprint 1** | **28** | **13/03** | **17/03** | | **5.600.000** | | | | | | | |
| S1.7.1 | Thực thi toàn bộ TC Sprint 1 trên giao diện (~457 TC) | 6 | 13/03 | 14/03 | App chạy thật | 1.200.000 | I | S | R | I | I | KQ mỗi TC có PASS/FAIL, screenshot cho TC FAIL | S1.2→S1.6 |
| S1.7.2 | Ghi nhận Defect List (≥ 25 lỗi, có KQ mong đợi vs thực tế) | 2 | 13/03 | 14/03 | Excel | 400.000 | I | I | R | I | I | Defect có: Mức độ, Bước tái hiện, KQ mong đợi, KQ thực tế, Screenshot | S1.7.1 |
| S1.7.3 | Sửa lỗi Nghiêm trọng + Quan trọng | 8 | 14/03 | 16/03 | C#, IDE | 1.600.000 | R | R | S | I | I | 0 lỗi Nghiêm trọng còn mở trước Demo | S1.7.2 |
| S1.7.4 | Chuẩn hóa SRS tổng hợp Sprint 1 (gom AC → Word chính thức) | 4 | 16/03 | 17/03 | Word | 800.000 | I | S | R | C | I | File SRS Word hoàn chỉnh theo template (Use Case, Workflow, Prototype, Ràng buộc) | S1.2→S1.6 |
| S1.7.5 | Viết hướng dẫn cài đặt + sử dụng Sprint 1 | 2 | 16/03 | 17/03 | Word, Screenshots | 400.000 | I | I | R | I | I | HDSD có ảnh chụp từng bước, phân theo vai trò | S1.7.3 |
| S1.7.6 | Triển khai lên máy thật (Server + 2 Client LAN) | 2 | 16/03 | 17/03 | SQL Server, LAN | 400.000 | R | S | I | I | I | 3 máy chạy: Server SQL + 2 máy trạm WinForms kết nối được | S1.7.3 |
| S1.7.7 | Họp Sprint Review — Demo S1 cho Thầy + KH | 2 | 17/03 | 17/03 | Máy chiếu, App | 400.000 | A | R | R | A | A | Biên bản nghiệm thu có chữ ký, KH xác nhận đúng yêu cầu | S1.7.6 |
| S1.7.8 | Họp Sprint Retrospective — rút kinh nghiệm | 2 | 17/03 | 17/03 | Phòng họp | 400.000 | A | R | R | C | I | Biên bản ghi: điều tốt / cần cải thiện / hành động cụ thể | S1.7.7 |
| | **TỔNG SPRINT 1** | **196** | **04/03** | **17/03** | | **39.200.000** | | | | | | | |

**Phân bổ:** NG ~76h · TN ~68h · NH ~52h

**Sản phẩm bàn giao Sprint 1:** Sprint Backlog · SRS (4 CN) · Source Code · ~457 TC · Defect List (≥25) · HDSD · Biên bản Review

---
---

## 🚀 SPRINT 2 — KHO HÀNG & QUẢN LÝ KHÁCH HÀNG

**Thời gian:** 18/03/2026 → 31/03/2026 (10 ngày)
**Mục tiêu:** Xây dựng quản lý kho hàng (nhập—xuất—kiểm—tồn) và khách hàng (ví RFID, tích điểm), sẵn sàng cho POS Sprint 3.

### Lộ trình Sprint 2

| Giai đoạn | Ngày | Nội dung |
|:---------:|:-----|:---------|
| Khởi động | 18/03 | Họp Planning, chỉnh sửa CSDL nhỏ |
| Phân tích | 18–19/03 | NH viết AC cho Kho, KH, ĐH, KM |
| Song song | 20–26/03 | NH viết SRS+TC · NG code BUS · TN dựng UI |
| Tích hợp | 27/03 | TN nối giao diện |
| Kiểm thử | 27–28/03 | NH chạy TC mới + hồi quy S1 |
| Sửa lỗi | 30/03 | NG + TN sửa lỗi |
| Tổng kết | 31/03 | SRS Word, HDSD, Demo, Retro |

### Bảng công việc Sprint 2

| ID | Công việc | Giờ | Bắt đầu | Kết thúc | Resource Name | Chi phí (VNĐ) | NG | TN | NH | GV | KH | Tiêu chí chất lượng (Acceptance Criteria) | Phụ thuộc |
|:---|:----------|:---:|:-------:|:--------:|:--------------|:-------------:|:--:|:--:|:--:|:--:|:--:|:------------------------------------------|:----------|
| **S2.0** | **Khởi động Sprint 2** | **12** | **18/03** | **19/03** | | **2.400.000** | | | | | | | |
| S2.0.1 | Họp Sprint Planning — review S1, phân công S2 | 4 | 18/03 | 18/03 | Phòng họp, Trello | 800.000 | A | R | R | C | I | Biên bản có mục tiêu S2 + bài học từ S1 | S1.7.8 |
| S2.0.2 | Phân tích nghiệp vụ + Viết AC cho 4 chức năng S2 | 6 | 18/03 | 19/03 | Word, Trello | 1.200.000 | S | I | R | I | C | AC chi tiết cho Kho, ĐH, KH, KM (≥ 5 tiêu chí/CN) | S2.0.1 |
| S2.0.3 | Chỉnh sửa CSDL nhỏ (nếu có từ phản hồi S1) | 2 | 18/03 | 18/03 | SQL Server | 400.000 | R | I | I | I | I | Script chạy OK, dữ liệu S1 không mất | S2.0.1 |
| **S2.1** | **Quản lý Kho Hàng (Nhập—Xuất—Kiểm—Tồn—Thẻ Kho)** | **52** | **20/03** | **27/03** | | **10.400.000** | | | | | | | |
| S2.1.1 | Xử lý nghiệp vụ NCC + Phiếu Nhập (quy đổi ĐVT → cộng tồn) | 8 | 20/03 | 23/03 | C#, DAL, BUS | 1.600.000 | R | I | I | I | I | Nhập 1 Thùng → tồn +24 Lon. NCC có SĐT không trùng | S2.0.3 |
| S2.1.2 | Xử lý nghiệp vụ Phiếu Xuất (kiểm tồn → chặn âm) + Thẻ Kho | 6 | 23/03 | 24/03 | C#, BUS | 1.200.000 | R | I | I | I | I | Xuất quá tồn → cảnh báo đỏ, mỗi biến động ghi 1 dòng Thẻ Kho | S2.1.1 |
| S2.1.3 | Xử lý nghiệp vụ Kiểm Kho (Blind Mode, chênh lệch) | 4 | 24/03 | 25/03 | C#, BUS | 800.000 | R | I | I | I | I | Blind Mode ẩn tồn hệ thống, chênh lệch = Thực tế − HT | S2.1.1 |
| S2.1.4 | Thiết kế giao diện Kho Hàng (KPI + lưới + Thẻ Kho embedded) | 8 | 20/03 | 25/03 | Guna UI2, Panel | 1.600.000 | I | R | I | I | I | 3 thẻ KPI (Tổng vốn, Sắp hết, Đã hết) + lưới SP + panel phải | S2.0.3 |
| S2.1.5 | Thiết kế giao diện Phiếu Nhập/Xuất (popup) + Kiểm Kho | 6 | 23/03 | 26/03 | Guna UI2, Popup | 1.200.000 | I | R | I | I | I | Popup chọn SP → nhập SL → tính tổng tiền, Kiểm Kho Blind rõ | S2.1.4 |
| S2.1.6 | Nối giao diện Kho ↔ dữ liệu | 6 | 26/03 | 27/03 | Backend + Frontend | 1.200.000 | S | R | I | I | I | Nhập kho → tồn tăng → Thẻ Kho ghi → lưới cập nhật | S2.1.1→S2.1.5 |
| S2.1.7 | Unit Test nghiệp vụ kho (cộng/trừ tồn, chặn âm, quy đổi) | 2 | 24/03 | 25/03 | xUnit, Moq | 400.000 | R | I | I | I | I | Tất cả test pass, không cho tồn kho âm | S2.1.1 |
| S2.1.8 | _(Song song)_ Viết SRS Kho Hàng | 6 | 20/03 | 27/03 | Word | 1.200.000 | I | I | R | I | I | SRS đủ: Use Case NCC, Nhập, Xuất, Kiểm, Tồn, Thẻ Kho | S2.0.2 |
| S2.1.9 | _(Song song)_ Viết TC Kho Hàng (~120 TC) | 6 | 20/03 | 27/03 | Excel | 1.200.000 | I | I | R | I | I | TC gồm: nhập SL âm, xuất quá tồn, kiểm blind, quy đổi sai | S2.0.2 |
| **S2.2** | **Quản lý Đơn Hàng & Phiếu Thu/Chi** | **16** | **20/03** | **26/03** | | **3.200.000** | | | | | | | |
| S2.2.1 | Xử lý nghiệp vụ Đơn Hàng + Phiếu Thu/Chi | 6 | 20/03 | 23/03 | C#, DAL, BUS | 1.200.000 | R | I | I | I | I | ĐH hiển thị đúng: ngày, KH, tổng tiền, trạng thái | S2.0.3 |
| S2.2.2 | Thiết kế giao diện Đơn Hàng + Phiếu Thu/Chi | 6 | 23/03 | 25/03 | Guna UI2 | 1.200.000 | I | R | I | I | I | Danh sách ĐH, click → chi tiết SP, form phiếu thu/chi gọn | S2.2.1 |
| S2.2.3 | _(Song song)_ Viết SRS + TC Đơn Hàng (~50 TC) | 4 | 20/03 | 26/03 | Word, Excel | 800.000 | I | I | R | I | I | TC gồm: xem chi tiết, lọc ngày, phiếu liên kết đúng ĐH | S2.0.2 |
| **S2.3** | **Quản lý Khách Hàng (Ví RFID + Loyalty + Sự cố)** | **52** | **20/03** | **27/03** | | **10.400.000** | | | | | | | |
| S2.3.1 | Xử lý nghiệp vụ KH: CRUD, phân hạng (Thường/VIP/VVIP/Đoàn/DN/NB) | 6 | 20/03 | 23/03 | C#, BUS | 1.200.000 | R | I | I | I | C | SĐT không trùng, hạng KH gán đúng hệ số tích điểm | S2.0.3 |
| S2.3.2 | Xử lý nghiệp vụ Ví: cấp ví, nạp tiền, trừ tiền, lịch sử | 6 | 23/03 | 24/03 | C#, BUS | 1.200.000 | R | I | I | I | I | Nạp → số dư tăng, trừ → giảm, mỗi GD ghi 1 dòng lịch sử | S2.3.1 |
| S2.3.3 | Xử lý nghiệp vụ RFID + Tích điểm (VIP ×2, VVIP ×3) | 6 | 24/03 | 25/03 | C#, BUS | 1.200.000 | R | I | I | I | I | Gắn thẻ → liên kết KH, khóa → chặn thanh toán, ×2/×3 đúng | S2.3.1 |
| S2.3.4 | Thiết kế giao diện KH (lưới + KPI + 3 Tab + popup) | 12 | 20/03 | 26/03 | Guna UI2, TabControl | 2.400.000 | I | R | I | I | I | Click KH → panel phải hiện 3 tab: Giao dịch, Điểm, Sự cố | S2.0.3 |
| S2.3.5 | Thiết kế popup Nạp Tiền + Thêm Nhanh + Điều Chỉnh Điểm | 4 | 24/03 | 26/03 | Guna UI2, Popup | 800.000 | I | R | S | I | I | Mỗi popup mở/đóng không mất dữ liệu form cha | S2.3.4 |
| S2.3.6 | Nối giao diện KH ↔ dữ liệu | 6 | 26/03 | 27/03 | Backend + Frontend | 1.200.000 | S | R | I | I | I | Tạo KH → cấp ví → nạp → Tab GD hiện dòng nạp đúng | S2.3.1→S2.3.5 |
| S2.3.7 | Unit Test nghiệp vụ KH (tính điểm hệ số, quy đổi điểm→tiền) | 2 | 25/03 | 25/03 | xUnit, Moq | 400.000 | R | I | I | I | I | Tất cả test pass, VIP ×2, VVIP ×3, Nội bộ ×0 | S2.3.3 |
| S2.3.8 | _(Song song)_ Viết SRS + TC KH (~110 TC) | 10 | 20/03 | 27/03 | Word, Excel | 2.000.000 | I | I | R | I | I | SRS+TC đủ: tạo trùng SĐT, nạp âm, khóa thẻ, quy đổi điểm | S2.0.2 |
| **S2.4** | **Khuyến Mãi & Marketing** | **16** | **23/03** | **26/03** | | **3.200.000** | | | | | | | |
| S2.4.1 | Xử lý nghiệp vụ KM (% / cố định, thời hạn) + Marketing | 6 | 23/03 | 24/03 | C#, BUS | 1.200.000 | R | I | I | I | C | KM hết hạn → tự ngưng, KM chồng chéo lấy giá tốt nhất | S2.0.3 |
| S2.4.2 | Thiết kế giao diện KM + Marketing | 4 | 24/03 | 25/03 | Guna UI2 | 800.000 | I | R | S | I | I | Thấy ngay KM nào đang active / hết hạn, mã màu rõ | S2.4.1 |
| S2.4.3 | _(Song song)_ Viết SRS + TC KM (~50 TC) | 6 | 23/03 | 26/03 | Word, Excel | 1.200.000 | I | I | R | I | I | TC gồm: KM hết hạn, KM chồng, KM % vs cố định | S2.0.2 |
| **S2.5** | **Kiểm thử, Hồi quy S1 & Tổng kết** | **40** | **27/03** | **31/03** | | **8.000.000** | | | | | | | |
| S2.5.1 | Thực thi TC Sprint 2 trên giao diện (~330 TC mới) | 6 | 27/03 | 28/03 | App chạy thật | 1.200.000 | I | S | R | I | I | ≥ 90% TC Pass, ghi nhận TC FAIL rõ ràng | S2.1→S2.4 |
| S2.5.2 | ⟲ Kiểm thử hồi quy S1 — chạy lại 100 TC ưu tiên cao | 4 | 27/03 | 28/03 | App chạy thật | 800.000 | I | S | R | I | I | ≥ 95% TC S1 vẫn Pass, form danh mục không bị ảnh hưởng | S1.7.1 |
| S2.5.3 | 🔗 Kiểm thử tích hợp S1×S2: SP→Kho, NV→Phiếu, RBAC→chặn | 4 | 27/03 | 28/03 | App chạy thật | 800.000 | S | I | R | I | I | SP tạo S1 nhập kho S2 đúng, Thu ngân không mở Kho | S2.1, S1.3 |
| S2.5.4 | Ghi nhận Defect List S2 (≥ 20 mới + 5 hồi quy S1) | 2 | 28/03 | 28/03 | Excel | 400.000 | I | I | R | I | I | Defect có: KQ mong đợi vs KQ thực tế, screenshot | S2.5.1→S2.5.3 |
| S2.5.5 | Sửa lỗi Sprint 2 | 8 | 28/03 | 30/03 | C#, IDE | 1.600.000 | R | R | S | I | I | Tất cả Nghiêm trọng sửa xong trước Demo | S2.5.4 |
| S2.5.6 | Chuẩn hóa SRS tổng hợp Sprint 2 | 4 | 30/03 | 31/03 | Word | 800.000 | I | S | R | C | I | File Word SRS S2 hoàn chỉnh theo template chuẩn | S2.1→S2.4 |
| S2.5.7 | Cập nhật HDSD (bổ sung Kho, KH, KM) | 2 | 30/03 | 31/03 | Word, Screenshots | 400.000 | I | I | R | I | I | HDSD bổ sung phần Kho Hàng, Khách Hàng, KM | S2.5.5 |
| S2.5.8 | Triển khai gia tăng lên máy thật | 2 | 30/03 | 31/03 | SQL Server, LAN | 400.000 | R | S | I | I | I | Hệ thống S1+S2 chạy ổn định trên 3 máy | S2.5.5 |
| S2.5.9 | Họp Sprint Review — Demo S2 | 2 | 31/03 | 31/03 | Máy chiếu, App | 400.000 | A | R | R | A | A | Biên bản nghiệm thu, KH xác nhận nghiệp vụ kho đúng | S2.5.8 |
| S2.5.10 | Họp Sprint Retrospective | 2 | 31/03 | 31/03 | Phòng họp | 400.000 | A | R | R | C | I | Ghi nhận cải tiến quy trình kiểm thử hồi quy | S2.5.9 |
| | **TỔNG SPRINT 2** | **188** | **18/03** | **31/03** | | **37.600.000** | | | | | | | |

**Phân bổ:** NG ~74h · TN ~64h · NH ~50h

**Sản phẩm bàn giao Sprint 2:** Sprint Backlog · SRS (4 CN) · Source Code · ~330 TC mới + 100 hồi quy · Defect List (≥25) · HDSD cập nhật · Biên bản Review

---
---

## 🚀 SPRINT 3 — BÁN HÀNG POS & DỊCH VỤ

**Thời gian:** 01/04/2026 → 14/04/2026 (10 ngày)
**Mục tiêu:** Xây dựng nghiệp vụ POS end-to-end, kiểm soát vé, đoàn khách, thuê đồ. **Sprint trọng yếu nhất** — tất cả S1+S2 hội tụ tại đây.

### Lộ trình Sprint 3

| Giai đoạn | Ngày | Nội dung |
|:---------:|:-----|:---------|
| Khởi động | 01/04 | Họp Planning, review dependency POS ← S1+S2 |
| Phân tích | 01–02/04 | NH viết AC cho POS, Vé, Đoàn, Thuê Đồ |
| Song song | 03–09/04 | NH viết SRS+TC · NG code POS · TN dựng UI POS |
| Tích hợp | 10/04 | Nối POS ↔ Kho ↔ KH ↔ KM |
| Kiểm thử | 10–11/04 | TC mới + hồi quy S1+S2 + tích hợp cross-form |
| Sửa lỗi | 13/04 | Ưu tiên lỗi POS |
| Tổng kết | 14/04 | SRS, HDSD, Demo POS live, Retro |

### Bảng công việc Sprint 3

| ID | Công việc | Giờ | Bắt đầu | Kết thúc | Resource Name | Chi phí (VNĐ) | NG | TN | NH | GV | KH | Tiêu chí chất lượng (Acceptance Criteria) | Phụ thuộc |
|:---|:----------|:---:|:-------:|:--------:|:--------------|:-------------:|:--:|:--:|:--:|:--:|:--:|:------------------------------------------|:----------|
| **S3.0** | **Khởi động Sprint 3** | **10** | **01/04** | **02/04** | | **2.000.000** | | | | | | | |
| S3.0.1 | Họp Sprint Planning — chốt dependency POS, phân công | 4 | 01/04 | 01/04 | Phòng họp, Trello | 800.000 | A | R | R | C | C | Biên bản có danh sách dependency POS cần từ S1+S2 | S2.5.10 |
| S3.0.2 | Phân tích nghiệp vụ + Viết AC cho POS, Vé, Đoàn, Thuê Đồ | 6 | 01/04 | 02/04 | Word, Trello | 1.200.000 | S | I | R | I | C | AC chi tiết cho POS (≥ 10 tiêu chí), Vé, Đoàn, Thuê | S3.0.1 |
| **S3.1** | **POS Bán Hàng ⭐** | **72** | **03/04** | **10/04** | | **14.400.000** | | | | | | | |
| S3.1.1 | Xử lý nghiệp vụ Giỏ Hàng: thêm/xóa/đổi ĐVT, tính tổng | 8 | 03/04 | 06/04 | C#, BUS | 1.600.000 | R | I | I | I | I | Thêm SP→giỏ hiện đúng tên/giá/SL, đổi Lon↔Thùng giá tính lại | S2.0.3 |
| S3.1.2 | Xử lý nghiệp vụ Thanh Toán: 3 hình thức (TM/Ví RFID/CK) | 6 | 06/04 | 07/04 | C#, BUS | 1.200.000 | R | I | I | I | I | TM→trả dư. Ví→trừ+check đủ tiền. CK→ghi nhận | S3.1.1, S2.3.2 |
| S3.1.3 | Xử lý nghiệp vụ Chiết khấu VIP + KM (lấy cao nhất, không cộng) | 6 | 07/04 | 08/04 | C#, BUS | 1.200.000 | R | I | I | I | I | VIP giảm 5%, KM giảm 15% → áp 15% (cao hơn) | S2.3, S2.4 |
| S3.1.4 | Xử lý nghiệp vụ Tích điểm + Dùng điểm (100 điểm = 10.000đ) | 4 | 07/04 | 08/04 | C#, BUS | 800.000 | R | I | I | I | I | TT xong → KH cộng điểm. Dùng điểm → giảm tiền phải trả | S2.3.3 |
| S3.1.5 | Xử lý nghiệp vụ Trừ Kho + Phát Vé tự động sau bán | 6 | 08/04 | 09/04 | C#, BUS | 1.200.000 | R | I | I | I | I | Bán 1 Thùng→Kho trừ 24 Lon. Vé→popup Phát Vé sinh QR | S2.1, S3.1.2 |
| S3.1.6 | Thiết kế giao diện POS cảm ứng (3 cột: Menu—Giỏ—TT) | 12 | 03/04 | 08/04 | Guna UI2, Touch | 2.400.000 | I | R | I | I | C | Giao diện tối ưu 15.6", nút ≥ 48px, menu có ảnh thumbnail | S2.0.3 |
| S3.1.7 | Tích hợp camera quét mã vạch/QR + popup Menu SP | 6 | 06/04 | 09/04 | Camera, ZXing | 1.200.000 | S | R | I | I | I | Quét mã → SP tự thêm giỏ, có hình ảnh, SL cộng dồn | S3.1.1 |
| S3.1.8 | Nối POS hoàn chỉnh: quét→giỏ→KH→KM→thanh toán→vé | 8 | 09/04 | 10/04 | Backend + Frontend | 1.600.000 | R | R | I | I | I | Luồng chạy mượt end-to-end, không crash | S3.1.1→S3.1.7 |
| S3.1.9 | Unit Test POS (áp KM cao nhất, trừ ví check đủ, trừ kho quy đổi) | 4 | 07/04 | 08/04 | xUnit, Moq | 800.000 | R | I | I | I | I | Tất cả test pass, logic tiền không sai | S3.1.2→S3.1.5 |
| S3.1.10 | _(Song song)_ Viết SRS POS | 8 | 03/04 | 10/04 | Word | 1.600.000 | I | I | R | I | I | SRS POS chi tiết: Workflow 7 scenario, Prototype, Validation | S3.0.2 |
| S3.1.11 | _(Song song)_ Viết TC POS (~200 TC) | 8 | 03/04 | 10/04 | Excel | 1.600.000 | I | I | R | I | I | TC gồm: giỏ trống, hết hàng, KM hết hạn, ví không đủ | S3.0.2 |
| **S3.2** | **Kiểm Soát Vé (Cổng quét QR)** | **16** | **06/04** | **09/04** | | **3.200.000** | | | | | | | |
| S3.2.1 | Xử lý nghiệp vụ quét vé: kiểm hạn, số lượt, ghi lịch sử | 6 | 06/04 | 07/04 | C#, BUS, Camera | 1.200.000 | R | I | I | I | I | Vé hợp lệ→xanh+âm thanh. Hết lượt/hạn→đỏ+lý do | S3.0.2 |
| S3.2.2 | Thiết kế giao diện Kiểm Soát Vé (camera + Xanh/Đỏ) | 6 | 07/04 | 08/04 | Guna UI2, Camera | 1.200.000 | I | R | I | I | I | Màn hình quét rõ ràng, kết quả Xanh/Đỏ full screen | S3.2.1 |
| S3.2.3 | _(Song song)_ Viết SRS + TC Vé (~50 TC) | 4 | 06/04 | 09/04 | Word, Excel | 800.000 | I | I | R | I | I | TC: vé hết lượt, vé hôm qua, vé đã quét, vé giả | S3.0.2 |
| **S3.3** | **Đoàn Khách & Quầy Vé Lễ Tân** | **24** | **06/04** | **09/04** | | **4.800.000** | | | | | | | |
| S3.3.1 | Xử lý nghiệp vụ Đoàn: booking, quota, phát hàng 0đ | 6 | 06/04 | 07/04 | C#, BUS | 1.200.000 | R | I | I | I | C | Mã BK-XXX tự sinh, phát quá quota→từ chối | S2.3.1 |
| S3.3.2 | Thiết kế giao diện Đoàn + Quầy Vé + dialog Chọn DV | 8 | 07/04 | 09/04 | Guna UI2 | 1.600.000 | I | R | S | I | I | Booking có trạng thái (Chờ/Phục vụ/Hoàn tất), thao tác nhanh | S3.3.1 |
| S3.3.3 | Nối giao diện Đoàn ↔ POS | 4 | 09/04 | 09/04 | Backend + Frontend | 800.000 | S | R | I | I | I | Quét BK-XXX → POS phát 0đ → quota giảm đúng | S3.3.1, S3.1 |
| S3.3.4 | _(Song song)_ Viết SRS + TC Đoàn (~60 TC) | 6 | 06/04 | 09/04 | Word, Excel | 1.200.000 | I | I | R | I | I | TC: phát quá quota, đoàn hoàn tất, booking trùng ngày | S3.0.2 |
| **S3.4** | **Thuê Đồ (Phao, Tủ, Chòi + Cọc Ví)** | **20** | **06/04** | **09/04** | | **4.000.000** | | | | | | | |
| S3.4.1 | Xử lý nghiệp vụ Thuê: cọc ví → trả → hoàn/phạt | 6 | 06/04 | 07/04 | C#, BUS | 1.200.000 | R | I | I | I | I | Thuê→ví giảm cọc. Trả→hoàn cọc. Hư→phạt thêm | S2.3.2 |
| S3.4.2 | Thiết kế giao diện Thuê Đồ (DS + trạng thái + popup cọc) | 6 | 07/04 | 09/04 | Guna UI2 | 1.200.000 | I | R | I | I | I | Mã màu: Sẵn/Đang thuê/Hư, bấm Thuê→popup xác nhận | S3.4.1 |
| S3.4.3 | Nối giao diện ↔ dữ liệu | 4 | 09/04 | 09/04 | Backend + Frontend | 800.000 | S | R | I | I | I | Thuê→ví giảm→trả→ví hoàn→Tab GD KH hiện cả 2 | S3.4.1, S3.4.2 |
| S3.4.4 | _(Song song)_ Viết SRS + TC Thuê Đồ (~40 TC) | 4 | 06/04 | 09/04 | Word, Excel | 800.000 | I | I | R | I | I | TC: ví không đủ cọc, trả quá hạn, đồ hư phạt | S3.0.2 |
| **S3.5** | **Kiểm thử, Hồi quy S1+S2, Tích hợp & Tổng kết** | **44** | **10/04** | **14/04** | | **8.800.000** | | | | | | | |
| S3.5.1 | Thực thi TC Sprint 3 (~350 TC mới) | 6 | 10/04 | 11/04 | App chạy thật | 1.200.000 | I | S | R | I | I | ≥ 90% TC Pass | S3.1→S3.4 |
| S3.5.2 | ⟲ Hồi quy S1 (50 TC) + S2 (100 TC) | 4 | 10/04 | 11/04 | App chạy thật | 800.000 | I | S | R | I | I | ≥ 95% TC cũ Pass, form S1+S2 không bị ảnh hưởng | S1, S2 |
| S3.5.3 | 🔗 Tích hợp: POS→Kho trừ, POS→KH tích điểm, POS→Ví trừ | 4 | 10/04 | 11/04 | App chạy thật | 800.000 | S | I | R | I | I | Bán 1 Thùng→frmKhoHang tồn -24 Lon. KH VIP +×2 điểm | S3.1, S2.1, S2.3 |
| S3.5.4 | 🔗 Tích hợp: POS→Vé→Cổng, Thuê→Ví→frmKH | 2 | 10/04 | 11/04 | App chạy thật | 400.000 | I | I | R | I | I | Bán vé→QR→quét cổng→check-in. Thuê→ví giảm→frmKH hiện | S3.2, S3.4 |
| S3.5.5 | Defect List S3 (≥ 20 mới + 5 hồi quy) | 2 | 11/04 | 11/04 | Excel | 400.000 | I | I | R | I | I | Lỗi phân loại rõ: POS / Vé / Đoàn / Thuê / Hồi quy | S3.5.1→S3.5.4 |
| S3.5.6 | Sửa lỗi Sprint 3 (ưu tiên POS) | 10 | 11/04 | 13/04 | C#, IDE | 2.000.000 | R | R | S | I | I | 0 lỗi Nghiêm trọng POS trước Demo | S3.5.5 |
| S3.5.7 | Chuẩn hóa SRS tổng hợp Sprint 3 | 4 | 13/04 | 14/04 | Word | 800.000 | I | S | R | C | I | SRS Word S3 hoàn chỉnh | S3.1→S3.4 |
| S3.5.8 | Cập nhật HDSD (bổ sung POS, Vé, Đoàn, Thuê) | 2 | 13/04 | 14/04 | Word, Screenshots | 400.000 | I | I | R | I | I | HDSD có hình minh họa luồng POS | S3.5.6 |
| S3.5.9 | Triển khai gia tăng lên máy thật | 2 | 13/04 | 14/04 | SQL Server, LAN | 400.000 | R | S | I | I | I | S1+S2+S3 chạy ổn trên 3 máy | S3.5.6 |
| S3.5.10 | Sprint Review — Demo POS live | 2 | 14/04 | 14/04 | Máy chiếu, App | 400.000 | A | R | R | A | A | Demo bán hàng live: quét→giỏ→TT→vé QR | S3.5.9 |
| S3.5.11 | Sprint Retrospective | 2 | 14/04 | 14/04 | Phòng họp | 400.000 | A | R | R | C | I | Đánh giá hiệu quả tích hợp cross-form | S3.5.10 |
| S3.5.12 | _(Tùy chọn)_ Quay video demo POS end-to-end | 2 | 14/04 | 14/04 | Máy quay, App | 400.000 | I | R | S | I | I | Video ≥ 3 phút, rõ luồng POS từ quét đến vé | S3.5.9 |
| | **TỔNG SPRINT 3** | **186** | **01/04** | **14/04** | | **37.200.000** | | | | | | | |

**Phân bổ:** NG ~78h · TN ~66h · NH ~42h

**Sản phẩm bàn giao Sprint 3:** Sprint Backlog · SRS (4 CN) · Source Code · ~350 TC mới + 150 hồi quy · Defect List (≥25) · HDSD · Biên bản Review · Video POS

---
---

## 🚀 SPRINT 4 — DỊCH VỤ LƯU TRÚ ĐẠI NAM

**Thời gian:** 15/04/2026 → 28/04/2026 (10 ngày)
**Mục tiêu:** Khách sạn (200+ phòng), Nhà hàng (đặt bàn + gọi món), Gửi xe (OCR).

### Lộ trình Sprint 4

| Giai đoạn | Ngày | Nội dung |
|:---------:|:-----|:---------|
| Khởi động | 15/04 | Họp Planning, phân 3 module |
| Phân tích | 15–16/04 | NH viết AC cho KS, NH, Xe |
| Song song | 17–24/04 | NH viết SRS+TC · NG code · TN dựng UI |
| Tích hợp | 24–25/04 | Nối giao diện |
| Kiểm thử | 25–26/04 | TC mới + hồi quy S1→S3 |
| Sửa lỗi | 27/04 | Sửa lỗi |
| Tổng kết | 28/04 | SRS, HDSD, Demo, Retro |

### Bảng công việc Sprint 4

| ID | Công việc | Giờ | Bắt đầu | Kết thúc | Resource Name | Chi phí (VNĐ) | NG | TN | NH | GV | KH | Tiêu chí chất lượng (Acceptance Criteria) | Phụ thuộc |
|:---|:----------|:---:|:-------:|:--------:|:--------------|:-------------:|:--:|:--:|:--:|:--:|:--:|:------------------------------------------|:----------|
| **S4.0** | **Khởi động Sprint 4** | **10** | **15/04** | **16/04** | | **2.000.000** | | | | | | | |
| S4.0.1 | Họp Sprint Planning | 4 | 15/04 | 15/04 | Phòng họp, Trello | 800.000 | A | R | R | C | C | Biên bản phân công: NG-Xe, TN-KS/NH, NH-QA | S3.5.11 |
| S4.0.2 | Phân tích + Viết AC cho KS, NH, Xe | 6 | 15/04 | 16/04 | Word, Trello | 1.200.000 | S | S | R | I | C | AC chi tiết cho từng module | S4.0.1 |
| **S4.1** | **Khách Sạn & Đặt Phòng** | **46** | **17/04** | **25/04** | | **9.200.000** | | | | | | | |
| S4.1.1 | Xử lý nghiệp vụ Đặt Phòng: check-in/out, giữ, hủy, tính tiền | 8 | 17/04 | 20/04 | C#, BUS | 1.600.000 | S | R | I | I | C | Phòng chuyển trạng thái đúng, giá = Đêm × Giá/đêm + Phụ thu | S4.0.2 |
| S4.1.2 | Xử lý Đặt Phòng Đoàn hàng loạt | 4 | 20/04 | 21/04 | C#, BUS | 800.000 | S | R | I | I | I | Chọn đoàn BK-XXX → gán N phòng → đúng nhóm | S3.3, S4.1.1 |
| S4.1.3 | Thiết kế Sơ Đồ Phòng (Visual Layout: Xanh/Đỏ/Vàng) | 10 | 17/04 | 22/04 | Guna UI2, Custom | 2.000.000 | I | R | I | I | C | Layout phòng trực quan, click→thao tác, mã màu rõ | S4.0.2 |
| S4.1.4 | Dialog Booking + Reserve + Đặt Đoàn | 6 | 21/04 | 23/04 | Guna UI2, Popup | 1.200.000 | I | R | S | I | I | 3 dialog đúng nghiệp vụ, lưu→quay lại sơ đồ cập nhật | S4.1.1 |
| S4.1.5 | Nối giao diện KS ↔ dữ liệu | 6 | 23/04 | 24/04 | Backend + Frontend | 1.200.000 | S | R | I | I | I | Click phòng trống→booking→check-in→sơ đồ đổi màu | S4.1.1→S4.1.4 |
| S4.1.6 | _(Song song)_ Viết SRS + TC KS (~80 TC) | 12 | 17/04 | 25/04 | Word, Excel | 2.400.000 | I | I | R | I | I | SRS+TC đủ: đặt trùng, check-out quá hạn, đoàn hàng loạt | S4.0.2 |
| **S4.2** | **Nhà Hàng & Đặt Bàn** | **42** | **17/04** | **25/04** | | **8.400.000** | | | | | | | |
| S4.2.1 | Xử lý nghiệp vụ Bàn + Gọi Món + Thanh toán (trừ kho NVL) | 10 | 17/04 | 21/04 | C#, BUS | 2.000.000 | R | S | I | I | I | Gọi Pepsi→Kho trừ. TT xong→bàn Xanh. Chia bill hỗ trợ | S4.0.2, S2.1 |
| S4.2.2 | Thiết kế Layout Bàn + Đặt bàn + Ghép bàn | 10 | 17/04 | 23/04 | Guna UI2, Custom | 2.000.000 | I | R | I | I | C | Layout bàn realtime, ghép bàn cộng sức chứa, đặt trước vàng | S4.0.2 |
| S4.2.3 | Dialog Thanh Toán nhiều hình thức | 4 | 22/04 | 23/04 | Guna UI2 | 800.000 | I | R | I | I | I | Chọn TM/Ví/CK/Chia bill, số tiền đúng | S4.2.1 |
| S4.2.4 | Nối giao diện NH ↔ dữ liệu | 6 | 23/04 | 24/04 | Backend + Frontend | 1.200.000 | S | R | I | I | I | Walk-in→bàn Đỏ→gọi món→TT→bàn Xanh | S4.2.1→S4.2.3 |
| S4.2.5 | _(Song song)_ Viết SRS + TC NH (~70 TC) | 12 | 17/04 | 25/04 | Word, Excel | 2.400.000 | I | I | R | I | I | TC: ghép bàn, gọi món hết kho, chia bill lẻ, đặt trước trùng | S4.0.2 |
| **S4.3** | **Gửi Xe (OCR + Barcode)** | **28** | **17/04** | **24/04** | | **5.600.000** | | | | | | | |
| S4.3.1 | Xử lý Xe Vào (OCR biển số + barcode) + Xe Ra (tính phí) | 10 | 17/04 | 21/04 | C#, OCR, Camera | 2.000.000 | R | I | I | I | I | Camera chụp→OCR đọc→barcode in. Xe ra→tính phí×thời gian | S4.0.2 |
| S4.3.2 | Thiết kế giao diện Gửi Xe (camera + bảng xe đang gửi) | 8 | 20/04 | 23/04 | Guna UI2, Camera | 1.600.000 | I | R | I | I | I | Camera live nửa trên, danh sách xe nửa dưới, nút Vào/Ra | S4.3.1 |
| S4.3.3 | Nối giao diện ↔ dữ liệu | 4 | 23/04 | 24/04 | Backend + Frontend | 800.000 | R | R | I | I | I | Vào→biển hiện→barcode in→ra→quét→tính phí→thu | S4.3.1, S4.3.2 |
| S4.3.4 | _(Song song)_ Viết SRS + TC Xe (~50 TC) | 6 | 17/04 | 24/04 | Word, Excel | 1.200.000 | I | I | R | I | I | TC: OCR sai→nhập tay, qua đêm, ví RFID TT xe | S4.0.2 |
| **S4.4** | **Kiểm thử, Hồi quy & Tổng kết** | **38** | **25/04** | **28/04** | | **7.600.000** | | | | | | | |
| S4.4.1 | Thực thi TC Sprint 4 (~200 TC mới) | 6 | 25/04 | 26/04 | App chạy thật | 1.200.000 | I | S | R | I | I | ≥ 90% Pass | S4.1→S4.3 |
| S4.4.2 | ⟲ Hồi quy S1(30) + S2(50) + S3(80 POS) | 4 | 25/04 | 26/04 | App chạy thật | 800.000 | I | S | R | I | I | ≥ 95% Pass, POS vẫn bán đúng | S1→S3 |
| S4.4.3 | 🔗 Tích hợp: KS×Ví, KS×Đoàn, NH×Kho, Xe×Ví RFID | 4 | 25/04 | 26/04 | App chạy thật | 800.000 | S | I | R | I | I | TT phòng ví→frmKH dư giảm. Gọi món→Kho trừ NVL | S4.1→S4.3, S2 |
| S4.4.4 | Defect List S4 (≥ 20 mới + 5 hồi quy) | 2 | 26/04 | 26/04 | Excel | 400.000 | I | I | R | I | I | Lỗi phân loại: KS / NH / Xe / Hồi quy | S4.4.1→S4.4.3 |
| S4.4.5 | Sửa lỗi Sprint 4 | 8 | 26/04 | 27/04 | C#, IDE | 1.600.000 | R | R | S | I | I | 0 Nghiêm trọng trước Demo | S4.4.4 |
| S4.4.6 | Chuẩn hóa SRS S4 + Cập nhật HDSD | 6 | 27/04 | 28/04 | Word | 1.200.000 | I | S | R | C | I | SRS Word S4 + HDSD bổ sung KS, NH, Xe | S4.1→S4.3 |
| S4.4.7 | Triển khai gia tăng | 2 | 27/04 | 28/04 | SQL Server, LAN | 400.000 | R | S | I | I | I | S1→S4 chạy ổn trên máy thật | S4.4.5 |
| S4.4.8 | Sprint Review — Demo KS + NH + Xe | 2 | 28/04 | 28/04 | Máy chiếu, App | 400.000 | A | R | R | A | A | Demo sơ đồ phòng, layout bàn, quét biển số live | S4.4.7 |
| S4.4.9 | Sprint Retrospective | 2 | 28/04 | 28/04 | Phòng họp | 400.000 | A | R | R | C | I | Đánh giá hiệu quả chia 3 module song song | S4.4.8 |
| S4.4.10 | _(Tùy chọn)_ Quay video demo KS + NH | 2 | 28/04 | 28/04 | Máy quay | 400.000 | I | R | S | I | I | Video rõ luồng đặt phòng + gọi món | S4.4.7 |
| | **TỔNG SPRINT 4** | **164** | **15/04** | **28/04** | | **32.800.000** | | | | | | | |

**Phân bổ:** NG ~60h · TN ~62h · NH ~42h

**Sản phẩm bàn giao Sprint 4:** Sprint Backlog · SRS (3 CN) · Source Code · ~200 TC mới + 160 hồi quy · Defect List (≥25) · HDSD · Biên bản Review

---
---

## 🚀 SPRINT 5 — HOÀN THIỆN, BÁO CÁO & BÀN GIAO

**Thời gian:** 29/04/2026 → 12/05/2026 (10 ngày)
**Mục tiêu:** Hoàn thiện module phụ trợ, Dashboard báo cáo, AI + Web B2C, kiểm thử hồi quy toàn bộ, bàn giao sản phẩm.

### Lộ trình Sprint 5

| Giai đoạn | Ngày | Nội dung |
|:---------:|:-----|:---------|
| Khởi động | 29/04 | Họp Planning, checklist bàn giao |
| Phân tích | 29/04 | NH viết AC cho các CN S5 |
| Song song | 04–08/05 | NH viết SRS+TC · NG code API/AI · TN dựng UI |
| Tích hợp | 08/05 | Nối Dashboard + Web ↔ Data |
| Kiểm thử | 09–11/05 | Hồi quy TOÀN BỘ + TC mới |
| Bàn giao | 11–12/05 | SRS cuối, HDSD, Video, Deploy, Demo |

### Bảng công việc Sprint 5

| ID | Công việc | Giờ | Bắt đầu | Kết thúc | Resource Name | Chi phí (VNĐ) | NG | TN | NH | GV | KH | Tiêu chí chất lượng (Acceptance Criteria) | Phụ thuộc |
|:---|:----------|:---:|:-------:|:--------:|:--------------|:-------------:|:--:|:--:|:--:|:--:|:--:|:------------------------------------------|:----------|
| **S5.0** | **Khởi động Sprint 5** | **6** | **29/04** | **29/04** | | **1.200.000** | | | | | | | |
| S5.0.1 | Họp Sprint Planning — Sprint cuối, checklist bàn giao | 4 | 29/04 | 29/04 | Phòng họp | 800.000 | A | R | R | C | C | Biên bản có checklist đầy đủ | S4.4.9 |
| S5.0.2 | Phân tích + Viết AC chức năng S5 | 2 | 29/04 | 29/04 | Word | 400.000 | I | I | R | I | I | AC cho Thú, Biển, Bảo trì, Dashboard, AI, Web | S5.0.1 |
| **S5.1** | **Thú + Biển + Bảo Trì (3 module nhỏ)** | **36** | **04/05** | **07/05** | | **7.200.000** | | | | | | | |
| S5.1.1 | Xử lý nghiệp vụ Động Vật + Biển + Bảo Trì | 10 | 04/05 | 05/05 | C#, BUS | 2.000.000 | R | S | R | I | C | Thú: 100+ loài. Biển: cảnh báo pH. Bảo trì: lịch + quá hạn | S1.2 |
| S5.1.2 | Thiết kế giao diện 3 form | 12 | 04/05 | 07/05 | Guna UI2 | 2.400.000 | I | R | I | I | I | 3 form giao diện hoàn chỉnh, mã màu trạng thái rõ | S5.1.1 |
| S5.1.3 | Nối giao diện ↔ dữ liệu | 4 | 07/05 | 07/05 | Backend + Frontend | 800.000 | S | R | I | I | I | Thêm loài→hiện đúng, pH>8→cảnh báo đỏ, quá hạn→vàng | S5.1.1, S5.1.2 |
| S5.1.4 | _(Song song)_ Viết SRS + TC 3 CN (~80 TC) | 10 | 04/05 | 07/05 | Word, Excel | 2.000.000 | I | I | R | I | I | SRS 3 CN + TC đủ trường hợp | S5.0.2 |
| **S5.2** | **Dashboard Báo Cáo Tổng Hợp** | **18** | **04/05** | **08/05** | | **3.600.000** | | | | | | | |
| S5.2.1 | Xử lý logic Dashboard: tổng hợp doanh thu POS+NH+KS | 6 | 04/05 | 06/05 | C#, BUS | 1.200.000 | R | I | I | I | I | 4 KPI đúng: DT = Σ(POS+NH+KS), Tồn kho khớp Kho Hàng | S3.1, S4.1, S4.2 |
| S5.2.2 | Thiết kế giao diện Dashboard (KPI + biểu đồ + filter) | 8 | 05/05 | 07/05 | Guna UI2, Chart | 1.600.000 | I | R | I | I | I | Biểu đồ doanh thu theo ngày, filter range, drill-down | S5.2.1 |
| S5.2.3 | _(Song song)_ Viết SRS + TC Dashboard (~20 TC) | 4 | 04/05 | 08/05 | Word, Excel | 800.000 | I | I | R | I | I | TC: lọc ngày→đúng, DT khớp frmDonHang tổng | S5.0.2 |
| **S5.3** | **AI Chatbox + Web Bán Vé (B2C)** | **40** | **04/05** | **08/05** | | **8.000.000** | | | | | | | |
| S5.3.1 | API bán vé (Kestrel C#) + bảo mật server-side | 10 | 04/05 | 07/05 | C#, Kestrel, API | 2.000.000 | R | I | I | I | I | API trả đúng SP+giá, giá 0đ client→reject, tính lại server | S3.1 |
| S5.3.2 | Website đặt vé (Blazor WASM) | 10 | 04/05 | 07/05 | Blazor, HTML/CSS | 2.000.000 | I | R | I | I | C | Web responsive, giỏ hàng OK, TT→sinh vé QR | S5.3.1 |
| S5.3.3 | Nối Web ↔ API ↔ CSDL | 4 | 07/05 | 08/05 | Backend + Frontend | 800.000 | R | R | I | I | I | Mua vé web→API tạo đơn→WinForms thấy đơn→vé QR quét OK | S5.3.1, S5.3.2 |
| S5.3.4 | AI Chatbox: giao diện chat + điều khiển form | 8 | 05/05 | 08/05 | C#, AI Service | 1.600.000 | R | S | I | I | I | Chat "Tìm KH X"→lưới KH chọn đúng dòng | S2.3 |
| S5.3.5 | _(Song song)_ Viết SRS + TC AI + Web (~40 TC) | 8 | 04/05 | 08/05 | Word, Excel | 1.600.000 | I | I | R | I | I | TC: hack giá web→reject, AI lệnh sai→không crash | S5.0.2 |
| **S5.4** | **Hồi Quy Toàn Bộ, Bàn Giao & Triển Khai** | **56** | **08/05** | **12/05** | | **11.200.000** | | | | | | | |
| S5.4.1 | Thực thi TC S5 (~140 TC mới) | 4 | 08/05 | 09/05 | App chạy thật | 800.000 | I | S | R | I | I | ≥ 90% Pass | S5.1→S5.3 |
| S5.4.2 | ⟲ Hồi quy TOÀN BỘ S1→S4: 200 TC ưu tiên cao | 6 | 08/05 | 09/05 | App chạy thật | 1.200.000 | S | S | R | I | I | ≥ 95% Pass toàn hệ thống, 0 Nghiêm trọng mới | S1→S4 |
| S5.4.3 | 🔗 Tích hợp: Dashboard khớp POS+KS+NH, Web→Vé→Cổng | 4 | 09/05 | 09/05 | App chạy thật | 800.000 | I | I | R | I | I | DT Dashboard = Σ tất cả kênh. Vé web quét cổng OK | S5.2, S5.3, S3.2 |
| S5.4.4 | Defect List S5 (≥ 20 mới + 5 hồi quy cuối) | 2 | 09/05 | 09/05 | Excel | 400.000 | I | I | R | I | I | Defect tổng S1→S5, trạng thái đóng/mở rõ ràng | S5.4.1→S5.4.3 |
| S5.4.5 | Sửa lỗi cuối cùng toàn hệ thống | 8 | 09/05 | 11/05 | C#, IDE | 1.600.000 | R | R | S | I | I | Tất cả Nghiêm trọng + Quan trọng đã sửa xong | S5.4.4 |
| S5.4.6 | Tạo dữ liệu mẫu thuyết phục (Seed Data) | 4 | 09/05 | 11/05 | SQL, Script | 800.000 | I | I | R | I | I | Dữ liệu mẫu trông thực tế, đủ demo các luồng | S5.4.5 |
| S5.4.7 | Chuẩn hóa SRS tổng hợp cuối cùng (gom S1→S5) | 6 | 11/05 | 12/05 | Word | 1.200.000 | I | S | R | C | I | File SRS Word tổng ~20 chức năng, đầy đủ theo template | S5.1→S5.3 |
| S5.4.8 | HDSD phiên bản cuối cùng (phân theo vai trò) | 4 | 11/05 | 12/05 | Word, Screenshots | 800.000 | I | S | R | C | I | HDSD có hình từng bước, chia: Admin/Thu ngân/Lễ tân | S5.4.5 |
| S5.4.9 | Quay video demo toàn bộ hệ thống (≥ 10 phút) | 4 | 11/05 | 12/05 | Máy quay, OBS | 800.000 | I | R | R | I | I | Video clear, thuyết minh, thể hiện tất cả CN chính | S5.4.6 |
| S5.4.10 | DevOps: `run.bat` + `deploy.bat` | 2 | 11/05 | 11/05 | Batch script | 400.000 | R | I | I | I | I | 1 click khởi động: SQL Server + API + Web | S5.3 |
| S5.4.11 | Triển khai LAN (Server + 2 Client) máy thật | 4 | 11/05 | 12/05 | SQL Server, LAN | 800.000 | R | R | S | I | I | 3 máy hoạt động: Server + 2 Client POS chạy mượt | S5.4.10 |
| S5.4.12 | Sprint Review cuối cùng — Demo TOÀN BỘ | 2 | 12/05 | 12/05 | Máy chiếu, App | 400.000 | A | R | R | A | A | Biên bản nghiệm thu cuối cùng, KH ký chấp nhận | S5.4.11 |
| S5.4.13 | Retrospective cuối — Tổng kết dự án | 2 | 12/05 | 12/05 | Phòng họp | 400.000 | A | R | R | A | I | Báo cáo tổng kết: 5 Sprint, 20 CN, 10 tuần | S5.4.12 |
| S5.4.14 | Khóa phiên bản v1.0, đóng gói nộp | 4 | 12/05 | 12/05 | Git, ZIP | 800.000 | R | I | I | A | I | Tag v1.0 Git, folder nộp: Code + DB + Docs + Video | S5.4.13 |
| | **TỔNG SPRINT 5** | **156** | **29/04** | **12/05** | | **31.200.000** | | | | | | | |

**Phân bổ:** NG ~62h · TN ~52h · NH ~42h

**Sản phẩm bàn giao Sprint 5 (BÀN GIAO CUỐI CÙNG):**
- Sprint Backlog S5
- **SRS tổng hợp S1→S5** (20 chức năng, đầy đủ Use Case, Workflow, Prototype, Ràng buộc)
- **Source Code hoàn chỉnh** (CSDL + 20 chức năng)
- Test Case (~140 TC mới + 200 TC hồi quy S1→S4)
- **Defect List tổng hợp** (S1→S5)
- **Hướng dẫn cài đặt + sử dụng** phiên bản cuối
- **Video demo** toàn hệ thống (≥ 10 phút có thuyết minh)
- File `run.bat` + `deploy.bat`
- Source code đóng gói + DB script
- Biên bản Sprint Review cuối cùng có chữ ký

---
---

## TỔNG KẾT TOÀN DỰ ÁN

### Tổng quan 5 Sprint

| Sprint | Thời gian | Số CN | Giờ | Chi phí (VNĐ) | TC mới | Hồi quy | Sản phẩm chính |
|:------:|:----------|:-----:|:---:|:-------------:|:------:|:-------:|:---------------|
| S1 | 04/03→17/03 | 4 | 196 | 39.200.000 | ~457 | — | CSDL toàn bộ + Danh mục |
| S2 | 18/03→31/03 | 4 | 188 | 37.600.000 | ~330 | 100 (S1) | Kho + KH + Ví + KM |
| S3 | 01/04→14/04 | 4 | 186 | 37.200.000 | ~350 | 150 (S1+S2) | POS ⭐ + Vé + Đoàn + Thuê |
| S4 | 15/04→28/04 | 3 | 164 | 32.800.000 | ~200 | 160 (S1→S3) | KS + NH + Xe |
| S5 | 29/04→12/05 | 5 | 156 | 31.200.000 | ~140 | 200 (S1→S4) | Dashboard + AI + Web + Bàn giao |
| **Tổng** | **10 tuần** | **20** | **890** | **178.000.000** | **~1.477** | **610** | **20 chức năng hoàn chỉnh** |

### Phân bổ tổng theo thành viên

| Thành viên | S1 | S2 | S3 | S4 | S5 | **Tổng** | Vai trò chính |
|:-----------|:--:|:--:|:--:|:--:|:--:|:--------:|:--------------|
| **Nguyên** | 76 | 74 | 78 | 60 | 62 | **350** | CSDL, BUS, kiến trúc, API, DevOps |
| **Tấn** | 68 | 64 | 66 | 62 | 52 | **312** | Giao diện, nối dữ liệu, video demo |
| **Nhi** | 52 | 50 | 42 | 42 | 42 | **228** | SRS, Test Case, Defect, HDSD |

### Checklist bàn giao mỗi Sprint

| # | Sản phẩm | S1 | S2 | S3 | S4 | S5 |
|:-:|:---------|:--:|:--:|:--:|:--:|:--:|
| 1 | Sprint Backlog | ✅ | ✅ | ✅ | ✅ | ✅ |
| 2 | SRS (Use Case, Workflow, Prototype, Ràng buộc) | ✅ | ✅ | ✅ | ✅ | ✅ Tổng |
| 3 | Source Code (chạy được 100%) | ✅ | ✅ | ✅ | ✅ | ✅ |
| 4 | Test Case (≥ 200 TC / Sprint, gồm hồi quy) | ✅ | ✅ | ✅ | ✅ | ✅ |
| 5 | Defect List (≥ 25 lỗi: 20 mới + 5 hồi quy) | ✅ | ✅ | ✅ | ✅ | ✅ |
| 6 | Hướng dẫn cài đặt + sử dụng | ✅ | ✅ | ✅ | ✅ | ✅ Cuối |
| 7 | Biên bản Sprint Review (có chữ ký) | ✅ | ✅ | ✅ | ✅ | ✅ |
| 8 | Video demo | — | — | ✅ | ✅ | ✅ ≥10' |
| 9 | Triển khai máy thật (Server + Client LAN) | ✅ | ✅ | ✅ | ✅ | ✅ |
