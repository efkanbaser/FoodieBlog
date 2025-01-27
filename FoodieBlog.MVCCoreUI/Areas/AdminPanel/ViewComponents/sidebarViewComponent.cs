using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.ViewComponents
{
    public class sidebarViewComponent :ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}

