using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TheSnackHole.Data;
using TheSnackHole.Models;
using TheSnackHole.ViewModels;

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

        public ActionResult Add()
        {
            var model = new Product();
                        
            return View(model);
        }

        [HttpPost]
        public ActionResult Add(Product model)
        {
           

            if (ModelState.IsValid)
            {
                var product = model;
                
                // TODO Add the comic book.

                TempData["Message"] = "Product was successfully added!";

                return RedirectToAction("Detail", new { id = product.ProductId });
            }

            

            return View(model);
        }
    }
}