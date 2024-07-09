using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using WebCore.Areas.Admin.Models;
using WebCore.Domain.Catalog;
using WebCore.Filters;
using WebCore.Infrastructures.Services.Interfaces.Article;
using WebCore.Infrastructures.Services.Interfaces.Catalog;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TokenFilterAttribute()]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;
        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {
            List<Product> products = new List<Product>();
            var req = _productService.AdminGetAll();
            return View(req);
        }

        public IActionResult Add()
        {
            ViewBag.Categories = _categoryService.GetAllCategories().Where(x => x.Published).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(EditProductViewModel model)
        {
            List<string> listImage = new List<string>();
            listImage.Add(model.Image != null ? model.Image : "");
            listImage.Add(model.Image2 != null ? model.Image2 : "");
            listImage.Add(model.Image3 != null ? model.Image3 : "");
            listImage.Add(model.Image4 != null ? model.Image4 : "");
            string stringlist = JsonConvert.SerializeObject(listImage);

            Product articleCategory = new Product()
            {
                Name = model.Name,
                Description = model.Description,
                Content = model.Content,
                SeoUrl = model.SeoUrl,
                MetaKeywords = model.MetaKeywords,
                Published = model.Published,
                Images = stringlist,
                Note = model.Note,
                Enjoy = model.Enjoy,
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now,
                BasicInformation = model.BasicInformation

            };
            var res = _productService.InsertProduct(articleCategory, model.CategoryId);
            if (res > 0)
            {

                return Ok(new { Status = 1, Message = "Thêm mới thành công" });
            }
            return Ok(new { Status = -1, Message = "Thêm mới không thành công" });
        }

        public IActionResult Edit(int Id)
        {
            ViewBag.Categories = _categoryService.GetAllCategories().Where(x => x.Published).ToList();
            var product = _productService.GetProductById(Id);
            return View(product);
        }
        [HttpPost]
        public IActionResult Edit(EditProductViewModel model)
        {
            List<string> listImage = new List<string>();
            listImage.Add(model.Image != null ? model.Image : "");
            listImage.Add(model.Image2 != null ? model.Image2 : "");
            listImage.Add(model.Image3 != null ? model.Image3 : "");
            listImage.Add(model.Image4 != null ? model.Image4 : "");
            string stringlist = JsonConvert.SerializeObject(listImage);
            Product product = new Product()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Content = model.Content,
                SeoUrl = model.SeoUrl,
                MetaKeywords = model.MetaKeywords,
                Published = model.Published,
                Images = stringlist,
                Note = model.Note,
                Enjoy = model.Enjoy,
                DateModified = DateTime.Now,
                BasicInformation =model.BasicInformation

            };
            var res = _productService.UpdateProduct(product, model.CategoryId);
            if (res > 0)
            {
                return Ok(new { Status = 1, Message = "Cập nhật thành công" });
            }
            return Ok(new { Status = -1, Message = "Cập nhật không thành công" });

        }


        [HttpPost]
        public IActionResult Delete(EditProductViewModel model)
        {
            
            var res = _productService.DeleteProduct(model.Id);
            if (res > 0)
            {

                return Ok(new { Status = 1, Message = "Xóa thành công" });
            }
            return Ok(new { Status = -1, Message = "Xóa không thành công" });
        }

    }
}

