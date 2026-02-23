using ContactManager.Interfaces;
using ContactManager.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ContactManager
{
    public class App
    {
        private bool _running = true;
        private readonly ContactServiceInterface _service;

        public App(ContactServiceInterface service)
        {
            _service = service;
        }
        public void Run()
        {
            Console.Clear();
            PrintWelcome();
            LoadAndDisplaySummary();

            while (_running)
            {
                ShowMainMenu();
                HandleMenuChoice(Prompt("Enter choice").ToLower());
            }
        }

        private void PrintWelcome()
        {
            Console.WriteLine("Welcome to Contact Manager Program!");
        }

        private void LoadAndDisplaySummary()
        {
            var contacts = _service.ListContacts().ToList();
            Console.WriteLine();
            if (contacts.Count == 0)
            {
                Console.WriteLine("No contacts found. Start by adding a new contact.");
            }
            else
            {
                Console.WriteLine($"Loaded {contacts.Count} contact(s) from storage.");
            }
        }
        private void ShowMainMenu()
        {
            Console.WriteLine("MAIN MENU");
            Console.WriteLine("   1. Add Contact");
            Console.WriteLine("   2. Edit Contact");
            Console.WriteLine("   3. Delete Contact");
            Console.WriteLine("   4. View Contact");
            Console.WriteLine("   5. List Contacts");
            Console.WriteLine("   6. Search");
            Console.WriteLine("   7. Filter");
            Console.WriteLine("   8. Save");
            Console.WriteLine("   9. Exit");
            Console.WriteLine();
        }

        private void HandleMenuChoice(string choice)
        {
            Console.Clear();
            try
            {
                switch (choice)
                {
                    case "1": AddContact(); break;
                    case "2": EditContact(); break;
                    case "3": DeleteContact(); break;
                    case "4": ViewContact(); break;
                    case "5": ListContacts(); break;
                    case "6": SearchContacts(); break;
                    case "7": FilterContacts(); break;
                    case "8": SaveContacts(); break;
                    case "9": Exit(); break;
                    default:
                        Console.WriteLine("Invalid option. Please enter a number between 1 and 9.");
                        PressAnyKey();
                        Console.Clear();
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                PressAnyKey();
                Console.Clear();
            }
        }

        private void Exit()
        {
            Console.WriteLine("EXIT");
            Console.WriteLine();
            Console.WriteLine("Goodbye :)");
            Console.WriteLine();
            _running = false;
        }

        private void SaveContacts()
        {
            Console.WriteLine("SAVE");
            _service.SaveContacts();
            Console.WriteLine("All contacts saved to disk.");
            PressAnyKey();
            Console.Clear();
        }

        private void FilterContacts()
        {

            Console.WriteLine("FILTER CONTACTS");
            Console.WriteLine("  Filter by field:");
            Console.WriteLine("    1. Name");
            Console.WriteLine("    2. Phone");
            Console.WriteLine("    3. Email");
            Console.WriteLine("    4. Date (yyyy-MM-dd)");
            Console.WriteLine();

            var fieldChoice = Prompt("Field (1-4)");
            string field = null;
            if (fieldChoice == "1")
                field = "name";
            else if (fieldChoice == "2")
                field = "phone";
            else if (fieldChoice == "3")
                field = "email";
            else if (fieldChoice == "4")
                field = "date";
            if (field == null)
            {
                Console.WriteLine("Invalid field selection.");
                PressAnyKey();
                Console.Clear();
                return;
            }
            var value = Prompt($"Filter value for '{field}'");
            var results = _service.FilterContacts(field, value).ToList();
            if (!results.Any())
            {
                Console.WriteLine($"No contacts match filter [{field} contains '{value}'].");
            }
            else
            {
                PrintContactList(results);
                Console.WriteLine();
                Console.WriteLine($"Found {results.Count} result(s).");
            }
            PressAnyKey();
            Console.Clear();
        }

        private void SearchContacts()
        {
            Console.WriteLine("SEARCH CONTACTS");
            var query = Prompt("Search (name / phone / email)");
            var results = _service.SearchContacts(query).ToList();

            if (!results.Any())
            {
                Console.WriteLine($"No contacts found matching '{query}'.");
            }
            else
            {
                PrintContactList(results);
                Console.WriteLine();
                Console.WriteLine($"Found {results.Count} result(s).");
            }

            PressAnyKey();
            Console.Clear();
        }

        private void ListContacts()
        {
           Console.WriteLine("Listing all contacts...");
            var contacts = _service.ListContacts().ToList();
            if (!contacts.Any())
            {
                Console.WriteLine("No contacts found. Add one from the main menu.");
            }
            else
            {
                PrintContactList(contacts);
                Console.WriteLine();
                Console.WriteLine($"Total: {contacts.Count} contact(s)");
            }
            PressAnyKey();
            Console.Clear();
        }

        private void ViewContact()
        {
            Console.WriteLine("VIEW CONTACT");
            var contacts = _service.ListContacts().ToList();
            if (!checkContactsCount(contacts)) return;
            PrintContactList(contacts);
            var contact = SelectContact(contacts);
            if (contact == null) { Console.Clear(); return; }
            PrintContactDetail(contact);
            PressAnyKey();
            Console.Clear();
        }

        private void DeleteContact()
        {

            Console.WriteLine("DELETE CONTACT");
            var contacts = _service.ListContacts().ToList();
            if (!checkContactsCount(contacts)) return;
            PrintContactList(contacts);
            var contact = SelectContact(contacts);
            if (contact == null) { Console.Clear(); return; }
            PrintContactDetail(contact);
            Console.WriteLine($"Are you sure you want to delete '{contact.Name}'? (y/n)");
            var input = Console.ReadLine()?.Trim().ToLower();
            if (input == "y" || input == "Y" || input == "yes")
            {
                if (_service.DeleteContact(contact.Id))
                    Console.WriteLine("Contact deleted.");
                else
                    Console.WriteLine("Contact not found.");
            }
            else
            {
                Console.WriteLine("Delete cancelled.");
            }

            PressAnyKey();
            Console.Clear();
        }

        private void EditContact()
        {
            Console.WriteLine("EDIT CONTACT");
            var contacts = _service.ListContacts().ToList();
           if(!checkContactsCount(contacts)) return;
            PrintContactList(contacts);
            var contact = SelectContact(contacts);
            if (contact == null) { Console.Clear(); return; }
            Console.WriteLine();
            Console.WriteLine("Leave blank to keep current value.");
            var name = PromptWithDefault("Name", contact.Name);
            var phone = PromptWithDefault("Phone", contact.Phone);
            var email = PromptWithDefault("Email", contact.Email);
            if (_service.EditContact(contact.Id, name, phone, email))
                Console.WriteLine($"Contact updated successfully.");
            else
                Console.WriteLine("Contact not found.");
            PressAnyKey();
            Console.Clear();
        }

        private void AddContact()
        {
            Console.WriteLine("Adding a new contact...");
            var name = Prompt("Name");
            var email = Prompt("Email");
            var phone = Prompt("Phone");
            var contact = _service.AddContact(name,phone,email);
            Console.WriteLine($"Contact '{contact.Name}' added successfully.");
            PrintContactDetail(contact);
            PressAnyKey();
            Console.Clear();
        }
        private void PrintContactList(List<Contact> contacts)
        {
            PrintTableHeader();
            for (int i = 0; i < contacts.Count; i++)
                PrintContact(contacts[i], i + 1);
        }

        private Contact SelectContact(List<Contact> contacts)
        {
            Console.WriteLine();
            var input = Prompt("Enter contact Number (or 0 to cancel)");
            if (!int.TryParse(input, out int idx) || idx == 0) return null;
            if (idx < 1 || idx > contacts.Count)
            {
                Console.WriteLine("Invalid number.");
                return null;
            }
            return contacts[idx - 1];
        }
        public static string Prompt(string label)
        {
            Console.Write($"  {label}: ");
            return Console.ReadLine()?.Trim() ?? "";
        }
        public static string PromptWithDefault(string label, string defaultValue)
        {
            Console.Write($"  {label} [{defaultValue}]: ");
            var input = Console.ReadLine()?.Trim();
            return string.IsNullOrEmpty(input) ? defaultValue : input;
        }
        public static void PrintContactDetail(Contact c)
        {
            Console.WriteLine();
            Console.WriteLine($"  ┌─ Contact Detail {'─'.ToString().PadRight(50, '─')}");
            Console.WriteLine($"  │  ID       : {c.Id}");
            Console.WriteLine($"  │  Name     : {c.Name}");
            Console.WriteLine($"  │  Phone    : {c.Phone}");
            Console.WriteLine($"  │  Email    : {c.Email}");
            Console.WriteLine($"  │  Created  : {c.CreatedAt:yyyy-MM-dd HH:mm:ss}");
            Console.WriteLine($"  └{'─'.ToString().PadRight(58, '─')}");
        }
        public static void PressAnyKey()
        {
            Console.WriteLine();
            Console.Write("  Press any key to continue...");
            Console.ReadKey(true);
        }
        public static void PrintContact(Contact c, int index = -1)
        {
            Console.ForegroundColor = ConsoleColor.White;
            if (index >= 0)
                Console.Write($"  [{index}] ");
            else
                Console.Write("  ");

            Console.Write($"{c.Name,-25}");
            Console.Write($"  {c.Phone,-18}");
            Console.Write($"  {c.Email,-30}");
            Console.WriteLine($"  {c.CreatedAt:yyyy-MM-dd}");
        }
        public static void PrintTableHeader()
        {
            Console.WriteLine();
            Console.WriteLine($"  {"#",-4} {"Name",-25}  {"Phone",-18}  {"Email",-30}  {"Created",-10}");
            Console.WriteLine($"  {new string('-', 4)} {new string('-', 25)}  {new string('-', 18)}  {new string('-', 30)}  {new string('-', 10)}");
        }
        bool checkContactsCount (List<Contact> contacts)
        {
            if (!contacts.Any())
            {
                Console.WriteLine("No contacts found.");
                PressAnyKey();
                Console.Clear();
                return false;
            } else
            {
                return true;
            }
        }
    }
}
