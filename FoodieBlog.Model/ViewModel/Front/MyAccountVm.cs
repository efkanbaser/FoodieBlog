using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.ViewModel.Front
{
    public class MyAccountVm
    {
        public string UserName { get; set; }
        [BindNever]
        public string Email { get; set; }

        public string Password { get; set; }
        public string Bio { get; set; }
        public string ProfilePic { get; set; }
        public string ConfirmPassword { get; set; }

        [BindNever]
        public string CreatedOn { get; set; }

        public string ErrorMessage { get; set; }
    }
}
