# TEST CASE – Sprint 2

**Mã dự án**: SD001  
**Mã tài liệu**: SD001_TC_Sprint2_v2.0  
**Họ tên**: Nguyễn Tấn Nhị  
**Đề tài**: Hệ thống Quản lý Công viên Vui chơi Giải trí  
**Phiên bản**: 2.0  
**Ngày cập nhật**: 25/03/2026  

---

## RECORD OF CHANGE

*A - Added, M - Modified, D - Deleted*

| Effective Date | Changed Items | A/M/D | Change Description | New Version |
|:---|:---|:---:|:---|:---:|
| 25/03/2026 | First release | A | Khởi tạo 858 Test Case Sprint 2 | 2.0 |

---

## 1. QUẢN LÝ NHÂN VIÊN (Employee Management)

| TC ID | Title | Estimation (mins) | Test Type | Area | Procedure / Steps | Expected Results | Priority | RESULT |
|:---|:---|:---:|:---|:---|:---|:---|:---:|:---:|
| **NV_01** | Xem DS nhân viên | 1 | UI | Nhân viên | 1. Mở menu "Quản lý nhân viên" | 1. Grid hiển thị danh sách NV. 2. Các cột đúng header: Mã NV, Họ tên, SĐT... | High | PENDING |
| **NV_02** | Form rỗng | 2 | Logic | Nhân viên | 1. Xóa toàn bộ NV trong DB. 2. Mở form | 1. Grid không hiển thị dữ liệu, không văng lỗi. | Medium | PENDING |
| **NV_03** | Tự sinh Mã Code | 1 | Functional | Nhân viên | 1. Nhấn nút "Làm mới" | 1. Ô Mã Code tự động nhảy mã tiếp theo (ví dụ NV015). | High | PENDING |
| **NV_04** | Thêm NV hợp lệ | 5 | Functional | Nhân viên | 1. Nhập Họ tên: "Nguyễn Văn A" 2. Nhập CCCD: "079123456789" 3. Nhấn "Thêm" | 1. Thêm thành công. 2. Thông báo "Lưu thành công". 3. Grid cập nhật dòng mới. | High | PENDING |
| **NV_05** | Bỏ trống Họ tên | 2 | Validation | Nhân viên | 1. Để trống Họ tên. 2. Nhập các thông tin khác. 3. Nhấn "Thêm" | 1. Thông báo lỗi: "Họ tên không được để trống!" | High | PENDING |
| **NV_06** | Họ tên quá dài | 2 | Validation | Nhân viên | 1. Nhập Họ tên > 100 ký tự. 2. Nhấn "Thêm" | 1. Thông báo lỗi: "Họ tên quá dài (tối đa 100 ký tự)!" | Medium | PENDING |
| **NV_07** | Bỏ trống Mã Code | 2 | Validation | Nhân viên | 1. Xóa nội dung ô Mã Code. 2. Nhấn "Thêm" | 1. Thông báo lỗi: "Mã nhân viên không được để trống!" | High | PENDING |
| **NV_08** | Trùng Mã Code | 3 | Validation | Nhân viên | 1. Nhập Mã Code đã tồn tại trong hệ thống. 2. Nhấn "Thêm" | 1. Thông báo lỗi: "Mã nhân viên đã tồn tại!" | High | PENDING |
| **NV_09** | CCCD quá dài | 2 | Validation | Nhân viên | 1. Nhập số CCCD 13 chữ số. 2. Nhấn "Thêm" | 1. Thông báo lỗi: "CCCD quá dài (tối đa 12 ký tự)!" | High | PENDING |
| **NV_10** | SĐT quá dài | 2 | Validation | Nhân viên | 1. Nhập SĐT 16 số. 2. Nhấn "Thêm" | 1. Thông báo lỗi: "Số điện thoại quá dài (tối đa 15 ký tự)!" | Medium | PENDING |
| **NV_11** | Chức vụ bỏ trống | 2 | Validation | Nhân viên | 1. Xóa chọn trong Cbo Chức vụ. 2. Nhấn "Thêm" | 1. Thông báo lỗi: "Chức vụ không được để trống!" | High | PENDING |
| **NV_12** | Ngày vào làm lỗi | 2 | Validation | Nhân viên | 1. Nhập ngày vào làm là giá trị mặc định hệ thống (NULL). 2. Nhấn "Thêm" | 1. Thông báo lỗi: "Ngày vào làm không hợp lệ!" | Medium | PENDING |
| **NV_13** | Chọn ảnh nhân viên | 2 | UI/Functional | Nhân viên | 1. Nhấn nút "Chọn ảnh". 2. Chọn một tệp .jpg | 1. Ảnh hiển thị lên PictureBox xem trước. | High | PENDING |
| **NV_14** | Lưu ảnh vào thư mục | 3 | IO | Nhân viên | 1. Thực hiện thêm NV có ảnh. 2. Kiểm tra thư mục `bin/Debug/Images/NhanVien/` | 1. Tồn tại file ảnh mới với tên là MaCode của NV đó. | High | PENDING |
| **NV_15** | Load ảnh khi chọn Grid | 2 | UI | Nhân viên | 1. Chọn một dòng NV có ảnh trên Grid. | 1. Ảnh của NV đó hiển thị đúng lên PictureBox. | Medium | PENDING |
| **NV_16** | Load thông tin lên Form | 2 | UI | Nhân viên | 1. Click chọn row trên Grid. | 1. Toàn bộ thông tin (Tên, CCCD, SĐT, Cbo...) được nạp đúng vào các control nhập liệu. | High | PENDING |
| **NV_17** | Sửa NV hợp lệ | 4 | Functional | Nhân viên | 1. Chọn NV. 2. Sửa SĐT. 3. Nhấn "Sửa" | 1. Thông báo thành công. 2. Dữ liệu trên Grid và DB thay đổi. | High | PENDING |
| **NV_18** | Sửa khi chưa chọn | 2 | Validation | Nhân viên | 1. Nhấn "Sửa" khi chưa click dòng nào. | 1. Thông báo: "Vui lòng chọn nhân viên cần sửa!" | Medium | PENDING |
| **NV_19** | Sửa Mã Code trùng | 3 | Validation | Nhân viên | 1. Chọn NV A. 2. Sửa Mã Code thành mã của NV B. 3. Nhấn "Sửa" | 1. Thông báo lỗi: "Mã nhân viên đã tồn tại!" | High | PENDING |
| **NV_20** | Cập nhật ảnh NV | 3 | Functional | Nhân viên | 1. Chọn NV. 2. Chọn ảnh mới. 3. Nhấn "Sửa" | 1. Ảnh cũ trong thư mục bị ghi đè/thay thế bởi ảnh mới. | Medium | PENDING |
| **NV_21** | Xóa NV hợp lệ | 3 | Functional | Nhân viên | 1. Chọn NV chưa có hóa đơn. 2. Nhấn "Xóa". 3. Chọn "Yes" | 1. Xóa thành công khỏi Grid và DB. | High | PENDING |
| **NV_22** | Hủy xác nhận xóa | 1 | Functional | Nhân viên | 1. Nhấn "Xóa". 2. Chọn "No" | 1. Không có gì thay đổi. | Low | PENDING |
| **NV_23** | Xóa NV có hóa đơn | 4 | Logic | Nhân viên | 1. Chọn NV đã từng lập hóa đơn bán vé. 2. Nhấn "Xóa" | 1. Thông báo lỗi (Trigger/Logic chặn): Không thể xóa nhân viên đã có giao dịch. | Critical | PENDING |
| **NV_24** | Tìm kiếm theo Tên | 2 | Functional | Nhân viên | 1. Nhập "Thị" vào ô tìm kiếm. 2. Nhấn "Tìm" | 1. Grid chỉ hiện các NV có tên chứa chữ "Thị". | High | PENDING |
| **NV_25** | Tìm kiếm theo Mã | 2 | Functional | Nhân viên | 1. Nhập "NV005" vào ô tìm kiếm. 2. Nhấn "Tìm" | 1. Grid hiện duy nhất NV có mã NV005. | High | PENDING |
| **NV_26** | Lọc theo Chức vụ | 2 | Functional | Nhân viên | 1. Chọn "Kỹ thuật" tại Cbo lọc. | 1. Grid chỉ hiện các NV có chức vụ Kỹ thuật. | High | PENDING |
| **NV_27** | Kết hợp Tìm + Lọc | 3 | Functional | Nhân viên | 1. Nhập tên "Nam". 2. Chọn Chức vụ "Quản lý". | 1. Kết quả thỏa mãn đồng thời cả 2 điều kiện. | Medium | PENDING |
| **NV_28** | Làm mới Form | 1 | UI | Nhân viên | 1. Nhấn nút "Làm mới" | 1. Các ô nhập liệu trống. 2. Ngày về mặc định. 3. MaCode nạp mới. 4. Grid load đủ. | High | PENDING |
| **NV_29** | Tab Order | 1 | UI | Nhân viên | 1. Nhấn phím Tab liên tục từ Tên -> CCCD -> SĐT... | 1. Con trỏ di chuyển đúng thứ tự logic từ trên xuống dưới, trái qua phải. | Low | PENDING |
| **NV_30** | Email sai định dạng | 2 | Validation | Nhân viên | 1. Nhập Email: "abc#gmail.com" 2. Nhấn "Thêm" | 1. (Tùy code) Thông báo lỗi định dạng Email nếu có check. | Medium | PENDING |
| **NV_31** | SĐT chứa chữ cái | 2 | Validation | Nhân viên | 1. Nhập SĐT: "090abc123" 2. Nhấn "Thêm" | 1. Báo lỗi hoặc tự động xóa chữ (tùy control). | Medium | PENDING |
| **NV_32** | Ngày sinh tương lai | 2 | Validation | Nhân viên | 1. Chọn ngày sinh > Ngày hiện tại. 2. Nhấn "Thêm" | 1. Thông báo lỗi: Ngày sinh không hợp lệ. | Medium | PENDING |
| **NV_33** | Giới tính mặc định | 1 | UI | Nhân viên | 1. Mở form/Làm mới | 1. Giới tính mặc định chọn "Nam". | Low | PENDING |
| **NV_34** | Trạng thái mặc định | 1 | UI | Nhân viên | 1. Mở form/Làm mới | 1. Trạng thái mặc định "Đang làm việc". | Low | PENDING |
| **NV_35** | Thoát form | 1 | Functional | Nhân viên | 1. Nhấn nút "Thoát" | 1. Đóng form Nhân viên, quay lại Main Dashboard. | High | PENDING |
| **NV_36** | Quyền quản lý Form | 2 | Security | Nhân viên | 1. Đăng nhập với User thường. 2. Mở menu Nhân viên. | 1. (Nếu có phân quyền) Thông báo không đủ quyền hoặc Ẩn menu này. | High | PENDING |
| **NV_37** | Ẩn cột kỹ thuật trên Grid | 1 | UI | Nhân viên | 1. Xem Grid Nhân viên. | 1. Các cột như MaNhanVien, HinhAnh không hiển thị trên lưới (đã ẩn code). | Medium | PENDING |
| **NV_38** | Định dạng tiêu đề cột | 1 | UI | Nhân viên | 1. Xem Grid. | 1. Các cột hiển thị tên Tiếng Việt có dấu (Họ tên, Chức vụ...). | Medium | PENDING |
| **NV_39** | Tự động viết hoa Tên | 2 | Functional | Nhân viên | 1. Nhập "nguyen văn a". 2. Nhấn Lưu/Rời ô. | 1. (Tùy code) Tự động chuyển thành "Nguyễn Văn A" nếu có xử lý String. | Low | PENDING |
| **NV_40** | Kiểm tra CCCD 12 số | 2 | Validation | Nhân viên | 1. Nhập CCCD 9 số. | 1. Chấp nhận (hoặc báo lỗi nếu yêu cầu cứng 12 số). | Medium | PENDING |

