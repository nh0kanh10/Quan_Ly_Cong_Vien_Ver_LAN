# PROMPT SINH TEST CASE — QUẢN LÝ SẢN PHẨM
# Hệ thống Khu Du lịch Đại Nam

> Sao chép toàn bộ nội dung file này và dán vào AI bất kỳ (Gemini, ChatGPT, Claude).
> Sau đó gõ: "Viết 30 TC tiếp theo bắt đầu từ TC_SP_[STT]"

---

## PHẦN 1 — BỐI CẢNH HỆ THỐNG

Mày là sinh viên năm cuối ngành CNTT đang viết đồ án tốt nghiệp cho hệ thống quản lý Khu Du lịch Đại Nam. Nhiệm vụ là viết bộ Test Case cho chức năng **Quản lý Sản phẩm & Dịch vụ** (màn hình CN09).

Hệ thống là ứng dụng Desktop WinForms, ngôn ngữ tiếng Việt. Người dùng thao tác bằng chuột và bàn phím trên giao diện Windows.

---

## PHẦN 2 — MÔ TẢ CHỨC NĂNG CẦN TEST

### 2.1 Màn hình chính (1.1 Danh sách SP)

- Giao diện Split View: lưới bên trái, panel chi tiết bên phải
- Lưới hiển thị dạng cây, nhóm theo Loại SP, tất cả nhóm tự mở rộng
- Thanh công cụ: nút **Thêm mới**, nút **Làm mới**, ô **Tìm kiếm**
- Bộ lọc nhanh gồm 7 nút toggle: Tất cả, Vé, Đồ ăn, Đồ uống, Cho thuê, Lưu trú, Vật tư
  - Nút đang chọn đổi màu nổi bật, các nút khác mờ
  - Nút Tất cả mặc định được chọn khi mở màn hình
- Lưới có cột Mã SP, Tên SP, Loại SP, Trạng thái, Hành động (nút Xóa)
- Màu chữ cột Trạng thái: Đang bán = xanh lá, Tạm ngưng = vàng đậm, Ngừng bán = đỏ
- Thanh trạng thái phía dưới hiển thị "Tổng {N}"
- Tìm kiếm realtime theo Mã SP và Tên SP (không cần nhấn Enter)
- Khi nhấp chọn một dòng SP → panel chi tiết bên phải hiện ra và nạp thông tin
- Khi nhấp vào dòng tiêu đề nhóm → panel chi tiết ẩn đi
- Khi panel chi tiết đang có dữ liệu chưa lưu mà nhấp sang dòng khác → hộp thoại cảnh báo xuất hiện với 3 nút: **Có** (lưu rồi chuyển), **Không** (bỏ thay đổi, chuyển), **Hủy** (ở lại)

### 2.2 Tab Thông tin chung (1.2)

**Các ô nhập liệu:**
- Ảnh đại diện (click để chọn file, chấp nhận: jpg, png, gif, webp)
- Mã SP: khi thêm mới tự sinh tiền tố theo Loại SP; khi sửa bị khóa không cho đổi
- Tên SP: bắt buộc, tối đa 150 ký tự
- Loại SP: bắt buộc, khi sửa bị khóa không cho đổi
- ĐVT gốc: bắt buộc
- Thuế VAT: bắt buộc
- Trạng thái: mặc định là Đang bán
- Điểm bán: chọn nhiều điểm bán
- Áp dụng toàn bộ POS: tích vào thì khóa ô Điểm bán, tự áp dụng tất cả
- Là vật tư: nếu Loại SP là Vé thì tự động tắt và khóa xám
- Quản lý lô (HSD): nếu Loại SP là Vé thì tự động tắt và khóa xám
- Giá tham khảo: luôn khóa (read-only), tự cập nhật từ dòng bảng giá Mặc định đầu tiên
- Nút **Lưu** (hotkey Ctrl+S), nút **Hủy** (hotkey Esc)

