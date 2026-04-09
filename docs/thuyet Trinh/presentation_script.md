# 🎤 BÀI THUYẾT TRÌNH: HỆ THỐNG QUẢN LÝ KHU DU LỊCH ĐẠI NAM
## Thời lượng: 15 phút | Đối tượng: Lớp học + Giảng viên phản biện

---

# 📋 MỤC LỤC THUYẾT TRÌNH

| Phần | Nội dung | Thời gian |
|------|----------|-----------|
| 1 | Mở đầu — Giới thiệu đề tài | 0:00 — 1:30 |
| 2 | Tổng quan kiến trúc hệ thống | 1:30 — 3:30 |
| 3 | Phân tích Database — 56 bảng | 3:30 — 6:00 |
| 4 | 7 Module nghiệp vụ chính | 6:00 — 9:30 |
| 5 | Cơ chế bảo mật & an toàn dữ liệu | 9:30 — 11:00 |
| 6 | Phân tích lỗ hổng logic | 11:00 — 13:30 |
| 7 | Tổng kết & bài học rút ra | 13:30 — 15:00 |

---

# PHẦN 1: MỞ ĐẦU — GIỚI THIỆU ĐỀ TÀI (0:00 — 1:30)

## Script:

> Xin chào thầy/cô và các bạn. Hôm nay nhóm em xin trình bày đề tài **"Xây dựng phần mềm Quản lý Khu du lịch Đại Nam"**.

> Đại Nam là một trong những khu du lịch lớn nhất Việt Nam, nằm ở Bình Dương, với diện tích khoảng **450 hecta**. Nơi đây có rất nhiều mảng dịch vụ: biển nhân tạo 21.6 ha, vườn thú mở 12.5 ha với hơn 600 cá thể của 63 loài, trường đua ngựa/chó, khách sạn hơn 300 phòng, hệ thống nhà hàng sức chứa hàng ngàn người, và hàng chục trò chơi cảm giác mạnh.

> **Vấn đề thực tế** là: Một nơi lớn như vậy, mỗi ngày có hàng chục ngàn lượt khách, nếu quản lý bằng tay hoặc bằng Excel thì sẽ gặp rất nhiều vấn đề:
> - Không theo dõi được doanh thu từng khu vực
> - Không kiểm soát được tồn kho khi bán hàng tại các Kiosk
> - Không chống được gian lận vé (1 vé QR screenshot chia cho 10 người)
> - Không xử lý được thanh toán đa phương thức (tiền mặt + ví RFID + thẻ ngân hàng)

> Vì vậy, nhóm em xây dựng một **hệ thống quản lý tổng hợp** bao gồm: bán vé POS, quản lý khách sạn, nhà hàng, cho thuê đồ, bãi đỗ xe OCR, ví điện tử RFID, kho hàng, và bảo trì thiết bị.

> Công nghệ sử dụng:
> - **Backend**: SQL Server (Database), C# .NET Framework 4.7.2 (Business Logic)
> - **Frontend**: WinForms + DevExpress Controls + Guna.UI2
> - **Kiến trúc**: 3-tier (DAL → BUS → GUI)
> - **Nhận dạng**: Tesseract OCR (biển số xe), ZXing.Net (QR Code)

---

# PHẦN 2: TỔNG QUAN KIẾN TRÚC HỆ THỐNG (1:30 — 3:30)

## Script:

> Trước khi đi vào chi tiết database, em xin giới thiệu nhanh kiến trúc tổng quan của hệ thống.

### 2.1 Kiến trúc 3-tier

> Hệ thống của nhóm em theo mô hình **3 lớp chuẩn**:

```
┌─────────────────────────────────────────────────────────────────┐
│  🖥️  GUI Layer (WinForms + DevExpress)                        │
│     ├── 15 Module UI: POS, Hotel, Restaurant, Inventory...     │
│     ├── ThemeManager: Quản lý style tập trung cho toàn bộ app  │
│     └── Helpers: Format tiền, xử lý ảnh, OCR...               │
├─────────────────────────────────────────────────────────────────┤
│  ⚙️  BUS Layer (Business Logic)                               │
│     ├── 1 class BUS cho mỗi module (Singleton Pattern)         │
│     ├── Xử lý nghiệp vụ: tính giá, kiểm tra tồn kho, OCC     │
│     └── Gọi DAL, không bao giờ truy cập DB trực tiếp          │
├─────────────────────────────────────────────────────────────────┤
│  🗄️  DAL Layer (Data Access)                                  │
│     ├── ADO.NET thuần (SqlConnection, SqlCommand)              │
│     ├── Centralized connection string qua DatabaseHelper       │
│     └── Parameterized queries chống SQL Injection              │
└─────────────────────────────────────────────────────────────────┘
```

> **Tại sao chọn 3-tier thay vì cách khác?** Vì đây là đề tài học thuật, mình cần thể hiện rõ sự tách biệt trách nhiệm. GUI chỉ lo hiển thị, BUS lo logic, DAL lo kết nối DB. Nếu sau này muốn đổi từ WinForms sang Web, mình chỉ cần viết lại lớp GUI mà không ảnh hưởng gì tới BUS và DAL.

