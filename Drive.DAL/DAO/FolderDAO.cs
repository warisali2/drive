using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.DAL
{
    public class FolderDAO : BaseDAO, IDAO<Folder>
    {

        public FolderDAO() : base() { }
        public FolderDAO(String key) : base(key) { }

        public int Delete(int id)
        {
            var command = _context.CreateCommand();
            command.CommandText = @"UPDATE dbo.Folder SET IsActive=0 WHERE Id=@Id";

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

        public List<Folder> GetAll()
        {
            var command = _context.CreateCommand();
            command.CommandText = @"SELECT * FROM dbo.Folder WHERE IsActive = 1;";

            var reader = _context.ExecuteReader(command);
            List<Folder> list = new List<Folder>();

            while (reader.Read())
            {
                var folder = Map(reader);
                list.Add(folder);
            }

            reader.Close();
            return list;
        }

        public Folder GetById(int id)
        {
            var command = _context.CreateCommand();

            command.CommandText = @"SELECT * FROM dbo.Folder WHERE Id = @Id";
            command.AddParameter("@Id", id);

            var reader = _context.ExecuteReader(command);

            if (reader.Read())
            {
                var folder = Map(reader);
                reader.Close();
                return folder;
            }

            return null;
        }

        public int Insert(Folder folder)
        {
            var command = _context.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Folder (Name, ParentFolderId, CreatedBy, CreatedOn, IsActive) OUTPUT INSERTED.Id VALUES(@Name, @ParentFolderId, @CreatedBy, @CreatedOn, @IsActive);";

            command.AddParameter("@Name", folder.Name);
            command.AddParameter("@ParentFolderId", folder.ParentFolderId);
            command.AddParameter("@CreatedBy", folder.CreatedBy);
            command.AddParameter("@CreatedOn", folder.CreatedOn.Truncate(TimeSpan.FromSeconds(1)));
            command.AddParameter("@IsActive", folder.IsActive);

            try
            {
                return (int)_context.ExecuteScalar(command);
            }
            catch (Exception e)
            {
                return -1;
            }

        }

        public Folder Map(SqlDataReader reader)
        {
            Folder folder = new Folder();

            folder.Id = (int)reader["Id"];
            folder.Name = (String)reader["Name"];
            folder.ParentFolderId = (int)reader["ParentFolderId"];
            folder.CreatedBy = (int)reader["CreatedBy"];
            folder.CreatedOn = (DateTime)reader["CreatedOn"];
            folder.IsActive = (bool)reader["IsActive"];

            return folder;
        }

        public int Update(Folder folder)
        {
            var command = _context.CreateCommand();
            command.CommandText = @"UPDATE dbo.Folder SET Name=@Name, ParentFolderId=@ParentFolderId, IsActive=@IsActive WHERE Id=@Id";

            command.AddParameter("@Name", folder.Name);
            command.AddParameter("@ParentFolderId", folder.ParentFolderId);
            command.AddParameter("@IsActive", folder.IsActive);
            command.AddParameter("@Id", folder.Id);

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
