# DEFECT LIST – Sprint 2

**Mã dự án**: SD001  
**Mã tài liệu**: SD001_DL_Sprint2_v1.0  
**Nhóm**: NguyenTanNhi  
**Đề tài**: Hệ thống Quản lý Công viên Vui chơi Giải trí  
**Phiên bản**: 2.0 (Bản hoàn chỉnh)  
**Ngày tạo**: 30/03/2026  
**Tham chiếu SRS**: SD001_SRS_Sprint2_v2.0  

---

## RECORD OF CHANGE

| Effective Date | Changed Items | A/M/D | Change Description | New Version |
|----------------|--------------|-------|-------------------|-------------|
| 30/03/2026 | Bổ sung 40 lỗi | M | Tái cấu trúc Defect List Sprint 2 (10 lỗi/module) | 2.0 |

---

## TỔNG HỢP

| Mức độ | Số lượng | Vị trí chập chờn nhất |
|--------|:--------:|-----------------------|
| 🔴 Critical | 6 | Mất kết nối, Xóa nhầm Database, Tính sai tiền POS |
| 🟠 Major | 14 | Crash Image Upload, Regex sai SĐT |
| 🟡 Medium | 10 | Vấn đề UX, Lọc thiếu dữ liệu |
| ⚪ Minor | 10 | Lỗi Text alignment, Màu sắc |
| **Tổng** | **40** | |

---

## CHI TIẾT DEFECT CÁC NHÓM CHỨC NĂNG

### 1. Nhóm Quản lý Nhân viên (frmNhanVien) - 10 Lỗi
| DEF_ID | Mức | Mô tả lỗi | Bước tái hiện | Kết quả thực tế | Kết quả mong đợi |
|--------|-----|-----------|--------------|-----------------|------------------|
| DEF-S2-001 | 🔴 | Xóa NV có giao dịch bán hàng ở POS gây lỗi Constraint | Cố xóa thu ngân "Nguyễn A" đã lập bill | Lỗi FK DB văng đỏ màn hình | Bắt buộc chuyển sang trạng thái "Nghỉ việc", khóa nút Xóa |
| DEF-S2-002 | 🟠 | Up ảnh dọc từ iPhone bị lật ngang 90 độ | Chèn file ảnh Avatar chup từ ĐT dọc | Avatar NV xoay ngang trên form | Đọc mã EXIF tự động AutoRotate |
| DEF-S2-003 | 🟠 | Sắp xếp tuổi Nhân viên theo chuỗi Text (Tuổi 2 < Tuổi 10) | Click vào cột Tuổi trên DataGrid | 1, 12, 2, 23 | Bắt buộc cast về Int để xếp: 1, 2, 12, 23 |
| DEF-02-004 | 🟠 | Email không có dấu @ vẫn cho nhập | Nhập email: `abcxyz.com` | Lưu thẳng | RegEx bắt lỗi dịnh dạng email hợp lệ |
| DEF-S2-005 | 🟡 | Mã NV tự sinh bị hụt nếu có xóa (Lỗ hổng mã) | Đang có NV01, NV02. Xóa NV02, thêm mới | Mã sinh ra: NV02 gây rủi ro nhầm | Mã tự sinh chỉ tăng (NV03) |
| DEF-S2-006 | 🟡 | Form mất trắng dữ liệu gõ dở nếu nháy nhầm nút Làm mới | Nhập 1 trang profile NV, bấm nút | Wipe sạch ko chừa gì | Hiện thông báo cảnh báo "Wipe data?" |
| DEF-S2-007 | 🟡 | Lọc tìm kiếm Nhân viên bằng thẻ Chức vụ không ăn khớp | Gõ tìm họ "Lê", chọn lọc Phòng Sales | Hiện toàn bộ người phòng Sales (Bỏ chữ Lê) | Cần query logic AND, giữ nguyên từ khóa tìm |
| DEF-S2-008 | ⚪ | Ngày sinh NV xuất hiện thêm `12:00:00 AM` | Khởi động lưới Data | Ghi: 01/01/2000 12:00 AM | Trim DateTime format = `dd/MM/yyyy` |
| DEF-S2-009 | ⚪ | Không thể tab ra khỏi ô nhập Lương | Bấm phím Tab | Kẹt ở ô Textbox Lương | Canh TabIndex logic |
| DEF-S2-010 | ⚪ | Thiếu placeholder gõ Số điện thoại | Nhìn vào form NV trống | Không biết gõ dạng +84 hay 09 | Set Textbox Hint "VD: 09..." |

