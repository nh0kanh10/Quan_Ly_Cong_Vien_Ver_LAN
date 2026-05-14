---
name: tc-writing-rules
description: Quy chuẩn viết Test Case cho dự án Quản Lý Công Viên Đại Nam. Áp dụng bắt buộc khi tạo hoặc chỉnh sửa bất kỳ file test case nào trong thư mục docs/.
---

# 📋 TEST CASE WRITING STANDARDS — Đại Nam Park Management System

> **Mục đích**: Đây là Single Source of Truth cho định dạng và giọng viết Test Case trong dự án.
> **AI Instruction**: PHẢI ĐỌC FILE NÀY TRƯỚC KHI VIẾT BẤT KỲ TEST CASE NÀO. KHÔNG ĐƯỢC VI PHẠM.

---

## I. VAI TRÒ VÀ NGỮ CẢNH

Người viết test case đóng vai **sinh viên năm cuối ngành CNTT đang làm đồ án tốt nghiệp** cho hệ thống công viên vui chơi Đại Nam. Test case phải:

- Tỉ mỉ, đầy đủ từ bước khởi đầu
- Ngôn ngữ thuần Việt, dễ đọc, dễ thực hiện
- Phù hợp trình độ sinh viên: tập trung chức năng và giao diện, không bịa lỗi hệ thống cao siêu

---

## II. CẤU TRÚC BẢNG BẮT BUỘC

Mỗi file test case phải là bảng Markdown với đúng các cột sau:

```markdown
| REQ ID | TC ID | Title | Est. (phút) | Test Type | Area | Precondition | Procedure / Steps | Expected Results | Priority | Result | Change TC | Author | Remark |
```

**Quy tắc từng cột:**

| Cột | Quy tắc |
|---|---|
| REQ ID | Mã yêu cầu liên quan, ví dụ: UC_SP_001 |
| TC ID | Mã test case: TC\_[MODULE]\_[STT]. Ví dụ: TC_SP_001 |
| Title | Tiêu đề ngắn gọn, nêu rõ kịch bản đang test |
| Est. (phút) | Thời gian ước tính thực hiện (số nguyên dương) |
| Test Type | Chức năng / Giao diện / Phủ định / Luồng sai |
| Area | Tên màn hình hoặc use-case liên quan |
| **Precondition** | **Điều kiện hệ thống cần có trước khi chạy TC. Ghi rõ dữ liệu phải tồn tại sẵn. Nếu không có điều kiện đặc biệt thì ghi: Hệ thống đang chạy bình thường.** |
| Procedure / Steps | Các bước thực hiện, viết đánh số từng bước |
| Expected Results | Kết quả mong đợi, đánh số tương ứng từng bước |
| Priority | Mức độ ưu tiên: High / Medium / Low |
| Result | Để trống (Tester điền sau) |
| Change TC | Để trống |
| Author | Tên người viết |
| Remark | Ghi chú nếu có |

---

## III. 6 LUẬT THÉP — TUYỆT ĐỐI KHÔNG VI PHẠM

### LUẬT 1 — CHI TIẾT TỪ SỐ 0, KHÔNG LÀM BIẾNG

Mỗi test case phải viết đầy đủ các bước từ đầu. Bước 1 luôn là mở phần mềm và đăng nhập, bước 2 là điều hướng đến màn hình cần test.

**CẤM tuyệt đối:**
- `Thực hiện lại bước 1 đến 4`
- `Tương tự TC trước`
- `1–4. Đăng nhập và vào màn hình`

**Lý do**: Người đọc test case có thể không có TC trước đó trước mắt. Mỗi TC phải tự đứng một mình.

**Đúng:**
```
Bước 1. Mở phần mềm Đại Nam POS.
Bước 2. Đăng nhập tài khoản Quản lý (tên đăng nhập: admin).
Bước 3. Trên menu bên trái, chọn Danh mục.
Bước 4. Chọn mục Hàng hóa và Dịch vụ.
Bước 5. Nhấn nút Thêm mới.
```

**Bắt buộc ghi giá trị cụ thể khi nhập liệu:**
Bất kỳ bước nào có nhập liệu phải ghi rõ giá trị thực tế, không viết chung chung.

| Sai | Đúng |
|---|---|
| Nhập tên sản phẩm vào ô Tên SP. | Nhập vào ô Tên SP giá trị: Vé người lớn. |
| Nhập mã vào ô Mã SP. | Nhập vào ô Mã SP giá trị: VE_NGUOILON. |
| Nhập số lượng âm. | Nhập vào ô Hệ số giá trị: -5. |

---

### LUẬT 2 — ÁNH XẠ BƯỚC VÀ KẾT QUẢ 1:1

Cột Procedure có bao nhiêu bước thì cột Expected Results phải có bấy nhiêu kết quả tương ứng. Không gộp, không bỏ sót.

**Đúng:**
```
Bước 1. Mở phần mềm.    →    Kết quả 1. Phần mềm khởi động, hiển thị màn hình đăng nhập.
Bước 2. Đăng nhập.      →    Kết quả 2. Đăng nhập thành công, vào màn hình chính.
```

