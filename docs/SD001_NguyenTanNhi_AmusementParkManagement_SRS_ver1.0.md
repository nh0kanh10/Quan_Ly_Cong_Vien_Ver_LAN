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

| Scenario | Actor | System |
|----------|-------|--------|
| 1. Xem danh sách | 1. Chọn menu "Quản lý loại vé" | 2. Truy vấn trực tiếp Database qua LAN. Hiển thị thông tin: Mã Vé, Tên vé, Giá tiền, Đối tượng, Trạng thái. |
| 2. Thêm loại vé | 1. Nhấn "Thêm mới" -> Nhập Tên, Giá, Đối tượng -> Nhấn "Lưu" | 2. Validate dữ liệu. Thực thi lệnh INSERT vào Database. Trả về thành công và cập nhật danh sách. |
| 3. Sửa loại vé | 1. Chọn loại vé -> Nhấn "Sửa" -> Thay đổi dữ liệu -> "Lưu" | 2. Thực thi lệnh UPDATE cập nhật thông tin vào Database. |
| 4. Xóa loại vé | 1. Chọn vé -> "Xóa" -> Xác nhận | 2. Thực thi lệnh UPDATE (soft delete) trong Database. Không cho xóa nếu vé đã từng được bán. |

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
| 2 | Tìm | Button | — | N/A | N/A | Thực hiện tìm kiếm |
| 3 | DataGridView Lưới | DataGridView | — | — | — | Hiển thị danh sách loại vé (Master) |
| 4 | Mã loại vé | TextBox (ReadOnly) | Yes | Text | Auto | Mã tự động |
| 5 | Tên loại vé | TextBox | Yes | Text | Blank | Tên loại vé, tối đa 100 ký tự |
| 6 | Giá ngày thường | NumericUpDown | Yes | Number | 0 | Giá vé (≥ 0 đ) |
| 7 | Giá cuối tuần | NumericUpDown | No | Number | 0 | Giá vé T7, CN |
| 8 | Đối tượng | ComboBox | Yes | Text | Người lớn | Gồm: Người lớn, Trẻ em, Tất cả |
| 9 | Là Vé Combo | CheckBox | No | Bool | False | Tích vào sẽ hiện lưới nhập vé con |
| 10 | Lưới ChiTietCombo | DataGridView | No | — | — | Chỉ Enable khi "Là Combo" = True |
| 11 | Các nút thao tác | Buttons | — | N/A | N/A | Thêm, Sửa, Xóa, Lưu, Làm mới |

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
| 1. Chọn menu "Quản lý khu vực" | 2. Truy vấn Database qua mạng LAN lấy danh sách khu vực. Hiển thị trên DataGridView với thông tin: Mã KV, Tên, Mô tả, Trạng thái, Số trò chơi. |

**Scenario 2 — Thêm khu vực mới**

| Actor | System |
|-------|--------|
| 1. Nhấn nút "Thêm mới" | 2. Hiển thị form thêm khu vực với các trường trống. |
| 3. Nhập thông tin: Tên khu vực, Mô tả, Trạng thái → Nhấn "Lưu" | 4. Validate dữ liệu. Nếu hợp lệ → Thực thi lệnh INSERT. Thêm thành công → Hiển thị thông báo "Thêm khu vực thành công". Refresh danh sách. |

**Scenario 3 — Sửa khu vực**

| Actor | System |
|-------|--------|
| 1. Chọn khu vực trong danh sách → Nhấn "Sửa" | 2. Hiển thị form sửa với dữ liệu hiện tại. |
| 3. Sửa thông tin → Nhấn "Lưu" | 4. Validate dữ liệu. Nếu hợp lệ → Thực thi lệnh UPDATE. Cập nhật thành công → Thông báo "Cập nhật thành công". Refresh danh sách. |

**Scenario 4 — Xóa khu vực**

