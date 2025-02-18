using Infrastructure.Entity;
using System;
using System.Collections.Generic;

namespace FoodieBlog.Model.Entity;

public partial class MenuAuthorization : BaseEntity
{

    public int? MenuId { get; set; }

    public int? RoleId { get; set; }

    public bool? InsertAuthorization { get; set; }

    public bool? UpdateAuthorization { get; set; }

    public bool? DeleteAuthorization { get; set; }

    public bool? SelectAuthorization { get; set; }

    public virtual AdminMenu Menu { get; set; }

    public virtual Role Role { get; set; }
}
