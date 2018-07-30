using System.Data.Entity;
using ContactLibrary.Core;
using ContactLibrary.Data.Common;
using System.Linq;
using ContactLibrary.Data.DataContext;

namespace ContactLibrary.Data.Repositories
{
    public class ContactRepository : Repository<ContactEntity>, IContactRepository
    {
        public ContactRepository(DbContext context)
            : base(context)
        {

        }

        public ContactEntity GetById(int id)
        {
            return FindBy(x => x.ID == id).FirstOrDefault();
        }
    }
}
