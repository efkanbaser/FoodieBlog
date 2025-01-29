using Infrastructure.Entity;
using System;
using System.Collections.Generic;

namespace FoodieBlog.Model.Entity;

public partial class PostTag : BaseEntity
{

    public int? TagId { get; set; }

    public int? PostId { get; set; }

    public virtual Post Post { get; set; }

    public virtual Tag Tag { get; set; }
}