**Tiền tố mã SP theo loại (tự sinh khi thêm mới):**
| Loại SP | Tiền tố |
|---|---|
| Vé vào khu | VE_ |
| Vé trò chơi | VE_ |
| Ăn uống | FB_ |
| Đồ uống | DU_ |
| Hàng hóa | HH_ |
| Tủ đồ / Cho thuê | CT_ |
| Lưu trú | LT_ |
| Nguyên liệu | NL_ |
| Gửi xe | GX_ |

**Khi đổi Loại SP:** tiền tố mã tự cập nhật theo loại mới. Tab thứ 4 đổi nhãn thành Cấu hình vé hoặc Cấu hình F&B tùy loại.

**Quy tắc validate khi nhấn Lưu:**
- Mã SP trống → thông báo: "Bắt buộc có mã sản phẩm"
- Tên SP trống → thông báo: "Bắt buộc nhập tên sản phẩm"
- Loại SP chưa chọn → thông báo: "Bắt buộc chọn loại sản phẩm"
- ĐVT chưa chọn → thông báo: "Bắt buộc chọn đơn vị tính"
- Mã SP trùng → thông báo: "Mã sản phẩm đã tồn tại: {mã}"
- Mã SP chỉ chứa tiền tố (ví dụ chỉ có VE_) → thông báo: "Mã sản phẩm không được chỉ chứa mỗi tiền tố. Hãy gõ tiếp hoặc quét mã vạch."
- Trạng thái Đang bán nhưng bảng giá trống → hệ thống hỏi xác nhận rồi tự ép về Tạm ngưng
- Ảnh sai định dạng → thông báo: "Ảnh không đúng định dạng. Chỉ chấp nhận jpg, png, gif, webp"

**Sau khi lưu thành công:** thông báo "Lưu thành công", lưới bên trái tải lại, ô Giá tham khảo cập nhật.

**Nút Hủy khi thêm mới:** xóa trắng toàn bộ ô.
**Nút Hủy khi sửa:** tải lại dữ liệu gốc của SP đang sửa.

### 2.3 Tab Bảng giá (1.3)

- Lưới hiển thị các dòng giá, mỗi dòng gồm: Loại giá (Mặc định / Ngày lễ / Khuyến mãi), Hiệu lực từ, Hiệu lực đến, Giá bán
- Thêm dòng bằng nút Thêm dòng phía dưới lưới
- Xóa dòng bằng nút xóa cố định bên phải mỗi dòng
- Dòng vừa sửa được tô nền vàng nhạt
- Nếu Loại SP là Cho thuê → lưới hiện thêm 4 cột: Tiền cọc, Phút block đầu, Phút tiếp, Giá phụ thu
- Nếu không phải Cho thuê → 4 cột trên ẩn hoàn toàn
- Ô Giá tham khảo trên Tab Thông tin chung tự cập nhật theo giá Mặc định đầu tiên trong bảng

**Validate:**
- Ngày hiệu lực đến phải sau ngày hiệu lực từ → thông báo: "Ngày hiệu lực đến phải sau ngày hiệu lực từ"
- Giá bán phải lớn hơn 0 → thông báo: "Giá bán phải lớn hơn 0"
- Hai dòng cùng loại giá (cùng Mặc định) có khoảng ngày chồng lấp → thông báo: "Khoảng thời gian hiệu lực bị trùng lấp"
- Hai dòng khác loại giá, cùng ngày → hợp lệ, không báo lỗi

### 2.4 Tab Quy đổi ĐVT (1.4)

- Lưới gồm: ĐVT đích, Hệ số, Giá bán, nút xóa
- Dòng vừa sửa được tô nền vàng nhạt
- Nếu ô Giá bán để trống → cột hiển thị chữ Tự tính
- Nếu nhập giá cụ thể → dùng giá ấn định đó khi bán

**Validate:**
- Hệ số quy đổi phải là số dương hợp lệ (lớn hơn 0) → thông báo: "Tỷ lệ quy đổi phải là số dương hợp lệ"
- Hệ số = 0 → thông báo tương tự
- Hệ số âm → thông báo tương tự
- Hệ số để trống → thông báo tương tự

