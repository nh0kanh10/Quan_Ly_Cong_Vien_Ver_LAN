import csv
import os

BASE = r"c:\Users\ADMIN\Desktop\Quan_Ly_Cong_Vien_Ver_1.2\Docs\04_SPRINT_03_Winform_CorePOS"

# === STEP 1: Load ALL test cases into dict by TC ID ===
tc_files = [
    os.path.join(BASE, "POS_TestCases_BOM.csv"),
    os.path.join(BASE, "KiemSoatVe_TestCases.csv"),
    os.path.join(BASE, "DoanKhach_TestCases.csv"),
    os.path.join(BASE, "ThueDo_TestCases.csv"),
]

tc_map = {}
for fpath in tc_files:
    if not os.path.exists(fpath):
        print(f"[WARN] Not found: {fpath}")
        continue
    with open(fpath, "r", encoding="utf-8-sig") as f:
        for row in csv.DictReader(f):
            tc_id = row.get("TC ID", "").strip()
            if tc_id:
                tc_map[tc_id] = {
                    "title": row.get("Title", "").strip(),
                    "steps": row.get("Procedure / Steps", "").strip(),
                    "expected": row.get("Expected Results", "").strip(),
                }

print(f"[INFO] Loaded {len(tc_map)} test cases")

# === STEP 2: Defects - CHI GHI tc_id + actual + metadata, con lai lay tu test case ===
SCREEN = {
    "POS": "Man hinh Ban hang",
    "GATE": "Man hinh Kiem soat ve",
    "BK": "Man hinh Xuat ve doan",
    "RT": "Man hinh Thue do",
}

