using Microsoft.AspNetCore.Mvc;
using FoodieBlog.MVCCoreUI.Filters;
using Microsoft.Extensions.Configuration;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [AdminFilter]
    public class PanelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
