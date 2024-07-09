using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebCore.Areas.Admin.Models;
using WebCore.Domain.Catalog;
using WebCore.Filters;
using WebCore.Infrastructures.Services.Catalog;
using WebCore.Infrastructures.Services.Interfaces.Article;
using WebCore.Infrastructures.Services.Interfaces.Catalog;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TokenFilterAttribute()]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IArticleService _articleService;
        public CategoryController(ICategoryService categoryService, IArticleService articleService)
        {
            _articleService = articleService;
            _categoryService = categoryService;
        }
        // GET: /<controller>/
        public IActionResult Index()
        {

            List<Category> lst = new List<Category>();
            var req = _categoryService.AdminGetAll();
            return View(req);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(EditCategoryViewModel model)
        {
            if (model.Image.Length > 0)
            {
                var filePath = Path.Combine("Upload",
                    Path.GetRandomFileName());

                using (var stream = System.IO.File.Create(filePath))
                {
                 //   model.Image.CopyTo(stream);
                }
            }
            return Ok();
        }
        public IActionResult Edit(int Id)
        {
            var category = _categoryService.GetAllCategoryAdmin(Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }
        [HttpPost]
        public IActionResult Edit(EditCategoryViewModel model)
        {
            Category Category = new Category()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                SeoUrl = model.SeoUrl,
                MetaTitle = model.MetaTitle,
                MetaKeywords = model.MetaKeywords,
                MetaDescription = model.MetaDescription,
                ParentCategoryId = model.ParentCategoryId,
                Published = model.Published,
                Image = model.Image,
                IsShowMenu = model.IsShowMenu,
                IsShowHome = model.IsShowHome,
                DateModified = DateTime.Now,
                DateAdded = DateTime.Now,



            };
            var res = _categoryService.Update(Category);
            if (res > 0)
            {
                return Ok(new { Status = 1, Message = "Cập nhật thành công" });
            }
            return Ok(new { Status = -1, Message = "Cập nhật không thành công" });

        }
        [HttpPost]
        public IActionResult Delete(EditCategoryViewModel model)
        {

            var res = _categoryService.Delete(model.Id);
            if (res > 0)
            {

                return Ok(new { Status = 1, Message = "Xóa thành công" });
            }
            return Ok(new { Status = -1, Message = "Xóa không thành công" });
        }
    }
}

