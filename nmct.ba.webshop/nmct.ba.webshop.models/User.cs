using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace nmct.ba.webshop.models
{
    [Serializable]
    public class User
    {
        [Key]
        public string ID { get; set; }
        public string Naam { get; set; }
        public string Voornaam { get; set; }
        public string Adres { get; set; }
        public string Postcode { get; set; }
        public string ZipCode { get; set; }
    }
}
