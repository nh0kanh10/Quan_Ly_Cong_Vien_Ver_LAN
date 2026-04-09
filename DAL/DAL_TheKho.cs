using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace DAL
{
    public partial class DAL_TheKho
    {
        private DataQuanLyDaiNamDataContext db = new DataQuanLyDaiNamDataContext();
        private static DAL_TheKho instance;
        public static DAL_TheKho Instance
        {
            get
            {
                if (instance == null) instance = new DAL_TheKho();
                return instance;
            }
        }

        public bool Them(ET_TheKho et)
        {
            try
            {
                TheKho obj = new TheKho();
                obj.IdKho = et.IdKho;
                obj.IdSanPham = et.IdSanPham;
                obj.LoaiGiaoDich = et.LoaiGiaoDich;
                obj.SoLuongThayDoi = et.SoLuongThayDoi;
                obj.TonCuoi = et.TonCuoi;
                obj.DonGiaVatTu = et.DonGiaVatTu;
                obj.IdThamChieu = et.IdThamChieu;
                obj.ThoiGianGiaoDich = et.ThoiGianGiaoDich == default(DateTime) ? DateTime.Now : et.ThoiGianGiaoDich;
                obj.CreatedBy = et.CreatedBy;
                obj.GhiChu = et.GhiChu;

                db.GetTable<TheKho>().InsertOnSubmit(obj);
                db.SubmitChanges();
                return true;
            }
            catch { return false; }
        }

        public List<ET_TheKho> LoadDS()
        {
            return db.GetTable<TheKho>().Select(s => new ET_TheKho
            {
                Id = s.Id,
                IdKho = s.IdKho,
                IdSanPham = s.IdSanPham,
                LoaiGiaoDich = s.LoaiGiaoDich,
                SoLuongThayDoi = s.SoLuongThayDoi,
                TonCuoi = s.TonCuoi,
                DonGiaVatTu = s.DonGiaVatTu,
                IdThamChieu = s.IdThamChieu,
                ThoiGianGiaoDich = s.ThoiGianGiaoDich,
                CreatedBy = s.CreatedBy,
                GhiChu = s.GhiChu
            }).ToList();
        }

        public List<ET_TheKho> LayTheoSanPham(int idKho, int idSanPham)
        {
            return db.GetTable<TheKho>()
                     .Where(x => x.IdKho == idKho && x.IdSanPham == idSanPham)
                     .OrderByDescending(x => x.ThoiGianGiaoDich)
                     .Select(s => new ET_TheKho
                     {
                         Id = s.Id,
                         IdKho = s.IdKho,
                         IdSanPham = s.IdSanPham,
                         LoaiGiaoDich = s.LoaiGiaoDich,
                         SoLuongThayDoi = s.SoLuongThayDoi,
                         TonCuoi = s.TonCuoi,
                         DonGiaVatTu = s.DonGiaVatTu,
                         IdThamChieu = s.IdThamChieu,
                         ThoiGianGiaoDich = s.ThoiGianGiaoDich,
                         CreatedBy = s.CreatedBy,
                         GhiChu = s.GhiChu
                     }).ToList();
        }
    }
}