### 2.2 Cấu trúc thư mục dự án

> Dự án có **15 thư mục module** trong GUI:

| # | Module | Mô tả |
|---|--------|-------|
| 1 | **Auth** | Đăng nhập, phân quyền |
| 2 | **POS** | Bán vé tại quầy (Point of Sale) |
| 3 | **Ticket** | Mô phỏng quét vé QR/RFID |
| 4 | **Hotel** | Đặt phòng, check-in/check-out |
| 5 | **Restaurant** | Đặt bàn, gọi món |
| 6 | **Parking** | Gửi xe, OCR biển số |
| 7 | **Rental** | Cho thuê phao, tủ đồ, xe điện |
| 8 | **Wallet_RFID** | Ví điện tử, nạp tiền, quẹt thẻ |
| 9 | **Inventory** | Kho hàng, nhập/xuất, thẻ kho |
| 10 | **Catalog** | Danh mục sản phẩm, bảng giá, combo |
| 11 | **Customer** | Quản lý khách hàng, đoàn khách |
| 12 | **Staff** | Nhân sự, lịch ca làm việc |
| 13 | **Finance** | Phiếu thu/chi, sổ cái |
| 14 | **Marketing** | Sự kiện, khuyến mãi |
| 15 | **Maintenance** | Bảo trì thiết bị toàn khu |

---

# PHẦN 3: PHÂN TÍCH DATABASE — 56 BẢNG (3:30 — 6:00)

## Script:

> Phần quan trọng nhất của đề tài này nằm ở thiết kế CSDL. Database tên là **DaiNamResort**, gồm **56 bảng**, **5 views**, **2 triggers**, và **1 stored procedure**. Em sẽ chia thành 7 nhóm chính để dễ hiểu.

### 3.1 Nhóm 1: Danh Mục Cơ Bản (Master Data) — 11 bảng

```
VaiTro ──→ PhanQuyen ←── QuyenHan
   │
   ↓
NhanVien ──→ KhuVuc ──→ KhuVucBien (Weak Entity)
                    └──→ KhuVucThu  (Weak Entity)

DonViTinh ──→ SanPham ──→ SanPham_Ve (Weak Entity)
                     └──→ QuyDoiDonVi

DoanKhach ←── KhachHang
```

> Ở đây có một số điểm thiết kế đáng chú ý:

> **Thứ nhất — Soft Delete (Xóa mềm)**: Các bảng danh mục như NhanVien, KhachHang, SanPham đều có cột `IsDeleted BIT DEFAULT 0`. Tại sao? Vì nếu nhân viên A nghỉ việc mà mình DELETE cứng, thì hàng ngàn đơn hàng do A tạo sẽ bị mồ côi khóa ngoại. Nên mình chỉ đánh dấu `IsDeleted = 1` để ẩn khỏi giao diện, nhưng dữ liệu cũ vẫn tham chiếu được.

> **Thứ hai — Weak Entity**: Bảng `KhuVucBien` và `KhuVucThu` là **thực thể yếu** — PK của nó chính là FK trỏ về `KhuVuc`. Thiết kế này giúp mở rộng thuộc tính riêng (biển có độ sâu, yêu cầu phao; thú có diện tích, sức chứa động vật) mà không cần nhồi nhét vào bảng `KhuVuc` gốc.

> **Thứ ba — Đơn vị tính (UoM)**: Bảng `QuyDoiDonVi` giải quyết bài toán thực tế: Kiosk bán nước suối, lúc thì bán 1 Lon 15k, lúc khách đoàn mua 1 Thùng 24 lon giá sỉ 220k (thay vì 360k). Hệ thống khai báo tỷ lệ quy đổi 1 Thùng = 24 Lon, kèm `GiaBanRieng` cho đơn vị lớn. Khi bán 1 thùng, kho tự trừ 24 lon.

### 3.2 Nhóm 2: Đơn Hàng & Tài Chính (Core Transaction) — 10 bảng

```
          ┌──────────────┐
          │   DonHang    │ ← Trung tâm vũ trụ
          └──────┬───────┘
                 │ 1-N
    ┌────────────┼────────────────┐
    ↓            ↓                ↓
ChiTietDonHang  PhieuThu(N)    KhuyenMai
    │               │
    ├── VeDienTu    ├── PhieuChi
    ├── DatPhongChiTiet
    ├── ThueDoChiTiet
    ├── DatBan
    ├── VeDoXeChiTiet
    └── DatChoThuAn
```

> **Triết lý Centralized Order**: Mọi thứ khách mua — vé, cơm, phòng, phao, gửi xe — đều chui hết vào **MỘT bảng DonHang**. Từ đó chi tiết ra `ChiTietDonHang`, rồi từng dòng chi tiết lại "mọc cành" sang bảng con tương ứng (vé → VeDienTu, phòng → DatPhongChiTiet, v.v.).

> Các bảng tài chính (`DonHang`, `GiaoDichVi`, `PhieuThu`, `PhieuChi`) **KHÔNG có IsDeleted**. Tại sao? Vì đây là sổ cái bất biến (Immutable Ledger). Tiền đã vào/ra hệ thống là phải có dấu vết. Nếu thu nhầm, kế toán phải làm phiếu hoàn tiền (reversal), tuyệt đối không xóa giao dịch cũ.

