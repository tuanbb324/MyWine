using Microsoft.AspNetCore.Mvc;
using WebCore.Areas.Admin.Models;
using WebCore.Domain.Catalog;
using WebCore.Filters;
using WebCore.Infrastructures.Services.Interfaces.Article;

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    [TokenFilterAttribute()]
    public class ArticleController : Controller
    {
        private readonly IArticleCategoryService _articleCategoryService;
        private readonly IArticleService _articleService;
        public ArticleController(IArticleCategoryService articleCategoryService, IArticleService articleService)
        {
            _articleCategoryService = articleCategoryService;
            _articleService = articleService;
        }
        public IActionResult Index()
        {
            var req = _articleService.AdminGetAll();
            return View(req);
        }

        public IActionResult Add()
        {
            ViewBag.Categories = _articleCategoryService.GetAll().Where(x => x.Published).ToList();
            return View();
        }
        [HttpPost]
        public IActionResult Add(EditArticleViewModel model)
        {
            
            Articles articleCategory = new Articles()
            {
                Name = model.Name,
                Decsription = model.Decsription,
                Content = model.Content,
                SeoUrl = model.SeoUrl,
                Keywords = model.Keywords,
                Published = model.Published,
                Image = model.Image,
                ArticleCategoryId = model.ArticleCategoryId,
                DateAdded = DateTime.Now,
                DateModified = DateTime.Now

            };
            var res = _articleService.Insert(articleCategory);
            if (res > 0)
            {
                return Ok(new { Status = 1, Message = "Thêm mới thành công" });
            }
            return Ok(new { Status = -1, Message = "Thêm mới không thành công" });
        }

        public IActionResult Edit(int Id)
        {
            ViewBag.Categories = _articleCategoryService.GetAll().Where(x => x.Published).ToList();

            var article = _articleService.GetById(Id);
            if (article == null)
            {
                return NotFound();
            }
            return View(article);
        }
        [HttpPost]
        public IActionResult Edit(EditArticleViewModel model)
        {
            Articles articleCategory = new Articles()
            {
                Id = model.Id,
                Name = model.Name,
                Decsription = model.Decsription,
                Content = model.Content,
                SeoUrl = model.SeoUrl,
                Keywords = model.Keywords,
                Published = model.Published,
                Image = model.Image,
                ArticleCategoryId = model.ArticleCategoryId,
                DateModified = DateTime.Now

            };
            var res = _articleService.Update(articleCategory);
            if (res > 0)
            {
                return Ok(new { Status = 1, Message = "Cập nhật thành công" });
            }
            return Ok(new { Status = -1, Message = "Cập nhật không thành công" });

        }
    }
}
