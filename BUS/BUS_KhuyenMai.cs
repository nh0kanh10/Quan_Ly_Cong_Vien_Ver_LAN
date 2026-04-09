using System;
using DAL;
using ET;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    public class BUS_KhuyenMai
    {
        private static BUS_KhuyenMai instance;
        public static BUS_KhuyenMai Instance
        {
            get
            {
                if (instance == null) instance = new BUS_KhuyenMai();
                return instance;
            }
        }

        public List<ET_KhuyenMai> LoadDS()
        {
            return DAL_KhuyenMai.Instance.LoadDS().Where(x => !x.IsDeleted).ToList();
        }

        public ResponseResult ThemKhuyenMai(ET_KhuyenMai et)
        {
            if (string.IsNullOrEmpty(et.MaCode)) et.MaCode = et.TenKhuyenMai; // Dùng tên làm mã nếu trống
            et.CreatedAt = DateTime.Now;
            et.CreatedBy = (SessionManager.CurrentUser != null) ? (int?)SessionManager.CurrentUser.Id : null;

            bool success = DAL_KhuyenMai.Instance.Them(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm khuyến mãi.");
        }

        public ResponseResult SuaKhuyenMai(ET_KhuyenMai et)
        {
            bool success = DAL_KhuyenMai.Instance.Sua(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể cập nhật khuyến mãi.");
        }

        public ResponseResult XoaKhuyenMai(int id)
        {
            bool success = DAL_KhuyenMai.Instance.Xoa(id);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa khuyến mãi.");
        }

        public ET_KhuyenMai KiemTraKhuyenMai(string code, decimal totalAmount)
        {
            var km = DAL_KhuyenMai.Instance.LoadDS().FirstOrDefault(x => x.MaCode == code && !x.IsDeleted && x.TrangThai);
            if (km == null) return null;

            if (DateTime.Now < km.NgayBatDau || DateTime.Now > km.NgayKetThuc) return null;
            if (km.DonToiThieu.HasValue && totalAmount < km.DonToiThieu.Value) return null;

            return km;
        }

        /// <summary>
        /// Lấy chương trình sự kiện lớn nhất đang active (để áp Max-Discount rule).
        /// </summary>
        public ET_KhuyenMai GetBestActivePromotion(decimal totalAmount)
        {
            var now = DateTime.Now;
            return DAL_KhuyenMai.Instance.LoadDS()
                .Where(x => !x.IsDeleted && x.TrangThai && x.NgayBatDau <= now && x.NgayKetThuc >= now
                            && (!x.DonToiThieu.HasValue || totalAmount >= x.DonToiThieu.Value))
                .OrderByDescending(x => x.GiaTriGiam)
                .FirstOrDefault();
        }
    }
}
