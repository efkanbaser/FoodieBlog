using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.Statics
{
    public static class SessionKeys
    {
        public static string ActiveUser { get; } = "ACTIVEUSER";

        public static string ActiveAdmin { get; } = "ACTIVEADMIN";

        public static string Captcha { get; set; } = "CAPTCHA";
    }
}