### 3.3 Nhóm 3: Ví Điện Tử & RFID — 3 bảng

```
KhachHang ──1:1──→ ViDienTu ──1:N──→ TheRFID
                       │
                       ↓ 1:N
                  GiaoDichVi (Sổ cái giao dịch)
```

> Bảng `ViDienTu` có 2 cột quan trọng:
> - `SoDuKhaDung`: Tiền khách còn dùng được
> - `SoDuDongBang`: Tiền bị "cất giấu" khi đặt cọc (thuê phao, thuê xe...)
>
> Cả hai đều có constraint `CHECK >= 0` — nghĩa là không bao giờ cho phép số dư âm.

> Bảng có cột `RowVer ROWVERSION` — dùng cho **Optimistic Concurrency Control**. Giải thích đơn giản: nếu 2 nhân viên cùng lúc trừ tiền ví của 1 khách, RowVer sẽ đảm bảo chỉ có 1 người thành công, người kia phải refresh lại.

### 3.4 Nhóm 4: Khách Sạn — 4 bảng

```
LoaiPhong ──→ Phong (18 phòng seed)
                │
                ↓
         DatPhongChiTiet ──→ ChiTietDatPhong (1 booking, N phòng)
```

> Thiết kế khách sạn tách thành **LoaiPhong** (Superior, Deluxe, Family, Villa) và **Phong** (phòng vật lý). `LoaiPhong` gắn với `SanPham` để dùng chung hệ thống giá `BangGia`.

> Tại sao `DatPhongChiTiet` và `ChiTietDatPhong` lại tách ra 2 bảng? Vì 1 booking có thể đặt nhiều phòng cùng lúc (vd: gia đình 10 người đặt 3 phòng). Từng phòng có thể checkout độc lập với giá riêng.

### 3.5 Nhóm 5: Bảng Giá Phẳng (Flat Pricing Matrix)

```sql
BangGia (
    IdSanPham,        -- Sản phẩm nào
    GiaNgayThuong,    -- Giá ngày thường (T2-T6)
    GiaCuoiTuan,      -- Giá cuối tuần (T7-CN)
    GiaNgayLe,        -- Giá ngày lễ (tra CauHinhNgayLe)
    GioBatDau,        -- Khung giờ áp dụng (Happy Hour)
    GioKetThuc,
    PhutBlock,        -- Cấu hình thuê theo giờ
    PhutTiep,         -- Phụ thu mỗi block tiếp theo
    GiaPhuThu,
    TienCoc           -- Đặt cọc (NULL = không cần)
)
```

> Đây là thiết kế **Ma trận Giá Phẳng** — thay vì tạo bảng giá riêng cho từng loại dịch vụ (giá phòng, giá vé, giá thuê), tất cả đều đổ vào **1 bảng duy nhất** `BangGia`. Mọi sản phẩm (vé, phòng, thuê xe) đều map qua `IdSanPham` rồi tra bảng này để lấy giá.

> Hệ thống tra giá theo logic: 
> 1. Lấy ngày hôm nay → kiểm tra bảng `CauHinhNgayLe` → nếu có → dùng `GiaNgayLe`
> 2. Nếu không phải ngày lễ → kiểm tra `DATEPART(DW)` → T7/CN → `GiaCuoiTuan`
> 3. Còn lại → `GiaNgayThuong`

### 3.6 Nhóm 6: Kho Hàng & Chuỗi Cung Ứng — 7 bảng

```
NhaCungCap ──→ PhieuNhapKho ──→ ChiTietNhapKho ──→ TonKho
                                                      ↑
KhoHang ──────→ PhieuXuatKho ──→ ChiTietXuatKho ──────┘
                                                      │
                                                   TheKho (Ledger)
```

> Bảng `TheKho` là **sổ cái kho** — mỗi khi hàng vào/ra đều ghi 1 dòng. Có 5 loại giao dịch: `NHAP_KHO`, `XUAT_POS`, `XUAT_HUY`, `CHUYEN_KHO`, `KIEM_KE`. Nhờ vậy, quản lý luôn biết tại sao tồn kho thay đổi.

> `KhoHang` có 3 loại: `TrungTam` (kho tổng), `Kiosk` (kho quầy lẻ), `NhaHang` (kho bếp). Thiết kế này cho phép **chuyển kho** (kho trung tâm → kiosk khi sắp hết hàng).

### 3.7 Nhóm 7: Vận hành & Phụ trợ — 21 bảng còn lại

> Bao gồm: Trường đua (7 bảng), Biển nhân tạo (5 bảng), Vườn thú (4 bảng), An toàn/Sự cố (2 bảng), Bảo trì thiết bị (2 bảng), Đánh giá dịch vụ (1 bảng). Em sẽ không đi sâu vào từng bảng mà tập trung vào các module nghiệp vụ quan trọng ở phần tiếp theo.

---

# PHẦN 4: 7 MODULE NGHIỆP VỤ CHÍNH (6:00 — 9:30)

## Script:

