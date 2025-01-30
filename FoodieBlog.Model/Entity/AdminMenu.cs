using Infrastructure.Entity;
using System;
using System.Collections.Generic;

namespace FoodieBlog.Model.Entity;

public partial class AdminMenu : BaseEntity
{


    public string Header { get; set; }

    public string MenuIcon { get; set; }

    public int? ParentMenuId { get; set; }

    public string Url { get; set; }

    public int? MenuOrder { get; set; }


    public virtual ICollection<AdminMenu> InverseParentMenu { get; set; } = new List<AdminMenu>();

    public virtual ICollection<MenuAuthorization> MenuAuthorizations { get; set; } = new List<MenuAuthorization>();

    public virtual AdminMenu ParentMenu { get; set; }
}
