using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheSnackHole.Data;
using TheSnackHole.Models;

namespace TheSnackHole.Controllers
{
    public class ProductsController : Controller
    {
        private Context _context = null;

        public ProductsController()
        {
            _context = new Context();
        }

        public ActionResult Index()
        {
            using (var context = new Context())
            {
                var products = context.Products
                    .ToList();

                return View(products);
            }                      
        }
    }
}