using System;
using System.Collections.Generic;

namespace BusinessLogic.Services
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Linq;
    using System.Linq.Expressions;

    public abstract class BaseService<T> where T : class
    {
        protected DataContext dataContext;
        protected readonly DbSet<T> dbset;

        protected BaseService(DataContext dataContext)
        {
            this.dataContext = dataContext;
            dbset = dataContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            dbset.Add(entity);
            dataContext.SaveChanges();
        }

        public virtual void Update(T entity)
        {
            dbset.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;

            dataContext.SaveChanges();
        }

        public virtual void Delete(T entity)
        {
            dbset.Remove(entity);

            dataContext.SaveChanges();
        }

        public virtual T GetById(int id)
        {
            return dbset.Find(id);
        }

        public virtual T GetById(string id)
        {
            return dbset.Find(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbset;
        }


        public T Get(Expression<Func<T, bool>> where)
        {
            return dbset.Where(where).FirstOrDefault<T>();
        }
    }
}
