using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.ViewModel.Areas.AdminPanel;
using FoodieBlog.MVCCoreUI.Filters;
using Infrastructure.Enumarations;
using Microsoft.AspNetCore.Mvc;

namespace FoodieBlog.MVCCoreUI.Areas.AdminPanel.ViewComponents
{
    public class sidebarViewComponent :ViewComponent
    {
        private readonly IAdminMenuBs _menuBs;
        private readonly ISessionManager _session;

        public sidebarViewComponent(IAdminMenuBs menuBs, ISessionManager session)
        {
            _menuBs = menuBs;
            _session = session;
        }

        public IViewComponentResult Invoke()
        {
            // TODO: Change SidebarVm to include less stuff, atm you basically send the entity AdminMenu
            List<AdminMenu> menu = _menuBs.GetAll(filter: x => x.Active == true, orderby: x => x.MenuOrder, sorted: Sorted.ASC, Tracking: false, "MenuAuthorizations", "MenuAuthorizations.Role", "MenuAuthorizations.Role.UserRoles", "MenuAuthorizations.Role.UserRoles.User");

            SidebarVm vm = new SidebarVm();
            vm.AdminMenus = menu;

            return View(vm);
        }
    }
}

