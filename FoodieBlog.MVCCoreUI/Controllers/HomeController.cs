using System.Diagnostics;
using FoodieBlog.MVCCoreUI.Filters;
using FoodieBlog.MVCCoreUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ISessionManager _session;

        public HomeController(ILogger<HomeController> logger, ISessionManager session)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
