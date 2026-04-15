# 📋 PRODUCT BACKLOG & SPRINT PLANNING v2.0
## Hệ Thống Quản Lý Khu Du Lịch Đại Nam — Phân Tích Dependency & Thứ Tự Triển Khai

**Mục đích:** Tài liệu này xác định thứ tự triển khai tối ưu dựa trên **Dependency Graph** — form nào cần form nào trước để test tích hợp hoàn chỉnh.

**Nguyên tắc chia Sprint:**
- 5 Sprint × 2 tuần/Sprint
- Chỉ tính **chức năng chính** (không tính Login, Config, UI Shell)
- Mỗi chức năng = 1 nhóm form liên quan. Form nào đã tích hợp bên trong = gộp chung.
- Sprint sau **phải test tích hợp được** với Sprint trước

---

## 1. THỰC TẾ TÍCH HỢP FORM — GỘP ĐÚNG THỰC TẾ CODE

> [!IMPORTANT]
> Trước khi chia Sprint, cần hiểu chính xác form nào **đã tích hợp sẵn** tính năng nào bên trong,
> để tránh tách thành 2 chức năng riêng biệt trong khi thực tế chúng nằm chung 1 form.

| Form Chính | Tích hợp sẵn bên trong | Ghi chú |
|:-----------|:-----------------------|:--------|
| `frmSanPham` | Tab Quy Đổi ĐVT + Tab Bảng Giá Flat Matrix + popup `frmCauHinhNgayLe` + popup `frmDonViTinh` | → **Sản Phẩm = 1 chức năng** (gồm luôn DVT, Bảng giá, Ngày lễ) |
| `frmKhachHang` | KPI Ví (số dư, thẻ RFID) + Tab Giao Dịch Ví + Tab Điểm Loyalty + Tab Sự Cố + nút Nạp Tiền (popup `frmQuayNapTien`) + nút Khóa/Mở Thẻ + nút Cấp Ví + nút Điều Chỉnh Điểm | → **Khách Hàng = 1 chức năng** (gồm luôn Ví RFID, Loyalty, Sự cố) |
| `frmKhoHang` | Embedded Thẻ Kho (split panel bên phải) + nút F2 mở `frmTaoPhieuKho` Nhập + nút F3 Xuất + nút F4 `frmKiemKho` + dropdown NCC (`frmNhaCungCap`) + `frmPhieuNhapXuat` | → **Kho Hàng = 1 chức năng** (gồm luôn Thẻ kho, Kiểm kho, NCC, Phiếu Nhập/Xuất) |
| `frmBanHang` | Chọn KH + Áp giá động + Áp KM + Dùng điểm + Thanh toán 3 hình thức + Trừ kho + Sinh đơn hàng + auto popup `frmPhatVe` khi bán vé | → **POS = 1 chức năng** (DEPEND vào: SP, Kho, KH, KM) |
| `frmDoanKhach` | Tạo booking + Chọn dịch vụ đoàn (`frmChonDichVuDoanDialog`) + popup `frmQuayVe_LeTan` + popup `frmXuatVeDoan` | → **Đoàn Khách = 1 chức năng** |
| `frmDatPhong` | Sơ đồ phòng + `frmBookingDialog` + `frmReserveDialog` + `frmDatPhongDoanDialog` | → **Khách Sạn = 1 chức năng** |
| `frmNhaHang` + `frmDatBan` | Layout bàn + `frmDatBanTruocDialog` + `frmGhepBanDialog` + `frmThanhToanHinhThuc` | → **Nhà Hàng = 1 chức năng** |
| `frmCombo` | Master-Detail: Combo cha → Thành phần con (chọn SP) | → Gộp vào **Sản Phẩm** (cùng domain Catalog) |

---

## 2. DEPENDENCY GRAPH — SƠ ĐỒ PHỤ THUỘC (THEO FORM THỰC TẾ)

