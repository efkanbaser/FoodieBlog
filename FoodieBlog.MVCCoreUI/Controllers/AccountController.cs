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
using Infrastructure.CrossCuttingConcern.Converters;
using System.Drawing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using FoodieBlog.Business.Concrete.Base;

namespace FoodieBlog.MVCCoreUI.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserBs _userBs;
        private readonly IRoleBs _roleBs;
        private readonly IUserRoleBs _userRoleBs;
        private readonly MailIslemleri _mail;
        private readonly IConfiguration _config;
        private readonly ISessionManager _session;
        private readonly IPostBs _postBs;

        // to delete or edit posts
        private readonly ICommentBs _commentBs;
        private readonly IPostIngredientBs _postIngredientBs;
        private readonly IPostTagBs _postTagBs;
        private readonly IPostCategoryBs _postCategoryBs;
        private readonly IInteractionBs _interactionBs;
        private readonly IPostDirectionBs _postDirectionBs;


        public AccountController(IUserBs userBs, MailIslemleri mail, IConfiguration config, ISessionManager session, IRoleBs roleBs, IUserRoleBs userRoleBs, IPostBs postBs, ICommentBs commentBs, IPostIngredientBs postIngredientBs, IPostTagBs postTagBs, IPostCategoryBs postCategoryBs, IInteractionBs interactionBs, IPostDirectionBs postDirectionBs)
        {
            _userBs = userBs;
            _mail = mail;
            _config = config;
            _session = session;
            _userRoleBs = userRoleBs;
            _roleBs = roleBs;
            _postBs = postBs;


            _commentBs = commentBs;
            _postIngredientBs = postIngredientBs;
            _postTagBs = postTagBs;
            _postCategoryBs = postCategoryBs;
            _interactionBs = interactionBs;
            _postDirectionBs = postDirectionBs;
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
                    return RedirectToAction("Index", "Home");
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

        public IActionResult Logout()
        {
            _session.ActiveUser = null;
            HttpContext.Response.Cookies.Delete("ActiveUserCookie");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult EmailAuth()
        {
            return View();
        }

        [UserFilter]
        public async Task<IActionResult> MyPosts()
        {
            int id = _session.ActiveUser.Id;

            List<Post> posts = await _postBs.GetAllByActive(x => x.UserId == id);
            List<MyPostsVm> models = new List<MyPostsVm>();

            foreach (var item in posts)
            {
                MyPostsVm model = new MyPostsVm
                {
                    Id = item.Id,
                    Title = item.Title,
                    Href = "/Post/" + item.Url
                };
                models.Add(model);
            }
            return View(models);
        }

        [UserFilter]
        [HttpPost]
        public async Task<IActionResult> DeletePost(int id)
        {
            // this just makes the active false
            if (id != 0)
            {
                //_postBs.DeleteById(id);
                Post post = await _postBs.GetById(id);
                post.Active = false;
                await _postBs.Update(post);

                return Json(new { result = true });
            }

            return Json(new { result = false });
        }

        [UserFilter]
        [HttpPost]
        public async Task<IActionResult> UpdatePost()
        {

            return View();
        }





        [UserFilter]
        public async Task<IActionResult> Profile()
        {
            int id = _session.ActiveUser.Id;

            User user = await _userBs.GetById(id);

            string formattedDate = Converters.FormatNullableDate(user.PublicationDate);

            MyAccountVm model = new MyAccountVm
            {
                UserName = user.UserName,
                Email = user.Email,
                Bio = user.Bio,
                ProfilePic = user.ProfilePic,
                CreatedOn = formattedDate
            };

            return View(model);
        }

        [UserFilter]
        [HttpPost]
        public async Task<IActionResult> Profile([FromForm]MyAccountVm model, IFormFile ProfilePicNew)
        {
            //ModelStateisvalid ekle
            if (ModelState.IsValid)
            {
                int id = _session.ActiveUser.Id;

                User user = await _userBs.GetById(id);

                user.UserName = model.UserName;
                user.Bio = model.Bio;
                user.Password = CryptoManager.SHA256Encrypt(model.Password);
                user.ProfilePic = model.ProfilePic;

                await _userBs.Insert(user);

                return View();
            }
            else
            {
                model.ErrorMessage = "Please check all your information";
                return View();
            }

        }








    }

}
