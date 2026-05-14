import re

file_path = r'c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Properties\UpdateResx.cs'
with open(file_path, 'r', encoding='utf-8') as f:
    content = f.read()

pattern = re.compile(r'new\s*\{\s*Key\s*=\s*"([^"]+)",\s*Vi\s*=\s*"([^"]+)",\s*En\s*=\s*"([^"]+)",\s*Zh\s*=\s*"([^"]+)"\s*\}')
matches = pattern.findall(content)

missing = []
for m in matches:
    key, vi, en, zh = m
    if '(EN)' in en or '(ZH)' in zh or en == vi or zh == vi or en == key or zh == key:
        missing.append((key, vi))

with open(r'c:\Users\ADMIN\Desktop\DaiNamNew\.agent\scripts\missing_translations.txt', 'w', encoding='utf-8') as f:
    for key, vi in missing:
        f.write(f'{key}|{vi}\n')

print(f'Found {len(missing)} missing translations.')
