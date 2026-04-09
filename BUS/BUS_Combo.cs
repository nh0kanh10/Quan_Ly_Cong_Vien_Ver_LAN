using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using ET;

namespace BUS
{
    public class BUS_Combo : IBaseBUS<ET_Combo>
    {
        private static BUS_Combo instance;
        public static BUS_Combo Instance
        {
            get
            {
                if (instance == null) instance = new BUS_Combo();
                return instance;
            }
        }

        public List<ET_Combo> LoadDS()
        {
            return DAL_Combo.Instance.LoadDS().Where(x => !x.IsDeleted).ToList();
        }

        public ET_Combo GetById(int id)
        {
            return DAL_Combo.Instance.LayTheoId(id);
        }

 
        public ET_Combo GetByMaCode(string maCode)
        {
            if (string.IsNullOrEmpty(maCode)) return null;
            return LoadDS().FirstOrDefault(x => x.MaCode == maCode);
        }

        public ResponseResult Them(ET_Combo et)
        {
            if (string.IsNullOrEmpty(et.Ten)) return ResponseResult.Error("Tên combo không được để trống.");
            et.CreatedAt = DateTime.Now;
            bool success = DAL_Combo.Instance.Them(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm Combo.");
        }

        public ResponseResult Sua(ET_Combo et)
        {
            et.UpdatedAt = DateTime.Now;
            bool success = DAL_Combo.Instance.Sua(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể cập nhật Combo.");
        }

        public ResponseResult Xoa(int id)
        {
            bool success = DAL_Combo.Instance.Xoa(id);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa Combo.");
        }

        public List<ET_Combo> TimKiem(string kw, string trangThai = "Tất cả")
        {
            var query = LoadDS();
            if (!string.IsNullOrWhiteSpace(kw))
            {
                kw = kw.ToLower();
                query = query.FindAll(x => (x.Ten != null && x.Ten.ToLower().Contains(kw)) ||
                                           (x.MaCode != null && x.MaCode.ToLower().Contains(kw)));
            }
            if (trangThai != "Tất cả")
            {
                query = query.FindAll(x => x.TrangThai == trangThai);
            }
            return query;
        }

        public List<ET_ComboChiTiet> LayChiTiet(int idCombo)
        {
            return DAL_ComboChiTiet.Instance.LoadDS().Where(x => x.IdCombo == idCombo).ToList();
        }

        public ResponseResult ThemChiTiet(int idCombo, int idSanPham, int soLuong, decimal tyLePhanBo)
        {
            // Kiểm tra tổng TyLePhanBo có vượt quá 100% không
            var chiTiets = LayChiTiet(idCombo);
            decimal tongTyLe = chiTiets.Sum(x => x.TyLePhanBo) + tyLePhanBo;
            if (tongTyLe > 100) return ResponseResult.Error("Tổng tỷ lệ phân bổ không được vượt quá 100%.");

            var ct = new ET_ComboChiTiet
            {
                IdCombo = idCombo,
                IdSanPham = idSanPham,
                SoLuong = soLuong,
                TyLePhanBo = tyLePhanBo
            };
            bool success = DAL_ComboChiTiet.Instance.Them(ct);
            return success ? ResponseResult.Success() : ResponseResult.Error("Lỗi thêm chi tiết combo.");
        }
        
        public ResponseResult XoaChiTiet(int idCT)
        {
            bool success = DAL_ComboChiTiet.Instance.Xoa(idCT);
            return success ? ResponseResult.Success() : ResponseResult.Error("Lỗi xóa chi tiết combo.");
        }

        public ResponseResult LuuChiTiet(int idCombo, List<ET_ComboChiTiet> items)
        {
            if (items == null || items.Count == 0)
                return ResponseResult.Error("Rổ combo đang trống. Vui lòng thêm ít nhất 1 sản phẩm.");

            foreach (var item in items)
            {
                if (item.SoLuong <= 0)
                    return ResponseResult.Error("Số lượng phải > 0 cho mỗi sản phẩm.");
                if (item.TyLePhanBo <= 0)
                    return ResponseResult.Error("Tỷ lệ phân bổ phải > 0% cho mỗi sản phẩm.");
            }

            decimal tongTyLe = items.Sum(x => x.TyLePhanBo);
            if (tongTyLe != 100m)
                return ResponseResult.Error($"Tổng tỷ lệ phân bổ phải = 100% (hiện tại: {tongTyLe:N2}%).");

            // Batch save: Delete all old → Insert all new
            bool deleted = DAL_ComboChiTiet.Instance.XoaTheoCombo(idCombo);
            if (!deleted)
                return ResponseResult.Error("Lỗi khi xóa chi tiết cũ.");

            foreach (var item in items)
            {
                item.IdCombo = idCombo;
                bool inserted = DAL_ComboChiTiet.Instance.Them(item);
                if (!inserted)
                    return ResponseResult.Error($"Lỗi khi lưu chi tiết sản phẩm ID={item.IdSanPham}.");
            }

            return ResponseResult.Success();
        }
    }
}
