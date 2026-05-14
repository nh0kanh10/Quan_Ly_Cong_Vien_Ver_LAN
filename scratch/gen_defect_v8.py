import csv
import os

target_tcs = {
    'TC_PhanQuyen.csv': [
        'TC_PQ_012', 'TC_PQ_024', 'TC_PQ_026', 'TC_PQ_032'
    ],
    'TC_Combo.csv': [
        'TC_CB_014', 'TC_CB_052', 'TC_CB_063', 'TC_CB_079', 'TC_CB_087', 'TC_CB_099', 'TC_CB_100'
    ],
    'TC_SanPham.csv': [
        'TC_SP_031', 'TC_SP_032', 'TC_SP_035', 'TC_SP_052', 'TC_SP_077', 'TC_SP_081', 'TC_SP_096', 'TC_SP_097', 'TC_SP_104'
    ]
}

actual_results_map = {
    'TC_PQ_012': 'Hệ thống không hiển thị thông báo cảnh báo nào, nút Lưu không có tác dụng nhưng không báo lỗi rõ ràng cho người dùng.',
    'TC_PQ_024': 'Hệ thống không có cảnh báo mất dữ liệu (Dirty Tracking). Khi chuyển vai trò, các phân quyền chưa lưu bị xóa trắng mà không hỏi xác nhận.',
    'TC_PQ_026': 'Hệ thống đóng màn hình ngay lập tức mà không kiểm tra trạng thái dữ liệu dở dang chưa lưu.',
    'TC_PQ_032': 'Hệ thống vẫn hiển thị các nhóm quyền rỗng (không có quyền con nào) trên cây phân quyền, gây rối giao diện.',
    
    'TC_CB_014': 'Hệ thống vẫn cho phép lưu Combo với tên bỏ trống mà không hiển thị cảnh báo đỏ hoặc chặn lại.',
    'TC_CB_052': 'Label Tổng phân bổ không đổi màu đỏ và vẫn cho phép lưu khi tổng lớn hơn 100%, gây sai lệch logic nghiệp vụ.',
    'TC_CB_063': 'Hệ thống cho phép lưu Combo ở trạng thái Hoạt động dù lưới chi tiết rổ Combo trống hoàn toàn.',
    'TC_CB_079': 'Hệ thống văng lỗi SqlException (String or binary data would be truncated) do không giới hạn độ dài textbox Mô tả ở giao diện.',
    'TC_CB_087': 'Giao diện không báo lỗi trùng lặp khi người dùng nhập, hệ thống đẩy thẳng xuống DB và bị văng SqlException Unique Key Constraint.',
    'TC_CB_099': 'Hệ thống không chặn việc xóa số lượng, cho phép lưu số lượng trống (null) dẫn đến lỗi NullReferenceException khi tính toán doanh thu.',
    'TC_CB_100': 'Hệ thống cho phép nhập và lưu tỷ lệ phân bổ trống (0%), dẫn đến lỗi DivideByZeroException ở các hàm tính giá.',
    
    'TC_SP_031': 'Hệ thống không có validation Required Field cho Mã SP, cho phép lưu xuống DB và bị văng lỗi ràng buộc Not Null.',
    'TC_SP_032': 'Hệ thống không báo lỗi khi bỏ trống Tên SP, vẫn hiển thị popup Lưu thành công với Tên trống.',
    'TC_SP_035': 'Hệ thống văng lỗi SqlException thay vì hiển thị thông báo lỗi thân thiện "Mã sản phẩm đã tồn tại" trên giao diện.',
    'TC_SP_052': 'Hệ thống cho phép lưu hệ số quy đổi là số âm mà không có ErrorProvider cảnh báo trên TextBox.',
    'TC_SP_077': 'Hệ thống cho phép lưu ĐVT quy đổi trùng với ĐVT gốc, gây sai lệch logic tính toán tồn kho.',
    'TC_SP_081': 'Lưới BOM (Định mức) cho phép thêm nhiều lần cùng một nguyên liệu mà không gộp số lượng hoặc cảnh báo trùng lặp.',
    'TC_SP_096': 'Ô nhập VAT không được thiết lập giới hạn MinValue=0, hệ thống cho phép lưu giá trị thuế VAT âm.',
    'TC_SP_097': 'Ô nhập VAT không được thiết lập giới hạn MaxValue=100, hệ thống cho phép lưu giá trị thuế VAT vượt quá 100%.',
    'TC_SP_104': 'Hệ thống không kiểm tra logic nghiệp vụ, cho phép lưu mức tiền cọc âm hoặc tiền cọc lớn hơn giá trị thực tế của sản phẩm.'
}

