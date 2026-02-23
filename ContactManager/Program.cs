using ContactManager.Data;
using ContactManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var data = new JsonDataManager("Storage/contacts.json");
            var cs = new ContactStore();
            var service = new ContactService(cs, data);
            var app = new App(service);
            app.Run();
        }
    }
}
