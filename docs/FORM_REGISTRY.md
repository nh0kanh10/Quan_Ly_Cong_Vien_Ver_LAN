# 📋 FORM REGISTRY — DANH SÁCH FORM ĐÃ HOÀN CHỈNH
**Cập nhật lần cuối:** 2026-04-04 (Kiểm toán chuyên sâu 4 module chính)
**Mục đích:** AI đọc file này TRƯỚC khi đọc source code. Đã ghi chi tiết từng control, luồng nghiệp vụ, và đánh giá theo nhiều đối tượng.

---

## TỔNG QUAN NHANH

| # | Form | Module | Trạng thái | Đánh giá |
|:---:|:---|:---|:---:|:---:|
| 1 | frmLogin | Auth | 🟢 100% | ⭐5/5 | |
| 2 | frmBanHang | POS | 🟢 95% | ⭐5/5 | Tiền thừa preview tính theo tổng gốc (BUG-POS-01) |
| 3 | frmMenuPopup | POS | 🟢 100% | ⭐5/5 | |
| 4 | frmDonHang | Đơn hàng | 🟢 95% | ⭐4/5 |
| 5 | frmChiTietHoaDon | Đơn hàng | 🟢 95% | ⭐4/5 |
| 6 | frmKhachHang | Khách hàng | 🟢 95% | ⭐5/5 |
| 7 | frmNhanVien | Nhân viên | 🟢 100% | ⭐5/5 |
| 8 | frmSanPham | Sản phẩm | 🟢 95% | ⭐4/5 |
| 9 | frmDonViTinh | Đơn vị tính | 🟢 100% | ⭐5/5 |
| 10 | frmBangGia | Bảng giá | 🟢 100% | ⭐5/5 |
| 11 | frmCombo | Combo | 🟢 100% | ⭐5/5 |
| 12 | frmComboChiTiet | Combo | 🟢 100% | ⭐5/5 |
| 13 | frmKhuyenMai | Marketing | 🟢 100% | ⭐4/5 |
| 14 | frmMarketing | Marketing | 🟢 100% | ⭐4/5 |
| 15 | frmViDienTu | Ví RFID | 🟢 100% | ⭐5/5 |
| 16 | frmTheRFID | Ví RFID | 🟢 100% | ⭐5/5 |
| 17 | frmQuayNapTien | Ví RFID | 🟢 100% | ⭐5/5 |
| 18 | frmPhieuThuChi | Tài chính | 🟢 95% | ⭐4/5 |
| 19 | frmKiemSoatVe | Vé điện tử | 🟢 100% | ⭐5/5 |
| 20 | frmPhatVe | Vé điện tử | 🟢 100% | ⭐5/5 |
| 21 | frmAppDatVeMoPhong | Vé điện tử | 🟢 100% | ⭐4/5 |
| 22 | frmGuiXe | Bãi đỗ xe | 🟢 100% | ⭐5/5 |
| 23 | frmThueDo | Thuê đồ | 🟢 100% | ⭐5/5 |
| 24 | frmDatPhong | Khách sạn | 🟢 95% | ⭐5/5 | "Thêm dịch vụ" chưa implement |
| 25 | frmNhaHang | Nhà hàng | 🟢 100% | ⭐5/5 |
| 26 | frmDatBan | Nhà hàng | 🟡 85% | ⭐4/5 | Có Đặt trước+Ghép bàn, bug cọc TT 2 lần (BUG-REST-02) |
| 27 | frmBookingDialog | Khách sạn | 🟢 100% | ⭐4/5 | Dialog Check-in phòng |
| 28 | frmReserveDialog | Khách sạn | 🟢 100% | ⭐4/5 | Dialog Đặt trước phòng |
| 29 | frmKhoHang | Kho hàng | 🟡 90% | ⭐4/5 | Bug DB CHECK constraint TheKho (BUG-INV-01) |
| 30 | frmNhaCungCap | Kho hàng | 🟢 100% | ⭐5/5 |
| 31 | frmTaoPhieuKho | Kho hàng | 🟢 100% | ⭐5/5 |
| 32 | frmPhieuNhapXuat | Kho hàng | 🟢 100% | ⭐4/5 |
| 33 | frmKiemKho | Kho hàng | 🟢 100% | ⭐5/5 |
| 34 | frmBaoTri | Bảo trì | 🟢 100% | ⭐4/5 |
| 35 | frmThemBaoTri | Bảo trì | 🟢 100% | ⭐4/5 |
| 36 | frmLichLamViec | Lịch ca | 🟢 100% | ⭐5/5 |
| 37 | frmSuCo | Sự cố | 🟡 80% | ⭐3/5 |
| 38 | frmKhuVuc | Khu vực | 🟢 100% | ⭐5/5 |
| 39 | frmKhuVucBien | Biển | 🟢 90% | ⭐4/5 |
| 40 | frmChatLuongNuoc | Biển | 🟢 100% | ⭐5/5 |
| 41 | frmKhuVucThu | Vườn thú | 🟡 60% | ⭐3/5 |
| 42 | frmDongVat | Vườn thú | 🟡 70% | ⭐3/5 |
| 43 | frmTroChoi | Trò chơi | 🟢 100% | ⭐4/5 |
| 44 | frmVaiTro | RBAC | 🟢 100% | ⭐5/5 |
| 45 | frmPhanQuyen | RBAC | 🟢 100% | ⭐5/5 |
| 46 | frmDashboard | Dashboard | 🟡 50% | ⭐3/5 | Có filter date+4 cards+grid, thiếu chart |
| 47 | frmConfigConnect | Config | 🟢 100% | ⭐5/5 | |
| 48 | frmDatPhongDoanDialog | Khách sạn | 🟢 100% | ⭐5/5 | Group Booking — lõi chiết khấu+cọc+pricing |
| 49 | frmDatBanTruocDialog | Nhà hàng | 🟢 95% | ⭐4/5 | Reservation — cọc+SĐT+KhachHang lookup |
| 50 | frmGhepBanDialog | Nhà hàng | 🟢 100% | ⭐5/5 | Chọn bàn gốc khi merge bill |
| 51 | frmThanhToanHinhThuc | Nhà hàng | 🟢 100% | ⭐4/5 | Chọn phương thức TT (TienMat/CK/Vi) |
| 52 | frmTheKho | Kho hàng | 🟢 100% | ⭐4/5 | Embedded thẻ kho trong SplitContainer |

