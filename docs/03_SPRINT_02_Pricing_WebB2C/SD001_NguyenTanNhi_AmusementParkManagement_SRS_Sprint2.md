# SOFTWARE REQUIREMENT SPECIFICATION (SRS) – Sprint 2

**Mã dự án**: SD001  
**Mã tài liệu**: SD001_SRS_Sprint2_v2.0  
**Nhóm**: NguyenTanNhi  
**Đề tài**: Hệ thống Quản lý Công viên Vui chơi Giải trí  
**Phiên bản**: 2.0  
**Ngày tạo**: 25/03/2026  
**Địa điểm**: TP. Hồ Chí Minh  

---

## RECORD OF CHANGE

*A - Added, M - Modified, D - Deleted*

| Effective Date | Changed Items | A/M/D | Change Description | New Version |
|----------------|--------------|-------|-------------------|-------------|
| 04/03/2026 | First release | A | Khởi tạo SRS Sprint 1 | 1.0 |
| 15/03/2026 | Date Handling & UI | M | Quy định NgayCapNhat mặc định 01/01/1753 | 1.1 |
| 25/03/2026 | Sprint 2 Release | A | Bổ sung Nhân viên, Khách hàng, Dịch vụ theo chuẩn chi tiết | 2.0 |

---

## SIGNATURE PAGE

| Vai trò | Họ tên | Ngày |
|---------|--------|------|
| **ORIGINATOR** | [Thành viên 1] | 25/03/2026 |
| **REVIEWERS** | [Thành viên 2], [Thành viên 3] | |
| **APPROVAL** | | |

---

## TABLE OF CONTENT

