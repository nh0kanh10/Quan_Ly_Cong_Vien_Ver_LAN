# 📘 Nghiệp Vụ Nhân Sự — Căn Cứ Pháp Lý & Thực Tế Đại Nam

> **Nguồn:** Bộ Luật Lao Động 2019 (BLLĐ 2019), Luật BHXH, Thông tư 02/2011/TT-BVHTTDL  
> **Áp dụng cho:** Dự án Quản Lý Công Viên Đại Nam  
> **Cập nhật lần cuối:** 14/04/2026  
>  
> ⚠️ **LƯU Ý QUAN TRỌNG:** Khi luật thay đổi, cập nhật file này TRƯỚC, sau đó update bảng `CauHinhHeThong` trong DB. Không hardcode số ngày/hệ số vào code C#.

---

## 1. PHÂN LOẠI NHÓM CÔNG VIỆC (NhomCongViec)

Theo Điều 113, BLLĐ 2019 — ảnh hưởng trực tiếp đến số ngày phép năm:

| NhomCongViec | Mô tả | Ngày phép cơ bản | Vai trò tại Đại Nam |
|---|---|---|---|
| `ThuongThuong` | Điều kiện làm việc bình thường | **12 ngày/năm** | Bán vé, lễ tân, thu ngân, hành chính |
| `NangNhocNguyHiem` | Công việc nặng nhọc, độc hại, nguy hiểm | **14 ngày/năm** | Cứu hộ bơi lội, vận hành trò chơi cơ khí, chăm sóc động vật nguy hiểm (hổ, sư tử) |
| `DacBietNguyHiem` | Đặc biệt nặng nhọc, độc hại, nguy hiểm | **16 ngày/năm** | (Hiếm — nếu có khu hóa chất, điện cao áp...) |

### Quy tắc cộng thâm niên (Điều 114, BLLĐ 2019)
```
Phép năm thực tế = Phép cơ bản + FLOOR(SoNamThamnien / 5)

Ví dụ:
  NV bình thường, 7 năm thâm niên: 12 + FLOOR(7/5) = 12 + 1 = 13 ngày
  Cứu hộ, 11 năm thâm niên:       14 + FLOOR(11/5) = 14 + 2 = 16 ngày
```

**→ Config DB (CauHinhHeThong):**
```
PHEP_NAM_THUONG_THUONG     = 12
PHEP_NAM_NANG_NHOC         = 14
PHEP_NAM_DAC_BIET          = 16
PHEP_NAM_THAMNIEN_CYCLE    = 5   (cứ 5 năm +1 ngày)
```

---

## 2. NGÀY NGHỈ LỄ TẾT BẮT BUỘC (11 ngày/năm)

Theo Điều 112, BLLĐ 2019:

| Ngày lễ | Số ngày | Ghi chú |
|---|---|---|
| Tết Dương Lịch (01/01) | 1 ngày | |
| Tết Nguyên Đán | 5 ngày | 01 ngày cuối năm + 4 ngày đầu năm âm lịch |
| Ngày Giỗ Tổ Hùng Vương (10/3 âm) | 1 ngày | |
| Ngày Chiến Thắng (30/4) | 1 ngày | **Cao điểm lớn nhất Đại Nam** |
| Ngày Quốc Tế Lao Động (01/5) | 1 ngày | **Cao điểm** |
| Quốc Khánh (02/9) | 2 ngày | 02/09 + 1 ngày liền kề |

### Hệ số lương khi làm ngày lễ (Điều 98, BLLĐ 2019)

| Loại ngày | Hệ số | Ghi chú |
|---|---|---|
| Ngày làm việc thường | 1.5x | Tăng ca ngày thường |
| Ngày nghỉ hằng tuần (T7/CN) | 2.0x | |
| Ngày nghỉ lễ, Tết | **3.0x** | **Chưa kể lương ngày lễ đã được trả** |
| Làm ban đêm (22:00–06:00) | +30% | Cộng thêm vào hệ số trên |

