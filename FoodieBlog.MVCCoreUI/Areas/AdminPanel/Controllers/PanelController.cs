using Microsoft.AspNetCore.Mvc;
using FoodieBlog.MVCCoreUI.Filters;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.Controllers
{
    public class PanelController : Controller
    {
        [Area("AdminPanel")]
        [AdminFilter]
        public IActionResult Index()
        {
            return View();
        }
    }
}
