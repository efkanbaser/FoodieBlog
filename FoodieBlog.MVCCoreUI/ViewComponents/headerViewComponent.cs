using FoodieBlog.MVCCoreUI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.ViewComponents
{
    public class headerViewComponent : ViewComponent
    {
        private readonly ISessionManager _session;

        public headerViewComponent(ISessionManager session)
        {
            _session = session;
        }

        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
