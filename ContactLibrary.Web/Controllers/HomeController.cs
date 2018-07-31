using System;
using System.Web.Mvc;
using System.Linq;
using System.Net;
using ContactLibrary.Data.Service;
using ContactLibrary.Domain;

namespace ContactLibrary.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IContactService _contactService;
        public HomeController(IContactService contactService)
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
            {
                var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
            }

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
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(long? id)
        {
            if (ModelState.IsValid && id.HasValue && id.Value > 0)
            {
                var contact = _contactService.GetContact(id.Value);
                _contactService.DeleteContact(contact);
                return RedirectToAction("Index");
            }

            var message = string.Join(" | ", ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage));
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest, message);
        }


    }
}