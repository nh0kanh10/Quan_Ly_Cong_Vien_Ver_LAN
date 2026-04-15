# 🏟️ Báo Cáo Audit Schema — Database_DaiNam.sql
> **Scope:** Kiểm tra thiếu sót, dư thừa, và vi phạm chuẩn 3NF  
> **File:** `database/Database_DaiNam.sql` (~2261 dòng, ~87 tables)  
> **Audit Date:** 14/04/2026  
> **Session Update:** 14/04/2026 — Cập nhật trạng thái sau thảo luận

---

## LEGEND TRẠNG THÁI

| Icon | Ý nghĩa |
|---|---|
| ✅ DONE | Đã quyết định, chờ patch SQL |
| ❌ DROPPED | Bỏ đề xuất sau thảo luận |
| ⏳ PENDING | Chưa quyết định, đang suy nghĩ |
| 🔴 BLOCKED | Cần trả lời câu hỏi trước |
| ⚙️ IN DESIGN | Đang thiết kế chi tiết |

---

## TỔNG QUAN NHANH

| Hạng mục | Số vấn đề | Mức độ |
|---|---|---|
| Thiếu sót nghiệp vụ | 8 | HIGH |
| Dư thừa / Redundant | 5 | MEDIUM |
| Vi phạm 3NF | 6 | MEDIUM |
| Kỹ thuật / Consistency | 4 | LOW |

---

## PHẦN 1: THIẾU SÓT NGHIỆP VỤ

### 1.1 [HIGH] Không có bảng `BangChamCong`
**⚙️ IN DESIGN — Đang patch**

`LichLamViec` chỉ lưu kế hoạch ca, không có ghi nhận giờ thực tế.

**Quyết định sau thảo luận:**
- Chấm công bằng **quẹt thẻ RFID** vào/ra
- Kỷ luật cần track để ra **bảng lương**
- Hỗ trợ cả **FullTime (theo tháng)** và **PartTime (theo giờ)**

**Schema đã thiết kế:**
```sql
-- Template ca (thay CHECK cứng)
CaLamMau (Id, TenCa, GioBatDau, GioKetThuc, SoGioChuan PERSISTED, LoaiCa)

-- Mở rộng NhanVien
ALTER TABLE NhanVien ADD
    LoaiHopDong  NVARCHAR(20) CHECK IN ('FullTime','PartTime','TheoMua','Intern'),
    LuongCoBan   DECIMAL(15,0) NULL,   -- FullTime: lương/tháng
    LuongTheoGio DECIMAL(10,0) NULL;   -- PartTime: lương/giờ

-- Redesign LichLamViec (bỏ CHECK cứng CaLam)
LichLamViec (Id, IdNhanVien, IdKhuVuc, NgayLam, IdCaLamMau,
             GioBatDau, GioKetThuc, LoaiNV, TrangThai,
             UNIQUE(IdNhanVien, NgayLam, GioBatDau))

-- Chấm công thực tế (quẹt RFID)
BangChamCong (Id, IdNhanVien, IdLichLamViec NULL,
              ThoiGianVao, ThoiGianRa NULL,
              SoGioThucTe COMPUTED PERSISTED, TrangThai)

-- Kỷ luật
KyLuat (Id, IdNhanVien, NgayApDung, HinhThuc, SoTienTru,
        SoNgayDinhChi, MoTa, IdNguoiQuyetDinh)

-- Bảng lương tháng (xử lý cả 2 loại NV)
BangLuong (Id, IdNhanVien, Thang, Nam, LoaiHopDong,
           LuongCoBan, NgayLamThucTe,       -- FullTime
           SoGioThucTe, LuongTheoGio,        -- PartTime
           SoGioTangCa, PhuCapKhac,
           TongTruKyLuat, ThucLinh,
           TrangThai, UNIQUE(IdNhanVien, Thang, Nam))
```

