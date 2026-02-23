using ContactManager.Interfaces;
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace ContactManager.Services
{
    public class ContactStore : ContactStoreInterface
    {
        private readonly Dictionary<Guid, Contact> contactsDict = new Dictionary<Guid, Contact>();
        public void Add(Contact contact)
        {
            if (contactsDict.ContainsKey(contact.Id))
                throw new InvalidOperationException($"Contact with ID {contact.Id} already exists.");
            contactsDict[contact.Id] = contact;
        }
        public void Update(Contact contact)
        {
            if (!contactsDict.ContainsKey(contact.Id))
                throw new KeyNotFoundException($"Contact with ID {contact.Id} not found.");
            contactsDict[contact.Id] = contact;
        }
        public void Delete(Guid id)
        {
           if (!contactsDict.ContainsKey(id))
                throw new KeyNotFoundException($"Contact with ID {id} not found.");
            contactsDict.Remove(id);
        }
        public IEnumerable<Contact> GetAll()
        {
            return contactsDict.Values.OrderBy(c => c.Name);
        }

        public Contact GetById(Guid id)
        {
            return contactsDict.TryGetValue(id, out var contact) ? contact : null;
        }
        public IEnumerable<Contact> Filter(Func<Contact, bool> predicate)
        {
            return contactsDict.Values.Where(predicate).OrderBy(c => c.Name);
        }
        public IEnumerable<Contact> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query)) return GetAll();
            var q = query.ToLowerInvariant();
            return contactsDict.Values
                .Where(c =>
                    c.Name.ToLowerInvariant().Contains(q) ||
                    c.Phone.ToLowerInvariant().Contains(q) ||
                    c.Email.ToLowerInvariant().Contains(q))
                .OrderBy(c => c.Name);
        }
        public void LoadFrom(IEnumerable<Contact> contacts)
        {
            contactsDict.Clear();
            foreach (var c in contacts)
                contactsDict[c.Id] = c;
        }

        public IEnumerable<Contact> GetAllRaw() => contactsDict.Values;


    }
}
