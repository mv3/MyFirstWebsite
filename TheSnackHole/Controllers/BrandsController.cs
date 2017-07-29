using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using TheSnackHole.Models;
using System.Data.Entity;
using TheSnackHole.Data;
using TheSnackHole.ViewModels;

namespace TheSnackHole.Controllers
{
    public class BrandsController : BaseController
    {
        //private Context Context = null;

        //public BrandsController()
        //{
        //    Context = new Context();
        //}

        public ActionResult Index()
        {
            using (var context = new Context())
            {
                var brands = context.Brands
                    //.Include(b => b.Name)
                    .OrderBy(b => b.Name)
                    .ToList();

                return View(brands);
            }
        }

        public ActionResult Detail(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

           
            var brand = Context.Brands
                    .Include(b => b.Products)
                    .Where(b => b.BrandId == id)
                    .SingleOrDefault();

            if (brand == null)
            {
                return HttpNotFound();
            }

            // Sort the products
            brand.Products = brand.Products
                .OrderBy(p => p.Name)
                .ToList();

            return View(brand);
        }

        public ActionResult Add()
        {
            var brand = new Brand();

            return View(brand);
        }

        [HttpPost]
        public ActionResult Add(Brand brand)
        {
            ValidateBrand(brand);

            if (ModelState.IsValid)
            {
                // Add the brand. 
                Context.Brands.Add(brand);
                Context.SaveChanges();

                TempData["Message"] = "Your brand was successfully added!";

                return RedirectToAction("Detail", new { id = brand.BrandId });
            }

            return View(brand);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Get the brand.
            var brand = Context.Brands
                 .Where(b => b.BrandId == id)
                 .SingleOrDefault();

            if (brand == null)
            {
                return HttpNotFound();
            }

            return View(brand);
        }

        [HttpPost]
        public ActionResult Edit(Brand brand)
        {
            ValidateBrand(brand);

            if (ModelState.IsValid)
            {
                // Update the brand.

                Context.Entry(brand).State = EntityState.Modified;
                Context.SaveChanges();

                TempData["Message"] = "Your brand was successfully updated!";

                return RedirectToAction("Detail", new { id = brand.BrandId });
            }

            return View(brand);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            // Get the brand.
            var brand = Context.Brands
                .Include(b => b.Products)
                .Where(b => b.BrandId == id)
                .SingleOrDefault();

            if (brand == null)
            {
                return HttpNotFound();
            }

            return View(brand);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            // Delete the brand.
            var brand = new Brand() { BrandId = id };
            Context.Entry(brand).State = EntityState.Deleted;
            Context.SaveChanges();

            TempData["Message"] = "Your brand was successfully deleted!";

            return RedirectToAction("Index");
        }

        /// <summary>
        /// Validates a brand on the server
        /// before adding a new record or updating an existing record.
        /// </summary>
        /// <param name="brand">The brand to validate.</param>
        private void ValidateBrand(Brand brand)
        {
            // If there aren't any "Name" field validation errors...
            if (ModelState.IsValidField("Name"))
            {
                // Then make sure that the provided Name is unique.
                // TODO Call method to check if the name is available.
                if (Context.Brands
                        .Any(b => b.BrandId != brand.BrandId &&
                                   b.Name == brand.Name))
                {
                    ModelState.AddModelError("Name",
                        "The provided Name is in use by another brand.");
                }
            }
        }
    }
}