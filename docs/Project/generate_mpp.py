import xml.etree.ElementTree as ET
from xml.dom import minidom
from datetime import datetime, timedelta

# === PROJECT CONFIG ===
PROJECT_START = datetime(2026, 3, 4, 8, 0, 0)
HOURS_PER_DAY = 8

# Build working dates map (skipping weekends)
working_dates = []
curr = PROJECT_START.replace(hour=0, minute=0, second=0)
end_date = datetime(2026, 5, 20)
while curr <= end_date:
    if curr.weekday() < 5:
        working_dates.append(curr)
    curr += timedelta(days=1)

def get_date_at_idx(idx, is_end=False):
    if idx >= len(working_dates): idx = len(working_dates) - 1
    d = working_dates[idx]
    return d.replace(hour=17) if is_end else d.replace(hour=8)

# === RESOURCES ===
resources = [
    {"uid": 1, "name": "Bùi Trí Nguyên", "initials": "BTN", "role": "Team Lead / Full-stack"},
    {"uid": 2, "name": "Bùi Thị Yến Nhi", "initials": "BTYN", "role": "Dev / BA"},
    {"uid": 3, "name": "Đỗ Duy Tấn", "initials": "DDT", "role": "Dev / Tester"},
]

# === SPRINT & TASK DATA ===
sprints = [
    {
        "num": 1, "name": "Sprint 1 — Nền tảng: Thiết kế, Phân quyền, Danh mục",
        "start_idx": 0, "end_idx": 9,
        "tasks": [
            {"id": "TASK-00", "name": "[Hạ tầng] frmMain Shell, Đăng nhập", "days": 3, "res": 1, "preds": [], "sp": 5, "pri": "High"},
            {"id": "US-01", "name": "Phân tích yêu cầu & Thiết kế UI/UX", "days": 3, "res": 2, "preds": [], "sp": 5, "pri": "High"},
            {"id": "US-02", "name": "Phân quyền theo vai trò (QL/KT/TN)", "days": 2, "res": 3, "preds": [], "sp": 3, "pri": "High"},
            {"id": "US-03", "name": "Quản lý Sản phẩm, Vé, BOM", "days": 4, "res": 1, "preds": [("TASK-00", "FS", 0)], "sp": 8, "pri": "High"},
            {"id": "US-04", "name": "Quản lý Combo sản phẩm", "days": 4, "res": 3, "preds": [("US-02", "FS", 0)], "sp": 5, "pri": "Medium"},
            {"id": "US-05", "name": "Chuẩn bị Mock Data & Test Database", "days": 2, "res": 2, "preds": [("US-01", "FS", 0)], "sp": 3, "pri": "Medium"},
            {"id": "TEST-S1-1", "name": "Viết Test Cases (Sprint 1)", "days": 2, "res": 2, "preds": [("US-05", "FS", 0)], "sp": 3, "pri": "Medium"},
            {"id": "TEST-S1-2", "name": "Kiểm thử & Sửa lỗi", "days": 2, "res": 3, "preds": [("US-03", "FS", 0), ("US-04", "FS", 0)], "sp": 5, "pri": "High"},
            {"id": "DOC-S1-1", "name": "Hoàn thiện SRS & Hướng dẫn SD", "days": 1, "res": 2, "preds": [("TEST-S1-2", "FS", 0)], "sp": 2, "pri": "Low"},
            {"id": "DEP-S1-1", "name": "Triển khai Server-Client", "days": 1, "res": 1, "preds": [("TEST-S1-2", "FS", 0)], "sp": 2, "pri": "High"},
        ]
    },
    {
        "num": 2, "name": "Sprint 2 — Khách hàng, Dashboard, Thuê đồ, Kho",
        "start_idx": 10, "end_idx": 19,
        "tasks": [
            {"id": "US-06", "name": "Quản lý Khách hàng & Lịch sử GD", "days": 3, "res": 1, "preds": [("DEP-S1-1", "FS", 0)], "sp": 5, "pri": "High"},
            {"id": "US-07", "name": "Dashboard tổng quan", "days": 3, "res": 3, "preds": [("DEP-S1-1", "FS", 0)], "sp": 5, "pri": "Medium"},
            {"id": "US-08", "name": "Viết tài liệu User Manual (Tính năng S1)", "days": 3, "res": 2, "preds": [("DEP-S1-1", "FS", 0)], "sp": 3, "pri": "Low"},
            {"id": "US-09", "name": "Thuê đồ theo block giờ", "days": 4, "res": 1, "preds": [("US-06", "FS", 0)], "sp": 8, "pri": "High"},
            {"id": "US-10", "name": "Nhập kho nguyên liệu theo lô", "days": 4, "res": 3, "preds": [("US-07", "FS", 0)], "sp": 8, "pri": "High"},
            {"id": "US-11", "name": "Review UI/UX & Chuẩn hóa giao diện", "days": 2, "res": 2, "preds": [("US-08", "FS", 0)], "sp": 3, "pri": "Medium"},
            {"id": "US-12", "name": "Auto xuất kho theo BOM", "days": 1, "res": 1, "preds": [("US-09", "FS", 0), ("US-10", "FS", 0)], "sp": 2, "pri": "High"},
            {"id": "TEST-S2-1", "name": "Viết Test Cases (Sprint 2)", "days": 2, "res": 2, "preds": [("US-11", "FS", 0)], "sp": 3, "pri": "Medium"},
            {"id": "TEST-S2-2", "name": "Kiểm thử & Sửa lỗi", "days": 1, "res": 3, "preds": [("US-12", "FS", 0)], "sp": 3, "pri": "High"},
            {"id": "DOC-S2-1", "name": "Cập nhật SRS & Nghiệp vụ kho", "days": 1, "res": 2, "preds": [("TEST-S2-2", "FS", 0)], "sp": 2, "pri": "Low"},
            {"id": "DEP-S2-1", "name": "Triển khai & Demo", "days": 1, "res": 1, "preds": [("TEST-S2-2", "FS", 0)], "sp": 2, "pri": "High"},
        ]
    },
    {
        "num": 3, "name": "Sprint 3 — POS Bán hàng, Khuyến mãi, Nhà hàng",
        "start_idx": 20, "end_idx": 29,
        "tasks": [
            {"id": "US-13", "name": "POS: Tạo đơn, thanh toán, in hóa đơn", "days": 4, "res": 1, "preds": [("DEP-S2-1", "FS", 0)], "sp": 8, "pri": "Critical"},
            {"id": "US-14", "name": "Khuyến mãi & Voucher trong POS", "days": 3, "res": 3, "preds": [("DEP-S2-1", "FS", 0)], "sp": 5, "pri": "High"},
            {"id": "US-15", "name": "Soạn Data Khuyến mãi mẫu & In Test", "days": 2, "res": 2, "preds": [("DEP-S2-1", "FS", 0)], "sp": 2, "pri": "Medium"},
            {"id": "US-16", "name": "Tra cứu hóa đơn & Hoàn hàng", "days": 2, "res": 1, "preds": [("US-13", "FS", 0)], "sp": 3, "pri": "High"},
            {"id": "US-17", "name": "Nhà hàng: Quản lý bàn & Gọi món", "days": 4, "res": 3, "preds": [("US-14", "FS", 0)], "sp": 8, "pri": "Critical"},
            {"id": "US-18", "name": "Thiết kế luồng Gọi món (BPMN) & UI", "days": 2, "res": 2, "preds": [("US-15", "FS", 0)], "sp": 3, "pri": "Medium"},
            {"id": "TEST-S3-1", "name": "Viết Test Cases (Sprint 3)", "days": 2, "res": 2, "preds": [("US-18", "FS", 0)], "sp": 3, "pri": "Medium"},
            {"id": "TEST-S3-2", "name": "Kiểm thử & Sửa lỗi", "days": 2, "res": 3, "preds": [("US-16", "FS", 0), ("US-17", "FS", 0)], "sp": 5, "pri": "High"},
            {"id": "DOC-S3-1", "name": "Hoàn thiện SRS & Hướng dẫn SD", "days": 1, "res": 2, "preds": [("TEST-S3-2", "FS", 0)], "sp": 2, "pri": "Low"},
            {"id": "DEP-S3-1", "name": "Triển khai & Demo", "days": 1, "res": 1, "preds": [("TEST-S3-2", "FS", 0)], "sp": 2, "pri": "High"},
        ]
    },
    {
        "num": 4, "name": "Sprint 4 — Khách sạn, i18n, AI, Báo cáo, REST API",
        "start_idx": 30, "end_idx": 39,
        "tasks": [
            {"id": "TASK-01", "name": "[Kỹ thuật] REST API (ASP.NET Core)", "days": 4, "res": 1, "preds": [("DEP-S3-1", "FS", 0)], "sp": 8, "pri": "Critical"},
            {"id": "US-19", "name": "Khách sạn: Đặt phòng, Check-in/out", "days": 4, "res": 3, "preds": [("DEP-S3-1", "FS", 0)], "sp": 8, "pri": "Critical"},
            {"id": "US-20", "name": "Dịch i18n Anh/Việt (Hệ thống & DB)", "days": 3, "res": 1, "preds": [("TASK-01", "FS", 0)], "sp": 5, "pri": "Medium"},
            {"id": "US-21", "name": "Báo cáo doanh thu & Xuất file", "days": 3, "res": 3, "preds": [("US-19", "FS", 0)], "sp": 5, "pri": "High"},
            {"id": "US-22", "name": "AI Assistant (NLP tra cứu)", "days": 2, "res": 1, "preds": [("US-20", "FS", 0)], "sp": 5, "pri": "Medium"},
            {"id": "US-23", "name": "Chuẩn bị Kịch bản AI & Dữ liệu khách sạn", "days": 3, "res": 2, "preds": [("DEP-S3-1", "FS", 0)], "sp": 3, "pri": "Medium"},
            {"id": "US-24", "name": "Báo cáo tiến độ đồ án (Bản nháp 1)", "days": 2, "res": 2, "preds": [("US-23", "FS", 0)], "sp": 3, "pri": "High"},
            {"id": "TEST-S4-1", "name": "Viết Test Cases (Sprint 4)", "days": 2, "res": 2, "preds": [("US-24", "FS", 0)], "sp": 3, "pri": "Medium"},
            {"id": "TEST-S4-2", "name": "Kiểm thử & Sửa lỗi", "days": 2, "res": 3, "preds": [("US-21", "FS", 0), ("US-22", "FS", 0)], "sp": 5, "pri": "High"},
            {"id": "DOC-S4-1", "name": "Hoàn thiện SRS & HDSD", "days": 1, "res": 2, "preds": [("TEST-S4-2", "FS", 0)], "sp": 2, "pri": "Low"},
            {"id": "DEP-S4-1", "name": "Triển khai Server-Client", "days": 1, "res": 1, "preds": [("TEST-S4-2", "FS", 0)], "sp": 2, "pri": "High"},
        ]
    },
    {
        "num": 5, "name": "Sprint 5 — Web Portal + Android Staff App",
        "start_idx": 40, "end_idx": 49,
        "tasks": [
            {"id": "US-25", "name": "WEB: Đặt vé online + QR Code", "days": 3, "res": 1, "preds": [("DEP-S4-1", "FS", 0)], "sp": 5, "pri": "High"},
            {"id": "US-26", "name": "WEB: Đặt món ăn trước", "days": 3, "res": 3, "preds": [("DEP-S4-1", "FS", 0)], "sp": 5, "pri": "High"},
            {"id": "US-27", "name": "Soạn ND Website & App Mockup", "days": 3, "res": 2, "preds": [("DEP-S4-1", "FS", 0)], "sp": 3, "pri": "Medium"},
            {"id": "US-28", "name": "WEB: Đặt phòng khách sạn", "days": 3, "res": 1, "preds": [("US-25", "FS", 0)], "sp": 5, "pri": "High"},
            {"id": "US-29", "name": "APP: Quẹt vé cổng (Android)", "days": 2, "res": 3, "preds": [("US-26", "FS", 0)], "sp": 3, "pri": "High"},
            {"id": "US-30", "name": "Viết Báo cáo Đồ án bản Final", "days": 2, "res": 2, "preds": [("US-27", "FS", 0)], "sp": 5, "pri": "Critical"},
            {"id": "US-31", "name": "APP: Nhận đơn & Nấu (Android)", "days": 2, "res": 1, "preds": [("US-28", "FS", 0)], "sp": 3, "pri": "Medium"},
            {"id": "US-32", "name": "APP: Xác nhận dọn phòng", "days": 2, "res": 3, "preds": [("US-29", "FS", 0)], "sp": 3, "pri": "Medium"},
            {"id": "TEST-S5-1", "name": "Viết Test Cases (Sprint 5)", "days": 2, "res": 2, "preds": [("US-30", "FS", 0)], "sp": 3, "pri": "Medium"},
            {"id": "TEST-S5-2", "name": "Kiểm thử & Sửa lỗi", "days": 1, "res": 3, "preds": [("US-31", "FS", 0), ("US-32", "FS", 0)], "sp": 3, "pri": "High"},
            {"id": "DOC-S5-1", "name": "Bàn giao & Nghiệm thu", "days": 1, "res": 2, "preds": [("TEST-S5-2", "FS", 0)], "sp": 2, "pri": "Critical"},
            {"id": "DEP-S5-1", "name": "Triển khai Production", "days": 1, "res": 1, "preds": [("TEST-S5-2", "FS", 0)], "sp": 2, "pri": "Critical"},
        ]
    },
]

