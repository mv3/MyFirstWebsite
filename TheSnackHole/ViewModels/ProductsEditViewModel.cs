using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TheSnackHole.ViewModels
{
    public class ProductsEditViewModel : ProductsBaseViewModel
    {
        public int Id
        {
            get { return Product.ProductId; }
            set { Product.ProductId = value; }

        }
    }
}