---

## CHI TIẾT TỪNG FORM

### ═══════════════════════════════════════
### 1. frmBanHang (POS — Bán Hàng)
**File:** `WindowsFormsApp1/frmBanHang.cs` (1020+ dòng)
**Bảng DB:** DonHang, ChiTietDonHang, PhieuThu, VeDienTu, TonKho, KhuyenMai, LichSuDiem, QuyTacDiem

#### Layout
- **SplitContainerControl** (Panel1: Catalog | Panel2: Cart)
- Panel1 (Trái): `gridSanPham` (TileView) + `txtTimKiem` + Category buttons (Tất cả, Vé, F&B, Thuê)
- Panel2 (Phải): `gbKhachHang` (Top) → `pnlCart` (Grid giỏ hàng) → `pnlSummary` (Tổng tiền + Quick Cash) → `pnlCheckout` (Thanh toán)

#### Controls chính
| Control | Loại | Chức năng |
|:---|:---|:---|
| txtScanner | Guna2TextBox | Quét barcode/nhập mã (Enter trigger) |
| txtTimKiem | Guna2TextBox | Tìm kiếm sản phẩm (real-time filter) |
| btnCatAll/Ve/Food/Rental | Guna2Button | Category filter tabs |
| gridSanPham + tileViewSanPham | DevExpress TileView | Catalog sản phẩm dạng card |
| gridGioHang + gridViewGioHang | DevExpress GridView | Giỏ hàng (editable SoLuong, SpinEdit, nút Xóa) |
| txtMaKH + btnTimKH | TextBox + Button | Quét mã KH / SĐT (F2) |
| lblTenKH | Label | Hiển thị tên + VIP% + điểm tích lũy |
| lblTongTienLarge | Label (32pt) | Tổng tiền lớn (Cascadia Code font) |
| lblTongTienTitle | Label | ← **MỚI**: Real-time discount preview |
| txtKhachDua | Guna2TextBox | Tiền khách đưa (F8) + auto-format N0 |
| lblTienThua | Guna2TextBox readonly | Tiền thừa / CÒN THIẾU |
| btn50K/100K/200K/500K/DuaDu | Quick Cash buttons | Nhanh chọn mệnh giá |
| btnThanhToanTienMat | Guna2Button (Green) | Thanh toán tiền mặt (F9/F12) |
| btnThanhToanVi | Guna2Button (Slate) | Thanh toán ví RFID (F10) |
| btnHuyDon | Guna2Button (Red) | Hủy đơn (ESC) |
| cboKhoXuLy | Guna2ComboBox (runtime) | Chọn kho xuất hàng |
| picCamera | PictureBox | Camera barcode scan |

