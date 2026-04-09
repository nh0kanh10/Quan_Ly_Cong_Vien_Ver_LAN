using System;
using DAL;
using ET;
using System.Collections.Generic;
using System.Linq;

namespace BUS
{
    public class BUS_SuKien
    {
        private static BUS_SuKien instance;
        public static BUS_SuKien Instance
        {
            get
            {
                if (instance == null) instance = new BUS_SuKien();
                return instance;
            }
        }

        public List<ET_SuKien> LoadDS()
        {
            return DAL_SuKien.Instance.LoadDS().Where(x => !x.IsDeleted).ToList();
        }

        public ResponseResult ThemSuKien(ET_SuKien et)
        {
            if (string.IsNullOrEmpty(et.MaCode)) et.MaCode = "SK" + DateTime.Now.ToString("yyyyMMddHHmmss");
            et.CreatedAt = DateTime.Now;
            et.CreatedBy = (SessionManager.CurrentUser != null) ? (int?)SessionManager.CurrentUser.Id : null;

            bool success = DAL_SuKien.Instance.Them(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm sự kiện.");
        }

        public ResponseResult SuaSuKien(ET_SuKien et)
        {
            bool success = DAL_SuKien.Instance.Sua(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể cập nhật sự kiện.");
        }

        public ResponseResult XoaSuKien(int id)
        {
            bool success = DAL_SuKien.Instance.Xoa(id);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa sự kiện.");
        }
    }
}
