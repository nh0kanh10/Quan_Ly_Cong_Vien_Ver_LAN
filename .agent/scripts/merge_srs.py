
import re

def merge_srs():
    # Đọc file Lưu trú
    with open(r'c:\Users\ADMIN\Desktop\DaiNamNew\docs\Sprint1\DN01_SRS_LuuTru_v1.0.md', 'r', encoding='utf-8') as f:
        content1 = f.read()

    # Đọc file Báo cáo
    with open(r'c:\Users\ADMIN\Desktop\DaiNamNew\docs\Sprint1\08_SRS_BaoCaoDoanhThu_v1.0.md', 'r', encoding='utf-8') as f:
        content2 = f.read()

    # Tách phần chính (Section 1) và phần yêu cầu khác (Section 2) của file 1
    # Tìm vị trí bắt đầu của "# 1. Quản Lý Lưu Trú"
    match_main1 = re.search(r'# 1\. Quản Lý Lưu Trú', content1)
    # Tìm vị trí bắt đầu của "# 2. Yêu cầu khác"
    match_other1 = re.search(r'# 2\. Yêu cầu khác', content1)
    
    lodging_main = content1[match_main1.start():match_other1.start()]
    other_reqs1 = content1[match_other1.start():]

    # Tách phần chính và phần yêu cầu khác của file 2
    match_main2 = re.search(r'# 1\. CN12.*Báo cáo Doanh thu', content2)
    match_other2 = re.search(r'# 2\. Yêu cầu khác', content2)
    
    reporting_main = content2[match_main2.start():match_other2.start()]
    other_reqs2 = content2[match_other2.start():]

    # Xử lý reporting_main: Đổi các mục 1.x thành 2.x và tiêu đề mục 1 thành 2
    reporting_main = re.sub(r'# 1\. CN12.*Báo cáo Doanh thu', '# 2. Báo Cáo Doanh Thu', reporting_main)
    # Đổi 1.1 -> 2.1, 1.2 -> 2.2 ...
    reporting_main = re.sub(r'## 1\.(\d)\.', r'## 2.\1.', reporting_main)
    reporting_main = re.sub(r'### 1\.(\d)\.(\d)\.', r'### 2.\1.\2.', reporting_main)
    reporting_main = re.sub(r'#### 1\.(\d)\.(\d)\.(\d)\.', r'#### 2.\1.\2.\3.', reporting_main)
    
    # Header mới
    header = """# Khu Du Lịch Đại Nam
# Đặc Tả Yêu Cầu Phần Mềm
# Mã dự án: DN01
# Mã tài liệu: DN01_SRS_LuuTru_BaoCao_Merged_v1.1

Hồ Chí Minh, Tháng 04/2026

---

## Lịch sử thay đổi

| Ngày hiệu lực | Hạng mục thay đổi | A/M/D | Mô tả | Phiên bản |
|---|---|---|---|---|
| 25/04/2025 | Toàn bộ tài liệu | A | Tạo mới tài liệu SRS chức năng Lưu Trú | 1.0 |
| 29/04/2026 | Đặc tả Báo cáo Doanh thu | A | Tạo mới tài liệu SRS cho Module Báo cáo Doanh thu | 1.0 |
| 29/04/2026 | Hợp nhất tài liệu | M | Gộp SRS Lưu trú và Báo cáo Doanh thu thành một tài liệu thống nhất | 1.1 |

---

## Mục lục

- [1. Quản Lý Lưu Trú](#1-quản-lý-lưu-trú)
- [2. Báo Cáo Doanh Thu](#2-báo-cáo-doanh-thu)
- [3. Yêu cầu khác](#3-yêu-cầu-khác)

---
"""

    # Gộp nội dung nghiệp vụ
    merged_main = lodging_main + "\n---\n\n" + reporting_main

    # Gộp phần 3: Yêu cầu khác
    # Trích xuất các bảng định dạng dữ liệu (bỏ tiêu đề ## 2.1)
    format_table1 = re.search(r'## 2\.1\. Định dạng dữ liệu\n\n(.*?)\n\n##', other_reqs1, re.S)
    format_table2 = re.search(r'## 2\.1\. Định dạng dữ liệu\n(.*?)\n\n##', other_reqs2, re.S)
    
    # Trích xuất các bảng tham chiếu
    ref_table1 = re.search(r'## 2\.2\. Danh mục dữ liệu tham chiếu\n\n(.*?)\n\n##', other_reqs1, re.S)
    ref_table2 = re.search(r'## 2\.2\. Danh mục dữ liệu tham chiếu\n(.*?)\n\n##', other_reqs2, re.S)

    # Trích xuất bảng mã lỗi
    error_table1 = re.search(r'## 2\.3\. Bảng mã thông báo lỗi\n\n(.*?)$', other_reqs1, re.S)
    error_table2 = re.search(r'## 2\.3\. Bảng mã thông báo lỗi\n\n(.*?)$', other_reqs2, re.S)

    section3 = "\n# 3. Yêu cầu khác\n\n## 3.1. Định dạng dữ liệu\n\n"
    section3 += format_table1.group(1).strip() if format_table1 else ""
    if format_table2: 
        table2_lines = format_table2.group(1).strip().split('\n')
        # Bỏ header của bảng 2 nếu giống bảng 1 (nhưng ở đây là danh sách nên cứ dán xuống)
        section3 += "\n" + "\n".join([line for line in table2_lines if line.startswith('|') and 'Loại dữ liệu' not in line and '---' not in line])

    section3 += "\n\n## 3.2. Danh mục dữ liệu tham chiếu\n\n"
    section3 += ref_table1.group(1).strip() if ref_table1 else ""
    if ref_table2:
        section3 += "\n\n" + ref_table2.group(1).strip()

    section3 += "\n\n## 3.3. Bảng mã thông báo lỗi\n\n| Mã thông báo | Nội dung tiếng Việt |\n|---|---|\n"
    if error_table1:
        lines = error_table1.group(1).strip().split('\n')
        section3 += '\n'.join([l for l in lines if l.startswith('|') and 'Mã thông báo' not in l and '---|---' not in l]) + '\n'
    if error_table2:
        lines = error_table2.group(1).strip().split('\n')
        section3 += '\n'.join([l for l in lines if l.startswith('|') and 'Mã thông báo' not in l and '---|---' not in l]) + '\n'

    # Kết quả cuối cùng
    final_content = header + merged_main + section3

    with open(r'c:\Users\ADMIN\Desktop\DaiNamNew\docs\Sprint1\09_SRS_LuuTru_Va_BaoCao_Merged_v1.1.md', 'w', encoding='utf-8') as f:
        f.write(final_content)

if __name__ == "__main__":
    merge_srs()
