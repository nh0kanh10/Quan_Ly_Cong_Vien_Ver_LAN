# SPRINT BACKLOG — HỆ THỐNG QUẢN LÝ KHU DU LỊCH ĐẠI NAM

**Mã dự án:** SD001 · **Nhóm:** 15 · **Lớp:** 25211CNC10762001
**Thành viên:** Nguyên (Leader) · Tấn · Nhi
**GVHD:** Th. Phan Gia Phước

---
---

# SPRINT BACKLOG 1 — NỀN TẢNG & QUẢN LÝ DANH MỤC

**Thời gian:** 04/03/2026 → 17/03/2026 (2 tuần)
**Mục tiêu Sprint:** Thiết kế toàn bộ cơ sở dữ liệu hệ thống, xây dựng khung ứng dụng chính và 4 nhóm chức năng danh mục cốt lõi phục vụ nền tảng cho các Sprint tiếp theo.

## Các chức năng trong Sprint 1

| STT | Chức năng | Mô tả ngắn | Phụ trách |
|:---:|:----------|:------------|:----------|
| 1 | Đăng nhập & Khung ứng dụng | Xác thực người dùng, mã hóa mật khẩu, khung Ribbon Bar điều hướng | Nguyên, Tấn |
| 2 | Quản lý Khu Vực & Trò Chơi | CRUD khu vực công viên + trò chơi/thiết bị giải trí theo từng khu | Nguyên, Tấn |
| 3 | Quản lý Sản Phẩm, ĐVT, Combo & Bảng Giá | Danh mục sản phẩm 3 Tab (thông tin, quy đổi đơn vị tính, bảng giá linh hoạt theo loại ngày) | Nguyên, Tấn |
| 4 | Quản lý Nhân Viên & Lịch Ca | CRUD nhân viên + phân ca làm việc dạng tuần, kiểm tra xung đột ca | Nguyên, Tấn |
| 5 | Phân Quyền RBAC | Cấp quyền theo vai trò, ẩn/hiện chức năng trên giao diện tùy role | Nguyên, Tấn |

## Các công việc trong Sprint 1

| STT | Công việc | Thời gian | Ngày | Phụ trách |
|:---:|:----------|:---------:|:-----|:----------|
| 1 | Họp Sprint Planning — xác định mục tiêu, phân công nhiệm vụ | 4h | 04/03 | Cả nhóm |
| 2 | Thiết kế cơ sở dữ liệu toàn bộ hệ thống (~50 bảng dự kiến cho 5 Sprint) | 8h | 04–05/03 | Nguyên |
| 3 | Phân tích nghiệp vụ + Viết Acceptance Criteria cho 4 chức năng Sprint 1 | 8h | 04–05/03 | Nhi |
| 4 | Phác thảo Wireframe giao diện cho 4 chức năng | 4h | 04–05/03 | Tấn |
| 5 | Viết SRS Sprint 1 chi tiết (Use Case, Workflow, Prototype, Ràng buộc) | 20h | 06–12/03 | Nhi |
| 6 | Lập trình tầng nghiệp vụ (BUS) cho 5 chức năng | 38h | 06–12/03 | Nguyên |
| 7 | Thiết kế giao diện (GUI) cho 5 chức năng | 38h | 06–12/03 | Tấn |
| 8 | Nối giao diện ↔ dữ liệu toàn bộ form Sprint 1 | 16h | 10–12/03 | Tấn, Nguyên hỗ trợ |
| 9 | Unit Test nghiệp vụ phức tạp tầng BUS (quy đổi ĐVT, tính giá theo ngày) | 2h | 10–11/03 | Nguyên |
| 10 | Viết Test Case Sprint 1 (~457 TC, dựa trên AC đã viết) | 14h | 06–12/03 | Nhi |
| 11 | Thực thi Test Case trên giao diện (~457 TC) | 6h | 13–14/03 | Nhi, Tấn hỗ trợ |
| 12 | Ghi nhận Defect List (≥ 25 lỗi, có KQ mong đợi vs KQ thực tế) | 2h | 13–14/03 | Nhi |
| 13 | Sửa lỗi Nghiêm trọng + Quan trọng | 8h | 14–16/03 | Nguyên, Tấn |
| 14 | Chuẩn hóa SRS tổng hợp Sprint 1 (gom thành file Word chính thức) | 4h | 16–17/03 | Nhi |
| 15 | Viết hướng dẫn cài đặt và sử dụng Sprint 1 (có hình minh họa) | 2h | 16–17/03 | Nhi |
| 16 | Triển khai lên hệ thống thật (Server SQL + 2 Client WinForms qua LAN) | 2h | 16–17/03 | Nguyên, Tấn hỗ trợ |
| 17 | Họp Sprint Review — Demo sản phẩm Sprint 1 cho GVHD + Khách hàng | 2h | 17/03 | Cả nhóm |
| 18 | Họp Sprint Retrospective — rút kinh nghiệm | 2h | 17/03 | Cả nhóm |

