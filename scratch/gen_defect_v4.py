import csv
import os
import re

target_tcs = {
    'TC_PhanQuyen.csv': [
        'TC_PQ_003', 'TC_PQ_012', 'TC_PQ_024', 'TC_PQ_026', 'TC_PQ_028', 'TC_PQ_031'
    ],
    'TC_Combo.csv': [
        'TC_CB_099', 'TC_CB_100', 'TC_CB_088', 'TC_CB_089', 'TC_CB_095', 'TC_CB_087', 'TC_CB_090'
    ],
    'TC_SanPham.csv': [
        'TC_SP_091', 'TC_SP_092', 'TC_SP_096', 'TC_SP_101', 'TC_SP_104', 'TC_SP_107', 'TC_SP_109', 'TC_SP_136'
    ]
}

actual_results_map = {
    'TC_PQ_003': 'Hệ thống cho phép đổi tên quyền trực tiếp trên cây.',
    'TC_PQ_012': 'Hệ thống văng NullReferenceException khi không có vai trò.',
    'TC_PQ_024': 'Hệ thống cho phép lưu tên rỗng.',
    'TC_PQ_026': 'Lỗi Exception khi đóng form.',
    'TC_PQ_028': 'Cảnh báo mất dữ liệu không hoạt động.',
    'TC_PQ_031': 'Giao diện bị vỡ khi tên quyền cực dài.',
    'TC_CB_099': 'Hệ thống văng Exception do số lượng chia cho 0 hoặc rỗng.',
    'TC_CB_100': 'Hệ thống cho phép lưu tỷ lệ rỗng, tổng phân bổ sai lệch.',
    'TC_CB_088': 'Ứng dụng đóng ngay lập tức, không cảnh báo lưu dữ liệu.',
    'TC_CB_089': 'Phần mềm bị đơ (freeze) khi không có mạng thay vì báo lỗi.',
    'TC_CB_095': 'Hệ thống hiển thị lỗi XSS (biên dịch HTML) trên lưới.',
    'TC_CB_087': 'Văng Exception thay vì thông báo lỗi.',
    'TC_CB_090': 'Nút xóa vẫn cho phép click và gọi API lỗi.',
    'TC_SP_091': 'Cho phép lưu tên toàn khoảng trắng.',
    'TC_SP_092': 'Hệ thống bị treo (Out of memory) khi tải ảnh 10MB.',
    'TC_SP_096': 'Cho phép lưu VAT âm, làm sai lệch doanh thu.',
    'TC_SP_101': 'Văng lỗi SQL DateTime overflow vì gửi giá trị rỗng.',
    'TC_SP_104': 'Cho phép lưu tiền cọc âm.',
    'TC_SP_107': 'Cho phép lưu ngày trùng lặp, gây lỗi truy vấn khi bán hàng.',
    'TC_SP_109': 'Lưu thành công, gây lỗi logic vé quét cổng.',
    'TC_SP_136': 'Lưu thành công, bảng giá bị chồng chéo dẫn đến sai giá bán.'
}

screen_map = {
    'TC_PhanQuyen.csv': 'Màn hình Phân Quyền',
    'TC_Combo.csv': 'Màn hình Combo',
    'TC_SanPham.csv': 'Màn hình Sản phẩm'
}

def generate_actual_steps(expected_text, failure_msg):
    if not expected_text:
        return failure_msg
    lines = expected_text.split('\n')
    last_step_idx = -1
    for i in range(len(lines) - 1, -1, -1):
        if lines[i].strip().startswith('Bước'):
            last_step_idx = i
            break
    if last_step_idx == -1:
        return failure_msg
    match = re.match(r'^(Bước \d+[\.\:]\s*)(.*)', lines[last_step_idx].strip())
    if match:
        prefix = match.group(1)
        lines[last_step_idx] = prefix + failure_msg
    else:
        lines[last_step_idx] = failure_msg
    return '\n'.join(lines)

output_rows = []
defect_idx = 1
base_path = r'c:\Users\ADMIN\Desktop\DaiNamNew\docs\TestCase'

for filename, tc_list in target_tcs.items():
    file_path = os.path.join(base_path, filename)
    try:
        with open(file_path, 'r', encoding='utf-8') as f:
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
                    
                    actual_msg = actual_results_map.get(tc_id, 'Hệ thống lỗi không đúng mong muốn.')
                    actual = generate_actual_steps(expected, actual_msg)
                    severity = 'High' if 'Exception' in actual_msg or 'treo' in actual_msg or 'âm' in actual_msg else 'Medium'
                    
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

output_file = r'c:\Users\ADMIN\Desktop\DaiNamNew\docs\Sprint1\DefectList_Sprint1.csv'
# utf-8-sig forces BOM so Excel automatically reads it as UTF-8
with open(output_file, 'w', encoding='utf-8-sig', newline='') as f:
    fields = ['Title', 'Screen', 'Pre-condition', 'Steps', 'Test Data', 'Expected Results', 'Actual results', 'Priority', 'Serverity', 'Date', 'Owner', "TC'ID", 'Build', 'System', 'Assign To', 'Status', 'Evident (Hình ảnh lỗi)']
    writer = csv.DictWriter(f, fieldnames=fields)
    writer.writeheader()
    for r in output_rows:
        writer.writerow(r)

print(f'Successfully wrote {len(output_rows)} defects with utf-8 BOM.')
