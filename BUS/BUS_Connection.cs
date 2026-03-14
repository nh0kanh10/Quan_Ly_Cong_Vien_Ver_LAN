using System;
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
                connStr = $@"Data Source={server};Initial Catalog={database};Integrated Security=True";
            }
            else
            {
                connStr = $@"Data Source={server};Initial Catalog={database};User ID={user};Password={pass}";
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
    }
}
