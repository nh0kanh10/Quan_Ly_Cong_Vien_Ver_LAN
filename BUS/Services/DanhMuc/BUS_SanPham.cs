using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using DAL.Repositories.DanhMuc;
using ET.Constants;
using ET.Results;
using ET.Models.DanhMuc;

namespace BUS.Services.DanhMuc
{
    /// <summary>
    /// Lớp xử lý nghiệp vụ cho module Sản phẩm (CN09).
    /// 
    /// Quy tắc kiến trúc 3 lớp:
    /// - BUS nhận và trả kiểu ET_ (lớp dữ liệu thuần, chỉ chứa property).
    /// - Bên trong BUS tự chuyển đổi sang kiểu DAL khi cần đọc/ghi DB.
    /// - GUI không bao giờ truy cập DAL trực tiếp.
    /// 
    /// Sơ đồ phụ thuộc:
    ///   GUI -> BUS + ET (chỉ biết 2 lớp này)
    ///   BUS -> DAL + ET (biết cả 3 lớp)
    ///   DAL -> LINQ to SQL (nội bộ, không lộ ra ngoài)
    /// </summary>
    public class BUS_SanPham
    {
        #region Khởi tạo (Singleton)

        public static BUS_SanPham Instance { get; } = new BUS_SanPham();

        private readonly DAL_SanPham _dal = DAL_SanPham.Instance;

        #endregion

        #region Mapping DAL ↔ ET

        /// <summary>
        /// Chuyển đối tượng DAL (do LINQ to SQL sinh ra) sang ET_ (lớp dữ liệu thuần).
        /// 
        /// "Làm phẳng" thuộc tính liên kết: thay vì truy cập sp.DonViTinh.TenDonVi
        /// (sẽ gây lỗi ObjectDisposedException sau khi đóng kết nối DB),
        /// ta sao chép giá trị sang TenDonViTinh để Grid hiển thị trực tiếp.
        /// </summary>
        /// <param name="dal">Đối tượng SanPham lấy từ tầng DAL</param>
        /// <returns>ET_SanPham chứa dữ liệu đã sao chép, GUI dùng được ngay</returns>
        private ET_SanPham MapToET(SanPham dal)
        {
            return new ET_SanPham
            {
                Id = dal.Id,
                MaSanPham = dal.MaSanPham,
                TenSanPham = dal.TenSanPham,
                LoaiSanPham = dal.LoaiSanPham,
                IdDonViTinh = dal.IdDonViTinh,
                TenDonViTinh = dal.DonViTinh?.TenDonVi,
                DonGia = dal.DonGia,
                AnhDaiDien = dal.AnhDaiDien,
                LaVatTu = dal.LaVatTu,
                CanQuanLyLo = dal.CanQuanLyLo,
                TrangThai = dal.TrangThai,
                DaXoa = dal.DaXoa,
                NgayTao = dal.NgayTao,

                // Map bảng con (chỉ khi eager-loaded trong DAL)
                BangGias = dal.BangGias?.Select(MapBangGiaToET).ToList()
                           ?? new List<ET_BangGia>(),

                SanPham_Ve = dal.SanPham_Ve != null ? new ET_SanPham_Ve
                {
                    IdSanPham = dal.SanPham_Ve.IdSanPham,
                    LoaiVe = dal.SanPham_Ve.LoaiVe,
                    DoiTuongVe = dal.SanPham_Ve.DoiTuongVe,
                    CanTaoToken = dal.SanPham_Ve.CanTaoToken
                } : null,

                MonAn = dal.MonAn != null ? new ET_MonAn
                {
                    IdSanPham = dal.MonAn.IdSanPham,
                    IdNhaHang = dal.MonAn.IdNhaHang,
                    CoDiUng = dal.MonAn.CoDiUng,
                    PhanLoai = dal.MonAn.PhanLoai,
                    MoTaNgan = dal.MonAn.MoTaNgan,
                    AnHienMenu = dal.MonAn.AnHienMenu
                } : null,

                Ve_QuyenTruyCaps = dal.Ve_QuyenTruyCaps?.Select(qv => new ET_Ve_QuyenTruyCap
                {
                    Id = qv.Id,
                    IdSanPhamVe = qv.IdSanPhamVe,
                    IdKhuVuc = qv.IdKhuVuc ?? 0,
                    IdTroChoi = qv.IdTroChoi,
                    SoLuotChoPhep = qv.SoLuotChoPhep,
                    GhiChu = qv.GhiChu
                }).ToList() ?? new List<ET_Ve_QuyenTruyCap>()
            };
        }

