using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace nmct.ba.webshop.models.PresentationModel
{
    public class OrderLinePM
    {
        public List<OrderLine> OrderLines { get; set; }
        public double TotaalPrijsProducten { get; set; }
        public string btnEnabled { get; set; }
    }
}