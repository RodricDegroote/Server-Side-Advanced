using nmct.ba.webshop.interfaces;
using nmct.ba.webshop.models;
using nmct.ba.webshop.repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.webshop.cache.InternalService
{
    public class CacheInternalService<TEntity>
    {
        public static IProductRepository repoProduct = new ProductRepository();
        public static IGenericRepository<Framework> repoFramework = null;
        public static IGenericRepository<OS> repoOS = null;
        public static IBasketRepository repoBasket = new BasketRepository();
        public static List<TEntity> Objects = new List<TEntity>();
        private static IDatabase cache = null;


        public static void Setup()
        {
            try
            {
                cache = MakeCache.cache;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public static List<TEntity> GetObjectsFromCache(string key)
        {
            try
            {

                if (!cache.KeyExists(key) || cache == null)
                {
                    RefreshCash(key);
                }

                Objects = cache.Get(key) as List<TEntity>;
                return Objects;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                RefreshCash(key);
                return Objects;
            }

        }

        public static int GetCountBasketItemsCache(string key, string userId)
        {

            try
            {
                if (!cache.KeyExists("Aantal")) 
                    RefreshCount(key, userId);

                RefreshCount(key, userId);
                return Convert.ToInt32(cache.Get("Aantal"));
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return repoBasket.getWinkelWagen(userId).Count();
            }

        }

        public static void RefreshCash(string key)
        {
            switch(key)
            { 
                case "Producten":
                    Objects = repoProduct.All().ToList() as List<TEntity>;
                    break;
                case "Frameworks":
                    repoFramework = new GenericRepository<Framework>();
                    Objects = repoFramework.All().ToList() as List<TEntity>;
                    break;
                case "OperatingSystems":
                    repoOS = new GenericRepository<OS>();
                    Objects = repoOS.All().ToList() as List<TEntity>;
                    break;
            }

            if(cache != null)
                cache.Set(key, Objects);
        }

        public static void RefreshCount(string key, string userId)
        {
            List<Basket> Baskets = repoBasket.getWinkelWagen(userId).ToList<Basket>();

            if(cache != null)
                cache.Set("Aantal", Baskets.Count);
        }
    }
}