### 2. Nhóm Quản lý Khách hàng (frmKhachHang) - 10 Lỗi
| DEF_ID | Mức | Mô tả lỗi | Bước tái hiện | Kết quả thực tế | Kết quả mong đợi |
|--------|-----|-----------|--------------|-----------------|------------------|
| DEF-S2-011 | 🔴 | Dữ liệu SĐT khách bị lặp do khoảng trắng | Khách 090 123 45. Có ng nhập lại dính 09012345 | Lưu thành 2 KH khác nhau | Phải `Trim()` trc khi lưu |
| DEF-S2-012 | 🔴 | Khách được đăng ký mới dù tên trống trơn | Nhập rỗng tên, bỏ qua form validation kh | System lỗi | Form bắt validate rỗng |
| DEF-S2-013 | 🟠 | SĐT nhận cả chữ "O" thay số 0 | Gõ O98... | Lưu OK | Khống chế Regex numeric only |
| DEF-S2-014 | 🟠 | Khóa tài khoản Khách nhưng Ví tiền vẫn xài được | Tắt status KH, cầm thẻ quẹt ở POS | Thanh toán vé qua Ví ngon lành | POS phải check `TrangThai` của KH |
| DEF-S2-015 | 🟠 | Load 10,000 Khách hàng gây đơ form vài giây | Chuyển Tab KH | Treo UI 3s | Implement Paging (Phân trang) 100/page |
| DEF-S2-016 | 🟡 | Dấu check "Khách VIP" vẫn ở trạng thái disable | KH đạt chục ngàn điểm tích lũy | Checkbox VIP mờ không tick | Tự động nâng cấp hạng VIP (Trigger DB) |
| DEF-S2-017 | 🟡 | Tìm khách hàng không hỗ trợ tìm SĐT 4 số cuối | Gõ ô tìm kiếm "3456" | Ra kết quả null rỗng | Sửa Query `LIKE '%3456'` |
| DEF-S2-018 | 🟡 | Double click dòng KH bật popup sửa không kéo hình đại diện lên | Nhấp sửa KH | PictureBox Avatar trống | Map hình base64 hoặc Image URL |
| DEF-S2-019 | ⚪ | Font tiền số Toàn bộ chi tiêu ko có dấu phân cách | Lưới DataGrid hiện | KH chi 15000000 | Phải format `15,000,000đ` |
| DEF-S2-020 | ⚪ | Label Ghi chú quá bé ko đọc hết | Click KH có note dài | TextBox ghi chú k cuộn dc | Chuyển label -> Textbox MultiLine |

### 3. Nhóm Bán Hàng POS (frmBanHang) - 10 Lỗi
| DEF_ID | Mức | Mô tả lỗi | Bước tái hiện | Kết quả thực tế | Kết quả mong đợi |
|--------|-----|-----------|--------------|-----------------|------------------|
| DEF-S2-021 | 🔴 | Gian lận: Edit mã nguồn frontend giá giỏ hàng thành 0đ | Nằm trong mục Test Security SRS | Gửi Request giá 0đ, DB lưu Hóa Đơn 0đ | BUS tầng chặn phải truy ra giá gốc từ DB |
| DEF-S2-022 | 🔴 | Gian lận: Thêm -1 Vé để làm giảm tổng hóa đơn | Nhập số lượng vé -1, và nước ngọt 1 | Tổng tiền hóa đơn rẻ hơn | Cấm số lượng < 0 (Trừ khi Admin trả hàng) |
| DEF-S2-023 | 🟠 | Cắt tính năng tự động tích điểm Loyalty | Mua xong Đơn hàng | Số điểm Khách trên DB nằm im | Đơn Hàng xử lý xong trigger cộng điểm KH |
| DEF-S2-024 | 🟠 | Áp mã Khuyến mãi hết hạn hệ thống vẫn cấn trừ tiền | Gõ coupon "2024", click Apply | Bill giảm 50k | Văng lỗi hết hạn SD |
| DEF-S2-025 | 🟠 | Tắt app giữa chừng trong lúc thanh toán POS hụt Hóa đơn | Quẹt thẻ, cúp điện, data ko commit DB | Mất dữ liệu đồng bộ | Handle qua `TransactionScope`, kẹp RollBack |
| DEF-S2-026 | 🟡 | Máy in hóa đơn không chọn khổ A5 hay K80 | Nhấn in thẻ/Bill | Setup in rập khuôn vào Pdf khổ lớn | Bổ sung Cấu hình in |
| DEF-S2-027 | 🟡 | Chuyển Tab cực lag vì gọi lại DB SP | Chuyển qua chuyển lại tab Khu Rừng - Khu Nước | Delay ngậm giật nảy | Cache Memory SP load lên RAM lúc Form mở |
| DEF-S2-028 | 🟡 | Màn POS thiếu đồng hồ ngày giờ Real-time | Giờ hiện ở Label Top | Đứng im một thời gian do chưa bỏ vào Timer | Thêm Timer 1s cập nhật Label |
| DEF-S2-029 | ⚪ | Nút Xóa Món trong rỏ nhỏ li ti rát khó bấm cho cảm ứng | Ở giỏ hàng góc trái | Tick bé, phải thao tác chuột kĩ | Nút bự + Viền đỏ bọc rõ |
| DEF-S2-030 | ⚪ | Màu sắc nền POS chói lóa làm mỏi mắt thu ngân | Xài Theme trắng Full | Mỏi mắt sau 5h làm | Cung cấp DarkMode Switcher |

