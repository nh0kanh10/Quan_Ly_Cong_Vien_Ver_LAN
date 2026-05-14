using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Linq;

namespace DAL.Repositories.DanhMuc
{
    public class DAL_LoaiPhong
    {
        public static DAL_LoaiPhong Instance { get; } = new DAL_LoaiPhong();
        private DAL_LoaiPhong() { }

        // thêm hoặc sửa LoaiPhong trong cùng 1 Transaction của SanPham
        public void InsertOrUpdate(DaiNamDBDataContext db, ET.Models.DanhMuc.ET_LoaiPhong et)
        {
            if (et == null || et.Id <= 0) return;

            // Vì chung id sản phâm, ta check xem đã tồn tại chưa
            var existing = db.LoaiPhongs.SingleOrDefault(x => x.IdSanPham == et.Id);

            if (existing != null)
            {
                // cập nhật
                existing.TenLoai = et.TenLoai;
                existing.MoTa = et.MoTa;
                existing.SoNguoiToiDa = et.SoNguoiToiDa;
                existing.SoTreEmToiDa = et.SoTreEmToiDa;
                existing.DienTich = et.DienTich;
                existing.TienNghi = et.TienNghi;
                existing.ConHoatDong = et.ConHoatDong;
            }
            else
            {
                // thêm mới
                var newLp = new LoaiPhong
                {
                    IdSanPham = et.Id,
                    TenLoai = et.TenLoai,
                    MoTa = et.MoTa,
                    SoNguoiToiDa = et.SoNguoiToiDa,
                    SoTreEmToiDa = et.SoTreEmToiDa,
                    DienTich = et.DienTich,
                    TienNghi = et.TienNghi,
                    ConHoatDong = et.ConHoatDong
                };
                db.LoaiPhongs.InsertOnSubmit(newLp);
            }
            
            // Không gọi db.SubmitChanges() ở đây vì nó sẽ được gọi chung ở BUS_SanPham vì đi chung trong cụm
        }

        public ET.Models.DanhMuc.ET_LoaiPhong LayChiTiet(int id)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var lp = db.LoaiPhongs.SingleOrDefault(x => x.IdSanPham == id);
                if (lp == null) return null;

                return new ET.Models.DanhMuc.ET_LoaiPhong
                {
                    Id = lp.IdSanPham,
                    TenLoai = lp.TenLoai,
                    MoTa = lp.MoTa,
                    SoNguoiToiDa = lp.SoNguoiToiDa,
                    SoTreEmToiDa = lp.SoTreEmToiDa,
                    DienTich = lp.DienTich,
                    TienNghi = lp.TienNghi,
                    ConHoatDong = lp.ConHoatDong
                };
            }
        }
    }
}


