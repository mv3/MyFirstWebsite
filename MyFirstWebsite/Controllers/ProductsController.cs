using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
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
                    .Include(p => p.Brand)
                    .OrderBy(p=> p.Brand.Name)
                    .ThenBy(p => p.Name)
                    .ToList();

                return View(products);
            }                      
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _context.Products
                    .Include(p => p. Brand)
                    .Where(cb => cb.ProductId == id)
                    .SingleOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }

           

            return View(product);
        }

        public ActionResult Add()
        {
            var viewModel = new ProductsAddViewModel();

            viewModel.Init(_context);
                        
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(ProductsAddViewModel viewModel)
        {
           

            if (ModelState.IsValid)
            {
                var product = viewModel.Product;

                _context.Products.Add(product);
                _context.SaveChanges();

                TempData["Message"] = "Product was successfully added!";

                return RedirectToAction("Index");
            }

            viewModel.Init(_context);

            return View(viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _context.Products
                .Where(cb => cb.ProductId == id)
                .SingleOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }

            var viewModel = new ProductsEditViewModel()
            {
                Product = product
            };
            viewModel.Init(_context);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductsEditViewModel viewModel)
        {
            //ValidateComicBook(model.ComicBook);

            if (ModelState.IsValid)
            {
                var product = viewModel.Product;

                _context.Entry(product).State = EntityState.Modified;
                _context.SaveChanges();

                TempData["Message"] = "Product was successfully updated!";

                return RedirectToAction("Detail", new { id = product.ProductId });
            }

            viewModel.Init(_context);

            return View(viewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = _context.Products
                .Include(p => p.Brand)
                .Where(p => p.ProductId == id)
                .SingleOrDefault();

            if (product == null)
            {
                return HttpNotFound();
            }

            return View(product);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var product = new Product() { ProductId = id };
            _context.Entry(product).State = EntityState.Deleted;
            _context.SaveChanges();

            TempData["Message"] = "Your product was successfully deleted!";

            return RedirectToAction("Index");
        }
    }
}