        private ET_BangGia MapBangGiaToET(BangGia dal)
        {
            return new ET_BangGia
            {
                Id = dal.Id,
                IdSanPham = dal.IdSanPham,
                LoaiGia = dal.LoaiGia,
                GiaBan = dal.GiaBan,
                HieuLucTu = dal.HieuLucTu,
                HieuLucDen = dal.HieuLucDen,
                TrangThai = dal.TrangThai
            };
        }

        /// <summary>
        /// Chuyển ET_ ngược lại thành kiểu DAL (hướng ghi DB).
        /// Chỉ sao chép các cột đơn giản — bảng con được gắn riêng trong ThemMoi/CapNhat.
        /// </summary>
        private BangGia MapBangGiaToDAL(ET_BangGia et)
        {
            return new BangGia
            {
                LoaiGia = et.LoaiGia,
                GiaBan = et.GiaBan,
                HieuLucTu = et.HieuLucTu,
                HieuLucDen = et.HieuLucDen,
                TrangThai = et.TrangThai ?? "HoatDong",
                NgayTao = DateTime.Now
            };
        }

        private SanPham MapToDAL(ET_SanPham et)
        {
            return new SanPham
            {
                Id = et.Id,
                MaSanPham = et.MaSanPham,
                TenSanPham = et.TenSanPham,
                LoaiSanPham = et.LoaiSanPham,
                IdDonViTinh = et.IdDonViTinh,
                DonGia = et.DonGia,
                AnhDaiDien = et.AnhDaiDien,
                LaVatTu = et.LaVatTu,
                CanQuanLyLo = et.CanQuanLyLo,
                TrangThai = et.TrangThai,
                NgayTao = et.Id == 0 ? DateTime.Now : et.NgayTao 
            };
        }

        #endregion

        #region Truy vấn dữ liệu

        /// <summary>
        /// Lấy danh sách sản phẩm chưa bị xoá mềm (DaXoa = false).
        /// Có thể lọc theo loại SP (VeVaoKhu, AnUong, TuDo...).
        /// Tên đơn vị tính đã được sao chép sẵn từ bảng DonViTinh.
        /// </summary>
        /// <param name="loaiSPFilter">Mã loại SP cần lọc (null = lấy tất cả)</param>
        /// <param name="langCode">Mã ngôn ngữ (vi-VN, en-US, zh-CN)</param>
        /// <returns>Danh sách ET_SanPham sắp xếp theo ngày tạo giảm dần theo loại ngôn ngữ</returns>
        public List<ET_SanPham> LayDanhSach(string loaiSPFilter = null, string langCode = "vi-VN")
        {
            return _dal.LayDanhSach(loaiSPFilter, langCode).Select(MapToET).ToList();
        }

        /// <summary>
        /// Lấy đầy đủ chi tiết 1 sản phẩm bao gồm toàn bộ bảng con:
        /// BangGia, SanPham_Ve, MonAn, Ve_QuyenTruyCap.
        /// Dùng cho form chỉnh sửa (frmSanPham_Detail) khi cần hiển thị 4 tab.
        /// </summary>
        /// <param name="idSanPham">Id sản phẩm cần lấy chi tiết</param>
        /// <returns>ET_SanPham đầy đủ hoặc null nếu không tìm thấy</returns>
        public ET_SanPham LayChiTiet(int idSanPham)
        {
            var dal = _dal.LayChiTiet(idSanPham);
            return dal != null ? MapToET(dal) : null;
        }