---

## 2. QUẢN LÝ KHÁCH HÀNG (Customer Management)

| TC ID | Title | Estimation (mins) | Test Type | Area | Procedure / Steps | Expected Results | Priority | RESULT |
|:---|:---|:---:|:---|:---|:---|:---|:---:|:---:|
| **KH_01** | Hiển thị DS Khách hàng | 1 | UI | Khách hàng | 1. Mở form Khách hàng | 1. Grid hiện đầy đủ khách hàng trong hệ thống. | High | PENDING |
| **KH_02** | Thêm khách hàng hợp lệ | 4 | Functional | Khách hàng | 1. Nhập Họ tên: "Trần Minh" 2. Nhập SĐT: "0908111222" 3. Nhấn "Thêm" | 1. Thành công. 2. DataGridView cập nhật khách mới. | High | PENDING |
| **KH_03** | Bỏ trống Họ tên | 2 | Validation | Khách hàng | 1. Để trống Họ tên. 2. Nhấn "Thêm" | 1. Thông báo lỗi: "Vui lòng nhập họ tên khách hàng" | High | PENDING |
| **KH_04** | Tên quá 100 ký tự | 2 | Validation | Khách hàng | 1. Nhập Tên cực dài (>100 ký tự). | 1. Thông báo lỗi: "Họ tên không được vượt quá 100 ký tự" | Medium | PENDING |
| **KH_05** | SĐT sai định dạng (Chữ) | 2 | Validation | Khách hàng | 1. Nhập SĐT: "0908ABC123" | 1. Thông báo hoặc chặn nhập nếu dùng MaskedTextBox. | High | PENDING |
| **KH_06** | SĐT không đủ 10 số | 2 | Validation | Khách hàng | 1. Nhập SĐT: "0908123" 2. Nhấn "Thêm" | 1. Thông báo: "Số điện thoại phải gồm 10 chữ số, bắt đầu bằng 0" | High | PENDING |
| **KH_07** | SĐT không bắt đầu bằng 0 | 2 | Validation | Khách hàng | 1. Nhập SĐT: "9081234567" | 1. Thông báo: "Số điện thoại phải gồm 10 chữ số, bắt đầu bằng 0" | High | PENDING |
| **KH_08** | Trùng Số điện thoại | 3 | Validation | Khách hàng | 1. Nhập SĐT đã có của khách khác. 2. Nhấn "Thêm" | 1. Thông báo: "Số điện thoại đã được đăng ký bởi khách hàng khác" | Critical | PENDING |
| **KH_09** | Email sai định dạng | 2 | Validation | Khách hàng | 1. Nhập Email: "test.com" | 1. Thông báo: "Email không đúng định dạng" | Medium | PENDING |
| **KH_10** | Ngày sinh tương lai | 2 | Validation | Khách hàng | 1. Chọn ngày sinh > Today. | 1. Thông báo: "Ngày sinh không hợp lệ" | Medium | PENDING |
| **KH_11** | Sửa thông tin khách | 3 | Functional | Khách hàng | 1. Chọn khách. 2. Sửa địa chỉ. 3. Nhấn "Sửa" | 1. Thông báo thành công. Grid nạp lại dữ liệu mới. | High | PENDING |
| **KH_12** | Sửa sang SĐT đã tồn tại | 3 | Validation | Khách hàng | 1. Chọn khách A. 2. Đổi SĐT thành SĐT của khách B. 3. Nhấn "Sửa" | 1. Thông báo: "Số điện thoại đã được đăng ký bởi khách hàng khác" | High | PENDING |
| **KH_13** | Sửa khi chưa chọn dòng | 2 | Validation | Khách hàng | 1. Nhấn nút "Sửa" khi chưa click vào List. | 1. Thông báo: "Vui lòng chọn khách hàng cần sửa!" | Medium | PENDING |
| **KH_14** | Xóa khách hàng | 3 | Functional | Khách hàng | 1. Chọn khách. 2. Nhấn "Xóa". | 1. Thông báo xác nhận. 2. Đồng ý -> Xóa thành công. | High | PENDING |
| **KH_15** | Xóa khách đã có đơn hàng | 4 | Logic | Khách hàng | 1. Chọn khách VIP đã mua nhiều vé. 2. Nhấn "Xóa" | 1. (Tùy thiết kế) Chặn xóa hoặc thông báo lỗi ràng buộc SQL. | Critical | PENDING |
| **KH_16** | Tìm kiếm theo Tên khách | 2 | Functional | Khách hàng | 1. Nhập "Hoàng" vào ô tìm kiếm. | 1. Grid hiển thị những người tên "Hoàng". | High | PENDING |
| **KH_17** | Tìm kiếm theo SĐT | 2 | Functional | Khách hàng | 1. Nhập 4 số cuối SĐT. | 1. Grid lọc đúng khách hàng khớp SĐT. | High | PENDING |
| **KH_18** | Lọc theo Giới tính | 2 | Functional | Khách hàng | 1. Chọn "Nữ" trong combo lọc. | 1. Grid chỉ hiện khách hàng Nữ. | High | PENDING |
| **KH_19** | Làm mới dữ liệu | 1 | UI/Functional | Khách hàng | 1. Nhấn "Làm mới" | 1. Xóa trắng form. 2. Load lại List đầy đủ. | Medium | PENDING |
| **KH_20** | Kiểm tra Hiển thị Tổng chi | 2 | UI/Logic | Khách hàng | 1. Chọn khách đã mua vé. | 1. Label/TextBox "Tổng chi" hiển thị số tiền khớp DB (ví dụ 1,200,000). | High | PENDING |
| **KH_21** | Hiển thị Mã thành viên | 2 | UI | Khách hàng | 1. Xem chi tiết khách. | 1. Mã thành viên tự sinh hiển thị đúng (KH001...). | Medium | PENDING |
| **KH_22** | Định dạng số tiền Tổng chi | 1 | UI | Khách hàng | 1. Xem cột "Tổng chi tiêu" trên Grid. | 1. Hiển thị ngăn cách phần nghìn (ví dụ 1,500,000). | Low | PENDING |
| **KH_23** | Checkbox/Combo giới tính | 1 | UI | Khách hàng | 1. Kiểm tra lựa chọn giới tính. | 1. Chỉ có 2 lựa chọn: Nam, Nữ. | Low | PENDING |
| **KH_24** | Nhấn Esc để đóng | 1 | UI | Khách hàng | 1. Nhấn phím Escape khi đang ở form. | 1. Form đóng (Nếu có thiết lập KeyPreview). | Low | PENDING |
| **KH_25** | Email bỏ trống | 1 | Functional | Khách hàng | 1. Bỏ trống Email. 2. Nhấn Lưu. | 1. Lưu thành công (Email không bắt buộc). | Medium | PENDING |
| **KH_26** | Địa chỉ bỏ trống | 1 | Functional | Khách hàng | 1. Bỏ trống Địa chỉ. 2. Nhấn Lưu. | 1. Lưu thành công. | Medium | PENDING |
| **KH_27** | Tìm khách không tồn tại | 2 | Functional | Khách hàng | 1. Nhập chuỗi rác "XYZ123". 2. Nhấn Tìm. | 1. Grid hiển thị rỗng, không lỗi crash. | Medium | PENDING |
| **KH_28** | SĐT chứa khoảng trắng | 2 | Functional | Khách hàng | 1. Nhập "090 123 456". 2. Lưu. | 1. (Tùy BUS) Tự động cắt khoảng trắng để lưu "090123456". | Low | PENDING |
| **KH_29** | Kiểm tra phím nóng | 2 | UI | Khách hàng | 1. Nhấn Alt + T (Thêm). | 1. Thực hiện lệnh thêm (nếu form có Shortcut key). | Low | PENDING |
| **KH_30** | Đăng ký nhanh từ bán vé | 5 | Integration | Khách hàng | 1. Tại Form Bán vé, khách chưa có SĐT. 2. Nhấn nút "Đăng ký nhanh". | 1. Mở popup/form Khách hàng. Sau khi lưu, thông tin nạp ngược về Form Bán vé. | High | PENDING |
| **KH_31** | Hiển thị ngày sinh | 1 | UI | Khách hàng | 1. Xem cột ngày sinh trên Grid. | 1. Định dạng dd/MM/yyyy. | Low | PENDING |
| **KH_32** | Tên có ký tự đặc biệt | 2 | Validation | Khách hàng | 1. Nhập tên: "Hùng @#$" | 1. Chấp nhận hoặc báo lỗi (Tùy yêu cầu nghiệp vụ). | Low | PENDING |
| **KH_33** | Thông báo xác nhận thoát | 1 | UI | Khách hàng | 1. Nhấn Thoát. | 1. Hiển thị: "Bạn có muốn thoát không?". | Medium | PENDING |
| **KH_34** | Focus vào ô Tên đầu tiên | 1 | UI | Khách hàng | 1. Nhấn Làm mới. | 1. Con trỏ chuột tự động focus vào ô Họ tên. | Low | PENDING |
| **KH_35** | Kiểm tra Scrollbar Grid | 1 | UI | Khách hàng | 1. Danh sách > 100 khách. | 1. Thanh cuộn dọc Grid hoạt động mượt mà. | Low | PENDING |

