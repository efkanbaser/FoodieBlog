using Infrastructure.Entity;
using System;
using System.Collections.Generic;

namespace FoodieBlog.Model.Entity;

public partial class Interaction : BaseEntity
{


    public int? UserId { get; set; }

    public int? PostId { get; set; }

    public byte[] Timestamp { get; set; }



    public virtual Post Post { get; set; }

    public virtual User User { get; set; }
}
