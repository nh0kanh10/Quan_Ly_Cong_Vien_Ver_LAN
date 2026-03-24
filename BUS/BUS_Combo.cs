using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace BUS
{
    public class BUS_Combo : IBaseBUS<ET_Combo>
    {
        private readonly IComboGateway _gateway;
        private readonly IComboChiTietGateway _ctGateway;

        private static BUS_Combo instance;
        public static BUS_Combo Instance
        {
            get
            {
                if (instance == null) instance = new BUS_Combo();
                return instance;
            }
        }

        public BUS_Combo() : this(new DefaultComboGateway(), new DefaultComboChiTietGateway()) { }
        public BUS_Combo(IComboGateway gw, IComboChiTietGateway ctGw)
        {
            _gateway = gw;
            _ctGateway = ctGw;
        }

        public List<ET_Combo> LoadDS()
        {
            return _gateway.LoadDS().Where(x => !x.IsDeleted).ToList();
        }

        public ET_Combo GetById(int id) => _gateway.LayTheoId(id);

        public ET_Combo GetByMaCode(string maCode)
        {
            if (string.IsNullOrEmpty(maCode)) return null;
            return LoadDS().FirstOrDefault(x => x.MaCode == maCode);
        }

        public ResponseResult Them(ET_Combo et)
        {
            if (string.IsNullOrEmpty(et.Ten)) return ResponseResult.Error("Tên combo không được để trống.");
            et.CreatedAt = DateTime.Now;
            bool success = _gateway.Them(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm Combo.");
        }

        public ResponseResult Sua(ET_Combo et)
        {
            et.UpdatedAt = DateTime.Now;
            bool success = _gateway.Sua(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể cập nhật Combo.");
        }

        public ResponseResult Xoa(int id)
        {
            bool success = _gateway.Xoa(id);
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
            return _ctGateway.LoadDS().Where(x => x.IdCombo == idCombo).ToList();
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
            bool success = _ctGateway.Them(ct);
            return success ? ResponseResult.Success() : ResponseResult.Error("Lỗi thêm chi tiết combo.");
        }
        
        public ResponseResult XoaChiTiet(int idCT)
        {
            bool success = _ctGateway.Xoa(idCT);
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
            bool deleted = _ctGateway.XoaTheoCombo(idCombo);
            if (!deleted)
                return ResponseResult.Error("Lỗi khi xóa chi tiết cũ.");

            foreach (var item in items)
            {
                item.IdCombo = idCombo;
                bool inserted = _ctGateway.Them(item);
                if (!inserted)
                    return ResponseResult.Error($"Lỗi khi lưu chi tiết sản phẩm ID={item.IdSanPham}.");
            }

            return ResponseResult.Success();
        }
    }
}
