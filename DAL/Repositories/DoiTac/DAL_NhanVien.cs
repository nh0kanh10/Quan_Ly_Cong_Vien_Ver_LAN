using System;
using System.Collections.Generic;
using System.Linq;
using ET.Models.DoiTac;

namespace DAL.Repositories.DoiTac
{
    public class DAL_NhanVien
    {
        #region Khởi tạo (Singleton)
        public static DAL_NhanVien Instance { get; } = new DAL_NhanVien();
        #endregion

        #region Truy vấn dữ liệu

        public List<DTO_NhanVienChiTiet> LayDanhSach(string tuKhoa = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = from nv in db.NhanViens
                            join tt in db.ThongTins on nv.IdDoiTac equals tt.Id
                            where tt.DaXoa == false
                            
                            join vt in db.VaiTros on nv.IdVaiTro equals vt.Id into vtGroup
                            from vt in vtGroup.DefaultIfEmpty()
                            
                            join kv in db.KhuVucs on nv.IdKhuVuc equals kv.Id into kvGroup
                            from kv in kvGroup.DefaultIfEmpty()
                            
                            select new { nv, tt, vt, kv };

                if (!string.IsNullOrWhiteSpace(tuKhoa))
                {
                    tuKhoa = tuKhoa.Trim();
                    query = query.Where(x =>
                        x.tt.HoTen.Contains(tuKhoa) ||
                        x.tt.DienThoai.Contains(tuKhoa) ||
                        x.nv.MaNhanVien.Contains(tuKhoa));
                }

                return query.OrderByDescending(x => x.tt.NgayTao)
                    .Select(x => new DTO_NhanVienChiTiet
                    {
                        IdDoiTac = x.nv.IdDoiTac,
                        MaNhanVien = x.nv.MaNhanVien,
                        HoTen = x.tt.HoTen,
                        DienThoai = x.tt.DienThoai,
                        ChucVu = x.nv.ChucVu,
                        TrangThai = x.nv.TrangThai,
                        TenVaiTro = x.vt != null ? x.vt.TenVaiTro : null,
                        TenKhuVuc = x.kv != null ? x.kv.TenKhuVuc : null,
                        NgayTao = x.tt.NgayTao
                    }).ToList();
            }
        }