```
                    ┌──────────────────────────────────────┐
                    │        LAYER 0: NỀN TẢNG             │
                    │  Database · Architecture · Auth       │
                    │  (Không tính là chức năng)            │
                    └──────────────┬───────────────────────┘
                                   │
                    ┌──────────────▼───────────────────────┐
                    │     LAYER 1: MASTER DATA (Sprint 1)  │
                    │                                      │
                    │  ① Khu Vực & Trò Chơi               │
                    │  ② Sản Phẩm (gồm DVT, Combo,        │
                    │     Bảng Giá, Cấu Hình Ngày Lễ)     │
                    │  ③ Nhân Viên & Lịch Ca               │
                    │  ④ Phân Quyền RBAC                   │
                    └──────┬──────────┬────────────────────┘
                           │          │
            ┌──────────────▼──┐  ┌────▼─────────────────────┐
            │  LAYER 2a       │  │  LAYER 2b                │
            │  (Sprint 2)     │  │  (Sprint 2)              │
            │                 │  │                           │
            │ ⑤ Kho Hàng     │  │ ⑦ Khách Hàng             │
            │   (gồm NCC,    │  │   (gồm Ví RFID, Nạp Tiền │
            │    Nhập, Xuất,  │  │    Loyalty, Thẻ RFID,    │
            │    Kiểm, Thẻ   │  │    Sự Cố - tất cả trong  │
            │    Kho embed)   │  │    1 form frmKhachHang)  │
            │                 │  │                           │
            │ ⑥ Đơn Hàng &   │  │ ⑧ Khuyến Mãi & Marketing│
            │   Phiếu Thu/Chi │  │                           │
            └────────┬────────┘  └────────┬─────────────────┘
                     │                    │
                     └────────┬───────────┘
                              │
                    ┌─────────▼──────────────────────────────┐
                    │   LAYER 3: BÁN HÀNG & DỊCH VỤ (Spr.3) │
                    │  ⑨ POS Bán Hàng (gồm auto Phát Vé)    │
                    │  ⑩ Kiểm Soát Vé (Cổng quét QR)        │
                    │  ⑪ Đoàn Khách & Quầy Vé Lễ Tân        │
                    │  ⑫ Thuê Đồ (Phao, Tủ, Chòi + Cọc Ví) │
                    └─────────┬──────────────────────────────┘
                              │
                    ┌─────────▼──────────────────────────────┐
                    │   LAYER 4: DỊCH VỤ LƯU TRÚ (Spr.4)   │
                    │  ⑬ Khách Sạn & Đặt Phòng              │
                    │  ⑭ Nhà Hàng & Đặt Bàn                 │
                    │  ⑮ Gửi Xe (OCR + Barcode)              │
                    └─────────┬──────────────────────────────┘
                              │
                    ┌─────────▼──────────────────────────────┐
                    │   LAYER 5: HOÀN THIỆN (Sprint 5)       │
                    │  ⑯ Vườn Thú & Động Vật                 │
                    │  ⑰ Khu Vực Biển & Chất Lượng Nước      │
                    │  ⑱ Bảo Trì Thiết Bị                    │
                    │  ⑲ Dashboard Báo Cáo                   │
                    │  ⑳ AI Chatbox + Web B2C                │
                    └────────────────────────────────────────┘
```

---

## 3. PRODUCT BACKLOG — 20 CHỨC NĂNG CHÍNH

