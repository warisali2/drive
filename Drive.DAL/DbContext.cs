using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.DAL
{
    internal class DbContext : IDisposable
    {
        String _conString = ConfigurationManager.ConnectionStrings["SQL_SERVER_CON_STRING"].ConnectionString;
        SqlConnection _connection = null;

        public DbContext()
        {
            _connection = new SqlConnection(_conString);
            _connection.Open();
        }
        public void Dispose()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }
    }
}
