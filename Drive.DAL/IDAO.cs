using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Drive.DAL
{
    public interface IDAO<T>
    {
        int Insert(T obj);
        int Update(T obj);
        T GetById(int id);
        List<T> GetAll();
        int Delete(int id);
        T Map(SqlDataReader reader);

    }
}
