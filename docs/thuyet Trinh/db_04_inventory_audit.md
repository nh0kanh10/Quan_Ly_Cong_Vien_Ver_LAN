# PHÂN TÍCH DATABASE — PHẦN 4: KHO HÀNG, BẢO TRÌ, AUDIT & SUPPORTING

> Phân tích 5W1H cho từng nhóm bảng. Mọi thông tin xác minh từ `Database_DaiNam.sql` và BUS layer.

---

## 1. KHO HÀNG & MUA SẮM (Section 12 SQL)

### NhaCungCap

| Item | Detail |
|------|--------|
| **What** | Danh mục nhà cung cấp: tên, MST, liên hệ |
| **How** | `Ten`, `MaSoThue`, `DiaChi`, `DienThoai`, `NguoiLienHe`, `IsDeleted` |
| **Seed** | 4 NCC: Thực phẩm ABC, Đồ chơi XYZ, Bia Sài Gòn, Thực phẩm Đại Nam |

### KhoHang

| Item | Detail |
|------|--------|
| **What** | Danh mục kho vật lý: phân loại TrungTam / Kiosk / NhaHang |
| **How** | `TenKho`, `LoaiKho` CHECK IN (TrungTam/Kiosk/NhaHang), `IsDeleted` |
| **Seed** | 4 kho: Kho trung tâm, Kiosk biển, Kiosk trường đua, Kho nhà hàng chính |
| **Thiết kế** | Gộp Kiosk TonKho cũ vào bảng TonKho chung (dùng `LoaiKho='Kiosk'`) |

### PhieuNhapKho + ChiTietNhapKho

| Item | Detail |
|------|--------|
| **PhieuNhapKho** | Header nhập kho: `IdKho`, `IdNhaCungCap`, `SoChungTu`, `TongTien`, `IdPhieuChi` FK (link phiếu chi thanh toán). Immutable — KHÔNG có IsDeleted |
| **ChiTietNhapKho** | Line items: `IdSanPham`, `SoLuong`, `DonGiaNhap`, `IdDonViNhap` FK (nhập theo Thùng), `TyLeQuyDoi` (quy ra đơn vị cơ bản) |

### PhieuXuatKho + ChiTietXuatKho

| Item | Detail |
|------|--------|
| **PhieuXuatKho** | Header xuất kho: `IdKhoXuat`, `IdKhoNhan` (NULL = xuất POS, NOT NULL = chuyển kho), `LyDo`, `IdDonHangLienQuan` |
| **ChiTietXuatKho** | Line items: `IdSanPham`, `SoLuong`, `DonGiaXuat`, `IdDonViXuat`, `TyLeQuyDoi` |
| **Luồng xuất** | *Xác minh BUS_KhoHang*: POS bán hàng → `BUS_KhoHang.GhiXuatKho` → PhieuXuatKho + ChiTietXuatKho + TonKho cập nhật |

### TonKho

| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Snapshot tồn kho realtime: kho nào, sản phẩm nào, còn bao nhiêu |
| **How** | `IdKho`, `IdSanPham`, `SoLuong` DEFAULT 0, `NguongCanhBao` DEFAULT 5, UNIQUE(IdKho,IdSanPham), `RowVer` ROWVERSION |
| **Tại sao RowVersion** | Chống race condition 2 quầy POS cùng trừ kho 1 sản phẩm |
| **View hỗ trợ** | `V_CanhBaoTonKho` — tự động phân loại: Hết hàng / Sắp hết / Đủ hàng |

### TheKho (Inventory Ledger / Sổ Thẻ Kho)

| Câu hỏi | Trả lời |
|---------|---------|
| **What** | **Sổ cái kho** — ghi LẠI MỌI biến động kho (append-only ledger) |
| **Why** | TonKho chỉ là snapshot hiện tại. TheKho giữ lịch sử đầy đủ để kiểm toán |
| **How** | 11 cột. Đáng chú ý: |

