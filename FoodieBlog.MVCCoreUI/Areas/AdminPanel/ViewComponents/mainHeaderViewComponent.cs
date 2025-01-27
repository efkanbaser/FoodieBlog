using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.ViewComponents
{
    public class mainHeaderViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}