defects = [
    # POS
    {"id": "DEF_POS_001", "tc_id": "POS_020", "screen": SCREEN["POS"],
     "actual": "He thong van hien popup xac nhan voi Tong don = 0 VND. Neu nhan Co, tao don hang 0d trong co so du lieu, lang phi ma don.",
     "priority": "High", "severity": "Major", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_POS_002", "tc_id": "POS_065", "screen": SCREEN["POS"],
     "actual": "He thong bi vang do co gang truy xuat thong tin khach hang khi chua quet the.",
     "priority": "Critical", "severity": "Critical", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_POS_003", "tc_id": "POS_054", "screen": SCREEN["POS"],
     "actual": "Giam ap cho ca F&B lan Ve: (200K + 22.5K) x 10% = 22.250d. Tong = 200.250d. Thieu 2K doanh thu F&B.",
     "priority": "Medium", "severity": "Major", "date": "2026-04-10", "build": "v3.0.0", "status": "Fixed"},

    {"id": "DEF_POS_004", "tc_id": "POS_100", "screen": SCREEN["POS"],
     "actual": "Ton kho Com phan van = 100. Combo khong tru ton kho cho tung thanh phan ben trong.",
     "priority": "High", "severity": "Major", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_POS_005", "tc_id": "POS_014", "screen": SCREEN["POS"],
     "actual": "SP hien 0d trong danh sach, them gio binh thuong. Thanh toan tong 0d, mat hang khong thu tien.",
     "priority": "Medium", "severity": "Major", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_POS_006", "tc_id": "POS_066", "screen": SCREEN["POS"],
     "actual": "Tao 2 don hang giong nhau trong co so du lieu. Vi bi tru 2 lan. Khach mat tien gap doi.",
     "priority": "Critical", "severity": "Critical", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_POS_007", "tc_id": "POS_012", "screen": SCREEN["POS"],
     "actual": "Danh sach san pham trong. Chi tim chinh xac co dau. Nhan vien phai go dung dau moi ra ket qua.",
     "priority": "Low", "severity": "Minor", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_POS_008", "tc_id": "POS_075", "screen": SCREEN["POS"],
     "actual": "Trong mot so truong hop ngoai le khi dung diem, tien giam tinh lech lam so diem tich luy cao hon thuc te.",
     "priority": "Low", "severity": "Minor", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_POS_009", "tc_id": "POS_033", "screen": SCREEN["POS"],
     "actual": "Cot DVT hien ma so (VD: 3) thay vi ten Thung. Nhan vien khong biet dang ban theo don vi nao.",
     "priority": "Medium", "severity": "Minor", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_POS_010", "tc_id": "POS_096", "screen": SCREEN["POS"],
     "actual": "Con tro chuot bi dung sai vi tri o nut thanh toan. Nhan vien phai click tay vao o quet ma, lam cham quy trinh.",
     "priority": "Low", "severity": "Minor", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    # GATE
    {"id": "DEF_GATE_001", "tc_id": "GATE_061", "screen": SCREEN["GATE"],
     "actual": "Bo dem van hien Hop le: 3 / Tu choi: 2 tu khu cu. Gay nham lan thong ke.",
     "priority": "Medium", "severity": "Minor", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_GATE_002", "tc_id": "GATE_009", "screen": SCREEN["GATE"],
     "actual": "The thong tin hien Luot con lai: 1 (hien thi so truoc khi tru). Hien thi lech so voi thuc te.",
     "priority": "Medium", "severity": "Major", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_GATE_003", "tc_id": "GATE_048", "screen": SCREEN["GATE"],
     "actual": "Lan 2 van Hop le, tru them 1 luot, con 1. Khong co khoang cho. Khach bi tru oan.",
     "priority": "High", "severity": "Major", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_GATE_004", "tc_id": "GATE_024", "screen": SCREEN["GATE"],
     "actual": "Trong mot so truong hop bo dem thoi gian khong kich hoat neu bi dung roi khoi dong lai qua nhanh. Panel ket trang thai cu cho den khi quet ve tiep.",
     "priority": "Low", "severity": "Minor", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_GATE_005", "tc_id": "GATE_028", "screen": SCREEN["GATE"],
     "actual": "Danh sach lich su chua tat ca 2000 dong. Bo nho tang dan. Cuon trang cham. Khong tu xoa dong cu.",
     "priority": "Medium", "severity": "Minor", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_GATE_006", "tc_id": "GATE_005", "screen": SCREEN["GATE"],
     "actual": "Click vung khac, o nhap ma mat con tro chuot. Go ban phim khong nhap vao o. Phai click lai vao o, gian doan nhan vien quet.",
     "priority": "Medium", "severity": "Minor", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_GATE_007", "tc_id": "GATE_017", "screen": SCREEN["GATE"],
     "actual": "Hien Khong tim thay, vi he thong chua xu ly dung ma bat dau bang BK- trong mot so truong hop.",
     "priority": "High", "severity": "Major", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_GATE_008", "tc_id": "GATE_025", "screen": SCREEN["GATE"],
     "actual": "Vien khong chop lan dau (bo dem nhay den chua duoc khoi tao). Tu lan quet thu 2 tro di moi chop.",
     "priority": "Low", "severity": "Minor", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_GATE_009", "tc_id": "GATE_031", "screen": SCREEN["GATE"],
     "actual": "Danh sach tro choi giu nguyen item cu (Tau Luon) + them item moi (Nha Ma). Danh sach bi trung / nham khu.",
     "priority": "Medium", "severity": "Major", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_GATE_010", "tc_id": "GATE_036", "screen": SCREEN["GATE"],
     "actual": "Chuoi gui nguyen vao he thong, tim khong ra, hien Khong tim thay. Mat ca 2 ve.",
     "priority": "Low", "severity": "Minor", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    # BOOKING
    {"id": "DEF_BK_001", "tc_id": "BK_006", "screen": SCREEN["BK"],
     "actual": "Khong tim thay. He thong chi tim theo ma booking. SDT khong duoc tim kiem.",
     "priority": "Medium", "severity": "Major", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_BK_002", "tc_id": "BK_035", "screen": SCREEN["BK"],
     "actual": "Popup ket qua hien dong Chiet khau bi trong khi gia tri = 0. Loi hien thi.",
     "priority": "Low", "severity": "Minor", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_BK_003", "tc_id": "BK_073", "screen": SCREEN["BK"],
     "actual": "Nhan xanh bao Booking hop le. Nut Xuat Ve van nhan duoc. He thong so sanh ngay bi sai lech.",
     "priority": "High", "severity": "Major", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_BK_004", "tc_id": "BK_003", "screen": SCREEN["BK"],
     "actual": "Nut Xuat Ve nhan duoc binh thuong. Khi nhan, ung dung bi vang do khong co du lieu doan.",
     "priority": "Medium", "severity": "Major", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_BK_005", "tc_id": "BK_069", "screen": SCREEN["BK"],
     "actual": "Khong tim thay. He thong so sanh nguyen chuoi chua xoa khoang trang. Phai go chinh xac khong co dau cach.",
     "priority": "Low", "severity": "Minor", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_BK_006", "tc_id": "BK_031", "screen": SCREEN["BK"],
     "actual": "O tim van hien BK-001. Con tro chuot o nut Xuat Ve. Nhan vien phai xoa tay va click lai o tim.",
     "priority": "Medium", "severity": "Minor", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_BK_007", "tc_id": "BK_088", "screen": SCREEN["BK"],
     "actual": "Panel van hien thong tin doan cu. Nut Xuat Ve van nhan duoc, nhan lai ung dung bi vang vi du lieu doan da bi xoa sau khi reset.",
     "priority": "Medium", "severity": "Major", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_BK_008", "tc_id": "BK_042", "screen": SCREEN["BK"],
     "actual": "Nghe tieng 'ting' (am thanh Windows mac dinh). Chua chan phim Enter truoc khi xu ly.",
     "priority": "Low", "severity": "Cosmetic", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_BK_009", "tc_id": "BK_056", "screen": SCREEN["BK"],
     "actual": "Truong Combo bi trong hoan toan. Nhan vien khong biet chua chon hay loi hien thi.",
     "priority": "Low", "severity": "Minor", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_BK_010", "tc_id": "BK_071", "screen": SCREEN["BK"],
     "actual": "Nhan trang thai trong. Nut Xuat Ve van nhan duoc vi khong co xu ly cho truong hop trang thai la. Nhan thi xac nhan duoc doan loi.",
     "priority": "Medium", "severity": "Major", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    # RENTAL
    {"id": "DEF_RT_001", "tc_id": "RT_037", "screen": SCREEN["RT"],
     "actual": "Gio van giu Phao boi (tram A) nhung danh sach SP hien tram B. Khi thanh toan luu sai thong tin tram.",
     "priority": "Medium", "severity": "Major", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_RT_002", "tc_id": "RT_053", "screen": SCREEN["RT"],
     "actual": "Phuong thuc thanh toan ghi thanh RFID (he thong tu gan khi khach co the). Vi bi tru du khach khong muon.",
     "priority": "High", "severity": "Major", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_RT_003", "tc_id": "RT_023", "screen": SCREEN["RT"],
     "actual": "Nhan F12 khong co phan hoi. Cot Khach tra van = 0. Tinh nang phim tat F12 chua duoc ket noi.",
     "priority": "Medium", "severity": "Major", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_RT_004", "tc_id": "RT_019", "screen": SCREEN["RT"],
     "actual": "Loi khi doc du lieu abc, popup bi vang. Hop nhap lieu khong kiem tra du lieu dau vao.",
     "priority": "Medium", "severity": "Major", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_RT_005", "tc_id": "RT_055", "screen": SCREEN["RT"],
     "actual": "Hoan 500K (coc + thue). He thong hoan ca tien thue. Lo doanh thu.",
     "priority": "Critical", "severity": "Critical", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_RT_006", "tc_id": "RT_042", "screen": SCREEN["RT"],
     "actual": "Ma bien lai trung nhau. Co so du lieu bao loi luu trung lap.",
     "priority": "Low", "severity": "Minor", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_RT_007", "tc_id": "RT_066", "screen": SCREEN["RT"],
     "actual": "Tab chuyen nhung bang trong. Thong tin the RFID / bien lai khong tu gan. Nhan vien phai quet lai RFID thu cong.",
     "priority": "Medium", "severity": "Minor", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_RT_008", "tc_id": "RT_078", "screen": SCREEN["RT"],
     "actual": "Gia hien 50.000d (gia ngay thuong). Khong lay gia theo ngay, hien thang gia goc mac dinh.",
     "priority": "High", "severity": "Major", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_RT_009", "tc_id": "RT_022", "screen": SCREEN["RT"],
     "actual": "Hoan 400K, so dung nhung phep tinh chia tong coc / tong so luong. Neu phien co nhieu san pham khac gia coc thi chia sai, gay lech vai ngan.",
     "priority": "Low", "severity": "Minor", "date": "2026-04-11", "build": "v3.1.0", "status": "Open"},

    {"id": "DEF_RT_010", "tc_id": "RT_004", "screen": SCREEN["RT"],
     "actual": "SP duoc them vao gio binh thuong du chua chon tram. Khi thanh toan, co so du lieu ghi sai toan bo thong tin tram.",
     "priority": "Medium", "severity": "Major", "date": "2026-04-10", "build": "v3.1.0", "status": "Open"},
]

# === STEP 3: Generate CSV — Title, Steps, Expected lay 100% tu test case goc ===
output_path = os.path.join(BASE, "Sprint3_DefectList_QA.csv")
header = [
    "Defect ID", "Title", "Screen", "Pre-condition", "Steps", "Test Data",
    "Expected Results", "Actual Results", "Priority", "Severity",
    "Date", "Owner", "TC ID", "Build", "System", "Assign To", "Status"
]

rows = []
missing = []
for d in defects:
    tc = tc_map.get(d["tc_id"])
    if not tc:
        missing.append(d["tc_id"])
        title = f"[TC {d['tc_id']} NOT FOUND]"
        steps = ""
        expected = ""
    else:
        title = tc["title"]
        steps = tc["steps"]
        expected = tc["expected"]

    rows.append([
        d["id"],          # Defect ID
        title,            # Title <- TEST CASE
        d["screen"],      # Screen
        "",               # Pre-condition (de trong, Steps da co du)
        steps,            # Steps <- TEST CASE
        "",               # Test Data (de trong, Steps da co du)
        expected,         # Expected <- TEST CASE
        d["actual"],      # Actual <- DEFECT (duy nhat tu defect)
        d["priority"],
        d["severity"],
        d["date"],
        "QA_NhiNT",
        d["tc_id"],
        d["build"],
        "WinForms C#",
        "DEV_NhiNT",
        d["status"],
    ])

with open(output_path, "w", encoding="utf-8-sig", newline="") as f:
    writer = csv.writer(f)
    writer.writerow(header)
    writer.writerows(rows)

if missing:
    print(f"[WARN] Missing TCs: {missing}")
print(f"[OK] {len(rows)} defects -> {output_path}")
