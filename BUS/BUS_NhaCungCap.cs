using System;
using System.Collections.Generic;
using System.Linq;
using DAL;
using ET;

namespace BUS
{
    public class BUS_NhaCungCap : IBaseBUS<ET_NhaCungCap>
    {
        private static BUS_NhaCungCap instance;
        public static BUS_NhaCungCap Instance
        {
            get
            {
                if (instance == null) instance = new BUS_NhaCungCap();
                return instance;
            }
        }

        public List<ET_NhaCungCap> LoadDS()
        {
            return DAL_NhaCungCap.Instance.LoadDS().Where(x => x.IsDeleted == false).ToList();
        }

        public List<ET_NhaCungCap> TimKiem(string keyword, string filter)
        {
            var p = LoadDS();
            if (!string.IsNullOrEmpty(keyword))
            {
                keyword = keyword.ToLower();
                p = p.Where(x => 
                    (x.Ten != null && x.Ten.ToLower().Contains(keyword)) || 
                    (x.MaSoThue != null && x.MaSoThue.ToLower().Contains(keyword)) ||
                    (x.DienThoai != null && x.DienThoai.Contains(keyword))).ToList();
            }
            return p;
        }

        public ET_NhaCungCap GetById(int id)
        {
            return DAL_NhaCungCap.Instance.LayTheoId(id);
        }

        public ResponseResult Them(ET_NhaCungCap entity)
        {
            entity.CreatedAt = DateTime.Now;
            entity.IsDeleted = false;
            
            if (DAL_NhaCungCap.Instance.Them(entity))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi thêm nhà cung cấp!" };
        }

        public ResponseResult Sua(ET_NhaCungCap entity)
        {
            entity.UpdatedAt = DateTime.Now;
            if (DAL_NhaCungCap.Instance.Sua(entity))
                return new ResponseResult { IsSuccess = true };
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi cập nhật nhà cung cấp!" };
        }

        public ResponseResult Xoa(int id)
        {
            var entity = GetById(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.UpdatedAt = DateTime.Now;
                if (DAL_NhaCungCap.Instance.Sua(entity))
                    return new ResponseResult { IsSuccess = true };
            }
            return new ResponseResult { IsSuccess = false, ErrorMessage = "Lỗi khi xóa nhà cung cấp!" };
        }
    }
}