| # | Chức Năng | Form(s) Thực Tế | Gộp Sẵn Bên Trong | Phụ Thuộc | Ưu Tiên | Sprint |
|:-:|:----------|:-----------------|:-------------------|:----------|:-------:|:------:|
| ① | Khu Vực & Trò Chơi | `frmKhuVuc` · `frmTroChoi` | — | — | 🔴 | S1 |
| ② | Sản Phẩm (Full) | `frmSanPham` · `frmDonViTinh` · `frmCombo` | Tab Quy Đổi ĐVT · Tab Bảng Giá · popup Ngày Lễ | — | 🔴 | S1 |
| ③ | Nhân Viên & Lịch Ca | `frmNhanVien` · `frmLichLamViec` | — | — | 🔴 | S1 |
| ④ | Phân Quyền RBAC | `frmVaiTro` · `frmPhanQuyen` | — | ③ | 🔴 | S1 |
| ⑤ | Kho Hàng (Full) | `frmKhoHang` | Embedded Thẻ Kho · popup NCC · popup Nhập/Xuất · popup Kiểm Kho · Lịch sử Phiếu | ②④ | 🔴 | S2 |
| ⑥ | Đơn Hàng & Phiếu Thu/Chi | `frmDonHang` · `frmChiTietHoaDon` · `frmPhieuThuChi` | — | ②③ | 🟠 | S2 |
| ⑦ | Khách Hàng (Full) | `frmKhachHang` | KPI Ví · Nạp Tiền · Khóa/Mở Thẻ · Cấp Ví · Tab Điểm · Tab Giao Dịch · Tab Sự Cố · Điều Chỉnh Điểm | ③ | 🔴 | S2 |
| ⑧ | Khuyến Mãi & Marketing | `frmKhuyenMai` · `frmMarketing` | — | ② | 🟡 | S2 |
| ⑨ | **POS Bán Hàng** ⭐ | `frmBanHang` · `frmMenuPopup` | Auto `frmPhatVe` khi có vé · Chọn KH · Áp KM · Trừ kho · Sinh đơn hàng | ②⑤⑦⑧ | 🔴 | S3 |
| ⑩ | Kiểm Soát Vé (Cổng) | `frmKiemSoatVe` | Quét QR vé → kiểm hạn/lượt → check-in | ⑨ | 🔴 | S3 |
| ⑪ | Đoàn Khách & Quầy Vé Lễ Tân | `frmDoanKhach` · `frmQuayVe_LeTan` · `frmXuatVeDoan` | popup Chọn Dịch Vụ Đoàn | ⑦⑨ | 🟠 | S3 |
| ⑫ | Thuê Đồ | `frmThueDo` | Cọc ví tích hợp · Hẹn giờ trả · Hoàn cọc · Phạt | ⑦ | 🟡 | S3 |
| ⑬ | Khách Sạn & Đặt Phòng | `frmDatPhong` · 3 dialog | — | ⑦⑪ | 🟠 | S4 |
| ⑭ | Nhà Hàng & Đặt Bàn | `frmNhaHang` · `frmDatBan` · 3 dialog | — | ⑦⑤ | 🟠 | S4 |
| ⑮ | Gửi Xe | `frmGuiXe` | OCR + Barcode tích hợp | ⑦ | 🟡 | S4 |
| ⑯ | Vườn Thú & Động Vật | `frmDongVat` · `frmKhuVucThu` | — | ① | 🟡 | S5 |
| ⑰ | Khu Vực Biển & Chất Lượng Nước | `frmKhuVucBien` · `frmChatLuongNuoc` | — | ① | 🟡 | S5 |
| ⑱ | Bảo Trì Thiết Bị | `frmBaoTri` · `frmThemBaoTri` | — | ①③ | 🟡 | S5 |
| ⑲ | Dashboard Báo Cáo | `frmDashboard` | — | ⑤⑥⑨ | 🟡 | S5 |
| ⑳ | AI Chatbox + Web B2C | `AIChatPanel` · `DaiNamWeb.*` | AI điều khiển UI · API + Blazor WASM | ⑦⑨ | 🟢 | S5 |

---

## 4. SPRINT PLANNING CHI TIẾT

---

### 🚀 SPRINT 1: NỀN TẢNG & DANH MỤC (Tuần 1–2)

**Sprint Goal:** Hoàn thành toàn bộ Master Data — tạo nền tảng dữ liệu để các Sprint sau build tính năng nghiệp vụ lên trên.

**Tại sao Sprint 1 phải làm những cái này?**
> Không có Sản Phẩm → không thể test Kho, POS.
> Không có Nhân Viên → không ghi nhận "ai tạo" giao dịch.
> Không có Bảng Giá (nằm trong frmSanPham Tab 3) → POS không biết tính tiền.
> Không có RBAC → không phân ai được mở form nào.

| # | Chức Năng | Mô Tả Thực Tế | Story Pts |
|:-:|:----------|:---------------|:---------:|
| ① | **Khu Vực & Trò Chơi** | CRUD khu vực (Biển, Zoo, KS...) + danh sách trò chơi gắn theo khu. Cơ sở để gán sản phẩm/nhân viên theo khu vực. | 5 |
| ② | **Sản Phẩm (Full)** | `frmSanPham` 3 Tab: Tab 1 = Thông tin SP (loại, giá, hình, khu vực). Tab 2 = Quy Đổi ĐVT (VD: 1 Thùng = 24 Lon, giá bán riêng). Tab 3 = Bảng Giá Flat (Thường/Cuối tuần/Lễ/Cọc/Block). Popup `frmCauHinhNgayLe`. Popup `frmDonViTinh`. Form `frmCombo` (Combo cha-con). | 10 |
| ③ | **Nhân Viên & Lịch Ca** | CRUD nhân viên + hình ảnh + chức vụ. `frmLichLamViec`: phân ca theo tuần, kiểm tra trùng ca. Dữ liệu NV là `CreatedBy` cho mọi giao dịch về sau. | 5 |
| ④ | **Phân Quyền RBAC** | `frmVaiTro` + `frmPhanQuyen`: 5 vai trò × 36 quyền. Gate động: ẩn/hiện nút theo quyền khi `ApplyPermissions()` chạy trên mọi form. | 3 |
| | **Tổng Sprint 1** | | **23 SP** |

