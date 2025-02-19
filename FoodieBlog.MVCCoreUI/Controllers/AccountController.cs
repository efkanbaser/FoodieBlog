using AutoMapper;
using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Front;
using FoodieBlog.MVCCoreUI.Filters;
using Infrastructure.CrossCuttingConcern.Comunication;
using Infrastructure.CrossCuttingConcern.Crypto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace FoodieBlog.MVCCoreUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserBs _userBs;
        private readonly MailIslemleri _mail;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ISessionManager _session;

        public AccountController(IUserBs userBs, MailIslemleri mail, IConfiguration config, IMapper mapper, ISessionManager session)
        {
            _userBs = userBs;
            _mail = mail;
            _config = config;
            _mapper = mapper;
            _session = session;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInVm vm)
        {
            string decryptedPassword = CryptoManager.AESDecrypt(vm.Password, "efkey");

            User userCheck = await _userBs.Get(x => x.UserName == vm.UserName && x.Password == decryptedPassword, false);

            if (userCheck != null)
            {
                _session.ActiveUser = userCheck;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                TempData["Mesaj"] = "Login failed";
            }

            return View();

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpVm vm)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(y => y.ErrorMessage).ToList();
                return Json(new { result = false, errors = errors, message = "Please check your information" });
            }

            User user = _mapper.Map<User>(vm);

            //User user = new User();
            //user.UserName = UserName;
            user.Password = CryptoManager.AESEncrypt(user.Password, "efkey");
            //user.Email = Email;
            user.UniqueId = Guid.NewGuid();
            user.Active = false;

            user = await _userBs.Insert(user);

            // TODO: Mail onay al
            // TODO: Kullanıcıyı yaratınca ona user roles tablosundan kullanıcı rolü de yarat
            // TODO: Ön yüzden view model yakala

            ////string website = _config.GetSection("websitehost").Value;
            ////_mail.Send(user.Email, "SparkMarket", "Merhaba " + user.UserName + " SparkMarkete  hoşgeldiniz <br><a href='" + website + "/Kullanici/EmailOnay/" + user.UniqueId + "'> Lütfen emailinizi onaylamak için tıklayınız.</a>");


            return Json(new { result = true, message = "Sign up successful" });
        }

        public IActionResult EmailAuth()
        {
            return View();
        }
    }
}
