using Shop.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Shop.Services
{
    public interface IProductService
    {
        bool Create(ProductServiceModel productServiceModel);
        bool CreateCategory(CategoryServiceModel categoryServiceModel);
        IEnumerable<CategoryServiceModel> GetAllCategories();
    }
}
