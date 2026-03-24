# DEFECT LIST – Sprint 1

**Mã dự án**: SD001  
**Mã tài liệu**: SD001_DL_Sprint1_v1.0  
**Nhóm**: NguyenTanNhi  
**Đề tài**: Hệ thống Quản lý Công viên Vui chơi Giải trí  
**Phiên bản**: 1.0  
**Ngày tạo**: 16/03/2026  
**Tham chiếu SRS**: SD001_SRS_Sprint1_v2.0  

---

## RECORD OF CHANGE

| Effective Date | Changed Items | A/M/D | Change Description | New Version |
|----------------|--------------|-------|-------------------|-------------|
| 16/03/2026 | First release | A | Khởi tạo Defect List Sprint 1 với 50 lỗi chia đều 5 module | 1.0 |

---

## TỔNG HỢP

| Mức độ | Số lượng | Phân bổ Module |
|--------|:--------:|-----------------|
| 🔴 Critical | 8 | Khu vực (2), Trò chơi (1), Loại vé (2), Dịch vụ (2), Auth (1) |
| 🟠 Major | 15 | Khu vực (3), Trò chơi (4), Loại vé (3), Dịch vụ (3), Auth (2) |
| 🟡 Medium | 15 | Phân bổ đều các module |
| ⚪ Minor | 12 | Các lỗi UI/UX, chính tả |
| **Tổng** | **50** | 10 Defects / Nhóm chức năng cốt lõi |

---

## CHI TIẾT DEFECT CÁC NHÓM CHỨC NĂNG

### 1. Nhóm Quản lý Khu Vực (frmKhuVuc) - 10 Lỗi
| DEF_ID | Mức | Mô tả lỗi | Bước tái hiện | Kết quả thực tế | Kết quả mong đợi |
|--------|-----|-----------|--------------|-----------------|------------------|
| DEF-001 | 🔴 | Xóa khu vực đang có trò chơi, văng Exception SQL | Chọn xóa khu vực "Công viên nước" đang có trò. Nhấn YES | Crash app: `FK_TroChoi_KhuVuc constraint error` | Báo lỗi user-friendly: "Không thể xóa do còn trò chơi" |
| DEF-002 | 🔴 | Trùng tên khu vực (phân biệt hoa thường) | Tạo "Khu Rừng", sau đó tạo "khu rừng" | Vẫn lưu thành công 2 khu vực trùng tên | Dừng lại báo lỗi: "Tên khu vực đã tồn tại" |
| DEF-003 | 🟠 | Không validate tên khu vực > 100 ký tự | Nhập tên dài 150 ký tự, nhấn Lưu | Lưu thành công (bị cắt cụt ở DB) | Báo lỗi validate "Tên <= 100 ký tự" ở tầng UI |
| DEF-004 | 🟠 | Trạng thái mặc định khi thêm mới là rỗng | Nhấn nút "Làm mới", combobox trạng thái trống | Cbo rỗng không chọn gì | Phải tự chọn mặc định là "Hoạt động" |
| DEF-005 | 🟠 | Sửa mã khu vực gây mất đồng bộ khóa ngoại | Chọn sửa khu vực, sửa trường MaKV | Dữ liệu cũ bị lỗi Orphan | TextBox mã KV phải bị Disable (`ReadOnly = true`) |
| DEF-006 | 🟡 | DataGridView không resize chiều rộng cột Tên tự động | Mở form lên xem danh sách | Tên khu vực dài bị che khuất | Cột Tên KV phải set `AutoSizeMode = Fill` |
| DEF-007 | 🟡 | Ô tìm kiếm không thực hiện "Live Search" | Gõ từ khóa vào ô tìm kiếm | Phải nhấn nút Tìm mới có kết quả | Grid tự động cập nhật khi gõ phím (`TextChanged`) |
| DEF-008 | 🟡 | Nút "Thoát" đóng form không cảnh báo mất dữ liệu đang nhập dở | Đang gõ tên, nhấn thoát | Đóng liền ngay lập tức | Hiện hộp thoại hỏi "Thoát mà chưa lưu?" |
| DEF-009 | ⚪ | Nút Lưu và Sửa bị thụt lề sai trên UI | Khởi động form | Nút Lưu bị lệch 5px so với nút Sửa | Canh lề (Align) bằng nhau |
| DEF-010 | ⚪ | Sai chính tả chữ "Tình trạng" | Nhìn label góc phải | Label ghi: "Trạng thái họt động" | Phải ghi: "Trạng thái hoạt động" |

