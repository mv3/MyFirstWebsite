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
            var brandPlanters = new Brand()
            {
                Name = "Planters"
            };
            var brandDavid = new Brand()
            {
                Name = "David"
            };

            var product1 = new Product()
            {
                Brand = brandPringles,
                Name = "Potato Chips",
                Style = "Original",
                Description = "Original flavored potato chips.",
                Price = 0.50m,
                InStock = true
            };
            context.Products.Add(product1);

            var product2 = new Product()
            {
                Brand = brandPringles,
                Name = "Potato Chips",
                Style = "Bacon",
                Description = "Bacon flavored potato chips.",
                Price = 0.50m,
                InStock = false
            };
            context.Products.Add(product2);

            var product3 = new Product()
            {
                Brand = brandLittleDebbie,
                Name = "Fruit Pies",
                Style = "Apple",
                Description = "Apple pies.",
                Price = 0.50m,
                InStock = true
            };
            context.Products.Add(product3);

            var product4 = new Product()
            {
                Brand = brandKellogs,
                Name = "Pop-Tarts",
                Style = "Cinnamon",
                Description = "Cinnamon Pop-Tarts.",
                Price = 0.50m,
                InStock = true
            };
            context.Products.Add(product4);

            var product5 = new Product()
            {
                Brand = brandLays,
                Name = "Potato Chips",
                Style = "Barbecue",
                Description = "Barbecue flavored potato chips.",
                Price = 0.50m,
                InStock = true
            };
            context.Products.Add(product5);

            var product6 = new Product()
            {
                Brand = brandLays,
                Name = "Potato Chips",
                Style = "Sour Cream & Onion",
                Description = "Sour Cream & Onion flavored potato chips.",
                Price = 0.50m,
                InStock = true
            };
            context.Products.Add(product6);

            var product7 = new Product()
            {
                Brand = brandKellogs,
                Name = "Pop-Tarts",
                Style = "Blueberry",
                Description = "Blueberry filled Pop-Tarts.",
                Price = 0.50m,
                InStock = true
            };
            context.Products.Add(product7);

            var product8 = new Product()
            {
                Brand = brandKellogs,
                Name = "Pop-Tarts",
                Style = "Raspberry",
                Description = "Raspberry filled Pop-Tarts.",
                Price = 0.50m,
                InStock = true
            };
            context.Products.Add(product8);

            var product9 = new Product()
            {
                Brand = brandDavid,
                Name = "Sunflower seeds",
                Style = "Barbecue",
                Description = "Barbecue flavored Sunflower seeds.",
                Price = 0.50m,
                InStock = true
            };
            context.Products.Add(product9);

            var product10 = new Product()
            {
                Brand = brandLittleDebbie,
                Name = "Fruit Pies",
                Style = "Cherry",
                Description = "Cherry filled pies.",
                Price = 0.50m,
                InStock = false
            };
            context.Products.Add(product10);

            var product11 = new Product()
            {
                Brand = brandPringles,
                Name = "Potato Chips",
                Style = "Pizza",
                Description = "Pizza flavored potato chips.",
                Price = 0.50m,
                InStock = true
            };
            context.Products.Add(product11);

            var product12 = new Product()
            {
                Brand = brandDavid,
                Name = "Sunflower seeds",
                Style = "Ranch",
                Description = "Ranch flavored sunflower seeds",
                Price = 0.50m,
                InStock = true
            };
            context.Products.Add(product12);

            var product13 = new Product()
            {
                Brand = brandDavid,
                Name = "Pumpkin seeds",
                Style = "Salted",
                Description = "Roasted and salted pumpkin seeds",
                Price = 0.50m,
                InStock = true
            };
            context.Products.Add(product13);

            context.SaveChanges();

        }

    }
}