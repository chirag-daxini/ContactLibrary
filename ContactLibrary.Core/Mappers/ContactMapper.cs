using ContactLibrary.Domain;

namespace ContactLibrary.Core.Mappers
{
    public class ContactMapper
    {
        public ContactObject GetObject(ContactEntity entity)
        {
            var contact = new ContactObject();
            contact.Email = entity.Email;
            contact.FirstName = entity.FirstName;
            contact.LastName = entity.LastName;
            contact.PhoneNumber = entity.PhoneNumber;
            contact.ID = entity.ID;
            return contact;
        }

        public ContactEntity GetObject(ContactObject entity)
        {
            return new ContactEntity()
            {
                Email = entity.Email,
                FirstName = entity.FirstName,
                ID = entity.ID,
                IsActive = entity.IsActive,
                LastName = entity.LastName,
                PhoneNumber = entity.PhoneNumber
            };
        }
    }
}
