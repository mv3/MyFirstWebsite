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
                .WithSorting(sorting: true, defaultSortColumn: "Name", defaultSortDirection: SortDirection.Dsc)
                //.WithPaging(paging: true, itemsPerPage: 10, allowChangePageSize: true, maxItemsPerPage: 100)
                .WithAdditionalQueryOptionNames("Search")
                .AddColumns(cols =>
            {
                // Add your columns here
                cols.Add("BrandId").WithHeaderText("Brand")
                    .WithValueExpression((p, c) => c.UrlHelper.Action("Detail", "Brands", new { id = p.BrandId }))
                    .WithValueTemplate("<a href='{Value}'>{Model.Brand.Name}</a>", false)
                    .WithPlainTextValueExpression(p => p.DisplayText)
                    .WithVisibility(visible: true, allowChangeVisibility: false)
                    .WithSorting(true); 
                cols.Add("DisplayText").WithHeaderText("Proudct")
                    .WithValueExpression((p, c) => c.UrlHelper.Action("Detail", "Products", new { id = p.ProductId }))
                    .WithValueTemplate("<a href='{Value}'>{Model.DisplayText}</a>", false)
                    .WithPlainTextValueExpression(p => p.DisplayText)
                    .WithVisibility(visible: true, allowChangeVisibility: false)
                    .WithSorting(true);
                cols.Add("Name").WithHeaderText("Name")
                    .WithValueExpression(p => p.Name)
                    .WithVisibility(visible: false, allowChangeVisibility: true)
                    .WithSorting(true);
                cols.Add("Style").WithHeaderText("Flavor/Style")
                    .WithValueExpression(p => p.Style)
                    .WithVisibility(visible: false, allowChangeVisibility: true)
                    .WithSorting(true);
                cols.Add("Description")
                    .WithValueExpression(p => p.Description)
                    .WithVisibility(visible: true, allowChangeVisibility: true)
                    .WithSorting(false); 
                cols.Add("Price").WithHeaderText("Price ($)")
                    .WithValueExpression(p => p.Price.ToString())
                    .WithVisibility(visible: true, allowChangeVisibility: true)
                    .WithSorting(false);       
                cols.Add("InStock").WithHeaderText("Status")
                    .WithValueExpression(p => p.InStock ? "In Stock" : "Out of Stock")
                    .WithCellCssClassExpression(p => p.InStock ? "success" : "danger")
                    .WithVisibility(visible: true, allowChangeVisibility: true)
                    .WithSorting(false);
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
        var options = context.QueryOptions;
        var result = new QueryResult<Product>();

        using (var qContext = new Context())
        {                     
            var query = qContext.Products.Include(p => p.Brand);
            if (!String.IsNullOrWhiteSpace(options.SortColumnName))
            {
                switch (options.SortColumnName)
                {
                    case "BrandId":
                        //query = query.OrderBy(p => p.Brand.Name);
                        query = options.SortDirection == SortDirection.Asc ? query.OrderBy(p => p.Brand.Name) : query.OrderByDescending(p => p.Brand.Name);
                        break;
                    case "DisplayText":                        
                        query = options.SortDirection == SortDirection.Asc ? query.OrderBy(p => p.Name).ThenBy(p => p.Style) : query.OrderByDescending(p => p.Name).ThenBy(p => p.Style);
                        break;
                    case "Name":
                        query = options.SortDirection == SortDirection.Asc ? query.OrderBy(p => p.Name) : query.OrderByDescending(p => p.Name);
                        break;
                    case "Style":
                        query = options.SortDirection == SortDirection.Asc ? query.OrderBy(p => p.Style) : query.OrderByDescending(p => p.Style);
                        break;
                    default:
                        query = query.OrderBy(p => p.ProductId);
                        break;
                }
            }
            result.Items = query.ToList();
        }

        return result;
        //return new QueryResult<Product>()
        //{
        //    Items = products
        //}; 
    })
);
        }
    }
}