**Chi tiết cột quan trọng**:
- `LoaiGiaoDich` CHECK IN: `NHAP_KHO`, `XUAT_POS`, `XUAT_HUY`, `CHUYEN_KHO`, `KIEM_KE`
- `SoLuongThayDoi` INT — dương = nhập, âm = xuất
- `TonCuoi` INT NULL — tồn cuối sau giao dịch (snapshot at point)
- `DonGiaVatTu` DECIMAL — giá vốn hàng hóa (GVHB) tại thời điểm giao dịch
- `IdThamChieu` INT NULL — reference ID (VD: IdPhieuNhap, IdPhieuXuat, IdDonHang)
- `CreatedBy` INT NOT NULL — bắt buộc biết ai làm

**Index**: `IxTheKho_TruyVanNhanh ON TheKho(IdKho, IdSanPham, ThoiGianGiaoDich DESC)` — tra lịch sử nhanh.

**Thiết kế Eventual Consistency**: Trigger đồng bộ TonKho đã bị gỡ (comment SQL line 843). BUS layer tự cập nhật TonKho + ghi TheKho cùng lúc trong TransactionScope.

---

## 2. KIỂM KHO THEO CA

### LichKiemKho + ChiTietKiemKho

| Item | Detail |
|------|--------|
| **LichKiemKho** | Lịch kiểm: `Ca` CHECK (Sang/Trua/Chieu/Toi), `IdNhanVien`, `ThoiGianKiem` |
| **ChiTietKiemKho** | So sánh thực tế vs hệ thống: `SoLuongThucTe`, `SoLuongHeThong`, `ChenhLech` AS computed PERSISTED, `TrangThai` (OK/CanBoSung/ChenhLech) |
| **Dùng ở đâu** | `frmKiemKho` — thủ kho nhập số thực đếm, hệ thống tự tính chênh lệch |

---

## 3. LOYALTY — ĐIỂM TÍCH LŨY (Section 15 SQL)

### LichSuDiem

| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Sổ cái điểm loyalty — ghi mọi giao dịch tích/tiêu/hết hạn/điều chỉnh |
| **How** | `IdKhachHang` FK, `LoaiGiaoDich` CHECK (TichLuy/SuDung/HetHan/DieuChinh), `SoDiem` INT (dương/âm), `SoDuTruoc`, `SoDuSau`, `IdDonHang` FK NULL, `LyDo`, `CreatedBy` NOT NULL |
| **Immutable** | Không có IsDeleted — sổ cái không bao giờ xóa |
| **Xác minh** | `BUS_TichDiem.CongDiem` line 139-151 — tạo LichSuDiem mỗi lần cộng/trừ điểm |

### QuyTacDiem

| Item | Detail |
|------|--------|
| **What** | Quy tắc tích điểm theo hạng khách — cấu hình linh hoạt từ DB |
| **How** | `TenQuyTac`, `TongDonToiThieu`, `SoDiemThuong`, `LoaiKhachApDung` (NULL = tất cả), `TrangThai` BIT |
| **Seed** | 4 quy tắc: Thường (100k→1đ), Bạc (200k→3đ), Vàng (500k→8đ), Diamond (500k→15đ) |

---

## 4. BẢO TRÌ THIẾT BỊ TẬP TRUNG (Section 16 SQL)

### DanhSachThietBi

| Câu hỏi | Trả lời |
|---------|---------|
| **What** | Danh mục thiết bị TOÀN KHU: trò chơi, máy tạo sóng, xe điện, kiosk... |
| **Why** | Gộp quản lý thiết bị thay vì mỗi module 1 bảng riêng |
| **How** | `MaCode`, `TenThietBi`, `LoaiThietBi` CHECK 8 loại, `IdKhuVuc` FK, `NgayMua`, `GiaTriMua`, `ChuKyBaoTriThang`, `TrangThai` (HoatDong/BaoTri/TamDong/Hong/ThanhLy) |
| **Seed** | 14 thiết bị: 5 trò chơi, 3 máy sóng, 3 xe điện, 3 kiosk |

### LichBaoTri

