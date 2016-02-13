using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ba.webshop.models
{
    [Serializable]
    public class OS
    {
        public int OSId { get; set; }
        public string Naam { get; set; }
        public List<Product> Producten { get; set; }
    }
}