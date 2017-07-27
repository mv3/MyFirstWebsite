using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TheSnackHole.Models
{
    public class Brand
    {
        public Brand()
        {
            //Products = new List<ProductBrand>();
        }

        public int BrandId { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }

        //public ICollection<ProductBrand> Products { get; set; }
    }
}