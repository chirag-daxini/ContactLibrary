using System.Data.Entity;
using System.Linq;
using ContactLibrary.Core;
using System;

namespace ContactLibrary.Data.Common
{
    

    public abstract class Repository<T> : IRepository<T> where T : Entitybase
    {
        protected DbContext _entities;
        protected readonly IDbSet<T> _dbset;
        private bool disposed = false;

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

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _entities.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
