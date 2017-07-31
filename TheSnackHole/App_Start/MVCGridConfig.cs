using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCGrid.Web;
using MVCGrid.Models;
using TheSnackHole.Models;
using TheSnackHole.Data;
using System.Data.Entity;
using System.Web.Mvc;

namespace TheSnackHole.App_Start
{
    public class MVCGridConfig
    {
        public static void RegisterGrids()
        {
            // add your Grid definitions here, using the MVCGridDefinitionTable.Add method
            MVCGridDefinitionTable.Add("ProductsGrid", new MVCGridBuilder<Product>()
                .WithAuthorizationType(AuthorizationType.AllowAnonymous)
                .WithSorting(sorting: true, defaultSortColumn: "BrandId", defaultSortDirection: SortDirection.Dsc)
                .WithPaging(true, 10, true, 100)
                .WithAdditionalQueryOptionNames("Search")
                .WithFiltering(true)
                .AddColumns(cols =>
            {
                // Add your columns here
                cols.Add("BrandId").WithHeaderText("Brand")
                    .WithValueExpression((p, c) => c.UrlHelper.Action("Detail", "Brands", new { id = p.BrandId }))
                    .WithValueTemplate("<a href='{Value}'>{Model.Brand.Name}</a>", false)
                    .WithPlainTextValueExpression(p => p.DisplayText)
                    .WithVisibility(visible: true, allowChangeVisibility: false)
                    .WithSorting(true)
                    .WithFiltering(false);
                cols.Add("DisplayText").WithHeaderText("Proudct")
                    .WithValueExpression((p, c) => c.UrlHelper.Action("Detail", "Products", new { id = p.ProductId }))
                    .WithValueTemplate("<a href='{Value}'>{Model.DisplayText}</a>", false)
                    .WithPlainTextValueExpression(p => p.DisplayText)
                    .WithVisibility(visible: true, allowChangeVisibility: false)
                    .WithSorting(true)
                    .WithFiltering(false);
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
                    .WithSorting(false)
                    .WithFiltering(true);
            })
    .WithRetrieveDataMethod((context) =>
    {
    //var products = new List<Product>();
    var options = context.QueryOptions;
    int totalRecords;
    //var repo = DependencyResolver.Current.GetService<IProductRepository>();
    ProductRepository _repo = null;
    Context _context = new Context();
    _repo = new ProductRepository(_context);
    string globalSearch = options.GetAdditionalQueryOptionString("search");
    bool? inStock = null;
    string fa = options.GetFilterString("InStock");
    if (!String.IsNullOrWhiteSpace(fa))
    {
        inStock = (String.Compare(fa, "in stock", true) == 0);
    }
    string sortColumn = options.GetSortColumnData<string>();
    var items = _repo.GetData(out totalRecords, globalSearch, inStock, options.GetLimitOffset(), options.GetLimitRowcount(),
        sortColumn, options.SortDirection == SortDirection.Dsc);
    return new QueryResult<Product>()
    {
        Items = items,
        TotalRecords = totalRecords
    };
    })
);
        }
    }
}