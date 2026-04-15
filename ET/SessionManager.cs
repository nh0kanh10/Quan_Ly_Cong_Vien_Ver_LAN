using ET;

namespace ET
{
    public static class SessionManager
    {
        public static ET_NhanVien CurrentUser { get; set; }
        
        // ── Context cho ca làm hiện tại ──────────────────────────────
        // Được load lúc đăng nhập, dựa vào lịch làm việc (LichLamViec) của ngày hôm nay
        public static int? CurrentIdKhuVuc { get; set; }
        public static string CurrentTenKhuVuc { get; set; }
    }
}
