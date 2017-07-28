using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheSnackHole.Data;
using TheSnackHole.Models;
using System.Data.Entity;

namespace TheSnackHole.Controllers
{
    public class LinksController : Controller
    {
        private Context _context = null;

        public LinksController()
        {
            _context = new Context();
        }

        // GET: Links
        public ActionResult Index()
        {
            

            return View();
        }
    }
}