**Sản phẩm bàn giao:** Sprint BackLog · SRS S1 · Source Code · ~457 TC · Defect List (≥25 lỗi) · HDSD · Biên bản Review

---
---

# SPRINT BACKLOG 2 — KHO HÀNG & QUẢN LÝ KHÁCH HÀNG

**Thời gian:** 18/03/2026 → 31/03/2026 (2 tuần)
**Mục tiêu Sprint:** Xây dựng quản lý kho hàng toàn diện (nhập—xuất—kiểm—tồn—thẻ kho) và quản lý khách hàng tích hợp ví điện tử RFID + hệ thống tích điểm Loyalty, sẵn sàng phục vụ POS Sprint 3.

## Các chức năng trong Sprint 2

| STT | Chức năng | Mô tả ngắn | Phụ trách |
|:---:|:----------|:------------|:----------|
| 1 | Quản lý Kho Hàng (Nhập—Xuất—Kiểm—Tồn—Thẻ Kho) | Nhập kho quy đổi ĐVT, xuất kho chặn âm, kiểm kho Blind Mode, thẻ kho ghi lịch sử biến động | Nguyên, Tấn |
| 2 | Quản lý Đơn Hàng & Phiếu Thu/Chi | Lịch sử đơn hàng, phiếu thu/chi liên kết giao dịch | Nguyên, Tấn |
| 3 | Quản lý Khách Hàng (Ví RFID + Loyalty) | CRUD khách hàng, cấp ví điện tử, gắn thẻ RFID, tích điểm theo hệ số hạng (VIP ×2, VVIP ×3) | Nguyên, Tấn |
| 4 | Khuyến Mãi & Marketing | Cấu hình khuyến mãi theo % hoặc cố định, thời hạn hiệu lực, quản lý chương trình marketing | Nguyên, Tấn |

## Các công việc trong Sprint 2

