using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using ContactLibrary.Core;
using System.Linq;

namespace ContactLibrary.Data.Common
{
    public interface IRepository<T> where T : Entitybase
    {
        IQueryable<T> GetAll();
        T GetById(long id);
        IEnumerable<T> FindBy(Expression<Func<T, bool>> predicate);
        T Add(T entity);
        T Delete(T entity);
        void Edit(T entity);
        void Save();
    }
}
