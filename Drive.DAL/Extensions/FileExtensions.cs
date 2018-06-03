using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.DAL.Extensions
{
    public static class FileExtensions
    {
        public static int Delete(this File file)
        {
            FileDAO dao = new FileDAO();
            return dao.Delete(file.Id);
        }

        public static int Save(this File file)
        {
            FileDAO dao = new FileDAO();
            return dao.Insert(file);
        }

        public static int Update(this File file)
        {
            FileDAO dao = new FileDAO();
            return dao.Update(file);
        }
    }
}
