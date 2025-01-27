using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.ViewComponents
{
    public class heroViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
