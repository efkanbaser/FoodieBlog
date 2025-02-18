using Infrastructure.Entity;
using System;
using System.Collections.Generic;

namespace FoodieBlog.Model.Entity;

public partial class Role : BaseEntity
{

    public string RoleName { get; set; }


    public virtual ICollection<MenuAuthorization> MenuAuthorizations { get; set; } = new List<MenuAuthorization>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