### Module 1: POS — Bán vé tại quầy

> Luồng hoạt động:

```
Khách đến quầy
  │
  ├── Khách lẻ: Chọn vé/combo trên màn → Tạo DonHang → Thanh toán (Mặt/QR/RFID)
  │                                                        │
  │                                          ┌──────────────┴──────────────┐
  │                                          ↓                             ↓
  │                                    PhieuThu (tiền mặt)          GiaoDichVi (RFID)
  │                                          │
  │                                     VeDienTu sinh ra (N mã QR)
  │
  └── Khách đoàn: Chọn đoàn → Áp chiết khấu % → Tạo bill tổng → Sinh N vé
```

> POS là module "xương sống" — mọi giao dịch mua bán đều đi qua đây. Sau khi thanh toán, hệ thống sinh bảng `VeDienTu` với mã GUID duy nhất cho từng vé, in ra QR Code để khách quét tại cổng.

### Module 2: Ví RFID & Thanh toán

> Luồng nạp tiền → tiêu tiền → hoàn cọc:

```
1. NẠP TIỀN: Khách đưa tiền mặt → NV quẹt thẻ RFID
   → GiaoDichVi (LoaiGD='NapTien', SoTien=500k)
   → SoDuKhaDung += 500k

2. TIÊU TIỀN: Quẹt RFID tại POS/Nhà hàng
   → GiaoDichVi (LoaiGD='ThanhToanDichVu', SoTien=50k)
   → SoDuKhaDung -= 50k

3. ĐẶT CỌC: Thuê phao, cọc 100k
   → GiaoDichVi (LoaiGD='ThuCoc', SoTien=100k)
   → SoDuKhaDung -= 100k
   → SoDuDongBang += 100k

4. TRẢ PHAO: Phao nguyên vẹn → Hoàn cọc
   → GiaoDichVi (LoaiGD='HoanCoc', SoTien=100k)
   → SoDuDongBang -= 100k
   → SoDuKhaDung += 100k
   
5. PHAO RÁCH: Phạt 30k, hoàn 70k (Partial Penalty)
   → GiaoDichVi #1 (LoaiGD='HoanCoc',     SoTien=70k)
   → GiaoDichVi #2 (LoaiGD='ThuTienPhat', SoTien=30k)
   → Phải bọc trong BEGIN TRAN...COMMIT để đảm bảo tính nguyên tử
```

### Module 3: Khách Sạn

> Luồng đặt phòng:

```
Tìm phòng trống (WHERE TrangThai = 'Trong')
  │
  ├── Chọn loại phòng → Tra BangGia theo ngày (Thường/CT/Lễ)
  │
  ├── Tạo DonHang → ChiTietDonHang (LuuTru)
  │                    └── DatPhongChiTiet (NgayNhan, NgayTra)
  │                          └── ChiTietDatPhong (từng phòng, giá từng đêm)
  │
  ├── Phong.TrangThai → 'DaDat'
  │
  ├── Check-in: Phong.TrangThai → 'DangSuDung'
  │
  └── Check-out: Phong.TrangThai → 'Trong' (nhả RowVer)
```

### Module 4: Nhà Hàng & Đặt Bàn

```
Khách gọi đặt bàn
  │
  ├── DatBan (ThoiGianDenDuKien, SoLuongKhach)
  │     └── ChiTietDatBan (IdBanAn × N bàn)
  │
  ├── BanAn.TrangThai → 'DaDat'
  │
  ├── Khách đến: BanAn → 'DangSuDung', DatBan → 'DaNhan'
  │
  ├── Gọi món → Menu POS → ChiTietDonHang (AnUong)
  │
  └── Thanh toán → BanAn → 'Trong'
```

### Module 5: Bãi Đỗ Xe + OCR

```
XE VÀO:
  Camera chụp → Tesseract OCR đọc biển số
  → LuotVaoRaBaiXe (BienSo, ThoiGianVao, TrangThai='DangGui')
  → In vé QR (hoặc quẹt RFID)

XE RA:
  Quét QR/RFID → Tra LuotVaoRaBaiXe
  → Tính tiền: DATEDIFF(giờ) × GiaGuiXe
  → Tạo VeDoXeChiTiet → gộp vào DonHang
  → Thanh toán → Barrier mở
```

### Module 6: Cho Thuê Đồ

```
Thuê phao/tủ/xe:
  → ThueDoChiTiet (SoTienCoc, ThoiGianBatDau)
  → GiaoDichVi (ThuCoc) — đóng băng tiền trong ví
  → ThueTu (nếu thuê tủ đồ — gán MaPin cho khách)

Trả đồ:
  → Kiểm tra tình trạng → Hoàn cọc / Phạt
  → ThueDoChiTiet.TrangThaiCoc → 'DaHoan' / 'DaPhat'
```

### Module 7: Kho Hàng

