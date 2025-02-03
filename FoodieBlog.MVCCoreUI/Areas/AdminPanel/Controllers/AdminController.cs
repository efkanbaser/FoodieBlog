using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.ViewModel.Areas.AdminPanel;
using FoodieBlog.MVCCoreUI.Filters;
using Infrastructure.CrossCuttingConcern.Converters;
using Infrastructure.CrossCuttingConcern.Crypto;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Drawing;
using System.Drawing.Text;
using FoodieBlog.Model.Entity;
using Azure.Identity;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    public class AdminController : Controller
    {
        private readonly IUserBs _userBs;
        private readonly ISessionManager _session;
        private readonly IAdminMenuBs _menuBs;

        public AdminController(IUserBs userBs, ISessionManager session, IAdminMenuBs menuBs)
        {
            _userBs = userBs;
            _session = session;
            _menuBs = menuBs;
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
        public async Task<IActionResult> Login(LoginVm user)
        {
            // TODO: Password Crypto
            // TODO: Remember Me button function with cookies
            // TODO: are DTO's useful anywhere here? 

            user.Email = "eb@gmail.com";
            user.Password = "1234@efkan";

            //string CryptoPassword = CryptoManager.SHA256Encrypt(user.Password);

            User user1 = await _userBs.Get(x => x.Email == user.Email && x.Password == user.Password, false, "UserRoles", "UserRoles.Role");

            if(user1 != null)
            {
                _session.ActiveAdmin = user1;
                return RedirectToAction("Index", "Panel");
            }
            else
            {
                TempData["Mesaj"] = "Login failed";
            }

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
