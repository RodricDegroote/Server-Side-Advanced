using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.webshop.cache.InternalService
{
    public class MakeCache
    {
        private static ConnectionMultiplexer connection = null;
        public static IDatabase cache = null;
        public static void Setup()
        {
            try
            {
                var config = new ConfigurationOptions();
                config.SyncTimeout = 5000;
                config.EndPoints.Add("rodricdegroote.redis.cache.windows.net");
                config.Ssl = true;

                //dit is de key die gegenereerd wordt in portal.azure.com
                config.Password = "5e7zJr6NCXWnLWqOVlEAkppgKa+Rl5qvzA3vCvTln1s=";

                connection = ConnectionMultiplexer.Connect(config);

                cache = connection.GetDatabase();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
