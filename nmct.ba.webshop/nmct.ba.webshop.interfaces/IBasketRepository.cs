using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
namespace nmct.ba.webshop.interfaces
{
    public interface IBasketRepository: IGenericRepository<Basket>
    {
        void deleteBasket(List<Basket> bask);
        IEnumerable<Basket> getWinkelWagen(string userId);
    }
}
