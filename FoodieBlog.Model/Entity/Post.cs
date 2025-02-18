using Infrastructure.Entity;
using System;
using System.Collections.Generic;

namespace FoodieBlog.Model.Entity;

public partial class Post : BaseEntity
{

    public int? UserId { get; set; }

    public string MainImage { get; set; }

    public string Title { get; set; }

    public int? ReadingTime { get; set; }

    public string Contents { get; set; }

    public string SecondaryImage { get; set; }

    public string ServingSize { get; set; }

    public int? PrepTime { get; set; }

    public int? CookTime { get; set; }

    public string MiddleText { get; set; }

    public string DescriptionFirst { get; set; }

    public string MoreDetails { get; set; }

    public string LastText { get; set; }

    public string DescriptionHeader { get; set; }

    public string DescriptionLast { get; set; }

    public string Quote { get; set; }
    public int? PreviousPostId { get; set; }
    public int? NextPostId { get; set; }


    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();

    public virtual ICollection<PostCategory> PostCategories { get; set; } = new List<PostCategory>();

    public virtual ICollection<PostDirection> PostDirections { get; set; } = new List<PostDirection>();

    public virtual ICollection<PostIngredient> PostIngredients { get; set; } = new List<PostIngredient>();

    public virtual ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();

    public virtual User User { get; set; }
}
