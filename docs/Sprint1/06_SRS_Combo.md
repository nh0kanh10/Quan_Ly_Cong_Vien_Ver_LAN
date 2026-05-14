# Khu Du Lịch Đại Nam
# Đặc Tả Yêu Cầu Phần Mềm
# Mã dự án: DN01
# Mã tài liệu: DN01_SRS_QuanLyCombo_v1.0

Hồ Chí Minh, Tháng 04/2026

---

## Lịch sử thay đổi

| Ngày hiệu lực | Hạng mục thay đổi | A/M/D | Mô tả | Phiên bản |
|---|---|---|---|---|
| 23/04/2026 | Phát hành lần đầu | A | | 1.0 |

*A - Thêm mới, M - Chỉnh sửa, D - Xóa bỏ*

---

## Mục lục

1. [Quản lý combo sản phẩm](#1-quản-lý-combo-sản-phẩm)
   - 1.1. [Màn hình danh sách combo](#11-màn-hình-danh-sách-combo)
   - 1.2. [Thông tin combo](#12-thông-tin-combo)
   - 1.3. [Kho sản phẩm và Rổ combo](#13-kho-sản-phẩm-và-rổ-combo)
   - 1.4. [Thêm mới combo](#14-thêm-mới-combo)
   - 1.5. [Chỉnh sửa combo](#15-chỉnh-sửa-combo)
   - 1.6. [Xóa combo](#16-xóa-combo)
2. [Yêu cầu khác](#2-yêu-cầu-khác)
   - 2.1. [Định dạng dữ liệu](#21-định-dạng-dữ-liệu)
   - 2.2. [Danh mục dữ liệu tham chiếu](#22-danh-mục-dữ-liệu-tham-chiếu)
   - 2.3. [Bảng mã thông báo lỗi](#23-bảng-mã-thông-báo-lỗi)

---

## 0. Phạm vi tài liệu

Tài liệu này đặc tả phân hệ Quản lý Combo sản phẩm thuộc hệ thống quản lý vận hành Khu Du lịch Đại Nam.
- **Phạm vi bao gồm:** tạo và chỉnh sửa combo sản phẩm; thiết lập thành phần rổ combo; phân bổ tỷ lệ doanh thu; xóa ẩn combo.
- **Không bao gồm:** bán combo tại quầy POS (đặc tả tại SRS Bán hàng POS), thống kê doanh thu combo (đặc tả tại SRS Báo cáo), quản lý tồn kho thành phần (đặc tả tại SRS Quản lý Kho).
- **Đối tượng đọc:** BA, Lập trình viên (Dev), Chuyên viên kiểm thử (Tester).

---

# 1. Quản lý combo sản phẩm

Phân hệ quản lý combo cho phép tạo gói sản phẩm (combo) bao gồm nhiều sản phẩm đơn lẻ với giá bán trọn gói. Bao gồm các chức năng sau:

- Xem danh sách combo
- Thêm mới combo
- Chỉnh sửa thông tin combo
- Thiết lập thành phần sản phẩm trong combo (rổ combo)
- Phân bổ tỷ lệ doanh thu cho từng sản phẩm thành phần
- Xóa ẩn combo

---

## 1.1. Màn hình danh sách combo

### 1.1.1. Tổng quan

Màn hình hiển thị dưới dạng chia đôi (Split View). Bên trái là lưới danh sách combo. Bên phải chia thành 2 phần: phần trên là thông tin chung của combo, phần dưới chia đôi thành Kho sản phẩm (trái) và Rổ combo (phải). Khi người dùng chọn một dòng trên lưới combo, toàn bộ panel phải tự nạp thông tin của combo đó.

### 1.1.2. Tác nhân

- Quản lý (thêm, sửa, xóa combo, thiết lập tỷ lệ phân bổ)

### 1.1.3. Biểu đồ use-case

```
Quản lý ──── Xem danh sách combo
         ├── Thêm mới combo
         ├── Chỉnh sửa combo
         ├── Thiết lập thành phần combo       <<extend>> Thêm/Sửa
         ├── Phân bổ tỷ lệ doanh thu         <<extend>> Thêm/Sửa
         └── Xóa ẩn combo
```

#### 1.1.3.1. Tiền điều kiện

- Người dùng đã đăng nhập.
- Danh mục sản phẩm đã được thiết lập (tối thiểu 1 sản phẩm đang bán).

#### 1.1.3.2. Hậu điều kiện

Dữ liệu combo được lưu đồng bộ vào cơ sở dữ liệu, bao gồm thông tin chung và danh sách thành phần.

#### 1.1.3.3. Điểm kích hoạt

Người dùng truy cập menu Danh mục, chọn mục Combo sản phẩm.

### 1.1.4. Giao diện

#### 1.1.4.1. Mô tả màn hình — Thanh tiêu đề

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Tiêu đề | Label | N/A | N/A | N/A | Hiển thị tên phân hệ: Quản lý Combo sản phẩm. |
| 2 | Thêm combo | Button | N/A | N/A | N/A | Xóa trắng form, chuyển sang chế độ thêm mới. |
| 3 | Làm mới | Button | N/A | N/A | N/A | Tải lại toàn bộ dữ liệu từ cơ sở dữ liệu. |

#### 1.1.4.2. Mô tả màn hình — Lưới danh sách combo (Grid Control, Read-only)

| STT | Tên cột | Control type | Data type | Mô tả |
|---|---|---|---|---|
| 1 | Mã | Label | Text | Mã combo tự sinh bởi hệ thống. |
| 2 | Tên Combo | Label | Text | Tên hiển thị của combo. |
| 3 | Trạng thái | Label | Text | Bản nháp, Hoạt động, hoặc Ngừng áp dụng. Màu chữ thay đổi theo trạng thái. |

#### 1.1.4.3. Row style — Lưới danh sách combo

| Trạng thái | Màu chữ |
|---|---|
| Hoạt động | Xanh lá  |
| Bản nháp | Vàng đậm |
| Ngừng áp dụng | Đỏ |

#### 1.1.4.4. Mô tả màn hình — Thanh trạng thái

| STT | Tên trường | Control type | Data type | Mô tả |
|---|---|---|---|---|
| 1 | Tổng combo | Label | Text | Hiển thị: Tổng N với N bằng số combo trên lưới. |

### 1.1.5. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Split View | Lưới combo bên trái (khoảng 1 phần 4 chiều rộng). Panel chi tiết bên phải gồm thông tin chung ở trên, kho sản phẩm và rổ combo ở dưới. |
| 2 | Chọn dòng | Khi nhấn chọn một combo trên lưới, panel phải tự nạp toàn bộ thông tin và danh sách thành phần của combo đó. |
| 3 | Đa ngôn ngữ | Khi ngôn ngữ thay đổi, tải lại toàn bộ label, nút bấm, tiêu đề. |

### 1.1.6. Liên kết use-case

- Thông tin combo (1.2)
- Kho sản phẩm và Rổ combo (1.3)
- Xóa combo (1.6)

---

## 1.2. Thông tin combo

### 1.2.1. Tổng quan

Panel thông tin chung nằm ở phần trên bên phải của Split View. Hiển thị các trường nhập liệu cơ bản của combo và các nút hành động chính.

### 1.2.2. Giao diện

#### 1.2.2.1. Mô tả màn hình — Panel thông tin chung

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Mã Code | Text Edit (Read-only) | N/A | Text | Blank | Mã combo do hệ thống tự sinh khi lưu. Khi thêm mới hiển thị chữ (Tự sinh). Không cho chỉnh sửa. |
| 2 | Tên Combo | Text Edit | Yes | Nvarchar(200) | Blank | Tên hiển thị của combo. |
| 3 | Giá Combo | Text Edit | Yes | Decimal(15,0) | Blank | Giá bán trọn gói. Định dạng N0. |
| 4 | Trạng thái | Combo Box | Yes | Text | Bản nháp | Bản nháp, Hoạt động, hoặc Ngừng áp dụng. Không cho gõ tự do, chỉ chọn từ danh sách. |
| 5 | Mô tả | Memo Edit | No | Text | Blank | Ghi chú nội dung combo. Trải rộng toàn bộ chiều ngang. |
| 6 | Thêm Combo | Button | N/A | N/A | N/A | Xóa trắng form, chuẩn bị chế độ thêm mới. |
| 7 | Lưu Combo | Button | N/A | N/A | N/A | Lưu thông tin combo và toàn bộ rổ chi tiết. |
| 8 | Xóa Combo | Button | N/A | N/A | N/A | Xóa ẩn combo đang chọn. |

### 1.2.3. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Mã tự sinh | Mã combo được hệ thống tự tạo khi lưu lần đầu. Người dùng không nhập mã. |
| 2 | Ép trạng thái Bản nháp | Nếu người dùng chọn trạng thái Hoạt động nhưng tổng tỷ lệ phân bổ trong rổ combo chưa đạt đúng 100%, hệ thống tự động chuyển trạng thái về Bản nháp và hiển thị cảnh báo. |
| 3 | Lưu đồng bộ | Khi nhấn nút Lưu Combo, hệ thống lưu cả thông tin chung lẫn danh sách thành phần trong rổ combo cùng lúc. |

### 1.2.4. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Tên combo bắt buộc, không được để trống | ERR_COMBO_TEN_RONG |
| 2 | Giá combo không được là số âm | ERR_COMBO_GIA_AM |

### 1.2.5. Liên kết use-case

- Màn hình danh sách combo (1.1)
- Kho sản phẩm và Rổ combo (1.3)

---

## 1.3. Kho sản phẩm và Rổ combo

### 1.3.1. Tổng quan

Phần dưới của panel phải chia đôi theo chiều ngang. Bên trái là Kho sản phẩm, hiển thị toàn bộ sản phẩm có thể chọn vào combo. Bên phải là Rổ combo, hiển thị các sản phẩm đã được chọn vào combo hiện tại kèm số lượng, đơn giá, thành tiền và tỷ lệ phân bổ doanh thu.

### 1.3.2. Giao diện

#### 1.3.2.1. Mô tả màn hình — Panel Kho sản phẩm

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Tìm sản phẩm | Text Edit | No | Text | Blank | Lọc trực tiếp trên lưới kho theo mã hoặc tên sản phẩm. (*) Placeholder: "Tìm sản phẩm..." |
| 2 | Thêm vào Rổ | Button | N/A | N/A | N/A | Thêm sản phẩm đang chọn trên lưới kho vào rổ combo. |

#### 1.3.2.2. Mô tả màn hình — Lưới Kho sản phẩm (Grid Control, Read-only)

| STT | Tên cột | Control type | Data type | Mô tả |
|---|---|---|---|---|
| 1 | Mã SP | Label | Text | Mã sản phẩm. |
| 2 | Tên Sản Phẩm | Label | Text | Tên hiển thị sản phẩm. |
| 3 | Giá Bán | Label | Decimal(15,0) | Giá bán hiện tại. Định dạng N0. |

#### 1.3.2.3. Mô tả màn hình — Lưới Rổ combo (Grid Control, Editable)

| STT | Tên cột | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Tên SP | Label (Read-only) | N/A | Text | N/A | Tên sản phẩm, không cho sửa. |
| 2 | SL | Spin Edit (in-grid) | Yes | Integer | 1 | Số lượng sản phẩm trong combo. |
| 3 | Đơn giá | Label (Read-only) | N/A | Decimal(15,0) | N/A | Giá bán đơn lẻ. Định dạng N0. Không cho sửa. |
| 4 | Thành tiền | Label (Read-only) | N/A | Decimal(15,0) | N/A | Bằng đơn giá nhân số lượng. Định dạng N0. Tự tính. |
| 5 | Phân bổ (%) | Spin Edit (in-grid) | Yes | Decimal(5,2) | 0 | Tỷ lệ phân bổ doanh thu. Định dạng N2. |
| 6 | Xóa | Button (Delete icon) | N/A | N/A | N/A | Nút xóa dòng khỏi rổ, cố định bên phải. |

#### 1.3.2.4. Mô tả màn hình — Footer Rổ combo

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Tổng phân bổ | Label | N/A | Text | Phân bổ: 0.00% / 100% | Tổng tỷ lệ phân bổ hiện tại. Xanh lá khi đạt 100%, vàng khi chưa đủ, đỏ khi vượt 100%. |
| 2 | Tổng giá gốc | Label | N/A | Text | Giá gốc: 0đ | Tổng thành tiền tất cả sản phẩm trong rổ. |
| 3 | Chia đều | Button | N/A | N/A | N/A | Tự chia đều tỷ lệ phân bổ cho tất cả sản phẩm sao cho tổng bằng 100%. |
| 4 | Thanh phân bổ | Bar Chart | N/A | N/A | N/A | Thanh ngang trực quan hiển thị tỷ lệ phân bổ từng sản phẩm bằng màu khác nhau. |

### 1.3.3. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Thêm vào rổ | Nhấn nút Thêm vào Rổ hoặc nhấn đúp chuột vào dòng trên lưới kho. Nếu sản phẩm đã có trong rổ, hệ thống tự cộng thêm 1 vào số lượng. |
| 2 | Tìm kiếm kho | Gõ ký tự vào ô tìm, lưới kho tự lọc ngay theo mã hoặc tên sản phẩm. |
| 3 | Chia đều tỷ lệ | Chia bằng nhau cho tất cả sản phẩm. Dòng cuối nhận phần dư để tổng luôn đạt chính xác 100%. |
| 4 | Cập nhật tức thì | Mỗi khi thay đổi số lượng hoặc tỷ lệ, hệ thống tự cập nhật tổng và thanh phân bổ ngay. |
| 5 | Dữ liệu tạm | Dữ liệu rổ chỉ nằm trong bộ nhớ tạm của ứng dụng. Chỉ khi nhấn Lưu Combo mới ghi vào cơ sở dữ liệu. |
| 6 | Tỷ lệ phân bổ dùng để gì | Tỷ lệ phân bổ được hệ thống dùng để tính doanh thu theo từng thành phần khi báo cáo. Ví dụ: combo 300,000đ, sản phẩm chiếm 60% → doanh thu phân bổ = 180,000đ. |

### 1.3.4. Liên kết use-case

- Thông tin combo (1.2)
- Thêm mới combo (1.4)
- Chỉnh sửa combo (1.5)

---

## 1.4. Thêm mới combo

### 1.4.1. Tổng quan

Cho phép tạo một combo mới bao gồm thông tin chung và danh sách thành phần sản phẩm.

### 1.4.2. Tác nhân

- Quản lý

### 1.4.3. Luồng thao tác

#### 1.4.3.1. Tình huống 1 — Thêm combo thành công

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Thêm Combo. | Xóa trắng form. Mã combo hiển thị chữ (Tự sinh). Trạng thái mặc định là Bản nháp. Rổ combo trống. |
| 2 | Nhập tên combo, giá combo, mô tả. Chọn trạng thái. | Ghi nhận. |
| 3 | Trên lưới Kho, nhấn đúp hoặc nhấn nút Thêm vào Rổ để chọn sản phẩm. | Sản phẩm xuất hiện trên rổ combo với số lượng bằng 1. |
| 4 | Nhập tỷ lệ phân bổ cho từng sản phẩm, hoặc nhấn nút Chia đều. | Cập nhật tổng phân bổ và thanh trực quan. |
| 5 | Nhấn nút Lưu Combo. | Kiểm tra tên không trống, giá không âm. Nếu trạng thái Hoạt động mà tổng phân bổ khác 100%, ép về Bản nháp và cảnh báo. Lưu combo và chi tiết rổ, thông báo thành công, tải lại lưới. |

### 1.4.4. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Tên combo bắt buộc | ERR_COMBO_TEN_RONG |
| 2 | Giá combo không được âm | ERR_COMBO_GIA_AM |
| 3 | Tổng phân bổ phải đạt 100% nếu muốn kích hoạt | MSG_COMBO_TYLE_CHUA_DU |

### 1.4.5. Liên kết use-case

- Thông tin combo (1.2)
- Kho sản phẩm và Rổ combo (1.3)

---

## 1.5. Chỉnh sửa combo

### 1.5.1. Tổng quan

Cho phép chỉnh sửa thông tin và thành phần của combo đã tồn tại.

### 1.5.2. Tác nhân

- Quản lý

### 1.5.3. Luồng thao tác

#### 1.5.3.1. Tình huống 1 — Chỉnh sửa thành công

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Nhấp chọn một combo trên lưới danh sách. | Nạp thông tin combo: mã, tên, giá, mô tả, trạng thái. Nạp danh sách thành phần vào rổ combo. |
| 2 | Sửa các trường cần thay đổi. | Cập nhật tổng phân bổ và thanh trực quan. |
| 3 | Nhấn nút Lưu Combo. | Kiểm tra nghiệp vụ. Lưu thông tin và ghi đè rổ chi tiết. Thông báo thành công, tải lại rổ. |

### 1.5.4. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Ghi đè rổ | Khi lưu, xóa toàn bộ thành phần cũ rồi ghi lại danh sách mới từ rổ. |
| 2 | Ép Bản nháp | Nếu chuyển sang Hoạt động mà tổng phân bổ khác 100%, tự ép về Bản nháp. |

### 1.5.4. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Tên combo bắt buộc, không được để trống | ERR_COMBO_TEN_RONG |
| 2 | Giá combo không được là số âm | ERR_COMBO_GIA_AM |
| 3 | Tổng phân bổ phải đạt 100% nếu muốn kích hoạt | MSG_COMBO_TYLE_CHUA_DU |

### 1.5.5. Liên kết use-case

- Màn hình danh sách combo (1.1)
- Kho sản phẩm và Rổ combo (1.3)

---

## 1.6. Xóa combo

### 1.6.1. Tổng quan

Chức năng xóa ẩn combo (soft delete). Hệ thống không xóa vật lý mà chỉ đánh dấu đã xóa.

### 1.6.2. Tác nhân

- Quản lý

### 1.6.3. Luồng thao tác

| | Người dùng | Hệ thống |
|---|---|---|
| 1 | Chọn một combo trên lưới, nhấn nút Xóa Combo. | Hiển thị hộp thoại xác nhận xóa kèm tên combo. |
| 2 | Nhấn nút Có. | Đánh dấu combo đã xóa. Thông báo thành công. Tải lại lưới và xóa trắng form. |

### 1.6.4. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Xóa ẩn | Combo chỉ được đánh dấu đã xóa, không xóa vật lý. Combo đã xóa không còn hiển thị trên lưới. |
| 2 | Phải chọn combo | Nếu chưa chọn combo nào, nút Xóa Combo không thực hiện hành động nào. |

### 1.6.4. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Xóa thành công | MSG_XOA_OK |

### 1.6.5. Liên kết use-case

- Màn hình danh sách combo (1.1)

---

# 2. Yêu cầu khác

## 2.1. Định dạng dữ liệu

### 2.1.1. Số

- Giá combo: dấu phẩy phân cách hàng nghìn, không có chữ số thập phân, kèm ký hiệu đ. Ví dụ: 350,000đ
- Tỷ lệ phân bổ: định dạng N2 (2 chữ số thập phân). Ví dụ: 33.33%

## 2.2. Danh mục dữ liệu tham chiếu

### 2.2.1. Trạng thái combo

| Mã | Tên hiển thị |
|---|---|
| BanNhap | Bản nháp |
| HoatDong | Hoạt động |
| NgungApDung | Ngưng áp dụng |

## 2.3. Bảng mã thông báo lỗi

| Mã thông báo | Nội dung tiếng Việt |
|---|---|
| ERR_COMBO_TEN_RONG | Tên combo không được để trống |
| ERR_COMBO_GIA_AM | Giá combo không được là số âm |
| MSG_COMBO_TYLE_CHUA_DU | Tổng tỷ lệ phân bổ chưa đạt 100%, combo tự chuyển về trạng thái Bản nháp |
| MSG_LUU_OK | Lưu thành công |
| MSG_XOA_OK | Xóa combo thành công |
