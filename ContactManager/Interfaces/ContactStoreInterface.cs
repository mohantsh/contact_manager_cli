using ContactManager.Models;
using System;
using System.Collections.Generic;

namespace ContactManager.Interfaces
{
    public interface ContactStoreInterface
    {
        void Add(Contact contact);
        void Update(Contact contact);
        void Delete(Guid id);
        Contact GetById(Guid id);
        IEnumerable<Contact> GetAll();
        IEnumerable<Contact> Search(string query);
        IEnumerable<Contact> Filter(Func<Contact, bool> predicate);
    }
}
