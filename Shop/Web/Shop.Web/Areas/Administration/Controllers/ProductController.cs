using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Shop.CloudinaryService;
using Shop.Services;
using Shop.Services.Models;
using Shop.Web.BindingModels;

namespace Shop.Web.Areas.Administration.Controllers
{
    [Area("Administration")]
    public class ProductController : AdminController
    {
        private IProductService productService;
        private ICloudinaryService cloudinaryService;
        public ProductController(IProductService productService,ICloudinaryService cloudinaryService)
        {
            this.productService = productService;
            this.cloudinaryService = cloudinaryService;
        }

        [HttpGet]
        [Route("Administration/Product/Create")]
        public IActionResult Create()
        {
            this.ViewData["categories"] = this.productService.GetAllCategories().Select(c => new ProductCreateCategoryBindingModel
            {
                Name = c.Name
            });
          return View("Create");
        }

        [HttpPost]
        [Route("Administration/Product/Create")]
        public async Task<IActionResult> Create(ProductCreateBindingModel productCreateBindingModel)
        {
            ProductServiceModel productServiceModel = new ProductServiceModel
            {
                Name = productCreateBindingModel.Name,
                Price = productCreateBindingModel.Price,
                Category = new CategoryServiceModel() { Name = productCreateBindingModel.Category },
                Picture = await cloudinaryService.UploadFile(productCreateBindingModel.Picture)
            };
            productService.Create(productServiceModel);
            return Redirect("/");
        }

        [HttpGet("Administration/Category/Create")]
        public IActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost("Administraion/Category/Create")]
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