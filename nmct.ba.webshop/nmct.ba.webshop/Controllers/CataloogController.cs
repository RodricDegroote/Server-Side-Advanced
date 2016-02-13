using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using nmct.ba.webshop.Cache;
using nmct.ba.webshop.context;
using nmct.ba.webshop.interfaces;
using nmct.ba.webshop.models;
using nmct.ba.webshop.models.PresentationModel;
using nmct.ba.webshop.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.UI.WebControls;

namespace nmct.ba.webshop.Controllers
{
    public class CataloogController : Controller
    {
        private IProductService ps;
        private IBasketService bs;
        private IUserService us;
        private ILanguageService ls;
        private List<Product> Producten = new List<Product>();
        private List<Cultuur> Talen = new List<Cultuur>();

        public CataloogController(IProductService ps, IBasketService bs, IUserService us, ILanguageService ls)
        {
            this.ps = ps;
            this.bs = bs;
            this.us = us;
            this.ls = ls;
            Cultuur cult1 = new Cultuur()
            {
                ID = "en-US"
            };
            Cultuur cult2 = new Cultuur()
            {
                ID = "nl-BE"
            };

            Talen.Add(cult1);
            Talen.Add(cult2);
        }


        // GET: Cataloog
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Culturen = Talen;
            Producten = ps.getProducten();
            return View(Producten);
        }

        [HttpPost]
        public ViewResult Change_Language(string ID)
        {
            //hier redirect doen, maar eerst maken dat uw culture is verandert, eenmaal 
            // hij terug gaat naar ActionResult Index() en zal automatisch weer gaan kijken welke taal is ingesteld
            // en zal dan de juiste taal tonen
            ls.ChangeCulture(new CultureInfo(ID));
            ViewBag.Culturen = Talen;
            Producten = ps.getProducten();
            return View("Index",Producten);
        }
        

        public int GetAantalBasketItems()
        {
            string id = us.getAppUser(User).Id;
            if(id != "") return WebshopCache.getCountBasketCache("Aantal", id); else return 0;
        }

        [HttpGet]
        [Authorize]
        public ActionResult InfoPlus(int? id)
        {
            if (!id.HasValue) return RedirectToAction("Index");
            return View(ps.getProductById(id.Value));
        }

        [HttpPost]
        [Authorize]
        public ActionResult InfoPlus(int? VerhurenAantal, int? ProductId)
        {
            if (!VerhurenAantal.HasValue && ProductId.HasValue) return RedirectToAction("Index");
                
            bs.saveBasket(VerhurenAantal.Value, ProductId.Value,us.getAppUser(User).Id);
            return RedirectToAction("Index");
        }
    }

}