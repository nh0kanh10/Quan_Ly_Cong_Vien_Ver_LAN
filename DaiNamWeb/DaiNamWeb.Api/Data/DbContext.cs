using Microsoft.Data.SqlClient;
using System.Data;

namespace DaiNamWeb.Api.Data;

public class DbContext
{
    private readonly string _connectionString;

    public DbContext(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Missing DefaultConnection in appsettings.json");
    }

    public IDbConnection CreateConnection() => new SqlConnection(_connectionString);
}
