using Microsoft.VisualStudio.TestTools.UnitTesting;
using ContactLibrary.Data.Service;
using ContactLibrary.Web.Controllers;
using Moq;
using System.Collections.Generic;
using System.Linq;
using ContactLibrary.Core.Mappers;
using ContactLibrary.Domain;
using System.Web.Mvc;

namespace ContactLibrary.Tests.Controllers
{
    [TestClass]
    public class HomeControllerTest
    {
        private HomeController homeController;
        private Mock<IContactService> contactServiceMock;
        IQueryable<ContactObject> listContactObjects;

        [TestInitialize]
        public void Initialize()
        {
            contactServiceMock = new Mock<IContactService>();
            homeController = new HomeController(contactServiceMock.Object);
            listContactObjects = new List<ContactObject>() {
             new ContactObject()
                 {
                     ID = 1, FirstName = "Chirag", LastName = "Daxini", Email = "daxinichirag26@gmail.com", PhoneNumber = "9623252606", IsActive = true
                 },
             new ContactObject() { ID = 2, FirstName = "Hemal",LastName = "Patel", Email = "Hemal.Patel@gmail.com", PhoneNumber = "1234567890",IsActive = true},
             new ContactObject() { ID = 3, FirstName = "Vinayak", LastName = "Dave", Email = "Vinayak.Dave@gmail.com", PhoneNumber = "8866625979", IsActive = true}
            }.AsQueryable();

            contactServiceMock.Setup(x => x.GetContacts()).Returns(listContactObjects);
            contactServiceMock.Setup(x => x.GetContact(It.IsAny<long>())).Returns((long x) => this.listContactObjects.FirstOrDefault(id => id.ID == x));
            contactServiceMock.Setup(x => x.CreateContact(new ContactObject() { ID = 4, Email = "test@gmail.com", FirstName = "Unit", LastName = "Test", PhoneNumber = "123", IsActive = true }));
        }

        [TestMethod]
        public void Given_HomeController_Index()
        {
            //Act
            var result = ((homeController.Index() as ViewResult).Model) as IEnumerable<ContactObject>;

            //Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(result.Count(), 3);
        }

        [TestMethod]
        public void Given_HomeController_EditGet()
        {
            var contact = (homeController.Edit(1) as ViewResult).Model as ContactObject;

            Assert.IsNotNull(contact);
            Assert.IsTrue(contact.ID == 1);
        }

        [TestMethod]
        public void Given_HomeController_EditPost()
        {
            var postedObject = listContactObjects.Take(2).FirstOrDefault();

            var redirectResult = (homeController.Edit(postedObject) as RedirectToRouteResult);

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
        }

        [TestMethod]
        public void Given_HomeController_Negative_EditPost()
        {
            var postedObject = listContactObjects.Take(2).FirstOrDefault();

            var redirectResult = (homeController.Edit(postedObject) as RedirectToRouteResult);

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
        }

        [TestMethod]
        public void Given_HomeController_Delete()
        {
            var redirectResult = (homeController.Delete(1) as RedirectToRouteResult);

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual("Index", redirectResult.RouteValues["action"]);
        }

        [TestMethod]
        public void Given_HomeController_Negative_Delete()
        {
            var redirectResult = (homeController.Delete(null) as HttpStatusCodeResult);

            Assert.IsNotNull(redirectResult);
            Assert.AreEqual(redirectResult.StatusCode, 400);
        }
    }
}
