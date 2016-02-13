using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.webshop.models.PresentationModel
{
    public class ProductPM
    {
        //product
        public Product product { get; set; }

        //Operating Systems
        public SelectList OSsn { get; set; }
        public SelectList GekozenOSn { get; set; }
        public int SelectedOS { get; set; }
        public string ids { get; set; }

        //Frameworks
        public SelectList FrameWorks { get; set; }
        public SelectList GekozenFMs { get; set; }
        public int SelectedFrameWork {get; set; }
        public string idsF { get; set; }

        //Image
        public HttpPostedFileBase file { get; set; }
    }
}