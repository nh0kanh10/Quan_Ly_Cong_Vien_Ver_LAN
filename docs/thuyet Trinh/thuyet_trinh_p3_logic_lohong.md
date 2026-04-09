# THUYẾT TRÌNH — PHẦN 3: LUỒNG NGHIỆP VỤ + LỖ HỔNG LOGIC
## Thời gian: ~5 phút (8:30 — 13:30)

---

## 3.1 Các luồng nghiệp vụ chính (8:30 — 10:00)

### Luồng 1: Khách mua vé tại POS

```
Khách đến quầy → NV mở POS
  ↓
Chọn sản phẩm (vé, combo, đồ ăn)
  ↓
Hệ thống tra BangGia theo ngày (thường/CT/lễ) → lấy giá
  ↓
Tạo DonHang + ChiTietDonHang
  ↓
Thanh toán:
  - Tiền mặt → PhieuThu (PhuongThuc='TienMat')
  - RFID → GiaoDichVi (LoaiGD='ThanhToanDichVu') + PhieuThu (PhuongThuc='ViRFID')
  - Chia bill → 2 PhieuThu trỏ về 1 DonHang
  ↓
Nếu mua vé → sinh VeDienTu (GUID) → in QR
DonHang.TrangThai = 'DaThanhToan'
```

### Luồng 2: Thuê phao + trả phao

```
Thuê:
  → ThueDoChiTiet (SoTienCoc=100k)
  → GiaoDichVi (LoaiGD='ThuCoc', 100k)
  → ViDienTu: SoDuKhaDung -= 100k, SoDuDongBang += 100k

Trả (còn nguyên):
  → GiaoDichVi (LoaiGD='HoanCoc', 100k)
  → ViDienTu: SoDuDongBang -= 100k, SoDuKhaDung += 100k

Trả (hư hỏng, phạt 30k):
  → Bọc trong BEGIN TRAN
  → GiaoDichVi #1 (HoanCoc, 70k) → trả lại 70k
  → GiaoDichVi #2 (ThuTienPhat, 30k) → phạt 30k
  → COMMIT
```

### Luồng 3: Đặt phòng khách sạn

```
Tìm phòng trống → Chọn loại phòng → Tra BangGia
  ↓
Tạo DonHang → ChiTietDonHang
  → DatPhongChiTiet (NgayNhan, NgayTra)
    → ChiTietDatPhong (phòng 101, giá 600k; phòng 201, giá 1tr)
  ↓
Phong.TrangThai = 'DaDat'
  ↓
Check-in: TrangThai = 'DangSuDung'
Check-out: TrangThai = 'Trong', nhả RowVer
```

### Luồng 4: Gửi xe + OCR

```
Xe vào → Camera chụp → OCR đọc biển số
  → LuotVaoRaBaiXe (BienSo, ThoiGianVao)
  → In vé QR hoặc quẹt RFID

Xe ra → Quét QR/RFID → Tìm LuotVaoRaBaiXe
  → Tính tiền: giờ gửi × đơn giá
  → Tạo VeDoXeChiTiet → gộp vào DonHang → thanh toán → barrier mở
```

---

## 3.2 Cơ chế bảo vệ dữ liệu (10:00 — 11:00)

### RowVersion — Chống 2 người sửa cùng 1 record

> Các bảng có RowVer: `Phong`, `BanAn`, `ViDienTu`, `TonKho`, `ViTriNgoi`, `ChoiNghiMat`

> **Cách hoạt động**: Đọc record → lưu RowVer vào biến. Khi UPDATE → gửi kèm RowVer cũ trong WHERE. Nếu ai đó đã sửa trước → RowVer đã đổi → WHERE không match → UPDATE ảnh hưởng 0 dòng → code bắt lỗi.

### Soft Delete — Không xóa cứng danh mục

> `NhanVien`, `KhachHang`, `SanPham`, `KhoHang`, `KhuVuc`, `Combo`, `NhaCungCap` đều có `IsDeleted BIT DEFAULT 0`.

> Có **Filtered Index** để query nhanh: `CREATE INDEX IxSanPham_Active ON SanPham(Id) WHERE IsDeleted = 0`

### Immutable Ledger — Sổ cái không xóa

> `DonHang`, `GiaoDichVi`, `PhieuThu`, `PhieuChi` **không có IsDeleted**. Ghi rồi là giữ. Sai thì tạo giao dịch đảo ngược.

### HashSignature — Chống sửa DB trái phép

> `GiaoDichVi.HashSignature`: Mỗi giao dịch hợp lệ, code băm SHA256(IdVi + SoTien + ThoiGian + SecretKey). Nếu ai vào thẳng DB sửa số dư, khi audit sẽ thấy hash không khớp.

### Audit Trail — Tự động ghi vết

> Trigger `TrgAuditDonHang`: Mỗi khi UPDATE `DonHang.TrangThai`, tự INSERT 1 dòng vào `AuditDonHang` ghi trạng thái cũ → mới.

---

## 3.3 Phân tích lỗ hổng logic (11:00 — 13:30)

> Phần này em tự phát hiện trong quá trình code. Không phải chỗ nào cũng hoàn hảo.

### 🔴 Lỗ hổng 1: DonHang.TongTien không tự tính

**Vấn đề**: `TongTien` là giá trị do code BUS tính rồi INSERT vào, không phải Computed Column. Nếu code tính sai, DB không chặn được.

