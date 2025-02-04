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
    public class UserRoleController : Controller
    {
        private readonly IUserRoleBs _userRoleBs;
        private readonly IRoleBs _roleBs;
        private readonly IUserBs _userBs;

        public UserRoleController(IUserRoleBs userRoleBs, IRoleBs roleBs, IUserBs userBs)
        {
            _userRoleBs = userRoleBs;
            _roleBs = roleBs;
            _userBs = userBs;
        }

        public async Task<IActionResult> Index()
        {
            UserRoleIndexVm vm = new UserRoleIndexVm();
            List<Role> roles = await _roleBs.GetAll();

            vm.RoleList = roles.Select(x => new SelectListItem() { Text = x.RoleName, Value = x.Id.ToString()}).ToList();

            vm.RoleList.Insert(0, new SelectListItem() { Text = "Please Select A Role", Value = "0", Selected = true });

            return View(vm);
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

            UserRole.RoleId = Convert.ToInt32(data["RoleName"]);

            await _userRoleBs.Update(UserRole);

            return Json(new { result = true, message = "User Role is updated successfully"});
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

            UserRoleIndexVm vm = new UserRoleIndexVm();

            User user = await _userBs.Get(x => x.Id == k.UserId);
            Role role = await _roleBs.Get(x => x.Id == k.RoleId);

            vm.UserName = user.UserName;
            vm.RoleName = role.RoleName;

            return Json(new { result = true, model = vm });

        }
    }
}
