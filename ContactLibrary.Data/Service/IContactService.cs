using ContactLibrary.Core;
using ContactLibrary.Domain;
using System.Collections.Generic;

namespace ContactLibrary.Data.Service
{
    public interface IContactService
    {
        IEnumerable<ContactObject> GetContacts();

        ContactObject GetContact(long id);

        void CreateContact(ContactObject entity);

        void UpdateContact(ContactObject entity);

        void DeleteContact(ContactObject entity);
    }
}
