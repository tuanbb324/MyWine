using Microsoft.AspNetCore.Mvc;
using WebCore.Domain.Catalog;
using WebCore.Infrastructures.Services.Interfaces.Article;
using WebCore.Infrastructures.Services.Interfaces.Catalog;
using WebCore.Models;

namespace WebCore.Controllers
{

    public class ArticleController : Controller
    {
        private readonly IArticleCategoryService _articleCategoryService;
        private readonly IArticleService _articleService;
        public ArticleController(IArticleCategoryService articleCategoryService, IArticleService articleService)
        {
            _articleCategoryService = articleCategoryService;
            _articleService = articleService;
        }
        public IActionResult Index(int Id)
        {
            ArticleViewModel articleViewModel = new ArticleViewModel();
            articleViewModel.ArticleCategories = _articleCategoryService.GetAll().ToList();
            articleViewModel.ArticleCategory = _articleCategoryService.GetById(Id);
            articleViewModel.Articles = _articleService.GetByArticleCategory(Id);
            articleViewModel.ArticleSuggesion = _articleService.GetSuggestion();
            return View(articleViewModel);
        }

        public IActionResult Detail(int Id)
        {
            ArticleDetailViewModel articleViewModel = new ArticleDetailViewModel();
            articleViewModel.ArticleCategories = _articleCategoryService.GetAll().ToList();
            articleViewModel.Articles = _articleService.GetById(Id);
            articleViewModel.ArticleCategory = _articleCategoryService.GetById(articleViewModel.Articles.ArticleCategoryId);
            articleViewModel.ArticlePre = _articleService.GetByPre(Id);
            articleViewModel.ArticleNext = _articleService.GetByNext(Id);
            articleViewModel.ArticleSuggesion = _articleService.GetSuggestion();
            return View(articleViewModel);
        }
    }
}
