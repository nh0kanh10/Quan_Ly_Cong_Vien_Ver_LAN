# Khu Du Lịch Đại Nam
# Đặc Tả Yêu Cầu Phần Mềm
# Mã dự án: DN01
# Mã tài liệu: DN01_SRS_BaoCaoDoanhThu_v1.0

Hồ Chí Minh, Tháng 04/2026

---

## Lịch sử thay đổi

| Ngày hiệu lực | Hạng mục thay đổi | A/M/D | Mô tả | Phiên bản |
|---|---|---|---|---|
| 29/04/2026 | Đặc tả Báo cáo Doanh thu – phát hành lần đầu | A | Tạo mới tài liệu SRS cho Module Báo cáo Doanh thu | 1.0 |

*A - Thêm mới, M - Chỉnh sửa, D - Xóa bỏ*

## Mục lục

1. [CN12 – Báo cáo Doanh thu](#1-cn12--báo-cáo-doanh-thu)
   - 1.1. Truy vấn dữ liệu doanh thu tổng hợp
     - 1.1.1. Tổng quan
     - 1.1.2. Tác nhân
     - 1.1.3. Biểu đồ use-case
     - 1.1.4. Luồng thao tác
     - 1.1.5. Giao diện
     - 1.1.6. Mô tả nghiệp vụ
     - 1.1.7. Quy tắc kiểm tra
     - 1.1.8. Liên kết use-case
   - 1.2. Tùy biến chiều phân tích dữ liệu (Xoay trục Pivot)
     - 1.2.1. Tổng quan
     - 1.2.2. Tác nhân
     - 1.2.3. Biểu đồ use-case
     - 1.2.4. Luồng thao tác
     - 1.2.5. Giao diện
     - 1.2.6. Mô tả nghiệp vụ
     - 1.2.7. Quy tắc kiểm tra
     - 1.2.8. Liên kết use-case
   - 1.3. Xuất báo cáo định dạng Excel
     - 1.3.1. Tổng quan
     - 1.3.2. Tác nhân
     - 1.3.3. Biểu đồ use-case
     - 1.3.4. Luồng thao tác
     - 1.3.5. Giao diện
     - 1.3.6. Mô tả nghiệp vụ
     - 1.3.7. Quy tắc kiểm tra
     - 1.3.8. Liên kết use-case

2. [Yêu cầu khác](#2-yêu-cầu-khác)
   - 2.1. Định dạng dữ liệu
   - 2.2. Danh mục dữ liệu tham chiếu
   - 2.3. Bảng mã thông báo lỗi

---

# 1. CN12 – Báo cáo Doanh thu

## 1.1. Truy vấn dữ liệu doanh thu tổng hợp

### 1.1.1. Tổng quan
Use-case này mô tả cách người dùng truy xuất số liệu doanh thu tổng hợp trong một khoảng thời gian cụ thể. Hệ thống sẽ quét toàn bộ hóa đơn hợp lệ từ các dịch vụ Vé, F&B, Lưu trú, Cho thuê để trả về kết quả phân tích chi tiết.

### 1.1.2. Tác nhân
- Nhân viên kế toán, Quản lý

### 1.1.3. Biểu đồ use-case / Activity Diagram
- [Tải sơ đồ Use Case](file:///c:/Users/ADMIN/Desktop/DaiNamNew/docs/Sprint1/08_UseCase_BaoCaoDoanhThu_v1.0.html)
- [Xem luồng Activity Diagram](file:///c:/Users/ADMIN/Desktop/DaiNamNew/activity_diagrams.html)

#### 1.1.3.1. Tiền điều kiện
- Người dùng đã đăng nhập thành công và được cấp quyền kế toán hoặc quản lý.

#### 1.1.3.2. Hậu điều kiện
- Số liệu tổng hợp được nạp tạm thời vào bộ nhớ đệm và hiển thị trực quan trên lưới.

#### 1.1.3.3. Điểm kích hoạt
Người dùng nhấn nút Làm mới trên thanh công cụ.

### 1.1.4. Luồng thao tác

#### 1.1.4.1. Tình huống 1 — Luồng chuẩn (Happy Path)
| | Nhân viên kế toán | Hệ thống |
|---|---|---|
| 1 | Nhập khoảng thời gian hợp lệ tại Từ ngày và Đến ngày. | Ghi nhận khoảng thời gian. |
| 2 | Nhấn nút Làm mới. | Kiểm tra ràng buộc ngày. |
| 3 | — | Truy xuất dữ liệu doanh thu theo thời gian đã chọn. |
| 4 | — | Cập nhật số liệu và nạp toàn bộ danh mục sản phẩm liên quan lên Pivot Grid. |

#### 1.1.4.2. Tình huống 2 — Khoảng thời gian không hợp lệ
| | Nhân viên kế toán | Hệ thống |
|---|---|---|
| 1 | Nhập ngày bắt đầu lớn hơn ngày kết thúc. | Ghi nhận giá trị. |
| 2 | Nhấn nút Làm mới. | Chặn thao tác, hiển thị thông báo lỗi ERR_BC_NGAY_KHONG_HOP_LE. |

### 1.1.5. Giao diện

#### 1.1.5.1. Bảng thuộc tính các trường dữ liệu (Field Description Table)
| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Từ ngày | Date Edit | Yes | DateTime | Ngày 1 của tháng | Mốc thời gian bắt đầu thống kê. (*) Tooltip: Chọn ngày bắt đầu |
| 2 | Đến ngày | Date Edit | Yes | DateTime | Ngày hiện tại | Mốc thời gian kết thúc thống kê. (*) Tooltip: Chọn ngày kết thúc |
| 3 | Làm mới | Button | N/A | N/A | N/A | Nạp lại dữ liệu giao dịch. |
| 4 | Tổng Doanh Thu | Label | N/A | Decimal(18,0) | 0₫ | Hiển thị tổng tiền thu được. |

### 1.1.6. Mô tả nghiệp vụ
| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Định nghĩa doanh thu | Doanh thu chỉ được ghi nhận đối với các đơn hàng có trạng thái Đã thanh toán. Các đơn hàng bị hủy hoặc đang phục vụ không được cộng dồn. |
| 2 | Bóc tách nguồn tiền | Tiền được chia nhỏ dựa theo bảng chi tiết đơn hàng để bảo đảm doanh thu được ghi nhận đúng cho từng dịch vụ thành phần trong trường hợp thanh toán gộp. |

### 1.1.7. Quy tắc kiểm tra
| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Ngày bắt đầu không được lớn hơn ngày kết thúc | ERR_BC_NGAY_KHONG_HOP_LE |

### 1.1.8. Liên kết use-case
- Tùy biến chiều phân tích dữ liệu (1.2)

---

## 1.2. Tùy biến chiều phân tích dữ liệu (Xoay trục Pivot)

### 1.2.1. Tổng quan
Người dùng có thể tùy ý sắp xếp, gom nhóm các cột Ngày giao dịch, Khách hàng, Thu ngân, Nhóm sản phẩm để phục vụ các nhu cầu báo cáo chuyên sâu khác nhau.

### 1.2.2. Tác nhân
- Nhân viên kế toán

### 1.2.3. Biểu đồ use-case / Activity Diagram
- [Tải sơ đồ Use Case](file:///c:/Users/ADMIN/Desktop/DaiNamNew/docs/Sprint1/08_UseCase_BaoCaoDoanhThu_v1.0.html)
- [Xem luồng Activity Diagram](file:///c:/Users/ADMIN/Desktop/DaiNamNew/activity_diagrams.html)

#### 1.2.3.1. Tiền điều kiện
- Lưới đã được nạp dữ liệu thành công từ use-case 1.1.

#### 1.2.3.2. Hậu điều kiện
- Bố cục lưới thay đổi tương ứng với cấu hình người dùng vừa thiết lập.

#### 1.2.3.3. Điểm kích hoạt
Người dùng kéo thả tiêu đề cột trên giao diện lưới.

### 1.2.4. Luồng thao tác

#### 1.2.4.1. Tình huống 1 — Phân tích doanh thu theo Nhân viên thu ngân
| | Nhân viên kế toán | Hệ thống |
|---|---|---|
| 1 | Kéo cột Thu ngân từ vùng Lọc thả vào vùng Dòng. | Tự động gom nhóm dữ liệu theo từng Thu ngân. |
| 2 | — | Tính toán lại doanh thu tổng cho từng thu ngân tại các dòng trung gian. |

### 1.2.5. Giao diện

#### 1.2.5.1. Bảng dữ liệu Pivot Grid (Read-only)
| STT | Tên hiển thị | Control type | Data type | Mô tả |
|---|---|---|---|---|
| 1 | Ngày giao dịch | Label (in-grid) | DateTime | Mốc thời gian bán hàng |
| 2 | Loại giao dịch | Label (in-grid) | Nvarchar(100) | Nhóm doanh thu dịch vụ |
| 3 | Tên sản phẩm | Label (in-grid) | Nvarchar(200) | Tên mặt hàng kinh doanh |
| 4 | Doanh thu | Label (in-grid) | Decimal(18,0) | Số tiền thanh toán |

### 1.2.6. Mô tả nghiệp vụ
| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Kéo thả linh hoạt | Hỗ trợ kéo thả các trường từ danh sách (Field List) vào bốn vùng hiển thị chính (Row, Column, Data, Filter). |

### 1.2.7. Quy tắc kiểm tra
| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Vùng dữ liệu phải luôn chứa ít nhất 1 trường giá trị số | — |

### 1.2.8. Liên kết use-case
- Xuất báo cáo định dạng Excel (1.3)

---

## 1.3. Xuất báo cáo định dạng Excel

### 1.3.1. Tổng quan
Hỗ trợ xuất toàn bộ giao diện báo cáo doanh thu ra tệp tin Excel (.xlsx) giữ nguyên cấu trúc phân tích đã dựng.

### 1.3.2. Tác nhân
- Nhân viên kế toán

### 1.3.3. Biểu đồ use-case / Activity Diagram
- [Tải sơ đồ Use Case](file:///c:/Users/ADMIN/Desktop/DaiNamNew/docs/Sprint1/08_UseCase_BaoCaoDoanhThu_v1.0.html)
- [Xem luồng Activity Diagram](file:///c:/Users/ADMIN/Desktop/DaiNamNew/activity_diagrams.html)

#### 1.3.3.1. Tiền điều kiện
- Báo cáo đang có dữ liệu hiển thị.

#### 1.3.3.2. Hậu điều kiện
- File Excel được lưu thành công xuống ổ đĩa máy tính.

#### 1.3.3.3. Điểm kích hoạt
Người dùng bấm nút Xuất Excel.

### 1.3.4. Luồng thao tác

#### 1.3.4.1. Tình huống 1 — Xuất file thành công
| | Nhân viên kế toán | Hệ thống |
|---|---|---|
| 1 | Nhấp nút Xuất Excel. | Mở cửa sổ lưu tệp tin hệ thống. |
| 2 | Đặt tên tệp tin và nhấn Lưu. | Trích xuất dữ liệu Pivot Grid và tiến hành xuất file. |
| 3 | — | Thông báo thành công MSG_BC_XUAT_EXCEL_THANH_CONG. |

### 1.3.5. Giao diện
Sử dụng SaveFileDialog hệ thống để lưu tệp tin.

### 1.3.6. Mô tả nghiệp vụ
| STT | Tên | Quy tắc |
|---|---|---|
| 1 | WYSIWYG | Bố cục dữ liệu, gom nhóm, các dòng tổng thu trong tệp Excel phải giống hệt bố cục đang xem trên màn hình ứng dụng. |

### 1.3.7. Quy tắc kiểm tra
| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Không thể xuất báo cáo khi bảng dữ liệu đang rỗng | ERR_BC_DULIEU_RONG |

### 1.3.8. Liên kết use-case
- Truy vấn dữ liệu doanh thu tổng hợp (1.1)

---

# 2. Yêu cầu khác

## 2.1. Định dạng dữ liệu
- **Tiền tệ**: Hiển thị có dấu phân cách phần nghìn, kèm hậu tố ₫. Ví dụ: 25,000₫.
- **Thời gian**: dd/MM/yyyy.

## 2.2. Danh mục dữ liệu tham chiếu
- **Loại sản phẩm**: Vé vào khu, Vé trò chơi, Ăn uống, Đồ uống, Đồ cho thuê, Lưu trú, Hàng hóa, Nguyên liệu.

## 2.3. Bảng mã thông báo lỗi

| Mã thông báo | Nội dung tiếng Việt |
|---|---|
| ERR_BC_NGAY_KHONG_HOP_LE | Ngày bắt đầu không được lớn hơn ngày kết thúc |
| MSG_BC_XUAT_EXCEL_THANH_CONG | Xuất dữ liệu báo cáo Excel thành công |
| ERR_BC_DULIEU_RONG | Bảng số liệu đang trống, không thể xuất Excel |

