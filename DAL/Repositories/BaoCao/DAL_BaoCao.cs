using System;
using System.Collections.Generic;
using System.Linq;
using ET.DTOs;
using DAL;

namespace DAL.Repositories.BaoCao
{
    public class DAL_BaoCao
    {
        public static DAL_BaoCao Instance { get; } = new DAL_BaoCao();

        private DAL_BaoCao() { }

        public List<DTO_BaoCaoDoanhThu> LayBaoCaoDoanhThu(DateTime tuNgay, DateTime denNgay)
        {
            using (var db = new DaiNamDBDataContext())
            {
                // Gọi Stored Procedure qua DataContext
                var result = db.ExecuteQuery<DTO_BaoCaoDoanhThu>(
                    "EXEC dbo.sp_BaoCaoDoanhThu {0}, {1}", 
                    tuNgay, 
                    denNgay
                ).ToList();
                
                return result;
            }
        }
    }
}


