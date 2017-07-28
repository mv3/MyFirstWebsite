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
    public class BrandsController : Controller
    {
        private Context _context = null;

        public BrandsController()
        {
            _context = new Context();
        }

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

           
            var brand = _context.Brands
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
            //ValidateSeries(brand);

            if (ModelState.IsValid)
            {
                // Add the brand. 
                _context.Brands.Add(brand);
                _context.SaveChanges();

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
            var brand = _context.Brands
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
            //ValidateSeries(brand);

            if (ModelState.IsValid)
            {
                // Update the brand.
                _context.Entry(brand).State = EntityState.Modified;
                _context.SaveChanges();

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
            var brand = _context.Brands
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
            _context.Entry(brand).State = EntityState.Deleted;
            _context.SaveChanges();

            TempData["Message"] = "Your brand was successfully deleted!";

            return RedirectToAction("Index");
        }

        ///// <summary>
        ///// Validates a series on the server
        ///// before adding a new record or updating an existing record.
        ///// </summary>
        ///// <param name="series">The series to validate.</param>
        //private void ValidateSeries(Brand brand)
        //{
        //    //// If there aren't any "Title" field validation errors...
        //    //if (ModelState.IsValidField("Title"))
        //    //{
        //    //    // Then make sure that the provided title is unique.
        //    //    // TODO Call method to check if the title is available.
        //    //    if (false)
        //    //    {
        //    //        ModelState.AddModelError("Title",
        //    //            "The provided Title is in use by another series.");
        //    //    }
        //    //}
        //}
    }
}