```
NHẬP KHO:
  PhieuNhapKho → ChiTietNhapKho (DonGiaNhap riêng từng lô)
  → UPDATE TonKho += SoLuong
  → INSERT TheKho (Ledger ghi vết)

XUẤT KHO (bán POS):
  → UPDATE TonKho -= SoLuong
  → INSERT TheKho (LoaiGD='XUAT_POS')

CHUYỂN KHO:
  PhieuXuatKho (KhoXuat → KhoNhan)
  → Trừ tồn kho A, cộng tồn kho B

KIỂM KÊ:
  LichKiemKho → ChiTietKiemKho (SoLuongThucTe vs SoLuongHeThong)
  → Computed Column: ChenhLech = ThucTe - HeThong
```

---

# PHẦN 5: CƠ CHẾ BẢO MẬT & AN TOÀN DỮ LIỆU (9:30 — 11:00)

## Script:

### 5.1 Phân Quyền RBAC (Role-Based Access Control)

> Hệ thống phân quyền qua 3 bảng: `VaiTro`, `QuyenHan`, `PhanQuyen` (bảng trung gian quan hệ N-N).

| Vai trò | Quyền hạn | Hạn chế |
|---------|-----------|---------|
| **Admin** | Toàn quyền (36 MaQuyen) | Không có hạn chế |
| **QuanLy** | VIEW tất cả + MANAGE cơ bản | Không xóa được nhân viên |
| **NhanVien** | VIEW + Tạo đơn + POS | Không thấy giá vốn, không thấy kho |
| **ThuKho** | Nhập/Xuất kho | Không thấy tài chính |
| **KeToan** | Thu/Chi, Báo cáo | Không sửa tồn kho |

> Hệ thống có **36 mã quyền** chi tiết (VIEW_DONHANG, MANAGE_PRICE, MANAGE_RFID...), được seed sẵn trong database. Code C# kiểm tra quyền trước khi hiển thị menu.

### 5.2 RowVersion — Chống Overbooking

> Các bảng `Phong`, `BanAn`, `ViDienTu`, `TonKho` đều có cột `RowVer ROWVERSION`. Giải thích bằng ví dụ:

> Kịch bản: Phòng VIP duy nhất. Lễ tân A và Sales B cùng mở phòng đó lên lúc 10h00. RowVer lúc này = "0x0A". Sales B bấm Lưu trước → phòng chuyển "Đã Đặt", RowVer đổi thành "0x0B". Lễ tân A bấm Lưu sau 1 giây → gửi kèm RowVer cũ "0x0A". SQL Server đối chiếu, thấy khác → **từ chối UPDATE** → thông báo "Phòng này vừa được người khác đặt, vui lòng làm mới".

### 5.3 HashSignature — Chống Gian Lận Ví

> Bảng `GiaoDichVi` có cột `HashSignature NVARCHAR(255)`. Mỗi khi nạp tiền hợp lệ, code C# sẽ băm SHA256(IdVi + SoTien + ThoiGian + SecretKey) → lưu vào cột này. Nếu có ai chui thẳng vào DB sửa số dư, khi audit sẽ phát hiện tổng giao dịch không khớp hash.

### 5.4 Audit Trail — Theo Dõi Thay Đổi

> Trigger `TrgAuditDonHang` tự động ghi log mỗi khi trạng thái đơn hàng thay đổi:

```sql
-- Mỗi lần UPDATE DonHang.TrangThai → 1 dòng INSERT vào AuditDonHang
-- (IdDonHang, TrangThaiCu, TrangThaiMoi, ThoiGianThayDoi)
```

> Nhờ vậy, nếu có ai hủy đơn hàng bất thường, quản lý truy ngược được ai hủy, lúc nào.

---

# PHẦN 6: PHÂN TÍCH LỖ HỔNG LOGIC (11:00 — 13:30)

## Script:

> Phần này em xin thành thật chia sẻ **những lỗ hổng logic** mà nhóm đã phát hiện trong quá trình phát triển. Đây cũng là bài học thực tế mà em muốn ghi nhận.

### 🔴 Lỗ hổng 1: DonHang.TongTien không có Computed Column

> **Vấn đề**: Cột `TongTien` trong `DonHang` là giá trị nhập tay (hoặc code tính), không phải `PERSISTED Computed Column` từ `SUM(ChiTietDonHang.ThanhTien)`. Nếu code BUS tính sai hoặc có bug, `TongTien` sẽ lệch so với tổng chi tiết.

> **Tại sao chưa fix**: Vì TongTien = SUM(ThanhTien) - TienGiamGia, mà `TienGiamGia` phụ thuộc vào bảng `KhuyenMai` — không thể đặt Computed Column cross-table trong SQL Server.

> **Đề xuất**: Tạo View `V_TongTienDonHang` để audit check, hoặc dùng Trigger AFTER INSERT/UPDATE trên `ChiTietDonHang` để cập nhật lại `TongTien`.

---

### 🔴 Lỗ hổng 2: Không có ràng buộc chéo giữa ChiTietDonHang và các bảng con

> **Vấn đề**: `ChiTietDonHang` cho phép cả `IdSanPham` và `IdCombo` đều NULL. Comment trong SQL ghi: "Booking hubs (DatBan, DatPhong) may not have IdSanPham/IdCombo at creation". Nhưng không có CHECK constraint nào buộc ít nhất 1 trong 2 phải NOT NULL khi đơn hàng ở trạng thái `DaThanhToan`.