**Tiêu chí hoàn thành (DoD) Sprint 1:**
- [x] Tạo ≥ 5 SP có đa DVT (ít nhất 1 SP có 3 DVT: Lon/Lốc/Thùng)
- [x] Tab Bảng Giá trong frmSanPham: cùng 1 vé → giá Thường ≠ giá Lễ
- [x] Tạo ≥ 3 Combo (Combo 2+1, GĐ, VIP)
- [x] frmPhanQuyen: ThuNgan chỉ thấy POS, Admin thấy hết
- [x] frmLichLamViec: NV phân ca, kiểm trùng lịch thành công

**Test tích hợp nội Sprint 1:**
| Test | Mô Tả |
|:-----|:-------|
| S1-IT-01 | Tạo SP → Tab 2 Quy Đổi → xác nhận Combo (frmCombo) hiển thị đúng thành phần |
| S1-IT-02 | Tạo NV → gán Vai trò ThuNgan (frmVaiTro) → đăng nhập → xác nhận chỉ thấy menu POS |
| S1-IT-03 | Cấu hình Ngày Lễ (popup) → Tab 3 Bảng Giá tự chuyển giá Lễ đúng ngày |

---

### 🚀 SPRINT 2: KHO HÀNG & KHÁCH HÀNG (Tuần 3–4)

**Sprint Goal:** Hoàn thành chuỗi cung ứng (Kho) và hệ thống CRM (Khách hàng + Ví + Loyalty — tất cả trong 1 form `frmKhachHang`). Đây là 2 trụ cột mà POS ở Sprint 3 sẽ "đứng lên".

**Tại sao Sprint 2 phải làm Kho + KH TRƯỚC POS?**
> POS bán hàng → cần biết **TỒN KHO** bao nhiêu để chặn bán âm → Kho phải có.
> POS thanh toán → cần **KHÁCH HÀNG** để tích điểm + dùng Ví RFID → KH phải có.
> POS áp khuyến mãi → cần **KM** đang active → KM phải có.
> Nếu làm POS trước → không thể test trừ kho, không thể test loyalty.
> → **Kho & KH phải có trước POS ít nhất 1 Sprint.**

| # | Chức Năng | Mô Tả Thực Tế | Story Pts |
|:-:|:----------|:---------------|:---------:|
| ⑤ | **Kho Hàng (Full)** | `frmKhoHang` = Tổng chỉ huy: Dashboard (Tổng vốn, Sắp hết, Hết hàng) + Lưới tồn kho + Thẻ Kho embedded (split panel phải, hiện khi click SP) + **popup F2** `frmTaoPhieuKho` Nhập + **popup F3** Xuất + **popup F4** `frmKiemKho` (Blind Mode, Chênh Lệch) + **F5** Đồng bộ Ledger + **dropdown NCC** `frmNhaCungCap` + `frmPhieuNhapXuat` lịch sử. | 8 |
| ⑥ | **Đơn Hàng & Phiếu Thu/Chi** | `frmDonHang` + `frmChiTietHoaDon`: Lịch sử đơn hàng. `frmPhieuThuChi`: Phiếu thu (nhận tiền) / phiếu chi (trả lại). Tách riêng vì form Đơn Hàng là **consumer** — POS Sprint 3 sinh dữ liệu, form này chỉ đọc. | 3 |
| ⑦ | **Khách Hàng (Full)** | `frmKhachHang` = Trung tâm CRM: Lưới danh sách → Click KH → Panel chi tiết: Header (tên, hạng, badge VIP) + **KPI Strip** (Số dư ví · Điểm tích lũy · Tổng chi tiêu) + **Tab Giao dịch** (gộp ví + đơn hàng) + **Tab Điểm** (lịch sử tích/tiêu/điều chỉnh) + **Tab Sự cố** + **Nút Nạp Tiền** (popup `frmQuayNapTien`) + **Nút Khóa/Mở Thẻ RFID** + **Nút Cấp Ví** + **Nút Điều Chỉnh Điểm** (popup dialog). Popup `frmSuaKhachHang` + `frmThemKhachNhanh`. | 8 |
| ⑧ | **Khuyến Mãi & Marketing** | `frmKhuyenMai`: CRUD chương trình KM (% hoặc số tiền, ngày hiệu lực). `frmMarketing`: chiến dịch marketing. KM sẽ được POS (Sprint 3) auto-apply khi thanh toán. | 3 |
| | **Tổng Sprint 2** | | **22 SP** |

