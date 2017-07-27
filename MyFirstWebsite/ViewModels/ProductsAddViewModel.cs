using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TheSnackHole.Data;
using TheSnackHole.Models;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace TheSnackHole.ViewModels
{
    public class ProductsAddViewModel
    {
        public Product Product { get; set; } = new Product();

        


    }
}