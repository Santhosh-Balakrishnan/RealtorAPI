using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T Get(Func<T,bool> expression);
        IEnumerable<T> GetAll(Func<T, bool>? expression = null);
        void Add(T entity);
        void Delete(T entity);
    }
}