| Actor | System |
|-------|--------|
| 1. Chọn khu vực → Nhấn "Xóa" | 2. Hiển thị hộp thoại xác nhận: "Bạn có chắc muốn xóa khu vực [Tên]?" |
| 3. Nhấn "Có" | 4. Kiểm tra khu vực có trò chơi không. Nếu không → Thực thi lệnh DELETE (hoặc soft delete). Xóa thành công → Refresh danh sách. Nếu có → Thông báo: "Không thể xóa. Khu vực đang có trò chơi." |

**Scenario 5 — Tìm kiếm**

| Actor | System |
|-------|--------|
| 1. Nhập từ khóa vào ô tìm kiếm → Nhấn "Tìm" | 2. Thực thi truy vấn SELECT với điều kiện LIKE chứa từ khóa. Hiển thị kết quả trên danh sách. |

#### 2.1.5. External interfaces

##### 2.1.5.1. Prototype — Quản lý Khu vực (Master-Detail)

```text
╔══════════════════════════════════════════════════════════════════════════╗
║  QUẢN LÝ KHU VỰC                                                         ║
║  Tìm kiếm: [________________________] [🔍 Tìm]                           ║
║                                                                          ║
║  ┌─────────────────────────────────────────┐ ┌─────────────────────────┐ ║
║  │ LƯỚI DANH SÁCH (MASTER)                 │ │ THÔNG TIN CHI TIẾT      │ ║
║  │ ┌────┬──────────────┬─────────┬───────┐ │ │                         │ ║
║  │ │ Mã │ Tên khu vực  │Trạng thái│ Số TC │ │ │ Mã KV: [_____] (Auto)   │ ║
║  │ ├────┼──────────────┼─────────┼───────┤ │ │ Tên:   [______________] │ ║
║  │ │ A01│ Khu Mạo hiểm │Hoạt động│ 5     │ │ │ Trạng thái:[▼ Hoạt động]│ ║
║  │ │ A02│ Khu Nước     │Hoạt động│ 3     │ │ │                         │ ║
║  │ └────┴──────────────┴─────────┴───────┘ │ │ Mô tả:                  │ ║
║  │                                         │ │ ┌─────────────────────┐ │ ║
║  │                                         │ │ │                     │ │ ║
║  │                                         │ │ └─────────────────────┘ │ ║
║  └─────────────────────────────────────────┘ └─────────────────────────┘ ║
║                                                                          ║
║  [ + Thêm mới ]    [ ✏ Sửa ]    [ 🗑 Xóa ]    [ 💾 Lưu ]    [ ↻ Làm mới ] ║
╚══════════════════════════════════════════════════════════════════════════╝
```

#### 2.1.5.2. Screen description — Thuộc tính màn hình

| No | Field name | Control type | Required | Data type | Default value | Description |
|----|-----------|-------------|----------|-----------|---------------|-------------|
| 1 | Tìm kiếm | TextBox | No | Text | Blank | Tìm theo tên khu vực |
| 2 | DataGridView KhuVuc | DataGridView | — | — | — | Hiển thị danh sách phía trái |
| 3 | Mã khu vực | TextBox (ReadOnly) | Yes | Text | Auto | Tự động sinh khi thêm mới. |
| 4 | Tên khu vực | TextBox | Yes | Text | Blank | Tên khu vực, tối đa 100 ký tự |
| 5 | Mô tả | TextBox (Multiline) | No | Text | Blank | Mô tả chi tiết khu vực |
| 6 | Trạng thái | ComboBox | Yes | Text | "Hoạt động" | Hoạt động, Bảo trì, Ngừng HĐ |
| 7 | Các nút thao tác | Buttons | — | N/A | N/A | Thêm, Sửa, Xóa, Lưu, Làm mới |

#### 2.1.6. Business descriptions

| No | Name | Description |
|----|------|-------------|
| 1 | Mã khu vực | Tự động tạo theo format: A + số thứ tự 2 chữ số (A01, A02, ...). Không cho phép trùng. |
| 2 | Tên khu vực | Không được trùng với khu vực đã có. Hệ thống tự trim khoảng trắng. |
| 3 | Trạng thái | Mặc định "Hoạt động". Khi chuyển sang "Ngừng hoạt động", tất cả trò chơi trong khu vực cũng chuyển sang "Ngừng hoạt động". |
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
| 1. Chọn menu "Quản lý trò chơi" | 2. Truy vấn Database lấy danh sách trò chơi. Hiển thị DataGridView: Mã TC, Tên, Khu vực, Loại, Sức chứa, Giới hạn tuổi, Trạng thái. |

