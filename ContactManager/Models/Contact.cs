using System;

namespace ContactManager.Models
{
    public class Contact
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; private set; }

        public Contact(string name, string phone, string email)
        {
            Id = Guid.NewGuid();
            Name = name;
            Phone = phone;
            Email = email;
            CreatedAt = DateTime.Now;
        }
        public Contact(Guid id, string name, string phone, string email, DateTime createdAt)
        {
            Id = id;
            Name = name;
            Phone = phone;
            Email = email;
            CreatedAt = createdAt;
        }

        public override string ToString()
        {
            return $"[{Id}] {Name} | {Phone} | {Email} | Created: {CreatedAt:yyyy-MM-dd HH:mm}";
        }
    }
}
