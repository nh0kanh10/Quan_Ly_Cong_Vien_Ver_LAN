import re
import os

# The issue: ResXResourceWriter generates a schema header with example non-string resources
# (Bitmap1, Icon1) inside comments. MSBuild parses them and requires System.Resources.Extensions.
# Solution: Remove the problematic example entries from the comment block.

files = [
    r'c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Properties\UIStrings.resx',
    r'c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Properties\UIStrings.en-US.resx',
    r'c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Properties\UIStrings.zh-CN.resx'
]

for fpath in files:
    with open(fpath, 'r', encoding='utf-8') as f:
        content = f.read()
    
    # Remove the entire comment block that contains the examples
    # The pattern is:  <!-- ... Microsoft ResX Schema ... -->
    content = re.sub(
        r'\s*<!-- \s*\n\s*Microsoft ResX Schema.*?-->\s*\n',
        '\n',
        content,
        flags=re.DOTALL
    )
    
    with open(fpath, 'w', encoding='utf-8') as f:
        f.write(content)
    print(f'Cleaned: {os.path.basename(fpath)}')

print('Done!')
