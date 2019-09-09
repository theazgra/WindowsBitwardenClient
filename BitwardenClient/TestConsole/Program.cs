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

            //using (StreamReader reader = new StreamReader("single_item.json"))
            using (StreamReader reader = new StreamReader("item_dump.json"))
            {
                string json = reader.ReadToEnd();
                IEnumerable<VaultItem> items = JsonConvert.DeserializeObject<IEnumerable<VaultItem>>(json);
                //VaultItem items = JsonConvert.DeserializeObject<VaultItem>(json);

                Console.WriteLine(items);
            }
        }
    }
}
