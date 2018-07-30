using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ContactLibrary.Web.Controllers
{
    using ContactLibrary.Data.Service;
    using Domain;

    public class HomeController : Controller
    {
        private readonly IContactService _contactService;
        public HomeController(ContactService contactService)
        {
            _contactService = contactService;
        }
        // GET: Home
        public ActionResult Index()
        {
            var contacts = _contactService.GetContacts();
            return View(contacts);
        }

        [HttpGet]
        public ActionResult Edit(long? id)
        {
            if (!ModelState.IsValid)
                return null;

            ContactObject obj = id.HasValue ? _contactService.GetContact(id.Value) : new ContactObject();
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ContactObject obj)
        {
            if (ModelState.IsValid)
            {
                if (obj.ID == default(Int64))
                {
                    _contactService.CreateContact(obj);
                }
                else
                {
                    _contactService.UpdateContact(obj);
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long id)
        {
            if (ModelState.IsValid)
            {
                var contact = _contactService.GetContact(id);
                _contactService.DeleteContact(contact);
            }
            return RedirectToAction("Index");
        }


    }
}