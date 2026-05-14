---
name: srs-writing-rules
description: Quy chuẩn viết tài liệu Đặc Tả Yêu Cầu Phần Mềm (SRS) cho dự án Quản Lý Công Viên Đại Nam. Áp dụng bắt buộc khi tạo hoặc chỉnh sửa bất kỳ file SRS nào trong thư mục docs/.
---

# 📄 SRS WRITING STANDARDS — Đại Nam Park Management System

> **Mục đích**: Đây là Single Source of Truth cho định dạng và giọng viết SRS trong dự án.
> **AI Instruction**: PHẢI ĐỌC FILE NÀY TRƯỚC KHI VIẾT BẤT KỲ TÀI LIỆU SRS NÀO. KHÔNG ĐƯỢC VI PHẠM.

---

## I. FILE STRUCTURE — MANDATORY TEMPLATE

Mọi file SRS phải tuân thủ cấu trúc sau theo đúng thứ tự. Không được bỏ mục, không được đảo thứ tự.

```
# Khu Du Lịch Đại Nam
# Đặc Tả Yêu Cầu Phần Mềm
# Mã dự án: DN01
# Mã tài liệu: DN01_SRS_[TenChucNang]_v[X.Y]

Hồ Chí Minh, Tháng MM/YYYY

---

## Lịch sử thay đổi
(Bảng: Ngày hiệu lực | Hạng mục thay đổi | A/M/D | Mô tả | Phiên bản)

## Mục lục
(Liệt kê các mục lớn, mục con có anchor link)

---

# 1. [Tên chức năng chính]

  ## 1.X. [Tên use-case]
    ### 1.X.1. Tổng quan
    ### 1.X.2. Tác nhân
    ### 1.X.3. Biểu đồ use-case (nếu có)
      #### 1.X.3.1. Tiền điều kiện
      #### 1.X.3.2. Hậu điều kiện
      #### 1.X.3.3. Điểm kích hoạt
    ### 1.X.4. Luồng thao tác
      #### 1.X.4.1. Tình huống 1 — [mô tả]
      #### 1.X.4.2. Tình huống 2 — [mô tả]
    ### 1.X.5. Giao diện            ← BẮT BUỘC CHI TIẾT
    ### 1.X.6. Mô tả nghiệp vụ      ← BẮT BUỘC
    ### 1.X.7. Quy tắc kiểm tra      ← BẮT BUỘC
    ### 1.X.8. Liên kết use-case     ← BẮT BUỘC

# 2. Yêu cầu khác
  ## 2.1. Định dạng dữ liệu
  ## 2.2. Danh mục dữ liệu tham chiếu
  ## 2.3. Bảng mã thông báo lỗi
```

---

## II. SCREEN DESCRIPTION — MANDATORY TABLE FORMAT

### 2.1. Field Description Table (7 columns)

Mỗi màn hình, mỗi panel, mỗi form riêng biệt phải có bảng riêng với đúng 7 cột:

```markdown
| STT | Tên trường | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
| 1 | Tên sản phẩm | Text Edit | Yes | Nvarchar(200) | Blank | Tên hiển thị. (*) Tooltip: Nhập tên sản phẩm |
```

**Quy tắc từng cột:**

| Cột | Quy tắc |
|---|---|
| STT | Số thứ tự liên tục trong bảng. |
| Tên trường | Tên hiển thị trên giao diện, viết tiếng Việt. |
| Control type | Ghi rõ loại control DevExpress hoặc WinForms: Text Edit, Spin Edit, Combo Box, Search Lookup Edit, Check Box, Date Edit, Memo Edit, Button, Label (Read-only), Grid Control, Tile View... Nếu control nằm trong grid, ghi thêm (in-grid). |
| Required | Yes, No, Conditional, hoặc N/A (cho label, button). |
| Data type | Ghi kiểu SQL hoặc C# tương ứng: Nvarchar(200), Decimal(15,0), Integer, Boolean, Text, Image, DateTime... Ghi N/A cho button và label. |
| Default value | Giá trị mặc định khi mở form: Blank, 0, Unchecked, Đang chọn, N/A... |
| Mô tả | Giải thích nghiệp vụ. Nếu có tooltip thì ghi (*) Tooltip: nội dung. Nếu có placeholder thì ghi (*) Placeholder: nội dung. |

