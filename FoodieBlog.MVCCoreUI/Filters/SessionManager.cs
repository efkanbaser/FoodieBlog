using FoodieBlog.Business.Abstract;
using FoodieBlog.Model.Entity;
using FoodieBlog.Model.Statics;
using FoodieBlog.MVCCoreUI.Extensions;
using System.ComponentModel.Design;

namespace FoodieBlog.MVCCoreUI.Filters
{
    public class SessionManager : ISessionManager
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserBs _userBs;
        private readonly IAdminMenuBs _menuBs;

        public SessionManager(IHttpContextAccessor httpContextAccessor, IUserBs userBs, IAdminMenuBs menuBs)
        {
            _httpContextAccessor = httpContextAccessor;
            _userBs = userBs;
            _menuBs = menuBs;
        }

        

        public User ActiveUser
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session.GetObject<User>(SessionKeys.ActiveUser);
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.SetObject(SessionKeys.ActiveUser, value);
            }
        }
        public User ActiveAdmin {
            get
            {
                return _httpContextAccessor.HttpContext.Session.GetObject<User>(SessionKeys.ActiveAdmin);
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.SetObject(SessionKeys.ActiveAdmin, value);
            }
        }
        public string Captcha
        {
            get
            {
                return _httpContextAccessor.HttpContext.Session.GetObject<string>(SessionKeys.Captcha);
            }
            set
            {
                _httpContextAccessor.HttpContext.Session.SetObject(SessionKeys.Captcha, value);
            }
        }

        public async Task<bool> IsAllowed(int MenuId, int UserId)
        {
            AdminMenu menu = await _menuBs.Get(x => x.Id == MenuId, false, "MenuAuthorizations", "MenuAuthorizations.Role", "MenuAuthorizations.Role.UserRoles", "MenuAuthorizations.Role.UserRoles.User");

            User user = await _userBs.Get(x => x.Id == UserId, false, "UserRoles");

            bool auth = false;

            foreach (MenuAuthorization mauth in menu.MenuAuthorizations)
            {
                foreach (UserRole urole in user.UserRoles)
                {
                    if (mauth.SelectAuthorization == true && mauth.RoleId == urole.RoleId)
                    {
                        auth = true;

                        break;
                    }
                }
            }

            return auth;
        }

    }
}
