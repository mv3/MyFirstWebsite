using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCGrid.Web;
using MVCGrid.Models;
using TheSnackHole.Models;
using TheSnackHole.Data;
using System.Data.Entity;

namespace TheSnackHole.App_Start
{
    public class MVCGridConfig
    {
        public static void RegisterGrids()
        {
            // add your Grid definitions here, using the MVCGridDefinitionTable.Add method
            MVCGridDefinitionTable.Add("ProductsGrid", new MVCGridBuilder<Product>()
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .AddColumns(cols =>
            {
                // Add your columns here
                cols.Add("BrandId").WithHeaderText("Brand")
                    .WithValueExpression((p, c) => c.UrlHelper.Action("Detail", "Brands", new { id = p.BrandId }))
                    .WithValueTemplate("<a href='{Value}'>{Model.Brand.Name}</a>", false)
                    .WithPlainTextValueExpression(p => p.DisplayText);
                cols.Add("DisplayText").WithHeaderText("Proudct")
                    .WithValueExpression((p, c) => c.UrlHelper.Action("Detail", "Products", new { id = p.ProductId }))
                    .WithValueTemplate("<a href='{Value}'>{Model.DisplayText}</a>", false)
                    .WithPlainTextValueExpression(p => p.DisplayText);
                cols.Add("Description")
                    .WithValueExpression(p => p.Description);
                cols.Add("Price").WithHeaderText("Price ($)")
                    .WithValueExpression(p => p.Price.ToString());       
                cols.Add("InStock").WithHeaderText("Status")
                    .WithValueExpression(p => p.InStock ? "In Stock" : "Out of Stock")
            .WithCellCssClassExpression(p => p.InStock ? "success" : "danger");
            })
    .WithRetrieveDataMethod((context) =>
    {
        // Query your data here. Obey Ordering, paging and filtering paramters given in the context.QueryOptions.
        // Use Entity Framwork, a module from your IoC Container, or any other method.
        // Return QueryResult object containing IEnumerable<YouModelItem>
        //return new QueryResult<Product>()
        //{
        //    Items = new List<Product>(),
        //    TotalRecords = 0 // if paging is enabled, return the total number of records of all pages
        //};
        
        var products = new List<Product>();
        //int totalRecords;
        using (var qContext = new Context())
        {
            products = qContext.Products
                .Include(p => p.Brand)
                .OrderBy(p => p.Brand.Name)
                .ThenBy(p => p.Name)
                .ToList();            
        }


        return new QueryResult<Product>()
        {
            Items = products
        }; 
    })
);
        }
    }
}