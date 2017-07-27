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
            var product1 = new Product()
            {
                Brand = "Pringles",
                Name = "Original",
                Description = "Original flavored potato chips.",
                Price = 0.50m
            };
            context.Products.Add(product1);

            var product2 = new Product()
            {
                Brand = "Pringles",
                Name = "Bacon",
                Description = "Bacon flavored potato chips.",
                Price = 0.50m
            };
            context.Products.Add(product2);

            var product3 = new Product()
            {
                Brand = "Little Debbie",
                Name = "Apple Fruit Pies",
                Description = "Apple pies.",
                Price = 0.50m
            };
            context.Products.Add(product3);

            context.SaveChanges();

        }

    }
}