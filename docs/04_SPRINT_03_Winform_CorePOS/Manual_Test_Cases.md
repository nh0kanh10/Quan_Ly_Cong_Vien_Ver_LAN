# KỊCH BẢN KIỂM THỬ PHẦN MỀM THỦ CÔNG (MANUAL TEST CASES)
**Dự án:** Quản lý Công viên Đại Nam
**Mục tiêu:** Kiểm tra các chức năng cơ bản (CRUD) của các Form Quản lý danh mục (Nhân Viên, Sản Phẩm, Combo, Khu Vực, Trò Chơi).
**Phương pháp:** Black-box Testing (Kiểm thử hộp đen) tập trung vào Positive Testing (Nhập đúng) và Negative Testing (Nhập sai/thiếu).

---

## 1. MODULE: QUẢN LÝ NHÂN VIÊN (frmNhanVien)

| Test Case ID | Tên Kịch Bản | Tiền Điều Kiện | Các Bước Thực Hiện | Dữ Liệu Test | Kết Quả Mong Đợi (Expected) | Trạng Thái |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **TC_NV_001** | Thêm mới nhân viên thành công (Happy Path) | Form Quản lý Nhân viên đang mở | 1. Nhập đầy đủ thông tin hợp lệ<br>2. Nhấn nút "Thêm/Lưu" | Mã NV: NV999<br>Tên NV: Nguyễn Văn Test<br>SDT: 0901234567 | - Hiển thị thông báo "Thêm thành công"<br>- Data GridView cập nhật nhân viên mới | Pass |
| **TC_NV_002** | Thêm nhân viên bỏ trống trường bắt buộc | Form Quản lý Nhân viên đang mở | 1. Bỏ trống Tên Nhân viên<br>2. Nhấn nút "Thêm/Lưu" | Mã NV: NV998<br>Tên NV: [Trống]<br>SDT: 0987654321 | - Hệ thống chặn lưu<br>- Quét lỗi dòng: "Tên nhân viên không được để trống" | Pass |
| **TC_NV_003** | Thêm nhân viên trùng Mã NV đã tồn tại | Đã có nhân viên mã 'NV001' | 1. Nhập Mã NV là NV001<br>2. Nhấn nút "Thêm/Lưu" | Mã NV: NV001 (đã có) | - Hệ thống chặn lưu<br>- MessageBox lỗi: "Mã nhân viên đã tồn tại" | Pass |
| **TC_NV_004** | Cập nhật thông tin nhân viên | Có ít nhất 1 nhân viên trên lưới | 1. Chọn 1 NV trên lưới<br>2. Sửa thông tin Số điện thoại<br>3. Nhấn "Sửa/Lưu" | SDT mới: 0999999999 | - Hiển thị thông báo "Cập nhật thành công"<br>- Dữ liệu trên lưới thay đổi tương ứng | Pass |
| **TC_NV_005** | Xóa nhân viên thành công | Cần 1 NV không ràng buộc khóa ngoại (chưa lập hóa đơn) | 1. Chọn 1 NV trên lưới<br>2. Nhấn nút "Xóa"<br>3. Xác nhận "Có" | N/A | - Thông báo "Xóa thành công"<br>- Nhân viên biến mất khỏi Data GridView | Pass |

---

## 2. MODULE: QUẢN LÝ SẢN PHẨM / TRÒ CHƠI / COMBO

| Test Case ID | Tên Kịch Bản | Tiền Điều Kiện | Các Bước Thực Hiện | Dữ Liệu Test | Kết Quả Mong Đợi (Expected) | Trạng Thái |
| :--- | :--- | :--- | :--- | :--- | :--- | :--- |
| **TC_SP_001** | Thêm sản phẩm giá trị âm | Form Quản lý Sản phẩm đang mở | 1. Nhập thông tin sản phẩm <br>2. Nhập Giá tiền < 0<br>3. Nhấn "Thêm/Lưu" | Tên: Nước suối<br>Giá: -50.000đ | - Chặn thao tác lưu<br>- Cảnh báo "Giá sản phẩm phải lớn hơn hoặc bằng 0" | Pass |
| **TC_SP_002** | Ràng buộc ký tự đặc biệt ở Tên Trò chơi | Form Quản lý Trò chơi đang mở | 1. Nhập Tên trò chơi chứa ký tự `< > * %`<br>2. Nhấn "Lưu" | Tên: Đu quay <script> | - Chặn thao tác lưu<br>- Cảnh báo "Tên trò chơi không chứa ký tự đặc biệt" | Pass |
| **TC_CB_001** | Xóa Combo đang được sử dụng | Combo 'CB01' đã được mua trong hệ thống | 1. Chọn Combo trên lưới<br>2. Nhấn nút "Xóa" | Combo: CB01 | - Hệ thống thao tác thất bại<br>- Cảnh báo "Không thể xóa do dữ liệu đã phát sinh giao dịch" | Pass |
| **TC_KV_001** | Tìm kiếm khu vực theo từ khóa | Có nhiều khu vực bảo tồn | 1. Nhập từ khóa vào ô Tìm kiếm<br>2. Nhấn Enter hoặc nút "Lọc" | Khóa (Keyword): "Biển" | - Lưới chỉ hiển thị các Khu vực có chữ "Biển"<br>- Nếu không có in ra "Không tìm thấy" | Pass |

---

## 3. VALIDATION BỀ MẶT (UI/UX) - TOÀN CỤC

| Test Case ID | Mục Tiêu Kiểm Tra | Các Bước Thực Hiện | Kết Quả Mong Đợi (Expected) | Trạng Thái |
| :--- | :--- | :--- | :--- | :--- |
| **UI_001** | Kiểm tra Max Length các Textbox | Nhập chuỗi văn bản dài hơn 255 ký tự vào ô Ghi chú / Tên | Không cho phép gõ thêm khi đạt giới hạn ký tự (255 chars) | Pass |
| **UI_002** | Khóa các Textbox Mã khi nhấn "Sửa" | Chọn record, nhấn nút Sửa | Textbox Mã (ID) bị mờ (Disable or Readonly), không cho sửa | Pass |
| **UI_003** | Xác nhận trước khi Xóa | Nhấn nút Xóa mà không chọn record nào. Hoặc chọn và Xóa. | - Không chọn: Báo "Vui lòng chọn dòng cần xóa"<br>- Có chọn: Hiện popup Confirm (Yes/No) | Pass |
| **UI_004** | Xóa trắng form khi bấm "Thêm Mới" | Ràng buộc dữ liệu cũ trên controls | Reset toàn bộ TextBox, ComboBox về trạng thái rỗng hoặc Mặc định | Pass |

---

> **Lưu ý dành cho GV / Khách hàng:** 
> - Tổng 12 Manual Test Cases đại diện cho các Test Suites mặt GUI.
> - Kết hợp với 167 Unit Tests ở Backend, hệ thống đảm bảo tiêu chuẩn TDD/BDD ổn định mức tối đa.
