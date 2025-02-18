using Infrastructure.Entity;
using System;
using System.Collections.Generic;

namespace FoodieBlog.Model.Entity;

public partial class User : BaseEntity
{

    public string UserName { get; set; }

    public string Email { get; set; }

    public string Password { get; set; }

    public string Bio { get; set; }

    public string ProfilePic { get; set; }


    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual ICollection<Interaction> Interactions { get; set; } = new List<Interaction>();

    public virtual ICollection<Post> Posts { get; set; } = new List<Post>();

    public virtual ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
}
