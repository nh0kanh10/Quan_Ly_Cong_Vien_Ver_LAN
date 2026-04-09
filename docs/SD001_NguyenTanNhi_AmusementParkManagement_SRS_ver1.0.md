# SOFTWARE REQUIREMENT SPECIFICATION (SRS) – Sprint 1

**Mã dự án**: SD001  
**Mã tài liệu**: SD001_SRS_Sprint1_v1.0  
**Nhóm**: NguyenTanNhi  
**Đề tài**: Hệ thống Quản lý Công viên Vui chơi Giải trí  
**Phiên bản**: 1.0  
**Ngày tạo**: 04/03/2026  
**Địa điểm**: TP. Hồ Chí Minh  

---

## RECORD OF CHANGE

*A - Added, M - Modified, D - Deleted*

| Effective Date | Changed Items | A/M/D | Change Description | New Version |
|----------------|--------------|-------|-------------------|-------------|
| 04/03/2026 | First release | A | Khởi tạo SRS Sprint 1 | 1.0 |
| 15/03/2026 | Date Handling & UI | M | Quy định NgayTao/NgayCapNhat ReadOnly; NgayCapNhat mặc định 01/01/1753 | 1.1 |
| 15/03/2026 | Screen Description Sync | M | Đồng bộ Screen Description với code thực tế: sửa control type (NumericUpDown → TextBox), bổ sung controls mới (Lọc, Combo detail), thêm validation rules cho TextBox numeric | 1.2 |

---

## SIGNATURE PAGE

| Vai trò | Họ tên | Ngày |
|---------|--------|------|
| **ORIGINATOR** | [Thành viên 1] | 04/03/2026 |
| **REVIEWERS** | [Thành viên 2], [Thành viên 3] | |
| **APPROVAL** | | |

---

## TABLE OF CONTENT

