
using Shop.Data;
using Shop.Data.Models;
using Shop.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Shop.Services
{
    public class ProductService : IProductService
    {
        private readonly ShopDbContext shopDbContext;

        public ProductService(ShopDbContext shopDbContext)
        {
            this.shopDbContext = shopDbContext;
        }

        public bool Create(ProductServiceModel productServiceModel)
        {
            Product product = new Product
            {
                Name = productServiceModel.Name,
                Price = productServiceModel.Price,
                Category = shopDbContext.Categories.Where(c => c.Name == productServiceModel.Category.Name).FirstOrDefault(),
                Picture = productServiceModel.Picture
            };

            shopDbContext.Products.Add(product);
            var result = shopDbContext.SaveChanges();

            return result > 0;

        }

        public bool CreateCategory(CategoryServiceModel categoryServiceModel)
        {
            Category category = new Category()
            {
                Name = categoryServiceModel.Name
            };

            shopDbContext.Categories.Add(category);
            var result = shopDbContext.SaveChanges();

            return result > 0;
        }

        public IEnumerable<CategoryServiceModel> GetAllCategories()
        {
            return this.shopDbContext.Categories
                .Select(c => new CategoryServiceModel
                {
                    Id = c.Id,
                    Name = c.Name
                }).ToList();
        }
    }
}