### 2.5 Tab Cấu hình vé (1.5)

- Chỉ hiển thị khi Loại SP là **Vé vào khu** hoặc **Vé trò chơi**
- Ô **Kích hoạt quyền truy cập** (checkbox): tích vào thì lưới bên dưới hiện ra
- Ô **Đối tượng**: Người lớn / Trẻ em / Tất cả
- Lưới quyền truy cập: cột Khu vực + Số lượt

**Quy tắc nghiệp vụ:**
- Loại SP = **Vé vào khu**: được thêm nhiều dòng khu vực
- Loại SP = **Vé trò chơi**: chỉ được thêm tối đa 1 dòng. Khi thêm dòng thứ 2 → thông báo: "Vé trò chơi chỉ được cấu hình 1 khu vực quẹt vé"
- Đã tích Kích hoạt nhưng lưới trống khi lưu → thông báo: "Lưới quyền truy cập cổng xoay không được để trống"

### 2.6 Tab Cấu hình F&B / BOM (1.6)

- Chỉ hiển thị khi Loại SP là **Ăn uống** hoặc **Đồ uống**
- Ô Cảnh báo dị ứng (ghi chú thành phần gây dị ứng)
- Ô Nhà hàng xuất món
- Lưới BOM: cột Nguyên liệu, ĐVT (tự điền), Số lượng tiêu hao, nút xóa
- Dòng vừa sửa được tô nền vàng nhạt
- Lookup Nguyên liệu chỉ liệt kê SP có cờ Là vật tư. Không cho chọn SP Vé vào làm nguyên liệu

### 2.7 Xóa sản phẩm (1.7)

- Nhấn nút Xóa ở cột Hành động → hộp thoại xác nhận xóa xuất hiện
- Nhấn Có → kiểm tra ràng buộc:
  - SP còn tồn kho lớn hơn 0 → thông báo: "Sản phẩm còn tồn kho, không thể xóa"
  - SP đang trong đơn hàng chưa chốt → thông báo: "Sản phẩm đang nằm trong đơn hàng chưa chốt, không thể xóa"
  - Không có ràng buộc → xóa ẩn, SP biến mất khỏi lưới, thông báo: "Đã xóa sản phẩm"
- Nhấn Không → hủy bỏ, SP vẫn còn trên lưới

---

## PHẦN 3 — FORMAT CSV BẮT BUỘC

Xuất ra dạng CSV với đúng 14 cột sau (có dấu ngoặc kép bao mỗi ô):

```
"REQ ID","TC ID","Title","Est. (phút)","Test Type","Area","Precondition","Procedure / Steps","Expected Results","Priority","Result","Change TC","Author","Remark"
```

**Quy tắc:**
- REQ ID: UC_SP_001 (dùng cho toàn bộ)
- TC ID: TC_SP_001, TC_SP_002, ... (đánh số tăng dần, không trùng)
- Test Type: chỉ dùng **Functional**, **UI/UX**, hoặc **Negative**
- Priority: **High**, **Medium**, hoặc **Low**
- Result, Change TC: để trống
- Author: Bùi Trí Nguyên
- Procedure / Steps và Expected Results: xuống hàng bằng ký tự `\n` trong cùng một ô

---

## PHẦN 4 — 7 LUẬT THÉP PHẢI TUÂN THỦ

**LUẬT 1 — Không làm biếng, viết đầy đủ từng bước:**
Bước 1 luôn là "Mở phần mềm Đại Nam POS."
Bước 2 luôn là "Đăng nhập tài khoản Quản lý (tên đăng nhập: admin)."
Bước 3 luôn là điều hướng đến màn hình cần test.
TUYỆT ĐỐI CẤM viết "Thực hiện lại bước 1 đến 4" hay "Tương tự TC trước".

**LUẬT 2 — Ánh xạ 1:1:**
Cột Procedure có N bước thì Expected Results phải có đúng N kết quả tương ứng (Bước 1 → Kết quả 1, Bước 2 → Kết quả 2...).

