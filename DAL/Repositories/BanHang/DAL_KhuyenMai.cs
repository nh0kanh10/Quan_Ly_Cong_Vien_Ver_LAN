using System;
using System.Collections.Generic;
using System.Linq;
using ET.DTOs;

namespace DAL.Repositories.BanHang
{
    public class DAL_KhuyenMai
    {
        public static DAL_KhuyenMai Instance { get; } = new DAL_KhuyenMai();

        /// Tìm KM active theo MaKhuyenMai (quét mã manual).
        public DTO_KhuyenMaiPOS TimTheoMa(string maKM)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var now = DateTime.Now;
                var km = db.KhuyenMais
                    .Where(k => k.MaKhuyenMai == maKM
                             && k.TrangThai == true
                             && k.DaXoa == false
                             && k.NgayBatDau <= now
                             && k.NgayKetThuc >= now)
                    .Select(k => new DTO_KhuyenMaiPOS
                    {
                        Id = k.Id,
                        MaKhuyenMai = k.MaKhuyenMai,
                        TenKhuyenMai = k.TenKhuyenMai,
                        LoaiGiamGia = k.LoaiGiamGia,
                        GiaTriGiam = k.GiaTriGiam,
                        DonToiThieu = k.DonToiThieu ?? 0,
                        CoChongCheo = k.CoChongCheo,
                        SoLanToiDa = k.SoLanToiDa,
                        TrangThai = k.TrangThai,
                        NgayBatDau = k.NgayBatDau,
                        NgayKetThuc = k.NgayKetThuc,
                        SoLanDaDung = db.KhuyenMai_LichSus.Count(ls => ls.IdKhuyenMai == k.Id)
                    })
                    .FirstOrDefault();

                if (km == null) return null;

