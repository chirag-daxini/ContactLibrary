using System.Linq;
using ContactLibrary.Core;
using System.Collections.Generic;
using ContactLibrary.Data.Common;
using System;
using ContactLibrary.Data.Repositories;
using ContactLibrary.Core.Mappers;
using ContactLibrary.Domain;

namespace ContactLibrary.Data.Service
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository contactRepository;
        readonly IUnitOfWork unitOfWork;
        readonly ContactMapper contactMapper;

        public ContactService(IContactRepository contactRepository, IUnitOfWork unitOfWork, ContactMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.contactRepository = contactRepository;
            this.contactMapper = mapper;
        }
        public void CreateContact(ContactObject contact)
        {
            if (contact == null)
                throw new ArgumentNullException("contact");

            var contactEntity = contactMapper.GetObject(contact);
            contactEntity.IsActive = true;
            this.contactRepository.Add(contactEntity);
            this.unitOfWork.Commit();
        }

        public void DeleteContact(ContactObject contact)
        {
            if (contact == null)
                throw new ArgumentNullException("contact");

            var contactEntity = this.contactRepository.GetById(contact.ID);
            this.contactRepository.Delete(contactEntity);
            this.unitOfWork.Commit();
        }

        public ContactObject GetContact(long id)
        {
            var entity = this.contactRepository.FindBy(m => m.ID == id).FirstOrDefault();
            return contactMapper.GetObject(entity);
        }

        public IEnumerable<ContactObject> GetContacts()
        {
            return this.contactRepository.GetAll().Where(x=>x.IsActive).ToList().Select(x => contactMapper.GetObject(x));
        }

        public void UpdateContact(ContactObject contact)
        {
            if (contact == null)
                throw new ArgumentNullException("contact");

            var existingEntity = this.contactRepository.GetById(contact.ID);

            if(existingEntity != null)
            {
                existingEntity.ID = contact.ID;
                existingEntity.IsActive = contact.IsActive;
                existingEntity.FirstName = contact.FirstName;
                existingEntity.LastName = contact.LastName;
                existingEntity.PhoneNumber = contact.PhoneNumber;
                existingEntity.Email = contact.Email;
            }
            
            this.contactRepository.Edit(existingEntity);
            this.unitOfWork.Commit();
        }
    }
}
