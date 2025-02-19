using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Areas.AdminPanel;
using FoodieBlog.MVCCoreUI.Filters;
using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.Controllers
{
    [Area("AdminPanel")]
    [AdminFilter]
    public class RoleController : Controller
    {
        private readonly IRoleBs _roleBs;

        public RoleController(IRoleBs roleBs)
        {
            _roleBs = roleBs;
        }

        public IActionResult Index()
        {
            


            return View();
        }

        [HttpPost]
        public async Task<IActionResult> List()
        {
            List<RoleIndexVm> vm = new List<RoleIndexVm>();
            List<Role> Roles = await _roleBs.GetAll();

            foreach (var item in Roles)
            {
                RoleIndexVm Rolevm = new RoleIndexVm
                {
                    Id = item.Id,
                    RoleName = item.RoleName,
                    Active = item.Active
                };
                vm.Add(Rolevm);
            }

            return Json(new { data = vm });
        }

        [HttpPost]
        public IActionResult Add(IFormCollection data)
        {
            Role Role = new Role();
            Role.RoleName = data["RoleName"];
            Role.Active = true;

            _roleBs.Insert(Role);


            return Json(new { result = true, mesaj = "Role is added successfully" });
        }

        [HttpPost]
        public async Task<IActionResult> Update(IFormCollection data)
        {
            int Id = Convert.ToInt32(data["Id"]);
            Role Role = await _roleBs.Get(x => x.Id == Id);

            // Convert active to bool
            if (!String.IsNullOrEmpty(data["Active"]))
            {
                var temp = data["Active"].ToString();
                bool dataActive = bool.Parse(temp);
                Role.Active = dataActive;
            }
            // ---------------

            Role.RoleName = data["RoleName"];


            await _roleBs.Update(Role);

            List<Role> Roles = await _roleBs.GetAll();

            RoleIndexVm model = new RoleIndexVm();

            model.RoleName = Role.RoleName;
            model.Active = Role.Active;

            return Json(new { result = true, message = "Role is updated successfully", model = model });
        }

        public async Task<IActionResult> Delete(int id)
        {
            Role k = await _roleBs.Get(x => x.Id == id);

            _roleBs.Delete(k);

            return Json(new { result = true, mesaj = "Role is deleted successfully" });
        }

        public async Task<IActionResult> ActiveInactive(int id, bool active)
        {
            Role k = await _roleBs.Get(x => x.Id == id);
            k.Active = active;
            await _roleBs.Update(k);

            //  Thread.Sleep(2000);
            return Json(new { result = true, mesaj = "Activity is updated successfully" });
        }

        public async Task<IActionResult> GetRole(int id)
        {

            Role k = await _roleBs.Get(x => x.Id == id);


            return Json(new { result = true, model = k });

        }
    }
}