> **Rủi ro**: Có thể tồn tại dòng ChiTietDonHang "ma" — không reference gì cả.

> **Đề xuất**: Thêm trigger sau khi DonHang chuyển `DaThanhToan` → validate mọi CTDH phải có IdSanPham hoặc IdCombo.

---

### 🟡 Lỗ hổng 3: BangGia không có UNIQUE constraint

> **Vấn đề**: Bảng `BangGia` không có UNIQUE trên `(IdSanPham, GioBatDau, GioKetThuc)`. Nghĩa là có thể INSERT 2 dòng giá cho cùng 1 sản phẩm, cùng khung giờ, với giá khác nhau.

> **Comment trong SQL**: "trigger overlap đã GỠ BỎ (gây lỗi khi seed nhiều giá cùng SP, kiểm tra bằng BUS layer)". Tức là nhóm đã nhận ra vấn đề nhưng chuyển sang kiểm tra ở tầng code C# thay vì database.

> **Rủi ro**: Nếu BUS layer có bug → 2 giá song song cho 1 sản phẩm → tính tiền sai.

---

### 🟡 Lỗ hổng 4: GiaGuiXe tách biệt khỏi BangGia engine

> **Vấn đề**: Bảng `GiaGuiXe` lưu giá gửi xe riêng (GiaBanNgay, GiaQuaDem), KHÔNG đi qua hệ thống `BangGia` chung. Trong khi mọi dịch vụ khác (vé, phòng, thuê đồ) đều dùng chung engine BangGia.

> **Hệ quả**: Code phải xử lý 2 luồng tính giá khác nhau — 1 luồng cho BangGia, 1 luồng riêng cho xe. Tăng complexity.

> **Đề xuất**: Gộp GiaGuiXe vào BangGia. Tạo SanPham "Gửi xe ô tô", "Gửi xe máy" rồi dùng chung engine.

---

### 🟡 Lỗ hổng 5: Không có cơ chế hết hạn vé tự động

> **Vấn đề**: Database không có **SQL Agent Job** nào tự chuyển `VeDienTu.TrangThai = 'HetHan'` vào cuối ngày. Nếu khách mua vé hôm nay nhưng không xài, ngày mai vẫn quét được.

> **Tài liệu gốc**: File `BAN_MO_TA_HE_THONG_CONG_VIEN.txt` có mô tả Job "Chốt Ca & Filter" chạy lúc 18:00 mỗi ngày. Nhưng trong SQL schema thực tế **không có Job nào được tạo**.

> **Đề xuất**: Tạo SQL Agent Job hoặc xử lý ở tầng BUS khi quét vé (check ngày tạo vs ngày hiện tại).

---

### 🟡 Lỗ hổng 6: ComboChiTiet trigger 100% quá strict 

> **Vấn đề**: Trigger `TrgComboChiTietTyLe100` bắt tổng `TyLePhanBo` phải bằng 100% SAU MỖI INSERT/UPDATE/DELETE. Nhưng khi tạo combo mới, ta phải thêm từng sản phẩm một — dòng đầu mới 30%, chưa đủ 100% → trigger chặn ngay.

> **Giải pháp hiện tại**: Combo có `TrangThai = 'Bản nháp'`. Nhưng trigger không phân biệt trạng thái — nó check **tất cả** combo, kể cả bản nháp.

> **Đề xuất**: Sửa trigger để chỉ check combo ở trạng thái `'Kích hoạt'`, bỏ qua `'Bản nháp'`.

---

### 🟢 Lỗ hổng 7: DoanKhach.IdCombo đánh dấu DEPRECATED nhưng chưa xóa

> **Vấn đề**: Cột `IdCombo` trong `DoanKhach` ghi comment "[DEPRECATED] Dùng DoanKhach_DichVu thay thế". Nhưng cột vẫn tồn tại trong schema. Tuy không gây lỗi, nhưng gây nhầm lẫn cho developer mới đọc code.

---

### 🟢 Lỗ hổng 8: PhieuThu constraint loại trừ có thể quá chặt

> **Vấn đề**: PhieuThu có constraint `CHECK (NOT (IdDonHang IS NOT NULL AND IdGiaoDichVi IS NOT NULL))` — nghĩa là phiếu thu không được cùng lúc gắn với đơn hàng VÀ giao dịch ví.

> **Kịch bản mâu thuẫn**: Khách thanh toán đơn hàng BẰNG ví RFID. Lúc đó cần ghi phiếu thu liên kết cả `IdDonHang` (để biết đơn nào) VÀ `IdGiaoDichVi` (để biết trừ ví nào). Constraint này chặn kịch bản đó.

> **Đề xuất**: Bỏ constraint hoặc đổi thành: chỉ bắt buộc ít nhất 1 trong 2 phải NOT NULL.

---

### 🟢 Lỗ hổng 9: DatBan.TienCoc không liên kết ngược ViDienTu

> **Vấn đề**: `DatBan.TienCoc` lưu số tiền cọc và có `IdPhieuThuCoc` tham chiếu. Nhưng không có cơ chế tự động "đóng băng" số dư ví khi đặt cọc qua RFID — luồng cọc nhà hàng bị đứt gãy so với luồng cọc cho thuê (ThueDoChiTiet có `IdGiaoDichCoc`).

