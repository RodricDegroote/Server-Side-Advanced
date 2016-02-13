using Microsoft.Azure;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Auth;
using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudService
{
    public class AzureStorage
    {
        public static CloudBlockBlob getBlobReference(string fileName, string connectiestring, string containername)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting(connectiestring));

            // Create the blob client.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(containername);

            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(fileName);
            return blockBlob;
        }

        public static void saveOrderInQueue<T>(T orderInfo, string connectiestring, string containername) where T : class
        {
            //http://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-queues/
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(orderInfo);

            // Retrieve storage account from connection string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting(connectiestring));

            // Create the queue client
            CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();

            // Retrieve a reference to a queue
            CloudQueue queue = queueClient.GetQueueReference(containername);
            queue.CreateIfNotExists();

            CloudQueueMessage message = new CloudQueueMessage(json);
            queue.AddMessage(message);
        }

        public static void saveToTableStorage<T>(T temp, string connectiestring, string containername) where T : TableEntity
        {
            // Retrieve storage account from connection string
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting(connectiestring));

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the table if it doesn't exist.
            CloudTable table = tableClient.GetTableReference(containername);
            table.CreateIfNotExists();

            // Create a new temperature entity.
            // TemperatureEntity werd eerder gecreeërd namelijk de parameter temp die we meekrijgen in onze methode.

            // Create the TableOperation that inserts the customer entity.
            TableOperation insertOperation = TableOperation.Insert(temp);

            // Execute the insert operation.
            table.Execute(insertOperation);
        }
    }
}
