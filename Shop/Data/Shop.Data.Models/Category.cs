﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Data.Models
{
    public  class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}
