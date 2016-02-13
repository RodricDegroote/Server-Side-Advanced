using nmct.ba.webshop.interfaces;
using nmct.ba.webshop.models;
using nmct.ba.webshop.models.PresentationModel;
using nmct.ba.webshop.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.webshop.Controllers
{
    public class ProductController : Controller
    {
        private IProductService ps; 
        private List<OS> Osn = new List<OS>();
        private List<Framework> Frameworks = new List<Framework>();

        public ProductController(IProductService ps)
        {
            this.ps = ps;
            this.Osn = ps.getOperatingSystems();
            this.Frameworks = ps.getFrameworks();
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public ActionResult Toevoegen()
        {
            ProductPM PM = ps.GetPM(Osn,Frameworks);
            return View(PM);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [Route("Toevoegen")]
        public ActionResult Toevoegen(ProductPM ProductPM, string btnToevoegenOS, string btnToevoegenFW, string btnVolgende, HttpPostedFileBase file)
        {
            ps.ControleButton(ProductPM, btnToevoegenOS, btnToevoegenFW);

            ProductPM.OSsn = ps.getSelectListOS(Osn);
            ProductPM.FrameWorks = ps.getSelectListFW(Frameworks);

            if (btnVolgende == "Volgende")
            {
                if(ModelState.IsValid)
                {
                    ps.saveFile(file);
                    ps.SaveProduct(ProductPM, file);
                    return RedirectToAction("Index", "Cataloog");
                }
            }

            return View(ProductPM);

        }
    }
}