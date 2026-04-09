using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public class DAL_LichSuDiem
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_LichSuDiem instance;
        public static DAL_LichSuDiem Instance
        {
            get
            {
                if (instance == null) instance = new DAL_LichSuDiem();
                return instance;
            }
        }

        public List<ET_LichSuDiem> LoadDS()
        {
            return db.GetTable<LichSuDiem>().Select(s => new ET_LichSuDiem
            {
                Id = s.Id,
                IdKhachHang = s.IdKhachHang,
                LoaiGiaoDich = s.LoaiGiaoDich,
                SoDiem = s.SoDiem,
                SoDuTruoc = s.SoDuTruoc,
                SoDuSau = s.SoDuSau,
                IdDonHang = s.IdDonHang,
                LyDo = s.LyDo,
                ThoiGian = s.ThoiGian,
                CreatedBy = s.CreatedBy
            }).ToList();
        }

        public List<ET_LichSuDiem> LayTheoKhachHang(int idKhachHang)
        {
            var query = from s in db.GetTable<LichSuDiem>()
                        join n in db.GetTable<NhanVien>() on s.CreatedBy equals n.Id into nj
                        from nv in nj.DefaultIfEmpty()
                        where s.IdKhachHang == idKhachHang
                        orderby s.ThoiGian descending
                        select new ET_LichSuDiem
                        {
                            Id = s.Id,
                            IdKhachHang = s.IdKhachHang,
                            LoaiGiaoDich = s.LoaiGiaoDich,
                            SoDiem = s.SoDiem,
                            SoDuTruoc = s.SoDuTruoc,
                            SoDuSau = s.SoDuSau,
                            IdDonHang = s.IdDonHang,
                            LyDo = s.LyDo,
                            ThoiGian = s.ThoiGian,
                            CreatedBy = s.CreatedBy,
                            TenNhanVien = nv != null ? nv.HoTen : "Hệ thống"
                        };
            return query.ToList();
        }

        public bool Them(ET_LichSuDiem et)
        {
            try
            {
                var obj = new LichSuDiem
                {
                    IdKhachHang = et.IdKhachHang,
                    LoaiGiaoDich = et.LoaiGiaoDich,
                    SoDiem = et.SoDiem,
                    SoDuTruoc = et.SoDuTruoc,
                    SoDuSau = et.SoDuSau,
                    IdDonHang = et.IdDonHang,
                    LyDo = et.LyDo,
                    ThoiGian = et.ThoiGian,
                    CreatedBy = et.CreatedBy
                };
                db.GetTable<LichSuDiem>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }
    }
}