1. [QUẢN LÝ LOẠI VÉ](#1-quản-lý-loại-vé)
   - 1.1. Overview
   - 1.2. Actors
   - 1.3. Use-case diagram
   - 1.4. Workflows
   - 1.5. External interfaces
   - 1.6. Business descriptions
   - 1.7. User-validation rules
2. [QUẢN LÝ KHU VỰC & TRÒ CHƠI](#2-quản-lý-khu-vực--trò-chơi)
   - 2.1. Quản lý Khu vực
   - 2.2. Quản lý Trò chơi
3. [YÊU CẦU KHÁC](#3-yêu-cầu-khác)
   - 3.1. Header / Menu chính
   - 3.2. Định dạng dữ liệu
   - 3.3. Database Schema
   - 3.4. API Endpoints
   - 3.5. Message List

---

## 1. QUẢN LÝ LOẠI VÉ

### 1.1. Overview

Cho phép Admin quản lý danh sách các loại vé trong công viên (ví dụ: Vé vào cổng, Vé trọn gói, Vé trẻ em...). Bao gồm: Thêm, Sửa, Xóa, Tìm kiếm các loại vé cùng bảng giá tương ứng.

### 1.2. Actors

- Admin

### 1.3. Use-case diagram

```
                    ┌───────────────────┐
              ┌─────┤Manage Ticket Types├─────┐
              │     └───────────────────┘     │
        ┌─────┴────┐                    ┌─────┴────┐
        │Add Ticket│                    │EditTicket│
        └──────────┘                    └──────────┘
              │                               │
        ┌─────┴─────┐                  ┌──────┴──────┐
        │Delete     │                  │ Search      │
        └───────────┘                  └─────────────┘
                        │
                   ┌────┴────┐
                   │  Admin  │
                   └─────────┘
```

#### 1.3.1. Pre-condition
- Hệ thống đã sẵn sàng.

#### 1.3.2. Post-condition
- Thông tin loại vé và giá vé được cập nhật thành công vào database.

#### 1.3.3. Triggers
- Vào menu "Danh mục" -> "Quản lý Loại vé".

### 1.4. Workflows

**Scenario 1 — Xem danh sách loại vé**

| Actor | System |
|-------|--------|
| 1. Chọn menu "Quản lý loại vé" | 2. Lấy dữ liệu danh sách loại vé và hiển thị lên màn hình. Các thông tin hiển thị gồm: Mã Loại Vé, Tên loại vé, Giá thường, Giá cuối tuần, Đối tượng, Là Combo, Trạng thái. |

**Scenario 2 — Thêm loại vé mới**

| Actor | System |
|-------|--------|
| 1. Nhấn nút "Thêm mới" | 2. Hiển thị form thêm loại vé với các trường thông tin trống. |
| 3. Nhập thông tin: Tên loại vé, Giá vé, Đối tượng... (Nếu là vé Combo thì chọn thêm các dịch vụ/vé con) → Nhấn "Lưu" | 4. Kiểm tra tính hợp lệ của dữ liệu. Nếu hợp lệ, lưu thông tin vào hệ thống. Hiển thị thông báo "Lưu thành công" và làm mới lại danh sách loại vé. |

**Scenario 3 — Sửa loại vé**

| Actor | System |
|-------|--------|
| 1. Chọn loại vé trong danh sách → Nhấn "Sửa" | 2. Hiển thị chi tiết dữ liệu hiện tại của loại vé đó lên Form. |
| 3. Chỉnh sửa thông tin loại vé/combo → Nhấn "Lưu" | 4. Kiểm tra tính hợp lệ của dữ liệu. Nếu hợp lệ, cập nhật thông tin mới vào hệ thống. Hiển thị thông báo "Lưu thành công" và làm mới lại danh sách loại vé. |

**Scenario 4 — Xóa loại vé**

| Actor | System |
|-------|--------|
| 1. Chọn loại vé trong danh sách → Nhấn "Xóa" | 2. Hiển thị hộp thoại xác nhận: "Bạn có chắc muốn xóa loại vé này?" |
| 3. Nhấn "Có" | 4. Kiểm tra vé đã từng được bán chưa. Nếu chưa bán, tiến hành xóa vé khỏi hệ thống, báo "Xóa thành công" và làm mới danh sách. Nếu vé đã có hóa đơn bán ra, chặn thao tác và báo lỗi: Hệ thống không cho phép xóa vé đã phát sinh giao dịch lưu trữ. |

**Scenario 5 — Tìm kiếm và Lọc**

| Actor | System |
|-------|--------|
| 1. Nhập từ khóa vào ô tìm kiếm và/hoặc chọn bộ lọc (Đối tượng) → Nhấn "Tìm" | 2. Tìm kiếm và lọc dữ liệu khớp với điều kiện người dùng đưa ra. Cập nhật và hiển thị danh sách kết quả tương ứng lên màn hình. |

### 1.5. External interfaces

#### 1.5.1. Prototype — Quản lý Loại Vé (Master-Detail)

```text
╔════════════════════════════════════════════════════════════════════════╗
║  QUẢN LÝ LOẠI VÉ VÀ COMBO                                              ║
║  Tìm kiếm: [________________________] [🔍 Tìm]                         ║
║                                                                        ║
║  ┌────┬────────────────────┬──────────────┬──────────────┬──────────┐  ║
║  │ Mã │ Tên Loại Vé        │ Giá Tiền(VNĐ)│ G.Cuối Tuần  │ Là Combo │  ║
║  ├────┼────────────────────┼──────────────┼──────────────┼──────────┤  ║
║  │ T01│ Vé vào cổng (Lớn)  │ 150,000      │ 200,000      │ [ ]      │  ║
║  │ T02│ Combo Trọn gói VIP │ 450,000      │ 500,000      │ [x]      │  ║
║  └────┴────────────────────┴──────────────┴──────────────┴──────────┘  ║
║                                                                        ║
║  --- THÔNG TIN CHI TIẾT ---------------------------------------------  ║
║  Mã loại vé:    [______] (Tự động)      Trạng thái: [▼ Hoạt động  ]    ║
║  Tên loại vé:   [____________________]  Đối tượng:  [▼ Người lớn  ]    ║
║  Giá ngày TV:   [___________] VNĐ       Giá C.Tuần: [___________] VNĐ  ║
║                                                                        ║
║  [x] LÀ VÉ COMBO (Bao gồm các dịch vụ bên dưới)                        ║
║  ┌──────────────────────────────────────────────────────────────────┐  ║
║  │ 1. Vé vào cổng (Lớn) - Số lượng: 1                               │  ║
║  │ 2. Trò cáp treo      - Số lượng: Không giới hạn                  │  ║
║  └──────────────────────────────────────────────────────────────────┘  ║
║                                                                        ║
║  [ + Thêm mới ]    [ ✏ Sửa ]    [ 🗑 Xóa ]    [ 💾 Lưu ]    [ ↻ Làm mới ]  ║
╚════════════════════════════════════════════════════════════════════════╝
```

#### 1.5.2. Screen description — Thuộc tính màn hình

| No | Field name | Control type | Required | Data type | Default value | Description |
|----|-----------|-------------|----------|-----------|---------------|-------------|
| 1 | Tìm kiếm | TextBox | No | Text | Blank | Tìm theo tên loại vé |
| 2 | Lọc Đối tượng | ComboBox | No | Text | "Tất cả" | Lọc danh sách theo đối tượng (Tất cả, Người lớn, Trẻ em) |
| 3 | Tìm | Button | — | N/A | N/A | Thực hiện tìm kiếm |
| 4 | DataGridView Lưới | DataGridView | — | — | — | Hiển thị danh sách loại vé (Master) |
| 5 | Mã loại vé | TextBox (ReadOnly) | Yes | Text | Auto | Mã tự động (ID) |
| 6 | Mã Code | TextBox (ReadOnly) | Yes | Text | Blank | Mã hiển thị (T01, T02...) |
| 7 | Tên loại vé | TextBox | Yes | Text | Blank | Tên loại vé, tối đa 100 ký tự |
| 8 | Giá ngày thường | TextBox | Yes | Number | 0 | Giá vé (≥ 0 đ). Chỉ cho phép nhập số, validate phía code |
| 9 | Giá cuối tuần | TextBox | No | Number | 0 | Giá vé T7, CN. Chỉ cho phép nhập số, validate phía code |
| 10 | Đối tượng | ComboBox | Yes | Text | Người lớn | Gồm: Người lớn, Trẻ em, Tất cả |
| 11 | Trạng thái | ComboBox | Yes | Text | Hoạt động | Trạng thái sử dụng |
| 12 | Là Vé Combo | ToggleSwitch | No | Bool | False | Bật/tắt để hiện lưới nhập vé con |
| 13 | Lưới ChiTietCombo | DataGridView | No | — | — | Chỉ Enable khi "Là Combo" = True |
| 14 | Chọn vé con | ComboBox | No | Text | — | Chọn loại vé con để thêm vào combo (chỉ hiện khi Là Combo = True) |
| 15 | Số lượt | NumericUpDown | No | Number | 1 | Số lượt chơi của vé con trong combo |
| 16 | Nút Thêm/Xóa vé con | Buttons | — | N/A | N/A | Thêm hoặc xóa vé con khỏi combo |
| 17 | Ngày tạo | DatePicker (Disabled) | No | Date | Today | Ngày tạo bản ghi (Không cho sửa) |
| 18 | Ngày cập nhật | DatePicker (Disabled) | No | Date | 01/01/1753 | Ngày cập nhật cuối cùng. Mặc định 01/01/1753 nếu chưa bao giờ cập nhật|
| 19 | Các nút thao tác | Buttons | — | N/A | N/A | Thêm, Sửa, Xóa, Lưu, Làm mới |

### 1.6. Business descriptions

| No | Name | Description |
|----|------|-------------|
| 1 | Giá vé | Phải lớn hơn hoặc bằng 0 đ. |
| 2 | Ràng buộc xóa | Chỉ cho phép xóa nếu vé này chưa từng phát sinh giao dịch bán hàng. |

### 1.7. User-validation rules

| No | Validation rule | Message | Code |
|----|----------------|---------|------|
| 1 | Tên bỏ trống | "Vui lòng nhập tên loại vé" | ERR_REQUIRED_TICKET_NAME |
| 2 | Giá vé âm | "Giá vé không hợp lệ" | ERR_INVALID_PRICE |
| 3 | Trùng loại vé | "Tên loại vé đã tồn tại" | ERR_DUPLICATE_TICKET |
| 4 | Giá vé không phải số | "Giá vé phải là số hợp lệ" | ERR_PRICE_NOT_NUMBER |
| 5 | Giá cuối tuần không phải số | "Giá cuối tuần phải là số hợp lệ" | ERR_WEEKEND_PRICE_NOT_NUMBER |

### 1.8. Related use-cases
- Quản lý Khu vực & Trò chơi
- Bán vé tại quầy (Sprint 2)

## 2. QUẢN LÝ KHU VỰC & TRÒ CHƠI

### 2.1. Quản lý Khu vực (Area Management)

#### 2.1.1. Overview

Cho phép Admin quản lý danh sách các khu vực trong công viên (ví dụ: Khu Trò chơi Mạo hiểm, Khu Nước, Khu Trẻ em, Khu Văn hóa...). Bao gồm: Thêm, Sửa, Xóa, Tìm kiếm.

#### 2.1.2. Actors

- Admin

#### 2.1.3. Use-case diagram

```
                    ┌───────────────────┐
              ┌─────┤  Manage Areas     ├─────┐
              │     └───────────────────┘     │
        ┌─────┴────┐                    ┌─────┴────┐
        │ Add Area │                    │Edit Area │
        └──────────┘                    └──────────┘
              │                               │
        ┌─────┴─────┐                  ┌──────┴──────┐
        │Delete Area│                  │ Search Area │
        └───────────┘                  └─────────────┘
                        │
                   ┌────┴────┐
                   │  Admin  │
                   └─────────┘
```

##### 2.1.3.1. Pre-condition

- Admin đã đăng nhập thành công.

##### 2.1.3.2. Post-condition

- Danh sách khu vực được thêm/sửa/xóa thành công trong database.

##### 2.1.3.3. Triggers

- Admin chọn menu "Quản lý khu vực" từ thanh menu chính.

#### 2.1.4. Workflows

**Scenario 1 — Xem danh sách khu vực**

| Actor | System |
|-------|--------|
| 1. Chọn menu "Quản lý khu vực" | 2. Lấy dữ liệu danh sách các khu vực hiện có. Hiển thị trên màn hình (dạng lưới) với các thông tin: Mã KV, Tên khu vực, Mô tả, Trạng thái, Số lượng trò chơi. |

**Scenario 2 — Thêm khu vực mới**

| Actor | System |
|-------|--------|
| 1. Nhấn nút "Thêm mới" | 2. Hiển thị form nhập liệu khu vực mới với các trường thông tin trống. |
| 3. Nhập thông tin: Tên khu vực, Mô tả, Trạng thái → Nhấn "Lưu" | 4. Kiểm tra tính hợp lệ của dữ liệu. Nếu hợp lệ, lưu khu vực mới vào hệ thống. Hiển thị thông báo "Thêm khu vực thành công" và làm mới danh sách hiển thị. |

**Scenario 3 — Sửa khu vực**

| Actor | System |
|-------|--------|
| 1. Chọn khu vực trong danh sách → Nhấn "Sửa" | 2. Hiển thị dữ liệu hiện tại của khu vực được chọn lên form nhập liệu. |
| 3. Chỉnh sửa thông tin → Nhấn "Lưu" | 4. Kiểm tra tính hợp lệ của dữ liệu. Nếu hợp lệ, cập nhật thông tin đã sửa vào hệ thống. Hiển thị thông báo "Cập nhật thành công" và làm mới danh sách. |

**Scenario 4 — Xóa khu vực**

| Actor | System |
|-------|--------|
| 1. Chọn khu vực trong danh sách → Nhấn "Xóa" | 2. Hiển thị hộp thoại yêu cầu xác nhận: "Bạn có chắc muốn xóa khu vực [Tên]?" |
| 3. Nhấn "Có" | 4. Hệ thống kiểm tra khu vực này có đang chứa trò chơi nào không. Nếu khu vực trống (không có trò chơi), tiến hành xóa khỏi hệ thống và trả về kết quả "Xóa thành công", làm mới danh sách. Nếu khu vực đang chứa trò chơi, chặn thao tác và thông báo: "Không thể xóa. Khu vực đang có trò chơi được phân bổ." |

**Scenario 5 — Tìm kiếm**

| Actor | System |
|-------|--------|
| 1. Nhập từ khóa vào ô tìm kiếm → Nhấn "Tìm" | 2. Tìm kiếm danh sách các khu vực có tên chứa từ khóa tương ứng. Hiển thị kết quả tìm được lên danh sách. |

#### 2.1.5. External interfaces

##### 2.1.5.1. Prototype — Quản lý Khu vực (Master-Detail)

```text
╔═══════════════════════════════════════════════════════════════════════════╗
║  QUẢN LÝ KHU VỰC                                                          ║
║  Tìm kiếm: [________________________] [🔍 Tìm]                            ║
║                                                                           ║
║  ┌──────────────────────────────────────────┐ ┌─────────────────────────┐ ║
║  │ LƯỚI DANH SÁCH (MASTER)                  │ │ THÔNG TIN CHI TIẾT      │ ║
║  │ ┌────┬──────────────┬──────────┬───────┐ │ │                         │ ║
║  │ │ Mã │ Tên khu vực  │Trạng thái│ Số TC │ │ │ Mã KV: [_____]          │ ║
║  │ ├────┼──────────────┼──────────┼───────┤ │ │ Tên:   [______________] │ ║
║  │ │ A01│ Khu Mạo hiểm │Hoạt động │ 5     │ │ │ Trạng thái:[▼ Hoạt động]│ ║
║  │ │ A02│ Khu Nước     │Hoạt động │ 3     │ │ │                         │ ║
║  │ └────┴──────────────┴──────────┴───────┘ │ │ Mô tả:                  │ ║
║  │                                          │ │ ┌─────────────────────┐ │ ║
║  │                                          │ │ │                     │ │ ║
║  │                                          │ │ └─────────────────────┘ │ ║
║  └──────────────────────────────────────────┘ └─────────────────────────┘ ║
║                                                                           ║
║  [ + Thêm mới ]    [ ✏ Sửa ]    [ 🗑 Xóa ]    [ 💾 Lưu ]    [ ↻ Làm mới ]║
╚═══════════════════════════════════════════════════════════════════════════╝
```

#### 2.1.5.2. Screen description — Thuộc tính màn hình

| No | Field name | Control type | Required | Data type | Default value | Description |
|----|-----------|-------------|----------|-----------|---------------|-------------|
| 1 | Tìm kiếm | TextBox | No | Text | Blank | Tìm theo tên khu vực |
| 2 | Lọc Trạng thái | ComboBox | No | Text | "Tất cả" | Lọc danh sách theo trạng thái (Tất cả, Mở cửa, Bảo trì, Đóng cửa) |
| 3 | Tìm | Button | — | N/A | N/A | Thực hiện tìm kiếm |
| 4 | DataGridView KhuVuc | DataGridView | — | — | — | Hiển thị danh sách phía trái |
| 5 | Mã khu vực | TextBox (ReadOnly) | Yes | Text | Auto | Tự động sinh khi thêm mới. |
| 6 | Tên khu vực | TextBox | Yes | Text | Blank | Tên khu vực, tối đa 100 ký tự |
| 7 | Mô tả | TextBox (Multiline) | No | Text | Blank | Mô tả chi tiết khu vực |
| 8 | Trạng thái | ComboBox | Yes | Text | "Mở cửa" | Mở cửa, Bảo trì, Đóng cửa |
| 9 | Mã Code | TextBox (ReadOnly) | Yes | Text | Blank | Mã hiển thị (A01, A02...) |
| 10 | Ngày tạo | DatePicker (Disabled) | No | Date | Today | Ngày tạo khu vực (ReadOnly) |
| 11 | Ngày cập nhật | DatePicker (Disabled) | No | Date | 01/01/1753 | Ngày cập nhật. Mặc định 01/01/1753 nếu rỗng (ReadOnly) |
| 12 | Các nút thao tác | Buttons | — | N/A | N/A | Thêm, Sửa, Xóa, Lưu, Làm mới |

#### 2.1.6. Business descriptions

| No | Name | Description |
|----|------|-------------|
| 1 | Mã khu vực | Tự động tạo theo format: A + số thứ tự 2 chữ số (A01, A02, ...). Không cho phép trùng. |
| 2 | Tên khu vực | Không được trùng với khu vực đã có. Hệ thống tự trim khoảng trắng. |
| 3 | Trạng thái | Mặc định "Mở cửa". Khi chuyển sang "Đóng cửa", tất cả trò chơi trong khu vực cũng tự động ngừng phục vụ. |
| 4 | Xóa | Chỉ cho phép xóa khu vực không có trò chơi nào (soft delete recommended). |
| 5 | Tìm kiếm | Tìm theo tên khu vực, không phân biệt hoa thường. |

#### 2.1.7. User-validation rules

| No | Validation rule | Message | Code |
|----|----------------|---------|------|
| 1 | Tên khu vực bỏ trống | "Vui lòng nhập tên khu vực" | ERR_REQUIRED_AREA_NAME |
| 2 | Tên khu vực trùng | "Tên khu vực đã tồn tại" | ERR_DUPLICATE_AREA_NAME |
| 3 | Tên khu vực > 100 ký tự | "Tên khu vực không được vượt quá 100 ký tự" | ERR_AREA_NAME_MAX_LENGTH |
| 4 | Mô tả > 500 ký tự | "Mô tả không được vượt quá 500 ký tự" | ERR_AREA_DESC_MAX_LENGTH |
| 5 | Xóa khu vực có trò chơi | "Không thể xóa. Khu vực đang có trò chơi." | ERR_AREA_HAS_GAMES |

#### 2.1.8. Related use-cases

- Quản lý Trò chơi
- Main Form

---

### 2.2. Quản lý Trò chơi (Game/Ride Management)

#### 2.2.1. Overview

Cho phép Admin quản lý danh sách trò chơi/thiết bị giải trí trong từng khu vực (ví dụ: Tàu lượn siêu tốc, Đu quay, Nhà ma...). Bao gồm: Thêm, Sửa, Xóa, Tìm kiếm.

#### 2.2.2. Actors

- Admin

#### 2.2.3. Use-case diagram

```
                    ┌───────────────────┐
              ┌─────┤  Manage Games     ├─────┐
              │     └───────────────────┘     │
        ┌─────┴────┐                    ┌─────┴────┐
        │ Add Game │                    │ Edit Game│
        └──────────┘                    └──────────┘
              │                               │
        ┌─────┴─────┐                  ┌──────┴──────┐
        │Delete Game│                  │ Search Game │
        └───────────┘                  └─────────────┘
                        │
                   ┌────┴────┐
                   │  Admin  │
                   └─────────┘
```

##### 2.2.3.1. Pre-condition

- Admin đã đăng nhập thành công.
- Đã có ít nhất 1 khu vực trong hệ thống.

##### 2.2.3.2. Post-condition

- Danh sách trò chơi được thêm/sửa/xóa thành công trong database.

##### 2.2.3.3. Triggers

- Admin chọn menu "Quản lý trò chơi" từ thanh menu chính.

#### 2.2.4. Workflows

**Scenario 1 — Xem danh sách trò chơi**

| Actor | System |
|-------|--------|
| 1. Chọn menu "Quản lý trò chơi" | 2. Lấy dữ liệu danh sách toàn bộ trò chơi/thiết bị từ hệ thống. Hiển thị dạng danh sách với các thông tin: Mã TC, Tên trò chơi, Khu vực thuộc về, Loại, Sức chứa, Giới hạn tuổi, Trạng thái. |

**Scenario 2 — Thêm trò chơi mới**

| Actor | System |
|-------|--------|
| 1. Nhấn "Thêm mới" | 2. Hiển thị form bổ sung thông tin trò chơi mới. |
| 3. Nhập các thông tin (Tên, Sức chứa, Thời gian, Chọn Khu vực, v.v...) → Nhấn "Lưu" | 4. Kiểm tra tính hợp lệ của dữ liệu (ví dụ: tuổi, số lượng). Nếu hợp lệ, lưu thông tin trò chơi vào hệ thống, thông báo "Thêm trò chơi thành công" và làm mới lại màn hình danh sách. |

**Scenario 3 — Sửa trò chơi**

| Actor | System |
|-------|--------|
| 1. Chọn trò chơi trong danh sách → Nhấn "Sửa" | 2. Nạp dữ liệu cấu hình hiện hành của trò chơi đó lên form sửa. |
| 3. Chỉnh sửa thông tin liên quan → Nhấn "Lưu" | 4. Kiểm tra quy tắc dữ liệu. Nếu hợp lệ, tiến hành cập nhật bản ghi trong hệ thống, thông báo "Cập nhật thành công" và tải lại danh sách hiển thị. |

**Scenario 4 — Xóa trò chơi**

| Actor | System |
|-------|--------|
| 1. Chọn một trò chơi trong danh sách → Nhấn "Xóa" | 2. Hiển thị hộp thoại để đảm bảo người dùng muốn xóa: "Bạn có chắc chắn muốn xóa trò chơi này?" |
| 3. Nhấn "Có" | 4. Hệ thống tiến hành xóa bỏ trò chơi đã chọn, thông báo kết quả "Xóa hoàn tất" và cập nhật trực tiếp sự biến mất này trên vùng hiển thị danh sách. |

**Scenario 5 — Lọc theo khu vực/loại trò chơi**

| Actor | System |
|-------|--------|
| 1. Người dùng chọn Khu vực hoặc Loại trò chơi tại phần bộ lọc phía trên màn hình | 2. Rút gọn danh sách, chỉ hiển thị những trò chơi của khu vực hoặc loại hình mà người dùng vừa chọn. |

#### 2.2.5. External interfaces

##### 2.2.5.1. Prototype — Quản lý Trò chơi (Master-Detail)

```text
╔══════════════════════════════════════════════════════════════════════════╗
║  QUẢN LÝ TRÒ CHƠI                                                        ║
║  Khu vực: [▼ Tất cả     ]   Tìm kiếm: [________________] [🔍 Tìm]        ║
║                                                                          ║
║  ┌────────────────────────────────────┐ ┌──────────────────────────────┐ ║
║  │ LƯỚI DANH SÁCH (MASTER)            │ │ THÔNG TIN CHI TIẾT           │ ║
║  │ ┌────┬──────────────┬────────────┐ │ │ Mã TC: [_____] (Auto)        │ ║
║  │ │ Mã │ Tên trò chơi │ Khu vực    │ │ │ Tên:   [___________________] │ ║
║  │ ├────┼──────────────┼────────────┤ │ │ Khu vực:   [▼ Khu Mạo hiểm ] │ ║
║  │ │G001│ Tàu lượn     │ Khu Mạo hiểm│ │ │ Loại TC:   [▼ Cảm giác mạnh] │ ║
║  │ │G002│ Bể bơi sóng  │ Khu Nước   │ │ │ Trạng thái:[▼ Hoạt động    ] │ ║
║  │ └────┴──────────────┴────────────┘ │ │                              │ ║
║  │                                    │ │ CẢNH BÁO AN TOÀN:            │ ║
║  │                                    │ │ Sức chứa:    [_____] người   │ ║
║  │                                    │ │ Tuổi Min:    [_____] tuổi    │ ║
║  │                                    │ │ C.Cao Min:   [_____] cm      │ ║
║  │                                    │ │ T.Gian/Lượt: [_____] phút    │ ║
║  └────────────────────────────────────┘ └──────────────────────────────┘ ║
║                                                                          ║
║  [ + Thêm mới ]    [ ✏ Sửa ]    [ 🗑 Xóa ]    [ 💾 Lưu ]    [ ↻ Làm mới ]    ║
╚══════════════════════════════════════════════════════════════════════════╝
```

##### 2.2.5.2. Screen description — Thuộc tính màn hình

| No | Field name | Control type | Required | Data type | Default value | Description |
|----|-----------|-------------|----------|-----------|---------------|-------------|
| 1 | Tìm kiếm | TextBox | No | Text | Blank | Tìm theo tên trò chơi |
| 2 | Lọc Khu vực | ComboBox | No | Text | "Tất cả" | Lọc danh sách theo khu vực |
| 3 | Lọc Loại trò chơi | ComboBox | No | Text | "Tất cả" | Lọc danh sách theo loại trò chơi |
| 4 | Tìm | Button | — | N/A | N/A | Thực hiện tìm kiếm |
| 5 | DataGridView Trò Chơi | DataGridView | — | — | — | Hiển thị danh sách phía trái |
| 6 | Mã trò chơi | TextBox (ReadOnly) | Yes | Text | Auto | Mã tự động (ID) |
| 7 | Tên trò chơi | TextBox | Yes | Text | Blank | Tên trò chơi, tối đa 150 ký tự |
| 8 | Mã Code | TextBox (ReadOnly) | Yes | Text | Blank | Mã hiển thị (G001, G002...) |
| 9 | Khu vực | ComboBox | Yes | Text | Blank | Chọn khu vực chứa trò chơi |
| 10 | Loại trò chơi | ComboBox | Yes | Text | Blank | Loại: Cảm giác mạnh, Trẻ em, Nước, Văn hóa... |
| 11 | Trạng thái | ComboBox | Yes | Text | "Đang hoạt động" | Đang hoạt động, Đang bảo trì, Hỏng/Sự cố, Ngừng hoạt động |
| 12 | Sức chứa | TextBox | Yes | Number | 1 | Số người tối đa / lượt (> 0). Chỉ nhập số, validate phía code |
| 13 | Tuổi tối thiểu | TextBox | Yes | Number | 0 | Giới hạn độ tuổi (≥ 0). Chỉ nhập số, validate phía code |
| 14 | Chiều cao tối thiểu | ComboBox | Yes | Number | 0 | Chọn mốc chiều cao: 0, 100, 110, 120, 130, 140 cm |
| 15 | Thời gian / lượt | TextBox | Yes | Number | 0 | Thời gian chơi bằng phút (≥ 0). Chỉ nhập số, validate phía code |
| 16 | Mô tả | TextBox (Multiline) | No | Text | Blank | Mô tả chi tiết trò chơi |
| 17 | Ngày tạo | DatePicker (Disabled) | No | Date | Today | Ngày tạo trò chơi (ReadOnly) |
| 18 | Ngày cập nhật | DatePicker (Disabled) | No | Date | 01/01/1753 | Ngày cập nhật thông tin. Mặc định 01/01/1753 nếu rỗng (ReadOnly) |
| 19 | Các nút thao tác | Buttons | — | N/A | N/A | Thêm, Sửa, Xóa, Lưu, Làm mới |

#### 2.2.6. Business descriptions

| No | Name | Description |
|----|------|-------------|
| 1 | Mã trò chơi | Auto-generate: G + 3 chữ số (G001, G002...). Không trùng. |
| 2 | Tên trò chơi | Không được trùng trong cùng khu vực. Tự động trim khoảng trắng. |
| 3 | Khu vực | ComboBox chỉ load khu vực có trạng thái "Hoạt động". |
| 4 | Trạng thái | Mặc định "Đang hoạt động". Nếu khu vực chuyển "Đóng cửa", trò chơi tự động ngừng phục vụ. |
| 5 | Xóa | Cho phép xóa trực tiếp. Hiển thị hộp thoại xác nhận trước khi xóa. |
| 6 | Lọc | Lọc theo khu vực. Chọn "Tất cả" hiện toàn bộ. |
| 7 | Sắp xếp | Có thể click vào header DataGridView để sắp xếp theo cột. |

#### 2.2.7. User-validation rules

| No | Validation rule | Message | Code |
|----|----------------|---------|------|
| 1 | Tên trò chơi bỏ trống | "Vui lòng nhập tên trò chơi" | ERR_REQUIRED_GAME_NAME |
| 2 | Chưa chọn khu vực | "Vui lòng chọn khu vực" | ERR_REQUIRED_GAME_AREA |
| 3 | Chưa chọn loại | "Vui lòng chọn loại trò chơi" | ERR_REQUIRED_GAME_TYPE |
| 4 | Sức chứa ≤ 0 | "Sức chứa phải lớn hơn 0" | ERR_GAME_CAPACITY_MIN |
| 5 | Tên trùng trong khu vực | "Tên trò chơi đã tồn tại trong khu vực này" | ERR_DUPLICATE_GAME_NAME |
| 6 | Tên > 150 ký tự | "Tên trò chơi không được vượt quá 150 ký tự" | ERR_GAME_NAME_MAX_LENGTH |
| 7 | Mô tả > 500 ký tự | "Mô tả không được vượt quá 500 ký tự" | ERR_GAME_DESC_MAX_LENGTH |
| 8 | Tuổi tối thiểu < 0 | "Tuổi tối thiểu không được âm" | ERR_GAME_AGE_MIN |
| 9 | Chiều cao < 0 | "Chiều cao tối thiểu không được âm" | ERR_GAME_HEIGHT_MIN |
| 10 | Sức chứa không phải số | "Sức chứa phải là số nguyên" | ERR_CAPACITY_NOT_NUMBER |
| 11 | Tuổi tối thiểu không phải số | "Tuổi phải là số nguyên" | ERR_AGE_NOT_NUMBER |
| 12 | Thời gian lượt không phải số | "Thời gian phải là số nguyên" | ERR_DURATION_NOT_NUMBER |

#### 2.2.8. Related use-cases

- Quản lý Khu vực
- Quản lý Bảo trì Thiết bị (Sprint 5)
- Main Form

---

## 3. YÊU CẦU KHÁC

### 3.1. Header / Menu chính (Main Form)

#### 3.1.1. Overview

Hiển thị cửa sổ chính (MDI Parent Form) với thanh menu điều hướng.

#### 3.1.2. External interfaces

##### 3.1.2.1. Prototype

```
╔═══════════════════════════════════════════════════════════════════════╗
║  Hệ thống Quản lý Công viên Vui chơi Giải trí                       ║
║  ┌──────────────────────────────────────────────────────────────────┐ ║
║  │ Hệ thống │ Khu vực │ Trò chơi │ Vé │ Khách hàng │ Thống kê │  ...│ ║
║  └──────────────────────────────────────────────────────────────────┘ ║
║                                                                      ║
║  ┌──────────────────────────────────────────────────────────────────┐ ║
║  │                                                                  │ ║
║  │                    (Khu vực hiển thị MDI Child)                   │ ║
║  │                                                                  │ ║
║  │                                                                  │ ║
║  └──────────────────────────────────────────────────────────────────┘ ║
║                                                                      ║
║  Người dùng: [Admin]         Ngày: 04/03/2026         Trạng thái: ● ║
╚═══════════════════════════════════════════════════════════════════════╝
```

##### 3.1.2.2. Menu structure

| No | Menu | Submenu | Vai trò | Description |
|----|------|---------|---------|-------------|
| 1 | Hệ thống | Cấu hình | Admin | Cấu hình hệ thống |
| 2 | Hệ thống | Đăng xuất | All | Đăng xuất |
| 3 | Hệ thống | Thoát | All | Đóng ứng dụng |
| 4 | Khu vực | Quản lý khu vực | Admin | Mở form quản lý khu vực |
| 5 | Trò chơi | Quản lý trò chơi | Admin | Mở form quản lý trò chơi |
| 6 | Vé | Quản lý loại vé | Admin | Mở form quản lý loại vé *(Sprint 1)* |
| 7 | Khách hàng | — | — | *(Sprint 2)* |
| 8 | Nhân viên | — | Admin | *(Sprint 3)* |
| 9 | Dịch vụ | — | Admin | *(Sprint 3)* |
| 10 | Thống kê | — | Admin | *(Sprint 4)* |
| 11 | Sự kiện | — | Admin | *(Sprint 4)* |
| 12 | Bảo trì | — | Admin | *(Sprint 5)* |
| 13 | Báo cáo | — | Admin | *(Sprint 5)* |

##### 3.1.2.3. Status bar

| No | Field name | Description |
|----|-----------|-------------|
| 1 | Ngày | Ngày hiện tại, format dd/MM/yyyy |
| 2 | Trạng thái | Trạng thái kết nối server: ● Xanh = Connected, ● Đỏ = Disconnected |

---

### 3.2. Định dạng dữ liệu (Data Format)

#### 3.2.1. Date/Time Format

| Loại | Format | Ví dụ |
|------|--------|-------|
| Ngày | dd/MM/yyyy | 04/03/2026 |
| Giờ | HH:mm:ss | 14:30:00 |
| Ngày giờ | dd/MM/yyyy HH:mm | 04/03/2026 14:30 |

#### 3.2.2. Number Format

| Loại | Format | Ví dụ |
|------|--------|-------|
| Tiền tệ | #,###đ | 50,000đ |
| Số nguyên | #,### | 1,234 |
| Phần trăm | ##.#% | 25.5% |

---

### 3.3. Database Schema (Sprint 1)

#### Bảng TicketTypes

| Column | Data Type | Constraint | Description |
|--------|-----------|-----------|-------------|
| MaLoaiVe | INT | PK, Identity | Mã loại vé |
| MaCode | NVARCHAR(10) | NOT NULL, UNIQUE | Mã vé (T01, T02...) |
| TenLoaiVe | NVARCHAR(100) | NOT NULL | Tên loại vé |
| GiaVe | DECIMAL(18,2)| NOT NULL | Giá vé |
| GiaCuoiTuan| DECIMAL(18,2)| NULL | Giá cuối tuần (nếu có) |
| DoiTuong | NVARCHAR(50) | NOT NULL | Đối tượng (Người lớn, Trẻ em...) |
| LaCombo | BIT | NOT NULL, DEFAULT 0 | Là vé combo hay vé lẻ |
| TrangThai | NVARCHAR(20) | NOT NULL, DEFAULT 'Hoạt động' | Trạng thái (Hoạt động/Ngừng bán) |
| NgayTao | DATETIME | NOT NULL, DEFAULT GETDATE() | Ngày tạo |
| NgayCapNhat| DATETIME | NULL | Ngày cập nhật |

#### Bảng KhuVuc

| Column | Data Type | Constraint | Description |
|--------|-----------|-----------|-------------|
| MaKhuVuc | INT | PK, Identity | Mã khu vực |
| MaCode | NVARCHAR(10) | NOT NULL, UNIQUE | Mã code (A01, A02...) |
| TenKhuVuc | NVARCHAR(100) | NOT NULL, UNIQUE | Tên khu vực |
| MoTa | NVARCHAR(500) | NULL | Mô tả |
| TrangThai | NVARCHAR(20) | NOT NULL, DEFAULT 'Hoạt động' | Trạng thái |
| NgayTao | DATETIME | NOT NULL, DEFAULT GETDATE() | Ngày tạo |
| NgayCapNhat| DATETIME | NULL | Ngày cập nhật |

#### Bảng TroChoi

| Column | Data Type | Constraint | Description |
|--------|-----------|-----------|-------------|
| MaTroChoi | INT | PK, Identity | Mã trò chơi |
| MaCode | NVARCHAR(10) | NOT NULL, UNIQUE | Mã code (G001, G002...) |
| TenTroChoi | NVARCHAR(150) | NOT NULL | Tên trò chơi |
| MaKhuVuc | INT | FK → KhuVuc.MaKhuVuc | Mã khu vực chứa trò chơi |
| LoaiTroChoi| NVARCHAR(50) | NOT NULL | Loại (Cảm giác mạnh, Trẻ em...) |
| SucChua | INT | NOT NULL, DEFAULT 1 | Sức chứa (người/lượt) |
| TuoiToiThieu| INT | NOT NULL, DEFAULT 0 | Tuổi tối thiểu |
| ChieuCaoToiThieu | INT | NOT NULL, DEFAULT 0 | Chiều cao tối thiểu (cm) |
| ThoiGianLuot| INT | NOT NULL, DEFAULT 5 | Thời gian/lượt (phút) |
| MoTa | NVARCHAR(500) | NULL | Mô tả |
| TrangThai | NVARCHAR(20) | NOT NULL, DEFAULT 'Đang hoạt động' | Trạng thái (Đang hoạt động/Đang bảo trì/Hỏng/Sự cố/Ngừng hoạt động) |
| NgayTao | DATETIME | NOT NULL, DEFAULT GETDATE() | Ngày tạo |
| NgayCapNhat| DATETIME | NULL | Ngày cập nhật |

#### Bảng NhanVien
| Column | Data Type | Constraint | Description |
|--------|-----------|-----------|-------------|
| MaNhanVien | INT | PK, Identity | Mã nhân viên |
| MaCode | NVARCHAR(10) | NOT NULL, UNIQUE | Mã code (NV001, NV002...) |
| HoTen | NVARCHAR(100) | NOT NULL | Họ và tên nhân viên |
| NgaySinh | DATE | NOT NULL | Ngày sinh |
| GioiTinh | NVARCHAR(10) | NOT NULL | Giới tính (Nam/Nữ/Khác) |
| DiaChi | NVARCHAR(255) | NULL | Địa chỉ |
| SoDienThoai | NVARCHAR(15) | NULL | Số điện thoại |
| Email | NVARCHAR(100) | NULL, UNIQUE | Email |
| ChucVu | NVARCHAR(50) | NOT NULL | Chức vụ |
| Luong | DECIMAL(18,2) | NOT NULL | Mức lương |
| NgayVaoLam | DATE | NOT NULL, DEFAULT GETDATE() | Ngày vào làm |
| TrangThai | NVARCHAR(20) | NOT NULL, DEFAULT 'Đang làm việc' | Trạng thái (Đang làm việc/Nghỉ phép/Đã nghỉ) |
| NgayTao | DATETIME | NOT NULL, DEFAULT GETDATE() | Ngày tạo |
| NgayCapNhat| DATETIME | NULL | Ngày cập nhật |

---

### 3.4. Mô hình Kết nối LAN (Client-Server Database)

Hệ thống hoạt động theo mô hình Client-Server trong mạng nội bộ (LAN):
- **Server Database**: Một máy tính được chỉ định làm máy chủ cài đặt SQL Server, cấu hình mở cổng kết nối mạng (thường là Port 1433) để tiếp nhận các yêu cầu truy vấn.
- **Client (Các máy trạm)**: Ứng dụng Desktop kết nối trực tiếp đến IP của máy Server và Port tương ứng thông qua chuỗi kết nối (Connection String).
- **Chuỗi kết nối mẫu**: `Server=IP_MAY_SERVER,PORT;Database=AmusementParkDB;User Id=username;Password=password;`

*Ghi chú: Mô hình này giúp loại bỏ độ trễ của HTTP API, phù hợp với hệ thống nội bộ cần tốc độ cao và thao tác trực tiếp với cơ sở dữ liệu.*

---

### 3.5. Message List (Sprint 1)

| Message Code | Vietnamese Content |
|-------------|-------------------|
| ERR_REQUIRED_TICKET_NAME | Vui lòng nhập tên loại vé |
| ERR_INVALID_PRICE | Giá vé không hợp lệ |
| ERR_DUPLICATE_TICKET | Tên loại vé đã tồn tại |
| ERR_TICKET_HAS_TRANSACTION | Không thể xóa loại vé đã phát sinh giao dịch |
| ERR_SERVER_CONNECTION | Không thể kết nối đến server |
| ERR_REQUIRED_AREA_NAME | Vui lòng nhập tên khu vực |
| ERR_DUPLICATE_AREA_NAME | Tên khu vực đã tồn tại |
| ERR_AREA_NAME_MAX_LENGTH | Tên khu vực không được vượt quá 100 ký tự |
| ERR_AREA_DESC_MAX_LENGTH | Mô tả không được vượt quá 500 ký tự |
| ERR_AREA_HAS_GAMES | Không thể xóa. Khu vực đang có trò chơi. |
| ERR_REQUIRED_GAME_NAME | Vui lòng nhập tên trò chơi |
| ERR_REQUIRED_GAME_AREA | Vui lòng chọn khu vực |
| ERR_REQUIRED_GAME_TYPE | Vui lòng chọn loại trò chơi |
| ERR_GAME_CAPACITY_MIN | Sức chứa phải lớn hơn 0 |
| ERR_DUPLICATE_GAME_NAME | Tên trò chơi đã tồn tại trong khu vực này |
| ERR_GAME_NAME_MAX_LENGTH | Tên trò chơi không được vượt quá 150 ký tự |
| ERR_GAME_DESC_MAX_LENGTH | Mô tả không được vượt quá 500 ký tự |
| ERR_GAME_AGE_MIN | Tuổi tối thiểu không được âm |
| ERR_GAME_HEIGHT_MIN | Chiều cao tối thiểu không được âm |
| ERR_PRICE_NOT_NUMBER | Giá vé phải là số hợp lệ |
| ERR_WEEKEND_PRICE_NOT_NUMBER | Giá cuối tuần phải là số hợp lệ |
| ERR_CAPACITY_NOT_NUMBER | Sức chứa phải là số nguyên |
| ERR_AGE_NOT_NUMBER | Tuổi phải là số nguyên |
| ERR_DURATION_NOT_NUMBER | Thời gian phải là số nguyên |
| MSG_SAVE_SUCCESS | Lưu thành công |
| MSG_DELETE_SUCCESS | Xóa thành công |
| MSG_DELETE_CONFIRM | Bạn có chắc muốn xóa? |
| MSG_GAME_UNDER_MAINTENANCE | Trò chơi này đang bảo trì, không thể bán vé! |
| MSG_GAME_BROKEN | Trò chơi này đang gặp sự cố hỏng hóc! |

---