### 4. Nhóm Liên Module (Cross-cutting / Mạng LAN / Ví RFID) - 10 Lỗi
| DEF_ID | Mức | Mô tả lỗi | Bước tái hiện | Kết quả thực tế | Kết quả mong đợi |
|--------|-----|-----------|--------------|-----------------|------------------|
| DEF-S2-031 | 🔴 | Kết nối Server ngắt LAN đột ngột làm treo App vĩnh viễn | Bứt cáp LAN ra | Ứng dụng dơ 60s và crash hẳn | Thử try..catch SQL Timeout -> Hiện bảng Yêu cầu cấu hình Mạng |
| DEF-S2-032 | 🔴 | Thanh toán Ví Điện Tử / thẻ lỗi Deadlock | 2 Thu ngân quẹt thẻ nạp trừ 1 thẻ RFID cùng lúc | DB văng Data Lock | Cấu trúc Row Lock Version khi Update Ví |
| DEF-S2-033 | 🟠 | Plaintext Password lù lù lưu trong App.config | Mở file local exe config config | Chuỗi connection String lộ Pass SA SQL | Mã hóa EncryptConnectionStrings |
| DEF-S2-034 | 🟠 | Mã Thẻ RFID trùng ID vẫn Map được Khách Hàng 2 | Gán The 009 cho KH A, sau gán tiếp KH B | 1 thẻ mở Cả 2 cổng, xài 2 ví | Tích UNIQUE Thẻ vật lí liên kết 1 User |
| DEF-S2-035 | 🟠 | Backup Database chép đè gây mất file | Dùng tính năng Sao lưu Tool | File cũ "backup.bak" bị ghi đè lên | Generate tên theo ngày giờ: `Backup_yyyy_dd_MM.bak` |
| DEF-S2-036 | 🟡 | Nút "Trợ Giúp" link URL đã hỏng 404 | Bấm phím F1 | Web nảy lên báo trang chết | Trỏ URL chính xác về wiki |
| DEF-S2-037 | 🟡 | Form "Đăng ký Khách Vãng Lai từ Cổng POS" nảy ra k đóng | Pop lên làm | Làm xong Đăng Ký nút ẨN không đóng Pop | Tự động `this.Close()` |
| DEF-S2-038 | 🟡 | Export Excel Danh sách sai định dạng UTF-8 | Xuất danh sách KH ra Excel | Máy mở lên tiếng Việt bị lỗi Font `?%&` | Khi lưu Set UTF-8 encoding Excel Csv |
| DEF-S2-039 | ⚪ | Logo Dai Nam Splash Screen bị xé hình | Nổi Flash Screen ban đầu khởi động App | Ảnh nhạt | Tùy chỉnh AspectRatio |
| DEF-S2-040 | ⚪ | Nút Chụp hình lưu CSDL icon bị sai hình Save | Camera capture | Hình dĩa mềm | Sửa icon thành Chụp ảnh `camera_icon.png` |
