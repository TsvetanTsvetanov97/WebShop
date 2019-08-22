using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.Services;
using Shop.Services.Models;
using Shop.Web.BindingModels;

namespace Shop.Web.Areas.Administration.Controllers
{

    public class ProductController : Controller
    {
        private IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpGet]
        [Route("Administration/Product/Create")]
        public IActionResult Create()
        {
            this.ViewData["categories"] = this.productService.GetAllCategories().Select(c => new ProductCreateCategoryBindingModel
            {
                Name = c.Name
            });
          return  View("Create");
        }

        [HttpPost]
        [Route("Administration/Product/Create")]
        public IActionResult Create(ProductCreateBindingModel productCreateBindingModel)
        {
            ProductServiceModel productServiceModel = new ProductServiceModel
            {
                Name = productCreateBindingModel.Name,
                Price = productCreateBindingModel.Price,
                Category = new CategoryServiceModel() { Name = productCreateBindingModel.Name }
            };
            productService.Create(productServiceModel);
            return Redirect("/");
        }

        [HttpGet]
        [Route("Administration/Category/Create")]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        [Route("Administration/Category/Create")]
    
        public  IActionResult CreateCategory(CategoryCreateBindingModel categoryCreateBindingModel)
        {
            CategoryServiceModel categoryServiceModel = new CategoryServiceModel
            {
                Name = categoryCreateBindingModel.Name
            };
            productService.CreateCategory(categoryServiceModel);
            return Redirect("/");
        }
    }
}