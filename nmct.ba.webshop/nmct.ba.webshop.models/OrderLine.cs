using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ba.webshop.models
{
    [Serializable]
    public class OrderLine
    {
        public int Id { get; set; }

        public Basket BasketItem { get; set; }
        public int BasketID { get; set; }
        public double TotaalPrijs { get; set; }
        public string UserID { get; set; }
    }
}