**Tiêu chí hoàn thành (DoD) Sprint 2:**
- [x] Nhập kho ≥ 50 đơn vị sản phẩm → Thẻ kho ghi đúng dòng Nhập
- [x] Kiểm kho: nhập tồn thực tế khác hệ thống → chênh lệch hiện đúng màu
- [x] Tạo ≥ 3 khách hàng (1 VIP, 1 VVIP, 1 Thường) có đầy đủ điểm/chi tiêu
- [x] Nạp tiền ví RFID → số dư cập nhật → lịch sử giao dịch ví đúng
- [x] Tạo ≥ 1 chương trình KM đang active

**Test tích hợp Sprint 2 + hồi quy Sprint 1:**
| Test | Mô Tả |
|:-----|:-------|
| S2-IT-01 | Sản Phẩm (S1) → Nhập Kho → xác nhận Tồn kho + Thẻ kho ghi dòng Nhập |
| S2-IT-02 | Nhập Kho dạng Thùng (24 lon) → Tồn kho hiển thị đúng đơn vị cơ bản (lon) |
| S2-IT-03 | Tạo KH VIP → Nạp Ví RFID → Tab Giao dịch KH hiện dòng "Nạp tiền" |
| S2-IT-04 | Tạo KM giảm 10% cho VIP → KM hiện trong danh sách active |
| S2-IT-05 | Phân quyền (S1): ThuNgan KHÔNG được mở form Kho Hàng |
| S2-IT-06 | Nhân viên (S1) → xuất hiện trong dropdown "Người lập" của Phiếu nhập kho |

---

### 🚀 SPRINT 3: POS BÁN HÀNG & DỊCH VỤ (Tuần 5–6)

**Sprint Goal:** Bán hàng end-to-end — từ quét mã → thêm giỏ → thanh toán → phát vé → trừ kho → tích điểm → ghi đơn hàng. Bổ sung Thuê đồ (cọc ví) và Kiểm soát vé cổng. Đây là **Sprint quan trọng nhất**, nơi mọi thứ ở Sprint 1+2 hội tụ.

**Tại sao bây giờ mới làm POS?**
> Sprint 1 cho ta: Sản phẩm, Bảng giá, Đơn vị tính, RBAC.
> Sprint 2 cho ta: Kho (tồn kho), Khách hàng (gồm Ví RFID + Loyalty), Khuyến mãi.
> → POS là **consumer** của TẤT CẢ. Nếu làm POS mà chưa có Kho, KH → phải mock hết, không test tích hợp thực được.

> [!IMPORTANT]
> **Sprint 3 là Sprint nặng nhất** vì POS là nexus của toàn hệ thống.
> Team nên dành 60% effort cho POS (⑨) và 40% cho ⑩⑪⑫.

| # | Chức Năng | Mô Tả Thực Tế | Story Pts |
|:-:|:----------|:---------------|:---------:|
| ⑨ | **POS Bán Hàng** ⭐ | Quét mã/Click → Thêm giỏ → Chọn KH → Áp KM → Dùng điểm → Thanh toán (Tiền mặt/Ví RFID/CK) → Trừ kho (quy đổi DVT) → Tích điểm → Sinh đơn hàng. Bán vé → Auto popup `frmPhatVe` sinh vé QR. Hỗ trợ đa đơn vị tính, camera quét mã. | 13 |
| ⑩ | **Kiểm Soát Vé (Cổng)** | `frmKiemSoatVe`: Nhân viên cổng quét QR vé → Hệ thống kiểm tra hạn sử dụng + số lượt còn lại → Check-in thành công hoặc từ chối. Ghi lịch sử quét vé. | 3 |
| ⑪ | **Đoàn Khách & Quầy Vé Lễ Tân** | Tạo booking đoàn (BK-xxx) → Gán quota ăn uống → Quầy vé lễ tân xuất vé đoàn → POS quét BK-xxx → Phát hàng giá 0đ theo quota. | 5 |
| ⑫ | **Thuê Đồ** (Phao, Tủ, Chòi) | `frmThueDo`: Chọn loại đồ thuê → Đặt cọc (trừ ví RFID) → Hẹn giờ trả → Trả đồ → Hoàn cọc. Phạt nếu hư/mất. Dependency: cần Ví RFID từ frmKhachHang (S2:⑦). | 5 |
| | **Tổng Sprint 3** | | **26 SP** |

