using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace DAL.Repositories.VanHanh
{
    public class DAL_VatTuPhongMacDinh
    {
        public static DAL_VatTuPhongMacDinh Instance { get; } = new DAL_VatTuPhongMacDinh();
        private DAL_VatTuPhongMacDinh() { }

        // Xóa sạch cũ, Lưu mới toàn bộ trong cùng 1 Transaction
        public void SyncVatTu(DaiNamDBDataContext db, int idSanPham, List<ET.Models.VanHanh.ET_VatTuPhongMacDinh> dsVatTuMoi)
        {
            if (idSanPham <= 0) return;

            // Lấy toàn bộ vật tư cũ của LoaiPhong (thông qua IdSanPham)
            var dsCu = db.VatTuPhongMacDinhs.Where(x => x.IdLoaiPhong == idSanPham).ToList();

            // 2. Xoá sạch
            if (dsCu.Any())
            {
                db.VatTuPhongMacDinhs.DeleteAllOnSubmit(dsCu);
            }

            // 3. Thêm mới
            if (dsVatTuMoi != null && dsVatTuMoi.Any())
            {
                var dsInsert = dsVatTuMoi.Select(x => new VatTuPhongMacDinh
                {
                    IdLoaiPhong = idSanPham,
                    IdSanPham = x.IdSanPham,
                    SoLuong = x.SoLuong
                }).ToList();

                db.VatTuPhongMacDinhs.InsertAllOnSubmit(dsInsert);
            }
        }

        public List<ET.Models.VanHanh.ET_VatTuPhongMacDinh> LayDanhSach(int idSanPham)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.VatTuPhongMacDinhs
                    .Where(x => x.IdLoaiPhong == idSanPham)
                    .Select(x => new ET.Models.VanHanh.ET_VatTuPhongMacDinh
                    {
                        Id = x.Id,
                        IdLoaiPhong = x.IdLoaiPhong,
                        IdSanPham = x.IdSanPham,
                        SoLuong = x.SoLuong,
                        TenSanPham = x.SanPham.TenSanPham
                    }).ToList();
            }
        }
    }
}


