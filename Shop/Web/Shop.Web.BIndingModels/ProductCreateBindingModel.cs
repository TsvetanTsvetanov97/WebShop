using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Web.BindingModels
{
    public class ProductCreateBindingModel
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public IFormFile Picture { get; set; }

        public string Category { get; set; }
    }
}