        public List<ET_QuyDoiDonVi> LayQuyDoiTheoSanPham(int idSanPham)
        {
            return _dal.LayQuyDoiTheoSanPham(idSanPham).Select(q => new ET_QuyDoiDonVi
            {
                Id = q.Id, IdSanPham = q.IdSanPham,
                IdDonViGoc = q.IdDonViGoc, IdDonViDich = q.IdDonViDich,
                TyLeQuyDoi = q.TyLeQuyDoi, GiaBan = q.GiaBan
            }).ToList();
        }

        /// <summary>
        /// Lấy BOM (Định mức nguyên liệu) cho sản phẩm FnB.
        /// VD: 1 Ly Cafe Sữa cần 20g Cafe + 30ml Sữa đặc.
        /// </summary>
        public List<ET_DinhMucNguyenLieu> LayDinhMuc(int idThanhPham)
        {
            return _dal.LayDinhMuc(idThanhPham).Select(d => new ET_DinhMucNguyenLieu
            {
                Id = d.Id, IdThanhPham = d.IdThanhPham,
                IdNguyenLieu = d.IdNguyenLieu, SoLuong = d.SoLuong
            }).ToList();
        }

        //  Dữ liệu tra cứu (Lookup) 
        // Các hàm dưới đây dùng cho ComboBox trên form.
        // GUI gọi BUS, không gọi DAL trực tiếp.

        public List<ET_DonViTinh> LayDonViTinh(string langCode = "vi-VN")
        {
            return _dal.LayDonViTinh(langCode).Select(d => new ET_DonViTinh
            {
                Id = d.Id, MaDonVi = d.MaDonVi, TenDonVi = d.TenDonVi,
                ConHoatDong = d.ConHoatDong
            }).ToList();
        }

        public List<ET_DiemBanHang_POS> LayDiemBanPOS()
        {
            return _dal.LayDiemBanPOS().Select(d => new ET_DiemBanHang_POS
            {
                Id = d.Id, TenDiemBan = d.TenDiemBan, IdKhuVuc = d.IdKhuVuc
            }).ToList();
        }

        public List<ET_TroChoi> LayTroChoi()
        {
            return _dal.LayTroChoi().Select(t => new ET_TroChoi
            {
                Id = t.Id, TenTroChoi = t.TenTroChoi, IdKhuVuc = t.IdKhuVuc
            }).ToList();
        }

        public List<ET_NhaHang> LayNhaHang()
        {
            return _dal.LayNhaHang().Select(n => new ET_NhaHang
            {
                Id = n.Id, TenNhaHang = n.TenNhaHang, IdKhuVuc = n.IdKhuVuc
            }).ToList();
        }

        public List<ET_CauHinhThue> LayCauHinhThue(string langCode = "vi-VN")
        {
            return _dal.LayCauHinhThue(langCode).Select(c => new ET_CauHinhThue
            {
                Id = c.Id, MaThue = c.MaThue, TenThue = c.TenThue,
                TyLePhanTram = c.TyLePhanTram,
                ApDungChoLoaiSP = c.ApDungChoLoaiSP
            }).ToList();
        }

        /// <summary>
        /// Chỉ lấy SP có LaVatTu = true (dùng cho Combobox chọn nguyên liệu BOM).
        /// </summary>
        public List<ET_SanPham> LayVatTuKho()
        {
            return _dal.LayVatTuKho().Select(MapToET).ToList();
        }

        #endregion

        #region Thêm / Sửa / Xoá

