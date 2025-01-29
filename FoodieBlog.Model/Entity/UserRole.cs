using Infrastructure.Entity;
using System;
using System.Collections.Generic;

namespace FoodieBlog.Model.Entity;

public partial class UserRole : BaseEntity
{

    public int? UserId { get; set; }

    public int? RoleId { get; set; }
}