**Scenario 2 — Thêm trò chơi mới**

| Actor | System |
|-------|--------|
| 1. Nhấn "Thêm mới" | 2. Hiển thị form thêm trò chơi. |
| 3. Nhập thông tin → Nhấn "Lưu" | 4. Validate. Nếu hợp lệ → Lệnh INSERT → Thông báo "Thêm trò chơi thành công". Refresh danh sách. |

**Scenario 3 — Sửa trò chơi**

| Actor | System |
|-------|--------|
| 1. Chọn trò chơi → Nhấn "Sửa" | 2. Load dữ liệu vào form sửa. |
| 3. Sửa thông tin → Nhấn "Lưu" | 4. Validate → Lệnh UPDATE → Thông báo "Cập nhật thành công". Refresh. |

**Scenario 4 — Xóa trò chơi**

| Actor | System |
|-------|--------|
| 1. Chọn trò chơi → Nhấn "Xóa" | 2. Hộp thoại xác nhận. |
| 3. Nhấn "Có" | 4. Lệnh DELETE → Xóa thành công → Refresh danh sách. |

**Scenario 5 — Lọc theo khu vực**

| Actor | System |
|-------|--------|
| 1. Chọn khu vực từ ComboBox lọc | 2. Thực thi truy vấn với điều kiện lọc theo area_id. Hiển thị danh sách trò chơi thuộc khu vực đó. |

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
| 1 | Bộ lọc tìm kiếm | Dropdown/TextBox | No | N/A | Blank | Bộ lọc Khu vực và Tên |
| 2 | DataGridView Trò Chơi | DataGridView | — | — | — | Hiển thị danh sách phía trái |
| 3 | Mã & Tên Trò chơi | TextBox | Yes | Text | Blank | Dữ liệu cơ bản |
| 4 | Khu vực & Loại TC | ComboBox | Yes | Text | Blank | Phân loại trò chơi |
| 5 | Tuổi tối thiểu | NumericUpDown | Yes | Number | 0 | Giới hạn độ tuổi (≥ 0) |
| 6 | Chiều cao tối thiểu | NumericUpDown | Yes | Number | 0 | Giới hạn chiều cao cm (≥ 0) |
| 7 | Sức chứa | NumericUpDown | Yes | Number | 1 | Số người tối đa / lượt (> 0) |
| 8 | Thời gian lượt | NumericUpDown | Yes | Number | 0 | Thời gian chơi bằng phút (≥ 0) |
| 9 | Trạng thái | ComboBox | Yes | Text | "Hoạt động" | Hoạt động, Bảo trì, Ngừng hoạt động |
| 10| Các nút thao tác | Buttons | — | N/A | N/A | Thêm, Sửa, Xóa, Lưu, Làm mới |

#### 2.2.6. Business descriptions

| No | Name | Description |
|----|------|-------------|
| 1 | Mã trò chơi | Auto-generate: G + 3 chữ số (G001, G002...). Không trùng. |
| 2 | Tên trò chơi | Không được trùng trong cùng khu vực. Tự động trim khoảng trắng. |
| 3 | Khu vực | ComboBox chỉ load khu vực có trạng thái "Hoạt động". |
| 4 | Trạng thái | Mặc định "Hoạt động". Nếu khu vực chuyển "Ngừng hoạt động", trò chơi tự động chuyển theo. |
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
| TrangThai | NVARCHAR(20) | NOT NULL, DEFAULT 'Hoạt động' | Trạng thái |
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
| MSG_SAVE_SUCCESS | Lưu thành công |
| MSG_DELETE_SUCCESS | Xóa thành công |
| MSG_DELETE_CONFIRM | Bạn có chắc muốn xóa? |
