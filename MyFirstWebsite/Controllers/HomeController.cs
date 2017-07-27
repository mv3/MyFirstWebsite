using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace TheSnackHole.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Servicing your hunger since 2013.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Contact Us.";

            return View();
        }

        public ActionResult Products()
        {
            return View();
        }
    }
}