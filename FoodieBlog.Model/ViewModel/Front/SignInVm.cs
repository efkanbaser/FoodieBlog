using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.ViewModel.Front
{
    public class SignInVm
    {
        [BindRequired]
        public string UserNameSignIn { get; set; }
        [BindRequired]
        public string PasswordSignIn { get; set; }
    }
}
