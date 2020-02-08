using System;
using Business.Interfaces;
using Data.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace Business.Repositories
{
    public class TitleRepository<T> : ITitleRepository<T> where T : class
    {
        private readonly TitleContext titleContext;

        private DbSet<T> Table { get; set; }

        public TitleRepository(TitleContext titleContext)
        {
            this.titleContext = titleContext;
            this.titleContext.Database.EnsureCreated();
            Table = titleContext.Set<T>();
        }


        public void Add(T entity)
        {
            Table.Add(entity);
            titleContext.SaveChanges();
        }

        public void AddRange(IEnumerable<T> entities)
        {
            Table.AddRange(entities);
            titleContext.SaveChanges();
        }

        public T Get(int Id)
        {
            return Table.Find(Id);
        }

        public IQueryable<T> GetTable()
        {
            return Table;
        }

        public IQueryable<T> Where(Expression<Func<T, bool>> where)
        {
            return Table.Where(where);
        }
        
        public Boolean Any(Expression<Func<T, bool>> where)
        {
            return Table.Any(where);
        }
    }
}
