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
            BitwardenClient bwClient = new BitwardenClient("bwclinet@email.cz", "JCgg4K8jRGFURA", true);
            bwClient.Login();

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
