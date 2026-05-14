# Khu Du Lịch Đại Nam — Bảng Tiêu Chí Chất Lượng Toàn Dự Án
# Mã tài liệu: DN01_QualityCriteria_v2.0
# ⚠️ Viết MỘT LẦN DUY NHẤT — Áp dụng toàn bộ 5 Sprint
# Ngày lập: 04/03/2026 | Người lập: Bùi Trí Nguyên

---

## Cam kết nhóm

> Nhóm cam kết không tính một chức năng là **"Done"** nếu chưa đáp ứng đủ các tiêu chí dưới đây.
> Bảng này **không thay đổi** sau mỗi sprint. Mọi điều chỉnh phải ghi vào mục Lịch sử thay đổi kèm lý do rõ ràng.

---

## Lịch sử thay đổi

| Ngày | Nội dung | Người cập nhật |
|---|---|---|
| 04/03/2026 | Phát hành lần đầu | Bùi Trí Nguyên |

---

## Bảng Tiêu Chí Chất Lượng — 36 Tiêu Chí

| Giai đoạn | Công việc | Tiêu chí chất lượng | KPI / Chuẩn đánh giá |
|---|---|---|---|
| **1. Requirement** | Thu thập yêu cầu | Yêu cầu rõ ràng, không mơ hồ. Mỗi User Story có Acceptance Criteria. Có mockup/prototype cho tất cả màn hình | 100% User Story có Acceptance Criteria. Có ảnh prototype cho tất cả chức năng chính |
| | Phân tích nghiệp vụ | SRS đầy đủ: UseCase, Activity Diagram, mô tả ràng buộc, bảng mã lỗi | Core Database Schema ổn định trên 90%. Logic UI/UX và luồng phụ có thể linh hoạt thay đổi theo đánh giá sau mỗi Sprint |
| | Review requirement | Tài liệu SRS được duyệt (sign-off) trước khi code | 100% chức năng có SRS trước khi bắt đầu code sprint |
| **2. Design** | UI/UX Design | Giao diện nhất quán, dễ dùng, ≤ 3 bước cho hành động chính. Có design cho trạng thái loading / lỗi / trống | Sinh viên khác nhóm (chưa biết hệ thống) thao tác được chức năng chính trong lần đầu tiên, không cần giải thích |
| | System Design | Kiến trúc phân tách logic rõ ràng. Mọi câu lệnh truy vấn LINQ phải được đóng gói vào class DAL/BUS. Tuyệt đối không viết LINQ query trực tiếp trong code-behind của Form (UI) | 0 LINQ query hoặc Raw SQL nằm trong file đuôi .Designer.cs hoặc sự kiện Button_Click |
| | Thiết kế Database | Database đạt chuẩn 3NF, dữ liệu nhất quán, có đủ bảng/view cho tất cả chức năng | 100% bảng có khóa chính. Bảng quan hệ có khóa ngoại. Không lưu dữ liệu tính toán được |
| | API Design | REST API chuẩn, có route rõ ràng, trả về JSON nhất quán | Response time API ≤ 1 giây (môi trường lab nội bộ). Endpoint đặt tên theo chuẩn RESTful |
| **3. Development** | Coding chuẩn | Clean code, theo coding convention dự án. Comment chỗ khó. Không magic number, không duplicate | Comment ≥ 90% hàm business logic phức tạp. 0 tên biến mơ hồ |
| | Code review | Peer review trước khi merge vào nhánh chính | 100% code được ít nhất 1 thành viên khác review trước khi merge |
| | Unit test | Bắt buộc có Unit Test tự động (Passed 100%) cho 3 thuật toán dòng tiền cốt lõi: Tính tổng hóa đơn POS, Phân bổ tỷ lệ Combo, Tính tiền thuê đồ theo block giờ. Các module CRUD thông thường dùng Manual Test | 3 Unit Test cốt lõi Passed 100%. Không yêu cầu Coverage tổng thể |
| | **CN01 – Đăng nhập & Bảo mật** | Đăng nhập sai mật khẩu bị chặn sau 5 lần thử. Mật khẩu mã hóa trong DB (không lưu plaintext) | Khóa tài khoản 15 phút sau 5 lần sai. 0 trường hợp lưu mật khẩu dạng plaintext trong DB |
| | **CN02 – Phân quyền** | Phân quyền chi tiết đến từng form. Menu/nút bấm không có quyền sẽ bị ẩn hoặc vô hiệu hóa | Trong các kịch bản test được thiết kế, không có trường hợp truy cập trái phép vào màn hình ngoài phạm vi vai trò |
| | **CN04 – Quản lý Khách hàng** | Tìm kiếm khách theo tên/số điện thoại nhanh. Hồ sơ lưu đầy đủ thông tin. Lịch sử giao dịch hiển thị chính xác | Tìm kiếm khách tức thời dưới 1 giây với 100.000 bản ghi (có Index). Không được xóa khách đang có đơn hàng |
| | **CN05 – POS, Thanh toán & Hoàn tiền** | Tạo đơn hàng, in hóa đơn, thanh toán đa phương thức. Xử lý hoàn hàng và trả tồn kho đúng luồng | Tính tiền chính xác 100%. Thời gian hoàn tất thanh toán < 3s |
| | **CN06 – Quản lý Combo** | Tổng tỷ lệ phân bổ phải bằng 100%. Tự động chuyển về Nháp nếu sai tỷ lệ. Biểu đồ hiển thị màu chuẩn | 0 combo lưu được khi tổng tỷ lệ khác 100% |
| | **CN07 – Khuyến mãi & Voucher** | Voucher chỉ dùng được 1 lần, đúng thời hạn, đúng điều kiện tối thiểu. Tự động tính tiền giảm trừ | 0 voucher hết hạn vẫn áp dụng được. Chiết khấu tính chính xác 100% |
| | **CN08 – Kho hàng** | Nhập kho theo lô, có HSD. Tồn kho cập nhật realtime khi bán F&B theo BOM. Cảnh báo hàng sắp hết | Tồn kho khớp 100% với giao dịch bán ra. Cảnh báo popup hiển thị ngay trên màn hình POS khi thêm sản phẩm gần hết hàng |
| | **CN09 – Sản phẩm, Vé & BOM** | Thêm sửa xóa SP chuẩn. Bảng giá hoạt động theo đối tượng/thời gian. Cấu hình định mức BOM hợp lệ | Tìm kiếm SP ra kết quả < 1 giây. BOM định mức không được lưu rỗng |
| | **CN10 – Dashboard** | Thống kê số liệu doanh thu, khách hàng, sản phẩm realtime. Giao diện trực quan (biểu đồ) | Tải Dashboard dưới 3s. Số liệu hiển thị khớp hoàn toàn với báo cáo chi tiết |
| | **CN11 – Thuê đồ** | Tính tiền thuê đúng theo block giờ. Phiếu thuê ghi đủ giờ bắt đầu/kết thúc. Thu tiền đúng tiến độ | Sai số tính tiền thuê = 0₫. Cảnh báo khách quá giờ thuê |
| | **CN12 – Nhà hàng** | Trạng thái bàn cập nhật realtime. Phiếu gọi món gửi đúng bếp. Đầu bếp xác nhận xong món thì bàn cập nhật | Thời gian đồng bộ trạng thái bàn nhỏ hơn 2 giây. 0 đơn món bị mất/nhầm bàn |
| | **CN13 – Khách sạn** | Đặt phòng không bị trùng. Check-in/Check-out cập nhật trạng thái phòng ngay. Tính tiền đúng số ngày | 0 phòng bị đặt 2 lần cùng thời điểm. Tính tiền đúng theo loại phòng |
| | **CN14 – AI Assistant** | AI trả lời đúng các câu hỏi về doanh thu, sản phẩm, khách hàng bằng ngôn ngữ tự nhiên. Không bịa data | Thời gian phản hồi AI nhỏ hơn 5 giây. 0 câu trả lời sai nghiệp vụ |
| | **CN15 – Báo cáo** | Xuất báo cáo Doanh thu, Xuất nhập tồn ra Excel/PDF đúng format, layout chuyên nghiệp | Dữ liệu báo cáo khớp 100% số liệu bán hàng. Layout không bị vỡ cột khi xuất |
| | **WEB – Portal Khách hàng** | Khách tự đặt vé, phòng, món ăn. Nhận mã QR xác thực. Giao diện Responsive trên thiết bị di động | Tất cả chức năng chạy đúng trong môi trường demo (localhost tunnel). Không đặt KPI tốc độ vì phụ thuộc đường truyền |
| | **Android – Staff App** | Quẹt QR bằng camera xác thực đúng, nhận thông báo đẩy cho bếp/dọn phòng khi có đơn mới | QR quét được và xác thực đúng trong điều kiện demo. Thông báo hiển thị được trên thiết bị |
| **4. Integration** | Tích hợp module | WinForms, Web, Android cùng dùng chung DB và API — không xung đột | < 5% lỗi integration khi chạy đồng thời 3 nền tảng |
| | Build system | Build không lỗi, không warning nghiêm trọng | Build success rate ≥ 95% trên máy thành viên và máy demo |
| **5. Testing (QA)** | Functional test | Tất cả Happy Path và Exception Path trong SRS được test | ≥ 95% test case pass khi demo. 0 lỗi P1 (crash, sai tiền, mất dữ liệu) |
| | Test case đầy đủ | Mỗi chức năng có ≥ 1 TC happy path + ≥ 1 TC exception, ghi đầy đủ bước và kết quả mong đợi | ≥ 50 TC toàn dự án. Thiếu bước hoặc kết quả mong đợi = không tính |
| | Kiểm tra biên | Ô số test với 0, âm, tối đa. Ô text test với rỗng, quá dài, ký tự đặc biệt | 100% ô nhập liệu có test biên được ghi vào file TC |
| | Defect List | 100% bug phát hiện được ghi nhận vào Defect List. Bug nghiêm trọng (crash app, sai tiền) không được lọt ra bản demo | Sprint demo không có lỗi P1. Tối đa 1 lỗi P2 không gây dừng hệ thống. Không ép số lượng |
| | Performance test | Màn hình danh sách tải nhanh. Lưu dữ liệu phản hồi nhanh | Lần đầu mở app ≤ 5 giây. Các màn hình sau (đã cache) ≤ 3 giây. Lưu dữ liệu < 2 giây |
| | Security test | Không lộ dữ liệu nhạy cảm. Phân quyền đúng. Không SQL injection cơ bản | 0 lỗi phân quyền (user xem được dữ liệu không có quyền). 0 SQL injection cơ bản |
| **6. Deployment** | Cài đặt & chạy | Phần mềm cài đặt và chạy được theo hướng dẫn trên máy thầy và máy demo | Chạy qua tất cả chức năng chính không crash khi demo |
| | Dữ liệu mẫu | Hệ thống có sẵn seed data thực tế để demo không cần nhập tay | Có đủ SP, KH, ĐH mẫu để demo tất cả chức năng ngay lập tức |
| | Hướng dẫn sử dụng | Có README.md hoặc PDF mô tả cách cài đặt, chạy chương trình (kèm yêu cầu phần mềm, framework, cổng) | Giảng viên làm theo README có thể chạy được chương trình ngay lần đầu, không cần hỏi thêm |
| | Backup & Restore | Có script backup DB, source code lưu trên Git | Có thể restore về commit ổn định gần nhất trong < 10 phút |
| **7. Monitoring** | Logging | Hệ thống ghi log lỗi và hoạt động quan trọng | Khi có lỗi xảy ra, có thể tìm thấy log trong vòng 5 phút |
| **8. Maintenance** | Sửa lỗi | Bug P1 (crash, sai tiền) phải sửa trong sprint đang chạy, không để sang sprint sau | Critical bug fix < 24 giờ sau khi phát hiện |
| | Cải tiến | Mỗi sprint có ít nhất 1 cải tiến từ phản hồi sprint trước (UI, hiệu năng, UX) | Ghi rõ cải tiến vào Sprint Review và Backlog sprint kế tiếp |
| **9. Quản lý dự án** | Sprint management | Đúng tiến độ, đủ artifact nộp cuối mỗi sprint | ≥ 90% task hoàn thành đúng deadline sprint |
| | Communication | Biên bản họp (file docs) ghi đủ nội dung thảo luận và quyết định | ≥ 1 biên bản họp/sprint, có xác nhận của ≥ 2/3 thành viên qua comment hoặc tick |
| | Risk management | Nhận diện và xử lý rủi ro kịp thời | 100% rủi ro được nhận diện có phương án xử lý ghi vào tài liệu |

---

## Định nghĩa "Done" (Definition of Done)

Một User Story được tính là **DONE** khi và chỉ khi đáp ứng tất cả:

| # | Điều kiện |
|---|---|
| 1 | SRS của chức năng đã viết xong, có prototype/mockup |
| 2 | Code tuân thủ kiến trúc phân tách logic (tuyệt đối không truy vấn DB trực tiếp ở UI), không lỗi build |
| 3 | Tất cả luồng SRS chạy đúng (happy path + exception path) |
| 4 | Validate dữ liệu đầy đủ, thông báo lỗi tiếng Việt |
| 5 | Có file Test Case đầy đủ bước và kết quả mong đợi |
| 6 | Sprint demo không có lỗi P1 (crash, sai tiền, mất dữ liệu). Tối đa 1 lỗi P2 không gây dừng hệ thống |
| 7 | Phân quyền đúng vai trò |

> ⚠️ **Thiếu bất kỳ điều kiện nào = chưa Done.**
