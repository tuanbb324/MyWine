using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebCore.Domain.Catalog;
using WebCore.Infrastructures.Services.Catalog;
using WebCore.Infrastructures.Services.Interfaces.Catalog;
using WebCore.Models;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCore.Controllers
{
    public class ProductController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;

        public ProductController(ICategoryService categoryService, IProductService productService)
        {
            _categoryService = categoryService;
            _productService = productService;
        }


        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        // GET: Product by category id
        public IActionResult Category(int id)
        {
            var products = _productService.GetProductByCategoryId(id).Where(x => x.Published).ToList();
            var allProducts = _productService.GetAllProducts().Where(x => x.Published).ToList();

            IDictionary<Category, int> categories = _categoryService.GetAllCategories().Where(x => x.Published).ToList().Select(
                cate => new { Key = cate, Value = allProducts.Where(p => p.Categories.Any(pcm => pcm.CategoryId == cate.Id)).Count() }
                ).ToDictionary(item => item.Key, item => item.Value);

            return View("Index", new ProductViewModel() { Products = products, Categories = categories });
        }
        public IActionResult Search(string s)
        {
            var products = _productService.SearchProduct(s).Where(x => x.Published).ToList(); ;
            var allProducts = _productService.GetAllProducts().Where(x => x.Published).ToList(); ;

            IDictionary<Category, int> categories = _categoryService.GetAllCategories().Select(
                cate => new { Key = cate, Value = allProducts.Where(p => p.Categories.Any(pcm => pcm.CategoryId == cate.Id)).Count() }
                ).ToDictionary(item => item.Key, item => item.Value);

            return View("Index", new ProductViewModel() { Products = products, Categories = categories });
        }
        // GET: Detail
        public IActionResult Detail(int id)
        {
            var product = _productService.GetProductById(id);
            var model = new ProductDetailViewModel(product);
            model.Categories = _categoryService.GetCategoryByProductId(product.Id).Where(x => x.Published).ToList(); ;
            var catId = model.Categories.FirstOrDefault().Id;
            var suggestions = _productService.GetProductByCategoryId(catId).Where(x => x.Published).ToList(); ;
            model.ProductSuggestions = suggestions;
            return View(model);

        }
    }
}

