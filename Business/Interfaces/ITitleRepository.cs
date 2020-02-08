using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Business.Interfaces
{
    public interface ITitleRepository<T> where T : class
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
        T Get(int Id);
        IQueryable<T> GetTable();
        IQueryable<T> Where(Expression<Func<T, bool>> where);
        Boolean Any(Expression<Func<T, bool>> where);
    }
}