**Tiêu chí hoàn thành (DoD) Sprint 3:**
- [x] Bán hàng POS → Kho trừ đúng (kể cả quy đổi Thùng → Lon)
- [x] Bán hàng POS → Khách hàng tích đúng điểm theo hệ số VIP/VVIP
- [x] Bán vé → Form phát vé tự mở → Vé QR sinh → Cổng kiểm soát quét được
- [x] Thanh toán Ví RFID → Số dư ví giảm đúng
- [x] Bán hết tồn kho → POS chặn mua tiếp (không cho bán âm)
- [x] Thuê phao → Cọc trừ ví → Trả → Hoàn cọc → Ví tăng lại

**Test tích hợp Sprint 3 — CROSS-FORM (quan trọng nhất):**
| Test | Mô Tả | Dependency |
|:-----|:-------|:-----------|
| S3-IT-01 | POS bán 1 Thùng nước → qua frmKhoHang xem tồn giảm 24 lon | S2:⑤ |
| S3-IT-02 | POS bán cho KH VIP → qua frmKhachHang xem điểm tích ×2 | S2:⑦ |
| S3-IT-03 | POS thanh toán Ví RFID → qua frmKhachHang xem số dư ví giảm | S2:⑦ |
| S3-IT-04 | POS bán đơn lớn → KM sự kiện áp 15% → đúng số tiền | S2:⑧ |
| S3-IT-05 | POS bán vé → frmPhatVe mở → quét QR ở frmKiemSoatVe → check-in OK | S3:⑩ |
| S3-IT-06 | POS bán xong → qua frmDonHang xem trạng thái "Đã thanh toán" | S2:⑥ |
| S3-IT-07 | POS bán xong → qua Thẻ Kho (embedded frmKhoHang) xem dòng Xuất | S2:⑤ |
| S3-IT-08 | POS bán hết tồn (0) → mua tiếp → bị chặn, thông báo hết hàng | S2:⑤ |
| S3-IT-09 | Quét BK-xxx đoàn → POS phát hàng 0đ theo quota | S3:⑪ |
| S3-IT-10 | Thuê phao → Cọc trừ ví → Trả đồ → Hoàn cọc → Ví tăng lại | S2:⑦ |
| S3-IT-11 | Thuê phao mất → Phạt → Ví bị trừ thêm tiền phạt | S2:⑦ |

---

### 🚀 SPRINT 4: DỊCH VỤ LƯU TRÚ ĐẠI NAM (Tuần 7–8)

**Sprint Goal:** Mở rộng sang các dịch vụ lưu trú lớn của KDL Đại Nam — Khách sạn (200+ phòng), Nhà hàng (đặt bàn + gọi món), Gửi xe (OCR). Các module này nặng về UI và nghiệp vụ riêng.

**Tại sao đặt sau POS?**
> Khách sạn cần: Khách hàng (S2) + Đoàn khách (S3) cho đặt phòng đoàn.
> Nhà hàng cần: Kho hàng (S2) để trừ nguyên vật liệu khi gọi món.
> Gửi xe cần: Khách hàng (S2) để gắn biển số + thanh toán ví.
> → Tất cả đều cần S1+S2, và nên để sau POS để team focus POS trước.

| # | Chức Năng | Mô Tả Thực Tế | Story Pts |
|:-:|:----------|:---------------|:---------:|
| ⑬ | **Khách Sạn & Đặt Phòng** | `frmDatPhong`: Sơ đồ 200+ phòng (Visual Layout). Check-in / Check-out / Reserve / Cancel. `frmBookingDialog`: đặt phòng đơn. `frmReserveDialog`: giữ phòng. `frmDatPhongDoanDialog`: đặt phòng đoàn hàng loạt. Thanh toán ví/tiền mặt. | 8 |
| ⑭ | **Nhà Hàng & Đặt Bàn** | `frmNhaHang`: Layout bàn ăn real-time (Trống/Đang dùng/Đặt trước). `frmDatBan`: Đặt bàn trước / Walk-in / Ghép bàn. `frmDatBanTruocDialog` + `frmGhepBanDialog`. Gọi món + Thanh toán nhiều hình thức (`frmThanhToanHinhThuc`). | 8 |
| ⑮ | **Gửi Xe** (OCR + Barcode) | `frmGuiXe`: Xe vào → Camera chụp biển số → OCR đọc tự động → In vé barcode → Xe ra → Quét barcode → Tính phí theo thời gian. Hỗ trợ KH có thẻ RFID thanh toán ví. | 5 |
| | **Tổng Sprint 4** | | **21 SP** |

