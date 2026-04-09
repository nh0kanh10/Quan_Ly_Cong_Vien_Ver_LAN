# TỔNG HỢP DANH SÁCH MÀN HÌNH (UI/UX FORMS) THEO NGHIỆP VỤ
**Dự án**: Hệ thống Quản lý Siêu Công viên Đại Nam
**Tổng số màn hình dự kiến**: 73 Màn hình

---

## 1. Quản trị danh mục (Master Data)
*Nhóm các màn hình quản lý dữ liệu gốc, phân quyền và cấu hình hệ thống.*

| STT | Tên Màn Hình | Mô Tả Chức Năng |
| :---: | :--- | :--- |
| 1 | **Danh sách nhân viên** | CRUD nhân viên, gán vai trò phân quyền. |
| 2 | **Danh sách khách hàng** | CRUD khách hàng cá nhân, tích điểm. |
| 3 | **Danh sách đoàn khách** | CRUD đoàn khách (công ty, đại lý tour), thiết lập chiết khấu. |
| 4 | **Danh sách khu vực** | CRUD phân khu trong công viên (Biển, Thú, Đua). |
| 5 | **Danh sách sản phẩm** | CRUD sản phẩm, dịch vụ, check giá cơ bản. |
| 6 | **Danh sách đơn vị tính** | CRUD đơn vị tính cơ sở (Lon, Thùng, Lốc) và tỷ lệ quy đổi. |
| 7 | **Danh sách combo** | CRUD gói combo, thiết lập tỷ lệ phân bổ % doanh thu. |
| 8 | **Danh sách nhà cung cấp** | CRUD đối tác cung ứng hàng hóa cho kho. |
| 9 | **Danh sách kho hàng** | CRUD danh mục kho (Trung tâm, Kiosk, Nhà hàng). |
| 10 | **Danh sách phòng khách sạn** | CRUD phòng, thiết lập giá ngày thường / cuối tuần. |
| 11 | **Danh sách bàn ăn** | CRUD danh mục bàn trong các nhà hàng, sức chứa. |
| 12 | **Danh sách đường đua** | CRUD đường đua vật lý (độ dài, loại mặt đường). |
| 13 | **Danh sách loại hình đua** | CRUD loại hình (Ngựa, Chó, Go-Kart). |
| 14 | **Danh sách vận động viên** | CRUD thông tin Nài ngựa, Cua-rơ. |
| 15 | **Danh sách ngựa đua** | CRUD hồ sơ sức khỏe ngựa đua, gán Nài. |
| 16 | **Danh sách phương tiện đua** | CRUD xe đua, thiết bị, gán tay đua. |
| 17 | **Danh sách khán đài & ghế** | CRUD sức chứa khán đài, định danh vị trí ghế (VIP/Thường). |
| 18 | **Danh sách khu vực biển** | CRUD thông tin bãi tắm, yêu cầu phao bơi. |
| 19 | **Danh sách thiết bị tạo sóng** | CRUD máy phát sóng, thông số điện. |
| 20 | **Danh sách chòi nghỉ mát** | CRUD danh sách chòi Cabana cho thuê. |
| 21 | **Danh sách khu vực vườn thú** | CRUD phân khu Safari. |
| 22 | **Danh sách động vật** | CRUD hồ sơ thú, loài, tuổi, sức khỏe. |
| 23 | **Danh sách chuồng trại** | CRUD danh mục chuồng nuôi nhốt thú. |
| 24 | **Danh sách Kiosk** | CRUD các máy/quầy bán hàng tự động, vị trí. |
| 25 | **Danh sách vai trò & quyền** | CRUD Role (Vai trò), Permission (Quyền) và map RBAC. |
| 26 | **Danh sách CT khuyến mãi** | CRUD mã code Voucher, Flash sale, % giảm. |

---

## 2. Quản lý Đơn hàng & Thanh toán (Core Sales & Finance)
*Trái tim của hệ thống: Dòng tiền và Giao dịch.*

