using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using ET.Constants;
using ET.DTOs;
using ET.Models.BanHang;

namespace DAL.Repositories.BanHang
{
    public class DAL_POS
    {
        public static DAL_POS Instance { get; } = new DAL_POS();

        #region Truy vấn dữ liệu

        // Lấy danh sách SP bán được tại 1 điểm POS, kèm giá đang hiệu lực hôm nay.
        public object LayDanhSachSanPhamPOS(int? idDiemBan, string langCode = "vi-VN")
        {
            using (var db = new DaiNamDBDataContext())
            {
                var now = DateTime.Now.Date;
                var diemBan = idDiemBan.HasValue ? db.DiemBanHang_POs.FirstOrDefault(d => d.Id == idDiemBan.Value) : null;
                
                // Cấu hình Menu_POS
                var menuSp = diemBan != null ? db.Menu_POs.Where(m => m.IdDiemBan == diemBan.Id && m.ConHoatDong).Select(m => m.IdSanPham).ToList() : new List<int>();

                bool hasMenu = menuSp.Any();
                bool banVe = diemBan == null || diemBan.ChoPhepBanVe;
                bool banFnb = diemBan == null || diemBan.ChoPhepBanFNB;

                var query = from sp in db.SanPhams
                            where sp.TrangThai == AppConstants.TrangThaiSanPham.DangBan
                               && sp.LoaiSanPham != AppConstants.LoaiSanPham.DoChoThue
                               && sp.LoaiSanPham != AppConstants.LoaiSanPham.LuuTru
                               && (
                                   diemBan == null 
                                   || ((sp.LoaiSanPham == AppConstants.LoaiSanPham.VeVaoKhu || sp.LoaiSanPham == AppConstants.LoaiSanPham.VeTroChoi) && banVe) // Bán vé nếu được phép
                                   || ((sp.LoaiSanPham != AppConstants.LoaiSanPham.VeVaoKhu && sp.LoaiSanPham != AppConstants.LoaiSanPham.VeTroChoi) && hasMenu && menuSp.Contains(sp.Id)) // Chỉ lấy món trong Menu
                                   || ((sp.LoaiSanPham != AppConstants.LoaiSanPham.VeVaoKhu && sp.LoaiSanPham != AppConstants.LoaiSanPham.VeTroChoi) && !hasMenu && banFnb) // Nếu không có Menu, lấy hết nếu được phép bán FNB
                               )
                            

                            join thTmp in db.CauHinhThues.Where(t => t.ConHoatDong && t.HieuLucTu <= now && (t.HieuLucDen == null || t.HieuLucDen >= now))
                            on sp.LoaiSanPham equals thTmp.ApDungChoLoaiSP into thueGroup
                            from th in thueGroup.DefaultIfEmpty()

                            join bdTmp in db.BanDiches
                            on new { Id = sp.Id, Loai = AppConstants.ThucTheDich.SanPham, Cot = AppConstants.TruongDich.TenSanPham, NgonNgu = langCode }
                            equals new { Id = bdTmp.IdThucThe, Loai = bdTmp.LoaiThucThe, Cot = bdTmp.TruongDich, NgonNgu = bdTmp.NgonNgu } into dichGroup
                            from bd in dichGroup.DefaultIfEmpty()

                            // Sắp xếp mặc định
                            orderby sp.LoaiSanPham, sp.TenSanPham
                            
                            select new 
                            {
                                Id = sp.Id,
                                MaSanPham = sp.MaSanPham,
                                TenSanPham = (langCode != "vi-VN" && bd != null) ? bd.NoiDung : sp.TenSanPham,
                                LoaiSanPham = sp.LoaiSanPham,
                                DonGia = sp.DonGia, // Gán giá gốc làm mặc định trước khi tính giá động
                                IdBangGia = (int?)null,
                                IdCauHinhThue = th != null ? (int?)th.Id : (int?)null,
                                PhanTramThue = th != null ? th.TyLePhanTram : 0m,
                                LaVatTu = sp.LaVatTu,
                                AnhDaiDien = sp.AnhDaiDien,
                                IdDonViGoc = sp.IdDonViTinh,
                                TenDonViGoc = sp.DonViTinh != null ? sp.DonViTinh.TenDonVi : "Cái"
                            };

                var listRaw = query.ToList();

                // lấy giá động
                var dsIdSanPham = listRaw.Select(x => x.Id).Distinct().ToList();
                var listBg = db.BangGias.Where(b => dsIdSanPham.Contains(b.IdSanPham) 
                                                && b.HieuLucTu <= now 
                                                && b.HieuLucDen >= now 
                                                && b.TrangThai == AppConstants.TrangThaiHieuLuc.HoatDong).ToList();
                
                bool isHoliday = db.CauHinhNgayLes.Any(n => n.NgayBatDau <= now && n.NgayKetThuc >= now);
                bool isWeekend = now.DayOfWeek == DayOfWeek.Saturday || now.DayOfWeek == DayOfWeek.Sunday;

                var list = listRaw.Select(x =>
                {
                    var bgItem = listBg.Where(b => b.IdSanPham == x.Id).ToList();
                    decimal donGia = x.DonGia ?? 0m;
                    int? idBg = null;

                    if (bgItem.Any())
                    {
                        var bg = bgItem.FirstOrDefault(b => isHoliday && b.LoaiGia == AppConstants.LoaiGiaBan.NgayLe)
                              ?? bgItem.FirstOrDefault(b => isWeekend && b.LoaiGia == AppConstants.LoaiGiaBan.CuoiTuan)
                              ?? bgItem.FirstOrDefault(b => b.LoaiGia == AppConstants.LoaiGiaBan.MacDinh);

                        if (bg != null)
                        {
                            donGia = bg.GiaBan;
                            idBg = bg.Id;
                        }
                    }

                    return new 
                    {
                        x.Id, x.MaSanPham, x.TenSanPham, x.LoaiSanPham, DonGia = donGia, IdBangGia = idBg, 
                        x.IdCauHinhThue, x.PhanTramThue, x.LaVatTu, x.AnhDaiDien, x.IdDonViGoc, x.TenDonViGoc
                    };
                }).ToList();

                // Sắp xếp lại theo cấu hình Menu_POS (nếu có)
                if (diemBan != null && hasMenu)
                {
                    var dictThuTu = db.Menu_POs.Where(m => m.IdDiemBan == diemBan.Id).ToDictionary(m => m.IdSanPham, m => m.ThuTuHienThi);
                    list = list.OrderBy(x => !x.LoaiSanPham.StartsWith("Ve") && dictThuTu.ContainsKey(x.Id) ? dictThuTu[x.Id] : 9999)
                               .ThenBy(x => x.LoaiSanPham)
                               .ThenBy(x => x.TenSanPham)
                               .ToList();
                }

                if (langCode != "vi-VN" && list.Count > 0)
                {
                    var dicDVT = DAL.Repositories.HeThong.DAL_BanDich.Instance.LayTuDienThucThe(
                        AppConstants.ThucTheDich.DonViTinh, langCode, AppConstants.TruongDich.TenDonVi);

                    var resultList = list.Select(x =>
                    {
                        var trans = dicDVT.ContainsKey(x.IdDonViGoc) ? dicDVT[x.IdDonViGoc] : x.TenDonViGoc;
                        return new
                        {
                            x.Id, x.MaSanPham, x.TenSanPham, x.LoaiSanPham, x.DonGia, x.IdBangGia, 
                            x.IdCauHinhThue, x.PhanTramThue, x.LaVatTu, x.AnhDaiDien, x.IdDonViGoc,
                            TenDonViGoc = trans
                        };
                    }).ToList();
                    return resultList;
                }

                return list;
            }
        }

