using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.DAL
{
    public class DbContext : IDisposable
    {
        String _conKey = "SQL_SERVER_CON_STRING";
        String _conString = null;
        SqlConnection _connection = null;

        public DbContext()
        {
            _conString = ConfigurationManager.ConnectionStrings[_conKey].ConnectionString ?? null;

            if (_conString == null)
                throw new ConfigurationErrorsException("Connection String is null");

            _connection = new SqlConnection(_conString);
            _connection.Open();
        }

        public DbContext(String key)
        {
            _conString = ConfigurationManager.ConnectionStrings[key].ConnectionString ?? null;

            if (_conString == null)
                throw new ConfigurationErrorsException("Connection String is null");

            _connection = new SqlConnection(_conString);
            _connection.Open();
        }

        public SqlCommand CreateCommand()
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
