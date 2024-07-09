using Microsoft.AspNetCore.Mvc;
using WebCore.Domain.Catalog;
using WebCore.Infrastructures.Services.Catalog;
using WebCore.Infrastructures.Services.Interfaces.Catalog;

namespace WebCore.Controllers
{

    public class MenuController : Controller
    {
        private readonly ICategoryService _categoryService;
        public IActionResult Index()
        {
            var categoryNames = _categoryService.GetAllCategory();
            return View(categoryNames);
        }
    }
}
