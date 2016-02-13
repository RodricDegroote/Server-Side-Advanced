using Calculater;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using nmct.ba.webshop.context;
using nmct.ba.webshop.interfaces;
using nmct.ba.webshop.models;
using nmct.ba.webshop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace nmct.ba.webshop.Controllers
{
    public class OrderController : Controller
    {        
        private IBasketService bs;
        private IOrderService os;
        private IUserService us;

        public OrderController(IBasketService bs, IUserService us, IOrderService os)
        {
            this.bs = bs;
            this.os = os;
            this.us = us;
        }


        // GET: Order
        [HttpPost]
        public ActionResult Bestelling()
        {
            Order order = new Order();
            order.Gebruiker = us.GetUserInformation(us.getAppUser(User).Id);
            return View(order);
        }

        [HttpPost]
        public ActionResult Order(Order orderInfo)
        {
            string userId = us.getAppUser(User).Id;
            orderInfo.Gebruiker.ID = userId;

            List<Basket> bask = bs.getBasketItems(userId).ToList<Basket>();

            orderInfo.OrderLines = os.makeOrderLineList(bask, userId);
            orderInfo.TotaalPrijsOrder = Calculate.TotaalPrijsBasket(orderInfo.OrderLines);
            orderInfo.TimeStamp = DateTime.Now;

            //orderinfo gaan opslaan in de queue
            os.saveOrderInQueue(orderInfo);

            //basket gaan deleten (status van 0 to 1)
            bs.deleteBasket(bask, userId);

            return RedirectToAction("Index","Cataloog");
        }
    }
}