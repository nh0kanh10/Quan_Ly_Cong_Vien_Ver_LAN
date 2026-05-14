import re

file_path = r'c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Properties\UpdateResx.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

pattern = re.compile(r'new\s*\{\s*Key\s*=\s*"([^"]+)",\s*Vi\s*=\s*"([^"]+)",\s*En\s*=\s*"([^"]+)",\s*Zh\s*=\s*"([^"]+)"\s*\}')
matches = pattern.findall(content)

vietnamese_chars = set("ăâđêôơưáàảãạấầẩẫậắằẳẵặéèẻẽẹếềểễệíìỉĩịóòỏõọốồổỗộớờởỡợúùủũụứừửữựýỳỷỹỵ")

untranslated = []
for key, vi, en, zh in matches:
    # If it is missing a proper translation
    if en == vi or zh == vi or en == key or zh == key or '(EN)' in en or '(ZH)' in zh:
        untranslated.append((key, vi))

with open(r'c:\Users\ADMIN\Desktop\DaiNamNew\.agent\scripts\still_untranslated.txt', 'w', encoding='utf-8') as f:
    for key, vi in untranslated:
        f.write(f"{key}|{vi}\n")
print(f"Found {len(untranslated)} still untranslated.")
