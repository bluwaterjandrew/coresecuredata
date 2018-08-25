using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Formatters;
using SecureDataApp.Authorization;
using SecureDataApp.Data;
using SecureDataApp.Data.Repository;
using SecureDataApp.Models;

namespace SecureDataApp.Controllers
{
    [Authorize]
    public class ContactsController : Controller
    {

        private readonly IContactRepository _contactRepository;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAuthorizationService _authorizationService;

        public ContactsController(IContactRepository contactRepository, 
                                    UserManager<IdentityUser> userManager,
                                    IAuthorizationService authorizationService)
        {
            _contactRepository = contactRepository;
            _userManager = userManager;
            _authorizationService = authorizationService;

        }

        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            IEnumerable<Contact> contacts = _contactRepository.ListAllContacts(user);
            return View(contacts);
        }

        [HttpGet("contacts/details/{contactId}")]
        public async Task<IActionResult> Details(int contactId)
        {
            Contact contact = _contactRepository.ListContact(contactId);
            if (contact == null)
            {
                return NotFound();
            }
            var authorizationResult = await _authorizationService.AuthorizeAsync(User, contact, ContactOperations.Read);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            return View(contact);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Add(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }
            var user = await _userManager.GetUserAsync(User);
            contact.UserId = user.Id;
            _contactRepository.AddContact(contact);
            return RedirectToAction("Index");
        }

        [HttpGet("contacts/edit/{contactId}")]
        public async Task<IActionResult> Edit(int contactId)
        {
            Contact contact = _contactRepository.ListContact(contactId);
            var authorizationResult =
                await _authorizationService.AuthorizeAsync(User, contact, ContactOperations.Update);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }

            return View(contact);
            
        }

        [HttpPost("contacts/edit/{contactId}"), ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int contactId ,Contact contact)
        {
            var query = _contactRepository.FindNoTrackingContact(contactId);
            if (query.ContactId != contact.ContactId)
            {
                return BadRequest();
            }
            var authorizationResult =
                await _authorizationService.AuthorizeAsync(HttpContext.User, contact, ContactOperations.Update);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            if (!ModelState.IsValid)
            {
                return ValidationProblem();
            }
            _contactRepository.EditContact(contact);
            return RedirectToAction("Index");
        }

        
        [HttpGet("contacts/delete/{contactId}")]
        public async Task<IActionResult> Delete(int contactId)
        {
            Contact contact = _contactRepository.ListContact(contactId);
            var authorizationResult =
                await _authorizationService.AuthorizeAsync(User, contact, ContactOperations.Delete);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            return View(contact);
        }

        
        [HttpPost("contacts/delete/{contactId}"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Destroy(int contactId)
        {
            Contact contact = _contactRepository.ListContact(contactId);
            var authorizationResult =
                await _authorizationService.AuthorizeAsync(HttpContext.User, contact, ContactOperations.Delete);
            if (!authorizationResult.Succeeded)
            {
                return Forbid();
            }
            
            _contactRepository.DeleteContact(contact);
            return RedirectToAction("Index");
            
        }

    }
}