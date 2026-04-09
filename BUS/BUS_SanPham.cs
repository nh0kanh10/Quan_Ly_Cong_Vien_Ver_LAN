using System;
using DAL;
using ET;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    public class BUS_SanPham : IBaseBUS<ET_SanPham>
    {
        private static BUS_SanPham instance;
        public static BUS_SanPham Instance
        {
            get
            {
                if (instance == null) instance = new BUS_SanPham();
                return instance;
            }
        }

        public List<ET_SanPham> LoadDS()
        {
            return DAL_SanPham.Instance.LoadDS().Where(x => !x.IsDeleted).ToList();
        }

        public ET_SanPham GetById(int id)
        {
            return DAL_SanPham.Instance.LayTheoId(id);
        }

        public ET_SanPham GetByMaCode(string maCode)
        {
            return DAL_SanPham.Instance.LayTheoMaCode(maCode);
        }

        /// <summary>
        /// Thêm sản phẩm + tự động ghi SanPham_Ve nếu LoaiSanPham = "Ve"
        /// </summary>
        public ResponseResult Them(ET_SanPham et)
        {
            if (string.IsNullOrEmpty(et.Ten)) return ResponseResult.Error("Tên sản phẩm không được để trống.");
            et.CreatedAt = DateTime.Now;

            int newId = DAL_SanPham.Instance.ThemVaLayId(et);
            if (newId <= 0) return ResponseResult.Error("Không thể thêm sản phẩm.");

            // Nếu là Vé -> Ghi bảng SanPham_Ve
            if (et.LoaiSanPham == AppConstants.LoaiSanPham.Ve && et._veInfo != null)
            {
                et._veInfo.IdSanPham = newId;
                if (!DAL_SanPham_Ve.Instance.ThemHoacCapNhat(et._veInfo))
                    return ResponseResult.Error("Đã tạo sản phẩm nhưng lỗi khi ghi thông tin vé.");
            }

            return ResponseResult.Success();
        }

        /// <summary>
        /// Sửa sản phẩm + Sync SanPham_Ve (Upsert nếu là Vé, Delete nếu đổi sang loại khác)
        /// </summary>
        public ResponseResult Sua(ET_SanPham et)
        {
            bool success = DAL_SanPham.Instance.Sua(et);
            if (!success) return ResponseResult.Error("Không thể cập nhật sản phẩm.");

            if (et.LoaiSanPham == AppConstants.LoaiSanPham.Ve && et._veInfo != null)
            {
                et._veInfo.IdSanPham = et.Id;
                DAL_SanPham_Ve.Instance.ThemHoacCapNhat(et._veInfo);
            }
            else
            {
                // Đổi từ Vé sang loại khác -> Xóa bản ghi SanPham_Ve nếu tồn tại
                DAL_SanPham_Ve.Instance.Xoa(et.Id);
            }

            return ResponseResult.Success();
        }

        public ResponseResult Xoa(int id)
        {
            bool success = DAL_SanPham.Instance.Xoa(id);
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
            return DAL_SanPham.Instance.LoadDS().Where(x => x.IdKhuVuc == idKhuVuc && !x.IsDeleted).ToList();
        }

        public List<ET_SanPham> LayTheoLoai(string loai)
        {
            return DAL_SanPham.Instance.LoadDS().Where(x => x.LoaiSanPham == loai && !x.IsDeleted).ToList();
        }

        /// <summary>
        /// Load thông tin vé riêng cho 1 sản phẩm (dùng khi user click vào dòng Grid)
        /// </summary>
        public ET_SanPham_Ve LayVeInfo(int idSanPham)
        {
            return DAL_SanPham_Ve.Instance.LayTheoIdSanPham(idSanPham);
        }

        // 
        //  QUY ĐỔI ĐƠN VỊ — Wrappers cho UI (frmSanPham Tab QuyĐổi)
        //      

        public List<ET_QuyDoiDonVi> LayQuyDoiTheoSP(int idSanPham)
        {
            return DAL_QuyDoiDonVi.Instance.LoadDS()
                .Where(x => x.IdSanPham == idSanPham)
                .ToList();
        }

        public ResponseResult LuuQuyDoi(ET_QuyDoiDonVi et)
        {
            if (et.TyLeQuyDoi <= 0) return ResponseResult.Error("Hệ số quy đổi phải > 0.");
            et.CreatedAt = DateTime.Now;
            bool ok = (et.Id == 0)
                ? DAL_QuyDoiDonVi.Instance.Them(et)
                : DAL_QuyDoiDonVi.Instance.Sua(et);
            return ok ? ResponseResult.Success() : ResponseResult.Error("Không thể lưu quy đổi.");
        }

        public ResponseResult XoaQuyDoi(int id)
        {
            bool ok = DAL_QuyDoiDonVi.Instance.Xoa(id);
            return ok ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa quy đổi.");
        }
    }
}