| Item | Detail |
|------|--------|
| **What** | Lịch sử + kế hoạch bảo trì: `LoaiBaoTri` (DieuDo/SuaChua/ThayThe/ThanhLy), `ChiPhi`, `IdPhieuChi` FK |
| **TrangThai** | KeHoach → DangLam → HoanTat |
| **FK PhieuChi** | Link chi phí bảo trì → dòng tiền tài chính |

---

## 5. ĐÁNH GIÁ DỊCH VỤ (Section 17 SQL)

### DanhGiaDichVu

| Item | Detail |
|------|--------|
| **What** | Feedback khách: `LoaiDichVu` (ToanBo/Phong/NhaHang/TroChoi/VuonThu/TruongDua/Bien), `DiemSo` 1-5, `NhanXet` |
| **Workflow** | ChoXuLy → DaXuLy (nhân viên phản hồi `PhanHoiNhanVien`) → AnDi (ẩn review xấu/spam) |

---

## 6. LỊCH LÀM VIỆC + AN TOÀN (Sections 13, 18 SQL)

### LichLamViec

| Item | Detail |
|------|--------|
| **What** | Lịch ca: `CaLam` (Sang/Chieu/Toi/Full), `GioBatDau/KetThuc`, `TrangThai` (KeHoach/DaXacNhan/TreGio/VangMat/HoanThanh) |
| **Constraint** | UNIQUE(IdNhanVien, NgayLam, CaLam) — 1 NV không thể trùng ca, CHECK(GioKetThuc > GioBatDau) |

### SuCo + ThatLac

| Bảng | Vai trò |
|------|---------|
| **SuCo** | Sự cố: `MucDo` (Nhe/TrungBinh/NghiemTrong/KhanCap), `LoaiSuCo` (Thuong/DuoiNuoc/MatTre/DanhNhau/ThietBi/Khac), `ToaDoGps` |
| **ThatLac** | Đồ vật thất lạc: `TrangThai` (ChoNhan/DaTra/DaThanhLy) |

### AuditDonHang

| Item | Detail |
|------|--------|
| **What** | Trigger tự động ghi log mỗi khi DonHang thay đổi TrangThai |
| **How** | `IdDonHang`, `TrangThaiCu`, `TrangThaiMoi`, `ThoiGianThayDoi`, `NguoiThayDoi` |
| **Trigger** | `TrgAuditDonHang ON DonHang AFTER UPDATE` — chỉ fire khi `UPDATE(TrangThai)` |

---

## 7. VIEWS (Reporting Layer)

| View | Mục đích | Join từ |
|------|----------|---------|
| **V_CanhBaoTonKho** | Cảnh báo tồn kho: Hết hàng / Sắp hết / Đủ hàng | TonKho + KhoHang + SanPham |
| **V_DoanhThuTheoModule** | Doanh thu theo ngày/module (Giải trí/Lưu trú/Ăn uống/Cho thuê/Bãi xe) | CTDH + DonHang + SanPham |
| **V_CongSuatPhong** | Tỷ lệ lấp đầy phòng theo loại | Phong + LoaiPhong |
| **V_LichSuKhachHang** | Tổng chi tiêu + điểm + lần cuối đến của mỗi KH | KhachHang + DonHang + CTDH |
| **V_DashboardHomNay** | KPI dashboard: đơn hôm nay, doanh thu, phòng đã đặt, SP hết hàng | DonHang + CTDH + Phong + TonKho |
| **V_ChuaThanhToan** | Đơn chờ thanh toán + phút chờ | DonHang + KhachHang |

---

## 8. TRIGGERS & STORED PROCEDURES

| Object | Loại | Mục đích |
|--------|------|----------|
| `TrgAuditDonHang` | Trigger AFTER UPDATE | Ghi log mọi thay đổi TrangThai đơn hàng |
| `TrgComboChiTietTyLe100` | Trigger AFTER INSERT/UPDATE/DELETE | Kiểm tra tổng TyLePhanBo = 100% cho mỗi Combo. RAISERROR + ROLLBACK nếu vi phạm |
| `SpGetChiTietDonHangToanPhan` | Stored Procedure | Truy vấn toàn bộ chi tiết 1 đơn hàng: CTDH + Phòng + Thuê đồ + Xe + Nhà hàng + Vườn thú (7 resultsets) |

