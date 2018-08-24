using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SecureDataApp.Models;

namespace SecureDataApp.Data.Repository
{
    public interface IContactRepository
    {
        IEnumerable<Contact> ListAllContacts();
        Contact ListContact(int contactId);
        void AddContact(Contact contact);
        void DeleteContact(Contact contact);
        void EditContact(Contact contact);
    }
}
