import re

file_path = r'c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Properties\UpdateResx.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

pattern = re.compile(r'new\s*\{\s*Key\s*=\s*"([^"]+)",\s*Vi\s*=\s*"([^"]+)",\s*En\s*=\s*"([^"]+)",\s*Zh\s*=\s*"([^"]+)"\s*\}')
matches = pattern.findall(content)

trash = []
for key, vi, en, zh in matches:
    if vi.startswith("Lỗi:") and sum(c.isupper() for c in vi) < 3 and "Lỗi nạp màn hình" not in vi:
        # likely trash from previous script
        trash.append(key)

with open(r'c:\Users\ADMIN\Desktop\DaiNamNew\.agent\scripts\trash_errors.txt', 'w', encoding='utf-8') as f:
    for key in trash:
        f.write(f"{key}\n")

print(f"Found {len(trash)} trash strings.")
