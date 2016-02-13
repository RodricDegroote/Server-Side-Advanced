using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace nmct.ba.webshop.models
{
    [Serializable]
    public class Order
    {
        public int ID { get; set; }
        public List<OrderLine> OrderLines { get; set; }
        public double TotaalPrijsOrder { get; set; }
        public User Gebruiker { get; set; }
        public DateTime TimeStamp { get; set; }
        public int Deleted { get; set; }
    }
}