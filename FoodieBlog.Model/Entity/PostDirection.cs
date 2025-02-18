using Infrastructure.Entity;
using System;
using System.Collections.Generic;

namespace FoodieBlog.Model.Entity;

public partial class PostDirection : BaseEntity
{

    public int? PostId { get; set; }

    public string Directions { get; set; }

    public virtual Post Post { get; set; }
}
