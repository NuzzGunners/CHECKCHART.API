using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CHECKCHART.API.Abstract
{
    public interface IEntityBaseRepository<T> where T : class
    {
        IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);
        void Add(T item);
        IEnumerable<T> GetAll();
        T Find(Expression<Func<T, bool>> predicate);
        void Remove(T item);
        void Update(T item);
        void Commit();
    }
}