        /// <summary>
        /// Lấy danh sách các đơn hàng đã thanh toán trong một phiên làm việc
        /// </summary>
        public object LayDanhSachDonHangTheoPhien(int idPhien)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.DonHangs
                    .Where(d => d.IdPhienThuNgan == idPhien && d.TrangThai == ET.Constants.AppConstants.TrangThaiDonHang.DaThanhToan)
                    .Select(d => new
                    {
                        d.Id,
                        d.MaDonHang,
                        d.NgayTao,
                        d.TongThanhToan,
                        KhachHang = d.KhachHang != null ? (d.KhachHang.ThongTin.HoTen ?? d.KhachHang.MaKhachHang) : "Khách lẻ",
                        NhanVien = d.NhanVien != null ? d.NhanVien.ThongTin.HoTen : "",
                        SoLuongMon = d.ChiTietDonHangs.Count,
                        GhiChu = d.GhiChu
                    })
                    .OrderByDescending(d => d.NgayTao)
                    .ToList();
            }
        }

        /// <summary>
        /// Lấy các đơn vị bán khả dụng cho 1 SP (VD: Lon, Lốc, Thùng).
        /// POS dùng để hiện dropdown chọn đơn vị khi thêm vào giỏ.
        /// </summary>
        public List<ET_DonViBanPOS> LayDonViBanTheoSanPham(int idSanPham, decimal donGiaGoc, string langCode = "vi-VN")
        {
            using (var db = new DaiNamDBDataContext())
            {
                var raw = db.QuyDoiDonVis
                    .Where(q => q.IdSanPham == idSanPham)
                    .Select(q => new
                    {
                        q.IdDonViDich,
                        TenDonVi = q.DonViTinh1.TenDonVi,
                        q.TyLeQuyDoi,
                        q.GiaBan
                    }).ToList();

                var dicDVT = new Dictionary<int, string>();
                if (langCode != "vi-VN" && raw.Count > 0)
                {
                    dicDVT = DAL.Repositories.HeThong.DAL_BanDich.Instance.LayTuDienThucThe(
                        AppConstants.ThucTheDich.DonViTinh, langCode, AppConstants.TruongDich.TenDonVi);
                }

                return raw.Select(q => new ET_DonViBanPOS
                {
                    IdDonViDich = q.IdDonViDich,
                    TenDonVi = dicDVT.ContainsKey(q.IdDonViDich) ? dicDVT[q.IdDonViDich] : q.TenDonVi,
                    TyLeQuyDoi = q.TyLeQuyDoi,
                    GiaBan = q.GiaBan ?? (donGiaGoc * q.TyLeQuyDoi)
                }).ToList();
            }
        }

        #endregion

        #region Combo POS

        /// <summary>
        /// Lấy danh sách combo được phép bán trên POS.
        /// Filter: TrangThai == HoatDong, TyLePhanBo tổng = 100%, DaXoa = false.
        /// </summary>
        public List<DTO_ComboPOS> LayDanhSachComboPOS()
        {
            using (var db = new DaiNamDBDataContext())
            {
                var combos = db.Combos
                    .Where(c => !c.DaXoa && c.TrangThai == AppConstants.TrangThaiCombo.HoatDong)
                    .OrderBy(c => c.TenCombo)
                    .ToList();

                var result = new List<DTO_ComboPOS>();
                foreach (var c in combos)
                {
                    result.Add(new DTO_ComboPOS
                    {
                        Id = c.Id,
                        MaCombo = c.MaCombo,
                        TenCombo = c.TenCombo,
                        GiaCombo = c.GiaCombo,
                        MoTa = c.MoTa,
                        SoThanhPhan = db.ComboChiTiets.Count(ct => ct.IdCombo == c.Id)
                    });
                }

                return result;
            }
        }

        // Lấy chi tiết thành phần combo kèm thông tin SP (LoaiSanPham, LaVatTu, giá) để tách vào giỏ POS.
        public List<DTO_ComboItemPOS> LayChiTietComboPOS(int idCombo)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var now = DateTime.Now.Date;
                var listRaw = (from ct in db.ComboChiTiets
                        join sp in db.SanPhams on ct.IdSanPham equals sp.Id
                        where ct.IdCombo == idCombo
                        join thTmp in db.CauHinhThues.Where(t => t.ConHoatDong && t.HieuLucTu <= now && (t.HieuLucDen == null || t.HieuLucDen >= now))
                        on sp.LoaiSanPham equals thTmp.ApDungChoLoaiSP into thueGroup
                        from th in thueGroup.DefaultIfEmpty()

                        select new DTO_ComboItemPOS
                        {
                            IdSanPham = sp.Id,
                            MaSanPham = sp.MaSanPham,
                            TenSanPham = sp.TenSanPham,
                            LoaiSanPham = sp.LoaiSanPham,
                            LaVatTu = sp.LaVatTu,
                            SoLuong = (int)ct.SoLuong,
                            TyLePhanBo = ct.TyLePhanBo,
                            DonGia = sp.DonGia, // Gán giá gốc làm mặc định trước khi tính giá động
                            IdCauHinhThue = th != null ? (int?)th.Id : null,
                            PhanTramThue = th != null ? th.TyLePhanTram : 0m
                }).ToList();

                // Áp dụng giá động (BangGia) cho từng thành phần combo
                var dsIdSanPham = listRaw.Select(x => x.IdSanPham).Distinct().ToList();
                var listBg = db.BangGias.Where(b => dsIdSanPham.Contains(b.IdSanPham) 
                                                && b.HieuLucTu <= now 
                                                && b.HieuLucDen >= now 
                                                && b.TrangThai == AppConstants.TrangThaiHieuLuc.HoatDong).ToList();
                
                bool isHoliday = db.CauHinhNgayLes.Any(n => n.NgayBatDau <= now && n.NgayKetThuc >= now);
                bool isWeekend = now.DayOfWeek == DayOfWeek.Saturday || now.DayOfWeek == DayOfWeek.Sunday;

                foreach (var item in listRaw)
                {
                    var bgItem = listBg.Where(b => b.IdSanPham == item.IdSanPham).ToList();
                    if (bgItem.Any())
                    {
                        var bg = bgItem.FirstOrDefault(b => isHoliday && b.LoaiGia == AppConstants.LoaiGiaBan.NgayLe)
                              ?? bgItem.FirstOrDefault(b => isWeekend && b.LoaiGia == AppConstants.LoaiGiaBan.CuoiTuan)
                              ?? bgItem.FirstOrDefault(b => b.LoaiGia == AppConstants.LoaiGiaBan.MacDinh);

                        if (bg != null) item.DonGia = bg.GiaBan;
                    }
                }

                return listRaw;
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        /// <summary>
        /// Thực thi giao dịch Checkout tập trung cho hệ thống POS.
        /// Sử dụng db.Transaction tuần tự để ghi nhận đồng thời 7 bảng:
        /// Đơn hàng, Chi tiết đơn hàng, Vé điện tử (nếu có), Sổ cái kho (trừ tồn vật lý), Chứng từ thu, Chi tiết thanh toán, và Sổ cái ví RFID.
        /// Đảm bảo an toàn (trả lại toàn bộ hệ thống nếu 1 khâu thất bại).
        /// </summary>
        /// <param name="cart">Mô hình giỏ hàng chứa danh sách sản phẩm (Tickets, F&B) và các phương thức thanh toán.</param>
        /// <param name="idKhoXuatBan">ID kho dùng để trừ tồn vật lý trực tiếp khi sản phẩm có cờ LaVatTu = true.</param>
        /// <returns>Trả về ID thực tế của Đơn Hàng vừa được tạo thành công trong Database.</returns>
        public int Checkout(ET_CartSession cart, int idKhoXuatBan)
        {
            using (var db = new DaiNamDBDataContext())
            {
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    db.Transaction = tx;
                    try
                    {
                        // 1. Lưu Header Đơn Hàng
                        var donHang = new DonHang
                        {
                            MaDonHang = cart.MaDonHang,
                            IdKhachHang = cart.IdKhachHang,
                            IdNhanVien = cart.IdNhanVien,
                            IdPhienThuNgan = cart.IdPhienThuNgan,
                            NguonBan = cart.NguonBan ?? "TrucTiep",
                            TongTienHang = cart.TongTienHang,
                            TongGiamGia = cart.TongGiamGia,
                            TienThueVAT = cart.TienThueVAT,
                            TienPhiDichVu = cart.TienPhiDichVu,
                            TongThanhToan = cart.TongThanhToan,
                            TrangThai = AppConstants.TrangThaiDonHang.DaThanhToan,
                            GhiChu = cart.GhiChu,
                            NgayTao = DateTime.Now
                        };
                        db.DonHangs.InsertOnSubmit(donHang);
                        db.SubmitChanges(); 

                        // Khởi tạo Phiếu Xuất Kho nếu có xuất bán vật tư
                        int? idChungTuKho = null;
                        int idKhoKhachAo = db.KhoHangs.FirstOrDefault(k => k.MaKho == "KHO_KHACH")?.Id ?? idKhoXuatBan;
                        if (idKhoXuatBan > 0 && cart.DanhSachDong.Any(i => i.LaVatTu || i.LoaiSanPham == AppConstants.LoaiSanPham.Combo))
                        {
                            var chungTu = new ChungTu
                            {
                                MaChungTu = "PX-" + cart.MaDonHang,
                                LoaiChungTu = AppConstants.LoaiChungTuKho.XUAT_BAN,
                                NgayChungTu = DateTime.Now,
                                TrangThai = AppConstants.TrangThaiChungTuKho.DaDuyet,
                                IdNguoiTao = cart.IdNhanVien,
                                GhiChu = "Xuất bán POS - Đơn " + cart.MaDonHang,
                                NgayTao = DateTime.Now
                            };
                            db.ChungTus.InsertOnSubmit(chungTu);
                            db.SubmitChanges();
                            idChungTuKho = chungTu.Id;
                        }

                        // 2. Loop insert Chi Tiết & Vé & Tồn kho
                        foreach (var item in cart.DanhSachDong)
                        {
                            if (item.LoaiSanPham == AppConstants.LoaiSanPham.Combo)
                            {
                                ProcessCombo(item, donHang, cart, idKhoXuatBan, idChungTuKho ?? 0, idKhoKhachAo, db);
                            }
                            else
                            {
                                var chiTiet = InsertChiTietDonHang(item, donHang.Id, db);
                                if (item.LoaiSanPham == AppConstants.LoaiSanPham.VeVaoKhu || item.LoaiSanPham == AppConstants.LoaiSanPham.VeTroChoi)
                                {
                                    ProcessSinhVe(chiTiet, item, cart, db);
                                }
                                else if (item.LaVatTu)
                                {
                                    ProcessTruKho(item, cart, idKhoXuatBan, idChungTuKho ?? 0, idKhoKhachAo, db);
                                }

                                if (item.IdQuyenLoiDoan.HasValue)
                                {
                                    var lsTru = new LichSuQuetDoan
                                    {
                                        IdQuyenLoi = item.IdQuyenLoiDoan.Value,
                                        SoSuatTru = (int)item.SoLuong,
                                        IdNhanVien = cart.IdNhanVien,
                                        ThoiGian = DateTime.Now
                                    };
                                    db.LichSuQuetDoans.InsertOnSubmit(lsTru);
                                }
                            }
                        }

                        // Đã Insert ChiTietDonHang xong.
                        
                        var kh = cart.IdKhachHang.HasValue ? db.KhachHangs.FirstOrDefault(x => x.IdDoiTac == cart.IdKhachHang.Value) : null;

                        // Trừ điểm 
                        if (kh != null)
                        {
                            // Tính điểm hiện có từ LichSuDiem
                            int diemHienTai = db.LichSuDiems
                                .Where(ld => ld.IdKhachHang == kh.IdDoiTac)
                                .Sum(ld => (int?)ld.SoDiem) ?? 0;

                            foreach (var pay in cart.DanhSachThanhToan)
                            {
                                if (pay.PhuongThuc == AppConstants.PhuongThucTT.DiemTichLuy && pay.DiemQuyDoi.HasValue)
                                {
                                    diemHienTai -= pay.DiemQuyDoi.Value;
                                    
                                    var lsTru = new LichSuDiem
                                    {
                                        IdKhachHang = kh.IdDoiTac,
                                        LoaiGiaoDich = AppConstants.LoaiGiaoDichDiem.TruDiem,
                                        SoDiem = -pay.DiemQuyDoi.Value,
                                        SoDuSauGD = diemHienTai,
                                        IdDonHang = donHang.Id,
                                        MoTa = "Thanh toán bằng điểm",
                                        NgayTao = DateTime.Now
                                    };
                                    db.LichSuDiems.InsertOnSubmit(lsTru);
                                }
                            }
                        }

                        // 3. Lập phiếu tài chính tổng
                        string phuongThucChinh = AppConstants.PhuongThucTT.TienMat;
                        if (cart.DanhSachThanhToan != null && cart.DanhSachThanhToan.Any())
                        {
                            phuongThucChinh = cart.DanhSachThanhToan.OrderByDescending(p => p.SoTien).First().PhuongThuc;
                        }

                        var chungTuTC = new ChungTuTC
                        {
                            MaChungTu = "PT-" + cart.MaDonHang,
                            LoaiChungTu = AppConstants.LoaiChungTuTC.THU_THANHTOAN,
                            IdDonHang = donHang.Id,
                            IdPhienThuNgan = cart.IdPhienThuNgan,
                            MaGiaoDichClient = Guid.NewGuid(),
                            SoTien = cart.TongThanhToan,
                            PhuongThuc = phuongThucChinh,
                            TrangThai = AppConstants.TrangThaiChungTuTC.DaDuyet,
                            IdNguoiTao = cart.IdNhanVien,
                            NgayChungTu = DateTime.Now,
                            NgayTao = DateTime.Now
                        };
                        db.ChungTuTCs.InsertOnSubmit(chungTuTC);
                        db.SubmitChanges(); 

                        // 4. Lập các dòng chi tiết phương thức thanh toán & Trừ RFID
                        foreach (var pay in cart.DanhSachThanhToan)
                        {
                            var ctPay = new ChiTietThanhToan
                            {
                                IdChungTu = chungTuTC.Id,
                                PhuongThuc = pay.PhuongThuc,
                                SoTien = pay.SoTien,
                                GhiChu = pay.GhiChu
                            };
                            db.ChiTietThanhToans.InsertOnSubmit(ctPay);

                            if (pay.PhuongThuc == AppConstants.PhuongThucTT.ViRFID && pay.IdViDienTu.HasValue)
                            {
                                var socaiVi = new SoCaiVi
                                {
                                    IdVi = pay.IdViDienTu.Value,
                                    LoaiPhep = AppConstants.LoaiPhepVi.Tru,
                                    SoTien = pay.SoTien,
                                    IdChungTu = chungTuTC.Id,
                                    MoTa = "Thanh toan POS",
                                    NguoiTao = cart.IdNhanVien,
                                    NgayTao = DateTime.Now
                                };
                                db.SoCaiVis.InsertOnSubmit(socaiVi);
                            }
                        }

                        //  Tính điểm được thưởng từ số tiền thực trả
                        if (kh != null && cart.TongThanhToan > 0)
                        {
                            decimal tongTienDuocTich = cart.DanhSachThanhToan
                                .Where(p => p.PhuongThuc != AppConstants.PhuongThucTT.DiemTichLuy)
                                .Sum(p => p.SoTien);

                            if (tongTienDuocTich > cart.TongThanhToan)
                                tongTienDuocTich = cart.TongThanhToan;

                            if (tongTienDuocTich > 0)
                            {
                                var configDIEM = db.CauHinhs.FirstOrDefault(c => c.Khoa == AppConstants.ConfigKeys.DIEM_QUY_DOI);
                                decimal tyLeQuyDoi = 1000m;
                                if (configDIEM != null && decimal.TryParse(configDIEM.GiaTri, out var val) && val > 0) 
                                    tyLeQuyDoi = val;

                                int diemCong = (int)(tongTienDuocTich / tyLeQuyDoi);
                                if (diemCong > 0)
                                {
                                    // Tính điểm hiện có từ LichSuDiem 
                                    int diemSauCong = (db.LichSuDiems
                                        .Where(ld => ld.IdKhachHang == kh.IdDoiTac)
                                        .Sum(ld => (int?)ld.SoDiem) ?? 0) + diemCong;

                                    var lsCong = new LichSuDiem
                                    {
                                        IdKhachHang = kh.IdDoiTac,
                                        LoaiGiaoDich = AppConstants.LoaiGiaoDichDiem.CongDiem,
                                        SoDiem = diemCong,
                                        SoDuSauGD = diemSauCong,
                                        IdDonHang = donHang.Id,
                                        MoTa = "Tích điểm mua hàng",
                                        NgayTao = DateTime.Now
                                    };
                                    db.LichSuDiems.InsertOnSubmit(lsCong);
                                }
                            }
                        }

                        db.SubmitChanges();
                        tx.Commit();
                        return donHang.Id;
                    }
                    catch
                    {
                        tx.Rollback();
                        throw; 
                    }
                }
            }
        }

        public List<ET.DTOs.DTO_MaVachVe> LayDanhSachMaVachVe(int idDonHang)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.VeDienTus
                         .Where(v => v.ChiTietDonHang.IdDonHang == idDonHang)
                         .Select(v => new ET.DTOs.DTO_MaVachVe
                         {
                             MaVach = v.MaVach,
                             TenVe = v.ChiTietDonHang.SanPham.TenSanPham
                         })
                         .ToList();
            }
        }

        // Tra số dư ví theo Id (dùng trước khi bắt đầu transaction)
        public decimal LaySoDuVi(int idVi)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.SoCaiVis
                         .Where(s => s.IdVi == idVi)
                         .Select(s => (decimal?)(s.LoaiPhep == AppConstants.LoaiPhepVi.Nap ? s.SoTien : -s.SoTien))
                         .Sum() ?? 0m;
            }
        }

        // Tra thông tin thẻ RFID theo mã thẻ
        public DTO_TheRFID TraCuuTheRFID(string maThe)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = from r in db.TheRFIDs
                            where r.MaThe == maThe && r.TrangThai == "DangSuDung"
                            join khTmp in db.KhachHangs on r.IdKhachHang equals khTmp.IdDoiTac into khGroup
                            from kh in khGroup.DefaultIfEmpty()
                            join ttTmp in db.ThongTins on kh.IdDoiTac equals ttTmp.Id into ttGroup
                            from tt in ttGroup.DefaultIfEmpty()
                            select new DTO_TheRFID
                            {
                                Id = r.Id,
                                MaThe = r.MaThe,
                                IdViDienTu = r.IdViDienTu,
                                TrangThai = r.TrangThai,
                                IdKhachHang = kh != null ? (int?)kh.IdDoiTac : null,
                                TenKhach = tt != null ? tt.HoTen : null
                            };
                            
                return query.FirstOrDefault();
            }
        }

        #endregion

        #region Helper Methods (Nghiệp vụ phụ trợ Checkout)

        private ChiTietDonHang InsertChiTietDonHang(ET_CartItem item, int idDonHang, DaiNamDBDataContext db)
        {
            var chiTiet = new ChiTietDonHang
            {
                IdDonHang = idDonHang,
                IdSanPham = item.IdSanPham,
                SoLuong = item.SoLuong,
                DonGiaThucTe = item.DonGiaThucTe,
                IdBangGia = item.IdBangGia,
                IdLoHang = item.IdLoHang,
                IdCauHinhThue = item.IdCauHinhThue,
                TienThue = item.TienThue,
                GhiChu = item.GhiChu
            };
            db.ChiTietDonHangs.InsertOnSubmit(chiTiet);
            return chiTiet;
        }

        private void ProcessSinhVe(ChiTietDonHang chiTiet, ET_CartItem item, ET_CartSession cart, DaiNamDBDataContext db)
        {
            for (int count = 0; count < (int)item.SoLuong; count++)
            {
                var ve = new VeDienTu
                {
                    ChiTietDonHang = chiTiet, 
                    IdSanPham = item.IdSanPham,
                    IdKhachHang = cart.IdKhachHang,
                    MaVach = $"{cart.MaDonHang}-{item.IdSanPham}-{Guid.NewGuid().ToString().Substring(0, 8).ToUpper()}", 
                    TrangThai = ET.Constants.AppConstants.TrangThaiVe.ChuaSuDung,
                    NgayTao = DateTime.Now
                };
                db.VeDienTus.InsertOnSubmit(ve);
            }
        }

        private void ProcessTruKho(ET_CartItem item, ET_CartSession cart, int idKhoXuatBan, int idChungTuKho, int idKhoKhachAo, DaiNamDBDataContext db)
        {
            if (idKhoXuatBan <= 0 || idChungTuKho <= 0) return;

            var dinhmucs = db.DinhMucNguyenLieus.Where(d => d.IdThanhPham == item.IdSanPham).ToList();
            if (dinhmucs.Any())
            {
                foreach (var dm in dinhmucs)
                {
                    int soLuongTruDm = Convert.ToInt32(dm.SoLuong * item.SoLuong * item.HeSoQuyDoi);
                    TruTonKhoVaSoCai(dm.IdNguyenLieu, soLuongTruDm, item.TenSanPham, idKhoXuatBan, idChungTuKho, idKhoKhachAo, cart.IdNhanVien, db);
                }
            }
            else
            {
                int soLuongTru = Convert.ToInt32(item.SoLuong * item.HeSoQuyDoi);
                decimal donGiaTheoGoc = item.DonGiaThucTe / (item.HeSoQuyDoi > 0 ? item.HeSoQuyDoi : 1m);
                TruTonKhoVaSoCai(item.IdSanPham, soLuongTru, item.TenSanPham, idKhoXuatBan, idChungTuKho, idKhoKhachAo, cart.IdNhanVien, db, donGiaTheoGoc);
            }
        }

        private void TruTonKhoVaSoCai(int idSanPham, int soLuongTru, string tenSanPhamGoc, int idKhoXuatBan, int idChungTuKho, int idKhoKhachAo, int idNhanVien, DaiNamDBDataContext db, decimal? giaXuat = null)
        {
            // Kiểm tra tồn kho từ ChiTietChungTu 
            var kho = db.KhoHangs.FirstOrDefault(k => k.Id == idKhoXuatBan);
            if (kho != null && !kho.ChoPhepTonAm)
            {
                decimal tongNhap = db.ChiTietChungTus
                    .Where(ct => ct.IdKhoNhap == idKhoXuatBan && ct.IdSanPham == idSanPham
                              && ct.ChungTu.TrangThai == ET.Constants.AppConstants.TrangThaiChungTuKho.DaDuyet)
                    .Sum(ct => (decimal?)ct.SoLuong) ?? 0m;

                decimal tongXuat = db.ChiTietChungTus
                    .Where(ct => ct.IdKhoXuat == idKhoXuatBan && ct.IdSanPham == idSanPham
                              && ct.ChungTu.TrangThai == ET.Constants.AppConstants.TrangThaiChungTuKho.DaDuyet)
                    .Sum(ct => (decimal?)ct.SoLuong) ?? 0m;

                decimal tonHienTai = tongNhap - tongXuat;
                if (tonHienTai < soLuongTru)
                {
                    var sp = db.SanPhams.FirstOrDefault(s => s.Id == idSanPham);
                    string tenSp = sp != null ? sp.TenSanPham : "Nguyên liệu";
                    throw new Exception($"Sản phẩm '{tenSp}' (của món '{tenSanPhamGoc}') chỉ còn {tonHienTai:N0} cái trong kho, không đủ xuất bán!");
                }
            }

            // insert ChiTietChungTu
            var spItem = db.SanPhams.FirstOrDefault(s => s.Id == idSanPham);
            decimal donGiaFinal = (giaXuat.HasValue && giaXuat.Value > 0) ? giaXuat.Value
                : (spItem != null && spItem.DonGia.HasValue ? spItem.DonGia.Value : 0m);

            var ctiet = new ChiTietChungTu
            {
                IdChungTu = idChungTuKho,
                IdSanPham = idSanPham,
                IdKhoXuat = idKhoXuatBan,
                IdKhoNhap = idKhoKhachAo,
                SoLuong = soLuongTru,
                DonGia = donGiaFinal,
                GhiChu = $"Xuất bán từ món {tenSanPhamGoc}"
            };
            db.ChiTietChungTus.InsertOnSubmit(ctiet);
        }

        private void ProcessCombo(ET_CartItem item, DonHang donHang, ET_CartSession cart, int idKhoXuatBan, int idChungTuKho, int idKhoKhachAo, DaiNamDBDataContext db)
        {
            var chiTietCombos = db.ComboChiTiets.Where(c => c.IdCombo == item.IdSanPham).ToList();
            if (!chiTietCombos.Any()) return;

            decimal tongTienComboCuaMotCombo = item.DonGiaThucTe;
            decimal tienDaPhanBoChoMotCombo = 0m;

            for (int i = 0; i < chiTietCombos.Count; i++)
            {
                var ct = chiTietCombos[i];
                var sp = db.SanPhams.FirstOrDefault(x => x.Id == ct.IdSanPham);
                if (sp == null) continue;

                decimal giaPhanBoChoMotMon;
                if (i == chiTietCombos.Count - 1)
                {
                    giaPhanBoChoMotMon = tongTienComboCuaMotCombo - tienDaPhanBoChoMotCombo;
                }
                else
                {
                    giaPhanBoChoMotMon = Math.Round(tongTienComboCuaMotCombo * (ct.TyLePhanBo / 100m));
                    tienDaPhanBoChoMotCombo += giaPhanBoChoMotMon;
                }

                var childItem = new ET_CartItem
                {
                    IdSanPham = ct.IdSanPham,
                    SoLuong = ct.SoLuong * item.SoLuong,
                    DonGiaGoc = sp.DonGia ?? 0m,
                    TienGiamGiaDong = Math.Round((decimal)((sp.DonGia ?? 0m) * ct.SoLuong * item.SoLuong) - giaPhanBoChoMotMon),
                    DonGiaThucTe = giaPhanBoChoMotMon,
                    IdBangGia = item.IdBangGia,
                    IdLoHang = null,
                    IdCauHinhThue = item.IdCauHinhThue,
                    PhanTramThue = item.PhanTramThue,
                    LaVatTu = sp.LaVatTu,
                    LoaiSanPham = sp.LoaiSanPham,
                    HeSoQuyDoi = 1m,
                    GhiChu = $"Từ Combo: {item.TenSanPham}"
                };

                var chiTiet = InsertChiTietDonHang(childItem, donHang.Id, db);

                if (childItem.LoaiSanPham == AppConstants.LoaiSanPham.VeVaoKhu || childItem.LoaiSanPham == AppConstants.LoaiSanPham.VeTroChoi)
                {
                    ProcessSinhVe(chiTiet, childItem, cart, db);
                }
                else if (childItem.LaVatTu)
                {
                    ProcessTruKho(childItem, cart, idKhoXuatBan, idChungTuKho, idKhoKhachAo, db);
                }
            }
        }

        #endregion
    }
}


