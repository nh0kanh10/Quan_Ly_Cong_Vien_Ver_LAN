using System;
using System.IO;

namespace DAL
{
    public static class ConnectionManager
    {
        private static string configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "connection.txt");
        
        private static string defaultConn = @"Data Source=.\SQLEXPRESS;Initial Catalog=DaiNamResort;Integrated Security=True";

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
                connString = string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security=True", server, database);
            }
            else
            {
                connString = string.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3}", server, database, user, pass);
            }
            File.WriteAllText(configPath, connString);
        }
        
        public static void SaveRawConnectionString(string connString)
        {
            File.WriteAllText(configPath, connString);
        }
    }
}
