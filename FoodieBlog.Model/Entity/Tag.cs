using Infrastructure.Entity;
using System;
using System.Collections.Generic;

namespace FoodieBlog.Model.Entity;

public partial class Tag : BaseEntity
{

    public string TagName { get; set; }

    public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();
}
