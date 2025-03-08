using Infrastructure.Entity;
using System;
using System.Collections.Generic;

namespace FoodieBlog.Model.Entity;

public partial class Comment : BaseEntity
{

    public int? PostId { get; set; }

    public int? UserId { get; set; }

    public string Contents { get; set; }


    public virtual Post Post { get; set; }

    public virtual User User { get; set; }
}
