using Infrastructure.Entity;
using System;
using System.Collections.Generic;

namespace FoodieBlog.Model.Entity;

public partial class Category : BaseEntity
{

    public string CategoryName { get; set; }

    public virtual ICollection<PostCategory> PostCategories { get; set; } = new List<PostCategory>();
}
