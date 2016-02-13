using System;
namespace nmct.ba.webshop.interfaces
{
    public interface IOrderQueueRepository
    {
        void saveOrderInQueue(nmct.ba.webshop.models.Order orderInfo);
    }
}
