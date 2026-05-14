import os, re, glob

GUI_ROOT = r'c:\Users\ADMIN\Desktop\DaiNamNew\GUI'
resx_files = glob.glob(os.path.join(GUI_ROOT, '**', '*.resx'), recursive=True)
cleaned = 0

for fpath in resx_files:
    with open(fpath, 'r', encoding='utf-8') as f:
        content = f.read()
    
    original = content
    
    # Remove the entire comment block containing Bitmap1/Icon1/Color1 examples
    content = re.sub(
        r'\s*<!--\s*\n\s*Microsoft ResX Schema.*?-->\s*\n',
        '\n',
        content,
        flags=re.DOTALL
    )
    
    # Remove standalone example data entries NOT inside comments
    # Color1
    content = re.sub(
        r'\s*<data name="Color1" type="System\.Drawing\.Color, System\.Drawing">Blue</data>\s*\n',
        '\n',
        content
    )
    # Bitmap1
    content = re.sub(
        r'\s*<data name="Bitmap1"[^>]*>\s*\n\s*<value>[^<]*</value>\s*\n\s*</data>\s*\n',
        '\n',
        content
    )
    # Icon1
    content = re.sub(
        r'\s*<data name="Icon1"[^>]*>\s*\n\s*<value>[^<]*</value>\s*\n(\s*<comment>[^<]*</comment>\s*\n)?\s*</data>\s*\n',
        '\n',
        content
    )
    
    if content != original:
        with open(fpath, 'w', encoding='utf-8') as f:
            f.write(content)
        cleaned += 1
        rel = os.path.relpath(fpath, GUI_ROOT)
        print(f'Cleaned: {rel}')

print(f'\nTotal cleaned: {cleaned} / {len(resx_files)} files')
