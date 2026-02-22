using ContactManager.Data;
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContactManager.Services
{
    public class ContactService : Interfaces.ContactServiceInterface
    {
        private  ContactStore _cs;
        private  JsonDataManager _jdm;
        public ContactService(ContactStore cs, JsonDataManager jdm)
        {
            _cs = cs;
            _jdm = jdm;
            var contacts = _jdm.Load();
            _cs.LoadFrom(contacts);
        }
        public Contact AddContact(string name, string phone, string email)
        {
            ValidateInfo(name, phone, email);
            var contact = new Contact(name, phone, email);
            _cs.Add(contact);
            return contact;
        }

        public bool DeleteContact(Guid id)
        {
            try
            {
                _cs.Delete(id);
                return true;
            }
            catch (KeyNotFoundException)
            {
                return false;
            }
        }

        public bool EditContact(Guid id, string name, string phone, string email)
        {
            var contact = _cs.GetById(id);
            if (contact == null) return false;
            ValidateInfo(name, phone, email);
            contact.Email = email; 
            contact.Name = name;
            contact.Phone = phone;
            _cs.Update(contact);
            return true;
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
        private static void ValidateInfo(string name, string phone, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone cannot be empty.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.");
            if (!email.Contains('@'))
                throw new ArgumentException("Email must contain '@'.");
        }   
    }
}
