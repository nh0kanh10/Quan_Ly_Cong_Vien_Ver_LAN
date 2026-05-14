using System;
using System.Collections.Generic;

namespace GUI.Infrastructure
{
    // Giao tiếp giữa các UserControl/Form không cần biết nhau.
    // Ví dụ: UC_KhachHang lưu xong -> publish "KhachHang_DaLuu" -> UC_DonHang tự reload.
    //
    // QUAN TRỌNG: Phải gọi Unsubscribe khi đóng tab/form để tránh memory leak. nghĩa là EventBus sẽ giữ reference vĩnh viễn nếu không huỷ đăng ký.
    public static class EventBus
    {
        // Key = tên sự kiện, Value = danh sách callback
        private static readonly Dictionary<string, List<Action<object>>> _handlers
            = new Dictionary<string, List<Action<object>>>();

        /// <summary>
        /// Đăng ký lắng nghe sự kiện.
        /// </summary>
        /// <param name="eventName">Tên sự kiện (VD: "KhachHang_DaLuu")</param>
        /// <param name="handler">Callback xử lý khi sự kiện xảy ra</param>
        public static void Subscribe(string eventName, Action<object> handler)
        {
            if (!_handlers.ContainsKey(eventName))
                _handlers[eventName] = new List<Action<object>>();

            _handlers[eventName].Add(handler);
        }

        /// <summary>
        /// Huỷ đăng ký — BẮT BUỘC gọi khi đóng form/tab.
        /// Không gọi = memory leak vì EventBus giữ reference vĩnh viễn.
        /// </summary>
        /// <param name="eventName">Tên sự kiện cần huỷ</param>
        /// <param name="handler">Callback đã đăng ký trước đó</param>
        public static void Unsubscribe(string eventName, Action<object> handler)
        {
            if (_handlers.ContainsKey(eventName))
                _handlers[eventName].Remove(handler);
        }

        /// <summary>
        /// Phát sự kiện tới tất cả subscriber.
        /// </summary>
        /// <param name="eventName">Tên sự kiện</param>
        /// <param name="data">Dữ liệu kèm theo (null nếu không có)</param>
        public static void Publish(string eventName, object data = null)
        {
            if (!_handlers.ContainsKey(eventName)) return;

            // Duyệt bản copy để tránh lỗi nếu handler tự Unsubscribe trong callback
            var snapshot = new List<Action<object>>(_handlers[eventName]);
            foreach (var handler in snapshot)
            {
                handler?.Invoke(data);
            }
        }
    }
}
