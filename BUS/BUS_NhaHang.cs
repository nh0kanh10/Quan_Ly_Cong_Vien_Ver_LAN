using System;
using System.Collections.Generic;
using DAL;
using ET;

namespace BUS
{
    public class BUS_NhaHang
    {
        private static BUS_NhaHang _instance;
        public static BUS_NhaHang Instance
        {
            get
            {
                if (_instance == null) _instance = new BUS_NhaHang();
                return _instance;
            }
        }

        private BUS_NhaHang() { }

        public List<ET_NhaHang> LoadDS()
        {
            return DAL_NhaHang.Instance.LoadDS();
        }

        public ResponseResult Them(ET_NhaHang et)
        {
            if (string.IsNullOrWhiteSpace(et.TenNhaHang))
                return ResponseResult.Error("Tên nhà hàng không được trống.");
            bool success = DAL_NhaHang.Instance.Them(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể thêm nhà hàng.");
        }

        public ResponseResult Sua(ET_NhaHang et)
        {
            if (et.Id <= 0)
                return ResponseResult.Error("Nhà hàng không hợp lệ.");
            bool success = DAL_NhaHang.Instance.Sua(et);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể sửa nhà hàng.");
        }

        public ResponseResult Xoa(int id)
        {
            bool success = DAL_NhaHang.Instance.Xoa(id);
            return success ? ResponseResult.Success() : ResponseResult.Error("Không thể xóa nhà hàng, có thể dữ liệu đang được sử dụng.");
        }
    }
}
