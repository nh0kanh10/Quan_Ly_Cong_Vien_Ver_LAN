using System;
using System.IO;

namespace DAL
{
    public static class ConnectionManager
    {
        private static string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "connection.txt");
        
        private static string defaultConn = @"Data Source=.\SQLEXPRESS;Initial Catalog=QuanLyCongVien;Integrated Security=True";

        public static string GetConnectionString()
        {
            if (File.Exists(configPath))
            {
                string storedConn = File.ReadAllText(configPath);
                if (!string.IsNullOrWhiteSpace(storedConn))
                    return storedConn;
            }
            return defaultConn;
        }

        public static void SaveConnectionString(string server, string database, string user, string pass, bool isWindowsAuth)
        {
            string connString = "";
            if (isWindowsAuth)
            {
                connString = $@"Data Source={server};Initial Catalog={database};Integrated Security=True";
            }
            else
            {
                connString = $@"Data Source={server};Initial Catalog={database};User ID={user};Password={pass}";
            }
            File.WriteAllText(configPath, connString);
        }
        
        public static void SaveRawConnectionString(string connString)
        {
            File.WriteAllText(configPath, connString);
        }
    }
}
