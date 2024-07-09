using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebCore.Infrastructures.Services.Interfaces.Catalog;

namespace WebCore.ViewComponents
{

    public class MenuMobileViewComponent : ViewComponent
    {
        private readonly ICategoryService _categoryService;
        public MenuMobileViewComponent(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var data = _categoryService.GetAllCategory();
            return View(data);
        }

    }
}

