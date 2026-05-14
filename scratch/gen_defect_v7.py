import csv

def create_defect_list():
    defects = [
        # Phân Quyền
        {
            "Title": "[DF_001] Thiếu validation khi Lưu phân quyền nhưng chưa chọn Vai trò",
            "Screen": "Màn hình Phân Quyền",
            "Pre-condition": "Đang ở màn hình Phân Quyền, danh sách Vai trò chưa được chọn.",
            "Steps": "Bước 1. Tick chọn một vài quyền trên cây.\nBước 2. Nhấn nút Lưu thay đổi.",
            "Expected Results": "Hệ thống chặn lại và hiển thị thông báo: 'Vui lòng chọn vai trò cần phân quyền'.",
            "Actual results": "Hệ thống không hiển thị thông báo lỗi, bỏ qua lệnh lưu và không có bất kỳ cảnh báo valid nào cho người dùng.",
            "Priority": "High", "Serverity": "Medium", "TC'ID": "TC_PQ_012", "Module": "Phân Quyền"
        },
        {
            "Title": "[DF_002] Không cảnh báo mất dữ liệu khi chuyển Vai trò (Dirty Tracking)",
            "Screen": "Màn hình Phân Quyền",
            "Pre-condition": "Đã chọn Vai trò A và có chỉnh sửa tick chọn quyền trên cây nhưng chưa Lưu.",
            "Steps": "Bước 1. Chỉnh sửa quyền cho Vai trò A.\nBước 2. Nhấn chọn sang Vai trò B trên danh sách.",
            "Expected Results": "Hệ thống hiển thị hộp thoại cảnh báo: 'Bạn có dữ liệu chưa lưu. Bạn có muốn lưu lại không?'.",
            "Actual results": "Hệ thống tự động tải quyền của Vai trò B đè lên, làm mất hoàn toàn các thao tác chưa lưu của Vai trò A mà không hề cảnh báo.",
            "Priority": "High", "Serverity": "High", "TC'ID": "TC_PQ_023", "Module": "Phân Quyền"
        },
        {
            "Title": "[DF_003] Thiếu cờ kiểm tra dữ liệu chưa lưu khi đóng màn hình",
            "Screen": "Màn hình Phân Quyền",
            "Pre-condition": "Đang thao tác phân quyền dở dang trên giao diện.",
            "Steps": "Bước 1. Tick chọn thêm quyền mới.\nBước 2. Nhấn nút [X] hoặc tắt tab màn hình Phân quyền.",
            "Expected Results": "Hệ thống chặn đóng form và cảnh báo dữ liệu chưa được lưu.",
            "Actual results": "Hệ thống đóng màn hình ngay lập tức, không kiểm tra trạng thái dữ liệu (Dirty state), gây mất dữ liệu.",
            "Priority": "High", "Serverity": "High", "TC'ID": "TC_PQ_026", "Module": "Phân Quyền"
        },
        {
            "Title": "[DF_004] Logic tick chọn Node cha không tự động tick Node con",
            "Screen": "Màn hình Phân Quyền",
            "Pre-condition": "Cây phân quyền đang hiển thị đầy đủ các Module và chức năng con.",
            "Steps": "Bước 1. Tick chọn vào một Node cha (Ví dụ: Quản lý Bán hàng).",
            "Expected Results": "Tất cả các Node con bên trong (Thêm, Sửa, Xóa đơn hàng) phải tự động được tick chọn.",
            "Actual results": "Hệ thống không tự động tick các nút con, bắt buộc người dùng phải mở rộng cây và tick thủ công từng dòng.",
            "Priority": "Medium", "Serverity": "Medium", "TC'ID": "TC_PQ_006", "Module": "Phân Quyền"
        },
        {
            "Title": "[DF_005] Trạng thái nửa tích (Indeterminate) của Node cha hoạt động sai logic",
            "Screen": "Màn hình Phân Quyền",
            "Pre-condition": "Node cha đang được tick chọn toàn bộ Node con.",
            "Steps": "Bước 1. Bỏ tick một Node con bất kỳ.",
            "Expected Results": "Node cha chuyển sang trạng thái màu xám (nửa tích) để báo hiệu Module chưa được cấp toàn quyền.",
            "Actual results": "Node cha không cập nhật trạng thái hiển thị, dẫn đến sai lệch trực quan về quyền hạn của nhóm.",
            "Priority": "Medium", "Serverity": "Low", "TC'ID": "TC_PQ_008", "Module": "Phân Quyền"
        },
        # Combo
        {
            "Title": "[DF_006] Lỗi nghiệp vụ: Cho phép lưu Combo khi Tổng tỷ lệ phân bổ khác 100%",
            "Screen": "Màn hình Combo",
            "Pre-condition": "Đang tạo mới một Combo gồm 2 sản phẩm con.",
            "Steps": "Bước 1. Nhập tỷ lệ SP 1 là 50%.\nBước 2. Nhập tỷ lệ SP 2 là 40% (Tổng 90%).\nBước 3. Nhấn Lưu Combo.",
            "Expected Results": "Hệ thống chặn lại và báo lỗi: 'Tổng tỷ lệ phân bổ doanh thu phải bằng đúng 100%'.",
            "Actual results": "Hệ thống không kiểm tra tổng tỷ lệ, cho phép lưu thành công, gây sai lệch nghiêm trọng khi hạch toán doanh thu bán hàng.",
            "Priority": "High", "Serverity": "High", "TC'ID": "TC_CB_100", "Module": "Combo"
        },
        {
            "Title": "[DF_007] Thiếu validation: Cho phép thêm sản phẩm vào Combo với tỷ lệ 0%",
            "Screen": "Màn hình Combo",
            "Pre-condition": "Đang gán sản phẩm chi tiết vào Combo.",
            "Steps": "Bước 1. Chọn sản phẩm 'Vé vào cổng'.\nBước 2. Nhập Tỷ lệ phân bổ = 0.\nBước 3. Nhấn Thêm vào lưới.",
            "Expected Results": "Hệ thống cảnh báo tỷ lệ phân bổ phải lớn hơn 0.",
            "Actual results": "Hệ thống chấp nhận tỷ lệ 0%, dẫn đến lỗi chia cho 0 hoặc sai logic khi tính giá trị phân bổ thực tế.",
            "Priority": "High", "Serverity": "High", "TC'ID": "TC_CB_099", "Module": "Combo"
        },
        {
            "Title": "[DF_008] Thiếu validation: Cho phép Ngày Kết Thúc nhỏ hơn Ngày Bắt Đầu",
            "Screen": "Màn hình Combo",
            "Pre-condition": "Đang nhập thời gian áp dụng Combo.",
            "Steps": "Bước 1. Chọn Từ ngày: 15/05/2026.\nBước 2. Chọn Đến ngày: 10/05/2026.\nBước 3. Nhấn Lưu.",
            "Expected Results": "Hệ thống báo lỗi logic thời gian không hợp lệ.",
            "Actual results": "Hệ thống bỏ qua validation thời gian và lưu thành công.",
            "Priority": "High", "Serverity": "Medium", "TC'ID": "TC_CB_024", "Module": "Combo"
        },
        {
            "Title": "[DF_009] Không bắt lỗi trùng tên Combo trên giao diện",
            "Screen": "Màn hình Combo",
            "Pre-condition": "Trong hệ thống đã tồn tại Combo tên 'Combo Mùa Hè'.",
            "Steps": "Bước 1. Nhập Tên Combo: 'Combo Mùa Hè'.\nBước 2. Nhấn Lưu.",
            "Expected Results": "Giao diện hiển thị cảnh báo 'Tên Combo đã tồn tại, vui lòng nhập tên khác'.",
            "Actual results": "Giao diện không bắt lỗi trùng lặp (Duplicate validation), dữ liệu bị đẩy thẳng xuống DB và bị từ chối ở tầng dữ liệu.",
            "Priority": "Medium", "Serverity": "Medium", "TC'ID": "TC_CB_087", "Module": "Combo"
        },
        {
            "Title": "[DF_010] Thiếu validation: Giá bán Combo cho phép nhập số âm",
            "Screen": "Màn hình Combo",
            "Pre-condition": "Đang tạo Combo mới.",
            "Steps": "Bước 1. Nhập giá bán: -500000.\nBước 2. Nhấn Lưu.",
            "Expected Results": "Hệ thống không cho phép nhập số âm ở ô Giá bán.",
            "Actual results": "Ô nhập liệu không có ràng buộc giá trị tối thiểu (Min value = 0), cho phép nhập và lưu số âm.",
            "Priority": "High", "Serverity": "High", "TC'ID": "TC_CB_027", "Module": "Combo"
        },
        {
            "Title": "[DF_011] Lỗi Logic trạng thái: Combo đã duyệt vẫn cho sửa giá",
            "Screen": "Màn hình Combo",
            "Pre-condition": "Mở một Combo đang ở trạng thái 'Đang hoạt động' (Đã duyệt).",
            "Steps": "Bước 1. Thay đổi thông tin Giá bán.\nBước 2. Nhấn Lưu.",
            "Expected Results": "Hệ thống chặn không cho sửa Giá bán khi Combo đã được duyệt và đang áp dụng bán.",
            "Actual results": "Hệ thống không khóa (Disable) trường Giá bán, cho phép sửa đổi tự do gây mất tính đồng nhất của dữ liệu kế toán.",
            "Priority": "High", "Serverity": "High", "TC'ID": "TC_CB_028", "Module": "Combo"
        },
        {
            "Title": "[DF_012] Lỗi giao diện: Nút Xóa chi tiết không bị mờ khi lưới trống",
            "Screen": "Màn hình Combo",
            "Pre-condition": "Bảng chi tiết sản phẩm của Combo đang trống.",
            "Steps": "Bước 1. Quan sát nút 'Xóa dòng' trên thanh công cụ của lưới.",
            "Expected Results": "Nút Xóa dòng phải ở trạng thái Disable (mờ đi).",
            "Actual results": "Nút Xóa dòng vẫn Enable, click vào không có tác dụng nhưng vi phạm tiêu chuẩn UI/UX.",
            "Priority": "Low", "Serverity": "Low", "TC'ID": "TC_CB_090", "Module": "Combo"
        },
        # Sản Phẩm
        {
            "Title": "[DF_013] Lỗi nghiệp vụ: Chồng chéo thời gian Bảng giá Sản phẩm",
            "Screen": "Màn hình Sản phẩm",
            "Pre-condition": "Sản phẩm A đã có bảng giá hiệu lực từ 01/05 - 31/05.",
            "Steps": "Bước 1. Thêm một bảng giá mới cho Sản phẩm A.\nBước 2. Chọn hiệu lực từ 15/05 - 15/06.\nBước 3. Nhấn Lưu.",
            "Expected Results": "Hệ thống báo lỗi: 'Thời gian hiệu lực của bảng giá bị chồng chéo với bảng giá đã tồn tại'.",
            "Actual results": "Hệ thống không có logic kiểm tra sự giao nhau của thời gian, cho phép lưu thành công gây lỗi nghiêm trọng khi POS truy xuất giá bán.",
            "Priority": "Critical", "Serverity": "Critical", "TC'ID": "TC_SP_107", "Module": "Sản Phẩm"
        },
        {
            "Title": "[DF_014] Thiếu validation: Tên sản phẩm toàn khoảng trắng",
            "Screen": "Màn hình Sản phẩm",
            "Pre-condition": "Đang thêm mới sản phẩm.",
            "Steps": "Bước 1. Nhập Tên sản phẩm là 5 dấu cách (Space).\nBước 2. Nhấn Lưu.",
            "Expected Results": "Hệ thống tự động Trim() và báo lỗi 'Vui lòng nhập tên sản phẩm'.",
            "Actual results": "Hệ thống không xử lý Trim() chuỗi nhập, cho phép lưu Sản phẩm với tên rỗng (chỉ có khoảng trắng).",
            "Priority": "Medium", "Serverity": "Medium", "TC'ID": "TC_SP_091", "Module": "Sản Phẩm"
        },
        {
            "Title": "[DF_015] Thiếu validation miền giá trị Thuế VAT",
            "Screen": "Màn hình Sản phẩm",
            "Pre-condition": "Đang thiết lập thông tin thuế cho sản phẩm.",
            "Steps": "Bước 1. Nhập VAT (%): -5 hoặc 105.\nBước 2. Nhấn Lưu.",
            "Expected threshold": "Hệ thống giới hạn nhập VAT trong khoảng 0 đến 100.",
            "Actual results": "SpinEdit (Ô nhập số) không được cấu hình MinValue/MaxValue, cho phép nhập số âm hoặc số > 100.",
            "Priority": "Medium", "Serverity": "Medium", "TC'ID": "TC_SP_096", "Module": "Sản Phẩm"
        },
        {
            "Title": "[DF_016] Lỗi Logic: Tiền cọc lớn hơn Giá bán sản phẩm",
            "Screen": "Màn hình Sản phẩm",
            "Pre-condition": "Đang thiết lập thông tin Thuê Tài Sản.",
            "Steps": "Bước 1. Nhập Giá bán: 50,000.\nBước 2. Nhập Tiền cọc: 500,000.\nBước 3. Nhấn Lưu.",
            "Expected Results": "Hệ thống cảnh báo Tiền cọc không được lớn hơn Giá trị sản phẩm (hoặc có cảnh báo nhắc nhở).",
            "Actual results": "Hệ thống không kiểm tra tính hợp lý giữa Tiền cọc và Giá bán.",
            "Priority": "Medium", "Serverity": "Low", "TC'ID": "TC_SP_104", "Module": "Sản Phẩm"
        },
        {
            "Title": "[DF_017] Lỗi Logic Bảng giá Mặc định",
            "Screen": "Màn hình Sản phẩm",
            "Pre-condition": "Sản phẩm chỉ có duy nhất 1 bảng giá và đang là Mặc định.",
            "Steps": "Bước 1. Bỏ tick ô 'Mặc định' của bảng giá đó.\nBước 2. Nhấn Lưu.",
            "Expected Results": "Hệ thống bắt buộc mỗi sản phẩm phải có ít nhất 1 bảng giá mặc định, không cho phép bỏ tick nếu là bảng giá cuối cùng.",
            "Actual results": "Hệ thống cho phép bỏ tick, dẫn đến sản phẩm bị mất giá cơ sở để hiển thị trên POS.",
            "Priority": "High", "Serverity": "High", "TC'ID": "TC_SP_136", "Module": "Sản Phẩm"
        },
        {
            "Title": "[DF_018] Thiếu liên kết Logic khi chọn Loại Nhóm Hàng",
            "Screen": "Màn hình Sản phẩm",
            "Pre-condition": "Thêm mới sản phẩm.",
            "Steps": "Bước 1. Chọn Nhóm hàng hóa là 'F&B' (Chế biến).\nBước 2. Không nhập thông tin Định mức nguyên liệu (BOM).\nBước 3. Nhấn Lưu.",
            "Expected Results": "Hệ thống yêu cầu: Sản phẩm F&B bắt buộc phải có định mức nguyên vật liệu.",
            "Actual results": "Hệ thống cho phép lưu sản phẩm F&B trống định mức, gây lỗi khi thực hiện nghiệp vụ trừ kho.",
            "Priority": "High", "Serverity": "Medium", "TC'ID": "TC_SP_037", "Module": "Sản Phẩm"
        },
        {
            "Title": "[DF_019] Không giới hạn độ dài textbox Cảnh báo dị ứng",
            "Screen": "Màn hình Sản phẩm",
            "Pre-condition": "Nhập liệu cho mặt hàng F&B.",
            "Steps": "Bước 1. Tại trường Cảnh báo dị ứng, copy và dán một đoạn text dài 2000 ký tự.\nBước 2. Nhấn Lưu.",
            "Expected Results": "Textbox tự động chặn nhập quá 500 ký tự (MaxLength = 500).",
            "Actual results": "Textbox cho phép nhập vô hạn ký tự, dữ liệu bị cắt cụt (truncate) hoặc gây lỗi ở tầng CSDL.",
            "Priority": "Low", "Serverity": "Medium", "TC'ID": "TC_SP_113", "Module": "Sản Phẩm"
        },
        {
            "Title": "[DF_020] Cảnh báo lưu dữ liệu trùng lặp Bảng Giá",
            "Screen": "Màn hình Sản phẩm",
            "Pre-condition": "Đang thiết lập giá áp dụng cho Đối tượng 'Người lớn'.",
            "Steps": "Bước 1. Thêm 1 dòng giá: Khách Lẻ - Người lớn - 100k.\nBước 2. Thêm tiếp 1 dòng giá y hệt: Khách Lẻ - Người lớn - 150k.\nBước 3. Nhấn Lưu.",
            "Expected Results": "Lưới (Grid) báo đỏ hoặc chặn lưu do cấu hình giá bị trùng lặp đối tượng áp dụng.",
            "Actual results": "Hệ thống không phát hiện trùng lặp trên Grid, cho phép đẩy xuống CSDL.",
            "Priority": "High", "Serverity": "High", "TC'ID": "TC_SP_109", "Module": "Sản Phẩm"
        }
    ]

    output_file = r'c:\Users\ADMIN\Desktop\DaiNamNew\docs\Sprint1\DefectList_Sprint1_Final.csv'
    with open(output_file, 'w', encoding='utf-8-sig', newline='') as f:
        fields = ['Title', 'Screen', 'Pre-condition', 'Steps', 'Test Data', 'Expected Results', 'Actual results', 'Priority', 'Serverity', 'Date', 'Owner', "TC'ID", 'Build', 'System', 'Assign To', 'Status', 'Evident (Hình ảnh lỗi)']
        writer = csv.DictWriter(f, fieldnames=fields)
        writer.writeheader()
        
        for row in defects:
            out = {k: '' for k in fields}
            out.update(row)
            out['Date'] = '24/04/2026'
            out['Owner'] = 'QA Team'
            out['Build'] = 'Sprint 1'
            out['System'] = 'Đại Nam POS'
            out['Assign To'] = 'Dev Team'
            out['Status'] = 'New'
            writer.writerow(out)

if __name__ == '__main__':
    create_defect_list()
    print('Generated strictly professional logic/validation defects.')
