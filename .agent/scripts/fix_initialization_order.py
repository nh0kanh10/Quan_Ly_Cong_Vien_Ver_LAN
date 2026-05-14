import re

def fix_initialization_order():
    filepath = r"c:\Users\ADMIN\Desktop\DaiNamNew\GUI\Modules\BanHang\ucLuuTru_Main.Designer.cs"
    with open(filepath, "r", encoding="utf-8") as f:
        content = f.read()

    # The block of properties is from "// btnActionCheckOut" to the line before "// btnLamMoi"
    # Let's extract it
    block_regex = r'(\s*// btnActionCheckOut\s*//\s*this\.btnActionCheckOut\..*?)(?=\s*// btnLamMoi)'
    match = re.search(block_regex, content, flags=re.DOTALL)
    if not match:
        print("Could not find the properties block!")
        return
    
    properties_block = match.group(1)
    
    # Remove the block from its current position
    content = content.replace(properties_block, "")
    
    # Insert it right before "// layoutControlRight"
    insertion_point = "// layoutControlRight"
    if insertion_point not in content:
        print("Could not find layoutControlRight insertion point!")
        return
        
    content = content.replace(insertion_point, properties_block + "\n            " + insertion_point)

    with open(filepath, "w", encoding="utf-8") as f:
        f.write(content)
        
if __name__ == "__main__":
    fix_initialization_order()
