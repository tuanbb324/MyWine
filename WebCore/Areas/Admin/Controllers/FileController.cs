using System.Drawing;
using System.IO;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;
using WebCore.Areas.Admin.Models;

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FileController : Controller
    {
        [HttpPost]
        public IActionResult Upload(FileUploadViewModel model)
        {
           
            if (model.File.Length > 0)
            {
                var uploadDirecotroy = "wwwroot/Uploads";
                var uploadPath = Path.Combine(uploadDirecotroy);

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var fileName = Guid.NewGuid() + Path.GetExtension(model.File.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var strem = System.IO.File.Create(filePath))
                {
                    model.File.CopyTo(strem);
                }
                return Ok(new { StatusCode = 1, Url= "/Uploads/"+ fileName });
            }
            return Ok(new { StatusCode = 0, Url = "" });
        }
        [HttpPost]
        public IActionResult UploadV2(FileUploadViewModel model)
        {

            if (model.File.Length > 0)
            {
                var uploadDirecotroy = "wwwroot/Uploads";
                var uploadPath = Path.Combine(uploadDirecotroy);

                if (!Directory.Exists(uploadPath))
                    Directory.CreateDirectory(uploadPath);

                var fileName = Guid.NewGuid() + Path.GetExtension(model.File.FileName);
                var filePath = Path.Combine(uploadPath, fileName);

                using (var strem = System.IO.File.Create(filePath))
                {
                    model.File.CopyTo(strem);
                }
                return Ok(new { location = "/Uploads/" + fileName });
            }
            return Ok(new { location = "" });
        }
    }
}
