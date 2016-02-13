using Calculater;
using nmct.ba.webshop.context;
using nmct.ba.webshop.interfaces;
using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.webshop.repositories
{
    public class OrderRepository: GenericRepository<Order>, IOrderRepository
    {
        public void saveOrder(Order orderInfo)
        {
            foreach (OrderLine orderline in orderInfo.OrderLines)
            {
                context.Entry<Basket>(orderline.BasketItem).State = EntityState.Unchanged;
                context.Entry<Product>(orderline.BasketItem.prod).State = EntityState.Unchanged;
            }


            var query = (from f in context.User
                         where f.ID == orderInfo.Gebruiker.ID
                         select f);

            if(query.Count() != 0)
            {
                context.Entry<User>(orderInfo.Gebruiker).State = EntityState.Unchanged;
            }

            context.Order.Add(orderInfo);
            context.SaveChanges();
        }

        public List<OrderLine> getOrderLines(string userId)
        {
            var query = (from f in context.OrderLines.Include(f => f.BasketItem)
                         where f.UserID == userId
                         select f);
            return query.ToList<OrderLine>();
        }

        public void DeleteOldOrders(DateTime OldDateTime)
        {
            List<Order> orders = (from f in context.Order
                         where f.TimeStamp <= OldDateTime
                         select f).ToList<Order>();

            foreach(Order ord in orders)
            {
                ord.Deleted = 1;
                context.Entry<Order>(ord).State = EntityState.Modified;
            }

            context.SaveChanges();
        }
    }
}
