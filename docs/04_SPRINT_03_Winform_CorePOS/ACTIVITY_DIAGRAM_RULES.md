# ACTIVITY DIAGRAM RULES — Hướng dẫn vẽ Activity Diagram

> **Mục đích**: Hướng dẫn cho AI hoặc developer thêm/sửa Activity Diagram trong file `activity_diagrams.html`.
> Tuân thủ đúng format để diagram tự render chuẩn UML Swimlane.

---

## I. CẤU TRÚC DỮ LIỆU

### 1. Thêm Module mới

Mở file `activity_diagrams.html`, tìm object `DIAGRAMS`. Thêm key mới:

```javascript
const DIAGRAMS = {
  pos: { ... },     // Module đã có
  gate: { ... },    // Module đã có
  booking: { ... }, // Module đã có
  rental: { ... },  // Module đã có
  
  // === THÊM MODULE MỚI TẠI ĐÂY ===
  kho: {
    tabs: [
      {id:'kho_nhap', label:'Nhập kho'},
      {id:'kho_xuat', label:'Xuất kho'},
    ],
    diagrams: {
      kho_nhap: { title:'...', lanes:[...], nodes:[...], edges:[...] },
      kho_xuat: { title:'...', lanes:[...], nodes:[...], edges:[...] },
    }
  },
};
```

Đồng thời **thêm button** vào mảng `modules` trong hàm `initTabs()`:

```javascript
const modules = [
  {id:'pos',   label:'I. Bán Hàng POS'},
  // ...existing...
  {id:'kho',   label:'V. Quản Lý Kho'},  // ← THÊM
];
```

### 2. Cấu trúc 1 Diagram

```javascript
{
  title: 'Tiêu đề diagram (hiện trên cùng)',
  lanes: ['Tên lane trái (Actor)', 'Tên lane phải (System)'],
  nodes: [ /* danh sách node */ ],
  edges: [ /* danh sách edge */ ],
}
```

---

## II. NODE TYPES (Các loại node)

### Bảng tham chiếu

| Type       | Hình dạng           | Thuộc tính bắt buộc          | Mô tả                        |
|------------|---------------------|------------------------------|-------------------------------|
| `start`    | ● Hình tròn đen     | `id, lane, row`              | Điểm bắt đầu                 |
| `end`      | ◉ Tròn lồng tròn    | `id, lane, row`              | Điểm kết thúc                |
| `activity` | ▢ Hình chữ nhật bo  | `id, lane, row, text`        | Hành động / Action            |
| `decision` | ◇ Hình thoi lớn     | `id, lane, row, text`        | Điểm rẽ nhánh có điều kiện   |
| `fork`     | ▬ Thanh ngang đen   | `id, lane, row`              | Bắt đầu luồng song song     |
| `join`     | ▬ Thanh ngang đen   | `id, lane, row`              | Kết thúc luồng song song    |

### Thuộc tính Node

```javascript
{
  id:   'a1',          // ID duy nhất trong diagram
  type: 'activity',    // Loại node (xem bảng trên)
  lane: 0,             // 0 = lane trái (Actor), 1 = lane phải (System), 0.5 = giữa 2 lane
  row:  3,             // Vị trí hàng (0 = trên cùng, tăng dần xuống dưới)
  text: 'Dòng 1\nDòng 2',  // Nội dung (dùng \n để xuống dòng)
  barW: 290,           // (Chỉ fork/join) Chiều rộng thanh bar, mặc định=100, dùng 290 để kéo rộng qua 2 lane
}
```

### Quy tắc đặt Lane

| Loại hành động | Lane | Ví dụ |
|---|---|---|
| **Actor thao tác** (bấm nút, nhập liệu, quét mã) | `lane: 0` | "Nhấn thanh toán", "Quét mã vé" |
| **Actor quyết định** (xác nhận, hủy, chọn) | `lane: 0` | "Xác nhận thanh toán?", "Tiếp tục quét?" |
| **System xử lý** (tính toán, lưu DB, validate) | `lane: 1` | "Tính CK/KM", "Lưu đơn hàng" |
| **System kiểm tra** (tìm thấy?, đủ tồn kho?) | `lane: 1` | "Tìm thấy SP?", "SL hợp lệ?" |
| **Shared / Điểm chung** (start, end, fork/join giữa) | `lane: 0.5` | Start, End, Fork/Join rộng |

> ⚠️ **QUAN TRỌNG**: Decision "Xác nhận?" luôn ở **lane 0 (Actor)** vì NHÂN VIÊN bấm nút xác nhận, không phải hệ thống tự quyết.

---

## III. EDGE RULES (Quy tắc nối)

### Cấu trúc Edge

