using System;
using System.Collections.Generic;
using System.Linq;
using ET;

namespace BUS
{
    public class BUS_SanPham : IBaseBUS<ET_SanPham>
    {
        private readonly ISanPhamGateway _gateway;
        private readonly ISanPhamVeGateway _veGateway;
        private readonly IQuyDoiDonViGateway _quyDoiGateway;

        private static BUS_SanPham instance;
        public static BUS_SanPham Instance
        {
            get
            {
                if (instance == null) instance = new BUS_SanPham();
                return instance;
            }
        }

        public BUS_SanPham() : this(new DefaultSanPhamGateway(), new DefaultSanPhamVeGateway(), new DefaultQuyDoiDonViGateway()) { }
        public BUS_SanPham(ISanPhamGateway gw, ISanPhamVeGateway veGw, IQuyDoiDonViGateway quyDoiGw)
        {
            _gateway = gw;
            _veGateway = veGw;
            _quyDoiGateway = quyDoiGw;
        }

        public List<ET_SanPham> LoadDS()
        {
            return _gateway.LoadDS().Where(x => !x.IsDeleted).ToList();
        }

        public ET_SanPham GetById(int id) => _gateway.LayTheoId(id);

        public ET_SanPham GetByMaCode(string maCode) => _gateway.LayTheoMaCode(maCode);

        /// <summary>
        /// Thêm sản phẩm + tự động ghi SanPham_Ve nếu LoaiSanPham = "Ve"
        /// </summary>
        public ResponseResult Them(ET_SanPham et)
        {
            if (string.IsNullOrEmpty(et.Ten)) return ResponseResult.Error("Tên sản phẩm không được để trống.");
            
            if (et.DonGia <= 0) return ResponseResult.Error("Đơn giá không được âm.");
            var validLoai = new[] { 
                AppConstants.LoaiSanPham.Ve, AppConstants.LoaiSanPham.Combo, AppConstants.LoaiSanPham.Thue,
                AppConstants.LoaiSanPham.AnUong, AppConstants.LoaiSanPham.LuuTru, AppConstants.LoaiSanPham.DoLuuNiem,
                AppConstants.LoaiSanPham.GuiXe, AppConstants.LoaiSanPham.DichVu, AppConstants.LoaiSanPham.Khac 
            };
            if (!string.IsNullOrEmpty(et.LoaiSanPham) && !validLoai.Contains(et.LoaiSanPham)) 
                return ResponseResult.Error("Loại sản phẩm không hợp lệ.");

            et.CreatedAt = DateTime.Now;

            int newId = _gateway.ThemVaLayId(et);
            if (newId <= 0) return ResponseResult.Error("Không thể thêm sản phẩm.");

            // Nếu là Vé -> Ghi bảng SanPham_Ve
            if (et.LoaiSanPham == AppConstants.LoaiSanPham.Ve && et._veInfo != null)
            {
                et._veInfo.IdSanPham = newId;
                if (!_veGateway.ThemHoacCapNhat(et._veInfo))
                    return ResponseResult.Error("Đã tạo sản phẩm nhưng lỗi khi ghi thông tin vé.");
            }

            return ResponseResult.Success();
        }

        /// <summary>
        /// Sửa sản phẩm + Sync SanPham_Ve (Upsert nếu là Vé, Delete nếu đổi sang loại khác)
        /// </summary>
        public ResponseResult Sua(ET_SanPham et)
        {
            if (et.DonGia <= 0) return ResponseResult.Error("Đơn giá không được âm.");
            var validLoai = new[] { 
                AppConstants.LoaiSanPham.Ve, AppConstants.LoaiSanPham.Combo, AppConstants.LoaiSanPham.Thue,
                AppConstants.LoaiSanPham.AnUong, AppConstants.LoaiSanPham.LuuTru, AppConstants.LoaiSanPham.DoLuuNiem,
                AppConstants.LoaiSanPham.GuiXe, AppConstants.LoaiSanPham.DichVu, AppConstants.LoaiSanPham.Khac 
            };
            if (!string.IsNullOrEmpty(et.LoaiSanPham) && !validLoai.Contains(et.LoaiSanPham)) 
                return ResponseResult.Error("Loại sản phẩm không hợp lệ.");

            bool success = _gateway.Sua(et);
            if (!success) return ResponseResult.Error("Không thể cập nhật sản phẩm.");

            if (et.LoaiSanPham == AppConstants.LoaiSanPham.Ve && et._veInfo != null)
            {
                et._veInfo.IdSanPham = et.Id;
                _veGateway.ThemHoacCapNhat(et._veInfo);
            }
            else
            {
                // Đổi từ Vé sang loại khác -> Xóa bản ghi SanPham_Ve nếu tồn tại
                _veGateway.Xoa(et.Id);
            }

            return ResponseResult.Success();
        }

        public ResponseResult Xoa(int id)
        {
            bool success = _gateway.Xoa(id);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa sản phẩm.");
        }

        public List<ET_SanPham> TimKiem(string kw, string loai = "Tất cả")
        {
            var query = LoadDS();
            if (!string.IsNullOrWhiteSpace(kw))
            {
                kw = kw.ToLower();
                query = query.FindAll(x => (x.Ten != null && x.Ten.ToLower().Contains(kw)) ||
                                           (x.MoTa != null && x.MoTa.ToLower().Contains(kw)));
            }
            if (loai != "Tất cả")
            {
                query = query.FindAll(x => x.LoaiSanPham == loai);
            }
            return query;
        }

        public List<ET_SanPham> TimKiemNangCao(string kw, string loai) => TimKiem(kw, loai);

        public List<ET_SanPham> LayTheoKhuVuc(int idKhuVuc)
        {
            return _gateway.LoadDS().Where(x => x.IdKhuVuc == idKhuVuc && !x.IsDeleted).ToList();
        }

        public List<ET_SanPham> LayTheoLoai(string loai)
        {
            return _gateway.LoadDS().Where(x => x.LoaiSanPham == loai && !x.IsDeleted).ToList();
        }

        /// <summary>
        /// Load thông tin vé riêng cho 1 sản phẩm (dùng khi user click vào dòng Grid)
        /// </summary>
        public ET_SanPham_Ve LayVeInfo(int idSanPham) => _veGateway.LayTheoIdSanPham(idSanPham);

        // 
        //  QUY ĐỔI ĐƠN VỊ — Wrappers cho UI (frmSanPham Tab QuyĐổi)
        //      

        public List<ET_QuyDoiDonVi> LayQuyDoiTheoSP(int idSanPham)
        {
            return _quyDoiGateway.LoadDS()
                .Where(x => x.IdSanPham == idSanPham)
                .ToList();
        }

        public ResponseResult LuuQuyDoi(ET_QuyDoiDonVi et)
        {
            if (et.TyLeQuyDoi <= 0) return ResponseResult.Error("Hệ số quy đổi phải > 0.");
            et.CreatedAt = DateTime.Now;
            bool ok = (et.Id == 0)
                ? _quyDoiGateway.Them(et)
                : _quyDoiGateway.Sua(et);
            return ok ? ResponseResult.Success() : ResponseResult.Error("Không thể lưu quy đổi.");
        }

        public ResponseResult XoaQuyDoi(int id)
        {
            bool ok = _quyDoiGateway.Xoa(id);
            return ok ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa quy đổi.");
        }
    }
}
