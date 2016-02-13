using System;
namespace nmct.ba.webshop.Services
{
    public interface IServiceBusService
    {
        void MakeSubscription();
        void MakeTopic(nmct.ba.webshop.models.FormPost post);
        void SaveSubscription(nmct.ba.webshop.models.FormPost post);
    }
}