| STT | Tên Màn Hình | Mô Tả Chức Năng |
| :---: | :--- | :--- |
| 27 | **Màn hình POS Quầy** | Tạo Đơn Hàng tổng hợp, chọn SP/Combo, áp khuyến mãi, Thu tiền mặt/Ví. |
| 28 | **Màn hình App Khách đặt** | Giao diện cho User tự chọn món, thanh toán qua Cổng Online (MoMo). |
| 29 | **Danh sách Đơn hàng** | Giao diện tra cứu, lọc, tìm kiếm Bill tổng. |
| 30 | **Chi tiết Đơn hàng** | Mổ xẻ Item (Mua đứt, Thuê đồ, Khách sạn, Giữ xe). |
| 31 | **Quản lý Ví điện tử** | Quản trị Ledger Wallet: Nạp/Rút, khóa ví, tra cứu số dư khả dụng/đóng băng. |
| 32 | **Quản lý Thẻ RFID** | Cấp phát vòng tay, báo mất (Lost), đổi thẻ từ (Revoke). |
| 33 | **Quản lý Phiếu Thu / Chi** | Sổ quỹ công ty, xem lịch sử, hạch toán giao dịch thủ công. |
| 34 | **Bảng Đối soát & Kiểm toán** | Tool rà soát `HashSignature` bảo mật nhằm phát hiện gian lận sửa Database. |

---

## 3. Dịch vụ Cho thuê (Operations)
*Quản lý tài nguyên luân chuyển nội bộ.*

| STT | Tên Màn Hình | Mô Tả Chức Năng |
| :---: | :--- | :--- |
| 35 | **Quầy Thuê Thiết bị** | Bán dịch vụ thuê Phao/Xe điện. Quẹt RFID thu cọc (Hold fund). |
| 36 | **Quản lý Tủ đồ Locker** | Theo dõi tủ trống/bận, setup mã PIN, Trả đồ & Hoàn cọc / Phạt. |
| 37 | **Quản lý Chòi Cabana** | Đặt chòi VIP, Check-in / Check-out, tính phụ trội giờ. |
| 38 | **Dashboad Đồ đang thuê** | Màn hình radar theo dõi ai đang giữ đồ gì quá hạn chưa trả. |

---

## 4. Quản lý Khách sạn (Hospitality)

| STT | Tên Màn Hình | Mô Tả Chức Năng |
| :---: | :--- | :--- |
| 39 | **Lễ tân Đặt phòng** | Màn hình Time-line check phòng trống theo ngày. Chốt Booking. |
| 40 | **Check-in Nhận phòng** | Cấp chìa khóa vật lý/RFID, update trạng thái Nhận phòng. |
| 41 | **Check-out Trả phòng** | Thanh toán phụ thu (Mini-bar), giải phóng phòng. |
| 42 | **Sơ đồ Phòng (Map)** | Giao diện Lưới/Sơ đồ phòng hiển thị màu sắc Trống/Đầy/Bảo trì. |
| 43 | **Danh sách Booking** | Bảng quản lý đơn đặt, hỗ trợ hủy đơn trước 24h. |

---

## 5. Quản lý Nhà hàng (F&B)

| STT | Tên Màn Hình | Mô Tả Chức Năng |
| :---: | :--- | :--- |
| 44 | **Lễ tân Đặt bàn** | Tra cứu bàn trống, nhận Booking khách VIP giữ chỗ. |
| 45 | **Sơ đồ Bàn ăn (Map)** | Hiển thị trực quan khu vực nhà hàng, bàn nào đang ăn, bàn nào chờ dọn. |
| 46 | **Order Gọi Món (POS)** | Nhân viên quẹt RFID khách, Thêm Món Gộp Bill trực tiếp. |
| 47 | **Lịch trình Đặt Bàn** | Theo dõi thời gian đến dự kiến để set mâm, cảnh báo trễ hẹn. |

---

## 6. Quản lý Bãi đỗ xe (Parking)

| STT | Tên Màn Hình | Mô Tả Chức Năng |
| :---: | :--- | :--- |
| 48 | **Trạm Barie Vào** | Camera nhận diện Biển số (hoặc nhập tay), cấp thẻ RFID Giữ xe. |
| 49 | **Trạm Barie Ra** | Quét RFID, tính phí qua DATEDIFF, thu tiền/Trừ ví & Nâng cần môn. |
| 50 | **Dashboard Bãi Đỗ** | Hiển thị sức chứa realtime, Số xe đang trong bãi. |

---

## 7. Giải trí Biển & Vườn thú (Leisure Ops)

| STT | Tên Màn Hình | Mô Tả Chức Năng |
| :---: | :--- | :--- |
| 51 | **Thông số Nước (Biển)** | Form nhập chỉ số pH, Độ mặn hàng ngày cho kiểm tra Y tế. |
| 52 | **Điều khiển Tạo sóng** | Bảng lịch biểu bật/tắt thiết bị buồng sóng kỹ thuật. |
| 53 | **Lịch trực Cứu hộ** | Bảng phân công nhân sự trực gác tại các tháp canh báo động. |
| 54 | **Bảng biểu Cho thú ăn** | Quản thú nhập lịch khẩu phần ăn, tránh tình trạng thú đói/bội thực. |
| 55 | **App Khách: Tương tác Thú** | Màn hình mua vé đút thú ăn (Cho hươu ăn/Hổ ăn). |
| 56 | **Sổ Khám Bệnh Động Vật** | Hồ sơ Y bạ thú y, ghi chú tiêm chủng. |

