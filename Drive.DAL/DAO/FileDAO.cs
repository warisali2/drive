﻿using Drive.Entities;
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

        public FileDAO() : base() { }
        public FileDAO(String key) : base(key) { }

        public int Delete(int id)
        {
            var command = _context.CreateCommand();
            command.CommandText = @"UPDATE dbo.Files SET IsActive=0 WHERE Id=@Id";

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

        public List<File> GetAll()
        {
            var command = _context.CreateCommand();
            command.CommandText = @"SELECT * FROM dbo.Files WHERE IsActive = 1;";

            var reader = _context.ExecuteReader(command);
            List<File> list = new List<File>();

            while (reader.Read())
            {
                var file = Map(reader);
                list.Add(file);
            }

            reader.Close();
            return list;
        }

        public File GetById(int id)
        {
            var command = _context.CreateCommand();

            command.CommandText = @"SELECT * FROM dbo.Files WHERE Id = @Id";
            command.AddParameter("@Id", id);

            var reader = _context.ExecuteReader(command);

            if (reader.Read())
            {
                var file = Map(reader);
                reader.Close();
                return file;
            }

            return null;
        }

        public int Insert(File file)
        {
            var command = _context.CreateCommand();
            command.CommandText = @"INSERT INTO dbo.Files (Name, ParentFolderId, FileExt, FileSizeInKB, CreatedBy, UploadedOn, IsActive) OUTPUT INSERTED.Id VALUES(@Name, @ParentFolderId, @FileExt, @FileSizeInKB, @CreatedBy, @UploadedOn, @IsActive);";

            command.AddParameter("@Name", file.Name);
            command.AddParameter("@ParentFolderId", file.ParenFolderId);
            command.AddParameter("@FileExt", file.FileExt);
            command.AddParameter("@FileSizeInKB", file.FileSizeInKB);
            command.AddParameter("@CreatedBy", file.CreatedBy);
            command.AddParameter("@UploadedOn", file.UploadedOn.Truncate(TimeSpan.FromSeconds(1)));
            command.AddParameter("@IsActive", file.IsActive);

            try
            {
                return (int)_context.ExecuteScalar(command);
            }
            catch (Exception)
            {
                return -1;
            }

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

        public int Update(File file)
        {
            var command = _context.CreateCommand();
            command.CommandText = @"UPDATE dbo.Files SET Name=@Name, ParentFolderId=@ParentFolderId, IsActive=@IsActive WHERE Id=@Id";

            command.AddParameter("@Name", file.Name);
            command.AddParameter("@ParentFolderId", file.ParenFolderId);
            command.AddParameter("@IsActive", file.IsActive);
            command.AddParameter("@Id", file.Id);

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
