using BitwardenNET;
using BitwardenNET.VaultTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] secrets = File.ReadAllLines("../../../../secrets.txt");
            BitwardenClient bwClient = new BitwardenClient(secrets[0], secrets[1], true);
            if (bwClient.CheckForLoggedUser())
            {
                Console.WriteLine("There is logged user: {0}", bwClient.UserEmail);

                Console.WriteLine("Logging off the user...");
                if (bwClient.Logout())
                {
                    Console.WriteLine("Logout successful.");
                }
            }

            Console.WriteLine("Login in...");
            if (bwClient.Login())
            {
                Console.WriteLine("Login successful.");
            }

            Console.WriteLine("Unlocking the vault...");
            if (bwClient.UnlockVault())
            {
                Console.WriteLine("Unlock successful.");
            }

            Console.WriteLine("Locking the vault...");
            if (bwClient.LockVault())
            {
                Console.WriteLine("Lock successful.");
            }

            Console.WriteLine("Logging off the user...");
            if (bwClient.Logout())
            {
                Console.WriteLine("Logout successful.");
            }

            //using (StreamReader reader = new StreamReader("item_dump.json"))
            //{
            //    string json = reader.ReadToEnd();
            //    IEnumerable<VaultItem> items = JsonConvert.DeserializeObject<IEnumerable<VaultItem>>(json);
            //    Console.WriteLine(items);
            //}

            //using (StreamReader reader = new StreamReader("folder_dump.json"))
            //{
            //    string json = reader.ReadToEnd();
            //    IEnumerable<VaultFolder> folders = JsonConvert.DeserializeObject<IEnumerable<VaultFolder>>(json);
            //    Console.WriteLine(folders);
            //}
        }
    }
}
