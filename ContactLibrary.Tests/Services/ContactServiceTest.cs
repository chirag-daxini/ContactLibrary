using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactLibrary.Core;
using ContactLibrary.Data.Common;
using ContactLibrary.Data.Service;
using Moq;
using ContactLibrary.Data.Repositories;
using ContactLibrary.Core.Mappers;
using ContactLibrary.Domain;

namespace ContactLibrary.Tests.Services
{
    [TestClass]
    public class ContactServiceTest
    {
        private Mock<IContactRepository> mockRepository;
        private IContactService service;
        Mock<IUnitOfWork> mockUnitWork;
        Mock<ContactMapper> mapper;
        List<ContactEntity> listCountry;

        [TestInitialize]
        public void Initialize()
        {
            this.mockRepository = new Mock<IContactRepository>();

            this.mockUnitWork = new Mock<IUnitOfWork>();

            this.mapper = new Mock<ContactMapper>();

            this.service = new ContactService(this.mockRepository.Object, this.mockUnitWork.Object, this.mapper.Object);
            listCountry = new List<ContactEntity>() {
             new ContactEntity()
                 {
                     ID = 1, FirstName = "Chirag", LastName = "Daxini", Email = "daxinichirag26@gmail.com", PhoneNumber = "9623252606", IsActive = true
                 },
             new ContactEntity() { ID = 2, FirstName = "Hemal",LastName = "Patel", Email = "Hemal.Patel@gmail.com", PhoneNumber = "1234567890",IsActive = true},
             new ContactEntity() { ID = 3, FirstName = "Vinayak", LastName = "Dave", Email = "Vinayak.Dave@gmail.com", PhoneNumber = "8866625979", IsActive = true}
            };
        }

        [TestMethod]
        public void Given_ContactService_GetAll()
        {
            //Arrange
            this.mockRepository.Setup(x => x.GetAll().GetEnumerator()).Returns(listCountry.GetEnumerator());

            //Act
            var results = this.service.GetContacts() as List<ContactObject>;

            //Assert
            Assert.IsNotNull(results);
            Assert.AreEqual(3, results.Count);
        }

        [TestMethod]
        public void Given_ContactService_AddContact()
        {
            //Arrange
            int Id = 1;
            var contact = new ContactEntity()
            {
                ID = 1,
                FirstName = "Rahul",
                LastName = "Patel",
                Email = "rahul.patel@gmail.com",
                PhoneNumber = "9623252606",
                IsActive = true
            };

            this.mockRepository.Setup(m => m.Add(contact)).Returns((ContactEntity e) =>
            {
                e.ID = Id;
                return e;
            });

            var contactObject = mapper.Object.GetObject(contact);

            //Act
            this.service.CreateContact(contactObject);

            //Assert
            Assert.AreEqual(Id, contact.ID);
            this.mockUnitWork.Verify(m => m.Commit(), Times.Once);
        }
    }
}
