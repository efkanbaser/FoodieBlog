using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.ViewComponents
{
    public class footerViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
