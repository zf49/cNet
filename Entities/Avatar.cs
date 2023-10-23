using System;
using System.Collections.Generic;

namespace efcore.Entities;

public partial class Avatar
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public bool? IsPredefined { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