### 2. Nhóm Quản lý Trò Chơi (frmTroChoi) - 10 Lỗi
| DEF_ID | Mức | Mô tả lỗi | Bước tái hiện | Kết quả thực tế | Kết quả mong đợi |
|--------|-----|-----------|--------------|-----------------|------------------|
| DEF-011 | 🔴 | Lọc Trò chơi theo Khu Vực bị crash nếu danh sách KV rỗng | Xóa hết khu vực, mở form trò chơi | NullReferenceException tại Combobox | Báo "Vui lòng tạo Khu vực trước" hoặc để rỗng |
| DEF-012 | 🟠 | Nhập chữ vào ô Số lượng người/lượt không bắt lỗi Regex | Nhập "Mười người" vào ô Capacity | Gửi xuống BUS gây crash ép kiểu INT | Báo lỗi đỏ "Chỉ được nhập số" ngay lập tức |
| DEF-013 | 🟠 | Thời gian lượt = 0 bị từ chối | Nhập 0 vào thời gian | Báo: "Thời gian phải > 0" | Đặc tả SRS cho = 0 (trò chơi tự do). Phải cho qua |
| DEF-014 | 🟠 | Không reload Cbo Khu vực từ DB khi KV mới thêm | Mở form trò chơi để không, nhờ máy khác thêm KV mới | Cbo không tự thêm KV mới (cần tắt mở lại) | Phải có nút "Refresh" hoặc bắt sự kiện update |
| DEF-015 | 🟠 | Xóa trò chơi đang bảo trì báo lỗi SQL Parameter | Nhấn xóa trò chơi có trạng thái Bảo Trì | Lỗi thiếu param @id trong SP SQL | Xóa mềm thành công (`IsDeleted = 1`) |
| DEF-016 | 🟡 | Khi đổi Khu vực, danh sách Grid không tự filter | Chọn KV A ở Combobox filter trên lưới | Grid vẫn hiển thị toàn bộ trò chơi | Grid chỉ hiện trò chơi thuộc khu vực A |
| DEF-017 | 🟡 | Sức chứa tối đa > 1000 người không có hộp thoại cảnh báo | Nhập giới hạn 5000 người | Lưu thẳng xuống DB | Cảnh báo: "Sức chứa quá lớn, bạn có chắc?" |
| DEF-018 | 🟡 | Hình ảnh trò chơi dung lượng lớn 5MB gây nghẽn Grid | Chọn ảnh 4K cho trò chơi | Form bị đơ lag khi cuộn GridView | Phải Resize hình ảnh (Thumbnail) trước khi lưu DB |
| DEF-019 | ⚪ | Tab Order nhảy loạn xạ | Nhấn nút Tab liên tục | Trỏ nhảy từ Tên -> Nút Lưu -> Sức chứa | TabIndex theo thứ tự logic từ trên xuống |
| DEF-020 | ⚪ | Nút Xóa không màu đỏ như guideline UI | Click chọn Row | Nút Xóa vẫn màu Xanh dương mặc định | Phải set style `BackColor = Red` (Danger) |