screen_map = {
    'TC_PhanQuyen.csv': 'Màn hình Phân Quyền',
    'TC_Combo.csv': 'Màn hình Combo',
    'TC_SanPham.csv': 'Màn hình Sản phẩm'
}

output_rows = []
defect_idx = 1
base_path = r'c:\Users\ADMIN\Desktop\DaiNamNew\docs\TestCase'

for filename, tc_list in target_tcs.items():
    file_path = os.path.join(base_path, filename)
    try:
        with open(file_path, 'r', encoding='utf-8-sig') as f:
            reader = csv.DictReader(f)
            tc_col = next((c for c in reader.fieldnames if 'TC ID' in c or 'TC_ID' in c), None)
            title_col = next((c for c in reader.fieldnames if 'Title' in c), None)
            pre_col = next((c for c in reader.fieldnames if 'Precondition' in c or 'Pre-condition' in c), None)
            steps_col = next((c for c in reader.fieldnames if 'Steps' in c or 'Procedure' in c), None)
            exp_col = next((c for c in reader.fieldnames if 'Expected' in c), None)
            
            for row in reader:
                tc_id = row.get(tc_col, '').strip()
                if tc_id in tc_list:
                    pre_cond = row.get(pre_col, '') if pre_col else ''
                    steps = row.get(steps_col, '') if steps_col else ''
                    expected = row.get(exp_col, '') if exp_col else ''
                    title = row.get(title_col, '') if title_col else ''
                    
                    actual = actual_results_map.get(tc_id, 'Hệ thống lỗi logic.')
                    severity = 'High' if 'Exception' in actual or 'âm' in actual or 'sai lệch' in actual else 'Medium'
                    
                    output_rows.append({
                        'Title': f'[DF_{defect_idx:03d}] {title}',
                        'Screen': screen_map[filename],
                        'Pre-condition': pre_cond,
                        'Steps': steps,
                        'Test Data': '',
                        'Expected Results': expected,
                        'Actual results': actual,
                        'Priority': 'High',
                        'Serverity': severity,
                        'Date': '24/04/2026',
                        'Owner': 'QA Team',
                        "TC'ID": tc_id,
                        'Build': 'Sprint 1',
                        'System': 'Đại Nam POS',
                        'Assign To': 'Dev Team',
                        'Status': 'New',
                        'Evident (Hình ảnh lỗi)': ''
                    })
                    defect_idx += 1
                    if defect_idx > 20: break
    except Exception as e:
        print(f'Error reading {filename}: {e}')

output_file = r'c:\Users\ADMIN\Desktop\DaiNamNew\docs\Sprint1\DefectList_Sprint1_Final.csv'
with open(output_file, 'w', encoding='utf-8-sig', newline='') as f:
    fields = ['Title', 'Screen', 'Pre-condition', 'Steps', 'Test Data', 'Expected Results', 'Actual results', 'Priority', 'Serverity', 'Date', 'Owner', "TC'ID", 'Build', 'System', 'Assign To', 'Status', 'Evident (Hình ảnh lỗi)']
    writer = csv.DictWriter(f, fieldnames=fields)
    writer.writeheader()
    for r in output_rows:
        writer.writerow(r)

print(f'Successfully wrote {len(output_rows)} perfectly matching defects.')