| STT | Công việc | Thời gian | Ngày | Phụ trách |
|:---:|:----------|:---------:|:-----|:----------|
| 1 | Họp Sprint Planning — review Sprint 1, phân công Sprint 2 | 4h | 18/03 | Cả nhóm |
| 2 | Chỉnh sửa CSDL (thêm cột, ràng buộc nếu cần từ phản hồi Sprint 1) | 2h | 18/03 | Nguyên |
| 3 | Phân tích nghiệp vụ + Viết Acceptance Criteria cho 4 chức năng Sprint 2 | 6h | 18–19/03 | Nhi |
| 4 | Viết SRS Sprint 2 chi tiết (Use Case, Workflow, Prototype, Ràng buộc) | 22h | 20–27/03 | Nhi |
| 5 | Lập trình tầng nghiệp vụ (BUS) cho Kho Hàng, Đơn Hàng, KH, KM | 36h | 20–26/03 | Nguyên |
| 6 | Thiết kế giao diện (GUI) cho Kho, ĐH, KH, KM | 34h | 20–26/03 | Tấn |
| 7 | Nối giao diện ↔ dữ liệu toàn bộ form Sprint 2 | 18h | 26–27/03 | Tấn, Nguyên hỗ trợ |
| 8 | Unit Test nghiệp vụ kho (cộng/trừ tồn, chặn âm, quy đổi) và KH (tích điểm hệ số) | 4h | 24–25/03 | Nguyên |
| 9 | Viết Test Case Sprint 2 (~330 TC mới, dựa trên AC) | 14h | 20–27/03 | Nhi |
| 10 | Thực thi Test Case Sprint 2 trên giao diện (~330 TC mới) | 6h | 27–28/03 | Nhi, Tấn hỗ trợ |
| 11 | Kiểm thử hồi quy Sprint 1 — chạy lại 100 TC ưu tiên cao | 4h | 27–28/03 | Nhi |
| 12 | Kiểm thử tích hợp S1×S2: SP→Nhập Kho→Tồn, NV→Phiếu, RBAC→chặn form | 4h | 27–28/03 | Nhi, Nguyên hỗ trợ |
| 13 | Ghi nhận Defect List (≥ 20 lỗi mới + 5 lỗi hồi quy Sprint 1) | 2h | 28/03 | Nhi |
| 14 | Sửa lỗi Sprint 2 | 8h | 28–30/03 | Nguyên, Tấn |
| 15 | Chuẩn hóa SRS tổng hợp Sprint 2 | 4h | 30–31/03 | Nhi |
| 16 | Cập nhật hướng dẫn sử dụng (bổ sung Kho, KH, KM) | 2h | 30–31/03 | Nhi |
| 17 | Triển khai gia tăng lên hệ thống thật (Server + 2 Client LAN) | 2h | 30–31/03 | Nguyên, Tấn hỗ trợ |
| 18 | Họp Sprint Review — Demo Sprint 2 cho GVHD + Khách hàng | 2h | 31/03 | Cả nhóm |
| 19 | Họp Sprint Retrospective — rút kinh nghiệm | 2h | 31/03 | Cả nhóm |

**Sản phẩm bàn giao:** Sprint BackLog · SRS S2 · Source Code · ~330 TC mới + 100 TC hồi quy · Defect List (≥25 lỗi) · HDSD cập nhật · Biên bản Review

---
---

# SPRINT BACKLOG 3 — BÁN HÀNG POS & DỊCH VỤ

**Thời gian:** 01/04/2026 → 14/04/2026 (2 tuần)
**Mục tiêu Sprint:** Xây dựng nghiệp vụ bán hàng POS end-to-end (giỏ hàng → thanh toán → phát vé → trừ kho), kiểm soát vé cổng QR, quản lý đoàn khách, và dịch vụ thuê đồ. Đây là Sprint trọng yếu nhất — tất cả chức năng Sprint 1+2 hội tụ tại POS.

## Các chức năng trong Sprint 3

| STT | Chức năng | Mô tả ngắn | Phụ trách |
|:---:|:----------|:------------|:----------|
| 1 | POS Bán Hàng ⭐ | Giỏ hàng (thêm/xóa/đổi ĐVT), thanh toán 3 hình thức (TM/Ví RFID/CK), chiết khấu VIP + khuyến mãi (lấy mức cao nhất), tích điểm/dùng điểm, trừ kho tự động, phát vé QR | Nguyên, Tấn |
| 2 | Kiểm Soát Vé (Cổng quét QR) | Quét vé QR tại cổng → kiểm tra hạn sử dụng, số lượt → ghi lịch sử check-in | Nguyên, Tấn |
| 3 | Đoàn Khách & Quầy Vé Lễ Tân | Booking đoàn (BK-XXX), quota dịch vụ, phát hàng 0đ theo hợp đồng | Nguyên, Tấn |
| 4 | Thuê Đồ (Phao, Tủ, Chòi + Cọc Ví) | Thuê đồ gắn ví RFID (cọc → hẹn giờ → trả → hoàn cọc hoặc phạt) | Nguyên, Tấn |

