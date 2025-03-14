﻿using FoodieBlog.Model.Entity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodieBlog.Model.ViewModel.Areas.AdminPanel
{
    public class UserRoleIndexVm
    {
        public int Id { get; set; }
        public bool? Active { get; set; }
        public int? UserId { get; set; }

        public int? RoleId { get; set; }
        public string? UserName { get; set; }

        public string? RoleName { get; set; }

        public List<SelectListItem> RoleList { get; set; }

        public virtual Role Role { get; set; }

        public virtual User User { get; set; }
    }
}