**→ Config DB:**
```
HE_SO_TANG_CA_THUONG       = 1.5
HE_SO_TANG_CA_CUOI_TUAN    = 2.0
HE_SO_TANG_CA_NGAY_LE      = 3.0
HE_SO_TANG_CA_BAN_DEM      = 1.3   (nhân thêm)
```

### Chính sách Đại Nam với ngày lễ cao điểm
Đề xuất **Hybrid Policy** (nhân viên chọn trước khi nhận ca):
- **Option A:** Nhận lương 300% ngay trong kỳ lương
- **Option B:** Tích ngày bù (phải dùng trong 30 ngày theo Điều 97)

---

## 3. CÁC LOẠI NGHỈ PHÉP & CHẾ ĐỘ HƯỞNG

### 3.1 Tổng hợp

| LoaiNghi | Căn cứ pháp lý | % Lương | Ai chi trả | Tối đa | Yêu cầu hồ sơ |
|---|---|---|---|---|---|
| `PhepNam` | Đ.113 BLLĐ | 100% | Công ty | = Phép năm còn lại | Đơn xin nghỉ |
| `NghiOm` | Luật BHXH | 75% | BHXH | 30 ngày/năm (đóng <15 năm) | Giấy chứng nhận bệnh viện |
| `ThaiSanNu` | Đ.34 Luật BHXH | 100% | BHXH | **6 tháng** | Giấy sinh hoặc giấy chứng nhận mang thai |
| `ThaiSanNam` | Đ.34 Luật BHXH | 100% | BHXH | 5-14 ngày tùy TH | Giấy khai sinh/sinh con |
| `TaiNanLaoDong` | Luật ATLĐ | 100% | BHXH | Đến khi hồi phục | Biên bản tai nạn, giám định y tế |
| `NghiBu` | Đ.97 BLLĐ | 100% | Công ty | Dùng trong 30 ngày | Từ ngày bù tích lũy |
| `DotXuatCoLuong` | Đ.115 BLLĐ | 100% | Công ty | Cưới: 3 ngày, Tang: 3 ngày, Con cưới: 1 ngày | Giấy đăng ký kết hôn / Giấy chứng tử |
| `NghiKhongLuong` | Đ.115 BLLĐ | 0% | — | Thỏa thuận | Đơn xin nghỉ không lương |

### 3.2 Nghỉ ốm — Chi tiết theo thâm niên BHXH

| Thời gian đóng BHXH | Điều kiện làm việc | Ngày nghỉ ốm tối đa/năm |
|---|---|---|
| < 15 năm | Bình thường | 30 ngày |
| < 15 năm | Nặng nhọc, nguy hiểm | 40 ngày |
| 15–30 năm | Bình thường | 40 ngày |
| 15–30 năm | Nặng nhọc | 50 ngày |
| > 30 năm | Bình thường | 60 ngày |

---

## 4. TAI NẠN LAO ĐỘNG — Đặc Biệt Quan Trọng Đại Nam

### 4.1 Các khu vực rủi ro cao

| Khu vực | Loại rủi ro | Mức độ |
|---|---|---|
| KV01 — Biển nhân tạo | Đuối nước | 🔴 Rất cao |
| KV08 — Trò chơi cảm giác mạnh | Kẹp, ngã, va đập cơ học | 🔴 Cao |
| KV03 — Vườn thú | Bị cắn, cào (hổ, sư tử, cá sấu) | 🔴 Cao |
| KV02 — Trường đua | Ngã ngựa, tai nạn xe | 🟡 Trung bình |
| Kho, Bảo trì | Điện, vật nặng | 🟡 Trung bình |

### 4.2 Quy trình khi xảy ra tai nạn lao động

```
1. Sơ cứu ngay tại chỗ (nhân viên y tế + cứu hộ)
2. Báo cáo Manager trong vòng 8 giờ
3. Lập Biên Bản Tai Nạn Lao Động (mẫu theo Nghị định 39/2016)
4. Báo Sở LĐTBXH nếu mức độ NẶNg/TỬ VONG trong vòng 24 giờ
5. Chuyển hồ sơ BHXH để hưởng chế độ tai nạn lao động
```