---

## 3. QUẢN LÝ DỊCH VỤ / F&B (Service Management)

| TC ID | Title | Estimation (mins) | Test Type | Area | Procedure / Steps | Expected Results | Priority | RESULT |
|:---|:---|:---:|:---|:---|:---|:---|:---:|:---:|
| **DV_01** | Xem DS Dịch vụ | 1 | UI | Dịch vụ | 1. Mở menu "Quản lý dịch vụ" | 1. Grid hiển thị danh sách món ăn/tiện ích. | High | PENDING |
| **DV_02** | Thêm dịch vụ hợp lệ | 4 | Functional | Dịch vụ | 1. Nhập Tên: "Pepsi" 2. Chọn Danh mục: Đồ uống 3. Nhập giá: 20000 4. Nhấn "Thêm" | 1. Thêm thành công. 2. MaCode tự sinh (Fxxx/Dxxx). | High | PENDING |
| **DV_03** | Tên dịch vụ bỏ trống | 2 | Validation | Dịch vụ | 1. Để trống Tên. 2. Nhấn "Thêm" | 1. Thông báo lỗi: "Tên dịch vụ không được trống" | High | PENDING |
| **DV_04** | Chưa chọn danh mục | 2 | Validation | Dịch vụ | 1. Để trống Danh mục (SelectedIndex = -1). 2. Nhấn "Thêm" | 1. Thông báo lỗi: "Vui lòng chọn danh mục" | High | PENDING |
| **DV_05** | Giá bán âm | 2 | Validation | Dịch vụ | 1. Nhập giá: -10000. 2. Nhấn "Thêm" | 1. Thông báo lỗi: "Giá bán không hợp lệ" | High | PENDING |
| **DV_06** | Giá bán không phải số | 2 | Validation | Dịch vụ | 1. Nhập giá: "Hai mươi ngàn" | 1. Báo lỗi chuyển đổi kiểu dữ liệu hoặc chặn nhập. | Medium | PENDING |
| **DV_07** | Trùng Mã Code dịch vụ | 3 | Validation | Dịch vụ | 1. Nhập cưỡng bức Mã Code đã có. 2. Nhấn Thêm | 1. Thông báo: "Mã code dịch vụ đã tồn tại" | High | PENDING |
| **DV_08** | Phân bổ Khu vực (NULL) | 2 | Functional | Dịch vụ | 1. Chọn "-- Toàn công viên --" tại combo Khu vực. 2. Lưu | 1. Dịch vụ được bán tại mọi quầy, MaKhuVuc trong DB = NULL. | Medium | PENDING |
| **DV_09** | Phân bổ Khu vực cụ thể | 2 | Functional | Dịch vụ | 1. Chọn "Khu Nước" tại combo Khu vực. 2. Lưu | 1. Dịch vụ chỉ khả dụng tại quầy thuộc Khu Nước. | High | PENDING |
| **DV_10** | Hiển thị Đơn vị tính | 1 | UI | Dịch vụ | 1. Nhập ĐVT: "Lon". 2. Lưu. | 1. Grid hiển thị đúng cột ĐVT là "Lon". | Low | PENDING |
| **DV_11** | Sửa thông tin dịch vụ | 3 | Functional | Dịch vụ | 1. Chọn món. 2. Sửa giá từ 20k -> 25k. 3. Nhấn "Sửa" | 1. Giá trên Grid cập nhật ngay lập tức. | High | PENDING |
| **DV_12** | Load DS Hoạt động | 2 | Logic | Dịch vụ | 1. Chuyển trạng thái sang "Ngừng bán". 2. Lưu. 3. Mở Form Bán vé. | 1. Món này không còn xuất hiện trong danh sách chọn bán. | Critical | PENDING |
| **DV_13** | Sửa khi chưa chọn | 2 | Validation | Dịch vụ | 1. Nhấn Sửa khi chưa chọn dòng. | 1. Thông báo yêu cầu chọn dịch vụ. | Medium | PENDING |
| **DV_14** | Xóa dịch vụ | 3 | Functional | Dịch vụ | 1. Chọn món. 2. Nhấn "Xóa". | 1. Hiện thông báo xác thực -> Đồng ý -> Xóa thành công. | High | PENDING |
| **DV_15** | Xóa món đã có trong Bill | 4 | Logic | Dịch vụ | 1. Chọn món "Cơm gà" đã bán nhiều lần. 2. Nhấn "Xóa" | 1. Chặn xóa để bảo toàn lịch sử hóa đơn (Báo lỗi ràng buộc). | Critical | PENDING |
| **DV_16** | Tìm kiếm theo Tên món | 2 | Functional | Dịch vụ | 1. Nhập "Gà". 2. Nhấn Tìm. | 1. Grid hiện Cơm gà, Cánh gà... | High | PENDING |
| **DV_17** | Lọc theo Danh mục | 2 | Functional | Dịch vụ | 1. Chọn "Đồ uống" tại combo lọc. | 1. Grid chỉ hiện bia, nước ngọt, cafe... | High | PENDING |
| **DV_18** | Tìm theo Mã Code F/D | 2 | Functional | Dịch vụ | 1. Nhập "F001". 2. Tìm. | 1. Hiện đúng món khớp mã. | Medium | PENDING |
| **DV_19** | Làm mới dữ liệu dịch vụ | 1 | UI/Functional | Dịch vụ | 1. Nhấn "Làm mới" | 1. Reset form. 2. Sinh MaCode mới. 3. Load Grid. | Medium | PENDING |
| **DV_20** | Format số tiền giá bán | 1 | UI | Dịch vụ | 1. Xem cột "Giá bán" trên Grid. | 1. Hiển thị dấu phân cách nghìn (ví dụ: 65,000). | Low | PENDING |
| **DV_21** | Kiểm tra Tồn kho mặc định | 1 | Functional | Dịch vụ | 1. Để trống ô số lượng tồn. 2. Thêm. | 1. DB lưu giá trị 0. | Low | PENDING |
| **DV_22** | Sức tải dữ liệu lớn | 3 | Performance | Dịch vụ | 1. Load > 500 món ăn/dịch vụ. | 1. Grid load nhanh (< 2s), cuộn mượt. | Medium | PENDING |
| **DV_23** | Combo Khu vực load động | 2 | UI/Logic | Dịch vụ | 1. Thêm 1 Khu vực mới bên Form Khu vực. 2. Quay lại Form Dịch vụ. | 1. Combo Khu vực hiển thị thêm khu vực vừa tạo. | Medium | PENDING |
| **DV_24** | Danh mục load động | 2 | UI/Logic | Dịch vụ | 1. Xem nội dung combo Danh mục. | 1. Dữ liệu lấy từ bảng NH_DanhMuc (không hardcode). | Medium | PENDING |
| **DV_25** | Trạng thái "Ngừng bán" | 2 | UI | Dịch vụ | 1. Chọn "Ngừng bán" trong combo. | 1. Cột trạng thái trên Grid hiển thị "Ngừng bán". | Medium | PENDING |
| **DV_26** | Thêm món giá = 0 | 2 | Functional | Dịch vụ | 1. Nhập tên: "Đũa muỗng thêm". 2. Giá = 0. | 1. Chấp nhận lưu (Quà tặng/Tiện ích miễn phí). | Low | PENDING |
| **DV_27** | Cắt khoảng trắng dư thừa | 2 | Functional | Dịch vụ | 1. Nhập tên: "  Cơm gà nướng  ". | 1. Hệ thống lưu thành "Cơm gà nướng". | Low | PENDING |
| **DV_28** | SĐT liên hệ quầy (Mô tả) | 2 | UI | Dịch vụ | 1. (Nếu có field) Nhập thông tin bổ sung. | 1. Hiển thị chính xác trên Grid/Form. | Low | PENDING |
| **DV_29** | Kiểm tra phím tắt F5 | 1 | Functional | Dịch vụ | 1. Nhấn F5 khi đang ở form. | 1. Thực hiện làm mới dữ liệu. | Low | PENDING |
| **DV_30** | Đăng xuất từ Form Dịch vụ | 1 | Security | Dịch vụ | 1. Nhấn nút đóng (X) hoặc Thoát. | 1. Trở về menu chính, trạng thái cũ được bảo toàn. | Medium | PENDING |
| **DV_31** | Mã Code viết hoa | 1 | UI | Dịch vụ | 1. Xem Mã Code tự sinh. | 1. Định dạng chuẩn (ví dụ: F005 - viết hoa). | Low | PENDING |
| **DV_32** | Click Header để Sort | 1 | UI | Dịch vụ | 1. Click vào cột "Tên Dịch Vụ". | 1. Danh sách sắp xếp theo A-Z hoặc Z-A. | Low | PENDING |
| **DV_33** | Chiều ngang các cột | 1 | UI | Dịch vụ | 1. Kiểm tra hiển thị. | 1. Các cột không bị ẩn mất nội dung do quá hẹp. | Low | PENDING |
| **DV_34** | Hình ảnh món ăn (DEF) | 3 | UI/Functional | Dịch vụ | 1. (Nâng cao) Gán ảnh cho món ăn. | 1. Hiển thị ảnh minh họa trên Menu bán hàng. | Low | N/A |
| **DV_35** | Thoát xác nhận (Thoát) | 1 | UI | Dịch vụ | 1. Nhấn Thoát. | 1. Hiện MessageBox xác nhận. | Medium | PENDING |
