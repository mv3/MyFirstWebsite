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
    public class ProductsController : BaseController
    {
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

            var product = Context.Products
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

            viewModel.Init(Context);
                        
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Add(ProductsAddViewModel viewModel)
        {
            ValidateProduct(viewModel.Product);

            if (ModelState.IsValid)
            {
                var product = viewModel.Product;

                Context.Products.Add(product);
                Context.SaveChanges();

                TempData["Message"] = "Product was successfully added!";

                return RedirectToAction("Index");
            }

            viewModel.Init(Context);

            return View(viewModel);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = Context.Products
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
            viewModel.Init(Context);

            return View(viewModel);
        }

        [HttpPost]
        public ActionResult Edit(ProductsEditViewModel viewModel)
        {
            ValidateProduct(viewModel.Product);

            if (ModelState.IsValid)
            {
                var product = viewModel.Product;

                Context.Entry(product).State = EntityState.Modified;
                Context.SaveChanges();

                TempData["Message"] = "Product was successfully updated!";

                return RedirectToAction("Detail", new { id = product.ProductId });
            }

            viewModel.Init(Context);

            return View(viewModel);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var product = Context.Products
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
            Context.Entry(product).State = EntityState.Deleted;
            Context.SaveChanges();

            TempData["Message"] = "Your product was successfully deleted!";

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Validates a product on the server
        /// before adding a new record or updating an existing record.
        /// </summary>
        /// <param name="product">The product to validate.</param>
        private void ValidateProduct(Product product)
        {
            // If there aren't any "Brand", "Name", and "Style" field validation errors...
            if (ModelState.IsValidField("Product.BrandId") &&
                ModelState.IsValidField("Product.Name") &&
                ModelState.IsValidField("Product.Style"))
            {
                // Then make sure that the provided product is unique for the provided brand.

                if (Context.Products
                        .Any(p => p.ProductId != product.ProductId &&
                                   p.BrandId == product.BrandId &&
                                   p.Name == product.Name &&
                                   p.Style == product.Style))
                {
                    ModelState.AddModelError("Product.Style",
                        "The provided product has already been entered for the selected Brand.");
                }
            }
        }

    }
}

