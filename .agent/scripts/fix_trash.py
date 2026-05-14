import re

translations = {
    # Fix the missing translations
    'POS_TAB_FNB': ('F&B', 'F&B', '餐饮'),
    'CHK_LO': ('Có quản lý hạn sử dụng (Lô)', 'Manage Expiration Date (Batch)', '管理保质期 (批次)'),
    'CUST_BTN_CLEAR': ('Bỏ', 'Clear', '清除'),
    'CUST_BTN_SEARCH': ('Tìm', 'Search', '搜索'),
    'CUST_LBL_POINTS': ('điểm', 'points', '积分'),
    'CUST_SEARCH_HINT': ('SĐT / Mã KH / Quét RFID...', 'Phone / Cust ID / Scan RFID...', '电话 / 客户号 / 扫描RFID...'),
    'LBL_KH_EMAIL': ('Email', 'Email', '电子邮件'),
    'LBL_KH_FIELD_EMAIL': ('Email', 'Email', '电子邮件'),
    'MSG_KH_NHAP_SOTIEN': ('Nhập số tiền nạp:', 'Enter deposit amount:', '输入充值金额:'),
    'MSG_RFID_CHUYEN_OK': ('Chuyển trạng thái thẻ thành công!', 'Card status changed successfully!', '卡状态更改成功！'),
    'MSG_RFID_KHOA_OK': ('Khóa thẻ thành công!', 'Card locked successfully!', '卡锁定成功！'),
    'PTTT_MOMO': ('MoMo', 'MoMo', 'MoMo'),
    'PTTT_QR': ('QR Code', 'QR Code', '二维码'),
    'ERR_CHITIETRONG': ('Chưa có chi tiết hàng hóa (hoặc hàng hóa rỗng)!', 'No item details provided (or empty)!', '没有提供商品明细（或为空）！'),
    'ERR_CHONNGAY': ('Vui lòng chọn ngày lập phiếu hợp lệ!', 'Please select a valid voucher date!', '请选择有效的单据日期！'),
    'ERR_NAP_MAN_HINH': ('Lỗi nạp màn hình:', 'Screen loading error:', '屏幕加载错误:'),
    'ERR_VI_SO_TIEN_AM': ('Số tiền phải lớn hơn 0!', 'Amount must be greater than 0!', '金额必须大于0！'),
    'MSG_COMBO_TYLE_CHUA_DU': ("Tổng tỷ lệ phân bổ chưa đạt 100%.\nHệ thống tự động chuyển Combo về 'BanNhap' để đảm bảo an toàn doanh thu.", "Total allocation ratio has not reached 100%.\nThe system automatically sets Combo to 'Draft' to ensure revenue safety.", "总分配比例未达到 100%。\n系统自动将套餐设为“草稿”以确保收入安全。"),
    'MSG_DUYETTHANHCONG': ('Đã phê duyệt phiếu!', 'Voucher approved!', '单据已批准！'),
    'MSG_HUYTHANHCONG': ('Đã hủy phiếu!', 'Voucher cancelled!', '单据已取消！'),
    'MSG_KM_APPLIED': ('Đã áp mã {0}: giảm {1:#,##0}₫', 'Applied code {0}: discount {1:#,##0}₫', '已应用代码 {0}: 折扣 {1:#,##0}₫'),
    'MSG_LOI': ('Lỗi', 'Error', '错误'),
    'MSG_LUUTHATBAI': ('Lưu thất bại!', 'Save failed!', '保存失败！'),
    'MSG_REFUND_CONFIRM': ('Bạn có chắc chắn muốn thực hiện hoàn hàng? Quá trình này không thể phục hồi!', 'Are you sure you want to refund? This process is irreversible!', '您确定要退款吗？此过程不可逆转！'),
    'MSG_WARN_UNSAVED': ('Phiếu chưa lưu! Chuyển trang sẽ mất dữ liệu nhập dở. Bạn có tiếp tục?', 'Unsaved voucher! Navigating away will lose draft data. Continue?', '单据未保存！离开页面将丢失草稿数据。是否继续？'),
    'AI_TITLE': ('AI Assistant', 'AI Assistant', 'AI 助手'),
    'AI_SETTINGS_TITLE': ('AI Settings', 'AI Settings', 'AI 设置'),
    'AI_SETTINGS_APIKEY': ('Gemini API Key:', 'Gemini API Key:', 'Gemini API 密钥:'),
    'AI_SETTINGS_MODEL': ('Model:', 'Model:', '模型:'),
    'AI_SETTINGS_TEST': ('Test Connection', 'Test Connection', '测试连接'),
    
    # Fix the trash errors
    'ERR_COMBO_GIA_AM': ('Giá Combo không được âm.', 'Combo price cannot be negative.', '套餐价格不能为负。'),
    'ERR_COMBO_TEN_RONG': ('Tên Combo không được để trống.', 'Combo name cannot be empty.', '套餐名称不能为空。'),
    'ERR_CTK_DA_DUYET_TRUOC_DO': ('Phiếu kiểm kê này đã được duyệt trước đó.', 'This inventory voucher has already been approved.', '此盘点单之前已被批准。'),
    'ERR_CTK_KHONG_TON_TAI': ('Phiếu chứng từ kho không tồn tại.', 'Warehouse voucher does not exist.', '仓库凭证不存在。'),
    'ERR_CTK_KIEMKE_THIEU_KHONHAP': ('Phiếu kiểm kê thiếu thông tin kho nhập.', 'Inventory voucher is missing receiving warehouse.', '盘点单缺少收货仓库信息。'),
    'ERR_CTK_KIEMKE_THIEU_KHOXUAT': ('Phiếu kiểm kê thiếu thông tin kho xuất.', 'Inventory voucher is missing issuing warehouse.', '盘点单缺少发货仓库信息。'),
    'ERR_CTK_MA_RONG': ('Mã chứng từ kho không được để trống.', 'Warehouse voucher code cannot be empty.', '仓库凭证代码不能为空。'),
    'ERR_CTK_SOLUONG_AM': ('Số lượng chứng từ kho không hợp lệ.', 'Warehouse voucher quantity is invalid.', '仓库凭证数量无效。'),
    'ERR_CTK_THIEU_CHITIET': ('Phiếu chứng từ kho chưa có chi tiết.', 'Warehouse voucher has no details.', '仓库凭证没有明细。'),
    'ERR_CTK_THIEU_KHONGUON_KHODICH': ('Thiếu thông tin kho nguồn hoặc kho đích.', 'Missing source or destination warehouse info.', '缺少源仓库或目标仓库信息。'),
    'ERR_CTK_TRUNG_KHO_XUAT_NHAP': ('Kho xuất và kho nhập không được trùng nhau.', 'Issuing and receiving warehouses cannot be the same.', '发货仓库和收货仓库不能相同。'),
    'ERR_DUP_UNIT_GRID': ('Đơn vị tính này đã tồn tại trong danh sách.', 'This unit already exists in the list.', '此单位已存在于列表中。'),
    'ERR_GIU_SO_LUONG_AM': ('Số lượng giữ chỗ không được âm.', 'Reserved quantity cannot be negative.', '预留数量不能为负。'),
    'ERR_GIU_THIEU_DONHANG': ('Thiếu thông tin đơn hàng giữ chỗ.', 'Missing reservation order info.', '缺少预留订单信息。'),
    'ERR_GIU_THIEU_SANPHAM': ('Thiếu thông tin sản phẩm giữ chỗ.', 'Missing reservation product info.', '缺少预留产品信息。'),
    'ERR_GIU_THOIGIAN_SAI': ('Thời gian giữ chỗ không hợp lệ.', 'Reservation time is invalid.', '预留时间无效。'),
    'ERR_KHO_MA_RONG': ('Mã kho không được để trống.', 'Warehouse code cannot be empty.', '仓库代码不能为空。'),
    'ERR_KHO_TEN_RONG': ('Tên kho không được để trống.', 'Warehouse name cannot be empty.', '仓库名称不能为空。'),
    'ERR_KHO_TRUNG_MA': ('Mã kho đã tồn tại trong hệ thống.', 'Warehouse code already exists.', '仓库代码已存在。'),
    'ERR_KH_CCCD_TRUNG': ('Số CCCD đã tồn tại trong hệ thống.', 'ID card number already exists.', '身份证号码已存在。'),
    'ERR_LOHANG_HSD_NHO_HON_NSX': ('Hạn sử dụng không được nhỏ hơn ngày sản xuất.', 'Expiration date cannot be earlier than manufacture date.', '保质期不能早于生产日期。'),
    'ERR_LOHANG_MA_RONG': ('Mã lô hàng không được để trống.', 'Batch code cannot be empty.', '批次代码不能为空。'),
    'ERR_LOHANG_NSX_TUONGLAI': ('Ngày sản xuất không được lớn hơn ngày hiện tại.', 'Manufacture date cannot be in the future.', '生产日期不能是将来的日期。'),
    'ERR_LOHANG_THIEU_SANPHAM': ('Thiếu thông tin sản phẩm cho lô hàng.', 'Missing product info for the batch.', '批次缺少产品信息。'),
    'ERR_PHANQUYEN_INVALID_ROLE': ('Vai trò phân quyền không hợp lệ.', 'Invalid permission role.', '无效的权限角色。'),
    'ERR_RENTAL_NO_PRICE': ('Sản phẩm cho thuê chưa được thiết lập giá.', 'Rental product has no price set.', '租赁产品未设置价格。'),
    'ERR_RFID_KHONG_TIM_THAY': ('Không tìm thấy thẻ RFID trên hệ thống.', 'RFID card not found in the system.', '系统中未找到RFID卡。'),
    'ERR_RFID_LUONG_KHONG_HOP_LE': ('Số lượng thẻ RFID không hợp lệ.', 'Invalid RFID card quantity.', 'RFID卡数量无效。'),
    'ERR_RFID_MA_RONG': ('Mã thẻ RFID không được để trống.', 'RFID card code cannot be empty.', 'RFID卡代码不能为空。'),
    'ERR_RFID_MA_TRUNG': ('Mã thẻ RFID đã tồn tại trong hệ thống.', 'RFID card code already exists.', 'RFID卡代码已存在。'),
    'ERR_RFID_TRANG_THAI_TRUNG': ('Trạng thái thẻ RFID không thay đổi.', 'RFID card status is unchanged.', 'RFID卡状态未改变。'),
    'ERR_SYSTEM_FAIL': ('Lỗi hệ thống. Vui lòng liên hệ quản trị viên.', 'System error. Please contact administrator.', '系统错误。请联系管理员。'),
    'ERR_VI_CHUA_CO': ('Ví điện tử chưa được khởi tạo.', 'E-wallet is not initialized.', '电子钱包未初始化。'),
    'ERR_VI_DA_CO': ('Ví điện tử đã tồn tại.', 'E-wallet already exists.', '电子钱包已存在。')
}

import re
file_path = r'c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Properties\UpdateResx.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

# Replace any new {...} block that has these keys with the EXACT translations provided above
for key, (vi, en, zh) in translations.items():
    pattern = re.compile(rf'new\s*\{{\s*Key\s*=\s*"{key}",\s*Vi\s*=\s*"[^"]*",\s*En\s*=\s*"[^"]*",\s*Zh\s*=\s*"[^"]*"\s*\}}')
    repl = f'new {{ Key = "{key}", Vi = "{vi}", En = "{en}", Zh = "{zh}" }}'
    content = pattern.sub(repl, content)

with open(file_path, 'w', encoding='utf-8') as f:
    f.write(content)

print("Trash and missed translations applied successfully!")
