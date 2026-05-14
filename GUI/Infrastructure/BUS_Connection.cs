using System;
using System.Data.SqlClient;
using System.Configuration;

namespace GUI.Infrastructure
{
    /// <summary>
    /// Service xử lý kết nối DB nằm tại tầng GUI để đảm bảo tính độc lập khi khởi động App.
    /// </summary>
    public class BUS_Connection
    {
        private static BUS_Connection _instance;
        public static BUS_Connection Instance => _instance ?? (_instance = new BUS_Connection());

        private const string ConnectionStringName = "DAL.Properties.Settings.Database_DaiNamv2ConnectionString";

        public string TestConnection(string server, string database, string user, string password, bool isWindowsAuth)
        {
            string connectionString = BuildConnectionString(server, database, user, password, isWindowsAuth);
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
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

        public bool CheckCurrentConnection()
        {
            try
            {
                string connectionString = ConfigurationManager.ConnectionStrings[ConnectionStringName]?.ConnectionString;
                if (string.IsNullOrEmpty(connectionString)) return false;

                using (SqlConnection conn = new SqlConnection(connectionString))
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

        public void SaveConnectionString(string server, string database, string user, string password, bool isWindowsAuth)
        {
            string connectionString = BuildConnectionString(server, database, user, password, isWindowsAuth);
            
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            
            if (config.ConnectionStrings.ConnectionStrings[ConnectionStringName] != null)
            {
                config.ConnectionStrings.ConnectionStrings[ConnectionStringName].ConnectionString = connectionString;
            }
            else
            {
                config.ConnectionStrings.ConnectionStrings.Add(new ConnectionStringSettings(ConnectionStringName, connectionString, "System.Data.SqlClient"));
            }

            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("connectionStrings");
        }

        private string BuildConnectionString(string server, string database, string user, string password, bool isWindowsAuth)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder
            {
                DataSource = server,
                InitialCatalog = database,
                TrustServerCertificate = true,
                ConnectTimeout = 5
            };

            if (isWindowsAuth)
            {
                builder.IntegratedSecurity = true;
            }
            else
            {
                builder.IntegratedSecurity = false;
                builder.UserID = user;
                builder.Password = password;
            }

            return builder.ConnectionString;
        }
    }
}
