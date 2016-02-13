using nmct.ba.webshop.context;
using nmct.ba.webshop.models;
using nmct.ba.webshop.models.PresentationModel;
using nmct.ba.webshop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using Calculater;
using nmct.ba.webshop.interfaces;

namespace nmct.ba.webshop.Controllers
{
    public class BasketController : Controller
    {
        private IBasketService bs;
        private IOrderService os;
        private IUserService us;

        public BasketController(IBasketService bs, IOrderService os, IUserService us)
	    {
            this.bs = bs;
            this.os = os;
            this.us = us;
	    }

        [Authorize]
        public ActionResult Index()
        {
            OrderLinePM pm = new OrderLinePM();
            string id = us.getAppUser(User).Id;
          
            List<Basket> bask = bs.getBasketItems(id).ToList<Basket>();
            pm.OrderLines = os.makeOrderLineList(bask, id);
            pm.TotaalPrijsProducten = Calculate.TotaalPrijsBasket(pm.OrderLines);

            if (bask.Count <= 0) pm.btnEnabled = "Disabled"; else pm.btnEnabled = "Enabled";

            return View(pm);
        }

        [HttpGet]
        public ActionResult Wijzigen(int? id)
        {
            if (!id.HasValue)
                return View("Index");
        
            return View(bs.getBasket(id.Value));
        }

        public ActionResult Save(Basket bas)
        {
            bs.updateBasket(bas);
            return RedirectToAction("Index");
        }
    }
}