import re

with open(r'c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Properties\UpdateResx.cs', 'r', encoding='utf-8') as f:
    content = f.read()

pattern = re.compile(r'new\s*\{\s*Key\s*=\s*"([^"]+)",\s*Vi\s*=\s*"([^"]+)",\s*En\s*=\s*"([^"]+)",\s*Zh\s*=\s*"([^"]+)"\s*\}')
matches = pattern.findall(content)

vietnamese_chars = set("ăâđêôơưáàảãạấầẩẫậắằẳẵặéèẻẽẹếềểễệíìỉĩịóòỏõọốồổỗộớờởỡợúùủũụứừửữựýỳỷỹỵ")

def looks_like_untranslated(vi, en, zh):
    if en == vi: return True
    if zh == vi: return True
    if '(EN)' in en or '(ZH)' in zh: return True
    if '(Translate)' in en: return True
    if 'phiếu' in en.lower() or 'dòng' in en.lower(): return True
    # Check if En contains specific Vietnamese characters
    if any(c in en.lower() for c in vietnamese_chars): return True
    return False

needs_translation = []
for m in matches:
    key, vi, en, zh = m
    if looks_like_untranslated(vi, en, zh):
        needs_translation.append((key, vi))

with open(r'c:\Users\ADMIN\Desktop\DaiNamNew\.agent\scripts\to_translate.txt', 'w', encoding='utf-8') as f:
    for key, vi in needs_translation:
        f.write(f'{key}|{vi}\n')

print(f"Found {len(needs_translation)} items needing translation.")
