using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Areas.AdminPanel;
using FoodieBlog.MVCCoreUI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [AdminFilter]
    public class MenuAuthController : Controller
    {
        private readonly IMenuAuthorizationBs _menuAuthBs;
        private readonly IAdminMenuBs _menuBs;
        private readonly IRoleBs _roleBs;

        public MenuAuthController(IMenuAuthorizationBs menuAuthBs, IAdminMenuBs menuBs, IRoleBs roleBs)
        {
            _menuAuthBs = menuAuthBs;
            _menuBs = menuBs;
            _roleBs = roleBs;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            string[] include = new string[2];
            include = ["Menu", "Role"];


            List<MenuAuthIndexVm> vm = new List<MenuAuthIndexVm>();
            List<MenuAuthorization> menuAuths = await _menuAuthBs.GetAll(includelist:include);

            foreach (var item in menuAuths)
            {
                MenuAuthIndexVm temp = new MenuAuthIndexVm
                {
                    Id = item.Id,
                    MenuName = item.Menu.Header,
                    RoleName = item.Role.RoleName,
                    InsertAuthorization = item.InsertAuthorization,
                    UpdateAuthorization = item.UpdateAuthorization,
                    DeleteAuthorization = item.DeleteAuthorization,
                    SelectAuthorization = item.SelectAuthorization,
                    Active = item.Active
                };
                vm.Add(temp);
            }

            return Json(new { data = vm });
        }

        [HttpPost]
        public async Task<IActionResult> Add(IFormCollection data)
        {
            // This is terrible
            List<AdminMenu> menu = await _menuBs.GetAll();
            List<Role> role = await _roleBs.GetAll();

            MenuAuthorization menuAuth = new MenuAuthorization();

            int menuId = 0;
            int roleId = 0;

            foreach (var item in menu)
            {
                if (data["MenuName"] == item.Header)
                {
                    menuId = item.Id;
                }
            }

            foreach (var item in role)
            {
                if (data["MenuName"] == item.RoleName)
                {
                    roleId = item.Id;
                }
            }

            menuAuth.RoleId = roleId;
            menuAuth.MenuId = menuId;
            menuAuth.SelectAuthorization = bool.Parse(data["SelectAuthorization"]);
            menuAuth.InsertAuthorization = bool.Parse(data["InsertAuthorization"]);
            menuAuth.UpdateAuthorization = bool.Parse(data["UpdateAuthorization"]);
            menuAuth.DeleteAuthorization = bool.Parse(data["DeleteAuthorization"]);
            menuAuth.Active = true;

            await _menuAuthBs.Insert(menuAuth);


            return Json(new { result = true, mesaj = "Menu Authorization is added successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> Update(IFormCollection data)
        {
            int Id = Convert.ToInt32(data["Id"]);
            MenuAuthorization menuAuth = await _menuAuthBs.Get(x => x.Id == Id);

            // Convert active to bool
            if (!String.IsNullOrEmpty(data["Active"]))
            {
                var temp = data["Active"].ToString();
                bool dataActive = bool.Parse(temp);
                menuAuth.Active = dataActive;
            }
            // ---------------

            List<AdminMenu> menu = await _menuBs.GetAll();
            List<Role> role = await _roleBs.GetAll();

            int menuId = 0;
            int roleId = 0;

            foreach (var item in menu)
            {
                if (data["MenuName"] == item.Header)
                {
                    menuId = item.Id;
                }
            }

            foreach (var item in role)
            {
                if (data["MenuName"] == item.RoleName)
                {
                    roleId = item.Id;
                }
            }

            menuAuth.SelectAuthorization = bool.Parse(data["SelectAuthorization"]);
            menuAuth.InsertAuthorization = bool.Parse(data["InsertAuthorization"]);
            menuAuth.UpdateAuthorization = bool.Parse(data["UpdateAuthorization"]);
            menuAuth.DeleteAuthorization = bool.Parse(data["DeleteAuthorization"]);


            await _menuAuthBs.Update(menuAuth);

            //List<MenuAuth> menuAuths = await _menuAuthBs.GetAll();

            //MenuAuthIndexVm model = new MenuAuthIndexVm();

            //model.MenuAuthName = menuAuth.MenuAuthName;
            //model.Email = menuAuth.Email;
            //model.Bio = menuAuth.Bio;
            //model.ProfilePic = menuAuth.ProfilePic;
            //model.Active = menuAuth.Active;

            return Json(new { result = true, message = "MenuAuth is updated successfully"/*, model = model*/ });
        }

        public async Task<IActionResult> Delete(int id)
        {
            MenuAuthorization k = await _menuAuthBs.Get(x => x.Id == id);

            _menuAuthBs.Delete(k);

            return Json(new { result = true, mesaj = "MenuAuth is deleted successfully" });
        }

        public async Task<IActionResult> ActiveInactive(int id, bool active)
        {
            MenuAuthorization k = await _menuAuthBs.Get(x => x.Id == id);
            k.Active = active;
            await _menuAuthBs.Update(k);

            //  Thread.Sleep(2000);
            return Json(new { result = true, mesaj = "Activity is updated successfully" });
        }

        public async Task<IActionResult> GetUser(int id)
        {

            MenuAuthorization k = await _menuAuthBs.Get(x => x.Id == id);


            return Json(new { result = true, model = k });

        }
    }
}
