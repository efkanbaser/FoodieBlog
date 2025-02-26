using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.ViewModel.Front
{
    public class SignUpVm
    {
        [BindRequired]
        public string UserNameSignUp { get; set; }
        [BindRequired]
        public string PasswordSignUp { get; set; }
        [BindRequired]
        public string ConfirmPassword { get; set; }
        [BindRequired]
        public string Email { get; set; }

    }
}
