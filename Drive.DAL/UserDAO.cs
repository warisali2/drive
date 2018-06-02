using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.DAL
{
    public class UserDAO : BaseDAO, IDAO<User>
    {

        public UserDAO() : base() { }
        public UserDAO(String key) : base(key) { }

        public int Delete(int id)
        {
            var command = _context.CreateCommand();
            command.CommandText = @"DELETE FROM dbo.Users WHERE Id=@Id";

            command.AddParameter("@Id", id);

            try
            {
                return _context.ExecuteQuery(command);
            }
            catch (Exception)
            {
                return -1;
            }
        }

        public List<User> GetAll()
        {
            var command = _context.CreateCommand();
            command.CommandText = @"SELECT * FROM dbo.Users;";

            var reader = _context.ExecuteReader(command);
            List<User> list = new List<User>();

            while (reader.Read())
            {
                var user = Map(reader);
                list.Add(user);
            }

            reader.Close();
            return list;
        }

        public User GetById(int id)
        {
            var command = _context.CreateCommand();

            command.CommandText = @"SELECT * FROM dbo.Users WHERE Id = @Id";
            command.AddParameter("@Id", id);

            var reader = _context.ExecuteReader(command);

            if (reader.Read())
            {
                var user = Map(reader);
                reader.Close();
                return user;
            }

            return null;
        }

        public int Insert(User user)
        {
            var command = _context.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Users (Name, Password, Login, Email) OUTPUT INSERTED.Id VALUES(@Name, @Password, @Login, @Email);";

            command.AddParameter("@Name", user.Name);
            command.AddParameter("@Password", user.Password);
            command.AddParameter("@Login", user.Login);
            command.AddParameter("@Email", user.Email);

            try
            {
                return (int)_context.ExecuteScalar(command);
            }
            catch (Exception e)
            {
                return -1;
            }

        }

        public User Map(SqlDataReader reader)
        {
            User user = new User();

            user.Id = (int)reader["Id"];
            user.Name = (String)reader["Name"];
            user.Login = (String)reader["Login"];
            user.Password = (String)reader["Password"];
            user.Email = (String)reader["Email"];

            return user;
        }

        public int Update(User user)
        {
            var command = _context.CreateCommand();
            command.CommandText = @"UPDATE dbo.Users SET Name=@Name, Password=@Password, Login=@Login, Email=@Email WHERE Id=@Id";

            command.AddParameter("@Name", user.Name);
            command.AddParameter("@Password", user.Password);
            command.AddParameter("@Login", user.Login);
            command.AddParameter("@Email", user.Email);
            command.AddParameter("@Id", user.Id);

            try
            {
                return _context.ExecuteQuery(command);
            }
            catch (Exception)
            {
                return -1;
            }
        }
    }
}
