# BIÊN BẢN YÊU CẦU THAY ĐỔI – Sprint 1

**Mã dự án**: SD001  
**Mã tài liệu**: SD001_CR_Sprint1_v1.0  
**Nhóm**: NguyenTanNhi  
**Đề tài**: Hệ thống Quản lý Công viên Vui chơi Giải trí  
**Ngày tạo**: 16/03/2026  

---

## RECORD OF CHANGE

| Effective Date | Changed Items | A/M/D | Change Description | New Version |
|----------------|--------------|-------|-------------------|-------------|
| 16/03/2026 | First release | A | Khởi tạo Biên bản yêu cầu thay đổi Sprint 1 | 1.0 |

---

## 1. DANH SÁCH YÊU CẦU THAY ĐỔI

### CR-001: Đổi NgayCapNhat mặc định từ NULL sang 01/01/1753

| Hạng mục | Chi tiết |
|----------|---------|
| **Mã yêu cầu** | CR-001 |
| **Ngày yêu cầu** | 15/03/2026 |
| **Người yêu cầu** | [Tên khách hàng / PO] |
| **Mức độ ưu tiên** | 🟠 Medium |
| **Tài liệu ảnh hưởng** | SRS Section 1.5.2, 2.1.5.2, 2.2.5.2 |

**Mô tả yêu cầu thay đổi:**  
- **Trước đây**: NgayCapNhat = NULL khi chưa có cập nhật, DatePicker cho phép sửa.
- **Yêu cầu mới**: NgayCapNhat mặc định = `01/01/1753` khi chưa bao giờ cập nhật. DatePicker ở trạng thái **Disabled** (ReadOnly), hệ thống tự quản lý.

**Lý do thay đổi:**  
Để tránh lỗi NULL khi hiển thị trên giao diện và thống nhất format date trên toàn hệ thống.

**Ảnh hưởng:**  
- 3 form: frmLoaiVe, frmKhuVuc, frmTroChoi
- SRS: Cập nhật Screen Description, thêm ghi chú ReadOnly
- Database: Không thay đổi schema

**Trạng thái:** ✅ Đã thực hiện (SRS v1.1)

---

### CR-002: Đồng bộ Control Type với giao diện thực tế

| Hạng mục | Chi tiết |
|----------|---------|
| **Mã yêu cầu** | CR-002 |
| **Ngày yêu cầu** | 15/03/2026 |
| **Người yêu cầu** | Team phát triển |
| **Mức độ ưu tiên** | 🟡 Low |
| **Tài liệu ảnh hưởng** | SRS Section 1.5.2, 2.2.5.2 |

**Mô tả yêu cầu thay đổi:**  
- **Trước đây**: SRS ghi Giá vé, Sức chứa, Tuổi, Thời gian dùng `NumericUpDown`.
- **Yêu cầu mới**: Đổi sang `TextBox` cho đồng nhất giao diện (Guna2TextBox theme đẹp hơn NumericUpDown mặc định).

**Lý do thay đổi:**  
- NumericUpDown mặc định của WinForms không có phiên bản Guna2 → giao diện "lạc quẻ" so với theme tổng thể.
- TextBox phù hợp hơn cho các trường số lớn (giá vé 500,000đ).

**Ảnh hưởng:**  
- SRS: Cập nhật control type, bổ sung validation rules cho TextBox numeric
- Code: Thêm validate "chỉ nhập số" vào BUS layer
- Message List: Thêm 5 error codes mới (ERR_PRICE_NOT_NUMBER, ERR_WEEKEND_PRICE_NOT_NUMBER, ERR_CAPACITY_NOT_NUMBER, ERR_AGE_NOT_NUMBER, ERR_DURATION_NOT_NUMBER)

**Trạng thái:** ✅ Đã thực hiện (SRS v1.2)

---

### CR-003: Bổ sung controls lọc vào thanh tìm kiếm

| Hạng mục | Chi tiết |
|----------|---------|
| **Mã yêu cầu** | CR-003 |
| **Ngày yêu cầu** | 15/03/2026 |
| **Người yêu cầu** | Team phát triển |
| **Mức độ ưu tiên** | 🟡 Low |
| **Tài liệu ảnh hưởng** | SRS Section 1.5.2, 2.1.5.2, 2.2.5.2 |

**Mô tả yêu cầu thay đổi:**  
Bổ sung ComboBox lọc nâng cao trên thanh tìm kiếm:
- frmLoaiVe: Thêm `cboLocDoiTuong` (Lọc theo đối tượng)
- frmKhuVuc: Thêm `cboLocTrangThai` (Lọc theo trạng thái)
- frmTroChoi: Tách rõ `cboLocKhuVuc` + `cboLocLoaiTC`

**Lý do thay đổi:**  
Cải thiện trải nghiệm người dùng, giúp lọc nhanh danh sách thay vì chỉ tìm kiếm bằng text.

**Ảnh hưởng:**  
- SRS: Cập nhật Screen Description, thêm các field lọc
- Code: Đã implemented trong GUI + BUS layer

**Trạng thái:** ✅ Đã thực hiện (SRS v1.2)

---

## 2. TEMPLATE — YÊU CẦU THAY ĐỔI MỚI

> Sử dụng template dưới đây cho các yêu cầu thay đổi phát sinh trong tương lai.

```markdown
### CR-[XXX]: [Tiêu đề ngắn gọn]

| Hạng mục | Chi tiết |
|----------|---------|
| **Mã yêu cầu** | CR-[XXX] |
| **Ngày yêu cầu** | [DD/MM/YYYY] |
| **Người yêu cầu** | [Tên người yêu cầu] |
| **Mức độ ưu tiên** | 🔴 High / 🟠 Medium / 🟡 Low |
| **Tài liệu ảnh hưởng** | [SRS section, Code files...] |

**Mô tả yêu cầu thay đổi:**  
- **Trước đây**: [Mô tả hiện trạng]
- **Yêu cầu mới**: [Mô tả thay đổi mong muốn]

**Lý do thay đổi:**  
[Giải thích tại sao cần thay đổi]

**Ảnh hưởng:**  
- [Liệt kê các thành phần bị ảnh hưởng]

**Phê duyệt:**

| Vai trò | Họ tên | Ngày | Chữ ký |
|---------|--------|------|--------|
| Người yêu cầu | | | |
| PM / PO | | | |
| Lead Dev | | | |

**Trạng thái:** ⬜ Chờ duyệt / ✅ Đã duyệt / ❌ Từ chối / 🔄 Đang thực hiện
```

---

## 3. TỔNG HỢP

| CR_ID | Tiêu đề | Ưu tiên | Ngày YC | Trạng thái | SRS Version |
|-------|---------|---------|---------|:----------:|:-----------:|
| CR-001 | NgayCapNhat mặc định 01/01/1753 | 🟠 Medium | 15/03/2026 | ✅ Done | v1.1 |
| CR-002 | NumericUpDown → TextBox | 🟡 Low | 15/03/2026 | ✅ Done | v1.2 |
| CR-003 | Bổ sung ComboBox lọc | 🟡 Low | 15/03/2026 | ✅ Done | v1.2 |
