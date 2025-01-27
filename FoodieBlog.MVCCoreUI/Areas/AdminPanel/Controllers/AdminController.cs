using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.ViewModel.Areas.AdminPanel;
using FoodieBlog.MVCCoreUI.Filters;
using Infrastructure.CrossCuttingConcern.Converters;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Drawing;
using System.Drawing.Text;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AdminController : Controller
    {
        private readonly IUserBs _userBs;
        private readonly ISessionManager _session;

        public AdminController(IUserBs userBs, ISessionManager session)
        {
            _userBs = userBs;
            _session = session;
        }   


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            LoginVm vm = new LoginVm();

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginVm user)
        {


            return View();
        }

        public IActionResult GetCaptcha()
        {
            CaptchaGenerator captcha = new CaptchaGenerator(6, "Verdana", 20);
            Bitmap b = captcha.GuvenlikResmiUret();
            _session.Captcha = captcha.olusturanString;

            byte[] imagebyte = Converters.ImageToByteArray(b);


            return File(imagebyte, "image/jpg");
        }
    }
}
