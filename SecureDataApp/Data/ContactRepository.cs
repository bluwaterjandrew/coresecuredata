using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public IEnumerable<Contact> ListAllContacts()
        {
            return _dbContext.Contacts.AsEnumerable();
        }

        public Contact ListContact(int contactId)
        {

            return _dbContext.Contacts.Find(contactId);
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
