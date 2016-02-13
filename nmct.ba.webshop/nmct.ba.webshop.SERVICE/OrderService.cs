using Calculater;
using nmct.ba.webshop.interfaces;
using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.webshop.Services
{
    public class OrderService : IOrderService
    {
        private IOrderRepository repoOrder = null;
        private IOrderQueueRepository repoOrderQueue = null;

        public OrderService(IOrderRepository repoOrder, IOrderQueueRepository repoOrderQueue)
        {
            this.repoOrder = repoOrder;
            this.repoOrderQueue = repoOrderQueue;
        }

        public List<OrderLine> getOrderLines(string userId)
        {
            return repoOrder.getOrderLines(userId);
        }

        public List<OrderLine> makeOrderLineList(List<Basket> bask, string userId)
        {
            //orderline lijst aanmaken om die dan te gaan opslaan in onze tabel orders in de database
            List<OrderLine> OrderLines = new List<OrderLine>();


            foreach (Basket bas in bask)
            {
                OrderLine orderline = new OrderLine()
                {
                    BasketID = bas.ID,
                    BasketItem = bas,
                    TotaalPrijs = Calculate.TotaalPrijs(bas),
                    UserID = userId
                };

                OrderLines.Add(orderline);
            }

            return OrderLines;
        }

        public void saveOrder(Order orderInfo)
        {
            //de definitieve order, inclusief gegevens van de klant, gaan opslaan in de order tabel
            repoOrder.saveOrder(orderInfo);
        }

        public void saveOrderInQueue(Order orderInfo)
        {
            //orderinfo in de queue gaan schrijven
            repoOrderQueue.saveOrderInQueue(orderInfo);
        }

        public void DeleteOldOrders()
        {
            repoOrder.DeleteOldOrders(DateTime.Now.AddYears(-1));
        }
    }
}
