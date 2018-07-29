using ContactLibrary.Core;
using System.Data.Entity;

namespace ContactLibrary.Data.DataContext
{
    public interface IDbContext
    {
        IDbSet<TEntity> Set<TEntity>() where TEntity : Entitybase;
        int SaveChanges();
    }
}
