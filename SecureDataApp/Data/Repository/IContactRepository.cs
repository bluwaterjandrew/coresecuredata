using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using SecureDataApp.Models;

namespace SecureDataApp.Data.Repository
{
    public interface IContactRepository
    {
        IEnumerable<Contact> ListAllContacts(IdentityUser user);
        Contact ListContact(int contactId);
        Contact FindNoTrackingContact(int contactId);
        void AddContact(Contact contact);
        void DeleteContact(Contact contact);
        void EditContact(Contact contact);
    }
}
