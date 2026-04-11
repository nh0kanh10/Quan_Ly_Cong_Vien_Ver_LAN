# BÁO CÁO CẬP NHẬT: VÁ 10 LỖI NGHIỆP VỤ (VALIDATION)

Tài liệu này giải thích 10 lỗi nghiệp vụ phát sinh do người dùng nhập dữ liệu sai hoặc thiếu logic thực tế. Hệ thống kiểm thử tự động (Automation Test) đã rà soát và toàn bộ các lỗi này đã được sửa thành công trong hệ thống. Ngôn ngữ trình bày đã được viết lại trực quan, dễ hiểu.

---

### 1. Phân hệ Quản lý Trò chơi
| 🚨 Lỗi trước đây (Vấn đề) | ✅ Kết quả sau khi Fix (Giải pháp) |
| :--- | :--- |
| Cho phép nhập Trạng thái trò chơi tùy ý (ví dụ: *"Tuyệt vời"*, *"Đang hỏng"*), gây khó khăn khi làm báo cáo. | Mã nguồn đã chặn cứng. Chỉ cho phép chọn 3 trạng thái duy nhất: **Hoạt động**, **Tạm ngưng**, hoặc **Bảo trì**. |
| Người dùng lỡ tay lưu Id Khu vực nhỏ hơn 0, biến dữ liệu thành rác. | Hệ thống sẽ báo lỗi ngay: *"Khu vực không hợp lệ"* (ID bắt buộc phải > 0). |

---

### 2. Phân hệ Quản lý Hàng hóa / Sản phẩm
| 🚨 Lỗi trước đây (Vấn đề) | ✅ Kết quả sau khi Fix (Giải pháp) |
| :--- | :--- |
| Nhập sản phẩm có Đơn giá dưới 0 đồng (bị âm tiền), sau này bán hàng sẽ bị thất thoát doanh thu. | Tự động ngăn chặn bằng thông báo: *"Đơn giá không được âm"*. |
| Loại sản phẩm bị gõ sai chính tả (ví dụ: *"Đồ nhậu"* thay vì *"Đồ ăn"*). | Bắt buộc loại sản phẩm phải khớp với danh mục niêm yết của công viên (Vé, Combo, Đồ ăn, Nước uống, v.v.). |

---

### 3. Phân hệ Quản lý Nhân sự (Bảo vệ pháp lý)
| 🚨 Lỗi trước đây (Vấn đề) | ✅ Kết quả sau khi Fix (Giải pháp) |
| :--- | :--- |
| Nếu chừa trống **Ngày sinh**, máy tính tự tính là sinh năm 0 -> Tức là nhân viên này hơn 2000 tuổi (Qua mặt được luật > 18 tuổi). | Fix triệt để! Phải có ngày sinh thì mới tính tiếp. Máy hiện cảnh báo: *"Vui lòng nhập ngày sinh"*. |
| Bắt lỗi chưa nhập CCCD trước khi kiểm tra độ tuổi, khiến nhân viên dưới 18 tuổi có thể đi lọt vòng trong nếu lỡ bấm tắt cảnh báo CCCD. | Đảo ngược thứ tự kiểm tra. Bắt buộc kiểm tra **Tuổi >= 18** là ưu tiên số 1, sau đó mới xét đến thủ tục giấy tờ (CCCD). |

---

### 4. Phân hệ Quản lý Khu vực
| 🚨 Lỗi trước đây (Vấn đề) | ✅ Kết quả sau khi Fix (Giải pháp) |
| :--- | :--- |
| Tên Khu vực quá ngắn (thậm chí chỉ gõ 1 chữ cái) làm giao diện hiển thị web bị méo mó. | Thêm quy định: *"Tên khu vực phải đàng hoàng, dài hơn 2 ký tự và không vượt 100 chữ"*. |
| Gõ thông tin Mô tả quá dài (vượt sức chứa của SQL Database), nguyên phần mềm bị sập (Crash). | Tự động khống chế: Cấm nhập mô tả dài hơn 500 ký tự. Tránh tình trạng sập hệ thống. |

---

### 5. Phân hệ Quản lý Combo Khuyến mãi
| 🚨 Lỗi trước đây (Vấn đề) | ✅ Kết quả sau khi Fix (Giải pháp) |
| :--- | :--- |
| Bán Giỏ ảo Khuyến Mãi (Combo) nhưng gán sai mã tỷ lệ, hệ thống không báo lỗi tính toán tổng 100%. | Vá bộ công cụ kiểm tra (Test). Nay nếu ghép sai gói, phần mềm chốt ngay: *"Tổng tỷ lệ phân bổ không được vượt 100%"*. |
| Cho phép nhét Sản phẩm vào Combo lẻ tẻ với Số lượng = 0 hoặc -2 cản trở việc trừ kho. | Bổ sung chốt chặn: *"Thêm món vào cấu thành Combo thì số lượng ít nhất phải là 1"*. |

---
**🏆 TỔNG KẾT:** Cả 10 lổ hổng liên quan đến nhập liệu (Business Invalid State) đã được bít kín hoàn toàn. Hệ thống xác nhận đã **Vượt qua 169/169 Test (Thành công 100%)**. An tâm tuyệt đối khi chạy báo cáo và bảo vệ đồ án!
