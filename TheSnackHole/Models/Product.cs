using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TheSnackHole.Models
{
    public class Product
    {
        public Product()
        {

        }

        public int ProductId { get; set; }
        [Display(Name = "Brand")]
        public int BrandId { get; set; }
        public string Name { get; set; }
        [Display(Name = "Style/Flavor")]
        public string Style { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public Brand Brand { get; set; }


        public string DisplayText
        {
            get
            {
                return $"{Brand.Name} {Style} {Name}";
            }
        }
    }

    
}