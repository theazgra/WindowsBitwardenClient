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
            bwClient.Login();
            bwClient.Logout();

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
