using nmct.ba.webshop.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Calculater
{
    public class Calculate
    {
        public static double TotaalPrijs(Basket bas)
        {
            return bas.prod.Aankoopprijs * bas.Aantal;
        }

        public static double TotaalPrijsBasket(List<OrderLine> list)
        {
            double prijs = 0.0;

            foreach (OrderLine orderline in list)
            {
                prijs += orderline.TotaalPrijs;
            }

            return prijs;
        }
    }
}