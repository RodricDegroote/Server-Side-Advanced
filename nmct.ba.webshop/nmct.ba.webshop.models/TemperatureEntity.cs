using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.webshop.models
{
    public class TemperatureEntity : TableEntity
    {
        public string Location { get; set; }
        public string Temp { get; set; }
        public string Time { get; set; }
    }
}
