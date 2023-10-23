using System;
using System.Collections.Generic;

namespace efcore.Entities;

public partial class User
{
    public int Id { get; set; }

    public string Username { get; set; } = null!;

    public string Pwd { get; set; } = null!;

    public int? AvatarId { get; set; }

    public int? DetailId { get; set; }

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual Avatar? Avatar { get; set; }

    public virtual ICollection<Comment> Comments { get; set; } = new List<Comment>();

    public virtual UserDetail? Detail { get; set; }
}