### 4.3 Mức hưởng BHXH tai nạn lao động

| Mức suy giảm lao động | Trợ cấp một lần | Trợ cấp hàng tháng |
|---|---|---|
| 5% – 30% | Một lần (5% → 1.5 tháng lương tối thiểu) | Không |
| ≥ 31% | — | Hàng tháng (31% → 30% lương tối thiểu) |
| Tử vong | Phụ cấp thân nhân | 40% lương hưu người bị tai nạn |

---

## 5. CHỨNG CHỈ HÀNH NGHỀ BẮT BUỘC

### 5.1 Danh sách theo vị trí

| Vai trò | Chứng chỉ bắt buộc | Cơ quan cấp | Chu kỳ gia hạn |
|---|---|---|---|
| Cứu hộ bơi lội | Chứng nhận cứu hộ bơi lội | Sở VHTT&DL tỉnh | **1 năm** |
| Cứu hộ + Sơ cứu | CPR/AED Certificate | Hiệp hội Thể thao dưới nước VN | 2 năm |
| Vận hành trò chơi cơ khí | Chứng chỉ ATLĐ vận hành máy | Cục ATLĐ – Bộ LĐTBXH | 3 năm |
| Chăm sóc thú lớn (hổ, sư tử) | Chứng chỉ chăm sóc động vật hoang dã | Cục Kiểm lâm | 5 năm |
| Lái xe nâng hàng (kho) | Bằng lái xe nâng | Sở GTVT | 5 năm |
| Thợ điện (bảo trì) | Chứng chỉ An toàn điện | EVN / Cục ATLĐ | 3 năm |

### 5.2 Quy tắc cảnh báo
```
> 30 ngày trước hết hạn → Cảnh báo màu vàng (SapHetHan)
> Đã hết hạn            → Cảnh báo đỏ, KHÔNG xếp ca (HetHan)
```

---

## 6. KHÁI NIỆM TĂNG CA & NGHỈ BÙ

### 6.1 Giới hạn tăng ca (Điều 107, BLLĐ 2019)

```
Tối đa 40 giờ/tháng
Tối đa 200 giờ/năm (trường hợp thông thường)
Tối đa 300 giờ/năm (ngành đặc biệt — công viên giải trí có thể được chấp thuận)

→ Đặc biệt: Mùa hè, Tết: Đại Nam nên xin phép UBND để được 300h/năm
```

### 6.2 Ngày bù

Theo Điều 97, nếu làm ngày nghỉ lễ chọn tích ngày bù:
- Phải nghỉ bù trong vòng **30 ngày** sau khi nghỉ bù tích lũy
- Nếu không thể bố trí nghỉ bù → chuyển sang trả tiền 300%

---

## 7. QUẢN LÝ KỶ LUẬT LAO ĐỘNG

Theo Điều 124–129, BLLĐ 2019:

| Hình thức | Điều kiện | Lưu giữ hồ sơ | Xóa bỏ sau |
|---|---|---|---|
| `CanhCao` | Vi phạm lần đầu, không nghiêm trọng | 1 năm | 1 năm không tái phạm |
| `TruLuong` | Vi phạm quy chế tài chính | Vĩnh viễn | — |
| `DinhChiCoLuong` | Chờ điều tra (tối đa 15 ngày, đặc biệt 90 ngày) | Vĩnh viễn | — |
| `DinhChiKhongLuong` | (Không có trong Luật VN — **không được phép**) | — | — |
| `SaThái` | Vi phạm nghiêm trọng, trộm cắp, đánh nhau | Vĩnh viễn | — |

> ⚠️ **Lưu ý:** `DinhChiKhongLuong` không có cơ sở pháp lý tại VN. Không implement form này để tránh tranh chấp lao động.

---

## 8. CHÍNH SÁCH ĐẶC THÙ ĐẠI NAM (Đề Xuất Nội Bộ)

Các chính sách sau KHÔNG phải luật bắt buộc nhưng đề xuất để cạnh tranh nhân sự:

| Chính sách | Nội dung | Cấu hình DB |
|---|---|---|
| Bữa ăn ca | Nhân viên làm > 6 tiếng được ăn ca | `PCAT_AN_CA = 1` |
| Vé tự do | NV + 2 người thân vào cổng miễn phí | Xử lý qua `DonHang` với `TienGiamGia = 100%` |
| Phụ cấp khu nguy hiểm | +500k/tháng với cứu hộ, vận hành | `PHUCAP_KHU_NGUYHIEM = 500000` |
| Trang phục | Cấp 2 bộ/năm | Xuất kho `SanPham (LoaiSP='DongPhuc')` |

---

## 9. CẤU HÌNH DB — DANH SÁCH ĐẦY ĐỦ

Tất cả các con số sau **phải lưu trong `CauHinhHeThong`**, không hardcode trong C#:

```sql
INSERT INTO CauHinhHeThong (Khoa, GiaTri, MoTa) VALUES
-- Phép năm
('PHEP_NAM_THUONG_THUONG',   '12',  N'Ngày phép cơ bản NV thường (Đ.113 BLLĐ 2019)'),
('PHEP_NAM_NANG_NHOC',       '14',  N'Ngày phép NV làm việc nặng nhọc nguy hiểm'),
('PHEP_NAM_DAC_BIET',        '16',  N'Ngày phép NV đặc biệt nguy hiểm'),
('PHEP_NAM_THAMNIEN_CYCLE',  '5',   N'Cứ bao nhiêu năm thâm niên thì +1 ngày phép'),
-- Hệ số lương tăng ca
('HE_SO_TANG_CA_THUONG',     '1.5', N'Hệ số tăng ca ngày thường (Đ.98 BLLĐ 2019)'),
('HE_SO_TANG_CA_CUOI_TUAN',  '2.0', N'Hệ số tăng ca ngày nghỉ hàng tuần'),
('HE_SO_TANG_CA_NGAY_LE',    '3.0', N'Hệ số tăng ca ngày lễ, Tết'),
('HE_SO_CA_DEM',             '1.3', N'Hệ số phụ trội ban đêm 22h-6h sáng'),
-- Giới hạn tăng ca
('TANG_CA_TOIDATUNG_THANG',  '40',  N'Giờ tăng ca tối đa 1 tháng (Đ.107 BLLĐ)'),
('TANG_CA_TOIDA_NAM',        '200', N'Giờ tăng ca tối đa 1 năm (thông thường)'),
-- Cảnh báo chứng chỉ
('CHUNGCHI_CANHBAO_NGAY',    '30',  N'Số ngày trước hết hạn cần cảnh báo chứng chỉ'),
-- Ngày bù
('NHIBÙ_DEADLINE_NGAY',      '30',  N'Số ngày phải dùng ngày bù sau khi tích lũy'),
-- Phụ cấp
('PHUCAP_NGUYHIEM_THANG',    '500000', N'Phụ cấp khu nguy hiểm/tháng (nội bộ Đại Nam)'),
-- Điểm tích lũy
('DIEM_QUY_DOI_VND',         '1000',  N'Số tiền để đổi 1 điểm tích lũy'),
('DIEM_HET_HAN_THANG',       '24',    N'Điểm hết hạn sau bao nhiêu tháng');
```

---

## 10. KHI LUẬT THAY ĐỔI — QUY TRÌNH CẬP NHẬT

```
Bước 1: Đọc văn bản pháp luật mới
Bước 2: Xác định dòng nào trong file này bị ảnh hưởng
Bước 3: Cập nhật file này (commit git có chú thích luật mới)
Bước 4: UPDATE CauHinhHeThong SET GiaTri = [giá trị mới] WHERE Khoa = '...'
Bước 5: KHÔNG cần deploy lại app — logic đọc từ DB sẽ tự cập nhật
Bước 6: Test lại các màn hình: frmBangLuong, frmDonXinNghi, frmXepLich
```

> ✅ **Thiết kế đúng = Luật đổi → Sửa DB → Xong. Không cần lập trình lại.**