---

### LUẬT 3 — CẤM MŨI TÊN, DÙNG TIẾNG VIỆT

Tuyệt đối không dùng: `->`, `=>`, `→`, `>`.

Thay bằng chữ tiếng Việt rõ nghĩa:

| Sai | Đúng |
|---|---|
| Nút Lưu -> form đóng | Nhấn nút Lưu, hệ thống đóng form |
| trạng thái => Đã duyệt | trạng thái chuyển thành Đã duyệt |
| nhấn OK → thông báo | nhấn OK, hệ thống hiển thị thông báo |

---

### LUẬT 4 — THÔNG BÁO LỖI BẰNG TIẾNG VIỆT THUẦN, KHÔNG MÃ HÓA

Cấm viết mã lỗi kiểu `ERR_001`, `ERR_NULL`, `ERR_TRUNG_MASP`. Phải ghi đúng câu chữ hiển thị trên màn hình mà người dùng thực sự thấy.

| Sai | Đúng |
|---|---|
| Hiển thị lỗi ERR_MASP_RONG | Hệ thống hiển thị cảnh báo: "Mã sản phẩm không được để trống." |
| Lỗi ERR_TRUNG_MASP | Hệ thống hiển thị: "Mã sản phẩm đã tồn tại, vui lòng nhập mã khác." |
| ERR_HESO_KHONGHOPLE | Hệ thống hiển thị: "Tỷ lệ quy đổi phải là số dương lớn hơn 0." |

---

### LUẬT 5 — CHỈ VIẾT LỖI THỰC TẾ, PHẠM VI SINH VIÊN

Chỉ viết các kịch bản kiểm thử phổ biến, có thể xảy ra trong sử dụng bình thường:

**Được phép:**
- Bỏ trống trường bắt buộc
- Nhập sai định dạng (ngày tháng, số, email)
- Nhập số âm hoặc số 0
- Trùng mã, trùng tên
- Sai logic nghiệp vụ (chọn sai trạng thái, vượt giới hạn số lượng)
- Chọn thiếu dữ liệu bắt buộc

**CẤM bịa ra:**
- Tràn RAM / rò rỉ bộ nhớ
- Quá tải CPU hoặc GPU
- Mất kết nối mạng giữa chừng
- Lỗi đường truyền TCP/IP
- SQL Injection, XSS, buffer overflow

---

### LUẬT 6 — MÔ TẢ GIAO DIỆN NGẮN GỌN, TRỰC TIẾP

Cấm giải thích thừa. Nói thẳng vào màu sắc, hành vi, trạng thái.

| Sai | Đúng |
|---|---|
| chữ màu xanh lá (Success) | chữ màu xanh lá |
| nút bị vô hiệu hóa (disabled) | nút mờ đi, không nhấn được |
| màu đỏ (Danger/Error) | chữ màu đỏ |
| màu vàng đậm (Amber/Warning) | chữ màu vàng đậm |
| toast notification | thông báo nhỏ góc màn hình |

---

### LUẬT 7 — TIỀN ĐIỀU KIỆN BẮT BUỘC VÀ CỤ THỂ

Cột Precondition phải ghi rõ dữ liệu hệ thống cần có sẵn trước khi Tester bắt đầu chạy. Nếu thiếu precondition, TC thất bại không rõ nguyên nhân — đó không phải bug mà là thiếu chuẩn bị.

**Nguyên tắc viết Precondition:**
- Ghi dữ liệu cụ thể: tên, mã, trạng thái của bản ghi cần có sẵn trong hệ thống.
- Nếu TC là negative (test lỗi trùng mã) thì precondition phải ghi rõ bản ghi đó đã tồn tại.
- Nếu không có điều kiện gì đặc biệt thì ghi: Hệ thống đang chạy bình thường, chưa có dữ liệu liên quan.

| Sai | Đúng |
|---|---|
| (bỏ trống) | Trong hệ thống đã có sản phẩm với mã VE_NGUOILON và trạng thái Đang bán. |
| Có dữ liệu sẵn | Sản phẩm Bia Heineken (mã: BIA_HEI) đang có tồn kho bằng 5 lon tại kho chính. |
| Đã đăng nhập | Tài khoản admin đang hoạt động. Hệ thống chưa có combo nào. |

**Ví dụ:**
- TC test xóa sản phẩm còn tồn kho: `Sản phẩm Bia Heineken (mã: BIA_HEI) đang có tồn kho bằng 5 lon.`
- TC test trùng mã sản phẩm: `Trong hệ thống đã tồn tại sản phẩm có mã VE_NGUOILON.`
- TC test mở màn hình: `Hệ thống đang chạy bình thường, chưa có dữ liệu đặc biệt.`

---

## IV. LOẠI TEST CASE HỢP LỆ (TEST TYPE)

