using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.DAL.Extensions
{
    public static class FolderExtensions
    {
        public static int Delete(this Folder Folder)
        {
            FolderDAO dao = new FolderDAO();
            return dao.Delete(Folder.Id);
        }

        public static int Save(this Folder Folder)
        {
            FolderDAO dao = new FolderDAO();
            return dao.Insert(Folder);
        }

        public static int Update(this Folder Folder)
        {
            FolderDAO dao = new FolderDAO();
            return dao.Update(Folder);
        }
    }
}
