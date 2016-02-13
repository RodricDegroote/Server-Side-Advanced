using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ba.webshop.models
{
    [Serializable]
    public class Basket
    {
        public int ID { get; set; }

        public int Aantal { get; set; }
        public string UserID { get; set; }
        public int ProductID { get; set; }
        public DateTime TimeStamp { get; set; }
        public Product prod { get; set; }
        public int Deleted { get; set; }
    }
}