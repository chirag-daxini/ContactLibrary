using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using ContactLibrary.Core;
using ContactLibrary.Data.DataContext;
using System.Data.Entity.Infrastructure;

namespace ContactLibrary.Data.Common
{
    public abstract class Repository<T> : IRepository<T> where T : Entitybase
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbset;

        protected Repository(DbContext context)
        {
            this._entities = context;
            this._dbset = context.Set<T>();
        }
        public virtual IQueryable<T> GetAll()
        {
            return this._dbset;
        }

        public virtual T GetById(long id)
        {
            return this._dbset.Find(id);
        }

        public virtual IEnumerable<T> FindBy(System.Linq.Expressions.Expression<Func<T, bool>> predicate)
        {
            return  this._dbset.Where(predicate).AsEnumerable();
        }

        public virtual T Add(T entity)
        {
            return this._dbset.Add(entity);
        }

        public virtual T Delete(T entity)
        {
            entity.IsActive = false;
            this._entities.Entry(entity).State = EntityState.Modified;
            return entity;
        }

        public virtual void Edit(T entity)
        {
            _entities.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Save()
        {
            this._entities.SaveChanges();
        }
    }
}
