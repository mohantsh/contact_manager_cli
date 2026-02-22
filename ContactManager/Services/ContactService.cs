using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager.Services
{
    public class ContactService : Interfaces.ContactServiceInterface
    {
        public Contact AddContact(string name, string phone, string email)
        {
            throw new NotImplementedException();
        }

        public bool DeleteContact(Guid id)
        {
            throw new NotImplementedException();
        }

        public bool EditContact(Guid id, string name, string phone, string email)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> FilterContacts(string field, string value)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> ListContacts()
        {
            throw new NotImplementedException();
        }

        public void SaveContacts()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Contact> SearchContacts(string query)
        {
            throw new NotImplementedException();
        }

        public Contact ViewContact(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
