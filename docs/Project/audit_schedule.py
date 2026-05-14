"""Audit script: trace scheduling logic and resource allocation"""
from datetime import datetime, timedelta

PROJECT_START = datetime(2026, 3, 4, 8, 0, 0)
working_dates = []
curr = PROJECT_START.replace(hour=0, minute=0, second=0)
end_date = datetime(2026, 5, 20)
while curr <= end_date:
    if curr.weekday() < 5:
        working_dates.append(curr)
    curr += timedelta(days=1)

print("=== WORKING DATE INDEX MAP ===")
for i, d in enumerate(working_dates[:55]):
    marker = ""
    if i in [0,10,20,30,40]: marker = " <<<< SPRINT START"
    if i in [9,19,29,39,49]: marker = " <<<< SPRINT END"
    if i == 45: marker = " <<<< TODAY (2026-05-06)"
    print(f"idx={i:2d}  {d.strftime('%Y-%m-%d %A')}{marker}")

# === Now trace the actual task scheduling ===
sprints = [
    {
        "num": 1, "start_idx": 0, "end_idx": 9,
        "tasks": [
            {"id": "TASK-00", "days": 3, "res": 1, "preds": []},
            {"id": "US-01", "days": 2, "res": 3, "preds": []},
            {"id": "US-02", "days": 4, "res": 1, "preds": [("TASK-00", "FS", 0)]},
            {"id": "US-03", "days": 4, "res": 3, "preds": [("US-01", "FS", 0)]},
            {"id": "TEST-S1-1", "days": 2, "res": 2, "preds": [("US-01", "SS", 2)]},
            {"id": "TEST-S1-2", "days": 2, "res": 3, "preds": [("US-02", "FS", 0), ("US-03", "FS", 0)]},
            {"id": "DOC-S1-1", "days": 1, "res": 2, "preds": [("TEST-S1-2", "FS", 0)]},
            {"id": "DEP-S1-1", "days": 1, "res": 1, "preds": [("TEST-S1-2", "FS", 0)]},
        ]
    },
    {
        "num": 2, "start_idx": 10, "end_idx": 19,
        "tasks": [
            {"id": "US-08", "days": 3, "res": 1, "preds": [("DEP-S1-1", "FS", 0)]},
            {"id": "US-09", "days": 3, "res": 3, "preds": [("DEP-S1-1", "FS", 0)]},
            {"id": "US-10", "days": 4, "res": 1, "preds": [("US-08", "FS", 0)]},
            {"id": "US-11", "days": 4, "res": 3, "preds": [("US-09", "FS", 0)]},
            {"id": "US-12", "days": 1, "res": 1, "preds": [("US-10", "FS", 0), ("US-11", "FS", 0)]},
            {"id": "TEST-S2-1", "days": 2, "res": 2, "preds": [("US-08", "SS", 3)]},
            {"id": "TEST-S2-2", "days": 1, "res": 3, "preds": [("US-12", "FS", 0)]},
            {"id": "DOC-S2-1", "days": 1, "res": 2, "preds": [("TEST-S2-2", "FS", 0)]},
            {"id": "DEP-S2-1", "days": 1, "res": 1, "preds": [("TEST-S2-2", "FS", 0)]},
        ]
    },
    {
        "num": 3, "start_idx": 20, "end_idx": 29,
        "tasks": [
            {"id": "US-14", "days": 4, "res": 1, "preds": [("DEP-S2-1", "FS", 0)]},
            {"id": "US-15", "days": 3, "res": 3, "preds": [("DEP-S2-1", "FS", 0)]},
            {"id": "US-16", "days": 2, "res": 1, "preds": [("US-14", "FS", 0)]},
            {"id": "US-17", "days": 4, "res": 3, "preds": [("US-15", "FS", 0)]},
            {"id": "TEST-S3-1", "days": 2, "res": 2, "preds": [("US-14", "SS", 4)]},
            {"id": "TEST-S3-2", "days": 2, "res": 3, "preds": [("US-16", "FS", 0), ("US-17", "FS", 0)]},
            {"id": "DOC-S3-1", "days": 1, "res": 2, "preds": [("TEST-S3-2", "FS", 0)]},
            {"id": "DEP-S3-1", "days": 1, "res": 1, "preds": [("TEST-S3-2", "FS", 0)]},
        ]
    },
    {
        "num": 4, "start_idx": 30, "end_idx": 39,
        "tasks": [
            {"id": "TASK-01", "days": 4, "res": 1, "preds": [("DEP-S3-1", "FS", 0)]},
            {"id": "US-18", "days": 4, "res": 3, "preds": [("DEP-S3-1", "FS", 0)]},
            {"id": "US-19", "days": 3, "res": 1, "preds": [("TASK-01", "FS", 0)]},
            {"id": "US-20", "days": 3, "res": 3, "preds": [("US-18", "FS", 0)]},
            {"id": "TEST-S4-1", "days": 2, "res": 2, "preds": [("TASK-01", "SS", 4)]},
            {"id": "TEST-S4-2", "days": 2, "res": 3, "preds": [("US-19", "FS", 0), ("US-20", "FS", 0)]},
            {"id": "DOC-S4-1", "days": 1, "res": 2, "preds": [("TEST-S4-2", "FS", 0)]},
            {"id": "DEP-S4-1", "days": 1, "res": 1, "preds": [("TEST-S4-2", "FS", 0)]},
        ]
    },
    {
        "num": 5, "start_idx": 40, "end_idx": 49,
        "tasks": [
            {"id": "US-21", "days": 3, "res": 1, "preds": [("DEP-S4-1", "FS", 0)]},
            {"id": "US-22", "days": 3, "res": 3, "preds": [("DEP-S4-1", "FS", 0)]},
            {"id": "US-23", "days": 3, "res": 1, "preds": [("US-21", "FS", 0)]},
            {"id": "US-24", "days": 2, "res": 3, "preds": [("US-22", "FS", 0)]},
            {"id": "US-25", "days": 2, "res": 1, "preds": [("US-23", "FS", 0)]},
            {"id": "US-26", "days": 2, "res": 3, "preds": [("US-24", "FS", 0)]},
            {"id": "TEST-S5-1", "days": 2, "res": 2, "preds": [("US-21", "SS", 4)]},
            {"id": "TEST-S5-2", "days": 1, "res": 3, "preds": [("US-25", "FS", 0), ("US-26", "FS", 0)]},
            {"id": "DOC-S5-1", "days": 1, "res": 2, "preds": [("TEST-S5-2", "FS", 0)]},
            {"id": "DEP-S5-1", "days": 1, "res": 1, "preds": [("TEST-S5-2", "FS", 0)]},
        ]
    },
]

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
                else:
                    idx = pt["start_idx"]
                if idx > max_idx:
                    max_idx = idx
        t["start_idx"] = max_idx

