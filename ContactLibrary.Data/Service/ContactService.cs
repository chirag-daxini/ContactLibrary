using System.Linq;
using ContactLibrary.Core;
using ContactLibrary.Data.Repository;

namespace ContactLibrary.Data.Service
{
    public class ContactService : IContactService
    {
        private IRepository<ContactEntity> contactRepository;
        public ContactService(IRepository<ContactEntity> _contacts)
        {
            contactRepository = _contacts;
        }
        public void CreateContact(ContactEntity contact)
        {
            contactRepository.Insert(contact);
        }

        public void DeleteContact(ContactEntity contact)
        {
            contactRepository.Delete(contact);
        }

        public ContactEntity GetContact(long id)
        {
            return contactRepository.GetById(id);
        }

        public IQueryable<ContactEntity> GetContacts()
        {
            return contactRepository.Table;
        }

        public void UpdateContact(ContactEntity contact)
        {
            contactRepository.Update(contact);
        }
    }
}
