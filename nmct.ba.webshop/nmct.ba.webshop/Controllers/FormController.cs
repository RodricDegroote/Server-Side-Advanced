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
    public class FormController : Controller
    {
        private IProductService ps;
        private IServiceBusService sbs;

        public FormController(IProductService ps, IServiceBusService sbs)
        {
            this.ps = ps;
            this.sbs = sbs;
        }

        // GET: Form
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(FormPost post)
        {
            sbs.MakeTopic(post);
            sbs.MakeSubscription();
            ///sbs.MakeTopic(post);
            sbs.SaveSubscription(post);
            return View();
        }
    }
}