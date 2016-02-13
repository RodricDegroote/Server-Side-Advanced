using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
namespace nmct.ba.webshop.interfaces
{
    public interface IBasketService
    {
        void deleteBasket(List<Basket> bask, string userId);
        IEnumerable<Basket> getBasketItems(string userId);
        void saveBasket(int VerhurenAantal, int ProductId, string id);
        Basket getBasket(int id);
        void updateBasket(Basket bas);
    }
}
