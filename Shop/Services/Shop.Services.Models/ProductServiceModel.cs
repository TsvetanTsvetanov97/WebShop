using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services.Models
{
    public class ProductServiceModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }


        public int CategoryServiceId { get; set; }
        public CategoryServiceModel Category { get; set; }
    }
}
 