using Infrastructure.Entity;
using System;
using System.Collections.Generic;

namespace FoodieBlog.Model.Entity;

public partial class PostIngredient : BaseEntity
{


    public int? PostId { get; set; }

    public string Ingredient { get; set; }

    public virtual Post Post { get; set; }
}
