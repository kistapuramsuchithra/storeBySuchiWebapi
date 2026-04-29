//namespace WebApisforstorebySuchi.Common
//{
//    public class BaseDAO
//    {
//    }
//}
using Microsoft.Data.SqlClient;
using System.Data;

namespace WebApisforstorebySuchi.Common
{
    public abstract class BaseDAO
    {
        private readonly string _connectionString;
        private readonly string _connectionString1;
        private readonly string _clientConnectionString;
        // private readonly string _connectionString1;

        protected BaseDAO(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnectionString");
            _clientConnectionString = configuration.GetConnectionString("ClientConnectionString");
            _connectionString1 = configuration.GetConnectionString("DefaultConnection1");

        }

        protected IDbConnection CreateConnection()
        {
            var connection = new SqlConnection(_connectionString);
            connection.Open();

            return connection;
        }


        protected IDbConnection CreateClientDBConnection()
        {
            var connection = new SqlConnection(_clientConnectionString);
            connection.Open();
            return connection;
        }

        protected IDbConnection CreateConnection1()
        {
            var connection = new SqlConnection(_connectionString1);
            connection.Open(); // Ensure connection is always open
            return connection;
        }
    }
}
