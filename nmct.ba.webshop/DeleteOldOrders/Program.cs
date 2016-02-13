using nmct.ba.webshop.interfaces;
using nmct.ba.webshop.models;
using nmct.ba.webshop.repositories;
using nmct.ba.webshop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeleteOldOrders
{
    class Program
    {
        static void Main(string[] args)
        {
            IOrderRepository repoOrder = new OrderRepository();
            IOrderQueueRepository repoOrderQueue = new OrderQueueRepository();
            

            //ophalen van orders die langer dan een jaar bestaan
            OrderService os = new OrderService(repoOrder, repoOrderQueue);
            os.DeleteOldOrders();
        }
    }
}