**Test tích hợp Sprint 4:**
| Test | Mô Tả | Dependency |
|:-----|:-------|:-----------|
| S4-IT-01 | Đặt phòng KH VIP → Check-out → Thanh toán Ví RFID → Ví giảm đúng | S2:⑦ |
| S4-IT-02 | Đặt phòng Đoàn → KH thuộc Đoàn đã tạo ở S3 → phòng gán đúng nhóm | S3:⑪ |
| S4-IT-03 | Đặt bàn nhà hàng → Gọi món F&B → Kho trừ nguyên liệu | S2:⑤ |
| S4-IT-04 | Nhà hàng thanh toán → Tích điểm KH (giống POS) | S2:⑦ |
| S4-IT-05 | Gửi xe → KH có thẻ RFID → OCR đọc biển số → Xe ra → Thanh toán ví | S2:⑦ |
| S4-IT-06 | Phân quyền (S1): NhanVien KHÔNG được mở form Đặt phòng Đoàn | S1:④ |

---

### 🚀 SPRINT 5: HOÀN THIỆN & MỞ RỘNG (Tuần 9–10)

**Sprint Goal:** Hoàn thiện các module phụ trợ (Vườn thú, Biển, Bảo trì), xây Dashboard báo cáo tổng hợp, tích hợp AI và triển khai Web B2C. Sprint cuối = polish + delivery.

**Tại sao để cuối?**
> Vườn thú, Biển, Bảo trì = module **hiển thị dữ liệu**, ít giao dịch tài chính.
> Dashboard = cần **đủ dữ liệu từ S1→S4** mới có gì để hiển thị.
> AI Chatbox = tính năng nâng cao, phải có KH + POS sẵn mới có context.
> Web B2C = kênh bán thứ hai, cần toàn bộ backend stable trước.