#### Nghiệp vụ tích hợp
- **Loyalty Engine:** Tích điểm (CongDiem) + Tiêu điểm (TieuDiem) + Auto-upgrade VIP/VVIP
- **Chiết khấu:** MAX(VIP%, Event%, Điểm) — không stack. Đoàn/DN đọc từ DB DoanKhach
- **Khuyến mãi:** BUS_KhuyenMai.GetBestActivePromotion() auto-apply
- **Kho:** Kiểm tra TonKho trước khi thêm vào giỏ (sản phẩm vật lý)
- **Dynamic Pricing:** BUS_BangGia.GetDynamicPrice() theo thời điểm
- **Vé điện tử:** Auto-gen VeDienTu + mở frmPhatVe nếu đơn có vé
- **Camera Scan:** ZXing.Net barcode từ webcam hoặc file ảnh

#### Phím tắt
| Phím | Chức năng |
|:---|:---|
| F1 | Focus ô tìm kiếm |
| F2 | Focus ô mã KH |
| F8 | Focus ô tiền khách đưa |
| F12 | Thanh toán tiền mặt |
| ESC | Hủy đơn |
| Delete | Xóa dòng trong giỏ |
| `10*VEM001` | Thêm 10 sản phẩm VEM001 |

#### Đánh giá theo đối tượng
- 👤 **Thu ngân:** ⭐5/5 — Full POS workflow, phím tắt chuyên nghiệp
- 👤 **Quản lý:** ⭐5/5 — RBAC kiểm tra VIEW_POS/MANAGE_POS
- 👤 **Khách VIP:** ⭐5/5 — Hiển thị chiết khấu + điểm real-time
- 👤 **Kế toán:** ⭐4/5 — DonHang + PhieuThu tự động, thiếu report ở đây

---

### 2. frmKhachHang (Khách Hàng + Đoàn Khách)
**File:** `WindowsFormsApp1/frmKhachHang.cs` (410+ dòng)
**Bảng DB:** KhachHang, DoanKhach, LichSuDiem, QuyTacDiem

#### Layout
- **Guna2TabControl** (2 tabs: Khách Hàng | Đoàn Khách)
- Tab 1 — Khách Hàng:
  - Left: `gbDanhSach` (GridView KH) + `gbLichSuDiem` (GridView điểm — bottom 240px)
  - Right: `gbThongTin` (form nhập liệu) + `gbChucNang` (4 buttons)
- Tab 2 — Đoàn Khách:
  - Left: `gbDanhSachDoan` (GridView đoàn)
  - Right: `gbThongTinDoan` (form nhập) + `gbChucNangDoan` (4 buttons)

#### Controls chính — Tab KH
| Control | Loại | Chức năng |
|:---|:---|:---|
| gridControl + gridView | DevExpress GridView | DS Khách hàng (cột 🏆 Điểm vàng bold) |
| gridControlDiem + gridViewDiem | DevExpress GridView | **MỚI**: Lịch sử tích/tiêu điểm (xanh/đỏ row) |
| gbLichSuDiem | GroupBox | **MỚI**: Hiện "🏆 Lịch sử Điểm — Hiện có: X điểm (=Y đ)" |
| txtMaKH | Guna2TextBox readonly | Mã KH auto-gen |
| txtHoTen, txtSDT, txtEmail, txtDiaChi | Guna2TextBox | CRUD fields |
| cboGioiTinh | Guna2ComboBox | Nam/Nữ |
| cboLoaiKhach | Guna2ComboBox | CaNhan/Doan/DoanhNghiep — ẩn/hiện slkDoan |
| dtpNgaySinh | DevExpress DateEdit | Ngày sinh |
| slkDoan | DevExpress SearchLookUpEdit | Dropdown chọn đoàn (popup grid TenDoan, ChietKhau%, NguoiDaiDien) |

