using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using Microsoft.WindowsAzure.Storage.Queue;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure;
using System.Data;
using Calculater;
using nmct.ba.webshop.context;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using nmct.ba.webshop.models;
using nmct.ba.webshop.interfaces;

namespace nmct.ba.webshop.repositories
{
    //repositorie bevat de code die zal gebruik maken van onze database
    public class BasketRepository : GenericRepository<Basket>, IBasketRepository
    {
        public IEnumerable<Basket> getWinkelWagen(string userId)
        {
                var query = (from f in context.BasketItems.Include(f => f.prod)
                             where f.UserID == userId && f.Deleted == 0
                             select f) ;
                return query.ToList<Basket>();
        }
        public void deleteBasket(List<Basket> bask)
        {
                foreach(Basket bas in bask)
                {
                    bas.Deleted = 1;
                    context.Entry<Product>(bas.prod).State = EntityState.Unchanged;
                    context.Entry<Basket>(bas).State = EntityState.Modified;
                }

                context.SaveChanges();
        }


    }
}