        /// <summary>
        /// Lưu sản phẩm mới kèm toàn bộ bảng con (bảng giá, cấu hình vé, món ăn, định mức).
        /// Luôn kiểm tra trước khi lưu:
        /// B1: Rỗng trường bắt buộc (Mã, Tên, ĐVT, Loại SP)
        /// B2: Trùng mã sản phẩm trong DB
        /// B3: Nếu là Vé -> bắt buộc có ít nhất 1 cổng quẹt
        /// B4: Nếu là Cho Thuê -> bắt buộc có bảng giá kèm thông số block giờ
        /// B5: Gắn bảng con vào SanPham rồi đẩy xuống DAL
        /// </summary>
        /// <param name="sp">Đối tượng SanPham thu từ form</param>
        /// <param name="dsBangGia">Danh sách bảng giá</param>
        /// <param name="dsQuyenVe">Danh sách quyền cổng (null nếu không phải Vé)</param>
        /// <param name="cauHinhVe">Cấu hình vé (null nếu không phải Vé)</param>
        /// <param name="cauHinhMonAn">Cấu hình món ăn (null nếu không phải đồ ăn/uống)</param>
        /// <param name="dsDinhMuc">Danh sách định mức nguyên liệu (null nếu không phải đồ ăn/uống)</param>
        /// <returns>Thành công kèm Id mới hoặc thất bại kèm lý do</returns>
        public OperationResult ThemMoi(ET_SanPham sp,
            List<ET_BangGia> dsBangGia,
            List<ET_QuyDoiDonVi> dsQuyDoi,
            List<ET_Ve_QuyenTruyCap> dsQuyenVe,
            ET_SanPham_Ve cauHinhVe,
            ET_MonAn cauHinhMonAn,
            List<ET_DinhMucNguyenLieu> dsDinhMuc,
            ET_LoaiPhong loaiPhong,
            List<ET.Models.VanHanh.ET_VatTuPhongMacDinh> dsVatTu,
            List<ET_TaiSanChoThue> dsTaiSan)
        {
            var ketQua = KiemTraBatBuoc(sp);
            if (!ketQua.Success) return ketQua;

            if (_dal.KiemTraTrungMa(sp.MaSanPham))
                return OperationResult.Fail($"ERR_TRUNG_MASP|{sp.MaSanPham}");

            ketQua = KiemTraTheoLoai(sp.LoaiSanPham, dsQuyenVe, dsBangGia, dsQuyDoi);
            if (!ketQua.Success) return ketQua;

            ketQua = KiemTraChongLapBangGia(dsBangGia);
            if (!ketQua.Success) return ketQua;

            try
            {
                //  Map ET -> DAL 
                var dalSP = MapToDAL(sp);
                dalSP.DaXoa = false;
                dalSP.NgayTao = DateTime.Now;

                // Gắn bảng giá vào navigation property
                foreach (var bg in dsBangGia)
                    dalSP.BangGias.Add(MapBangGiaToDAL(bg));

                // Gắn cấu hình Vé (nếu có)
                if (cauHinhVe != null)
                {
                    dalSP.SanPham_Ve = new SanPham_Ve
                    {
                        LoaiVe = cauHinhVe.LoaiVe,
                        DoiTuongVe = cauHinhVe.DoiTuongVe,
                        CanTaoToken = cauHinhVe.CanTaoToken
                    };
                    // Gắn quyền truy cập cổng cho vé
                    if (dsQuyenVe != null)
                        foreach (var qv in dsQuyenVe)
                            dalSP.Ve_QuyenTruyCaps.Add(new Ve_QuyenTruyCap
                            {
                                IdKhuVuc = qv.IdKhuVuc,
                                IdTroChoi = qv.IdTroChoi,
                                SoLuotChoPhep = qv.SoLuotChoPhep,
                                GhiChu = qv.GhiChu
                            });
                }

                // Gắn cấu hình Món ăn + BOM (nếu có)
                if (cauHinhMonAn != null)
                {
                    dalSP.MonAn = new MonAn
                    {
                        IdNhaHang = cauHinhMonAn.IdNhaHang ?? 0,
                        CoDiUng = cauHinhMonAn.CoDiUng,
                        PhanLoai = cauHinhMonAn.PhanLoai,
                        MoTaNgan = cauHinhMonAn.MoTaNgan,
                        AnHienMenu = cauHinhMonAn.AnHienMenu
                    };
                    // Gắn BOM (pha chế cần gì, bao nhiêu)
                    if (dsDinhMuc != null)
                        foreach (var dm in dsDinhMuc)
                            dalSP.DinhMucNguyenLieus.Add(new DinhMucNguyenLieu
                            {
                                IdNguyenLieu = dm.IdNguyenLieu,
                                SoLuong = dm.SoLuong
                            });
                }

                using (var db = new DaiNamDBDataContext())
                {
                    db.Connection.Open();
                    using (var tx = db.Connection.BeginTransaction())
                    {
                        db.Transaction = tx;
                        try
                        {
                            int newId = _dal.ThemMoi(dalSP, dsQuyDoi?.Select(q => new QuyDoiDonVi
                            {
                                IdDonViGoc = sp.IdDonViTinh, 
                                IdDonViDich = q.IdDonViDich,
                                TyLeQuyDoi = q.TyLeQuyDoi,
                                GiaBan = q.GiaBan,
                                ConHoatDong = true
                            }).ToList(), db);

                            if (loaiPhong != null)
                            {
                                loaiPhong.Id = newId;
                                DAL_LoaiPhong.Instance.InsertOrUpdate(db, loaiPhong);
                            }

                            if (dsVatTu != null)
                            {
                                DAL.Repositories.VanHanh.DAL_VatTuPhongMacDinh.Instance.SyncVatTu(db, newId, dsVatTu);
                            }

                            if (dsTaiSan != null)
                            {
                                DAL_TaiSanChoThue.Instance.SyncTaiSan(db, newId, dsTaiSan);
                            }

                            db.SubmitChanges();
                            tx.Commit();
                            return OperationResult.Ok(newId, "MSG_ADD_SUCCESS");
                        }
                        catch (Exception ex)
                        {
                            tx.Rollback();
                            return OperationResult.Fail("Lỗi khi lưu: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("Lỗi hệ thống: " + ex.Message);
            }
        }

        /// <summary>
        /// Cập nhật sản phẩm đã tồn tại.
        /// Với các bảng con (bảng giá, quyền cổng, định mức), DAL sẽ xoá hết bản ghi cũ
        /// rồi thêm lại bản ghi mới — đảm bảo dữ liệu luôn đồng bộ với form.
        /// Kiểm tra nghiệp vụ giống ThemMoi (trùng mã, loại SP, chồng lấp bảng giá).
        /// </summary>
        /// <param name="spMoi">Đối tượng SanPham đã chỉnh sửa từ form</param>
        /// <param name="dsBangGia">Danh sách bảng giá (toàn bộ, không chỉ phần thay đổi)</param>
        /// <param name="dsQuyDoi">Danh sách quy đổi đơn vị</param>
        /// <param name="dsQuyenVe">Danh sách quyền cổng (null nếu không phải Vé)</param>
        /// <param name="cauHinhVe">Cấu hình vé (null nếu không phải Vé)</param>
        /// <param name="cauHinhMonAn">Cấu hình món ăn (null nếu không phải đồ ăn/uống)</param>
        /// <param name="dsDinhMuc">Danh sách định mức nguyên liệu (null nếu không phải đồ ăn/uống)</param>
        /// <returns>Thành công hoặc thất bại kèm lý do</returns>
        public OperationResult CapNhat(ET_SanPham spMoi,
            List<ET_BangGia> dsBangGia,
            List<ET_QuyDoiDonVi> dsQuyDoi,
            List<ET_Ve_QuyenTruyCap> dsQuyenVe,
            ET_SanPham_Ve cauHinhVe,
            ET_MonAn cauHinhMonAn,
            List<ET_DinhMucNguyenLieu> dsDinhMuc,
            ET_LoaiPhong loaiPhong,
            List<ET.Models.VanHanh.ET_VatTuPhongMacDinh> dsVatTu,
            List<ET_TaiSanChoThue> dsTaiSan)
        {
            var ketQua = KiemTraBatBuoc(spMoi);
            if (!ketQua.Success) return ketQua;

            if (_dal.KiemTraTrungMa(spMoi.MaSanPham, spMoi.Id))
                return OperationResult.Fail($"ERR_TRUNG_MASP|{spMoi.MaSanPham}");

            ketQua = KiemTraTheoLoai(spMoi.LoaiSanPham, dsQuyenVe, dsBangGia, dsQuyDoi);
            if (!ketQua.Success) return ketQua;

            ketQua = KiemTraChongLapBangGia(dsBangGia);
            if (!ketQua.Success) return ketQua;

            try
            {
                var dalSP = MapToDAL(spMoi);
                var dalBangGia = dsBangGia.Select(MapBangGiaToDAL).ToList();

                List<Ve_QuyenTruyCap> dalQuyenVe = dsQuyenVe?.Select(qv => new Ve_QuyenTruyCap
                {
                    IdKhuVuc = qv.IdKhuVuc,
                    IdTroChoi = qv.IdTroChoi,
                    SoLuotChoPhep = qv.SoLuotChoPhep,
                    GhiChu = qv.GhiChu
                }).ToList();

                SanPham_Ve dalCauHinhVe = cauHinhVe != null ? new SanPham_Ve
                {
                    LoaiVe = cauHinhVe.LoaiVe,
                    DoiTuongVe = cauHinhVe.DoiTuongVe,
                    CanTaoToken = cauHinhVe.CanTaoToken
                } : null;

                var dalMonAn = cauHinhMonAn == null ? null : new MonAn
                {
                    IdSanPham = spMoi.Id,
                    IdNhaHang = cauHinhMonAn.IdNhaHang ?? 0,
                    CoDiUng = cauHinhMonAn.CoDiUng,
                    PhanLoai = cauHinhMonAn.PhanLoai,
                    MoTaNgan = cauHinhMonAn.MoTaNgan,
                    AnHienMenu = cauHinhMonAn.AnHienMenu
                };
                var dalDinhMuc = dsDinhMuc?.Select(d => new DinhMucNguyenLieu
                {
                    IdThanhPham = spMoi.Id,
                    IdNguyenLieu = d.IdNguyenLieu,
                    SoLuong = d.SoLuong
                }).ToList();

                var dalQuyDoi = dsQuyDoi?.Select(q => new QuyDoiDonVi
                {
                    IdDonViGoc = spMoi.IdDonViTinh,
                    IdDonViDich = q.IdDonViDich,
                    TyLeQuyDoi = q.TyLeQuyDoi,
                    GiaBan = q.GiaBan,
                    ConHoatDong = true
                }).ToList();

                using (var db = new DaiNamDBDataContext())
                {
                    db.Connection.Open();
                    using (var tx = db.Connection.BeginTransaction())
                    {
                        db.Transaction = tx;
                        try
                        {
                            _dal.CapNhat(dalSP, dalBangGia, dalQuyenVe, dalCauHinhVe, dalMonAn, dalDinhMuc, dalQuyDoi, db);

                            if (loaiPhong != null)
                            {
                                loaiPhong.Id = spMoi.Id;
                                DAL_LoaiPhong.Instance.InsertOrUpdate(db, loaiPhong);
                            }

                            if (dsVatTu != null)
                            {
                                DAL.Repositories.VanHanh.DAL_VatTuPhongMacDinh.Instance.SyncVatTu(db, spMoi.Id, dsVatTu);
                            }

                            if (dsTaiSan != null)
                            {
                                DAL_TaiSanChoThue.Instance.SyncTaiSan(db, spMoi.Id, dsTaiSan);
                            }

                            db.SubmitChanges();
                            tx.Commit();
                            return OperationResult.Ok("MSG_UPDATE_SUCCESS");
                        }
                        catch (Exception ex)
                        {
                            tx.Rollback();
                            return OperationResult.Fail("Lỗi khi cập nhật: " + ex.Message);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("Lỗi hệ thống: " + ex.Message);
            }
        }

        /// <summary>
        /// Xoá mềm sản phẩm (đặt DaXoa = true, không xoá vật lý trong DB).
        /// Từ chối xoá nếu:
        /// - Đang có đơn hàng hoặc phiếu kho (chứng từ) ở trạng thái chưa hoàn tất
        /// - Sản phẩm là vật tư (LaVatTu = true) và vẫn còn tồn kho thực tế lớn hơn 0
        /// </summary>
        /// <param name="idSanPham">Id sản phẩm cần xoá mềm</param>
        /// <returns>Thành công hoặc thất bại kèm lý do cụ thể</returns>
        public OperationResult XoaMem(int idSanPham)
        {
            var dal = _dal.LayChiTiet(idSanPham);
            if (dal == null)
                return OperationResult.Fail("Không tìm thấy sản phẩm.");

            if (_dal.CoDonHangHoacChungTuTreo(idSanPham))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CONDONHANG);

            if (dal.LaVatTu && _dal.CoTonKho(idSanPham))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_CONTONKHO);

            try
            {
                _dal.XoaMem(idSanPham);
                return OperationResult.Ok("Xoá sản phẩm thành công.");
            }
            catch (Exception ex)
            {
                return OperationResult.Fail("Lỗi khi xoá: " + ex.Message);
            }
        }

        #endregion

        #region Nghiệp vụ kiểm tra

        /// <summary>
        /// Kiểm tra các trường bắt buộc chung — áp dụng cho mọi loại sản phẩm.
        /// Kiểm tra: Mã SP, Tên SP, Loại SP, Đơn vị tính.
        /// </summary>
        /// <param name="sp">Đối tượng ET_SanPham cần kiểm tra</param>
        /// <returns>OperationResult.Ok() nếu hợp lệ, Fail kèm lý do nếu thiếu</returns>
        private static readonly string[] _anhHopLe = { ".jpg", ".jpeg", ".png", ".gif", ".webp" };

        private OperationResult KiemTraBatBuoc(ET_SanPham sp)
        {
            if (string.IsNullOrWhiteSpace(sp.MaSanPham))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_REQUIRED_MASP);

            if (string.IsNullOrWhiteSpace(sp.TenSanPham))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_REQUIRED_TENSP);

            if (string.IsNullOrWhiteSpace(sp.LoaiSanPham))
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_REQUIRED_LOAISP);

            if (sp.IdDonViTinh <= 0)
                return OperationResult.Fail(AppConstants.ErrorMessages.ERR_REQUIRED_DVT);

            if (!string.IsNullOrWhiteSpace(sp.AnhDaiDien))
            {
                string ext = System.IO.Path.GetExtension(sp.AnhDaiDien)?.ToLowerInvariant();
                if (!_anhHopLe.Contains(ext))
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_ANH_SAIDINHDANG);
            }

            return OperationResult.Ok();
        }

        /// <summary>
        /// Kiểm tra nghiệp vụ đặc thù theo từng loại sản phẩm.
        /// Mỗi nhóm có quy tắc riêng:
        /// - Vé (VeVaoKhu, VeTroChoi): bắt buộc ít nhất 1 cổng quẹt thẻ RFID
        /// - Cho Thuê (TuDo, DoChoThue, ChoiNghiMat): bắt buộc bảng giá kèm block giờ
        /// </summary>
        /// <param name="loaiSP">Mã loại sản phẩm (lấy từ lớp hằng số LoaiSanPham)</param>
        /// <param name="dsQuyenVe">Danh sách quyền truy cập cổng (chỉ dùng cho Vé)</param>
        /// <param name="dsBangGia">Danh sách bảng giá (chỉ kiểm tra cho nhóm Cho Thuê)</param>
        /// <returns>Ok nếu đạt, Fail kèm lý do nếu vi phạm</returns>
        private OperationResult KiemTraTheoLoai(string loaiSP,
            List<ET_Ve_QuyenTruyCap> dsQuyenVe,
            List<ET_BangGia> dsBangGia,
            List<ET_QuyDoiDonVi> dsQuyDoi)
        {
            if (dsQuyDoi != null)
            {
                dsQuyDoi.RemoveAll(q => q.IdDonViDich <= 0);
                
                if (dsQuyDoi.Any(q => q.TyLeQuyDoi <= 0))
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_HESO_KHONGHOPLE);
                    
                if (dsQuyDoi.Any(q => q.GiaBan < 0))
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_GIABAN_AMHOAC0);

                if (dsQuyDoi.GroupBy(q => q.IdDonViDich).Any(g => g.Count() > 1))
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_DUP_UNIT_GRID);
            }