---

## 9. THỐNG KÊ TỔNG HỢP

### Tổng số bảng: **56 bảng** (không kể Views/SPs/Triggers)

| Nhóm | Số bảng | Bảng tiêu biểu |
|------|---------|-----------------|
| **Master Data** | 13 | VaiTro, NhanVien, SanPham, BangGia, Combo |
| **Giao dịch & Tài chính** | 10 | DonHang, CTDH, ViDienTu, TheRFID, GiaoDichVi, VeDienTu |
| **Thuê đồ** | 3 | ThueDoChiTiet, TuDo, ThueTu |
| **Khách sạn** | 4 | LoaiPhong, Phong, DatPhongChiTiet, ChiTietDatPhong |
| **Bãi xe** | 4 | LuotVaoRaBaiXe, GiaGuiXe, BaiDoXe, VeDoXeChiTiet |
| **Biển nhân tạo** | 7 | KhuVucBien, ThietBiTaoSong, LichTaoSong, ChatLuongNuoc, CaTrucCuuHo, ChoiNghiMat, ThueChoi |
| **Trường đua** | 9 | DuongDua, LoaiHinhDua, GiaiDua, LichThiDau, VanDongVien, NguaDua, PhuongTienDua, BaoTriPhuongTienDua, KetQuaDua, KhanDai, ViTriNgoi, VeKhanDai |
| **Vườn thú** | 4 | KhuVucThu, DongVat, ChuongTrai, LichChoAn, DatChoThuAn |
| **Nhà hàng** | 4 | NhaHang, BanAn, DatBan, ChiTietDatBan |
| **Kho hàng** | 8 | NhaCungCap, KhoHang, PhieuNhapKho, CTNK, PhieuXuatKho, CTXK, TonKho, TheKho |
| **Supporting** | 8+ | SuCo, ThatLac, LichLamViec, LichKiemKho, CTKK, DanhGiaDichVu, LichSuDiem, QuyTacDiem, DanhSachThietBi, LichBaoTri, AuditDonHang, Kiosk |

### Tổng số Constraints & Indexes

| Loại | Số lượng | Ví dụ |
|------|----------|-------|
| **CHECK constraints** | ~45 | TrangThai enums, SoLuong > 0, ChietKhau 0-100 |
| **UNIQUE constraints** | ~15 | MaCode, Email, (IdKho+IdSanPham), (IdNhaHang+MaBan) |
| **Foreign Keys** | ~85 | Tập trung ở cuối file (centralized FK section) |
| **Indexes** | ~35 | Covering indexes cho FK + filtered indexes cho IsDeleted |
| **Computed Columns** | 3 | ThanhTien (CTDH, DoanKhach_DichVu), ChenhLech (CTKK) |
| **RowVersion** | 5 bảng | ViDienTu, Phong, BanAn, ChoiNghiMat, TonKho |
| **Triggers** | 2 | TrgAuditDonHang, TrgComboChiTietTyLe100 |

### Seed Data Summary

| Loại | Số bản ghi |
|------|-----------|
| VaiTro | 5 (Admin, QuanLy, NhanVien, ThuKho, KeToan) |
| QuyenHan | 36 quyền (18 cặp VIEW/MANAGE) |
| KhuVuc | 11 khu + 6 khu biển + 10 khu thú = 27 bản ghi |
| SanPham | ~60 sản phẩm (vé, thuê, ăn uống, lưu trú, dịch vụ) |
| DongVat | 29 cá thể |
| Phòng | 18 phòng (8 Sup, 5 Dlx, 3 Fam, 2 Villa) |
| BanAn | 50 bàn (30 + 20) |
| TuDo | 50 tủ đồ |
| DanhSachThietBi | 14 thiết bị |
| TonKho (mock) | ~5 sản phẩm F&B × 500 đơn vị |
