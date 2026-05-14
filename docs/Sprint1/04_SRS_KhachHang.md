# Khu Du Lịch Đại Nam
# Đặc Tả Yêu Cầu Phần Mềm
# Mã dự án: DN01
# Mã tài liệu: DN01_SRS_QuanLyKhachHang_v1.0

Hồ Chí Minh, Tháng 04/2026

---

## Lịch sử thay đổi

| Ngày hiệu lực | Hạng mục thay đổi | A/M/D | Mô tả | Phiên bản |
|---|---|---|---|---|
| 22/04/2026 | Phát hành lần đầu | A | | 1.0 |

*A - Thêm mới, M - Chỉnh sửa, D - Xóa bỏ*

---

## Mục lục

1. [Quản lý khách hàng](#1-quản-lý-khách-hàng)
   - 1.1. [Màn hình danh sách khách hàng](#11-màn-hình-danh-sách-khách-hàng)
   - 1.2. [Màn hình chi tiết khách hàng](#12-màn-hình-chi-tiết-khách-hàng)
   - 1.3. [Form thêm mới và chỉnh sửa khách hàng](#13-form-thêm-mới-và-chỉnh-sửa-khách-hàng)
   - 1.4. [Nạp tiền ví điện tử](#14-nạp-tiền-ví-điện-tử)
   - 1.5. [Cấp ví và thẻ RFID](#15-cấp-ví-và-thẻ-rfid)
   - 1.6. [Khóa và mở khóa thẻ RFID](#16-khóa-và-mở-khóa-thẻ-rfid)
   - 1.7. [Điều chỉnh điểm tích lũy](#17-điều-chỉnh-điểm-tích-lũy)
   - 1.8. [Xóa khách hàng](#18-xóa-khách-hàng)
2. [Yêu cầu khác](#2-yêu-cầu-khác)
   - 2.1. [Định dạng dữ liệu](#21-định-dạng-dữ-liệu)
   - 2.2. [Danh mục dữ liệu tham chiếu](#22-danh-mục-dữ-liệu-tham-chiếu)
   - 2.3. [Bảng mã thông báo lỗi](#23-bảng-mã-thông-báo-lỗi)

---

## 0. Phạm vi tài liệu

Tài liệu này đặc tả phân hệ Quản lý Khách hàng thuộc hệ thống quản lý vận hành Khu Du lịch Đại Nam.
- **Phạm vi bao gồm:** tra cứu, thêm mới, chỉnh sửa khách hàng; nạp tiền ví; cấp và quản lý thẻ RFID; điều chỉnh điểm tích lũy; xóa ẩn khách hàng.
- **Không bao gồm:** thanh toán bằng ví RFID tại quầy (đặc tả tại SRS Bán hàng POS), báo cáo doanh thu theo khách hàng (đặc tả tại SRS Báo cáo).
- **Đối tượng đọc:** BA, Lập trình viên (Dev), Chuyên viên kiểm thử (Tester).

---

# 1. Quản lý khách hàng

Phân hệ quản lý khách hàng bao gồm các chức năng sau:

- Tra cứu, tìm kiếm khách hàng theo số điện thoại, mã thẻ RFID hoặc tên
- Thêm mới khách hàng
- Chỉnh sửa thông tin khách hàng
- Xem chi tiết hồ sơ khách hàng (số dư ví, điểm tích lũy, trạng thái thẻ, tổng chi tiêu)
- Nạp tiền ví điện tử
- Cấp ví điện tử và thẻ RFID
- Khóa hoặc mở khóa thẻ RFID
- Điều chỉnh điểm tích lũy thủ công
- Xem lịch sử giao dịch ví và lịch sử biến động điểm
- Xóa ẩn khách hàng

---

## 1.1. Màn hình danh sách khách hàng

### 1.1.1. Tổng quan

Màn hình này hiển thị toàn bộ danh sách khách hàng dưới dạng lưới bên trái. Bên phải là panel chi tiết hồ sơ khách hàng (Split View). Khi người dùng chọn một dòng trên lưới, panel phải tự nạp thông tin chi tiết của khách hàng đó bao gồm số dư ví, điểm tích lũy, trạng thái thẻ RFID và tổng chi tiêu.

### 1.1.2. Tác nhân

- Quản lý (thêm, sửa, xóa khách hàng, điều chỉnh điểm)
- Thu ngân (xem thông tin, nạp tiền ví, cấp thẻ)

### 1.1.3. Biểu đồ use-case

```
Quản lý ──── Tìm kiếm khách hàng
         ├── Thêm mới khách hàng
         ├── Chỉnh sửa khách hàng
         ├── Nạp tiền ví điện tử          <<extend>> Xem chi tiết
         ├── Cấp ví và thẻ RFID           <<extend>> Xem chi tiết
         ├── Khóa / Mở khóa thẻ RFID     <<extend>> Xem chi tiết
         ├── Điều chỉnh điểm tích lũy     <<extend>> Xem chi tiết
         └── Xóa ẩn khách hàng
```

#### 1.1.3.1. Tiền điều kiện

- Người dùng đã đăng nhập.
- Người dùng có quyền truy cập menu Khách hàng.

#### 1.1.3.2. Hậu điều kiện

Dữ liệu khách hàng được lưu đồng bộ vào cơ sở dữ liệu (bảng ThongTin và KhachHang).

#### 1.1.3.3. Điểm kích hoạt

Người dùng truy cập menu Quản lý, chọn mục Khách hàng.

### 1.1.4. Luồng thao tác

#### 1.1.4.1. Tình huống 1 — Tìm kiếm khách hàng

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhập từ khóa vào ô tìm kiếm (số điện thoại, mã thẻ, tên khách hàng). | Tự lọc lưới theo thời gian thực. Lưới chỉ hiển thị các khách hàng khớp điều kiện. |
| 2 | Nhấn nút Làm mới. | Xóa ô tìm kiếm, tải lại toàn bộ danh sách. |

#### 1.1.4.2. Tình huống 2 — Xem chi tiết khách hàng

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhấp chọn một dòng trên lưới danh sách. | Panel phải hiển thị đầy đủ hồ sơ khách hàng: tên, loại khách, mã KH, SĐT, email, CCCD, địa chỉ, ngày đăng ký, hạng thành viên. |
| 2 | — | Nạp bốn thẻ chỉ số: Số dư khả dụng, Điểm tích lũy, Trạng thái thẻ RFID, Tổng chi tiêu. |
| 3 | — | Hiển thị các nút thao tác tùy trạng thái: Nạp tiền ví (nếu đã có ví), Khóa thẻ hoặc Mở khóa thẻ (nếu có thẻ), Cấp ví và thẻ (nếu chưa có ví), Chỉnh sửa điểm. |
| 4 | — | Nạp hai tab lịch sử: Lịch sử giao dịch ví và Lịch sử điểm. |

### 1.1.5. Giao diện

#### 1.1.5.1. Mô tả màn hình — Thanh công cụ

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Thêm mới | Button | N/A | N/A | N/A | Mở form thêm mới khách hàng ở panel phải. |
| 2 | Làm mới | Button | N/A | N/A | N/A | Xóa ô tìm kiếm, tải lại toàn bộ danh sách. |
| 3 | Tìm kiếm | Text Edit | No | Text | Blank | Lọc trực tiếp trên lưới theo SĐT, mã thẻ hoặc tên KH. (*) Placeholder: "Nhập SĐT, Mã thẻ, Tên KH..." |

#### 1.1.5.2. Mô tả màn hình — Lưới khách hàng (Grid Control, Read-only)

| STT | Tên cột | Control type | Data type | Mô tả |
|---|---|---|---|---|
| 1 | Mã KH | Label | Text | Mã định danh khách hàng, sinh tự động theo format KH00001. |
| 2 | Họ Tên | Label | Text | Tên hiển thị của khách hàng. |
| 3 | SĐT | Label | Text | Số điện thoại. |
| 4 | Loại khách | Label | Text | Cá nhân, Đoàn khách, Doanh nghiệp, HSSV, hoặc Nội bộ. Giá trị hiển thị được dịch đa ngôn ngữ. |

#### 1.1.5.3. Row style — Lưới khách hàng

| Điều kiện | Hiệu ứng |
|---|---|
| Hạng thành viên là Kim Cương | Nền vàng nhạt, tên in đậm. |

#### 1.1.5.4. Mô tả màn hình — Thanh trạng thái

| STT | Tên trường | Control type | Data type | Mô tả |
|---|---|---|---|---|
| 1 | Tổng KH | Label | Text | Hiển thị tổng số khách hàng trên lưới. Ví dụ: 150 Khách hàng |

### 1.1.6. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Split View | Lưới bên trái, panel chi tiết bên phải. Panel chi tiết ẩn cho đến khi người dùng chọn 1 dòng hoặc nhấn nút Thêm mới. |
| 2 | Tìm kiếm thời gian thực | Gõ ký tự vào ô tìm kiếm, hệ thống tự gọi lại dữ liệu từ máy chủ theo từ khóa. Tìm theo SĐT, mã KH, CCCD hoặc tên. |
| 3 | Đa ngôn ngữ | Khi ngôn ngữ thay đổi, hệ thống tải lại toàn bộ lưới, nạp lại combo box, label, nút bấm, và panel chi tiết. |
| 4 | Dirty tracking | Khi chuyển dòng mà form chỉnh sửa đang có dữ liệu chưa lưu, hệ thống hiển thị hộp thoại xác nhận hủy. Nếu người dùng từ chối, lưới giữ nguyên dòng cũ. |
| 5 | Sắp xếp | Danh sách mặc định sắp xếp theo ngày tạo giảm dần, khách mới nhất hiện trên cùng. |

### 1.1.7. Liên kết use-case

- Màn hình chi tiết khách hàng (1.2)
- Form thêm mới và chỉnh sửa khách hàng (1.3)
- Xóa khách hàng (1.8)

---

## 1.2. Màn hình chi tiết khách hàng

### 1.2.1. Tổng quan

Panel này được nạp vào nửa phải của Split View khi người dùng chọn một khách hàng trên lưới. Gồm ba vùng chính: vùng header (tên khách, badge loại khách, thông tin cơ bản), vùng chỉ số (bốn thẻ metric), và vùng tab lịch sử (hai tab: Lịch sử giao dịch ví và Lịch sử điểm).

### 1.2.2. Giao diện

#### 1.2.2.1. Mô tả màn hình — Vùng header

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Tên khách hàng | Label (Read-only) | — | Text | — | Họ tên đầy đủ, font lớn, in đậm. |
| 2 | Badge loại khách | Label (Read-only) | — | Text | — | Hiển thị loại khách (Cá nhân, Đoàn khách...) trên nền xanh Teal. Vị trí nằm cạnh phải tên. |
| 3 | Mã KH | Label (Read-only) | — | Text | — | Mã KH: KH00001 |
| 4 | SĐT | Label (Read-only) | — | Text | — | SĐT: 0901234567 |
| 5 | Email | Label (Read-only) | — | Text | — | Nếu trống hiển thị dấu gạch ngang. |
| 6 | CCCD | Label (Read-only) | — | Text | — | Nếu trống hiển thị dấu gạch ngang. |
| 7 | Địa chỉ | Label (Read-only) | — | Text | — | Nếu trống hiển thị dấu gạch ngang. |
| 8 | Ngày ĐK | Label (Read-only) | — | Date | — | Định dạng dd/MM/yyyy. |
| 9 | Hạng TV | Label (Read-only) | — | Text | — | Hạng thành viên, dịch đa ngôn ngữ. |

#### 1.2.2.2. Mô tả màn hình — Vùng chỉ số (Metrics)

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Số dư khả dụng | Label (Read-only) | — | Decimal(18,0) | 0đ | Tổng nạp trừ tổng chi từ sổ cái ví. Màu Teal. Định dạng N0 kèm đ. |
| 2 | Điểm tích lũy | Label (Read-only) | — | Integer | 0 pts | Số dư sau giao dịch điểm gần nhất. Màu xanh lá. |
| 3 | Thẻ RFID | Label (Read-only) | — | Text | N/A | Trạng thái thẻ kèm mã thẻ. Xanh lá nếu Đang dùng, đỏ nếu Đã khóa, xám nếu trạng thái khác. |
| 4 | Tổng chi tiêu | Label (Read-only) | — | Decimal(18,0) | 0đ | Tổng giá trị đơn hàng (trừ đơn đã hủy). Màu navy. |

#### 1.2.2.3. Mô tả màn hình — Thanh hành động

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Nạp tiền ví | Button | N/A | N/A | N/A | Chỉ hiện khi khách đã có ví điện tử. Mở hộp thoại nạp tiền. |
| 2 | Khóa thẻ / Mở khóa thẻ | Button | N/A | N/A | N/A | Chỉ hiện khi khách có thẻ RFID. Nếu thẻ Đang dùng thì hiện Khóa thẻ (nút đỏ). Nếu thẻ Đã khóa thì hiện Mở khóa thẻ (nút xanh). |
| 3 | Cấp ví và thẻ | Button | N/A | N/A | N/A | Chỉ hiện khi khách chưa có ví điện tử. |
| 4 | Chỉnh sửa điểm | Button | N/A | N/A | N/A | Luôn hiện. Mở hộp thoại nhập điểm cộng hoặc trừ. |
| 5 | Sửa | Button | N/A | N/A | N/A | Mở form chỉnh sửa, nạp dữ liệu hiện tại vào các trường. |
| 6 | Xóa | Button | N/A | N/A | N/A | Xóa ẩn khách hàng sau khi xác nhận. |

#### 1.2.2.4. Mô tả màn hình — Tab Lịch sử giao dịch ví (Grid Control, Read-only)

| STT | Tên cột | Control type | Data type | Mô tả |
|---|---|---|---|---|
| 1 | Mã GD | Label | Text | Mã giao dịch. |
| 2 | Nhóm GD | Label | Text | Nhóm giao dịch. |
| 3 | Loại GD | Label | Text | Nạp, Trừ, Thu, Chi, hoặc Cộng. Dịch đa ngôn ngữ. |
| 4 | Số Tiền | Label | Decimal | Hiển thị dấu cộng (+) và màu xanh lá cho giao dịch Nạp hoặc Cộng. Dấu trừ (-) và màu đỏ cho giao dịch Trừ hoặc Chi. Định dạng N0. |
| 5 | Thời Gian | Label | DateTime | Định dạng dd/MM/yyyy HH:mm. |

#### 1.2.2.5. Mô tả màn hình — Tab Lịch sử điểm (Grid Control, Read-only)

| STT | Tên cột | Control type | Data type | Mô tả |
|---|---|---|---|---|
| 1 | Loại GD | Label | Text | Cộng Điểm hoặc Trừ Điểm. Dịch đa ngôn ngữ. |
| 2 | Số Điểm | Label | Integer | Hiển thị dấu cộng (+) và màu xanh lá khi Cộng Điểm. Dấu trừ (-) và màu đỏ khi Trừ Điểm. Định dạng N0. |
| 3 | Số Dư Sau GD | Label | Integer | Số dư điểm sau giao dịch. Định dạng N0. |
| 4 | Mô Tả | Label | Text | Nội dung mô tả giao dịch điểm. |
| 5 | Thời Gian | Label | DateTime | Định dạng dd/MM/yyyy HH:mm. |

### 1.2.3. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Số dư ví | Số dư khả dụng bằng tổng các bút toán Nạp (Cộng) trừ đi tổng các bút toán Trừ trong sổ cái ví của khách hàng. |
| 2 | Điểm tích lũy | Lấy giá trị số dư sau giao dịch của bản ghi lịch sử điểm mới nhất. |
| 3 | Nút thao tác động | Các nút Nạp tiền ví, Khóa thẻ, Cấp ví và thẻ chỉ xuất hiện tùy trạng thái dữ liệu. Nếu khách chưa có ví thì chỉ hiện nút Cấp ví và thẻ, không hiện Nạp tiền ví. |
| 4 | Nút Khóa thẻ đa trạng thái | Cùng một vị trí nút nhưng đổi nhãn và màu tùy trạng thái thẻ hiện tại: Đang dùng thì hiện Khóa thẻ (đỏ), Đã khóa thì hiện Mở khóa thẻ (xanh). |
| 5 | Đa ngôn ngữ | Toàn bộ label, caption cột, giá trị loại giao dịch, loại khách, hạng thành viên đều được dịch qua hệ thống đa ngôn ngữ. |

### 1.2.4. Liên kết use-case

- Màn hình danh sách khách hàng (1.1)
- Nạp tiền ví điện tử (1.4)
- Cấp ví và thẻ RFID (1.5)
- Khóa và mở khóa thẻ RFID (1.6)
- Điều chỉnh điểm tích lũy (1.7)

---

## 1.3. Form thêm mới và chỉnh sửa khách hàng

### 1.3.1. Tổng quan

Form này hiển thị dạng panel inline ở phía dưới vùng header, thay thế tạm thời vùng chỉ số và tab lịch sử. Dùng chung cho cả hai chế độ thêm mới và chỉnh sửa.

### 1.3.2. Giao diện

#### 1.3.2.1. Mô tả màn hình — Form nhập liệu

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Họ tên | Text Edit | Yes | Nvarchar(200) | Blank | Họ và tên đầy đủ khách hàng. |
| 2 | Điện thoại | Text Edit (Masked) | Yes | Varchar(11) | Blank | Mask input chỉ cho phép nhập số, tối đa 11 ký tự. |
| 3 | Email | Text Edit | No | Nvarchar(200) | Blank | Địa chỉ email. |
| 4 | CCCD | Text Edit (Masked) | No | Varchar(12) | Blank | Mask input chỉ cho phép nhập số, tối đa 12 ký tự. |
| 5 | Địa chỉ | Text Edit | No | Nvarchar(300) | Blank | Địa chỉ liên lạc. |
| 6 | Loại khách | Combo Box | Yes | Varchar(20) | Cá nhân | Cá nhân, Đoàn khách, Doanh nghiệp, HSSV, hoặc Nội bộ. Giá trị hiển thị dịch đa ngôn ngữ. |
| 7 | Hạng thành viên | Combo Box | Yes | Varchar(20) | Thường | Thường, Bạc, Vàng, hoặc Kim Cương. Giá trị hiển thị dịch đa ngôn ngữ. |
| 8 | Lưu | Button | N/A | N/A | N/A | Lưu dữ liệu. Hotkey: F2. |
| 9 | Hủy | Button | N/A | N/A | N/A | Đóng form, quay về chế độ xem. Hotkey: Esc. Nếu có dữ liệu chưa lưu thì hiện hộp thoại xác nhận. |

### 1.3.3. Luồng thao tác

#### 1.3.3.1. Tình huống 1 — Thêm mới khách hàng

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Thêm mới trên thanh công cụ. | Ẩn panel chi tiết, hiển thị form nhập liệu trống. Tiêu đề đổi thành Thêm khách hàng mới. |
| 2 | Nhập họ tên, điện thoại, chọn loại khách. Nhập thêm email, CCCD, địa chỉ nếu có. | Ghi nhận. |
| 3 | Nhấn nút Lưu hoặc phím F2. | Kiểm tra nghiệp vụ. Nếu hợp lệ: sinh mã KH tự động, lưu vào cơ sở dữ liệu, thông báo thành công, đóng form, tải lại lưới, mở chi tiết khách vừa tạo. |

#### 1.3.3.2. Tình huống 2 — Chỉnh sửa khách hàng

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Chọn khách hàng trên lưới, nhấn nút Sửa. | Hiển thị form nhập liệu, nạp dữ liệu hiện tại vào các trường. |
| 2 | Sửa các trường cần thay đổi. | Kích hoạt cờ đã thay đổi. |
| 3 | Nhấn nút Lưu hoặc phím F2. | Kiểm tra nghiệp vụ. Nếu hợp lệ: cập nhật cơ sở dữ liệu, thông báo thành công, đóng form, tải lại lưới, mở lại chi tiết. |

#### 1.3.3.3. Tình huống 3 — Hủy thao tác khi có dữ liệu chưa lưu

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Hủy hoặc phím Esc khi đang có dữ liệu chưa lưu. | Hiển thị hộp thoại xác nhận hủy. |
| 2a | Xác nhận hủy. | Đóng form, quay về chế độ xem chi tiết (nếu đang sửa) hoặc ẩn panel (nếu đang thêm mới). |
| 2b | Từ chối hủy. | Giữ nguyên form, người dùng tiếp tục nhập liệu. |

### 1.3.4. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Sinh mã tự động | Khi thêm mới, mã khách hàng được hệ thống tự sinh theo format KH00001, tăng dần. Người dùng không cần nhập mã. |
| 2 | Kiểm tra trùng SĐT | Số điện thoại phải là duy nhất trong toàn hệ thống (trừ bản ghi đang sửa). Nếu trùng, hệ thống từ chối và thông báo. |
| 3 | Kiểm tra trùng CCCD | Nếu có nhập CCCD, hệ thống kiểm tra trùng (trừ bản ghi đang sửa). Nếu trùng, hệ thống từ chối và thông báo. |
| 4 | Dirty tracking | Mọi thao tác sửa đổi trên bất kỳ trường nào đều kích hoạt cờ đã thay đổi. Cờ này quyết định việc hiển thị hộp thoại xác nhận khi hủy hoặc chuyển dòng. |
| 5 | Hotkey | F2 bằng Lưu. Esc bằng Hủy. |
| 6 | Input Mask | Trường Điện thoại chỉ cho phép nhập số, tối đa 11 ký tự. Trường CCCD chỉ cho phép nhập số, tối đa 12 ký tự. |

### 1.3.5. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Họ tên không được để trống | ERR_KH_TEN_RONG |
| 2 | Số điện thoại không được để trống | ERR_KH_SDT_RONG |
| 3 | Số điện thoại trùng với khách hàng khác | ERR_KH_SDT_TRUNG |
| 4 | CCCD trùng với khách hàng khác | ERR_KH_CCCD_TRUNG |

### 1.3.6. Liên kết use-case

- Màn hình danh sách khách hàng (1.1)

---

## 1.4. Nạp tiền ví điện tử

### 1.4.1. Tổng quan

Hộp thoại cho phép nhân viên nạp tiền vào ví điện tử của khách hàng. Hỗ trợ nhiều phương thức thanh toán.

### 1.4.2. Tác nhân

Thu ngân, Quản lý.

### 1.4.3. Luồng thao tác

#### 1.4.3.1. Tình huống 1 — Nạp tiền thành công

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Nạp tiền ví trên màn hình chi tiết. | Mở hộp thoại Nạp tiền ví điện tử. |
| 2 | Nhập số tiền, chọn phương thức thanh toán. | Ghi nhận. |
| 3 | Nhấn nút Xác nhận nạp tiền. | Kiểm tra số tiền lớn hơn 0. Nếu hợp lệ: tạo bút toán nạp trong sổ cái ví, lập chứng từ tài chính, thông báo thành công, tải lại chi tiết khách hàng. |

#### 1.4.3.2. Tình huống 2 — Lỗi hệ thống khi ghi dữ liệu

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Xác nhận nạp tiền. | Thực hiện ghi dữ liệu. Nếu xảy ra lỗi kết nối hoặc lỗi cơ sở dữ liệu: giữ nguyên hộp thoại, hiển thị ERR_SYSTEM_FAIL, không ghi bất kỳ bút toán nào. |

### 1.4.4. Giao diện

#### 1.4.4.1. Mô tả màn hình — Hộp thoại nạp tiền

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Số tiền nạp | Spin Edit | Yes | Decimal(18,0) | 0 | Số tiền cần nạp. Phải lớn hơn 0. |
| 2 | Hình thức thanh toán | Combo Box | Yes | Varchar(20) | Tiền mặt | Tiền mặt, Chuyển khoản, QR Code, Ví MoMo, hoặc Thẻ ngân hàng. |
| 3 | Xác nhận nạp tiền | Button | N/A | N/A | N/A | Thực hiện nạp tiền. |
| 4 | Hủy | Button | N/A | N/A | N/A | Đóng hộp thoại, không nạp. |

### 1.4.5. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Số tiền nạp phải lớn hơn 0 | ERR_VI_SO_TIEN_AM |

### 1.4.6. Liên kết use-case

- Màn hình chi tiết khách hàng (1.2)

---

## 1.5. Cấp ví và thẻ RFID

### 1.5.1. Tổng quan

Chức năng tạo ví điện tử và gán thẻ RFID cho khách hàng chưa có ví. Nhân viên quét hoặc nhập mã thẻ RFID vật lý, hệ thống tự tạo ví và liên kết.

### 1.5.2. Luồng thao tác

#### 1.5.2.1. Tình huống 1 — Cấp ví thành công

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Cấp ví và thẻ trên màn hình chi tiết. | Mở hộp thoại nhập mã thẻ RFID. |
| 2 | Nhập mã thẻ RFID, nhấn nút xác nhận. | Kiểm tra mã thẻ hợp lệ và chưa bị gán cho ai, đồng thời kiểm tra khách chưa có ví. Nếu hợp lệ: tạo ví điện tử, gán thẻ RFID, thông báo thành công, tải lại chi tiết. |

#### 1.5.2.2. Tình huống 2 — Mã thẻ đã được gán cho khách khác

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhập mã thẻ RFID, nhấn nút xác nhận. | Phát hiện mã thẻ đã tồn tại trong hệ thống. Hiển thị ERR_RFID_MA_TRUNG, giữ nguyên hộp thoại để nhân viên nhập mã thẻ khác. |

#### 1.5.2.3. Tình huống 3 — Khách đã có ví từ trước

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Cấp ví và thẻ. | Phát hiện khách đã có ví điện tử. Hiển thị ERR_VI_DA_CO. Nút Cấp ví và thẻ tự ẩn sau khi tải lại chi tiết. |

### 1.5.3. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Một khách một ví | Mỗi khách hàng chỉ có tối đa một ví điện tử. Nếu đã có ví, nút Cấp ví và thẻ tự ẩn. |
| 2 | Thẻ duy nhất | Mã thẻ RFID phải là duy nhất trong toàn hệ thống. Nếu trùng, hệ thống từ chối. |

### 1.5.4. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Mã thẻ RFID không được để trống | ERR_RFID_MA_RONG |
| 2 | Mã thẻ RFID đã được gán cho khách khác | ERR_RFID_MA_TRUNG |
| 3 | Khách hàng đã có ví điện tử | ERR_VI_DA_CO |

### 1.5.5. Liên kết use-case

- Màn hình chi tiết khách hàng (1.2)

---

## 1.6. Khóa và mở khóa thẻ RFID

### 1.6.1. Tổng quan

Cho phép nhân viên khóa hoặc mở khóa thẻ RFID của khách hàng. Thẻ bị khóa sẽ không thể dùng để thanh toán tại POS.

### 1.6.2. Luồng thao tác

#### 1.6.2.1. Tình huống 1 — Khóa thẻ

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Khóa thẻ (khi thẻ đang ở trạng thái Đang dùng). | Chuyển trạng thái thẻ sang Đã khóa. Thông báo thành công. Tải lại chi tiết. Nút đổi thành Mở khóa thẻ. |

#### 1.6.2.2. Tình huống 2 — Mở khóa thẻ

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Mở khóa thẻ (khi thẻ đang ở trạng thái Đã khóa). | Chuyển trạng thái thẻ sang Đang dùng. Thông báo thành công. Tải lại chi tiết. Nút đổi thành Khóa thẻ. |

#### 1.6.2.3. Tình huống 3 — Thẻ đã ở đúng trạng thái mục tiêu

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhấn nút (ví dụ: nhấn Khóa thẻ khi thẻ đã ở trạng thái Đã khóa). | Hệ thống phát hiện thẻ đã ở trạng thái mục tiêu. Hiển thị ERR_RFID_TRANG_THAI_TRUNG, không thực hiện thay đổi. |

### 1.6.3. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Không tìm thấy thẻ RFID hợp lệ của khách hàng này | ERR_RFID_KHONG_TIM_THAY |
| 2 | Thẻ đã ở trạng thái mục tiêu, không cần thực hiện lại | ERR_RFID_TRANG_THAI_TRUNG |

### 1.6.4. Liên kết use-case

- Màn hình chi tiết khách hàng (1.2)

---

## 1.7. Điều chỉnh điểm tích lũy

### 1.7.1. Tổng quan

Hộp thoại cho phép nhân viên cộng hoặc trừ điểm tích lũy thủ công cho khách hàng. Thao tác được ghi nhật ký kèm tên nhân viên thực hiện.

### 1.7.2. Luồng thao tác

#### 1.7.2.1. Tình huống 1 — Cộng điểm

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Chỉnh sửa điểm. | Mở hộp thoại nhập điểm. |
| 2 | Nhập số điểm dương (ví dụ: 100), nhập lý do. Nhấn nút xác nhận. | Cộng điểm vào tài khoản khách, ghi lịch sử điểm kèm tên nhân viên. Thông báo thành công, tải lại chi tiết. |

#### 1.7.2.2. Tình huống 2 — Trừ điểm

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhập số điểm âm (ví dụ: -50), nhập lý do. Nhấn nút xác nhận. | Trừ điểm khỏi tài khoản khách. Ghi lịch sử. Thông báo thành công. |

### 1.7.3. Giao diện

#### 1.7.3.1. Mô tả màn hình — Hộp thoại điều chỉnh điểm

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Số điểm | Spin Edit | Yes | Integer | 100 | Cho phép nhập từ -1,000,000 đến 1,000,000. Dương là cộng, âm là trừ. |
| 2 | Lý do | Text Edit | Yes | Text | Điều chỉnh cộng/trừ thao tác tay | Bắt buộc nhập. Nội dung lưu kèm tên nhân viên vào lịch sử. |
| 3 | OK | Button | N/A | N/A | N/A | Xác nhận điều chỉnh. |
| 4 | Cancel | Button | N/A | N/A | N/A | Hủy, đóng hộp thoại. |

### 1.7.4. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Số điểm không được bằng 0 | ERR_DIEM_BANG_KHONG |
| 2 | Lý do không được để trống | ERR_DIEM_LY_DO_RONG |

### 1.7.5. Liên kết use-case

- Màn hình chi tiết khách hàng (1.2)

---

## 1.8. Xóa khách hàng

### 1.8.1. Tổng quan

Chức năng xóa ẩn khách hàng (soft delete). Hệ thống không xóa vật lý mà chỉ đánh dấu đã xóa.

### 1.8.2. Tác nhân

Quản lý.

### 1.8.3. Luồng thao tác

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Xóa trên màn hình chi tiết. | Hiển thị hộp thoại xác nhận xóa kèm tên khách hàng. |
| 2 | Nhấn xác nhận. | Đánh dấu đã xóa, thông báo thành công, tải lại lưới, ẩn panel chi tiết. |

### 1.8.4. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Xóa ẩn | Hệ thống chỉ đánh dấu đã xóa, không xóa vật lý. Dữ liệu lịch sử giao dịch, điểm, ví vẫn được bảo toàn. Khách hàng đã xóa không còn hiển thị trên mọi giao diện. |

### 1.8.5. Liên kết use-case

- Màn hình danh sách khách hàng (1.1)

---

# 2. Yêu cầu khác

## 2.1. Định dạng dữ liệu

### 2.1.1. Ngày giờ

- Định dạng ngày: dd/MM/yyyy. Ví dụ: 22/04/2026
- Định dạng ngày giờ: dd/MM/yyyy HH:mm. Ví dụ: 22/04/2026 14:30

### 2.1.2. Số

- Số tiền: dấu phẩy phân cách hàng nghìn, không có chữ số thập phân, kèm đ. Ví dụ: 150,000đ
- Điểm tích lũy: định dạng N0 kèm pts. Ví dụ: 1,200 pts

## 2.2. Danh mục dữ liệu tham chiếu

### 2.2.1. Loại khách hàng

| Mã | Tên hiển thị |
|---|---|
| CaNhan | Cá nhân |
| Doan | Đoàn khách |
| DoanhNghiep | Doanh nghiệp |
| HocSinhSinhVien | HSSV |
| NoiBo | Nội bộ |

### 2.2.2. Hạng thành viên

| Mã | Tên hiển thị |
|---|---|
| Thuong | Thường |
| Bac | Bạc |
| Vang | Vàng |
| KimCuong | Kim Cương |

### 2.2.3. Trạng thái thẻ RFID

| Mã | Tên hiển thị |
|---|---|
| DangDung | Đang dùng |
| DaKhoa | Đã khóa |
| ChuaKichHoat | Chưa kích hoạt |
| DaTra | Đã trả |

### 2.2.4. Loại giao dịch ví

| Mã | Tên hiển thị |
|---|---|
| Nap | Nạp |
| Tru | Trừ |
| Thu | Thu |
| Chi | Chi |
| Cong | Cộng |

### 2.2.5. Loại giao dịch điểm

| Mã | Tên hiển thị |
|---|---|
| CongDiem | Cộng Điểm |
| TruDiem | Trừ Điểm |

### 2.2.6. Phương thức thanh toán (nạp ví)

| Mã | Tên hiển thị |
|---|---|
| TienMat | Tiền mặt |
| ChuyenKhoan | Chuyển khoản |
| QR | QR Code |
| MoMo | Ví MoMo |
| TheNganHang | Thẻ ngân hàng |

## 2.3. Bảng mã thông báo lỗi

| Mã thông báo | Nội dung tiếng Việt |
|---|---|
| ERR_KH_TEN_RONG | Họ tên không được để trống |
| ERR_KH_SDT_RONG | Số điện thoại không được để trống |
| ERR_KH_SDT_TRUNG | Số điện thoại đã tồn tại trong hệ thống |
| ERR_KH_CCCD_TRUNG | Số CCCD đã tồn tại trong hệ thống |
| ERR_VI_SO_TIEN_AM | Số tiền phải lớn hơn 0 |
| ERR_VI_CHUA_CO | Khách hàng chưa có ví điện tử |
| ERR_VI_DA_CO | Khách hàng đã có ví điện tử, không thể cấp thêm |
| ERR_RFID_MA_RONG | Vui lòng nhập mã thẻ RFID |
| ERR_RFID_MA_TRUNG | Mã thẻ RFID đã được gán cho khách hàng khác |
| ERR_RFID_KHONG_TIM_THAY | Không tìm thấy thẻ RFID hợp lệ cho khách hàng này |
| ERR_RFID_TRANG_THAI_TRUNG | Thẻ đã ở trạng thái này, không cần thực hiện lại |
| ERR_DIEM_BANG_KHONG | Số điểm điều chỉnh không được bằng 0 |
| ERR_DIEM_LY_DO_RONG | Lý do điều chỉnh điểm không được để trống |
| ERR_SYSTEM_FAIL | Lỗi hệ thống, vui lòng thử lại |
| MSG_KH_LUU_OK | Lưu thông tin khách hàng thành công |
| MSG_KH_XOA_OK | Đã xóa khách hàng |
| MSG_NAP_TIEN_OK | Nạp tiền ví thành công |
| MSG_RFID_KICH_HOAT_OK | Cấp ví và thẻ RFID thành công |
| MSG_RFID_KHOA_OK | Đã khóa thẻ RFID |
| MSG_RFID_MO_KHOA_OK | Đã mở khóa thẻ RFID |
