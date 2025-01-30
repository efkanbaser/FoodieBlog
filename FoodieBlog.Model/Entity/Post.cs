using Infrastructure.Entity;
using System;
using System.Collections.Generic;

namespace FoodieBlog.Model.Entity;

public partial class Post : BaseEntity
{


    public string Title { get; set; }

    public string Contents { get; set; }

    public string Category { get; set; }



    public int? UserId { get; set; }



    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();

    public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();

    public virtual User User { get; set; }
}