## Các công việc trong Sprint 3

| STT | Công việc | Thời gian | Ngày | Phụ trách |
|:---:|:----------|:---------:|:-----|:----------|
| 1 | Họp Sprint Planning — chốt dependency POS ← Sprint 1+2, phân công | 4h | 01/04 | Cả nhóm |
| 2 | Phân tích nghiệp vụ + Viết Acceptance Criteria cho POS, Vé, Đoàn, Thuê Đồ | 6h | 01–02/04 | Nhi |
| 3 | Viết SRS Sprint 3 chi tiết (POS: 7 scenario Workflow, Prototype, Ràng buộc) | 24h | 03–10/04 | Nhi |
| 4 | Lập trình tầng nghiệp vụ POS: Giỏ hàng, Thanh toán, Chiết khấu, Trừ Kho, Phát Vé | 32h | 03–09/04 | Nguyên |
| 5 | Lập trình nghiệp vụ Kiểm Soát Vé, Đoàn Khách, Thuê Đồ | 18h | 06–09/04 | Nguyên |
| 6 | Thiết kế giao diện POS cảm ứng (3 cột: Menu—Giỏ—Thanh toán) | 12h | 03–08/04 | Tấn |
| 7 | Thiết kế giao diện Kiểm Soát Vé, Đoàn, Thuê Đồ | 20h | 06–09/04 | Tấn |
| 8 | Tích hợp camera quét mã vạch/QR vào POS + Cổng Vé | 6h | 06–09/04 | Tấn, Nguyên hỗ trợ |
| 9 | Nối POS hoàn chỉnh: quét → giỏ → KH → KM → thanh toán → vé → kho | 8h | 09–10/04 | Nguyên, Tấn |
| 10 | Unit Test POS (áp KM lấy cao nhất, trừ ví check đủ tiền, trừ kho quy đổi ĐVT) | 4h | 07–08/04 | Nguyên |
| 11 | Viết Test Case Sprint 3 (~350 TC mới, trọng tâm POS ~200 TC) | 16h | 03–10/04 | Nhi |
| 12 | Thực thi Test Case Sprint 3 trên giao diện (~350 TC mới) | 6h | 10–11/04 | Nhi, Tấn hỗ trợ |
| 13 | Kiểm thử hồi quy Sprint 1 (50 TC) + Sprint 2 (100 TC) | 4h | 10–11/04 | Nhi |
| 14 | Kiểm thử tích hợp: POS→Kho trừ tồn, POS→KH tích điểm, POS→Ví trừ dư, POS→Vé→Cổng quét, Thuê→Ví cọc/hoàn | 6h | 10–11/04 | Nhi, Nguyên hỗ trợ |
| 15 | Ghi nhận Defect List (≥ 20 lỗi mới + 5 lỗi hồi quy) | 2h | 11/04 | Nhi |
| 16 | Sửa lỗi Sprint 3 (ưu tiên POS trước) | 10h | 11–13/04 | Nguyên, Tấn |
| 17 | Chuẩn hóa SRS tổng hợp Sprint 3 | 4h | 13–14/04 | Nhi |
| 18 | Cập nhật hướng dẫn sử dụng (bổ sung POS, Vé, Đoàn, Thuê) | 2h | 13–14/04 | Nhi |
| 19 | Triển khai gia tăng lên hệ thống thật | 2h | 13–14/04 | Nguyên, Tấn hỗ trợ |
| 20 | Họp Sprint Review — Demo POS bán hàng live cho GVHD + Khách hàng | 2h | 14/04 | Cả nhóm |
| 21 | Họp Sprint Retrospective | 2h | 14/04 | Cả nhóm |
| 22 | Quay video demo luồng POS end-to-end (tùy chọn) | 2h | 14/04 | Tấn, Nhi hỗ trợ |