**LUẬT 3 — Cấm mũi tên:**
TUYỆT ĐỐI KHÔNG dùng ->, =>, →.
Dùng: "hệ thống hiển thị", "chuyển thành", "hiện ra", "thông báo".

**LUẬT 4 — Thông báo lỗi bằng tiếng Việt thuần:**
Ghi đúng câu chữ người dùng nhìn thấy trên màn hình.
TUYỆT ĐỐI KHÔNG viết ERR_001, ERR_NULL hay mã lỗi kỹ thuật.

**LUẬT 5 — Chỉ test lỗi thực tế sinh viên gặp:**
Cho phép: bỏ trống trường bắt buộc, nhập số âm, trùng mã, chọn sai nghiệp vụ.
CẤM: bịa lỗi tràn RAM, quá tải CPU, mất kết nối mạng, SQL Injection.

**LUẬT 6 — Mô tả UI ngắn gọn, không giải thích trong ngoặc:**
Viết: "chữ màu xanh lá", "nút mờ đi", "hộp thoại hiện ra".
CẤM: "màu xanh lá (Success)", "disabled (không dùng được)".

**LUẬT 7 — Precondition cụ thể:**
Ghi rõ dữ liệu cần có sẵn trong hệ thống trước khi chạy TC.
Nếu không có gì đặc biệt thì ghi: "Hệ thống đang chạy bình thường."

---

## PHẦN 5 — GỢI Ý PHÂN VÙNG TC (để AI không bịa lung tung)

Viết TC lần lượt theo từng khu vực, mỗi khu vực 25-35 TC:

| Khu vực | Nội dung gợi ý |
|---|---|
| **1.1 Danh sách SP** | Mở màn hình, bộ lọc 7 nút, tìm kiếm, màu trạng thái, chọn dòng, cảnh báo chuyển dòng |
| **1.2 Tab Thông tin chung** | Thêm mới trống, tiền tố mã theo từng loại (8 loại), validate bắt buộc, trùng mã, chỉ tiền tố, ảnh sai định dạng, khóa Mã/Loại khi sửa, checkbox tự lock, giá tham khảo cập nhật, Ctrl+S, Esc |
| **1.3 Tab Bảng giá** | Thêm dòng giá, xóa dòng, chống chồng lấp cùng loại, khác loại OK, ngày đến trước ngày từ, giá âm/0, cột cho thuê ẩn/hiện, giá tham khảo cập nhật |
| **1.4 Tab Quy đổi ĐVT** | Thêm dòng, hệ số âm, hệ số 0, hệ số trống, giá tự tính vs giá ấn định, xóa dòng |
| **1.5 Tab Cấu hình vé** | Tab ẩn khi không phải Vé, kích hoạt quyền, vé trò chơi giới hạn 1 dòng, vé vào khu nhiều dòng OK, lưới trống khi đã kích hoạt |
| **1.6 Tab F&B/BOM** | Tab ẩn khi không phải F&B, thêm BOM, chặn chọn SP Vé làm nguyên liệu |
| **1.7 Xóa SP** | Xóa thành công, hủy xóa, SP có tồn kho, SP trong đơn hàng |

---

## PHẦN 6 — MẪU 3 TC ĐỂ AI HIỂU FORMAT

