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
            var v = value.ToLowerInvariant();
            switch (field.ToLower())
            {
                case "name":
                    return _cs.Filter(c => c.Name.ToLowerInvariant().Contains(v));
                case "phone":
                    return _cs.Filter(c => c.Phone.ToLowerInvariant().Contains(v));
                case "email":
                    return _cs.Filter(c => c.Email.ToLowerInvariant().Contains(v));
                case "date":
                    return _cs.Filter(c => c.CreatedAt.ToString("yyyy-MM-dd").Contains(v));
                default:
                    throw new ArgumentException($"Unknown filter field: {field}");
            }
        }

        public IEnumerable<Contact> ListContacts()
        {
           return _cs.GetAll();
        }

        public void SaveContacts()
        {
           _jdm.Save(_cs.GetAllRaw());
        }

        public IEnumerable<Contact> SearchContacts(string query)
        {
            return _cs.Search(query);
        }

        public Contact ViewContact(Guid id)
        {
            return _cs.GetById(id);
        }
        private static void ValidateInfo(string name, string phone, string email)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Name cannot be empty.");
            if (string.IsNullOrWhiteSpace(phone))
                throw new ArgumentException("Phone cannot be empty.");
            if (string.IsNullOrWhiteSpace(email))
                throw new ArgumentException("Email cannot be empty.");
            if (!email.Contains("@"))
                throw new ArgumentException("Email must contain '@'.");
        }   
    }
}
