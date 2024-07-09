using System.Drawing;
using System.IO;
using System.Security.Policy;
using Microsoft.AspNetCore.Mvc;
using WebCore.Areas.Admin.Models;
using WebCore.Filters;

namespace WebCore.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AccountController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult login(LoginViewModel model)
        {
            if (model.Username.ToUpper() == "ADMIN" && model.Password == "Abc@12345")
            {


                HttpContext.Session.SetString("LoginSessionName", model.Username.ToUpper());
                return Ok(new { Status = 1 });
            }

            return Ok(new { Status = 0 });
        }
    }
}