| Test Type | Mô tả |
|---|---|
| Functional | Kiểm tra luồng happy path — thao tác đúng, kết quả đúng |
| UI/UX | Kiểm tra bố cục, màu sắc, font chữ, ẩn hiện control |
| Negative | Kiểm tra khi nhập sai, thiếu, vượt giới hạn, trùng lặp |
| Edge Case | Kiểm tra tình huống biên, luồng sai nghiệp vụ, thao tác bất thường |

---

## V. MẪU TEST CASE CHUẨN

```markdown
| TC_SP_001 | UC_SP_001 | Mở màn hình danh sách sản phẩm thành công | 5 | Functional | 1.1 Danh sách SP | Hệ thống đang chạy bình thường, chưa có dữ liệu đặc biệt. |
Bước 1. Mở phần mềm Đại Nam POS.
Bước 2. Đăng nhập tài khoản Quản lý (tên đăng nhập: admin).
Bước 3. Trên menu bên trái, chọn Danh mục.
Bước 4. Chọn mục Hàng hóa và Dịch vụ.
|
Bước 1. Phần mềm khởi động, hiển thị màn hình đăng nhập.
Bước 2. Đăng nhập thành công, chuyển sang màn hình chính.
Bước 3. Menu mở rộng, hiển thị các mục con.
Bước 4. Màn hình danh sách sản phẩm hiển thị: lưới bên trái, panel chi tiết ẩn bên phải, thanh công cụ có 3 nút Thêm mới, Làm mới, Tìm kiếm.
| High | | | Bùi Trí Nguyên | |
```

---

## VI. PROMPT CHUẨN CHO AI SINH TEST CASE

Khi dùng AI để sinh test case, sử dụng prompt sau (thay phần trong ngoặc vuông):

```
Mày là sinh viên năm cuối ngành CNTT đang viết đồ án tốt nghiệp cho hệ thống công viên Đại Nam.

Nhiệm vụ: Viết [SỐ LƯỢNG] kịch bản kiểm thử cho chức năng: [TÊN CHỨC NĂNG].

Bảng phải có đúng các cột: REQ ID | TC ID | Title | Est. (phút) | Test Type | Area | Precondition | Procedure / Steps | Expected Results | Priority | Result | Change TC | Author | Remark.

Bắt buộc tuân thủ 7 luật sau:
1. Viết đầy đủ từng bước từ đầu. Cấm viết "thực hiện lại bước 1 đến 4". Các bước có nhập liệu phải ghi rõ giá trị nhập cụ thể.
2. Cột Procedure có N bước thì Expected Results phải có N kết quả tương ứng.
3. Cấm dùng mũi tên (-> => →). Dùng câu văn tiếng Việt.
4. Cấm dùng mã lỗi (ERR_XXX). Ghi đúng câu thông báo tiếng Việt người dùng thấy.
5. Chỉ viết lỗi thực tế sinh viên gặp: bỏ trống, sai định dạng, trùng mã, sai nghiệp vụ. Cấm bịa lỗi hệ thống.
6. Mô tả UI ngắn gọn: "chữ màu đỏ", "nút mờ đi", không giải thích thêm trong ngoặc.
7. Cột Precondition bắt buộc phải điền dữ liệu cần có sẵn (vd: "Đã có sản phẩm mã VE_01"), nếu không có ghi "Hệ thống bình thường".

Lưu ý: Test Type dùng tiếng Anh (Functional, UI/UX, Negative, Edge Case). Priority dùng tiếng Anh (High, Medium, Low).
Author: [TÊN NGƯỜI VIẾT].
Bao gồm cả luồng đúng và luồng sai. Nếu nội dung dài thì dừng lại và báo "Tiếp tục?" để tôi xác nhận.
```

---

## VII. CHECKLIST TRƯỚC KHI NỘP

- [ ] Mỗi TC bắt đầu từ bước Mở phần mềm và Đăng nhập
- [ ] Cột Precondition đã điền: ghi rõ dữ liệu cần có sẵn hoặc ghi "Hệ thống đang chạy bình thường"
- [ ] Số bước trong Procedure = số kết quả trong Expected Results
- [ ] Mọi bước nhập liệu đều ghi rõ giá trị cụ thể (không viết chung chung)
- [ ] Không có mũi tên nào (`->`, `=>`, `→`)
- [ ] Không có mã lỗi kiểu ERR_XXX, thay bằng câu tiếng Việt
- [ ] Không có cụm "thực hiện lại bước X đến Y" hay "tương tự TC trước"
- [ ] Không có giải thích màu sắc trong ngoặc như "(Success)", "(Danger)"
- [ ] Không có lỗi hệ thống bịa đặt (RAM, CPU, mạng, SQL injection)
- [ ] Tất cả mô tả UI bằng tiếng Việt thuần, ngắn gọn, dễ hiểu

---

## VIII. FILE MẪU THAM KHẢO

- Test case mẫu chuẩn: `docs/TestCase/TC_SanPham_v1.md`
- SRS gốc để đối chiếu: `docs/Sprint1/02_SRS_CN09_SanPham.md`