---

## 8. Quản lý Trường đua (Racing System)

| STT | Tên Màn Hình | Mô Tả Chức Năng |
| :---: | :--- | :--- |
| 57 | **Thiết lập Giải đua** | Master Form tạo Cup thi đấu (Thời gian, Đường đua, Thể loại). |
| 58 | **Bảng Lịch Thi Đấu** | Lắp ghép Vận động viên - Xe - Ngựa vào từng Frame giờ đua. |
| 59 | **Trọng tài: Nhập Kết Quả** | Form cập nhật Nài nào về thứ mấy, Thời gian bao nhiêu mili-giây. |
| 60 | **Phòng Kỹ Thuật Bảo Trì** | Lập phiếu bảo dưỡng Xe Go-Kart, chi phí thuốc men Ngựa. |
| 61 | **App Khách: Đặt vé Khán đài**| Sơ đồ chỗ ngồi chọn ghế xem đua (giống mô hình book vé xem phim CGV). |

---

## 9. Cung ứng & Kiosk (Supply Chain)

| STT | Tên Màn Hình | Mô Tả Chức Năng |
| :---: | :--- | :--- |
| 62 | **Dashboard Tồn Kho Kiosk** | Màn hình nháy đỏ cảnh báo khi Kiosk có hàng hóa rớt dưới ngưỡng. |
| 63 | **Nhập hàng (Purchasing)** | Form tạo Phiếu Nhập từ Nhà Cung Cấp, tự động trigger thanh toán chi PhieuChi. |
| 64 | **Điều chuyển / Xuất Bán** | Form xuất hàng nội bộ (Kho Tổng -> Kiosk) hoặc Hủy hàng hư hỏng. |
| 65 | **Sổ Quản lý Tồn kho** | Thống kê số lượng hàng Tồn vật lý Realtime trên toàn hệ thống. |

---

## 10. Sự cố & An toàn (Security)

| STT | Tên Màn Hình | Mô Tả Chức Năng |
| :---: | :--- | :--- |
| 66 | **Bộ đàm Sự Cố** | Form Log khẩn cấp: Tọa độ, Phân loại sự cố (Đuối nước, Đánh nhau) -> Gán quân xử lý. |
| 67 | **Trung tâm Đồ Thất lạc** | Quản trị viên nhập kho Món đồ lụm được, Giao diện C/S tra cứu trả khách. |

---

## 11. Báo cáo & Thống kê (Dashboards/Reports)

| STT | Tên Màn Hình | Mô Tả Chức Năng |
| :---: | :--- | :--- |
| 68 | **Báo cáo Doanh Thu Cuối Ngày**| Bóc tách tiền thực thu, Nguồn (Mặt/Ví/Thẻ). |
| 69 | **Báo cáo Khúc Doanh Thu (Combo)**| Report tự động phân rã Combo để biết Biển, Thú, Nhà hàng lãi bao nhiêu. |
| 70 | **Sổ Công Nợ (B2B)** | List các Hợp đồng Đại lý chưa chuyển khoản, Đôn đốc thu hồi nợ. |
| 71 | **Báo cáo Thẻ Kho** | Lịch sử Nhập / Xuất / Tồn của 1 mặt hàng (Biến động hàng hóa). |
| 72 | **Báo cáo Vé Tồn Đọng** | Thống kê bao nhiêu khách mua vé nhưng không/chưa quẹt mã qua cổng. |
| 73 | **EXECUTIVE DASHBOARD** | Bảng điều khiển tối cao (Biểu đồ, KPI, Tiền tươi thóc thật) dành riêng cho C-Level. |

---

### 📊 BẢNG TỔNG KẾT TIẾN ĐỘ THIẾT KẾ
| Nhóm Nghiệp Vụ | Phân hệ (Module) | Số Lượng Form UI |
| :--- | :--- | :---: |
| **I** | Quản trị Danh mục (Config) | 26 |
| **II** | Dòng Tiền & Bán Hàng (Sales) | 8 |
| **III** | Phân hệ Chuyên Sâu (Ops) | 33 |
| **IV** | Báo cáo Thống kê (Reports) | 6 |
| **TỔNG CỘNG** | **Full System MVP** | **73 Màn hình** |
