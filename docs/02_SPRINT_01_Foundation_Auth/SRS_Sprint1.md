# Software Requirement Specification

**Project Name:** Hệ thống Quản lý Công viên Giải trí (Amusement Park Management)
**Project Code:** APM
**Document Code:** APM_SRS_Sprint1_v1.0
**Location/Date:** Ho Chi Minh, March 2026

---

## RECORD OF CHANGE
*A - Added M - Modified D - Deleted*

| Effective Date | Changed Items | A / M / D | Change Description | New Version |
|---|---|---|---|---|
| 04-Mar-26 | First release | A | Khởi tạo tài liệu SRS cho Sprint 1 | 1.0 |
| 04-Apr-26 | Screen Details | M | Cập nhật chính xác các field vật lý từ mã nguồn C# | 1.1 |

---

## SIGNATURE PAGE
| Role | Name | Date |
|---|---|---|
| **ORIGINATOR** | [Thành viên 1] | 04-Mar-26 |
| **REVIEWERS** | [Thành viên 2], [Thành viên 3] | |
| **APPROVAL** | | |

---

## TABLE OF CONTENT
1. PRODUCT MANAGEMENT (Sản Phẩm & Giá)
2. AREA & GAME MANAGEMENT (Khu Vực & Trò Chơi)
3. EMPLOYEE MANAGEMENT (Nhân Sự & Quyền)
4. OTHERS REQUIREMENT
   4.1. Data Format
   4.2. Message List

---

## 1. PRODUCT MANAGEMENT (Phân hệ Sản Phẩm)
Phân hệ quản lý toàn bộ sản phẩm bán ra của công viên (Vé, F&B, Lưu niệm) làm đầu vào cho giao diện Bán hàng POS (Sprint 2).

### 1.1. Overview
Màn hình này (`frmSanPham`) cho phép Admin tạo mới, cập nhật, xóa và tìm kiếm các thông tin liên quan đến Sản phẩm. Nó phân biệt rõ ràng giữa Sản phẩm thông thường và "Vé điện tử" thông qua tham số số lượt chơi và trò chơi liên kết.

### 1.2. Actors
- Administrator (Admin)
- Quản lý (Manager)

### 1.3. Use-case diagrams
`[User] ---> (Product Management)`

#### 1.3.1. Pre-condition
User must be logged in with `MANAGE_PRODUCT` permission.

#### 1.3.2. Post-condition
New product was added/updated into the database, optionally inserting barcode data.

#### 1.3.3. Triggers
Go to menu "Danh Mục" từ màn hình chính -> Click "Quản lý Sản Phẩm".

### 1.4. Workflows

| Scenario | Actor | System |
|---|---|---|
| **1. Insert New Product** | 1.1. Go to this screen.<br>1.3. Click "Làm mới" to empty fields.<br>1.5. Fill logical fields (Tên SP, Đơn Giá, Loại) and click "Thêm mới". | 1.2. Display current Product list on DataGrid.<br>1.4. Clear details inputs.<br>1.6. Validate mandatory elements. Insert logical Product into DB and trigger grid refresh. Show success message. |
| **2. Edit Information** | 2.1. Select a Product row on grid.<br>2.3. Modify values (Trạng thái or Đơn giá).<br>2.4. Click "Cập nhật". | 2.2. Load specific attributes into the right-side detail panels.<br>2.5. Commit transaction and perform full refresh on grid. |

### 1.5. External interfaces
#### 1.5.1. Prototype 
*(File: `GUI/Catalog/frmSanPham.Designer.cs`)*

#### 1.5.2. Screen description
Màn hình được chia làm 2 phần chính: Danh sách (trái) và Thống tin chi tiết (phải).

