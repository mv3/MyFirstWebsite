using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using TheSnackHole.Models;

namespace TheSnackHole.Data
{
    /// <summary>
    /// Entity Framework context class.
    /// </summary>
    public class Context : DbContext
    {
        public DbSet<Product> Products { get; set; }

        //public Context()
        //{
        //    // This call to the SetInitializer method is used 
        //    // to configure EF to use our custom database initializer class
        //    // which contains our app's database seed data.
        //    Database.SetInitializer(new DatabaseInitializer());
        //}

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Removing the pluralizing table name convention 
            // so our table names will use our entity class singular names.
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Using the fluent API to configure the precision and scale
            // for the Product.Price property.
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(5, 2);
        }

    }
}