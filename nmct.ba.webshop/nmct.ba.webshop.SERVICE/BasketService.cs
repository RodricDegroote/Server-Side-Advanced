using nmct.ba.webshop.Cache;
using nmct.ba.webshop.interfaces;
using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ba.webshop.Services
{
    public class BasketService : IBasketService
    {

        private IBasketRepository repoBasket = null;

        public BasketService(IBasketRepository repoBasket)
        {
            this.repoBasket = repoBasket;
        }

        public void saveBasket(int VerhurenAantal, int ProductId, string id)
        {
            #region AanmakenBasket
            Basket item = new Basket()
            {
                Aantal = VerhurenAantal,
                UserID = id,
                ProductID = ProductId,
                TimeStamp = DateTime.Now
            };
            #endregion
            repoBasket.Insert(item);
            repoBasket.SaveChanges();
            WebshopCache.RefreshCashCount("Aantal", id);
        }

        public IEnumerable<Basket> getBasketItems(string userId)
        {
            return repoBasket.getWinkelWagen(userId);
        }

        public void deleteBasket(List<Basket> bask, string userId)
        {
            repoBasket.deleteBasket(bask);
            WebshopCache.RefreshCashCount("Aantal", userId);
        }

        public Basket getBasket(int id)
        {
            return repoBasket.GetByID(id);
        }

        public void updateBasket(Basket bas)
        {
            repoBasket.Update(bas);
            repoBasket.SaveChanges();
        }
    }
}