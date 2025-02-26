using AutoMapper;
using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Front;
using FoodieBlog.MVCCoreUI.Filters;
using Infrastructure.CrossCuttingConcern.Comunication;
using Infrastructure.CrossCuttingConcern.Crypto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Drawing;

namespace FoodieBlog.MVCCoreUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserBs _userBs;
        private readonly IRoleBs _roleBs;
        private readonly IUserRoleBs _userRoleBs;
        private readonly MailIslemleri _mail;
        private readonly IConfiguration _config;
        private readonly IMapper _mapper;
        private readonly ISessionManager _session;

        public AccountController(IUserBs userBs, MailIslemleri mail, IConfiguration config, IMapper mapper, ISessionManager session, IRoleBs roleBs, IUserRoleBs userRoleBs)
        {
            _userBs = userBs;
            _mail = mail;
            _config = config;
            _mapper = mapper;
            _session = session;
            _userRoleBs = userRoleBs;
            _roleBs = roleBs;
        }

        public async Task<IActionResult> Index()
        {
            string? Id = HttpContext.Request.Cookies["ActiveUserCookie"];
            if (!string.IsNullOrEmpty(Id))
            {
                int id = Convert.ToInt32(Id);
                User u = await _userBs.Get(x => x.Id == id);
                if (u != null)
                {
                    _session.ActiveUser = u;
                    return View();
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignIn(SignInVm vm)
        {
    
            if (!String.IsNullOrEmpty(vm.PasswordSignIn) && !String.IsNullOrEmpty(vm.UserNameSignIn))
            {
                string encryptedPassword = CryptoManager.SHA256Encrypt(vm.PasswordSignIn);

                User userCheck = await _userBs.Get(x => x.UserName == vm.UserNameSignIn && x.Password == encryptedPassword, false);

                if (userCheck != null)
                {
                    // Sets the user in session
                    _session.ActiveUser = userCheck;

                    // Sets the cookie for the user that lasts a day long
                    CookieOptions options = new CookieOptions();
                    options.Expires = DateTime.Now.AddDays(1);
                    HttpContext.Response.Cookies.Append("ActiveUserCookie", userCheck.Id.ToString(), options);


                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["Message"] = "Sign in failed";
                    return RedirectToAction("Index", "Account");
                }

            }
            else
            {
                TempData["Message"] = "Please fill all your sign in information";
                return RedirectToAction("Index", "Account");
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SignUp(SignUpVm vm)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(y => y.ErrorMessage).ToList();
                TempData["Message"] = "Please check your sign up information";
                return RedirectToAction("Index", "Account");
            }


            if (await _userBs.Get(x => x.Email == vm.Email) != null)
            {
                TempData["Message"] = "Please enter an unused email to sign up";
                return RedirectToAction("Index", "Account");

            }

            User user = new User();
            user.UserName = vm.UserNameSignUp;
            user.Email = vm.Email;
            user.Password = CryptoManager.SHA256Encrypt(vm.PasswordSignUp);
            user.UniqueId = Guid.NewGuid();
            user.Active = false;

            user = await _userBs.Insert(user);

            UserRole blogger = new UserRole();

            blogger.UserId = user.Id;
            blogger.RoleId = 3; // ID 3 IS BLOGGER RIGHT NOW 26.02.25

            await _userRoleBs.Insert(blogger);

            // TODO: Mail onay al

            return RedirectToAction("Index", "Home");
        }

        public IActionResult EmailAuth()
        {
            return View();
        }
    }
}
