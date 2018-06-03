using Drive.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.DAL.Extensions
{
    public static class UserExtensions
    {
        public static int Delete(this User User)
        {
            UserDAO dao = new UserDAO();
            return dao.Delete(User.Id);
        }

        public static int Save(this User User)
        {
            UserDAO dao = new UserDAO();
            return dao.Insert(User);
        }

        public static int Update(this User User)
        {
            UserDAO dao = new UserDAO();
            return dao.Update(User);
        }
    }
}
