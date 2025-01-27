using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.ViewComponents
{
    public class searchViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}