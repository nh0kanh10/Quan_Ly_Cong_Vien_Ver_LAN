using System.Collections.Generic;
using System.Linq;
using ET.Constants;
using ET.DTOs;
using ET.Models.BanHang;
using ET.Models.DanhMuc;

namespace DAL.Repositories.BanHang
{
    public class DAL_MenuPOS
    {
        public static DAL_MenuPOS Instance { get; } = new DAL_MenuPOS();

        #region Truy vấn dữ liệu

        public List<ET_DiemBanHang> LayDanhSachDiemBan()
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.DiemBanHang_POs
                    .Where(d => d.TrangThai != AppConstants.TrangThaiHieuLuc.DaXoa)
                    .OrderBy(d => d.TenDiemBan)
                    .Select(d => new ET_DiemBanHang
                    {
                        Id           = d.Id,
                        MaDiemBan    = d.MaDiemBan,
                        TenDiemBan   = d.TenDiemBan,
                        IdKhuVuc     = d.IdKhuVuc,
                        ChoPhepBanVe  = d.ChoPhepBanVe,
                        ChoPhepBanFNB = d.ChoPhepBanFNB,
                        ChoPhepThue   = d.ChoPhepThue,
                        TrangThai    = d.TrangThai
                    })
                    .ToList();
            }
        }

        // Lấy danh sách SP trong menu của 1 điểm bán, kèm thông tin SP để hiển thị.
        public List<DTO_MenuPOSItem> LayMenuTheoDiemBan(int idDiemBan)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return (from m in db.Menu_POs
                        where m.IdDiemBan == idDiemBan
                        join sp in db.SanPhams on m.IdSanPham equals sp.Id
                        orderby m.ThuTuHienThi, sp.TenSanPham
                        select new DTO_MenuPOSItem
                        {
                            IdSanPham    = sp.Id,
                            MaSanPham    = sp.MaSanPham,
                            TenSanPham   = sp.TenSanPham,
                            LoaiSanPham  = sp.LoaiSanPham,
                            DonGia       = sp.DonGia ?? 0m,
                            ThuTuHienThi = m.ThuTuHienThi,
                            ConHoatDong  = m.ConHoatDong
                        }).ToList();
            }
        }

        // Lấy tất cả sản phẩm đang bán để hiển thị bên kho (grid trái).
        public List<ET_SanPham> LayKhoSanPham()
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.SanPhams
                    .Where(sp => sp.TrangThai == AppConstants.TrangThaiSanPham.DangBan
                              && sp.LoaiSanPham != AppConstants.LoaiSanPham.LuuTru
                              && sp.LoaiSanPham != AppConstants.LoaiSanPham.DoChoThue)
                    .OrderBy(sp => sp.LoaiSanPham)
                    .ThenBy(sp => sp.TenSanPham)
                    .Select(sp => new ET_SanPham
                    {
                        Id = sp.Id,
                        MaSanPham = sp.MaSanPham,
                        TenSanPham = sp.TenSanPham,
                        LoaiSanPham = sp.LoaiSanPham,
                        DonGia = sp.DonGia ?? 0,
                        TrangThai = sp.TrangThai
                    })
                    .ToList();
            }
        }

        #endregion

        #region Thêm / Sửa / Xoá

        public int ThemDiemBan(ET_DiemBanHang dto)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var entity = new DiemBanHang_PO
                {
                    MaDiemBan    = dto.MaDiemBan,
                    TenDiemBan   = dto.TenDiemBan,
                    IdKhuVuc     = dto.IdKhuVuc,
                    ChoPhepBanVe  = dto.ChoPhepBanVe,
                    ChoPhepBanFNB = dto.ChoPhepBanFNB,
                    ChoPhepThue   = dto.ChoPhepThue,
                    TrangThai    = AppConstants.TrangThaiHieuLuc.HoatDong
                };
                db.DiemBanHang_POs.InsertOnSubmit(entity);
                db.SubmitChanges();
                return entity.Id;
            }
        }

        public void CapNhatDiemBan(ET_DiemBanHang dto)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var entity = db.DiemBanHang_POs.FirstOrDefault(d => d.Id == dto.Id);
                if (entity == null) return;

                entity.TenDiemBan   = dto.TenDiemBan;
                entity.IdKhuVuc     = dto.IdKhuVuc;
                entity.ChoPhepBanVe  = dto.ChoPhepBanVe;
                entity.ChoPhepBanFNB = dto.ChoPhepBanFNB;
                entity.ChoPhepThue   = dto.ChoPhepThue;
                entity.TrangThai    = dto.TrangThai;
                db.SubmitChanges();
            }
        }

        // Xoá mềm: chuyển TrangThai sang "DaXoa", không xoá dữ liệu vật lý.
        public void XoaMem(int id)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var entity = db.DiemBanHang_POs.FirstOrDefault(d => d.Id == id);
                if (entity == null) return;
                entity.TrangThai = AppConstants.TrangThaiHieuLuc.DaXoa;
                db.SubmitChanges();
            }
        }

        public bool CoBatKyPhienMo(int idDiemBan)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var d = db.DiemBanHang_POs.FirstOrDefault(x => x.Id == idDiemBan);
                if (d == null) return false;

                return db.PhienThuNgans.Any(p =>
                    p.IdMayBan == d.MaDiemBan &&
                    p.ThoiGianDong == null);
            }
        }

        public bool TrungMaDiemBan(string ma, int bỏQuaId = 0)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.DiemBanHang_POs.Any(d =>
                    d.MaDiemBan == ma && d.Id != bỏQuaId);
            }
        }

        // Xoá menu cũ rồi lưu lại toàn bộ danh sách mới trong 1 lần gọi DB.
        public void LuuMenu(int idDiemBan, List<ET_MenuPOS> danhSach)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var cuItems = db.Menu_POs.Where(m => m.IdDiemBan == idDiemBan).ToList();
                db.Menu_POs.DeleteAllOnSubmit(cuItems);
                db.SubmitChanges();

                foreach (var item in danhSach)
                {
                    db.Menu_POs.InsertOnSubmit(new Menu_PO
                    {
                        IdDiemBan    = idDiemBan,
                        IdSanPham    = item.IdSanPham,
                        ThuTuHienThi = item.ThuTuHienThi,
                        ConHoatDong  = item.ConHoatDong
                    });
                }
                db.SubmitChanges();
            }
        }

        #endregion
    }
}