        public DTO_NhanVienChiTiet LayChiTiet(int idDoiTac)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return LayChiTietQuery(db, idDoiTac, null);
            }
        }

        public DTO_NhanVienChiTiet LayChiTiet(string maNhanVien)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return LayChiTietQuery(db, null, maNhanVien);
            }
        }

        private DTO_NhanVienChiTiet LayChiTietQuery(DaiNamDBDataContext db, int? idDoiTac, string maNhanVien)
        {
            return (from nv in db.NhanViens
                    join tt in db.ThongTins on nv.IdDoiTac equals tt.Id
                    join kv in db.KhuVucs on nv.IdKhuVuc equals kv.Id into kvGroup
                    from kv in kvGroup.DefaultIfEmpty()
                    where tt.DaXoa == false
                          && (idDoiTac.HasValue ? nv.IdDoiTac == idDoiTac.Value : nv.MaNhanVien == maNhanVien)
                    select new DTO_NhanVienChiTiet
                    {
                        IdDoiTac = nv.IdDoiTac,
                        MaNhanVien = nv.MaNhanVien,
                        HoTen = tt.HoTen,
                        DienThoai = tt.DienThoai,
                        Email = tt.Email,
                        Cccd = tt.Cccd,
                        DiaChi = tt.DiaChi,
                        HinhAnh = tt.HinhAnh,
                        NgayTao = tt.NgayTao,
                        NgayVaoLam = tt.NgayTao,
                        IdVaiTro = nv.IdVaiTro,
                        IdKhuVuc = nv.IdKhuVuc,
                        IdNguoiQuanLy = nv.IdNguoiQuanLy,
                        ChucVu = nv.ChucVu,
                        GioiTinh = nv.GioiTinh,
                        NgaySinh = nv.NgaySinh,
                        LoaiKhoi = nv.LoaiKhoi,
                        LoaiHopDong = nv.LoaiHopDong,
                        NhomCongViec = nv.NhomCongViec,
                        LuongCoBan = nv.LuongCoBan,
                        LuongTheoGio = nv.LuongTheoGio,
                        TrangThai = nv.TrangThai,
                        TenKhuVuc = kv != null ? kv.TenKhuVuc : null,
                        PhongBan = kv != null ? kv.TenKhuVuc : null
                    }).FirstOrDefault();
        }

        #endregion

        #region Thêm / Sửa / Xoá

        public int Them(DTO_NhanVienChiTiet dto)
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
                        LoaiDoiTac = ET.Constants.AppConstants.LoaiDoiTac.NhanVien,
                        DaXoa = false,
                        NgayTao = DateTime.Now
                    };
                    db.ThongTins.InsertOnSubmit(thongTin);
                    db.SubmitChanges();

                    var nhanVien = new NhanVien
                    {
                        IdDoiTac = thongTin.Id,
                        MaNhanVien = TaoMaNhanVien(),
                        IdVaiTro = dto.IdVaiTro,
                        IdKhuVuc = dto.IdKhuVuc,
                        IdNguoiQuanLy = dto.IdNguoiQuanLy,
                        ChucVu = dto.ChucVu,
                        GioiTinh = dto.GioiTinh ?? "Khac",
                        NgaySinh = dto.NgaySinh,
                        LoaiKhoi = dto.LoaiKhoi ?? "VanHanh",
                        LoaiHopDong = dto.LoaiHopDong ?? "ThuViec",
                        NhomCongViec = dto.NhomCongViec,
                        LuongCoBan = dto.LuongCoBan ?? 0,
                        LuongTheoGio = dto.LuongTheoGio ?? 0,
                        TrangThai = dto.TrangThai ?? "DangLamViec"
                    };
                    db.NhanViens.InsertOnSubmit(nhanVien);
                    db.SubmitChanges();

                    tx.Complete();
                    return thongTin.Id;
                }
            }
        }

        public void CapNhat(DTO_NhanVienChiTiet dto)
        {
            using (var db = new DaiNamDBDataContext())
            {
                using (var tx = new System.Transactions.TransactionScope())
                {
                    var tt = db.ThongTins.FirstOrDefault(t => t.Id == dto.IdDoiTac);
                    if (tt != null)
                    {
                        tt.HoTen = dto.HoTen;
                        tt.DienThoai = dto.DienThoai;
                        tt.Email = dto.Email;
                        tt.Cccd = dto.Cccd;
                        tt.DiaChi = dto.DiaChi;
                        tt.CapNhatLuc = DateTime.Now;
                    }

                    var nv = db.NhanViens.FirstOrDefault(k => k.IdDoiTac == dto.IdDoiTac);
                    if (nv != null)
                    {
                        nv.IdVaiTro = dto.IdVaiTro;
                        nv.IdKhuVuc = dto.IdKhuVuc;
                        nv.IdNguoiQuanLy = dto.IdNguoiQuanLy;
                        nv.ChucVu = dto.ChucVu;
                        nv.GioiTinh = dto.GioiTinh;
                        nv.NgaySinh = dto.NgaySinh;
                        nv.LoaiKhoi = dto.LoaiKhoi;
                        nv.LoaiHopDong = dto.LoaiHopDong;
                        nv.NhomCongViec = dto.NhomCongViec;
                        nv.LuongCoBan = dto.LuongCoBan;
                        nv.LuongTheoGio = dto.LuongTheoGio;
                        nv.TrangThai = dto.TrangThai;
                    }

                    db.SubmitChanges();
                    tx.Complete();
                }
            }
        }

        public void XoaMem(int idDoiTac)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var tt = db.ThongTins.FirstOrDefault(t => t.Id == idDoiTac);
                if (tt != null)
                {
                    tt.DaXoa = true;
                    tt.CapNhatLuc = DateTime.Now;
                    db.SubmitChanges();
                }
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        public string TaoMaNhanVien()
        {
            using (var db = new DaiNamDBDataContext())
            {
                try
                {
                    string rs = db.ExecuteQuery<string>("EXEC HeThong.sp_GetNextSeq_NhanVien").FirstOrDefault();
                    if (!string.IsNullOrEmpty(rs)) return "NV" + rs.PadLeft(5, '0');
                }
                catch { }

                string maLonNhat = db.NhanViens
                    .Select(k => k.MaNhanVien)
                    .Where(m => m.StartsWith("NV"))
                    .OrderByDescending(m => m)
                    .FirstOrDefault();

                int soTiepTheo = 1;
                if (!string.IsNullOrEmpty(maLonNhat))
                {
                    int.TryParse(maLonNhat.Substring(2), out soTiepTheo);
                    soTiepTheo++;
                }

                return "NV" + soTiepTheo.ToString("D5");
            }
        }

        public bool KiemTraTrungSDT(string dienThoai, int? excludeId = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = from nv in db.NhanViens
                            join tt in db.ThongTins on nv.IdDoiTac equals tt.Id
                            where tt.DienThoai == dienThoai && tt.DaXoa == false
                            select tt.Id;

                if (excludeId.HasValue)
                    query = query.Where(id => id != excludeId.Value);

                return query.Any();
            }
        }

        #endregion
    }
}