RES_NAMES = {1: "Nguyên", 2: "Nhi", 3: "Tấn"}

for s in sprints:
    print(f"\n{'='*60}")
    print(f"SPRINT {s['num']}  (idx {s['start_idx']}-{s['end_idx']})")
    print(f"{'='*60}")
    
    # Check resource conflicts
    res_timeline = {1: [], 2: [], 3: []}
    overflows = []
    
    for t in s["tasks"]:
        end_idx = t["start_idx"] + t["days"] - 1
        overflow = end_idx > s["end_idx"]
        overflow_marker = " !!!! OVERFLOW !!!!" if overflow else ""
        if overflow:
            overflows.append(t["id"])
        
        start_date = working_dates[t["start_idx"]].strftime("%m/%d")
        end_date_str = working_dates[min(end_idx, len(working_dates)-1)].strftime("%m/%d")
        
        print(f"  {t['id']:12s}  res={RES_NAMES[t['res']]:6s}  idx={t['start_idx']:2d}-{end_idx:2d}  ({start_date}-{end_date_str})  {t['days']}d{overflow_marker}")
        
        for day in range(t["start_idx"], t["start_idx"] + t["days"]):
            res_timeline[t["res"]].append((day, t["id"]))
    
    # Check resource over-allocation
    print(f"\n  --- Resource Allocation Check ---")
    for res_id in [1, 2, 3]:
        days_used = {}
        for day, tid in res_timeline[res_id]:
            if day not in days_used:
                days_used[day] = []
            days_used[day].append(tid)
        
        conflicts = {d: tids for d, tids in days_used.items() if len(tids) > 1}
        if conflicts:
            print(f"  🔴 {RES_NAMES[res_id]}: OVER-ALLOCATED on {len(conflicts)} days!")
            for d, tids in sorted(conflicts.items()):
                print(f"      idx={d} ({working_dates[d].strftime('%m/%d')}): {', '.join(tids)}")
        else:
            total = len(days_used)
            print(f"  ✅ {RES_NAMES[res_id]}: OK ({total} days used of 10)")
    
    if overflows:
        print(f"\n  🔴 TASKS OVERFLOW SPRINT: {', '.join(overflows)}")
    else:
        print(f"\n  ✅ All tasks fit within sprint window")

# Cross-sprint dependency check
print(f"\n{'='*60}")
print("CROSS-SPRINT DEPENDENCY VALIDATION")
print(f"{'='*60}")
for s in sprints:
    for t in s["tasks"]:
        for p_id, p_type, lag in t["preds"]:
            if p_id in task_map:
                pt = task_map[p_id]
                if p_type == "FS":
                    required_start = pt["start_idx"] + pt["days"] + lag
                elif p_type == "SS":
                    required_start = pt["start_idx"] + lag
                else:
                    required_start = pt["start_idx"]
                
                if t["start_idx"] < required_start:
                    print(f"🔴 {t['id']} starts at idx={t['start_idx']} but pred {p_id} requires idx>={required_start}")
