using System;
using System.Collections.Generic;
using System.Linq;
using ET.Models.DoiTac;

namespace DAL.Repositories.DoiTac
{
    public class DAL_KhachHang
    {
        #region Khởi tạo (Singleton)

        public static DAL_KhachHang Instance { get; } = new DAL_KhachHang();

        #endregion

        #region Truy vấn dữ liệu

        public List<DTO_KhachHangChiTiet> LayDanhSach(string tuKhoa = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = from kh in db.KhachHangs
                            join tt in db.ThongTins on kh.IdDoiTac equals tt.Id
                            where tt.DaXoa == false
                            select new { kh, tt };

                if (!string.IsNullOrWhiteSpace(tuKhoa))
                {
                    tuKhoa = tuKhoa.Trim();
                    query = query.Where(x =>
                        x.tt.HoTen.Contains(tuKhoa) ||
                        x.tt.DienThoai.Contains(tuKhoa) ||
                        (x.tt.Cccd != null && x.tt.Cccd.Contains(tuKhoa)) ||
                        x.kh.MaKhachHang.Contains(tuKhoa));
                }

                return query.OrderByDescending(x => x.tt.NgayTao)
                    .Select(x => new DTO_KhachHangChiTiet
                    {
                        IdDoiTac = x.kh.IdDoiTac,
                        MaKhachHang = x.kh.MaKhachHang,
                        HoTen = x.tt.HoTen,
                        DienThoai = x.tt.DienThoai,
                        LoaiKhach = x.kh.LoaiKhach,
                        HangThanhVien = x.kh.HangThanhVien,
                        NgayTao = x.tt.NgayTao
                    }).ToList();
            }
        }

        /// <summary>
        /// Lấy chi tiết khách hàng: thông tin cơ sở + số dư ví + điểm + trạng thái thẻ + tổng chi tiêu.
        /// Gộp 5 bảng: ThongTin, KhachHang, ViDienTu(SoCaiVi), TheRFID, DonHang.
        /// </summary>
        public DTO_KhachHangChiTiet LayChiTiet(int idDoiTac)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var kh = db.KhachHangs.FirstOrDefault(k => k.IdDoiTac == idDoiTac);
                if (kh == null) return null;

                var tt = db.ThongTins.FirstOrDefault(t => t.Id == idDoiTac && t.DaXoa == false);
                if (tt == null) return null;

                var vi = db.ViDienTus.FirstOrDefault(v => v.IdKhachHang == idDoiTac);
                decimal soDuVi = 0;
                bool coVi = false;
                if (vi != null)
                {
                    coVi = true;
                    decimal tongCong = db.SoCaiVis.Where(s => s.IdVi == vi.Id && s.LoaiPhep == ET.Constants.AppConstants.LoaiPhepVi.Cong).Sum(s => (decimal?)s.SoTien) ?? 0;
                    decimal tongTru = db.SoCaiVis.Where(s => s.IdVi == vi.Id && s.LoaiPhep == ET.Constants.AppConstants.LoaiPhepVi.Tru).Sum(s => (decimal?)s.SoTien) ?? 0;
                    soDuVi = tongCong - tongTru;
                }

                var theRfid = db.TheRFIDs
                    .Where(t => t.IdKhachHang == idDoiTac && t.TrangThai != ET.Constants.AppConstants.TrangThaiThe.DaTra)
                    .OrderByDescending(t => t.NgayTao)
                    .FirstOrDefault();

                int diemTichLuy = db.LichSuDiems
                    .Where(ld => ld.IdKhachHang == idDoiTac)
                    .OrderByDescending(ld => ld.NgayTao)
                    .Select(ld => ld.SoDuSauGD)
                    .FirstOrDefault();

                decimal tongChiTieu = db.DonHangs
                    .Where(d => d.IdKhachHang == idDoiTac && d.TrangThai != ET.Constants.AppConstants.TrangThaiDonHang.DaHuy)
                    .Sum(d => (decimal?)d.TongThanhToan) ?? 0;