```javascript
{
  from:  'a1',       // ID node nguồn
  to:    'a2',       // ID node đích
  guard: 'Có',       // (Tùy chọn) Điều kiện rẽ nhánh, hiện dạng [Có]
  fp:    'left',     // (Tùy chọn) Cổng ra: 'top'|'bottom'|'left'|'right'
  tp:    'top',      // (Tùy chọn) Cổng vào, mặc định='top'
  skipR: true,       // (Tùy chọn) Route qua cạnh PHẢI (bypass)
  loop:  true,       // (Tùy chọn) Đánh dấu là vòng lặp
}
```

### Bảng routing tự động (KHÔNG cần set fp thủ công)

| Tình huống | fp tự động | Routing |
|---|---|---|
| Đích ở dưới, cùng lane | `bottom` | Đường thẳng xuống ↓ |
| Đích ở dưới, khác lane | `bottom` | Step pattern (xuống → ngang → xuống) |
| Đích ở trên (loop-back) | `left` | Vòng qua **lề trái** ← ↑ → |
| Cùng hàng, khác lane | `right`/`left` | Đường ngang → |

### Khi NÀO cần set `fp` thủ công

```javascript
// 1. Decision rẽ trái (fp:'left') — nhánh "Không" thường exit trái
{from:'d1', to:'a3', guard:'Không', fp:'left'}

// 2. Decision rẽ phải + bypass (fp:'right', skipR:true) — vòng qua lề phải
{from:'d1', to:'j1', guard:'Không: ĐỎ', fp:'right', skipR:true}

// 3. Loop-back (fp:'left') — quay lại node phía trên
{from:'a7', to:'a4', guard:'Tiếp tục', fp:'left'}
```

### Quy tắc decision edges

Decision luôn có **ít nhất 2 đường ra**:

```javascript
// Mẫu chuẩn: 1 nhánh "Có" đi xuống, 1 nhánh "Không" đi trái
{from:'d1', to:'nextNode',   guard:'Có'},              // → xuống (default)
{from:'d1', to:'loopTarget', guard:'Không', fp:'left'}, // ← trái (loop/alt)
```

### Fork / Join edges

Fork/Join có routing đặc biệt (đường thẳng đứng qua bar):

```javascript
// Fork: 1 vào, 2+ ra
{from:'d4', to:'f1', guard:'Có'},   // vào fork bar
{from:'f1', to:'a9a'},              // ra nhánh trái (tự align theo lane)
{from:'f1', to:'a9b'},              // ra nhánh phải

// Join: 2+ vào, 1 ra
{from:'a9a', to:'j1'},              // vào join bar từ trái
{from:'a9b', to:'j1'},              // vào join bar từ phải
{from:'j1',  to:'nextNode'},        // ra khỏi join
```

> Fork/Join với `barW: 290` sẽ kéo bar rộng qua cả 2 lane, mũi tên tự canh thẳng đứng theo lane của node đích.

---

## IV. QUY TẮC UML CẦN TUÂN THỦ

### ✅ BẮT BUỘC

1. **Mỗi diagram phải có 1 Start và ≥1 End** — không được có "ngõ cụt" (dead-end)
2. **Mọi node phải reachable từ Start** — không có node mồ côi
3. **Phải có đường đến End** — tránh vòng lặp vô tận (loop phải có exit path)
4. **Decision phải có ≥2 nhánh ra** với guard conditions rõ ràng
5. **Guard dùng ngoặc vuông** — hiển thị dạng `[Có]`, `[Không]`
6. **Viết tiếng Việt CÓ DẤU** — "Kiểm soát", không phải "Kiem soat"

### ❌ KHÔNG DÙNG

1. **KHÔNG dùng Merge node** (type:'merge') — Nối 2 nhánh thẳng vào node tiếp theo
2. **KHÔNG dùng emoji** trong text node — "Trả đồ thành công", không phải "✅ Trả đồ"
3. **KHÔNG đặt "Xác nhận" ở lane System** — Actor xác nhận, System validate

### ⚡ PHÂN BIỆT Fork/Join vs Decision

| | Decision (◇) | Fork/Join (▬) |
|---|---|---|
| **Luồng** | THAY THẾ — chỉ 1 nhánh được chọn | SONG SONG — tất cả nhánh chạy đồng thời |
| **Hình** | Hình thoi | Thanh ngang đen |
| **Guard** | Bắt buộc (`[Có]`, `[Không]`) | Không cần |
| **Ví dụ** | "Đủ tồn kho?" → Có/Không | Lưu đơn ∥ Phiếu thu ∥ Trừ kho |

---

## V. LAYOUT TIPS

### Row spacing

- Mỗi `row` cách nhau **74px** (C.ROW_H)
- Node cùng hàng nhưng khác lane → cùng `row`, khác `lane`
- Start thường ở `row: 0`, End ở row cuối cùng

