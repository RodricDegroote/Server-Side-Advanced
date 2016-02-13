using CloudService;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;
using nmct.ba.webshop.interfaces;
using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.webshop.repositories
{
    public class OrderQueueRepository : IOrderQueueRepository
    {
        // OFWEL een nieuwe repositorie aanmaken OFWEL deze gaan toevoegen aan onze service layer. Want het is namelijk een 
        //service die we aanroepen.
        public void saveOrderInQueue(Order orderInfo)
        {
            AzureStorage.saveOrderInQueue<Order>(orderInfo, "StorageConnectionString", "orders");
        }
    }
}
