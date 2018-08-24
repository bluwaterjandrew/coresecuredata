using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SecureDataApp.Data;
using SecureDataApp.Data.Repository;
using SecureDataApp.Models;

namespace SecureDataApp.Controllers
{
    public class ContactsController : Controller
    {

        private readonly IContactRepository _contactRepository;

        public ContactsController(IContactRepository contactRepository)
        {
            _contactRepository = contactRepository;
        }

        public IActionResult Index()
        {
            IEnumerable<Contact> _contacts = _contactRepository.ListAllContacts();
            return View(_contacts);
        }

        [HttpGet("contacts/details/{contactId}")]
        public IActionResult Details(int contactId)
        {
            Contact _contact = _contactRepository.ListContact(contactId);
            if (_contact != null)
            {
                return View(_contact);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(Contact contact)
        {
            _contactRepository.AddContact(contact);
            return RedirectToAction("Index");
        }

        [HttpGet("contacts/edit/{contactId}")]
        public IActionResult Edit(int contactId)
        {
            Contact _contact = _contactRepository.ListContact(contactId);
            if (_contact != null)
            {
                return View(_contact);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Contact contact)
        {
            _contactRepository.EditContact(contact);
            return RedirectToAction("Index");
        }

        [HttpGet("contacts/delete/{contactId}")]
        public IActionResult Delete(int contactId)
        {
            Contact _contact = _contactRepository.ListContact(contactId);
            if (_contact != null)
            {
                return View(_contact);
            }
            else
            {
                return NotFound();
            }
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult Destroy(Contact contact)
        {
            _contactRepository.DeleteContact(contact);
            return RedirectToAction("Index");
        }

    }
}