### Tránh chồng chéo

```javascript
// ✅ TỐT: 2 activity song hàng ở 2 lane khác nhau
{id:'a8', type:'activity', lane:1, row:11, text:'Hoàn RFID'},
{id:'a9', type:'activity', lane:0, row:11, text:'Hoàn tiền mặt'},

// ❌ XẤU: 2 node cùng lane, cùng row → CHỒNG LÊN NHAU
{id:'a8', type:'activity', lane:1, row:11, text:'...'},
{id:'a9', type:'activity', lane:1, row:11, text:'...'}, // BUG!
```

### Loop-back offsets

Nhiều loop-back arrows tự offset ra lề trái (C.LOOP_GAP = 10px):
- Loop đầu tiên: sát lề
- Loop thứ 2: dịch ra 10px
- Loop thứ 3: dịch ra 20px
- v.v.

Tương tự, nhiều `skipR` arrows offset ra lề phải.

---

## VI. TEMPLATE MẪU — Copy & Paste

```javascript
// === TEMPLATE: Thêm 1 diagram đơn giản ===
my_workflow: {
  title: 'Tiêu đề Activity Diagram',
  lanes: ['Nhân viên', 'Hệ thống'],
  nodes: [
    {id:'s',  type:'start',    lane:0, row:0},
    {id:'a1', type:'activity', lane:0, row:1, text:'Mở màn hình'},
    {id:'a2', type:'activity', lane:1, row:2, text:'Tải dữ liệu'},
    {id:'a3', type:'activity', lane:0, row:3, text:'Nhập thông tin'},
    {id:'d1', type:'decision', lane:1, row:4, text:'Hợp lệ?'},
    {id:'a4', type:'activity', lane:1, row:5, text:'Lưu dữ liệu\nThông báo thành công'},
    {id:'e',  type:'end',      lane:.5,row:6},
  ],
  edges: [
    {from:'s',  to:'a1'},
    {from:'a1', to:'a2'},
    {from:'a2', to:'a3'},
    {from:'a3', to:'d1'},
    {from:'d1', to:'a3', guard:'Không', fp:'left'},     // loop back
    {from:'d1', to:'a4', guard:'Có'},                    // continue
    {from:'a4', to:'e'},
  ],
},
```

### Template: Fork/Join (Song song)

```javascript
// Sau 1 decision hoặc activity:
{id:'f1', type:'fork', lane:.5, row:5, barW:290},         // bar rộng 2 lane
{id:'a5', type:'activity', lane:0, row:6, text:'Actor làm gì đó'},
{id:'a6', type:'activity', lane:1, row:6, text:'System xử lý'},
{id:'j1', type:'join', lane:.5, row:7, barW:290},

// Edges:
{from:'prev_node', to:'f1'},          // vào fork
{from:'f1', to:'a5'},                 // ra nhánh trái
{from:'f1', to:'a6'},                 // ra nhánh phải
{from:'a5', to:'j1'},                 // vào join
{from:'a6', to:'j1'},                 // vào join
{from:'j1', to:'next_node'},          // ra khỏi join
```

### Template: Bypass (skipR)

```javascript
// Decision với 1 nhánh bypass qua bên phải:
{from:'d3', to:'a5', guard:'Có (xử lý)', fp:'left'},              // nhánh chính
{from:'d3', to:'d4', guard:'Không (bỏ qua)', fp:'right', skipR:true}, // bypass phải
```

---

## VII. CONSTANTS (Tham khảo)

```javascript
PAD: 55        // Lề ngoài
LANE_W: 290    // Chiều rộng mỗi lane
ROW_H: 74      // Khoảng cách giữa các hàng
ACT_W: 192     // Chiều rộng activity box
ACT_H: 44      // Chiều cao activity box
DIA_HW: 48     // Nửa chiều rộng hình thoi
DIA_HH: 28     // Nửa chiều cao hình thoi
BAR_W: 100     // Chiều rộng mặc định fork/join bar
```

SVG width = `PAD × 2 + LANE_W × 2` = **690px**

---

## VIII. CHECKLIST TRƯỚC KHI HOÀN TẤT

- [ ] Có đúng 1 Start node?
- [ ] Có ≥1 End node?
- [ ] Mọi node đều reachable từ Start?
- [ ] Có đường đến End (không vòng lặp vô tận)?
- [ ] Decision có ≥2 nhánh ra với guard?
- [ ] "Xác nhận" ở lane Actor (0)?
- [ ] Không có merge diamond?
- [ ] Không có emoji trong text?
- [ ] Viết tiếng Việt có dấu?
- [ ] Không có 2 node cùng lane + cùng row?
- [ ] Fork/Join đúng cặp (1 fork → 1 join)?
