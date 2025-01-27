using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.ViewComponents
{
    public class humbergerViewComponent:ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
