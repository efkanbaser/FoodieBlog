using FoodieBlog.MVCCoreUI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.Controllers
{
    public class CategoryController : Controller
    {
        [Area("AdminPanel")]
        [AdminFilter]
        public IActionResult Index()
        {
            // TODO: Add category entity
            return View();
        }
    }
}