**Tại sao không dùng Computed Column?** Vì TongTien = SUM(CTDH.ThanhTien) - TienGiamGia - ChietKhauDoan. SQL Server không cho Computed Column tham chiếu cross-table.

**Cách khắc phục tạm**: Code BUS phải validate: sau khi INSERT xong, SELECT lại SUM(ThanhTien) so sánh với TongTien đã lưu.

---

### 🔴 Lỗ hổng 2: ChiTietDonHang cho phép cả IdSanPham và IdCombo đều NULL

**Vấn đề**: Không có CHECK ép ít nhất 1 cái phải NOT NULL. Có thể tồn tại dòng "ma" không reference gì.

**Lý do**: Comment trong SQL ghi "Booking hubs (DatBan, DatPhong) may not have IdSanPham at creation" — tức là khi đặt phòng/bàn, lúc đầu có thể chưa biết giá.

**Rủi ro**: Nếu không validate đúng ở BUS → dữ liệu rác.

---

### 🔴 Lỗ hổng 3: BangGia không có UNIQUE constraint

**Vấn đề**: Có thể INSERT 2 dòng giá cho cùng 1 sản phẩm, cùng khung giờ, giá khác nhau. SQL không chặn.

**Lý do**: Comment ghi "trigger overlap đã GỠ BỎ (gây lỗi khi seed nhiều giá cùng SP)". Nhóm chuyển sang check ở BUS.

**Rủi ro**: BUS có bug → 2 giá tồn tại song song → POS lấy giá nào?

---

### 🟡 Lỗ hổng 4: GiaGuiXe tách riêng khỏi BangGia

**Vấn đề**: Mọi dịch vụ (vé, phòng, thuê đồ) đều dùng BangGia. Riêng gửi xe có bảng giá riêng `GiaGuiXe`. Code phải xử lý 2 luồng tính giá khác nhau.

**Tốt hơn là**: Tạo SanPham "Gửi xe ô tô", "Gửi xe máy" rồi dùng chung BangGia. Nhưng chưa kịp refactor.

---

### 🟡 Lỗ hổng 5: Không có Job hết hạn vé tự động

**Vấn đề**: `VeDienTu` không tự chuyển 'HetHan' vào cuối ngày. Nếu khách mua vé hôm nay không xài, ngày mai vẫn quét được.

**Cách khắc phục**: Hoặc tạo SQL Agent Job chạy 18:00 mỗi ngày, hoặc code BUS check ngày tạo vé khi quét.

---

### 🟡 Lỗ hổng 6: Trigger ComboChiTiet 100% quá chặt khi tạo mới

**Vấn đề**: Trigger bắt tổng TyLePhanBo = 100% ngay sau mỗi INSERT. Nhưng khi tạo combo mới, thêm từng sản phẩm → dòng đầu 30% → trigger chặn vì chưa đủ 100%.

**Giải pháp**: Combo có trạng thái 'Bản nháp', nhưng trigger không phân biệt — nó check tất cả. Cần sửa trigger thêm điều kiện: chỉ check khi Combo.TrangThai = 'Kích hoạt'.

---

### 🟡 Lỗ hổng 7: DoanKhach.IdCombo deprecated nhưng chưa xóa

**Vấn đề**: Cột ghi "[DEPRECATED] Dùng DoanKhach_DichVu thay thế". Cột vẫn nằm đó. Không gây lỗi nhưng gây nhầm lẫn.

---

### 🟢 Lỗ hổng 8: PhieuThu constraint loại trừ có thể gây kẹt

**Vấn đề**: `CHECK (NOT (IdDonHang IS NOT NULL AND IdGiaoDichVi IS NOT NULL))` — phiếu thu không được gắn cùng lúc đơn hàng VÀ giao dịch ví.

**Kịch bản kẹt**: Khách thanh toán đơn hàng bằng RFID. Cần ghi phiếu thu liên kết cả IdDonHang (đơn nào) VÀ IdGiaoDichVi (trừ ví nào). Constraint chặn.

---

### 🟢 Lỗ hổng 9: DatBan.TienCoc không liên kết ví

**Vấn đề**: DatBan có `TienCoc` và `IdPhieuThuCoc`. Nhưng không có `IdGiaoDichCoc` như ThueDoChiTiet. Luồng cọc bàn ăn bị đứt gãy so với luồng cọc cho thuê.

---

### 🟢 Lỗ hổng 10: Không có lịch sử giá

**Vấn đề**: Sửa giá trong BangGia → giá cũ mất. Không truy lại được "tháng trước giá bao nhiêu".

**Cần**: Bảng `BangGia_LichSu` hoặc thêm cột `HieuLucTu`/`HieuLucDen` cho bảng BangGia.

---

### 🟢 Lỗ hổng 11: View V_DoanhThuTheoModule bỏ sót Combo

**Vấn đề**: View JOIN ChiTietDonHang LEFT JOIN SanPham. Bán combo → IdSanPham NULL → LoaiSanPham NULL → rơi vào nhóm "Khác". Doanh thu combo không phân loại đúng module.

---

### 🟢 Lỗ hổng 12: SanPham.DonGia vs BangGia — dùng cái nào?

**Vấn đề**: SanPham có `DonGia`, BangGia có 3 cột giá riêng. Nếu sản phẩm chưa có BangGia thì fallback sang SanPham.DonGia? Logic này không rõ ràng trong schema.

**Thực tế trong code**: BUS luôn tra BangGia trước. Nếu không có → dùng SanPham.DonGia. Nhưng đây là convention trong code, không có ràng buộc DB.