            bool laVe = loaiSP == AppConstants.LoaiSanPham.VeVaoKhu || loaiSP == AppConstants.LoaiSanPham.VeTroChoi;
            if (laVe)
            {
                if (dsQuyenVe == null || dsQuyenVe.Count == 0)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_LUOI_CONG_RONG);
                    
                if (dsQuyenVe.Count > 1 && loaiSP == AppConstants.LoaiSanPham.VeTroChoi)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_LUOI_VE_QUA1DONG);
            }

            bool laChoThue = loaiSP == AppConstants.LoaiSanPham.TuDo
                          || loaiSP == AppConstants.LoaiSanPham.DoChoThue
                          || loaiSP == AppConstants.LoaiSanPham.ChoiNghiMat;
            if (laChoThue)
            {
                if (dsBangGia == null || dsBangGia.Count == 0)
                    return OperationResult.Fail(AppConstants.ErrorMessages.ERR_RENTAL_NO_PRICE);
            }

            return OperationResult.Ok();
        }

        /// <summary>
        /// Kiểm tra bảng giá cùng loại không được chồng lấp thời gian hiệu lực.
        /// VD: Giá 'MacDinh' từ 01/01 đến 30/06, dòng kế phải bắt đầu SAU 30/06.
        /// Nếu dòng trước kết thúc 30/06 mà dòng sau bắt đầu 15/06 -> chồng lấp -> từ chối.
        /// 
        /// Lưu ý: DB cũng có trigger chặn ở mức SQL,
        /// nhưng kiểm tra ở BUS trước để báo lỗi rõ ràng bằng tiếng Việt cho người dùng
        /// (thay vì thông báo lỗi SQL chung chung).
        /// </summary>
        /// <param name="dsBangGia">Danh sách bảng giá cần kiểm tra</param>
        /// <returns>Ok nếu không chồng lấp, Fail kèm chi tiết dòng vi phạm</returns>
        private OperationResult KiemTraChongLapBangGia(List<ET_BangGia> dsBangGia)
        {
            if (dsBangGia == null || dsBangGia.Count <= 1)
                return OperationResult.Ok();

            var nhomTheoLoai = dsBangGia.GroupBy(bg => bg.LoaiGia);
            foreach (var nhom in nhomTheoLoai)
            {
                var dsSapXep = nhom.OrderBy(bg => bg.HieuLucTu).ToList();
                for (int i = 0; i < dsSapXep.Count; i++)
                {
                    if (dsSapXep[i].HieuLucDen < dsSapXep[i].HieuLucTu)
                        return OperationResult.Fail(AppConstants.ErrorMessages.ERR_NGAY_KHONGHOPLE);
                        
                    if (i < dsSapXep.Count - 1 && dsSapXep[i].HieuLucDen > dsSapXep[i + 1].HieuLucTu)
                    {
                        return OperationResult.Fail($"ERR_PRICE_OVERLAP|{nhom.Key}|{dsSapXep[i].HieuLucDen:dd/MM/yyyy HH:mm}|{dsSapXep[i + 1].HieuLucTu:dd/MM/yyyy HH:mm}");
                    }
                }
            }

            return OperationResult.Ok();
        }

        #endregion
    }
}