                km.DieuKiens = LayDieuKienTheoKM(db, km.Id);
                return km;
            }
        }

        /// Lấy tất cả KM đang active (auto-promotion).
        public List<DTO_KhuyenMaiPOS> LayDanhSachActive()
        {
            using (var db = new DaiNamDBDataContext())
            {
                var now = DateTime.Now;
                var list = db.KhuyenMais
                    .Where(k => k.TrangThai == true
                             && k.DaXoa == false
                             && k.NgayBatDau <= now
                             && k.NgayKetThuc >= now)
                    .Select(k => new DTO_KhuyenMaiPOS
                    {
                        Id = k.Id,
                        MaKhuyenMai = k.MaKhuyenMai,
                        TenKhuyenMai = k.TenKhuyenMai,
                        LoaiGiamGia = k.LoaiGiamGia,
                        GiaTriGiam = k.GiaTriGiam,
                        DonToiThieu = k.DonToiThieu ?? 0,
                        CoChongCheo = k.CoChongCheo,
                        SoLanToiDa = k.SoLanToiDa,
                        TrangThai = k.TrangThai,
                        NgayBatDau = k.NgayBatDau,
                        NgayKetThuc = k.NgayKetThuc,
                        SoLanDaDung = db.KhuyenMai_LichSus.Count(ls => ls.IdKhuyenMai == k.Id)
                    })
                    .ToList();

                foreach (var km in list)
                    km.DieuKiens = LayDieuKienTheoKM(db, km.Id);

                return list;
            }
        }

        /// Lấy tất cả KM (kể cả hết hạn) dành cho màn hình quản lý.
        public List<DTO_KhuyenMaiPOS> LayTatCa()
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.KhuyenMais
                    .Where(k => k.DaXoa == false)
                    .OrderByDescending(k => k.NgayTao)
                    .Select(k => new DTO_KhuyenMaiPOS
                    {
                        Id = k.Id,
                        MaKhuyenMai = k.MaKhuyenMai,
                        TenKhuyenMai = k.TenKhuyenMai,
                        LoaiGiamGia = k.LoaiGiamGia,
                        GiaTriGiam = k.GiaTriGiam,
                        DonToiThieu = k.DonToiThieu ?? 0,
                        CoChongCheo = k.CoChongCheo,
                        SoLanToiDa = k.SoLanToiDa,
                        TrangThai = k.TrangThai,
                        NgayBatDau = k.NgayBatDau,
                        NgayKetThuc = k.NgayKetThuc,
                        SoLanDaDung = db.KhuyenMai_LichSus.Count(ls => ls.IdKhuyenMai == k.Id)
                    })
                    .ToList();
            }
        }

        /// Thêm mới chương trình KM, trả về Id mới.
        public int ThemKhuyenMai(DTO_KhuyenMaiPOS dto, int nguoiTao)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var entity = new KhuyenMai
                {
                    MaKhuyenMai  = dto.MaKhuyenMai,
                    TenKhuyenMai = dto.TenKhuyenMai,
                    LoaiGiamGia  = dto.LoaiGiamGia,
                    GiaTriGiam   = dto.GiaTriGiam,
                    DonToiThieu  = dto.DonToiThieu == 0 ? (decimal?)null : dto.DonToiThieu,
                    NgayBatDau   = dto.NgayBatDau,
                    NgayKetThuc  = dto.NgayKetThuc,
                    CoChongCheo  = dto.CoChongCheo,
                    SoLanToiDa   = dto.SoLanToiDa,
                    TrangThai    = dto.TrangThai,
                    NguoiTao     = nguoiTao,
                    NgayTao      = DateTime.Now
                };
                db.KhuyenMais.InsertOnSubmit(entity);
                db.SubmitChanges();
                return entity.Id;
            }
        }

        /// Cập nhật thông tin chương trình KM.
        public void CapNhatKhuyenMai(DTO_KhuyenMaiPOS dto)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var entity = db.KhuyenMais.FirstOrDefault(k => k.Id == dto.Id);
                if (entity == null) return;

                entity.TenKhuyenMai = dto.TenKhuyenMai;
                entity.LoaiGiamGia  = dto.LoaiGiamGia;
                entity.GiaTriGiam   = dto.GiaTriGiam;
                entity.DonToiThieu  = dto.DonToiThieu == 0 ? (decimal?)null : dto.DonToiThieu;
                entity.NgayBatDau   = dto.NgayBatDau;
                entity.NgayKetThuc  = dto.NgayKetThuc;
                entity.CoChongCheo  = dto.CoChongCheo;
                entity.SoLanToiDa   = dto.SoLanToiDa;
                entity.TrangThai    = dto.TrangThai;
                db.SubmitChanges();
            }
        }

        /// Xóa mềm KM (không xóa cứng vì có lịch sử sử dụng).
        public void XoaMem(int id)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var entity = db.KhuyenMais.FirstOrDefault(k => k.Id == id);
                if (entity == null) return;

                entity.DaXoa = true;
                db.SubmitChanges();
            }
        }

        /// Lưu lại điều kiện cho một KM (xóa cũ, insert mới theo batch).
        public void LuuDieuKien(int idKhuyenMai, List<DTO_DieuKienKM> danhSach)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var cu = db.KhuyenMai_DieuKiens.Where(d => d.IdKhuyenMai == idKhuyenMai).ToList();
                db.KhuyenMai_DieuKiens.DeleteAllOnSubmit(cu);

                foreach (var dk in danhSach)
                {
                    db.KhuyenMai_DieuKiens.InsertOnSubmit(new KhuyenMai_DieuKien
                    {
                        IdKhuyenMai    = idKhuyenMai,
                        LoaiDieuKien   = dk.LoaiDieuKien,
                        PhepSo         = dk.PhepSo,
                        GiaTriDieuKien = dk.GiaTriDieuKien
                    });
                }
                db.SubmitChanges();
            }
        }

        /// Ghi lịch sử sử dụng KM sau checkout.
        public void GhiLichSu(int idKhuyenMai, int idDonHang, int? idKhachHang, decimal soTienGiam)
        {
            using (var db = new DaiNamDBDataContext())
            {
                db.KhuyenMai_LichSus.InsertOnSubmit(new KhuyenMai_LichSu
                {
                    IdKhuyenMai = idKhuyenMai,
                    IdDonHang   = idDonHang,
                    IdKhachHang = idKhachHang,
                    SoTienGiam  = soTienGiam
                });
                db.SubmitChanges();
            }
        }

        private List<DTO_DieuKienKM> LayDieuKienTheoKM(DaiNamDBDataContext db, int idKM)
        {
            return db.KhuyenMai_DieuKiens
                .Where(d => d.IdKhuyenMai == idKM)
                .Select(d => new DTO_DieuKienKM
                {
                    LoaiDieuKien   = d.LoaiDieuKien,
                    PhepSo         = d.PhepSo,
                    GiaTriDieuKien = d.GiaTriDieuKien
                })
                .ToList();
        }
    }
}