---

### 🟢 Lỗ hổng 10: Không có bảng lịch sử giá

> **Vấn đề**: Khi sửa giá trong `BangGia`, giá cũ bị ghi đè mất. Không có bảng `BangGia_LichSu` để lưu vết thay đổi giá theo thời gian.

> **Hệ quả**: Không thể audit "tháng trước bán vé 100k, tháng này đổi 120k" — vì chỉ còn giá mới nhất.

---

### 🟢 Lỗ hổng 11: View V_DoanhThuTheoModule bỏ sót Combo

> **Vấn đề**: View `V_DoanhThuTheoModule` JOIN `ChiTietDonHang LEFT JOIN SanPham`. Nhưng nếu dòng CTDH bán Combo (IdCombo NOT NULL, IdSanPham IS NULL), thì `sp.LoaiSanPham` sẽ NULL → rơi vào nhóm "Khác" → doanh thu combo không được phân loại đúng module.

---

### 🟢 Lỗ hổng 12: SanPham.DonGia vs BangGia — dư thừa?

> **Vấn đề**: Bảng `SanPham` có cột `DonGia`, bảng `BangGia` cũng có 3 cột giá riêng. Vậy khi code lấy giá, dùng cái nào? Nếu không có bảng giá → fallback sang `SanPham.DonGia`? Nhưng logic này không rõ ràng trong schema.

> **Đề xuất**: Coi `SanPham.DonGia` là "giá niêm yết tham khảo" (display price), còn `BangGia` là "giá bán thực tế". Code BUS luôn ưu tiên tra `BangGia` trước.

---

# PHẦN 7: TỔNG KẾT & BÀI HỌC RÚT RA (13:30 — 15:00)

## Script:

### 7.1 Tổng kết thành quả

> Tổng kết lại, hệ thống của nhóm em bao gồm:

| Hạng mục | Số liệu |
|----------|---------|
| Bảng dữ liệu | 56 bảng |
| Views báo cáo | 5 views (Dashboard, Doanh thu, Công suất, Cảnh báo kho, Chưa thanh toán) |
| Triggers | 2 (Audit đơn hàng, Combo tỷ lệ 100%) |
| Stored Procedure | 1 (Truy vấn chi tiết đơn hàng tổng hợp) |
| Foreign Keys | 90+ ràng buộc khóa ngoại |
| Indexes | 30+ chỉ mục (bao gồm Filtered Index cho Soft Delete) |
| Module GUI | 15 module chức năng |
| Seed Data | ~200 sản phẩm, 18 phòng, 50 bàn ăn, 50 tủ đồ, 29 loài thú |

### 7.2 Điểm mạnh

1. **Centralized Order**: Tất cả giao dịch đều hội tụ về DonHang — dễ audit, dễ báo cáo, dễ thanh toán 1 lần
2. **Ví RFID & Đóng băng cọc**: Giải quyết được bài toán thực tế "cầm tiền mặt đi bơi" rất thực tế
3. **OCC (RowVersion)**: Chống tranh chấp khi nhiều nhân viên thao tác cùng lúc
4. **Flat Pricing Matrix**: Đơn giản hóa hệ thống giá — 3 cột giá cho 3 loại ngày
5. **Phân quyền RBAC động**: Thêm/bớt quyền không cần sửa code

### 7.3 Hạn chế & hướng phát triển

1. **Chưa có SQL Agent Jobs**: Các job tự động (hết hạn vé, giải phóng bàn) chưa implement
2. **Chưa có lịch sử giá**: Cần bổ sung bảng tracking thay đổi giá
3. **Mobile App**: Hiện tại chỉ có WinForms, chưa có App khách hàng
4. **Reports nâng cao**: Cần thêm dashboard trực quan (chart, biểu đồ)
5. **Offline Mode**: Khi mất mạng, POS cần hoạt động offline rồi sync sau

### 7.4 Bài học rút ra

> Bài học lớn nhất của nhóm em là: **Database không chỉ là nơi chứa dữ liệu, mà là nơi thực thi nghiệp vụ**. Những thứ như CHECK constraint, UNIQUE index, RowVersion, Trigger — đây là "tuyến phòng thủ cuối cùng". Dù code C# có bug, database vẫn phải đảm bảo dữ liệu nhất quán.

> Và bài học thứ hai: **Đừng over-engineer**. Nhóm em đã cố gắng giữ thiết kế MVP — đủ dùng, đủ demo, và có thể mở rộng. Những tính năng phức tạp (nâng hạng phòng, hoàn tiền tự động) được đẩy sang Phase 2 thay vì cố nhồi vào và gây bug.

> Em xin kết thúc bài thuyết trình tại đây. Mời thầy/cô và các bạn đặt câu hỏi. Cảm ơn mọi người đã lắng nghe!

---

# 📝 PHỤ LỤC: CHUẨN BỊ Q&A (10 câu thường gặp)

