using System;
using System.IO;
using Microsoft.Azure.Storage.Blob;
using Microsoft.Azure.Storage;

namespace DataWarehouseLoad
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Enter your storage account connection string: ");
            string connectionString = Console.ReadLine();
            var account = CloudStorageAccount.Parse(connectionString);
            var client = account.CreateCloudBlobClient();
            var container = client.GetContainerReference("demographics");
            container.CreateIfNotExists();
            foreach(var file in Directory.GetFiles($"{Environment.CurrentDirectory}//CountyDelimited")) {
                var blob = container.GetBlockBlobReference(Path.GetFileName(file).ToLower());
                blob.UploadFromFile(file);
            }
            Console.WriteLine("Done.");
        }
    }
}
