using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
