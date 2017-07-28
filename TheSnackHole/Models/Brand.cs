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
            
        }

        public int BrandId { get; set; }
        [Required, StringLength(100)]
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}