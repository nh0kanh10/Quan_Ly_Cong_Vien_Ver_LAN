import re

with open(r'c:\Users\ADMIN\Desktop\DaiNamNew\.agent\scripts\missing_translations.txt', 'r', encoding='utf-8') as f:
    lines = f.readlines()

kho_related = []
for line in lines:
    line = line.strip()
    if not line: continue
    key, vi = line.split('|', 1)
    # Check if key or vi contains KHO, TON, PHIEU, CTK, NHAP, XUAT
    lower_key = key.lower()
    lower_vi = vi.lower()
    keywords = ['kho', 'ton', 'tồn', 'phiếu', 'phieu', 'ctk', 'nhập', 'xuất', 'nhap', 'xuat', 'vật tư', 'nguyên liệu', 'dvt', 'trạng thái']
    if any(kw in lower_key or kw in lower_vi for kw in keywords):
        kho_related.append((key, vi))

with open(r'c:\Users\ADMIN\Desktop\DaiNamNew\.agent\scripts\kho_translations.txt', 'w', encoding='utf-8') as f:
    for key, vi in kho_related:
        f.write(f'{key}|{vi}\n')

print(f'Found {len(kho_related)} warehouse related items.')
