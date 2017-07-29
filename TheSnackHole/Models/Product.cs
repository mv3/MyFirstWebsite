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
        [Display(Name = "Brand"), Required]
        public int BrandId { get; set; }
        [Required, MaxLength(100, ErrorMessage = "The Name field cannot be longer than 100 characters.")]
        public string Name { get; set; }
        [Display(Name = "Style/Flavor"), Required, MaxLength(100, ErrorMessage = "The Stlye/Flavor field cannot be longer than 100 characters.")]
        public string Style { get; set; }
        [MaxLength(200, ErrorMessage = "The Description field cannot be longer than 200 characters.")]
        public string Description { get; set; }
        [Required]
        [RegularExpression(@"\d+(\.\d{1,2})?", ErrorMessage = "Invalid price")]
        [Range(0.01, 99999, ErrorMessage = "Price must be greater than 0.00")]
        [Display(Name = "Price ($)")]
        public decimal Price { get; set; }
        [Required, Display(Name = "Availability")]
        public bool InStock { get; set; }

        public Brand Brand { get; set; }


        public string DisplayText
        {
            get
            {
                return $"{Style} {Name}";
            }
        }
    }

    
}