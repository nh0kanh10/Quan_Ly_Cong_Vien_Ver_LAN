# Khu Du Lịch Đại Nam
# Đặc Tả Yêu Cầu Phần Mềm
# Mã dự án: DN01
# Mã tài liệu: DN01_SRS_PhanQuyen_v1.0

Hồ Chí Minh, Tháng 04/2026

---

## Lịch sử thay đổi

| Ngày hiệu lực | Hạng mục thay đổi | A/M/D | Mô tả | Phiên bản |
|---|---|---|---|---|
| 23/04/2026 | Phát hành lần đầu | A | | 1.0 |

*A - Thêm mới, M - Chỉnh sửa, D - Xóa bỏ*

---

## 0. Phạm vi tài liệu

Tài liệu này đặc tả phân hệ Quản lý Phân quyền thuộc hệ thống quản lý vận hành Khu Du lịch Đại Nam.
- **Phạm vi bao gồm:** xem danh sách vai trò, xem và cập nhật quyền theo vai trò, lưu thay đổi phân quyền.
- **Không bao gồm:** quản lý tài khoản người dùng, phân ca, báo cáo nhân sự.
- **Đối tượng đọc:** BA, Lập trình viên (Dev), Quản trị viên hệ thống.

---

## Mục lục

1. [Phân quyền hệ thống](#1-phân-quyền-hệ-thống)
   - 1.1. [Màn hình thiết lập phân quyền](#11-màn-hình-thiết-lập-phân-quyền)
   - 1.2. [Xem quyền theo vai trò](#12-xem-quyền-theo-vai-trò)
   - 1.3. [Cập nhật quyền cho vai trò](#13-cập-nhật-quyền-cho-vai-trò)
2. [Yêu cầu khác](#2-yêu-cầu-khác)
   - 2.1. [Danh mục dữ liệu tham chiếu](#21-danh-mục-dữ-liệu-tham-chiếu)
   - 2.2. [Bảng mã thông báo lỗi](#22-bảng-mã-thông-báo-lỗi)

---

# 1. Phân quyền hệ thống

Phân hệ phân quyền cho phép quản trị viên thiết lập quyền truy cập các chức năng trong hệ thống theo từng vai trò. Bao gồm các chức năng sau:

- Xem danh sách vai trò
- Xem cây phân quyền theo nhóm chức năng
- Tích chọn hoặc bỏ chọn quyền cho một vai trò cụ thể
- Lưu thay đổi phân quyền

---

## 1.1. Màn hình thiết lập phân quyền

### 1.1.1. Tổng quan

Màn hình này hiển thị dưới dạng chia đôi (Split View). Bên trái là danh sách các vai trò trong hệ thống. Bên phải là cây phân quyền hiển thị toàn bộ danh mục quyền hạn được tổ chức theo nhóm chức năng. Khi người dùng chọn một vai trò bên trái, cây bên phải tự động tích chọn các quyền đã được gán cho vai trò đó.

### 1.1.2. Tác nhân

- Quản trị viên hệ thống (xem, gán, thu hồi quyền)

### 1.1.3. Biểu đồ use-case

```
Quản trị viên ──── Xem danh sách vai trò
               ├── Xem quyền theo vai trò
               ├── Gán quyền cho vai trò
               ├── Thu hồi quyền khỏi vai trò
               └── Lưu thay đổi phân quyền
```

#### 1.1.3.1. Tiền điều kiện

- Người dùng đã đăng nhập với tài khoản có quyền quản trị hệ thống.
- Danh mục vai trò đã được thiết lập trong hệ thống (tối thiểu 1 vai trò).
- Danh mục quyền hạn đã được khai báo trong cơ sở dữ liệu, mỗi quyền thuộc một nhóm quyền nhất định.

#### 1.1.3.2. Hậu điều kiện

Dữ liệu phân quyền được lưu đồng bộ vào cơ sở dữ liệu. Vai trò được chọn sẽ chỉ có quyền truy cập các chức năng đã được tích chọn trên cây phân quyền.

#### 1.1.3.3. Điểm kích hoạt

Người dùng truy cập menu Hệ thống, chọn mục Phân quyền.

### 1.1.4. Giao diện

#### 1.1.4.1. Mô tả màn hình — Panel danh sách vai trò (bên trái)

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Danh sách vai trò | List Box | N/A | N/A | Dòng đầu tiên | Hiển thị tên các vai trò đang có trong hệ thống. Nhấn chọn một vai trò để xem và thiết lập quyền tương ứng. |

#### 1.1.4.2. Mô tả màn hình — Panel cây phân quyền (bên phải)

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Cây phân quyền | Tree List | N/A | N/A | Tất cả bỏ chọn | Hiển thị toàn bộ danh mục quyền theo dạng cây phân cấp 2 bậc. Bậc 1 là nhóm quyền (nút cha). Bậc 2 là quyền cụ thể (nút con). Mỗi nút có ô tích chọn. |
| 2 | Cột Danh mục quyền | Tree List Column | N/A | Text | N/A | Cột duy nhất trên cây, hiển thị tên nhóm quyền hoặc tên quyền cụ thể. |

#### 1.1.4.3. Mô tả màn hình — Thanh công cụ (Footer)

| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Lưu thay đổi | Button | N/A | N/A | N/A | Lưu toàn bộ thay đổi phân quyền cho vai trò đang chọn. |
| 2 | Làm mới | Button | N/A | N/A | N/A | Tải lại danh sách vai trò và cây phân quyền từ cơ sở dữ liệu. Mọi thay đổi chưa lưu sẽ bị mất. |

### 1.1.5. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Split View | Giao diện chia đôi theo chiều ngang. Bên trái là danh sách vai trò (chiếm khoảng 1 phần 4 chiều rộng). Bên phải là cây phân quyền (chiếm phần còn lại). |
| 2 | Cây phân cấp 2 bậc | Cây phân quyền gồm 2 bậc. Bậc 1 là các nhóm quyền (nút cha), đại diện cho một nhóm chức năng nghiệp vụ (ví dụ: Quản lý Bán Hàng, Quản lý Kho). Bậc 2 là các quyền cụ thể (nút con), đại diện cho từng thao tác trong nhóm đó (ví dụ: Thêm Đơn Hàng, Xem Báo Cáo Kho). |
| 3 | Nhóm quyền tự động | Hệ thống tự phân nhóm các quyền dựa trên trường Nhóm quyền trong cơ sở dữ liệu. Những quyền không thuộc nhóm nào được tự động gom vào nhóm Khác. |
| 4 | Tích chọn đệ quy | Khi tích hoặc bỏ tích một nút cha (nhóm quyền), tất cả các nút con bên trong tự động được tích hoặc bỏ tích theo. Ngược lại, khi tích đủ tất cả nút con, nút cha tự động được tích. Khi bỏ một nút con bất kỳ, nút cha chuyển về trạng thái nửa tích (indeterminate). |
| 5 | Không cho sửa tên | Cây phân quyền chỉ cho phép tích chọn và bỏ chọn. Không cho phép chỉnh sửa tên quyền trực tiếp trên cây. |
| 6 | Mở rộng mặc định | Khi tải lên, tất cả các nhóm quyền trên cây đều ở trạng thái mở rộng để người dùng nhìn thấy toàn bộ quyền. |
| 7 | Đa ngôn ngữ | Tên nhóm quyền trên cây được dịch đa ngôn ngữ. Tiêu đề form, tên nút, tên cột cũng được dịch theo ngôn ngữ đang dùng. |

### 1.1.6. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Không có quy tắc đặc biệt | — |

### 1.1.7. Liên kết use-case

- Xem quyền theo vai trò (1.2)
- Cập nhật quyền cho vai trò (1.3)

---

## 1.2. Xem quyền theo vai trò

### 1.2.1. Tổng quan

Khi người dùng chọn một vai trò trên danh sách bên trái, hệ thống tự động truy vấn danh sách quyền đã gán cho vai trò đó và hiển thị lên cây phân quyền bằng cách tích chọn các nút con tương ứng.

### 1.2.2. Tác nhân

- Quản trị viên hệ thống

### 1.2.3. Luồng thao tác

#### 1.2.3.1. Tình huống 1 — Xem quyền của một vai trò

| | Quản trị viên | Hệ thống |
|---|---|---|
| 1 | Nhấn chọn một vai trò trên danh sách bên trái (ví dụ: Quản lý). | Truy vấn danh sách quyền đã gán cho vai trò đó từ cơ sở dữ liệu. |
| 2 | — | Bỏ tích toàn bộ cây trước. Sau đó tích chọn các nút con có quyền nằm trong danh sách vừa truy vấn. Các nút cha tự động cập nhật trạng thái tích (đầy hoặc nửa tích) tùy theo số nút con được tích. |

#### 1.2.3.2. Tình huống 2 — Chuyển sang vai trò khác

| | Quản trị viên | Hệ thống |
|---|---|---|
| 1 | Nhấn chọn một vai trò khác trên danh sách. | Bỏ tích toàn bộ cây. Tải lại danh sách quyền của vai trò mới và tích chọn tương ứng. |

#### 1.2.3.3. Tình huống 3 — Lỗi khi tải quyền

| | Quản trị viên | Hệ thống |
|---|---|---|
| 1 | Chọn một vai trò trên danh sách. | Truy vấn danh sách quyền nhưng xảy ra lỗi kết nối. Hiển thị thông báo lỗi. Cây phân quyền giữ nguyên trạng thái cũ. |

### 1.2.4. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Chỉ tích nút con | Hệ thống chỉ tích chọn các nút con (quyền cụ thể). Các nút cha (nhóm quyền) tự cập nhật trạng thái tích dựa trên nút con bên trong. |
| 2 | Tải ngay khi chọn | Mỗi khi người dùng chọn một vai trò mới, cây tự cập nhật ngay lập tức, không cần nhấn thêm nút nào. |

### 1.2.5. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Không có quy tắc kiểm tra đặc biệt cho thao tác xem | — |

### 1.2.6. Liên kết use-case

- Màn hình thiết lập phân quyền (1.1)
- Cập nhật quyền cho vai trò (1.3)

---

## 1.3. Cập nhật quyền cho vai trò

### 1.3.1. Tổng quan

Cho phép quản trị viên thay đổi bộ quyền của một vai trò bằng cách tích chọn hoặc bỏ chọn các quyền trên cây, sau đó nhấn nút Lưu thay đổi để ghi nhận vào cơ sở dữ liệu.

### 1.3.2. Tác nhân

- Quản trị viên hệ thống

### 1.3.3. Luồng thao tác

#### 1.3.3.1. Tình huống 1 — Gán thêm quyền cho vai trò

| | Quản trị viên | Hệ thống |
|---|---|---|
| 1 | Chọn vai trò Nhân viên thu ngân trên danh sách. | Tải và hiển thị quyền hiện có của vai trò trên cây. |
| 2 | Tích chọn thêm các quyền mong muốn (ví dụ: tích chọn nhóm Quản lý Bán Hàng để gán toàn bộ quyền bán hàng). | Tất cả nút con trong nhóm được tích. |
| 3 | Nhấn nút Lưu thay đổi. | Hệ thống thu thập toàn bộ quyền đang được tích trên cây (chỉ lấy nút con, bỏ qua nút cha). Gửi danh sách quyền mới lên để ghi đè toàn bộ quyền cũ của vai trò. Nếu thành công, hiển thị thông báo cập nhật thành công. |

#### 1.3.3.2. Tình huống 2 — Thu hồi quyền khỏi vai trò

| | Quản trị viên | Hệ thống |
|---|---|---|
| 1 | Chọn vai trò cần thu hồi quyền. | Tải và hiển thị quyền hiện có. |
| 2 | Bỏ tích các quyền muốn thu hồi. | Cập nhật trạng thái cây. |
| 3 | Nhấn nút Lưu thay đổi. | Lưu bộ quyền mới (đã bỏ các quyền vừa thu hồi). Hiển thị thông báo thành công. |

#### 1.3.3.3. Tình huống 3 — Lưu khi chưa chọn vai trò

| | Quản trị viên | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Lưu thay đổi mà chưa chọn vai trò nào trên danh sách. | Hệ thống không thực hiện thao tác nào, bỏ qua yêu cầu lưu. |

#### 1.3.3.4. Tình huống 4 — Lưu thất bại

| | Quản trị viên | Hệ thống |
|---|---|---|
| 1 | Nhấn nút Lưu thay đổi. | Hệ thống gửi dữ liệu nhưng xảy ra lỗi (mất kết nối, lỗi cơ sở dữ liệu). Hiển thị hộp thoại với thông báo lỗi cụ thể. |

### 1.3.4. Mô tả nghiệp vụ

| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Ghi đè toàn bộ | Khi lưu, hệ thống xóa toàn bộ quyền cũ của vai trò rồi ghi lại danh sách quyền mới. Cách này đảm bảo trạng thái quyền trên cây luôn khớp chính xác với dữ liệu trong cơ sở dữ liệu. |
| 2 | Chỉ lưu nút con | Hệ thống chỉ lưu các quyền cụ thể (nút con). Các nhóm quyền (nút cha) không phải là quyền thực, không được lưu vào cơ sở dữ liệu. |
| 3 | Kiểm tra vai trò hợp lệ | Hệ thống từ chối cập nhật nếu mã vai trò không hợp lệ (nhỏ hơn hoặc bằng 0) và trả về thông báo lỗi. |
| 4 | Làm mới | Nhấn nút Làm mới sẽ tải lại danh sách vai trò và xây dựng lại cây phân quyền từ đầu. Mọi thay đổi chưa lưu trên cây sẽ bị mất. |

### 1.3.5. Quy tắc kiểm tra dữ liệu

| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Phải chọn một vai trò trước khi nhấn nút Lưu thay đổi | ERR_PHANQUYEN_CHUA_CHON_VAITRO |
| 2 | Mã vai trò phải là số dương hợp lệ | ERR_PHANQUYEN_INVALID_ROLE |

### 1.3.6. Liên kết use-case

- Màn hình thiết lập phân quyền (1.1)
- Xem quyền theo vai trò (1.2)

---

# 2. Yêu cầu khác

## 2.1. Định dạng dữ liệu

### 2.1.1. Text
- Tên vai trò và tên quyền hiển thị không quá 50 ký tự, hỗ trợ đa ngôn ngữ Unicode.
## 2.2. Danh mục dữ liệu tham chiếu

### 2.2.1. Vai trò mẫu

| Tên vai trò | Mô tả |
|---|---|
| Quản trị viên | Toàn quyền truy cập hệ thống |
| Quản lý | Quản lý nhân sự, sản phẩm, báo cáo |
| Thu ngân | Thao tác bán hàng POS |
| Kế toán | Xem báo cáo, quản lý kho |
| Nhân viên | Quyền hạn chế, chỉ xem |

### 2.2.2. Nhóm quyền mẫu (dữ liệu minh họa)

| Nhóm quyền | Ví dụ quyền con |
|---|---|
| Quản lý Bán Hàng | Tạo đơn hàng, Hủy đơn hàng, Hoàn hàng |
| Quản lý Sản Phẩm | Thêm sản phẩm, Sửa sản phẩm, Xóa sản phẩm |
| Quản lý Kho | Nhập kho, Xuất kho, Kiểm kê |
| Quản lý Nhân Sự | Thêm nhân viên, Sửa nhân viên, Phân ca |
| Báo Cáo | Xem báo cáo doanh thu, Xem báo cáo tồn kho |
| Hệ Thống | Phân quyền, Cấu hình hệ thống |
| Khác | Các quyền chưa được phân nhóm |

## 2.3. Bảng mã thông báo lỗi

| Mã thông báo | Nội dung tiếng Việt |
|---|---|
| ERR_PHANQUYEN_CHUA_CHON_VAITRO | Vui lòng chọn một vai trò trước khi lưu |
| ERR_PHANQUYEN_INVALID_ROLE | Vai trò không hợp lệ, không thể cập nhật quyền |
| MSG_PHANQUYEN_SUCCESS | Cập nhật phân quyền thành công |
| ERR_PHANQUYEN_FAIL | Cập nhật phân quyền thất bại. Vui lòng thử lại |