#### Controls chính — Tab Đoàn
| Control | Loại | Chức năng |
|:---|:---|:---|
| gridControlDoan + gridViewDoan | DevExpress GridView | DS Đoàn khách |
| txtTenDoan, txtMaSoThue, txtNguoiDaiDien, txtDienThoaiDoan | TextBox | CRUD |
| spnChietKhau | Guna2NumericUpDown | % chiết khấu (0-100) |

#### Nghiệp vụ
- CRUD KhachHang + DoanKhach đầy đủ (Thêm/Sửa/Xóa/Làm mới)
- Validation qua BUS_KhachHang.ValidateKhachHang()
- Khi chọn LoaiKhach = "Doan" → hiện slkDoan lookup
- Khi click 1 KH → load lịch sử điểm bên dưới (xanh = tích, đỏ = tiêu)
- DiemTichLuy hiển thị trên grid (cột vàng bold)
- RBAC: VIEW_CUSTOMER, MANAGE_CUSTOMER

#### Đánh giá theo đối tượng
- 👤 **Lễ tân:** ⭐5/5 — CRUD nhanh, lookup đoàn tiện
- 👤 **Quản lý:** ⭐5/5 — Xem được lịch sử điểm + chiết khấu đoàn
- 👤 **Kế toán:** ⭐4/5 — Thấy được điểm, chưa có export

---

### 3. frmGuiXe (Bãi Đỗ Xe — OCR + RFID)
**File:** `WindowsFormsApp1/frmGuiXe.cs`
**Bảng DB:** LuotVaoRaBaiXe, GiaGuiXe, BaiDoXe, VeDoXeChiTiet, DonHang

#### Features tích hợp
- Camera OCR biển số (Tesseract)
- Barcode/QR scan vé xe (ZXing.Net)
- Tính phí tự động (DATEDIFF giờ vào/ra × đơn giá)
- Sinh VeDoXeChiTiet gộp vào DonHang
- Trả xe flow: Quét vé → xác nhận biển số → tính phí → thu tiền

#### Đánh giá
- 👤 **Bảo vệ bãi xe:** ⭐5/5 — OCR + scan mượt, ít thao tác
- 👤 **Khách hàng:** ⭐5/5 — Nhanh, không xé vé giấy

---

### 4. frmDatPhong (Khách Sạn)
**File:** `WindowsFormsApp1/frmDatPhong.cs`
**Bảng DB:** Phong, LoaiPhong, DatPhongChiTiet, ChiTietDatPhong, BangGia

#### Features
- Sơ đồ phòng visual (Room Map — grid button động theo LoaiPhong)
- Booking flow: Chọn phòng → Nhập ngày → Check-in → Check-out
- RowVer OCC chống đặt trùng
- Giá dynamic theo BangGia engine (ngày thường/cuối tuần/lễ)
- Trạng thái phòng màu sắc: Trống (xanh), Đã đặt (vàng), Đang ở (đỏ), Bảo trì (xám)

#### Đánh giá
- 👤 **Lễ tân KS:** ⭐5/5 — Sơ đồ trực quan, đặt phòng nhanh
- 👤 **Khách hàng:** ⭐4/5 — Chưa có self-booking (Phase 2)

---

### 5. frmDatBan + frmNhaHang (Nhà Hàng)
**File:** `WindowsFormsApp1/frmDatBan.cs`, `frmNhaHang.cs`
**Bảng DB:** NhaHang, BanAn, DatBan, ChiTietDatBan

#### Features
- Sơ đồ bàn visual (Table Map)
- Booking dialog (frmBookingDialog) — chọn bàn, thời gian
- Walk-in + Đặt trước
- Order gọi món (frmReserveDialog)
- Gộp bill vào DonHang tổng

#### Đánh giá
- 👤 **Nhân viên phục vụ:** ⭐5/5 — Sơ đồ bàn rõ ràng
- 👤 **Khách VIP:** ⭐4/5 — Đặt trước OK, chưa có app mobile

---

### 6. frmThueDo (Cho Thuê Đồ)
**File:** `WindowsFormsApp1/frmThueDo.cs`
**Bảng DB:** ThueDoChiTiet, TuDo, ThueTu, ChoiNghiMat, ThueChoi, ViDienTu, GiaoDichVi

