using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebCore.Domain.Catalog;
using WebCore.Infrastructures.Services.Catalog;
using WebCore.Infrastructures.Services.Interfaces.Article;
using WebCore.Infrastructures.Services.Interfaces.Catalog;
using WebCore.Models;

namespace WebCore.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly ICategoryService _categoryService;
    private readonly IArticleService _articleService;
    public HomeController(ILogger<HomeController> logger , ICategoryService categoryService , IArticleService articleService )
    {
        _logger = logger;
        _categoryService = categoryService;
        _articleService = articleService;
    }

    public IActionResult Index()
    {
        // var data =  _categoryService.GetAllCategories().ToList();
        IDictionary<Category, int> categories = _categoryService.GetAllimage().Select(
            cate => new { Key = cate, Value = 0 }
            ).ToDictionary(item => item.Key, item => item.Value);
        var categories1 = _categoryService.GetAllimage();
        var newProducts = _categoryService.GetListProducts();

        ProductViewModel model = new ProductViewModel()
        {
            Products = newProducts,
            Categories = categories,
            Category = categories1,
            Articles = _articleService.GetSuggestion().Where(x => x.Published).ToList()

    };
        var cate = _categoryService.GetCategoryId().Where(x => x.Published).ToList(); 

        for (int i = 0; i < cate.Count; i++)
        {
            HomeCategoryViewModel homeCategoryViewModel = new HomeCategoryViewModel();
            homeCategoryViewModel.Category = cate[i];
            homeCategoryViewModel.Products = _categoryService.GetProductsByCategoryId(cate[i].Id).Where(x => x.Published).ToList();
            model.CategoryViewModels.Add(homeCategoryViewModel);
        };


        return View(model);
    }

    public IActionResult Privacy()
    {
        var categoryImage = _categoryService.GetAllimage();
        return View(categoryImage);
    }

    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