| # | Chức Năng | Mô Tả Thực Tế | Story Pts |
|:-:|:----------|:---------------|:---------:|
| ⑯ | **Vườn Thú & Động Vật** | `frmDongVat` + `frmKhuVucThu`: Danh sách 100+ loài (theo khu vực thú). Thông tin sức khoẻ, chế độ ăn, ngày về chuồng. Quản lý khu vực chuồng trại. | 3 |
| ⑰ | **Khu Vực Biển & Chất Lượng Nước** | `frmKhuVucBien` + `frmChatLuongNuoc`: Dashboard biển nhân tạo. Lịch đo chất lượng nước (pH, Chlor, nhiệt độ). Cảnh báo chỉ số bất thường (đỏ/vàng/xanh). | 3 |
| ⑱ | **Bảo Trì Thiết Bị** | `frmBaoTri` + `frmThemBaoTri`: Lịch bảo trì định kỳ trò chơi cơ giới. Ghi nhận chi phí sửa chữa. Cảnh báo quá hạn bảo trì. | 2 |
| ⑲ | **Dashboard Báo Cáo** | `frmDashboard`: 4 KPI cards (Doanh thu, Đơn hàng, Tồn kho, Khách mới). Filter theo khoảng ngày. Grid chi tiết drill-down. Cần dữ liệu đủ từ S1→S4 mới có ý nghĩa. | 3 |
| ⑳ | **AI Chatbox + Web B2C** | `AIChatPanel`: Chat tiếng Việt + AI điều khiển UI (tìm KH, mở form nạp tiền). `DaiNamWeb.Api` + `DaiNamWeb.Client`: API RESTful (Kestrel C#) + Website đặt vé online (Blazor WASM). Giỏ hàng → Thanh toán → Sinh vé QR. Server-side price validation chống hack. | 8 |
| | **Tổng Sprint 5** | | **19 SP** |

**Test tích hợp Sprint 5:**
| Test | Mô Tả | Dependency |
|:-----|:-------|:-----------|
| S5-IT-01 | Bảo trì trò chơi thuộc Khu vực Zoo → hiện trong lịch đúng khu | S1:① |
| S5-IT-02 | Động vật gắn khu vực Thú → frmKhuVucThu hiển thị danh sách | S1:① |
| S5-IT-03 | Chất lượng nước vượt ngưỡng → cảnh báo đỏ trên dashboard biển | S1:① |
| S5-IT-04 | Dashboard: Doanh thu ngày = tổng đơn hàng POS + Nhà hàng + KS | S3:⑨ S4:⑬⑭ |
| S5-IT-05 | Dashboard: Tồn kho = khớp với frmKhoHang | S2:⑤ |
| S5-IT-06 | AI Chat: "Tìm khách hàng Nguyễn Văn A" → Grid KH tự chọn đúng | S2:⑦ |
| S5-IT-07 | Web B2C: Đặt vé online → API tạo đơn → WinForms hiện vé mới | S3:⑨ |
| S5-IT-08 | Web B2C: Hack giá 0đ → Server reject → Giá tính lại server-side | S3:⑨ |

---

## 5. TỔNG KẾT — MA TRẬN DEPENDENCY SPRINT

Bảng dưới cho thấy **mỗi Sprint test tích hợp với Sprint nào**:

| | Sprint 1 | Sprint 2 | Sprint 3 | Sprint 4 | Sprint 5 |
|:--|:--------:|:--------:|:--------:|:--------:|:--------:|
| **Sprint 1** | ✅ Nội bộ | — | — | — | — |
| **Sprint 2** | ↗️ Dùng S1 | ✅ Nội bộ | — | — | — |
| **Sprint 3** | ↗️ Dùng S1 | ↗️ Dùng S2 | ✅ Nội bộ | — | — |
| **Sprint 4** | ↗️ Dùng S1 | ↗️ Dùng S2 | ↗️ Dùng S3 | ✅ Nội bộ | — |
| **Sprint 5** | ↗️ Dùng S1 | ↗️ Dùng S2 | ↗️ Dùng S3 | ↗️ Dùng S4 | ✅ Nội bộ |

**Key Insight:** Sprint 3 (POS) là **nexus** — nó phụ thuộc S1 + S2 và là nền cho S4, S5. Nếu POS chưa xong → không test E2E được.

---

## 6. VELOCITY & TIMELINE

| Sprint | Chức Năng | Story Points | Tuần | Ngày Bắt Đầu | Ngày Kết Thúc |
|:------:|:---------:|:------------:|:----:|:------------:|:-------------:|
| S1 | 4 | 23 SP | 1–2 | 04/03/2026 | 17/03/2026 |
| S2 | 4 | 22 SP | 3–4 | 18/03/2026 | 31/03/2026 |
| S3 | 4 | 26 SP | 5–6 | 01/04/2026 | 14/04/2026 |
| S4 | 3 | 21 SP | 7–8 | 15/04/2026 | 28/04/2026 |
| S5 | 5 | 19 SP | 9–10 | 29/04/2026 | 12/05/2026 |
| **Tổng** | **20** | **111 SP** | **10 tuần** | | |

---

## 7. GHI CHÚ & RỦI RO

> [!WARNING]
> **Rủi ro lớn nhất:** Sprint 3 (POS) quá nặng. Nếu POS chưa stable → Sprint 4+5 không test tích hợp được.
> **Giảm thiểu:** Reserve 2 ngày cuối Sprint 3 chỉ để test tích hợp + fix bug, không thêm feature mới.

> [!TIP]
> **Mẹo chia việc 3 người:**
> - **M1 (Backend):** Làm DAL/BUS cho Sprint tiếp theo SONG SONG với M2 làm UI Sprint hiện tại.
> - **M2 (Frontend):** Làm UI Sprint hiện tại, dùng data M1 vừa xong Sprint trước.
> - **M3 (QA):** Viết test case Sprint hiện tại + chạy regression test Sprint trước.
> → Kiểu "pipeline" — M1 luôn đi trước M2 nửa Sprint.

> [!IMPORTANT]
> **Các chức năng KHÔNG tính vào 20 chức năng chính:**
> - Đăng nhập / Đăng ký tài khoản (cơ sở hạ tầng)
> - Cấu hình kết nối LAN (DevOps)
> - Main Shell / Ribbon Bar (UI framework)
> - Theme Manager / Dark Mode (UX)
> - Unit Testing framework (Technical)
> - Seed Data / Migration scripts (DevOps)
> - `frmAppDatVeMoPhong` (chỉ mô phỏng, không phải chức năng thực — Web B2C ở S5 thay thế)