### 3. Nhóm Quản lý Loại Vé (frmLoaiVe) - 10 Lỗi
| DEF_ID | Mức | Mô tả lỗi | Bước tái hiện | Kết quả thực tế | Kết quả mong đợi |
|--------|-----|-----------|--------------|-----------------|------------------|
| DEF-021 | 🔴 | Giá vé âm (< 0) vẫn cho lưu | Cố tình nhập giá = -50000 | Thêm vé thành công | Báo lỗi logic cực gắt: "Giá vé không thể âm" |
| DEF-022 | 🔴 | Thêm Combo: Chọn vé con nhưng số lượng 0 văng lỗi chia cho 0 | Tạo Combo, gán vé con SL = 0 | DivideByZero exception | Phải yêu cầu Số lượng >= 1 khi dồn combo |
| DEF-023 | 🟠 | Trùng loại vé (Tên + Đối tượng) không chặn | Thêm "Vé Người Lớn T7" 2 lần | Hệ thống cho lưu 2 bản ghi | Chặn UNIQUE Constraint tên vé + đối tượng |
| DEF-024 | 🟠 | Giá vé bỏ trống gán mặc định 0 | Không nhập Giá vé | Sql ghi vào 0 đ | Gắn validate rỗng bắt buộc nhập số tiền |
| DEF-025 | 🟠 | Cbo Trạng thái hiển thị Id (1,2) thay vì DisplayName | Nhìn vào Grid danh sách vé | Cột đối tượng ghi 1,2 | Phải Join table định dạng "Nlơn", "Trẻ em" |
| DEF-026 | 🟡 | Cột Thành Tiền trong bảng Combo không tự Update | Thay đổi "Số lượng" vé con trong nhóm combo | Cột Tổng giá combo không chạy phép tính | Auto update giá sau kiện event CellValueChanged |
| DEF-027 | 🟡 | Tìm kiếm vé "NL" không ra "Người lớn" | Gõ "NL" vào textbox search | Khong ra kết quả | Hỗ trợ regex tìm kiếm từ viết tắt (hoặc unmark unicode) |
| DEF-028 | 🟡 | Nút Làm mới không clear DataGridView của Combo chi tiết | Đang tạo vé Combo, nhấn Làm mới | Form sạch nhưng Grid dưới cùng vẫn kẹt vé cũ | Cần `Grid.Rows.Clear()` |
| DEF-029 | ⚪ | Font chữ Giá vé quá nhỏ trên GridView | Hiện danh sách vé ra trang chủ | Chữ "150000" font size 8 khó nhìn | Chỉnh cellStyle bold, fontsize 10, hiển thị VNĐ |
| DEF-030 | ⚪ | Tooltip của nút Hủy gây hiểu nhầm | Di chuột lên nút Thoát | Tooltip báo: "Xóa vé hiện tại" | Tooltip phải là: "Thoát chương trình" |

### 4. Nhóm Quản lý Dịch vụ F&B (frmSanPham) - 10 Lỗi
| DEF_ID | Mức | Mô tả lỗi | Bước tái hiện | Kết quả thực tế | Kết quả mong đợi |
|--------|-----|-----------|--------------|-----------------|------------------|
| DEF-031 | 🔴 | Lỗi SQL Injection trên Textbox Tên món ăn | Gõ: `'; DROP TABLE ChiTiet;--` | Gây crash SQL / DB command exception | Thay API DAL sang dạng Paramaterized Query `@ten` |
| DEF-032 | 🔴 | Sửa dịch vụ: Không cập nhật giá bán mới xuống hệ thống (chỉ update tên) | Sửa giá lon Coca từ 15k lên 20k | UI hiện 20k, mở DB coi vẫn là 15k | Update query bị lọt mất cột `DonGia` |
| DEF-033 | 🟠 | Đơn vị tính tự gõ tay dễ sai chính tả (Ly, Lon, l, lón) | Thêm đồ uống, field đơn vị tính là Textbox | DB có đủ từ: ly, Ly, lY | Chỉnh Textbox thành Combobox gợi ý (Lon, Chai, Đĩa) |
| DEF-034 | 🟠 | Mã Code không tự phát sinh tuần tự | Nhấn thêm dịch vụ mới | Mã Code hiển thị trống | Phải nhảy tự động kiểu `SP001`, `SP002`... |
| DEF-035 | 🟠 | Ngừng bán dịch vụ: Lỗi không ẩn khỏi POS | Set trạng thái "Ngừng HĐ" | POS v1 vẫn hiện danh sách mời khách mua | POS phải check `WHERE TrangThai = 1` |
| DEF-036 | 🟡 | Dịch vụ bán theo kg không có số thập phân | Nhập trọng lượng: 1.5 kg | Lỗi validate không nhận kiểu float | Sửa SQL DataType thành Float/Decimal |
| DEF-037 | 🟡 | Double click dòng không map đúng dữ liệu lên form sửa | Nháy đúp dịch vụ dòng số 2 trên DataGrid | Dữ liệu đẩy lên textbox là của dòng số 1 | Lỗi đọc nhầm biến `RowIndex` |
| DEF-038 | 🟡 | Tiêu đề Grid ghi "Column 1, Column 2" | Mở form dịch vụ | Tên cột Header không được đặt định dạng | Tùy chỉnh HeaderText = "Tên Món, Mã, Đơn Giá" |
| DEF-039 | ⚪ | Thông báo Lưu thành công che khuất màn hình | Lưu sp xong | Hiện MessageBox to chình ình bắt nhấn OK | Nên dùng Toast Notif nhỏ góc màn hình tự trôi 3s |
| DEF-040 | ⚪ | Không có màu sắc phân biệt Món Hàng Cấm | Nhìn vào ds | Món Ngừng Bán trông y chang món Đang bán | Cell style: món ngừng bán chữ màu đỏ, gạch ngang |

