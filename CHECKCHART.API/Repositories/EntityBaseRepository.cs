using CHECKCHART.API.Abstract;
using CHECKCHART.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CHECKCHART.API.Repositories
{
    public class EntityBaseRepository<T> : IEntityBaseRepository<T> where T : class
    {
        private readonly CheckChartDbContext _context;

        public EntityBaseRepository(CheckChartDbContext context)
        {
            _context = context;
        }

        public virtual IEnumerable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _context.Set<T>();
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
            return query.AsEnumerable();
        }
        public void Add(T item)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(item);
            _context.Set<T>().Add(item);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().AsEnumerable();
        }

        public void Remove(T item)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(item);
            dbEntityEntry.State = EntityState.Deleted;
        }

        public void Update(T item)
        {
            EntityEntry dbEntityEntry = _context.Entry<T>(item);
            dbEntityEntry.State = EntityState.Modified;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
