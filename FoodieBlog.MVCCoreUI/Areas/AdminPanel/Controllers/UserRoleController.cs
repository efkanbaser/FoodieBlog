using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Areas.AdminPanel;
using FoodieBlog.MVCCoreUI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [AdminFilter]
    public class UserRoleController : Controller
    {
        private readonly IUserRoleBs _userRoleBs;
        private readonly IRoleBs _roleBs;

        public UserRoleController(IUserRoleBs userRoleBs, IRoleBs roleBs)
        {
            _userRoleBs = userRoleBs;
            _roleBs = roleBs;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            List<UserRoleIndexVm> vm = new List<UserRoleIndexVm>();

            string[] list = new string[2];
            list = ["Role", "User"];

            List<UserRole> UserRoles = await _userRoleBs.GetAll(includelist:list);

            foreach (var item in UserRoles)
            {
                UserRoleIndexVm UserRolevm = new UserRoleIndexVm
                {
                    Id = item.Id,
                    UserName = item.User.UserName,
                    RoleName = item.Role.RoleName,
                    Active = item.Active
                };
                vm.Add(UserRolevm);
            }

            return Json(new { data = vm });
        }

        //[HttpPost]
        //public IActionResult Add(IFormCollection data)
        //{
        //    UserRole UserRole = new UserRole();
        //    UserRole.UserRoleName = data["UserRoleName"];
        //    UserRole.Active = true;

        //    _userRoleBs.Insert(UserRole);


        //    return Json(new { result = true, mesaj = "UserRole is added successfully" });
        //}

        [HttpPost]
        public async Task<IActionResult> Update(IFormCollection data)
        {
            int Id = Convert.ToInt32(data["Id"]);
            UserRole UserRole = await _userRoleBs.Get(x => x.Id == Id);

            // Convert active to bool
            if (!String.IsNullOrEmpty(data["Active"]))
            {
                var temp = data["Active"].ToString();
                bool dataActive = bool.Parse(temp);
                UserRole.Active = dataActive;
            }
            // ---------------

            int? newRoleId = null;
            // Temp fix
            List<Role> roles = await _roleBs.GetAll();
            foreach (var role in roles)
            {
                if(role.RoleName == data["RoleName"])
                {
                    newRoleId = role.Id;
                }
            }

            UserRole.RoleId = newRoleId;


            await _userRoleBs.Update(UserRole);

            List<UserRole> UserRoles = await _userRoleBs.GetAll();

            UserRoleIndexVm model = new UserRoleIndexVm();

            model.UserName = UserRole.User.UserName;
            model.RoleName = UserRole.Role.RoleName;
            model.Active = UserRole.Active;

            return Json(new { result = true, message = "User Role is updated successfully", model = model });
        }

        public async Task<IActionResult> Delete(int id)
        {
            UserRole k = await _userRoleBs.Get(x => x.Id == id);

            _userRoleBs.Delete(k);

            return Json(new { result = true, mesaj = "User Role is deleted successfully" });
        }

        public async Task<IActionResult> ActiveInactive(int id, bool active)
        {
            UserRole k = await _userRoleBs.Get(x => x.Id == id);
            k.Active = active;
            await _userRoleBs.Update(k);

            //  Thread.Sleep(2000);
            return Json(new { result = true, mesaj = "Activity is updated successfully" });
        }

        public async Task<IActionResult> GetUserRole(int id)
        {

            UserRole k = await _userRoleBs.Get(x => x.Id == id);


            return Json(new { result = true, model = k });

        }
    }
}