**Sản phẩm bàn giao:** Sprint BackLog · SRS S3 · Source Code · ~350 TC mới + 150 TC hồi quy · Defect List (≥25 lỗi) · HDSD · Biên bản Review · Video demo POS

---
---

# SPRINT BACKLOG 4 — DỊCH VỤ LƯU TRÚ ĐẠI NAM

**Thời gian:** 15/04/2026 → 28/04/2026 (2 tuần)
**Mục tiêu Sprint:** Triển khai 3 module dịch vụ lưu trú: Khách sạn (sơ đồ phòng, đặt phòng, check-in/out), Nhà hàng (layout bàn, gọi món, thanh toán), và Gửi xe (nhận diện biển số OCR, barcode, tính phí thời gian).

## Các chức năng trong Sprint 4

| STT | Chức năng | Mô tả ngắn | Phụ trách |
|:---:|:----------|:------------|:----------|
| 1 | Khách Sạn & Đặt Phòng | Sơ đồ phòng trực quan (Xanh/Đỏ/Vàng), đặt phòng, giữ phòng, check-in/out, đặt đoàn hàng loạt | Tấn, Nguyên hỗ trợ |
| 2 | Nhà Hàng & Đặt Bàn | Layout bàn realtime, đặt bàn trước, ghép bàn, gọi món trừ kho NVL, thanh toán chia bill | Nguyên, Tấn |
| 3 | Gửi Xe (OCR + Barcode) | Xe vào: camera chụp→OCR biển số→barcode in vé. Xe ra: quét barcode→tính phí × thời gian. Hỗ trợ ví RFID | Nguyên, Tấn |

## Các công việc trong Sprint 4

| STT | Công việc | Thời gian | Ngày | Phụ trách |
|:---:|:----------|:---------:|:-----|:----------|
| 1 | Họp Sprint Planning — review Sprint 3, phân công 3 module song song | 4h | 15/04 | Cả nhóm |
| 2 | Phân tích nghiệp vụ + Viết Acceptance Criteria cho KS, NH, Xe | 6h | 15–16/04 | Nhi |
| 3 | Viết SRS Sprint 4 chi tiết (KS, NH, Xe) | 30h | 17–25/04 | Nhi |
| 4 | Lập trình nghiệp vụ Nhà Hàng (bàn, gọi món, trừ kho, thanh toán) | 10h | 17–21/04 | Nguyên |
| 5 | Lập trình nghiệp vụ Gửi Xe (OCR, barcode, tính phí) | 10h | 17–21/04 | Nguyên |
| 6 | Lập trình nghiệp vụ Khách Sạn (đặt phòng, check-in/out, đoàn) | 12h | 17–21/04 | Tấn, Nguyên hỗ trợ |
| 7 | Thiết kế giao diện Sơ Đồ Phòng KS (Visual Layout) + dialog đặt phòng | 16h | 17–23/04 | Tấn |
| 8 | Thiết kế giao diện Layout Bàn NH + dialog gọi món + thanh toán | 14h | 17–23/04 | Tấn, Nguyên hỗ trợ |
| 9 | Thiết kế giao diện Gửi Xe (camera live + bảng xe đang gửi) | 8h | 20–23/04 | Tấn |
| 10 | Nối giao diện ↔ dữ liệu toàn bộ 3 module | 12h | 23–25/04 | Tấn, Nguyên |
| 11 | Viết Test Case Sprint 4 (~200 TC mới) | 12h | 17–25/04 | Nhi |
| 12 | Thực thi Test Case Sprint 4 trên giao diện (~200 TC mới) | 6h | 25–26/04 | Nhi, Tấn hỗ trợ |
| 13 | Kiểm thử hồi quy Sprint 1 (30 TC) + Sprint 2 (50 TC) + Sprint 3 (80 TC POS) | 4h | 25–26/04 | Nhi |
| 14 | Kiểm thử tích hợp: KS×Ví, KS×Đoàn, NH×Kho trừ NVL, Xe×Ví RFID | 4h | 25–26/04 | Nhi, Nguyên hỗ trợ |
| 15 | Ghi nhận Defect List (≥ 20 lỗi mới + 5 lỗi hồi quy) | 2h | 26/04 | Nhi |
| 16 | Sửa lỗi Sprint 4 | 8h | 26–27/04 | Nguyên, Tấn |
| 17 | Chuẩn hóa SRS tổng hợp Sprint 4 + Cập nhật HDSD | 6h | 27–28/04 | Nhi |
| 18 | Triển khai gia tăng lên hệ thống thật | 2h | 27–28/04 | Nguyên, Tấn hỗ trợ |
| 19 | Họp Sprint Review — Demo Sơ đồ phòng + Layout bàn + Quét biển số live | 2h | 28/04 | Cả nhóm |
| 20 | Họp Sprint Retrospective | 2h | 28/04 | Cả nhóm |

