using ContactLibrary.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactLibrary.Data.Service
{
    public interface IContactService
    {
        IQueryable<ContactEntity> GetContacts();
        ContactEntity GetContact(long id);
        void CreateContact(ContactEntity contact);
        void UpdateContact(ContactEntity contact);
        void DeleteContact(ContactEntity contact);
    }
}
