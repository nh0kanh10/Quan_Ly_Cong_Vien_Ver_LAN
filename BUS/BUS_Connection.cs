using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL;

namespace BUS
{
    public class BUS_Connection
    {
        private static BUS_Connection instance;
        public static BUS_Connection Instance
        {
            get
            {
                if (instance == null) instance = new BUS_Connection();
                return instance;
            }
        }

        public string GetConnectionString()
        {
            return ConnectionManager.GetConnectionString();
        }

        public void SaveConnectionString(string server, string database, string user, string pass, bool isWindowsAuth)
        {
            ConnectionManager.SaveConnectionString(server, database, user, pass, isWindowsAuth);
        }

        public string TestConnection(string server, string database, string user, string pass, bool isWindowsAuth)
        {
            string connStr = "";
            if (isWindowsAuth)
            {
                connStr = string.Format(@"Data Source={0};Initial Catalog={1};Integrated Security=True", server, database);
            }
            else
            {
                connStr = string.Format(@"Data Source={0};Initial Catalog={1};User ID={2};Password={3}", server, database, user, pass);
            }

            try
            {
                using (var conn = new System.Data.SqlClient.SqlConnection(connStr))
                {
                    conn.Open();
                    return "OK";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Kiểm tra kết nối DB theo connection string hiện tại (đang lưu trong ConnectionManager).
        /// </summary>
        /// <returns></returns>
        public bool CheckConnection()
        {
            try
            {
                var connStr = GetConnectionString();
                using (var conn = new System.Data.SqlClient.SqlConnection(connStr))
                {
                    conn.Open();
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Lấy danh sách SQL Server phổ biến trên máy hiện tại.
        /// </summary>
        /// <returns></returns>
        public List<string> GetSqlServerInstances()
        {
            string pc = Environment.MachineName; 
            return new List<string>
            {
                $"{pc}\\SQLEXPRESS",  
                pc,                    
                ".\\SQLEXPRESS",       
                "."                   
            };
        }

        /// <summary>
        /// Lấy danh sách database từ server chỉ định.
        /// </summary>
        /// <param name="server"></param>
        /// <param name="user"></param>
        /// <param name="pass"></param>
        /// <param name="isWindowsAuth"></param>
        /// <returns></returns>
        public List<string> GetDatabases(string server, string user, string pass, bool isWindowsAuth)
        {
            var result = new List<string>();
            string connStr;

            if (isWindowsAuth)
                connStr = $"Data Source={server};Integrated Security=True;Connect Timeout=5";
            else
                connStr = $"Data Source={server};User ID={user};Password={pass};Connect Timeout=5";

            try
            {
                using (var conn = new SqlConnection(connStr))
                {
                    conn.Open();
                    using (var cmd = new SqlCommand("SELECT name FROM sys.databases WHERE database_id > 4 ORDER BY name", conn))
                    {
                        using (var reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                result.Add(reader.GetString(0));
                            }
                        }
                    }
                }
            }
            catch { }

            return result;
        }
    }
}