**Luồng:**
- FullTime: Manager xếp LichLamViec cả tháng → NV quẹt → SP chốt lương cuối tháng
- PartTime: NV đăng ký ngày rảnh (KeHoach) → Manager approve (DaXacNhan) → quẹt → SP chốt theo giờ

---

### 1.2 [HIGH] Thiếu `MonAn` / `MenuNhaHang`
**⏳ PENDING — Chờ quyết định hướng thiết kế**

Hiện tại F&B chỉ có `SanPham (LoaiSanPham='AnUong')` — quá gộp, không có menu.

**Vấn đề cốt lõi:** Nếu `MonAn` tách riêng khỏi `SanPham` → ChiTietDonHang phải thêm FK mới.

**Đề xuất (chờ confirm):** MonAn IS-A SanPham, thêm metadata:
```sql
MonAn_Meta (
    IdSanPham    INT PK FK→SanPham,   -- LoaiSanPham='AnUong'
    IdNhaHang    INT FK→NhaHang,
    DanhMuc      NVARCHAR(50),         -- 'KhaiVi','MonChinh','TrangMieng','DoUong'
    MoTaNgan     NVARCHAR(200) NULL,
    CoMatThit    BIT DEFAULT 0,        -- allergy flag
    IsHienTren   BIT DEFAULT 1         -- ẩn/hiện trên menu
)
```

**Kho:** Tiếp tục xuất theo **Suất** — không đến nguyên liệu thô (out of scope).

> ❓ **[BLOCKED Q3]** Nhà hàng gọi món đi qua POS chung (`DonHang`) hay hệ thống riêng?

---

### 1.3 [HIGH] Thiếu log quét vé `LichSuQuetVe`
**✅ DONE — Sẽ thêm (audit only)**

`VeDienTu.ThoiGianQuet` chỉ lưu lần cuối — không biết lịch sử chi tiết.

**Mục đích (đã xác nhận):**
- Audit log — chống gian lận copy vé
- Báo cáo lưu lượng theo giờ (nếu cần)

```sql
LichSuQuetVe (
    Id              INT IDENTITY(1,1) PRIMARY KEY,
    IdVeDienTu      UNIQUEIDENTIFIER NOT NULL FK→VeDienTu,
    ThoiGianQuet    DATETIME NOT NULL DEFAULT GETDATE(),
    KetQua          NVARCHAR(20) NOT NULL
        CHECK (KetQua IN ('ThanhCong','SaiVe','DaHetLuot','VeHetHan','DaHuy')),
    GhiChu          NVARCHAR(200) NULL
    -- IdThietBiCong thêm sau khi clear Q5
)
```

---

### 1.4 [HIGH] Sức chứa theo thời gian thực
**🔴 BLOCKED — Phụ thuộc kiến trúc gate**

**Vấn đề:** Đề xuất bảng `LuongKhachKhuVuc` nhưng **không có nguồn data** — muốn biết số khách hiện tại PHẢI có hệ thống đếm vào/ra.

| Phương án | Yêu cầu | Khả thi? |
|---|---|---|
| Đếm vé quét vào - ra từng khu | Gate quét **cả ra** tại mỗi khu | 🔴 BLOCKED Q |
| Cảm biến đếm người | Hardware riêng | ❌ Ngoài scope |
| Nhân viên báo cáo thủ công | UI nhập tay | ⚠️ Không real-time |

> ❓ **[BLOCKED Q]** Công viên có gate quét ra tại từng khu không? → Nếu không: **DROP đề xuất này**.

---

### 1.5 [MEDIUM] Thiếu `ChiTietVatTuBaoTri`
**⏳ PENDING**

`LichBaoTri` có tổng chi phí nhưng không biết vật tư nào lấy từ kho nào.

```sql
ChiTietVatTuBaoTri (
    IdLichBaoTri INT FK→LichBaoTri,
    IdSanPham    INT FK→SanPham,
    SoLuong      INT NOT NULL,
    DonGia       DECIMAL(15,0),
    IdPhieuXuat  INT NULL FK→PhieuXuatKho  -- truy vết xuất kho bảo trì
)
```

