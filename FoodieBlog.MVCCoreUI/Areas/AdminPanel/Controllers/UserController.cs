using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Areas.AdminPanel;
using FoodieBlog.MVCCoreUI.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [AdminFilter]
    public class UserController : Controller
    {
        private readonly IUserBs _userBs;

        public UserController(IUserBs userBs)
        {
            _userBs = userBs;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            List<UserIndexVm> vm = new List<UserIndexVm>();
            List<User> users = await _userBs.GetAll();

            foreach (var item in users)
            {
                UserIndexVm uservm = new UserIndexVm
                {
                    Id = item.Id,
                    UserName = item.UserName,
                    Email = item.Email,
                    Bio = item.Bio,
                    ProfilePic = item.ProfilePic,
                    PublicationDate = item.PublicationDate,
                    Active = item.Active
                };
                vm.Add(uservm);
            }

            return Json(new { data = vm });
        }

        [HttpPost]
        public IActionResult Add(IFormCollection data)
        {
            // REMINDER THAT THIS PART IS SOOOOO BROKEN
            User user = new User();
            user.UserName = data["UserName"];
            user.Email = data["Email"];
            user.Bio = data["Bio"];
            user.ProfilePic = data["ProfilePic"];
            user.Active = true;

            if (data.Files.Count != 0)
            {
                IFormFile file = data.Files[0];
                if (file != null)
                {
                    if (!file.ContentType.Contains("image/"))
                    {
                        return Json(new { result = false, mesaj = "Please select an image" });
                    }
                    if (file.Length > 10485760) // 10MB dan büyük ise
                    {
                        return Json(new { result = false, mesaj = "Your image cannot be larger than 10MB" });
                    }
                    string extension = Path.GetExtension(file.FileName);
                    string newFileName = DateTime.Now.Ticks.ToString() + extension;

                    string uploadpath = Directory.GetCurrentDirectory() + "/wwwroot/images/kategori/" + newFileName;
                    using (FileStream fs = new FileStream(uploadpath, FileMode.Create))
                    {
                        file.CopyTo(fs);
                        user.ProfilePic = "/images/user/" + newFileName;
                    }
                }

            }

            _userBs.Insert(user);


            return Json(new { result = true, mesaj = "User is added successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> Update(IFormCollection data)
        {
            int Id = Convert.ToInt32(data["Id"]);
            User user = await _userBs.Get(x => x.Id == Id);

            // Convert active to bool
            if (!String.IsNullOrEmpty(data["Active"])){
                var temp = data["Active"].ToString();
                bool dataActive = bool.Parse(temp);
                user.Active = dataActive;
            }
            // ---------------
                
            user.UserName = data["UserName"];
            user.Email = data["Email"];
            user.Bio = data["Bio"];
            user.ProfilePic = data["ProfilePic"];


            await _userBs.Update(user);

            List<User> users = await _userBs.GetAll();

            UserIndexVm model = new UserIndexVm();

            model.UserName = user.UserName;
            model.Email = user.Email;
            model.Bio = user.Bio;
            model.ProfilePic = user.ProfilePic;
            model.Active = user.Active;

            return Json(new { result = true, message = "User is updated successfully", model = model });
        }

        public async Task<IActionResult> Delete(int id)
        {
            User k = await _userBs.Get(x => x.Id == id);

            _userBs.Delete(k);

            return Json(new { result = true, mesaj = "User is deleted successfully" });
        }

        public async Task<IActionResult> ActiveInactive(int id, bool active)
        {
            User k = await _userBs.Get(x => x.Id == id);
            k.Active = active;
            await _userBs.Update(k);

            //  Thread.Sleep(2000);
            return Json(new { result = true, mesaj = "Activity is updated successfully" });
        }

        public async Task<IActionResult> GetUser(int id)
        {

            User k = await _userBs.Get(x => x.Id == id);


            return Json(new { result = true, model = k });

        }
    }
}