**Sản phẩm bàn giao:** Sprint BackLog · SRS S4 · Source Code · ~200 TC mới + 160 TC hồi quy · Defect List (≥25 lỗi) · HDSD · Biên bản Review

---
---

# SPRINT BACKLOG 5 — HOÀN THIỆN, BÁO CÁO & BÀN GIAO

**Thời gian:** 29/04/2026 → 12/05/2026 (2 tuần)
**Mục tiêu Sprint:** Hoàn thiện các module phụ trợ (Vườn Thú, Khu Biển, Bảo Trì), xây dựng Dashboard báo cáo tổng hợp, tích hợp AI Chatbox điều khiển hệ thống, triển khai Website bán vé online B2C, kiểm thử hồi quy toàn bộ, và bàn giao sản phẩm hoàn chỉnh.

## Các chức năng trong Sprint 5

| STT | Chức năng | Mô tả ngắn | Phụ trách |
|:---:|:----------|:------------|:----------|
| 1 | Vườn Thú & Động Vật | Quản lý 100+ loài động vật, khu vực chuồng, lịch cho ăn, tình trạng sức khỏe | Nhi, Tấn |
| 2 | Khu Biển & Chất Lượng Nước | Giám sát chỉ số nước (pH, Chlor), cảnh báo vượt ngưỡng an toàn | Nguyên |
| 3 | Bảo Trì Thiết Bị | Lịch bảo trì định kỳ, cảnh báo quá hạn, lịch sử sửa chữa | Nguyên |
| 4 | Dashboard Báo Cáo Tổng Hợp | 4 KPI chính (Doanh thu, Khách, Tồn kho, Công suất) + biểu đồ + lọc thời gian | Nguyên, Tấn |
| 5 | AI Chatbox + Website Bán Vé Online (B2C) | AI Chatbox điều khiển form bằng ngôn ngữ tự nhiên. Website Blazor WASM đặt vé → sinh QR | Nguyên, Tấn |

## Các công việc trong Sprint 5

