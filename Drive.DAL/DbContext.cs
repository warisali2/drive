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

        SqlCommand CreateCommand()
        {
            return _connection.CreateCommand();
        }

        public int ExecuteQuery(SqlCommand command)
        {
            var count = command.ExecuteNonQuery();
            return count;
        }

        public Object ExecuteScalar(SqlCommand command)
        {
            return command.ExecuteScalar();
        }

        public SqlDataReader ExecuteReader(SqlCommand command)
        {
            return command.ExecuteReader();
        }


        public void Dispose()
        {
            if (_connection != null && _connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
        }
    }
}
