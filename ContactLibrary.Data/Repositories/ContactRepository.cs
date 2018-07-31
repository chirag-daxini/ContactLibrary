using System.Data.Entity;
using ContactLibrary.Core;
using ContactLibrary.Data.Common;

namespace ContactLibrary.Data.Repositories
{
    public class ContactRepository : Repository<ContactEntity>, IContactRepository
    {
        public ContactRepository(DbContext context)
            : base(context)
        {

        }
    }
}