### 5. Nhóm Auth / Đăng nhập & Đăng ký (frmLogin) - 10 Lỗi
| DEF_ID | Mức | Mô tả lỗi | Bước tái hiện | Kết quả thực tế | Kết quả mong đợi |
|--------|-----|-----------|--------------|-----------------|------------------|
| DEF-041 | 🔴 | Bỏ qua đăng nhập bằng SQL injection `a' OR '1'='1` | Gõ password: `1'='1` | Hack vào được hệ thống với quyền admin | 100% sử dụng Entity/Param query an toàn + Hash |
| DEF-042 | 🟠 | Không mã hóa mật khẩu, lộ plaintext dưới DB | Đăng ký user mới tài khoản "demo" | Khám Database thấy mk ghi rõ chữ "123456" | Phải cài đặt thư viện Hash MD5 / BCrypt |
| DEF-043 | 🟠 | Nút Enter không focus vào hàm Login | Gõ Pass xong, phím Enter | Trỏ chuột nhảy sang ô khác, bấm nút chả có gì | Phím Enter phải trigger sự kiện Nhấn nút Đăng Nhập |
| DEF-044 | 🟠 | Hiển thị mật khẩu không bị che dấu (*) | Gõ chữ vào ô nhập Mật khẩu | Chữ cái hiện rõ mồn một cho người đứng sau thấy | Chỉnh property `PasswordChar` = * |
| DEF-045 | 🟠 | Sai pass 5 lần không khóa tài khoản | Gõ bậy pass 100 lần | Cho đăng nhập mù mờ miệt mài (Brute Force) | Quá 5 lần, block Account chờ Admin mở |
| DEF-046 | 🟡 | Label Đăng ký tài khoản bị lỗi font tiếng việt | Xem màn hình login | Chữ ghi "Đ?ng k? t?i kho?n" | Chú ý Set Font Unicode / Tahoma |
| DEF-047 | 🟡 | Form đăng nhập không nhảy ở chính giữa màn hình (Center) | Bật tool | Khởi động lên màn hình mẻ một góc ở trên | Thuộc tính `StartPosition` = CenterScreen |
| DEF-048 | 🟡 | Không lưu lịch sử Ai là người đăng nhập (CreatedBy id = null) | Log in thành công, tạo User | Session không nhớ Id người dùng đăng nhập hiện tại | Set `SessionManager.CurrentUser = loginedUser` |
| DEF-049 | ⚪ | Chọn "Lưu mật khẩu" nhưng tắt app mở lại bay sạch | Tích nút Remember | Lần sau chạy Winforms trống trơn | Implement `Settings.Default` local cache save |
| DEF-050 | ⚪ | Ảnh logo loading quá lâu bị nhòe pixel | Xem lúc app load | Ảnh bị lệch tỷ lệ, mờ | StretchImage / Zoom theo format chuẩn tỉ lệ |
