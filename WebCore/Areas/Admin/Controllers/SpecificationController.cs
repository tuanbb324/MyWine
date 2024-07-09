using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
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
    public class SpecificationController : Controller
    {
        private readonly ISpecificationService _articleCategoryService;
        public SpecificationController(ISpecificationService articleCategoryService)
        {
            _articleCategoryService = articleCategoryService;
        }
        public IActionResult Index()
        {
            var data = _articleCategoryService.GetAllSpecifications();
            return View(data);
        }

        public IActionResult Add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Add([FromBody] List<EditSpecificationViewModel> EditSpecificationViewModel)
        {
            List<Specification> articleCategorys = new List<Specification>();
            foreach (var item in EditSpecificationViewModel)
            {
                Specification articleCategory = new Specification()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Description = item.Description,
                    Position = item.Position,
                    Published = item.Published,
                    DateAdded = DateTime.Now,
                    DateModified = DateTime.Now

                };
                articleCategorys.Add(articleCategory);
            }
            var res = _articleCategoryService.InsertAndUpdates(articleCategorys);
            if (res > 0)
            {
                return Ok(new { Status = 1, Message = "Thêm mới thành công" });
            }
            return Ok(new { Status = -1, Message = "Thêm mới không thành công" });
        }

        public IActionResult Edit(int Id)
        {
            return View();
        }
        [HttpPost]
        public IActionResult Edit(EditSpecificationViewModel model)
        {
            Specification articleCategory = new Specification()
            {
                Id = model.Id,
                Name = model.Name,
                Description = model.Description,
                Position = model.Position,
                Published = model.Published,
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