# Calculate start indices based on dependencies
task_map = {}
for s in sprints:
    for t in s["tasks"]:
        t["start_idx"] = s["start_idx"]
        task_map[t["id"]] = t

for s in sprints:
    for t in s["tasks"]:
        max_idx = s["start_idx"]
        for p_id, p_type, lag in t["preds"]:
            if p_id in task_map:
                pt = task_map[p_id]
                if p_type == "FS":
                    idx = pt["start_idx"] + pt["days"] + lag
                elif p_type == "SS":
                    idx = pt["start_idx"] + lag
                if idx > max_idx:
                    max_idx = idx
        t["start_idx"] = max_idx

def dur(days):
    return f"PT{days * HOURS_PER_DAY}H0M0S"

def ds(d):
    return d.strftime("%Y-%m-%dT%H:%M:%S")

def ae(parent, tag, text=None):
    el = ET.SubElement(parent, tag)
    if text is not None: el.text = str(text)
    return el

def build_xml():
    root = ET.Element("Project")
    root.set("xmlns", "http://schemas.microsoft.com/project")

    ae(root, "Name", "KhuDuLichDaiNam_DN01")
    ae(root, "Title", "Khu Du Lịch Đại Nam — Quản Lý Dự Án")
    ae(root, "Subject", "Hệ thống POS & Quản lý tổng thể")
    ae(root, "Author", "Bùi Trí Nguyên")
    ae(root, "CreationDate", ds(datetime.now()))
    ae(root, "StartDate", ds(PROJECT_START))
    ae(root, "FinishDate", ds(get_date_at_idx(49, True)))
    ae(root, "ScheduleFromStart", "1")
    ae(root, "CalendarUID", "1")
    ae(root, "MinutesPerDay", "480")
    ae(root, "MinutesPerWeek", "2400")
    ae(root, "DaysPerMonth", "20")
    ae(root, "DefaultStartTime", "08:00:00")
    ae(root, "DefaultFinishTime", "17:00:00")
    ae(root, "CurrencySymbol", "₫")
    ae(root, "CurrencyCode", "VND")

    # === CALENDAR (exact copy from working XML) ===
    cals = ae(root, "Calendars")
    cal = ae(cals, "Calendar")
    ae(cal, "UID", "1")
    ae(cal, "Name", "Standard")
    ae(cal, "IsBaseCalendar", "1")
    ae(cal, "BaseCalendarUID", "-1")
    wds = ae(cal, "WeekDays")
    for day_type in range(1, 8):
        wd = ae(wds, "WeekDay")
        ae(wd, "DayType", str(day_type))
        if day_type in (1, 7):  # Sunday, Saturday
            ae(wd, "DayWorking", "0")
        else:
            ae(wd, "DayWorking", "1")
            wts = ae(wd, "WorkingTimes")
            wt1 = ae(wts, "WorkingTime")
            ae(wt1, "FromTime", "08:00:00")
            ae(wt1, "ToTime", "12:00:00")
            wt2 = ae(wts, "WorkingTime")
            ae(wt2, "FromTime", "13:00:00")
            ae(wt2, "ToTime", "17:00:00")

    # === BUILD TASK LIST ===
    task_uid = 0
    task_list = []
    assignment_list = []
    assign_uid = 0
    task_id_to_uid = {}

    # Root summary task
    task_list.append({
        "uid": 0, "name": "Khu Du Lịch Đại Nam — Dự Án CNTT",
        "ol": 0, "summary": True, "milestone": False,
        "start": ds(PROJECT_START),
        "duration": dur(50), "wbs": "0", "on": "0",
    })
    task_uid = 1

    for si, sprint in enumerate(sprints):
        s_start = get_date_at_idx(sprint["start_idx"])
        s_end = get_date_at_idx(sprint["end_idx"], True)

        # Sprint summary task
        task_list.append({
            "uid": task_uid, "name": sprint["name"],
            "ol": 1, "summary": True, "milestone": False,
            "start": ds(s_start), "finish": ds(s_end),
            "duration": dur(10), "wbs": str(si+1), "on": str(si+1),
        })
        task_uid += 1

        for ti, task in enumerate(sprint["tasks"]):
            t_uid = task_uid
            task_id_to_uid[task["id"]] = t_uid
            t_start = get_date_at_idx(task["start_idx"])

            notes = f"Story Points: {task.get('sp',0)} | Priority: {task.get('pri','Normal')}"

            task_list.append({
                "uid": t_uid, "name": f'{task["id"]}: {task["name"]}',
                "ol": 2, "summary": False, "milestone": False,
                "start": ds(t_start),
                "duration": dur(task["days"]),
                "wbs": f"{si+1}.{ti+1}", "on": f"{si+1}.{ti+1}",
                "notes": notes,
                "task_id": task["id"], "preds": task["preds"],
            })

            assignment_list.append({
                "uid": assign_uid, "task_uid": t_uid,
                "resource_uid": task["res"],
                "work": dur(task["days"]),
                "start": ds(t_start),
            })
            assign_uid += 1
            task_uid += 1

        # Sprint milestone
        task_list.append({
            "uid": task_uid,
            "name": f"✅ Sprint {si+1} Complete & Released",
            "ol": 2, "summary": False, "milestone": True,
            "start": ds(s_end), "finish": ds(s_end),
            "duration": "PT0H0M0S",
            "wbs": f"{si+1}.{len(sprint['tasks'])+1}",
            "on": f"{si+1}.{len(sprint['tasks'])+1}",
        })
        task_uid += 1

    # === WRITE TASKS ===
    tasks_el = ae(root, "Tasks")
    type_map = {"FF": 0, "FS": 1, "SF": 2, "SS": 3}

    for t in task_list:
        te = ae(tasks_el, "Task")
        ae(te, "UID", t["uid"])
        ae(te, "ID", t["uid"])
        ae(te, "Name", t["name"])
        ae(te, "Type", "1")
        ae(te, "IsNull", "0")
        ae(te, "CreateDate", ds(datetime.now()))
        ae(te, "WBS", t["wbs"])
        ae(te, "OutlineNumber", t["on"])
        ae(te, "OutlineLevel", t["ol"])
        ae(te, "Start", t["start"])
        if "finish" in t:
            ae(te, "Finish", t["finish"])
        ae(te, "Duration", t["duration"])
        ae(te, "DurationFormat", "7")
        ae(te, "Work", t["duration"])
        ae(te, "Summary", "1" if t["summary"] else "0")
        ae(te, "Milestone", "1" if t["milestone"] else "0")
        ae(te, "PercentComplete", "0")
        ae(te, "CalendarUID", "-1")
        ae(te, "ConstraintType", "0")
        if "notes" in t:
            ae(te, "Notes", t["notes"])

        # Add predecessors
        if "preds" in t:
            for p_id, p_type, lag in t["preds"]:
                if p_id in task_id_to_uid:
                    pl = ae(te, "PredecessorLink")
                    ae(pl, "PredecessorUID", task_id_to_uid[p_id])
                    ae(pl, "Type", type_map[p_type])
                    ae(pl, "LinkLag", lag * 4800)
                    ae(pl, "LagFormat", "7")

    # === WRITE RESOURCES ===
    resources_el = ae(root, "Resources")
    for r in resources:
        re = ae(resources_el, "Resource")
        ae(re, "UID", r["uid"])
        ae(re, "ID", r["uid"])
        ae(re, "Name", r["name"])
        ae(re, "Initials", r["initials"])
        ae(re, "Type", "1")
        ae(re, "MaxUnits", "1.0")
        ae(re, "CalendarUID", "1")
        ae(re, "Notes", r["role"])

    # === WRITE ASSIGNMENTS ===
    assigns_el = ae(root, "Assignments")
    for a in assignment_list:
        asgn = ae(assigns_el, "Assignment")
        ae(asgn, "UID", a["uid"])
        ae(asgn, "TaskUID", a["task_uid"])
        ae(asgn, "ResourceUID", a["resource_uid"])
        ae(asgn, "Units", "1")
        ae(asgn, "Work", a["work"])
        ae(asgn, "Start", a["start"])

    # === OUTPUT ===
    xml_str = '<?xml version="1.0" encoding="UTF-8"?>\n' + ET.tostring(root, encoding="unicode")
    dom = minidom.parseString(xml_str)
    pretty = dom.toprettyxml(indent="  ", encoding="UTF-8")

    with open("DaiNam_Project.xml", "wb") as f:
        f.write(pretty)

    print("✅ File created: DaiNam_Project.xml")
    print("📌 Mở MS Project → File → Open → chọn DaiNam_Project.xml → Save As .mpp")

if __name__ == "__main__":
    build_xml()
