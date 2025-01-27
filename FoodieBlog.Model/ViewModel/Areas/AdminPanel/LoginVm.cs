using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.ViewModel.Areas.AdminPanel
{
    public class LoginVm
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public bool RememberMe { get; set; }

        public string Captcha { get; set; }
    }
}
