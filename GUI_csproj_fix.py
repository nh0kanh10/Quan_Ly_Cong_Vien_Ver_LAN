import re

with open('GUI/GUI.csproj', 'r', encoding='utf-8') as f:
    content = f.read()

old = '    <Compile Include="Modules\\BanHang\\ucPOS.cs">\n      <SubType>UserControl</SubType>\n    </Compile>'
new = (
    '    <Compile Include="Modules\\BanHang\\ucPOS.cs">\n      <SubType>UserControl</SubType>\n    </Compile>\n'
    '    <Compile Include="Modules\\BanHang\\ucPOS.Designer.cs">\n'
    '      <DependentUpon>ucPOS.cs</DependentUpon>\n'
    '    </Compile>'
)

if 'ucPOS.Designer.cs' not in content:
    if old in content:
        content = content.replace(old, new)
        with open('GUI/GUI.csproj', 'w', encoding='utf-8') as f:
            f.write(content)
        print('[OK] Added ucPOS.Designer.cs to GUI.csproj')
    else:
        # try another pattern
        old2 = '    <Compile Include="Modules\\BanHang\\ucPOS.cs" />'
        new2 = (
            '    <Compile Include="Modules\\BanHang\\ucPOS.cs">\n      <SubType>UserControl</SubType>\n    </Compile>\n'
            '    <Compile Include="Modules\\BanHang\\ucPOS.Designer.cs">\n'
            '      <DependentUpon>ucPOS.cs</DependentUpon>\n'
            '    </Compile>'
        )
        if old2 in content:
            content = content.replace(old2, new2)
            with open('GUI/GUI.csproj', 'w', encoding='utf-8') as f:
                f.write(content)
            print('[OK] Added ucPOS.Designer.cs to GUI.csproj (pattern 2)')
        else:
            print('[FAIL] Could not find anchor for ucPOS.cs')
else:
    print('[SKIP] ucPOS.Designer.cs already in GUI.csproj')
