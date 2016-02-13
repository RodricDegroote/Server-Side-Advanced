using System;
namespace nmct.ba.webshop.interfaces
{
    public interface IOrderRepository
    {
        System.Collections.Generic.List<nmct.ba.webshop.models.OrderLine> getOrderLines(string userId);
        void saveOrder(nmct.ba.webshop.models.Order orderInfo);

        void DeleteOldOrders(DateTime dateTime);
    }
}
