using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.DAL
{
    public class BaseDAO
    {
        protected DbContext _context;

        public BaseDAO()
        {
            _context = new DbContext();
        }

        public BaseDAO(String key)
        {
            _context = new DbContext(key);
        }

    }
}
