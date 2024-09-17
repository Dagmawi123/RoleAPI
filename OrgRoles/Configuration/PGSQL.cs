using Npgsql;
using System.Data;

namespace OrgRoles.Configuration
{
    public class PGSQL(IConfiguration configuration)
    {
        public IDbConnection createConnection() {
            return new NpgsqlConnection(configuration.GetConnectionString("MyConnection"));
        }
    }
}
