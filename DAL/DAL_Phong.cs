using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_Phong
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_Phong instance;
        public static DAL_Phong Instance
        {
            get
            {
                if (instance == null) instance = new DAL_Phong();
                return instance;
            }
        }

        public List<ET_Phong> LoadDS()
        {
            return db.GetTable<Phong>().Select(s => new ET_Phong
            {
                Id = s.Id,
                MaCode = s.MaCode,
                TenPhong = s.TenPhong,
                IdSanPham = s.LoaiPhong != null ? s.LoaiPhong.IdSanPham : (int?)null,
                SucChua = s.SucChua,
                TrangThai = s.TrangThai,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted,
                RowVer = s.RowVer == null ? null : s.RowVer.ToArray()
            }).ToList();
        }

        public List<ET_Phong> GetAll()
        {
            return LoadDS();
        }

        public bool Them(ET_Phong et)
        {
            try {
                Phong obj = new Phong();
                obj.MaCode = et.MaCode;
                obj.TenPhong = et.TenPhong;
                obj.IdLoaiPhong = db.GetTable<LoaiPhong>().FirstOrDefault(x => x.IdSanPham == et.IdSanPham)?.Id ?? 0;
                obj.SucChua = et.SucChua;
                obj.TrangThai = et.TrangThai;
                obj.CreatedAt = et.CreatedAt;
                obj.UpdatedAt = et.UpdatedAt;
                obj.CreatedBy = et.CreatedBy;
                obj.IsDeleted = et.IsDeleted;
                obj.RowVer = et.RowVer;
                db.GetTable<Phong>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            } catch { return false; }
        }

        public bool Sua(ET_Phong et)
        {
            try {
                var obj = db.GetTable<Phong>().FirstOrDefault(x => x.Id == et.Id);
                if (obj != null) {
                    obj.MaCode = et.MaCode;
                    obj.TenPhong = et.TenPhong;
                    obj.IdLoaiPhong = db.GetTable<LoaiPhong>().FirstOrDefault(x => x.IdSanPham == et.IdSanPham)?.Id ?? 0;
                    obj.SucChua = et.SucChua;
                    obj.TrangThai = et.TrangThai;
                    obj.CreatedAt = et.CreatedAt;
                    obj.UpdatedAt = et.UpdatedAt;
                    obj.CreatedBy = et.CreatedBy;
                    obj.IsDeleted = et.IsDeleted;
                    obj.RowVer = et.RowVer;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public bool Xoa(int id)
        {
            try {
                var obj = db.GetTable<Phong>().FirstOrDefault(x => x.Id == id);
                if (obj != null) {
                    db.GetTable<Phong>().DeleteOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
                return false;
            } catch { return false; }
        }

        public ET_Phong LayTheoId(int id)
        {
            return db.GetTable<Phong>().Where(x => x.Id == id).Select(s => new ET_Phong {
                Id = s.Id,
                MaCode = s.MaCode,
                TenPhong = s.TenPhong,
                IdSanPham = s.LoaiPhong != null ? s.LoaiPhong.IdSanPham : (int?)null,
                SucChua = s.SucChua,
                TrangThai = s.TrangThai,
                CreatedAt = s.CreatedAt,
                UpdatedAt = s.UpdatedAt,
                CreatedBy = s.CreatedBy,
                IsDeleted = s.IsDeleted,
                RowVer = s.RowVer == null ? null : s.RowVer.ToArray()
            }).FirstOrDefault();
        }

        public List<int> GetBusyRoomIds(DateTime start, DateTime end, int bufferHours = 1)
        {
            try
            {
                // Quy tắc (Nhận < Trả + Buffer) <=> (Nhận - Buffer < Trả)
                DateTime bufferedStart = start.AddHours(-bufferHours);

                // Thực hiện Join và Filter ngay dưới SQL Server bằng LINQ to SQL
                var query = from dp in db.DatPhongChiTiets
                            join ctdp in db.ChiTietDatPhongs on dp.Id equals ctdp.IdDatPhongChiTiet
                            where dp.TrangThai != "DaHuy" && dp.TrangThai != "HoanTat"
                            && (bufferedStart < dp.NgayTra) 
                            && (end > dp.NgayNhan)
                            select ctdp.IdPhong;

                return query.Distinct().ToList();
            }
            catch { return new List<int>(); }
        }
    }
}

