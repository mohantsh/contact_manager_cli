# Contact Manager

A command-line app to manage contacts. Built with C# and .NET 8.

---

## Requirements

- .NET 8 SDK — check with `dotnet --version`

---

## Getting Started

```bash
git clone https://github.com/your-username/ContactManager.git
cd ContactManager
dotnet run
```

---

## What it does

On launch, the app loads any saved contacts and shows a menu with 9 options.

```
1. Add Contact
2. Edit Contact
3. Delete Contact
4. View Contact
5. List Contacts
6. Search
7. Filter
8. Save
9. Exit
```

Type a number and press Enter.

**Add** — enter a name, phone, and email. ID and creation date are generated automatically.

**Edit** — pick a contact from the list, then update its fields. Press Enter on any field to keep the current value.

**Delete** — pick a contact and confirm. Cannot be undone.

**View** — shows full details for one contact including ID and exact creation time.

**List** — shows all contacts sorted alphabetically in a table.

**Search** — searches across name, phone, and email at once. Case-insensitive.

**Filter** — narrows results by a specific field: name, phone, email, or date.

**Save** — writes contacts to `contacts.json` in the project folder. The app does not auto-save, so remember to save before exiting. Exit (9) will also prompt you to save.

---

## Data

Contacts are saved to `ContactManager\bin\Debug\Storage\contacts.json`. It's a plain JSON file you can back up or move between machines.

---

## Project Structure

```
ContactManager/
├── Models/           # Contact entity
├── Interfaces/       # Contracts for service    
├── Services/         # ContactService — validation and logic 
├                     # ContactStore — dictionary-based lookup
├── Data/             # JsonDataManager — reads and writes contacts.json
└── Program.cs        # Entry point
```
