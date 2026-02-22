using ContactManager.Interfaces;
using System;
using System.Linq;

namespace ContactManager
{
    public class App
    {
        private bool _running = true;
        private ContactServiceInterface _service;

        public void Run()
        {
            Console.Clear();
            PrintWelcome();
            LoadAndDisplaySummary();

            while (_running)
            {
                ShowMainMenu();
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

    }
}