### Q1: "Tại sao không dùng Entity Framework mà dùng ADO.NET thuần?"
> **A**: Tụi em muốn thể hiện sự hiểu biết về cách truy vấn SQL thực sự hoạt động. EF giấu hết đi — mình viết LINQ mà không biết SQL nó generate ra gì. Hơn nữa, với đề tài học thuật, thầy cô muốn thấy sinh viên viết được Parameterized Query, hiểu Connection Pooling, chứ không phải "bấm nút cho framework làm hết".

### Q2: "56 bảng có quá nhiều không? Có bảng nào dư thừa?"
> **A**: Thật ra có 1 số bảng chưa implement hoàn chỉnh trong code (Trường đua, Biển nhân tạo), nhưng schema vẫn để sẵn vì đây là thiết kế cho hệ thống thực sự của Đại Nam — nếu bỏ thì không phản ánh đầy đủ bài toán. Cột `DoanKhach.IdCombo` là 1 ví dụ dư thừa (đã DEPRECATED nhưng chưa xóa).

### Q3: "RowVersion hoạt động thế nào? Code xử lý ra sao?"
> **A**: Khi đọc 1 record, mình lưu cả RowVer vào Entity. Khi UPDATE, mình gửi kèm RowVer cũ trong WHERE clause: `WHERE Id = @id AND RowVer = @oldRowVer`. Nếu record đã bị ai sửa (RowVer đã đổi), UPDATE sẽ ảnh hưởng 0 dòng → code bắt được và báo lỗi concurrency.

### Q4: "Soft Delete có nhược điểm gì?"
> **A**: Có. Mọi query phải thêm `WHERE IsDeleted = 0`, nếu quên thì sẽ lấy cả dữ liệu đã xóa. Nhóm em dùng **Filtered Index** (`CREATE INDEX ... WHERE IsDeleted = 0`) để SQL Server tối ưu performance cho trường hợp này.

### Q5: "Hệ thống có chống SQL Injection không?"
> **A**: Có. DAL layer dùng **Parameterized Queries** 100% — không bao giờ nối chuỗi string vào SQL. Ví dụ: `cmd.Parameters.AddWithValue("@Id", id)` thay vì `"WHERE Id = " + id`.

### Q6: "Nếu 2 người cùng mua cái cuối cùng trong kho thì sao?"
> **A**: Bảng `TonKho` có `RowVer`. Người A mua trước → RowVer đổi → UPDATE của người B WHERE RowVer = oldVer sẽ match 0 dòng → BUS layer catch và thông báo "Sản phẩm vừa được bán, vui lòng kiểm tra lại tồn kho".

### Q7: "Tại sao DonHang cho phép cả tiền mặt và ví RFID cùng lúc?"
> **A**: Đó là kịch bản Split Payment thực tế. Khách ăn 2 triệu, ví chỉ còn 1.5 triệu → quẹt RFID trả 1.5tr, còn 500k trả tiền mặt. Mình tạo 2 dòng `PhieuThu` cùng trỏ về 1 `DonHang`. Khi tổng PhieuThu = TongTien → DonHang.TrangThai = 'DaThanhToan'.

### Q8: "Combo phân bổ doanh thu hoạt động ra sao?"
> **A**: Ví dụ Combo 1 triệu gồm: Vé cổng (20%), Vé biển (50%), Ăn trưa (30%). Khi bán 1 combo, hệ thống không chia tiền ngay — mà cuối tháng kế toán chạy query JOIN `ComboChiTiet` lấy tỷ lệ → phân bổ: 200k → sổ Vé cổng, 500k → sổ Biển, 300k → sổ Nhà hàng.

### Q9: "Hệ thống xử lý khách đoàn 500 người thế nào?"
> **A**: Tạo 1 DoanKhach (chiết khấu 15%), 1 DonHang tổng. ChiTietDonHang chỉ 1 dòng: SanPham=VéCombo, SL=500. Nhưng bảng VeDienTu sinh ra 500 GUID → 500 QR riêng biệt. Mỗi người quét 1 mã vào cổng, mã đã quét thì không quét lại được (TrangThai chuyển 'DaSuDung' ngay lập tức).

### Q10: "Bảng GiaoDichVi có HashSignature — mình hash bằng gì?"
> **A**: Hash = SHA256(IdVi + LoaiGiaoDich + SoTien + ThoiGian + SecretKey). SecretKey nằm trong file .env hoặc config, không hardcode. Khi audit, code C# re-hash và so sánh với HashSignature trong DB. Nếu khác → có dấu hiệu bị sửa DB trái phép.

---

> [!TIP]
> **Mẹo khi thuyết trình**: 
> - Khi đến phần lỗ hổng logic, **đừng sợ**. Việc nhóm tự phát hiện lỗi thể hiện sự hiểu biết sâu hơn việc "nó chạy được là xong".
> - Nếu giảng viên hỏi "tại sao có lỗi mà không fix?" → Trả lời: "Do thời gian có hạn và nhóm đã đánh giá rủi ro, những lỗ hổng này thuộc loại có thể khắc phục ở Phase 2 mà không ảnh hưởng đến demo MVP."
> - Chuẩn bị sẵn **database diagram** (ERD) in ra giấy A3 để chỉ khi được hỏi.