### 2.2. Grid Column Table (Read-only)

Dùng cho Grid hoặc Tile View chỉ hiển thị, không chỉnh sửa:

```markdown
| STT | Tên hiển thị | Control type | Data type | Mô tả |
|---|---|---|---|---|
```

### 2.3. Grid Column Table (Editable)

Dùng cho Grid mà user nhập liệu trực tiếp trên ô (ví dụ: giỏ hàng, lưới hoàn hàng):

```markdown
| STT | Tên cột | Control type | Required | Data type | Default value | Mô tả |
|---|---|---|---|---|---|---|
```

---

## III. USER FLOW — TABLE FORMAT

Bảng luồng thao tác gồm 3 cột: bước, người dùng, hệ thống.

```markdown
| | [Tác nhân] | Hệ thống |
|---|---|---|
| 1 | Mô tả hành động người dùng. | Mô tả phản hồi hệ thống. |
```

Quy tắc:
- Mỗi bước là một cặp hành động - phản hồi.
- Nếu bước chỉ có hệ thống xử lý (không cần user), ghi dấu — ở cột người dùng.
- Mỗi tình huống (happy path, exception path) là một bảng riêng, đánh số tình huống.

---

## IV. BUSINESS RULES — TABLE FORMAT

```markdown
| STT | Tên | Quy tắc |
|---|---|---|
| 1 | Tên quy tắc ngắn gọn | Mô tả chi tiết quy tắc bằng câu văn hoàn chỉnh. |
```

---

## V. VALIDATION RULES — MANDATORY TABLE FORMAT

Mỗi use-case phải có bảng kiểm tra riêng:

```markdown
| STT | Quy tắc | Mã thông báo |
|---|---|---|
| 1 | Tên trường không được để trống | ERR_SP_TEN_RONG |
```

**Quy tắc đặt mã thông báo:**
- Tiền tố ERR_ cho lỗi, MSG_ cho thông báo thành công.
- Viết HOA, dùng gạch dưới phân tách.
- Cấu trúc: `ERR_[MODULE]_[MO_TA]` hoặc `MSG_[MODULE]_[MO_TA]`.
- Ví dụ: ERR_POS_CART_EMPTY, MSG_REFUND_SUCCESS, ERR_KM_HET_LUOT.
- Mã thông báo phải trùng khớp với key trong file UIStrings.resx.

---

## VI. USE-CASE LINKS — MANDATORY

Cuối mỗi use-case, liệt kê danh sách các use-case liên quan:

```markdown
### 1.X.8. Liên kết use-case

- Tên use-case liên quan (số mục)
- Tên use-case liên quan (số mục)
```

---

## VII. WRITING TONE — ABSOLUTE RULES

### 7.1. FORBIDDEN

| Đối tượng | Lý do |
|---|---|
| Mũi tên `->` | Không phải ký hiệu văn bản chuẩn. Dùng câu văn thay thế. |
| Ngoặc kép bọc tên nút, tên trạng thái | Tránh nổi bật không cần thiết. Viết liền vào câu. |
| Thuật ngữ code (class, object, method, LINQ...) | SRS là tài liệu nghiệp vụ, không phải tài liệu kỹ thuật. |
| Tên biến, tên hằng số, tên namespace | Không bao giờ xuất hiện trong SRS. |
| Ký hiệu toán học (≥, ≤, ≠, =) | Thay bằng chữ: lớn hơn hoặc bằng, khác 0, bằng... |
| Từ ngữ PR sáo rỗng (tối ưu hóa, trải nghiệm liền mạch, nguyên tử...) | Văn phong SRS phải khách quan, lạnh lùng, đi thẳng vào logic nghiệp vụ, không quảng cáo. |
| Lối viết Catalog (Liệt kê tính năng khô khan) | Các đoạn mô tả tổng quan phải xâu chuỗi ý bằng quan hệ Nhân - Quả thay vì chỉ gạch đầu dòng liệt kê. |
| Cấu trúc diễn thuyết ("Tóm lại", "Nhìn chung") | Giải thích xong luồng thao tác/logic thì dừng lại. Không thêm các câu chốt làm màu hay chi tiết hư cấu. |

