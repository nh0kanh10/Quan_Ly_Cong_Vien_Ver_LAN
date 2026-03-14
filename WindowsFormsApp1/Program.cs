using System;
using System.Windows.Forms;
using BUS;

namespace WindowsFormsApp1
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Kiểm tra kết nối từ BUS
            string currentConn = BUS_Connection.Instance.GetConnectionString();
            if (CheckConnection())
            {
                Application.Run(new frmLogin());
            }
            else
            {
                MessageBox.Show("Không thể kết nối đến cơ sở dữ liệu. Vui lòng cấu hình lại!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Run(new frmConfigConnect());
            }
        }

        static bool CheckConnection()
        {
            try
            {
                // Thử khởi tạo DB qua BUS
                using (var db = new DAL.QLKVCGTDataContext(DAL.ConnectionManager.GetConnectionString()))
                {
                    db.Connection.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
