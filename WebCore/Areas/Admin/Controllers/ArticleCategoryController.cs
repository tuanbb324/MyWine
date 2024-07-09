using Microsoft.AspNetCore.Mvc;
using WebCore.Areas.Admin.Models;
using WebCore.Domain.Catalog;
using WebCore.Filters;
using WebCore.Infrastructures.Services.Interfaces.Article;

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TokenFilterAttribute()]
    public class ArticleCategoryController : Controller
    {
        private readonly IArticleCategoryService _articleCategoryService;
        private readonly IArticleService _articleService;
        public ArticleCategoryController(IArticleCategoryService articleCategoryService, IArticleService articleService)
        {
            _articleCategoryService = articleCategoryService;
            _articleService = articleService;
        }
        public IActionResult Index()
        {
            var req = _articleCategoryService.GetAll();
            return View(req);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add(EditArticleCategoryViewModel model)
        {
            try
            {
                ArticleCategory articleCategory = new ArticleCategory()
                {
                    Name = model.Name,
                    Decsription = model.Decsription,
                    SeoUrl = model.SeoUrl,
                    Keywords = model.Keywords,
                    Published = model.Published,
                    Image = model.Image,
                    DateAdded = DateTime.Now,
                    DateModified = DateTime.Now

                };
                var res = _articleCategoryService.Insert(articleCategory);
                if (res > 0)
                {
                    return Ok(new { Status = 1, Message = "Thêm mới thành công" });
                }
            }
            catch (Exception ex)
            {

               
            }
          
            return Ok(new { Status = -1, Message = "Thêm mới không thành công" });
        }

        public IActionResult Edit(int Id)
        {
            var category = _articleCategoryService.GetById(Id);
            if (category == null)
            {
                return NotFound();
            }
            return View(category);
            }
        [HttpPost]  
        public IActionResult Edit(EditArticleCategoryViewModel model)
        {
            ArticleCategory articleCategory = new ArticleCategory()
            {
                Id = model.Id,
                Name = model.Name,
                Decsription = model.Decsription,
                SeoUrl = model.SeoUrl,
                Keywords = model.Keywords,
                Published = model.Published,
                Image = model.Image,
                DateModified = DateTime.Now

            };
            var res = _articleCategoryService.Update(articleCategory);
            if (res > 0)
            {
                return Ok(new { Status = 1, Message = "Cập nhật thành công" });
            }
            return Ok(new { Status = -1, Message = "Cập nhật không thành công" });

        }
    }
}
