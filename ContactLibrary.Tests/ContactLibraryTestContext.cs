using System.Data.Entity;
using ContactLibrary.Data.DataContext;
using System.Data.Common;
using System.Collections.Generic;
using ContactLibrary.Core;

namespace ContactLibrary.Tests
{
    public class ContactLibraryTestContext : DbContext, IDbContext
    {
        public DbSet<ContactEntity> Contacts { get; set; }

        public ContactLibraryTestContext(DbConnection connection)
            : base(connection, true)
        {

        }

        public void Seed(ContactLibraryTestContext context)
        {
            var listCountry = new List<ContactEntity>() {
             new ContactEntity() { ID = 1, FirstName = "Chirag", LastName = "Daxini", Email = "daxinichirag26@gmail.com", PhoneNumber = "9623252606", IsActive = true},
             new ContactEntity() { ID = 2, FirstName = "Hemal",LastName = "Patel", Email = "Hemal.Patel@gmail.com", PhoneNumber = "1234567890",IsActive = true},
             new ContactEntity() { ID = 3, FirstName = "Vinayak", LastName = "Dave", Email = "Vinayak.Dave@gmail.com", PhoneNumber = "8866625979", IsActive = true}
            };
            context.Contacts.AddRange(listCountry);
            context.SaveChanges();
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new AlwaysCreateInitializer());
            base.OnModelCreating(modelBuilder);
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : Entitybase
        {
            return base.Set<TEntity>();
        }

        public class AlwaysCreateInitializer : DropCreateDatabaseAlways<ContactLibraryTestContext>
        {
            protected override void Seed(ContactLibraryTestContext context)
            {
                context.Seed(context);
                base.Seed(context);
            }
        }

    }
}
