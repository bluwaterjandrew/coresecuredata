using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecureDataApp.Data.Repository;
using SecureDataApp.Models;

namespace SecureDataApp.Data
{
    public class ContactRepository: IContactRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public ContactRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<Contact> ListAllContacts(IdentityUser user)
        {
            return _dbContext.Contacts.Where(c => c.UserId == user.Id).AsEnumerable();
        }

        public Contact ListContact(int contactId)
        {

            return _dbContext.Contacts.Find(contactId);
        }

        public Contact FindNoTrackingContact(int contactId)
        {
            return _dbContext.Contacts.AsNoTracking().FirstOrDefault(c => c.ContactId == contactId);
        }

        public void AddContact(Contact contact)
        {
            _dbContext.Contacts.Add(contact);
            _dbContext.SaveChanges();
        }

        public void DeleteContact(Contact contact)
        {
            _dbContext.Contacts.Remove(contact);
            _dbContext.SaveChanges();

        }

        public void EditContact(Contact contact)
        {
            _dbContext.Contacts.Update(contact);
            _dbContext.SaveChanges();
        }

    }
}