---

### 1.6 [MEDIUM] `KhuyenMai` thiếu scope áp dụng và giới hạn
**⏳ PENDING — Đã rõ hướng, chưa implement**

**Đã thống nhất:** `KhuyenMai` và `BangGia` cùng tồn tại — khác bản chất:
- `BangGia` = thay đổi giá niêm yết theo thời điểm (Lễ, cuối tuần)
- `KhuyenMai` = giảm thêm trên giá đó (voucher, VIP only)

**Cần thêm:**
```sql
-- Whitelist sản phẩm được giảm
KhuyenMai_SanPham (IdKhuyenMai INT, IdSanPham INT, PRIMARY KEY(IdKhuyenMai, IdSanPham))

-- Giới hạn đối tượng
KhuyenMai_LoaiKhach (IdKhuyenMai INT, LoaiKhach NVARCHAR(20))

-- Giới hạn số lần dùng (trên bảng KhuyenMai)
ALTER TABLE KhuyenMai ADD
    SoLuongGioiHan INT NULL,    -- NULL = không giới hạn
    SoLuongDaDung  INT NOT NULL DEFAULT 0;
```

---

### 1.7 [MEDIUM] Thiếu `HoaDon` tách bạch
**❌ DROPPED — Không cần**

**Quyết định:** `DonHang` + `PhieuThu` là đủ. In receipt = lấy data từ view gom các bảng lại.
MST doanh nghiệp lấy từ `DoanKhach.MaSoThue`. Không cần bảng `HoaDon` riêng.

---

### 1.8 [LOW] Dịch vụ `BaoGiuTre`
**⏳ PENDING — Chưa rõ Đại Nam có không**

---

## PHẦN 2: DƯ THỪA

### 2.1 `ThietBiTaoSong` trùng `DanhSachThietBi`
**✅ DONE — Xóa `ThietBiTaoSong`**

Seed data bị nhân đôi tại L1744-1748 và L2130-2132.
Thêm `CongSuat NVARCHAR(50) NULL` vào `DanhSachThietBi` để giữ thông tin đặc thù.

---

### 2.2 `BaoTriPhuongTienDua` trùng `LichBaoTri`
**✅ DONE — Xóa `BaoTriPhuongTienDua`**

Schema đã tự comment "Replaced" nhưng bảng vẫn còn — cần drop cùng FK.

---

### 2.3 `KhachHang.NgayDangKy` trùng `CreatedAt`
**✅ DONE — Xóa `NgayDangKy`**

Cùng `DEFAULT GETDATE()`, cùng ý nghĩa, không có gì khác biệt.

---

### 2.4 `DonHang.TongTien` computed redundancy
**✅ DONE (xử lý ở code, không phải SQL)**

