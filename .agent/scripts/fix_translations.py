import re

def main():
    file_path = r'c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Properties\UpdateResx.cs'
    with open(file_path, 'r', encoding='utf-8') as f:
        content = f.read()

    # Dictionary for common known keys
    known_keys = {
        'ERR_CUST_EMPTY_KEYWORD': ('Vui lòng nhập từ khóa tìm kiếm', 'Please enter a search keyword', '请输入搜索关键字'),
        'ERR_CUST_NOT_FOUND': ('Không tìm thấy khách hàng', 'Customer not found', '找不到客户'),
        'ERR_CUST_RFID_NOT_FOUND': ('Không tìm thấy thẻ RFID của khách', 'Customer RFID not found', '找不到客户RFID'),
        'ERR_DIEM_KHONG_DU': ('Điểm tích lũy không đủ', 'Insufficient points', '积分不足'),
        'ERR_DIEM_SO_AM': ('Số điểm không được âm', 'Points cannot be negative', '积分不能为负'),
        'ERR_KH_SDT_RONG': ('Số điện thoại không được để trống', 'Phone number cannot be empty', '电话号码不能为空'),
        'ERR_KH_SDT_TRUNG': ('Số điện thoại đã tồn tại', 'Phone number already exists', '电话号码已存在'),
        'ERR_KH_TEN_RONG': ('Tên khách hàng không được để trống', 'Customer name cannot be empty', '客户名称不能为空'),
        'ERR_POS_NO_OPEN_SESSION': ('Không có phiên thu ngân nào đang mở', 'No open cashier session', '没有开启的收银班次'),
        'ERR_POS_CART_EMPTY': ('Giỏ hàng trống', 'Cart is empty', '购物车为空'),
        'ERR_POS_PAYMENT_INSUFFICIENT': ('Số tiền thanh toán không đủ', 'Insufficient payment amount', '支付金额不足'),
        'ERR_POS_PRODUCT_NOT_FOUND': ('Không tìm thấy sản phẩm này', 'Product not found', '找不到此产品'),
        'MSG_POS_MULTI_RESULT': ('Tìm thấy nhiều sản phẩm, vui lòng chọn cụ thể', 'Multiple products found, please select one', '找到多个产品，请具体选择'),
        'ERR_POS_RFID_EMPTY': ('Vui lòng quét thẻ RFID', 'Please scan RFID card', '请扫描RFID卡'),
        'ERR_POS_CUSTOMER_REQUIRED': ('Vui lòng chọn khách hàng', 'Customer selection required', '请选择客户'),
        'MSG_POS_CART_NOT_EMPTY': ('Giỏ hàng đang có sản phẩm, bạn có chắc muốn hủy?', 'Cart is not empty, are you sure you want to cancel?', '购物车非空，您确定要取消吗？'),
        'TXT_CHON_ANH': ('Click vào đây để chọn ảnh', 'Click here to select an image', '点击此处选择图片'),
        'TXT_CHON_DIEMBAN': ('(Chọn Điểm Bán POS)', '(Select POS)', '(选择收银点)'),
        'TXT_CHON_DVT': ('(Chọn ĐVT)', '(Select Unit)', '(选择单位)'),
        'TXT_CHON_NGUYEN_LIEU': ('(Chọn nguyên liệu)', '(Select material)', '(选择材料)'),
        'TXT_CHON_THUE': ('(Chọn Mức Thuế)', '(Select Tax)', '(选择税率)'),
        'TXT_CHUA_CO': ('(Chưa có)', '(None)', '(无)'),
        'TXT_KHONG_CAU_HINH': ('Loại sản phẩm này không có cấu hình vận hành riêng', 'This product type has no specific configuration', '此产品类型没有特定配置'),
        'TXT_KH_KHACH': ('Khách hàng', 'Customer', '客户'),
        'TXT_TONG': ('Tổng cộng:', 'Total:', '总计:'),
        'VIP': ('Khách VIP', 'VIP Customer', '贵宾客户'),
        'VVIP': ('Khách VVIP', 'VVIP Customer', 'VVIP客户'),
        'VangGold': ('Vàng', 'Gold', '黄金'),
        'WATERMARK_TIM_KIEM': ('Tìm theo tên, mã sản phẩm...', 'Search by name, code...', '按名称、代码搜索...'),
        'XUAT_BAOTRI': ('Xuất vật tư bảo trì', 'Issue maintenance supplies', '发放维修物资'),
        'XUAT_SANXUAT': ('Xuất sản xuất (BOM)', 'Issue production (BOM)', '发放生产物资(BOM)')
    }

    # Replace known keys first
    for k, (vi, en, zh) in known_keys.items():
        pattern = re.compile(rf'new\s*\{{\s*Key\s*=\s*"{k}",\s*Vi\s*=\s*"[^"]*",\s*En\s*=\s*"[^"]*",\s*Zh\s*=\s*"[^"]*"\s*\}}')
        repl = f'new {{ Key = "{k}", Vi = "{vi}", En = "{en}", Zh = "{zh}" }}'
        content = pattern.sub(repl, content)

    # General fallback for ERR_
    def replacer_err(match):
        key = match.group(1)
        if key in known_keys:
            return match.group(0)
        words = key.replace('ERR_', '').split('_')
        readable = ' '.join(words).lower().capitalize()
        return f'new {{ Key = "{key}", Vi = "Lỗi: {readable}", En = "Error: {readable}", Zh = "错误: {readable}" }}'
    
    content = re.sub(r'new\s*\{\s*Key\s*=\s*"([^"]+)",\s*Vi\s*=\s*"Lỗi:\s*\1",\s*En\s*=\s*"\1",\s*Zh\s*=\s*"\1"\s*\}', replacer_err, content)

    # General fallback for TXT_ or other duplicated placeholders
    def replacer_dup(match):
        key = match.group(1)
        if key in known_keys:
            return match.group(0)
        vi = match.group(2)
        return f'new {{ Key = "{key}", Vi = "{vi}", En = "{vi} (EN)", Zh = "{vi} (ZH)" }}'
    
    content = re.sub(r'new\s*\{\s*Key\s*=\s*"([^"]+)",\s*Vi\s*=\s*"([^"]+)",\s*En\s*=\s*"\2",\s*Zh\s*=\s*"\2"\s*\}', replacer_dup, content)

    with open(file_path, 'w', encoding='utf-8') as f:
        f.write(content)

if __name__ == "__main__":
    main()
