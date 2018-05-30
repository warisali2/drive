using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace Drive.DAL
{
    public class FileDAO : BaseDAO, IDAO<File>
    {
        public int Delete(int id)
        {
                var command = _context.CreateCommand();
                command.CommandText = @"UPDATE dbo.File SET IsActive=0 WHERE Id=@Id";

                command.AddParameter("@Id", id);

                return _context.ExecuteQuery(command);
        }

        public List<File> GetAll()
        {
            throw new NotImplementedException();
        }

        public File GetById(int id)
        {
            throw new NotImplementedException();
        }

        public int Insert(File obj)
        {
            throw new NotImplementedException();
        }

        public File Map(SqlDataReader reader)
        {
            throw new NotImplementedException();
        }

        public int Update(File obj)
        {
            throw new NotImplementedException();
        }
    }
}
