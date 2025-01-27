using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.ViewComponents
{
    public class asideViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {

            return View();
        }
    }
}