| No | Field | Control type | Required | Default value | Data type | Description |
|---|---|---|---|---|---|---|
| **Search/Filter Section** |
| 1 | txtTimKiem | Guna2TextBox | No | Blank | Text | Từ khóa tìm kiếm |
| 2 | slkLocLoai | SearchLookUpEdit | No | Blank | Text | Lọc lưới theo Loại Sản Phẩm |
| **Product Attributes (gbModuleThongTin)** |
| 3 | cboLoaiSP | Guna2ComboBox | Yes | Trống | Text | Loại Sản Phẩm (Ví dụ: Vé, F&B, Qùa) |
| 4 | txtTenSP | Guna2TextBox | Yes | Blank | Text | Tên sản phẩm |
| 5 | txtDonGia | Guna2TextBox | Yes | 0 | Decimal | Giá bán hiện hành |
| 6 | slkDonVi | SearchLookUpEdit | Yes | Blank | Text | Đơn vị tính (VD: Vé, Cái, Phần) |
| 7 | slkKhuVuc | SearchLookUpEdit | No | Blank | Text | Khu vực phục vụ vật lý nếu có |
| 8 | cboTrangThai | Guna2ComboBox | Yes | Hoạt động | Text | Hoạt động / Ngừng bán |
| 9 | txtMoTa | Guna2TextBox | No | Blank | Text | Mô tả thêm |
| 10 | picHinhAnh | PictureBox | No | Default_img | Image | Hình ảnh sản phẩm đưa lên POS |
| **Electronic Ticket Specifics (pnlVeInfo)** |
| 11 | slkTroChoi | SearchLookUpEdit | No | (Vé cổng) | Text | Trò chơi liên kết cho vé |
| 12 | spnSoLuot | SpinEdit | Yes | 1 | Integer | Số lượt quẹt / vé (Max=100) |
| **Actions** |
| 13 | btnThem/Sua/Xoa | Guna2Button | N/A | N/A | N/A | Các chức năng CRUD |
| 14 | btnTaoMaVach | Guna2Button | N/A | N/A | N/A | Generator cho Barcode vé |

### 1.6. Business descriptions
| No | Name | Business Rules |
|---|---|---|
| 1 | Ticket Polymorphism | Nhóm "Vé điện tử" (`pnlVeInfo`) chỉ hiển thị hoặc được valid nếu `cboLoaiSP` được chọn là loại "Vé". |
| 2 | QR Generation | Khi click `btnTaoMaVach`, mã Code sẽ tự động sinh và lưu dựa trên Tên Sản Phẩm và Timestamp làm định danh duy nhất. |

---

## 2. AREA & GAME MANAGEMENT (Khu Vực & Trò Chơi)

### 2.1. Area Management (frmKhuVuc)
Phân chia sơ đồ công viên thành các khu vực quản lý vật lý.

#### 2.1.1. Screen description
| No | Field | Control type | Required | Default value | Data type | Description |
|---|---|---|---|---|---|---|
| 1 | txtMaKV | Guna2TextBox | Yes | Auto | Text | Mã KV (Read-only) |
| 2 | txtTenKV | Guna2TextBox | Yes | Blank | Text | Tên hiển thị khu vực |
| 3 | txtMaCode | Guna2TextBox | Yes | Auto | Text | Mã Barcode định danh (Read-only) |
| 4 | cboTrangThai | Guna2ComboBox | Yes | Mở cửa | Text | Trạng thái khu vực vận hành |
| 5 | txtMoTa | Guna2TextBox | No | Blank | Text | Mô tả thêm |
| 6 | dtpNgayTao | DateEdit | Yes | Now | Date | System generated data field |
| 7 | dtpNgayCapNhat | DateEdit | Yes | Now | Date | System generated data field |

### 2.2. Game Management (frmTroChoi)
Tạo tiểu mục các thiết bị trò chơi trực thuộc trong các khu vực.

#### 2.2.1. Screen description
| No | Field | Control type | Required | Default value | Data type | Description |
|---|---|---|---|---|---|---|
| 1 | txtTenTroChoi | Guna2TextBox | Yes | Blank | Text | Tên thiết bị hoặc trò chơi |
| 2 | slkKhuVuc | SearchLookUpEdit| Yes | Blank | Text | Tên Khu vực vật lý chứa game này |
| 3 | cboTrangThai | Guna2ComboBox | Yes | Mở cửa | Text | Hoạt động / Bảo trì |
| 4 | txtMoTa | Guna2TextBox | No | Blank | Text | Ghi chú vận hành, sức chứa |

