using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactLibrary.Data.DataContext
{
    public class CustomDatabaseIntializer : IDatabaseInitializer<SqlDbContext>
    {
        public void InitializeDatabase(SqlDbContext context)
        {
            if (context.Database.Exists())
            {
                if (!context.Database.CompatibleWithModel(true))
                {
                    context.Database.Delete();
                }
            }
            context.Database.CreateIfNotExists();
        }
    }
}