Giữ cột trong DB, nhưng logic tính và sync `TongTien` xử lý tại **BUS layer** (C# code), không dùng trigger SQL.

---

### 2.5 `GiaGuiXe` không dùng `BangGia` engine
**✅ DONE — Migrate sang BangGia**

Tạo `SanPham (LoaiSanPham='GuiXe')` cho từng loại xe → dùng `BangGia` engine chung (có giá ngày lễ, cuối tuần).

---

## PHẦN 3: VI PHẠM 3NF

### 3.1 [HIGH] `QuyTacDiem.LoaiKhachApDung` sai domain
**✅ DONE — Fix CHECK constraint**

Seed data dùng `'BacSilver','VangGold','Kim'` nhưng `KhachHang.LoaiKhach` CHECK không có các giá trị này → logic tích điểm **hoàn toàn không hoạt động**.

**Fix:**
```sql
-- Option A: Thêm vào CHECK constraint KhachHang
ALTER TABLE KhachHang DROP CONSTRAINT [tên_constraint_cũ];
ALTER TABLE KhachHang ADD CONSTRAINT ChkLoaiKhach
    CHECK (LoaiKhach IN (
        'CaNhan','Doan','DoanhNghiep','HocSinhSinhVien',
        'VIP','VVIP','NoiBo',
        'BacSilver','VangGold','Kim'  -- thêm mới
    ));

-- Đồng thời fix seed data QuyTacDiem
UPDATE QuyTacDiem SET LoaiKhachApDung = 'BacSilver' WHERE TenQuyTac LIKE N'%bạc%';
UPDATE QuyTacDiem SET LoaiKhachApDung = 'VangGold'  WHERE TenQuyTac LIKE N'%vàng%';
UPDATE QuyTacDiem SET LoaiKhachApDung = 'Kim'        WHERE TenQuyTac LIKE N'%diamond%';
```

> **Note:** Đang cân nhắc tạo bảng `HangKhach` riêng thay vì string CHECK.

---

### 3.2 [MEDIUM] `NhanVien.BoPhan` transitive dependency
**⏳ PENDING — Đang suy nghĩ UX login**

**Vấn đề:** `BoPhan` là string tự do, không FK.
**Mục tiêu rộng hơn:** Nhân viên đăng nhập → hệ thống tự biết thuộc khu nào → lọc sản phẩm tương ứng.

> **Hướng suy nghĩ:** `NhanVien.IdKhuVuc` (đã có) có thể làm điểm neo thay vì tạo `BoPhan` riêng. Cần clarify use case trước khi refactor.

---

### 3.3 [MEDIUM] `DiemTichLuy` vs `TongChiTieu`
**✅ DONE — Giữ cả 2, đúng bản chất**

Hai cột khác nhau về nghĩa:
- `DiemTichLuy` = điểm hiện có để **tiêu dùng** (có thể âm khi dùng)
- `TongChiTieu` = tổng tiền tích lũy để **tính hạng** (chỉ tăng, không giảm)

**Rủi ro sync vẫn còn** → đã có `sp_CapNhatChiTieuVaHang` nhưng SP này **chưa update `DiemTichLuy`** — cần fix SP.

---

### 3.4 [MEDIUM] `DatBan.TienCoc` duplicate với `PhieuThu`
**✅ DONE — Bỏ `TienCoc`, dùng JOIN PhieuThu**

```sql
ALTER TABLE DatBan DROP COLUMN TienCoc;
-- Đọc: SELECT pt.SoTien FROM PhieuThu pt WHERE pt.Id = DatBan.IdPhieuThuCoc
```

---

### 3.5 [LOW] `SanPham.DonGia` ambiguous với `BangGia`
**⏳ PENDING — Đang suy nghĩ UX form**

**Vấn đề UX:** Form thêm SP dùng chung tab BangGia (có ComboBox chọn LoaiGia). Nếu bỏ `DonGia` thì sản phẩm có cọc cần giao diện riêng.

**Giải pháp đề xuất:**
```
Form SanPham:
├── Tab "Thông tin": [Tên] [LoaiSP] [DonVi] [MoTa]
├── Tab "Giá cơ bản":
│   ├── Giá ngày thường → BUS auto INSERT BangGia('MacDinh', GiaBan)
│   └── Tiền cọc        → BUS auto INSERT BangGia.TienCoc
└── Tab "Bảng giá đầy đủ": GridView BangGia (cuối tuần, lễ, block time...)
```

> `SanPham.DonGia` giữ như display cache, canonical price = BangGia.

---

### 3.6 [LOW] `DatBan.TenNguoiDat/SoDienThoai` partially redundant
**✅ DONE — Giữ nguyên, document convention**

Walk-in không có tài khoản → cần fallback. Convention: Nếu `IdKhachHang IS NOT NULL` thì bỏ qua 2 cột này, đọc từ KhachHang.

---

## PHẦN 4: KỸ THUẬT & NHẤT QUÁN

### 4.1 [Security] `MatKhau` plain text
**✅ DONE — Enforce ở app layer**

Seed data dùng `'1'` và `'123456'` — chỉ dùng cho demo. Production phải hash BCrypt (NVARCHAR(100) đủ chứa hash 60 ký tự).

---

### 4.2 [MEDIUM] Thiếu `CauHinhHeThong`
**⏳ PENDING**

```sql
CauHinhHeThong (
    Khoa    NVARCHAR(100) PRIMARY KEY,   -- 'DIEM_QUY_DOI_1000D', 'DIEM_HET_HAN_THANG'
    GiaTri  NVARCHAR(500) NOT NULL,
    MoTa    NVARCHAR(200) NULL,
    UpdatedAt DATETIME, UpdatedBy INT
)
```

---

### 4.3 [MEDIUM] `KhuyenMai` thiếu `SoLuongGioiHan`
**⏳ PENDING — Gộp vào task 1.6**

---

### 4.4 [LOW] Section numbering lộn xộn
**✅ DONE — Dọn khi patch cuối**

---

## NHỮNG GÌ SCHEMA LÀM TỐT (Giữ nguyên)

| Điểm mạnh | Chi tiết |
|---|---|
| Universal Line Item Pattern | `ChiTietDonHang` làm hub cho mọi dịch vụ |
| Weak Entity Pattern | `KhuVucBien`, `KhuVucThu` kế thừa `KhuVuc` đúng chuẩn |
| Audit Trail | `AuditDonHang`, `BangGia_LichSu`, `TheKho` |
| OCC với ROWVERSION | `ViDienTu`, `TonKho`, `BanAn`, `Phong` |
| Soft Delete nhất quán | `IsDeleted` trên các bảng master |
| Filtered Index | `WHERE IsDeleted = 0` |
| BangGia Engine | Giá theo giờ, ngày lễ, block time |
| RFID + Ví ACID | HashSignature, ParentTransactionId |

---

## PATCH SQL THEO THỨ TỰ

```
PATCH 1 — HR Module (ưu tiên, đã xong thiết kế)
  ✅ ALTER NhanVien: + LoaiHopDong, LuongCoBan, LuongTheoGio
  ✅ CREATE CaLamMau + seed 7 ca mẫu
  ✅ DROP + RECREATE LichLamViec (FK→CaLamMau, bỏ CHECK cứng)
  ✅ CREATE BangChamCong
  ✅ CREATE KyLuat
  ✅ CREATE BangLuong

PATCH 2 — Cleanup Redundancy (an toàn, không ảnh hưởng logic)
  ✅ DROP ThietBiTaoSong + ALTER DanhSachThietBi ADD CongSuat
  ✅ DROP BaoTriPhuongTienDua (drop FK trước)
  ✅ ALTER KhachHang DROP COLUMN NgayDangKy
  ✅ ALTER DatBan DROP COLUMN TienCoc
  ✅ CREATE LichSuQuetVe

PATCH 3 — Fix Logic Errors (critical)
  ✅ Fix QuyTacDiem domain + KhachHang CHECK constraint
  ✅ Fix sp_CapNhatChiTieuVaHang (thêm update DiemTichLuy)
  ✅ Migrate GiaGuiXe → BangGia

PATCH 4 — F&B + KhuyenMai (sau khi Q3 clear)
  ⏳ CREATE MonAn_Meta
  ⏳ ALTER KhuyenMai + CREATE KhuyenMai_SanPham, KhuyenMai_LoaiKhach

PATCH 5 — Config + Cleanup
  ⏳ CREATE CauHinhHeThong
  ⏳ Dọn section numbering trong SQL file
```

---

## CÂU HỎI CÒN BLOCKED

| Q# | Câu hỏi | Block cái gì |
|---|---|---|
| Q3 | Nhà hàng gọi món → POS chung hay riêng? | Patch 4 - MonAn design |
| Q4 | Cần báo cáo lưu lượng theo giờ/cổng không? | LichSuQuetVe scope |
| Q5 | Thiết bị check vé: A/B/C/D? | IdThietBiCong trong LichSuQuetVe |
| Q-gate | Có gate quét ra tại từng khu không? | Mục 1.4 - sức chứa real-time |