### 7.2. MANDATORY

| Quy tắc | Ví dụ đúng |
|---|---|
| Viết câu văn hoàn chỉnh, có chủ ngữ vị ngữ | Nếu máy trống, hệ thống tạo phiên mới. |
| Khi nói điều kiện, dùng cấu trúc "Nếu ... thì ..." | Nếu không tìm thấy thì thông báo cho thu ngân. |
| Tên nút viết liền, không ngoặc kép | Nhấn nút Xác nhận / Nhấn nút Mở phiên |
| Tên trạng thái viết liền | Trạng thái chuyển thành Đã đóng |
| Giá trị mẫu (placeholder, ví dụ dữ liệu) ĐƯỢC dùng ngoặc kép | (*) Placeholder: "Quét mã vạch hoặc gõ tên..." |
| Số tiền luôn ghi kèm ₫ | 150,000đ (không viết 150000 hoặc 150,000 VND) |

### 7.3. Language

- Toàn bộ SRS viết bằng **tiếng Việt**.
- Thuật ngữ kỹ thuật giữ nguyên tiếng Anh khi không có từ tiếng Việt phổ biến: RFID, QR, F&B, POS, Hotkey.
- Control type trong bảng giao diện viết tiếng Anh (Text Edit, Spin Edit, Combo Box...) vì đây là tên chuẩn DevExpress.

---

## VIII. APPENDIX — SECTION 2 OF SRS

Phần 2 là phần phụ lục, bao gồm:

### 8.1. Data Formats
Liệt kê chuẩn hiển thị ngày giờ, số tiền, số lượng, điểm tích lũy.

### 8.2. Reference Data Catalogs
Bảng tra cứu các giá trị enum, trạng thái, cấu hình liên quan đến chức năng.

### 8.3. Error Code Master Table
Tổng hợp tất cả mã thông báo trong SRS thành 1 bảng cuối:

```markdown
| Mã thông báo | Nội dung tiếng Việt |
|---|---|
| ERR_SP_TEN_RONG | Tên sản phẩm không được để trống |
```

---

## IX. PRE-COMPLETION CHECKLIST

Trước khi đánh dấu SRS là hoàn thành, kiểm tra toàn bộ danh sách sau:

- [ ] Mọi màn hình đều có bảng 7 cột (STT, Tên trường, Control type, Required, Data type, Default value, Mô tả)
- [ ] Mọi use-case đều có bảng Quy tắc kiểm tra dữ liệu (STT, Quy tắc, Mã thông báo)
- [ ] Mọi use-case đều có mục Liên kết use-case
- [ ] Mọi use-case đều có mục Mô tả nghiệp vụ
- [ ] Không còn mũi tên `→` trong toàn bộ file
- [ ] Không còn ngoặc kép bọc tên nút, tên trạng thái
- [ ] Không có thuật ngữ code (class, method, LINQ, namespace...)
- [ ] Mã thông báo theo format ERR_[MODULE]_[MO_TA] hoặc MSG_[MODULE]_[MO_TA]
- [ ] Bảng mã thông báo cuối file đầy đủ, khớp với các bảng kiểm tra ở từng mục
- [ ] Số liệu tiền tệ ghi kèm ₫, phân cách hàng nghìn

---

## X. REFERENCE FILES

Khi viết SRS mới, bắt buộc mở 2 file mẫu sau để đối chiếu format:

1. **SRS Sản phẩm**: `docs/Sprint1/02_SRS_CN09_SanPham.md` — mẫu chuẩn cho module CRUD với nhiều tab chi tiết.
2. **SRS Bán hàng POS**: `docs/Sprint1/03_SRS_BanHangPOS.md` — mẫu chuẩn cho module nghiệp vụ phức tạp với nhiều luồng thao tác.

> **Nguyên tắc vàng**: SRS là tài liệu cho người đọc KHÔNG BIẾT CODE.
> Nếu đọc xong mà phải mở source code mới hiểu thì SRS đã THẤT BẠI.
