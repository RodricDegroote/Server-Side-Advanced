using nmct.ba.webshop.models;
using System;
namespace nmct.ba.webshop.Services
{
    public interface IOrderService
    {
        System.Collections.Generic.List<nmct.ba.webshop.models.OrderLine> getOrderLines(string userId);
        System.Collections.Generic.List<nmct.ba.webshop.models.OrderLine> makeOrderLineList(System.Collections.Generic.List<nmct.ba.webshop.models.Basket> bask, string userId);
        void saveOrder(nmct.ba.webshop.models.Order orderInfo);
        void saveOrderInQueue(Order orderInfo);
    }
}