                return new DTO_KhachHangChiTiet
                {
                    IdDoiTac = idDoiTac,
                    HoTen = tt.HoTen,
                    DienThoai = tt.DienThoai,
                    Email = tt.Email,
                    Cccd = tt.Cccd,
                    DiaChi = tt.DiaChi,
                    HinhAnh = tt.HinhAnh,
                    NgayTao = tt.NgayTao,
                    MaKhachHang = kh.MaKhachHang,
                    LoaiKhach = kh.LoaiKhach,
                    HangThanhVien = kh.HangThanhVien,
                    IdDoanKhach = kh.IdDoanKhach,
                    SoDuVi = soDuVi,
                    CoViDienTu = coVi,
                    DiemTichLuy = diemTichLuy,
                    TrangThaiThe = theRfid?.TrangThai,
                    MaTheRFID = theRfid?.MaThe,
                    TongChiTieu = tongChiTieu
                };
            }
        }

        /// <summary>
        /// Tìm khách hàng theo SĐT hoặc MaKhachHang.
        /// JOIN ThongTin lấy tên + SĐT, LEFT JOIN TuDien + BanDich để dịch hạng thành viên.
        /// </summary>
        public ET.DTOs.DTO_KhachHangPOS TimKhachHang(string tuKhoa, string langCode = "vi-VN")
        {
            if (string.IsNullOrWhiteSpace(tuKhoa)) return null;

            using (var db = new DaiNamDBDataContext())
            {
                if (!db.DatabaseExists()) return null;

                var result = (from kh in db.KhachHangs
                              join tt in db.ThongTins on kh.IdDoiTac equals tt.Id
                              where tt.DaXoa == false
                                    && (tt.DienThoai == tuKhoa || kh.MaKhachHang == tuKhoa)

                              join tdTmp in db.TuDiens
                              on new { Nhom = ET.Constants.AppConstants.NhomTuDien.HangThanhVien, Ma = kh.HangThanhVien }
                              equals new { Nhom = tdTmp.NhomMa, Ma = tdTmp.Ma } into tdGroup
                              from td in tdGroup.DefaultIfEmpty()

                              join bdTmp in db.BanDiches
                              on new { Loai = "TuDien_" + ET.Constants.AppConstants.NhomTuDien.HangThanhVien, Id = (td != null ? td.ThuTu : 0), NgonNgu = langCode, Cot = "NhanHienThi" }
                              equals new { Loai = bdTmp.LoaiThucThe, Id = bdTmp.IdThucThe, NgonNgu = bdTmp.NgonNgu, Cot = bdTmp.TruongDich } into bdGroup
                              from bd in bdGroup.DefaultIfEmpty()

                              select new ET.DTOs.DTO_KhachHangPOS
                              {
                                  IdDoiTac = kh.IdDoiTac,
                                  MaKhachHang = kh.MaKhachHang,
                                  HoTen = tt.HoTen,
                                  DienThoai = tt.DienThoai,
                                  LoaiKhach = kh.LoaiKhach,
                                  HangThanhVien = kh.HangThanhVien,
                                  TenHang = (langCode != "vi-VN" && bd != null) ? bd.NoiDung : (td != null ? td.NhanHienThi : kh.HangThanhVien),
                                  DiemTichLuy = db.LichSuDiems.Where(ld => ld.IdKhachHang == kh.IdDoiTac).Sum(ld => (int?)ld.SoDiem) ?? 0,
                                  TongChiTieu = db.DonHangs.Where(d => d.IdKhachHang == kh.IdDoiTac && d.TrangThai != ET.Constants.AppConstants.TrangThaiDonHang.DaHuy).Sum(d => (decimal?)d.TongThanhToan) ?? 0
                              }).FirstOrDefault();

                return result;
            }
        }

        public ET.DTOs.DTO_KhachHangPOS TimTheoRFID(string maThe, string langCode = "vi-VN")
        {
            if (string.IsNullOrWhiteSpace(maThe)) return null;

            using (var db = new DaiNamDBDataContext())
            {
                if (!db.DatabaseExists()) return null;

                var result = (from the in db.TheRFIDs
                              join kh in db.KhachHangs on the.IdKhachHang equals kh.IdDoiTac
                              join tt in db.ThongTins on kh.IdDoiTac equals tt.Id
                              where the.MaThe == maThe
                                    && the.TrangThai == ET.Constants.AppConstants.TrangThaiHieuLuc.HoatDong
                                    && tt.DaXoa == false

                              join tdTmp in db.TuDiens
                              on new { Nhom = ET.Constants.AppConstants.NhomTuDien.HangThanhVien, Ma = kh.HangThanhVien }
                              equals new { Nhom = tdTmp.NhomMa, Ma = tdTmp.Ma } into tdGroup
                              from td in tdGroup.DefaultIfEmpty()

                              join bdTmp in db.BanDiches
                              on new { Loai = "TuDien_" + ET.Constants.AppConstants.NhomTuDien.HangThanhVien, Id = (td != null ? td.ThuTu : 0), NgonNgu = langCode, Cot = "NhanHienThi" }
                              equals new { Loai = bdTmp.LoaiThucThe, Id = bdTmp.IdThucThe, NgonNgu = bdTmp.NgonNgu, Cot = bdTmp.TruongDich } into bdGroup
                              from bd in bdGroup.DefaultIfEmpty()

                              select new ET.DTOs.DTO_KhachHangPOS
                              {
                                  IdDoiTac = kh.IdDoiTac,
                                  MaKhachHang = kh.MaKhachHang,
                                  HoTen = tt.HoTen,
                                  DienThoai = tt.DienThoai,
                                  LoaiKhach = kh.LoaiKhach,
                                  HangThanhVien = kh.HangThanhVien,
                                  TenHang = (langCode != "vi-VN" && bd != null) ? bd.NoiDung : (td != null ? td.NhanHienThi : kh.HangThanhVien),
                                  DiemTichLuy = db.LichSuDiems.Where(ld => ld.IdKhachHang == kh.IdDoiTac).Sum(ld => (int?)ld.SoDiem) ?? 0,
                                  TongChiTieu = db.DonHangs.Where(d => d.IdKhachHang == kh.IdDoiTac && d.TrangThai != ET.Constants.AppConstants.TrangThaiDonHang.DaHuy).Sum(d => (decimal?)d.TongThanhToan) ?? 0
                              }).FirstOrDefault();

                return result;
            }
        }

        public ET.DTOs.DTO_KhachHangPOS LayThongTinKhachHang(int idKhachHang, string langCode = "vi-VN")
        {
            if (idKhachHang <= 0) return null;

            using (var db = new DaiNamDBDataContext())
            {
                if (!db.DatabaseExists()) return null;

                var result = (from kh in db.KhachHangs
                              join tt in db.ThongTins on kh.IdDoiTac equals tt.Id
                              where kh.IdDoiTac == idKhachHang
                                    && tt.DaXoa == false

                              join tdTmp in db.TuDiens
                              on new { Nhom = ET.Constants.AppConstants.NhomTuDien.HangThanhVien, Ma = kh.HangThanhVien }
                              equals new { Nhom = tdTmp.NhomMa, Ma = tdTmp.Ma } into tdGroup
                              from td in tdGroup.DefaultIfEmpty()

                              join bdTmp in db.BanDiches
                              on new { Loai = "TuDien_" + ET.Constants.AppConstants.NhomTuDien.HangThanhVien, Id = (td != null ? td.ThuTu : 0), NgonNgu = langCode, Cot = "NhanHienThi" }
                              equals new { Loai = bdTmp.LoaiThucThe, Id = bdTmp.IdThucThe, NgonNgu = bdTmp.NgonNgu, Cot = bdTmp.TruongDich } into bdGroup
                              from bd in bdGroup.DefaultIfEmpty()

                              select new ET.DTOs.DTO_KhachHangPOS
                              {
                                  IdDoiTac = kh.IdDoiTac,
                                  MaKhachHang = kh.MaKhachHang,
                                  HoTen = tt.HoTen,
                                  DienThoai = tt.DienThoai,
                                  LoaiKhach = kh.LoaiKhach,
                                  HangThanhVien = kh.HangThanhVien,
                                  TenHang = (langCode != "vi-VN" && bd != null) ? bd.NoiDung : (td != null ? td.NhanHienThi : kh.HangThanhVien),
                                  DiemTichLuy = db.LichSuDiems.Where(ld => ld.IdKhachHang == kh.IdDoiTac).Sum(ld => (int?)ld.SoDiem) ?? 0,
                                  TongChiTieu = db.DonHangs.Where(d => d.IdKhachHang == kh.IdDoiTac && d.TrangThai != ET.Constants.AppConstants.TrangThaiDonHang.DaHuy).Sum(d => (decimal?)d.TongThanhToan) ?? 0
                              }).FirstOrDefault();

                return result;
            }
        }

        #endregion

        #region Thêm / Sửa / Xoá

        /// <summary>
        /// Thêm khách hàng mới: INSERT cả 2 bảng ThongTin + KhachHang trong 1 Transaction.
        /// B1: INSERT ThongTin (lấy Id tự tăng).
        /// B2: INSERT KhachHang dùng Id vừa tạo làm PK (quan hệ 1-1).
        /// </summary>
        public int Them(DTO_KhachHangChiTiet dto)
        {
            using (var db = new DaiNamDBDataContext())
            {
                using (var tx = new System.Transactions.TransactionScope())
                {
                    var thongTin = new ThongTin
                    {
                        HoTen = dto.HoTen,
                        DienThoai = dto.DienThoai,
                        Email = dto.Email,
                        Cccd = dto.Cccd,
                        DiaChi = dto.DiaChi,
                        LoaiDoiTac = ET.Constants.AppConstants.LoaiDoiTac.CaNhan,
                        DaXoa = false,
                        NgayTao = DateTime.Now
                    };
                    db.ThongTins.InsertOnSubmit(thongTin);
                    db.SubmitChanges();

                    var khachHang = new KhachHang
                    {
                        IdDoiTac = thongTin.Id,
                        MaKhachHang = dto.MaKhachHang,
                        LoaiKhach = dto.LoaiKhach ?? ET.Constants.AppConstants.LoaiKhach.CaNhan,
                        HangThanhVien = dto.HangThanhVien ?? ET.Constants.AppConstants.HangThanhVien.Bac
                    };
                    db.KhachHangs.InsertOnSubmit(khachHang);
                    db.SubmitChanges();

                    tx.Complete();
                    return thongTin.Id;
                }
            }
        }

        /// <summary>
        /// Cập nhật thông tin: UPDATE cả 2 bảng ThongTin + KhachHang trong 1 Transaction.
        /// </summary>
        public void CapNhat(DTO_KhachHangChiTiet dto)
        {
            using (var db = new DaiNamDBDataContext())
            {
                using (var tx = new System.Transactions.TransactionScope())
                {
                    var tt = db.ThongTins.First(t => t.Id == dto.IdDoiTac);
                    tt.HoTen = dto.HoTen;
                    tt.DienThoai = dto.DienThoai;
                    tt.Email = dto.Email;
                    tt.Cccd = dto.Cccd;
                    tt.DiaChi = dto.DiaChi;
                    tt.CapNhatLuc = DateTime.Now;

                    var kh = db.KhachHangs.First(k => k.IdDoiTac == dto.IdDoiTac);
                    kh.LoaiKhach = dto.LoaiKhach;
                    kh.HangThanhVien = dto.HangThanhVien;

                    db.SubmitChanges();
                    tx.Complete();
                }
            }
        }

        public void XoaMem(int idDoiTac)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var tt = db.ThongTins.First(t => t.Id == idDoiTac);
                tt.DaXoa = true;
                tt.CapNhatLuc = DateTime.Now;
                db.SubmitChanges();
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        public bool KiemTraTrungSDT(string dienThoai, int? excludeId = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = from kh in db.KhachHangs
                            join tt in db.ThongTins on kh.IdDoiTac equals tt.Id
                            where tt.DienThoai == dienThoai && tt.DaXoa == false
                            select tt.Id;

                if (excludeId.HasValue)
                    query = query.Where(id => id != excludeId.Value);

                return query.Any();
            }
        }

        public bool KiemTraTrungCCCD(string cccd, int? excludeId = null)
        {
            if (string.IsNullOrWhiteSpace(cccd)) return false;

            using (var db = new DaiNamDBDataContext())
            {
                var query = from kh in db.KhachHangs
                            join tt in db.ThongTins on kh.IdDoiTac equals tt.Id
                            where tt.Cccd == cccd && tt.DaXoa == false
                            select tt.Id;

                if (excludeId.HasValue)
                    query = query.Where(id => id != excludeId.Value);

                return query.Any();
            }
        }

        public string TaoMaKhachHang()
        {
            using (var db = new DaiNamDBDataContext())
            {
                string maLonNhat = db.KhachHangs
                    .Select(k => k.MaKhachHang)
                    .OrderByDescending(m => m)
                    .FirstOrDefault();

                int soTiepTheo = 1;
                if (!string.IsNullOrEmpty(maLonNhat) && maLonNhat.StartsWith("KH"))
                {
                    int.TryParse(maLonNhat.Substring(2), out soTiepTheo);
                    soTiepTheo++;
                }

                return "KH" + soTiepTheo.ToString("D5");
            }
        }

        #endregion
    }
}


