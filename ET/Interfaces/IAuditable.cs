using System;

namespace ET.Interfaces
{
    // Đánh dấu entity có lưu vết tạo mới.
    // Dùng cho LINQ to SQL (qua partial class) để tự động gán lúc Insert()
    public interface IAuditable
    {
        DateTime NgayTao { get; set; }
        
        // Dùng int? vì một số action do Hệ thống/Khách hàng tự làm trên Kiosk không có NguoiTao
        int? NguoiTao { get; set; } 
    }
}
