using TheSnackHole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace TheSnackHole.Data
{
    public interface IProductRepository
    {
        IEnumerable<Product> GetData(out int totalRecords, string globalSearch, int? limitOffset, int? limitRowCount, string orderBy, bool desc);
        IEnumerable<Product> GetData(out int totalRecords, int? limitOffset, int? limitRowCount, string orderBy, bool desc);
        IEnumerable<Product> GetData(out int totalRecords, string filterBrand, string filterProduct, bool? filterInStock, int? limitOffset, int? limitRowCount, string orderBy, bool desc);
        IEnumerable<Product> GetData(out int totalRecords, string globalSearch, bool? filterInStock, int? limitOffset, int? limitRowCount, string orderBy, bool desc);
    }

    public class ProductRepository : IProductRepository
    {
        protected Context db { get; private set; }

        public ProductRepository(Context context)
        {
            db = context;
        }

        public IEnumerable<Product> GetData(out int totalRecords, string filterBrand, string filterProduct, bool? filterInStock, int? limitOffset, int? limitRowCount, string orderBy, bool desc)
        {
            return GetData(out totalRecords, null, filterBrand, filterProduct, filterInStock, limitOffset, limitRowCount, orderBy, desc);
        }

        public IEnumerable<Product> GetData(out int totalRecords, string globalSearch, int? limitOffset, int? limitRowCount, string orderBy, bool desc)
        {
            return GetData(out totalRecords, globalSearch, null, null, null, limitOffset, limitRowCount, orderBy, desc);
        }

        public IEnumerable<Product> GetData(out int totalRecords, string globalSearch, bool? filterInStock, int? limitOffset, int? limitRowCount, string orderBy, bool desc)
        {
            return GetData(out totalRecords, globalSearch, null, null, filterInStock, limitOffset, limitRowCount, orderBy, desc);
        }

        public IEnumerable<Product> GetData(out int totalRecords, string globalSearch, string filterBrand, string filterProduct, bool? filterInStock, int? limitOffset, int? limitRowCount, string orderBy, bool desc)
        {
            //using (var db = new Context())
            using (db)
            {
                var query = db.Products.AsQueryable();
                query = query.Include(p => p.Brand);

                if (!String.IsNullOrWhiteSpace(filterBrand))
                {
                    query = query.Where(p => p.Brand.Name.Contains(filterBrand));
                }
                if (!String.IsNullOrWhiteSpace(filterProduct))
                {
                    query = query.Where(p => p.Name.Contains(filterProduct) || p.Style.Contains(filterProduct));
                }
                if (filterInStock.HasValue)
                {
                    query = query.Where(p => p.InStock == filterInStock.Value);
                }

                if (!String.IsNullOrWhiteSpace(globalSearch))
                {
                    query = query.Where(p => (p.Brand.Name + " " + p.Name + " " + p.Style).Contains(globalSearch));
                }

                totalRecords = query.Count();

                if (!String.IsNullOrWhiteSpace(orderBy))
                {
                    switch (orderBy.ToLower())
                    {
                        case "brandid":
                            if (!desc)
                                query = query.OrderBy(p => p.Brand.Name).ThenBy(p => p.Style);
                            else
                                query = query.OrderByDescending(p => p.Brand.Name).ThenBy(p => p.Style);
                            break;
                        case "displaytext":
                            if (!desc)
                                query = query.OrderBy(p => p.Style).ThenBy(p => p.Name);
                            else
                                query = query.OrderByDescending(p => p.Style).ThenBy(p => p.Name);
                            break;
                        case "instock":
                            if (!desc)
                                query = query.OrderBy(p => p.InStock);
                            else
                                query = query.OrderByDescending(p => p.InStock);
                            break;
                        case "name":
                            if (!desc)
                                query = query.OrderBy(p => p.Name).ThenBy(p => p.Style);
                            else
                                query = query.OrderByDescending(p => p.Name).ThenBy(p => p.Style);
                            break;
                        case "style":
                            if (!desc)
                                query = query.OrderBy(p => p.Style).ThenBy(p => p.Name);
                            else
                                query = query.OrderByDescending(p => p.Style).ThenBy(p => p.Name);
                            break;
                    }
                }
                //else
                //{
                //    query = query.OrderBy(p => p.Brand.Name);
                //}


                if (limitOffset.HasValue)
                {
                    query = query.Skip(limitOffset.Value).Take(limitRowCount.Value);
                }

                return query.ToList();
            }
        }

        public IEnumerable<Product> GetData(out int totalRecords, int? limitOffset, int? limitRowCount, string orderBy, bool desc)
        {
            return GetData(out totalRecords, null, null, null, limitOffset, limitRowCount, orderBy, desc);
        }
    }
}
