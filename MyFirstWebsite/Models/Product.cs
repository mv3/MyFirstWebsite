using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace TheSnackHole.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public string DisplayText
        {
            get
            {
                return $"{Brand} {Name}";
            }
        }
    }

    
}