using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.ViewModel.Areas.AdminPanel
{
    public class RoleIndexVm
    {
        public int Id { get; set; }
        public bool? Active { get; set; }
        public string RoleName { get; set; }
    }
}
