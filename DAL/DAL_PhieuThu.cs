using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_PhieuThu
    {
        private static DAL_PhieuThu instance;
        private DataQuanLyDaiNamDataContext CreateDb() { return new DataQuanLyDaiNamDataContext(); }
        public static DAL_PhieuThu Instance
        {
            get
            {
                if (instance == null) instance = new DAL_PhieuThu();
                return instance;
            }
        }

        public List<ET_PhieuThu> LoadDS()
        {
            using (var db = CreateDb())
            {
                return db.GetTable<PhieuThu>().Select(s => new ET_PhieuThu
                {
                    Id = s.Id,
                    MaCode = s.MaCode,
                    IdDonHang = s.IdDonHang,
                    IdGiaoDichVi = s.IdGiaoDichVi,
                    SoTien = s.SoTien,
                    PhuongThuc = s.PhuongThuc,
                    MaGiaoDichDoiTac = s.MaGiaoDichDoiTac,
                    ThoiGian = s.ThoiGian,
                    CreatedAt = s.CreatedAt,
                    CreatedBy = s.CreatedBy
                }).ToList();
            }
        }

        public List<ET_PhieuThu> GetAll() => LoadDS();

        public bool Them(ET_PhieuThu et)
        {
            if (et == null) return false;
            try
            {
                using (var db = CreateDb())
                {
                    var obj = new PhieuThu
                    {
                        MaCode = et.MaCode, IdDonHang = et.IdDonHang,
                        IdGiaoDichVi = et.IdGiaoDichVi, SoTien = et.SoTien,
                        PhuongThuc = et.PhuongThuc, MaGiaoDichDoiTac = et.MaGiaoDichDoiTac,
                        ThoiGian = et.ThoiGian, CreatedAt = et.CreatedAt, CreatedBy = et.CreatedBy
                    };
                    db.GetTable<PhieuThu>().InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex) { DalErrorLogger.Log("DAL_PhieuThu.Them", ex); return false; }
        }

        public int ThemVaLayId(ET_PhieuThu et)
        {
            if (et == null) return 0;
            try
            {
                using (var db = CreateDb())
                {
                    var obj = new PhieuThu
                    {
                        MaCode = et.MaCode, IdDonHang = et.IdDonHang,
                        IdGiaoDichVi = et.IdGiaoDichVi, SoTien = et.SoTien,
                        PhuongThuc = et.PhuongThuc, MaGiaoDichDoiTac = et.MaGiaoDichDoiTac,
                        ThoiGian = et.ThoiGian, CreatedAt = et.CreatedAt, CreatedBy = et.CreatedBy
                    };
                    db.GetTable<PhieuThu>().InsertOnSubmit(obj);
                    db.SubmitChanges();
                    return obj.Id; 
                }
            }
            catch (Exception ex) { DalErrorLogger.Log("DAL_PhieuThu.ThemVaLayId", ex); return 0; }
        }

        public bool Sua(ET_PhieuThu et)
        {
            if (et == null) return false;
            try
            {
                using (var db = CreateDb())
                {
                    var obj = db.GetTable<PhieuThu>().FirstOrDefault(x => x.Id == et.Id);
                    if (obj != null)
                    {
                        obj.MaCode = et.MaCode;
                        obj.IdDonHang = et.IdDonHang;
                        obj.IdGiaoDichVi = et.IdGiaoDichVi;
                        obj.SoTien = et.SoTien;
                        obj.PhuongThuc = et.PhuongThuc;
                        obj.MaGiaoDichDoiTac = et.MaGiaoDichDoiTac;
                        obj.ThoiGian = et.ThoiGian;
                        obj.CreatedAt = et.CreatedAt;
                        obj.CreatedBy = et.CreatedBy;
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex) { DalErrorLogger.Log("DAL_PhieuThu.Sua", ex); return false; }
        }

        public bool Xoa(int id)
        {
            try
            {
                using (var db = CreateDb())
                {
                    var obj = db.GetTable<PhieuThu>().FirstOrDefault(x => x.Id == id);
                    if (obj != null)
                    {
                        db.GetTable<PhieuThu>().DeleteOnSubmit(obj);
                        db.SubmitChanges();
                        return true;
                    }
                    return false;
                }
            }
            catch (Exception ex) { DalErrorLogger.Log("DAL_PhieuThu.Xoa", ex); return false; }
        }

        public ET_PhieuThu LayTheoId(int id)
        {
            using (var db = CreateDb())
            {
                return db.GetTable<PhieuThu>().Where(x => x.Id == id).Select(s => new ET_PhieuThu
                {
                    Id = s.Id, MaCode = s.MaCode, IdDonHang = s.IdDonHang,
                    IdGiaoDichVi = s.IdGiaoDichVi, SoTien = s.SoTien,
                    PhuongThuc = s.PhuongThuc, MaGiaoDichDoiTac = s.MaGiaoDichDoiTac,
                    ThoiGian = s.ThoiGian, CreatedAt = s.CreatedAt, CreatedBy = s.CreatedBy
                }).FirstOrDefault();
            }
        }
    }
}