| STT | Công việc | Thời gian | Ngày | Phụ trách |
|:---:|:----------|:---------:|:-----|:----------|
| 1 | Họp Sprint Planning — Sprint cuối, chốt checklist bàn giao | 4h | 29/04 | Cả nhóm |
| 2 | Phân tích nghiệp vụ + Viết Acceptance Criteria cho các chức năng Sprint 5 | 2h | 29/04 | Nhi |
| 3 | Viết SRS Sprint 5 chi tiết (Thú, Biển, Bảo Trì, Dashboard, AI, Web) | 22h | 04–08/05 | Nhi |
| 4 | Lập trình nghiệp vụ Biển + Bảo Trì + Dashboard (tổng hợp DT POS+KS+NH) | 16h | 04–07/05 | Nguyên |
| 5 | Xây dựng API bán vé (Kestrel C#) + tích hợp AI Chatbox | 18h | 04–08/05 | Nguyên |
| 6 | Lập trình nghiệp vụ Vườn Thú + Động Vật | 4h | 04–05/05 | Nhi, Tấn hỗ trợ |
| 7 | Thiết kế giao diện Thú + Biển + Bảo Trì (3 form) | 12h | 04–07/05 | Tấn |
| 8 | Thiết kế giao diện Dashboard (KPI + biểu đồ + filter + drill-down) | 8h | 05–07/05 | Tấn |
| 9 | Thiết kế Website đặt vé (Blazor WASM): trang chủ, giỏ hàng, thanh toán | 10h | 04–07/05 | Tấn |
| 10 | Nối giao diện ↔ dữ liệu toàn bộ + Web ↔ API ↔ CSDL | 8h | 07–08/05 | Tấn, Nguyên |
| 11 | Viết Test Case Sprint 5 (~140 TC mới) | 10h | 04–08/05 | Nhi |
| 12 | Thực thi Test Case Sprint 5 trên giao diện (~140 TC mới) | 4h | 08–09/05 | Nhi, Tấn hỗ trợ |
| 13 | Kiểm thử hồi quy TOÀN BỘ Sprint 1→4: chạy 200 TC ưu tiên cao nhất | 6h | 08–09/05 | Nhi, Nguyên hỗ trợ |
| 14 | Kiểm thử tích hợp: Dashboard khớp POS+KS+NH, Web→Vé→Cổng quét QR | 4h | 09/05 | Nhi |
| 15 | Ghi nhận Defect List (≥ 20 lỗi mới + 5 lỗi hồi quy cuối) | 2h | 09/05 | Nhi |
| 16 | Sửa lỗi cuối cùng toàn hệ thống | 8h | 09–11/05 | Nguyên, Tấn |
| 17 | Tạo dữ liệu mẫu thuyết phục (Seed Data) cho demo | 4h | 09–11/05 | Nhi |
| 18 | Chuẩn hóa SRS tổng hợp cuối cùng (gom Sprint 1→5, ~20 chức năng) | 6h | 11–12/05 | Nhi |
| 19 | Viết hướng dẫn cài đặt + sử dụng phiên bản cuối cùng (phân theo vai trò) | 4h | 11–12/05 | Nhi |
| 20 | Quay video demo toàn bộ hệ thống (≥ 10 phút có thuyết minh) | 4h | 11–12/05 | Tấn, Nhi |
| 21 | Thiết lập DevOps: `run.bat` + `deploy.bat` (1 click khởi động) | 2h | 11/05 | Nguyên |
| 22 | Triển khai hệ thống LAN hoàn chỉnh (Server + 2 Client) trên máy thật | 4h | 11–12/05 | Nguyên, Tấn |
| 23 | Họp Sprint Review cuối cùng — Demo TOÀN BỘ hệ thống cho GVHD + Khách hàng | 2h | 12/05 | Cả nhóm |
| 24 | Họp Retrospective cuối — Tổng kết dự án 10 tuần | 2h | 12/05 | Cả nhóm |
| 25 | Khóa phiên bản v1.0, đóng gói nộp sản phẩm | 4h | 12/05 | Nguyên |

**Sản phẩm bàn giao cuối cùng:**
- Sprint BackLog S5
- SRS tổng hợp S1→S5 (20 chức năng, đầy đủ Use Case, Workflow, Prototype, Ràng buộc)
- Source Code hoàn chỉnh (CSDL + 20 chức năng + Web B2C + AI)
- Test Case (~140 TC mới + 200 TC hồi quy S1→S4)
- Defect List tổng hợp (S1→S5, trạng thái đóng/mở)
- Hướng dẫn cài đặt + sử dụng phiên bản cuối cùng
- Video demo toàn bộ hệ thống (≥ 10 phút có thuyết minh)
- File `run.bat` + `deploy.bat`
- Source code đóng gói + DB script (CSDL + Seed Data)
- Biên bản Sprint Review cuối cùng có chữ ký
