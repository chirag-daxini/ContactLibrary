using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Common;
using System.Linq;
using ContactLibrary.Core;
using ContactLibrary.Data.Repositories;

namespace ContactLibrary.Tests.Repositories
{
    [TestClass]
    public class ContactRepositoryTests
    {
        DbConnection connection;
        ContactLibraryTestContext databaseContext;
        ContactRepository objRepo;

        [TestInitialize]
        public void Initialize()
        {
            connection = Effort.DbConnectionFactory.CreateTransient();
            databaseContext = new ContactLibraryTestContext(connection);
            objRepo = new ContactRepository(databaseContext);
        }


        [TestMethod]
        public void Given_ContactRepository_GetAll()
        {
            var result = objRepo.GetAll().ToList();

            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void Given_ContactRepository_AddContact()
        {
            ContactEntity entity = new ContactEntity() { ID = 4, FirstName = "Prakash", LastName = "Rao", Email = "prakash.rao@gmail.com", PhoneNumber = "569896655", IsActive = true };
            objRepo.Add(entity);
            databaseContext.SaveChanges();

            var result = objRepo.GetAll().ToList();
            Assert.AreEqual(result.Count, 4);
        }

        [TestMethod]
        public void Given_ContactRepository_EditContact()
        {
            int updateId = 1;
            ContactEntity entity = objRepo.GetById(updateId);
            if (entity != null)
            {
                entity.FirstName = "TestUpdate";
            }

            objRepo.Edit(entity);
            databaseContext.SaveChanges();

            var result = objRepo.GetById(updateId);
            Assert.AreEqual(result.FirstName, "TestUpdate");
        }

        [TestMethod]
        public void Given_ContactRepository_DeleteContact()
        {
            int deleteId = 1;
            ContactEntity entity = objRepo.GetById(deleteId);

            objRepo.Delete(entity);
            databaseContext.SaveChanges();

            var result = objRepo.GetById(deleteId);
            Assert.AreEqual(result.IsActive, false);
        }
    }
}
