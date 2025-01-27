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
        //private readonly IMenuBs _menuBs;

        public SessionManager(IHttpContextAccessor httpContextAccessor, IUserBs userBs)
        {
            _httpContextAccessor = httpContextAccessor;
            _userBs = userBs;
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

        //public bool YetkisiVarmi(int MenuId, int KullaniciId)
        //{
        //    Menu menu = _menuBS.Get(x => x.Id == MenuId, false, "MenuYetkis", "MenuYetkis.Rol", "MenuYetkis.Rol.KullaniciRols", "MenuYetkis.Rol.KullaniciRols.Kullanici");

        //    Kullanici k = _kullaniciBS.Get(x => x.Id == KullaniciId, false, "KullaniciRols");

        //    bool yetki = false;

        //    foreach (MenuYetki myetki in menu.MenuYetkis)
        //    {

        //        foreach (KullaniciRol krol in k.KullaniciRols)
        //        {

        //            if (myetki.SelectYetki == true && myetki.RolId == krol.RolId)
        //            {
        //                yetki = true;

        //                break;
        //            }
        //        }

        //    }


        //    return yetki;
        //}
    }
}
