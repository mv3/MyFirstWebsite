using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using TheSnackHole.Models;

namespace TheSnackHole.Data
{
    internal class DatabaseInitializer : DropCreateDatabaseIfModelChanges<Context>
    {
        protected override void Seed(Context context)
        {
            var brandPringles = new Brand()
            {
                Name = "Pringles"
            };
            var brandLays = new Brand()
            {
                Name = "Lays"
            };
            var brandLittleDebbie = new Brand()
            {
                Name = "Little Debbie"
            };
            var brandKellogs = new Brand()
            {
                Name = "Kellogg's"
            };

            var product1 = new Product()
            {
                Brand = brandPringles,
                Name = "Potato Chips",
                Style = "Original",
                Description = "Original flavored potato chips.",
                Price = 0.50m
            };
            context.Products.Add(product1);

            var product2 = new Product()
            {
                Brand = brandPringles,
                Name = "Potato Chips",
                Style = "Bacon",
                Description = "Bacon flavored potato chips.",
                Price = 0.50m
            };
            context.Products.Add(product2);

            var product3 = new Product()
            {
                Brand = brandLittleDebbie,
                Name = "Fruit Pies",
                Style = "Apple",
                Description = "Apple pies.",
                Price = 0.50m
            };
            context.Products.Add(product3);

            var product4 = new Product()
            {
                Brand = brandKellogs,
                Name = "Pop-Tarts",
                Style = "Cinnamon",
                Description = "Cinnamon Pop-Tarts.",
                Price = 0.50m
            };
            context.Products.Add(product4);

            var product5 = new Product()
            {
                Brand = brandLays,
                Name = "Potato Chips",
                Style = "Barbecue",
                Description = "Barbecue flavored potato chips.",
                Price = 0.50m
            };
            context.Products.Add(product5);

            context.SaveChanges();

        }

    }
}