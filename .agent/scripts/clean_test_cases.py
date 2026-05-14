import csv
import re
import os

input_file = r"c:\Users\ADMIN\Desktop\Quan_Ly_Cong_Vien_Ver_1.2\Docs\04_SPRINT_03_Winform_CorePOS\DoanKhach_TestCases.csv"
output_file = r"c:\Users\ADMIN\Desktop\Quan_Ly_Cong_Vien_Ver_1.2\Docs\04_SPRINT_03_Winform_CorePOS\DoanKhach_TestCases_QA.csv"

def process_text(text):
    if not isinstance(text, str):
        return text
    
    # 1. Remove icons
    text = text.replace("✅", "")
    text = text.replace("❌", "")
    text = text.replace("🎁", "")
    
    # 2. Fix specific known ambiguities ("hoặc")
    text = text.replace('Nhấn menu "Đoàn khách" (hoặc "Quầy vé Lễ tân")', 'Nhấn menu Đoàn khách')
    text = text.replace('Hộp thoại thông báo "Không tìm thấy" hoặc panel hiện "Không tìm thấy đoàn".', 'Hệ thống hiển thị màn hình Không tìm thấy.')
    text = text.replace('Hiện cảnh báo "Vui lòng nhập mã booking hoặc SĐT" hoặc bỏ qua.', 'Hệ thống hiển thị cảnh báo yêu cầu nhập mã booking hoặc SĐT.')
    text = text.replace('Nhãn: lý do hết hạn (chữ đỏ)', 'Nhãn hiển thị dòng chữ báo hết hạn.')
    text = text.replace('Menu bị ẩn hoặc disabled, hoặc form hiện "Không có quyền".', 'Bộ công cụ bị khóa vô hiệu hóa.')
    text = text.replace('Hệ thống hiện booking phù hợp nhất (ưu tiên booking chưa xác nhận, đúng ngày hôm nay) hoặc hiện danh sách cho nhân viên chọn.', 'Hệ thống hiển thị booking ưu tiên chưa xác nhận trong ngày hôm nay.')
    text = text.replace('"Không tìm thấy" hoặc bỏ qua. Không crash, không overflow.', 'Giao diện hiển thị không tìm thấy.')
    text = text.replace('Trường SĐT hiện "(Chưa có)" thay vì trống hoặc crash.', 'Trường SĐT tự động chuyển thành Chưa có.')
    text = text.replace('Trường Ngày đến hiện "(Chưa xác định)" thay vì crash.', 'Trường Ngày đến tự động chuyển thành Chưa xác định.')
    text = text.replace('Trường Combo hiện "(Chưa chọn gói)" chứ không phải trống hoặc null.', 'Trường Combo hiển thị chữ Chưa chọn gói.')
    text = text.replace('Nhãn: " Booking đã HẾT HẠN" hoặc tương tự.', 'Hệ thống hiển thị nhãn báo Booking đã hết hạn.')
    text = text.replace('Hệ thống có thể xử lý (vì form không check SessionManager trực tiếp tại Xuất Vé) hoặc BUS kiểm tra. Quan trọng: không crash.', 'Hệ thống tự động điều hướng người dùng quay lại màn hình đăng nhập.')
    text = text.replace('Đã xác nhận → không thể xác nhận lần 2. TOÀN BỘ LUỒNG HOẠT ĐỘNG ĐÚNG.', 'Hệ thống chặn việc xác nhận lần 2. Chu trình kết thúc và thành công.')
    
    text = text.replace('(hoặc form quản lý đoàn)', '')
    text = text.replace('hoặc tương đương', '')
    text = text.replace('hoặc bỏ qua', '')
    text = text.replace('hoặc trống', '')
    
    # Replace other ambiguous cases using 'hoặc'
    text = text.replace('hoặc "Không tìm thấy"', '')
    text = text.replace('hoặc trống', '')
    text = text.replace('hoặc "Quầy vé Lễ tân"', '')
    text = re.sub(r'\s*hoặc tương tự\.?', '.', text)
    
    # Fix technical jargon
    text = text.replace("crash", "văng ứng dụng")
    text = text.replace("overflow", "tràn dữ liệu")
    text = text.replace("memory leak", "chiếm dụng bộ nhớ")
    text = text.replace("DB", "cơ sở dữ liệu")
    text = text.replace("CSDL", "cơ sở dữ liệu")
    text = text.replace("disabled", "bị vô hiệu hóa")
    text = text.replace("enabled", "nhấn được bình thường")
    text = text.replace("fallback", "cơ chế dự phòng")
    text = text.replace("validate format", "lỗi định dạng")
    text = text.replace("double-confirm", "xác nhận trùng lặp")
    text = text.replace("Memory leak", "Chiếm dụng bộ nhớ máy")
    text = text.replace("Session check", "Kiểm tra phiên đăng nhập")
    text = text.replace("session hết", "hết hạn phiên đăng nhập")
    text = text.replace("logout", "đăng xuất")
    text = text.replace("SessionManager", "hệ thống quản lý phiên")
    text = text.replace("BUS", "hệ thống ngầm")
    text = text.replace("rollback", "hủy thao tác mạng")
    text = text.replace("DialogResult != Yes", "Người dùng không đồng ý")
    text = text.replace("UI", "Giao diện")
    text = text.replace("Logic", "Chức năng")
    text = text.replace("Performance", "Hiệu năng")
    text = text.replace("Security", "Kết quả")
    text = text.replace("E2E", "Quy trình mạch lạc")
    
    text = text.replace('TrangThai = "TEST_STATE" (giá trị lạ)', 'Trạng thái không xác định')
    text = text.replace('Trạng thái không hợp lệ: TEST_STATE', 'Trạng thái không hợp lệ')
    text = text.replace('Trạng thái không hợp lệ: [trạng thái]', 'Trạng thái không hợp lệ')
    
    # Capitalizations
    text = text.replace("ĐÃ ĐƯỢC XÁC NHẬN", "Đã được xác nhận")
    text = text.replace("ĐÃ XÁC NHẬN", "Đã xác nhận")
    text = text.replace("HỦY", "Hủy")
    text = text.replace("KHÔNG", "Không")
    text = text.replace("CẬP NHẬT", "Cập nhật")
    text = text.replace("DẦ LIỆU AN TOÀN", "Tất cả dữ liệu được bảo vệ an toàn")
    text = text.replace("HẾT HẠN", "Hết hạn")
    
    return text

