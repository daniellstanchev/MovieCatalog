using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MoviesCatalog.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Admin Area HomeController is working! If you see this, Areas are configured correctly.");
        }

        public IActionResult Test()
        {
            return Content("Test method is working!");
        }
    }
}