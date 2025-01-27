using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.ViewComponents
{
    public class headerViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
