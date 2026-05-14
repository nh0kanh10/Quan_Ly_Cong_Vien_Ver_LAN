import sys

with open('database/Database_DaiNam_V2.sql', 'r', encoding='utf-8') as f:
    content = f.read()

lines = content.splitlines(True) # Keep newlines

# Find the start of DonViTinh (Line 450 in the previous view, let's search)
start_idx = -1
for i, line in enumerate(lines):
    if line.startswith('-- 3.1 Bảng Đơn vị tính'):
        start_idx = i
        break

end_idx = -1
for i, line in enumerate(lines):
    if line.startswith('-- 3.19 Bảng Cấu Hình Thuế'):
        end_idx = i
        break

if start_idx != -1 and end_idx != -1:
    block = lines[start_idx:end_idx]
    
    # Find where to insert it: before '-- 3.7 Bảng Loại Phòng'
    insert_idx = -1
    for i, line in enumerate(lines):
        if line.startswith('-- 3.7 Bảng Loại Phòng'):
            insert_idx = i
            break
            
    if insert_idx != -1:
        # Delete the block from original position
        # Note: insert_idx is before start_idx
        new_lines = lines[:insert_idx] + block + lines[insert_idx:start_idx] + lines[end_idx:]
        
        new_content = ''.join(new_lines)
        
        # Replace DatPhongChiTiet with ChiTietDatPhong
        new_content = new_content.replace('DatPhongChiTiet', 'ChiTietDatPhong')
        
        with open('database/Database_DaiNam_V2.sql', 'w', encoding='utf-8') as f:
            f.write(new_content)
        print('SUCCESS: Moved block and replaced text.')
    else:
        print('ERROR: Could not find insert_idx')
else:
    print(f'ERROR: start_idx={start_idx}, end_idx={end_idx}')
