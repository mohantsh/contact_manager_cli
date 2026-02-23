using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using ContactManager.Models;

namespace ContactManager.Data
{
    public class JsonDataManager

    {
        private readonly string _filePath;

        private static readonly JsonSerializerOptions _options = new JsonSerializerOptions()
        {
            WriteIndented = true,
            PropertyNameCaseInsensitive = true
        };

        public JsonDataManager(string filePath = "Storage/contacts.json")
        {
            var dir = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(dir))
                Directory.CreateDirectory(dir);

            _filePath = filePath;
        }

        public IEnumerable<Contact> Load()
        {
            if (!File.Exists(_filePath))
                return Array.Empty<Contact>();

            try
            {
                var json = File.ReadAllText(_filePath);
                var dtos = JsonSerializer.Deserialize<List<ContactDto>>(json, _options)
                           ?? new List<ContactDto>();
                var contacts = new List<Contact>();
                foreach (var dto in dtos)
                    contacts.Add(new Contact(dto.Id, dto.Name, dto.Phone, dto.Email, dto.CreatedAt));

                return contacts;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Warning] Failed to load contacts: {ex.Message}");
                return Array.Empty<Contact>();
            }
        }

        public void Save(IEnumerable<Contact> contacts)
        {
            var dtos = new List<ContactDto>();
            foreach (var c in contacts)
                dtos.Add(new ContactDto
                {
                    Id = c.Id,
                    Name = c.Name,
                    Phone = c.Phone,
                    Email = c.Email,
                    CreatedAt = c.CreatedAt
                });

            var json = JsonSerializer.Serialize(dtos, _options);
            File.WriteAllText(_filePath, json);
        }

        private class ContactDto 
        {
            public Guid Id { get; set; }
            public string Name { get; set; } = "";
            public string Phone { get; set; } = "";
            public string Email { get; set; } = "";
            public DateTime CreatedAt { get; set; }
        }
    }
}