#### Features
- Thu cọc → Đóng băng ViDienTu.SoDuDongBang
- Trả đồ → Hoàn cọc hoặc Phạt (Partial Penalty)
- Quản lý tủ đồ locker + chòi nghỉ mát
- Full audit trail qua GiaoDichVi

#### Đánh giá
- 👤 **NV cho thuê:** ⭐5/5 — Luồng rõ ràng
- 👤 **Khách hàng:** ⭐5/5 — Cọc/hoàn tự động qua ví

---

### 7. frmKhoHang + frmTaoPhieuKho + frmPhieuNhapXuat + frmKiemKho (Kho hàng)
**Bảng DB:** KhoHang, NhaCungCap, PhieuNhapKho, PhieuXuatKho, ChiTietNhapKho, ChiTietXuatKho, TonKho, TheKho

#### Pipeline
```
frmNhaCungCap (CRUD NCC)
    → frmTaoPhieuKho (Tạo phiếu Nhập/Xuất/Chuyển kho)
        → frmPhieuNhapXuat (Xem lịch sử phiếu + thẻ kho)
            → frmKiemKho (So sánh tồn hệ thống vs thực tế)
                → frmKhoHang (Dashboard tồn kho)
```

#### Đánh giá
- 👤 **Thủ kho:** ⭐5/5 — Full pipeline, TheKho audit trail
- 👤 **Kế toán:** ⭐4/5 — PhieuChi auto khi nhập, thiếu báo cáo visual

---

### 8. frmViDienTu + frmTheRFID + frmQuayNapTien (Ví RFID)
**Bảng DB:** ViDienTu, TheRFID, GiaoDichVi, PhieuThu

#### Features
- Nạp tiền vào ví → GiaoDichVi + PhieuThu
- SoDuKhaDung / SoDuDongBang accounting
- HashSignature bảo mật (SHA256)
- RowVer OCC chống nạp đúp
- Cấp/đổi/khóa thẻ RFID

#### Đánh giá
- 👤 **Thu ngân:** ⭐5/5 — Nạp tiền nhanh
- 👤 **Khách hàng:** ⭐5/5 — Ví an toàn, hash bảo vệ
- 👤 **Kế toán:** ⭐4/5 — Xem giao dịch OK, chưa có tool kiểm toán hash

---

### 9. frmKiemSoatVe + frmPhatVe + frmAppDatVeMoPhong (Vé Điện Tử)
**Bảng DB:** VeDienTu, ChiTietDonHang

#### Features
- Gen VeDienTu với GUID duy nhất
- In QR/Barcode trên frmPhatVe
- Quét camera/file tại frmKiemSoatVe → check TrangThai → Da_Su_Dung
- frmAppDatVeMoPhong: Mô phỏng app khách mua vé (demo defense)

#### Đánh giá
- 👤 **Bảo vệ cổng:** ⭐5/5 — Quét nhanh, check instant
- 👤 **Khách hàng:** ⭐5/5 — QR code tiện lợi

---

### 10. frmVaiTro + frmPhanQuyen (RBAC)
**Bảng DB:** VaiTro, QuyenHan, PhanQuyen

#### Features
- CRUD vai trò (Admin, QuanLy, NhanVien, ThuKho, KeToan)
- 35+ MaQuyen seed (VIEW_POS, MANAGE_CUSTOMER, VIEW_REVENUE...)
- Dynamic permission check qua BUS_QuyenHan.HasPermission()
- Form1.cs gate child forms dựa trên quyền

---

### 11. frmLichLamViec (Kanban Lịch Ca)
**Bảng DB:** LichLamViec
- Kanban board drag-drop ca làm việc
- Phân ca theo ngày/tuần

---

## MODULE CHƯA CÓ GUI (Phase 2)

