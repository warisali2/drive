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
            File file = new File();

            file.Id = (int)reader["Id"];
            file.Name = (String)reader["Name"];
            file.ParenFolderId = (int)reader["ParentFolderId"];
            file.FileExt = (String)reader["FileExt"];
            file.FileSizeInKB = (int)reader["FileSizeInKB"];
            file.CreatedBy = (int)reader["CreatedBy"];
            file.UploadedOn = (DateTime)reader["UploadedOn"];
            file.IsActive = (bool)reader["IsActive"];

            return file;
        }

        public int Update(File obj)
        {
            throw new NotImplementedException();
        }
    }
}
