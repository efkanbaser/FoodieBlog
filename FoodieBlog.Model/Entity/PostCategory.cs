using Infrastructure.Entity;
using System;
using System.Collections.Generic;

namespace FoodieBlog.Model.Entity;

public partial class PostCategory : BaseEntity
{


    public int CategoryId { get; set; } = 0;

    public int PostId { get; set; } = 0;


    public virtual Category Category { get; set; }

    public virtual Post Post { get; set; }
}
