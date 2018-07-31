using ContactLibrary.Domain;
using ContactLibrary.Tests.Shared;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace ContactLibrary.Tests.Models
{
    [TestClass]
    public class ModelValidationTest
    {
        [TestMethod]
        public void Given_ContactModel_ValidateAll()
        {
            ContactObject objContactObject = new ContactObject() { Email = "a@a.com", FirstName = "Chirag", LastName = "Daxini", IsActive = true, PhoneNumber = "9623252606" };

            var validationResult = ModelValidationHelper.Validate(objContactObject);

            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.Count == 0);
        }
        [TestMethod]
        public void Given_ContactModel_FirstName_CheckMinLength()
        {
            ContactObject objContactObject = new ContactObject() { Email = "a@a.com", FirstName = "A", LastName = "Daxini", IsActive = true, PhoneNumber = "9623252606" };

            var validationResult = ModelValidationHelper.Validate(objContactObject);
            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.Count == 1);
            Assert.IsTrue(validationResult[0].MemberNames.FirstOrDefault() == "FirstName");
            Assert.IsTrue(validationResult[0].ErrorMessage == "First Name must be between 3 characters to 20 characters");
        }
        [TestMethod]
        public void Given_ContactModel_FirstName_CheckEmpty()
        {
            ContactObject objContactObject = new ContactObject() { Email = "a@a.com", FirstName = "", LastName = "Daxini", IsActive = true, PhoneNumber = "9623252606" };

            var validationResult = ModelValidationHelper.Validate(objContactObject);
            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.Count == 1);
            Assert.IsTrue(validationResult[0].MemberNames.FirstOrDefault() == "FirstName");
            Assert.IsTrue(validationResult[0].ErrorMessage == "The FirstName field is required.");
        }
        [TestMethod]
        public void Given_ContactModel_FirstName_CheckMaxlength()
        {
            ContactObject objContactObject = new ContactObject() { Email = "a@a.com", FirstName = "123456789000000000000", LastName = "Daxini", IsActive = true, PhoneNumber = "9623252606" };

            var validationResult = ModelValidationHelper.Validate(objContactObject);
            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.Count == 1);
            Assert.IsTrue(validationResult[0].MemberNames.FirstOrDefault() == "FirstName");
            Assert.IsTrue(validationResult[0].ErrorMessage == "First Name must be between 3 characters to 20 characters");
        }
        [TestMethod]
        public void Given_ContactModel_LastName_CheckMinLength()
        {
            ContactObject objContactObject = new ContactObject() { Email = "a@a.com", FirstName = "Chirag", LastName = "AB", IsActive = true, PhoneNumber = "9623252606" };

            var validationResult = ModelValidationHelper.Validate(objContactObject);
            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.Count == 1);
            Assert.IsTrue(validationResult[0].MemberNames.FirstOrDefault() == "LastName");
            Assert.IsTrue(validationResult[0].ErrorMessage == "Last Name must be between 3 characters to 20 characters");
        }
        [TestMethod]
        public void Given_ContactModel_LastName_CheckEmpty()
        {
            ContactObject objContactObject = new ContactObject() { Email = "a@a.com", FirstName = "Chirag", LastName = "", IsActive = true, PhoneNumber = "9623252606" };

            var validationResult = ModelValidationHelper.Validate(objContactObject);
            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.Count == 1);
            Assert.IsTrue(validationResult[0].MemberNames.FirstOrDefault() == "LastName");
            Assert.IsTrue(validationResult[0].ErrorMessage == "The LastName field is required.");
        }
        [TestMethod]
        public void Given_ContactModel_LastName_CheckMaxlength()
        {
            ContactObject objContactObject = new ContactObject() { Email = "a@a.com", LastName = "123456789000000000000", FirstName = "Daxini", IsActive = true, PhoneNumber = "9623252606" };

            var validationResult = ModelValidationHelper.Validate(objContactObject);
            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.Count == 1);
            Assert.IsTrue(validationResult[0].MemberNames.FirstOrDefault() == "LastName");
            Assert.IsTrue(validationResult[0].ErrorMessage == "Last Name must be between 3 characters to 20 characters");
        }
        [TestMethod]
        public void Given_ContactModel_Email_CheckFormat()
        {
            ContactObject objContactObject = new ContactObject() { Email = "a", LastName = "Chirag", FirstName = "Daxini", IsActive = true, PhoneNumber = "9623252606" };

            var validationResult = ModelValidationHelper.Validate(objContactObject);
            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.Count == 1);
            Assert.IsTrue(validationResult[0].MemberNames.FirstOrDefault() == "Email");
            Assert.IsTrue(validationResult[0].ErrorMessage == "Email is not valid");
        }
        [TestMethod]
        public void Given_ContactModel_Phone_CheckFormat()
        {
            ContactObject objContactObject = new ContactObject() { Email = "a@a.com", LastName = "Chirag", FirstName = "Daxini", IsActive = true, PhoneNumber = "abcdefghid" };

            var validationResult = ModelValidationHelper.Validate(objContactObject);
            Assert.IsNotNull(validationResult);
            Assert.IsTrue(validationResult.Count == 1);
            Assert.IsTrue(validationResult[0].MemberNames.FirstOrDefault() == "PhoneNumber");
            Assert.IsTrue(validationResult[0].ErrorMessage == "Phone number is not valid");
        }
    }
}