```csv
"REQ ID","TC ID","Title","Est. (phút)","Test Type","Area","Precondition","Procedure / Steps","Expected Results","Priority","Result","Change TC","Author","Remark"
"UC_SP_001","TC_SP_001","Mở màn hình danh sách sản phẩm thành công","5","Functional","1.1 Danh sách SP","Tài khoản admin đang hoạt động, hệ thống bình thường.","Bước 1. Mở phần mềm Đại Nam POS.
Bước 2. Đăng nhập tài khoản Quản lý (tên đăng nhập: admin).
Bước 3. Trên menu bên trái chọn mục Danh mục.
Bước 4. Chọn mục Hàng hóa và Dịch vụ.","Bước 1. Màn hình đăng nhập xuất hiện.
Bước 2. Đăng nhập thành công, vào màn hình chính.
Bước 3. Menu mở rộng hiển thị mục con.
Bước 4. Màn hình danh sách SP mở ra, lưới bên trái, panel chi tiết bên phải đang ẩn.","High","","","Bùi Trí Nguyên",""
"UC_SP_001","TC_SP_002","Báo lỗi khi Mã SP trùng với sản phẩm đã có","5","Negative","1.2 Tab thông tin chung","Trong hệ thống đã tồn tại sản phẩm có mã VE_001.","Bước 1. Mở phần mềm Đại Nam POS.
Bước 2. Đăng nhập tài khoản Quản lý (admin).
Bước 3. Vào Danh mục, chọn Hàng hóa và Dịch vụ, nhấn nút Thêm mới.
Bước 4. Nhập vào ô Mã SP giá trị: VE_001.
Bước 5. Nhập vào ô Tên SP giá trị: Vé thử trùng mã.
Bước 6. Chọn Loại SP: Vé vào khu, chọn ĐVT: Lượt.
Bước 7. Nhấn nút Lưu.","Bước 1. Màn hình đăng nhập xuất hiện.
Bước 2. Vào màn hình chính.
Bước 3. Panel thêm mới hiện ra, tất cả ô trống.
Bước 4. Ô Mã SP hiển thị VE_001.
Bước 5. Ô Tên SP hiển thị Vé thử trùng mã.
Bước 6. Loại SP và ĐVT được chọn thành công.
Bước 7. Hệ thống dừng lưu, không tạo SP mới. Ô Mã SP được focus. Thông báo xuất hiện: Mã sản phẩm đã tồn tại: VE_001.","High","","","Bùi Trí Nguyên",""
"UC_SP_001","TC_SP_003","Vé trò chơi chỉ được thêm 1 dòng khu vực quẹt vé","5","Negative","1.5 Tab cấu hình vé","Đang ở màn hình Thêm mới SP loại Vé trò chơi, đã sang Tab Cấu hình vé.","Bước 1. Mở phần mềm Đại Nam POS.
Bước 2. Đăng nhập tài khoản Quản lý (admin).
Bước 3. Vào Danh mục, chọn Hàng hóa và Dịch vụ, nhấn Thêm mới.
Bước 4. Chọn Loại SP: Vé trò chơi.
Bước 5. Chuyển sang Tab Cấu hình vé, tích ô Kích hoạt quyền truy cập.
Bước 6. Nhấn Thêm dòng, chọn khu vực: Khu Trượt Nước.
Bước 7. Nhấn Thêm dòng lần thứ hai.","Bước 1. Màn hình đăng nhập xuất hiện.
Bước 2. Vào màn hình chính.
Bước 3. Panel thêm mới hiện ra.
Bước 4. Tab thứ 4 đổi tên thành Cấu hình vé.
Bước 5. Lưới quyền truy cập hiện ra.
Bước 6. Dòng đầu tiên được thêm vào lưới.
Bước 7. Hệ thống ngăn chặn, thông báo xuất hiện: Vé trò chơi chỉ được cấu hình 1 khu vực quẹt vé. Lưới vẫn chỉ có 1 dòng.","High","","","Bùi Trí Nguyên",""
```

---

## PHẦN 7 — LỆNH GỌI AI

Sau khi dán prompt này, gõ:

```
Bây giờ hãy viết 30 TC cho khu vực 1.1 Danh sách SP, bắt đầu từ TC_SP_001.
Xuất ra CSV thuần, không giải thích thêm.
```

Sau khi nhận xong, gõ tiếp:

```
Tiếp tục viết 30 TC cho khu vực 1.2 Tab Thông tin chung, bắt đầu từ TC_SP_031.
```

Tiếp tục theo từng khu vực:
- 1.3 Tab Bảng giá → bắt từ TC_SP_061
- 1.4 Tab Quy đổi ĐVT → bắt từ TC_SP_091
- 1.5 Tab Cấu hình vé → bắt từ TC_SP_111
- 1.6 Tab F&B/BOM → bắt từ TC_SP_131
- 1.7 Xóa SP → bắt từ TC_SP_151
