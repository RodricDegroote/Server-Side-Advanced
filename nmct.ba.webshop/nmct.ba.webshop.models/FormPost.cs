using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace nmct.ba.webshop.models
{
    public class FormPost
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }

        public int OrderNr { get; set; }
        public string FormType { get; set; }

        [Required]
        public string Description { get; set; }
    }
}