with open(input_file, 'r', encoding='utf-8') as f:
    reader = csv.reader(f)
    rows = list(reader)

processed_rows = []
for idx, row in enumerate(rows):
    if idx == 0:
        processed_rows.append(row)
        continue
    
    # Row format: TC ID,Title,Est. (mins),Test Type,Area,Procedure / Steps,Expected Results,Priority,RESULT
    tc_id = row[0]
    
    # Overwrite the silly technical test cases completely
    if tc_id == "BK_021":
        row = ["BK_021", "Tìm booking bằng mã vượt quá 200 ký tự cho phép", "2", "Chức năng", "Đoàn khách", 
               "1. Khởi động phần mềm.\n2. Đăng nhập hệ thống.\n3. Nhấn menu Đoàn khách.\n4. Nhập chuỗi ký tự độ dài vượt mức 200 ký tự.\n5. Nhấn tìm kiếm.",
               "1. Phần mềm khởi động bình thường.\n2. Đăng nhập thành công.\n3. Chuyển hướng sang màn hình Đoàn khách.\n4. Nhập dãy ký tự dài.\n5. Hệ thống hiển thị cảnh báo độ dài văn bản không được vượt quá 200 ký tự.",
               "Medium", "PENDING"]
    elif tc_id == "BK_022":
        row = ["BK_022", "Tìm booking bằng SĐT nhập thiếu số (Ít hơn 10 chữ số)", "3", "Chức năng", "Đoàn khách",
               "1. Khởi động phần mềm.\n2. Đăng nhập hệ thống.\n3. Nhấn menu Đoàn khách.\n4. Nhập số điện thoại 090123 (6 chữ số).\n5. Nhấn tìm kiếm.",
               "1. Phần mềm khởi động bình thường.\n2. Đăng nhập thành công.\n3. Chuyển hướng sang màn hình Đoàn khách.\n4. Nhập thông tin số liên lạc.\n5. Hệ thống hiển thị cảnh báo lỗi định dạng số điện thoại vì không đủ 10 chữ số.",
               "High", "PENDING"]
    
    # Apply processing to specific elements usually containing strings
    new_row = [process_text(str(col)) for col in row]
    processed_rows.append(new_row)

with open(output_file, 'w', encoding='utf-8', newline='') as f:
    writer = csv.writer(f)
    writer.writerows(processed_rows)

print("Hoàn tất xử lý Test Cases QA")
