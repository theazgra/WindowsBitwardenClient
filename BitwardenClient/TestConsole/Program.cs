using BitwardenNET;
using BitwardenNET.VaultTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestConsole
{
    class Program
    {
        static StringBuilder stdoutBuffer = new StringBuilder();
        static async Task Main(string[] args)
        {
            string[] secrets = File.ReadAllLines("../../../../secrets.txt");
            using (BitwardenClient bwClient = new BitwardenClient(secrets[0], secrets[1], false))
            {
                for (int i = 5; i < 20; i++)
                {
                    Console.WriteLine(bwClient.Generator.GenerateString((short)i));
                }
                //bwClient.Login();
                //bwClient.UnlockVault();

                //var vaultData = await bwClient.GetVaultDataAsync();
                //if (vaultData.Success)
                //{

                //    Console.WriteLine("Vault folders listing:");
                //    foreach (VaultFolder folder in vaultData.Result.VaultFolders)
                //    {
                //        Console.WriteLine(folder);
                //    }

                //    Console.WriteLine("Vault items listing:");
                //    foreach (VaultItem item in vaultData.Result.VaultItems)
                //    {
                //        Console.WriteLine(item);
                //    }
                //}
            }
        }

        private static void DataRecv(object sender, DataReceivedEventArgs e)
        {
            stdoutBuffer.Append(e.Data);
        }
    }
}
