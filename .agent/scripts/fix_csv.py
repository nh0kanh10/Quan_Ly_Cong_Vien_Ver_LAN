import csv
import re
import os

input_file = r"c:\Users\ADMIN\Desktop\Quan_Ly_Cong_Vien_Ver_1.2\Docs\04_SPRINT_03_Winform_CorePOS\Sprint3_DefectList.csv"
output_file = r"c:\Users\ADMIN\Desktop\Quan_Ly_Cong_Vien_Ver_1.2\Docs\04_SPRINT_03_Winform_CorePOS\Sprint3_DefectList_QA.csv"

# Read the CSV
with open(input_file, 'r', encoding='utf-8') as f:
    text = f.read()

# 1. Remove icons/emojis
text = text.replace("✅", "").replace("❌", "").replace("↕", "")

# 2. Fix casing / Remove sudden capitalizations
text = text.replace("KHÔNG TÌM THẤY", "Không tìm thấy")
text = text.replace("SẴN SÀNG", "Sẵn sàng")
text = text.replace("MỜI VÀO", "Mời vào")
text = text.replace("HẾT HẠN", "Hết hạn")
text = text.replace("HẾT QUOTA", "Hết số lượng")
text = text.replace("PASS", "Hợp lệ")
text = text.replace("FAIL", "Không hợp lệ")

# 3. Clean tech jargons (translated for QA readability)
replacements = {
    "frmBanHang": "Màn hình Bán hàng",
    "frmKiemSoatVe": "Màn hình Kiểm soát vé",
    "frmXuatVeDoan": "Màn hình Xuất vé đoàn",
    "frmThueDo": "Màn hình Thuê đồ",
    "Hệ thống xóa dòng có số lượng bằng 0 hoặc cảnh báo: Giỏ hàng trống. Không cho thanh toán.": "Hệ thống hiển thị cảnh báo: Giỏ hàng trống và chặn thanh toán.",
    "Sản phẩm hiện giá 0đ hoặc thông báo: Chưa thiết lập giá.": "Hệ thống hiển thị thông báo: Chưa thiết lập giá.",
    "Ô txtScanInput tự nhận focus lại (luôn active). Hoặc click vùng khác không mất focus.": "Ô nhập mã không bị mất con trỏ chuột khi bấm ra vùng nền.",
    "Hệ thống tách và xử lý mã đầu tiên (VE-001). Bỏ qua phần sau hoặc xử lý lần lượt.": "Hệ thống xử lý mã đầu tiên và bỏ qua các mã dính liền sau đó.",
    "Nhấn → _doanTimThay = null → NullReferenceException hoặc popup lỗi rối.": "Khi nhấn vào ứng dụng bị văng do không có dữ liệu.",
    "Trường Combo hiện \"null\" hoặc trống hoàn toàn.": "Trường Combo bị khóa và trống hoàn toàn.",
    "Giỏ tự xóa khi đổi trạm. Hoặc cảnh báo: \"Giỏ sẽ mất khi đổi trạm\".": "Hệ thống hiển thị cảnh báo: Giỏ sẽ mất khi đổi trạm.",
    "PhienThue.IdKhuVuc = -1 hoặc null → DB constraint lỗi hoặc ghi sai trạm.": "cơ sở dữ liệu ghi sai toàn bộ thông tin trạm.",
    "Hoạt động click vùng khác không mất focus": "Bấm ra nền vẫn giữ được con trỏ",
    "nullReferenceException": "lỗi ứng dụng",
    "NullReferenceException": "lỗi ứng dụng",
    "crash app": "ứng dụng bị đóng đột ngột",
    "crash": "ứng dụng bị văng",
    "_selectedKH = null": "khách hàng chưa được chọn",
    "_currentIdKhuVuc = -1": "chưa chọn khu vực",
    "DB unique constraint lỗi": "lỗi lưu dữ liệu cục bộ trùng lặp",
    "DB unique constraint": "ràng buộc dữ liệu",
    "DB": "cơ sở dữ liệu",
    "deduct stock": "giảm trừ kho",
    "deductStock": "giảm trừ kho",
    "Id": "mã",
    "txtScanner.Focus()": "con trỏ chuột nằm ở ô quét mã",
    "focus": "con trỏ chuột",
    "Focus": "Con trỏ chuột",
    "timerReset_Tick không fire": "bộ đếm thời gian không hoạt động",
    "timerReset_Tick": "bộ đếm",
    "timerFlash chưa được init đúng": "bộ đếm nháy đèn chưa được khởi tạo",
    "timerFlash": "bộ đếm đèn",
    "timer": "bộ đếm",
    "lstHistory": "danh sách lịch sử",
    "RAM": "bộ nhớ máy tính",
    "Scroll": "Cuộn trang",
    "scroll": "cuộn trang",
    "txtScanInput": "ô nhập mã",
    "ProcessScan": "quá trình xử lý quét thẻ",
    "Guid trùng — xác suất thấp nhưng đã xảy ra trên cùng seed": "mã hệ thống tạo trùng do mở cùng một thời điểm",
    "Guid": "mã hệ thống",
    "seed": "con số khởi tạo",
    "BUS_DonHang.ThanhToanBangVi()": "tính năng thanh toán bằng ví",
    "BUS_VeDienTu.CheckTicket": "hệ thống kiểm tra vé",
    "DateTime.Now thay vì DateTime.Today → 00:00 của hôm qua < DateTime.Now nhưng .Date so sánh sai": "sai lệch khi so sánh thời gian chính xác và số ngày",
    "_doanTimThay = null": "dữ liệu đoàn bị trống",
    "FormatException hoặc NumberFormatException": "lỗi sai định dạng",
    "Interaction.InputBox": "hộp thoại nhập chữ",
    "GetDynamicPrice()": "giá đặc biệt",
    "ET_SanPham": "thông tin gốc",
    "constraint lỗi": "bị từ chối lưu lại",
    "Visible = false": "bị ẩn đi",
    "disable": "vô hiệu hóa",
    "disabled": "bị vô hiệu hóa",
    "enabled": "nhấn được bình thường",
    "active": "sẵn sàng thao tác",
    "validate": "kiểm tra",
    "null": "trống",
    "prefix": "tiền tố",
    "suffix": "hậu tố",
    "handle": "xử lý",
    "fallback": "hiển thị thay thế",
    "default": "mặc định",
    "enum": "loại tiêu chuẩn",
    "switch-case": "xử lý điều kiện",
    "edge case": "trường hợp ngoại lệ",
    "Edge case": "Trường hợp ngoại lệ",
    "KeyPreview": "tính năng phím tắt",
    "event chưa wire": "chưa gắn sự kiện nút nhấn",
    "Deduct stock": "Trừ mức tồn kho",
    "parse": "đọc dữ liệu",
    "trim": "xóa khoảng trắng",
    "Trim": "Xóa khoảng trắng",
    "suppressKeyPress hoặc e.Handled": "chặn phím Enter",
    "SuppressKeyPress hoặc e.Handled": "Chặn phím Enter",
    "trigger": "kích hoạt",
}

for k, v in replacements.items():
    text = text.replace(k, v)

# Fix some remaining double spaces that might occur
text = re.sub(' +', ' ', text)

with open(output_file, 'w', encoding='utf-8') as f:
    f.write(text)

print("Xử lý file CSV thành công!")
