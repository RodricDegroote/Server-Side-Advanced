using nmct.ba.webshop.cache;
using nmct.ba.webshop.cache.InternalService;
using nmct.ba.webshop.models;
using nmct.ba.webshop.repositories;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace nmct.ba.webshop.Cache
{
    public class WebshopCache
    {     
        public static List<Product> GetProductenFromCache(string key)
        {
            return CacheInternalService<Product>.GetObjectsFromCache(key);
        }

        public static List<Framework> GetFrameworksFromCache(string key)
        {
            return CacheInternalService<Framework>.GetObjectsFromCache(key);
        }

        public static List<OS> GetOperatingSystemsFromCache(string key)
        {
            return CacheInternalService<OS>.GetObjectsFromCache(key);
        }

        public static void RefreshCash(string key)
        {
            CacheInternalService<OS>.RefreshCash(key);
        }

        public static int getCountBasketCache(string key, string userId)
        {
            return CacheInternalService<Basket>.GetCountBasketItemsCache(key, userId);
        }

        public static void RefreshCashCount(string key, string userId)
        {
            CacheInternalService<Basket>.RefreshCount(key, userId);
        }
    }
}