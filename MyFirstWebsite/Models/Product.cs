﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyFirstWebsite.Models
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
    }
}