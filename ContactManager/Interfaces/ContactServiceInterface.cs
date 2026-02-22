using ContactManager.Models;
using System;
using System.Collections.Generic;

namespace ContactManager.Interfaces
{
    public interface ContactServiceInterface
    {
        Contact AddContact(string name, string phone, string email);
        bool EditContact(Guid id, string name, string phone, string email);
        bool DeleteContact(Guid id);
        Contact ViewContact(Guid id);
        IEnumerable<Contact> ListContacts();
        IEnumerable<Contact> SearchContacts(string query);
        IEnumerable<Contact> FilterContacts(string field, string value);
        void SaveContacts();
    }
}
