using System.Linq;
using System.Collections.Generic;
using ContactLibrary.Data.Common;
using System;
using ContactLibrary.Core.Mappers;
using ContactLibrary.Domain;

namespace ContactLibrary.Data.Service
{
    public class ContactService : IContactService
    {
        readonly IUnitOfWork unitOfWork;
        readonly ContactMapper contactMapper;

        public ContactService(IUnitOfWork unitOfWork, ContactMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.contactMapper = mapper;
        }
        public void CreateContact(ContactObject contact)
        {
            if (contact == null)
                throw new ArgumentNullException("contact");

            var contactEntity = contactMapper.GetObject(contact);
            contactEntity.IsActive = true;
            this.unitOfWork.ContactRepository.Add(contactEntity);
            this.unitOfWork.Commit();
        }

        public void DeleteContact(ContactObject contact)
        {
            if (contact == null)
                throw new ArgumentNullException("contact");

            var contactEntity = this.unitOfWork.ContactRepository.GetById(contact.ID);
            this.unitOfWork.ContactRepository.Delete(contactEntity);
            this.unitOfWork.Commit();
        }

        public ContactObject GetContact(long id)
        {
            var entity = this.unitOfWork.ContactRepository.GetById(id);
            return contactMapper.GetObject(entity);
        }

        public IEnumerable<ContactObject> GetContacts()
        {
            return this.unitOfWork.ContactRepository.GetAll().Where(x=>x.IsActive).ToList().Select(x => contactMapper.GetObject(x));
        }

        public void UpdateContact(ContactObject contact)
        {
            if (contact == null)
                throw new ArgumentNullException("contact");

            var existingEntity = this.unitOfWork.ContactRepository.GetById(contact.ID);

            if(existingEntity != null)
            {
                existingEntity.FirstName = contact.FirstName;
                existingEntity.LastName = contact.LastName;
                existingEntity.PhoneNumber = contact.PhoneNumber;
                existingEntity.Email = contact.Email;
            }
            
            this.unitOfWork.ContactRepository.Edit(existingEntity);
            this.unitOfWork.Commit();
        }
    }
}
