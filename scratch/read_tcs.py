import csv
import os

files = ['TC_PhanQuyen.csv', 'TC_Combo.csv', 'TC_SanPham.csv']
base_path = r'c:\Users\ADMIN\Desktop\DaiNamNew\docs\TestCase'

for filename in files:
    file_path = os.path.join(base_path, filename)
    print(f"\n--- {filename} ---")
    try:
        with open(file_path, 'r', encoding='utf-8-sig') as f:
            reader = csv.DictReader(f)
            count = 0
            for row in reader:
                count += 1
                tc_id = row.get('TC ID', row.get('TC_ID', ''))
                title = row.get('Title', '')
                if tc_id:
                    # Print all of them to a log file instead to avoid massive stdout
                    pass
            print(f"Total rows: {count}")
    except Exception as e:
        print(f'Error reading {filename}: {e}')
