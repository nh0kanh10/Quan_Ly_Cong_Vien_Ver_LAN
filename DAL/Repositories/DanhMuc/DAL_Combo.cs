using System;
using System.Collections.Generic;
using System.Linq;
using ET.Constants;
using ET.DTOs;

namespace DAL.Repositories.DanhMuc
{
    public class DAL_Combo
    {
        public static DAL_Combo Instance { get; } = new DAL_Combo();

        #region Truy vấn dữ liệu

        public List<Combo> LayDanhSach()
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.Combos
                    .Where(c => c.DaXoa == false)
                    .OrderByDescending(c => c.NgayTao)
                    .ToList();
            }
        }


        public List<DTO_ComboChiTietDisplay> LayChiTiet(int idCombo)
        {
            using (var db = new DaiNamDBDataContext())
            {
                return (from ct in db.ComboChiTiets
                        join sp in db.SanPhams on ct.IdSanPham equals sp.Id
                        where ct.IdCombo == idCombo
                        select new DTO_ComboChiTietDisplay
                        {
                            IdSanPham = ct.IdSanPham,
                            MaSanPham = sp.MaSanPham,
                            TenSanPham = sp.TenSanPham,
                            DonGia = sp.DonGia ?? 0,
                            SoLuong = (int)ct.SoLuong,
                            TyLePhanBo = ct.TyLePhanBo
                        }).ToList();
            }
        }

        /// <summary>
        /// Danh sách SP dùng để chọn vào rổ combo.
        /// Chỉ lấy SP đang bán, loại trừ nguyên liệu (LaVatTu).
        /// </summary>
        public List<SanPham> LaySanPhamChonCombo()
        {
            using (var db = new DaiNamDBDataContext())
            {
                return db.SanPhams
                    .Where(sp => sp.DaXoa == false
                        && sp.TrangThai == AppConstants.TrangThaiSanPham.DangBan
                        && sp.LaVatTu == false)
                    .OrderBy(sp => sp.LoaiSanPham)
                    .ThenBy(sp => sp.TenSanPham)
                    .ToList();
            }
        }

        #endregion

        #region Kiểm tra ràng buộc

        public bool KiemTraTrungMa(string maCombo, int? excludeId = null)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var query = db.Combos.Where(c => c.MaCombo == maCombo && c.DaXoa == false);
                if (excludeId.HasValue)
                    query = query.Where(c => c.Id != excludeId.Value);
                return query.Any();
            }
        }

        /// <summary>
        /// Sinh mã combo tự tăng: CB00001, CB00002...
        /// </summary>
        public string SinhMaCombo()
        {
            using (var db = new DaiNamDBDataContext())
            {
                int maxNum = 0;
                var dsMa = db.Combos.Select(c => c.MaCombo).ToList();
                foreach (var ma in dsMa)
                {
                    if (ma != null && ma.StartsWith("CB") && int.TryParse(ma.Substring(2), out int num))
                    {
                        if (num > maxNum) maxNum = num;
                    }
                }
                return $"CB{(maxNum + 1):D5}";
            }
        }

        #endregion

        #region Thêm / Sửa / Xoá

        public int ThemMoi(Combo entity)
        {
            using (var db = new DaiNamDBDataContext())
            {
                db.Combos.InsertOnSubmit(entity);
                db.SubmitChanges();
                return entity.Id;
            }
        }

        public void CapNhat(Combo entityMoi)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var goc = db.Combos.Single(c => c.Id == entityMoi.Id);
                goc.TenCombo = entityMoi.TenCombo;
                goc.GiaCombo = entityMoi.GiaCombo;
                goc.MoTa = entityMoi.MoTa;
                goc.TrangThai = entityMoi.TrangThai;
                db.SubmitChanges();
            }
        }

        public void XoaMem(int id)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var goc = db.Combos.Single(c => c.Id == id);
                goc.DaXoa = true;
                db.SubmitChanges();
            }
        }

        #endregion

        #region Nghiệp vụ đặc thù

        /// <summary>
        /// Lưu danh sách thành phần combo: xoá hết dòng cũ rồi chèn lại dòng mới.
        /// </summary>
        public void LuuChiTiet(int idCombo, List<ComboChiTiet> dsChiTiet)
        {
            using (var db = new DaiNamDBDataContext())
            {
                db.Connection.Open();
                using (var tx = db.Connection.BeginTransaction())
                {
                    db.Transaction = tx;
                    try
                    {
                        var dsCu = db.ComboChiTiets.Where(ct => ct.IdCombo == idCombo).ToList();
                        db.ComboChiTiets.DeleteAllOnSubmit(dsCu);
                        db.SubmitChanges();

                        foreach (var ct in dsChiTiet)
                        {
                            ct.IdCombo = idCombo;
                            db.ComboChiTiets.InsertOnSubmit(ct);
                        }

                        db.SubmitChanges();
                        tx.Commit();
                    }
                    catch
                    {
                        tx.Rollback();
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Cập nhật trạng thái combo
        /// </summary>
        public void CapNhatTrangThai(int id, string trangThai)
        {
            using (var db = new DaiNamDBDataContext())
            {
                var goc = db.Combos.Single(c => c.Id == id);
                goc.TrangThai = trangThai;
                db.SubmitChanges();
            }
        }

        #endregion
    }
}


