using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Newtonsoft.Json;
using nmct.ba.webshop.models;
using nmct.ba.webshop.Services;
using nmct.ba.webshop.repositories;
using nmct.ba.webshop.interfaces;
using MailClient;
using nmct.ba.webshop.context;

namespace WebJob
{      
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called orders.
        public static void ProcessQueueMessage([QueueTrigger("orders")] string message, TextWriter log)
        {       
            IOrderRepository repoOrder = new OrderRepository();
            IOrderQueueRepository repoOrderQueue = new OrderQueueRepository();
            IUserRepository repoUser = new UserRepository();

            OrderService os = new OrderService(repoOrder, repoOrderQueue);
            Order orderInfo = JsonConvert.DeserializeObject<Order>(message);
            os.saveOrder(orderInfo);

            UserService us = new UserService(repoUser);
            ClientMail mailclient = new ClientMail();

            mailclient.SendMail(us.getAppUser(orderInfo.Gebruiker.ID), orderInfo);
        }

        
    }
}
