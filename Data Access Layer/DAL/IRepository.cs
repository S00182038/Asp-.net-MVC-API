using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.DAL
{
    public interface IRepository<T>
    {
        void getEntity(out T entity,int id);
        T UpdateEntity(T entity);

        T InsertEntity(T entity);

        T DeleteEntity(T entity);
        void Save();
    }
}
