using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheSnackHole.Data;
using TheSnackHole.Models;
using System.Web.Mvc;

namespace TheSnackHole.ViewModels
{
    public class ProductsBaseViewModel
    {
        public Product Product { get; set; } = new Product();

        //public SelectList BrandsSelectListItems { get; set; }

        //public virtual void Init(Context context)
        //{
        //    BrandsSelectListItems = new SelectList(
        //        );
        //}
    }
}