---

## 3. EMPLOYEE MANAGEMENT (Hồ Sơ Nhân Sự)

### 3.1. Overview (`frmNhanVien`)
Màn hình quản lý hồ sơ nhân sự phân tách 2 luồng dữ liệu: Thông tin cá nhân (Hồ sơ) và Thông tin xác thực (Tài khoản & Phân Quyền).

### 3.2. Screen description
Màn hình sử dụng `Guna2TabControl` phân 2 tab cấu trúc:

| No | Field | Control type | Required | Default value | Data type | Description |
|---|---|---|---|---|---|---|
| **Tab 1: Thông tin cá nhân (tabPagePersonalInfo)** |
| 1 | txtMaCode | Guna2TextBox | Yes | Auto | Text | Mã nhân viên (Read-only) |
| 2 | txtHoTen | Guna2TextBox | Yes | Blank | Text | Tên nhân viên |
| 3 | cboGioiTinh | Guna2ComboBox | Yes | Nam | Text | Giới tính |
| 4 | dtpNgaySinh | DateEdit | Yes | Now() | Date | Ngày sinh |
| 5 | txtCCCD | Guna2TextBox | Yes | Blank | Numeric | Chứng minh thư / Thẻ CCCD |
| 6 | txtSDT | Guna2TextBox | Yes | Blank | Numeric | Số điện thoại |
| 7 | txtEmail | Guna2TextBox | No | Blank | Text | Địa chỉ thư điện tử |
| 8 | txtDiaChi | Guna2TextBox | No | Blank | Text | Địa chỉ thường trú |
| 9 | slkChucVu | SearchLookUpEdit| Yes | Blank | Text | Title/Chức danh công việc |
| 10| slkKhuVuc | SearchLookUpEdit| Yes | Blank | Text | Nơi công tác điều động |
| 11| dtpNgayVaoLam| DateEdit | Yes | Now() | Date | |
| 12| cboTrangThai | Guna2ComboBox | Yes | Đi làm | Text | Đi làm / Nghỉ việc |
| **Tab 2: Tài khoản & Vai trò (tabPageAccount)** |
| 13| txtUsername | Guna2TextBox | Yes | Auto | Text | Tên đăng nhập (Read-only / Auto-gen) |
| 14| txtPassword | Guna2TextBox | Yes | Blank | Text | Mật khẩu xác thực đăng nhập |
| 15| slkAccountRole| SearchLookUpEdit| Yes | Blank | Text | Role map trong hệ thống RBAC |

---

## 4. OTHERS REQUIREMENT

### 4.1. Data Format
- **Date/Time Format**: `dd/MM/yyyy` (For example: 29/05/2026).
- **Number Format**: Phân cách hàng ngàn dùng dấu phẩy `,` (For example: `150,000`).
- **Currency**: `đ` or `VNĐ`. No decimal places for VNĐ.
- **Passwords**: Encrypted/Hashed on save. Giao diện masked (`●`).

### 4.2. Message List
| Message Code | EN / VN Content |
|---|---|
| ERR_REQUIRED_FIELD | "Vui lòng nhập đầy đủ các trường bắt buộc" |
| ERR_FORMAT_PRICE | "Đơn giá không hợp lệ (Phải là số >= 0)" (Price format is invalid) |
| ERR_REQUIRED_AREA | "Khu vực không được bỏ trống" (Area is required) |
| ERR_CONSTRAINT_DELETE_HAS_TRANS | "Không thể xóa do dữ liệu đã phát sinh giao dịch!" (Cannot delete item associated with active transactions) |
| MSG_SAVE_SUCCESS | "Lưu dữ liệu thành công!" (Data saved successfully) |
| MSG_DELETE_CONFIRM | "Bạn có chắc chắn muốn xóa bản ghi này?" (Are you sure you want to delete this record?) |
