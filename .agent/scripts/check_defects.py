import csv

path = r"c:\Users\ADMIN\Desktop\Quan_Ly_Cong_Vien_Ver_1.2\Docs\04_SPRINT_03_Winform_CorePOS\Sprint3_DefectList_QA.csv"
with open(path, encoding="utf-8-sig") as f:
    r = csv.DictReader(f)
    rows = list(r)

print(f"Total: {len(rows)} defects\n")

for d in rows[:3]:
    print(f"=== {d['Defect ID']} (TC: {d['TC ID']}) ===")
    print(f"  Title: {d['Title']}")
    print(f"  Steps (first 150): {d['Steps'][:150]}...")
    print(f"  Expected (first 150): {d['Expected Results'][:150]}...")
    print(f"  Actual: {d['Actual Results']}")
    print()
