using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ContactLibrary.Core;
using System.Threading.Tasks;

namespace ContactLibrary.Data.Repository
{
    public interface IRepository<T> where T : Entitybase
    {
        T GetById(object id);
        void Insert(T entity);
        void Update(T entity);
        void Delete(T entity);
        IQueryable<T> Table { get; }
    }
}
