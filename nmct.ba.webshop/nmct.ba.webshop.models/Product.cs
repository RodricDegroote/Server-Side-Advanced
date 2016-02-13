using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace nmct.ba.webshop.models
{
    [Serializable]
    public class Product
    {
        public int ProductId { get; set; }

        [Required]
        [Display(Name = "Naam", ResourceType = typeof(Properties.Product.Model))]
        public string Naam { get; set; }

        [Required]
        [Display(Name = "Aankoopprijs", ResourceType = typeof(Properties.Product.Model))]
        public double Aankoopprijs { get; set; }

        [Required]
        [Display(Name = "Huurprijs", ResourceType = typeof(Properties.Product.Model))]
        public double Huurprijs { get; set; }

        [Required]
        [Display(Name = "Aantal", ResourceType = typeof(Properties.Product.Model))]
        public int Aantal { get; set; }      
     
        public string Image { get; set; }

        [Required]
        [Display(Name = "Omschrijving", ResourceType = typeof(Properties.Product.Model))]
        public string Description { get; set; }

        public List<OS> OperatingSystems { get; set; }

        public List<Framework> Frameworks { get; set; }
    }
}