1. [QUẢN LÝ NHÂN VIÊN](#1-quản-lý-nhân-viên)
2. [QUẢN LÝ KHÁCH HÀNG](#2-quản-lý-khách-hàng)
3. [QUẢN LÝ DANH MỤC DỊCH VỤ](#3-quản-lý-danh-mục-dịch-vụ)
4. [PHỤ LỤC](#4-phụ-lục)

---

## 1. QUẢN LÝ NHÂN VIÊN

### 1.2.1. Overview
Cho phép Quản trị viên quản lý danh sách nhân sự trong toàn công viên. Hệ thống lưu trữ thông tin cá nhân, hình ảnh chân dung, chức vụ, bộ phận và trạng thái làm việc thực tế của từng nhân viên.

### 1.2.2. Actors
- Admin

### 1.2.3. Use-case diagram
![Employee Management Use Case Diagram](C:/Users/ADMIN/.gemini/antigravity/brain/a12d00d9-6bf2-4af8-8638-6db4b8292d86/employee_management_usecase_diagram_1774405400930.png)

#### 1.2.3.1. Pre-condition
- Hệ thống đã sẵn sàng và Admin đã đăng nhập.
- Danh mục Khu vực và Phòng ban đã được khởi tạo.

#### 1.2.3.2. Post-condition
- Thông tin nhân viên và dữ liệu tài khoản liên quan được cập nhật thành công vào Database.
- Tệp tin hình ảnh được sao lưu vào thư mục hệ thống.

#### 1.2.3.3. Triggers
- Admin vào menu "Nhân viên" -> "Quản lý Nhân viên".

### 1.2.4. Workflows

**Scenario 1 — Xem và Lọc danh sách**
| Actor | System |
|-------|--------|
| 1. Chọn menu "Quản lý nhân viên" | 2. Lấy dữ liệu toàn bộ nhân viên và hiển thị lên GridView. Hiển thị các cột: Mã NV, Họ tên, SĐT... |
| 3. Chọn Chức vụ tại bộ lọc phía trên (Combobox) | 4. Tự động rút gọn danh sách theo chức vụ đã chọn. |

**Scenario 2 — Thêm nhân viên mới**
| Actor | System |
|-------|--------|
| 1. Nhấn nút "Thêm mới" | 2. Hệ thống tự động sinh Mã Code tiếp theo (ví dụ: NV015) và hiển thị lên form trống. |
| 3. Nhập dữ liệu và nhấn "Chọn ảnh" để tải file ảnh | 4. Hiển thị ảnh xem trước tại khung Avatar. |
| 5. Nhấn "Lưu" | 6. Kiểm tra hợp lệ dữ liệu. Nếu thành công: Copy ảnh vào `Images/NhanVien/`, ghi DB, thông báo "Thành công" và tải lại Grid. |

**Scenario 3 — Xử lý Xóa nhân viên**
| Actor | System |
|-------|--------|
| 1. Chọn nhân viên trên Grid -> Nhấn "Xóa" | 2. Hiển thị xác nhận xóa. |
| 3. Chấp nhận xóa | 4. Kiểm tra: Nếu nhân viên đã từng lập hóa đơn bán hàng -> Chặn xóa và báo lỗi (Yêu cầu đổi trạng thái sang Đã nghỉ). Nếu chưa có giao dịch -> Thực hiện xóa thực tế. |

### 1.2.5. External interfaces

#### 1.2.5.1. Prototype — Quản lý Nhân viên
```text
╔══════════════════════════════════════════════════════════════════════════════╗
║  QUẢN LÝ NHÂN SỰ                                                  [ _ ][ X ] ║
╠══════════════════════════════════════════════════════════════════════════════╣
║  Tìm kiếm: [____________________]  Chức vụ: [▼ Tất cả         ]  [🔍 Tìm kiếm]║
║                                                                              ║
║  ┌───────────────────────────────────────────────────┐ ┌──────────────────┐  ║
║  │ DANH SÁCH NHÂN VIÊN (Master Grid)                 │ │     HÌNH ẢNH     │  ║
║  │ ┌────┬──────────────┬──────────────┬────────────┐ │ │  ┌────────────┐  │  ║
║  │ │ Mã │ Họ Tên       │ SĐT          │ Chức vụ    │ │ │  │            │  │  ║
║  │ ├────┼──────────────┼──────────────┼────────────┤ │ │  │   AVATAR   │  │  ║
║  │ │NV01│ Nguyễn Văn A │ 0901234567   │ Thu ngân   │ │ │  │            │  │  ║
║  │ │NV02│ Trần Thị B   │ 0917888999   │ Kỹ thuật   │ │ │  └────────────┘  │  ║
║  │ └────┴──────────────┴──────────────┴────────────┘ │ │  [ 📷 Chọn ảnh ] │  ║
║  └───────────────────────────────────────────────────┘ └──────────────────┘  ║
║                                                                              ║
║  --- THÔNG TIN CHI TIẾT ---------------------------------------------------  ║
║  Mã ID:   [______] (Auto)     Họ Tên:   [________________________________]   ║
║  Mã Code: [NV001 ] (ReadOnly) CCCD:     [________________________________]   ║
║  Giới tính: [▼ Nam ]          Ngày sinh: [ 01/01/2000 | 📅 ]                ║
║  SĐT:     [______________]    Email:    [________________________________]   ║
║  Địa chỉ: [______________________________________________________________]   ║
║  Chức vụ: [▼ Thu ngân      ]  Khu vực:  [▼ Khu Mạo hiểm                  ]   ║
║  Ngày vào: [ 25/03/2026 |📅 ] Trạng thái: [▼ Đang làm việc                ]   ║
║                                                                              ║
║  [ + Thêm ]  [ ✏ Sửa ]  [ 🗑 Xóa ]  [ ↻ Làm mới ]  [ 💾 Lưu ]  [ 🚪 Thoát ]  ║
╚══════════════════════════════════════════════════════════════════════════════╝
```

#### 1.2.5.2. Screen description — Thuộc tính màn hình
| No | Field name | Control type | Required | Data type | Description |
|----|-----------|-------------|----------|-----------|-------------|
| 1 | Tìm kiếm | TextBox | No | Text | Tìm theo Tên hoặc Mã NV |
| 2 | Lọc Chức vụ | ComboBox | No | Text | Rút gọn danh sách theo chức vụ |
| 3 | DataGridView | DevExpress Grid | — | — | Hiển thị bảng dữ liệu nhân sự |
| 4 | Mã ID | TextBox | Yes | INT | Khóa chính tự động (ID nội bộ) |
| 5 | Mã Code | TextBox | Yes | Text | Mã hiển thị có đầu ngữ (NV001, NV002...) |
| 6 | Họ Tên | TextBox | Yes | Text | Tối đa 100 ký tự |
| 7 | Số CCCD | TextBox | Yes | Text | Tối đa 12 số |
| 8 | Chức vụ | ComboBox | Yes | Text | Thu ngân, Kỹ thuật, Quản lý... |
| 9 | Khu vực | ComboBox | No | INT | Khu vực nhân viên đang trực |
| 10 | Trạng thái | ComboBox | Yes | Text | Đang làm việc, Tạm nghỉ, Đã nghỉ |

### 1.2.6. Business descriptions
1. **Quản lý Mã NV**: Mã code không được phép chỉnh sửa thủ công và phải là duy nhất.
2. **Hình ảnh**: Ảnh nhân viên được đổi tên theo MaCode để quản lý tập trung, lưu đường dẫn tương đối trong DB.
3. **Quy tắc Xóa**: Chặn xóa vật lý nhân viên đã có dữ liệu ràng buộc tại bảng Hóa đơn bán vé.

### 1.2.7. User-validation rules
| No | Validation rule | Message | Code |
|----|----------------|---------|------|
| 1 | Họ tên bỏ trống | "Họ tên không được để trống!" | ERR_REQUIRED_NAME |
| 2 | Tên quá dài | "Họ tên quá dài (tối đa 100 ký tự)!" | ERR_NAME_LENGTH |
| 3 | Trùng Mã Code | "Mã nhân viên đã tồn tại!" | ERR_DUPLICATE_CODE |
| 4 | CCCD không đúng | "CCCD quá dài (tối đa 12 ký tự)!" | ERR_INVALID_CCCD |

### 1.2.8. Related use-cases
- Quản lý Khu vực (Lấy danh sách Khu vực trực)
- Bán vé (Liên kết nhân viên lập hóa đơn)

---

## 2. QUẢN LÝ KHÁCH HÀNG

### 2.2.1. Overview
Hệ thống cho phép lưu trữ và tra cứu thông tin khách hàng định danh. Hỗ trợ nhận diện khách hàng thân thiết để thực hiện các chương trình tích điểm thưởng và tri ân.

### 2.2.2. Actors
- Admin, Nhân viên bán vé

### 2.2.3. Use-case diagram
![Customer Management Use Case Diagram](C:/Users/ADMIN/.gemini/antigravity/brain/a12d00d9-6bf2-4af8-8638-6db4b8292d86/customer_management_usecase_diagram_repro_1774405474357.png)

#### 2.2.3.1. Pre-condition
- Nhân viên có quyền truy cập module bán hàng/khách hàng.

#### 2.2.3.2. Post-condition
- Thông tin khách hàng được ghi nhận. Hệ thống có thể sẵn sàng tích điểm cho các giao dịch sau đó.

#### 2.2.3.3. Triggers
- Nhân viên vào menu "Khách hàng" hoặc khi khách hàng mới mua vé cần đăng ký thành viên.

### 2.2.4. Workflows

**Scenario 1 — Tra cứu và nhận diện khách**
| Actor | System |
|-------|--------|
| 1. Nhập SĐT vào ô tìm kiếm | 2. Thực hiện truy vấn đồng thời trên Họ tên, SĐT và Mã thành viên. |
| 3. Chọn đúng khách hàng từ danh sách | 4. Hiển thị thông tin tổng hợp: Tổng chi tiêu, Điểm tích lũy hiện có. |

**Scenario 2 — Đăng ký khách hàng mới**
| Actor | System |
|-------|--------|
| 1. Nhấn "Thêm mới" | 2. Hiển thị form đăng ký. |
| 3. Nhập SĐT, Họ tên và các thông tin liên hệ | 4. Kiểm tra: SĐT phải duy nhất và đúng định dạng 10 số. |
| 5. Nhấn "Lưu" | 6. Khởi tạo MaKhachHang, gán các giá trị mặc định (Diem = 0) và lưu vào Database. |

### 2.2.5. External interfaces

#### 2.2.5.1. Prototype — Quản lý Khách hàng
(Tham khảo giao diện Master-Detail chuẩn của Sprint 2)

#### 2.2.5.2. Screen description — Thuộc tính màn hình
| No | Field name | Control type | Required | Data type | Description |
|----|-----------|-------------|----------|-----------|-------------|
| 1 | SĐT | TextBox | Yes | Number (10) | Định danh chính của khách hàng |
| 2 | Họ tên | TextBox | Yes | Text | Họ tên khách hàng (Max 100) |
| 3 | Email | TextBox | No | Text | Validate format @... |
| 4 | Tổng Chi Tiêu | NumberLabel | No | Decimal | Tổng tiền khách đã thanh toán qua các Sprint |
| 5 | Điểm | NumberLabel | No | INT | Số điểm tích lũy hiện tại |

### 2.2.6. Business descriptions
1. **SĐT**: Là trường dữ liệu duy nhất dùng để nhận diện nhanh khách hàng tại quầy vé.
2. **Quy tắc tích điểm**: Mặc định 100,000đ chi tiêu = 100 điểm tích lũy (Có thể thay đổi theo bảng cấu hình).

### 2.2.7. User-validation rules
| No | Validation rule | Message | Code |
|----|----------------|---------|------|
| 1 | SĐT không đúng định dạng | "Số điện thoại phải gồm 10 chữ số, bắt đầu bằng 0" | ERR_INVALID_PHONE |
| 2 | Trùng SĐT | "Số điện thoại đã được đăng ký bởi khách hàng khác" | ERR_DUPLICATE_PHONE |
| 3 | Email sai format | "Email không đúng định dạng" | ERR_INVALID_EMAIL |
| 4 | Ngày sinh tương lai | "Ngày sinh không hợp lệ" | ERR_FUTURE_DATE |

### 2.2.8. Related use-cases
- Bán vé (Sprint 2 - Tích điểm từ hóa đơn)
- Báo cáo khách hàng (Sprint 5)

---

## 3. QUẢN LÝ DANH MỤC DỊCH VỤ

### 3.2.1. Overview
Quản lý các loại dịch vụ tiện ích, sản phẩm F&B (Thức ăn, nước uống) và dịch vụ cho thuê trong công viên. Thiết lập bảng giá và phân bổ dịch vụ theo danh mục/khu vực.

### 3.2.2. Actors
- Admin, Quản lý quầy

### 3.2.3. Use-case diagram
![Service Management Use Case Diagram](C:/Users/ADMIN/.gemini/antigravity/brain/a12d00d9-6bf2-4af8-8638-6db4b8292d86/service_management_usecase_diagram_1774405337659.png)

#### 3.2.3.1. Pre-condition
- Danh sách Danh mục dịch vụ (Category) đã được cấu hình trong bảng `NH_DanhMuc`.

#### 3.2.3.2. Post-condition
- Dịch vụ sẵn sàng phục vụ và hiển thị tại màn hình Bán hàng (POS).

#### 3.2.3.3. Triggers
- Admin vào menu "Dịch vụ" -> "Quản lý Dịch vụ".

### 3.2.4. Workflows

**Scenario 1 — Cập nhật bảng giá**
| Actor | System |
|-------|--------|
| 1. Chọn món ăn/dịch vụ trên Grid | 2. Hiển thị thông tin lên panel chi tiết. |
| 3. Thay đổi giá bán tại ô Giá | 4. Kiểm tra: Giá phải là số và >= 0. |
| 5. Nhấn "Lưu" | 6. Ghi nhận thời điểm thay đổi và cập nhật bảng giá. |

### 3.2.5. External interfaces

#### 3.2.5.1. Prototype — Quản lý Dịch vụ
(Sử dụng giao diện chuẩn F&B Dashboard)

#### 3.2.5.2. Screen description — Thuộc tính màn hình
| No | Field name | Control type | Required | Data type | Description |
|----|-----------|-------------|----------|-----------|-------------|
| 1 | Tên Dịch Vụ | TextBox | Yes | Text | Tối đa 150 ký tự |
| 2 | Đơn giá | TextBox (Numeric) | Yes | Decimal | Harga bán niêm yết |
| 3 | Danh mục | ComboBox | Yes | ID | Đồ ăn, Nước uống, Dịch vụ thuê... |
| 4 | Khu vực | ComboBox | No | ID | NULL = Toàn khu, Hoặc chọn khu vực cụ thể |
| 5 | Tồn kho | TextBox | No | INT | Sử dụng cho các mặt hàng đếm được (gift/drink) |

### 3.2.6. Business descriptions
1. **Giá áp dụng**: Hệ thống cho phép cấu hình giá thay đổi theo thời điểm (ví dụ: Cuối tuần/Ngày lễ).
2. **Khu vực phục vụ**: Một số dịch vụ chỉ được bán tại khu vực cụ thể (ví dụ: Đồ bơi chỉ bán tại Khu Nước).

### 3.2.7. User-validation rules
| No | Validation rule | Message | Code |
|----|----------------|---------|------|
| 1 | Tên dịch vụ rỗng | "Tên dịch vụ không được để trống!" | ERR_REQUIRED_SERVICE_NAME |
| 2 | Giá bán âm | "Giá bán không hợp lệ!" | ERR_INVALID_PRICE |
| 3 | Chưa chọn danh mục | "Vui lòng chọn danh mục!" | ERR_REQUIRED_CATEGORY |

### 3.2.8. Related use-cases
- Quản lý Khu vực
- Bán vé & Thanh toán (Sprint 2)

---

## 4. PHỤ LỤC

(Bổ sung các cập nhật Database Schema và Message List chi tiết như đã trình bày trong các bản thảo trước đó)