| Module | Bảng DB | BUS | DAL | Ghi chú |
|:---|:---|:---:|:---:|:---|
| **Trường đua** | 10 bảng (DuongDua, GiaiDua, LichThiDau, VanDongVien, NguaDua...) | ❌ | ✅ | Scope Phase 2 |
| **Lịch cho ăn (thú)** | LichChoAn | ✅ | ✅ | Có thể gộp vào frmDongVat |
| **Cho thú ăn (khách)** | DatChoThuAn | ❌ | ✅ | Scope Phase 2 |
| **Ca trực cứu hộ** | CaTrucCuuHo | ❌ | ✅ | Có thể gộp frmKhuVucBien |
| **Lịch tạo sóng** | LichTaoSong | ❌ | ✅ | Có thể gộp frmKhuVucBien |
| **Thất lạc** | ThatLac | ✅ | ✅ | Có thể gộp frmSuCo |
| **Đánh giá DV** | DanhGiaDichVu | ❌ | ❌ | Phase 2 |
| **Chuồng trại** | ChuongTrai | ✅ | ✅ | Có thể gộp frmKhuVucThu |
| **Dashboard báo cáo** | 5 Views SQL | ✅ | ✅ | Chờ hoàn thiện form xong mới làm |
| **Loyalty UI xem điểm** | LichSuDiem | ✅ | ✅ | ✅ ĐÃ THÊM vào frmKhachHang |

---

## ĐÁNH GIÁ TỔNG HỢP THEO ĐỐI TƯỢNG

### 👤 Khách hàng VIP (Đi chơi 1 ngày)
| Bước | Form | Kết quả |
|:---|:---|:---:|
| Mua vé + Nạp RFID | frmBanHang + frmQuayNapTien | ✅ |
| Quẹt vé qua cổng | frmKiemSoatVe | ✅ |
| Gửi xe ô tô | frmGuiXe (OCR) | ✅ |
| Thuê phao biển | frmThueDo | ✅ |
| Ăn nhà hàng | frmDatBan | ✅ |
| Đặt phòng KS | frmDatPhong | ✅ |
| Xem đua ngựa | ❌ | Phase 2 |
| Tích/dùng điểm | frmBanHang | ✅ |
| **Verdict:** ⭐4/5 |

### 👤 Nhân viên thu ngân
| Tác vụ | Form | Kết quả |
|:---|:---|:---:|
| Bán vé + combo | frmBanHang | ✅ |
| Áp khuyến mãi | frmBanHang (auto) | ✅ |
| Bán cho đoàn | frmBanHang (chiết khấu DB) | ✅ |
| Phát vé QR | frmPhatVe | ✅ |
| Xem lịch ca | frmLichLamViec | ✅ |
| **Verdict:** ⭐5/5 |

### 👤 Thủ kho
| Tác vụ | Form | Kết quả |
|:---|:---|:---:|
| Nhập hàng NCC | frmTaoPhieuKho | ✅ |
| Xuất/chuyển kho | frmTaoPhieuKho | ✅ |
| Kiểm kho | frmKiemKho | ✅ |
| Xem tồn | frmKhoHang | ✅ |
| **Verdict:** ⭐5/5 |

### 👤 Kế toán
| Tác vụ | Form | Kết quả |
|:---|:---|:---:|
| Phiếu Thu/Chi | frmPhieuThuChi | ✅ |
| Xem đơn hàng | frmDonHang | ✅ |
| Giao dịch ví | frmViDienTu | ✅ |
| Báo cáo doanh thu | frmDashboard | 🟡 Cơ bản |
| Đối soát hash | ❌ | Phase 2 |
| **Verdict:** ⭐3/5 |

### 👤 Quản lý trường đua
| Tác vụ | Form | Kết quả |
|:---|:---|:---:|
| Toàn bộ module | ❌ | Phase 2 |
| **Verdict:** ⭐0/5 |

---

## CHANGELOG
- **2026-04-04:** Kiểm toán chuyên sâu 4 module (POS/KS/NH/Kho). Phát hiện 4 bug (1 CRITICAL DB, 1 HIGH cọc NH, 1 MEDIUM tiền thừa POS, 1 LOW indent). Thêm 5 form mới chưa được ghi nhận (DatPhongDoan, DatBanTruoc, GhepBan, ThanhToanHinhThuc, TheKho). Điều chỉnh điểm đánh giá KhoHang 100%→90%, DatPhong 100%→95%, BanHang 100%→95%, DatBan 80%→85%, Dashboard 40%→50%.
- **2026-04-03:** Tạo file. Fix chiết khấu đoàn POS, thêm discount preview, thêm grid lịch sử điểm.
