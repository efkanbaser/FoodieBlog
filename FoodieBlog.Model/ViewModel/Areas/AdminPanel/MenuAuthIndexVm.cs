using FoodieBlog.Model.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.ViewModel.Areas.AdminPanel
{
    public class MenuAuthIndexVm
    {
        public int Id { get; set; }
        public bool? Active { get; set; }
        public int? MenuId { get; set; }

        public int? RoleId { get; set; }
        public bool? InsertAuthorization { get; set; }

        public bool? UpdateAuthorization { get; set; }

        public bool? DeleteAuthorization { get; set; }

        public bool? SelectAuthorization { get; set; }
        public string MenuName { get; set; }
        public string RoleName { get; set; }

        public virtual AdminMenu Menu { get; set; }

        public virtual Role Role { get; set; }

        public List<SelectListItem> MenuNameList { get; set; }
        public List<SelectListItem> RoleNameList { get; set; }
    }
}
